using System;
using System.Collections.ObjectModel;
using System.ComponentModel;


using System.Windows.Data;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using log4net;

//    LegiosViewModel Skriptdatum:  07.01.2021  10:32    

namespace ATIS.Ui.Views.Database.D30Legio
{

    public class LegiosViewModel : ViewModelBase
    {
        // Version with Generic Unit Of Work and AtisDbContext for general use   

        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(typeof(LegiosViewModel));
        private readonly CrudFunctions _extCrud = new CrudFunctions();
        private readonly DeleteFunctions _extDelete = new DeleteFunctions();
        private readonly SaveFunctions _extSave = new SaveFunctions();
        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private int _position;

        #endregion [Private Data Members]               

        #region [Constructor]

        public LegiosViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                // Code runs "for real" 
                Tbl30LegiosList = new ObservableCollection<Tbl30Legio>();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          


        //    Part 1    



        #region [Commands Legio]

        private RelayCommand _getLegiosByNameOrIdCommand;
        public ICommand GetLegiosByNameOrIdCommand => _getLegiosByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetLegiosByNameOrId(SearchLegioName); });

        private RelayCommand _addLegioCommand;
        public ICommand AddLegioCommand => _addLegioCommand ??= new RelayCommand(delegate { ExecuteAddLegio(null); });

        private RelayCommand _copyLegioCommand;
        public ICommand CopyLegioCommand => _copyLegioCommand ??= new RelayCommand(delegate { ExecuteCopyLegio(null); });

        private RelayCommand _deleteLegioCommand;
        public ICommand DeleteLegioCommand => _deleteLegioCommand ??= new RelayCommand(delegate { ExecuteDeleteLegio(SearchLegioName); });

        private RelayCommand _saveLegioCommand;
        public ICommand SaveLegioCommand => _saveLegioCommand ??= new RelayCommand(delegate { ExecuteSaveLegio(SearchLegioName); });

        #endregion [Commands Legio]       


        #region [Methods Legio]

        private void ExecuteGetLegiosByNameOrId(string searchName)
        {
            if (Tbl27InfraclassesAllList == null)
                Tbl27InfraclassesAllList ??= new ObservableCollection<Tbl27Infraclass>();
            else
                Tbl27InfraclassesAllList.Clear();

            Tbl27InfraclassesAllList = _extCrud.GetCollectionAllOrderBy<Tbl27Infraclass>("Infraclass");

            if (Tbl30LegiosList == null)
                Tbl30LegiosList ??= new ObservableCollection<Tbl30Legio>();
            else
                Tbl30LegiosList.Clear();

            Tbl30LegiosList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl30Legio>(searchName, "Legio");

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl30LegiosList.Count)) return;

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            LegiosView = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
            LegiosView.Refresh();
        }

        private void ExecuteAddLegio(object o)
        {
            if (Tbl30LegiosList == null)
                Tbl30LegiosList ??= new ObservableCollection<Tbl30Legio>();
            else
                Tbl30LegiosList.Clear();

            if (Tbl27InfraclassesAllList == null)
                Tbl27InfraclassesAllList ??= new ObservableCollection<Tbl27Infraclass>();
            else
                Tbl27InfraclassesAllList.Clear();

            Tbl27InfraclassesAllList = _extCrud.GetCollectionAllOrderBy<Tbl27Infraclass>("Infraclass");

            Tbl30LegiosList.Insert(0, new Tbl30Legio { LegioName = CultRes.StringsRes.DatasetNew });

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            LegiosView = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
            LegiosView.MoveCurrentToFirst();
        }

        private void ExecuteCopyLegio(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl30Legio)) return;

            Tbl30LegiosList = _extCrud.CopyLegio(CurrentTbl30Legio);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            LegiosView = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
            LegiosView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteLegio(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl30Legio)) return;

            _extDelete.DeleteLegio(CurrentTbl30Legio);

            Tbl30LegiosList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl30Legio>(searchName, "Legio");
            LegiosView = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
            LegiosView.MoveCurrentToLast();
        }

        private void ExecuteSaveLegio(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl30Legio)) return;

            _position = LegiosView.CurrentPosition;

            var ret = _extSave.SaveLegio(CurrentTbl30Legio);

            if (ret != true)
            {
                LegiosView = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
                LegiosView.Refresh();
                return;
            }

            if (CurrentTbl30Legio.LegioId == 0) //new
            {
                Tbl30LegiosList = _extCrud.GetLastLegiosDatasetOrderById();
                LegiosView = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
                LegiosView.MoveCurrentToFirst();
            }
            else
            {
                Tbl30LegiosList = _extCrud.GetLegiosCollectionFromSearchNameOrIdOrderBy<Tbl30Legio>(searchName);
                LegiosView = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
                LegiosView.MoveCurrentToPosition(_position);
            }
        }
        #endregion [Methods Legio]                



        //    Part 2    


        #region "Public Commands Connect <== Tbl27Infraclass"                 


        private RelayCommand _saveInfraclassCommand;

        public ICommand SaveInfraclassCommand => _saveInfraclassCommand ??= new RelayCommand(delegate { ExecuteSaveInfraclass(null); });

        private void ExecuteSaveInfraclass(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl27Infraclass)) return;

            _extSave.SaveInfraclass(CurrentTbl27Infraclass);

            Tbl27InfraclassesList = _extCrud.GetInfraclassesCollectionFromInfraclassIdOrderBy<Tbl27Infraclass>(CurrentTbl30Legio.InfraclassId);
            InfraclassesView = CollectionViewSource.GetDefaultView(Tbl27InfraclassesList);
            InfraclassesView.Refresh();
        }

        #endregion "Public Commands"                  


        //    Part 3    





        //    Part 4    


        #region [Public Commands Connect ==> Tbl33Ordo]                 

        private RelayCommand _addOrdoCommand;
        public ICommand AddOrdoCommand => _addOrdoCommand ??= new RelayCommand(delegate { ExecuteAddOrdo(null); });

        private RelayCommand _copyOrdoCommand;
        public ICommand CopyOrdoCommand => _copyOrdoCommand ??= new RelayCommand(delegate { ExecuteCopyOrdo(null); });

        private RelayCommand _deleteOrdoCommand;
        public ICommand DeleteOrdoCommand => _deleteOrdoCommand ??= new RelayCommand(delegate { ExecuteDeleteOrdo(null); });

        private RelayCommand _saveOrdoCommand;
        public ICommand SaveOrdoCommand => _saveOrdoCommand ??= new RelayCommand(delegate { ExecuteSaveOrdo(null); });

        #endregion [Public Commands Connect ==> Tbl33Ordo]    

        #region [Public Methods Connect ==> Tbl33Ordo]                   

        private void ExecuteAddOrdo(object o)
        {
            if (Tbl30LegiosAllList == null)
                Tbl30LegiosAllList ??= new ObservableCollection<Tbl30Legio>();
            else
                Tbl30LegiosAllList.Clear();

            Tbl30LegiosAllList = _extCrud.GetCollectionAllOrderBy<Tbl30Legio>("Legio");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            Tbl33OrdosList ??= new ObservableCollection<Tbl33Ordo>();

            Tbl33OrdosList.Insert(0, new Tbl33Ordo { OrdoName = CultRes.StringsRes.DatasetNew });

            OrdosView = CollectionViewSource.GetDefaultView(Tbl33OrdosList);
            OrdosView.MoveCurrentToFirst();
        }

        private void ExecuteCopyOrdo(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl33Ordo)) return;

            Tbl33OrdosList = _extCrud.CopyOrdo(CurrentTbl33Ordo);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            OrdosView = CollectionViewSource.GetDefaultView(Tbl33OrdosList);
            OrdosView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteOrdo(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl33Ordo)) return;

            _extDelete.DeleteOrdo(CurrentTbl33Ordo);

            Tbl33OrdosList = _extCrud.GetOrdosCollectionFromLegioIdOrderBy<Tbl33Ordo>(CurrentTbl33Ordo.OrdoId);
            OrdosView = CollectionViewSource.GetDefaultView(Tbl33OrdosList);
            OrdosView.MoveCurrentToFirst();
        }

        private void ExecuteSaveOrdo(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl33Ordo)) return;

            CurrentTbl33Ordo.LegioId = CurrentTbl30Legio.LegioId;

            _extSave.SaveOrdo(CurrentTbl33Ordo);
            Tbl33OrdosList = _extCrud.GetOrdosCollectionFromLegioIdOrderBy<Tbl33Ordo>(CurrentTbl33Ordo.LegioId);

            OrdosView = CollectionViewSource.GetDefaultView(Tbl33OrdosList);
            OrdosView.MoveCurrentToFirst();
        }

        #endregion [Public Methods  Connect ==> Tbl33Ordo]                                                                                                                                            



        //    Part 5    



        //    Part 6    




        //    Part 7    



        //    Part 8    


        #region [Commands Legio ==> Tbl90Reference Author]

        private RelayCommand _addReferenceAuthorCommand;

        public ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteAddReferenceAuthor(null); });

        private RelayCommand _copyReferenceAuthorCommand;

        public ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceAuthor(null); });

        private RelayCommand _deleteReferenceAuthorCommand;

        public ICommand DeleteReferenceAuthorCommand => _deleteReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceAuthor(null); });

        private RelayCommand _saveReferenceAuthorCommand;

        public ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceAuthor(null); });

        #endregion [Commands Legio ==> Tbl90Reference Author]                

        #region [Methods Legio ==> Tbl90Reference Author]

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

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceLegio(CurrentTbl90ReferenceAuthor, "Author");

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceAuthor(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            _extDelete.DeleteReferenceAuthor(CurrentTbl90ReferenceAuthor);

            Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromLegioIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90ReferenceAuthor.LegioId);
            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }

        public void ExecuteSaveReferenceAuthor(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            CurrentTbl90ReferenceAuthor.LegioId = CurrentTbl30Legio.LegioId;

            _extSave.SaveReferenceAuthor(CurrentTbl90ReferenceAuthor, "Legio");

            Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromLegioIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl30Legio.LegioId);

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }
        #endregion [Methods Legio ==> Tbl90Reference Author]              

        #region [Commands Legio ==> Tbl90Reference Source]      

        private RelayCommand _addReferenceSourceCommand;

        public ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteAddReferenceSource(null); });

        private RelayCommand _copyReferenceSourceCommand;

        public ICommand CopyReferenceSourceCommand => _copyReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceSource(null); });

        private RelayCommand _deleteReferenceSourceCommand;

        public ICommand DeleteReferenceSourceCommand => _deleteReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceSource(null); });

        private RelayCommand _saveReferenceSourceCommand;

        public ICommand SaveReferenceSourceCommand => _saveReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceSource(null); });


        #endregion [Commands Legio ==> Tbl90Reference Source]         

        #region [Methods Legio ==> Tbl90Reference Source]      

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

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceLegio(CurrentTbl90ReferenceSource, "Source");

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceSource(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            _extDelete.DeleteReferenceSource(CurrentTbl90ReferenceSource);

            Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromLegioIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl30Legio.LegioId);
            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        public void ExecuteSaveReferenceSource(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            CurrentTbl90ReferenceSource.LegioId = CurrentTbl30Legio.LegioId;

            _extSave.SaveReferenceSource(CurrentTbl90ReferenceSource, "Legio");

            Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromLegioIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl30Legio.LegioId);


            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }
        #endregion [Methods Legio ==> Tbl90Reference Source]                    

        #region [Commands Legio ==> Tbl90Reference Expert]                 

        private RelayCommand _addReferenceExpertCommand;

        public ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteAddReferenceExpert(null); });

        private RelayCommand _copyReferenceExpertCommand;

        public ICommand CopyReferenceExpertCommand => _copyReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceExpert(null); });

        private RelayCommand _deleteReferenceExpertCommand;

        public ICommand DeleteReferenceExpertCommand => _deleteReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceExpert(null); });
        private RelayCommand _saveReferenceExpertCommand;

        public ICommand SaveReferenceExpertCommand => _saveReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceExpert(null); });

        #endregion [Commands Legio ==> Tbl90Reference Expert]                    


        #region [Methods Legio ==> Tbl90Reference Expert]                 

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

            Tbl90ReferenceExpertsList = _extCrud.CopyReferenceLegio(CurrentTbl90ReferenceExpert, "Expert");

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceExpert(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            _extDelete.DeleteReferenceExpert(CurrentTbl90ReferenceExpert);

            Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromLegioIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl30Legio.LegioId);
            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }

        public void ExecuteSaveReferenceExpert(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            CurrentTbl90ReferenceExpert.LegioId = CurrentTbl30Legio.LegioId;

            _extSave.SaveReferenceExpert(CurrentTbl90ReferenceExpert, "Legio");

            Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromLegioIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl30Legio.LegioId);


            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }
        #endregion [Methods Legio ==> Tbl90Reference Expert]                               

        #region [Commands Legio ==> Tbl93Comments]        

        private RelayCommand _addCommentCommand;

        public ICommand AddCommentCommand => _addCommentCommand ??= new RelayCommand(delegate { ExecuteAddComment(null); });

        private RelayCommand _copyCommentCommand;

        public ICommand CopyCommentCommand => _copyCommentCommand ??= new RelayCommand(delegate { ExecuteCopyComment(null); });

        private RelayCommand _deleteCommentCommand;

        public ICommand DeleteCommentCommand => _deleteCommentCommand ??= new RelayCommand(delegate { ExecuteDeleteComment(null); });

        private RelayCommand _saveCommentCommand;

        public ICommand SaveCommentCommand => _saveCommentCommand ??= new RelayCommand(delegate { ExecuteSaveComment(null); });

        #endregion [Commands Legio ==> Tbl93Comments]        



        #region [Methods Legio ==> Tbl93Comments]        

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

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromLegioIdOrderBy<Tbl93Comment>(CurrentTbl30Legio.LegioId);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }

        private void ExecuteSaveComment(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            CurrentTbl93Comment.LegioId = CurrentTbl30Legio.LegioId;

            _extSave.SaveComment(CurrentTbl93Comment, "Legio");

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromLegioIdOrderBy<Tbl93Comment>(CurrentTbl30Legio.LegioId);


            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        #endregion [Methods Legio ==> Tbl93Comments]                 


        //    Part 9    



        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        #endregion "Public Commands Connected Tables by DoubleClick"

        #region "Public Method Connected Tables by DoubleClick"

        private void GetConnectedTablesById(object o)
        {
            Tbl27InfraclassesList = _extCrud.GetInfraclassesCollectionFromInfraclassIdOrderBy<Tbl27Infraclass>(CurrentTbl30Legio.InfraclassId);

            Tbl24SubclassesAllList = _extCrud.GetCollectionAllOrderBy<Tbl24Subclass>("Subclass");

            InfraclassesView = CollectionViewSource.GetDefaultView(Tbl27InfraclassesList);
            InfraclassesView.Refresh();

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
                    if (CurrentTbl30Legio != null)
                    {
                        Tbl27InfraclassesList = _extCrud.GetInfraclassesCollectionFromInfraclassIdOrderBy<Tbl27Infraclass>(CurrentTbl30Legio.InfraclassId);

                        Tbl24SubclassesAllList = _extCrud.GetCollectionAllOrderBy<Tbl24Subclass>("Subclass");

                        InfraclassesView = CollectionViewSource.GetDefaultView(Tbl27InfraclassesList);
                        InfraclassesView.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }

                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl30Legio != null)
                    {
                        Tbl33OrdosList = _extCrud.GetOrdosCollectionFromLegioIdOrderBy<Tbl33Ordo>(CurrentTbl30Legio.LegioId);

                        Tbl30LegiosAllList = _extCrud.GetCollectionAllOrderBy<Tbl30Legio>("Legio");

                        OrdosView = CollectionViewSource.GetDefaultView(Tbl33OrdosList);
                        OrdosView.Refresh();
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
                    if (CurrentTbl30Legio != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromLegioIdOrderBy<Tbl93Comment>(CurrentTbl30Legio.LegioId);

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
                    if (CurrentTbl30Legio != null)
                    {
                        Tbl27InfraclassesList = _extCrud.GetInfraclassesCollectionFromInfraclassIdOrderBy<Tbl27Infraclass>(CurrentTbl30Legio.InfraclassId);

                        InfraclassesView = CollectionViewSource.GetDefaultView(Tbl27InfraclassesList);
                        InfraclassesView.Refresh();
                    }
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 1)
                {
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 2)
                {
                    if (CurrentTbl30Legio != null)
                    {
                        Tbl33OrdosList = _extCrud.GetOrdosCollectionFromLegioIdOrderBy<Tbl33Ordo>(CurrentTbl30Legio.LegioId);

                        Tbl30LegiosAllList = _extCrud.GetCollectionAllOrderBy<Tbl30Legio>("Legio");

                        OrdosView = CollectionViewSource.GetDefaultView(Tbl33OrdosList);
                        OrdosView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTbl30Legio != null)
                    {
                        Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromLegioIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl30Legio.LegioId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 4)
                {
                    if (CurrentTbl30Legio != null)
                    {
                        Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromLegioIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl30Legio.LegioId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 5)
                {
                    if (CurrentTbl30Legio != null)
                    {
                        Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("Author");

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromLegioIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl30Legio.LegioId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 2;
                }

                if (_selectedDetailTabIndex == 6)
                {
                    if (CurrentTbl30Legio != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromLegioIdOrderBy<Tbl93Comment>(CurrentTbl30Legio.LegioId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                }

                if (_selectedDetailTabIndex == 7)
                {
                    if (CurrentTbl30Legio != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromLegioIdOrderBy<Tbl93Comment>(CurrentTbl30Legio.LegioId);

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
                    if (CurrentTbl30Legio != null)
                    {
                        Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromLegioIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl30Legio.LegioId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                }

                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (CurrentTbl30Legio != null)
                    {
                        Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromLegioIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl30Legio.LegioId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 2;
                }

                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (CurrentTbl30Legio != null)
                    {
                        Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("Author");

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromLegioIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl30Legio.LegioId);

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


        #region "Public Properties Tbl30Legio"

        private string _searchLegioName = "";
        public string SearchLegioName
        {
            get => _searchLegioName;
            set { _searchLegioName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView LegiosView;
        private Tbl30Legio CurrentTbl30Legio => LegiosView?.CurrentItem as Tbl30Legio;

        private ObservableCollection<Tbl30Legio> _tbl30LegiosList;
        public ObservableCollection<Tbl30Legio> Tbl30LegiosList
        {
            get => _tbl30LegiosList;
            set { _tbl30LegiosList = value; RaisePropertyChanged(""); }
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

        #endregion "Public Properties"   

        #region "Public Properties Tbl27Infraclass"

        public ICollectionView InfraclassesView;
        private Tbl27Infraclass CurrentTbl27Infraclass => InfraclassesView?.CurrentItem as Tbl27Infraclass;

        private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesList;
        public ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesList
        {
            get => _tbl27InfraclassesList;
            set { _tbl27InfraclassesList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesAllList;
        public ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesAllList
        {
            get => _tbl27InfraclassesAllList;
            set { _tbl27InfraclassesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   

        #region "Public Properties Tbl33Ordo"

        public ICollectionView OrdosView;
        private Tbl33Ordo CurrentTbl33Ordo => OrdosView?.CurrentItem as Tbl33Ordo;

        private ObservableCollection<Tbl33Ordo> _tbl33OrdosList;
        public ObservableCollection<Tbl33Ordo> Tbl33OrdosList
        {
            get => _tbl33OrdosList;
            set { _tbl33OrdosList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl36Subordo"

        public ICollectionView SubordosView;
        private Tbl36Subordo CurrentTbl36Subordo => SubordosView?.CurrentItem as Tbl36Subordo;

        private ObservableCollection<Tbl36Subordo> _tbl36SubordosList;
        public ObservableCollection<Tbl36Subordo> Tbl36SubordosList
        {
            get => _tbl36SubordosList;
            set { _tbl36SubordosList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl24Subclass"

        private ObservableCollection<Tbl24Subclass> _tbl24SubclassesAllList;
        public ObservableCollection<Tbl24Subclass> Tbl24SubclassesAllList
        {
            get => _tbl24SubclassesAllList;
            set { _tbl24SubclassesAllList = value; RaisePropertyChanged(""); }
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
