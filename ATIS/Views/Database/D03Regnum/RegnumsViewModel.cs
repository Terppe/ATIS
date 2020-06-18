using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using Microsoft.EntityFrameworkCore;

namespace ATIS.Ui.Views.Database.D03Regnum
{
    public class RegnumsViewModel : ViewModelBase
    {
        public override string Name => "Regnum";

        #region [ Constructor ]

        public RegnumsViewModel()
        {
            LoadCollections();
        }

        private void LoadCollections()
        {
            RegnumsCollection = new ObservableCollection<Tbl03Regnum>();
            PhylumsCollection = new ObservableCollection<Tbl06Phylum>();
            DivisionsCollection = new ObservableCollection<Tbl09Division>();
            SubphylumsCollection = new ObservableCollection<Tbl12Subphylum>();
            SubdivisionsCollection = new ObservableCollection<Tbl15Subdivision>();
            ReferenceExpertsCollection = new ObservableCollection<Tbl90Reference>();
            ReferenceSourcesCollection = new ObservableCollection<Tbl90Reference>();
            ReferenceAuthorsCollection = new ObservableCollection<Tbl90Reference>();
            ExpertsCollection = new ObservableCollection<Tbl90RefExpert>(_db.Tbl90RefExperts.ToList());
            SourcesCollection = new ObservableCollection<Tbl90RefSource>(_db.Tbl90RefSources.ToList());
            AuthorsCollection = new ObservableCollection<Tbl90RefAuthor>(_db.Tbl90RefAuthors.ToList());
            CommentsCollection = new ObservableCollection<Tbl93Comment>();

            //   GetAuthorsCombo();
        }


        //AtisDbContext for general use
        private AtisDbContext _db = new AtisDbContext();


        #endregion

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
            TabIndexDetail = 0;
            RegnumsCollection = SearchNameReturnRegnumsCollection(searchName);
            RaisePropertyChanged("RegnumsCollection");
        }
        private void ExecuteAddRegnum(object o)
        {
            if (_selectedRegnum == null) //No dataset selected 
            {
                MessageBox.Show("Select Regnum",
                    "Required select",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (RegnumsCollection == null)
                RegnumsCollection = new ObservableCollection<Tbl03Regnum>();

            RegnumsCollection.Insert(0, new Tbl03Regnum { RegnumName = "NewDataset" });
        }
        private void ExecuteCopyRegnum(object o)
        {
            if (_selectedRegnum == null)
            {
                MessageBox.Show("NewDataset",
                    "RequiredInput",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var regnum = _db.Tbl03Regnums.FirstOrDefault(x => x.RegnumId == _selectedRegnum.RegnumId);

            RegnumsCollection.Insert(0, new Tbl03Regnum
            {
                RegnumName = "NewDataset",
                Subregnum = regnum.Subregnum,
                Valid = regnum.Valid,
                ValidYear = regnum.ValidYear,
                Synonym = regnum.Synonym,
                Author = regnum.Author,
                AuthorYear = regnum.AuthorYear,
                Info = regnum.Info,
                EngName = regnum.EngName,
                GerName = regnum.GerName,
                FraName = regnum.FraName,
                PorName = regnum.PorName,
                Memo = regnum.Memo
            });
        }
        private void ExecuteDeleteRegnum(string searchName)
        {
            if (_selectedRegnum == null)
            {
                MessageBox.Show("NewDataset",
                    "RequiredInput",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var ret = true;

            //check if in Tbl06Phylums or Tbl09Divisions connected datasets no delete, Expert, Sources, authors and Comment delete and than return
            PhylumsCollection = new ObservableCollection<Tbl06Phylum>(_db.Tbl06Phylums.Where(x => x.RegnumId == _selectedRegnum.RegnumId));
            if (PhylumsCollection.Count > 0)
            {
                MessageBox.Show("Not to Delete", "Phylum" + " " + "ConnectedDataset",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                ret = false;
            }

            DivisionsCollection = new ObservableCollection<Tbl09Division>(_db.Tbl09Divisions.Where(x => x.RegnumId == _selectedRegnum.RegnumId));
            if (DivisionsCollection.Count > 0)
            {
                MessageBox.Show("Not to Delete", "Division" + " " + "ConnectedDataset",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                ret = false;
            }

            //Delete all Expert, Source, Authors
            ReferencesCollection = new ObservableCollection<Tbl90Reference>(_db.Tbl90References.Where(a => a.RegnumId == _selectedRegnum.RegnumId));
            if (ReferencesCollection.Count > 0)
            {
                if (MessageBox.Show("Wollen Sie die Datensätze löschen ?", "Reference Author, Reference Source, Reference Expert" + " " + "ConnectedDataset",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) !=
                    MessageBoxResult.Yes)
                    return;

                foreach (var t in ReferencesCollection)
                {
                    _db.Tbl90References.Remove(t);
                }
                _db.SaveChanges();

                MessageBox.Show("DeleteSuccess", "Reference",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                ReferencesCollection = new ObservableCollection<Tbl90Reference>(_db.Tbl90References.Where(x => x.RegnumId == _selectedRegnum.RegnumId));

                RaisePropertyChanged("ReferencesCollection");

                ret = true;
            }

            // alternate seperate T
            //ReferenceExpertsCollection = new ObservableCollection<Tbl90Reference>(_db.Tbl90References.Where(
            //    x => x.RegnumId == _selectedRegnum.RegnumId && x.RefSourceId == null && x.RefAuthorId == null));
            //if (ReferenceExpertsCollection.Count > 0)
            //{
            //    if (MessageBox.Show("DeleteQuestion1", "DeleteQuestion" + " " + "ReferenceExpert",
            //            MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            //        return;

            //    foreach (var t in ReferenceExpertsCollection)
            //    {
            //        _db.Tbl90References.Remove(t);
            //    }

            //    _db.SaveChanges();

            //    MessageBox.Show("DeleteSuccess", "ReferenceExpert",
            //        MessageBoxButton.OK, MessageBoxImage.Information);

            //    ReferenceExpertsCollection = new ObservableCollection<Tbl90Reference>(_db.Tbl90References.Where(
            //        x => x.RegnumId == _selectedRegnum.RegnumId && x.RefSourceId == null && x.RefAuthorId == null));

            //    RaisePropertyChanged("ReferenceExpertsCollection");

            //    ret = true;
            //}

            //ReferenceAuthorsCollection = new ObservableCollection<Tbl90Reference>(_db.Tbl90References.Where(
            //    x => x.RegnumId == _selectedRegnum.RegnumId && x.RefSourceId == null && x.RefExpertId == null));
            //if (ReferenceAuthorsCollection.Count > 0)
            //{
            //    if (MessageBox.Show("DeleteQuestion1", "DeleteQuestion" + " " + "ReferenceAuthor",
            //            MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            //        return;

            //    foreach (var t in ReferenceAuthorsCollection)
            //    {
            //        _db.Tbl90References.Remove(t);
            //    }

            //    _db.SaveChanges();

            //    MessageBox.Show("DeleteSuccess", "ReferenceAuthor",
            //        MessageBoxButton.OK, MessageBoxImage.Information);

            //    ReferenceAuthorsCollection = new ObservableCollection<Tbl90Reference>(_db.Tbl90References.Where(
            //        x => x.RegnumId == _selectedRegnum.RegnumId && x.RefSourceId == null && x.RefExpertId == null));

            //    RaisePropertyChanged("ReferenceAuthorsCollection");

            //    ret = true;
            //}

            //ReferenceSourcesCollection = new ObservableCollection<Tbl90Reference>(_db.Tbl90References.Where(
            //    x => x.RegnumId == _selectedRegnum.RegnumId && x.RefAuthorId == null && x.RefExpertId == null));
            //if (ReferenceSourcesCollection.Count > 0)
            //{
            //    if (MessageBox.Show("DeleteQuestion1", "DeleteQuestion" + " " + "ReferenceSource",
            //            MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            //        return;

            //    foreach (var t in ReferenceSourcesCollection)
            //    {
            //        _db.Tbl90References.Remove(t);
            //    }

            //    _db.SaveChanges();

            //    MessageBox.Show("DeleteSuccess", "ReferenceSource",
            //        MessageBoxButton.OK, MessageBoxImage.Information);

            //    ReferenceSourcesCollection = new ObservableCollection<Tbl90Reference>(_db.Tbl90References.Where(
            //        x => x.RegnumId == _selectedRegnum.RegnumId && x.RefAuthorId == null && x.RefExpertId == null));

            //    RaisePropertyChanged("ReferenceSourcesCollection");

            //    ret = true;
            //}

            CommentsCollection = new ObservableCollection<Tbl93Comment>(_db.Tbl93Comments.Where(x => x.RegnumId == _selectedRegnum.RegnumId));
            if (CommentsCollection.Count > 0)
            {
                if (MessageBox.Show("DeleteQuestion1", "DeleteQuestion" + " " + "Comment",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                    return;

                foreach (var t in CommentsCollection)
                {
                    _db.Tbl93Comments.Remove(t);
                }

                _db.SaveChanges();

                MessageBox.Show("DeleteSuccess", "Comment",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                CommentsCollection = new ObservableCollection<Tbl93Comment>(_db.Tbl93Comments.Where(x => x.RegnumId == _selectedRegnum.RegnumId));
                RaisePropertyChanged("CommentsCollection");

                ret = true;
            }

            if (ret == false) return;
            {
                try
                {
                    var regnum = _db.Tbl03Regnums.FirstOrDefault(x => x.RegnumId == _selectedRegnum.RegnumId);
                    if (regnum != null)
                    {
                        if (MessageBox.Show("DeleteQuestion1", "DeleteQuestion" + " " + _selectedRegnum.RegnumName + " " + _selectedRegnum.Subregnum,
                                MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;

                        _db.Tbl03Regnums.Remove(regnum);

                        _db.SaveChanges();


                        MessageBox.Show("DeleteSuccess", _selectedRegnum.RegnumName + " " + _selectedRegnum.Subregnum,
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Information", "DeleteCan" + " " + _selectedRegnum.RegnumName + " " + _selectedRegnum.Subregnum + " " + "DeleteCan1",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    //         Log.Error(e);
                }
            }
            ExecuteGetRegnumsByNameOrId(searchName);
        }

        private void ExecuteSaveRegnum(string searchName)
        {
            if (_selectedRegnum == null) //No dataset selected
            {
                MessageBox.Show("NewDataset",
                    "RequiredInput",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var regnum = _db.Tbl03Regnums.FirstOrDefault(x => x.RegnumId == _selectedRegnum.RegnumId);
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

                if (_selectedRegnum.RegnumId != 0) //update
                    _db.Tbl03Regnums.Update(regnum);
                else //add
                    _db.Tbl03Regnums.Add(regnum);

                _db.SaveChanges();

                ExecuteGetRegnumsByNameOrId(searchName);
            }
        }
        public ObservableCollection<Tbl03Regnum> SearchNameReturnRegnumsCollection(string searchName)
        {
            var collection = new ObservableCollection<Tbl03Regnum>();

            switch (searchName)
            {
                case "":
                    return collection;
                case "*":
                    collection = new ObservableCollection<Tbl03Regnum>(_db.Tbl03Regnums.ToList());
                    break;
                default:
                    collection = int.TryParse(searchName, out var id)
                        ? new ObservableCollection<Tbl03Regnum>(_db.Tbl03Regnums
                            .Where(e => e.RegnumId == id))
                        : new ObservableCollection<Tbl03Regnum>(_db.Tbl03Regnums
                            .Where(e => e.RegnumName.StartsWith(searchName))
                            .OrderBy(a => a.RegnumName + a.Subregnum)
                        );
                    break;
            }

            return collection;
        }
        private void GetPhylums(int regnumId)
        {
            var query = (from phylum in _db.Tbl06Phylums
                         where phylum.RegnumId == regnumId
                         select phylum).ToList();

            PhylumsCollection.Clear();
            foreach (Tbl06Phylum phylum in query)
            {
                if (phylum != null) PhylumsCollection.Add(phylum);
            }
        }
        private void GetDivisions(int regnumId)
        {
            var query = (from division in _db.Tbl09Divisions
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
            var query = (from subphylum in _db.Tbl12Subphylums
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
            var query = (from subdivision in _db.Tbl15Subdivisions
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
            var query = (from reference in _db.Tbl90References
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
            var query = (from reference in _db.Tbl90References
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
            var query = (from reference in _db.Tbl90References
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
            var query = (from comment in _db.Tbl93Comments
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

        private void GetAuthorsCombo()
        {
            var query = from authors in _db.Tbl90RefAuthors
                        select new
                        {
                            authors.RefAuthorName,
                            authors.ArticelTitle
                        };

            ////     AuthorsCollection.Clear();
            //     foreach (var authors in query)
            //     {
            //          AuthorsCollection.Add(authors);
            //     }

        }
        //private void GetAuthorsCombo(string refAuthorName, string articelTitle, string bookName, string page1,
        //    string publisher, string publicationPlace)
        //{
        //    var query = (from authors in _db.Tbl90RefAuthors
        //        where authors.RefAuthorName == refAuthorName &&
        //              authors.ArticelTitle == articelTitle &&
        //              authors.BookName == bookName &&
        //              authors.Page1 == page1 &&
        //              authors.Publisher == publisher &&
        //              authors.PublicationPlace == publicationPlace
        //        select authors).ToList();

        //    AuthorsCollection.Clear();
        //    foreach (Tbl90RefAuthor authors in query)
        //    {
        //        if (authors != null) AuthorsCollection.Add(authors);
        //    }
        //}

        #endregion

        //-----------------------------------------

        #region [Commands Regnum ==> Tbl90Reference Expert]

        private RelayCommand _addExpertCommand;
        public ICommand AddExpertCommand => _addExpertCommand ??= new RelayCommand(delegate { ExecuteAddExpert(null); });
        private RelayCommand _copyExpertCommand;
        public ICommand CopyExpertCommand => _copyExpertCommand ??= new RelayCommand(delegate { ExecuteCopyExpert(null); });
        private RelayCommand _deleteExpertCommand;
        public ICommand DeleteExpertCommand => _deleteExpertCommand ??= new RelayCommand(delegate { ExecuteDeleteExpert(SearchRegnumName); });
        private RelayCommand _saveExpertCommand;
        public ICommand SaveExpertCommand => _saveExpertCommand ??= new RelayCommand(delegate { ExecuteSaveExpert(SearchRegnumName); });

        #endregion

        #region[Methods Reference Expert]

        private void ExecuteAddExpert(object o)
        {
            if (_selectedRegnum == null)
            {
                MessageBox.Show("Select Regnum",
                    "Required select",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (ReferenceExpertsCollection == null)
                ReferenceExpertsCollection = new ObservableCollection<Tbl90Reference>();

            ReferenceExpertsCollection.Insert(0, new Tbl90Reference() { RegnumId = _selectedRegnum.RegnumId, Info = "NewDataset" });
        }
        private void ExecuteCopyExpert(object o)
        {
            if (_selectedReferenceExpert == null)
            {
                MessageBox.Show("NewDataset",
                    "RequiredInput",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var reference = _db.Tbl90References.FirstOrDefault(x => x.ReferenceId == _selectedReferenceExpert.ReferenceId);

            ReferenceExpertsCollection.Insert(0, new Tbl90Reference()
            {
                RegnumId = reference.RegnumId,
                RefSourceId = reference.RefSourceId,
                Valid = reference.Valid,
                ValidYear = reference.ValidYear,
                Info = "NewDataset",
                Memo = reference.Memo
            });
        }
        private void ExecuteDeleteExpert(string searchName)
        {
            if (_selectedReferenceExpert == null)
            {
                MessageBox.Show("NewDataset",
                    "RequiredInput",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                var reference = _db.Tbl90References.FirstOrDefault(x => x.ReferenceId == _selectedReferenceExpert.ReferenceId);
                if (reference != null)
                {
                    if (MessageBox.Show("DeleteQuestion1", "DeleteQuestion" + " " + _selectedReferenceExpert.Info,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;

                    _db.Tbl90References.Remove(reference);

                    _db.SaveChanges();


                    MessageBox.Show("DeleteSuccess", _selectedReferenceExpert.Info,
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Information", "DeleteCan" + " " + _selectedReferenceExpert.Info + " " + "DeleteCan1",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                //         Log.Error(e);
            }

            ReferenceExpertsCollection = new ObservableCollection<Tbl90Reference>(_db.Tbl90References
                .Where(x => x.RegnumId == _selectedRegnum.RegnumId && x.RefAuthorId == null && x.RefSourceId == null));

            RaisePropertyChanged("ReferenceExpertsCollection");
        }
        private void ExecuteSaveExpert(string searchName)
        {
            if (_selectedReferenceExpert == null)
            {
                MessageBox.Show("NewDataset",
                    "RequiredInput",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _selectedReferenceExpert.RegnumId = _selectedRegnum.RegnumId;

            try
            {
                var reference = _db.Tbl90References.FirstOrDefault(x => x.ReferenceId == _selectedReferenceExpert.ReferenceId);
                if (_selectedReferenceExpert.ReferenceId != 0)
                {
                    if (reference != null) //update
                    {
                        reference.RefExpertId = _selectedReferenceExpert.RefExpertId;
                        reference.RegnumId = _selectedReferenceExpert.RegnumId;
                        reference.Valid = _selectedReferenceExpert.Valid;
                        reference.ValidYear = _selectedReferenceExpert.ValidYear;
                        reference.Info = _selectedReferenceExpert.Info;
                        reference.Updater = Environment.UserName;
                        reference.UpdaterDate = DateTime.Now;
                        reference.Memo = _selectedReferenceExpert.Memo;

                        //           reference.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    reference = new Tbl90Reference //add new
                    {
                        RefExpertId = _selectedReferenceExpert.RefExpertId,
                        RegnumId = _selectedReferenceExpert.RegnumId,
                        CountId = RandomHelper.Randomnumber(),
                        Valid = _selectedReferenceExpert.Valid,
                        ValidYear = _selectedReferenceExpert.ValidYear,
                        Info = _selectedReferenceExpert.Info,
                        Memo = _selectedReferenceExpert.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        //         EntityState = EntityState.Added
                    };
                }

                try
                {
                    if (_selectedReferenceExpert.ReferenceId != 0) //update
                    {
                        if (reference != null) _db.Tbl90References.Update(reference);
                    }
                    else                                //add
                    if (reference != null) _db.Tbl90References.Add(reference);

                    _db.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        MessageBox.Show(e.InnerException.ToString(), "FailedToSave",
                            MessageBoxButton.OK, MessageBoxImage.Warning);

                    //     Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    //   Log.Error(e);
                    return;
                }

                MessageBox.Show("SaveSuccess",
                    _selectedReferenceExpert.RefSourceId == 0
                        ? "DatasetNew"
                        : _selectedReferenceExpert.Info,
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                //         Log.Error(e);
            }

            ReferenceExpertsCollection = new ObservableCollection<Tbl90Reference>(_db.Tbl90References
                .Where(x => x.RegnumId == _selectedRegnum.RegnumId && x.RefAuthorId == null && x.RefSourceId == null));

            RaisePropertyChanged("ReferenceExpertsCollection");
        }

        #endregion

        //-----------------------------------------

        #region [Commands Regnum ==> Tbl90Reference Source]

        private RelayCommand _getSourcesByNameOrIdCommand;

        public ICommand GetSourcesByNameOrIdCommand => _getSourcesByNameOrIdCommand ??=
            new RelayCommand(delegate { ExecuteGetSourcesByNameOrId(SearchSourceName); });

        private RelayCommand _addSourceCommand;

        public ICommand AddSourceCommand => _addSourceCommand ??= new RelayCommand(delegate { ExecuteAddSource(null); });

        private RelayCommand _copySourceCommand;

        public ICommand CopySourceCommand => _copySourceCommand ??= new RelayCommand(delegate { ExecuteCopySource(null); });

        private RelayCommand _deleteSourceCommand;

        public ICommand DeleteSourceCommand =>
            _deleteSourceCommand ??= new RelayCommand(delegate { ExecuteDeleteSource(SearchRegnumName); });

        private RelayCommand _saveSourceCommand;

        public ICommand SaveSourceCommand =>
            _saveSourceCommand ??= new RelayCommand(delegate { ExecuteSaveSource(SearchRegnumName); });


        #endregion

        #region[Methods Reference Source]

        private void ExecuteGetSourcesByNameOrId(string searchName)
        {
            ReferenceSourcesCollection = SearchNameReturnSourcesCollection(searchName);
            RaisePropertyChanged("ReferenceSourcesCollection");
        }
        private void ExecuteAddSource(object o)
        {
            if (_selectedRegnum == null)
            {
                MessageBox.Show("Select Regnum",
                    "Required select",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (ReferenceSourcesCollection == null)
                ReferenceSourcesCollection = new ObservableCollection<Tbl90Reference>();

            ReferenceSourcesCollection.Insert(0, new Tbl90Reference() { RegnumId = _selectedRegnum.RegnumId, Info = "NewDataset" });
            //RaisePropertyChanged("ReferenceSourcesCollection");

            //ReferenceSourcesCollView = CollectionViewSource.GetDefaultView(ReferenceSourcesCollection);
            //ReferenceSourcesCollView.MoveCurrentToFirst();
        }
        private void ExecuteCopySource(object o)
        {
            if (_selectedReferenceSource == null)
            {
                MessageBox.Show("NewDataset",
                    "RequiredInput",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var reference = _db.Tbl90References.FirstOrDefault(x => x.ReferenceId == _selectedReferenceSource.ReferenceId);

            ReferenceSourcesCollection.Insert(0, new Tbl90Reference()
            {
                RegnumId = reference.RegnumId,
                RefSourceId = reference.RefSourceId,
                Valid = reference.Valid,
                ValidYear = reference.ValidYear,
                Info = "NewDataset",
                Memo = reference.Memo
            });
        }
        private void ExecuteDeleteSource(string searchName)
        {
            if (_selectedReferenceSource == null)
            {
                MessageBox.Show("NewDataset",
                    "RequiredInput",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                var reference = _db.Tbl90References.FirstOrDefault(x => x.ReferenceId == _selectedReferenceSource.ReferenceId);
                if (reference != null)
                {
                    if (MessageBox.Show("DeleteQuestion1", "DeleteQuestion" + " " + _selectedReferenceSource.Info,
                            MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;

                    _db.Tbl90References.Remove(reference);

                    _db.SaveChanges();


                    MessageBox.Show("DeleteSuccess", _selectedReferenceSource.Info,
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Information", "DeleteCan" + " " + _selectedReferenceSource.Info + " " + "DeleteCan1",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                //         Log.Error(e);
            }

            ReferenceExpertsCollection = new ObservableCollection<Tbl90Reference>(_db.Tbl90References
                .Where(x => x.RegnumId == _selectedRegnum.RegnumId && x.RefAuthorId == null && x.RefExpertId == null));
            RaisePropertyChanged("ReferenceSourcesCollection");
        }
        private void ExecuteSaveSource(string searchName)
        {
            if (_selectedReferenceSource == null)
            {
                MessageBox.Show("NewDataset",
                    "RequiredInput",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _selectedReferenceSource.RegnumId = _selectedRegnum.RegnumId;

            try
            {
                var reference = _db.Tbl90References.FirstOrDefault(x => x.ReferenceId == _selectedReferenceSource.ReferenceId);
                //             var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceSource.ReferenceID);
                if (_selectedReferenceSource.ReferenceId != 0)
                {
                    if (reference != null) //update
                    {
                        reference.RefSourceId = _selectedReferenceSource.RefSourceId;
                        reference.RegnumId = _selectedReferenceSource.RegnumId;
                        reference.Valid = _selectedReferenceSource.Valid;
                        reference.ValidYear = _selectedReferenceSource.ValidYear;
                        reference.Info = _selectedReferenceSource.Info;
                        reference.Updater = Environment.UserName;
                        reference.UpdaterDate = DateTime.Now;
                        reference.Memo = _selectedReferenceSource.Memo;

                        //           reference.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    reference = new Tbl90Reference //add new
                    {
                        RefSourceId = _selectedReferenceSource.RefSourceId,
                        RegnumId = _selectedReferenceSource.RegnumId,
                        CountId = RandomHelper.Randomnumber(),
                        Valid = _selectedReferenceSource.Valid,
                        ValidYear = _selectedReferenceSource.ValidYear,
                        Info = _selectedReferenceSource.Info,
                        Memo = _selectedReferenceSource.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        //         EntityState = EntityState.Added
                    };
                }

                try
                {
                    if (_selectedReferenceSource.ReferenceId != 0) //update
                    {
                        if (reference != null) _db.Tbl90References.Update(reference);
                    }
                    else                                //add
                    if (reference != null) _db.Tbl90References.Add(reference);

                    _db.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        MessageBox.Show(e.InnerException.ToString(), "FailedToSave",
                            MessageBoxButton.OK, MessageBoxImage.Warning);

                    //     Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    //   Log.Error(e);
                    return;
                }

                MessageBox.Show("SaveSuccess",
                    _selectedReferenceSource.RefSourceId == 0
                        ? "DatasetNew"
                        : _selectedReferenceSource.Info,
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                //         Log.Error(e);
            }

            ReferenceExpertsCollection = new ObservableCollection<Tbl90Reference>(_db.Tbl90References
                .Where(x => x.RegnumId == _selectedRegnum.RegnumId && x.RefAuthorId == null && x.RefExpertId == null));
            RaisePropertyChanged("ReferenceSourcesCollection");
        }
        public ObservableCollection<Tbl90Reference> SearchNameReturnSourcesCollection(string searchName)
        {
            var collection = new ObservableCollection<Tbl90Reference>();

            switch (searchName)
            {
                case "":
                    return collection;
                case "*":
                    collection = new ObservableCollection<Tbl90Reference>(_db.Tbl90References.ToList());
                    break;
                default:
                    collection = int.TryParse(searchName, out var id)
                        ? new ObservableCollection<Tbl90Reference>(_db.Tbl90References
                            .Where(e => e.RegnumId == id && e.RefAuthorId == null && e.RefExpertId == null))
                        : new ObservableCollection<Tbl90Reference>(_db.Tbl90References
                            .Where(e => e.Info.StartsWith(searchName))
                        );
                    break;
            }

            return collection;
        }

        #endregion

        //-----------------------------------------

        #region [Commands Regnum ==> Tbl90Reference Author]

        private RelayCommand _addAuthorCommand;

        public ICommand AddAuthorCommand => _addAuthorCommand ??= new RelayCommand(delegate { ExecuteAddAuthor(null); });

        private RelayCommand _copyAuthorCommand;

        public ICommand CopyAuthorCommand => _copyAuthorCommand ??= new RelayCommand(delegate { ExecuteCopyAuthor(null); });

        private RelayCommand _deleteAuthorCommand;

        public ICommand DeleteAuthorCommand => _deleteAuthorCommand ??= new RelayCommand(delegate { ExecuteDeleteAuthor(SearchRegnumName); });

        private RelayCommand _saveAuthorCommand;

        public ICommand SaveAuthorCommand => _saveAuthorCommand ??= new RelayCommand(delegate { ExecuteSaveAuthor(SearchRegnumName); });


        #endregion

        #region[Methods Reference Author]

        private void ExecuteAddAuthor(object o)
        {
            if (_selectedRegnum == null)
            {
                MessageBox.Show("Select Regnum",
                    "Required select",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (ReferenceAuthorsCollection == null)
                ReferenceAuthorsCollection = new ObservableCollection<Tbl90Reference>();

            ReferenceAuthorsCollection.Insert(0, new Tbl90Reference() { RegnumId = _selectedRegnum.RegnumId, Info = "NewDataset" });
        }
        private void ExecuteCopyAuthor(object o)
        {
            if (_selectedReferenceAuthor == null)
            {
                MessageBox.Show("NewDataset",
                    "RequiredInput",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var reference = _db.Tbl90References.FirstOrDefault(x => x.ReferenceId == _selectedReferenceAuthor.ReferenceId);

            ReferenceAuthorsCollection.Insert(0, new Tbl90Reference()
            {
                RegnumId = reference.RegnumId,
                RefSourceId = reference.RefAuthorId,
                Valid = reference.Valid,
                ValidYear = reference.ValidYear,
                Info = "NewDataset",
                Memo = reference.Memo
            });
        }
        private void ExecuteDeleteAuthor(string searchName)
        {
            if (_selectedReferenceAuthor == null)
            {
                MessageBox.Show("NewDataset",
                    "RequiredInput",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                var reference = _db.Tbl90References.FirstOrDefault(x => x.ReferenceId == _selectedReferenceAuthor.ReferenceId);
                if (reference != null)
                {
                    if (MessageBox.Show("DeleteQuestion1", "DeleteQuestion" + " " + _selectedReferenceAuthor.Info,
                            MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;

                    _db.Tbl90References.Remove(reference);

                    _db.SaveChanges();


                    MessageBox.Show("DeleteSuccess", _selectedReferenceAuthor.Info,
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Information", "DeleteCan" + " " + _selectedReferenceAuthor.Info + " " + "DeleteCan1",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                //         Log.Error(e);
            }

            ReferenceAuthorsCollection = new ObservableCollection<Tbl90Reference>(_db.Tbl90References
                .Where(x => x.RegnumId == _selectedRegnum.RegnumId && x.RefSourceId == null && x.RefExpertId == null));
            RaisePropertyChanged("ReferenceAuthorsCollection");
        }
        private void ExecuteSaveAuthor(string searchName)
        {
            if (_selectedReferenceAuthor == null)
            {
                MessageBox.Show("NewDataset",
                    "RequiredInput",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _selectedReferenceAuthor.RegnumId = _selectedRegnum.RegnumId;

            try
            {
                var reference = _db.Tbl90References.FirstOrDefault(x => x.ReferenceId == _selectedReferenceAuthor.ReferenceId);
                if (_selectedReferenceAuthor.ReferenceId != 0)
                {
                    if (reference != null) //update
                    {
                        reference.RefAuthorId = _selectedReferenceAuthor.RefAuthorId;
                        reference.RegnumId = _selectedReferenceAuthor.RegnumId;
                        reference.Valid = _selectedReferenceAuthor.Valid;
                        reference.ValidYear = _selectedReferenceAuthor.ValidYear;
                        reference.Info = _selectedReferenceAuthor.Info;
                        reference.Updater = Environment.UserName;
                        reference.UpdaterDate = DateTime.Now;
                        reference.Memo = _selectedReferenceAuthor.Memo;

                        //           reference.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    reference = new Tbl90Reference //add new
                    {
                        RefAuthorId = _selectedReferenceAuthor.RefAuthorId,
                        RegnumId = _selectedReferenceAuthor.RegnumId,
                        CountId = RandomHelper.Randomnumber(),
                        Valid = _selectedReferenceAuthor.Valid,
                        ValidYear = _selectedReferenceAuthor.ValidYear,
                        Info = _selectedReferenceAuthor.Info,
                        Memo = _selectedReferenceAuthor.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        //         EntityState = EntityState.Added
                    };
                }

                try
                {
                    if (_selectedReferenceAuthor.ReferenceId != 0) //update
                    {
                        if (reference != null) _db.Tbl90References.Update(reference);
                    }
                    else                                //add
                    if (reference != null) _db.Tbl90References.Add(reference);

                    _db.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        MessageBox.Show(e.InnerException.ToString(), "FailedToSave",
                            MessageBoxButton.OK, MessageBoxImage.Warning);

                    //     Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    //   Log.Error(e);
                    return;
                }

                MessageBox.Show("SaveSuccess",
                    _selectedReferenceAuthor.RefAuthorId == 0
                        ? "DatasetNew"
                        : _selectedReferenceAuthor.Info,
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                //         Log.Error(e);
            }

            ReferenceAuthorsCollection = new ObservableCollection<Tbl90Reference>(_db.Tbl90References
                .Where(x => x.RegnumId == _selectedRegnum.RegnumId && x.RefSourceId == null && x.RefExpertId == null));
            RaisePropertyChanged("ReferenceAuthorsCollection");
        }

        #endregion

        //-----------------------------------------

        #region [Commands Regnum ==> Tbl93Comment]

        private RelayCommand _addCommentCommand;

        public ICommand AddCommentCommand => _addCommentCommand ??= new RelayCommand(delegate { ExecuteAddComment(null); });

        private RelayCommand _copyCommentCommand;

        public ICommand CopyCommentCommand => _copyCommentCommand ??= new RelayCommand(delegate { ExecuteCopyComment(null); });

        private RelayCommand _deleteCommentCommand;

        public ICommand DeleteCommentCommand => _deleteCommentCommand ??= new RelayCommand(delegate { ExecuteDeleteComment(SearchRegnumName); });

        private RelayCommand _saveCommentCommand;

        public ICommand SaveCommentCommand => _saveCommentCommand ??= new RelayCommand(delegate { ExecuteSaveComment(SearchRegnumName); });


        #endregion

        #region [Methods Comment]

        private void ExecuteAddComment(object o)
        {
            if (CommentsCollection == null)
                CommentsCollection = new ObservableCollection<Tbl93Comment>();

            CommentsCollection.Insert(0, new Tbl93Comment() { RegnumId = _selectedRegnum.RegnumId, Info = "NewDataset" });
        }
        private void ExecuteCopyComment(object o)
        {
            if (_selectedComment == null)
            {
                MessageBox.Show("NewDataset",
                    "RequiredInput",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var comment = _db.Tbl93Comments.FirstOrDefault(x => x.CommentId == _selectedComment.CommentId);

            CommentsCollection.Insert(0, new Tbl93Comment
            {
                RegnumId = comment.RegnumId,
                Valid = comment.Valid,
                ValidYear = comment.ValidYear,
                Info = "NewDataset",
                Memo = comment.Memo
            });
        }
        private void ExecuteDeleteComment(string searchName)
        {
            if (_selectedComment == null)
            {
                MessageBox.Show("NewDataset",
                    "RequiredInput",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                var comment = _db.Tbl93Comments.FirstOrDefault(x => x.CommentId == _selectedComment.CommentId);
                if (comment != null)
                {
                    if (MessageBox.Show("DeleteQuestion1", "DeleteQuestion" + " " + _selectedComment.Info,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;

                    _db.Tbl93Comments.Remove(comment);

                    _db.SaveChanges();


                    MessageBox.Show("DeleteSuccess", _selectedComment.Info,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Information", "DeleteCan" + " " + _selectedComment.Info + " " + "DeleteCan1",
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                //         Log.Error(e);
            }

            CommentsCollection = new ObservableCollection<Tbl93Comment>(_db.Tbl93Comments.Where(x => x.RegnumId == _selectedRegnum.RegnumId));
            RaisePropertyChanged("CommentsCollection");


            //if (comment != null && comment.RegnumId == 0)
            //    GetRegnumsByNameOrId(searchName);
            //else
            //{
            //    if (comment != null) _db.Tbl93Comments.Remove(comment);

            //    _db.SaveChanges();

            //    if (_selectedComment != null)
            //    {
            //        GetComments(_selectedComment.CommentId);
            //    }

            //  //  GetRegnumsByNameOrId(searchName);
            //}
        }
        private void ExecuteSaveComment(string searchName)
        {
            if (_selectedComment == null)
            {
                MessageBox.Show("NewDataset",
                    "RequiredInput",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _selectedComment.RegnumId = _selectedRegnum.RegnumId;

            try
            {
                var comment = _db.Tbl93Comments.FirstOrDefault(x => x.CommentId == _selectedComment.CommentId);
                if (_selectedComment.CommentId != 0)
                {
                    if (comment != null) //update
                    {
                        comment.RegnumId = _selectedComment.RegnumId;
                        comment.Valid = _selectedComment.Valid;
                        comment.ValidYear = _selectedComment.ValidYear;
                        comment.Info = _selectedComment.Info;
                        comment.Memo = _selectedComment.Memo;
                        comment.Updater = Environment.UserName;
                        comment.UpdaterDate = DateTime.Now;
                        //      comment.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    comment = new Tbl93Comment //add new
                    {
                        RegnumId = _selectedComment.RegnumId,
                        CountId = RandomHelper.Randomnumber(),
                        Valid = _selectedComment.Valid,
                        ValidYear = _selectedComment.ValidYear,
                        Info = _selectedComment.Info,
                        Memo = _selectedComment.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        //   EntityState = EntityState.Added
                    };
                }

                try
                {
                    if (_selectedComment.CommentId != 0) //update
                    {
                        if (comment != null) _db.Tbl93Comments.Update(comment);
                    }
                    else                                //add
                    if (comment != null) _db.Tbl93Comments.Add(comment);

                    _db.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        MessageBox.Show(e.InnerException.ToString(), "FailedToSave",
                            MessageBoxButton.OK, MessageBoxImage.Warning);

                    //     Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    //   Log.Error(e);
                    return;
                }

                MessageBox.Show("SaveSuccess",
                        _selectedComment.CommentId == 0
                            ? "DatasetNew"
                            : _selectedComment.Info,
                        MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                //         Log.Error(e);
            }

            CommentsCollection = new ObservableCollection<Tbl93Comment>(_db.Tbl93Comments.Where(x => x.RegnumId == _selectedRegnum.RegnumId));
            RaisePropertyChanged("CommentsCollection");
        }

        #endregion

        //------------------------------------------

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

        #region [ Properties ]

        #region Public Properties Regnum

        public string SearchRegnumName { get; set; }

        Tbl03Regnum _selectedRegnum;
        public Tbl03Regnum SelectedRegnum
        {
            get => _selectedRegnum;
            set
            {
                _selectedRegnum = value;
                RaisePropertyChanged("SelectedRegnum");
                if (_selectedRegnum != null) GetPhylums(_selectedRegnum.RegnumId);
                SubphylumsCollection.Clear();
                if (_selectedRegnum != null) GetDivisions(_selectedRegnum.RegnumId);
                SubdivisionsCollection.Clear();
                if (_selectedRegnum != null) GetReferenceExperts(_selectedRegnum.RegnumId);
                if (_selectedRegnum != null) GetReferenceSources(_selectedRegnum.RegnumId);
                if (_selectedRegnum != null) GetReferenceAuthors(_selectedRegnum.RegnumId);
                if (_selectedRegnum != null) GetComments(_selectedRegnum.RegnumId);
                //  ReferenceSourcesCollection.Clear();
            }
        }
        public ObservableCollection<Tbl03Regnum> RegnumsCollection { get; set; }

        #endregion Public Properties Regnum

        #region Public Properties Tbl06Phylum

        public ObservableCollection<Tbl06Phylum> PhylumsCollection { get; set; }

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

        #region "Public Properties Tbl90Author"
        public ObservableCollection<Tbl90RefAuthor> AuthorsCollection { get; set; }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Source"

        public ObservableCollection<Tbl90RefSource> SourcesCollection { get; set; }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Expert"

        public ObservableCollection<Tbl90RefExpert> ExpertsCollection { get; set; }

        #endregion "Public Properties "

        #region Public Properties Tbl90Reference

        public ObservableCollection<Tbl90Reference> ReferencesCollection { get; set; }


        #endregion

        #region "Public Properties Tbl90ReferenceAuthor"

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

        #region Public Properties Tbl90ReferenceExpert

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

        #region Public Properties Tbl93Comment

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
