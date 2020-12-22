using System;
using System.Collections.ObjectModel;
using System.ComponentModel;  

    
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using ATIS.Ui.Views.Database.DatabaseHelper;
using log4net;
using Microsoft.EntityFrameworkCore;          
    
using System.Collections.Generic;  
    
         //    UserProfilesViewModel Skriptdatum:   26.02.2019  10:32    

namespace ATIS.Ui.Views.Database.ListDetails
{     
    
    public class UserProfilesViewModel : ViewModelBase                     
    {  
        // Version with Generic Unit Of Work and AtisDbContext for general use   
         
        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(typeof(UserProfilesViewModel));
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly CrudFunctions _extCrud = new CrudFunctions();

        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private readonly GenericMessageBoxes<TblUserProfile> _genUserProfileMessageBoxes = new GenericMessageBoxes<TblUserProfile>();
        private readonly GenericMessageBoxes<NULL> _genNULLMessageBoxes = new GenericMessageBoxes<NULL>();
        private readonly GenericMessageBoxes<NULL> _genNULLMessageBoxes = new GenericMessageBoxes<NULL>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genExpertMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genSourceMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genAuthorMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl93Comment> _genCommentMessageBoxes = new GenericMessageBoxes<Tbl93Comment>();
        private int _position;   
         
        #endregion [Private Data Members]               
      
        #region [Constructor]

        public UserProfilesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {          
      
                 GetValueRole();
                 GetValueGender();
                 GetValueTitle();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion "Constructor"                     
 

 //    Part 1    

         

        #region [Commands UserProfile]

        private RelayCommand _getUserProfilesByNameOrIdCommand;
        public ICommand GetUserProfilesByNameOrIdCommand => _getUserProfilesByNameOrIdCommand ??= new RelayCommand(delegate {ExecuteGetUserProfilesByNameOrId(SearchUserProfileName); });    
             
        private RelayCommand _addUserProfileCommand;
        public ICommand AddUserProfileCommand => _addUserProfileCommand ??= new RelayCommand(delegate { ExecuteAddUserProfile(null); });

        private RelayCommand _copyUserProfileCommand;
        public ICommand CopyUserProfileCommand => _copyUserProfileCommand ??= new RelayCommand(delegate { ExecuteCopyUserProfile(null); });      
             
        private RelayCommand _deleteUserProfileCommand;
        public ICommand DeleteUserProfileCommand => _deleteUserProfileCommand ??= new RelayCommand(delegate { ExecuteDeleteUserProfile(SearchUserProfileName); });    
             
        private RelayCommand _saveUserProfileCommand;
        public ICommand SaveUserProfileCommand => _saveUserProfileCommand ??= new RelayCommand(delegate { ExecuteSaveUserProfile(SearchUserProfileName); });    

        #endregion [Commands UserProfile]       

UserProfilesView = CollectionViewSource.GetDefaultView(TblUserProfilesList);
            UserProfilesView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
        
        private void ExecuteAddUserProfile(object o)
        {
            TblUserProfilesList.Insert(0, new TblUserProfile {   LastName = CultRes.StringsRes.DatasetNew}  );

            TblCountriesAllList = new ObservableCollection<TblCountry>(_businessLayer.ListTblCountries());
            UserProfilesView = CollectionViewSource.GetDefaultView(TblUserProfilesList);
            UserProfilesView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
     
        private void ExecuteCopyUserProfile(object o)
        {
            if (_genUserProfileMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTblUserProfile)) return;

            TblUserProfilesList = _extCrud.CopyUserProfile(CurrentTblUserProfile);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            UserProfilesView = CollectionViewSource.GetDefaultView(TblUserProfilesList);
            UserProfilesView.MoveCurrentToFirst();
        }                         
     
        private void ExecuteDeleteUserProfile(string searchName)
        {
            if (_genUserProfileMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTblUserProfile)) return;               
 
    
            //check if in NULL connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return

            NULLList = _extCrud.SearchForConnectedDatasetsWithUserProfileIdInTableNULL(CurrentTblUserProfile);     
     
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(NULLList.Count, "NULL")) return;

            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithUserProfileIdInTableReference(CurrentTblUserProfile);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + 
                                              CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extCrud.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithUserProfileIdInTableComment(CurrentTblUserProfile);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extCrud.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }
            try
            {
                var userprofile= _uow.TblUserProfiles.GetById(CurrentTblUserProfile.UserProfileId);
                if (userprofile!= null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + 
                                          CurrentTblUserProfile.UserProfileName)) return;

                    _extCrud.DeleteUserProfile(userprofile);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, 
                                         CurrentTblUserProfile.UserProfileName);
                }
                else 
                        _allMessageBoxes.InfoMessageBox("Not To Delete", 
                                         CultRes.StringsRes.DeleteCan + " " + CurrentTblUserProfile.UserProfileName + " " + 
                                         CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGetUserProfilesByNameOrId(searchName);

            UserProfilesView.MoveCurrentToFirst();
        }                
     
        private void ExecuteSaveUserProfile(string searchName)
        {
            if (_genUserProfileMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTblUserProfile)) return;      
       
            //Combobox select NULLID  may be not 0
            if (CurrentTblUserProfile.NULLId == 0)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }     
       
        private void SaveUserProfile(object o)
        {
        }
        #endregion "Public Commands"                   
 
 

 //    Part 2    

     
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            ExecuteGetUserProfilesByNameOrId(searchName);
            UserProfilesView.MoveCurrentToPosition(_position);
        }

        #endregion "Public Commands"                  
                                                          

 //    Part 3    

                                                          



 //    Part 4    

                                                          


 //    Part 5    

                                                          
                      
 //    Part 6    

 
            

 //    Part 7    

 

 //    Part 8    

 
             
 //    Part 9    


     
        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        #endregion "Public Commands Connected Tables by DoubleClick"

        #region "Public Method Connected Tables by DoubleClick"

        private void GetConnectedTablesById(object o)
        {           
     
        }

        #endregion "Public Method Connected Tables by DoubleClick"     
 


 //    Part 10    

     
        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        private int _selectedMainSubRefTabIndex;
        private int _selectedDetailTabIndex;
        private int _selectedDetailSubRefTabIndex;

        public  int SelectedMainTabIndex
        {
            get => _selectedMainTabIndex; 
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value; RaisePropertyChanged("");        
     
                if (_selectedMainTabIndex == 0)             
                {
                    if (CurrentTblUserProfile != null)
                    {
                        NULLList = _extCrud.GetCollectionFromNULLIdOrderBy<NULL>(CurrentTblUserProfile.NULLId);

                        NULLAllList = _extCrud.GetCollectionAllOrderBy<NULL>("");

                        View = CollectionViewSource.GetDefaultView(NULLList);
                        View.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }         
     
                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTblUserProfile != null)
                    {
                        NULLList = _extCrud.GetCollectionFromUserProfileIdOrderBy<NULL>(CurrentTblUserProfile.UserProfileId);

                        TblUserProfilesAllList = _extCrud.GetCollectionAllOrderBy<TblUserProfile>("userprofile");

                        View = CollectionViewSource.GetDefaultView(NULLList);
                        View.Refresh();
                    }
                    SelectedDetailTabIndex = 2;   
               }      
     
                if (_selectedMainTabIndex == 2)
                {
                        SelectedDetailTabIndex = 3;
                        SelectedMainSubRefTabIndex = 0;                  
                }           
     
                if (_selectedMainTabIndex == 3)
                {
                    if (CurrentTblUserProfile != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromUserProfileIdOrderBy<Tbl93Comment>(CurrentTblUserProfile.UserProfileId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedDetailTabIndex = 6;
                }        
     
            }
        }

        public  int SelectedDetailTabIndex
        {
            get => _selectedDetailTabIndex; 
            set
            {
                if (value == _selectedDetailTabIndex) return;
                _selectedDetailTabIndex = value;    RaisePropertyChanged("");       
     
                if (_selectedDetailTabIndex == 0)
                {
                    if (CurrentTblUserProfile != null)
                    {
                        NULLList = _extCrud.GetCollectionFromNULLIdOrderBy<NULL>(CurrentTblUserProfile.NULLId);

                        View = CollectionViewSource.GetDefaultView(NULLList);
                        View.Refresh();
                    }
                    SelectedMainTabIndex = 0;  
               }     
     
                if (_selectedDetailTabIndex == 1)                
                {
                    SelectedMainTabIndex = 0;
                }    
     
                if (_selectedDetailTabIndex == 2)                
                {
                    if (CurrentTblUserProfile != null)
                    {
                        NULLList = _extCrud.GetCollectionFromUserProfileIdOrderBy<NULL>(CurrentTblUserProfile.UserProfileId);

                        TblUserProfilesAllList = _extCrud.GetCollectionAllOrderBy<TblUserProfile>("userprofile");

                        View = CollectionViewSource.GetDefaultView(NULLList);
                        View.Refresh();
                    }
                    SelectedMainTabIndex = 1;
               }    
     
                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTblUserProfile != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromUserProfileIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTblUserProfile.UserProfileId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }        
     
                if (_selectedDetailTabIndex == 4)
                {
                    if (CurrentTblUserProfile != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromUserProfileIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTblUserProfile.UserProfileId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 1;
                }        
     
                if (_selectedDetailTabIndex == 5)
                {
                    if (CurrentTblUserProfile != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromUserProfileIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTblUserProfile.UserProfileId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 2;
                }       
     
                if (_selectedDetailTabIndex == 6)
                {
                    if (CurrentTblUserProfile != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromUserProfileIdOrderBy<Tbl93Comment>(CurrentTblUserProfile.UserProfileId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                }       
     
                if (_selectedDetailTabIndex == 7)
                {
                    if (CurrentTblUserProfile != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromUserProfileIdOrderBy<Tbl93Comment>(CurrentTblUserProfile.UserProfileId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedMainTabIndex = 4;
                }       
     
            }
        }

        public int SelectedMainSubRefTabIndex
        {
            get => _selectedMainSubRefTabIndex;
            set
            {
                if (value == _selectedMainSubRefTabIndex) return;
                _selectedMainSubRefTabIndex = value;  RaisePropertyChanged("");     
     
                if (_selectedMainSubRefTabIndex == 0)
                {
                    if (CurrentTblUserProfile != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromUserProfileIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTblUserProfile.UserProfileId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                }        
     
                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (CurrentTblUserProfile != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromUserProfileIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTblUserProfile.UserProfileId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 2;
                }      
     
                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (CurrentTblUserProfile != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromUserProfileIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTblUserProfile.UserProfileId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedDetailTabIndex = 5;
                    SelectedMainTabIndex = 2;
                }      
                     
            }
        }    
        #endregion "Public Commands to open Detail TabItems"          
 

 //    Part 11    

        
        #region "Public Properties TblUserProfile"

        private string _searchEmail = "";
        public string SearchEmail
        {
            get => _searchEmail; 
            set { _searchEmail = value; RaisePropertyChanged();  }
        }

        public  ICollectionView UserProfilesView;
        private   TblUserProfile CurrentTblUserProfile => UserProfilesView?.CurrentItem as TblUserProfile;

        private ObservableCollection<TblUserProfile> _tblUserProfilesList;
        public  ObservableCollection<TblUserProfile> TblUserProfilesList
        {
            get => _tblUserProfilesList; 
            set {  _tblUserProfilesList = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<TblUserProfile> _tblUserProfilesAllList;
        public  ObservableCollection<TblUserProfile> TblUserProfilesAllList
        {
            get => _tblUserProfilesAllList; 
            set {  _tblUserProfilesAllList = value; RaisePropertyChanged();   }
        }

        private string _passwordBox;
        public string PasswordBox
        {
            get => _passwordBox;

            set { _passwordBox = value; RaisePropertyChanged("PasswordBox"); }
        }

        private void GetValueRole()
        {
            _roles = new List<Role>()
            {
                new Role {Name = "Administrator"},
                new Role {Name = "Developer"},
                new Role {Name = "Biologist"},
                new Role {Name = "Zoologist"},
                new Role {Name = "User"},};

            _selectedRole = new Role();
        }

        private void GetValueTitle()
        {
            _titles = new List<Title>()
            {
                new Title {Name = ""},
                new Title {Name = "Dr."},
                new Title {Name = "Prof."},
                new Title {Name = "Dipl.-Ing."},
                new Title {Name = "Ing. grad"},
                new Title {Name = "Dr. Ing"}
            };

            _selectedTitle = new Title();
        }

        private void GetValueGender()
        {
            _genders = new List<Gender>()
            {
                new Gender {Name = "Female"},
                new Gender {Name = "Male"}
            };

            _selectedGender = new Gender();
        }

        private ObservableCollection<TblCountry> _tblCountriesAllList;
        public ObservableCollection<TblCountry> TblCountriesAllList
        {
            get => _tblCountriesAllList;
            set { _tblCountriesAllList = value; RaisePropertyChanged(); }
        }

        //-----------------------------------------------------------
        private List<Role> _roles;
        public List<Role> Roles
        {
            get => _roles;
            set { _roles = value; RaisePropertyChanged(); }
        }

        private Role _selectedRole;
        public Role SelectedRole
        {
            get => _selectedRole;
            set { _selectedRole = value; RaisePropertyChanged(); }
        }

        private List<Gender> _genders;
        public List<Gender> Genders
        {
            get => _genders;
            set { _genders = value; RaisePropertyChanged(); }
        }

        private Gender _selectedGender;
        public Gender SelectedGender
        {
            get => _selectedGender;
            set { _selectedGender = value; RaisePropertyChanged(); }
        }

        private List<Title> _titles;
        public List<Title> Titles
        {
            get => _titles;
            set { _titles = value; RaisePropertyChanged(); }
        }

        private Title _selectedTitle;
        public Title SelectedTitle
        {
            get => _selectedTitle;
            set { _selectedTitle = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"      


        //    Part 12    



        #region "Private Classes"

        public class Gender
        {
            public string Name
            {
                get;
                set;
            }
        }
        public class Title
        {
            public string Name
            {
                get;
                set;
            }
        }

        public class Role
        {
            public string Name
            {
                get;
                set;
            }
        }

        #endregion "Private Methods"  
 

   }
}   
