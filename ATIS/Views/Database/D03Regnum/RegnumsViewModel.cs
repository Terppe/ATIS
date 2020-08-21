using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using ATIS.Ui.Views.Database.CrudHelper;
using ATIS.Ui.Views.Database.DatabaseHelper;
using ATIS.Ui.Views.Database.SearchMethods;
using Microsoft.EntityFrameworkCore;

namespace ATIS.Ui.Views.Database.D03Regnum
{
    public class RegnumsViewModel : ViewModelBase
    {
        // Version with Generic Unit Of Work and AtisDbContext for general use

        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();

        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private readonly GenericMessageBoxes<Tbl03Regnum> _genRegnumMessageBoxes = new GenericMessageBoxes<Tbl03Regnum>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genExpertMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genSourceMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genAuthorMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl93Comment> _genCommentMessageBoxes = new GenericMessageBoxes<Tbl93Comment>();
        private readonly BasicGet _extGet = new BasicGet();
        private readonly BasicCopy _extCopy = new BasicCopy();

        #region [ Constructor ]

        public RegnumsViewModel()
        {
            LoadCollections();
        }

        private void LoadCollections()
        {
            RegnumsAllCollection = new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums.GetAll());
            PhylumsAllCollection = new ObservableCollection<Tbl06Phylum>(_uow.Tbl06Phylums.GetAll());
            DivisionsAllCollection = new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions.GetAll());

            RegnumsCollection = new ObservableCollection<Tbl03Regnum>();
            PhylumsCollection = new ObservableCollection<Tbl06Phylum>();
            DivisionsCollection = new ObservableCollection<Tbl09Division>();
            SubphylumsCollection = new ObservableCollection<Tbl12Subphylum>();
            SubdivisionsCollection = new ObservableCollection<Tbl15Subdivision>();
            ReferencesCollection = new ObservableCollection<Tbl90Reference>();
            ReferenceExpertsCollection = new ObservableCollection<Tbl90Reference>();
            ReferenceSourcesCollection = new ObservableCollection<Tbl90Reference>();
            ReferenceAuthorsCollection = new ObservableCollection<Tbl90Reference>();
            ExpertsCollection = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());
            SourcesCollection = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());
            AuthorsCollection = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

            CommentsCollection = new ObservableCollection<Tbl93Comment>();
            TabIndexDetail = 1;

        }

        #endregion

        //-----------------------------------------

        #region [Commands Regnum]

        private RelayCommand _getByNameOrIdCommand;
        public ICommand GetRegnumsByNameOrIdCommand => _getByNameOrIdCommand ??=
            new RelayCommand(delegate { ExecuteGetRegnumsByNameOrId(SearchRegnumName); });
        private RelayCommand _addRegnumCommand;
        public ICommand AddRegnumCommand => _addRegnumCommand ??= new RelayCommand(delegate { ExecuteAddRegnum(null); });
        private RelayCommand _copyRegnumCommand;
        public ICommand CopyRegnumCommand => _copyRegnumCommand ??= new RelayCommand(delegate { ExecuteCopyRegnum(null); });
        private RelayCommand _deleteRegnumCommand;
        public ICommand DeleteRegnumCommand =>
            _deleteRegnumCommand ??= new RelayCommand(delegate { ExecuteDeleteRegnum(SearchRegnumName); });
        private RelayCommand _saveRegnumCommand;
        public ICommand SaveRegnumCommand =>
            _saveRegnumCommand ??= new RelayCommand(delegate { ExecuteSaveRegnum(SearchRegnumName); });

        #endregion

        #region [Methods Regnum]

        private void ExecuteGetRegnumsByNameOrId(string searchName)
        {
            TabIndexDetail = 1;

            //PhylumsCollection.Clear();
            //DivisionsCollection.Clear();
            //SubphylumsCollection.Clear();
            //SubdivisionsCollection.Clear();
            //ReferencesCollection.Clear();
            //ReferenceExpertsCollection.Clear();
            //ReferenceSourcesCollection.Clear();
            //ReferenceAuthorsCollection.Clear();
            //CommentsCollection.Clear();

            RegnumsCollection = _extGet.SearchNameAndIdReturnCollection<Tbl03Regnum>(searchName, "regnum");
            RaisePropertyChanged("RegnumsCollection");
        }
        private void ExecuteAddRegnum(object o)
        {
            RegnumsCollection.Insert(0, new Tbl03Regnum { RegnumName = CultRes.StringsRes.DatasetNew });
            RaisePropertyChanged("RegnumsCollection");
        }
        private void ExecuteCopyRegnum(object o)
        {
            if (_genRegnumMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedRegnum)) return;

            RegnumsCollection = _extCopy.CopyRegnum(SelectedRegnum);
            RaisePropertyChanged("RegnumsCollection");

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment
        }
        private void ExecuteDeleteRegnum(string searchName)
        {
            if (_genRegnumMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedRegnum)) return;

            //check if in Tbl06Phylums or Tbl09Divisions connected datasets no delete, Expert, Sources, authors and Comment delete and than return

            PhylumsCollection = new ObservableCollection<Tbl06Phylum>(_uow.Tbl06Phylums.Find(x => x.RegnumId == SelectedRegnum.RegnumId));
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(PhylumsCollection.Count, CultRes.StringsRes.Phylum)) return;

            DivisionsCollection = new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions.Find(x => x.RegnumId == SelectedRegnum.RegnumId));
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(DivisionsCollection.Count, CultRes.StringsRes.Division)) return;

            //Delete all References Expert, Source, Authors  ----------------------------------------------------

            ReferencesCollection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.RegnumId == SelectedRegnum.RegnumId));
            if (ReferencesCollection.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                foreach (var t in ReferencesCollection)
                {
                    _uow.Tbl90References.Remove(t);
                }
                _uow.Complete();

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            CommentsCollection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.RegnumId == SelectedRegnum.RegnumId));
            if (CommentsCollection.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                foreach (var t in CommentsCollection)
                {
                    _uow.Tbl93Comments.Remove(t);
                }
                _uow.Complete();

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }

            if (false) return;
            {
                try
                {
                    var regnum = _uow.Tbl03Regnums.GetById(SelectedRegnum.RegnumId);
                    if (regnum != null)
                    {
                        if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion1 + " " + SelectedRegnum.RegnumName + " " + SelectedRegnum.Subregnum)) return;

                        _uow.Tbl03Regnums.Remove(regnum);
                        _uow.Complete();


                        _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, SelectedRegnum.RegnumName + " " + SelectedRegnum.Subregnum);
                    }
                    else
                    {
                        _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + SelectedRegnum.RegnumName + " " + SelectedRegnum.Subregnum + " " + CultRes.StringsRes.DeleteCan1);
                    }
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                }
            }
            ExecuteGetRegnumsByNameOrId(searchName);

            RaisePropertyChanged("RegnumsCollection");
        }
        private void ExecuteSaveRegnum(string searchName)
        {
            if (_genRegnumMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedRegnum)) return;

            try
            {
                var regnum = _uow.Tbl03Regnums.GetById(SelectedRegnum.RegnumId);
                if (SelectedRegnum.RegnumId != 0)
                {
                    if (regnum != null) //update
                    {
                        regnum.RegnumName = _selectedRegnum.RegnumName;
                        regnum.Subregnum = _selectedRegnum.Subregnum;
                        regnum.Valid = _selectedRegnum.Valid;
                        regnum.ValidYear = _selectedRegnum.ValidYear;
                        regnum.Author = _selectedRegnum.Author;
                        regnum.AuthorYear = _selectedRegnum.AuthorYear;
                        regnum.Info = _selectedRegnum.Info;
                        regnum.Synonym = _selectedRegnum.Synonym;
                        regnum.EngName = _selectedRegnum.EngName;
                        regnum.GerName = _selectedRegnum.GerName;
                        regnum.FraName = _selectedRegnum.FraName;
                        regnum.PorName = _selectedRegnum.PorName;
                        regnum.Memo = _selectedRegnum.Memo;
                        regnum.Updater = Environment.UserName;
                        regnum.UpdaterDate = DateTime.Now;
                    }
                }
                else
                {
                    regnum = new Tbl03Regnum() //add new
                    {
                        RegnumName = _selectedRegnum.RegnumName,
                        Subregnum = _selectedRegnum.Subregnum,
                        CountId = RandomHelper.Randomnumber(),
                        Valid = _selectedRegnum.Valid,
                        ValidYear = _selectedRegnum.ValidYear,
                        Author = _selectedRegnum.Author,
                        AuthorYear = _selectedRegnum.AuthorYear,
                        Info = _selectedRegnum.Info,
                        Synonym = _selectedRegnum.Synonym,
                        EngName = _selectedRegnum.EngName,
                        GerName = _selectedRegnum.GerName,
                        FraName = _selectedRegnum.FraName,
                        PorName = _selectedRegnum.PorName,
                        Memo = _selectedRegnum.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                    };
                }

                try
                {
                    if (SelectedRegnum.RegnumId != 0) //update
                        _uow.Tbl03Regnums.Update(regnum);
                    else                            //add
                        _uow.Tbl03Regnums.Add(regnum);

                    _uow.Complete();

                    RaisePropertyChanged("RegnumsCollection");

                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    //     Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }
                _allMessageBoxes.InfoMessageBox("SaveSuccess", SelectedRegnum.RegnumId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : SelectedRegnum.RegnumName + " " + SelectedRegnum.Subregnum);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                //         Log.Error(e);
            }
            ExecuteGetRegnumsByNameOrId(searchName);
        }

        //private void UpdateCollection()
        //{
        //    RegnumsCollection.Clear();
        //    foreach (var regnum in _uow.Tbl03Regnums.GetAll())
        //    {
        //        RegnumsCollection.Add(regnum);
        //    }
        //}

        private void GetPhylums(int regnumId)
        {
            var query = (from phylum in _context.Tbl06Phylums
                         where phylum.RegnumId == regnumId
                         select phylum).ToList();

            PhylumsCollection.Clear();
            foreach (var phylum in query)
            {
                if (phylum != null) PhylumsCollection.Add(phylum);
            }
        }
        private void GetDivisions(int regnumId)
        {
            var query = (from division in _context.Tbl09Divisions
                         where division.RegnumId == regnumId
                         select division).ToList();

            DivisionsCollection.Clear();
            foreach (Tbl09Division division in query)
            {
                if (division != null) DivisionsCollection.Add(division);
            }
        }
        private void GetSubphylums(int phylumId)
        {
            //   var _phylumId = int.Parse(phylumId.ToString());
            var query = (from subphylum in _context.Tbl12Subphylums
                         where subphylum.PhylumId == phylumId
                         select subphylum).ToList();

            SubphylumsCollection.Clear();

            foreach (Tbl12Subphylum phylum in query)
            {
                if (phylum != null) SubphylumsCollection.Add(phylum);
            }

            if (query.Count != 0)
                TabIndexDetail = 3;
        }
        private void GetSubdivisions(int divisionId)
        {
            //   var _phylumId = int.Parse(phylumId.ToString());
            var query = (from subdivision in _context.Tbl15Subdivisions
                         where subdivision.DivisionId == divisionId
                         select subdivision).ToList();

            SubdivisionsCollection.Clear();

            foreach (Tbl15Subdivision division in query)
            {
                if (division != null) SubdivisionsCollection.Add(division);
            }

            if (query.Count != 0)
                TabIndexDetail = 4;
        }
        private void GetReferenceExperts(int? regnumId)
        {
            var query = (from reference in _context.Tbl90References
                         where reference.RegnumId == regnumId && reference.RefSourceId == null && reference.RefAuthorId == null
                         select reference).ToList();

            ReferenceExpertsCollection.Clear();
            foreach (Tbl90Reference reference in query)
            {
                if (reference != null) ReferenceExpertsCollection.Add(reference);
            }
        }
        private void GetReferenceSources(int? regnumId)
        {
            var query = (from reference in _context.Tbl90References
                         where reference.RegnumId == regnumId && reference.RefExpertId == null && reference.RefAuthorId == null
                         select reference).ToList();

            ReferenceSourcesCollection.Clear();
            foreach (Tbl90Reference reference in query)
            {
                if (reference != null) ReferenceSourcesCollection.Add(reference);
            }
        }
        private void GetReferenceAuthors(int? regnumId)
        {
            var query = (from reference in _context.Tbl90References
                         where reference.RegnumId == regnumId && reference.RefExpertId == null && reference.RefSourceId == null
                         select reference).ToList();

            ReferenceAuthorsCollection.Clear();
            foreach (Tbl90Reference reference in query)
            {
                if (reference != null) ReferenceAuthorsCollection.Add(reference);
            }
        }
        private void GetComments(int regnumId)
        {
            var query = (from comment in _context.Tbl93Comments
                         where comment.RegnumId == regnumId
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

        #region [Commands Regnum ==> Tbl90Reference Expert]

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
            ReferenceExpertsCollection = _extGet.SearchNameAndIdReturnCollection<Tbl90Reference>(searchName, "expert");
            RaisePropertyChanged("ReferenceExpertsCollection");
        }
        private void ExecuteAddExpert(object o)
        {
            if (_genRegnumMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedRegnum)) return;

            ReferenceExpertsCollection.Insert(0, new Tbl90Reference() { RegnumId = SelectedRegnum.RegnumId, Info = CultRes.StringsRes.DatasetNew });
            RaisePropertyChanged("ReferenceExpertsCollection");
        }
        private void ExecuteCopyExpert(object o)
        {
            if (_genExpertMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedReferenceExpert)) return;

            ReferenceExpertsCollection = _extCopy.CopyReferenceRegnum(SelectedReferenceExpert, "Expert");
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
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + SelectedReferenceExpert.Info)) return;

                    _uow.Tbl90References.Remove(reference);
                    _uow.Complete();

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, SelectedReferenceExpert.Info);
                }
                else
                {
                    _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + SelectedReferenceExpert.Info + " " + CultRes.StringsRes.DeleteCan1);
                }
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                //         Log.Error(e);
            }

            ReferenceExpertsCollection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.RegnumId == SelectedRegnum.RegnumId && x.RefAuthorId == null && x.RefSourceId == null));

            RaisePropertyChanged("ReferenceExpertsCollection");
        }
        private void ExecuteSaveExpert(object o)
        {
            if (_genRegnumMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedRegnum)) return;

            SelectedReferenceExpert.RegnumId = SelectedRegnum.RegnumId;

            try
            {
                var reference = _uow.Tbl90References.GetById(SelectedReferenceExpert.ReferenceId);
                if (SelectedReferenceExpert.ReferenceId != 0)
                {
                    if (reference != null) //update
                    {
                        reference.RefExpertId = SelectedReferenceExpert.RefExpertId;
                        reference.RegnumId = SelectedReferenceExpert.RegnumId;
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
                        RegnumId = SelectedReferenceExpert.RegnumId,
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
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    //     Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }
                _allMessageBoxes.InfoMessageBox("SaveSuccess", SelectedReferenceExpert.RefExpertId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : SelectedReferenceExpert.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                //         Log.Error(e);
            }

            ReferenceExpertsCollection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.RegnumId == SelectedRegnum.RegnumId && x.RefAuthorId == null && x.RefSourceId == null));

            RaisePropertyChanged("ReferenceExpertsCollection");
        }

        #endregion

        //-----------------------------------------

        #region [Commands Regnum ==> Tbl90Reference Source]

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
            ReferenceSourcesCollection = _extGet.SearchNameAndIdReturnCollection<Tbl90Reference>(searchName, "source");
            RaisePropertyChanged("ReferenceSourcesCollection");
        }
        private void ExecuteAddSource(object o)
        {
            if (_genRegnumMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedRegnum)) return;

            ReferenceSourcesCollection.Insert(0, new Tbl90Reference() { RegnumId = SelectedRegnum.RegnumId, Info = CultRes.StringsRes.DatasetNew });
            RaisePropertyChanged("ReferenceSourcesCollection");
        }
        private void ExecuteCopySource(object o)
        {
            if (_genSourceMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedReferenceSource)) return;

            ReferenceSourcesCollection = _extCopy.CopyReferenceRegnum(SelectedReferenceExpert, "Source");
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
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + SelectedReferenceSource.Info)) return;

                    _uow.Tbl90References.Remove(reference);
                    _uow.Complete();

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, SelectedReferenceSource.Info);
                }
                else
                {
                    _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + SelectedReferenceSource.Info + " " + CultRes.StringsRes.DeleteCan1);
                }
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                //         Log.Error(e);
            }

            ReferenceSourcesCollection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.RegnumId == SelectedRegnum.RegnumId && x.RefAuthorId == null && x.RefExpertId == null));

            RaisePropertyChanged("ReferenceSourcesCollection");
        }
        private void ExecuteSaveSource(object o)
        {
            if (_genRegnumMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedRegnum)) return;

            SelectedReferenceSource.RegnumId = SelectedRegnum.RegnumId;

            try
            {
                var reference = _uow.Tbl90References.GetById(SelectedReferenceSource.ReferenceId);
                if (SelectedReferenceSource.ReferenceId != 0)
                {
                    if (reference != null) //update
                    {
                        reference.RefSourceId = SelectedReferenceSource.RefSourceId;
                        reference.RegnumId = SelectedReferenceSource.RegnumId;
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
                        RegnumId = SelectedReferenceSource.RegnumId,
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
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    //     Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }
                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, SelectedReferenceSource.RefSourceId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : SelectedReferenceSource.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                //         Log.Error(e);
            }

            ReferenceSourcesCollection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.RegnumId == SelectedRegnum.RegnumId && x.RefAuthorId == null && x.RefExpertId == null));

            RaisePropertyChanged("ReferenceSourcesCollection");
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
            ReferenceAuthorsCollection = _extGet.SearchNameAndIdReturnCollection<Tbl90Reference>(searchName, "author");
            RaisePropertyChanged("ReferenceAuthorsCollection");
        }
        private void ExecuteAddAuthor(object o)
        {
            if (_genRegnumMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedRegnum)) return;

            ReferenceAuthorsCollection.Insert(0, new Tbl90Reference() { RegnumId = SelectedRegnum.RegnumId, Info = CultRes.StringsRes.DatasetNew });
            RaisePropertyChanged("ReferenceAuthorsCollection");
        }
        private void ExecuteCopyAuthor(object o)
        {
            if (_genAuthorMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedReferenceAuthor)) return;

            ReferenceAuthorsCollection = _extCopy.CopyReferenceRegnum(SelectedReferenceAuthor, "Author");
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
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + SelectedReferenceAuthor.Info)) return;

                    _uow.Tbl90References.Remove(reference);
                    _uow.Complete();

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, SelectedReferenceAuthor.Info);
                }
                else
                {
                    _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + SelectedReferenceAuthor.Info + " " + CultRes.StringsRes.DeleteCan1);
                }
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                //         Log.Error(e);
            }

            ReferenceAuthorsCollection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.RegnumId == SelectedRegnum.RegnumId && x.RefSourceId == null && x.RefExpertId == null));

            RaisePropertyChanged("ReferenceAuthorsCollection");
        }
        private void ExecuteSaveAuthor(object o)
        {
            if (_genRegnumMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedRegnum)) return;

            SelectedReferenceAuthor.RegnumId = SelectedRegnum.RegnumId;

            try
            {
                var reference = _uow.Tbl90References.GetById(SelectedReferenceAuthor.ReferenceId);
                if (SelectedReferenceAuthor.ReferenceId != 0)
                {
                    if (reference != null) //update
                    {
                        reference.RefAuthorId = SelectedReferenceAuthor.RefAuthorId;
                        reference.RegnumId = SelectedReferenceAuthor.RegnumId;
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
                        RegnumId = SelectedReferenceAuthor.RegnumId,
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
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    //     Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }
                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, SelectedReferenceAuthor.RefSourceId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : SelectedReferenceAuthor.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                //         Log.Error(e);
            }

            ReferenceAuthorsCollection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                .Find(x => x.RegnumId == SelectedRegnum.RegnumId && x.RefSourceId == null && x.RefExpertId == null));

            RaisePropertyChanged("ReferenceAuthorsCollection");
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
            CommentsCollection = _extGet.SearchNameAndIdReturnCollection<Tbl93Comment>(searchName, "comment");
            RaisePropertyChanged("CommentsCollection");
        }
        private void ExecuteAddComment(object o)
        {
            if (_genRegnumMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedRegnum)) return;

            CommentsCollection.Insert(0, new Tbl93Comment() { RegnumId = SelectedRegnum.RegnumId, Info = CultRes.StringsRes.DatasetNew });
            RaisePropertyChanged("CommentsCollection");
        }
        private void ExecuteCopyComment(object o)
        {
            if (_genCommentMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedComment)) return;


            CommentsCollection = _extCopy.CopyComment(SelectedComment, "Regnum");
            RaisePropertyChanged("CommentsCollection");
        }
        private void ExecuteDeleteComment(object o)
        {
            if (_genCommentMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedComment)) return;

            try
            {
                var comment = _uow.Tbl93Comments.GetById(SelectedComment.CommentId);
                if (comment != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + SelectedComment.Info)) return;

                    _uow.Tbl93Comments.Remove(comment);
                    _uow.Complete();

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, SelectedComment.Info);
                }
                else
                {
                    _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + SelectedComment.Info + " " + CultRes.StringsRes.DeleteCan1);
                }
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                //         Log.Error(e);
            }
            CommentsCollection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(x => x.RegnumId == SelectedRegnum.RegnumId));

            RaisePropertyChanged("CommentsCollection");
        }
        private void ExecuteSaveComment(object o)
        {
            if (_genCommentMessageBoxes.NoDatasetSelectedInfoMessageBox(SelectedComment)) return;

            SelectedComment.RegnumId = SelectedRegnum.RegnumId;

            try
            {
                var comment = _uow.Tbl93Comments.GetById(SelectedComment.CommentId);
                if (SelectedComment.CommentId != 0)
                {
                    if (comment != null) //update
                    {
                        comment.RegnumId = SelectedComment.RegnumId;
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
                        RegnumId = SelectedComment.RegnumId,
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
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    //     Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }
                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, SelectedComment.CommentId == 0
                    ? "DatasetNew"
                    : SelectedComment.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                //         Log.Error(e);
            }

            CommentsCollection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(x => x.RegnumId == SelectedRegnum.RegnumId));

            RaisePropertyChanged("CommentsCollection");
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
                {
                    TabIndexDetail = 0;

                }
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
                    TabIndexMain = 0;
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

        //private RelayCommand _openRegnumsCrudCommand;
        //public ICommand OpenRegnumsCrudCommand => _openRegnumsCrudCommand ??= new RelayCommand(delegate { OpenRegnumsCrud(RegnumsCollection); });

        //private void OpenRegnumsCrud(object regnumsCollection)
        //{
        //    RegnumsCollection = new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums
        //        .Find(e => e.RegnumId == SelectedRegnum.RegnumId)
        //        .OrderBy(a => a.RegnumName));

        //    ////var test1View = new Test1() { DataContext = typeof(Test1WindowViewModel) };
        //    //  var test1View = new Test1();
        //    //test1View.Show();

        //    var view = new Test1Window() { DataContext = typeof(RegnumsViewModel) };
        //    //      view.DataContext = new RegnumsViewModel();
        //    view.Title = "Test1View";
        //    view.Width = 820;
        //    view.Height = 620;
        //    view.Show();

        //}


        #endregion "Public Commands Connected Tables by DoubleClick"

        #region [ Properties ]

        #region Public Properties Tbl03Regnum

        public string SearchRegnumName { get; set; }
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
                if (_selectedRegnum != null)
                {
                    switch (_selectedRegnum.RegnumName)
                    {
                        case "Animalia":
                            TabIndexDetail = 1;
                            GetPhylums(_selectedRegnum.RegnumId);
                            DivisionsCollection.Clear();
                            break;
                        case "Plantae":
                            TabIndexDetail = 2;
                            GetDivisions(_selectedRegnum.RegnumId);
                            PhylumsCollection.Clear();
                            break;
                        default:
                            {
                                TabIndexDetail = 1;
                                GetPhylums(_selectedRegnum.RegnumId);  //change evtl. Archaea, Protozoa
                                DivisionsCollection.Clear();
                                break;
                            }
                    }
                }

                SubphylumsCollection.Clear();
                SubdivisionsCollection.Clear();
                if (_selectedRegnum != null) GetReferenceExperts(_selectedRegnum.RegnumId);
                if (_selectedRegnum != null) GetReferenceSources(_selectedRegnum.RegnumId);
                if (_selectedRegnum != null) GetReferenceAuthors(_selectedRegnum.RegnumId);
                if (_selectedRegnum != null) GetComments(_selectedRegnum.RegnumId);
                //  ReferenceSourcesCollection.Clear();
            }
        }

        #endregion Public Properties Regnum

        #region Public Properties Tbl06Phylum

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
                    GetSubphylums(_selectedPhylum.PhylumId);
                }
            }
        }

        #endregion "Public Properties"     

        #region Public Properties Tbl09Division

        public ObservableCollection<Tbl09Division> DivisionsCollection { get; set; }
        public ObservableCollection<Tbl09Division> DivisionsAllCollection { get; set; }

        Tbl09Division _selectedDivision = null;
        public Tbl09Division SelectedDivision
        {
            get => _selectedDivision;
            set
            {
                _selectedDivision = value;
                RaisePropertyChanged("SelectedPhylum");
                if (_selectedDivision != null)
                {
                    GetSubdivisions(_selectedDivision.DivisionId);
                }
            }
        }

        #endregion Public Properties

        #region Public Properties Tbl12Subphylum
        public ObservableCollection<Tbl12Subphylum> SubphylumsCollection { get; set; }

        #endregion Public Properties     

        #region Public Properties Tbl15Subdivision

        public ObservableCollection<Tbl15Subdivision> SubdivisionsCollection { get; set; }

        #endregion "Public Properties"     

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
