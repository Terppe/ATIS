using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using ATIS.Ui.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;

//    SubdivisionsViewModel Skriptdatum:  07.01.2021  12:32    

namespace ATIS.Ui.Views.Database.D15Subdivision
{

    public class SubdivisionsViewModel : ViewModelBase
    {
        // Version with Generic Unit Of Work and AtisDbContext for general use   

        #region [Private Data Members]
        private readonly AtisDbContext _context = new AtisDbContext();
        private readonly CrudFunctions _extCrud = new CrudFunctions();
        private readonly DeleteFunctions _extDelete = new DeleteFunctions();
        private readonly SaveFunctions _extSave = new SaveFunctions();
        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private int _position;

        #endregion [Private Data Members]               

        #region [Constructor]

        public SubdivisionsViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                // Code runs "for real" 
                Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          


        //    Part 1    



        #region [Commands Subdivision]

        private RelayCommand _getSubdivisionsByNameOrIdCommand;
        public ICommand GetSubdivisionsByNameOrIdCommand => _getSubdivisionsByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetSubdivisionsByNameOrId(SearchSubdivisionName); });

        private RelayCommand _addSubdivisionCommand;
        public ICommand AddSubdivisionCommand => _addSubdivisionCommand ??= new RelayCommand(delegate { ExecuteAddSubdivision(null); });

        private RelayCommand _copySubdivisionCommand;
        public ICommand CopySubdivisionCommand => _copySubdivisionCommand ??= new RelayCommand(delegate { ExecuteCopySubdivision(null); });

        private RelayCommand _deleteSubdivisionCommand;
        public ICommand DeleteSubdivisionCommand => _deleteSubdivisionCommand ??= new RelayCommand(delegate { ExecuteDeleteSubdivision(SearchSubdivisionName); });

        private RelayCommand _saveSubdivisionCommand;
        public ICommand SaveSubdivisionCommand => _saveSubdivisionCommand ??= new RelayCommand(delegate { ExecuteSaveSubdivision(SearchSubdivisionName); });

        #endregion [Commands Subdivision]       


        #region [Methods Subdivision]

        private void ExecuteGetSubdivisionsByNameOrId(string searchName)
        {
            if (Tbl09DivisionsAllList == null)
                Tbl09DivisionsAllList ??= new ObservableCollection<Tbl09Division>();
            else
                Tbl09DivisionsAllList.Clear();

            Tbl09DivisionsAllList = _extCrud.GetCollectionAllOrderBy<Tbl09Division>("Division");

            if (Tbl15SubdivisionsList == null)
                Tbl15SubdivisionsList ??= new ObservableCollection<Tbl15Subdivision>();
            else
                Tbl15SubdivisionsList.Clear();

            Tbl15SubdivisionsList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl15Subdivision>(searchName, "Subdivision");

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl15SubdivisionsList.Count)) return;

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            SubdivisionsView.Refresh();
        }

        private void ExecuteAddSubdivision(object o)
        {
            if (Tbl15SubdivisionsList == null)
                Tbl15SubdivisionsList ??= new ObservableCollection<Tbl15Subdivision>();
            else
                Tbl15SubdivisionsList.Clear();

            if (Tbl09DivisionsAllList == null)
                Tbl09DivisionsAllList ??= new ObservableCollection<Tbl09Division>();
            else
                Tbl09DivisionsAllList.Clear();

            Tbl09DivisionsAllList = _extCrud.GetCollectionAllOrderBy<Tbl09Division>("Division");

            Tbl15SubdivisionsList.Insert(0, new Tbl15Subdivision { SubdivisionName = CultRes.StringsRes.DatasetNew });

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            SubdivisionsView.MoveCurrentToFirst();
        }

        private void ExecuteCopySubdivision(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl15Subdivision)) return;

            Tbl15SubdivisionsList = _extCrud.CopySubdivision(CurrentTbl15Subdivision);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            SubdivisionsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteSubdivision(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl15Subdivision)) return;

            _extDelete.DeleteSubdivision(CurrentTbl15Subdivision);

            Tbl15SubdivisionsList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl15Subdivision>(searchName, "Subdivision");
            SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            SubdivisionsView.MoveCurrentToLast();
        }

        private void ExecuteSaveSubdivision(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl15Subdivision)) return;

            _position = SubdivisionsView.CurrentPosition;

            var ret = _extSave.SaveSubdivision(CurrentTbl15Subdivision);

            if (ret != true)
            {
                SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
                SubdivisionsView.Refresh();
                return;
            }

            if (CurrentTbl15Subdivision.SubdivisionId == 0) //new
            {
                Tbl15SubdivisionsList = _extCrud.GetLastSubdivisionsDatasetOrderById();
                SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
                SubdivisionsView.MoveCurrentToFirst();
            }
            else
            {
                Tbl15SubdivisionsList = _extCrud.GetSubdivisionsCollectionFromSearchNameOrIdOrderBy<Tbl15Subdivision>(searchName);
                SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
                SubdivisionsView.MoveCurrentToPosition(_position);
            }
        }
        #endregion [Methods Subdivision]                



        //    Part 2    


        #region "Public Commands Connect <== Tbl09Division"                 


        private RelayCommand _saveDivisionCommand;

        public ICommand SaveDivisionCommand => _saveDivisionCommand ??= new RelayCommand(delegate { ExecuteSaveDivision(null); });

        private void ExecuteSaveDivision(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl09Division)) return;

            _extSave.SaveDivision(CurrentTbl09Division);

            Tbl09DivisionsList = _extCrud.GetDivisionsCollectionFromDivisionIdOrderBy<Tbl09Division>(CurrentTbl15Subdivision.DivisionId);
            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            DivisionsView.Refresh();
        }

        #endregion "Public Commands"                  


        //    Part 3    





        //    Part 4    


        #region [Public Commands Connect ==> Tbl18Superclass]                 

        private RelayCommand _addSuperclassCommand;
        public ICommand AddSuperclassCommand => _addSuperclassCommand ??= new RelayCommand(delegate { ExecuteAddSuperclass(null); });

        private RelayCommand _copySuperclassCommand;
        public ICommand CopySuperclassCommand => _copySuperclassCommand ??= new RelayCommand(delegate { ExecuteCopySuperclass(null); });

        private RelayCommand _deleteSuperclassCommand;
        public ICommand DeleteSuperclassCommand => _deleteSuperclassCommand ??= new RelayCommand(delegate { ExecuteDeleteSuperclass(null); });

        private RelayCommand _saveSuperclassCommand;
        public ICommand SaveSuperclassCommand => _saveSuperclassCommand ??= new RelayCommand(delegate { ExecuteSaveSuperclass(null); });

        #endregion [Public Commands Connect ==> Tbl18Superclass]    

        #region [Public Methods Connect ==> Tbl18Superclass]                   

        private void ExecuteAddSuperclass(object o)
        {
            if (Tbl15SubdivisionsAllList == null)
                Tbl15SubdivisionsAllList ??= new ObservableCollection<Tbl15Subdivision>();
            else
                Tbl15SubdivisionsAllList.Clear();

            Tbl15SubdivisionsAllList = _extCrud.GetCollectionAllOrderBy<Tbl15Subdivision>("Subdivision");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            Tbl18SuperclassesList ??= new ObservableCollection<Tbl18Superclass>();

            Tbl18SuperclassesList.Insert(0, new Tbl18Superclass { SuperclassName = CultRes.StringsRes.DatasetNew });

            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.MoveCurrentToFirst();
        }

        private void ExecuteCopySuperclass(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl18Superclass)) return;

            Tbl18SuperclassesList = _extCrud.CopySuperclass(CurrentTbl18Superclass);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteSuperclass(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl18Superclass)) return;

            _extDelete.DeleteSuperclass(CurrentTbl18Superclass);

            Tbl18SuperclassesList = _extCrud.GetSuperclassesCollectionFromSubdivisionIdOrderBy<Tbl18Superclass>(CurrentTbl18Superclass.SuperclassId);
            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.MoveCurrentToFirst();
        }

        private void ExecuteSaveSuperclass(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl18Superclass)) return;

            CurrentTbl18Superclass.SubdivisionId = CurrentTbl15Subdivision.SubdivisionId;

            _extSave.SaveSuperclass(CurrentTbl18Superclass);

            //Search for CurrentTbl18Superclass.SubphylumID with Animalia#Regnum# 
            var plantaeRegnum = _context.Tbl12Subphylums.FirstOrDefault(e => e.SubphylumName == "Plantae#Regnum#");
            if (plantaeRegnum != null) CurrentTbl18Superclass.SubphylumId = plantaeRegnum.SubphylumId;

            Tbl18SuperclassesList = _extCrud.GetSuperclassesCollectionFromSubdivisionIdOrderBy<Tbl18Superclass>(CurrentTbl18Superclass.SubdivisionId);

            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.MoveCurrentToFirst();
        }

        #endregion [Public Methods  Connect ==> Tbl18Superclass]                                                                                                                                            



        //    Part 5    



        //    Part 6    




        //    Part 7    



        //    Part 8    


        #region [Commands Subdivision ==> Tbl90Reference Author]

        private RelayCommand _addReferenceAuthorCommand;

        public ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteAddReferenceAuthor(null); });

        private RelayCommand _copyReferenceAuthorCommand;

        public ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceAuthor(null); });

        private RelayCommand _deleteReferenceAuthorCommand;

        public ICommand DeleteReferenceAuthorCommand => _deleteReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceAuthor(null); });

        private RelayCommand _saveReferenceAuthorCommand;

        public ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceAuthor(null); });

        #endregion [Commands Subdivision ==> Tbl90Reference Author]                

        #region [Methods Subdivision ==> Tbl90Reference Author]

        public void ExecuteAddReferenceAuthor(object o)
        {
            Tbl90ReferenceAuthorsList ??= new ObservableCollection<Tbl90Reference>();

            Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("Author");
            Tbl90ReferenceAuthorsList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }

        public void ExecuteCopyReferenceAuthor(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceSubdivision(CurrentTbl90ReferenceAuthor, "Author");

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceAuthor(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            _extDelete.DeleteReferenceAuthor(CurrentTbl90ReferenceAuthor);

            Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromSubdivisionIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90ReferenceAuthor.SubdivisionId);
            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }

        public void ExecuteSaveReferenceAuthor(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            CurrentTbl90ReferenceAuthor.SubdivisionId = CurrentTbl15Subdivision.SubdivisionId;

            _extSave.SaveReferenceAuthor(CurrentTbl90ReferenceAuthor, "Subdivision");

            Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromSubdivisionIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl15Subdivision.SubdivisionId);

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }
        #endregion [Methods Subdivision ==> Tbl90Reference Author]              

        #region [Commands Subdivision ==> Tbl90Reference Source]      

        private RelayCommand _addReferenceSourceCommand;

        public ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteAddReferenceSource(null); });

        private RelayCommand _copyReferenceSourceCommand;

        public ICommand CopyReferenceSourceCommand => _copyReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceSource(null); });

        private RelayCommand _deleteReferenceSourceCommand;

        public ICommand DeleteReferenceSourceCommand => _deleteReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceSource(null); });

        private RelayCommand _saveReferenceSourceCommand;

        public ICommand SaveReferenceSourceCommand => _saveReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceSource(null); });


        #endregion [Commands Subdivision ==> Tbl90Reference Source]         

        #region [Methods Subdivision ==> Tbl90Reference Source]      

        public void ExecuteAddReferenceSource(object o)
        {
            Tbl90ReferenceSourcesList ??= new ObservableCollection<Tbl90Reference>();

            Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

            Tbl90ReferenceSourcesList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        public void ExecuteCopyReferenceSource(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceSubdivision(CurrentTbl90ReferenceSource, "Source");

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceSource(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            _extDelete.DeleteReferenceSource(CurrentTbl90ReferenceSource);

            Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromSubdivisionIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl15Subdivision.SubdivisionId);
            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        public void ExecuteSaveReferenceSource(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            CurrentTbl90ReferenceSource.SubdivisionId = CurrentTbl15Subdivision.SubdivisionId;

            _extSave.SaveReferenceSource(CurrentTbl90ReferenceSource, "Subdivision");

            Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromSubdivisionIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl15Subdivision.SubdivisionId);


            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }
        #endregion [Methods Subdivision ==> Tbl90Reference Source]                    

        #region [Commands Subdivision ==> Tbl90Reference Expert]                 

        private RelayCommand _addReferenceExpertCommand;

        public ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteAddReferenceExpert(null); });

        private RelayCommand _copyReferenceExpertCommand;

        public ICommand CopyReferenceExpertCommand => _copyReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceExpert(null); });

        private RelayCommand _deleteReferenceExpertCommand;

        public ICommand DeleteReferenceExpertCommand => _deleteReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceExpert(null); });
        private RelayCommand _saveReferenceExpertCommand;

        public ICommand SaveReferenceExpertCommand => _saveReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceExpert(null); });

        #endregion [Commands Subdivision ==> Tbl90Reference Expert]                    


        #region [Methods Subdivision ==> Tbl90Reference Expert]                 

        public void ExecuteAddReferenceExpert(object o)
        {
            Tbl90ReferenceExpertsList ??= new ObservableCollection<Tbl90Reference>();

            Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");
            Tbl90ReferenceExpertsList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }

        public void ExecuteCopyReferenceExpert(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            Tbl90ReferenceExpertsList = _extCrud.CopyReferenceSubdivision(CurrentTbl90ReferenceExpert, "Expert");

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceExpert(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            _extDelete.DeleteReferenceExpert(CurrentTbl90ReferenceExpert);

            Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromSubdivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl15Subdivision.SubdivisionId);
            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }

        public void ExecuteSaveReferenceExpert(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            CurrentTbl90ReferenceExpert.SubdivisionId = CurrentTbl15Subdivision.SubdivisionId;

            _extSave.SaveReferenceExpert(CurrentTbl90ReferenceExpert, "Subdivision");

            Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromSubdivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl15Subdivision.SubdivisionId);


            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }
        #endregion [Methods Subdivision ==> Tbl90Reference Expert]                               

        #region [Commands Subdivision ==> Tbl93Comments]        

        private RelayCommand _addCommentCommand;

        public ICommand AddCommentCommand => _addCommentCommand ??= new RelayCommand(delegate { ExecuteAddComment(null); });

        private RelayCommand _copyCommentCommand;

        public ICommand CopyCommentCommand => _copyCommentCommand ??= new RelayCommand(delegate { ExecuteCopyComment(null); });

        private RelayCommand _deleteCommentCommand;

        public ICommand DeleteCommentCommand => _deleteCommentCommand ??= new RelayCommand(delegate { ExecuteDeleteComment(null); });

        private RelayCommand _saveCommentCommand;

        public ICommand SaveCommentCommand => _saveCommentCommand ??= new RelayCommand(delegate { ExecuteSaveComment(null); });

        #endregion [Commands Subdivision ==> Tbl93Comments]        



        #region [Methods Subdivision ==> Tbl93Comments]        

        private void ExecuteAddComment(object o)
        {
            Tbl93CommentsList ??= new ObservableCollection<Tbl93Comment>();

            Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = CultRes.StringsRes.DatasetNew });

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }

        private void ExecuteCopyComment(object o)
        {

            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            Tbl93CommentsList = _extCrud.CopyComment(CurrentTbl93Comment, "Comment");

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteComment(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            _extDelete.DeleteComment(CurrentTbl93Comment);

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromSubdivisionIdOrderBy<Tbl93Comment>(CurrentTbl15Subdivision.SubdivisionId);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }

        private void ExecuteSaveComment(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            CurrentTbl93Comment.SubdivisionId = CurrentTbl15Subdivision.SubdivisionId;

            _extSave.SaveComment(CurrentTbl93Comment, "Subdivision");

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromSubdivisionIdOrderBy<Tbl93Comment>(CurrentTbl15Subdivision.SubdivisionId);


            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        #endregion [Methods Subdivision ==> Tbl93Comments]                 


        //    Part 9    



        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        #endregion "Public Commands Connected Tables by DoubleClick"

        #region "Public Method Connected Tables by DoubleClick"

        private void GetConnectedTablesById(object o)
        {
            Tbl09DivisionsList = _extCrud.GetDivisionsCollectionFromDivisionIdOrderBy<Tbl09Division>(CurrentTbl15Subdivision.DivisionId);

            Tbl03RegnumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl03Regnum>("Regnum");

            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            DivisionsView.Refresh();

        }

        #endregion "Public Method Connected Tables by DoubleClick"     



        //    Part 10    


        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        private int _selectedMainSubRefTabIndex;
        private int _selectedDetailTabIndex;


        public int SelectedMainTabIndex
        {
            get => _selectedMainTabIndex;
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value; RaisePropertyChanged("");

                if (_selectedMainTabIndex == 0)
                {
                    if (CurrentTbl15Subdivision != null)
                    {
                        Tbl09DivisionsList = _extCrud.GetDivisionsCollectionFromDivisionIdOrderBy<Tbl09Division>(CurrentTbl15Subdivision.DivisionId);

                        Tbl03RegnumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl03Regnum>("Regnum");

                        DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
                        DivisionsView.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }

                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl15Subdivision != null)
                    {
                        Tbl18SuperclassesList = _extCrud.GetSuperclassesCollectionFromSubdivisionIdOrderBy<Tbl18Superclass>(CurrentTbl15Subdivision.SubdivisionId);

                        Tbl15SubdivisionsAllList = _extCrud.GetCollectionAllOrderBy<Tbl15Subdivision>("Subdivision");

                        SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
                        SuperclassesView.Refresh();
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
                    if (CurrentTbl15Subdivision != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromSubdivisionIdOrderBy<Tbl93Comment>(CurrentTbl15Subdivision.SubdivisionId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedDetailTabIndex = 6;
                }

            }
        }

        public int SelectedDetailTabIndex
        {
            get => _selectedDetailTabIndex;
            set
            {
                if (value == _selectedDetailTabIndex) return;
                _selectedDetailTabIndex = value; RaisePropertyChanged("");

                if (_selectedDetailTabIndex == 0)
                {
                    if (CurrentTbl15Subdivision != null)
                    {
                        Tbl09DivisionsList = _extCrud.GetDivisionsCollectionFromDivisionIdOrderBy<Tbl09Division>(CurrentTbl15Subdivision.DivisionId);

                        DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
                        DivisionsView.Refresh();
                    }
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 1)
                {
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 2)
                {
                    if (CurrentTbl15Subdivision != null)
                    {
                        Tbl18SuperclassesList = _extCrud.GetSuperclassesCollectionFromSubdivisionIdOrderBy<Tbl18Superclass>(CurrentTbl15Subdivision.SubdivisionId);

                        Tbl15SubdivisionsAllList = _extCrud.GetCollectionAllOrderBy<Tbl15Subdivision>("Subdivision");

                        SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
                        SuperclassesView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTbl15Subdivision != null)
                    {
                        Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromSubdivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl15Subdivision.SubdivisionId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 4)
                {
                    if (CurrentTbl15Subdivision != null)
                    {
                        Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromSubdivisionIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl15Subdivision.SubdivisionId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 5)
                {
                    if (CurrentTbl15Subdivision != null)
                    {
                        Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("Author");

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromSubdivisionIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl15Subdivision.SubdivisionId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 2;
                }

                if (_selectedDetailTabIndex == 6)
                {
                    if (CurrentTbl15Subdivision != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromSubdivisionIdOrderBy<Tbl93Comment>(CurrentTbl15Subdivision.SubdivisionId);

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
                _selectedMainSubRefTabIndex = value; RaisePropertyChanged("");

                if (_selectedMainSubRefTabIndex == 0)
                {
                    if (CurrentTbl15Subdivision != null)
                    {
                        Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromSubdivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl15Subdivision.SubdivisionId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                }

                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (CurrentTbl15Subdivision != null)
                    {
                        Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromSubdivisionIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl15Subdivision.SubdivisionId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 2;
                }

                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (CurrentTbl15Subdivision != null)
                    {
                        Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("Author");

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromSubdivisionIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl15Subdivision.SubdivisionId);

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


        #region "Public Properties Tbl15Subdivision"

        private string _searchSubdivisionName = "";
        public string SearchSubdivisionName
        {
            get => _searchSubdivisionName;
            set { _searchSubdivisionName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView SubdivisionsView;
        private Tbl15Subdivision CurrentTbl15Subdivision => SubdivisionsView?.CurrentItem as Tbl15Subdivision;

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsList;
        public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsList
        {
            get => _tbl15SubdivisionsList;
            set { _tbl15SubdivisionsList = value; RaisePropertyChanged(""); }
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

        #endregion "Public Properties"   

        #region "Public Properties Tbl09Division"

        public ICollectionView DivisionsView;
        private Tbl09Division CurrentTbl09Division => DivisionsView?.CurrentItem as Tbl09Division;

        private ObservableCollection<Tbl09Division> _tbl09DivisionsList;
        public ObservableCollection<Tbl09Division> Tbl09DivisionsList
        {
            get => _tbl09DivisionsList;
            set { _tbl09DivisionsList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl09Division> _tbl09DivisionsAllList;
        public ObservableCollection<Tbl09Division> Tbl09DivisionsAllList
        {
            get => _tbl09DivisionsAllList;
            set { _tbl09DivisionsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   

        #region "Public Properties Tbl18Superclass"

        public ICollectionView SuperclassesView;
        private Tbl18Superclass CurrentTbl18Superclass => SuperclassesView?.CurrentItem as Tbl18Superclass;

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesList;
        public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesList
        {
            get => _tbl18SuperclassesList;
            set { _tbl18SuperclassesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl21Class"

        public ICollectionView ClassesView;
        private Tbl21Class CurrentTbl21Class => ClassesView?.CurrentItem as Tbl21Class;

        private ObservableCollection<Tbl21Class> _tbl21ClassesList;
        public ObservableCollection<Tbl21Class> Tbl21ClassesList
        {
            get => _tbl21ClassesList;
            set { _tbl21ClassesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl03Regnum"

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
        {
            get => _tbl03RegnumsAllList;
            set { _tbl03RegnumsAllList = value; RaisePropertyChanged(""); }
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
        public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
        {
            get => _tbl90AuthorsAllList;
            set { _tbl90AuthorsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Source"

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList;
        public ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
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

        #region "Public Properties Tbl93Comment"

        public ICollectionView CommentsView;
        private Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get => _tbl93CommentsList;
            set { _tbl93CommentsList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"     


    }
}
