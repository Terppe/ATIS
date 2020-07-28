using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using ATIS.Ui.Views.Database.D03Regnum;
using ATIS.Ui.Views.Database.DatabaseHelper;
using Microsoft.EntityFrameworkCore;

namespace ATIS.Ui.Views.Database.D06Phylum
{
    public class PhylumsViewModel : ViewModelBase
    {
        // Version with Generic Unit Of Work and AtisDbContext for general use

        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();
        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private GenericMessageBoxes<Tbl06Phylum> _genPhylumMessageBoxes = new GenericMessageBoxes<Tbl06Phylum>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genExpertMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genSourceMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genAuthorMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl93Comment> _genCommentMessageBoxes = new GenericMessageBoxes<Tbl93Comment>();

        #region [ Constructor ]

        public PhylumsViewModel()
        {
            LoadCollections();
        }

        private void LoadCollections()
        {
            RegnumsAllCollection = new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums.GetAll());
            PhylumsAllCollection = new ObservableCollection<Tbl06Phylum>(_uow.Tbl06Phylums.GetAll());
            SubphylumsAllCollection = new ObservableCollection<Tbl12Subphylum>(_uow.Tbl12Subphylums.GetAll());

            RegnumsCollection = new ObservableCollection<Tbl03Regnum>();
            SubphylumsCollection = new ObservableCollection<Tbl12Subphylum>();
            SuperclassesCollection = new ObservableCollection<Tbl18Superclass>();
            ReferencesCollection = new ObservableCollection<Tbl90Reference>();
            ReferenceExpertsCollection = new ObservableCollection<Tbl90Reference>();
            ReferenceSourcesCollection = new ObservableCollection<Tbl90Reference>();
            ReferenceAuthorsCollection = new ObservableCollection<Tbl90Reference>();
            ExpertsCollection = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.GetAll());
            SourcesCollection = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.GetAll());
            AuthorsCollection = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsToCombobox());

            CommentsCollection = new ObservableCollection<Tbl93Comment>();

            TabIndexDetail = 2;
        }

        #endregion

        #region [Commands Phylum]

        private RelayCommand _getPhylumsByNameOrIdCommand;
        public ICommand GetPhylumsByNameOrIdCommand => _getPhylumsByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetPhylumsByNameOrId(SearchPhylumName); });
        private RelayCommand _addPhylumCommand;
        public ICommand AddPhylumCommand => _addPhylumCommand ??= new RelayCommand(delegate { ExecuteAddPhylum(null); });
        private RelayCommand _copyPhylumCommand;
        public ICommand CopyPhylumCommand => _copyPhylumCommand ??= new RelayCommand(delegate { ExecuteCopyPhylum(null); });
        private RelayCommand _deletePhylumCommand;
        public ICommand DeletePhylumCommand => _deletePhylumCommand ??= new RelayCommand(delegate { ExecuteDeletePhylum(SearchPhylumName); });
        private RelayCommand _savePhylumCommand;
        public ICommand SavePhylumCommand => _savePhylumCommand ??= new RelayCommand(delegate { ExecuteSavePhylum(SearchPhylumName); });


        #endregion

        #region [Methods Phylum]

        private void ExecuteGetPhylumsByNameOrId(string searchName)
        {
            TabIndexDetail = 2;

            RegnumsCollection.Clear();
            SubphylumsCollection.Clear();
            SuperclassesCollection.Clear();
            ReferencesCollection.Clear();
            ReferenceExpertsCollection.Clear();
            ReferenceSourcesCollection.Clear();
            ReferenceAuthorsCollection.Clear();
            CommentsCollection.Clear();

            PhylumsCollection = SearchNameReturnPhylumsCollection(searchName);
            RaisePropertyChanged("PhylumsCollection");
        }
        private void ExecuteAddPhylum(object o)
        {
            PhylumsCollection.Insert(0, new Tbl06Phylum() { PhylumName = "NewDataset" });
            RaisePropertyChanged("PhylumsCollection");
        }
        private void ExecuteCopyPhylum(object o)
        {
            if (_genPhylumMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedPhylum)) return;

            var phylum = _uow.Tbl06Phylums.GetById(SelectedPhylum.PhylumId);

            PhylumsCollection.Insert(0, new Tbl06Phylum()
            {
                PhylumName = "NewDataset",
                RegnumId = phylum.RegnumId,
                Valid = phylum.Valid,
                ValidYear = phylum.ValidYear,
                Synonym = phylum.Synonym,
                Author = phylum.Author,
                AuthorYear = phylum.AuthorYear,
                Info = phylum.Info,
                EngName = phylum.EngName,
                GerName = phylum.GerName,
                FraName = phylum.FraName,
                PorName = phylum.PorName,
                Memo = phylum.Memo
            });

            RaisePropertyChanged("PhylumsCollection");
            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment
        }
        private void ExecuteDeletePhylum(string searchName)
        {
            if (_genPhylumMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedPhylum)) return;

            //check if in Tbl12Subphylums connected datasets no delete, Expert, Sources, authors and Comment delete and than return

            SubphylumsCollection = new ObservableCollection<Tbl12Subphylum>(_uow.Tbl12Subphylums.Find(x => x.PhylumId == SelectedPhylum.PhylumId));
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(SubphylumsCollection.Count, "Subphylum")) return;


            //Delete all References Expert, Source, Authors  ----------------------------------------------------

            ReferencesCollection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.PhylumId == SelectedPhylum.PhylumId));
            if (ReferencesCollection.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("Reference Author, Reference Source, Reference Expert")) return;

                foreach (var t in ReferencesCollection)
                {
                    _uow.Tbl90References.Remove(t);
                }
                _uow.Complete();

                _allMessageBoxes.InfoMessageBox("DeleteSuccess", "Reference");
            }

            CommentsCollection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.PhylumId == SelectedPhylum.PhylumId));
            if (CommentsCollection.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("Comment")) return;

                foreach (var t in CommentsCollection)
                {
                    _uow.Tbl93Comments.Remove(t);
                }
                _uow.Complete();

                _allMessageBoxes.InfoMessageBox("Delete successful", "Comment");
            }

            if (false) return;
            {
                try
                {
                    var phylum = _uow.Tbl06Phylums.GetById(SelectedPhylum.PhylumId);
                    if (phylum != null)
                    {
                        if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("DeleteQuestion" + " " + SelectedPhylum.PhylumName)) return;

                        _uow.Tbl06Phylums.Remove(phylum);
                        _uow.Complete();

                        _allMessageBoxes.InfoMessageBox("Delete successful", SelectedPhylum.PhylumName);
                    }
                    else
                    {
                        _allMessageBoxes.InfoMessageBox("Not To Delete", "DeleteCan" + " " + SelectedPhylum.PhylumName + " " + "DeleteCan1");
                    }
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, "Error");
                    //         Log.Error(e);
                }
            }
            ExecuteGetPhylumsByNameOrId(searchName);

            RaisePropertyChanged("PhylumsCollection");

        }
        private void ExecuteSavePhylum(string searchName)
        {
            if (_genPhylumMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedPhylum)) return;

            try
            {
                var phylum = _uow.Tbl06Phylums.GetById(SelectedPhylum.PhylumId);
                if (SelectedPhylum.PhylumId != 0)
                {
                    if (phylum != null) //update
                    {
                        phylum.PhylumName = _selectedPhylum.PhylumName;
                        phylum.RegnumId = _selectedPhylum.RegnumId;
                        phylum.Valid = _selectedPhylum.Valid;
                        phylum.ValidYear = _selectedPhylum.ValidYear;
                        phylum.Author = _selectedPhylum.Author;
                        phylum.AuthorYear = _selectedPhylum.AuthorYear;
                        phylum.Info = _selectedPhylum.Info;
                        phylum.Synonym = _selectedPhylum.Synonym;
                        phylum.EngName = _selectedPhylum.EngName;
                        phylum.GerName = _selectedPhylum.GerName;
                        phylum.FraName = _selectedPhylum.FraName;
                        phylum.PorName = _selectedPhylum.PorName;
                        phylum.Memo = _selectedPhylum.Memo;
                        phylum.Updater = Environment.UserName;
                        phylum.UpdaterDate = DateTime.Now;
                    }
                }
                else
                {
                    phylum = new Tbl06Phylum() //add new
                    {
                        PhylumName = _selectedPhylum.PhylumName,
                        RegnumId = _selectedPhylum.RegnumId,
                        CountId = RandomHelper.Randomnumber(),
                        Valid = _selectedPhylum.Valid,
                        ValidYear = _selectedPhylum.ValidYear,
                        Author = _selectedPhylum.Author,
                        AuthorYear = _selectedPhylum.AuthorYear,
                        Info = _selectedPhylum.Info,
                        Synonym = _selectedPhylum.Synonym,
                        EngName = _selectedPhylum.EngName,
                        GerName = _selectedPhylum.GerName,
                        FraName = _selectedPhylum.FraName,
                        PorName = _selectedPhylum.PorName,
                        Memo = _selectedPhylum.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                    };
                }

                try
                {
                    if (_selectedPhylum.PhylumId != 0) //update
                        _uow.Tbl06Phylums.Update(phylum);
                    else                            //add
                        _uow.Tbl06Phylums.Add(phylum);

                    _uow.Complete();

                    RaisePropertyChanged("PhylumsCollection");

                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(), "FailedToSave");
                    //     Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, "Error");
                    //         Log.Error(e);
                    return;
                }
                _allMessageBoxes.InfoMessageBox("SaveSuccess", SelectedPhylum.PhylumId == 0
                    ? "DatasetNew"
                    : SelectedPhylum.PhylumName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, "Error");
                //         Log.Error(e);
            }
            ExecuteGetPhylumsByNameOrId(searchName);
        }

        private void UpdateCollection()
        {
            PhylumsCollection.Clear();
            foreach (var phylum in _uow.Tbl06Phylums.GetAll())
            {
                PhylumsCollection.Add(phylum);
            }
        }

        private ObservableCollection<Tbl06Phylum> SearchNameReturnPhylumsCollection(string searchName)
        {
            var collection = new ObservableCollection<Tbl06Phylum>();

            switch (searchName)
            {
                case "":
                    return collection;
                case "*":
                    collection = new ObservableCollection<Tbl06Phylum>(_uow.Tbl06Phylums.GetAll());
                    break;
                default:
                    collection = int.TryParse(searchName, out var id)
                        ? new ObservableCollection<Tbl06Phylum>(_uow.Tbl06Phylums
                            .Find(e => e.PhylumId == id))
                        : new ObservableCollection<Tbl06Phylum>(_uow.Tbl06Phylums
                            .Find(e => e.PhylumName.StartsWith(searchName))
                            .OrderBy(a => a.PhylumName)
                        );
                    break;
            }

            return collection;
        }

        private void GetRegnums(int regnumId)
        {
            var query = (from regnum in _context.Tbl03Regnums
                         where regnum.RegnumId == regnumId
                         select regnum).ToList();

            RegnumsCollection.Clear();
            foreach (Tbl03Regnum regnum in query)
            {
                if (regnum != null) RegnumsCollection.Add(regnum);
            }

            if (query.Count != 0)
                TabIndexDetail = 0;
        }

        private void GetSubphylums(int phylumId)
        {
            //   var _phylumId = int.Parse(phylumId.ToString());
            var query = (from subphylum in _context.Tbl12Subphylums
                         where subphylum.PhylumId == phylumId
                         select subphylum).ToList();

            SubphylumsCollection.Clear();
            foreach (Tbl12Subphylum subphylum in query)
            {
                if (subphylum != null) SubphylumsCollection.Add(subphylum);
            }

            if (query.Count != 0)
                TabIndexDetail = 2;
        }
        private void GetSuperclasses(int subphylumId)
        {
            //   var _phylumId = int.Parse(phylumId.ToString());
            var query = (from superclass in _context.Tbl18Superclasses
                         where superclass.SubphylumId == subphylumId
                         select superclass).ToList();

            SuperclassesCollection.Clear();
            foreach (Tbl18Superclass superclass in query)
            {
                if (superclass != null) SuperclassesCollection.Add(superclass);
            }

            if (query.Count != 0)
                TabIndexDetail = 3;
        }

        private void GetReferenceExperts(int? phylumId)
        {
            var query = (from reference in _context.Tbl90References
                         where reference.PhylumId == phylumId && reference.RefSourceId == null && reference.RefAuthorId == null
                         select reference).ToList();

            ReferenceExpertsCollection.Clear();
            foreach (Tbl90Reference reference in query)
            {
                if (reference != null) ReferenceExpertsCollection.Add(reference);
            }
        }
        private void GetReferenceSources(int? phylumId)
        {
            var query = (from reference in _context.Tbl90References
                         where reference.PhylumId == phylumId && reference.RefExpertId == null && reference.RefAuthorId == null
                         select reference).ToList();

            ReferenceSourcesCollection.Clear();
            foreach (Tbl90Reference reference in query)
            {
                if (reference != null) ReferenceSourcesCollection.Add(reference);
            }
        }
        private void GetReferenceAuthors(int? phylumId)
        {
            var query = (from reference in _context.Tbl90References
                         where reference.PhylumId == phylumId && reference.RefExpertId == null && reference.RefSourceId == null
                         select reference).ToList();

            ReferenceAuthorsCollection.Clear();
            foreach (Tbl90Reference reference in query)
            {
                if (reference != null) ReferenceAuthorsCollection.Add(reference);
            }
        }
        private void GetComments(int phylumId)
        {
            var query = (from comment in _context.Tbl93Comments
                         where comment.PhylumId == phylumId
                         select comment).ToList();

            CommentsCollection.Clear();
            foreach (Tbl93Comment comment in query)
            {
                if (comment != null) CommentsCollection.Add(comment);
            }

            //if (query.Count != 0)
            //    TabIndexDetail = 0;
        }

        #endregion

        //-----------------------------------------
        //-----------------------------------------

        #region [Commands Phylum ==> Tbl90Reference Expert]

        private RelayCommand _getExpertsByNameOrIdCommand;
        public ICommand GetExpertsByNameOrIdCommand => _getExpertsByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetExpertsByNameOrId(SearchExpertName); });
        private RelayCommand _addExpertCommand;
        public ICommand AddExpertCommand => _addExpertCommand ??= new RelayCommand(delegate { ExecuteAddExpert(null); });
        private RelayCommand _copyExpertCommand;
        public ICommand CopyExpertCommand => _copyExpertCommand ??= new RelayCommand(delegate { ExecuteCopyExpert(null); });
        private RelayCommand _deleteExpertCommand;
        public ICommand DeleteExpertCommand => _deleteExpertCommand ??= new RelayCommand(delegate { ExecuteDeleteExpert(null); });
        private RelayCommand _saveExpertCommand;
        public ICommand SaveExpertCommand => _saveExpertCommand ??= new RelayCommand(delegate { ExecuteSaveExpert(null); });

        #endregion

        #region[Methods Reference Expert]

        private void ExecuteGetExpertsByNameOrId(string searchName)
        {
            //ReferenceExpertsCollection.Clear();
            //ReferenceExpertsCollection = _crudRef.GetExperts(searchName);
            ReferenceExpertsCollection = SearchNameReturnExpertsCollection(searchName);

            RaisePropertyChanged("ReferenceExpertsCollection");
        }
        private void ExecuteAddExpert(object o)
        {
            if (_genPhylumMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedPhylum)) return;

            ReferenceExpertsCollection.Insert(0, new Tbl90Reference() { PhylumId = SelectedPhylum.PhylumId, Info = "NewDataset" });
            RaisePropertyChanged("ReferenceExpertsCollection");
        }
        private void ExecuteCopyExpert(object o)
        {
            if (_genExpertMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedReferenceExpert)) return;

            if (SelectedReferenceExpert != null)
            {
                var reference = _uow.Tbl90References.GetById(SelectedReferenceExpert.ReferenceId);

                ReferenceExpertsCollection.Insert(0, new Tbl90Reference()
                {
                    PhylumId = reference.PhylumId,
                    RefExpertId = reference.RefExpertId,
                    Valid = reference.Valid,
                    ValidYear = reference.ValidYear,
                    Info = "NewDataset",
                    Memo = reference.Memo
                });
            }

            RaisePropertyChanged("ReferenceExpertsCollection");
        }
        private void ExecuteDeleteExpert(object o)
        {
            if (_genExpertMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedReferenceExpert)) return;

            try
            {
                var reference = _uow.Tbl90References.GetById(SelectedReferenceExpert.ReferenceId);
                if (reference != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("DeleteQuestion" + " " + SelectedReferenceExpert.Info)) return;

                    _uow.Tbl90References.Remove(reference);
                    _uow.Complete();

                    _allMessageBoxes.InfoMessageBox("DeleteSuccess", SelectedReferenceExpert.Info);
                }
                else
                {
                    _allMessageBoxes.InfoMessageBox("Not To Delete", "DeleteCan" + " " + SelectedReferenceExpert.Info + " " + "DeleteCan1");
                }
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, "Error");
                //         Log.Error(e);
            }

            ReferenceExpertsCollection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.PhylumId == SelectedPhylum.PhylumId && x.RefAuthorId == null && x.RefSourceId == null));

            RaisePropertyChanged("ReferenceExpertsCollection");
        }
        private void ExecuteSaveExpert(object o)
        {
            if (_genPhylumMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedPhylum)) return;

            SelectedReferenceExpert.PhylumId = SelectedPhylum.PhylumId;

            try
            {
                var reference = _uow.Tbl90References.GetById(SelectedReferenceExpert.ReferenceId);
                if (SelectedReferenceExpert.ReferenceId != 0)
                {
                    if (reference != null) //update
                    {
                        reference.RefExpertId = SelectedReferenceExpert.RefExpertId;
                        reference.PhylumId = SelectedReferenceExpert.PhylumId;
                        reference.Valid = SelectedReferenceExpert.Valid;
                        reference.ValidYear = SelectedReferenceExpert.ValidYear;
                        reference.Info = SelectedReferenceExpert.Info;
                        reference.Updater = Environment.UserName;
                        reference.UpdaterDate = DateTime.Now;
                        reference.Memo = SelectedReferenceExpert.Memo;
                    }
                }
                else
                {
                    reference = new Tbl90Reference //add new
                    {
                        RefExpertId = SelectedReferenceExpert.RefExpertId,
                        PhylumId = SelectedReferenceExpert.PhylumId,
                        CountId = RandomHelper.Randomnumber(),
                        Valid = SelectedReferenceExpert.Valid,
                        ValidYear = SelectedReferenceExpert.ValidYear,
                        Info = SelectedReferenceExpert.Info,
                        Memo = SelectedReferenceExpert.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                    };
                }

                try
                {
                    if (SelectedReferenceExpert.ReferenceId != 0) //update
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
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, "Error");
                    //         Log.Error(e);
                    return;
                }
                _allMessageBoxes.InfoMessageBox("SaveSuccess", SelectedReferenceExpert.RefExpertId == 0
                    ? "DatasetNew"
                    : SelectedReferenceExpert.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, "Error");
                //         Log.Error(e);
            }

            ReferenceExpertsCollection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.PhylumId == SelectedPhylum.PhylumId && x.RefAuthorId == null && x.RefSourceId == null));

            RaisePropertyChanged("ReferenceExpertsCollection");
        }
        private ObservableCollection<Tbl90Reference> SearchNameReturnExpertsCollection(string searchName)
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


        //-----------------------------------------

        #region [Commands Phylum ==> Tbl90Reference Source]

        private RelayCommand _getSourcesByNameOrIdCommand;
        public ICommand GetSourcesByNameOrIdCommand => _getSourcesByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetSourcesByNameOrId(SearchSourceName); });
        private RelayCommand _addSourceCommand;
        public ICommand AddSourceCommand => _addSourceCommand ??= new RelayCommand(delegate { ExecuteAddSource(null); });
        private RelayCommand _copySourceCommand;
        public ICommand CopySourceCommand => _copySourceCommand ??= new RelayCommand(delegate { ExecuteCopySource(null); });
        private RelayCommand _deleteSourceCommand;
        public ICommand DeleteSourceCommand => _deleteSourceCommand ??= new RelayCommand(delegate { ExecuteDeleteSource(null); });
        private RelayCommand _saveSourceCommand;
        public ICommand SaveSourceCommand => _saveSourceCommand ??= new RelayCommand(delegate { ExecuteSaveSource(null); });

        #endregion

        #region[Methods Reference Source]

        private void ExecuteGetSourcesByNameOrId(string searchName)
        {
            ReferenceSourcesCollection = SearchNameReturnSourcesCollection(searchName);
            RaisePropertyChanged("ReferenceSourcesCollection");
        }
        private void ExecuteAddSource(object o)
        {
            if (_genPhylumMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedPhylum)) return;

            ReferenceSourcesCollection.Insert(0, new Tbl90Reference() { PhylumId = SelectedPhylum.PhylumId, Info = "NewDataset" });
            RaisePropertyChanged("ReferenceSourcesCollection");
        }
        private void ExecuteCopySource(object o)
        {
            if (_genSourceMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedReferenceSource)) return;

            if (SelectedReferenceSource != null)
            {
                var reference = _uow.Tbl90References.GetById(SelectedReferenceSource.ReferenceId);

                ReferenceSourcesCollection.Insert(0, new Tbl90Reference()
                {
                    PhylumId = reference.PhylumId,
                    RefSourceId = reference.RefSourceId,
                    Valid = reference.Valid,
                    ValidYear = reference.ValidYear,
                    Info = "NewDataset",
                    Memo = reference.Memo
                });
            }

            RaisePropertyChanged("ReferenceSourcesCollection");
        }
        private void ExecuteDeleteSource(object o)
        {
            if (_genSourceMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedReferenceSource)) return;

            try
            {
                var reference = _uow.Tbl90References.GetById(SelectedReferenceSource.ReferenceId);
                if (reference != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("DeleteQuestion" + " " + SelectedReferenceSource.Info)) return;

                    _uow.Tbl90References.Remove(reference);
                    _uow.Complete();

                    _allMessageBoxes.InfoMessageBox("DeleteSuccess", SelectedReferenceSource.Info);
                }
                else
                {
                    _allMessageBoxes.InfoMessageBox("Not To Delete", "DeleteCan" + " " + SelectedReferenceSource.Info + " " + "DeleteCan1");
                }
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, "Error");
                //         Log.Error(e);
            }

            ReferenceSourcesCollection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.PhylumId == SelectedPhylum.PhylumId && x.RefAuthorId == null && x.RefExpertId == null));

            RaisePropertyChanged("ReferenceSourcesCollection");
        }
        private void ExecuteSaveSource(object o)
        {
            if (_genPhylumMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedPhylum)) return;

            SelectedReferenceSource.PhylumId = SelectedPhylum.PhylumId;

            try
            {
                var reference = _uow.Tbl90References.GetById(SelectedReferenceSource.ReferenceId);
                if (SelectedReferenceSource.ReferenceId != 0)
                {
                    if (reference != null) //update
                    {
                        reference.RefSourceId = SelectedReferenceSource.RefSourceId;
                        reference.PhylumId = SelectedReferenceSource.PhylumId;
                        reference.Valid = SelectedReferenceSource.Valid;
                        reference.ValidYear = SelectedReferenceSource.ValidYear;
                        reference.Info = SelectedReferenceSource.Info;
                        reference.Updater = Environment.UserName;
                        reference.UpdaterDate = DateTime.Now;
                        reference.Memo = SelectedReferenceSource.Memo;
                    }
                }
                else
                {
                    reference = new Tbl90Reference //add new
                    {
                        RefSourceId = SelectedReferenceSource.RefSourceId,
                        PhylumId = SelectedReferenceSource.PhylumId,
                        CountId = RandomHelper.Randomnumber(),
                        Valid = SelectedReferenceSource.Valid,
                        ValidYear = SelectedReferenceSource.ValidYear,
                        Info = SelectedReferenceSource.Info,
                        Memo = SelectedReferenceSource.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                    };
                }

                try
                {
                    if (SelectedReferenceSource.ReferenceId != 0) //update
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
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, "Error");
                    //         Log.Error(e);
                    return;
                }
                _allMessageBoxes.InfoMessageBox("SaveSuccess", SelectedReferenceSource.RefSourceId == 0
                    ? "DatasetNew"
                    : SelectedReferenceSource.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, "Error");
                //         Log.Error(e);
            }

            ReferenceSourcesCollection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.PhylumId == SelectedPhylum.PhylumId && x.RefAuthorId == null && x.RefExpertId == null));

            RaisePropertyChanged("ReferenceSourcesCollection");
        }
        private ObservableCollection<Tbl90Reference> SearchNameReturnSourcesCollection(string searchName)
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

        //-----------------------------------------

        #region [Commands Regnum ==> Tbl90Reference Author]

        private RelayCommand _getAuthorsByNameOrIdCommand;

        public ICommand GetAuthorsByNameOrIdCommand => _getAuthorsByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetAuthorsByNameOrId(SearchAuthorName); });
        private RelayCommand _addAuthorCommand;
        public ICommand AddAuthorCommand => _addAuthorCommand ??= new RelayCommand(delegate { ExecuteAddAuthor(null); });
        private RelayCommand _copyAuthorCommand;
        public ICommand CopyAuthorCommand => _copyAuthorCommand ??= new RelayCommand(delegate { ExecuteCopyAuthor(null); });
        private RelayCommand _deleteAuthorCommand;
        public ICommand DeleteAuthorCommand => _deleteAuthorCommand ??= new RelayCommand(delegate { ExecuteDeleteAuthor(null); });
        private RelayCommand _saveAuthorCommand;
        public ICommand SaveAuthorCommand => _saveAuthorCommand ??= new RelayCommand(delegate { ExecuteSaveAuthor(null); });


        #endregion

        #region[Methods Reference Author]

        private void ExecuteGetAuthorsByNameOrId(string searchName)
        {
            ReferenceAuthorsCollection = SearchNameReturnAuthorsCollection(searchName);

            RaisePropertyChanged("ReferenceAuthorsCollection");
        }
        private void ExecuteAddAuthor(object o)
        {
            if (_genPhylumMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedPhylum)) return;

            ReferenceAuthorsCollection.Insert(0, new Tbl90Reference() { PhylumId = SelectedPhylum.PhylumId, Info = "NewDataset" });
            RaisePropertyChanged("ReferenceAuthorsCollection");
        }
        private void ExecuteCopyAuthor(object o)
        {
            if (_genAuthorMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedReferenceAuthor)) return;

            if (SelectedReferenceAuthor != null)
            {
                var reference = _uow.Tbl90References.GetById(SelectedReferenceAuthor.ReferenceId);

                ReferenceAuthorsCollection.Insert(0, new Tbl90Reference()
                {
                    PhylumId = reference.PhylumId,
                    RefAuthorId = reference.RefAuthorId,
                    Valid = reference.Valid,
                    ValidYear = reference.ValidYear,
                    Info = "NewDataset",
                    Memo = reference.Memo
                });
            }

            RaisePropertyChanged("ReferenceAuthorsCollection");
        }
        private void ExecuteDeleteAuthor(object o)
        {
            if (_genAuthorMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedReferenceAuthor)) return;

            try
            {
                var reference = _uow.Tbl90References.GetById(SelectedReferenceAuthor.ReferenceId);
                if (reference != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("DeleteQuestion" + " " + SelectedReferenceAuthor.Info)) return;

                    _uow.Tbl90References.Remove(reference);
                    _uow.Complete();

                    _allMessageBoxes.InfoMessageBox("DeleteSuccess", SelectedReferenceAuthor.Info);
                }
                else
                {
                    _allMessageBoxes.InfoMessageBox("Not To Delete", "DeleteCan" + " " + SelectedReferenceAuthor.Info + " " + "DeleteCan1");
                }
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, "Error");
                //         Log.Error(e);
            }

            ReferenceAuthorsCollection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.PhylumId == SelectedPhylum.PhylumId && x.RefSourceId == null && x.RefExpertId == null));

            RaisePropertyChanged("ReferenceAuthorsCollection");
        }
        private void ExecuteSaveAuthor(object o)
        {
            if (_genPhylumMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedPhylum)) return;

            SelectedReferenceAuthor.PhylumId = SelectedPhylum.PhylumId;

            try
            {
                var reference = _uow.Tbl90References.GetById(SelectedReferenceAuthor.ReferenceId);
                if (SelectedReferenceAuthor.ReferenceId != 0)
                {
                    if (reference != null) //update
                    {
                        reference.RefAuthorId = SelectedReferenceAuthor.RefAuthorId;
                        reference.PhylumId = SelectedReferenceAuthor.PhylumId;
                        reference.Valid = SelectedReferenceAuthor.Valid;
                        reference.ValidYear = SelectedReferenceAuthor.ValidYear;
                        reference.Info = SelectedReferenceAuthor.Info;
                        reference.Updater = Environment.UserName;
                        reference.UpdaterDate = DateTime.Now;
                        reference.Memo = SelectedReferenceAuthor.Memo;
                    }
                }
                else
                {
                    reference = new Tbl90Reference //add new
                    {
                        RefAuthorId = SelectedReferenceAuthor.RefAuthorId,
                        PhylumId = SelectedReferenceAuthor.PhylumId,
                        CountId = RandomHelper.Randomnumber(),
                        Valid = SelectedReferenceAuthor.Valid,
                        ValidYear = SelectedReferenceAuthor.ValidYear,
                        Info = SelectedReferenceAuthor.Info,
                        Memo = SelectedReferenceAuthor.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                    };
                }

                try
                {
                    if (SelectedReferenceAuthor.ReferenceId != 0) //update
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
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, "Error");
                    //         Log.Error(e);
                    return;
                }
                _allMessageBoxes.InfoMessageBox("SaveSuccess", SelectedReferenceAuthor.RefSourceId == 0
                    ? "DatasetNew"
                    : SelectedReferenceAuthor.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, "Error");
                //         Log.Error(e);
            }

            ReferenceAuthorsCollection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.PhylumId == SelectedPhylum.PhylumId && x.RefSourceId == null && x.RefExpertId == null));

            RaisePropertyChanged("ReferenceAuthorsCollection");
        }
        private ObservableCollection<Tbl90Reference> SearchNameReturnAuthorsCollection(string searchName)
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

        //-----------------------------------------
        #region [Commands Regnum ==> Tbl93Comment]

        private RelayCommand _getCommentsByNameOrIdCommand;
        public ICommand GetCommentsByNameOrIdCommand => _getCommentsByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetCommentsByNameOrId(SearchCommentName); });
        private RelayCommand _addCommentCommand;
        public ICommand AddCommentCommand => _addCommentCommand ??= new RelayCommand(delegate { ExecuteAddComment(null); });
        private RelayCommand _copyCommentCommand;
        public ICommand CopyCommentCommand => _copyCommentCommand ??= new RelayCommand(delegate { ExecuteCopyComment(null); });
        private RelayCommand _deleteCommentCommand;
        public ICommand DeleteCommentCommand => _deleteCommentCommand ??= new RelayCommand(delegate { ExecuteDeleteComment(null); });
        private RelayCommand _saveCommentCommand;
        public ICommand SaveCommentCommand => _saveCommentCommand ??= new RelayCommand(delegate { ExecuteSaveComment(null); });


        #endregion

        #region [Methods Comment]

        private void ExecuteGetCommentsByNameOrId(string searchName)
        {
            CommentsCollection = SearchNameReturnCommentsCollection(searchName);
            RaisePropertyChanged("CommentsCollection");
        }
        private void ExecuteAddComment(object o)
        {
            if (_genPhylumMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedPhylum)) return;

            CommentsCollection.Insert(0, new Tbl93Comment() { PhylumId = SelectedPhylum.PhylumId, Info = "NewDataset" });
            RaisePropertyChanged("CommentsCollection");
        }
        private void ExecuteCopyComment(object o)
        {
            if (_genCommentMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedComment)) return;

            if (SelectedComment != null)
            {
                var comment = _uow.Tbl93Comments.GetById(SelectedComment.CommentId);

                CommentsCollection.Insert(0, new Tbl93Comment()
                {
                    PhylumId = comment.PhylumId,
                    Valid = comment.Valid,
                    ValidYear = comment.ValidYear,
                    Info = "NewDataset",
                    Memo = comment.Memo
                });
            }

            RaisePropertyChanged("CommentsCollection");
        }
        private void ExecuteDeleteComment(object o)
        {
            //  CommentsCollection = _crudCom.DeleteComment(SelectedRegnum, SelectedComment, CommentsCollection);
            if (_genCommentMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedComment)) return;

            try
            {
                var comment = _uow.Tbl93Comments.GetById(SelectedComment.CommentId);
                if (comment != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("DeleteQuestion" + " " + SelectedComment.Info)) return;

                    _uow.Tbl93Comments.Remove(comment);
                    _uow.Complete();

                    _allMessageBoxes.InfoMessageBox("DeleteSuccess", SelectedComment.Info);
                }
                else
                {
                    _allMessageBoxes.InfoMessageBox("Not To Delete", "DeleteCan" + " " + SelectedComment.Info + " " + "DeleteCan1");
                }
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, "Error");
                //         Log.Error(e);
            }
            CommentsCollection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(x => x.PhylumId == SelectedPhylum.PhylumId));

            RaisePropertyChanged("CommentsCollection");
        }
        private void ExecuteSaveComment(object o)
        {
            if (_genCommentMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedComment)) return;

            SelectedComment.PhylumId = SelectedPhylum.PhylumId;

            try
            {
                var comment = _uow.Tbl93Comments.GetById(SelectedComment.CommentId);
                if (SelectedComment.CommentId != 0)
                {
                    if (comment != null) //update
                    {
                        comment.PhylumId = SelectedComment.PhylumId;
                        comment.Valid = SelectedComment.Valid;
                        comment.ValidYear = SelectedComment.ValidYear;
                        comment.Info = SelectedComment.Info;
                        comment.Updater = Environment.UserName;
                        comment.UpdaterDate = DateTime.Now;
                        comment.Memo = SelectedComment.Memo;
                    }
                }
                else
                {
                    comment = new Tbl93Comment() //add new
                    {
                        PhylumId = SelectedComment.PhylumId,
                        CountId = RandomHelper.Randomnumber(),
                        Valid = SelectedComment.Valid,
                        ValidYear = SelectedComment.ValidYear,
                        Info = SelectedComment.Info,
                        Memo = SelectedComment.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                    };
                }

                try
                {
                    if (SelectedComment.CommentId != 0) //update
                    {
                        if (comment != null) _uow.Tbl93Comments.Update(comment);
                    }
                    else                                //add
                    if (comment != null) _uow.Tbl93Comments.Add(comment);

                    _uow.Complete();

                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(), "FailedToSave");
                    //     Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, "Error");
                    //         Log.Error(e);
                    return;
                }
                _allMessageBoxes.InfoMessageBox("SaveSuccess", SelectedComment.CommentId == 0
                    ? "DatasetNew"
                    : SelectedComment.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, "Error");
                //         Log.Error(e);
            }

            CommentsCollection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(x => x.PhylumId == SelectedPhylum.PhylumId));

            RaisePropertyChanged("CommentsCollection");
        }
        private ObservableCollection<Tbl93Comment> SearchNameReturnCommentsCollection(string searchName)
        {
            var collection = new ObservableCollection<Tbl93Comment>();

            switch (searchName)
            {
                case "":
                    return collection;
                case "*":
                    collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.GetAll());
                    break;
                default:
                    collection = int.TryParse(searchName, out var id)
                        ? new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                            .Find(e => e.CommentId == id))
                        : new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                            .Find(e => e.Info.StartsWith(searchName))
                        );
                    break;
            }

            return collection;
        }

        #endregion

        //-----------------------------------------


        #region [Selected Tab]

        private int _tabIndexMain;
        private int _tabIndexDetail;

        public int TabIndexMain
        {
            get => _tabIndexMain;
            set
            {
                if (value == _tabIndexMain) return;
                _tabIndexMain = value;
                RaisePropertyChanged("TabIndexMain");
                if (_tabIndexMain == 0)
                    TabIndexDetail = 0;
            }
        }
        public int TabIndexDetail
        {
            get => _tabIndexDetail;
            set
            {
                if (value == _tabIndexDetail) return;
                _tabIndexDetail = value;
                RaisePropertyChanged("TabIndexDetail");
                if (_tabIndexDetail == 0)
                    TabIndexMain = 0;
                if (_tabIndexDetail == 1)
                {
                    TabIndexMain = 0;
                    if (_selectedPhylum != null) GetReferenceExperts(_selectedPhylum.PhylumId);
                    if (_selectedPhylum != null) GetReferenceSources(_selectedPhylum.PhylumId);
                    if (_selectedPhylum != null) GetReferenceAuthors(_selectedPhylum.PhylumId);
                    if (_selectedPhylum != null) GetComments(_selectedPhylum.PhylumId);
                }

                if (_tabIndexDetail == 2)
                    TabIndexMain = 0;
                if (_tabIndexDetail == 3)
                    TabIndexMain = 0;
                if (_tabIndexDetail == 4)
                    TabIndexMain = 0;
            }
        }
        #endregion

        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _openRegnumsCrudCommand;
        public ICommand OpenRegnumsCrudCommand => _openRegnumsCrudCommand ??= new RelayCommand(delegate { OpenRegnumsCrud(RegnumsCollection); });

        private void OpenRegnumsCrud(object regnumsCollection)
        {
            RegnumsCollection = new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums
                .Find(e => e.RegnumId == SelectedRegnum.RegnumId)
                .OrderBy(a => a.RegnumName));

            ////var test1View = new Test1() { DataContext = typeof(Test1WindowViewModel) };
            //  var test1View = new Test1();
            //test1View.Show();

            //var view = new Test1Window() { DataContext = typeof(RegnumsViewModel) };
            ////      view.DataContext = new RegnumsViewModel();
            //view.Title = "Test1View";
            //view.Width = 820;
            //view.Height = 620;
            //view.Show();

        }


        #endregion "Public Commands Connected Tables by DoubleClick"


        #region [ Properties ]

        #region Public Properties Tbl03Regnum

        public ObservableCollection<Tbl03Regnum> RegnumsCollection { get; set; }
        public ObservableCollection<Tbl03Regnum> RegnumsAllCollection { get; set; }

        Tbl03Regnum _selectedRegnum;
        public Tbl03Regnum SelectedRegnum
        {
            get => _selectedRegnum;
            set
            {
                _selectedRegnum = value;
                RaisePropertyChanged("SelectedRegnum");
                SubphylumsCollection.Clear();
                SuperclassesCollection.Clear();
            }
        }

        #endregion Public Properties Regnum

        #region Public Properties Tbl06Phylum

        public string SearchPhylumName { get; set; }

        public ObservableCollection<Tbl06Phylum> PhylumsCollection { get; set; }
        public ObservableCollection<Tbl06Phylum> PhylumsAllCollection { get; set; }

        Tbl06Phylum _selectedPhylum = null;
        public Tbl06Phylum SelectedPhylum
        {
            get => _selectedPhylum;
            set
            {
                _selectedPhylum = value;
                RaisePropertyChanged("SelectedPhylum");
                if (_selectedPhylum != null)
                {
                    GetRegnums(_selectedPhylum.RegnumId);
                    GetSubphylums(_selectedPhylum.PhylumId);
                }
                TabIndexDetail = 2;

                SuperclassesCollection.Clear();

                if (_selectedPhylum != null) GetReferenceExperts(_selectedPhylum.PhylumId);
                if (_selectedPhylum != null) GetReferenceSources(_selectedPhylum.PhylumId);
                if (_selectedPhylum != null) GetReferenceAuthors(_selectedPhylum.PhylumId);
                if (_selectedPhylum != null) GetComments(_selectedPhylum.PhylumId);

            }
        }

        #endregion "Public Properties"

        #region Public Properties Tbl12Subphylum
        public ObservableCollection<Tbl12Subphylum> SubphylumsCollection { get; set; }
        public ObservableCollection<Tbl12Subphylum> SubphylumsAllCollection { get; set; }

        Tbl12Subphylum _selectedSubphylum = null;
        public Tbl12Subphylum SelectedSubphylum
        {
            get => _selectedSubphylum;
            set
            {
                _selectedSubphylum = value;
                RaisePropertyChanged("SelectedPhylum");
                if (_selectedSubphylum != null)
                {
                    GetSuperclasses(_selectedSubphylum.SubphylumId);
                }
            }
        }

        #endregion Public Properties     

        #region Public Properties Tbl18Superclass
        public ObservableCollection<Tbl18Superclass> SuperclassesCollection { get; set; }

        #endregion Public Properties     


        #region "Public Properties Tbl90Expert"

        public ObservableCollection<Tbl90RefExpert> ExpertsCollection { get; set; }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Source"

        public ObservableCollection<Tbl90RefSource> SourcesCollection { get; set; }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Author"
        public ObservableCollection<Tbl90RefAuthor> AuthorsCollection { get; set; }

        #endregion "Public Properties "

        #region Public Properties Tbl90Reference

        public ObservableCollection<Tbl90Reference> ReferencesCollection { get; set; }


        #endregion

        #region Public Properties Tbl90ReferenceExpert

        public string SearchExpertName { get; set; }

        Tbl90Reference _selectedReferenceExpert = null;
        public Tbl90Reference SelectedReferenceExpert
        {
            get => _selectedReferenceExpert;
            set
            {
                _selectedReferenceExpert = value;
                RaisePropertyChanged("SelectedReferenceExpert");
                //if (_selectedReferenceExpert != null)
                //{
                //    GetReferenceExperts(_selectedReferenceExpert.ReferenceId);
                //}
            }
        }


        public ObservableCollection<Tbl90Reference> ReferenceExpertsCollection { get; set; }

        #endregion "Public Properties"   


        #region "Public Properties Tbl90ReferenceSource"
        public string SearchSourceName { get; set; }


        Tbl90Reference _selectedReferenceSource = null;
        public Tbl90Reference SelectedReferenceSource
        {
            get => _selectedReferenceSource;
            set
            {
                _selectedReferenceSource = value;
                RaisePropertyChanged("SelectedReferenceSource");
                //if (_selectedReferenceSource != null)
                //{
                //    GetReferenceSources(_selectedReferenceSource.ReferenceId);
                //}
            }
        }
        public ObservableCollection<Tbl90Reference> ReferenceSourcesCollection { get; set; }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceAuthor"

        public string SearchAuthorName { get; set; }

        Tbl90Reference _selectedReferenceAuthor = null;

        public Tbl90Reference SelectedReferenceAuthor
        {
            get => _selectedReferenceAuthor;
            set
            {
                _selectedReferenceAuthor = value;
                RaisePropertyChanged("SelectedReferenceAuthor");
                //if (_selectedReferenceAuthor != null)
                //{
                //    GetReferenceAuthors(_selectedReferenceAuthor.ReferenceId);
                //}
            }
        }
        public ObservableCollection<Tbl90Reference> ReferenceAuthorsCollection { get; set; }

        #endregion "Public Properties"

        #region Public Properties Tbl93Comment

        public string SearchCommentName { get; set; }

        public ObservableCollection<Tbl93Comment> CommentsCollection { get; set; }

        Tbl93Comment _selectedComment = null;

        public Tbl93Comment SelectedComment
        {
            get => _selectedComment;
            set
            {
                _selectedComment = value;
                RaisePropertyChanged("SelectedComment");
            }
        }

        #endregion "Public Properties"

        #endregion

    }
}
