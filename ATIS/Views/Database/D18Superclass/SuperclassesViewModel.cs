using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;

//    SuperclassesViewModel Skriptdatum:  07.01.2021  12:32    

namespace ATIS.Ui.Views.Database.D18Superclass
{

    public class SuperclassesViewModel : ViewModelBase
    {
        // Version with Generic Unit Of Work and AtisDbContext for general use   

        #region [Private Data Members]
        private readonly CrudFunctions _extCrud = new CrudFunctions();
        private readonly DeleteFunctions _extDelete = new DeleteFunctions();
        private readonly SaveFunctions _extSave = new SaveFunctions();
        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private int _position;

        #endregion [Private Data Members]               

        #region [Constructor]

        public SuperclassesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                // Code runs "for real" 
                Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          


        //    Part 1    



        #region [Commands Superclass]

        private RelayCommand _getSuperclassesByNameOrIdCommand;
        public ICommand GetSuperclassesByNameOrIdCommand => _getSuperclassesByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetSuperclassesByNameOrId(SearchSuperclassName); });

        private RelayCommand _addSuperclassCommand;
        public ICommand AddSuperclassCommand => _addSuperclassCommand ??= new RelayCommand(delegate { ExecuteAddSuperclass(null); });

        private RelayCommand _copySuperclassCommand;
        public ICommand CopySuperclassCommand => _copySuperclassCommand ??= new RelayCommand(delegate { ExecuteCopySuperclass(null); });

        private RelayCommand _deleteSuperclassCommand;
        public ICommand DeleteSuperclassCommand => _deleteSuperclassCommand ??= new RelayCommand(delegate { ExecuteDeleteSuperclass(SearchSuperclassName); });

        private RelayCommand _saveSuperclassCommand;
        public ICommand SaveSuperclassCommand => _saveSuperclassCommand ??= new RelayCommand(delegate { ExecuteSaveSuperclass(SearchSuperclassName); });

        #endregion [Commands Superclass]       


        #region [Methods Superclass]

        private void ExecuteGetSuperclassesByNameOrId(string searchName)
        {
            if (Tbl12SubphylumsAllList == null)
                Tbl12SubphylumsAllList ??= new ObservableCollection<Tbl12Subphylum>();
            else
                Tbl12SubphylumsAllList.Clear();

            if (Tbl15SubdivisionsAllList == null)
                Tbl15SubdivisionsAllList ??= new ObservableCollection<Tbl15Subdivision>();
            else
                Tbl15SubdivisionsAllList.Clear();

            if (Tbl18SuperclassesList == null)
                Tbl18SuperclassesList ??= new ObservableCollection<Tbl18Superclass>();
            else
                Tbl18SuperclassesList.Clear();

            Tbl12SubphylumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl12Subphylum>("Subphylum");
            Tbl15SubdivisionsAllList = _extCrud.GetCollectionAllOrderBy<Tbl15Subdivision>("Subdivision");
            Tbl18SuperclassesList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl18Superclass>(searchName, "Superclass");

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl18SuperclassesList.Count)) return;

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.Refresh();
        }

        private void ExecuteAddSuperclass(object o)
        {
            if (Tbl18SuperclassesList == null)
                Tbl18SuperclassesList ??= new ObservableCollection<Tbl18Superclass>();
            else
                Tbl18SuperclassesList.Clear();

            if (Tbl12SubphylumsAllList == null)
                Tbl12SubphylumsAllList ??= new ObservableCollection<Tbl12Subphylum>();
            else
                Tbl12SubphylumsAllList.Clear();

            if (Tbl15SubdivisionsAllList == null)
                Tbl15SubdivisionsAllList ??= new ObservableCollection<Tbl15Subdivision>();
            else
                Tbl15SubdivisionsAllList.Clear();

            Tbl18SuperclassesList.Insert(0, new Tbl18Superclass { SuperclassName = CultRes.StringsRes.DatasetNew });

            Tbl12SubphylumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl12Subphylum>("Subphylum");
            Tbl15SubdivisionsAllList = _extCrud.GetCollectionAllOrderBy<Tbl15Subdivision>("Subdivision");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 2;

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

        private void ExecuteDeleteSuperclass(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl18Superclass)) return;

            _extDelete.DeleteSuperclass(CurrentTbl18Superclass);

            Tbl18SuperclassesList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl18Superclass>(searchName, "Superclass");
            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.MoveCurrentToLast();
        }

        private void ExecuteSaveSuperclass(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl18Superclass)) return;

            _position = SuperclassesView.CurrentPosition;

            var ret = _extSave.SaveSuperclass(CurrentTbl18Superclass);

            if (ret != true)
            {
                SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
                SuperclassesView.Refresh();
                return;
            }

            if (CurrentTbl18Superclass.SuperclassId == 0) //new
            {
                Tbl18SuperclassesList = _extCrud.GetLastSuperclassesDatasetOrderById();
                SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
                SuperclassesView.MoveCurrentToFirst();
            }
            else
            {
                Tbl18SuperclassesList = _extCrud.GetSuperclassesCollectionFromSearchNameOrIdOrderBy<Tbl18Superclass>(searchName);
                SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
                SuperclassesView.MoveCurrentToPosition(_position);
            }
        }
        #endregion [Methods Superclass]                



        //    Part 2    


        #region "Public Commands Connect <== Tbl12Subphylum"                 


        private RelayCommand _saveSubphylumCommand;

        public ICommand SaveSubphylumCommand => _saveSubphylumCommand ??= new RelayCommand(delegate { ExecuteSaveSubphylum(null); });

        private void ExecuteSaveSubphylum(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl12Subphylum)) return;

            _extSave.SaveSubphylum(CurrentTbl12Subphylum);

            Tbl12SubphylumsList = _extCrud.GetSubphylumsCollectionFromSubphylumIdOrderBy<Tbl12Subphylum>(CurrentTbl18Superclass.SubphylumId);
            SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            SubphylumsView.Refresh();
        }

        #endregion "Public Commands"                  


        //    Part 3    


        #region "Public Commands Connect <== Tbl15Subdivision"                 

        private RelayCommand _saveSubdivisionCommand;

        public ICommand SaveSubdivisionCommand =>
                            _saveSubdivisionCommand ??= new RelayCommand(delegate { ExecuteSaveSubdivision(null); });


        private void ExecuteSaveSubdivision(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl15Subdivision)) return;

            _extSave.SaveSubdivision(CurrentTbl15Subdivision);

            Tbl15SubdivisionsList = _extCrud.GetSubdivisionsCollectionFromSubdivisionIdOrderBy<Tbl15Subdivision>(CurrentTbl18Superclass.SubdivisionId);
            SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            SubdivisionsView.Refresh();
        }

        #endregion "Public Commands"                  




        //    Part 4    


        #region [Public Commands Connect ==> Tbl21Class]                 

        private RelayCommand _addClassCommand;
        public ICommand AddClassCommand => _addClassCommand ??= new RelayCommand(delegate { ExecuteAddClass(null); });

        private RelayCommand _copyClassCommand;
        public ICommand CopyClassCommand => _copyClassCommand ??= new RelayCommand(delegate { ExecuteCopyClass(null); });

        private RelayCommand _deleteClassCommand;
        public ICommand DeleteClassCommand => _deleteClassCommand ??= new RelayCommand(delegate { ExecuteDeleteClass(null); });

        private RelayCommand _saveClassCommand;
        public ICommand SaveClassCommand => _saveClassCommand ??= new RelayCommand(delegate { ExecuteSaveClass(null); });

        #endregion [Public Commands Connect ==> Tbl21Class]    

        #region [Public Methods Connect ==> Tbl21Class]                   

        private void ExecuteAddClass(object o)
        {
            if (Tbl18SuperclassesAllList == null)
                Tbl18SuperclassesAllList ??= new ObservableCollection<Tbl18Superclass>();
            else
                Tbl18SuperclassesAllList.Clear();

            Tbl18SuperclassesAllList = _extCrud.GetCollectionAllOrderBy<Tbl18Superclass>("Superclass");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            Tbl21ClassesList ??= new ObservableCollection<Tbl21Class>();

            Tbl21ClassesList.Insert(0, new Tbl21Class { ClassName = CultRes.StringsRes.DatasetNew });

            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            ClassesView.MoveCurrentToFirst();
        }

        private void ExecuteCopyClass(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl21Class)) return;

            Tbl21ClassesList = _extCrud.CopyClass(CurrentTbl21Class);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            ClassesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteClass(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl21Class)) return;

            _extDelete.DeleteClass(CurrentTbl21Class);

            Tbl21ClassesList = _extCrud.GetClassesCollectionFromSuperclassIdOrderBy<Tbl21Class>(CurrentTbl21Class.ClassId);
            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            ClassesView.MoveCurrentToFirst();
        }

        private void ExecuteSaveClass(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl21Class)) return;

            CurrentTbl21Class.SuperclassId = CurrentTbl18Superclass.SuperclassId;

            _extSave.SaveClass(CurrentTbl21Class);
            Tbl21ClassesList = _extCrud.GetClassesCollectionFromSuperclassIdOrderBy<Tbl21Class>(CurrentTbl21Class.SuperclassId);

            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            ClassesView.MoveCurrentToFirst();
        }

        #endregion [Public Methods  Connect ==> Tbl21Class]                                                                                                                                            



        //    Part 5    



        //    Part 6    




        //    Part 7    



        //    Part 8    


        #region [Commands Superclass ==> Tbl90Reference Author]

        private RelayCommand _addReferenceAuthorCommand;

        public ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteAddReferenceAuthor(null); });

        private RelayCommand _copyReferenceAuthorCommand;

        public ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceAuthor(null); });

        private RelayCommand _deleteReferenceAuthorCommand;

        public ICommand DeleteReferenceAuthorCommand => _deleteReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceAuthor(null); });

        private RelayCommand _saveReferenceAuthorCommand;

        public ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceAuthor(null); });

        #endregion [Commands Superclass ==> Tbl90Reference Author]                

        #region [Methods Superclass ==> Tbl90Reference Author]

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

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceSuperclass(CurrentTbl90ReferenceAuthor, "Author");

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceAuthor(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            _extDelete.DeleteReferenceAuthor(CurrentTbl90ReferenceAuthor);

            Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromSuperclassIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90ReferenceAuthor.SuperclassId);
            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }

        public void ExecuteSaveReferenceAuthor(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            CurrentTbl90ReferenceAuthor.SuperclassId = CurrentTbl18Superclass.SuperclassId;

            _extSave.SaveReferenceAuthor(CurrentTbl90ReferenceAuthor, "Superclass");

            Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromSuperclassIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl18Superclass.SuperclassId);

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }
        #endregion [Methods Superclass ==> Tbl90Reference Author]              

        #region [Commands Superclass ==> Tbl90Reference Source]      

        private RelayCommand _addReferenceSourceCommand;

        public ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteAddReferenceSource(null); });

        private RelayCommand _copyReferenceSourceCommand;

        public ICommand CopyReferenceSourceCommand => _copyReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceSource(null); });

        private RelayCommand _deleteReferenceSourceCommand;

        public ICommand DeleteReferenceSourceCommand => _deleteReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceSource(null); });

        private RelayCommand _saveReferenceSourceCommand;

        public ICommand SaveReferenceSourceCommand => _saveReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceSource(null); });


        #endregion [Commands Superclass ==> Tbl90Reference Source]         

        #region [Methods Superclass ==> Tbl90Reference Source]      

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

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceSuperclass(CurrentTbl90ReferenceSource, "Source");

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceSource(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            _extDelete.DeleteReferenceSource(CurrentTbl90ReferenceSource);

            Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromSuperclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl18Superclass.SuperclassId);
            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        public void ExecuteSaveReferenceSource(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            CurrentTbl90ReferenceSource.SuperclassId = CurrentTbl18Superclass.SuperclassId;

            _extSave.SaveReferenceSource(CurrentTbl90ReferenceSource, "Superclass");

            Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromSuperclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl18Superclass.SuperclassId);


            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }
        #endregion [Methods Superclass ==> Tbl90Reference Source]                    

        #region [Commands Superclass ==> Tbl90Reference Expert]                 

        private RelayCommand _addReferenceExpertCommand;

        public ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteAddReferenceExpert(null); });

        private RelayCommand _copyReferenceExpertCommand;

        public ICommand CopyReferenceExpertCommand => _copyReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceExpert(null); });

        private RelayCommand _deleteReferenceExpertCommand;

        public ICommand DeleteReferenceExpertCommand => _deleteReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceExpert(null); });
        private RelayCommand _saveReferenceExpertCommand;

        public ICommand SaveReferenceExpertCommand => _saveReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceExpert(null); });

        #endregion [Commands Superclass ==> Tbl90Reference Expert]                    


        #region [Methods Superclass ==> Tbl90Reference Expert]                 

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

            Tbl90ReferenceExpertsList = _extCrud.CopyReferenceSuperclass(CurrentTbl90ReferenceExpert, "Expert");

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceExpert(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            _extDelete.DeleteReferenceExpert(CurrentTbl90ReferenceExpert);

            Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromSuperclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl18Superclass.SuperclassId);
            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }

        public void ExecuteSaveReferenceExpert(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            CurrentTbl90ReferenceExpert.SuperclassId = CurrentTbl18Superclass.SuperclassId;

            _extSave.SaveReferenceExpert(CurrentTbl90ReferenceExpert, "Superclass");

            Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromSuperclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl18Superclass.SuperclassId);


            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }
        #endregion [Methods Superclass ==> Tbl90Reference Expert]                               

        #region [Commands Superclass ==> Tbl93Comments]        

        private RelayCommand _addCommentCommand;

        public ICommand AddCommentCommand => _addCommentCommand ??= new RelayCommand(delegate { ExecuteAddComment(null); });

        private RelayCommand _copyCommentCommand;

        public ICommand CopyCommentCommand => _copyCommentCommand ??= new RelayCommand(delegate { ExecuteCopyComment(null); });

        private RelayCommand _deleteCommentCommand;

        public ICommand DeleteCommentCommand => _deleteCommentCommand ??= new RelayCommand(delegate { ExecuteDeleteComment(null); });

        private RelayCommand _saveCommentCommand;

        public ICommand SaveCommentCommand => _saveCommentCommand ??= new RelayCommand(delegate { ExecuteSaveComment(null); });

        #endregion [Commands Superclass ==> Tbl93Comments]        



        #region [Methods Superclass ==> Tbl93Comments]        

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

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromSuperclassIdOrderBy<Tbl93Comment>(CurrentTbl18Superclass.SuperclassId);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }

        private void ExecuteSaveComment(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            CurrentTbl93Comment.SuperclassId = CurrentTbl18Superclass.SuperclassId;

            _extSave.SaveComment(CurrentTbl93Comment, "Superclass");

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromSuperclassIdOrderBy<Tbl93Comment>(CurrentTbl18Superclass.SuperclassId);


            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        #endregion [Methods Superclass ==> Tbl93Comments]                 


        //    Part 9    



        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        #endregion "Public Commands Connected Tables by DoubleClick"

        #region "Public Method Connected Tables by DoubleClick"

        private void GetConnectedTablesById(object o)
        {
            Tbl12SubphylumsList = _extCrud.GetSubphylumsCollectionFromSubphylumIdOrderBy<Tbl12Subphylum>(CurrentTbl18Superclass.SubphylumId);

            Tbl06PhylumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl06Phylum>("Phylum");

            SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            SubphylumsView.Refresh();

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 2;

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
                    if (CurrentTbl18Superclass != null)
                    {
                        Tbl12SubphylumsList = _extCrud.GetSubphylumsCollectionFromSubphylumIdOrderBy<Tbl12Subphylum>(CurrentTbl18Superclass.SubphylumId);

                        Tbl06PhylumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl06Phylum>("Phylum");

                        SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
                        SubphylumsView.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }

                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl18Superclass != null)
                    {
                        Tbl15SubdivisionsList = _extCrud.GetSubdivisionsCollectionFromSubdivisionIdOrderBy<Tbl15Subdivision>(CurrentTbl18Superclass.SubdivisionId);

                        Tbl09DivisionsAllList = _extCrud.GetCollectionAllOrderBy<Tbl09Division>("Division");

                        SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
                        SubdivisionsView.Refresh();
                    }
                    SelectedDetailTabIndex = 1;
                }

                if (_selectedMainTabIndex == 2)
                {
                    if (CurrentTbl18Superclass != null)
                    {
                        Tbl21ClassesList = _extCrud.GetClassesCollectionFromSuperclassIdOrderBy<Tbl21Class>(CurrentTbl18Superclass.SuperclassId);

                        Tbl18SuperclassesAllList = _extCrud.GetCollectionAllOrderBy<Tbl18Superclass>("Superclass");

                        ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
                        ClassesView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                }

                if (_selectedMainTabIndex == 3)
                {
                    SelectedDetailTabIndex = 4;
                    SelectedMainSubRefTabIndex = 0;
                }

                if (_selectedMainTabIndex == 4)
                {
                    if (CurrentTbl18Superclass != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromSuperclassIdOrderBy<Tbl93Comment>(CurrentTbl18Superclass.SuperclassId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedDetailTabIndex = 7;
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
                    if (CurrentTbl18Superclass != null)
                    {
                        Tbl12SubphylumsList = _extCrud.GetSubphylumsCollectionFromSubphylumIdOrderBy<Tbl12Subphylum>(CurrentTbl18Superclass.SubphylumId);

                        SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
                        SubphylumsView.Refresh();
                    }
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 1)
                {
                    if (CurrentTbl18Superclass != null)
                    {
                        Tbl15SubdivisionsList = _extCrud.GetSubdivisionsCollectionFromSubdivisionIdOrderBy<Tbl15Subdivision>(CurrentTbl18Superclass.SubdivisionId);

                        Tbl09DivisionsAllList = _extCrud.GetCollectionAllOrderBy<Tbl09Division>("Division");

                        SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
                        SubdivisionsView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 2)
                {
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTbl18Superclass != null)
                    {
                        Tbl21ClassesList = _extCrud.GetClassesCollectionFromSuperclassIdOrderBy<Tbl21Class>(CurrentTbl18Superclass.SuperclassId);

                        Tbl18SuperclassesAllList = _extCrud.GetCollectionAllOrderBy<Tbl18Superclass>("Superclass");

                        ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
                        ClassesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                }

                if (_selectedDetailTabIndex == 4)
                {
                    if (CurrentTbl18Superclass != null)
                    {
                        Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromSuperclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl18Superclass.SuperclassId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                    SelectedMainSubRefTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 5)
                {
                    if (CurrentTbl18Superclass != null)
                    {
                        Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromSuperclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl18Superclass.SuperclassId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                    SelectedMainSubRefTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 6)
                {
                    if (CurrentTbl18Superclass != null)
                    {
                        Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("Author");

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromSuperclassIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl18Superclass.SuperclassId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                    SelectedMainSubRefTabIndex = 2;
                }

                if (_selectedDetailTabIndex == 7)
                {
                    if (CurrentTbl18Superclass != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromSuperclassIdOrderBy<Tbl93Comment>(CurrentTbl18Superclass.SuperclassId);

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
                _selectedMainSubRefTabIndex = value; RaisePropertyChanged("");

                if (_selectedMainSubRefTabIndex == 0)
                {
                    if (CurrentTbl18Superclass != null)
                    {
                        Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");
                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromSuperclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl18Superclass.SuperclassId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 3;
                }

                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (CurrentTbl18Superclass != null)
                    {
                        Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromSuperclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl18Superclass.SuperclassId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 5;
                    SelectedMainTabIndex = 3;
                }

                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (CurrentTbl18Superclass != null)
                    {
                        Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("Author");

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromSuperclassIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl18Superclass.SuperclassId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedDetailTabIndex = 6;
                    SelectedMainTabIndex = 3;
                }

            }
        }
        #endregion "Public Commands to open Detail TabItems"          


        //    Part 11    


        #region "Public Properties Tbl18Superclass"

        private string _searchSuperclassName = "";
        public string SearchSuperclassName
        {
            get => _searchSuperclassName;
            set { _searchSuperclassName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView SuperclassesView;
        private Tbl18Superclass CurrentTbl18Superclass => SuperclassesView?.CurrentItem as Tbl18Superclass;

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesList;
        public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesList
        {
            get => _tbl18SuperclassesList;
            set { _tbl18SuperclassesList = value; RaisePropertyChanged(""); }
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

        #endregion "Public Properties"   

        #region "Public Properties Tbl12Subphylum"

        public ICollectionView SubphylumsView;
        private Tbl12Subphylum CurrentTbl12Subphylum => SubphylumsView?.CurrentItem as Tbl12Subphylum;

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsList;
        public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsList
        {
            get => _tbl12SubphylumsList;
            set { _tbl12SubphylumsList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsAllList;
        public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsAllList
        {
            get => _tbl12SubphylumsAllList;
            set { _tbl12SubphylumsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   


        #region "Public Properties Tbl15Subdivision"

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

        #region "Public Properties Tbl24Subclass"

        public ICollectionView SubclassesView;
        private Tbl24Subclass CurrentTbl24Subclass => SubclassesView?.CurrentItem as Tbl24Subclass;

        private ObservableCollection<Tbl24Subclass> _tbl24SubclassesList;
        public ObservableCollection<Tbl24Subclass> Tbl24SubclassesList
        {
            get => _tbl24SubclassesList;
            set { _tbl24SubclassesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl06Phylum"

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList;
        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
        {
            get => _tbl06PhylumsAllList;
            set { _tbl06PhylumsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"     

        #region "Public Properties Tbl09Division"

        private ObservableCollection<Tbl09Division> _tbl09DivisionsAllList;
        public ObservableCollection<Tbl09Division> Tbl09DivisionsAllList
        {
            get => _tbl09DivisionsAllList;
            set { _tbl09DivisionsAllList = value; RaisePropertyChanged(""); }
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
