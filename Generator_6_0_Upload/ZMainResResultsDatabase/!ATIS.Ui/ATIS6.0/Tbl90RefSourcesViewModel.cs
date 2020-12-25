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
    
         //    RefSourcesViewModel Skriptdatum:   29.11.2018  10:32    

namespace ATIS.Ui.Views.Database.ListDetails
{     
    
    public class RefSourcesViewModel : ViewModelBase                     
    {  
        // Version with Generic Unit Of Work and AtisDbContext for general use   
         
        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(typeof(RefSourcesViewModel));
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly CrudFunctions _extCrud = new CrudFunctions();

        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private readonly GenericMessageBoxes<Tbl90RefSource> _genRefSourceMessageBoxes = new GenericMessageBoxes<Tbl90RefSource>();
        private readonly GenericMessageBoxes<NULL> _genNULLMessageBoxes = new GenericMessageBoxes<NULL>();
        private readonly GenericMessageBoxes<NULL> _genFiSpeciesMessageBoxes = new GenericMessageBoxes<NULL>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genExpertMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genSourceMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genAuthorMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl93Comment> _genCommentMessageBoxes = new GenericMessageBoxes<Tbl93Comment>();
        private int _position;   
         
        #endregion [Private Data Members]               
      
        #region [Constructor]

        public RefSourcesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {          
        
                // Code runs "for real" 
                Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource>();    
            }
        }     
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          
 

 //    Part 1    

         

        #region [Commands RefSource]

        private RelayCommand _getRefSourcesByNameOrIdCommand;
        public ICommand GetRefSourcesByNameOrIdCommand => _getRefSourcesByNameOrIdCommand ??= new RelayCommand(delegate {ExecuteGetRefSourcesByNameOrId(SearchRefSourceName); });    
             
        private RelayCommand _addRefSourceCommand;
        public ICommand AddRefSourceCommand => _addRefSourceCommand ??= new RelayCommand(delegate { ExecuteAddRefSource(null); });

        private RelayCommand _copyRefSourceCommand;
        public ICommand CopyRefSourceCommand => _copyRefSourceCommand ??= new RelayCommand(delegate { ExecuteCopyRefSource(null); });      
             
        private RelayCommand _deleteRefSourceCommand;
        public ICommand DeleteRefSourceCommand => _deleteRefSourceCommand ??= new RelayCommand(delegate { ExecuteDeleteRefSource(SearchRefSourceName); });    
             
        private RelayCommand _saveRefSourceCommand;
        public ICommand SaveRefSourceCommand => _saveRefSourceCommand ??= new RelayCommand(delegate { ExecuteSaveRefSource(SearchRefSourceName); });    

        #endregion [Commands RefSource]       

RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
        
        private void ExecuteAddRefSource(object o)
        {
            Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource>();
            Tbl90RefSourcesList.Insert(0, new Tbl90RefSource {   RefSourceName = CultRes.StringsRes.DatasetNew}  );

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
     
        private void ExecuteCopyRefSource(object o)
        {
            if (_genRefSourceMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90RefSource)) return;

            Tbl90RefSourcesList = _extCrud.CopyRefSource(CurrentTbl90RefSource);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.MoveCurrentToFirst();
        }                         
     
        private void ExecuteDeleteRefSource(string searchName)
        {
            if (_genRefSourceMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90RefSource)) return;               
 
    
            //check if in Tbl69FiSpeciesses connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return

            Tbl69FiSpeciessesList = _extCrud.SearchForConnectedDatasetsWithRefSourceIdInTableFiSpecies(CurrentTbl90RefSource);     
     
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl69FiSpeciessesList.Count, "FiSpecies")) return;

            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithRefSourceIdInTableReference(CurrentTbl90RefSource);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + 
                                              CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extCrud.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithRefSourceIdInTableComment(CurrentTbl90RefSource);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extCrud.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }
            try
            {
                var refSource= _uow.Tbl90RefSources.GetById(CurrentTbl90RefSource.RefSourceId);
                if (refSource!= null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + 
                                          CurrentTbl90RefSource.RefSourceName)) return;

                    _extCrud.DeleteRefSource(refSource);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, 
                                         CurrentTbl90RefSource.RefSourceName);
                }
                else 
                        _allMessageBoxes.InfoMessageBox("Not To Delete", 
                                         CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefSource.RefSourceName + " " + 
                                         CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGetRefSourcesByNameOrId(searchName);

            RefSourcesView.MoveCurrentToFirst();
        }                
     
        private void ExecuteSaveRefSource(string searchName)
        {
            if (_genRefSourceMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90RefSource)) return;      
       
            //Combobox select NULLID  may be not 0
            if (CurrentTbl90RefSource.NULLId == 0)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }     
     
            try
            {
                var refSource = _uow.Tbl90RefSources .GetById(CurrentTbl90RefSource.RefSourceId);
                //   var phylum = _context.Tbl90RefSources.AsNoTracking().FirstOrDefault(a=>a.RefSourceId == CurrentTbl90RefSource.RefSourceId);
                //          _context.Entry(refSource).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl90RefSource.RefSourceName))
                    return;

                if (CurrentTbl90RefSource.RefSourceId == 0)
                    refSource = _extCrud.RefSourceAdd(CurrentTbl90RefSource);
                else
                    refSource = _extCrud.RefSourceUpdate(refSource, CurrentTbl90RefSource);

                _position = RefSourcesView.CurrentPosition;

                try
                {
                    _extCrud.RefSourceSave(refSource, CurrentTbl90RefSource);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(),
                            CultRes.StringsRes.FailedToSave); 
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error); 
                    Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, CurrentTbl90RefSource.RefSourceId == 0
                    ? "DatasetNew"
                    : CurrentTbl90RefSource.RefSourceName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error); 
                Log.Error(e);
            }
            ExecuteGetRefSourcesByNameOrId(searchName);
            RefSourcesView.MoveCurrentToPosition(_position);
        }
        #endregion [Methods RefSource]                
 
 

 //    Part 2    

     
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            ExecuteGetRefSourcesByNameOrId(searchName);
            RefSourcesView.MoveCurrentToPosition(_position);
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
                    if (CurrentTbl90RefSource != null)
                    {
                        NULLList = _extCrud.GetCollectionFromNULLIdOrderBy<NULL>(CurrentTbl90RefSource.NULLId);

                        NULLAllList = _extCrud.GetCollectionAllOrderBy<NULL>("");

                        View = CollectionViewSource.GetDefaultView(NULLList);
                        View.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }         
     
                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl90RefSource != null)
                    {
                        Tbl69FiSpeciessesList = _extCrud.GetCollectionFromRefSourceIdOrderBy<NULL>(CurrentTbl90RefSource.RefSourceId);

                        Tbl90RefSourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("refSource");

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
                    if (CurrentTbl90RefSource != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromRefSourceIdOrderBy<Tbl93Comment>(CurrentTbl90RefSource.RefSourceId);

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
                    if (CurrentTbl90RefSource != null)
                    {
                        NULLList = _extCrud.GetCollectionFromNULLIdOrderBy<NULL>(CurrentTbl90RefSource.NULLId);

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
                    if (CurrentTbl90RefSource != null)
                    {
                        Tbl69FiSpeciessesList = _extCrud.GetCollectionFromRefSourceIdOrderBy<NULL>(CurrentTbl90RefSource.RefSourceId);

                        Tbl90RefSourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("refSource");

                        View = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                        View.Refresh();
                    }
                    SelectedMainTabIndex = 1;
               }    
     
                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTbl90RefSource != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromRefSourceIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90RefSource.RefSourceId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }        
     
                if (_selectedDetailTabIndex == 4)
                {
                    if (CurrentTbl90RefSource != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromRefSourceIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90RefSource.RefSourceId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 1;
                }        
     
                if (_selectedDetailTabIndex == 5)
                {
                    if (CurrentTbl90RefSource != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromRefSourceIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90RefSource.RefSourceId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 2;
                }       
     
                if (_selectedDetailTabIndex == 6)
                {
                    if (CurrentTbl90RefSource != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromRefSourceIdOrderBy<Tbl93Comment>(CurrentTbl90RefSource.RefSourceId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                }       
     
                if (_selectedDetailTabIndex == 7)
                {
                    if (CurrentTbl90RefSource != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromRefSourceIdOrderBy<Tbl93Comment>(CurrentTbl90RefSource.RefSourceId);

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
                    if (CurrentTbl90RefSource != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromRefSourceIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90RefSource.RefSourceId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                }        
     
                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (CurrentTbl90RefSource != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromRefSourceIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90RefSource.RefSourceId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 2;
                }      
     
                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (CurrentTbl90RefSource != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromRefSourceIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90RefSource.RefSourceId);

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

     
        #region "Public Properties Tbl90RefSource"

        private string _searchRefSourceName = "";
        public string SearchRefSourceName
        {
            get => _searchRefSourceName; 
            set { _searchRefSourceName = value; RaisePropertyChanged("");  }
        }

        public  ICollectionView RefSourcesView;
        private   Tbl90RefSource CurrentTbl90RefSource => RefSourcesView?.CurrentItem as Tbl90RefSource;

        private ObservableCollection<Tbl90RefSource> _tbl90RefSourcesList;
        public  ObservableCollection<Tbl90RefSource> Tbl90RefSourcesList
        {
            get => _tbl90RefSourcesList; 
            set {  _tbl90RefSourcesList = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<Tbl90RefSource> _tbl90RefSourcesAllList;
        public  ObservableCollection<Tbl90RefSource> Tbl90RefSourcesAllList
        {
            get => _tbl90RefSourcesAllList; 
            set {  _tbl90RefSourcesAllList = value; RaisePropertyChanged("");   }
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
