﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;


using System.Windows.Data;
using System.Windows.Input;
using ATIS.Ui.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;

//    InfrafamiliesViewModel Skriptdatum:  07.01.2021  10:32    

namespace ATIS.Ui.Views.Database.D51Infrafamily
{

    public class InfrafamiliesViewModel : ViewModelBase
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

        public InfrafamiliesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                // Code runs "for real" 
                Tbl51InfrafamiliesList = new ObservableCollection<Tbl51Infrafamily>();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          


        //    Part 1    



        #region [Commands Infrafamily]

        private RelayCommand _getInfrafamiliesByNameOrIdCommand;
        public ICommand GetInfrafamiliesByNameOrIdCommand => _getInfrafamiliesByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetInfrafamiliesByNameOrId(SearchInfrafamilyName); });

        private RelayCommand _addInfrafamilyCommand;
        public ICommand AddInfrafamilyCommand => _addInfrafamilyCommand ??= new RelayCommand(delegate { ExecuteAddInfrafamily(null); });

        private RelayCommand _copyInfrafamilyCommand;
        public ICommand CopyInfrafamilyCommand => _copyInfrafamilyCommand ??= new RelayCommand(delegate { ExecuteCopyInfrafamily(null); });

        private RelayCommand _deleteInfrafamilyCommand;
        public ICommand DeleteInfrafamilyCommand => _deleteInfrafamilyCommand ??= new RelayCommand(delegate { ExecuteDeleteInfrafamily(SearchInfrafamilyName); });

        private RelayCommand _saveInfrafamilyCommand;
        public ICommand SaveInfrafamilyCommand => _saveInfrafamilyCommand ??= new RelayCommand(delegate { ExecuteSaveInfrafamily(SearchInfrafamilyName); });

        #endregion [Commands Infrafamily]       


        #region [Methods Infrafamily]

        private void ExecuteGetInfrafamiliesByNameOrId(string searchName)
        {
            if (Tbl48SubfamiliesAllList == null)
                Tbl48SubfamiliesAllList ??= new ObservableCollection<Tbl48Subfamily>();
            else
                Tbl48SubfamiliesAllList.Clear();

            Tbl48SubfamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl48Subfamily>("Subfamily");

            if (Tbl51InfrafamiliesList == null)
                Tbl51InfrafamiliesList ??= new ObservableCollection<Tbl51Infrafamily>();
            else
                Tbl51InfrafamiliesList.Clear();

            Tbl51InfrafamiliesList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl51Infrafamily>(searchName, "Infrafamily");

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl51InfrafamiliesList.Count)) return;

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
            InfrafamiliesView.Refresh();
        }

        private void ExecuteAddInfrafamily(object o)
        {
            if (Tbl51InfrafamiliesList == null)
                Tbl51InfrafamiliesList ??= new ObservableCollection<Tbl51Infrafamily>();

            if (Tbl48SubfamiliesAllList == null)
                Tbl48SubfamiliesAllList ??= new ObservableCollection<Tbl48Subfamily>();
            else
                Tbl48SubfamiliesAllList.Clear();

            Tbl48SubfamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl48Subfamily>("Subfamily");

            Tbl51InfrafamiliesList.Insert(0, new Tbl51Infrafamily { InfrafamilyName = CultRes.StringsRes.DatasetNew });

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
            InfrafamiliesView.MoveCurrentToFirst();
        }

        private void ExecuteCopyInfrafamily(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl51Infrafamily)) return;

            Tbl51InfrafamiliesList = _extCrud.CopyInfrafamily(CurrentTbl51Infrafamily);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
            InfrafamiliesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteInfrafamily(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl51Infrafamily)) return;

            _extDelete.DeleteInfrafamily(CurrentTbl51Infrafamily);

            Tbl51InfrafamiliesList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl51Infrafamily>(searchName, "Infrafamily");
            InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
            InfrafamiliesView.MoveCurrentToLast();
        }

        private void ExecuteSaveInfrafamily(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl51Infrafamily)) return;

            _position = InfrafamiliesView.CurrentPosition;

            var ret = _extSave.SaveInfrafamily(CurrentTbl51Infrafamily);

            if (ret != true)
            {
                InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
                InfrafamiliesView.Refresh();
                return;
            }

            if (CurrentTbl51Infrafamily.InfrafamilyId == 0) //new
            {
                Tbl51InfrafamiliesList = _extCrud.GetLastInfrafamiliesDatasetOrderById();
                InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
                InfrafamiliesView.MoveCurrentToFirst();
            }
            else
            {
                Tbl51InfrafamiliesList = _extCrud.GetInfrafamiliesCollectionFromSearchNameOrIdOrderBy<Tbl51Infrafamily>(searchName);
                InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
                InfrafamiliesView.MoveCurrentToPosition(_position);
            }
        }
        #endregion [Methods Infrafamily]                



        //    Part 2    


        #region "Public Commands Connect <== Tbl48Subfamily"                 


        private RelayCommand _saveSubfamilyCommand;

        public ICommand SaveSubfamilyCommand => _saveSubfamilyCommand ??= new RelayCommand(delegate { ExecuteSaveSubfamily(null); });

        private void ExecuteSaveSubfamily(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl48Subfamily)) return;

            _extSave.SaveSubfamily(CurrentTbl48Subfamily);

            Tbl48SubfamiliesList = _extCrud.GetSubfamiliesCollectionFromSubfamilyIdOrderBy<Tbl48Subfamily>(CurrentTbl51Infrafamily.SubfamilyId);
            SubfamiliesView = CollectionViewSource.GetDefaultView(Tbl48SubfamiliesList);
            SubfamiliesView.Refresh();
        }

        #endregion "Public Commands"                  


        //    Part 3    





        //    Part 4    


        #region [Public Commands Connect ==> Tbl54Supertribus]                 

        private RelayCommand _addSupertribusCommand;
        public ICommand AddSupertribusCommand => _addSupertribusCommand ??= new RelayCommand(delegate { ExecuteAddSupertribus(null); });

        private RelayCommand _copySupertribusCommand;
        public ICommand CopySupertribusCommand => _copySupertribusCommand ??= new RelayCommand(delegate { ExecuteCopySupertribus(null); });

        private RelayCommand _deleteSupertribusCommand;
        public ICommand DeleteSupertribusCommand => _deleteSupertribusCommand ??= new RelayCommand(delegate { ExecuteDeleteSupertribus(null); });

        private RelayCommand _saveSupertribusCommand;
        public ICommand SaveSupertribusCommand => _saveSupertribusCommand ??= new RelayCommand(delegate { ExecuteSaveSupertribus(null); });

        #endregion [Public Commands Connect ==> Tbl54Supertribus]    

        #region [Public Methods Connect ==> Tbl54Supertribus]                   

        private void ExecuteAddSupertribus(object o)
        {
            if (Tbl51InfrafamiliesAllList == null)
                Tbl51InfrafamiliesAllList ??= new ObservableCollection<Tbl51Infrafamily>();
            else
                Tbl51InfrafamiliesAllList.Clear();

            Tbl51InfrafamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl51Infrafamily>("Infrafamily");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            Tbl54SupertribussesList ??= new ObservableCollection<Tbl54Supertribus>();

            Tbl54SupertribussesList.Insert(0, new Tbl54Supertribus { SupertribusName = CultRes.StringsRes.DatasetNew });

            SupertribussesView = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
            SupertribussesView.MoveCurrentToFirst();
        }

        private void ExecuteCopySupertribus(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl54Supertribus)) return;

            Tbl54SupertribussesList = _extCrud.CopySupertribus(CurrentTbl54Supertribus);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            SupertribussesView = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
            SupertribussesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteSupertribus(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl54Supertribus)) return;

            _extDelete.DeleteSupertribus(CurrentTbl54Supertribus);

            Tbl54SupertribussesList = _extCrud.GetSupertribussesCollectionFromInfrafamilyIdOrderBy<Tbl54Supertribus>(CurrentTbl54Supertribus.SupertribusId);
            SupertribussesView = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
            SupertribussesView.MoveCurrentToFirst();
        }

        private void ExecuteSaveSupertribus(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl54Supertribus)) return;

            CurrentTbl54Supertribus.InfrafamilyId = CurrentTbl51Infrafamily.InfrafamilyId;

            _extSave.SaveSupertribus(CurrentTbl54Supertribus);
            Tbl54SupertribussesList = _extCrud.GetSupertribussesCollectionFromInfrafamilyIdOrderBy<Tbl54Supertribus>(CurrentTbl54Supertribus.InfrafamilyId);

            SupertribussesView = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
            SupertribussesView.MoveCurrentToFirst();
        }

        #endregion [Public Methods  Connect ==> Tbl54Supertribus]                                                                                                                                            



        //    Part 5    



        //    Part 6    




        //    Part 7    



        //    Part 8    


        #region [Commands Infrafamily ==> Tbl90Reference Author]

        private RelayCommand _addReferenceAuthorCommand;

        public ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteAddReferenceAuthor(null); });

        private RelayCommand _copyReferenceAuthorCommand;

        public ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceAuthor(null); });

        private RelayCommand _deleteReferenceAuthorCommand;

        public ICommand DeleteReferenceAuthorCommand => _deleteReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceAuthor(null); });

        private RelayCommand _saveReferenceAuthorCommand;

        public ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceAuthor(null); });

        #endregion [Commands Infrafamily ==> Tbl90Reference Author]                

        #region [Methods Infrafamily ==> Tbl90Reference Author]

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

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceInfrafamily(CurrentTbl90ReferenceAuthor, "Author");

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceAuthor(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            _extDelete.DeleteReferenceAuthor(CurrentTbl90ReferenceAuthor);

            Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromInfrafamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90ReferenceAuthor.InfrafamilyId);
            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }

        public void ExecuteSaveReferenceAuthor(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            CurrentTbl90ReferenceAuthor.InfrafamilyId = CurrentTbl51Infrafamily.InfrafamilyId;

            _extSave.SaveReferenceAuthor(CurrentTbl90ReferenceAuthor, "Infrafamily");

            Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromInfrafamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl51Infrafamily.InfrafamilyId);

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }
        #endregion [Methods Infrafamily ==> Tbl90Reference Author]              

        #region [Commands Infrafamily ==> Tbl90Reference Source]      

        private RelayCommand _addReferenceSourceCommand;

        public ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteAddReferenceSource(null); });

        private RelayCommand _copyReferenceSourceCommand;

        public ICommand CopyReferenceSourceCommand => _copyReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceSource(null); });

        private RelayCommand _deleteReferenceSourceCommand;

        public ICommand DeleteReferenceSourceCommand => _deleteReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceSource(null); });

        private RelayCommand _saveReferenceSourceCommand;

        public ICommand SaveReferenceSourceCommand => _saveReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceSource(null); });


        #endregion [Commands Infrafamily ==> Tbl90Reference Source]         

        #region [Methods Infrafamily ==> Tbl90Reference Source]      

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

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceInfrafamily(CurrentTbl90ReferenceSource, "Source");

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceSource(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            _extDelete.DeleteReferenceSource(CurrentTbl90ReferenceSource);

            Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromInfrafamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl51Infrafamily.InfrafamilyId);
            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        public void ExecuteSaveReferenceSource(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            CurrentTbl90ReferenceSource.InfrafamilyId = CurrentTbl51Infrafamily.InfrafamilyId;

            _extSave.SaveReferenceSource(CurrentTbl90ReferenceSource, "Infrafamily");

            Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromInfrafamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl51Infrafamily.InfrafamilyId);


            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }
        #endregion [Methods Infrafamily ==> Tbl90Reference Source]                    

        #region [Commands Infrafamily ==> Tbl90Reference Expert]                 

        private RelayCommand _addReferenceExpertCommand;

        public ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteAddReferenceExpert(null); });

        private RelayCommand _copyReferenceExpertCommand;

        public ICommand CopyReferenceExpertCommand => _copyReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceExpert(null); });

        private RelayCommand _deleteReferenceExpertCommand;

        public ICommand DeleteReferenceExpertCommand => _deleteReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceExpert(null); });
        private RelayCommand _saveReferenceExpertCommand;

        public ICommand SaveReferenceExpertCommand => _saveReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceExpert(null); });

        #endregion [Commands Infrafamily ==> Tbl90Reference Expert]                    


        #region [Methods Infrafamily ==> Tbl90Reference Expert]                 

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

            Tbl90ReferenceExpertsList = _extCrud.CopyReferenceInfrafamily(CurrentTbl90ReferenceExpert, "Expert");

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceExpert(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            _extDelete.DeleteReferenceExpert(CurrentTbl90ReferenceExpert);

            Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromInfrafamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl51Infrafamily.InfrafamilyId);
            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }

        public void ExecuteSaveReferenceExpert(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            CurrentTbl90ReferenceExpert.InfrafamilyId = CurrentTbl51Infrafamily.InfrafamilyId;

            _extSave.SaveReferenceExpert(CurrentTbl90ReferenceExpert, "Infrafamily");

            Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromInfrafamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl51Infrafamily.InfrafamilyId);


            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }
        #endregion [Methods Infrafamily ==> Tbl90Reference Expert]                               

        #region [Commands Infrafamily ==> Tbl93Comments]        

        private RelayCommand _addCommentCommand;

        public ICommand AddCommentCommand => _addCommentCommand ??= new RelayCommand(delegate { ExecuteAddComment(null); });

        private RelayCommand _copyCommentCommand;

        public ICommand CopyCommentCommand => _copyCommentCommand ??= new RelayCommand(delegate { ExecuteCopyComment(null); });

        private RelayCommand _deleteCommentCommand;

        public ICommand DeleteCommentCommand => _deleteCommentCommand ??= new RelayCommand(delegate { ExecuteDeleteComment(null); });

        private RelayCommand _saveCommentCommand;

        public ICommand SaveCommentCommand => _saveCommentCommand ??= new RelayCommand(delegate { ExecuteSaveComment(null); });

        #endregion [Commands Infrafamily ==> Tbl93Comments]        



        #region [Methods Infrafamily ==> Tbl93Comments]        

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

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromInfrafamilyIdOrderBy<Tbl93Comment>(CurrentTbl51Infrafamily.InfrafamilyId);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }

        private void ExecuteSaveComment(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            CurrentTbl93Comment.InfrafamilyId = CurrentTbl51Infrafamily.InfrafamilyId;

            _extSave.SaveComment(CurrentTbl93Comment, "Infrafamily");

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromInfrafamilyIdOrderBy<Tbl93Comment>(CurrentTbl51Infrafamily.InfrafamilyId);


            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        #endregion [Methods Infrafamily ==> Tbl93Comments]                 


        //    Part 9    



        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        #endregion "Public Commands Connected Tables by DoubleClick"

        #region "Public Method Connected Tables by DoubleClick"

        private void GetConnectedTablesById(object o)
        {
            Tbl48SubfamiliesList = _extCrud.GetSubfamiliesCollectionFromSubfamilyIdOrderBy<Tbl48Subfamily>(CurrentTbl51Infrafamily.SubfamilyId);

            Tbl45FamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl45Family>("Family");

            SubfamiliesView = CollectionViewSource.GetDefaultView(Tbl48SubfamiliesList);
            SubfamiliesView.Refresh();

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
                    if (CurrentTbl51Infrafamily != null)
                    {
                        Tbl48SubfamiliesList = _extCrud.GetSubfamiliesCollectionFromSubfamilyIdOrderBy<Tbl48Subfamily>(CurrentTbl51Infrafamily.SubfamilyId);

                        Tbl45FamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl45Family>("Family");

                        SubfamiliesView = CollectionViewSource.GetDefaultView(Tbl48SubfamiliesList);
                        SubfamiliesView.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }

                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl51Infrafamily != null)
                    {
                        Tbl54SupertribussesList = _extCrud.GetSupertribussesCollectionFromInfrafamilyIdOrderBy<Tbl54Supertribus>(CurrentTbl51Infrafamily.InfrafamilyId);

                        Tbl51InfrafamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl51Infrafamily>("Infrafamily");

                        SupertribussesView = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
                        SupertribussesView.Refresh();
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
                    if (CurrentTbl51Infrafamily != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromInfrafamilyIdOrderBy<Tbl93Comment>(CurrentTbl51Infrafamily.InfrafamilyId);

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
                    if (CurrentTbl51Infrafamily != null)
                    {
                        Tbl48SubfamiliesList = _extCrud.GetSubfamiliesCollectionFromSubfamilyIdOrderBy<Tbl48Subfamily>(CurrentTbl51Infrafamily.SubfamilyId);

                        SubfamiliesView = CollectionViewSource.GetDefaultView(Tbl48SubfamiliesList);
                        SubfamiliesView.Refresh();
                    }
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 1)
                {
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 2)
                {
                    if (CurrentTbl51Infrafamily != null)
                    {
                        Tbl54SupertribussesList = _extCrud.GetSupertribussesCollectionFromInfrafamilyIdOrderBy<Tbl54Supertribus>(CurrentTbl51Infrafamily.InfrafamilyId);

                        Tbl51InfrafamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl51Infrafamily>("Infrafamily");

                        SupertribussesView = CollectionViewSource.GetDefaultView(Tbl54SupertribussesList);
                        SupertribussesView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTbl51Infrafamily != null)
                    {
                        Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromInfrafamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl51Infrafamily.InfrafamilyId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 4)
                {
                    if (CurrentTbl51Infrafamily != null)
                    {
                        Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromInfrafamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl51Infrafamily.InfrafamilyId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 5)
                {
                    if (CurrentTbl51Infrafamily != null)
                    {
                        Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("Author");

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromInfrafamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl51Infrafamily.InfrafamilyId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 2;
                }

                if (_selectedDetailTabIndex == 6)
                {
                    if (CurrentTbl51Infrafamily != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromInfrafamilyIdOrderBy<Tbl93Comment>(CurrentTbl51Infrafamily.InfrafamilyId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                }

                if (_selectedDetailTabIndex == 7)
                {
                    if (CurrentTbl51Infrafamily != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromInfrafamilyIdOrderBy<Tbl93Comment>(CurrentTbl51Infrafamily.InfrafamilyId);

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
                    if (CurrentTbl51Infrafamily != null)
                    {
                        Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromInfrafamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl51Infrafamily.InfrafamilyId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                }

                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (CurrentTbl51Infrafamily != null)
                    {
                        Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromInfrafamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl51Infrafamily.InfrafamilyId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 2;
                }

                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (CurrentTbl51Infrafamily != null)
                    {
                        Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("Author");

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromInfrafamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl51Infrafamily.InfrafamilyId);

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


        #region "Public Properties Tbl51Infrafamily"

        private string _searchInfrafamilyName = "";
        public string SearchInfrafamilyName
        {
            get => _searchInfrafamilyName;
            set { _searchInfrafamilyName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView InfrafamiliesView;
        private Tbl51Infrafamily CurrentTbl51Infrafamily => InfrafamiliesView?.CurrentItem as Tbl51Infrafamily;

        private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesList;
        public ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesList
        {
            get => _tbl51InfrafamiliesList;
            set { _tbl51InfrafamiliesList = value; RaisePropertyChanged(""); }
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

        private ObservableCollection<Tbl48Subfamily> _tbl48SubfamiliesAllList;
        public ObservableCollection<Tbl48Subfamily> Tbl48SubfamiliesAllList
        {
            get => _tbl48SubfamiliesAllList;
            set { _tbl48SubfamiliesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   

        #region "Public Properties Tbl54Supertribus"

        public ICollectionView SupertribussesView;
        private Tbl54Supertribus CurrentTbl54Supertribus => SupertribussesView?.CurrentItem as Tbl54Supertribus;

        private ObservableCollection<Tbl54Supertribus> _tbl54SupertribussesList;
        public ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesList
        {
            get => _tbl54SupertribussesList;
            set { _tbl54SupertribussesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl57Tribus"

        public ICollectionView TribussesView;
        private Tbl57Tribus CurrentTbl57Tribus => TribussesView?.CurrentItem as Tbl57Tribus;

        private ObservableCollection<Tbl57Tribus> _tbl57TribussesList;
        public ObservableCollection<Tbl57Tribus> Tbl57TribussesList
        {
            get => _tbl57TribussesList;
            set { _tbl57TribussesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl45Family"

        private ObservableCollection<Tbl45Family> _tbl45FamiliesAllList;
        public ObservableCollection<Tbl45Family> Tbl45FamiliesAllList
        {
            get => _tbl45FamiliesAllList;
            set { _tbl45FamiliesAllList = value; RaisePropertyChanged(""); }
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
