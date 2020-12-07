using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using Microsoft.EntityFrameworkCore;

namespace ATIS.Ui.Helper
{

    public class CrudFunctions : ViewModelBase
    {
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();


      //  public ObservableCollection<T> SearchNameOrIdReturnCollection<T>(string searchName, string name)
        public ObservableCollection<T> GetCollectionFromSearchNameOrIdOrderBy<T>(string searchName, string name)
        {
            var collection = new ObservableCollection<T>();

            switch (searchName)
            {
                case "":
                    return collection;
                case "*":
                    collection = name switch
                    {
                        "regnum" => GetRegnumsCollectionAllOrderBy<T>(),
                        "phylum" => GetPhylumsCollectionAllOrderBy<T>(),
                        _ => collection
                    };
                    break;
                default:
                {
                    collection = name switch
                    {
                        "regnum" => GetRegnumsCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                        "phylum" => GetPhylumsCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                        _ => collection
                    };
                }
                    break;
            }
            return collection;
        }

        public ObservableCollection<T> GetCollectionAllOrderBy<T>(string name)
        {
            var collection = new ObservableCollection<T>();

            switch (name)
            {
                case "regnum":
                    collection = GetRegnumsCollectionAllOrderBy<T>();
                    break;
                case "phylum":
                    collection = GetPhylumsCollectionAllOrderBy<T>();
                    break;
                case "expert":
                    collection = GetReferenceExpertsCollectionAllOrderBy<T>();
                    break;
                case "source":
                    collection = GetReferenceSourcesCollectionAllOrderBy<T>();
                    break;
                case "author":
                    collection = GetReferenceAuthorsCollectionAllOrderBy<T>();
                    break;

            }

            return collection;
        }


        #region Regnum
        #region Get Regnum

        // ----------------------------------------   Regnum   ------------------------
        private ObservableCollection<T> GetRegnumsCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl03Regnums
                    .Find(e => e.RegnumId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl03Regnums
                    .Find(e => e.RegnumName.StartsWith(searchName))
                    .OrderBy(a => a.RegnumName)
                    .ThenBy(a => a.Subregnum)
                );
            return collection;
        }

        private ObservableCollection<T> GetRegnumsCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl03Regnums
                .OrderBy(a => a.RegnumName)
                .ThenBy(a => a.Subregnum));
            return collection;
        }
        public ObservableCollection<T> GetRegnumsCollectionFromRegnumIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl03Regnums
                .Where(e => e.RegnumId == id)
                .OrderBy(k => k.RegnumName)
                .ThenBy(k => k.Subregnum));

            return collection;
        }

        //-------------------------------------- Phylum   -------------------------
        public ObservableCollection<T> GetPhylumsCollectionFromRegnumIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl06Phylums
                .Where(e => e.RegnumId == id)
                .OrderBy(k => k.PhylumName));
            return collection;
        }
        //-------------------------------------- Division   -------------------------
        public ObservableCollection<T> GetDivisionsCollectionFromRegnumIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl09Divisions
                .Where(e => e.RegnumId == id)
                .OrderBy(k => k.DivisionName));
            return collection;
        }

        //-------------------------------------- Reference Experts   -------------------------
        public ObservableCollection<T> GetReferenceExpertsCollectionFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RegnumId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Sources   -------------------------
        public ObservableCollection<T> GetReferenceSourcesCollectionFromRegnumIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RegnumId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Authors   -------------------------
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromRegnumIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RegnumId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Comments   -------------------------
        public ObservableCollection<T> GetCommentsCollectionFromRegnumIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.RegnumId == id)
                .OrderBy(e => e.Info));
            return collection;
        }

        //Function
        public int GetRegnumIdFromPhylumsCollectionSelect(int id)
        {
            var coll = _context.Tbl06Phylums
                .SingleOrDefault(p => p.PhylumId == id);

            if (coll == null) return 0;
            return coll.RegnumId;
        }
        //Function
        public int GetRegnumIdFromDivisionsCollectionSelect(int id)
        {
            var coll = _context.Tbl09Divisions
                .SingleOrDefault(p => p.DivisionId == id);

            if (coll == null) return 0;
            return coll.RegnumId;
        }

        #endregion     

        #region Copy Regnum    
        // ----------------------------------------   Regnum   ------------------------
        public ObservableCollection<Tbl03Regnum> CopyRegnum(Tbl03Regnum selected)
        {
            var dataset = _uow.Tbl03Regnums.GetById(selected.RegnumId);
            var collection = new ObservableCollection<Tbl03Regnum>();

            collection.Insert(0, new Tbl03Regnum
            {
                RegnumName = CultRes.StringsRes.DatasetNew,
                Subregnum = dataset.Subregnum,
                Valid = dataset.Valid,
                ValidYear = dataset.ValidYear,
                Synonym = dataset.Synonym,
                Author = dataset.Author,
                AuthorYear = dataset.AuthorYear,
                Info = dataset.Info,
                EngName = dataset.EngName,
                GerName = dataset.GerName,
                FraName = dataset.FraName,
                PorName = dataset.PorName,
                Memo = dataset.Memo
            });

            return collection;
        }
        // ----------------------------------------   Phylum   ------------------------
        public ObservableCollection<Tbl06Phylum> CopyPhylum(Tbl06Phylum selected)
        {
            var dataset = _uow.Tbl06Phylums.GetById(selected.PhylumId);
            var collection = new ObservableCollection<Tbl06Phylum>();

            collection.Insert(0, new Tbl06Phylum
            {
                PhylumName = CultRes.StringsRes.DatasetNew,
                RegnumId = dataset.RegnumId,
                Valid = dataset.Valid,
                ValidYear = dataset.ValidYear,
                Synonym = dataset.Synonym,
                Author = dataset.Author,
                AuthorYear = dataset.AuthorYear,
                Info = dataset.Info,
                EngName = dataset.EngName,
                GerName = dataset.GerName,
                FraName = dataset.FraName,
                PorName = dataset.PorName,
                Memo = dataset.Memo
            });

            return collection;
        }

        // ----------------------------------------   Division   ------------------------
        public ObservableCollection<Tbl09Division> CopyDivision(Tbl09Division selected)
        {
            var dataset = _uow.Tbl09Divisions.GetById(selected.DivisionId);
            var collection = new ObservableCollection<Tbl09Division>();

            collection.Insert(0, new Tbl09Division
            {
                DivisionName = CultRes.StringsRes.DatasetNew,
                RegnumId = dataset.RegnumId,
                Valid = dataset.Valid,
                ValidYear = dataset.ValidYear,
                Synonym = dataset.Synonym,
                Author = dataset.Author,
                AuthorYear = dataset.AuthorYear,
                Info = dataset.Info,
                EngName = dataset.EngName,
                GerName = dataset.GerName,
                FraName = dataset.FraName,
                PorName = dataset.PorName,
                Memo = dataset.Memo
            });

            return collection;
        }
        // ----------------------------------------   Regnum   ------------------------

        public ObservableCollection<Tbl90Reference> CopyReferenceRegnum(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        RegnumId = dataset.RegnumId,
                        RefExpertId = dataset.RefExpertId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Source":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        RegnumId = dataset.RegnumId,
                        RefSourceId = dataset.RefSourceId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Author":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        RegnumId = dataset.RegnumId,
                        RefAuthorId = dataset.RefAuthorId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
            }
            return collection;
        }

        #endregion

        #region Delete Regnum

        //------------------------------ Regnum   --------------------------------------------------------------------------------------------
        public void DeleteRegnum(Tbl03Regnum selected)
        {
            _uow.Tbl03Regnums.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl06Phylum> SearchForConnectedDatasetsWithRegnumIdInTablePhylum(Tbl03Regnum selected)
        {
            var collection = new ObservableCollection<Tbl06Phylum>(_uow.Tbl06Phylums.Find(x => x.RegnumId == selected.RegnumId));
            return collection;
        }
        public ObservableCollection<Tbl09Division> SearchForConnectedDatasetsWithRegnumIdInTableDivision(Tbl03Regnum selected)
        {
            var collection = new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions.Find(x => x.RegnumId == selected.RegnumId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithRegnumIdInTableReference(Tbl03Regnum selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.RegnumId == selected.RegnumId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithRegnumIdInTableComment(Tbl03Regnum selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.RegnumId == selected.RegnumId));
            return collection;
        }
        //------------------------------ Phylum   --------------------------------------------------------------------------------------------
        public void DeletePhylum(Tbl06Phylum selected)
        {
            _uow.Tbl06Phylums.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl12Subphylum> SearchForConnectedDatasetsWithPhylumIdInTableSubphylum(Tbl06Phylum selected)
        {
            var collection = new ObservableCollection<Tbl12Subphylum>(_uow.Tbl12Subphylums.Find(x => x.PhylumId == selected.PhylumId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithPhylumIdInTableReference(Tbl06Phylum selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.PhylumId == selected.PhylumId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithPhylumIdInTableComment(Tbl06Phylum selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.PhylumId == selected.PhylumId));
            return collection;
        }
        //------------------------------ Division   --------------------------------------------------------------------------------------------
        public void DeleteDivision(Tbl09Division selected)
        {
            _uow.Tbl09Divisions.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl15Subdivision> SearchForConnectedDatasetsWithDivisionIdInTableSubdivision(Tbl09Division selected)
        {
            var collection = new ObservableCollection<Tbl15Subdivision>(_uow.Tbl15Subdivisions.Find(x => x.DivisionId == selected.DivisionId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithDivisionIdInTableReference(Tbl09Division selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.DivisionId == selected.DivisionId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithDivisionIdInTableComment(Tbl09Division selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.DivisionId == selected.DivisionId));
            return collection;
        }
        //-----------------------DeleteReferences, DeleteComments, DeleteReference, DeleteComment-----------------------------------

        public void DeleteReferences(ObservableCollection<Tbl90Reference> coll)
        {
            foreach (var t in coll)
                _uow.Tbl90References.Remove(t);
            _uow.Complete();
        }
        public void DeleteComments(ObservableCollection<Tbl93Comment> coll)
        {
            foreach (var t in coll)
                _uow.Tbl93Comments.Remove(t);
            _uow.Complete();
        }
        public void DeleteReference(Tbl90Reference selected)
        {
            _uow.Tbl90References.Remove(selected);
            _uow.Complete();
        }
        public void DeleteComment(Tbl93Comment selected)
        {
            _uow.Tbl93Comments.Remove(selected);
            _uow.Complete();
        }
        //--------------------------------------------------------------------------------------------------------------------------


        #endregion

        #region Save Regnum
        public Tbl03Regnum RegnumUpdate(Tbl03Regnum home, Tbl03Regnum selected)
        {
            if (home != null) //update
            {
                home.RegnumName = selected.RegnumName;
                home.Subregnum = selected.Subregnum;
                home.Valid = selected.Valid;
                home.ValidYear = selected.ValidYear;
                home.Author = selected.Author;
                home.AuthorYear = selected.AuthorYear;
                home.Info = selected.Info;
                home.Synonym = selected.Synonym;
                home.EngName = selected.EngName;
                home.GerName = selected.GerName;
                home.FraName = selected.FraName;
                home.PorName = selected.PorName;
                home.Memo = selected.Memo;
                home.Updater = Environment.UserName;
                home.UpdaterDate = DateTime.Now;
            }
            return home;
        }
        public Tbl03Regnum RegnumAdd(Tbl03Regnum selected)
        {
            var home = new Tbl03Regnum() //add new
            {
                RegnumName = selected.RegnumName,
                Subregnum = selected.Subregnum,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Author = selected.Author,
                AuthorYear = selected.AuthorYear,
                Info = selected.Info,
                Synonym = selected.Synonym,
                EngName = selected.EngName,
                GerName = selected.GerName,
                FraName = selected.FraName,
                PorName = selected.PorName,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return home;
        }
        public void RegnumSave(Tbl03Regnum home, Tbl03Regnum selected)
        {
            if (selected.RegnumId != 0)   //update
                _uow.Tbl03Regnums.Update(home);
            else                                //add
                _uow.Tbl03Regnums.Add(home);

            _uow.Complete();
        }
        public Tbl90Reference ReferenceExpertRegnumUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.RegnumId = selected.RegnumId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertRegnumAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                RegnumId = selected.RegnumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl90Reference ReferenceSourceRegnumUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.RegnumId = selected.RegnumId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceRegnumAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                RegnumId = selected.RegnumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl90Reference ReferenceAuthorRegnumUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.RegnumId = selected.RegnumId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorRegnumAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                RegnumId = selected.RegnumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl93Comment CommentRegnumUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.RegnumId = selected.RegnumId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentRegnumAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                RegnumId = selected.RegnumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return comment;
        }

        //------------------Phylum---------------------------------------
        public Tbl06Phylum PhylumUpdate(Tbl06Phylum home, Tbl06Phylum selected)
        {
            if (home != null) //update
            {
                home.PhylumName = selected.PhylumName;
                home.RegnumId = selected.RegnumId;
                home.Valid = selected.Valid;
                home.ValidYear = selected.ValidYear;
                home.Author = selected.Author;
                home.AuthorYear = selected.AuthorYear;
                home.Info = selected.Info;
                home.Synonym = selected.Synonym;
                home.EngName = selected.EngName;
                home.GerName = selected.GerName;
                home.FraName = selected.FraName;
                home.PorName = selected.PorName;
                home.Memo = selected.Memo;
                home.Updater = Environment.UserName;
                home.UpdaterDate = DateTime.Now;
            }
            return home;
        }
        public Tbl06Phylum PhylumAdd(Tbl06Phylum selected)
        {
            var home = new Tbl06Phylum() //add new
            {
                PhylumName = selected.PhylumName,
                RegnumId = selected.RegnumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Author = selected.Author,
                AuthorYear = selected.AuthorYear,
                Info = selected.Info,
                Synonym = selected.Synonym,
                EngName = selected.EngName,
                GerName = selected.GerName,
                FraName = selected.FraName,
                PorName = selected.PorName,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return home;
        }
        public void PhylumSave(Tbl06Phylum home, Tbl06Phylum selected)
        {

            if (selected.PhylumId != 0) //update
            {
                _uow.Tbl06Phylums.Update(home);
            }
            else                                //add
                _uow.Tbl06Phylums.Add(home);
            _uow.Complete();
        }
        //------------------Division---------------------------------------
        public Tbl09Division DivisionUpdate(Tbl09Division home, Tbl09Division selected)
        {
            if (home != null) //update
            {
                home.DivisionName = selected.DivisionName;
                home.RegnumId = selected.RegnumId;
                home.Valid = selected.Valid;
                home.ValidYear = selected.ValidYear;
                home.Author = selected.Author;
                home.AuthorYear = selected.AuthorYear;
                home.Info = selected.Info;
                home.Synonym = selected.Synonym;
                home.EngName = selected.EngName;
                home.GerName = selected.GerName;
                home.FraName = selected.FraName;
                home.PorName = selected.PorName;
                home.Memo = selected.Memo;
                home.Updater = Environment.UserName;
                home.UpdaterDate = DateTime.Now;
            }
            return home;
        }
        public Tbl09Division DivisionAdd(Tbl09Division selected)
        {
            var res = new Tbl09Division() //add new
            {
                DivisionName = selected.DivisionName,
                RegnumId = selected.RegnumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Author = selected.Author,
                AuthorYear = selected.AuthorYear,
                Info = selected.Info,
                Synonym = selected.Synonym,
                EngName = selected.EngName,
                GerName = selected.GerName,
                FraName = selected.FraName,
                PorName = selected.PorName,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return res;
        }
        public void DivisionSave(Tbl09Division home, Tbl09Division selected)
        {
            if (selected.DivisionId != 0) //update
            {
                _uow.Tbl09Divisions.Update(home);
            }
            else                                //add
                _uow.Tbl09Divisions.Add(home);
            _uow.Complete();
        }


        #endregion

        #endregion




        #region Phylum

        private ObservableCollection<T> GetPhylumsCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl06Phylums
                    .Find(e => e.PhylumId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl06Phylums.ListTbl06PhylumsOnlyAnimaliaOrderBy(searchName));
            return collection;
        }

        private ObservableCollection<T> GetPhylumsCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl06Phylums
                .OrderBy(a => a.PhylumName));
            return collection;
        }


        #endregion

        #region References

        private ObservableCollection<T> GetReferenceExpertsCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90RefExperts
                .OrderBy(a => a.RefExpertName));
            return collection;
        }

        private ObservableCollection<T> GetReferenceAuthorsCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90RefAuthors
                .OrderBy(a => a.RefAuthorName)
                .ThenBy(a => a.BookName)
                .ThenBy(a => a.Page1)
            );
            return collection;
        }

        private ObservableCollection<T> GetReferenceSourcesCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90RefSources
                .OrderBy(a => a.RefSourceName));
            return collection;
        }

        //-------------------Reference-------------------------------------
        public void ReferenceExpertSave(Tbl90Reference home, Tbl90Reference selected)
        {
            if (selected.ReferenceId != 0)   //update
                _uow.Tbl90References.Update(home);
            else                                            //add
                _uow.Tbl90References.Add(home);

            _uow.Complete();
        }
        public void ReferenceSourceSave(Tbl90Reference home, Tbl90Reference selected)
        {
            if (selected.ReferenceId != 0)   //update
                _uow.Tbl90References.Update(home);
            else                                            //add
                _uow.Tbl90References.Add(home);

            _uow.Complete();
        }
        public void ReferenceAuthorSave(Tbl90Reference home, Tbl90Reference selected)
        {
            if (selected.ReferenceId != 0)   //update
                _uow.Tbl90References.Update(home);
            else                                            //add
                _uow.Tbl90References.Add(home);

            _uow.Complete();
        }


        #endregion

        #region Comment

        public ObservableCollection<Tbl93Comment> CopyComment(Tbl93Comment selected, string name)
        {
            var dataset = _uow.Tbl93Comments.GetById(selected.CommentId);
            var collection = new ObservableCollection<Tbl93Comment>();
            switch (name)
            {
                case "Regnum":
                    collection.Insert(0, new Tbl93Comment
                    {
                        RegnumId = dataset.RegnumId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Phylum":
                    collection.Insert(0, new Tbl93Comment
                    {
                        PhylumId = dataset.PhylumId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Division":
                    collection.Insert(0, new Tbl93Comment
                    {
                        DivisionId = dataset.DivisionId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Subphylum":
                    collection.Insert(0, new Tbl93Comment
                    {
                        SubphylumId = dataset.SubphylumId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Subdivision":
                    collection.Insert(0, new Tbl93Comment
                    {
                        SubdivisionId = dataset.SubdivisionId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Superclass":
                    collection.Insert(0, new Tbl93Comment
                    {
                        SuperclassId = dataset.SuperclassId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
            }

            return collection;
        }
        //-------------------Comment-------------------------------------
        public void CommentSave(Tbl93Comment home, Tbl93Comment selected)
        {
            if (selected.CommentId != 0)             //update
                _uow.Tbl93Comments.Update(home);
            else                                            //add
                _uow.Tbl93Comments.Add(home);

            _uow.Complete();
        }


        #endregion

    }
}
