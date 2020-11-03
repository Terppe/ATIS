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

    
         //    CommentsViewModel Skriptdatum:  29.11.2018  10:32    

namespace ATIS.Ui.Views.Database.ListDetails
{     
    
    public class CommentsViewModel : ViewModelBase                     
    {  
        // Version with Generic Unit Of Work and AtisDbContext for general use   
         
        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();

        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private readonly GenericMessageBoxes<Tbl93Comment> _genCommentMessageBoxes = new GenericMessageBoxes<Tbl93Comment>();
        private readonly GenericMessageBoxes<NULL> _genNULLMessageBoxes = new GenericMessageBoxes<NULL>();
        private readonly GenericMessageBoxes<NULL> _genNULLMessageBoxes = new GenericMessageBoxes<NULL>();
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

        public CommentsViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {          
        
                // Code runs "for real" 
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment>();    
            }
        }     
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]         
 

 //    Part 1    

         

        #region [Commands Comment]

        private RelayCommand _getCommentsByNameOrIdCommand;
        public ICommand GetCommentsByNameOrIdCommand => _getCommentsByNameOrIdCommand ??= new RelayCommand(delegate {ExecuteGetCommentsByNameOrId(SearchCommentName); });    
             
        private RelayCommand _addCommentCommand;
        public ICommand AddCommentCommand => _addCommentCommand ??= new RelayCommand(delegate { ExecuteAddComment(null); });

        private RelayCommand _copyCommentCommand;
        public ICommand CopyCommentCommand => _copyCommentCommand ??= new RelayCommand(delegate { ExecuteCopyComment(null); });      
             
        private RelayCommand _deleteCommentCommand;
        public ICommand DeleteCommentCommand => _deleteCommentCommand ??= new RelayCommand(delegate { ExecuteDeleteComment(SearchCommentName); });    
             
        private RelayCommand _saveCommentCommand;
        public ICommand SaveCommentCommand => _saveCommentCommand ??= new RelayCommand(delegate { ExecuteSaveComment(SearchCommentName); });    

        #endregion [Commands Comment]       

CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
        
        private void ExecuteAddComment(object o)
        {  
            Tbl93CommentsList.Insert(0, new Tbl93Comment {   Info = CultRes.StringsRes.DatasetNew }  );

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

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
     
        private void ExecuteCopyComment(object o)
        {
            if (_genCommentMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            Tbl93CommentsList = _extCopy.CopyComment(CurrentTbl93Comment);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }                         
     
        private void ExecuteDeleteComment(string searchName)
        {
            if (_genCommentMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;               
 
    
            //check if in NULL connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return

            NULLList = _extDelete.SearchForConnectedDatasetsWithCommentIdInTableNULL(CurrentTbl93Comment);     
     
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(NULLList.Count, "NULL")) return;

            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extDelete.DeleteDatasetsWithCommentIdInTableReference(CurrentTbl93Comment);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extDelete.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extDelete.DeleteDatasetsWithCommentIdInTableComment(CurrentTbl93Comment);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extDelete.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }
            try
            {
                var comment= _uow.Tbl93Comments.GetById(CurrentTbl93Comment.CommentId);
                if (comment!= null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl93Comment.CommentName)) return;

                    _extDelete.DeleteComment(comment);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl93Comment.CommentName);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl93Comment.CommentName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGetCommentsByNameOrId(searchName);

            CommentsView.MoveCurrentToFirst();
        }                
     
        private void ExecuteSaveComment(string searchName)
        {
            if (_genCommentMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;      
       
            //Combobox select NULLID  may be not 0
            if (CurrentTbl93Comment.NULLId == 0)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }     
       
        private void SaveComment(object o)
        {
        }
        #endregion "Public Commands"                   
 
 

 //    Part 2    

     
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            ExecuteGetCommentsByNameOrId(searchName);
            CommentsView.MoveCurrentToPosition(_position);
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
                    if (CurrentTbl93Comment != null)
                    {
                        NULLList = _extGet.GetNULLCollectionOrderByFromNULLId<NULL>(CurrentTbl93Comment.NULLId);

                        Tbl66GenussesAllList = _extGet.AllCollection<Tbl66Genus>("");

                        NULLView = CollectionViewSource.GetDefaultView(NULLList);
                        NULLView.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }         
     
                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl93Comment != null)
                    {
                        NULLList = _extGet.GetNULLCollectionOrderByFromCommentId<NULL>(CurrentTbl93Comment.CommentId);

                        Tbl93CommentsAllList = _extGet.AllCollection<Tbl93Comment>("comment");

                        NULLView = CollectionViewSource.GetDefaultView(NULLList);
                        NULLView.Refresh();
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
                    if (CurrentTbl93Comment != null)
                    {
                        Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFromCommentId<Tbl93Comment>(CurrentTbl93Comment.CommentId);

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
                    if (CurrentTbl93Comment != null)
                    {
                        NULLList = _extGet.GetNULLCollectionOrderByFromNULLId<NULL>(CurrentTbl93Comment.NULLId);

                        NULLView = CollectionViewSource.GetDefaultView(NULLList);
                        NULLView.Refresh();
                    }
                    SelectedMainTabIndex = 0;  
               }     
     
                if (_selectedDetailTabIndex == 1)                
                {
                    SelectedMainTabIndex = 0;
                }    
     
                if (_selectedDetailTabIndex == 2)                
                {
                    if (CurrentTbl93Comment != null)
                    {
                        NULLList = _extGet.GetNULLCollectionOrderByFromCommentId<NULL>(CurrentTbl93Comment.CommentId);

                        Tbl93CommentsAllList = _extGet.AllCollection<Tbl93Comment>("comment");

                        NULLView = CollectionViewSource.GetDefaultView(NULLList);
                        NULLView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
               }    
     
                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTbl93Comment != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extGet.GetReferenceExpertsCollectionOrderByFromCommentIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(CurrentTbl93Comment.CommentId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }        
     
                if (_selectedDetailTabIndex == 4)
                {
                    if (CurrentTbl93Comment != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extGet.GetReferenceSourcesCollectionOrderByFromCommentIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl93Comment.CommentId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 1;
                }        
     
                if (_selectedDetailTabIndex == 5)
                {
                    if (CurrentTbl93Comment != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extGet.GetReferenceAuthorsCollectionOrderByFromCommentIdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl93Comment.CommentId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 2;
                }       
     
                if (_selectedDetailTabIndex == 6)
                {
                    if (CurrentTbl93Comment != null)
                    {
                        Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFromCommentId<Tbl93Comment>(CurrentTbl93Comment.CommentId);

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
                    if (CurrentTbl93Comment != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extGet.GetReferenceExpertsCollectionOrderByFromCommentIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(CurrentTbl93Comment.CommentId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                }        
     
                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (CurrentTbl93Comment != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extGet.GetReferenceSourcesCollectionOrderByFromCommentIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl93Comment.CommentId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 2;
                }      
     
                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (CurrentTbl93Comment != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extGet.GetReferenceAuthorsCollectionOrderByFromCommentIdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(CurrentTbl93Comment.CommentId);

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

        
        #region "Public Properties Tbl93Comment"

        private string _searchCommentInfo = "";
        public string SearchCommentInfo
        {
            get => _searchCommentInfo;
            set { _searchCommentInfo = value; RaisePropertyChanged(""); }
        }

        public ICollectionView CommentsView;
        public Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get => _tbl93CommentsList;
            set { _tbl93CommentsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl93Comment> _tbl93CommentsAllList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsAllList
        {
            get => _tbl93CommentsAllList;
            set { _tbl93CommentsAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
        {
            get => _tbl03RegnumsAllList;
            set { _tbl03RegnumsAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList;
        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
        {
            get => _tbl06PhylumsAllList;
            set { _tbl06PhylumsAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl09Division> _tbl09DivisionsAllList;
        public ObservableCollection<Tbl09Division> Tbl09DivisionsAllList
        {
            get => _tbl09DivisionsAllList;
            set { _tbl09DivisionsAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsAllList;
        public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsAllList
        {
            get => _tbl12SubphylumsAllList;
            set { _tbl12SubphylumsAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsAllList;
        public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsAllList
        {
            get => _tbl15SubdivisionsAllList;
            set { _tbl15SubdivisionsAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesAllList;
        public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesAllList
        {
            get => _tbl18SuperclassesAllList;
            set { _tbl18SuperclassesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl21Class> _tbl21ClassesAllList;
        public ObservableCollection<Tbl21Class> Tbl21ClassesAllList
        {
            get => _tbl21ClassesAllList;
            set { _tbl21ClassesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl24Subclass> _tbl24SubclassesAllList;
        public ObservableCollection<Tbl24Subclass> Tbl24SubclassesAllList
        {
            get => _tbl24SubclassesAllList;
            set { _tbl24SubclassesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesAllList;
        public ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesAllList
        {
            get => _tbl27InfraclassesAllList;
            set { _tbl27InfraclassesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl30Legio> _tbl30LegiosAllList;
        public ObservableCollection<Tbl30Legio> Tbl30LegiosAllList
        {
            get => _tbl30LegiosAllList;
            set { _tbl30LegiosAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl33Ordo> _tbl33OrdosAllList;
        public ObservableCollection<Tbl33Ordo> Tbl33OrdosAllList
        {
            get => _tbl33OrdosAllList;
            set { _tbl33OrdosAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl36Subordo> _tbl36SubordosAllList;
        public ObservableCollection<Tbl36Subordo> Tbl36SubordosAllList
        {
            get => _tbl36SubordosAllList;
            set { _tbl36SubordosAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl39Infraordo> _tbl39InfraordosAllList;
        public ObservableCollection<Tbl39Infraordo> Tbl39InfraordosAllList
        {
            get => _tbl39InfraordosAllList;
            set { _tbl39InfraordosAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl42Superfamily> _tbl42SuperfamiliesAllList;
        public ObservableCollection<Tbl42Superfamily> Tbl42SuperfamiliesAllList
        {
            get => _tbl42SuperfamiliesAllList;
            set { _tbl42SuperfamiliesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl45Family> _tbl45FamiliesAllList;
        public ObservableCollection<Tbl45Family> Tbl45FamiliesAllList
        {
            get => _tbl45FamiliesAllList;
            set { _tbl45FamiliesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl48Subfamily> _tbl48SubfamiliesAllList;
        public ObservableCollection<Tbl48Subfamily> Tbl48SubfamiliesAllList
        {
            get => _tbl48SubfamiliesAllList;
            set { _tbl48SubfamiliesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesAllList;
        public ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesAllList
        {
            get => _tbl51InfrafamiliesAllList;
            set { _tbl51InfrafamiliesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl54Supertribus> _tbl54SupertribussesAllList;
        public ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesAllList
        {
            get => _tbl54SupertribussesAllList;
            set { _tbl54SupertribussesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl57Tribus> _tbl57TribussesAllList;
        public ObservableCollection<Tbl57Tribus> Tbl57TribussesAllList
        {
            get => _tbl57TribussesAllList;
            set { _tbl57TribussesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesAllList;
        public ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesAllList
        {
            get => _tbl60SubtribussesAllList;
            set { _tbl60SubtribussesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesAllList;
        public ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesAllList
        {
            get => _tbl63InfratribussesAllList;
            set { _tbl63InfratribussesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList;
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList;
        public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
        {
            get => _tbl69FiSpeciessesAllList;
            set { _tbl69FiSpeciessesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesAllList;
        public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesAllList
        {
            get => _tbl72PlSpeciessesAllList;
            set { _tbl72PlSpeciessesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"     
 

   }
}   
