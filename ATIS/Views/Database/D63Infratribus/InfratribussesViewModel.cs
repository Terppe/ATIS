using System;
using System.Collections.ObjectModel;
using System.ComponentModel;


using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using log4net;
using Microsoft.EntityFrameworkCore;

//    InfratribussesViewModel Skriptdatum:  13.12.2020  10:32    

namespace ATIS.Ui.Views.Database.D63Infratribus
{

    public class InfratribussesViewModel : ViewModelBase
    {
        // Version with Generic Unit Of Work and AtisDbContext for general use   

        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(typeof(InfratribussesViewModel));
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly CrudFunctions _extCrud = new CrudFunctions();

        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private readonly GenericMessageBoxes<Tbl63Infratribus> _genInfratribusMessageBoxes = new GenericMessageBoxes<Tbl63Infratribus>();
        private readonly GenericMessageBoxes<Tbl60Subtribus> _genSubtribusMessageBoxes = new GenericMessageBoxes<Tbl60Subtribus>();
        private readonly GenericMessageBoxes<Tbl66Genus> _genGenusMessageBoxes = new GenericMessageBoxes<Tbl66Genus>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genExpertMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genSourceMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genAuthorMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl93Comment> _genCommentMessageBoxes = new GenericMessageBoxes<Tbl93Comment>();
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
            Tbl60SubtribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl60Subtribus>("subtribus");
            Tbl63InfratribussesList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl63Infratribus>(SearchInfratribusName, "infratribus");

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl63InfratribussesList.Count)) return;

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
            InfratribussesView.Refresh();
        }

        private void ExecuteAddInfratribus(object o)
        {
            Tbl60SubtribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl60Subtribus>("subtribus");

            Tbl63InfratribussesList = new ObservableCollection<Tbl63Infratribus>();
            Tbl63InfratribussesList.Insert(0, new Tbl63Infratribus { InfratribusName = CultRes.StringsRes.DatasetNew });

            InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
            InfratribussesView.MoveCurrentToFirst();
        }

        private void ExecuteCopyInfratribus(object o)
        {
            if (_genInfratribusMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl63Infratribus)) return;

            Tbl63InfratribussesList = _extCrud.CopyInfratribus(CurrentTbl63Infratribus);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
            InfratribussesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteInfratribus(string searchName)
        {
            if (_genInfratribusMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl63Infratribus)) return;


            //check if in Tbl66Genusses connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return

            Tbl66GenussesList = _extCrud.SearchForConnectedDatasetsWithInfratribusIdInTableGenus(CurrentTbl63Infratribus);

            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl66GenussesList.Count, "Genus")) return;

            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithInfratribusIdInTableReference(CurrentTbl63Infratribus);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extCrud.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithInfratribusIdInTableComment(CurrentTbl63Infratribus);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extCrud.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }
            try
            {
                var infratribus = _uow.Tbl63Infratribusses.GetById(CurrentTbl63Infratribus.InfratribusId);
                if (infratribus != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl63Infratribus.InfratribusName)) return;

                    _extCrud.DeleteInfratribus(infratribus);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl63Infratribus.InfratribusName);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl63Infratribus.InfratribusName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGetInfratribussesByNameOrId(searchName);

            InfratribussesView.MoveCurrentToFirst();
        }

        private void ExecuteSaveInfratribus(string searchName)
        {
            if (_genInfratribusMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl63Infratribus)) return;

            //Combobox select SubtribusID  may be not 0
            if (CurrentTbl63Infratribus.SubtribusId == 0)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                       MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                var infratribus = _uow.Tbl63Infratribusses.GetById(CurrentTbl63Infratribus.InfratribusId);
                //   var phylum = _context.Tbl63Infratribusses.AsNoTracking().FirstOrDefault(a=>a.InfratribusId == CurrentTbl63Infratribus.InfratribusId);
                //          _context.Entry(infratribus).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl63Infratribus.InfratribusName))
                    return;

                if (CurrentTbl63Infratribus.InfratribusId == 0)
                    infratribus = _extCrud.InfratribusAdd(CurrentTbl63Infratribus);
                else
                    infratribus = _extCrud.InfratribusUpdate(infratribus, CurrentTbl63Infratribus);

                _position = InfratribussesView.CurrentPosition;

                try
                {
                    _extCrud.InfratribusSave(infratribus, CurrentTbl63Infratribus);
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

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, CurrentTbl63Infratribus.InfratribusId == 0
                    ? "DatasetNew"
                    : CurrentTbl63Infratribus.InfratribusName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            ExecuteGetInfratribussesByNameOrId(searchName);
            InfratribussesView.MoveCurrentToPosition(_position);
        }
        #endregion [Methods Infratribus]                



        //    Part 2    


        #region "Public Commands Connect <== Tbl60Subtribus"                 


        private RelayCommand _saveSubtribusCommand;

        public ICommand SaveSubtribusCommand => _saveSubtribusCommand ??= new RelayCommand(delegate { ExecuteSaveSubtribus(null); });

        private void ExecuteSaveSubtribus(string searchName)
        {
            if (_genSubtribusMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl60Subtribus)) return;

            try
            {
                var subtribus = _uow.Tbl60Subtribusses.GetById(CurrentTbl60Subtribus.SubtribusId);

                if (CurrentTbl60Subtribus.SubtribusId == 0)
                    subtribus = _extCrud.SubtribusAdd(CurrentTbl60Subtribus);
                else
                    subtribus = _extCrud.SubtribusUpdate(subtribus, CurrentTbl60Subtribus);

                _position = InfratribussesView.CurrentPosition;

                var cap = CurrentTbl60Subtribus.SubtribusName;
                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(cap)) return;

                try
                {
                    _extCrud.SubtribusSave(subtribus, CurrentTbl60Subtribus);
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

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl60Subtribus.SubtribusId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl60Subtribus.SubtribusName);
            }

            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            ExecuteGetInfratribussesByNameOrId(searchName);
            InfratribussesView.MoveCurrentToPosition(_position);
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
        public ICommand DeleteGenusCommand => _deleteGenusCommand ??= new RelayCommand(delegate { ExecuteDeleteGenus(SearchInfratribusName); });

        private RelayCommand _saveGenusCommand;
        public ICommand SaveGenusCommand => _saveGenusCommand ??= new RelayCommand(delegate { ExecuteSaveGenus(SearchInfratribusName); });

        #endregion [Public Commands Connect ==> Tbl66Genus]    

        #region [Public Methods Connect ==> Tbl66Genus]                   

        private void ExecuteAddGenus(object o)
        {
            Tbl66GenussesList.Insert(0, new Tbl66Genus { GenusName = CultRes.StringsRes.DatasetNew });
            Tbl63InfratribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl63Infratribus>("infratribus");

            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.MoveCurrentToFirst();
        }

        private void ExecuteCopyGenus(object o)
        {
            if (_genGenusMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl66Genus)) return;

            Tbl66GenussesList = _extCrud.CopyGenus(CurrentTbl66Genus);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteGenus(string searchName)
        {
            if (_genGenusMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl66Genus)) return;

            //check if in Tbl69FiSpeciesses connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return
            Tbl69FiSpeciessesList = _extCrud.SearchForConnectedDatasetsWithGenusIdInTableFiSpecies(CurrentTbl66Genus);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl69FiSpeciessesList.Count, "FiSpecies")) return;

            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithGenusIdInTableReference(CurrentTbl66Genus);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extCrud.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithGenusIdInTableComment(CurrentTbl66Genus);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extCrud.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }

            try
            {
                var genus = _uow.Tbl66Genusses.GetById(CurrentTbl66Genus.GenusId);
                if (genus != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl66Genus.GenusName)) return;

                    _extCrud.DeleteGenus(genus);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl66Genus.GenusName);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl66Genus.GenusName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl66GenussesList = _extCrud.GetGenussesCollectionFromInfratribusIdOrderBy<Tbl66Genus>(CurrentTbl66Genus.InfratribusId);

            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.MoveCurrentToFirst();
        }

        private void ExecuteSaveGenus(string searchName)
        {
            if (_genGenusMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl66Genus)) return;

            CurrentTbl66Genus.InfratribusId = CurrentTbl63Infratribus.InfratribusId;

            try
            {
                var genus = _uow.Tbl66Genusses.GetById(CurrentTbl66Genus.GenusId);

                if (CurrentTbl66Genus.GenusId == 0)
                    genus = _extCrud.GenusAdd(CurrentTbl66Genus);
                else
                    genus = _extCrud.GenusUpdate(genus, CurrentTbl66Genus);

                //  _position = GenussesView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl66Genus.GenusName)) return;

                try
                {
                    _extCrud.GenusSave(genus, CurrentTbl66Genus);
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

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl66Genus.GenusId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl66Genus.GenusName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

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

            Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("author");
            Tbl90ReferenceAuthorsList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }

        public void ExecuteCopyReferenceAuthor(object o)
        {
            if (_genAuthorMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceInfratribus(CurrentTbl90ReferenceAuthor, "Author");

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

            CurrentTbl90ReferenceAuthor.InfratribusId = CurrentTbl63Infratribus.InfratribusId;

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
                    reference = _extCrud.ReferenceAuthorInfratribusAdd(CurrentTbl90ReferenceAuthor);

                else
                    reference = _extCrud.ReferenceAuthorInfratribusUpdate(reference, CurrentTbl90ReferenceAuthor);

                //    _position = InfratribussesView.CurrentPosition;

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

            Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("source");

            Tbl90ReferenceSourcesList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        public void ExecuteCopyReferenceSource(object o)
        {
            if (_genSourceMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceInfratribus(CurrentTbl90ReferenceSource, "Source");

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

            Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromInfratribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl63Infratribus.InfratribusId);

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

            CurrentTbl90ReferenceSource.InfratribusId = CurrentTbl63Infratribus.InfratribusId;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceSource.ReferenceId);


                if (CurrentTbl90ReferenceSource.ReferenceId == 0)
                    reference = _extCrud.ReferenceSourceInfratribusAdd(CurrentTbl90ReferenceSource);
                else
                    reference = _extCrud.ReferenceSourceInfratribusUpdate(reference, CurrentTbl90ReferenceSource);

                //        _position = InfratribussesView.CurrentPosition;

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

            Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("expert");
            Tbl90ReferenceExpertsList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }

        public void ExecuteCopyReferenceExpert(object o)
        {
            if (_genExpertMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            Tbl90ReferenceExpertsList = _extCrud.CopyReferenceInfratribus(CurrentTbl90ReferenceExpert, "Expert");

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

            Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromInfratribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl63Infratribus.InfratribusId);

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

            CurrentTbl90ReferenceExpert.InfratribusId = CurrentTbl63Infratribus.InfratribusId;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceExpert.ReferenceId);


                if (CurrentTbl90ReferenceExpert.ReferenceId == 0)
                    reference = _extCrud.ReferenceExpertInfratribusAdd(CurrentTbl90ReferenceExpert);
                else
                    reference = _extCrud.ReferenceExpertInfratribusUpdate(reference, CurrentTbl90ReferenceExpert);

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

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromInfratribusIdOrderBy<Tbl93Comment>(CurrentTbl93Comment.InfratribusId);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }

        private void ExecuteSaveComment(object o)
        {
            if (_genCommentMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            CurrentTbl93Comment.InfratribusId = CurrentTbl63Infratribus.InfratribusId;

            try
            {
                var comment = _uow.Tbl93Comments.GetById(CurrentTbl93Comment.CommentId);


                if (CurrentTbl93Comment.CommentId == 0)
                    comment = _extCrud.CommentInfratribusAdd(CurrentTbl93Comment);
                else
                    comment = _extCrud.CommentInfratribusUpdate(comment, CurrentTbl93Comment);

                //        _position = InfratribussesView.CurrentPosition;

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

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromInfratribusIdOrderBy<Tbl93Comment>(CurrentTbl93Comment.InfratribusId);


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

            Tbl57TribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl57Tribus>("");

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

                        Tbl57TribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl57Tribus>("");

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

                        Tbl63InfratribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl63Infratribus>("infratribus");

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

                        Tbl63InfratribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl63Infratribus>("infratribus");

                        GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
                        GenussesView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTbl63Infratribus != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

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
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

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
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

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
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

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
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

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
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

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
