using System;
using System.Collections.ObjectModel;
using System.ComponentModel;


using System.Windows.Data;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using log4net;
using Microsoft.EntityFrameworkCore;

//    OrdosViewModel Skriptdatum:  07.01.2021  10:32    

namespace ATIS.Ui.Views.Database.D33Ordo
{

    public class OrdosViewModel : ViewModelBase
    {
        // Version with Generic Unit Of Work and AtisDbContext for general use   

        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(typeof(OrdosViewModel));
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly CrudFunctions _extCrud = new CrudFunctions();
        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private int _position;

        #endregion [Private Data Members]               

        #region [Constructor]

        public OrdosViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                // Code runs "for real" 
                Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          


        //    Part 1    



        #region [Commands Ordo]

        private RelayCommand _getOrdosByNameOrIdCommand;
        public ICommand GetOrdosByNameOrIdCommand => _getOrdosByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetOrdosByNameOrId(SearchOrdoName); });

        private RelayCommand _addOrdoCommand;
        public ICommand AddOrdoCommand => _addOrdoCommand ??= new RelayCommand(delegate { ExecuteAddOrdo(null); });

        private RelayCommand _copyOrdoCommand;
        public ICommand CopyOrdoCommand => _copyOrdoCommand ??= new RelayCommand(delegate { ExecuteCopyOrdo(null); });

        private RelayCommand _deleteOrdoCommand;
        public ICommand DeleteOrdoCommand => _deleteOrdoCommand ??= new RelayCommand(delegate { ExecuteDeleteOrdo(SearchOrdoName); });

        private RelayCommand _saveOrdoCommand;
        public ICommand SaveOrdoCommand => _saveOrdoCommand ??= new RelayCommand(delegate { ExecuteSaveOrdo(SearchOrdoName); });

        #endregion [Commands Ordo]       


        #region [Methods Ordo]

        private void ExecuteGetOrdosByNameOrId(string searchName)
        {
            Tbl30LegiosAllList = _extCrud.GetCollectionAllOrderBy<Tbl30Legio>("Legio");
            Tbl33OrdosList = _extCrud.GetOrdosCollectionFromSearchNameOrIdOrderBy<Tbl33Ordo>(searchName);

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl33OrdosList.Count)) return;

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            OrdosView = CollectionViewSource.GetDefaultView(Tbl33OrdosList);
            OrdosView.Refresh();
        }

        private void ExecuteAddOrdo(object o)
        {
            Tbl30LegiosAllList = _extCrud.GetCollectionAllOrderBy<Tbl30Legio>("Legio");

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

        private void ExecuteDeleteOrdo(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl33Ordo)) return;


            //check if in Tbl36Subordos connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return

            Tbl36SubordosList = _extCrud.SearchForConnectedDatasetsWithOrdoIdInTableSubordo(CurrentTbl33Ordo.OrdoId);

            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl36SubordosList.Count, "Subordo")) return;

            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithOrdoIdInTableReference(CurrentTbl33Ordo.OrdoId);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extCrud.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithOrdoIdInTableComment(CurrentTbl33Ordo.OrdoId);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extCrud.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }
            try
            {
                var ordo = _uow.Tbl33Ordos.GetById(CurrentTbl33Ordo.OrdoId);
                if (ordo != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl33Ordo.OrdoName)) return;

                    _extCrud.DeleteOrdo(ordo);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl33Ordo.OrdoName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + CurrentTbl33Ordo.OrdoName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGetOrdosByNameOrId(searchName);

            OrdosView.MoveCurrentToFirst();
        }

        private void ExecuteSaveOrdo(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl33Ordo)) return;

            //Combobox select LegioID  may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(CurrentTbl33Ordo.LegioId)) return;

            try
            {
                var ordo = _uow.Tbl33Ordos.GetById(CurrentTbl33Ordo.OrdoId);
                //   var phylum = _context.Tbl33Ordos.AsNoTracking().FirstOrDefault(a=>a.OrdoId == CurrentTbl33Ordo.OrdoId);
                //          _context.Entry(ordo).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl33Ordo.OrdoName))
                    return;

                if (CurrentTbl33Ordo.OrdoId == 0)
                    ordo = _extCrud.OrdoAdd(CurrentTbl33Ordo);
                else
                    ordo = _extCrud.OrdoUpdate(ordo, CurrentTbl33Ordo);

                _position = OrdosView.CurrentPosition;

                try
                {
                    _extCrud.OrdoSave(ordo, CurrentTbl33Ordo);
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

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, CurrentTbl33Ordo.OrdoId == 0
                    ? "DatasetNew"
                    : CurrentTbl33Ordo.OrdoName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            ExecuteGetOrdosByNameOrId(searchName);
            OrdosView.MoveCurrentToPosition(_position);
        }
        #endregion [Methods Ordo]                



        //    Part 2    


        #region "Public Commands Connect <== Tbl30Legio"                 


        private RelayCommand _saveLegioCommand;

        public ICommand SaveLegioCommand => _saveLegioCommand ??= new RelayCommand(delegate { ExecuteSaveLegio(null); });

        private void ExecuteSaveLegio(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl30Legio)) return;

            try
            {
                var legio = _uow.Tbl30Legios.GetById(CurrentTbl30Legio.LegioId);

                if (CurrentTbl30Legio.LegioId == 0)
                    legio = _extCrud.LegioAdd(CurrentTbl30Legio);
                else
                    legio = _extCrud.LegioUpdate(legio, CurrentTbl30Legio);

                _position = OrdosView.CurrentPosition;

                var cap = CurrentTbl30Legio.LegioName;
                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(cap)) return;

                try
                {
                    _extCrud.LegioSave(legio, CurrentTbl30Legio);
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

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl30Legio.LegioId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl30Legio.LegioName);
            }

            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            ExecuteGetOrdosByNameOrId(searchName);
            OrdosView.MoveCurrentToPosition(_position);
        }

        #endregion "Public Commands"                  


        //    Part 3    





        //    Part 4    


        #region [Public Commands Connect ==> Tbl36Subordo]                 

        private RelayCommand _addSubordoCommand;
        public ICommand AddSubordoCommand => _addSubordoCommand ??= new RelayCommand(delegate { ExecuteAddSubordo(null); });

        private RelayCommand _copySubordoCommand;
        public ICommand CopySubordoCommand => _copySubordoCommand ??= new RelayCommand(delegate { ExecuteCopySubordo(null); });

        private RelayCommand _deleteSubordoCommand;
        public ICommand DeleteSubordoCommand => _deleteSubordoCommand ??= new RelayCommand(delegate { ExecuteDeleteSubordo(SearchOrdoName); });

        private RelayCommand _saveSubordoCommand;
        public ICommand SaveSubordoCommand => _saveSubordoCommand ??= new RelayCommand(delegate { ExecuteSaveSubordo(SearchOrdoName); });

        #endregion [Public Commands Connect ==> Tbl36Subordo]    

        #region [Public Methods Connect ==> Tbl36Subordo]                   

        private void ExecuteAddSubordo(object o)
        {
            Tbl36SubordosList ??= new ObservableCollection<Tbl36Subordo>();

            Tbl36SubordosList.Insert(0, new Tbl36Subordo { SubordoName = CultRes.StringsRes.DatasetNew });
            Tbl33OrdosAllList = _extCrud.GetCollectionAllOrderBy<Tbl33Ordo>("Ordo");

            SubordosView = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
            SubordosView.MoveCurrentToFirst();
        }

        private void ExecuteCopySubordo(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl36Subordo)) return;

            Tbl36SubordosList = _extCrud.CopySubordo(CurrentTbl36Subordo);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            SubordosView = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
            SubordosView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteSubordo(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl36Subordo)) return;

            //check if in Tbl39Infraordos connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return
            Tbl39InfraordosList = _extCrud.SearchForConnectedDatasetsWithSubordoIdInTableInfraordo(CurrentTbl36Subordo.SubordoId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl39InfraordosList.Count, "Infraordo")) return;

            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithSubordoIdInTableReference(CurrentTbl36Subordo.SubordoId);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extCrud.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithSubordoIdInTableComment(CurrentTbl36Subordo.SubordoId);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extCrud.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }

            try
            {
                var subordo = _uow.Tbl36Subordos.GetById(CurrentTbl36Subordo.SubordoId);
                if (subordo != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl36Subordo.SubordoName)) return;

                    _extCrud.DeleteSubordo(subordo);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl36Subordo.SubordoName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + CurrentTbl36Subordo.SubordoName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl36SubordosList = _extCrud.GetSubordosCollectionFromOrdoIdOrderBy<Tbl36Subordo>(CurrentTbl36Subordo.OrdoId);

            SubordosView = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
            SubordosView.MoveCurrentToFirst();
        }

        private void ExecuteSaveSubordo(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl36Subordo)) return;

            CurrentTbl36Subordo.OrdoId = CurrentTbl33Ordo.OrdoId;

            //Combobox select OrdoId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(CurrentTbl36Subordo.OrdoId)) return;

            try
            {
                var subordo = _uow.Tbl36Subordos.GetById(CurrentTbl36Subordo.SubordoId);

                if (CurrentTbl36Subordo.SubordoId == 0)
                    subordo = _extCrud.SubordoAdd(CurrentTbl36Subordo);
                else
                    subordo = _extCrud.SubordoUpdate(subordo, CurrentTbl36Subordo);

                //  _position = SubordosView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl36Subordo.SubordoName)) return;

                try
                {
                    _extCrud.SubordoSave(subordo, CurrentTbl36Subordo);
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

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, CurrentTbl36Subordo.SubordoId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl36Subordo.SubordoName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl36SubordosList = _extCrud.GetSubordosCollectionFromOrdoIdOrderBy<Tbl36Subordo>(CurrentTbl36Subordo.OrdoId);

            SubordosView = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
            SubordosView.MoveCurrentToFirst();
        }

        #endregion [Public Methods  Connect ==> Tbl36Subordo]                                                                                                                                            



        //    Part 5    



        //    Part 6    




        //    Part 7    



        //    Part 8    


        #region [Commands Ordo ==> Tbl90Reference Author]

        private RelayCommand _addReferenceAuthorCommand;

        public ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteAddReferenceAuthor(null); });

        private RelayCommand _copyReferenceAuthorCommand;

        public ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceAuthor(null); });

        private RelayCommand _deleteReferenceAuthorCommand;

        public ICommand DeleteReferenceAuthorCommand => _deleteReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceAuthor(null); });

        private RelayCommand _saveReferenceAuthorCommand;

        public ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceAuthor(null); });

        #endregion [Commands Ordo ==> Tbl90Reference Author]                

        #region [Methods Ordo ==> Tbl90Reference Author]

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

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceOrdo(CurrentTbl90ReferenceAuthor, "Author");

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceAuthor(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceAuthor.ReferenceId);
                if (reference != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceAuthor.Info)) return;

                    _extCrud.DeleteReference(reference);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceAuthor.Info);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceAuthor.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }

        public void ExecuteSaveReferenceAuthor(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            CurrentTbl90ReferenceAuthor.OrdoId = CurrentTbl33Ordo.OrdoId;

            //Combobox select RefAuthorId may be not null
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(CurrentTbl90ReferenceAuthor.RefAuthorId)) return;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceAuthor.ReferenceId);


                if (CurrentTbl90ReferenceAuthor.ReferenceId == 0)
                    reference = _extCrud.ReferenceAuthorOrdoAdd(CurrentTbl90ReferenceAuthor);

                else
                    reference = _extCrud.ReferenceAuthorOrdoUpdate(reference, CurrentTbl90ReferenceAuthor);

                //    _position = OrdosView.CurrentPosition;

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

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, CurrentTbl90ReferenceAuthor.ReferenceId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl90ReferenceAuthor.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromOrdoIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl33Ordo.OrdoId);


            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }
        #endregion [Methods Ordo ==> Tbl90Reference Author]              

        #region [Commands Ordo ==> Tbl90Reference Source]      

        private RelayCommand _addReferenceSourceCommand;

        public ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteAddReferenceSource(null); });

        private RelayCommand _copyReferenceSourceCommand;

        public ICommand CopyReferenceSourceCommand => _copyReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceSource(null); });

        private RelayCommand _deleteReferenceSourceCommand;

        public ICommand DeleteReferenceSourceCommand => _deleteReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceSource(null); });

        private RelayCommand _saveReferenceSourceCommand;

        public ICommand SaveReferenceSourceCommand => _saveReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceSource(null); });


        #endregion [Commands Ordo ==> Tbl90Reference Source]         

        #region [Methods Ordo ==> Tbl90Reference Source]      

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

            Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceOrdo(CurrentTbl90ReferenceSource, "Source");

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceSource(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceSource.ReferenceId);
                if (reference != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceSource.Info)) return;

                    _extCrud.DeleteReference(reference);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceSource.Info);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceSource.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromOrdoIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl33Ordo.OrdoId);

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }

        public void ExecuteSaveReferenceSource(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            //Combobox select RefSourceId may be not 0

            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(CurrentTbl90ReferenceSource.RefSourceId)) return;

            CurrentTbl90ReferenceSource.OrdoId = CurrentTbl33Ordo.OrdoId;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceSource.ReferenceId);


                if (CurrentTbl90ReferenceSource.ReferenceId == 0)
                    reference = _extCrud.ReferenceSourceOrdoAdd(CurrentTbl90ReferenceSource);
                else
                    reference = _extCrud.ReferenceSourceOrdoUpdate(reference, CurrentTbl90ReferenceSource);

                //        _position = OrdosView.CurrentPosition;

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

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, CurrentTbl90ReferenceSource.ReferenceId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl90ReferenceSource.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromOrdoIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl33Ordo.OrdoId);



            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }
        #endregion [Methods Ordo ==> Tbl90Reference Source]                    

        #region [Commands Ordo ==> Tbl90Reference Expert]                 

        private RelayCommand _addReferenceExpertCommand;

        public ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteAddReferenceExpert(null); });

        private RelayCommand _copyReferenceExpertCommand;

        public ICommand CopyReferenceExpertCommand => _copyReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceExpert(null); });

        private RelayCommand _deleteReferenceExpertCommand;

        public ICommand DeleteReferenceExpertCommand => _deleteReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceExpert(null); });
        private RelayCommand _saveReferenceExpertCommand;

        public ICommand SaveReferenceExpertCommand => _saveReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceExpert(null); });

        #endregion [Commands Ordo ==> Tbl90Reference Expert]                    


        #region [Methods Ordo ==> Tbl90Reference Expert]                 

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
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            Tbl90ReferenceExpertsList = _extCrud.CopyReferenceOrdo(CurrentTbl90ReferenceExpert, "Expert");

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceExpert(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceExpert.ReferenceId);
                if (reference != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceExpert.Info)) return;

                    _extCrud.DeleteReference(reference);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceExpert.Info);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceExpert.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromOrdoIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl33Ordo.OrdoId);

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }

        public void ExecuteSaveReferenceExpert(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            //Combobox select RefExpertId  may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(CurrentTbl90ReferenceExpert.RefExpertId)) return;

            CurrentTbl90ReferenceExpert.OrdoId = CurrentTbl33Ordo.OrdoId;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceExpert.ReferenceId);


                if (CurrentTbl90ReferenceExpert.ReferenceId == 0)
                    reference = _extCrud.ReferenceExpertOrdoAdd(CurrentTbl90ReferenceExpert);
                else
                    reference = _extCrud.ReferenceExpertOrdoUpdate(reference, CurrentTbl90ReferenceExpert);

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

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, CurrentTbl90ReferenceExpert.ReferenceId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl90ReferenceExpert.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromOrdoIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl33Ordo.OrdoId);


            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }
        #endregion [Methods Ordo ==> Tbl90Reference Expert]                               

        #region [Commands Ordo ==> Tbl93Comments]        

        private RelayCommand _addCommentCommand;

        public ICommand AddCommentCommand => _addCommentCommand ??= new RelayCommand(delegate { ExecuteAddComment(null); });

        private RelayCommand _copyCommentCommand;

        public ICommand CopyCommentCommand => _copyCommentCommand ??= new RelayCommand(delegate { ExecuteCopyComment(null); });

        private RelayCommand _deleteCommentCommand;

        public ICommand DeleteCommentCommand => _deleteCommentCommand ??= new RelayCommand(delegate { ExecuteDeleteComment(null); });

        private RelayCommand _saveCommentCommand;

        public ICommand SaveCommentCommand => _saveCommentCommand ??= new RelayCommand(delegate { ExecuteSaveComment(null); });

        #endregion [Commands Ordo ==> Tbl93Comments]        



        #region [Methods Ordo ==> Tbl93Comments]        

        public void ExecuteAddComment(object o)
        {
            Tbl93CommentsList ??= new ObservableCollection<Tbl93Comment>();

            Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = CultRes.StringsRes.DatasetNew });

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }

        public void ExecuteCopyComment(object o)
        {

            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            Tbl93CommentsList = _extCrud.CopyComment(CurrentTbl93Comment, "Comment");

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteComment(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            try
            {
                var comment = _uow.Tbl93Comments.GetById(CurrentTbl93Comment.CommentId);
                if (comment != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl93Comment.Info)) return;

                    _extCrud.DeleteComment(comment);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl93Comment.Info);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + CurrentTbl93Comment.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromOrdoIdOrderBy<Tbl93Comment>(CurrentTbl33Ordo.OrdoId);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }

        private void ExecuteSaveComment(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            CurrentTbl93Comment.OrdoId = CurrentTbl33Ordo.OrdoId;

            try
            {
                var comment = _uow.Tbl93Comments.GetById(CurrentTbl93Comment.CommentId);


                if (CurrentTbl93Comment.CommentId == 0)
                    comment = _extCrud.CommentOrdoAdd(CurrentTbl93Comment);
                else
                    comment = _extCrud.CommentOrdoUpdate(comment, CurrentTbl93Comment);

                //        _position = OrdosView.CurrentPosition;

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

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, CurrentTbl93Comment.CommentId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl93Comment.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl93CommentsList = _extCrud.GetCommentsCollectionFromOrdoIdOrderBy<Tbl93Comment>(CurrentTbl33Ordo.OrdoId);


            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        #endregion [Methods Ordo ==> Tbl93Comments]                 


        //    Part 9    



        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        #endregion "Public Commands Connected Tables by DoubleClick"

        #region "Public Method Connected Tables by DoubleClick"

        private void GetConnectedTablesById(object o)
        {
            Tbl30LegiosList = _extCrud.GetLegiosCollectionFromLegioIdOrderBy<Tbl30Legio>(CurrentTbl33Ordo.LegioId);

            Tbl27InfraclassesAllList = _extCrud.GetCollectionAllOrderBy<Tbl27Infraclass>("");

            LegiosView = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
            LegiosView.Refresh();

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
                    if (CurrentTbl33Ordo != null)
                    {
                        Tbl30LegiosList = _extCrud.GetLegiosCollectionFromLegioIdOrderBy<Tbl30Legio>(CurrentTbl33Ordo.LegioId);

                        Tbl27InfraclassesAllList = _extCrud.GetCollectionAllOrderBy<Tbl27Infraclass>("");

                        LegiosView = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
                        LegiosView.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }

                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl33Ordo != null)
                    {
                        Tbl36SubordosList = _extCrud.GetSubordosCollectionFromOrdoIdOrderBy<Tbl36Subordo>(CurrentTbl33Ordo.OrdoId);

                        Tbl33OrdosAllList = _extCrud.GetCollectionAllOrderBy<Tbl33Ordo>("ordo");

                        SubordosView = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
                        SubordosView.Refresh();
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
                    if (CurrentTbl33Ordo != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromOrdoIdOrderBy<Tbl93Comment>(CurrentTbl33Ordo.OrdoId);

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
                    if (CurrentTbl33Ordo != null)
                    {
                        Tbl30LegiosList = _extCrud.GetLegiosCollectionFromLegioIdOrderBy<Tbl30Legio>(CurrentTbl33Ordo.LegioId);

                        LegiosView = CollectionViewSource.GetDefaultView(Tbl30LegiosList);
                        LegiosView.Refresh();
                    }
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 1)
                {
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 2)
                {
                    if (CurrentTbl33Ordo != null)
                    {
                        Tbl36SubordosList = _extCrud.GetSubordosCollectionFromOrdoIdOrderBy<Tbl36Subordo>(CurrentTbl33Ordo.OrdoId);

                        Tbl33OrdosAllList = _extCrud.GetCollectionAllOrderBy<Tbl33Ordo>("ordo");

                        SubordosView = CollectionViewSource.GetDefaultView(Tbl36SubordosList);
                        SubordosView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTbl33Ordo != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromOrdoIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl33Ordo.OrdoId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 4)
                {
                    if (CurrentTbl33Ordo != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromOrdoIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl33Ordo.OrdoId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 5)
                {
                    if (CurrentTbl33Ordo != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromOrdoIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl33Ordo.OrdoId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 2;
                }

                if (_selectedDetailTabIndex == 6)
                {
                    if (CurrentTbl33Ordo != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromOrdoIdOrderBy<Tbl93Comment>(CurrentTbl33Ordo.OrdoId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                }

                if (_selectedDetailTabIndex == 7)
                {
                    if (CurrentTbl33Ordo != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromOrdoIdOrderBy<Tbl93Comment>(CurrentTbl33Ordo.OrdoId);

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
                    if (CurrentTbl33Ordo != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromOrdoIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl33Ordo.OrdoId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                }

                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (CurrentTbl33Ordo != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromOrdoIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl33Ordo.OrdoId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 2;
                }

                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (CurrentTbl33Ordo != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromOrdoIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl33Ordo.OrdoId);

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


        #region "Public Properties Tbl33Ordo"

        private string _searchOrdoName = "";
        public string SearchOrdoName
        {
            get => _searchOrdoName;
            set { _searchOrdoName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView OrdosView;
        private Tbl33Ordo CurrentTbl33Ordo => OrdosView?.CurrentItem as Tbl33Ordo;

        private ObservableCollection<Tbl33Ordo> _tbl33OrdosList;
        public ObservableCollection<Tbl33Ordo> Tbl33OrdosList
        {
            get => _tbl33OrdosList;
            set { _tbl33OrdosList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl33Ordo> _tbl33OrdosAllList;
        public ObservableCollection<Tbl33Ordo> Tbl33OrdosAllList
        {
            get => _tbl33OrdosAllList;
            set { _tbl33OrdosAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl36Subordo> _tbl36SubordosAllList;
        public ObservableCollection<Tbl36Subordo> Tbl36SubordosAllList
        {
            get => _tbl36SubordosAllList;
            set { _tbl36SubordosAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   

        #region "Public Properties Tbl30Legio"

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

        #region "Public Properties Tbl39Infraordo"

        public ICollectionView InfraordosView;
        private Tbl39Infraordo CurrentTbl39Infraordo => InfraordosView?.CurrentItem as Tbl39Infraordo;

        private ObservableCollection<Tbl39Infraordo> _tbl39InfraordosList;
        public ObservableCollection<Tbl39Infraordo> Tbl39InfraordosList
        {
            get => _tbl39InfraordosList;
            set { _tbl39InfraordosList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl27Infraclass"

        private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesAllList;
        public ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesAllList
        {
            get => _tbl27InfraclassesAllList;
            set { _tbl27InfraclassesAllList = value; RaisePropertyChanged(""); }
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
