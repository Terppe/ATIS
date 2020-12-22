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
    
         //    CountriesViewModel Skriptdatum:   29.11.2018 12:32      

namespace ATIS.Ui.Views.Database.ListDetails
{     
    
    public class CountriesViewModel : ViewModelBase                     
    {  
        // Version with Generic Unit Of Work and AtisDbContext for general use   
         
        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(typeof(CountriesViewModel));
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly CrudFunctions _extCrud = new CrudFunctions();

        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private readonly GenericMessageBoxes<TblCountry> _genCountryMessageBoxes = new GenericMessageBoxes<TblCountry>();
        private readonly GenericMessageBoxes<NULL> _genNULLMessageBoxes = new GenericMessageBoxes<NULL>();
        private readonly GenericMessageBoxes<NULL> _genNULLMessageBoxes = new GenericMessageBoxes<NULL>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genExpertMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genSourceMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genAuthorMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl93Comment> _genCommentMessageBoxes = new GenericMessageBoxes<Tbl93Comment>();
        private int _position;   
         
        #endregion [Private Data Members]               
      
        #region [Constructor]

        public CountriesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {          
        
                // Code runs "for real" 
                TblCountriesList = new ObservableCollection<TblCountry>();    
            }
        }     
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]         
 

 //    Part 1    

         

        #region [Commands Country]

        private RelayCommand _getCountriesByNameOrIdCommand;
        public ICommand GetCountriesByNameOrIdCommand => _getCountriesByNameOrIdCommand ??= new RelayCommand(delegate {ExecuteGetCountriesByNameOrId(SearchCountryName); });    
             
        private RelayCommand _addCountryCommand;
        public ICommand AddCountryCommand => _addCountryCommand ??= new RelayCommand(delegate { ExecuteAddCountry(null); });

        private RelayCommand _copyCountryCommand;
        public ICommand CopyCountryCommand => _copyCountryCommand ??= new RelayCommand(delegate { ExecuteCopyCountry(null); });      
             
        private RelayCommand _deleteCountryCommand;
        public ICommand DeleteCountryCommand => _deleteCountryCommand ??= new RelayCommand(delegate { ExecuteDeleteCountry(SearchCountryName); });    
             
        private RelayCommand _saveCountryCommand;
        public ICommand SaveCountryCommand => _saveCountryCommand ??= new RelayCommand(delegate { ExecuteSaveCountry(SearchCountryName); });    

        #endregion [Commands Country]       

CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
            CountriesView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
        
        private void ExecuteAddCountry(object o)
        {
            TblCountriesList.Insert(0, new TblCountry {   Name = CultRes.StringsRes.DatasetNew}  );

            CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
            CountriesView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
     
        private void ExecuteCopyCountry(object o)
        {
            if (_genCountryMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTblCountry)) return;

            TblCountriesList = _extCrud.CopyCountry(CurrentTblCountry);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
            CountriesView.MoveCurrentToFirst();
        }                         
     
        private void ExecuteDeleteCountry(string searchName)
        {
            if (_genCountryMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTblCountry)) return;               
 
    
            //check if in NULL connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return

            NULLList = _extCrud.SearchForConnectedDatasetsWithCountryIdInTableNULL(CurrentTblCountry);     
     
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(NULLList.Count, "NULL")) return;

            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithCountryIdInTableReference(CurrentTblCountry);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + 
                                              CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extCrud.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithCountryIdInTableComment(CurrentTblCountry);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extCrud.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }
            try
            {
                var country= _uow.TblCountries.GetById(CurrentTblCountry.CountryId);
                if (country!= null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + 
                                          CurrentTblCountry.CountryName)) return;

                    _extCrud.DeleteCountry(country);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, 
                                         CurrentTblCountry.CountryName);
                }
                else 
                        _allMessageBoxes.InfoMessageBox("Not To Delete", 
                                         CultRes.StringsRes.DeleteCan + " " + CurrentTblCountry.CountryName + " " + 
                                         CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGetCountriesByNameOrId(searchName);

            CountriesView.MoveCurrentToFirst();
        }                
     
        private void ExecuteSaveCountry(string searchName)
        {
            if (_genCountryMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTblCountry)) return;      
       
            //Combobox select NULLID  may be not 0
            if (CurrentTblCountry.NULLId == 0)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }     
       
        private void SaveCountry(object o)
        {
        }
        #endregion "Public Commands"                   
 
 

 //    Part 2    

     
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            ExecuteGetCountriesByNameOrId(searchName);
            CountriesView.MoveCurrentToPosition(_position);
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
                    if (CurrentTblCountry != null)
                    {
                        NULLList = _extCrud.GetCollectionFromNULLIdOrderBy<NULL>(CurrentTblCountry.NULLId);

                        NULLAllList = _extCrud.GetCollectionAllOrderBy<NULL>("");

                        View = CollectionViewSource.GetDefaultView(NULLList);
                        View.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }         
     
                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTblCountry != null)
                    {
                        NULLList = _extCrud.GetCollectionFromCountryIdOrderBy<NULL>(CurrentTblCountry.CountryId);

                        TblCountriesAllList = _extCrud.GetCollectionAllOrderBy<TblCountry>("country");

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
                    if (CurrentTblCountry != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromCountryIdOrderBy<Tbl93Comment>(CurrentTblCountry.CountryId);

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
                    if (CurrentTblCountry != null)
                    {
                        NULLList = _extCrud.GetCollectionFromNULLIdOrderBy<NULL>(CurrentTblCountry.NULLId);

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
                    if (CurrentTblCountry != null)
                    {
                        NULLList = _extCrud.GetCollectionFromCountryIdOrderBy<NULL>(CurrentTblCountry.CountryId);

                        TblCountriesAllList = _extCrud.GetCollectionAllOrderBy<TblCountry>("country");

                        View = CollectionViewSource.GetDefaultView(NULLList);
                        View.Refresh();
                    }
                    SelectedMainTabIndex = 1;
               }    
     
                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTblCountry != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromCountryIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTblCountry.CountryId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }        
     
                if (_selectedDetailTabIndex == 4)
                {
                    if (CurrentTblCountry != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromCountryIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTblCountry.CountryId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 1;
                }        
     
                if (_selectedDetailTabIndex == 5)
                {
                    if (CurrentTblCountry != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromCountryIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTblCountry.CountryId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 2;
                }       
     
                if (_selectedDetailTabIndex == 6)
                {
                    if (CurrentTblCountry != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromCountryIdOrderBy<Tbl93Comment>(CurrentTblCountry.CountryId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                }       
     
                if (_selectedDetailTabIndex == 7)
                {
                    if (CurrentTblCountry != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromCountryIdOrderBy<Tbl93Comment>(CurrentTblCountry.CountryId);

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
                    if (CurrentTblCountry != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromCountryIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTblCountry.CountryId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                }        
     
                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (CurrentTblCountry != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromCountryIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTblCountry.CountryId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 2;
                }      
     
                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (CurrentTblCountry != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromCountryIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTblCountry.CountryId);

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

     
        #region "Public Properties TblCountry"

        private string _searchCountryName = "";
        public string SearchCountryName
        {
            get => _searchCountryName; 
            set { _searchCountryName = value; RaisePropertyChanged("");  }
        }

        public  ICollectionView CountriesView;
        private   TblCountry CurrentTblCountry => CountriesView?.CurrentItem as TblCountry;

        private ObservableCollection<TblCountry> _tblCountriesList;
        public  ObservableCollection<TblCountry> TblCountriesList
        {
            get => _tblCountriesList; 
            set {  _tblCountriesList = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<TblCountry> _tblCountriesAllList;
        public  ObservableCollection<TblCountry> TblCountriesAllList
        {
            get => _tblCountriesAllList; 
            set {  _tblCountriesAllList = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<NULL> NULLAllList;
        public  ObservableCollection<NULL> NULLAllList
        {
            get => NULLAllList; 
            set {  NULLAllList = value; RaisePropertyChanged("");   }
        }

        #endregion "Public Properties"   
 

   }
}   
