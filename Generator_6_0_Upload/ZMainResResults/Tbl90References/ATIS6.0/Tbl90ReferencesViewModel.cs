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
    
         //    ReferencesViewModel Skriptdatum:  29.11.2018  10:32    

namespace ATIS.Ui.Views.Database.ListDetails
{     
    
    public class ReferencesViewModel : ViewModelBase                     
    {  
        // Version with Generic Unit Of Work and AtisDbContext for general use   
         
        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(typeof(ReferencesViewModel));
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly CrudFunctions _extCrud = new CrudFunctions();

        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private readonly GenericMessageBoxes<Tbl90Reference> _genReferenceMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90RefExpert> _genRefExpertMessageBoxes = new GenericMessageBoxes<Tbl90RefExpert>();
        private readonly GenericMessageBoxes<Tbl90RefAuthor> _genRefAuthorMessageBoxes = new GenericMessageBoxes<Tbl90RefAuthor>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genExpertMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genSourceMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genAuthorMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl93Comment> _genCommentMessageBoxes = new GenericMessageBoxes<Tbl93Comment>();
        private int _position;   
         
        #endregion [Private Data Members]               
      
        #region [Constructor]

        public ReferencesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {          
        
                // Code runs "for real" 
                Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>();    
            }
        }     
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          
 

 //    Part 1    

         

        #region [Commands Reference]

        private RelayCommand _getReferencesByNameOrIdCommand;
        public ICommand GetReferencesByNameOrIdCommand => _getReferencesByNameOrIdCommand ??= new RelayCommand(delegate {ExecuteGetReferencesByNameOrId(SearchReferenceName); });    
             
        private RelayCommand _addReferenceCommand;
        public ICommand AddReferenceCommand => _addReferenceCommand ??= new RelayCommand(delegate { ExecuteAddReference(null); });

        private RelayCommand _copyReferenceCommand;
        public ICommand CopyReferenceCommand => _copyReferenceCommand ??= new RelayCommand(delegate { ExecuteCopyReference(null); });      
             
        private RelayCommand _deleteReferenceCommand;
        public ICommand DeleteReferenceCommand => _deleteReferenceCommand ??= new RelayCommand(delegate { ExecuteDeleteReference(SearchReferenceName); });    
             
        private RelayCommand _saveReferenceCommand;
        public ICommand SaveReferenceCommand => _saveReferenceCommand ??= new RelayCommand(delegate { ExecuteSaveReference(SearchReferenceName); });    

        #endregion [Commands Reference]       

        
            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl90ReferencesList.Count)) return;

            ReferencesView = CollectionViewSource.GetDefaultView(Tbl90ReferencesList);
            ReferencesView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
        
        private void ExecuteAddReference(object o)
        {
            Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>();
             Tbl90ReferencesList.Insert(0, new Tbl90Reference {   Info = CultRes.StringsRes.DatasetNew }  );

            Tbl03RegnumsAllList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03Regnums());
            Tbl06PhylumsAllList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06Phylums());
            Tbl09DivisionsAllList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09Divisions());
            Tbl12SubphylumsAllList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12Subphylums());
            Tbl15SubdivisionsAllList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15Subdivisions());
            Tbl18SuperclassesAllList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18Superclasses());
            Tbl21ClassesAllList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21Classes());
            Tbl24SubclassesAllList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24Subclasses());
            Tbl27InfraclassesAllList = new ObservableCollection<Tbl27Infraclass>(_businessLayer.ListTbl27Infraclasses());
            Tbl30LegiosAllList = new ObservableCollection<Tbl30Legio>(_businessLayer.ListTbl30Legios());
            Tbl33OrdosAllList = new ObservableCollection<Tbl33Ordo>(_businessLayer.ListTbl33Ordos());
            Tbl36SubordosAllList = new ObservableCollection<Tbl36Subordo>(_businessLayer.ListTbl36Subordos());
            Tbl39InfraordosAllList = new ObservableCollection<Tbl39Infraordo>(_businessLayer.ListTbl39Infraordos());
            Tbl42SuperfamiliesAllList = new ObservableCollection<Tbl42Superfamily>(_businessLayer.ListTbl42Superfamilies());
            Tbl45FamiliesAllList = new ObservableCollection<Tbl45Family>(_businessLayer.ListTbl45Families());
            Tbl48SubfamiliesAllList = new ObservableCollection<Tbl48Subfamily>(_businessLayer.ListTbl48Subfamilies());
            Tbl51InfrafamiliesAllList = new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51Infrafamilies());
            Tbl54SupertribussesAllList = new ObservableCollection<Tbl54Supertribus>(_businessLayer.ListTbl54Supertribusses());
            Tbl57TribussesAllList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57Tribusses());
            Tbl60SubtribussesAllList = new ObservableCollection<Tbl60Subtribus>(_businessLayer.ListTbl60Subtribusses());
            Tbl63InfratribussesAllList = new ObservableCollection<Tbl63Infratribus>(_businessLayer.ListTbl63Infratribusses());
            Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66Genusses());
            Tbl69FiSpeciessesAllList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciesses());
            Tbl72PlSpeciessesAllList = new ObservableCollection<Tbl72PlSpecies>(_businessLayer.ListTbl72PlSpeciesses());

            Tbl90RefExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_businessLayer.ListTbl90RefExperts());       
            Tbl90RefSourcesAllList = new ObservableCollection<Tbl90RefSource>(_businessLayer.ListTbl90RefSources());
            Tbl90RefAuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_businessLayer.ListTbl90RefAuthors());

            ReferencesView = CollectionViewSource.GetDefaultView(Tbl90ReferencesList);
            ReferencesView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
     
        private void ExecuteCopyReference(object o)
        {
            if (_genReferenceMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90Reference)) return;

            Tbl90ReferencesList = _extCrud.CopyReference(CurrentTbl90Reference);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            ReferencesView = CollectionViewSource.GetDefaultView(Tbl90ReferencesList);
            ReferencesView.MoveCurrentToFirst();
        }                         
     
        private void ExecuteDeleteReference(string searchName)
        {
            if (_genReferenceMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90Reference)) return;               
 
    
            //check if in Tbl90RefAuthors connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return

            Tbl90RefAuthorsList = _extCrud.SearchForConnectedDatasetsWithReferenceIdInTableRefAuthor(CurrentTbl90Reference);     
     
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl90RefAuthorsList.Count, "RefAuthor")) return;

            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithReferenceIdInTableReference(CurrentTbl90Reference);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " +   CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extCrud.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithReferenceIdInTableComment(CurrentTbl90Reference);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extCrud.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }
            try
            {
                var reference= _uow.Tbl90References.GetById(CurrentTbl90Reference.ReferenceId);
                if (reference!= null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " +     CurrentTbl90Reference.ReferenceName)) return;

                    _extCrud.DeleteReference(reference);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl90Reference.ReferenceName);
                }
                else   _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl90Reference.ReferenceName + " " +  CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGetReferencesByNameOrId(searchName);

            ReferencesView.MoveCurrentToFirst();
        }                
     
        private void ExecuteSaveReference(string searchName)
        {
            if (_genReferenceMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90Reference)) return;      
       
            //Combobox select RefExpertID  may be not 0
            if (CurrentTbl90Reference.RefExpertId == 0)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,  
                       MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }     
       
        private void SaveReference(object o)
        {
        }
        #endregion "Public Commands"                   
 
 

 //    Part 2    

           
        #region "Public Commands Connect <== Tbl90RefExpert"                 
        

        private RelayCommand _saveRefExpertCommand;

        public ICommand SaveRefExpertCommand =>  _saveRefExpertCommand ??= new RelayCommand(delegate { ExecuteSaveRefExpert(null); });        
           
        private void ExecuteSaveRefExpert(string searchName)
        {
            if (_genRefExpertMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90RefExpert)) return;

            try
            {
                var refexpert = _uow.Tbl90RefExperts.GetById(CurrentTbl90RefExpert.RefExpertId);

                if (CurrentTbl90RefExpert.RefExpertId == 0)
                    refexpert = _extCrud.RefExpertAdd(CurrentTbl90RefExpert);
                else
                    refexpert = _extCrud.RefExpertUpdate(refexpert, CurrentTbl90RefExpert);

                _position = ReferencesView.CurrentPosition;   
       
                var cap = CurrentTbl90RefExpert.RefExpertName;
                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(cap))        return;               
       
                try
                {
                    _extCrud.RefExpertSave(refexpert, CurrentTbl90RefExpert);
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
                    //         Log.Error(e);
                    return;
                }            
      
                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl90RefExpert.RefExpertId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl90RefExpert.RefExpertName);
            }       
     
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            ExecuteGetReferencesByNameOrId(searchName);
            ReferencesView.MoveCurrentToPosition(_position);
        }

        #endregion "Public Commands"                  
                                                          

 //    Part 3    

           
        #region "Public Commands Connect <== Tbl90RefSource"                 
       
        private RelayCommand _saveRefSourceCommand;

        public ICommand SaveRefSourceCommand => 
                            _saveRefSourceCommand ??= new RelayCommand(delegate { ExecuteSaveRefSource(null); });
       
                                                          



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
Tbl90RefExpertsList = _extGet.GetRefExpertsCollectionOrderByFromRefExpertId<Tbl90RefExpert>(CurrentTbl90Reference.RefExpertId);
 
            Tbl90RefSourcesList?.Clear();

            Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource>(
                _businessLayer.ListTbl90RefSourcesByRefSourceId(CurrentTbl90Reference.RefSourceID));

            Tbl90RefAuthorsList?.Clear();

            Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor>(
                _businessLayer.ListTbl90RefAuthorsByRefAuthorId(CurrentTbl90Reference.RefAuthorID));

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();    
     
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
                    if (CurrentTbl90Reference != null)
                    {
                        Tbl90RefExpertsList = _extCrud.GetRefExpertsCollectionFromRefExpertIdOrderBy<Tbl90RefExpert>(CurrentTbl90Reference.RefExpertId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("");

                        RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                        RefExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }         
     
                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl90Reference != null)
                    {
                        Tbl90RefAuthorsList = _extCrud.GetRefAuthorsCollectionFromReferenceIdOrderBy<Tbl90RefAuthor>(CurrentTbl90Reference.ReferenceId);

                        Tbl90ReferencesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90Reference>("reference");

                        RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                        RefAuthorsView.Refresh();
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
                    if (CurrentTbl90Reference != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromReferenceIdOrderBy<Tbl93Comment>(CurrentTbl90Reference.ReferenceId);

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
                    if (CurrentTbl90Reference != null)
                    {
                        Tbl90RefExpertsList = _extCrud.GetRefExpertsCollectionFromRefExpertIdOrderBy<Tbl90RefExpert>(CurrentTbl90Reference.RefExpertId);

                        RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
                        RefExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 0;  
               }     
     
                if (_selectedDetailTabIndex == 1)                
                {
                    SelectedMainTabIndex = 0;
                }    
     
                if (_selectedDetailTabIndex == 2)                
                {
                    if (CurrentTbl90Reference != null)
                    {
                        Tbl90RefAuthorsList = _extCrud.GetRefAuthorsCollectionFromReferenceIdOrderBy<Tbl90RefAuthor>(CurrentTbl90Reference.ReferenceId);

                        Tbl90ReferencesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90Reference>("reference");

                        RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                        RefAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
               }    
     
                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTbl90Reference != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromReferenceIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90Reference.ReferenceId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }        
     
                if (_selectedDetailTabIndex == 4)
                {
                    if (CurrentTbl90Reference != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromReferenceIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90Reference.ReferenceId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 1;
                }        
     
                if (_selectedDetailTabIndex == 5)
                {
                    if (CurrentTbl90Reference != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromReferenceIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90Reference.ReferenceId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 2;
                }       
     
                if (_selectedDetailTabIndex == 6)
                {
                    if (CurrentTbl90Reference != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromReferenceIdOrderBy<Tbl93Comment>(CurrentTbl90Reference.ReferenceId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                }       
     
                if (_selectedDetailTabIndex == 7)
                {
                    if (CurrentTbl90Reference != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromReferenceIdOrderBy<Tbl93Comment>(CurrentTbl90Reference.ReferenceId);

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
                    if (CurrentTbl90Reference != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromReferenceIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90Reference.ReferenceId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                }        
     
                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (CurrentTbl90Reference != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromReferenceIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90Reference.ReferenceId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 2;
                }      
     
                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (CurrentTbl90Reference != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromReferenceIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90Reference.ReferenceId);

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

      
        #region "Public Properties Tbl90Reference"

        private string _searchReferenceInfo = "";
        public string SearchReferenceInfo
        {
            get => _searchReferenceInfo; 
            set { _searchReferenceInfo = value; RaisePropertyChanged("");  }
        }

        public   ICollectionView ReferencesView;
        private  Tbl90Reference CurrentTbl90Reference => ReferencesView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferencesList;
        public  ObservableCollection<Tbl90Reference> Tbl90ReferencesList
        {
            get => _tbl90ReferencesList; 
            set {  _tbl90ReferencesList = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<Tbl90Reference> _tbl90ReferencesAllList;
        public  ObservableCollection<Tbl90Reference> Tbl90ReferencesAllList
        {
            get => _tbl90ReferencesAllList; 
            set { _tbl90ReferencesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl90RefExpert> _tbl90RefExpertsAllList;

        public ObservableCollection<Tbl90RefExpert> Tbl90RefExpertsAllList
        {
            get => _tbl90RefExpertsAllList;
            set  {  _tbl90RefExpertsAllList = value;  RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl90RefSource> _tbl90RefSourcesAllList;

        public ObservableCollection<Tbl90RefSource> Tbl90RefSourcesAllList
        {
            get => _tbl90RefSourcesAllList;
            set  {  _tbl90RefSourcesAllList = value;  RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl90RefAuthor> _tbl90RefAuthorsAllList;

        public ObservableCollection<Tbl90RefAuthor> Tbl90RefAuthorsAllList
        {
            get => _tbl90RefAuthorsAllList;
            set  {  _tbl90RefAuthorsAllList = value;  RaisePropertyChanged("");  }
        }

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
        {
            get => _tbl03RegnumsAllList;
            set { _tbl03RegnumsAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList;
        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
        {
            get => _tbl06PhylumsAllList;
            set { _tbl06PhylumsAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl09Division> _tbl09DivisionsAllList;
        public ObservableCollection<Tbl09Division> Tbl09DivisionsAllList
        {
            get => _tbl09DivisionsAllList;
            set { _tbl09DivisionsAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsAllList;
        public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsAllList
        {
            get => _tbl12SubphylumsAllList;
            set { _tbl12SubphylumsAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsAllList;
        public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsAllList
        {
            get => _tbl15SubdivisionsAllList;
            set { _tbl15SubdivisionsAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesAllList;
        public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesAllList
        {
            get => _tbl18SuperclassesAllList;
            set { _tbl18SuperclassesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl21Class> _tbl21ClassesAllList;
        public ObservableCollection<Tbl21Class> Tbl21ClassesAllList
        {
            get => _tbl21ClassesAllList;
            set { _tbl21ClassesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl24Subclass> _tbl24SubclassesAllList;
        public ObservableCollection<Tbl24Subclass> Tbl24SubclassesAllList
        {
            get => _tbl24SubclassesAllList;
            set { _tbl24SubclassesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesAllList;
        public ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesAllList
        {
            get => _tbl27InfraclassesAllList;
            set { _tbl27InfraclassesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl30Legio> _tbl30LegiosAllList;
        public ObservableCollection<Tbl30Legio> Tbl30LegiosAllList
        {
            get => _tbl30LegiosAllList;
            set { _tbl30LegiosAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl33Ordo> _tbl33OrdosAllList;
        public ObservableCollection<Tbl33Ordo> Tbl33OrdosAllList
        {
            get => _tbl33OrdosAllList;
            set { _tbl33OrdosAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl36Subordo> _tbl36SubordosAllList;
        public ObservableCollection<Tbl36Subordo> Tbl36SubordosAllList
        {
            get => _tbl36SubordosAllList;
            set { _tbl36SubordosAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl39Infraordo> _tbl39InfraordosAllList;
        public ObservableCollection<Tbl39Infraordo> Tbl39InfraordosAllList
        {
            get => _tbl39InfraordosAllList;
            set { _tbl39InfraordosAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl42Superfamily> _tbl42SuperfamiliesAllList;
        public ObservableCollection<Tbl42Superfamily> Tbl42SuperfamiliesAllList
        {
            get => _tbl42SuperfamiliesAllList;
            set { _tbl42SuperfamiliesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl45Family> _tbl45FamiliesAllList;
        public ObservableCollection<Tbl45Family> Tbl45FamiliesAllList
        {
            get => _tbl45FamiliesAllList;
            set { _tbl45FamiliesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl48Subfamily> _tbl48SubfamiliesAllList;
        public ObservableCollection<Tbl48Subfamily> Tbl48SubfamiliesAllList
        {
            get => _tbl48SubfamiliesAllList;
            set { _tbl48SubfamiliesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesAllList;
        public ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesAllList
        {
            get => _tbl51InfrafamiliesAllList;
            set { _tbl51InfrafamiliesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl54Supertribus> _tbl54SupertribussesAllList;
        public ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesAllList
        {
            get => _tbl54SupertribussesAllList;
            set { _tbl54SupertribussesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl57Tribus> _tbl57TribussesAllList;
        public ObservableCollection<Tbl57Tribus> Tbl57TribussesAllList
        {
            get => _tbl57TribussesAllList;
            set { _tbl57TribussesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesAllList;
        public ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesAllList
        {
            get => _tbl60SubtribussesAllList;
            set { _tbl60SubtribussesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesAllList;
        public ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesAllList
        {
            get => _tbl63InfratribussesAllList;
            set { _tbl63InfratribussesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList;
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList;
        public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
        {
            get => _tbl69FiSpeciessesAllList;
            set { _tbl69FiSpeciessesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesAllList;
        public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesAllList
        {
            get => _tbl72PlSpeciessesAllList;
            set { _tbl72PlSpeciessesAllList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"   
       
        #region "Public Properties Tbl90RefExpert"

        private string _searchRefExpertName = string.Empty;
        public  string SearchRefExpertName
        {
            get  => _searchRefExpertName; 
            set { _searchRefExpertName = value; RaisePropertyChanged(""); }
        }

        public  ICollectionView RefExpertsView;
        private Tbl90RefExpert CurrentTbl90RefExpert => RefExpertsView?.CurrentItem as Tbl90RefExpert;           

        private ObservableCollection<Tbl90RefExpert> _tbl90RefExpertsList;
        public ObservableCollection<Tbl90RefExpert> Tbl90RefExpertsList
        {
            get => _tbl90RefExpertsList; 
            set { _tbl90RefExpertsList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"   
  
       
        #region "Public Properties Tbl90RefSource"

        private string _searchRefSourceName = string.Empty;
        public  string SearchRefSourceName
        {
            get => _searchRefSourceName; 
            set { _searchRefSourceName = value; RaisePropertyChanged(""); }
        }

        public  ICollectionView RefSourcesView;
        private  Tbl90RefSource CurrentTbl90RefSource => RefSourcesView?.CurrentItem as Tbl90RefSource;           

        private ObservableCollection<Tbl90RefSource> _tbl90RefSourcesList;
        public   ObservableCollection<Tbl90RefSource> Tbl90RefSourcesList
        {
            get => _tbl90RefSourcesList; 
            set { _tbl90RefSourcesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"   
        
        #region "Public Properties Tbl90RefAuthor"

        private string _searchRefAuthorName = string.Empty;
        public string SearchRefAuthorName
        {
            get => _searchRefAuthorName; 
            set { _searchRefAuthorName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView RefAuthorsView;
        private Tbl90RefAuthor CurrentTbl90RefAuthor => RefAuthorsView?.CurrentItem as Tbl90RefAuthor;           

        private ObservableCollection<Tbl90RefAuthor> _tbl90RefAuthorsList;
        public ObservableCollection<Tbl90RefAuthor> Tbl90RefAuthorsList
        {
            get => _tbl90RefAuthorsList; 
            set { _tbl90RefAuthorsList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     
           
        #region Public Properties Tbl90References

        private ObservableCollection<Tbl90Reference> _tbl90ReferencesList;

        public ObservableCollection<Tbl90Reference> Tbl90ReferencesList
        {
            get => _tbl90ReferencesList;
            set { _tbl90ReferencesList = value; RaisePropertyChanged(""); }
        }

        #endregion

        #region "Public Properties Tbl90Author"

        private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList;
        public  ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
        {
            get => _tbl90AuthorsAllList; 
            set { _tbl90AuthorsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Source"

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList;
        public  ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
        {
            get => _tbl90SourcesAllList; 
            set { _tbl90SourcesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Expert"

        private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList;
        public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
        {
            get => _tbl90ExpertsAllList; 
            set { _tbl90ExpertsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90ReferenceAuthor"

        public ICollectionView ReferenceAuthorsView;
        private Tbl90Reference CurrentTbl90ReferenceAuthor => ReferenceAuthorsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceAuthorsList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceAuthorsList
        {
            get => _tbl90ReferenceAuthorsList; 
            set { _tbl90ReferenceAuthorsList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceSource"

        public ICollectionView ReferenceSourcesView;
        private Tbl90Reference CurrentTbl90ReferenceSource => ReferenceSourcesView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceSourcesList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceSourcesList
        {
            get => _tbl90ReferenceSourcesList; 
            set { _tbl90ReferenceSourcesList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceExpert"

        public ICollectionView ReferenceExpertsView;
        private Tbl90Reference CurrentTbl90ReferenceExpert => ReferenceExpertsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceExpertsList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceExpertsList
        {
            get => _tbl90ReferenceExpertsList; 
            set { _tbl90ReferenceExpertsList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   
 

   }
}   
