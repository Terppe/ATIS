using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Common.Logging;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using ATIS.Ui.Views.Database.CrudHelper;
using ATIS.Ui.Views.Database.DatabaseHelper;
using Microsoft.EntityFrameworkCore;          

    
         //    RefExpertsViewModel Skriptdatum:  29.11.2018  10:32    

namespace ATIS.Ui.Views.Database.ListDetails
{     
    
    public class RefExpertsViewModel : ViewModelBase                     
    {  
        // Version with Generic Unit Of Work and AtisDbContext for general use   
         
        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();

        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private readonly GenericMessageBoxes<Tbl90RefExpert> _genRefExpertMessageBoxes = new GenericMessageBoxes<Tbl90RefExpert>();
        private readonly GenericMessageBoxes<NULL> _genNULLMessageBoxes = new GenericMessageBoxes<NULL>();
        private readonly GenericMessageBoxes<NULL> _genFiSpeciesMessageBoxes = new GenericMessageBoxes<NULL>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genExpertMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genSourceMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genAuthorMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl93Comment> _genCommentMessageBoxes = new GenericMessageBoxes<Tbl93Comment>();
        private readonly BasicGet _extGet = new BasicGet();
        private readonly BasicCopy _extCopy = new BasicCopy();
        private readonly BasicDelete _extDelete = new BasicDelete();
        private readonly BasicSave _extSave = new BasicSave();        
        private int _position;   
         
        #endregion [Private Data Members]               
      
        #region [Constructor]

        public RefExpertsViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {          
        
                // Code runs "for real" 
                Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert>();    
            }
        }     
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]         
 

 //    Part 1    

         

        #region [Commands RefExpert]

        private RelayCommand _getRefExpertsByNameOrIdCommand;
        public ICommand GetRefExpertsByNameOrIdCommand => _getRefExpertsByNameOrIdCommand ??= new RelayCommand(delegate {ExecuteGetRefExpertsByNameOrId(SearchRefExpertName); });    
             
        private RelayCommand _addRefExpertCommand;
        public ICommand AddRefExpertCommand => _addRefExpertCommand ??= new RelayCommand(delegate { ExecuteAddRefExpert(null); });

        private RelayCommand _copyRefExpertCommand;
        public ICommand CopyRefExpertCommand => _copyRefExpertCommand ??= new RelayCommand(delegate { ExecuteCopyRefExpert(null); });      
             
        private RelayCommand _deleteRefExpertCommand;
        public ICommand DeleteRefExpertCommand => _deleteRefExpertCommand ??= new RelayCommand(delegate { ExecuteDeleteRefExpert(SearchRefExpertName); });    
             
        private RelayCommand _saveRefExpertCommand;
        public ICommand SaveRefExpertCommand => _saveRefExpertCommand ??= new RelayCommand(delegate { ExecuteSaveRefExpert(SearchRefExpertName); });    

        #endregion [Commands RefExpert]       

RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
        
        private void ExecuteAddRefExpert(object o)
        {
            Tbl90RefExpertsList.Insert(0, new Tbl90RefExpert {   RefExpertName = CultRes.StringsRes.DatasetNew}  );

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
     
        private void ExecuteCopyRefExpert(object o)
        {
            if (_genRefExpertMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90RefExpert)) return;

            Tbl90RefExpertsList = _extCopy.CopyRefExpert(CurrentTbl90RefExpert);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.MoveCurrentToFirst();
        }                         
     
        private void ExecuteDeleteRefExpert(string searchName)
        {
            if (_genRefExpertMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90RefExpert)) return;               
 
    
            //check if in Tbl69FiSpeciesses connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return

            Tbl69FiSpeciessesList = _extDelete.SearchForConnectedDatasetsWithRefExpertIdInTableFiSpecies(CurrentTbl90RefExpert);     
     
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl69FiSpeciessesList.Count, "FiSpecies")) return;

            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extDelete.DeleteDatasetsWithRefExpertIdInTableReference(CurrentTbl90RefExpert);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extDelete.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extDelete.DeleteDatasetsWithRefExpertIdInTableComment(CurrentTbl90RefExpert);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extDelete.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }
            try
            {
                var refExpert= _uow.Tbl90RefExperts.GetById(CurrentTbl90RefExpert.RefExpertId);
                if (refExpert!= null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90RefExpert.RefExpertName)) return;

                    _extDelete.DeleteRefExpert(refExpert);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl90RefExpert.RefExpertName);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl90RefExpert.RefExpertName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGetRefExpertsByNameOrId(searchName);

            RefExpertsView.MoveCurrentToFirst();
        }                
     
        private void ExecuteSaveRefExpert(string searchName)
        {
            if (_genRefExpertMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90RefExpert)) return;      
       
            //Combobox select NULLID  may be not 0
            if (CurrentTbl90RefExpert.NULLId == 0)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }     
       
        private void SaveRefExpert(object o)
        {
 
        }
        #endregion "Public Commands"                   
 
 

 //    Part 2    

     
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            ExecuteGetRefExpertsByNameOrId(searchName);
            RefExpertsView.MoveCurrentToPosition(_position);
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
                    if (CurrentTbl90RefExpert != null)
                    {
                        NULLList = _extGet.GetCollectionOrderByFromNULLId<NULL>(CurrentTbl90RefExpert.NULLId);

                        NULLAllList = _extGet.AllCollection<NULL>("");

                        View = CollectionViewSource.GetDefaultView(NULLList);
                        View.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }         
     
                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl90RefExpert != null)
                    {
                        Tbl69FiSpeciessesList = _extGet.GetCollectionOrderByFromRefExpertId<NULL>(CurrentTbl90RefExpert.RefExpertId);

                        Tbl90RefExpertsAllList = _extGet.AllCollection<Tbl90RefExpert>("refExpert");

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
                    if (CurrentTbl90RefExpert != null)
                    {
                        Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFromRefExpertId<Tbl93Comment>(CurrentTbl90RefExpert.RefExpertId);

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
                    if (CurrentTbl90RefExpert != null)
                    {
                        NULLList = _extGet.GetCollectionOrderByFromNULLId<NULL>(CurrentTbl90RefExpert.NULLId);

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
                    if (CurrentTbl90RefExpert != null)
                    {
                        Tbl69FiSpeciessesList = _extGet.GetCollectionOrderByFromRefExpertId<NULL>(CurrentTbl90RefExpert.RefExpertId);

                        Tbl90RefExpertsAllList = _extGet.AllCollection<Tbl90RefExpert>("refExpert");

                        View = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                        View.Refresh();
                    }
                    SelectedMainTabIndex = 1;
               }    
     
                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTbl90RefExpert != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extGet.GetReferenceExpertsCollectionOrderByFromRefExpertIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(CurrentTbl90RefExpert.RefExpertId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }        
     
                if (_selectedDetailTabIndex == 4)
                {
                    if (CurrentTbl90RefExpert != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extGet.GetReferenceSourcesCollectionOrderByFromRefExpertIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl90RefExpert.RefExpertId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 1;
                }        
     
                if (_selectedDetailTabIndex == 5)
                {
                    if (CurrentTbl90RefExpert != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extGet.GetReferenceAuthorsCollectionOrderByFromRefExpertIdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl90RefExpert.RefExpertId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 2;
                }       
     
                if (_selectedDetailTabIndex == 6)
                {
                    if (CurrentTbl90RefExpert != null)
                    {
                        Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFromRefExpertId<Tbl93Comment>(CurrentTbl90RefExpert.RefExpertId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
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
                    if (CurrentTbl90RefExpert != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extGet.GetReferenceExpertsCollectionOrderByFromRefExpertIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(CurrentTbl90RefExpert.RefExpertId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                }        
     
                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (CurrentTbl90RefExpert != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extGet.GetReferenceSourcesCollectionOrderByFromRefExpertIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl90RefExpert.RefExpertId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 2;
                }      
     
                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (CurrentTbl90RefExpert != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extGet.GetReferenceAuthorsCollectionOrderByFromRefExpertIdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl90RefExpert.RefExpertId);

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

     
        #region "Public Properties Tbl90RefExpert"

        private string _searchRefExpertName = "";
        public string SearchRefExpertName
        {
            get => _searchRefExpertName; 
            set { _searchRefExpertName = value; RaisePropertyChanged("");  }
        }

        public  ICollectionView RefExpertsView;
        private   Tbl90RefExpert CurrentTbl90RefExpert => RefExpertsView?.CurrentItem as Tbl90RefExpert;

        private ObservableCollection<Tbl90RefExpert> _tbl90RefExpertsList;
        public  ObservableCollection<Tbl90RefExpert> Tbl90RefExpertsList
        {
            get => _tbl90RefExpertsList; 
            set {  _tbl90RefExpertsList = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<Tbl90RefExpert> _tbl90RefExpertsAllList;
        public  ObservableCollection<Tbl90RefExpert> Tbl90RefExpertsAllList
        {
            get => _tbl90RefExpertsAllList; 
            set {  _tbl90RefExpertsAllList = value; RaisePropertyChanged("");   }
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
