﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;


using System.Windows.Data;
using System.Windows.Input;
using ATIS.Ui.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;

//    SuperfamiliesViewModel Skriptdatum:  07.01.2021  10:32    

namespace ATIS.Ui.Views.Database.D42Superfamily
{

    public class SuperfamiliesViewModel : ViewModelBase
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

        public SuperfamiliesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                // Code runs "for real" 
                Tbl42SuperfamiliesList = new ObservableCollection<Tbl42Superfamily>();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          


        //    Part 1    



        #region [Commands Superfamily]

        private RelayCommand _getSuperfamiliesByNameOrIdCommand;
        public ICommand GetSuperfamiliesByNameOrIdCommand => _getSuperfamiliesByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetSuperfamiliesByNameOrId(SearchSuperfamilyName); });

        private RelayCommand _addSuperfamilyCommand;
        public ICommand AddSuperfamilyCommand => _addSuperfamilyCommand ??= new RelayCommand(delegate { ExecuteAddSuperfamily(null); });

        private RelayCommand _copySuperfamilyCommand;
        public ICommand CopySuperfamilyCommand => _copySuperfamilyCommand ??= new RelayCommand(delegate { ExecuteCopySuperfamily(null); });

        private RelayCommand _deleteSuperfamilyCommand;
        public ICommand DeleteSuperfamilyCommand => _deleteSuperfamilyCommand ??= new RelayCommand(delegate { ExecuteDeleteSuperfamily(SearchSuperfamilyName); });

        private RelayCommand _saveSuperfamilyCommand;
        public ICommand SaveSuperfamilyCommand => _saveSuperfamilyCommand ??= new RelayCommand(delegate { ExecuteSaveSuperfamily(SearchSuperfamilyName); });

        #endregion [Commands Superfamily]       


        #region [Methods Superfamily]

        private void ExecuteGetSuperfamiliesByNameOrId(string searchName)
        {
            if (Tbl39InfraordosAllList == null)
                Tbl39InfraordosAllList ??= new ObservableCollection<Tbl39Infraordo>();
            else
                Tbl39InfraordosAllList.Clear();

            Tbl39InfraordosAllList = _extCrud.GetCollectionAllOrderBy<Tbl39Infraordo>("Infraordo");

            if (Tbl42SuperfamiliesList == null)
                Tbl42SuperfamiliesList ??= new ObservableCollection<Tbl42Superfamily>();
            else
                Tbl42SuperfamiliesList.Clear();

            Tbl42SuperfamiliesList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl42Superfamily>(searchName, "Superfamily");

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl42SuperfamiliesList.Count)) return;

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            SuperfamiliesView = CollectionViewSource.GetDefaultView(Tbl42SuperfamiliesList);
            SuperfamiliesView.Refresh();
        }

        private void ExecuteAddSuperfamily(object o)
        {
            if (Tbl42SuperfamiliesList == null)
                Tbl42SuperfamiliesList ??= new ObservableCollection<Tbl42Superfamily>();

            if (Tbl39InfraordosAllList == null)
                Tbl39InfraordosAllList ??= new ObservableCollection<Tbl39Infraordo>();
            else
                Tbl39InfraordosAllList.Clear();

            Tbl39InfraordosAllList = _extCrud.GetCollectionAllOrderBy<Tbl39Infraordo>("Infraordo");

            Tbl42SuperfamiliesList.Insert(0, new Tbl42Superfamily { SuperfamilyName = CultRes.StringsRes.DatasetNew });

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

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

        private void ExecuteDeleteSuperfamily(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl42Superfamily)) return;

            _extDelete.DeleteSuperfamily(CurrentTbl42Superfamily);

            Tbl42SuperfamiliesList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl42Superfamily>(searchName, "Superfamily");
            SuperfamiliesView = CollectionViewSource.GetDefaultView(Tbl42SuperfamiliesList);
            SuperfamiliesView.MoveCurrentToLast();
        }

        private void ExecuteSaveSuperfamily(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl42Superfamily)) return;

            _position = SuperfamiliesView.CurrentPosition;

            var ret = _extSave.SaveSuperfamily(CurrentTbl42Superfamily);

            if (ret != true)
            {
                SuperfamiliesView = CollectionViewSource.GetDefaultView(Tbl42SuperfamiliesList);
                SuperfamiliesView.Refresh();
                return;
            }

            if (CurrentTbl42Superfamily.SuperfamilyId == 0) //new
            {
                Tbl42SuperfamiliesList = _extCrud.GetLastSuperfamiliesDatasetOrderById();
                SuperfamiliesView = CollectionViewSource.GetDefaultView(Tbl42SuperfamiliesList);
                SuperfamiliesView.MoveCurrentToFirst();
            }
            else
            {
                Tbl42SuperfamiliesList = _extCrud.GetSuperfamiliesCollectionFromSearchNameOrIdOrderBy<Tbl42Superfamily>(searchName);
                SuperfamiliesView = CollectionViewSource.GetDefaultView(Tbl42SuperfamiliesList);
                SuperfamiliesView.MoveCurrentToPosition(_position);
            }
        }
        #endregion [Methods Superfamily]                



        //    Part 2    


        #region "Public Commands Connect <== Tbl39Infraordo"                 


        private RelayCommand _saveInfraordoCommand;

        public ICommand SaveInfraordoCommand => _saveInfraordoCommand ??= new RelayCommand(delegate { ExecuteSaveInfraordo(null); });

        private void ExecuteSaveInfraordo(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl39Infraordo)) return;

            _extSave.SaveInfraordo(CurrentTbl39Infraordo);

            Tbl39InfraordosList = _extCrud.GetInfraordosCollectionFromInfraordoIdOrderBy<Tbl39Infraordo>(CurrentTbl42Superfamily.InfraordoId);
            InfraordosView = CollectionViewSource.GetDefaultView(Tbl39InfraordosList);
            InfraordosView.Refresh();
        }

        #endregion "Public Commands"                  


        //    Part 3    





        //    Part 4    


        #region [Public Commands Connect ==> Tbl45Family]                 

        private RelayCommand _addFamilyCommand;
        public ICommand AddFamilyCommand => _addFamilyCommand ??= new RelayCommand(delegate { ExecuteAddFamily(null); });

        private RelayCommand _copyFamilyCommand;
        public ICommand CopyFamilyCommand => _copyFamilyCommand ??= new RelayCommand(delegate { ExecuteCopyFamily(null); });

        private RelayCommand _deleteFamilyCommand;
        public ICommand DeleteFamilyCommand => _deleteFamilyCommand ??= new RelayCommand(delegate { ExecuteDeleteFamily(null); });

        private RelayCommand _saveFamilyCommand;
        public ICommand SaveFamilyCommand => _saveFamilyCommand ??= new RelayCommand(delegate { ExecuteSaveFamily(null); });

        #endregion [Public Commands Connect ==> Tbl45Family]    

        #region [Public Methods Connect ==> Tbl45Family]                   

        private void ExecuteAddFamily(object o)
        {
            if (Tbl42SuperfamiliesAllList == null)
                Tbl42SuperfamiliesAllList ??= new ObservableCollection<Tbl42Superfamily>();
            else
                Tbl42SuperfamiliesAllList.Clear();

            Tbl42SuperfamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl42Superfamily>("Superfamily");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            Tbl45FamiliesList ??= new ObservableCollection<Tbl45Family>();

            Tbl45FamiliesList.Insert(0, new Tbl45Family { FamilyName = CultRes.StringsRes.DatasetNew });

            FamiliesView = CollectionViewSource.GetDefaultView(Tbl45FamiliesList);
            FamiliesView.MoveCurrentToFirst();
        }

        private void ExecuteCopyFamily(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl45Family)) return;

            Tbl45FamiliesList = _extCrud.CopyFamily(CurrentTbl45Family);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            FamiliesView = CollectionViewSource.GetDefaultView(Tbl45FamiliesList);
            FamiliesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteFamily(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl45Family)) return;

            _extDelete.DeleteFamily(CurrentTbl45Family);

            Tbl45FamiliesList = _extCrud.GetFamiliesCollectionFromSuperfamilyIdOrderBy<Tbl45Family>(CurrentTbl45Family.FamilyId);
            FamiliesView = CollectionViewSource.GetDefaultView(Tbl45FamiliesList);
            FamiliesView.MoveCurrentToFirst();
        }

        private void ExecuteSaveFamily(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl45Family)) return;

            CurrentTbl45Family.SuperfamilyId = CurrentTbl42Superfamily.SuperfamilyId;

            _extSave.SaveFamily(CurrentTbl45Family);
            Tbl45FamiliesList = _extCrud.GetFamiliesCollectionFromSuperfamilyIdOrderBy<Tbl45Family>(CurrentTbl45Family.SuperfamilyId);

            FamiliesView = CollectionViewSource.GetDefaultView(Tbl45FamiliesList);
            FamiliesView.MoveCurrentToFirst();
        }

        #endregion [Public Methods  Connect ==> Tbl45Family]                                                                                                                                            



        //    Part 5    



        //    Part 6    




        //    Part 7    



        //    Part 8    


        #region [Commands Superfamily ==> Tbl90Reference Author]

        private RelayCommand _addReferenceAuthorCommand;

        public ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteAddReferenceAuthor(null); });

        private RelayCommand _copyReferenceAuthorCommand;

        public ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceAuthor(null); });

        private RelayCommand _deleteReferenceAuthorCommand;

        public ICommand DeleteReferenceAuthorCommand => _deleteReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceAuthor(null); });

        private RelayCommand _saveReferenceAuthorCommand;

        public ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceAuthor(null); });

        #endregion [Commands Superfamily ==> Tbl90Reference Author]                

        #region [Methods Superfamily ==> Tbl90Reference Author]

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

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceSuperfamily(CurrentTbl90ReferenceAuthor, "Author");

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceAuthor(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            _extDelete.DeleteReferenceAuthor(CurrentTbl90ReferenceAuthor);

            Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromSuperfamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90ReferenceAuthor.SuperfamilyId);
            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }

        public void ExecuteSaveReferenceAuthor(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            CurrentTbl90ReferenceAuthor.SuperfamilyId = CurrentTbl42Superfamily.SuperfamilyId;

            _extSave.SaveReferenceAuthor(CurrentTbl90ReferenceAuthor, "Superfamily");

            Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromSuperfamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl42Superfamily.SuperfamilyId);

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }
        #endregion [Methods Superfamily ==> Tbl90Reference Author]              

        #region [Commands Superfamily ==> Tbl90Reference Source]      

        private RelayCommand _addReferenceSourceCommand;

        public ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteAddReferenceSource(null); });

        private RelayCommand _copyReferenceSourceCommand;

        public ICommand CopyReferenceSourceCommand => _copyReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceSource(null); });

        private RelayCommand _deleteReferenceSourceCommand;

        public ICommand DeleteReferenceSourceCommand => _deleteReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceSource(null); });

        private RelayCommand _saveReferenceSourceCommand;

        public ICommand SaveReferenceSourceCommand => _saveReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceSource(null); });


        #endregion [Commands Superfamily ==> Tbl90Reference Source]         

        #region [Methods Superfamily ==> Tbl90Reference Source]      

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

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceSuperfamily(CurrentTbl90ReferenceSource, "Source");

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceSource(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            _extDelete.DeleteReferenceSource(CurrentTbl90ReferenceSource);

            Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromSuperfamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl42Superfamily.SuperfamilyId);
            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        public void ExecuteSaveReferenceSource(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            CurrentTbl90ReferenceSource.SuperfamilyId = CurrentTbl42Superfamily.SuperfamilyId;

            _extSave.SaveReferenceSource(CurrentTbl90ReferenceSource, "Superfamily");

            Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromSuperfamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl42Superfamily.SuperfamilyId);


            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }
        #endregion [Methods Superfamily ==> Tbl90Reference Source]                    

        #region [Commands Superfamily ==> Tbl90Reference Expert]                 

        private RelayCommand _addReferenceExpertCommand;

        public ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteAddReferenceExpert(null); });

        private RelayCommand _copyReferenceExpertCommand;

        public ICommand CopyReferenceExpertCommand => _copyReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceExpert(null); });

        private RelayCommand _deleteReferenceExpertCommand;

        public ICommand DeleteReferenceExpertCommand => _deleteReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceExpert(null); });
        private RelayCommand _saveReferenceExpertCommand;

        public ICommand SaveReferenceExpertCommand => _saveReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceExpert(null); });

        #endregion [Commands Superfamily ==> Tbl90Reference Expert]                    


        #region [Methods Superfamily ==> Tbl90Reference Expert]                 

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

            Tbl90ReferenceExpertsList = _extCrud.CopyReferenceSuperfamily(CurrentTbl90ReferenceExpert, "Expert");

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceExpert(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            _extDelete.DeleteReferenceExpert(CurrentTbl90ReferenceExpert);

            Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromSuperfamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl42Superfamily.SuperfamilyId);
            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }

        public void ExecuteSaveReferenceExpert(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            CurrentTbl90ReferenceExpert.SuperfamilyId = CurrentTbl42Superfamily.SuperfamilyId;

            _extSave.SaveReferenceExpert(CurrentTbl90ReferenceExpert, "Superfamily");

            Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromSuperfamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl42Superfamily.SuperfamilyId);


            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }
        #endregion [Methods Superfamily ==> Tbl90Reference Expert]                               

        #region [Commands Superfamily ==> Tbl93Comments]        

        private RelayCommand _addCommentCommand;

        public ICommand AddCommentCommand => _addCommentCommand ??= new RelayCommand(delegate { ExecuteAddComment(null); });

        private RelayCommand _copyCommentCommand;

        public ICommand CopyCommentCommand => _copyCommentCommand ??= new RelayCommand(delegate { ExecuteCopyComment(null); });

        private RelayCommand _deleteCommentCommand;

        public ICommand DeleteCommentCommand => _deleteCommentCommand ??= new RelayCommand(delegate { ExecuteDeleteComment(null); });

        private RelayCommand _saveCommentCommand;

        public ICommand SaveCommentCommand => _saveCommentCommand ??= new RelayCommand(delegate { ExecuteSaveComment(null); });

        #endregion [Commands Superfamily ==> Tbl93Comments]        



        #region [Methods Superfamily ==> Tbl93Comments]        

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

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromSuperfamilyIdOrderBy<Tbl93Comment>(CurrentTbl42Superfamily.SuperfamilyId);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }

        private void ExecuteSaveComment(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            CurrentTbl93Comment.SuperfamilyId = CurrentTbl42Superfamily.SuperfamilyId;

            _extSave.SaveComment(CurrentTbl93Comment, "Superfamily");

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromSuperfamilyIdOrderBy<Tbl93Comment>(CurrentTbl42Superfamily.SuperfamilyId);


            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        #endregion [Methods Superfamily ==> Tbl93Comments]                 


        //    Part 9    



        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        #endregion "Public Commands Connected Tables by DoubleClick"

        #region "Public Method Connected Tables by DoubleClick"

        private void GetConnectedTablesById(object o)
        {
            Tbl39InfraordosList = _extCrud.GetInfraordosCollectionFromInfraordoIdOrderBy<Tbl39Infraordo>(CurrentTbl42Superfamily.InfraordoId);

            Tbl36SubordosAllList = _extCrud.GetCollectionAllOrderBy<Tbl36Subordo>("Subordo");

            InfraordosView = CollectionViewSource.GetDefaultView(Tbl39InfraordosList);
            InfraordosView.Refresh();

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
                    if (CurrentTbl42Superfamily != null)
                    {
                        Tbl39InfraordosList = _extCrud.GetInfraordosCollectionFromInfraordoIdOrderBy<Tbl39Infraordo>(CurrentTbl42Superfamily.InfraordoId);

                        Tbl36SubordosAllList = _extCrud.GetCollectionAllOrderBy<Tbl36Subordo>("Subordo");

                        InfraordosView = CollectionViewSource.GetDefaultView(Tbl39InfraordosList);
                        InfraordosView.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }

                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl42Superfamily != null)
                    {
                        Tbl45FamiliesList = _extCrud.GetFamiliesCollectionFromSuperfamilyIdOrderBy<Tbl45Family>(CurrentTbl42Superfamily.SuperfamilyId);

                        Tbl42SuperfamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl42Superfamily>("Superfamily");

                        FamiliesView = CollectionViewSource.GetDefaultView(Tbl45FamiliesList);
                        FamiliesView.Refresh();
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
                    if (CurrentTbl42Superfamily != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromSuperfamilyIdOrderBy<Tbl93Comment>(CurrentTbl42Superfamily.SuperfamilyId);

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
                    if (CurrentTbl42Superfamily != null)
                    {
                        Tbl39InfraordosList = _extCrud.GetInfraordosCollectionFromInfraordoIdOrderBy<Tbl39Infraordo>(CurrentTbl42Superfamily.InfraordoId);

                        InfraordosView = CollectionViewSource.GetDefaultView(Tbl39InfraordosList);
                        InfraordosView.Refresh();
                    }
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 1)
                {
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 2)
                {
                    if (CurrentTbl42Superfamily != null)
                    {
                        Tbl45FamiliesList = _extCrud.GetFamiliesCollectionFromSuperfamilyIdOrderBy<Tbl45Family>(CurrentTbl42Superfamily.SuperfamilyId);

                        Tbl42SuperfamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl42Superfamily>("Superfamily");

                        FamiliesView = CollectionViewSource.GetDefaultView(Tbl45FamiliesList);
                        FamiliesView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTbl42Superfamily != null)
                    {
                        Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromSuperfamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl42Superfamily.SuperfamilyId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 4)
                {
                    if (CurrentTbl42Superfamily != null)
                    {
                        Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromSuperfamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl42Superfamily.SuperfamilyId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 5)
                {
                    if (CurrentTbl42Superfamily != null)
                    {
                        Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("Author");

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromSuperfamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl42Superfamily.SuperfamilyId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 2;
                }

                if (_selectedDetailTabIndex == 6)
                {
                    if (CurrentTbl42Superfamily != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromSuperfamilyIdOrderBy<Tbl93Comment>(CurrentTbl42Superfamily.SuperfamilyId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                }

                if (_selectedDetailTabIndex == 7)
                {
                    if (CurrentTbl42Superfamily != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromSuperfamilyIdOrderBy<Tbl93Comment>(CurrentTbl42Superfamily.SuperfamilyId);

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
                    if (CurrentTbl42Superfamily != null)
                    {
                        Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromSuperfamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl42Superfamily.SuperfamilyId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                }

                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (CurrentTbl42Superfamily != null)
                    {
                        Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromSuperfamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl42Superfamily.SuperfamilyId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 2;
                }

                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (CurrentTbl42Superfamily != null)
                    {
                        Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("Author");

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromSuperfamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl42Superfamily.SuperfamilyId);

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


        #region "Public Properties Tbl42Superfamily"

        private string _searchSuperfamilyName = "";
        public string SearchSuperfamilyName
        {
            get => _searchSuperfamilyName;
            set { _searchSuperfamilyName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView SuperfamiliesView;
        private Tbl42Superfamily CurrentTbl42Superfamily => SuperfamiliesView?.CurrentItem as Tbl42Superfamily;

        private ObservableCollection<Tbl42Superfamily> _tbl42SuperfamiliesList;
        public ObservableCollection<Tbl42Superfamily> Tbl42SuperfamiliesList
        {
            get => _tbl42SuperfamiliesList;
            set { _tbl42SuperfamiliesList = value; RaisePropertyChanged(""); }
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

        #endregion "Public Properties"   

        #region "Public Properties Tbl39Infraordo"

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

        #region "Public Properties Tbl48Subfamily"

        public ICollectionView SubfamiliesView;
        private Tbl48Subfamily CurrentTbl48Subfamily => SubfamiliesView?.CurrentItem as Tbl48Subfamily;

        private ObservableCollection<Tbl48Subfamily> _tbl48SubfamiliesList;
        public ObservableCollection<Tbl48Subfamily> Tbl48SubfamiliesList
        {
            get => _tbl48SubfamiliesList;
            set { _tbl48SubfamiliesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl36Subordo"

        private ObservableCollection<Tbl36Subordo> _tbl36SubordosAllList;
        public ObservableCollection<Tbl36Subordo> Tbl36SubordosAllList
        {
            get => _tbl36SubordosAllList;
            set { _tbl36SubordosAllList = value; RaisePropertyChanged(""); }
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
