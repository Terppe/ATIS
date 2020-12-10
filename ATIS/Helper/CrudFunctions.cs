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
                        "division" => GetDivisionsCollectionAllOrderBy<T>(),
                        "subphylum" => GetSubphylumsCollectionAllOrderBy<T>(),
                        "subdivision" => GetSubdivisionsCollectionAllOrderBy<T>(),
                        "superclass" => GetSuperclassesCollectionAllOrderBy<T>(),
                        "classe" => GetClassesCollectionAllOrderBy<T>(),
                        "subclass" => GetSubclassesCollectionAllOrderBy<T>(),
                        "infraclass" => GetInfraclassesCollectionAllOrderBy<T>(),
                        "legio" => GetLegiosCollectionAllOrderBy<T>(),
                        "ordo" => GetOrdosCollectionAllOrderBy<T>(),
                        "subordo" => GetSubordosCollectionAllOrderBy<T>(),
                        "infraordo" => GetInfraordosCollectionAllOrderBy<T>(),
                        _ => collection
                    };
                    break;
                default:
                {
                    collection = name switch
                    {
                        "regnum" => GetRegnumsCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                        "phylum" => GetPhylumsCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                        "division" => GetDivisionsCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                        "subphylum" => GetSubphylumsCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                        "subdivision" => GetSubdivisionsCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                        "superclass" => GetSuperclassesCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                        "classe" => GetClassesCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                        "subclass" => GetSubclassesCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                        "infraclass" => GetInfraclassesCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                        "legio" => GetLegiosCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                        "ordo" => GetOrdosCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                        "subordo" => GetSubordosCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                        "infraordo" => GetInfraordosCollectionFromSearchNameOrIdOrderBy<T>(searchName),
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
                case "division":
                    collection = GetDivisionsCollectionAllOrderBy<T>();
                    break;
                case "subphylum":
                    collection = GetSubphylumsCollectionAllOrderBy<T>();
                    break;
                case "subdivision":
                    collection = GetSubdivisionsCollectionAllOrderBy<T>();
                    break;
                case "superclass":
                    collection = GetSuperclassesCollectionAllOrderBy<T>();
                    break;
                case "classe":
                    collection = GetClassesCollectionAllOrderBy<T>();
                    break;
                case "subclass":
                    collection = GetSubclassesCollectionAllOrderBy<T>();
                    break;
                case "infraclass":
                    collection = GetInfraclassesCollectionAllOrderBy<T>();
                    break;
                case "legio":
                    collection = GetLegiosCollectionAllOrderBy<T>();
                    break;
                case "ordo":
                    collection = GetOrdosCollectionAllOrderBy<T>();
                    break;
                case "subordo":
                    collection = GetSubordosCollectionAllOrderBy<T>();
                    break;
                case "infraordo":
                    collection = GetInfraordosCollectionAllOrderBy<T>();
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
        public int RegnumIdFromPhylumsCollectionSelect(int id)
        {
            var coll = _context.Tbl06Phylums
                .SingleOrDefault(p => p.PhylumId == id);

            if (coll == null) return 0;
            return coll.RegnumId;
        }
        //Function
        public int RegnumIdFromDivisionsCollectionSelect(int id)
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

          #region Get Phylum

        //----------------------------------------   Phylum   ------------------------
        private ObservableCollection<T> GetPhylumsCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl06Phylums
                    .Find(e => e.PhylumId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl06Phylums
                    .Find(e => e.PhylumName.StartsWith(searchName))
                    .OrderBy(a => a.PhylumName)
                );
            return collection;
        }

        private ObservableCollection<T> GetPhylumsCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl06Phylums
                .OrderBy(a => a.PhylumName));
            return collection;
        }
        public ObservableCollection<T> GetPhylumsCollectionFromPhylumIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl06Phylums
                .Where(e => e.PhylumId == id)
                .OrderBy(k => k.PhylumName));

            return collection;
        }

        //-------------------------------------- Subphylum   -------------------------
        public ObservableCollection<T> GetSubphylumsCollectionFromPhylumIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl12Subphylums
                .Where(e => e.PhylumId == id)
                .OrderBy(k => k.SubphylumName));
            return collection;
        }

        //-------------------------------------- Reference Experts   -------------------------
        public ObservableCollection<T> GetReferenceExpertsCollectionFromPhylumIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.PhylumId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Sources   -------------------------
        public ObservableCollection<T> GetReferenceSourcesCollectionFromPhylumIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.PhylumId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Authors   -------------------------
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromPhylumIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.PhylumId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Comments   -------------------------
        public ObservableCollection<T> GetCommentsCollectionFromPhylumIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.PhylumId == id)
                .OrderBy(e => e.Info));
            return collection;
        }

        //Function
        public int PhylumIdFromSubphylumsCollectionSelect(int id)
        {
            var coll = _context.Tbl12Subphylums
                .SingleOrDefault(p => p.SubphylumId == id);

            if (coll == null) return 0;
            return coll.PhylumId;
        }

        #endregion

          #region Copy Phylum

        // ----------------------------------------   Subphylum  ------------------------
        public ObservableCollection<Tbl12Subphylum> CopySubphylum(Tbl12Subphylum selected)
        {
            var dataset = _uow.Tbl12Subphylums.GetById(selected.SubphylumId);
            var collection = new ObservableCollection<Tbl12Subphylum>();

            collection.Insert(0, new Tbl12Subphylum
            {
                SubphylumName = CultRes.StringsRes.DatasetNew,
                PhylumId = dataset.PhylumId,
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

        public ObservableCollection<Tbl90Reference> CopyReferencePhylum(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        PhylumId = dataset.PhylumId,
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
                        PhylumId = dataset.PhylumId,
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
                        PhylumId = dataset.PhylumId,
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

          #region Delete Phylum

        //------------------------------ Phylum --------------------------------------------------------------------------------------------
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithRegnumIdInTableReference(Tbl06Phylum selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.PhylumId == selected.PhylumId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithRegnumIdInTableComment(Tbl06Phylum selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.PhylumId == selected.PhylumId));
            return collection;
        }

        //------------------------------ Subphylum --------------------------------------------------------------------------------------------
        public void DeleteSubphylum(Tbl12Subphylum selected)
        {
            _uow.Tbl12Subphylums.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl18Superclass> SearchForConnectedDatasetsWithSubphylumIdInTableSuperclass(Tbl12Subphylum selected)
        {
            var collection = new ObservableCollection<Tbl18Superclass>(_uow.Tbl18Superclasses.Find(x => x.SubphylumId == selected.SubphylumId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithSubphylumIdInTableReference(Tbl12Subphylum selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SubphylumId == selected.SubphylumId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithSubphylumIdInTableComment(Tbl12Subphylum selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SubphylumId == selected.SubphylumId));
            return collection;
        }


        #endregion

        #region Save Phylum 

        //------------------ Subphylum ---------------------------------------
        public Tbl12Subphylum SubphylumUpdate(Tbl12Subphylum home, Tbl12Subphylum selected)
        {
            if (home != null) //update
            {
                home.SubphylumName = selected.SubphylumName;
                home.PhylumId = selected.PhylumId;
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
        public Tbl12Subphylum SubphylumAdd(Tbl12Subphylum selected)
        {
            var home = new Tbl12Subphylum() //add new
            {
                SubphylumName = selected.SubphylumName,
                PhylumId = selected.PhylumId,
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
                UpdaterDate = DateTime.Now
            };
            return home;
        }
        public void SubphylumSave(Tbl12Subphylum home, Tbl12Subphylum selected)
        {

            if (selected.SubphylumId != 0) //update
            {
                _uow.Tbl12Subphylums.Update(home);
            }
            else                                //add
                _uow.Tbl12Subphylums.Add(home);
            _uow.Complete();
        }


        public Tbl90Reference ReferenceExpertPhylumUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.PhylumId = selected.PhylumId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertPhylumAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                PhylumId = selected.PhylumId,
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
        public Tbl90Reference ReferenceSourcePhylumUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.PhylumId = selected.PhylumId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourcePhylumAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                PhylumId = selected.PhylumId,
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
        public Tbl90Reference ReferenceAuthorPhylumUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.PhylumId = selected.PhylumId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorPhylumAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                PhylumId = selected.PhylumId,
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
        public Tbl93Comment CommentPhylumUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.PhylumId = selected.PhylumId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentPhylumAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                PhylumId = selected.PhylumId,
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

        #endregion

        #endregion

        #region Division

          #region Get Division

        //----------------------------------------   Division   ------------------------
        private ObservableCollection<T> GetDivisionsCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl09Divisions
                    .Find(e => e.DivisionId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl09Divisions
                    .Find(e => e.DivisionName.StartsWith(searchName))
                    .OrderBy(a => a.DivisionName)
                );
            return collection;
        }

        private ObservableCollection<T> GetDivisionsCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl09Divisions
                .OrderBy(a => a.DivisionName));
            return collection;
        }
        public ObservableCollection<T> GetDivisionsCollectionFromDivisionIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl09Divisions
                .Where(e => e.DivisionId == id)
                .OrderBy(k => k.DivisionName));

            return collection;
        }

        //-------------------------------------- Subdivision   -------------------------
        public ObservableCollection<T> GetSubdivisionsCollectionFromDivisionIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl15Subdivisions
                .Where(e => e.DivisionId == id)
                .OrderBy(k => k.SubdivisionName));
            return collection;
        }

        //-------------------------------------- Reference Experts   -------------------------
        public ObservableCollection<T> GetReferenceExpertsCollectionFromDivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.DivisionId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Sources   -------------------------
        public ObservableCollection<T> GetReferenceSourcesCollectionFromDivisionIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.DivisionId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Authors   -------------------------
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromDivisionIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.DivisionId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Comments   -------------------------
        public ObservableCollection<T> GetCommentsCollectionFromDivisionIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.DivisionId == id)
                .OrderBy(e => e.Info));
            return collection;
        }

        //Function
        public int GetDivisionIdFromSubdivisionsCollectionSelect(int id)
        {
            var coll = _context.Tbl15Subdivisions
                .SingleOrDefault(p => p.SubdivisionId == id);

            if (coll == null) return 0;
            return coll.DivisionId;
        }

        #endregion

          #region Copy Division

        // ----------------------------------------   Subdivision  ------------------------
        public ObservableCollection<Tbl15Subdivision> CopySubdivision(Tbl15Subdivision selected)
        {
            var dataset = _uow.Tbl15Subdivisions.GetById(selected.SubdivisionId);
            var collection = new ObservableCollection<Tbl15Subdivision>();

            collection.Insert(0, new Tbl15Subdivision
            {
                SubdivisionName = CultRes.StringsRes.DatasetNew,
                DivisionId = dataset.DivisionId,
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

        public ObservableCollection<Tbl90Reference> CopyReferenceDivision(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        DivisionId = dataset.DivisionId,
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
                        DivisionId = dataset.DivisionId,
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
                        DivisionId = dataset.DivisionId,
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

          #region Delete Division

        //------------------------------ Division --------------------------------------------------------------------------------------------
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithRegnumIdInTableReference(Tbl09Division selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.DivisionId == selected.DivisionId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithRegnumIdInTableComment(Tbl09Division selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.DivisionId == selected.DivisionId));
            return collection;
        }

        //------------------------------ Subdivision --------------------------------------------------------------------------------------------
        public void DeleteSubdivision(Tbl15Subdivision selected)
        {
            _uow.Tbl15Subdivisions.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl18Superclass> SearchForConnectedDatasetsWithSubdivisionIdInTableSuperclass(Tbl15Subdivision selected)
        {
            var collection = new ObservableCollection<Tbl18Superclass>(_uow.Tbl18Superclasses.Find(x => x.SubdivisionId == selected.SubdivisionId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithSubdivisionIdInTableReference(Tbl15Subdivision selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SubdivisionId == selected.SubdivisionId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithSubdivisionIdInTableComment(Tbl15Subdivision selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SubdivisionId == selected.SubdivisionId));
            return collection;
        }


        #endregion

          #region Save Division 

        //------------------ Subdivision ---------------------------------------
        public Tbl15Subdivision SubdivisionUpdate(Tbl15Subdivision home, Tbl15Subdivision selected)
        {
            if (home != null) //update
            {
                home.SubdivisionName = selected.SubdivisionName;
                home.DivisionId = selected.DivisionId;
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
        public Tbl15Subdivision SubdivisionAdd(Tbl15Subdivision selected)
        {
            var home = new Tbl15Subdivision() //add new
            {
                SubdivisionName = selected.SubdivisionName,
                DivisionId = selected.DivisionId,
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
                UpdaterDate = DateTime.Now
            };
            return home;
        }
        public void SubdivisionSave(Tbl15Subdivision home, Tbl15Subdivision selected)
        {

            if (selected.SubdivisionId != 0) //update
            {
                _uow.Tbl15Subdivisions.Update(home);
            }
            else                                //add
                _uow.Tbl15Subdivisions.Add(home);
            _uow.Complete();
        }

        public Tbl90Reference ReferenceExpertDivisionUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.DivisionId = selected.DivisionId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertDivisionAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                DivisionId = selected.DivisionId,
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
        public Tbl90Reference ReferenceSourceDivisionUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.DivisionId = selected.DivisionId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceDivisionAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                DivisionId = selected.DivisionId,
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
        public Tbl90Reference ReferenceAuthorDivisionUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.DivisionId = selected.DivisionId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorDivisionAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                DivisionId = selected.DivisionId,
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
        public Tbl93Comment CommentDivisionUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.DivisionId = selected.DivisionId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentDivisionAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                DivisionId = selected.DivisionId,
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

        #endregion

        #endregion

        #region Subphylum

        #region Get Subphylum

        //----------------------------------------   Subphylum   ------------------------
        private ObservableCollection<T> GetSubphylumsCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl12Subphylums
                    .Find(e => e.SubphylumId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl12Subphylums
                    .Find(e => e.SubphylumName.StartsWith(searchName))
                    .OrderBy(a => a.SubphylumName)
                );
            return collection;
        }

        private ObservableCollection<T> GetSubphylumsCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl12Subphylums
                .OrderBy(a => a.SubphylumName));
            return collection;
        }
        public ObservableCollection<T> GetSubphylumsCollectionFromSubphylumIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl12Subphylums
                .Where(e => e.SubphylumId == id)
                .OrderBy(k => k.SubphylumName));

            return collection;
        }

        //-------------------------------------- Superclass   -------------------------
        public ObservableCollection<T> GetSuperclassesCollectionFromSubphylumIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl18Superclasses
                .Where(e => e.SubphylumId == id)
                .OrderBy(k => k.SuperclassName));
            return collection;
        }

        //-------------------------------------- Reference Experts   -------------------------
        public ObservableCollection<T> GetReferenceExpertsCollectionFromSubphylumIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.SubphylumId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Sources   -------------------------
        public ObservableCollection<T> GetReferenceSourcesCollectionFromSubphylumIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.SubphylumId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Authors   -------------------------
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromSubphylumIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.SubphylumId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Comments   -------------------------
        public ObservableCollection<T> GetCommentsCollectionFromSubphylumIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.SubphylumId == id)
                .OrderBy(e => e.Info));
            return collection;
        }

        //Function
        public int GetSubphylumIdFromSuperclassesCollectionSelect(int id)
        {
            var coll = _context.Tbl18Superclasses
                .SingleOrDefault(p => p.SuperclassId == id);

            if (coll == null) return 0;
            return coll.SubphylumId;
        }

        #endregion

        #region Copy Subphylum

        // ----------------------------------------   Superclass  ------------------------
        public ObservableCollection<Tbl18Superclass> CopySuperclass(Tbl18Superclass selected)
        {
            var dataset = _uow.Tbl18Superclasses.GetById(selected.SuperclassId);
            var collection = new ObservableCollection<Tbl18Superclass>();

            collection.Insert(0, new Tbl18Superclass
            {
                SuperclassName = CultRes.StringsRes.DatasetNew,
                SubphylumId = dataset.SubphylumId,
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

        public ObservableCollection<Tbl90Reference> CopyReferenceSubphylum(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        SubphylumId = dataset.SubphylumId,
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
                        SubphylumId = dataset.SubphylumId,
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
                        SubphylumId = dataset.SubphylumId,
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

        #region Delete Subphylum

        //------------------------------ Subphylum --------------------------------------------------------------------------------------------
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithRegnumIdInTableReference(Tbl12Subphylum selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SubphylumId == selected.SubphylumId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithRegnumIdInTableComment(Tbl12Subphylum selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SubphylumId == selected.SubphylumId));
            return collection;
        }

        //------------------------------ Superclass --------------------------------------------------------------------------------------------
        public void DeleteSuperclass(Tbl18Superclass selected)
        {
            _uow.Tbl18Superclasses.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl21Class> SearchForConnectedDatasetsWithSuperclassIdInTableClass(Tbl18Superclass selected)
        {
            var collection = new ObservableCollection<Tbl21Class>(_uow.Tbl21Classes.Find(x => x.SuperclassId == selected.SuperclassId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithSuperclassIdInTableReference(Tbl18Superclass selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SuperclassId == selected.SuperclassId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithSuperclassIdInTableComment(Tbl18Superclass selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SuperclassId == selected.SuperclassId));
            return collection;
        }


        #endregion

        #region Save Subphylum 

        //------------------ Superclass ---------------------------------------
        public Tbl18Superclass SuperclassUpdate(Tbl18Superclass home, Tbl18Superclass selected)
        {
            if (home != null) //update
            {
                home.SuperclassName = selected.SuperclassName;
                home.SubphylumId = selected.SubphylumId;
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
        public Tbl18Superclass SuperclassAdd(Tbl18Superclass selected)
        {
            var home = new Tbl18Superclass() //add new
            {
                SuperclassName = selected.SuperclassName,
                SubphylumId = selected.SubphylumId,
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
                UpdaterDate = DateTime.Now
            };
            return home;
        }
        public void SuperclassSave(Tbl18Superclass home, Tbl18Superclass selected)
        {

            if (selected.SuperclassId != 0) //update
            {
                _uow.Tbl18Superclasses.Update(home);
            }
            else                                //add
                _uow.Tbl18Superclasses.Add(home);
            _uow.Complete();
        }

        public Tbl90Reference ReferenceExpertSubphylumUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.SubphylumId = selected.SubphylumId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertSubphylumAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                SubphylumId = selected.SubphylumId,
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
        public Tbl90Reference ReferenceSourceSubphylumUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.SubphylumId = selected.SubphylumId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceSubphylumAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                SubphylumId = selected.SubphylumId,
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
        public Tbl90Reference ReferenceAuthorSubphylumUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.SubphylumId = selected.SubphylumId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorSubphylumAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                SubphylumId = selected.SubphylumId,
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
        public Tbl93Comment CommentSubphylumUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.SubphylumId = selected.SubphylumId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentSubphylumAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                SubphylumId = selected.SubphylumId,
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

        #endregion

        #endregion

        #region Subdivision

          #region Get Subdivision

        //----------------------------------------   Subdivision   ------------------------
        private ObservableCollection<T> GetSubdivisionsCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl15Subdivisions
                    .Find(e => e.SubdivisionId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl15Subdivisions
                    .Find(e => e.SubdivisionName.StartsWith(searchName))
                    .OrderBy(a => a.SubdivisionName)
                );
            return collection;
        }

        private ObservableCollection<T> GetSubdivisionsCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl15Subdivisions
                .OrderBy(a => a.SubdivisionName));
            return collection;
        }
        public ObservableCollection<T> GetSubdivisionsCollectionFromSubdivisionIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl15Subdivisions
                .Where(e => e.SubdivisionId == id)
                .OrderBy(k => k.SubdivisionName));

            return collection;
        }

        //-------------------------------------- Superclass   -------------------------
        public ObservableCollection<T> GetSuperclassesCollectionFromSubdivisionIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl18Superclasses
                .Where(e => e.SubdivisionId == id)
                .OrderBy(k => k.SuperclassName));
            return collection;
        }

        //-------------------------------------- Reference Experts   -------------------------
        public ObservableCollection<T> GetReferenceExpertsCollectionFromSubdivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.SubdivisionId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Sources   -------------------------
        public ObservableCollection<T> GetReferenceSourcesCollectionFromSubdivisionIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.SubdivisionId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Authors   -------------------------
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromSubdivisionIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.SubdivisionId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Comments   -------------------------
        public ObservableCollection<T> GetCommentsCollectionFromSubdivisionIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.SubdivisionId == id)
                .OrderBy(e => e.Info));
            return collection;
        }

        //Function
        public int GetSubdivisionIdFromSuperclassesCollectionSelect(int id)
        {
            var coll = _context.Tbl18Superclasses
                .SingleOrDefault(p => p.SuperclassId == id);

            if (coll == null) return 0;
            return coll.SubdivisionId;
        }

        #endregion

          #region Copy Subdivision

        // ----------------------------------------   Superclass  ------------------------
        //public ObservableCollection<Tbl18Superclass> CopySuperclass(Tbl18Superclass selected)
        //{
        //    var dataset = _uow.Tbl18Superclasses.GetById(selected.SuperclassId);
        //    var collection = new ObservableCollection<Tbl18Superclass>();

        //    collection.Insert(0, new Tbl18Superclass
        //    {
        //        SuperclassName = CultRes.StringsRes.DatasetNew,
        //        SubdivisionId = dataset.SubdivisionId,
        //        Valid = dataset.Valid,
        //        ValidYear = dataset.ValidYear,
        //        Synonym = dataset.Synonym,
        //        Author = dataset.Author,
        //        AuthorYear = dataset.AuthorYear,
        //        Info = dataset.Info,
        //        EngName = dataset.EngName,
        //        GerName = dataset.GerName,
        //        FraName = dataset.FraName,
        //        PorName = dataset.PorName,
        //        Memo = dataset.Memo
        //    });

        //    return collection;
        //}

        public ObservableCollection<Tbl90Reference> CopyReferenceSubdivision(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        SubdivisionId = dataset.SubdivisionId,
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
                        SubdivisionId = dataset.SubdivisionId,
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
                        SubdivisionId = dataset.SubdivisionId,
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

          #region Delete Subdivision

        //------------------------------ Subdivision --------------------------------------------------------------------------------------------
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithRegnumIdInTableReference(Tbl15Subdivision selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SubdivisionId == selected.SubdivisionId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithRegnumIdInTableComment(Tbl15Subdivision selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SubdivisionId == selected.SubdivisionId));
            return collection;
        }

        ////------------------------------ Superclass --------------------------------------------------------------------------------------------
        //public void DeleteSuperclass(Tbl18Superclass selected)
        //{
        //    _uow.Tbl18Superclasses.Remove(selected);
        //    _uow.Complete();
        //}
        //public ObservableCollection<Tbl21Class> SearchForConnectedDatasetsWithSuperclassIdInTableClass(Tbl18Superclass selected)
        //{
        //    var collection = new ObservableCollection<Tbl21Class>(_uow.Tbl21Classes.Find(x => x.SuperclassId == selected.SuperclassId));
        //    return collection;
        //}
        //public ObservableCollection<Tbl90Reference> DeleteDatasetsWithSuperclassIdInTableReference(Tbl18Superclass selected)
        //{
        //    var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SuperclassId == selected.SuperclassId));
        //    return collection;
        //}
        //public ObservableCollection<Tbl93Comment> DeleteDatasetsWithSuperclassIdInTableComment(Tbl18Superclass selected)
        //{
        //    var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SuperclassId == selected.SuperclassId));
        //    return collection;
        //}


        #endregion

          #region Save Subdivision 

        //------------------ Superclass ---------------------------------------
        //public Tbl18Superclass SuperclassUpdate(Tbl18Superclass home, Tbl18Superclass selected)
        //{
        //    if (home != null) //update
        //    {
        //        home.SuperclassName = selected.SuperclassName;
        //        home.SubdivisionId = selected.SubdivisionId;
        //        home.Valid = selected.Valid;
        //        home.ValidYear = selected.ValidYear;
        //        home.Author = selected.Author;
        //        home.AuthorYear = selected.AuthorYear;
        //        home.Info = selected.Info;
        //        home.Synonym = selected.Synonym;
        //        home.EngName = selected.EngName;
        //        home.GerName = selected.GerName;
        //        home.FraName = selected.FraName;
        //        home.PorName = selected.PorName;
        //        home.Memo = selected.Memo;
        //        home.Updater = Environment.UserName;
        //        home.UpdaterDate = DateTime.Now;
        //    }
        //    return home;
        //}
        //public Tbl18Superclass SuperclassAdd(Tbl18Superclass selected)
        //{
        //    var home = new Tbl18Superclass() //add new
        //    {
        //        SuperclassName = selected.SuperclassName,
        //        SubdivisionId = selected.SubdivisionId,
        //        CountId = RandomHelper.Randomnumber(),
        //        Valid = selected.Valid,
        //        ValidYear = selected.ValidYear,
        //        Author = selected.Author,
        //        AuthorYear = selected.AuthorYear,
        //        Info = selected.Info,
        //        Synonym = selected.Synonym,
        //        EngName = selected.EngName,
        //        GerName = selected.GerName,
        //        FraName = selected.FraName,
        //        PorName = selected.PorName,
        //        Memo = selected.Memo,
        //        Writer = Environment.UserName,
        //        WriterDate = DateTime.Now,
        //        Updater = Environment.UserName,
        //        UpdaterDate = DateTime.Now
        //    };
        //    return home;
        //}
        //public void SuperclassSave(Tbl18Superclass home, Tbl18Superclass selected)
        //{

        //    if (selected.SuperclassId != 0) //update
        //    {
        //        _uow.Tbl18Superclasses.Update(home);
        //    }
        //    else                                //add
        //        _uow.Tbl18Superclasses.Add(home);
        //    _uow.Complete();
        //}

        public Tbl90Reference ReferenceExpertSubdivisionUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.SubdivisionId = selected.SubdivisionId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertSubdivisionAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                SubdivisionId = selected.SubdivisionId,
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
        public Tbl90Reference ReferenceSourceSubdivisionUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.SubdivisionId = selected.SubdivisionId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceSubdivisionAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                SubdivisionId = selected.SubdivisionId,
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
        public Tbl90Reference ReferenceAuthorSubdivisionUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.SubdivisionId = selected.SubdivisionId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorSubdivisionAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                SubdivisionId = selected.SubdivisionId,
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
        public Tbl93Comment CommentSubdivisionUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.SubdivisionId = selected.SubdivisionId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentSubdivisionAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                SubdivisionId = selected.SubdivisionId,
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

        #endregion

        #endregion

        #region Superclass

          #region Get Superclass

        //----------------------------------------   Superclass   ------------------------
        private ObservableCollection<T> GetSuperclassesCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl18Superclasses
                    .Find(e => e.SuperclassId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl18Superclasses
                    .Find(e => e.SuperclassName.StartsWith(searchName))
                    .OrderBy(a => a.SuperclassName)
                );
            return collection;
        }

        private ObservableCollection<T> GetSuperclassesCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl18Superclasses
                .OrderBy(a => a.SuperclassName));
            return collection;
        }
        public ObservableCollection<T> GetSuperclassesCollectionFromSuperclassIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl18Superclasses
                .Where(e => e.SuperclassId == id)
                .OrderBy(k => k.SuperclassName));

            return collection;
        }

        //-------------------------------------- Class   -------------------------
        public ObservableCollection<T> GetClassesCollectionFromSuperclassIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl21Classes
                .Where(e => e.SuperclassId == id)
                .OrderBy(k => k.ClassName));
            return collection;
        }

        //-------------------------------------- Reference Experts   -------------------------
        public ObservableCollection<T> GetReferenceExpertsCollectionFromSuperclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.SuperclassId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Sources   -------------------------
        public ObservableCollection<T> GetReferenceSourcesCollectionFromSuperclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.SuperclassId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Authors   -------------------------
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromSuperclassIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.SuperclassId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Comments   -------------------------
        public ObservableCollection<T> GetCommentsCollectionFromSuperclassIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.SuperclassId == id)
                .OrderBy(e => e.Info));
            return collection;
        }

        //Function
        public int GetSuperclassIdFromClassesCollectionSelect(int id)
        {
            var coll = _context.Tbl21Classes
                .SingleOrDefault(p => p.ClassId == id);

            if (coll == null) return 0;
            return coll.SuperclassId;
        }

        #endregion

          #region Copy Superclass

        // ----------------------------------------   Class  ------------------------
        public ObservableCollection<Tbl21Class> CopyClass(Tbl21Class selected)
        {
            var dataset = _uow.Tbl21Classes.GetById(selected.ClassId);
            var collection = new ObservableCollection<Tbl21Class>();

            collection.Insert(0, new Tbl21Class
            {
                ClassName = CultRes.StringsRes.DatasetNew,
                SuperclassId = dataset.SuperclassId,
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

        public ObservableCollection<Tbl90Reference> CopyReferenceSuperclass(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        SuperclassId = dataset.SuperclassId,
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
                        SuperclassId = dataset.SuperclassId,
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
                        SuperclassId = dataset.SuperclassId,
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

          #region Delete Superclass

        //------------------------------ Superclass --------------------------------------------------------------------------------------------
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithRegnumIdInTableReference(Tbl18Superclass selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SuperclassId == selected.SuperclassId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithRegnumIdInTableComment(Tbl18Superclass selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SuperclassId == selected.SuperclassId));
            return collection;
        }

        //------------------------------ Class --------------------------------------------------------------------------------------------
        public void DeleteClass(Tbl21Class selected)
        {
            _uow.Tbl21Classes.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl24Subclass> SearchForConnectedDatasetsWithClassIdInTableSubclass(Tbl21Class selected)
        {
            var collection = new ObservableCollection<Tbl24Subclass>(_uow.Tbl24Subclasses.Find(x => x.ClassId == selected.ClassId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithClassIdInTableReference(Tbl21Class selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.ClassId == selected.ClassId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithClassIdInTableComment(Tbl21Class selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.ClassId == selected.ClassId));
            return collection;
        }


        #endregion

          #region Save Superclass 

        //------------------ Class ---------------------------------------
        public Tbl21Class ClassUpdate(Tbl21Class home, Tbl21Class selected)
        {
            if (home != null) //update
            {
                home.ClassName = selected.ClassName;
                home.SuperclassId = selected.SuperclassId;
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
        public Tbl21Class ClassAdd(Tbl21Class selected)
        {
            var home = new Tbl21Class() //add new
            {
                ClassName = selected.ClassName,
                SuperclassId = selected.SuperclassId,
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
                UpdaterDate = DateTime.Now
            };
            return home;
        }
        public void ClassSave(Tbl21Class home, Tbl21Class selected)
        {

            if (selected.ClassId != 0) //update
            {
                _uow.Tbl21Classes.Update(home);
            }
            else                                //add
                _uow.Tbl21Classes.Add(home);
            _uow.Complete();
        }

        public Tbl90Reference ReferenceExpertSuperclassUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.SuperclassId = selected.SuperclassId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertSuperclassAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                SuperclassId = selected.SuperclassId,
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
        public Tbl90Reference ReferenceSourceSuperclassUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.SuperclassId = selected.SuperclassId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceSuperclassAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                SuperclassId = selected.SuperclassId,
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
        public Tbl90Reference ReferenceAuthorSuperclassUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.SuperclassId = selected.SuperclassId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorSuperclassAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                SuperclassId = selected.SuperclassId,
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
        public Tbl93Comment CommentSuperclassUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.SuperclassId = selected.SuperclassId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentSuperclassAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                SuperclassId = selected.SuperclassId,
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

        #endregion

        #endregion

        #region Class

          #region Get Class

        //----------------------------------------   Class   ------------------------
        private ObservableCollection<T> GetClassesCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl21Classes
                    .Find(e => e.ClassId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl21Classes
                    .Find(e => e.ClassName.StartsWith(searchName))
                    .OrderBy(a => a.ClassName)
                );
            return collection;
        }

        private ObservableCollection<T> GetClassesCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl21Classes
                .OrderBy(a => a.ClassName));
            return collection;
        }
        public ObservableCollection<T> GetClassesCollectionFromClassIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl21Classes
                .Where(e => e.ClassId == id)
                .OrderBy(k => k.ClassName));

            return collection;
        }

        //-------------------------------------- Subclass   -------------------------
        public ObservableCollection<T> GetSubclassesCollectionFromClassIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl24Subclasses
                .Where(e => e.ClassId == id)
                .OrderBy(k => k.SubclassName));
            return collection;
        }

        //-------------------------------------- Reference Experts   -------------------------
        public ObservableCollection<T> GetReferenceExpertsCollectionFromClassIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.ClassId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Sources   -------------------------
        public ObservableCollection<T> GetReferenceSourcesCollectionFromClassIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.ClassId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Authors   -------------------------
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromClassIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.ClassId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Comments   -------------------------
        public ObservableCollection<T> GetCommentsCollectionFromClassIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.ClassId == id)
                .OrderBy(e => e.Info));
            return collection;
        }

        //Function
        public int GetClassIdFromSubclassesCollectionSelect(int id)
        {
            var coll = _context.Tbl24Subclasses
                .SingleOrDefault(p => p.SubclassId == id);

            if (coll == null) return 0;
            return coll.ClassId;
        }

        #endregion

          #region Copy Class

        // ----------------------------------------   Subclass  ------------------------
        public ObservableCollection<Tbl24Subclass> CopySubclass(Tbl24Subclass selected)
        {
            var dataset = _uow.Tbl24Subclasses.GetById(selected.SubclassId);
            var collection = new ObservableCollection<Tbl24Subclass>();

            collection.Insert(0, new Tbl24Subclass
            {
                SubclassName = CultRes.StringsRes.DatasetNew,
                ClassId = dataset.ClassId,
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

        public ObservableCollection<Tbl90Reference> CopyReferenceClass(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        ClassId = dataset.ClassId,
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
                        ClassId = dataset.ClassId,
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
                        ClassId = dataset.ClassId,
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

          #region Delete Class

        //------------------------------ Class --------------------------------------------------------------------------------------------
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithRegnumIdInTableReference(Tbl21Class selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.ClassId == selected.ClassId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithRegnumIdInTableComment(Tbl21Class selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.ClassId == selected.ClassId));
            return collection;
        }

        //------------------------------ Subclass --------------------------------------------------------------------------------------------
        public void DeleteSubclass(Tbl24Subclass selected)
        {
            _uow.Tbl24Subclasses.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl27Infraclass> SearchForConnectedDatasetsWithSubclassIdInTableInfraclass(Tbl24Subclass selected)
        {
            var collection = new ObservableCollection<Tbl27Infraclass>(_uow.Tbl27Infraclasses.Find(x => x.SubclassId == selected.SubclassId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithSubclassIdInTableReference(Tbl24Subclass selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SubclassId == selected.SubclassId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithSubclassIdInTableComment(Tbl24Subclass selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SubclassId == selected.SubclassId));
            return collection;
        }


        #endregion

          #region Save Class 

        //------------------ Subclass ---------------------------------------
        public Tbl24Subclass SubclassUpdate(Tbl24Subclass home, Tbl24Subclass selected)
        {
            if (home != null) //update
            {
                home.SubclassName = selected.SubclassName;
                home.ClassId = selected.ClassId;
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
        public Tbl24Subclass SubclassAdd(Tbl24Subclass selected)
        {
            var home = new Tbl24Subclass() //add new
            {
                SubclassName = selected.SubclassName,
                ClassId = selected.ClassId,
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
                UpdaterDate = DateTime.Now
            };
            return home;
        }
        public void SubclassSave(Tbl24Subclass home, Tbl24Subclass selected)
        {

            if (selected.SubclassId != 0) //update
            {
                _uow.Tbl24Subclasses.Update(home);
            }
            else                                //add
                _uow.Tbl24Subclasses.Add(home);
            _uow.Complete();
        }

        public Tbl90Reference ReferenceExpertClassUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.ClassId = selected.ClassId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertClassAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                ClassId = selected.ClassId,
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
        public Tbl90Reference ReferenceSourceClassUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.ClassId = selected.ClassId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceClassAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                ClassId = selected.ClassId,
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
        public Tbl90Reference ReferenceAuthorClassUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.ClassId = selected.ClassId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorClassAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                ClassId = selected.ClassId,
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
        public Tbl93Comment CommentClassUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.ClassId = selected.ClassId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentClassAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                ClassId = selected.ClassId,
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

        #endregion

        #endregion

        #region Subclass

          #region Get Subclass

        //----------------------------------------   Subclass   ------------------------
        private ObservableCollection<T> GetSubclassesCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl24Subclasses
                    .Find(e => e.SubclassId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl24Subclasses
                    .Find(e => e.SubclassName.StartsWith(searchName))
                    .OrderBy(a => a.SubclassName)
                );
            return collection;
        }

        private ObservableCollection<T> GetSubclassesCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl24Subclasses
                .OrderBy(a => a.SubclassName));
            return collection;
        }
        public ObservableCollection<T> GetSubclassesCollectionFromSubclassIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl24Subclasses
                .Where(e => e.SubclassId == id)
                .OrderBy(k => k.SubclassName));

            return collection;
        }

        //-------------------------------------- Infraclass   -------------------------
        public ObservableCollection<T> GetInfraclassesCollectionFromSubclassIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl27Infraclasses
                .Where(e => e.SubclassId == id)
                .OrderBy(k => k.InfraclassName));
            return collection;
        }

        //-------------------------------------- Reference Experts   -------------------------
        public ObservableCollection<T> GetReferenceExpertsCollectionFromSubclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.SubclassId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Sources   -------------------------
        public ObservableCollection<T> GetReferenceSourcesCollectionFromSubclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.SubclassId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Authors   -------------------------
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromSubclassIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.SubclassId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Comments   -------------------------
        public ObservableCollection<T> GetCommentsCollectionFromSubclassIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.SubclassId == id)
                .OrderBy(e => e.Info));
            return collection;
        }

        //Function
        public int GetSubclassIdFromInfraclassesCollectionSelect(int id)
        {
            var coll = _context.Tbl27Infraclasses
                .SingleOrDefault(p => p.InfraclassId == id);

            if (coll == null) return 0;
            return coll.SubclassId;
        }

        #endregion

          #region Copy Subclass

        // ----------------------------------------   Infraclass  ------------------------
        public ObservableCollection<Tbl27Infraclass> CopyInfraclass(Tbl27Infraclass selected)
        {
            var dataset = _uow.Tbl27Infraclasses.GetById(selected.InfraclassId);
            var collection = new ObservableCollection<Tbl27Infraclass>();

            collection.Insert(0, new Tbl27Infraclass
            {
                InfraclassName = CultRes.StringsRes.DatasetNew,
                SubclassId = dataset.SubclassId,
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

        public ObservableCollection<Tbl90Reference> CopyReferenceSubclass(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        SubclassId = dataset.SubclassId,
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
                        SubclassId = dataset.SubclassId,
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
                        SubclassId = dataset.SubclassId,
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

          #region Delete Subclass

        //------------------------------ Subclass --------------------------------------------------------------------------------------------
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithRegnumIdInTableReference(Tbl24Subclass selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SubclassId == selected.SubclassId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithRegnumIdInTableComment(Tbl24Subclass selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SubclassId == selected.SubclassId));
            return collection;
        }

        //------------------------------ Infraclass --------------------------------------------------------------------------------------------
        public void DeleteInfraclass(Tbl27Infraclass selected)
        {
            _uow.Tbl27Infraclasses.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl30Legio> SearchForConnectedDatasetsWithInfraclassIdInTableLegio(Tbl27Infraclass selected)
        {
            var collection = new ObservableCollection<Tbl30Legio>(_uow.Tbl30Legios.Find(x => x.InfraclassId == selected.InfraclassId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithInfraclassIdInTableReference(Tbl27Infraclass selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.InfraclassId == selected.InfraclassId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithInfraclassIdInTableComment(Tbl27Infraclass selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.InfraclassId == selected.InfraclassId));
            return collection;
        }


        #endregion

          #region Save Subclass 

        //------------------ Infraclass ---------------------------------------
        public Tbl27Infraclass InfraclassUpdate(Tbl27Infraclass home, Tbl27Infraclass selected)
        {
            if (home != null) //update
            {
                home.InfraclassName = selected.InfraclassName;
                home.SubclassId = selected.SubclassId;
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
        public Tbl27Infraclass InfraclassAdd(Tbl27Infraclass selected)
        {
            var home = new Tbl27Infraclass() //add new
            {
                InfraclassName = selected.InfraclassName,
                SubclassId = selected.SubclassId,
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
                UpdaterDate = DateTime.Now
            };
            return home;
        }
        public void InfraclassSave(Tbl27Infraclass home, Tbl27Infraclass selected)
        {

            if (selected.InfraclassId != 0) //update
            {
                _uow.Tbl27Infraclasses.Update(home);
            }
            else                                //add
                _uow.Tbl27Infraclasses.Add(home);
            _uow.Complete();
        }

        public Tbl90Reference ReferenceExpertSubclassUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.SubclassId = selected.SubclassId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertSubclassAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                SubclassId = selected.SubclassId,
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
        public Tbl90Reference ReferenceSourceSubclassUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.SubclassId = selected.SubclassId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceSubclassAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                SubclassId = selected.SubclassId,
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
        public Tbl90Reference ReferenceAuthorSubclassUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.SubclassId = selected.SubclassId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorSubclassAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                SubclassId = selected.SubclassId,
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
        public Tbl93Comment CommentSubclassUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.SubclassId = selected.SubclassId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentSubclassAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                SubclassId = selected.SubclassId,
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

        #endregion

        #endregion

        #region Infraclass

          #region Get Infraclass

        //----------------------------------------   Infraclass   ------------------------
        private ObservableCollection<T> GetInfraclassesCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl27Infraclasses
                    .Find(e => e.InfraclassId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl27Infraclasses
                    .Find(e => e.InfraclassName.StartsWith(searchName))
                    .OrderBy(a => a.InfraclassName)
                );
            return collection;
        }

        private ObservableCollection<T> GetInfraclassesCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl27Infraclasses
                .OrderBy(a => a.InfraclassName));
            return collection;
        }
        public ObservableCollection<T> GetInfraclassesCollectionFromInfraclassIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl27Infraclasses
                .Where(e => e.InfraclassId == id)
                .OrderBy(k => k.InfraclassName));

            return collection;
        }

        //-------------------------------------- Legio   -------------------------
        public ObservableCollection<T> GetLegiosCollectionFromInfraclassIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl30Legios
                .Where(e => e.InfraclassId == id)
                .OrderBy(k => k.LegioName));
            return collection;
        }

        //-------------------------------------- Reference Experts   -------------------------
        public ObservableCollection<T> GetReferenceExpertsCollectionFromInfraclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.InfraclassId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Sources   -------------------------
        public ObservableCollection<T> GetReferenceSourcesCollectionFromInfraclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.InfraclassId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Authors   -------------------------
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromInfraclassIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.InfraclassId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Comments   -------------------------
        public ObservableCollection<T> GetCommentsCollectionFromInfraclassIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.InfraclassId == id)
                .OrderBy(e => e.Info));
            return collection;
        }

        //Function
        public int GetInfraclassIdFromLegiosCollectionSelect(int id)
        {
            var coll = _context.Tbl30Legios
                .SingleOrDefault(p => p.LegioId == id);

            if (coll == null) return 0;
            return coll.InfraclassId;
        }

        #endregion

          #region Copy Infraclass

        // ----------------------------------------   Legio  ------------------------
        public ObservableCollection<Tbl30Legio> CopyLegio(Tbl30Legio selected)
        {
            var dataset = _uow.Tbl30Legios.GetById(selected.LegioId);
            var collection = new ObservableCollection<Tbl30Legio>();

            collection.Insert(0, new Tbl30Legio
            {
                LegioName = CultRes.StringsRes.DatasetNew,
                InfraclassId = dataset.InfraclassId,
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

        public ObservableCollection<Tbl90Reference> CopyReferenceInfraclass(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        InfraclassId = dataset.InfraclassId,
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
                        InfraclassId = dataset.InfraclassId,
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
                        InfraclassId = dataset.InfraclassId,
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

        //---------  insert Case in CopyComment  -------------------------

        #endregion

          #region Delete Infraclass

        //------------------------------ Infraclass --------------------------------------------------------------------------------------------
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithRegnumIdInTableReference(Tbl27Infraclass selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.InfraclassId == selected.InfraclassId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithRegnumIdInTableComment(Tbl27Infraclass selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.InfraclassId == selected.InfraclassId));
            return collection;
        }

        //------------------------------ Legio --------------------------------------------------------------------------------------------
        public void DeleteLegio(Tbl30Legio selected)
        {
            _uow.Tbl30Legios.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl33Ordo> SearchForConnectedDatasetsWithLegioIdInTableOrdo(Tbl30Legio selected)
        {
            var collection = new ObservableCollection<Tbl33Ordo>(_uow.Tbl33Ordos.Find(x => x.LegioId == selected.LegioId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithLegioIdInTableReference(Tbl30Legio selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.LegioId == selected.LegioId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithLegioIdInTableComment(Tbl30Legio selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.LegioId == selected.LegioId));
            return collection;
        }


        #endregion

          #region Save Infraclass 

        //------------------ Legio ---------------------------------------
        public Tbl30Legio LegioUpdate(Tbl30Legio home, Tbl30Legio selected)
        {
            if (home != null) //update
            {
                home.LegioName = selected.LegioName;
                home.InfraclassId = selected.InfraclassId;
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
        public Tbl30Legio LegioAdd(Tbl30Legio selected)
        {
            var home = new Tbl30Legio() //add new
            {
                LegioName = selected.LegioName,
                InfraclassId = selected.InfraclassId,
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
                UpdaterDate = DateTime.Now
            };
            return home;
        }
        public void LegioSave(Tbl30Legio home, Tbl30Legio selected)
        {

            if (selected.LegioId != 0) //update
            {
                _uow.Tbl30Legios.Update(home);
            }
            else                                //add
                _uow.Tbl30Legios.Add(home);
            _uow.Complete();
        }

        public Tbl90Reference ReferenceExpertInfraclassUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.InfraclassId = selected.InfraclassId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertInfraclassAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                InfraclassId = selected.InfraclassId,
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
        public Tbl90Reference ReferenceSourceInfraclassUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.InfraclassId = selected.InfraclassId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceInfraclassAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                InfraclassId = selected.InfraclassId,
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
        public Tbl90Reference ReferenceAuthorInfraclassUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.InfraclassId = selected.InfraclassId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorInfraclassAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                InfraclassId = selected.InfraclassId,
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
        public Tbl93Comment CommentInfraclassUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.InfraclassId = selected.InfraclassId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentInfraclassAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                InfraclassId = selected.InfraclassId,
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

        #endregion

        #endregion

        #region Legio

          #region Get Legio

        //----------------------------------------   Legio   ------------------------
        private ObservableCollection<T> GetLegiosCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl30Legios
                    .Find(e => e.LegioId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl30Legios
                    .Find(e => e.LegioName.StartsWith(searchName))
                    .OrderBy(a => a.LegioName)
                );
            return collection;
        }

        private ObservableCollection<T> GetLegiosCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl30Legios
                .OrderBy(a => a.LegioName));
            return collection;
        }
        public ObservableCollection<T> GetLegiosCollectionFromLegioIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl30Legios
                .Where(e => e.LegioId == id)
                .OrderBy(k => k.LegioName));

            return collection;
        }

        //-------------------------------------- Ordo   -------------------------
        public ObservableCollection<T> GetOrdosCollectionFromLegioIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl33Ordos
                .Where(e => e.LegioId == id)
                .OrderBy(k => k.OrdoName));
            return collection;
        }

        //-------------------------------------- Reference Experts   -------------------------
        public ObservableCollection<T> GetReferenceExpertsCollectionFromLegioIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.LegioId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Sources   -------------------------
        public ObservableCollection<T> GetReferenceSourcesCollectionFromLegioIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.LegioId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Authors   -------------------------
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromLegioIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.LegioId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Comments   -------------------------
        public ObservableCollection<T> GetCommentsCollectionFromLegioIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.LegioId == id)
                .OrderBy(e => e.Info));
            return collection;
        }

        //Function
        public int GetLegioIdFromOrdosCollectionSelect(int id)
        {
            var coll = _context.Tbl33Ordos
                .SingleOrDefault(p => p.OrdoId == id);

            if (coll == null) return 0;
            return coll.LegioId;
        }

        #endregion

          #region Copy Legio

        // ----------------------------------------   Ordo  ------------------------
        public ObservableCollection<Tbl33Ordo> CopyOrdo(Tbl33Ordo selected)
        {
            var dataset = _uow.Tbl33Ordos.GetById(selected.OrdoId);
            var collection = new ObservableCollection<Tbl33Ordo>();

            collection.Insert(0, new Tbl33Ordo
            {
                OrdoName = CultRes.StringsRes.DatasetNew,
                LegioId = dataset.LegioId,
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

        public ObservableCollection<Tbl90Reference> CopyReferenceLegio(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        LegioId = dataset.LegioId,
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
                        LegioId = dataset.LegioId,
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
                        LegioId = dataset.LegioId,
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

          #region Delete Legio

        //------------------------------ Legio --------------------------------------------------------------------------------------------
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithRegnumIdInTableReference(Tbl30Legio selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.LegioId == selected.LegioId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithRegnumIdInTableComment(Tbl30Legio selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.LegioId == selected.LegioId));
            return collection;
        }

        //------------------------------ Ordo --------------------------------------------------------------------------------------------
        public void DeleteOrdo(Tbl33Ordo selected)
        {
            _uow.Tbl33Ordos.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl36Subordo> SearchForConnectedDatasetsWithOrdoIdInTableSubordo(Tbl33Ordo selected)
        {
            var collection = new ObservableCollection<Tbl36Subordo>(_uow.Tbl36Subordos.Find(x => x.OrdoId == selected.OrdoId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithOrdoIdInTableReference(Tbl33Ordo selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.OrdoId == selected.OrdoId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithOrdoIdInTableComment(Tbl33Ordo selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.OrdoId == selected.OrdoId));
            return collection;
        }


        #endregion

          #region Save Legio 

        //------------------ Ordo ---------------------------------------
        public Tbl33Ordo OrdoUpdate(Tbl33Ordo home, Tbl33Ordo selected)
        {
            if (home != null) //update
            {
                home.OrdoName = selected.OrdoName;
                home.LegioId = selected.LegioId;
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
        public Tbl33Ordo OrdoAdd(Tbl33Ordo selected)
        {
            var home = new Tbl33Ordo() //add new
            {
                OrdoName = selected.OrdoName,
                LegioId = selected.LegioId,
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
                UpdaterDate = DateTime.Now
            };
            return home;
        }
        public void OrdoSave(Tbl33Ordo home, Tbl33Ordo selected)
        {

            if (selected.OrdoId != 0) //update
            {
                _uow.Tbl33Ordos.Update(home);
            }
            else                                //add
                _uow.Tbl33Ordos.Add(home);
            _uow.Complete();
        }

        public Tbl90Reference ReferenceExpertLegioUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.LegioId = selected.LegioId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertLegioAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                LegioId = selected.LegioId,
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
        public Tbl90Reference ReferenceSourceLegioUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.LegioId = selected.LegioId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceLegioAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                LegioId = selected.LegioId,
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
        public Tbl90Reference ReferenceAuthorLegioUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.LegioId = selected.LegioId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorLegioAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                LegioId = selected.LegioId,
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
        public Tbl93Comment CommentLegioUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.LegioId = selected.LegioId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentLegioAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                LegioId = selected.LegioId,
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

        #endregion

        #endregion

        #region Ordo

          #region Get Ordo

        //----------------------------------------   Ordo   ------------------------
        private ObservableCollection<T> GetOrdosCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl33Ordos
                    .Find(e => e.OrdoId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl33Ordos
                    .Find(e => e.OrdoName.StartsWith(searchName))
                    .OrderBy(a => a.OrdoName)
                );
            return collection;
        }

        private ObservableCollection<T> GetOrdosCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl33Ordos
                .OrderBy(a => a.OrdoName));
            return collection;
        }
        public ObservableCollection<T> GetOrdosCollectionFromOrdoIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl33Ordos
                .Where(e => e.OrdoId == id)
                .OrderBy(k => k.OrdoName));

            return collection;
        }

        //-------------------------------------- Subordo   -------------------------
        public ObservableCollection<T> GetSubordosCollectionFromOrdoIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl36Subordos
                .Where(e => e.OrdoId == id)
                .OrderBy(k => k.SubordoName));
            return collection;
        }

        //-------------------------------------- Reference Experts   -------------------------
        public ObservableCollection<T> GetReferenceExpertsCollectionFromOrdoIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.OrdoId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Sources   -------------------------
        public ObservableCollection<T> GetReferenceSourcesCollectionFromOrdoIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.OrdoId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Authors   -------------------------
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromOrdoIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.OrdoId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Comments   -------------------------
        public ObservableCollection<T> GetCommentsCollectionFromOrdoIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.OrdoId == id)
                .OrderBy(e => e.Info));
            return collection;
        }

        //Function
        public int GetOrdoIdFromSubordosCollectionSelect(int id)
        {
            var coll = _context.Tbl36Subordos
                .SingleOrDefault(p => p.SubordoId == id);

            if (coll == null) return 0;
            return coll.OrdoId;
        }

        #endregion

          #region Copy Ordo

        // ----------------------------------------   Subordo  ------------------------
        public ObservableCollection<Tbl36Subordo> CopySubordo(Tbl36Subordo selected)
        {
            var dataset = _uow.Tbl36Subordos.GetById(selected.SubordoId);
            var collection = new ObservableCollection<Tbl36Subordo>();

            collection.Insert(0, new Tbl36Subordo
            {
                SubordoName = CultRes.StringsRes.DatasetNew,
                OrdoId = dataset.OrdoId,
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

        public ObservableCollection<Tbl90Reference> CopyReferenceOrdo(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        OrdoId = dataset.OrdoId,
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
                        OrdoId = dataset.OrdoId,
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
                        OrdoId = dataset.OrdoId,
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

          #region Delete Ordo

        //------------------------------ Ordo --------------------------------------------------------------------------------------------
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithRegnumIdInTableReference(Tbl33Ordo selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.OrdoId == selected.OrdoId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithRegnumIdInTableComment(Tbl33Ordo selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.OrdoId == selected.OrdoId));
            return collection;
        }

        //------------------------------ Subordo --------------------------------------------------------------------------------------------
        public void DeleteSubordo(Tbl36Subordo selected)
        {
            _uow.Tbl36Subordos.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl39Infraordo> SearchForConnectedDatasetsWithSubordoIdInTableInfraordo(Tbl36Subordo selected)
        {
            var collection = new ObservableCollection<Tbl39Infraordo>(_uow.Tbl39Infraordos.Find(x => x.SubordoId == selected.SubordoId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithSubordoIdInTableReference(Tbl36Subordo selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SubordoId == selected.SubordoId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithSubordoIdInTableComment(Tbl36Subordo selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SubordoId == selected.SubordoId));
            return collection;
        }


        #endregion

          #region Save Ordo 

        //------------------ Subordo ---------------------------------------
        public Tbl36Subordo SubordoUpdate(Tbl36Subordo home, Tbl36Subordo selected)
        {
            if (home != null) //update
            {
                home.SubordoName = selected.SubordoName;
                home.OrdoId = selected.OrdoId;
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
        public Tbl36Subordo SubordoAdd(Tbl36Subordo selected)
        {
            var home = new Tbl36Subordo() //add new
            {
                SubordoName = selected.SubordoName,
                OrdoId = selected.OrdoId,
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
                UpdaterDate = DateTime.Now
            };
            return home;
        }
        public void SubordoSave(Tbl36Subordo home, Tbl36Subordo selected)
        {

            if (selected.SubordoId != 0) //update
            {
                _uow.Tbl36Subordos.Update(home);
            }
            else                                //add
                _uow.Tbl36Subordos.Add(home);
            _uow.Complete();
        }

        public Tbl90Reference ReferenceExpertOrdoUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.OrdoId = selected.OrdoId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertOrdoAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                OrdoId = selected.OrdoId,
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
        public Tbl90Reference ReferenceSourceOrdoUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.OrdoId = selected.OrdoId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceOrdoAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                OrdoId = selected.OrdoId,
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
        public Tbl90Reference ReferenceAuthorOrdoUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.OrdoId = selected.OrdoId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorOrdoAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                OrdoId = selected.OrdoId,
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
        public Tbl93Comment CommentOrdoUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.OrdoId = selected.OrdoId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentOrdoAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                OrdoId = selected.OrdoId,
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

        #endregion

        #endregion

        #region Subordo

          #region Get Subordo

        //----------------------------------------   Subordo   ------------------------
        private ObservableCollection<T> GetSubordosCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl36Subordos
                    .Find(e => e.SubordoId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl36Subordos
                    .Find(e => e.SubordoName.StartsWith(searchName))
                    .OrderBy(a => a.SubordoName)
                );
            return collection;
        }

        private ObservableCollection<T> GetSubordosCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl36Subordos
                .OrderBy(a => a.SubordoName));
            return collection;
        }
        public ObservableCollection<T> GetSubordosCollectionFromSubordoIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl36Subordos
                .Where(e => e.SubordoId == id)
                .OrderBy(k => k.SubordoName));

            return collection;
        }

        //-------------------------------------- Infraordo   -------------------------
        public ObservableCollection<T> GetInfraordosCollectionFromSubordoIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl39Infraordos
                .Where(e => e.SubordoId == id)
                .OrderBy(k => k.InfraordoName));
            return collection;
        }

        //-------------------------------------- Reference Experts   -------------------------
        public ObservableCollection<T> GetReferenceExpertsCollectionFromSubordoIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.SubordoId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Sources   -------------------------
        public ObservableCollection<T> GetReferenceSourcesCollectionFromSubordoIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.SubordoId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Authors   -------------------------
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromSubordoIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.SubordoId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Comments   -------------------------
        public ObservableCollection<T> GetCommentsCollectionFromSubordoIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.SubordoId == id)
                .OrderBy(e => e.Info));
            return collection;
        }

        //Function
        public int GetSubordoIdFromInfraordosCollectionSelect(int id)
        {
            var coll = _context.Tbl39Infraordos
                .SingleOrDefault(p => p.InfraordoId == id);

            if (coll == null) return 0;
            return coll.SubordoId;
        }

        #endregion

          #region Copy Subordo

        // ----------------------------------------   Infraordo  ------------------------
        public ObservableCollection<Tbl39Infraordo> CopyInfraordo(Tbl39Infraordo selected)
        {
            var dataset = _uow.Tbl39Infraordos.GetById(selected.InfraordoId);
            var collection = new ObservableCollection<Tbl39Infraordo>();

            collection.Insert(0, new Tbl39Infraordo
            {
                InfraordoName = CultRes.StringsRes.DatasetNew,
                SubordoId = dataset.SubordoId,
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

        public ObservableCollection<Tbl90Reference> CopyReferenceSubordo(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        SubordoId = dataset.SubordoId,
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
                        SubordoId = dataset.SubordoId,
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
                        SubordoId = dataset.SubordoId,
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

          #region Delete Subordo

        //------------------------------ Subordo --------------------------------------------------------------------------------------------
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithRegnumIdInTableReference(Tbl36Subordo selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SubordoId == selected.SubordoId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithRegnumIdInTableComment(Tbl36Subordo selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SubordoId == selected.SubordoId));
            return collection;
        }

        //------------------------------ Infraordo --------------------------------------------------------------------------------------------
        public void DeleteInfraordo(Tbl39Infraordo selected)
        {
            _uow.Tbl39Infraordos.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl42Superfamily> SearchForConnectedDatasetsWithInfraordoIdInTableSuperfamily(Tbl39Infraordo selected)
        {
            var collection = new ObservableCollection<Tbl42Superfamily>(_uow.Tbl42Superfamilies.Find(x => x.InfraordoId == selected.InfraordoId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithInfraordoIdInTableReference(Tbl39Infraordo selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.InfraordoId == selected.InfraordoId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithInfraordoIdInTableComment(Tbl39Infraordo selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.InfraordoId == selected.InfraordoId));
            return collection;
        }


        #endregion

          #region Save Subordo 

        //------------------ Infraordo ---------------------------------------
        public Tbl39Infraordo InfraordoUpdate(Tbl39Infraordo home, Tbl39Infraordo selected)
        {
            if (home != null) //update
            {
                home.InfraordoName = selected.InfraordoName;
                home.SubordoId = selected.SubordoId;
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
        public Tbl39Infraordo InfraordoAdd(Tbl39Infraordo selected)
        {
            var home = new Tbl39Infraordo() //add new
            {
                InfraordoName = selected.InfraordoName,
                SubordoId = selected.SubordoId,
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
                UpdaterDate = DateTime.Now
            };
            return home;
        }
        public void InfraordoSave(Tbl39Infraordo home, Tbl39Infraordo selected)
        {

            if (selected.InfraordoId != 0) //update
            {
                _uow.Tbl39Infraordos.Update(home);
            }
            else                                //add
                _uow.Tbl39Infraordos.Add(home);
            _uow.Complete();
        }

        public Tbl90Reference ReferenceExpertSubordoUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.SubordoId = selected.SubordoId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertSubordoAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                SubordoId = selected.SubordoId,
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
        public Tbl90Reference ReferenceSourceSubordoUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.SubordoId = selected.SubordoId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceSubordoAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                SubordoId = selected.SubordoId,
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
        public Tbl90Reference ReferenceAuthorSubordoUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.SubordoId = selected.SubordoId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorSubordoAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                SubordoId = selected.SubordoId,
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
        public Tbl93Comment CommentSubordoUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.SubordoId = selected.SubordoId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentSubordoAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                SubordoId = selected.SubordoId,
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

        #endregion

        #endregion


        #region Infraordo

        #region Get Infraordo

        //----------------------------------------   Infraordo   ------------------------
        private ObservableCollection<T> GetInfraordosCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl39Infraordos
                    .Find(e => e.InfraordoId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl39Infraordos
                    .Find(e => e.InfraordoName.StartsWith(searchName))
                    .OrderBy(a => a.InfraordoName)
                );
            return collection;
        }

        private ObservableCollection<T> GetInfraordosCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl39Infraordos
                .OrderBy(a => a.InfraordoName));
            return collection;
        }
        public ObservableCollection<T> GetInfraordosCollectionFromInfraordoIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl39Infraordos
                .Where(e => e.InfraordoId == id)
                .OrderBy(k => k.InfraordoName));

            return collection;
        }

        //-------------------------------------- Superfamily   -------------------------
        public ObservableCollection<T> GetSuperfamiliesCollectionFromInfraordoIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl42Superfamilies
                .Where(e => e.InfraordoId == id)
                .OrderBy(k => k.SuperfamilyName));
            return collection;
        }

        //-------------------------------------- Reference Experts   -------------------------
        public ObservableCollection<T> GetReferenceExpertsCollectionFromInfraordoIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.InfraordoId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Sources   -------------------------
        public ObservableCollection<T> GetReferenceSourcesCollectionFromInfraordoIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.InfraordoId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Reference Authors   -------------------------
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromInfraordoIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.InfraordoId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        //-------------------------------------- Comments   -------------------------
        public ObservableCollection<T> GetCommentsCollectionFromInfraordoIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.InfraordoId == id)
                .OrderBy(e => e.Info));
            return collection;
        }

        //Function
        public int GetInfraordoIdFromSuperfamiliesCollectionSelect(int id)
        {
            var coll = _context.Tbl42Superfamilies
                .SingleOrDefault(p => p.SuperfamilyId == id);

            if (coll == null) return 0;
            return coll.InfraordoId;
        }

        #endregion

        #region Copy Infraordo

        // ----------------------------------------   Superfamily  ------------------------
        public ObservableCollection<Tbl42Superfamily> CopySuperfamily(Tbl42Superfamily selected)
        {
            var dataset = _uow.Tbl42Superfamilies.GetById(selected.SuperfamilyId);
            var collection = new ObservableCollection<Tbl42Superfamily>();

            collection.Insert(0, new Tbl42Superfamily
            {
                SuperfamilyName = CultRes.StringsRes.DatasetNew,
                InfraordoId = dataset.InfraordoId,
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

        public ObservableCollection<Tbl90Reference> CopyReferenceInfraordo(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        InfraordoId = dataset.InfraordoId,
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
                        InfraordoId = dataset.InfraordoId,
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
                        InfraordoId = dataset.InfraordoId,
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

        #region Delete Infraordo

        //------------------------------ Infraordo --------------------------------------------------------------------------------------------
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithRegnumIdInTableReference(Tbl39Infraordo selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.InfraordoId == selected.InfraordoId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithRegnumIdInTableComment(Tbl39Infraordo selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.InfraordoId == selected.InfraordoId));
            return collection;
        }

        //------------------------------ Superfamily --------------------------------------------------------------------------------------------
        public void DeleteSuperfamily(Tbl42Superfamily selected)
        {
            _uow.Tbl42Superfamilies.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl45Family> SearchForConnectedDatasetsWithSuperfamilyIdInTableFamily(Tbl42Superfamily selected)
        {
            var collection = new ObservableCollection<Tbl45Family>(_uow.Tbl45Families.Find(x => x.SuperfamilyId == selected.SuperfamilyId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithSuperfamilyIdInTableReference(Tbl42Superfamily selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SuperfamilyId == selected.SuperfamilyId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithSuperfamilyIdInTableComment(Tbl42Superfamily selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SuperfamilyId == selected.SuperfamilyId));
            return collection;
        }


        #endregion

        #region Save Infraordo 

        //------------------ Superfamily ---------------------------------------
        public Tbl42Superfamily SuperfamilyUpdate(Tbl42Superfamily home, Tbl42Superfamily selected)
        {
            if (home != null) //update
            {
                home.SuperfamilyName = selected.SuperfamilyName;
                home.InfraordoId = selected.InfraordoId;
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
        public Tbl42Superfamily SuperfamilyAdd(Tbl42Superfamily selected)
        {
            var home = new Tbl42Superfamily() //add new
            {
                SuperfamilyName = selected.SuperfamilyName,
                InfraordoId = selected.InfraordoId,
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
                UpdaterDate = DateTime.Now
            };
            return home;
        }
        public void SuperfamilySave(Tbl42Superfamily home, Tbl42Superfamily selected)
        {

            if (selected.SuperfamilyId != 0) //update
            {
                _uow.Tbl42Superfamilies.Update(home);
            }
            else                                //add
                _uow.Tbl42Superfamilies.Add(home);
            _uow.Complete();
        }

        public Tbl90Reference ReferenceExpertInfraordoUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.InfraordoId = selected.InfraordoId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertInfraordoAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                InfraordoId = selected.InfraordoId,
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
        public Tbl90Reference ReferenceSourceInfraordoUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.InfraordoId = selected.InfraordoId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceInfraordoAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                InfraordoId = selected.InfraordoId,
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
        public Tbl90Reference ReferenceAuthorInfraordoUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.InfraordoId = selected.InfraordoId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorInfraordoAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                InfraordoId = selected.InfraordoId,
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
        public Tbl93Comment CommentInfraordoUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.InfraordoId = selected.InfraordoId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentInfraordoAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                InfraordoId = selected.InfraordoId,
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

        #endregion

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
                case "Class":
                    collection.Insert(0, new Tbl93Comment
                    {
                        ClassId = dataset.ClassId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Subclass":
                    collection.Insert(0, new Tbl93Comment
                    {
                        SubclassId = dataset.SubclassId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Infraclass":
                    collection.Insert(0, new Tbl93Comment
                    {
                        InfraclassId = dataset.InfraclassId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Legio":
                    collection.Insert(0, new Tbl93Comment
                    {
                        LegioId = dataset.LegioId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Ordo":
                    collection.Insert(0, new Tbl93Comment
                    {
                        OrdoId = dataset.OrdoId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Subordo":
                    collection.Insert(0, new Tbl93Comment
                    {
                        SubordoId = dataset.SubordoId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Infraordo":
                    collection.Insert(0, new Tbl93Comment
                    {
                        InfraordoId = dataset.InfraordoId,
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
