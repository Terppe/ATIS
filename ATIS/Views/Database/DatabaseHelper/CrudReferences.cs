using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using Microsoft.EntityFrameworkCore;

namespace ATIS.Ui.Views.Database.DatabaseHelper
{
    public class CrudReferences : ViewModelBase
    {
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();
        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();

        #region [ReferenceExperts]

        public ObservableCollection<Tbl90Reference> GetExperts(string searchName)
        {
            return SearchNameReturnExpertsCollection(searchName);
        }
        public ObservableCollection<Tbl90Reference> AddExpert(Tbl03Regnum selectedRegnum, ObservableCollection<Tbl90Reference> referenceExpertsCollection)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(selectedRegnum)) return referenceExpertsCollection;


            referenceExpertsCollection.Insert(0, new Tbl90Reference() { RegnumId = selectedRegnum.RegnumId, Info = "NewDataset" });

            return referenceExpertsCollection;
        }
        public ObservableCollection<Tbl90Reference> CopyExpert(Tbl90Reference selectedReferenceExpert, ObservableCollection<Tbl90Reference> referenceExpertsCollection)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(selectedReferenceExpert)) return referenceExpertsCollection;

            if (selectedReferenceExpert != null)
            {
                var reference = _uow.Tbl90References.GetById(selectedReferenceExpert.ReferenceId);

                referenceExpertsCollection.Insert(0, new Tbl90Reference()
                {
                    RegnumId = reference.RegnumId,
                    RefExpertId = reference.RefExpertId,
                    Valid = reference.Valid,
                    ValidYear = reference.ValidYear,
                    Info = "NewDataset",
                    Memo = reference.Memo
                });
            }

            return referenceExpertsCollection;
        }
        public ObservableCollection<Tbl90Reference> DeleteExpert(Tbl03Regnum selectedRegnum, Tbl90Reference selectedReferenceExpert, ObservableCollection<Tbl90Reference> referenceExpertsCollection)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(selectedReferenceExpert)) return referenceExpertsCollection;

            try
            {
                var reference = _uow.Tbl90References.GetById(selectedReferenceExpert.ReferenceId);
                if (reference != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("DeleteQuestion" + " " + selectedReferenceExpert.Info)) return referenceExpertsCollection;

                    _uow.Tbl90References.Remove(reference);
                    _uow.Complete();

                    _allMessageBoxes.InfoMessageBox("DeleteSuccess", selectedReferenceExpert.Info);
                }
                else
                {
                    _allMessageBoxes.InfoMessageBox("Not To Delete", "DeleteCan" + " " + selectedReferenceExpert.Info + " " + "DeleteCan1");
                }
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, "Error");
                //         Log.Error(e);
            }

            return referenceExpertsCollection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.RegnumId == selectedRegnum.RegnumId && x.RefAuthorId == null && x.RefSourceId == null));
        }
        public ObservableCollection<Tbl90Reference> SaveExpert(Tbl03Regnum selectedRegnum, Tbl90Reference selectedReferenceExpert, ObservableCollection<Tbl90Reference> referenceExpertsCollection)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(selectedRegnum)) return referenceExpertsCollection;

            selectedReferenceExpert.RegnumId = selectedRegnum.RegnumId;

            try
            {
                var reference = _uow.Tbl90References.GetById(selectedReferenceExpert.ReferenceId);
                if (selectedReferenceExpert.ReferenceId != 0)
                {
                    if (reference != null) //update
                    {
                        reference.RefExpertId = selectedReferenceExpert.RefExpertId;
                        reference.RegnumId = selectedReferenceExpert.RegnumId;
                        reference.Valid = selectedReferenceExpert.Valid;
                        reference.ValidYear = selectedReferenceExpert.ValidYear;
                        reference.Info = selectedReferenceExpert.Info;
                        reference.Updater = Environment.UserName;
                        reference.UpdaterDate = DateTime.Now;
                        reference.Memo = selectedReferenceExpert.Memo;
                    }
                }
                else
                {
                    reference = new Tbl90Reference //add new
                    {
                        RefExpertId = selectedReferenceExpert.RefExpertId,
                        RegnumId = selectedReferenceExpert.RegnumId,
                        CountId = RandomHelper.Randomnumber(),
                        Valid = selectedReferenceExpert.Valid,
                        ValidYear = selectedReferenceExpert.ValidYear,
                        Info = selectedReferenceExpert.Info,
                        Memo = selectedReferenceExpert.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                    };
                }

                try
                {
                    if (selectedReferenceExpert.ReferenceId != 0) //update
                    {
                        if (reference != null) _uow.Tbl90References.Update(reference);
                    }
                    else                                //add
                    if (reference != null) _uow.Tbl90References.Add(reference);

                    _uow.Complete();

                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(), "FailedToSave");
                    //     Log.Error(e);
                    return referenceExpertsCollection;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, "Error");
                    //         Log.Error(e);
                    return referenceExpertsCollection;
                }
                _allMessageBoxes.InfoMessageBox("SaveSuccess", selectedReferenceExpert.RefExpertId == 0
                    ? "DatasetNew"
                    : selectedReferenceExpert.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, "Error");
                //         Log.Error(e);
            }

            return     referenceExpertsCollection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.RegnumId == selectedRegnum.RegnumId && x.RefAuthorId == null && x.RefSourceId == null));
        }
        public ObservableCollection<Tbl90Reference> SearchNameReturnExpertsCollection(string searchName)
        {
            var collection = new ObservableCollection<Tbl90Reference>();

            switch (searchName)
            {
                case "":
                    return collection;
                case "*":
                    collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.GetAll());
                    break;
                default:
                    collection = int.TryParse(searchName, out var id)
                        ? new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                            .Find(e => e.ReferenceId == id && e.RefAuthorId == null && e.RefSourceId == null))
                        : new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                            .Find(e => e.Info.StartsWith(searchName))
                        );
                    break;
            }

            return collection;
        }

        #endregion

        #region [ReferenceSources]

        public ObservableCollection<Tbl90Reference> GetSources(string searchName)
        {
            return SearchNameReturnSourcesCollection(searchName);
        }
        public ObservableCollection<Tbl90Reference> AddSource(Tbl03Regnum selectedRegnum, ObservableCollection<Tbl90Reference> referenceSourcesCollection)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(selectedRegnum)) return referenceSourcesCollection;


            referenceSourcesCollection.Insert(0, new Tbl90Reference() { RegnumId = selectedRegnum.RegnumId, Info = "NewDataset" });

            return referenceSourcesCollection;
        }
        public ObservableCollection<Tbl90Reference> CopySource(Tbl90Reference selectedReferenceSource, ObservableCollection<Tbl90Reference> referenceSourcesCollection)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(selectedReferenceSource)) return referenceSourcesCollection;

            if (selectedReferenceSource != null)
            {
                var reference = _uow.Tbl90References.GetById(selectedReferenceSource.ReferenceId);

                referenceSourcesCollection.Insert(0, new Tbl90Reference()
                {
                    RegnumId = reference.RegnumId,
                    RefSourceId = reference.RefSourceId,
                    Valid = reference.Valid,
                    ValidYear = reference.ValidYear,
                    Info = "NewDataset",
                    Memo = reference.Memo
                });
            }

            return referenceSourcesCollection;
        }
        public ObservableCollection<Tbl90Reference> DeleteSource(Tbl03Regnum selectedRegnum, Tbl90Reference selectedReferenceSource, ObservableCollection<Tbl90Reference> referenceSourcesCollection)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(selectedReferenceSource)) return referenceSourcesCollection;

            try
            {
                var reference = _uow.Tbl90References.GetById(selectedReferenceSource.ReferenceId);
                if (reference != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("DeleteQuestion" + " " + selectedReferenceSource.Info)) return referenceSourcesCollection;

                    _uow.Tbl90References.Remove(reference);
                    _uow.Complete();

                    _allMessageBoxes.InfoMessageBox("DeleteSuccess", selectedReferenceSource.Info);
                }
                else
                {
                    _allMessageBoxes.InfoMessageBox("Not To Delete", "DeleteCan" + " " + selectedReferenceSource.Info + " " + "DeleteCan1");
                }
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, "Error");
                //         Log.Error(e);
            }

            return referenceSourcesCollection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.RegnumId == selectedRegnum.RegnumId && x.RefAuthorId == null && x.RefExpertId == null));
        }
        public ObservableCollection<Tbl90Reference> SaveSource(Tbl03Regnum selectedRegnum, Tbl90Reference selectedReferenceSource, ObservableCollection<Tbl90Reference> referenceSourcesCollection)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(selectedRegnum)) return referenceSourcesCollection;

            selectedReferenceSource.RegnumId = selectedRegnum.RegnumId;

            try
            {
                var reference = _uow.Tbl90References.GetById(selectedReferenceSource.ReferenceId);
                if (selectedReferenceSource.ReferenceId != 0)
                {
                    if (reference != null) //update
                    {
                        reference.RefSourceId = selectedReferenceSource.RefSourceId;
                        reference.RegnumId = selectedReferenceSource.RegnumId;
                        reference.Valid = selectedReferenceSource.Valid;
                        reference.ValidYear = selectedReferenceSource.ValidYear;
                        reference.Info = selectedReferenceSource.Info;
                        reference.Updater = Environment.UserName;
                        reference.UpdaterDate = DateTime.Now;
                        reference.Memo = selectedReferenceSource.Memo;
                    }
                }
                else
                {
                    reference = new Tbl90Reference //add new
                    {
                        RefSourceId = selectedReferenceSource.RefSourceId,
                        RegnumId = selectedReferenceSource.RegnumId,
                        CountId = RandomHelper.Randomnumber(),
                        Valid = selectedReferenceSource.Valid,
                        ValidYear = selectedReferenceSource.ValidYear,
                        Info = selectedReferenceSource.Info,
                        Memo = selectedReferenceSource.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                    };
                }

                try
                {
                    if (selectedReferenceSource.ReferenceId != 0) //update
                    {
                        if (reference != null) _uow.Tbl90References.Update(reference);
                    }
                    else                                //add
                    if (reference != null) _uow.Tbl90References.Add(reference);

                    _uow.Complete();

                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(), "FailedToSave");
                    //     Log.Error(e);
                    return referenceSourcesCollection;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, "Error");
                    //         Log.Error(e);
                    return referenceSourcesCollection;
                }
                _allMessageBoxes.InfoMessageBox("SaveSuccess", selectedReferenceSource.RefSourceId == 0
                    ? "DatasetNew"
                    : selectedReferenceSource.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, "Error");
                //         Log.Error(e);
            }

            return referenceSourcesCollection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.RegnumId == selectedRegnum.RegnumId && x.RefAuthorId == null && x.RefExpertId == null));
        }
        public ObservableCollection<Tbl90Reference> SearchNameReturnSourcesCollection(string searchName)
        {
            var collection = new ObservableCollection<Tbl90Reference>();

            switch (searchName)
            {
                case "":
                    return collection;
                case "*":
                    collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.GetAll());
                    break;
                default:
                    collection = int.TryParse(searchName, out var id)
                        ? new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                            .Find(e => e.ReferenceId == id && e.RefAuthorId == null && e.RefExpertId == null))
                        : new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                            .Find(e => e.Info.StartsWith(searchName))
                        );
                    break;
            }

            return collection;
        }


        #endregion

        #region [ReferenceAuthors]
        public ObservableCollection<Tbl90Reference> GetAuthors(string searchName)
        {
            return SearchNameReturnAuthorsCollection(searchName);
        }
        public ObservableCollection<Tbl90Reference> AddAuthor(Tbl03Regnum selectedRegnum, ObservableCollection<Tbl90Reference> referenceAuthorsCollection)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(selectedRegnum)) return referenceAuthorsCollection;


            referenceAuthorsCollection.Insert(0, new Tbl90Reference() { RegnumId = selectedRegnum.RegnumId, Info = "NewDataset" });

            return referenceAuthorsCollection;
        }
        public ObservableCollection<Tbl90Reference> CopyAuthor(Tbl90Reference selectedReferenceAuthor, ObservableCollection<Tbl90Reference> referenceAuthorsCollection)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(selectedReferenceAuthor)) return referenceAuthorsCollection;

            if (selectedReferenceAuthor != null)
            {
                var reference = _uow.Tbl90References.GetById(selectedReferenceAuthor.ReferenceId);

                referenceAuthorsCollection.Insert(0, new Tbl90Reference()
                {
                    RegnumId = reference.RegnumId,
                    RefAuthorId = reference.RefAuthorId,
                    Valid = reference.Valid,
                    ValidYear = reference.ValidYear,
                    Info = "NewDataset",
                    Memo = reference.Memo
                });
            }

            return referenceAuthorsCollection;
        }
        public ObservableCollection<Tbl90Reference> DeleteAuthor(Tbl03Regnum selectedRegnum, Tbl90Reference selectedReferenceAuthor, ObservableCollection<Tbl90Reference> referenceAuthorsCollection)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(selectedReferenceAuthor)) return referenceAuthorsCollection;

            try
            {
                var reference = _uow.Tbl90References.GetById(selectedReferenceAuthor.ReferenceId);
                if (reference != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("DeleteQuestion" + " " + selectedReferenceAuthor.Info)) return referenceAuthorsCollection;

                    _uow.Tbl90References.Remove(reference);
                    _uow.Complete();

                    _allMessageBoxes.InfoMessageBox("DeleteSuccess", selectedReferenceAuthor.Info);
                }
                else
                {
                    _allMessageBoxes.InfoMessageBox("Not To Delete", "DeleteCan" + " " + selectedReferenceAuthor.Info + " " + "DeleteCan1");
                }
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, "Error");
                //         Log.Error(e);
            }

            return referenceAuthorsCollection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.RegnumId == selectedRegnum.RegnumId && x.RefSourceId == null && x.RefExpertId == null));
        }
        public ObservableCollection<Tbl90Reference> SaveAuthor(Tbl03Regnum selectedRegnum, Tbl90Reference selectedReferenceAuthor, ObservableCollection<Tbl90Reference> referenceAuthorsCollection)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(selectedRegnum)) return referenceAuthorsCollection;

            selectedReferenceAuthor.RegnumId = selectedRegnum.RegnumId;

            try
            {
                var reference = _uow.Tbl90References.GetById(selectedReferenceAuthor.ReferenceId);
                if (selectedReferenceAuthor.ReferenceId != 0)
                {
                    if (reference != null) //update
                    {
                        reference.RefAuthorId = selectedReferenceAuthor.RefAuthorId;
                        reference.RegnumId = selectedReferenceAuthor.RegnumId;
                        reference.Valid = selectedReferenceAuthor.Valid;
                        reference.ValidYear = selectedReferenceAuthor.ValidYear;
                        reference.Info = selectedReferenceAuthor.Info;
                        reference.Updater = Environment.UserName;
                        reference.UpdaterDate = DateTime.Now;
                        reference.Memo = selectedReferenceAuthor.Memo;
                    }
                }
                else
                {
                    reference = new Tbl90Reference //add new
                    {
                        RefAuthorId = selectedReferenceAuthor.RefAuthorId,
                        RegnumId = selectedReferenceAuthor.RegnumId,
                        CountId = RandomHelper.Randomnumber(),
                        Valid = selectedReferenceAuthor.Valid,
                        ValidYear = selectedReferenceAuthor.ValidYear,
                        Info = selectedReferenceAuthor.Info,
                        Memo = selectedReferenceAuthor.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                    };
                }

                try
                {
                    if (selectedReferenceAuthor.ReferenceId != 0) //update
                    {
                        if (reference != null) _uow.Tbl90References.Update(reference);
                    }
                    else                                //add
                    if (reference != null) _uow.Tbl90References.Add(reference);

                    _uow.Complete();

                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(), "FailedToSave");
                    //     Log.Error(e);
                    return referenceAuthorsCollection;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, "Error");
                    //         Log.Error(e);
                    return referenceAuthorsCollection;
                }
                _allMessageBoxes.InfoMessageBox("SaveSuccess", selectedReferenceAuthor.RefSourceId == 0
                    ? "DatasetNew"
                    : selectedReferenceAuthor.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, "Error");
                //         Log.Error(e);
            }

            return referenceAuthorsCollection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.RegnumId == selectedRegnum.RegnumId && x.RefSourceId == null && x.RefExpertId == null));
        }
        public ObservableCollection<Tbl90Reference> SearchNameReturnAuthorsCollection(string searchName)
        {
            var collection = new ObservableCollection<Tbl90Reference>();

            switch (searchName)
            {
                case "":
                    return collection;
                case "*":
                    collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.GetAll());
                    break;
                default:
                    collection = int.TryParse(searchName, out var id)
                        ? new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                            .Find(e => e.ReferenceId == id && e.RefSourceId == null && e.RefExpertId == null))
                        : new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                            .Find(e => e.Info.StartsWith(searchName))
                        );
                    break;
            }

            return collection;
        }

        #endregion
    }
}
