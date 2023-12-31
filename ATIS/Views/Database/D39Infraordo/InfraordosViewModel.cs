﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;


using System.Windows.Data;
using System.Windows.Input;
using ATIS.Ui.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;

//    InfraordosViewModel Skriptdatum:  07.01.2021  10:32    

namespace ATIS.Ui.Views.Database.D39Infraordo
{

    public class InfraordosViewModel : ViewModelBase
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

        public InfraordosViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                // Code runs "for real" 
                Tbl39InfraordosList = new ObservableCollection<Tbl39Infraordo>();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          


        //    Part 1    



        #region [Commands Infraordo]

        private RelayCommand _getInfraordosByNameOrIdCommand;
        public ICommand GetInfraordosByNameOrIdCommand => _getInfraordosByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetInfraordosByNameOrId(SearchInfraordoName); });

        private RelayCommand _addInfraordoCommand;
        public ICommand AddInfraordoCommand => _addInfraordoCommand ??= new RelayCommand(delegate { ExecuteAddInfraordo(null); });

        private RelayCommand _copyInfraordoCommand;
        public ICommand CopyInfraordoCommand => _copyInfraordoCommand ??= new RelayCommand(delegate { ExecuteCopyInfraordo(null); });

        private RelayCommand _deleteInfraordoCommand;
        public ICommand DeleteInfraordoCommand => _deleteInfraordoCommand ??= new RelayCommand(delegate { ExecuteDeleteInfraordo(SearchInfraordoName); });

        private RelayCommand _saveInfraordoCommand;
        public ICommand SaveInfraordoCommand => _saveInfraordoCommand ??= new RelayCommand(delegate { ExecuteSaveInfraordo(SearchInfraordoName); });

        #endregion [Commands Infraordo]       


        #region [Methods Infraordo]

        private void ExecuteGetInfraordosByNameOrId(string searchName)
        {
            if (Tbl36SubordosAllList == null)
                Tbl36SubordosAllList ??= new ObservableCollection<Tbl36Subordo>();
            else
                Tbl36SubordosAllList.Clear();

            Tbl36SubordosAllList = _extCrud.GetCollectionAllOrderBy<Tbl36Subordo>("Subordo");

            if (Tbl39InfraordosList == null)
                Tbl39InfraordosList ??= new ObservableCollection<Tbl39Infraordo>();
            else
                Tbl39InfraordosList.Clear();

            Tbl39InfraordosList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl39Infraordo>(searchName, "Infraordo");

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl39InfraordosList.Count)) return;

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            InfraordosView = CollectionViewSource.GetDefaultView(Tbl39InfraordosList);
            InfraordosView.Refresh();
        }

        private void ExecuteAddInfraordo(object o)
        {
            if (Tbl39InfraordosList == null)
                Tbl39InfraordosList ??= new ObservableCollection<Tbl39Infraordo>();

            if (Tbl36SubordosAllList == null)
                Tbl36SubordosAllList ??= new ObservableCollection<Tbl36Subordo>();
            else
                Tbl36SubordosAllList.Clear();

            Tbl36SubordosAllList = _extCrud.GetCollectionAllOrderBy<Tbl36Subordo>("Subordo");

            Tbl39InfraordosList.Insert(0, new Tbl39Infraordo { InfraordoName = CultRes.StringsRes.DatasetNew });

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            InfraordosView = CollectionViewSource.GetDefaultView(Tbl39InfraordosList);
            InfraordosView.MoveCurrentToFirst();
        }

        private void ExecuteCopyInfraordo(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl39Infraordo)) return;

            Tbl39InfraordosList = _extCrud.CopyInfraordo(CurrentTbl39Infraordo);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            InfraordosView = CollectionViewSource.GetDefaultView(Tbl39InfraordosList);
            InfraordosView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteInfraordo(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl39Infraordo)) return;

            _extDelete.DeleteInfraordo(CurrentTbl39Infraordo);

            Tbl39InfraordosList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl39Infraordo>(searchName, "Infraordo");
            InfraordosView = CollectionViewSource.GetDefaultView(Tbl39InfraordosList);
            InfraordosView.MoveCurrentToLast();
        }

        private void ExecuteSaveInfraordo(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl39Infraordo)) return;

            _position = InfraordosView.CurrentPosition;

            var ret = _extSave.SaveInfraordo(CurrentTbl39Infraordo);

            if (ret != true)
            {
                InfraordosView = CollectionViewSource.GetDefaultView(Tbl39InfraordosList);
                InfraordosView.Refresh();
                return;
            }

            if (CurrentTbl39Infraordo.InfraordoId == 0) //new
            {
                Tbl39InfraordosList = _extCrud.GetLastInfraordosDatasetOrderById();
                InfraordosView = CollectionViewSource.GetDefaultView(Tbl39InfraordosList);
                InfraordosView.MoveCurrentToFirst();
            }
            else
            {
                Tbl39InfraordosList = _extCrud.GetInfraordosCollectionFromSearchNameOrIdOrderBy<Tbl39Infraordo>(searchName);
                InfraordosView = CollectionViewSource.GetDefaultView(Tbl39InfraordosList);
                InfraordosView.MoveCurrentToPosition(_position);
            }
        }
        #endregion [Methods Infraordo]                



        //    Part 2    


        #region "Public Commands Connect <== Tbl36Subordo"                 


        private RelayCommand _saveSubordoCommand;

        public ICommand SaveSubordoCommand => _saveSubordoCommand ??= new RelayCommand(delegate { ExecuteSaveSubordo(null); });

        private void ExecuteSaveSubordo(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl36Subordo)) return;

            _extSave.SaveSubordo(CurrentTbl36Subordo);

            Tbl36SubordosList = _extCrud.GetSubordosCollectionFromSubordoIdOrderBy<Tbl36Subordo>(CurrentTbl39Infraordo.SubordoId);
            SubordosView = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
            SubordosView.Refresh();
        }

        #endregion "Public Commands"                  


        //    Part 3    





        //    Part 4    


        #region [Public Commands Connect ==> Tbl42Superfamily]                 

        private RelayCommand _addSuperfamilyCommand;
        public ICommand AddSuperfamilyCommand => _addSuperfamilyCommand ??= new RelayCommand(delegate { ExecuteAddSuperfamily(null); });

        private RelayCommand _copySuperfamilyCommand;
        public ICommand CopySuperfamilyCommand => _copySuperfamilyCommand ??= new RelayCommand(delegate { ExecuteCopySuperfamily(null); });

        private RelayCommand _deleteSuperfamilyCommand;
        public ICommand DeleteSuperfamilyCommand => _deleteSuperfamilyCommand ??= new RelayCommand(delegate { ExecuteDeleteSuperfamily(null); });

        private RelayCommand _saveSuperfamilyCommand;
        public ICommand SaveSuperfamilyCommand => _saveSuperfamilyCommand ??= new RelayCommand(delegate { ExecuteSaveSuperfamily(null); });

        #endregion [Public Commands Connect ==> Tbl42Superfamily]    

        #region [Public Methods Connect ==> Tbl42Superfamily]                   

        private void ExecuteAddSuperfamily(object o)
        {
            if (Tbl39InfraordosAllList == null)
                Tbl39InfraordosAllList ??= new ObservableCollection<Tbl39Infraordo>();
            else
                Tbl39InfraordosAllList.Clear();

            Tbl39InfraordosAllList = _extCrud.GetCollectionAllOrderBy<Tbl39Infraordo>("Infraordo");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            Tbl42SuperfamiliesList ??= new ObservableCollection<Tbl42Superfamily>();

            Tbl42SuperfamiliesList.Insert(0, new Tbl42Superfamily { SuperfamilyName = CultRes.StringsRes.DatasetNew });

            SuperfamiliesView = CollectionViewSource.GetDefaultView(Tbl42SuperfamiliesList);
            SuperfamiliesView.MoveCurrentToFirst();
        }

        private void ExecuteCopySuperfamily(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl42Superfamily)) return;

            Tbl42SuperfamiliesList = _extCrud.CopySuperfamily(CurrentTbl42Superfamily);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            SuperfamiliesView = CollectionViewSource.GetDefaultView(Tbl42SuperfamiliesList);
            SuperfamiliesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteSuperfamily(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl42Superfamily)) return;

            _extDelete.DeleteSuperfamily(CurrentTbl42Superfamily);

            Tbl42SuperfamiliesList = _extCrud.GetSuperfamiliesCollectionFromInfraordoIdOrderBy<Tbl42Superfamily>(CurrentTbl42Superfamily.SuperfamilyId);
            SuperfamiliesView = CollectionViewSource.GetDefaultView(Tbl42SuperfamiliesList);
            SuperfamiliesView.MoveCurrentToFirst();
        }

        private void ExecuteSaveSuperfamily(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl42Superfamily)) return;

            CurrentTbl42Superfamily.InfraordoId = CurrentTbl39Infraordo.InfraordoId;

            _extSave.SaveSuperfamily(CurrentTbl42Superfamily);
            Tbl42SuperfamiliesList = _extCrud.GetSuperfamiliesCollectionFromInfraordoIdOrderBy<Tbl42Superfamily>(CurrentTbl42Superfamily.InfraordoId);

            SuperfamiliesView = CollectionViewSource.GetDefaultView(Tbl42SuperfamiliesList);
            SuperfamiliesView.MoveCurrentToFirst();
        }

        #endregion [Public Methods  Connect ==> Tbl42Superfamily]                                                                                                                                            



        //    Part 5    



        //    Part 6    




        //    Part 7    



        //    Part 8    


        #region [Commands Infraordo ==> Tbl90Reference Author]

        private RelayCommand _addReferenceAuthorCommand;

        public ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteAddReferenceAuthor(null); });

        private RelayCommand _copyReferenceAuthorCommand;

        public ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceAuthor(null); });

        private RelayCommand _deleteReferenceAuthorCommand;

        public ICommand DeleteReferenceAuthorCommand => _deleteReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceAuthor(null); });

        private RelayCommand _saveReferenceAuthorCommand;

        public ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceAuthor(null); });

        #endregion [Commands Infraordo ==> Tbl90Reference Author]                

        #region [Methods Infraordo ==> Tbl90Reference Author]

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

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceInfraordo(CurrentTbl90ReferenceAuthor, "Author");

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceAuthor(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            _extDelete.DeleteReferenceAuthor(CurrentTbl90ReferenceAuthor);

            Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromInfraordoIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90ReferenceAuthor.InfraordoId);
            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }

        public void ExecuteSaveReferenceAuthor(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            CurrentTbl90ReferenceAuthor.InfraordoId = CurrentTbl39Infraordo.InfraordoId;

            _extSave.SaveReferenceAuthor(CurrentTbl90ReferenceAuthor, "Infraordo");

            Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromInfraordoIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl39Infraordo.InfraordoId);

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }
        #endregion [Methods Infraordo ==> Tbl90Reference Author]              

        #region [Commands Infraordo ==> Tbl90Reference Source]      

        private RelayCommand _addReferenceSourceCommand;

        public ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteAddReferenceSource(null); });

        private RelayCommand _copyReferenceSourceCommand;

        public ICommand CopyReferenceSourceCommand => _copyReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceSource(null); });

        private RelayCommand _deleteReferenceSourceCommand;

        public ICommand DeleteReferenceSourceCommand => _deleteReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceSource(null); });

        private RelayCommand _saveReferenceSourceCommand;

        public ICommand SaveReferenceSourceCommand => _saveReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceSource(null); });


        #endregion [Commands Infraordo ==> Tbl90Reference Source]         

        #region [Methods Infraordo ==> Tbl90Reference Source]      

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

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceInfraordo(CurrentTbl90ReferenceSource, "Source");

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceSource(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            _extDelete.DeleteReferenceSource(CurrentTbl90ReferenceSource);

            Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromInfraordoIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl39Infraordo.InfraordoId);
            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        public void ExecuteSaveReferenceSource(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            CurrentTbl90ReferenceSource.InfraordoId = CurrentTbl39Infraordo.InfraordoId;

            _extSave.SaveReferenceSource(CurrentTbl90ReferenceSource, "Infraordo");

            Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromInfraordoIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl39Infraordo.InfraordoId);


            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }
        #endregion [Methods Infraordo ==> Tbl90Reference Source]                    

        #region [Commands Infraordo ==> Tbl90Reference Expert]                 

        private RelayCommand _addReferenceExpertCommand;

        public ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteAddReferenceExpert(null); });

        private RelayCommand _copyReferenceExpertCommand;

        public ICommand CopyReferenceExpertCommand => _copyReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceExpert(null); });

        private RelayCommand _deleteReferenceExpertCommand;

        public ICommand DeleteReferenceExpertCommand => _deleteReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceExpert(null); });
        private RelayCommand _saveReferenceExpertCommand;

        public ICommand SaveReferenceExpertCommand => _saveReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceExpert(null); });

        #endregion [Commands Infraordo ==> Tbl90Reference Expert]                    


        #region [Methods Infraordo ==> Tbl90Reference Expert]                 

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

            Tbl90ReferenceExpertsList = _extCrud.CopyReferenceInfraordo(CurrentTbl90ReferenceExpert, "Expert");

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceExpert(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            _extDelete.DeleteReferenceExpert(CurrentTbl90ReferenceExpert);

            Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromInfraordoIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl39Infraordo.InfraordoId);
            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }

        public void ExecuteSaveReferenceExpert(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            CurrentTbl90ReferenceExpert.InfraordoId = CurrentTbl39Infraordo.InfraordoId;

            _extSave.SaveReferenceExpert(CurrentTbl90ReferenceExpert, "Infraordo");

            Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromInfraordoIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl39Infraordo.InfraordoId);


            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }
        #endregion [Methods Infraordo ==> Tbl90Reference Expert]                               

        #region [Commands Infraordo ==> Tbl93Comments]        

        private RelayCommand _addCommentCommand;

        public ICommand AddCommentCommand => _addCommentCommand ??= new RelayCommand(delegate { ExecuteAddComment(null); });

        private RelayCommand _copyCommentCommand;

        public ICommand CopyCommentCommand => _copyCommentCommand ??= new RelayCommand(delegate { ExecuteCopyComment(null); });

        private RelayCommand _deleteCommentCommand;

        public ICommand DeleteCommentCommand => _deleteCommentCommand ??= new RelayCommand(delegate { ExecuteDeleteComment(null); });

        private RelayCommand _saveCommentCommand;

        public ICommand SaveCommentCommand => _saveCommentCommand ??= new RelayCommand(delegate { ExecuteSaveComment(null); });

        #endregion [Commands Infraordo ==> Tbl93Comments]        



        #region [Methods Infraordo ==> Tbl93Comments]        

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

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromInfraordoIdOrderBy<Tbl93Comment>(CurrentTbl39Infraordo.InfraordoId);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }

        private void ExecuteSaveComment(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            CurrentTbl93Comment.InfraordoId = CurrentTbl39Infraordo.InfraordoId;

            _extSave.SaveComment(CurrentTbl93Comment, "Infraordo");

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromInfraordoIdOrderBy<Tbl93Comment>(CurrentTbl39Infraordo.InfraordoId);


            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        #endregion [Methods Infraordo ==> Tbl93Comments]                 


        //    Part 9    



        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        #endregion "Public Commands Connected Tables by DoubleClick"

        #region "Public Method Connected Tables by DoubleClick"

        private void GetConnectedTablesById(object o)
        {
            Tbl36SubordosList = _extCrud.GetSubordosCollectionFromSubordoIdOrderBy<Tbl36Subordo>(CurrentTbl39Infraordo.SubordoId);

            Tbl33OrdosAllList = _extCrud.GetCollectionAllOrderBy<Tbl33Ordo>("Ordo");

            SubordosView = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
            SubordosView.Refresh();

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
                    if (CurrentTbl39Infraordo != null)
                    {
                        Tbl36SubordosList = _extCrud.GetSubordosCollectionFromSubordoIdOrderBy<Tbl36Subordo>(CurrentTbl39Infraordo.SubordoId);

                        Tbl33OrdosAllList = _extCrud.GetCollectionAllOrderBy<Tbl33Ordo>("Ordo");

                        SubordosView = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
                        SubordosView.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }

                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl39Infraordo != null)
                    {
                        Tbl42SuperfamiliesList = _extCrud.GetSuperfamiliesCollectionFromInfraordoIdOrderBy<Tbl42Superfamily>(CurrentTbl39Infraordo.InfraordoId);

                        Tbl39InfraordosAllList = _extCrud.GetCollectionAllOrderBy<Tbl39Infraordo>("Infraordo");

                        SuperfamiliesView = CollectionViewSource.GetDefaultView(Tbl42SuperfamiliesList);
                        SuperfamiliesView.Refresh();
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
                    if (CurrentTbl39Infraordo != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromInfraordoIdOrderBy<Tbl93Comment>(CurrentTbl39Infraordo.InfraordoId);

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
                    if (CurrentTbl39Infraordo != null)
                    {
                        Tbl36SubordosList = _extCrud.GetSubordosCollectionFromSubordoIdOrderBy<Tbl36Subordo>(CurrentTbl39Infraordo.SubordoId);

                        SubordosView = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
                        SubordosView.Refresh();
                    }
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 1)
                {
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 2)
                {
                    if (CurrentTbl39Infraordo != null)
                    {
                        Tbl42SuperfamiliesList = _extCrud.GetSuperfamiliesCollectionFromInfraordoIdOrderBy<Tbl42Superfamily>(CurrentTbl39Infraordo.InfraordoId);

                        Tbl39InfraordosAllList = _extCrud.GetCollectionAllOrderBy<Tbl39Infraordo>("Infraordo");

                        SuperfamiliesView = CollectionViewSource.GetDefaultView(Tbl42SuperfamiliesList);
                        SuperfamiliesView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTbl39Infraordo != null)
                    {
                        Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromInfraordoIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl39Infraordo.InfraordoId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 4)
                {
                    if (CurrentTbl39Infraordo != null)
                    {
                        Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromInfraordoIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl39Infraordo.InfraordoId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 5)
                {
                    if (CurrentTbl39Infraordo != null)
                    {
                        Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("Author");

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromInfraordoIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl39Infraordo.InfraordoId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 2;
                }

                if (_selectedDetailTabIndex == 6)
                {
                    if (CurrentTbl39Infraordo != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromInfraordoIdOrderBy<Tbl93Comment>(CurrentTbl39Infraordo.InfraordoId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                }

                if (_selectedDetailTabIndex == 7)
                {
                    if (CurrentTbl39Infraordo != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromInfraordoIdOrderBy<Tbl93Comment>(CurrentTbl39Infraordo.InfraordoId);

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
                    if (CurrentTbl39Infraordo != null)
                    {
                        Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromInfraordoIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl39Infraordo.InfraordoId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                }

                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (CurrentTbl39Infraordo != null)
                    {
                        Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromInfraordoIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl39Infraordo.InfraordoId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 2;
                }

                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (CurrentTbl39Infraordo != null)
                    {
                        Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("Author");

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromInfraordoIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl39Infraordo.InfraordoId);

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


        #region "Public Properties Tbl39Infraordo"

        private string _searchInfraordoName = "";
        public string SearchInfraordoName
        {
            get => _searchInfraordoName;
            set { _searchInfraordoName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView InfraordosView;
        private Tbl39Infraordo CurrentTbl39Infraordo => InfraordosView?.CurrentItem as Tbl39Infraordo;

        private ObservableCollection<Tbl39Infraordo> _tbl39InfraordosList;
        public ObservableCollection<Tbl39Infraordo> Tbl39InfraordosList
        {
            get => _tbl39InfraordosList;
            set { _tbl39InfraordosList = value; RaisePropertyChanged(""); }
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

        private ObservableCollection<Tbl36Subordo> _tbl36SubordosAllList;
        public ObservableCollection<Tbl36Subordo> Tbl36SubordosAllList
        {
            get => _tbl36SubordosAllList;
            set { _tbl36SubordosAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   

        #region "Public Properties Tbl42Superfamily"

        public ICollectionView SuperfamiliesView;
        private Tbl42Superfamily CurrentTbl42Superfamily => SuperfamiliesView?.CurrentItem as Tbl42Superfamily;

        private ObservableCollection<Tbl42Superfamily> _tbl42SuperfamiliesList;
        public ObservableCollection<Tbl42Superfamily> Tbl42SuperfamiliesList
        {
            get => _tbl42SuperfamiliesList;
            set { _tbl42SuperfamiliesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl45Family"

        public ICollectionView FamiliesView;
        private Tbl45Family CurrentTbl45Family => FamiliesView?.CurrentItem as Tbl45Family;

        private ObservableCollection<Tbl45Family> _tbl45FamiliesList;
        public ObservableCollection<Tbl45Family> Tbl45FamiliesList
        {
            get => _tbl45FamiliesList;
            set { _tbl45FamiliesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl33Ordo"

        private ObservableCollection<Tbl33Ordo> _tbl33OrdosAllList;
        public ObservableCollection<Tbl33Ordo> Tbl33OrdosAllList
        {
            get => _tbl33OrdosAllList;
            set { _tbl33OrdosAllList = value; RaisePropertyChanged(""); }
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
