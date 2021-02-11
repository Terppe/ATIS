using System;
using System.Collections.ObjectModel;
using System.ComponentModel;


using System.Windows.Data;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using log4net;

//    DivisionsViewModel Skriptdatum:  06.01.2021  12:32    

namespace ATIS.Ui.Views.Database.D09Division
{

    public class DivisionsViewModel : ViewModelBase
    {
        // Version with Generic Unit Of Work and AtisDbContext for general use   

        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(typeof(DivisionsViewModel));
        private readonly CrudFunctions _extCrud = new CrudFunctions();
        private readonly DeleteFunctions _extDelete = new DeleteFunctions();
        private readonly SaveFunctions _extSave = new SaveFunctions();
        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private int _position;

        #endregion [Private Data Members]               

        #region [Constructor]

        public DivisionsViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                // Code runs "for real" 
                Tbl09DivisionsList = new ObservableCollection<Tbl09Division>();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          


        //    Part 1    



        #region [Commands Division]

        private RelayCommand _getDivisionsByNameOrIdCommand;
        public ICommand GetDivisionsByNameOrIdCommand => _getDivisionsByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetDivisionsByNameOrId(SearchDivisionName); });

        private RelayCommand _addDivisionCommand;
        public ICommand AddDivisionCommand => _addDivisionCommand ??= new RelayCommand(delegate { ExecuteAddDivision(null); });

        private RelayCommand _copyDivisionCommand;
        public ICommand CopyDivisionCommand => _copyDivisionCommand ??= new RelayCommand(delegate { ExecuteCopyDivision(null); });

        private RelayCommand _deleteDivisionCommand;
        public ICommand DeleteDivisionCommand => _deleteDivisionCommand ??= new RelayCommand(delegate { ExecuteDeleteDivision(SearchDivisionName); });

        private RelayCommand _saveDivisionCommand;
        public ICommand SaveDivisionCommand => _saveDivisionCommand ??= new RelayCommand(delegate { ExecuteSaveDivision(SearchDivisionName); });

        #endregion [Commands Division]       


        #region [Methods Division]

        private void ExecuteGetDivisionsByNameOrId(string searchName)
        {
            if (Tbl03RegnumsAllList == null)
                Tbl03RegnumsAllList ??= new ObservableCollection<Tbl03Regnum>();
            else
                Tbl03RegnumsAllList.Clear();

            Tbl03RegnumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl03Regnum>("Regnum");

            if (Tbl09DivisionsList == null)
                Tbl09DivisionsList ??= new ObservableCollection<Tbl09Division>();
            else
                Tbl09DivisionsList.Clear();

            Tbl09DivisionsList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl09Division>(searchName, "Division");

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl09DivisionsList.Count)) return;

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            DivisionsView.Refresh();
        }

        private void ExecuteAddDivision(object o)
        {
            if (Tbl09DivisionsList == null)
                Tbl09DivisionsList ??= new ObservableCollection<Tbl09Division>();
            else
                Tbl09DivisionsList.Clear();

            if (Tbl03RegnumsAllList == null)
                Tbl03RegnumsAllList ??= new ObservableCollection<Tbl03Regnum>();
            else
                Tbl03RegnumsAllList.Clear();

            Tbl03RegnumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl03Regnum>("Regnum");

            Tbl09DivisionsList.Insert(0, new Tbl09Division { DivisionName = CultRes.StringsRes.DatasetNew });

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            DivisionsView.MoveCurrentToFirst();
        }

        private void ExecuteCopyDivision(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl09Division)) return;

            Tbl09DivisionsList = _extCrud.CopyDivision(CurrentTbl09Division);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            DivisionsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteDivision(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl09Division)) return;

            _extDelete.DeleteDivision(CurrentTbl09Division);

            Tbl09DivisionsList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl09Division>(searchName, "Division");
            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            DivisionsView.MoveCurrentToLast();
        }

        private void ExecuteSaveDivision(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl09Division)) return;

            _position = DivisionsView.CurrentPosition;

            var ret = _extSave.SaveDivision(CurrentTbl09Division);

            if (ret != true)
            {
                DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
                DivisionsView.Refresh();
                return;
            }

            if (CurrentTbl09Division.DivisionId == 0) //new
            {
                Tbl09DivisionsList = _extCrud.GetLastDivisionsDatasetOrderById();
                DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
                DivisionsView.MoveCurrentToFirst();
            }
            else
            {
                Tbl09DivisionsList = _extCrud.GetDivisionsCollectionFromSearchNameOrIdOrderBy<Tbl09Division>(searchName);
                DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
                DivisionsView.MoveCurrentToPosition(_position);
            }
        }
        #endregion [Methods Division]                



        //    Part 2    


        #region "Public Commands Connect <== Tbl03Regnum"                 


        private RelayCommand _saveRegnumCommand;

        public ICommand SaveRegnumCommand => _saveRegnumCommand ??= new RelayCommand(delegate { ExecuteSaveRegnum(null); });

        private void ExecuteSaveRegnum(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl03Regnum)) return;

            _extSave.SaveRegnum(CurrentTbl03Regnum);

            Tbl03RegnumsList = _extCrud.GetRegnumsCollectionFromRegnumIdOrderBy<Tbl03Regnum>(CurrentTbl09Division.RegnumId);
            RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            RegnumsView.Refresh();
        }

        #endregion "Public Commands"                  


        //    Part 3    





        //    Part 4    


        #region [Public Commands Connect ==> Tbl15Subdivision]                 

        private RelayCommand _addSubdivisionCommand;
        public ICommand AddSubdivisionCommand => _addSubdivisionCommand ??= new RelayCommand(delegate { ExecuteAddSubdivision(null); });

        private RelayCommand _copySubdivisionCommand;
        public ICommand CopySubdivisionCommand => _copySubdivisionCommand ??= new RelayCommand(delegate { ExecuteCopySubdivision(null); });

        private RelayCommand _deleteSubdivisionCommand;
        public ICommand DeleteSubdivisionCommand => _deleteSubdivisionCommand ??= new RelayCommand(delegate { ExecuteDeleteSubdivision(null); });

        private RelayCommand _saveSubdivisionCommand;
        public ICommand SaveSubdivisionCommand => _saveSubdivisionCommand ??= new RelayCommand(delegate { ExecuteSaveSubdivision(null); });

        #endregion [Public Commands Connect ==> Tbl15Subdivision]    

        #region [Public Methods Connect ==> Tbl15Subdivision]                   

        private void ExecuteAddSubdivision(object o)
        {
            if (Tbl09DivisionsAllList == null)
                Tbl09DivisionsAllList ??= new ObservableCollection<Tbl09Division>();
            else
                Tbl09DivisionsAllList.Clear();

            Tbl09DivisionsAllList = _extCrud.GetCollectionAllOrderBy<Tbl09Division>("Division");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            Tbl15SubdivisionsList ??= new ObservableCollection<Tbl15Subdivision>();

            Tbl15SubdivisionsList.Insert(0, new Tbl15Subdivision { SubdivisionName = CultRes.StringsRes.DatasetNew });

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

        private void ExecuteDeleteSubdivision(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl15Subdivision)) return;

            _extDelete.DeleteSubdivision(CurrentTbl15Subdivision);

            Tbl15SubdivisionsList = _extCrud.GetSubdivisionsCollectionFromDivisionIdOrderBy<Tbl15Subdivision>(CurrentTbl15Subdivision.SubdivisionId);
            SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            SubdivisionsView.MoveCurrentToFirst();
        }

        private void ExecuteSaveSubdivision(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl15Subdivision)) return;

            CurrentTbl15Subdivision.DivisionId = CurrentTbl09Division.DivisionId;

            _extSave.SaveSubdivision(CurrentTbl15Subdivision);
            Tbl15SubdivisionsList = _extCrud.GetSubdivisionsCollectionFromDivisionIdOrderBy<Tbl15Subdivision>(CurrentTbl15Subdivision.DivisionId);

            SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            SubdivisionsView.MoveCurrentToFirst();
        }

        #endregion [Public Methods  Connect ==> Tbl15Subdivision]                                                                                                                                            



        //    Part 5    



        //    Part 6    




        //    Part 7    



        //    Part 8    


        #region [Commands Division ==> Tbl90Reference Author]

        private RelayCommand _addReferenceAuthorCommand;

        public ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteAddReferenceAuthor(null); });

        private RelayCommand _copyReferenceAuthorCommand;

        public ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceAuthor(null); });

        private RelayCommand _deleteReferenceAuthorCommand;

        public ICommand DeleteReferenceAuthorCommand => _deleteReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceAuthor(null); });

        private RelayCommand _saveReferenceAuthorCommand;

        public ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceAuthor(null); });

        #endregion [Commands Division ==> Tbl90Reference Author]                

        #region [Methods Division ==> Tbl90Reference Author]

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

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceDivision(CurrentTbl90ReferenceAuthor, "Author");

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceAuthor(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            _extDelete.DeleteReferenceAuthor(CurrentTbl90ReferenceAuthor);

            Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromDivisionIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90ReferenceAuthor.DivisionId);
            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }

        public void ExecuteSaveReferenceAuthor(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            CurrentTbl90ReferenceAuthor.DivisionId = CurrentTbl09Division.DivisionId;

            _extSave.SaveReferenceAuthor(CurrentTbl90ReferenceAuthor, "Division");

            Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromDivisionIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl09Division.DivisionId);

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }
        #endregion [Methods Division ==> Tbl90Reference Author]              

        #region [Commands Division ==> Tbl90Reference Source]      

        private RelayCommand _addReferenceSourceCommand;

        public ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteAddReferenceSource(null); });

        private RelayCommand _copyReferenceSourceCommand;

        public ICommand CopyReferenceSourceCommand => _copyReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceSource(null); });

        private RelayCommand _deleteReferenceSourceCommand;

        public ICommand DeleteReferenceSourceCommand => _deleteReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceSource(null); });

        private RelayCommand _saveReferenceSourceCommand;

        public ICommand SaveReferenceSourceCommand => _saveReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceSource(null); });


        #endregion [Commands Division ==> Tbl90Reference Source]         

        #region [Methods Division ==> Tbl90Reference Source]      

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

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceDivision(CurrentTbl90ReferenceSource, "Source");

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceSource(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            _extDelete.DeleteReferenceSource(CurrentTbl90ReferenceSource);

            Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromDivisionIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl09Division.DivisionId);
            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        public void ExecuteSaveReferenceSource(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            CurrentTbl90ReferenceSource.DivisionId = CurrentTbl09Division.DivisionId;

            _extSave.SaveReferenceSource(CurrentTbl90ReferenceSource, "Division");

            Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromDivisionIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl09Division.DivisionId);


            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }
        #endregion [Methods Division ==> Tbl90Reference Source]                    

        #region [Commands Division ==> Tbl90Reference Expert]                 

        private RelayCommand _addReferenceExpertCommand;

        public ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteAddReferenceExpert(null); });

        private RelayCommand _copyReferenceExpertCommand;

        public ICommand CopyReferenceExpertCommand => _copyReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceExpert(null); });

        private RelayCommand _deleteReferenceExpertCommand;

        public ICommand DeleteReferenceExpertCommand => _deleteReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceExpert(null); });
        private RelayCommand _saveReferenceExpertCommand;

        public ICommand SaveReferenceExpertCommand => _saveReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceExpert(null); });

        #endregion [Commands Division ==> Tbl90Reference Expert]                    


        #region [Methods Division ==> Tbl90Reference Expert]                 

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

            Tbl90ReferenceExpertsList = _extCrud.CopyReferenceDivision(CurrentTbl90ReferenceExpert, "Expert");

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceExpert(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            _extDelete.DeleteReferenceExpert(CurrentTbl90ReferenceExpert);

            Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromDivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl09Division.DivisionId);
            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }

        public void ExecuteSaveReferenceExpert(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            CurrentTbl90ReferenceExpert.DivisionId = CurrentTbl09Division.DivisionId;

            _extSave.SaveReferenceExpert(CurrentTbl90ReferenceExpert, "Division");

            Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromDivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl09Division.DivisionId);


            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }
        #endregion [Methods Division ==> Tbl90Reference Expert]                               

        #region [Commands Division ==> Tbl93Comments]        

        private RelayCommand _addCommentCommand;

        public ICommand AddCommentCommand => _addCommentCommand ??= new RelayCommand(delegate { ExecuteAddComment(null); });

        private RelayCommand _copyCommentCommand;

        public ICommand CopyCommentCommand => _copyCommentCommand ??= new RelayCommand(delegate { ExecuteCopyComment(null); });

        private RelayCommand _deleteCommentCommand;

        public ICommand DeleteCommentCommand => _deleteCommentCommand ??= new RelayCommand(delegate { ExecuteDeleteComment(null); });

        private RelayCommand _saveCommentCommand;

        public ICommand SaveCommentCommand => _saveCommentCommand ??= new RelayCommand(delegate { ExecuteSaveComment(null); });

        #endregion [Commands Division ==> Tbl93Comments]        



        #region [Methods Division ==> Tbl93Comments]

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

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromDivisionIdOrderBy<Tbl93Comment>(CurrentTbl09Division.DivisionId);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }

        private void ExecuteSaveComment(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            CurrentTbl93Comment.DivisionId = CurrentTbl09Division.DivisionId;

            _extSave.SaveComment(CurrentTbl93Comment, "Division");

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromDivisionIdOrderBy<Tbl93Comment>(CurrentTbl09Division.DivisionId);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        #endregion [Methods Division ==> Tbl93Comments]                 


        //    Part 9    



        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        #endregion "Public Commands Connected Tables by DoubleClick"

        #region "Public Method Connected Tables by DoubleClick"

        private void GetConnectedTablesById(object o)
        {
            Tbl03RegnumsList = _extCrud.GetRegnumsCollectionFromRegnumIdOrderBy<Tbl03Regnum>(CurrentTbl09Division.RegnumId);

            RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            RegnumsView.Refresh();

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
                    if (CurrentTbl09Division != null)
                    {
                        Tbl03RegnumsList = _extCrud.GetRegnumsCollectionFromRegnumIdOrderBy<Tbl03Regnum>(CurrentTbl09Division.RegnumId);

                        RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
                        RegnumsView.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }

                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl09Division != null)
                    {
                        Tbl15SubdivisionsList = _extCrud.GetSubdivisionsCollectionFromDivisionIdOrderBy<Tbl15Subdivision>(CurrentTbl09Division.DivisionId);

                        Tbl09DivisionsAllList = _extCrud.GetCollectionAllOrderBy<Tbl09Division>("Division");

                        SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
                        SubdivisionsView.Refresh();
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
                    if (CurrentTbl09Division != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromDivisionIdOrderBy<Tbl93Comment>(CurrentTbl09Division.DivisionId);

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
                    if (CurrentTbl09Division != null)
                    {
                        Tbl03RegnumsList = _extCrud.GetRegnumsCollectionFromRegnumIdOrderBy<Tbl03Regnum>(CurrentTbl09Division.RegnumId);

                        RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
                        RegnumsView.Refresh();
                    }
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 1)
                {
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 2)
                {
                    if (CurrentTbl09Division != null)
                    {
                        Tbl15SubdivisionsList = _extCrud.GetSubdivisionsCollectionFromDivisionIdOrderBy<Tbl15Subdivision>(CurrentTbl09Division.DivisionId);

                        Tbl09DivisionsAllList = _extCrud.GetCollectionAllOrderBy<Tbl09Division>("Division");

                        SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
                        SubdivisionsView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTbl09Division != null)
                    {
                        Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromDivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl09Division.DivisionId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 4)
                {
                    if (CurrentTbl09Division != null)
                    {
                        Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromDivisionIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl09Division.DivisionId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 5)
                {
                    if (CurrentTbl09Division != null)
                    {
                        Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("Author");

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromDivisionIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl09Division.DivisionId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 2;
                }

                if (_selectedDetailTabIndex == 6)
                {
                    if (CurrentTbl09Division != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromDivisionIdOrderBy<Tbl93Comment>(CurrentTbl09Division.DivisionId);

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
                    if (CurrentTbl09Division != null)
                    {
                        Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromDivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl09Division.DivisionId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                }

                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (CurrentTbl09Division != null)
                    {
                        Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromDivisionIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl09Division.DivisionId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 2;
                }

                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (CurrentTbl09Division != null)
                    {
                        Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("Author");

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromDivisionIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl09Division.DivisionId);

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


        #region "Public Properties Tbl09Division"

        private string _searchDivisionName = "";
        public string SearchDivisionName
        {
            get => _searchDivisionName;
            set { _searchDivisionName = value; RaisePropertyChanged(""); }
        }

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

        #region "Public Properties Tbl03Regnum"

        public ICollectionView RegnumsView;
        private Tbl03Regnum CurrentTbl03Regnum => RegnumsView?.CurrentItem as Tbl03Regnum;

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsList
        {
            get => _tbl03RegnumsList;
            set { _tbl03RegnumsList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
        {
            get => _tbl03RegnumsAllList;
            set { _tbl03RegnumsAllList = value; RaisePropertyChanged(""); }
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
