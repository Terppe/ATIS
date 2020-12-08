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
    
         //    RefAuthorsViewModel Skriptdatum:  30.03.2019  10:32    

namespace ATIS.Ui.Views.Database.ListDetails
{     
    
    public class RefAuthorsViewModel : ViewModelBase                     
    {  
        // Version with Generic Unit Of Work and AtisDbContext for general use   
         
        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(typeof(RefAuthorsViewModel));
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly CrudFunctions _extCrud = new CrudFunctions();

        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private readonly GenericMessageBoxes<Tbl90RefAuthor> _genRefAuthorMessageBoxes = new GenericMessageBoxes<Tbl90RefAuthor>();
        private readonly GenericMessageBoxes<NULL> _genNULLMessageBoxes = new GenericMessageBoxes<NULL>();
        private readonly GenericMessageBoxes<NULL> _genFiSpeciesMessageBoxes = new GenericMessageBoxes<NULL>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genExpertMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genSourceMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genAuthorMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl93Comment> _genCommentMessageBoxes = new GenericMessageBoxes<Tbl93Comment>();
        private int _position;   
         
        #endregion [Private Data Members]               
      
        #region [Constructor]

        public RefAuthorsViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {          
        
                // Code runs "for real" 
                Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor>();    
            }
        }     
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]         
 

 //    Part 1    

         

        #region [Commands RefAuthor]

        private RelayCommand _getRefAuthorsByNameOrIdCommand;
        public ICommand GetRefAuthorsByNameOrIdCommand => _getRefAuthorsByNameOrIdCommand ??= new RelayCommand(delegate {ExecuteGetRefAuthorsByNameOrId(SearchRefAuthorName); });    
             
        private RelayCommand _addRefAuthorCommand;
        public ICommand AddRefAuthorCommand => _addRefAuthorCommand ??= new RelayCommand(delegate { ExecuteAddRefAuthor(null); });

        private RelayCommand _copyRefAuthorCommand;
        public ICommand CopyRefAuthorCommand => _copyRefAuthorCommand ??= new RelayCommand(delegate { ExecuteCopyRefAuthor(null); });      
             
        private RelayCommand _deleteRefAuthorCommand;
        public ICommand DeleteRefAuthorCommand => _deleteRefAuthorCommand ??= new RelayCommand(delegate { ExecuteDeleteRefAuthor(SearchRefAuthorName); });    
             
        private RelayCommand _saveRefAuthorCommand;
        public ICommand SaveRefAuthorCommand => _saveRefAuthorCommand ??= new RelayCommand(delegate { ExecuteSaveRefAuthor(SearchRefAuthorName); });    

        #endregion [Commands RefAuthor]       

RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
        
        private void ExecuteAddRefAuthor(object o)
        {
            Tbl90RefAuthorsList.Insert(0, new Tbl90RefAuthor {   RefAuthorName = CultRes.StringsRes.DatasetNew}  );

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
     
        private void ExecuteCopyRefAuthor(object o)
        {
            if (_genRefAuthorMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90RefAuthor)) return;

            Tbl90RefAuthorsList = _extCrud.CopyRefAuthor(CurrentTbl90RefAuthor);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.MoveCurrentToFirst();
        }                         
     
        private void ExecuteDeleteRefAuthor(string searchName)
        {
            if (_genRefAuthorMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90RefAuthor)) return;               
 
    
            //check if in Tbl69FiSpeciesses connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return

            Tbl69FiSpeciessesList = _extCrud.SearchForConnectedDatasetsWithRefAuthorIdInTableFiSpecies(CurrentTbl90RefAuthor);     
     
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl69FiSpeciessesList.Count, "FiSpecies")) return;

            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithRefAuthorIdInTableReference(CurrentTbl90RefAuthor);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extCrud.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithRefAuthorIdInTableComment(CurrentTbl90RefAuthor);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extCrud.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }
            try
            {
                var refAuthor= _uow.Tbl90RefAuthors.GetById(CurrentTbl90RefAuthor.RefAuthorId);
                if (refAuthor!= null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefAuthor.RefAuthorName)) return;

                    _extCrud.DeleteRefAuthor(refAuthor);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl90RefAuthor.RefAuthorName);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefAuthor.RefAuthorName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGetRefAuthorsByNameOrId(searchName);

            RefAuthorsView.MoveCurrentToFirst();
        }                
     
        private void ExecuteSaveRefAuthor(string searchName)
        {
            if (_genRefAuthorMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90RefAuthor)) return;      
       
            //Combobox select NULLID  may be not 0
            if (CurrentTbl90RefAuthor.NULLId == 0)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }     
       
        private void SaveRefAuthor(object o)
        {
        }
        #endregion "Public Commands"                   
 
 

 //    Part 2    

     
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            ExecuteGetRefAuthorsByNameOrId(searchName);
            RefAuthorsView.MoveCurrentToPosition(_position);
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

        public  int SelectedMainTabIndex
        {
            get => _selectedMainTabIndex; 
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value; RaisePropertyChanged("");        
     
                if (_selectedMainTabIndex == 0)             
                {
                    if (CurrentTbl90RefAuthor != null)
                    {
                        NULLList = _extCrud.GetCollectionFromNULLIdOrderBy<NULL>(CurrentTbl90RefAuthor.NULLId);

                        NULLAllList = _extCrud.GetCollectionAllOrderBy<NULL>("");

                        View = CollectionViewSource.GetDefaultView(NULLList);
                        View.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }         
     
                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl90RefAuthor != null)
                    {
                        Tbl69FiSpeciessesList = _extCrud.GetCollectionFromRefAuthorIdOrderBy<NULL>(CurrentTbl90RefAuthor.RefAuthorId);

                        Tbl90RefAuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("refAuthor");

                        View = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
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
                    if (CurrentTbl90RefAuthor != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromRefAuthorIdOrderBy<Tbl93Comment>(CurrentTbl90RefAuthor.RefAuthorId);

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
                    if (CurrentTbl90RefAuthor != null)
                    {
                        NULLList = _extCrud.GetCollectionFromNULLIdOrderBy<NULL>(CurrentTbl90RefAuthor.NULLId);

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
                    if (CurrentTbl90RefAuthor != null)
                    {
                        Tbl69FiSpeciessesList = _extCrud.GetCollectionFromRefAuthorIdOrderBy<NULL>(CurrentTbl90RefAuthor.RefAuthorId);

                        Tbl90RefAuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("refAuthor");

                        View = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                        View.Refresh();
                    }
                    SelectedMainTabIndex = 1;
               }    
     
                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTbl90RefAuthor != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromRefAuthorIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90RefAuthor.RefAuthorId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }        
     
                if (_selectedDetailTabIndex == 4)
                {
                    if (CurrentTbl90RefAuthor != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromRefAuthorIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90RefAuthor.RefAuthorId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 1;
                }        
     
                if (_selectedDetailTabIndex == 5)
                {
                    if (CurrentTbl90RefAuthor != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromRefAuthorIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90RefAuthor.RefAuthorId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 2;
                }       
     
                if (_selectedDetailTabIndex == 6)
                {
                    if (CurrentTbl90RefAuthor != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromRefAuthorIdOrderBy<Tbl93Comment>(CurrentTbl90RefAuthor.RefAuthorId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                }       
     
                if (_selectedDetailTabIndex == 7)
                {
                    if (CurrentTbl90RefAuthor != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromRefAuthorIdOrderBy<Tbl93Comment>(CurrentTbl90RefAuthor.RefAuthorId);

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
                    if (CurrentTbl90RefAuthor != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromRefAuthorIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90RefAuthor.RefAuthorId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                }        
     
                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (CurrentTbl90RefAuthor != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromRefAuthorIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90RefAuthor.RefAuthorId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 2;
                }      
     
                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (CurrentTbl90RefAuthor != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromRefAuthorIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90RefAuthor.RefAuthorId);

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

     
        #region "Public Properties Tbl90RefAuthor"

        private string _searchRefAuthorName = "";
        public string SearchRefAuthorName
        {
            get => _searchRefAuthorName; 
            set { _searchRefAuthorName = value; RaisePropertyChanged("");  }
        }

        public  ICollectionView RefAuthorsView;
        private   Tbl90RefAuthor CurrentTbl90RefAuthor => RefAuthorsView?.CurrentItem as Tbl90RefAuthor;

        private ObservableCollection<Tbl90RefAuthor> _tbl90RefAuthorsList;
        public  ObservableCollection<Tbl90RefAuthor> Tbl90RefAuthorsList
        {
            get => _tbl90RefAuthorsList; 
            set {  _tbl90RefAuthorsList = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<Tbl90RefAuthor> _tbl90RefAuthorsAllList;
        public  ObservableCollection<Tbl90RefAuthor> Tbl90RefAuthorsAllList
        {
            get => _tbl90RefAuthorsAllList; 
            set {  _tbl90RefAuthorsAllList = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<NULL> NULLAllList;
        public  ObservableCollection<NULL> Tbl69FiSpeciessesAllList
        {
            get => NULLAllList; 
            set {  NULLAllList = value; RaisePropertyChanged("");   }
        }

        #endregion "Public Properties"   
 

   }
}   
