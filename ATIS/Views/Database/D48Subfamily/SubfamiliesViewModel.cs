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

//    SubfamiliesViewModel Skriptdatum:  10.12.2020  10:32    

namespace ATIS.Ui.Views.Database.D48Subfamily
{

    public class SubfamiliesViewModel : ViewModelBase
    {
        // Version with Generic Unit Of Work and AtisDbContext for general use   

        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(typeof(SubfamiliesViewModel));
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly CrudFunctions _extCrud = new CrudFunctions();

        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private readonly GenericMessageBoxes<Tbl48Subfamily> _genSubfamilyMessageBoxes = new GenericMessageBoxes<Tbl48Subfamily>();
        private readonly GenericMessageBoxes<Tbl45Family> _genFamilyMessageBoxes = new GenericMessageBoxes<Tbl45Family>();
        private readonly GenericMessageBoxes<Tbl51Infrafamily> _genInfrafamilyMessageBoxes = new GenericMessageBoxes<Tbl51Infrafamily>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genExpertMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genSourceMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genAuthorMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl93Comment> _genCommentMessageBoxes = new GenericMessageBoxes<Tbl93Comment>();
        private int _position;

        #endregion [Private Data Members]               

        #region [Constructor]

        public SubfamiliesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                // Code runs "for real" 
                Tbl48SubfamiliesList = new ObservableCollection<Tbl48Subfamily>();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          


        //    Part 1    



        #region [Commands Subfamily]

        private RelayCommand _getSubfamiliesByNameOrIdCommand;
        public ICommand GetSubfamiliesByNameOrIdCommand => _getSubfamiliesByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetSubfamiliesByNameOrId(SearchSubfamilyName); });

        private RelayCommand _addSubfamilyCommand;
        public ICommand AddSubfamilyCommand => _addSubfamilyCommand ??= new RelayCommand(delegate { ExecuteAddSubfamily(null); });

        private RelayCommand _copySubfamilyCommand;
        public ICommand CopySubfamilyCommand => _copySubfamilyCommand ??= new RelayCommand(delegate { ExecuteCopySubfamily(null); });

        private RelayCommand _deleteSubfamilyCommand;
        public ICommand DeleteSubfamilyCommand => _deleteSubfamilyCommand ??= new RelayCommand(delegate { ExecuteDeleteSubfamily(SearchSubfamilyName); });

        private RelayCommand _saveSubfamilyCommand;
        public ICommand SaveSubfamilyCommand => _saveSubfamilyCommand ??= new RelayCommand(delegate { ExecuteSaveSubfamily(SearchSubfamilyName); });

        #endregion [Commands Subfamily]       


        #region [Methods Subfamily]

        private void ExecuteGetSubfamiliesByNameOrId(string searchName)
        {
            Tbl45FamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl45Family>("family");
            Tbl48SubfamiliesList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl48Subfamily>(SearchSubfamilyName, "subfamily");

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl48SubfamiliesList.Count)) return;

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            SubfamiliesView = CollectionViewSource.GetDefaultView(Tbl48SubfamiliesList);
            SubfamiliesView.Refresh();
        }

        private void ExecuteAddSubfamily(object o)
        {
            Tbl45FamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl45Family>("family");

            Tbl48SubfamiliesList = new ObservableCollection<Tbl48Subfamily>();
            Tbl48SubfamiliesList.Insert(0, new Tbl48Subfamily { SubfamilyName = CultRes.StringsRes.DatasetNew });

            SubfamiliesView = CollectionViewSource.GetDefaultView(Tbl48SubfamiliesList);
            SubfamiliesView.MoveCurrentToFirst();
        }

        private void ExecuteCopySubfamily(object o)
        {
            if (_genSubfamilyMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl48Subfamily)) return;

            Tbl48SubfamiliesList = _extCrud.CopySubfamily(CurrentTbl48Subfamily);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            SubfamiliesView = CollectionViewSource.GetDefaultView(Tbl48SubfamiliesList);
            SubfamiliesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteSubfamily(string searchName)
        {
            if (_genSubfamilyMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl48Subfamily)) return;


            //check if in Tbl51Infrafamilies connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return

            Tbl51InfrafamiliesList = _extCrud.SearchForConnectedDatasetsWithSubfamilyIdInTableInfrafamily(CurrentTbl48Subfamily);

            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl51InfrafamiliesList.Count, "Infrafamily")) return;

            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithSubfamilyIdInTableReference(CurrentTbl48Subfamily);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extCrud.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithSubfamilyIdInTableComment(CurrentTbl48Subfamily);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extCrud.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }
            try
            {
                var subfamily = _uow.Tbl48Subfamilies.GetById(CurrentTbl48Subfamily.SubfamilyId);
                if (subfamily != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl48Subfamily.SubfamilyName)) return;

                    _extCrud.DeleteSubfamily(subfamily);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl48Subfamily.SubfamilyName);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl48Subfamily.SubfamilyName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGetSubfamiliesByNameOrId(searchName);

            SubfamiliesView.MoveCurrentToFirst();
        }

        private void ExecuteSaveSubfamily(string searchName)
        {
            if (_genSubfamilyMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl48Subfamily)) return;

            //Combobox select FamilyID  may be not 0
            if (CurrentTbl48Subfamily.FamilyId == 0)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                       MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                var subfamily = _uow.Tbl48Subfamilies.GetById(CurrentTbl48Subfamily.SubfamilyId);
                //   var phylum = _context.Tbl48Subfamilies.AsNoTracking().FirstOrDefault(a=>a.SubfamilyId == CurrentTbl48Subfamily.SubfamilyId);
                //          _context.Entry(subfamily).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl48Subfamily.SubfamilyName))
                    return;

                if (CurrentTbl48Subfamily.SubfamilyId == 0)
                    subfamily = _extCrud.SubfamilyAdd(CurrentTbl48Subfamily);
                else
                    subfamily = _extCrud.SubfamilyUpdate(subfamily, CurrentTbl48Subfamily);

                _position = SubfamiliesView.CurrentPosition;

                try
                {
                    _extCrud.SubfamilySave(subfamily, CurrentTbl48Subfamily);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, CurrentTbl48Subfamily.SubfamilyId == 0
                    ? "DatasetNew"
                    : CurrentTbl48Subfamily.SubfamilyName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            ExecuteGetSubfamiliesByNameOrId(searchName);
            SubfamiliesView.MoveCurrentToPosition(_position);
        }
        #endregion [Methods Subfamily]                



        //    Part 2    


        #region "Public Commands Connect <== Tbl45Family"                 


        private RelayCommand _saveFamilyCommand;

        public ICommand SaveFamilyCommand => _saveFamilyCommand ??= new RelayCommand(delegate { ExecuteSaveFamily(null); });

        private void ExecuteSaveFamily(string searchName)
        {
            if (_genFamilyMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl45Family)) return;

            try
            {
                var family = _uow.Tbl45Families.GetById(CurrentTbl45Family.FamilyId);

                if (CurrentTbl45Family.FamilyId == 0)
                    family = _extCrud.FamilyAdd(CurrentTbl45Family);
                else
                    family = _extCrud.FamilyUpdate(family, CurrentTbl45Family);

                _position = SubfamiliesView.CurrentPosition;

                var cap = CurrentTbl45Family.FamilyName;
                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(cap)) return;

                try
                {
                    _extCrud.FamilySave(family, CurrentTbl45Family);
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

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl45Family.FamilyId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl45Family.FamilyName);
            }

            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            ExecuteGetSubfamiliesByNameOrId(searchName);
            SubfamiliesView.MoveCurrentToPosition(_position);
        }

        #endregion "Public Commands"                  


        //    Part 3    





        //    Part 4    


        #region [Public Commands Connect ==> Tbl51Infrafamily]                 

        private RelayCommand _addInfrafamilyCommand;
        public ICommand AddInfrafamilyCommand => _addInfrafamilyCommand ??= new RelayCommand(delegate { ExecuteAddInfrafamily(null); });

        private RelayCommand _copyInfrafamilyCommand;
        public ICommand CopyInfrafamilyCommand => _copyInfrafamilyCommand ??= new RelayCommand(delegate { ExecuteCopyInfrafamily(null); });

        private RelayCommand _deleteInfrafamilyCommand;
        public ICommand DeleteInfrafamilyCommand => _deleteInfrafamilyCommand ??= new RelayCommand(delegate { ExecuteDeleteInfrafamily(SearchSubfamilyName); });

        private RelayCommand _saveInfrafamilyCommand;
        public ICommand SaveInfrafamilyCommand => _saveInfrafamilyCommand ??= new RelayCommand(delegate { ExecuteSaveInfrafamily(SearchSubfamilyName); });

        #endregion [Public Commands Connect ==> Tbl51Infrafamily]    

        #region [Public Methods Connect ==> Tbl51Infrafamily]                   

        private void ExecuteAddInfrafamily(object o)
        {
            Tbl51InfrafamiliesList.Insert(0, new Tbl51Infrafamily { InfrafamilyName = CultRes.StringsRes.DatasetNew });
            Tbl48SubfamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl48Subfamily>("subfamily");

            InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
            InfrafamiliesView.MoveCurrentToFirst();
        }

        private void ExecuteCopyInfrafamily(object o)
        {
            if (_genInfrafamilyMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl51Infrafamily)) return;

            Tbl51InfrafamiliesList = _extCrud.CopyInfrafamily(CurrentTbl51Infrafamily);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
            InfrafamiliesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteInfrafamily(string searchName)
        {
            if (_genInfrafamilyMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl51Infrafamily)) return;

            //check if in Tbl54Supertribusses connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return
            Tbl54SupertribussesList = _extCrud.SearchForConnectedDatasetsWithInfrafamilyIdInTableSupertribus(CurrentTbl51Infrafamily);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl54SupertribussesList.Count, "Supertribus")) return;

            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithInfrafamilyIdInTableReference(CurrentTbl51Infrafamily);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extCrud.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithInfrafamilyIdInTableComment(CurrentTbl51Infrafamily);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extCrud.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }

            try
            {
                var infrafamily = _uow.Tbl51Infrafamilies.GetById(CurrentTbl51Infrafamily.InfrafamilyId);
                if (infrafamily != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl51Infrafamily.InfrafamilyName)) return;

                    _extCrud.DeleteInfrafamily(infrafamily);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl51Infrafamily.InfrafamilyName);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl51Infrafamily.InfrafamilyName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl51InfrafamiliesList = _extCrud.GetInfrafamiliesCollectionFromSubfamilyIdOrderBy<Tbl51Infrafamily>(CurrentTbl51Infrafamily.SubfamilyId);

            InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
            InfrafamiliesView.MoveCurrentToFirst();
        }

        private void ExecuteSaveInfrafamily(string searchName)
        {
            if (_genInfrafamilyMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl51Infrafamily)) return;

            CurrentTbl51Infrafamily.SubfamilyId = CurrentTbl48Subfamily.SubfamilyId;

            try
            {
                var infrafamily = _uow.Tbl51Infrafamilies.GetById(CurrentTbl51Infrafamily.InfrafamilyId);

                if (CurrentTbl51Infrafamily.InfrafamilyId == 0)
                    infrafamily = _extCrud.InfrafamilyAdd(CurrentTbl51Infrafamily);
                else
                    infrafamily = _extCrud.InfrafamilyUpdate(infrafamily, CurrentTbl51Infrafamily);

                //  _position = InfrafamiliesView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl51Infrafamily.InfrafamilyName)) return;

                try
                {
                    _extCrud.InfrafamilySave(infrafamily, CurrentTbl51Infrafamily);
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

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl51Infrafamily.InfrafamilyId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl51Infrafamily.InfrafamilyName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl51InfrafamiliesList = _extCrud.GetInfrafamiliesCollectionFromSubfamilyIdOrderBy<Tbl51Infrafamily>(CurrentTbl51Infrafamily.SubfamilyId);

            InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
            InfrafamiliesView.MoveCurrentToFirst();
        }

        #endregion [Public Methods  Connect ==> Tbl51Infrafamily]                                                                                                                                            



        //    Part 5    



        //    Part 6    




        //    Part 7    



        //    Part 8    


        #region [Commands Subfamily ==> Tbl90Reference Author]

        private RelayCommand _addReferenceAuthorCommand;

        public ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteAddReferenceAuthor(null); });

        private RelayCommand _copyReferenceAuthorCommand;

        public ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceAuthor(null); });

        private RelayCommand _deleteReferenceAuthorCommand;

        public ICommand DeleteReferenceAuthorCommand => _deleteReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceAuthor(null); });

        private RelayCommand _saveReferenceAuthorCommand;

        public ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceAuthor(null); });

        #endregion [Commands Subfamily ==> Tbl90Reference Author]                

        #region [Methods Subfamily ==> Tbl90Reference Author]

        public void ExecuteAddReferenceAuthor(object o)
        {
            Tbl90ReferenceAuthorsList ??= new ObservableCollection<Tbl90Reference>();

            Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("author");
            Tbl90ReferenceAuthorsList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }

        public void ExecuteCopyReferenceAuthor(object o)
        {
            if (_genAuthorMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceSubfamily(CurrentTbl90ReferenceAuthor, "Author");

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceAuthor(string searchName)
        {
            if (_genAuthorMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceAuthor.ReferenceId);
                if (reference != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceAuthor.Info)) return;

                    _extCrud.DeleteReference(reference);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceAuthor.Info);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceAuthor.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }

        public void ExecuteSaveReferenceAuthor(string searchName)
        {
            if (_genAuthorMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            CurrentTbl90ReferenceAuthor.SubfamilyId = CurrentTbl48Subfamily.SubfamilyId;

            //Combobox select RefExpertId or RefSourceId or RefAuthorId may be not null
            if (CurrentTbl90ReferenceAuthor.RefExpertId == null &&
                CurrentTbl90ReferenceAuthor.RefSourceId == null &&
                CurrentTbl90ReferenceAuthor.RefAuthorId == null)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceAuthor.ReferenceId);


                if (CurrentTbl90ReferenceAuthor.ReferenceId == 0)
                    reference = _extCrud.ReferenceAuthorSubfamilyAdd(CurrentTbl90ReferenceAuthor);

                else
                    reference = _extCrud.ReferenceAuthorSubfamilyUpdate(reference, CurrentTbl90ReferenceAuthor);

                //    _position = SubfamiliesView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl90ReferenceAuthor.Info)) return;

                try
                {
                    _extCrud.ReferenceAuthorSave(reference, CurrentTbl90ReferenceAuthor);
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
                    Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl90ReferenceAuthor.ReferenceId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl90ReferenceAuthor.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromSubfamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl48Subfamily.SubfamilyId);


            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }
        #endregion [Methods Subfamily ==> Tbl90Reference Author]              

        #region [Commands Subfamily ==> Tbl90Reference Source]      

        private RelayCommand _addReferenceSourceCommand;

        public ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteAddReferenceSource(null); });

        private RelayCommand _copyReferenceSourceCommand;

        public ICommand CopyReferenceSourceCommand => _copyReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceSource(null); });

        private RelayCommand _deleteReferenceSourceCommand;

        public ICommand DeleteReferenceSourceCommand => _deleteReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceSource(null); });

        private RelayCommand _saveReferenceSourceCommand;

        public ICommand SaveReferenceSourceCommand => _saveReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceSource(null); });


        #endregion [Commands Subfamily ==> Tbl90Reference Source]         

        #region [Methods Subfamily ==> Tbl90Reference Source]      

        public void ExecuteAddReferenceSource(object o)
        {
            Tbl90ReferenceSourcesList ??= new ObservableCollection<Tbl90Reference>();

            Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("source");

            Tbl90ReferenceSourcesList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        public void ExecuteCopyReferenceSource(object o)
        {
            if (_genSourceMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceSubfamily(CurrentTbl90ReferenceSource, "Source");

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceSource(object o)
        {
            if (_genSourceMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceSource.ReferenceId);
                if (reference != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceSource.Info)) return;

                    _extCrud.DeleteReference(reference);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceSource.Info);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceSource.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromSubfamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl48Subfamily.SubfamilyId);

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        public void ExecuteSaveReferenceSource(object o)
        {
            if (_genSourceMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            //RefExpertId or RefSourceId or RefAuthorId may be not 0
            if (CurrentTbl90ReferenceSource.RefExpertId == null &&
                CurrentTbl90ReferenceSource.RefSourceId == null &&
                CurrentTbl90ReferenceSource.RefAuthorId == null)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            CurrentTbl90ReferenceSource.SubfamilyId = CurrentTbl48Subfamily.SubfamilyId;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceSource.ReferenceId);


                if (CurrentTbl90ReferenceSource.ReferenceId == 0)
                    reference = _extCrud.ReferenceSourceSubfamilyAdd(CurrentTbl90ReferenceSource);
                else
                    reference = _extCrud.ReferenceSourceSubfamilyUpdate(reference, CurrentTbl90ReferenceSource);

                //        _position = SubfamiliesView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl90ReferenceSource.Info)) return;

                try
                {
                    _extCrud.ReferenceSourceSave(reference, CurrentTbl90ReferenceSource);

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
                    Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl90ReferenceSource.ReferenceId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl90ReferenceSource.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromSubfamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl48Subfamily.SubfamilyId);



            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }
        #endregion [Methods Subfamily ==> Tbl90Reference Source]                    

        #region [Commands Subfamily ==> Tbl90Reference Expert]                 

        private RelayCommand _addReferenceExpertCommand;

        public ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteAddReferenceExpert(null); });

        private RelayCommand _copyReferenceExpertCommand;

        public ICommand CopyReferenceExpertCommand => _copyReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceExpert(null); });

        private RelayCommand _deleteReferenceExpertCommand;

        public ICommand DeleteReferenceExpertCommand => _deleteReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceExpert(null); });
        private RelayCommand _saveReferenceExpertCommand;

        public ICommand SaveReferenceExpertCommand => _saveReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceExpert(null); });

        #endregion [Commands Subfamily ==> Tbl90Reference Expert]                    


        #region [Methods Subfamily ==> Tbl90Reference Expert]                 

        public void ExecuteAddReferenceExpert(object o)
        {
            Tbl90ReferenceExpertsList ??= new ObservableCollection<Tbl90Reference>();

            Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("expert");
            Tbl90ReferenceExpertsList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }

        public void ExecuteCopyReferenceExpert(object o)
        {
            if (_genExpertMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            Tbl90ReferenceExpertsList = _extCrud.CopyReferenceSubfamily(CurrentTbl90ReferenceExpert, "Expert");

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceExpert(object o)
        {
            if (_genExpertMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceExpert.ReferenceId);
                if (reference != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceExpert.Info)) return;

                    _extCrud.DeleteReference(reference);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceExpert.Info);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceExpert.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromSubfamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl48Subfamily.SubfamilyId);

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }

        public void ExecuteSaveReferenceExpert(object o)
        {
            if (_genExpertMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            //RefExpertId or RefSourceId or RefAuthorId may be not 0
            if (CurrentTbl90ReferenceExpert.RefExpertId == null &&
                CurrentTbl90ReferenceExpert.RefSourceId == null &&
                CurrentTbl90ReferenceExpert.RefAuthorId == null)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            CurrentTbl90ReferenceExpert.SubfamilyId = CurrentTbl48Subfamily.SubfamilyId;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceExpert.ReferenceId);


                if (CurrentTbl90ReferenceExpert.ReferenceId == 0)
                    reference = _extCrud.ReferenceExpertSubfamilyAdd(CurrentTbl90ReferenceExpert);
                else
                    reference = _extCrud.ReferenceExpertSubfamilyUpdate(reference, CurrentTbl90ReferenceExpert);

                //        _position = PhylumsView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl90ReferenceExpert.Info)) return;

                try
                {
                    _extCrud.ReferenceExpertSave(reference, CurrentTbl90ReferenceExpert);
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
                    Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl90ReferenceExpert.ReferenceId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl90ReferenceExpert.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromSubfamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl48Subfamily.SubfamilyId);


            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }
        #endregion [Methods Subfamily ==> Tbl90Reference Expert]                               

        #region [Commands Subfamily ==> Tbl93Comments]        

        private RelayCommand _addCommentCommand;

        public ICommand AddCommentCommand => _addCommentCommand ??= new RelayCommand(delegate { ExecuteAddComment(null); });

        private RelayCommand _copyCommentCommand;

        public ICommand CopyCommentCommand => _copyCommentCommand ??= new RelayCommand(delegate { ExecuteCopyComment(null); });

        private RelayCommand _deleteCommentCommand;

        public ICommand DeleteCommentCommand => _deleteCommentCommand ??= new RelayCommand(delegate { ExecuteDeleteComment(null); });

        private RelayCommand _saveCommentCommand;

        public ICommand SaveCommentCommand => _saveCommentCommand ??= new RelayCommand(delegate { ExecuteSaveComment(null); });

        #endregion [Commands Subfamily ==> Tbl93Comments]        



        #region [Methods Subfamily ==> Tbl93Comments]        

        public void ExecuteAddComment(object o)
        {
            Tbl93CommentsList ??= new ObservableCollection<Tbl93Comment>();

            Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = CultRes.StringsRes.DatasetNew });

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }

        public void ExecuteCopyComment(object o)
        {

            if (_genCommentMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            Tbl93CommentsList = _extCrud.CopyComment(CurrentTbl93Comment, "Comment");

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteComment(object o)
        {
            if (_genCommentMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            try
            {
                var comment = _uow.Tbl93Comments.GetById(CurrentTbl93Comment.CommentId);
                if (comment != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl93Comment.Info)) return;

                    _extCrud.DeleteComment(comment);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl93Comment.Info);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl93Comment.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromSubfamilyIdOrderBy<Tbl93Comment>(CurrentTbl93Comment.SubfamilyId);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }

        private void ExecuteSaveComment(object o)
        {
            if (_genCommentMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            CurrentTbl93Comment.SubfamilyId = CurrentTbl48Subfamily.SubfamilyId;

            try
            {
                var comment = _uow.Tbl93Comments.GetById(CurrentTbl93Comment.CommentId);


                if (CurrentTbl93Comment.CommentId == 0)
                    comment = _extCrud.CommentSubfamilyAdd(CurrentTbl93Comment);
                else
                    comment = _extCrud.CommentSubfamilyUpdate(comment, CurrentTbl93Comment);

                //        _position = SubfamiliesView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl93Comment.Info))
                    return;

                try
                {
                    _extCrud.CommentSave(comment, CurrentTbl93Comment);
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
                    Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl93Comment.CommentId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl93Comment.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromSubfamilyIdOrderBy<Tbl93Comment>(CurrentTbl93Comment.SubfamilyId);


            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        #endregion [Methods Subfamily ==> Tbl93Comments]                 


        //    Part 9    



        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        #endregion "Public Commands Connected Tables by DoubleClick"

        #region "Public Method Connected Tables by DoubleClick"

        private void GetConnectedTablesById(object o)
        {
            Tbl45FamiliesList = _extCrud.GetFamiliesCollectionFromFamilyIdOrderBy<Tbl45Family>(CurrentTbl48Subfamily.FamilyId);

            Tbl42SuperfamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl42Superfamily>("");

            FamiliesView = CollectionViewSource.GetDefaultView(Tbl45FamiliesList);
            FamiliesView.Refresh();

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
                    if (CurrentTbl48Subfamily != null)
                    {
                        Tbl45FamiliesList = _extCrud.GetFamiliesCollectionFromFamilyIdOrderBy<Tbl45Family>(CurrentTbl48Subfamily.FamilyId);

                        Tbl42SuperfamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl42Superfamily>("");

                        FamiliesView = CollectionViewSource.GetDefaultView(Tbl45FamiliesList);
                        FamiliesView.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }

                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl48Subfamily != null)
                    {
                        Tbl51InfrafamiliesList = _extCrud.GetInfrafamiliesCollectionFromSubfamilyIdOrderBy<Tbl51Infrafamily>(CurrentTbl48Subfamily.SubfamilyId);

                        Tbl48SubfamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl48Subfamily>("subfamily");

                        InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
                        InfrafamiliesView.Refresh();
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
                    if (CurrentTbl48Subfamily != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromSubfamilyIdOrderBy<Tbl93Comment>(CurrentTbl48Subfamily.SubfamilyId);

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
                    if (CurrentTbl48Subfamily != null)
                    {
                        Tbl45FamiliesList = _extCrud.GetFamiliesCollectionFromFamilyIdOrderBy<Tbl45Family>(CurrentTbl48Subfamily.FamilyId);

                        FamiliesView = CollectionViewSource.GetDefaultView(Tbl45FamiliesList);
                        FamiliesView.Refresh();
                    }
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 1)
                {
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 2)
                {
                    if (CurrentTbl48Subfamily != null)
                    {
                        Tbl51InfrafamiliesList = _extCrud.GetInfrafamiliesCollectionFromSubfamilyIdOrderBy<Tbl51Infrafamily>(CurrentTbl48Subfamily.SubfamilyId);

                        Tbl48SubfamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl48Subfamily>("subfamily");

                        InfrafamiliesView = CollectionViewSource.GetDefaultView(Tbl51InfrafamiliesList);
                        InfrafamiliesView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTbl48Subfamily != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromSubfamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl48Subfamily.SubfamilyId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 4)
                {
                    if (CurrentTbl48Subfamily != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromSubfamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl48Subfamily.SubfamilyId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 5)
                {
                    if (CurrentTbl48Subfamily != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromSubfamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl48Subfamily.SubfamilyId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 2;
                }

                if (_selectedDetailTabIndex == 6)
                {
                    if (CurrentTbl48Subfamily != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromSubfamilyIdOrderBy<Tbl93Comment>(CurrentTbl48Subfamily.SubfamilyId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                }

                if (_selectedDetailTabIndex == 7)
                {
                    if (CurrentTbl48Subfamily != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromSubfamilyIdOrderBy<Tbl93Comment>(CurrentTbl48Subfamily.SubfamilyId);

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
                    if (CurrentTbl48Subfamily != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromSubfamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl48Subfamily.SubfamilyId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                }

                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (CurrentTbl48Subfamily != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromSubfamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl48Subfamily.SubfamilyId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 2;
                }

                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (CurrentTbl48Subfamily != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromSubfamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl48Subfamily.SubfamilyId);

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


        #region "Public Properties Tbl48Subfamily"

        private string _searchSubfamilyName = "";
        public string SearchSubfamilyName
        {
            get => _searchSubfamilyName;
            set { _searchSubfamilyName = value; RaisePropertyChanged(""); }
        }

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

        private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesAllList;
        public ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesAllList
        {
            get => _tbl51InfrafamiliesAllList;
            set { _tbl51InfrafamiliesAllList = value; RaisePropertyChanged(""); }
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

        private ObservableCollection<Tbl45Family> _tbl45FamiliesAllList;
        public ObservableCollection<Tbl45Family> Tbl45FamiliesAllList
        {
            get => _tbl45FamiliesAllList;
            set { _tbl45FamiliesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   

        #region "Public Properties Tbl51Infrafamily"

        public ICollectionView InfrafamiliesView;
        private Tbl51Infrafamily CurrentTbl51Infrafamily => InfrafamiliesView?.CurrentItem as Tbl51Infrafamily;

        private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesList;
        public ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesList
        {
            get => _tbl51InfrafamiliesList;
            set { _tbl51InfrafamiliesList = value; RaisePropertyChanged(""); }
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

        #region "Public Properties Tbl42Superfamily"

        private ObservableCollection<Tbl42Superfamily> _tbl42SuperfamiliesAllList;
        public ObservableCollection<Tbl42Superfamily> Tbl42SuperfamiliesAllList
        {
            get => _tbl42SuperfamiliesAllList;
            set { _tbl42SuperfamiliesAllList = value; RaisePropertyChanged(""); }
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
