using System;
using System.Collections.ObjectModel;
using System.ComponentModel;


using System.Windows.Data;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using log4net;

//    InfratribussesViewModel Skriptdatum:  07.01.2021  10:32    

namespace ATIS.Ui.Views.Database.D63Infratribus
{

    public class InfratribussesViewModel : ViewModelBase
    {
        // Version with Generic Unit Of Work and AtisDbContext for general use   

        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(typeof(InfratribussesViewModel));
        private readonly CrudFunctions _extCrud = new CrudFunctions();
        private readonly DeleteFunctions _extDelete = new DeleteFunctions();
        private readonly SaveFunctions _extSave = new SaveFunctions();
        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private int _position;

        #endregion [Private Data Members]               

        #region [Constructor]

        public InfratribussesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                // Code runs "for real" 
                Tbl63InfratribussesList = new ObservableCollection<Tbl63Infratribus>();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          


        //    Part 1    



        #region [Commands Infratribus]

        private RelayCommand _getInfratribussesByNameOrIdCommand;
        public ICommand GetInfratribussesByNameOrIdCommand => _getInfratribussesByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetInfratribussesByNameOrId(SearchInfratribusName); });

        private RelayCommand _addInfratribusCommand;
        public ICommand AddInfratribusCommand => _addInfratribusCommand ??= new RelayCommand(delegate { ExecuteAddInfratribus(null); });

        private RelayCommand _copyInfratribusCommand;
        public ICommand CopyInfratribusCommand => _copyInfratribusCommand ??= new RelayCommand(delegate { ExecuteCopyInfratribus(null); });

        private RelayCommand _deleteInfratribusCommand;
        public ICommand DeleteInfratribusCommand => _deleteInfratribusCommand ??= new RelayCommand(delegate { ExecuteDeleteInfratribus(SearchInfratribusName); });

        private RelayCommand _saveInfratribusCommand;
        public ICommand SaveInfratribusCommand => _saveInfratribusCommand ??= new RelayCommand(delegate { ExecuteSaveInfratribus(SearchInfratribusName); });

        #endregion [Commands Infratribus]       


        #region [Methods Infratribus]

        private void ExecuteGetInfratribussesByNameOrId(string searchName)
        {
            if (Tbl60SubtribussesAllList == null)
                Tbl60SubtribussesAllList ??= new ObservableCollection<Tbl60Subtribus>();
            else
                Tbl60SubtribussesAllList.Clear();

            Tbl60SubtribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl60Subtribus>("Subtribus");

            if (Tbl63InfratribussesList == null)
                Tbl63InfratribussesList ??= new ObservableCollection<Tbl63Infratribus>();
            else
                Tbl63InfratribussesList.Clear();

            Tbl63InfratribussesList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl63Infratribus>(searchName, "Infratribus");

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl63InfratribussesList.Count)) return;

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
            InfratribussesView.Refresh();
        }

        private void ExecuteAddInfratribus(object o)
        {
            if (Tbl63InfratribussesList == null)
                Tbl63InfratribussesList ??= new ObservableCollection<Tbl63Infratribus>();
            else
                Tbl63InfratribussesList.Clear();

            if (Tbl60SubtribussesAllList == null)
                Tbl60SubtribussesAllList ??= new ObservableCollection<Tbl60Subtribus>();
            else
                Tbl60SubtribussesAllList.Clear();

            Tbl60SubtribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl60Subtribus>("Subtribus");

            Tbl63InfratribussesList.Insert(0, new Tbl63Infratribus { InfratribusName = CultRes.StringsRes.DatasetNew });

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
            InfratribussesView.MoveCurrentToFirst();
        }

        private void ExecuteCopyInfratribus(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl63Infratribus)) return;

            Tbl63InfratribussesList = _extCrud.CopyInfratribus(CurrentTbl63Infratribus);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
            InfratribussesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteInfratribus(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl63Infratribus)) return;

            _extDelete.DeleteInfratribus(CurrentTbl63Infratribus);

            Tbl63InfratribussesList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl63Infratribus>(searchName, "Infratribus");
            InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
            InfratribussesView.MoveCurrentToLast();
        }

        private void ExecuteSaveInfratribus(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl63Infratribus)) return;

            _position = InfratribussesView.CurrentPosition;

            var ret = _extSave.SaveInfratribus(CurrentTbl63Infratribus);

            if (ret != true)
            {
                InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
                InfratribussesView.Refresh();
                return;
            }

            if (CurrentTbl63Infratribus.InfratribusId == 0) //new
            {
                Tbl63InfratribussesList = _extCrud.GetLastInfratribussesDatasetOrderById();
                InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
                InfratribussesView.MoveCurrentToFirst();
            }
            else
            {
                Tbl63InfratribussesList = _extCrud.GetInfratribussesCollectionFromSearchNameOrIdOrderBy<Tbl63Infratribus>(searchName);
                InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
                InfratribussesView.MoveCurrentToPosition(_position);
            }
        }
        #endregion [Methods Infratribus]                



        //    Part 2    


        #region "Public Commands Connect <== Tbl60Subtribus"                 


        private RelayCommand _saveSubtribusCommand;

        public ICommand SaveSubtribusCommand => _saveSubtribusCommand ??= new RelayCommand(delegate { ExecuteSaveSubtribus(null); });

        private void ExecuteSaveSubtribus(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl60Subtribus)) return;

            _extSave.SaveSubtribus(CurrentTbl60Subtribus);

            Tbl60SubtribussesList = _extCrud.GetSubtribussesCollectionFromSubtribusIdOrderBy<Tbl60Subtribus>(CurrentTbl63Infratribus.SubtribusId);
            SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            SubtribussesView.Refresh();
        }

        #endregion "Public Commands"                  


        //    Part 3    





        //    Part 4    


        #region [Public Commands Connect ==> Tbl66Genus]                 

        private RelayCommand _addGenusCommand;
        public ICommand AddGenusCommand => _addGenusCommand ??= new RelayCommand(delegate { ExecuteAddGenus(null); });

        private RelayCommand _copyGenusCommand;
        public ICommand CopyGenusCommand => _copyGenusCommand ??= new RelayCommand(delegate { ExecuteCopyGenus(null); });

        private RelayCommand _deleteGenusCommand;
        public ICommand DeleteGenusCommand => _deleteGenusCommand ??= new RelayCommand(delegate { ExecuteDeleteGenus(null); });

        private RelayCommand _saveGenusCommand;
        public ICommand SaveGenusCommand => _saveGenusCommand ??= new RelayCommand(delegate { ExecuteSaveGenus(null); });

        #endregion [Public Commands Connect ==> Tbl66Genus]    

        #region [Public Methods Connect ==> Tbl66Genus]                   

        private void ExecuteAddGenus(object o)
        {
            if (Tbl63InfratribussesAllList == null)
                Tbl63InfratribussesAllList ??= new ObservableCollection<Tbl63Infratribus>();
            else
                Tbl63InfratribussesAllList.Clear();

            Tbl63InfratribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl63Infratribus>("Infratribus");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            Tbl66GenussesList ??= new ObservableCollection<Tbl66Genus>();

            Tbl66GenussesList.Insert(0, new Tbl66Genus { GenusName = CultRes.StringsRes.DatasetNew });

            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.MoveCurrentToFirst();
        }

        private void ExecuteCopyGenus(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl66Genus)) return;

            Tbl66GenussesList = _extCrud.CopyGenus(CurrentTbl66Genus);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteGenus(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl66Genus)) return;

            _extDelete.DeleteGenus(CurrentTbl66Genus);

            Tbl66GenussesList = _extCrud.GetGenussesCollectionFromInfratribusIdOrderBy<Tbl66Genus>(CurrentTbl66Genus.GenusId);
            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.MoveCurrentToFirst();
        }

        private void ExecuteSaveGenus(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl66Genus)) return;

            CurrentTbl66Genus.InfratribusId = CurrentTbl63Infratribus.InfratribusId;

            _extSave.SaveGenus(CurrentTbl66Genus);
            Tbl66GenussesList = _extCrud.GetGenussesCollectionFromInfratribusIdOrderBy<Tbl66Genus>(CurrentTbl66Genus.InfratribusId);

            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.MoveCurrentToFirst();
        }

        #endregion [Public Methods  Connect ==> Tbl66Genus]                                                                                                                                            



        //    Part 5    



        //    Part 6    




        //    Part 7    



        //    Part 8    


        #region [Commands Infratribus ==> Tbl90Reference Author]

        private RelayCommand _addReferenceAuthorCommand;

        public ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteAddReferenceAuthor(null); });

        private RelayCommand _copyReferenceAuthorCommand;

        public ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceAuthor(null); });

        private RelayCommand _deleteReferenceAuthorCommand;

        public ICommand DeleteReferenceAuthorCommand => _deleteReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceAuthor(null); });

        private RelayCommand _saveReferenceAuthorCommand;

        public ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceAuthor(null); });

        #endregion [Commands Infratribus ==> Tbl90Reference Author]                

        #region [Methods Infratribus ==> Tbl90Reference Author]

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

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceInfratribus(CurrentTbl90ReferenceAuthor, "Author");

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceAuthor(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            _extDelete.DeleteReferenceAuthor(CurrentTbl90ReferenceAuthor);

            Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromInfratribusIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90ReferenceAuthor.InfratribusId);
            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }

        public void ExecuteSaveReferenceAuthor(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            CurrentTbl90ReferenceAuthor.InfratribusId = CurrentTbl63Infratribus.InfratribusId;

            _extSave.SaveReferenceAuthor(CurrentTbl90ReferenceAuthor, "Infratribus");

            Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromInfratribusIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl63Infratribus.InfratribusId);

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }
        #endregion [Methods Infratribus ==> Tbl90Reference Author]              

        #region [Commands Infratribus ==> Tbl90Reference Source]      

        private RelayCommand _addReferenceSourceCommand;

        public ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteAddReferenceSource(null); });

        private RelayCommand _copyReferenceSourceCommand;

        public ICommand CopyReferenceSourceCommand => _copyReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceSource(null); });

        private RelayCommand _deleteReferenceSourceCommand;

        public ICommand DeleteReferenceSourceCommand => _deleteReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceSource(null); });

        private RelayCommand _saveReferenceSourceCommand;

        public ICommand SaveReferenceSourceCommand => _saveReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceSource(null); });


        #endregion [Commands Infratribus ==> Tbl90Reference Source]         

        #region [Methods Infratribus ==> Tbl90Reference Source]      

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

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceInfratribus(CurrentTbl90ReferenceSource, "Source");

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceSource(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            _extDelete.DeleteReferenceSource(CurrentTbl90ReferenceSource);

            Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromInfratribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl63Infratribus.InfratribusId);
            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        public void ExecuteSaveReferenceSource(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            CurrentTbl90ReferenceSource.InfratribusId = CurrentTbl63Infratribus.InfratribusId;

            _extSave.SaveReferenceSource(CurrentTbl90ReferenceSource, "Infratribus");

            Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromInfratribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl63Infratribus.InfratribusId);


            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }
        #endregion [Methods Infratribus ==> Tbl90Reference Source]                    

        #region [Commands Infratribus ==> Tbl90Reference Expert]                 

        private RelayCommand _addReferenceExpertCommand;

        public ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteAddReferenceExpert(null); });

        private RelayCommand _copyReferenceExpertCommand;

        public ICommand CopyReferenceExpertCommand => _copyReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceExpert(null); });

        private RelayCommand _deleteReferenceExpertCommand;

        public ICommand DeleteReferenceExpertCommand => _deleteReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceExpert(null); });
        private RelayCommand _saveReferenceExpertCommand;

        public ICommand SaveReferenceExpertCommand => _saveReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceExpert(null); });

        #endregion [Commands Infratribus ==> Tbl90Reference Expert]                    


        #region [Methods Infratribus ==> Tbl90Reference Expert]                 

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

            Tbl90ReferenceExpertsList = _extCrud.CopyReferenceInfratribus(CurrentTbl90ReferenceExpert, "Expert");

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceExpert(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            _extDelete.DeleteReferenceExpert(CurrentTbl90ReferenceExpert);

            Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromInfratribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl63Infratribus.InfratribusId);
            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }

        public void ExecuteSaveReferenceExpert(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            CurrentTbl90ReferenceExpert.InfratribusId = CurrentTbl63Infratribus.InfratribusId;

            _extSave.SaveReferenceExpert(CurrentTbl90ReferenceExpert, "Infratribus");

            Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromInfratribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl63Infratribus.InfratribusId);


            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }
        #endregion [Methods Infratribus ==> Tbl90Reference Expert]                               

        #region [Commands Infratribus ==> Tbl93Comments]        

        private RelayCommand _addCommentCommand;

        public ICommand AddCommentCommand => _addCommentCommand ??= new RelayCommand(delegate { ExecuteAddComment(null); });

        private RelayCommand _copyCommentCommand;

        public ICommand CopyCommentCommand => _copyCommentCommand ??= new RelayCommand(delegate { ExecuteCopyComment(null); });

        private RelayCommand _deleteCommentCommand;

        public ICommand DeleteCommentCommand => _deleteCommentCommand ??= new RelayCommand(delegate { ExecuteDeleteComment(null); });

        private RelayCommand _saveCommentCommand;

        public ICommand SaveCommentCommand => _saveCommentCommand ??= new RelayCommand(delegate { ExecuteSaveComment(null); });

        #endregion [Commands Infratribus ==> Tbl93Comments]        



        #region [Methods Infratribus ==> Tbl93Comments]        

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

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromInfratribusIdOrderBy<Tbl93Comment>(CurrentTbl63Infratribus.InfratribusId);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }

        private void ExecuteSaveComment(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            CurrentTbl93Comment.InfratribusId = CurrentTbl63Infratribus.InfratribusId;

            _extSave.SaveComment(CurrentTbl93Comment, "Infratribus");

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromInfratribusIdOrderBy<Tbl93Comment>(CurrentTbl63Infratribus.InfratribusId);


            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        #endregion [Methods Infratribus ==> Tbl93Comments]                 


        //    Part 9    



        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        #endregion "Public Commands Connected Tables by DoubleClick"

        #region "Public Method Connected Tables by DoubleClick"

        private void GetConnectedTablesById(object o)
        {
            Tbl60SubtribussesList = _extCrud.GetSubtribussesCollectionFromSubtribusIdOrderBy<Tbl60Subtribus>(CurrentTbl63Infratribus.SubtribusId);

            Tbl57TribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl57Tribus>("Tribus");

            SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            SubtribussesView.Refresh();

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
                    if (CurrentTbl63Infratribus != null)
                    {
                        Tbl60SubtribussesList = _extCrud.GetSubtribussesCollectionFromSubtribusIdOrderBy<Tbl60Subtribus>(CurrentTbl63Infratribus.SubtribusId);

                        Tbl57TribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl57Tribus>("Tribus");

                        SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
                        SubtribussesView.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }

                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl63Infratribus != null)
                    {
                        Tbl66GenussesList = _extCrud.GetGenussesCollectionFromInfratribusIdOrderBy<Tbl66Genus>(CurrentTbl63Infratribus.InfratribusId);

                        Tbl63InfratribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl63Infratribus>("Infratribus");

                        GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
                        GenussesView.Refresh();
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
                    if (CurrentTbl63Infratribus != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromInfratribusIdOrderBy<Tbl93Comment>(CurrentTbl63Infratribus.InfratribusId);

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
                    if (CurrentTbl63Infratribus != null)
                    {
                        Tbl60SubtribussesList = _extCrud.GetSubtribussesCollectionFromSubtribusIdOrderBy<Tbl60Subtribus>(CurrentTbl63Infratribus.SubtribusId);

                        SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
                        SubtribussesView.Refresh();
                    }
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 1)
                {
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 2)
                {
                    if (CurrentTbl63Infratribus != null)
                    {
                        Tbl66GenussesList = _extCrud.GetGenussesCollectionFromInfratribusIdOrderBy<Tbl66Genus>(CurrentTbl63Infratribus.InfratribusId);

                        Tbl63InfratribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl63Infratribus>("Infratribus");

                        GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
                        GenussesView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTbl63Infratribus != null)
                    {
                        Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromInfratribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl63Infratribus.InfratribusId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 4)
                {
                    if (CurrentTbl63Infratribus != null)
                    {
                        Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromInfratribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl63Infratribus.InfratribusId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 5)
                {
                    if (CurrentTbl63Infratribus != null)
                    {
                        Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("Author");

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromInfratribusIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl63Infratribus.InfratribusId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 2;
                }

                if (_selectedDetailTabIndex == 6)
                {
                    if (CurrentTbl63Infratribus != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromInfratribusIdOrderBy<Tbl93Comment>(CurrentTbl63Infratribus.InfratribusId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                }

                if (_selectedDetailTabIndex == 7)
                {
                    if (CurrentTbl63Infratribus != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromInfratribusIdOrderBy<Tbl93Comment>(CurrentTbl63Infratribus.InfratribusId);

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
                    if (CurrentTbl63Infratribus != null)
                    {
                        Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromInfratribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl63Infratribus.InfratribusId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                }

                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (CurrentTbl63Infratribus != null)
                    {
                        Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromInfratribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl63Infratribus.InfratribusId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 2;
                }

                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (CurrentTbl63Infratribus != null)
                    {
                        Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("Author");

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromInfratribusIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl63Infratribus.InfratribusId);

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


        #region "Public Properties Tbl63Infratribus"

        private string _searchInfratribusName = "";
        public string SearchInfratribusName
        {
            get => _searchInfratribusName;
            set { _searchInfratribusName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView InfratribussesView;
        private Tbl63Infratribus CurrentTbl63Infratribus => InfratribussesView?.CurrentItem as Tbl63Infratribus;

        private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesList;
        public ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesList
        {
            get => _tbl63InfratribussesList;
            set { _tbl63InfratribussesList = value; RaisePropertyChanged(""); }
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

        #endregion "Public Properties"   

        #region "Public Properties Tbl60Subtribus"

        public ICollectionView SubtribussesView;
        private Tbl60Subtribus CurrentTbl60Subtribus => SubtribussesView?.CurrentItem as Tbl60Subtribus;

        private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesList;
        public ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesList
        {
            get => _tbl60SubtribussesList;
            set { _tbl60SubtribussesList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesAllList;
        public ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesAllList
        {
            get => _tbl60SubtribussesAllList;
            set { _tbl60SubtribussesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   

        #region "Public Properties Tbl66Genus"

        public ICollectionView GenussesView;
        private Tbl66Genus CurrentTbl66Genus => GenussesView?.CurrentItem as Tbl66Genus;

        private ObservableCollection<Tbl66Genus> _tbl66GenussesList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesList
        {
            get => _tbl66GenussesList;
            set { _tbl66GenussesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl69FiSpecies"

        public ICollectionView FiSpeciessesView;
        private Tbl69FiSpecies CurrentTbl69FiSpecies => FiSpeciessesView?.CurrentItem as Tbl69FiSpecies;

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesList;
        public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
        {
            get => _tbl69FiSpeciessesList;
            set { _tbl69FiSpeciessesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl72PlSpecies"

        private string _searchPlSpeciesName = string.Empty;
        public string SearchPlSpeciesName
        {
            get => _searchPlSpeciesName;
            set { _searchPlSpeciesName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView PlSpeciessesView;
        private Tbl72PlSpecies CurrentTbl72PlSpecies => PlSpeciessesView?.CurrentItem as Tbl72PlSpecies;

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesList;
        public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesList
        {
            get => _tbl72PlSpeciessesList;
            set { _tbl72PlSpeciessesList = value; RaisePropertyChanged(""); }
        }
        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesAllList;
        public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesAllList
        {
            get => _tbl72PlSpeciessesAllList;
            set { _tbl72PlSpeciessesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"     

        #region "Public Properties Tbl57Tribus"

        private ObservableCollection<Tbl57Tribus> _tbl57TribussesAllList;
        public ObservableCollection<Tbl57Tribus> Tbl57TribussesAllList
        {
            get => _tbl57TribussesAllList;
            set { _tbl57TribussesAllList = value; RaisePropertyChanged(""); }
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
