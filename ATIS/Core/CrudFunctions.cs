using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using ATIS.Dal.Models;
using ATIS.Ui.Helper;
using Microsoft.EntityFrameworkCore;

namespace ATIS.Ui.Core
{

    public class CrudFunctions : ViewModelBase
    {
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();

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
                        "Regnum" => GetRegnumsCollectionAllOrderBy<T>(),
                        "Phylum" => GetPhylumsCollectionAllOrderBy<T>(),
                        "Division" => GetDivisionsCollectionAllOrderBy<T>(),
                        "Subphylum" => GetSubphylumsCollectionAllOrderBy<T>(),
                        "Subdivision" => GetSubdivisionsCollectionAllOrderBy<T>(),
                        "Superclass" => GetSuperclassesCollectionAllOrderBy<T>(),
                        "Classe" => GetClassesCollectionAllOrderBy<T>(),
                        "Subclass" => GetSubclassesCollectionAllOrderBy<T>(),
                        "Infraclass" => GetInfraclassesCollectionAllOrderBy<T>(),
                        "Legio" => GetLegiosCollectionAllOrderBy<T>(),
                        "Ordo" => GetOrdosCollectionAllOrderBy<T>(),
                        "Subordo" => GetSubordosCollectionAllOrderBy<T>(),
                        "Infraordo" => GetInfraordosCollectionAllOrderBy<T>(),
                        "Superfamily" => GetSuperfamiliesCollectionAllOrderBy<T>(),
                        "Family" => GetFamiliesCollectionAllOrderBy<T>(),
                        "Subfamily" => GetSubfamiliesCollectionAllOrderBy<T>(),
                        "Infrafamily" => GetInfrafamiliesCollectionAllOrderBy<T>(),
                        "Supertribus" => GetSupertribussesCollectionAllOrderBy<T>(),
                        "Tribus" => GetTribussesCollectionAllOrderBy<T>(),
                        "Subtribus" => GetSubtribussesCollectionAllOrderBy<T>(),
                        "Infratribus" => GetInfratribussesCollectionAllOrderBy<T>(),
                        "Genus" => GetGenussesCollectionAllOrderBy<T>(),
                        "Speciesgroup" => GetSpeciesgroupsCollectionAllOrderBy<T>(),
                        "FiSpecies" => GetFiSpeciessesCollectionAllOrderBy<T>(),
                        "PlSpecies" => GetPlSpeciessesCollectionAllOrderBy<T>(),
                        "Name" => GetNamesCollectionAllOrderBy<T>(),
                        "Synonym" => GetSynonymsCollectionAllOrderBy<T>(),
                        "Reference" => GetReferencesCollectionAllOrderBy<T>(),
                        "Expert" => GetExpertsCollectionAllOrderBy<T>(),
                        "Source" => GetSourcesCollectionAllOrderBy<T>(),
                        "Author" => GetAuthorsCollectionAllOrderBy<T>(),
                        "Comment" => GetCommentsCollectionAllOrderBy<T>(),
                        _ => collection
                    };
                    break;
                default:
                    {
                        collection = name switch
                        {
                            "Regnum" => GetRegnumsCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Phylum" => GetPhylumsCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Division" => GetDivisionsCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Subphylum" => GetSubphylumsCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Subdivision" => GetSubdivisionsCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Superclass" => GetSuperclassesCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Classe" => GetClassesCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Subclass" => GetSubclassesCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Infraclass" => GetInfraclassesCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Legio" => GetLegiosCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Ordo" => GetOrdosCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Subordo" => GetSubordosCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Infraordo" => GetInfraordosCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Superfamily" => GetSuperfamiliesCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Family" => GetFamiliesCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Subfamily" => GetSubfamiliesCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Infrafamily" => GetInfrafamiliesCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Supertribus" => GetSupertribussesCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Tribus" => GetTribussesCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Subtribus" => GetSubtribussesCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Infratribus" => GetInfratribussesCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Genus" => GetGenussesCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Speciesgroup" => GetSpeciesgroupsCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "FiSpecies" => GetFiSpeciessesCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "PlSpecies" => GetPlSpeciessesCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Name" => GetNamesCollectionFromSearchNameOrIdOrderBy<T>(searchName),
                            "Synonym" => GetSynonymsCollectionFromSearchNameOrIdOrderBy<T>(searchName),
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
                case "Regnum":
                    collection = GetRegnumsCollectionAllOrderBy<T>();
                    break;
                case "Phylum":
                    collection = GetPhylumsCollectionAllOrderBy<T>();
                    break;
                case "Division":
                    collection = GetDivisionsCollectionAllOrderBy<T>();
                    break;
                case "Subphylum":
                    collection = GetSubphylumsCollectionAllOrderBy<T>();
                    break;
                case "Subdivision":
                    collection = GetSubdivisionsCollectionAllOrderBy<T>();
                    break;
                case "Superclass":
                    collection = GetSuperclassesCollectionAllOrderBy<T>();
                    break;
                case "Classe":
                    collection = GetClassesCollectionAllOrderBy<T>();
                    break;
                case "Subclass":
                    collection = GetSubclassesCollectionAllOrderBy<T>();
                    break;
                case "Infraclass":
                    collection = GetInfraclassesCollectionAllOrderBy<T>();
                    break;
                case "Legio":
                    collection = GetLegiosCollectionAllOrderBy<T>();
                    break;
                case "Ordo":
                    collection = GetOrdosCollectionAllOrderBy<T>();
                    break;
                case "Subordo":
                    collection = GetSubordosCollectionAllOrderBy<T>();
                    break;
                case "Infraordo":
                    collection = GetInfraordosCollectionAllOrderBy<T>();
                    break;
                case "Superfamily":
                    collection = GetSuperfamiliesCollectionAllOrderBy<T>();
                    break;
                case "Family":
                    collection = GetFamiliesCollectionAllOrderBy<T>();
                    break;
                case "Subfamily":
                    collection = GetSubfamiliesCollectionAllOrderBy<T>();
                    break;
                case "Infrafamily":
                    collection = GetInfrafamiliesCollectionAllOrderBy<T>();
                    break;
                case "Supertribus":
                    collection = GetSupertribussesCollectionAllOrderBy<T>();
                    break;
                case "Tribus":
                    collection = GetTribussesCollectionAllOrderBy<T>();
                    break;
                case "Subtribus":
                    collection = GetSubtribussesCollectionAllOrderBy<T>();
                    break;
                case "Infratribus":
                    collection = GetInfratribussesCollectionAllOrderBy<T>();
                    break;
                case "Genus":
                    collection = GetGenussesCollectionAllOrderBy<T>();
                    break;
                case "Speciesgroup":
                    collection = GetSpeciesgroupsCollectionAllOrderBy<T>();
                    break;
                case "FiSpecies":
                    collection = GetFiSpeciessesCollectionAllOrderBy<T>();
                    break;
                case "PlSpecies":
                    collection = GetPlSpeciessesCollectionAllOrderBy<T>();
                    break;
                case "Name":
                    collection = GetNamesCollectionAllOrderBy<T>();
                    break;
                case "Synonym":
                    collection = GetSynonymsCollectionAllOrderBy<T>();
                    break;
                case "References":
                    collection = GetReferencesCollectionAllOrderBy<T>();
                    break;
                case "Expert":
                    collection = GetExpertsCollectionAllOrderBy<T>();
                    break;
                case "Source":
                    collection = GetSourcesCollectionAllOrderBy<T>();
                    break;
                case "Author":
                    collection = GetAuthorsCollectionAllOrderBy<T>();
                    break;
                case "Comment":
                    collection = GetCommentsCollectionAllOrderBy<T>();
                    break;
                case "Country":
                    collection = GetCountriesCollectionAllOrderBy<T>();
                    break;
            }

            return collection;
        }


        #region Regnum

        #region Get Regnum
        public ObservableCollection<T> GetRegnumsCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
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
        public Tbl03Regnum GetRegnumSingleByRegnumId<T>(int id)
        {
            Tbl03Regnum single = _uow.Tbl03Regnums.GetById(id);
            //    Tbl03Regnum single = _context.Tbl03Regnums.FirstOrDefault(a => a.RegnumId == id);
            return single;
        }
        public ObservableCollection<Tbl03Regnum> GetLastRegnumsDatasetOrderById()
        {
            var collection = _context.Tbl03Regnums
                .OrderBy(c => c.RegnumId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl03Regnum> { collection };
        }
        //Function
        public int RegnumIdFromPhylumsCollectionSelect(int id)
        {
            var coll = _context.Tbl06Phylums.SingleOrDefault(p => p.PhylumId == id);

            if (coll == null) return 0;
            return coll.RegnumId;
        }
        //Function
        public int RegnumIdFromDivisionsCollectionSelect(int id)
        {
            var coll = _context.Tbl09Divisions.SingleOrDefault(p => p.DivisionId == id);

            if (coll == null) return 0;
            return coll.RegnumId;
        }
        #endregion

        #region Copy Regnum    
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
        #endregion

        #region Delete Regnum
        public void DeleteRegnum(Tbl03Regnum selected)
        {
            _uow.Tbl03Regnums.Remove(selected);
            _uow.Complete();
        }
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
            else           //add
                _uow.Tbl03Regnums.Add(home);

            _uow.Complete();
        }
        #endregion

        #endregion

        #region Phylum

        #region Get Phylum
        public ObservableCollection<T> GetPhylumsCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_context.Tbl06Phylums
                    .Where(e => e.PhylumId == id)
                    .AsNoTracking())
                : new ObservableCollection<T>((IEnumerable<T>)_context.Tbl06Phylums
                    .Where(e => e.PhylumName.StartsWith(searchName))
                    .OrderBy(a => a.PhylumName)
                    .AsNoTracking());
            return collection;
        }
        private ObservableCollection<T> GetPhylumsCollectionAllOrderBy<T>()
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl06Phylums
                .OrderBy(a => a.PhylumName)
                .AsNoTracking());
            return collection;
        }
        public ObservableCollection<T> GetPhylumsCollectionFromRegnumIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl06Phylums
                .Where(e => e.RegnumId == id)
                .OrderBy(k => k.PhylumName));
            return collection;
        }
        public ObservableCollection<T> GetPhylumsCollectionFromPhylumIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl06Phylums
                .Where(e => e.PhylumId == id)
                .OrderBy(k => k.PhylumName)
                .AsNoTracking());
            return collection;
        }
        public Tbl06Phylum GetPhylumSingleByPhylumId(int id)
        {
            Tbl06Phylum single = _uow.Tbl06Phylums.GetById(id);
            //    Tbl06Phylum single = _context.Tbl06Phylums.FirstOrDefault(a => a.PhylumId == id);
            return single;
        }
        public ObservableCollection<Tbl06Phylum> GetLastPhylumsDatasetOrderById()
        {
            var collection = _context.Tbl06Phylums
                .OrderBy(c => c.PhylumId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl06Phylum> { collection };
        }

        //Function
        public int PhylumIdFromSubphylumsCollectionSelect(int id)
        {
            var coll = _context.Tbl12Subphylums
                .SingleOrDefault(p => p.SubphylumId == id);

            if (coll == null) return 0;
            return coll.PhylumId;
        }

        public ObservableCollection<Tbl06Phylum> GetConnectedDatasetsWithRegnumIdInTablePhylum(int selectedId)
        {
            var collection = new ObservableCollection<Tbl06Phylum>(_uow.Tbl06Phylums.Find(x => x.RegnumId == selectedId));
            return collection;
        }
        #endregion

        #region Copy Phylum
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
        #endregion

        #region Delete Phylum
        public void DeletePhylum(Tbl06Phylum selected)
        {
            //   _uow.Tbl06Phylums.Remove(selected);
            _context.Tbl06Phylums.Remove(selected);
            //  _uow.Complete();
            _context.SaveChanges();
        }
        #endregion

        #region Save Phylum 
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
            //_uow.BeginTransaction();
            if (selected.PhylumId != 0) //update
                _uow.Tbl06Phylums.Update(home);
            else                        //add
                _uow.Tbl06Phylums.Add(home);

            _uow.Complete();
            //_uow.Commit();
        }
        #endregion

        #endregion

        #region Division

        #region Get Division
        public ObservableCollection<T> GetDivisionsCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
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
        public ObservableCollection<T> GetDivisionsCollectionFromRegnumIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl09Divisions
                .Where(e => e.RegnumId == id)
                .OrderBy(k => k.DivisionName));
            return collection;
        }
        public Tbl09Division GetDivisionSingleByDivisionId<T>(int id)
        {
            Tbl09Division single = _uow.Tbl09Divisions.GetById(id);
            //    Tbl09Division single = _context.Tbl09Divisions.FirstOrDefault(a => a.DivisionId == id);
            return single;
        }
        public ObservableCollection<Tbl09Division> GetLastDivisionsDatasetOrderById()
        {
            var collection = _context.Tbl09Divisions
                .OrderBy(c => c.DivisionId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl09Division> { collection };
        }
        //Function
        public int GetDivisionIdFromSubdivisionsCollectionSelect(int id)
        {
            var coll = _context.Tbl15Subdivisions
                .SingleOrDefault(p => p.SubdivisionId == id);

            if (coll == null) return 0;
            return coll.DivisionId;
        }
        public ObservableCollection<Tbl09Division> GetConnectedDatasetsWithRegnumIdInTableDivision(int selectedId)
        {
            var collection = new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions.Find(x => x.RegnumId == selectedId));
            return collection;
        }
        #endregion

        #region Copy Division
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
        #endregion

        #region Delete Division
        public void DeleteDivision(Tbl09Division selected)
        {
            _uow.Tbl09Divisions.Remove(selected);
            _uow.Complete();
        }
        #endregion

        #region Save Division 
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
                _uow.Tbl09Divisions.Update(home);
            else                                //add
                _uow.Tbl09Divisions.Add(home);
            _uow.Complete();
        }
        #endregion

        #endregion

        #region Subphylum

        #region Get Subphylum
        public ObservableCollection<T> GetSubphylumsCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
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
        public ObservableCollection<T> GetSubphylumsCollectionFromPhylumIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl12Subphylums
                .Where(e => e.PhylumId == id)
                .OrderBy(k => k.SubphylumName));
            return collection;
        }
        public Tbl12Subphylum GetSubphylumSingleBySubphylumId<T>(int id)
        {
            Tbl12Subphylum single = _uow.Tbl12Subphylums.GetById(id);
            //    Tbl12Subphylum single = _context.Tbl12Subphylums.FirstOrDefault(a => a.SubphylumId == id);
            return single;
        }
        public ObservableCollection<Tbl12Subphylum> GetLastSubphylumsDatasetOrderById()
        {
            var collection = _context.Tbl12Subphylums
                .OrderBy(c => c.SubphylumId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl12Subphylum> { collection };
        }
        //Function
        public int GetSubphylumIdFromSuperclassesCollectionSelect(int id)
        {
            var coll = _context.Tbl18Superclasses
                .SingleOrDefault(p => p.SuperclassId == id);

            if (coll == null) return 0;
            return coll.SubphylumId;
        }
        public ObservableCollection<Tbl12Subphylum> GetConnectedDatasetsWithPhylumIdInTableSubphylum(int selectedId)
        {
            var collection = new ObservableCollection<Tbl12Subphylum>(_uow.Tbl12Subphylums.Find(x => x.PhylumId == selectedId));
            return collection;
        }
        #endregion

        #region Copy Subphylum
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
        #endregion

        #region Delete Subphylum
        public void DeleteSubphylum(Tbl12Subphylum selected)
        {
            _uow.Tbl12Subphylums.Remove(selected);
            _uow.Complete();
        }
        #endregion

        #region Save Subphylum 
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

            if (selected.SubphylumId != 0)  //update
                _uow.Tbl12Subphylums.Update(home);
            else                                //add
                _uow.Tbl12Subphylums.Add(home);
            _uow.Complete();
        }
        #endregion

        #endregion

        #region Subdivision

        #region Get Subdivision
        public ObservableCollection<T> GetSubdivisionsCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
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
        public ObservableCollection<T> GetSubdivisionsCollectionFromDivisionIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl15Subdivisions
                .Where(e => e.DivisionId == id)
                .OrderBy(k => k.SubdivisionName));
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
        public ObservableCollection<Tbl15Subdivision> GetConnectedDatasetsWithDivisionIdInTableSubdivision(int selectedId)
        {
            var collection = new ObservableCollection<Tbl15Subdivision>(_uow.Tbl15Subdivisions.Find(x => x.DivisionId == selectedId));
            return collection;
        }
        public Tbl15Subdivision GetSubdivisionSingleBySubdivisionId<T>(int id)
        {
            Tbl15Subdivision single = _uow.Tbl15Subdivisions.GetById(id);
            //    Tbl15Subdivision single = _context.Tbl15Subdivisions.FirstOrDefault(a => a.SubdivisionId == id);
            return single;
        }
        public ObservableCollection<Tbl15Subdivision> GetLastSubdivisionsDatasetOrderById()
        {
            var collection = _context.Tbl15Subdivisions
                .OrderBy(c => c.SubdivisionId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl15Subdivision> { collection };
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


        #endregion

        #region Delete Subdivision

        public void DeleteSubdivision(Tbl15Subdivision selected)
        {
            _uow.Tbl15Subdivisions.Remove(selected);
            _uow.Complete();
        }


        #endregion

        #region Save Subdivision 
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
        #endregion

        #endregion

        #region Superclass

        #region Get Superclass

        public ObservableCollection<T> GetSuperclassesCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
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
        public ObservableCollection<T> GetSuperclassesCollectionFromSubphylumIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl18Superclasses
                .Where(e => e.SubphylumId == id)
                .OrderBy(k => k.SuperclassName));
            return collection;
        }
        public ObservableCollection<T> GetSuperclassesCollectionFromSubdivisionIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl18Superclasses
                .Where(e => e.SubdivisionId == id)
                .OrderBy(k => k.SuperclassName));
            return collection;
        }
        public ObservableCollection<Tbl18Superclass> GetLastSuperclassesDatasetOrderById()
        {
            var collection = _context.Tbl18Superclasses
                .OrderBy(c => c.SuperclassId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl18Superclass> { collection };
        }

        public ObservableCollection<Tbl18Superclass> GetConnectedDatasetsWithSubphylumIdInTableSuperclass(int selectedId)
        {
            var collection = new ObservableCollection<Tbl18Superclass>(_uow.Tbl18Superclasses.Find(x => x.SubphylumId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl18Superclass> GetConnectedDatasetsWithSubdivisionIdInTableSuperclass(int selectedId)
        {
            var collection = new ObservableCollection<Tbl18Superclass>(_uow.Tbl18Superclasses.Find(x => x.SubdivisionId == selectedId));
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



        //public ObservableCollection<Tbl90Reference> CopyReferenceSuperclass(Tbl90Reference selected, string refer)
        //{
        //    var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
        //    var collection = new ObservableCollection<Tbl90Reference>();
        //    switch (refer)
        //    {
        //        case "Expert":
        //            collection.Insert(0, new Tbl90Reference()
        //            {
        //                SuperclassId = dataset.SuperclassId,
        //                RefExpertId = dataset.RefExpertId,
        //                Valid = dataset.Valid,
        //                ValidYear = dataset.ValidYear,
        //                Info = CultRes.StringsRes.DatasetNew,
        //                Memo = dataset.Memo
        //            });
        //            break;
        //        case "Source":
        //            collection.Insert(0, new Tbl90Reference()
        //            {
        //                SuperclassId = dataset.SuperclassId,
        //                RefSourceId = dataset.RefSourceId,
        //                Valid = dataset.Valid,
        //                ValidYear = dataset.ValidYear,
        //                Info = CultRes.StringsRes.DatasetNew,
        //                Memo = dataset.Memo
        //            });
        //            break;
        //        case "Author":
        //            collection.Insert(0, new Tbl90Reference()
        //            {
        //                SuperclassId = dataset.SuperclassId,
        //                RefAuthorId = dataset.RefAuthorId,
        //                Valid = dataset.Valid,
        //                ValidYear = dataset.ValidYear,
        //                Info = CultRes.StringsRes.DatasetNew,
        //                Memo = dataset.Memo
        //            });
        //            break;
        //    }

        //    return collection;
        //}


        #endregion

        #region Delete Superclass
        public void DeleteSuperclass(Tbl18Superclass selected)
        {
            _uow.Tbl18Superclasses.Remove(selected);
            _uow.Complete();
        }

        #endregion

        #region Save Superclass 
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
        #endregion

        #endregion

        #region Class

        #region Get Class

        public ObservableCollection<T> GetClassesCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            var collection = int.TryParse(searchName, out var id)
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
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl21Classes
                .OrderBy(a => a.ClassName));
            return collection;
        }
        public ObservableCollection<T> GetClassesCollectionFromSuperclassIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl21Classes
                .Where(e => e.SuperclassId == id)
                .OrderBy(k => k.ClassName));
            return collection;
        }
        public ObservableCollection<T> GetClassesCollectionFromClassIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl21Classes
                .Where(e => e.ClassId == id)
                .OrderBy(k => k.ClassName));
            return collection;
        }
        public Tbl21Class GetClassSingleByClassId<T>(int id)
        {
            Tbl21Class single = _uow.Tbl21Classes.GetById(id);
            //    Tbl21Class single = _context.Tbl21Classes.FirstOrDefault(a => a.ClassId == id);
            return single;
        }
        public ObservableCollection<Tbl21Class> GetLastClassesDatasetOrderById()
        {
            var collection = _context.Tbl21Classes
                .OrderBy(c => c.ClassId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl21Class> { collection };
        }
        //Function
        public int ClassIdFromSubclassesCollectionSelect(int id)
        {
            var coll = _context.Tbl24Subclasses
                .SingleOrDefault(p => p.SubclassId == id);

            if (coll == null) return 0;
            return coll.ClassId;
        }
        public ObservableCollection<Tbl21Class> GetConnectedDatasetsWithSuperclassIdInTableClass(int selectedId)
        {
            var collection = new ObservableCollection<Tbl21Class>(_uow.Tbl21Classes.Find(x => x.SuperclassId == selectedId));
            return collection;
        }
        public int GetClassIdFromSubclassesCollectionSelect(int id)
        {
            var coll = _context.Tbl24Subclasses
                .SingleOrDefault(p => p.SubclassId == id);

            if (coll == null) return 0;
            return coll.ClassId;
        }

        #endregion

        #region Copy Class

        //public ObservableCollection<Tbl90Reference> CopyReferenceClass(Tbl90Reference selected, string refer)
        //{
        //    var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
        //    var collection = new ObservableCollection<Tbl90Reference>();
        //    switch (refer)
        //    {
        //        case "Expert":
        //            collection.Insert(0, new Tbl90Reference()
        //            {
        //                ClassId = dataset.ClassId,
        //                RefExpertId = dataset.RefExpertId,
        //                Valid = dataset.Valid,
        //                ValidYear = dataset.ValidYear,
        //                Info = CultRes.StringsRes.DatasetNew,
        //                Memo = dataset.Memo
        //            });
        //            break;
        //        case "Source":
        //            collection.Insert(0, new Tbl90Reference()
        //            {
        //                ClassId = dataset.ClassId,
        //                RefSourceId = dataset.RefSourceId,
        //                Valid = dataset.Valid,
        //                ValidYear = dataset.ValidYear,
        //                Info = CultRes.StringsRes.DatasetNew,
        //                Memo = dataset.Memo
        //            });
        //            break;
        //        case "Author":
        //            collection.Insert(0, new Tbl90Reference()
        //            {
        //                ClassId = dataset.ClassId,
        //                RefAuthorId = dataset.RefAuthorId,
        //                Valid = dataset.Valid,
        //                ValidYear = dataset.ValidYear,
        //                Info = CultRes.StringsRes.DatasetNew,
        //                Memo = dataset.Memo
        //            });
        //            break;
        //    }

        //    return collection;
        //}
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

        #endregion

        #region Delete Class
        public void DeleteClass(Tbl21Class selected)
        {
            _uow.Tbl21Classes.Remove(selected);
            _uow.Complete();
        }

        #endregion

        #region Save Class

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
                UpdaterDate = DateTime.Now,
            };
            return home;
        }
        public void ClassSave(Tbl21Class home, Tbl21Class selected)
        {
            //_uow.BeginTransaction();
            if (selected.ClassId != 0) //update
                _uow.Tbl21Classes.Update(home);
            else                        //add
                _uow.Tbl21Classes.Add(home);

            _uow.Complete();
            //_uow.Commit();
        }

        #endregion

        #endregion

        #region Subclass

        #region Get Subclass
        public ObservableCollection<T> GetSubclassesCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            var collection = int.TryParse(searchName, out var id)
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
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl24Subclasses
                .OrderBy(a => a.SubclassName));
            return collection;
        }
        public ObservableCollection<T> GetSubclassesCollectionFromClassIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl24Subclasses
                .Where(e => e.ClassId == id)
                .OrderBy(k => k.SubclassName));
            return collection;
        }
        public ObservableCollection<T> GetSubclassesCollectionFromSubclassIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl24Subclasses
                .Where(e => e.SubclassId == id)
                .OrderBy(k => k.SubclassName));
            return collection;
        }
        public Tbl24Subclass GetSubclassSingleBySubclassId<T>(int id)
        {
            Tbl24Subclass single = _uow.Tbl24Subclasses.GetById(id);
            //    Tbl24Subclass single = _context.Tbl24Subclasses.FirstOrDefault(a => a.SubclassId == id);
            return single;
        }
        public ObservableCollection<Tbl24Subclass> GetLastSubclassesDatasetOrderById()
        {
            var collection = _context.Tbl24Subclasses
                .OrderBy(c => c.SubclassId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl24Subclass> { collection };
        }
        //Function
        public int SubclassIdFromInfraclassesCollectionSelect(int id)
        {
            var coll = _context.Tbl27Infraclasses
                .SingleOrDefault(p => p.InfraclassId == id);

            if (coll == null) return 0;
            return coll.SubclassId;
        }
        public ObservableCollection<Tbl24Subclass> GetConnectedDatasetsWithClassIdInTableSubclass(int selectedId)
        {
            var collection = new ObservableCollection<Tbl24Subclass>(_uow.Tbl24Subclasses.Find(x => x.ClassId == selectedId));
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
        #endregion

        #region Delete Subclass
        public void DeleteSubclass(Tbl24Subclass selected)
        {
            _uow.Tbl24Subclasses.Remove(selected);
            //   _context.Tbl24Subclasses.Remove(selected);
            _uow.Complete();
            //   _context.SaveChanges();
        }

        #endregion

        #region Save Subclass
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
                UpdaterDate = DateTime.Now,
            };
            return home;
        }
        public void SubclassSave(Tbl24Subclass home, Tbl24Subclass selected)
        {
            //_uow.BeginTransaction();
            if (selected.SubclassId != 0) //update
                _uow.Tbl24Subclasses.Update(home);
            else                        //add
                _uow.Tbl24Subclasses.Add(home);

            _uow.Complete();
            //_uow.Commit();
        }
        #endregion

        #endregion

        #region Infraclass

        #region Get Infraclass

        public ObservableCollection<T> GetInfraclassesCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            var collection = int.TryParse(searchName, out var id)
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
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl27Infraclasses
                .OrderBy(a => a.InfraclassName));
            return collection;
        }
        public ObservableCollection<T> GetInfraclassesCollectionFromSubclassIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl27Infraclasses
                .Where(e => e.SubclassId == id)
                .OrderBy(k => k.InfraclassName));
            return collection;
        }
        public ObservableCollection<T> GetInfraclassesCollectionFromInfraclassIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl27Infraclasses
                .Where(e => e.InfraclassId == id)
                .OrderBy(k => k.InfraclassName));
            return collection;
        }
        public Tbl27Infraclass GetInfraclassSingleByInfraclassId<T>(int id)
        {
            Tbl27Infraclass single = _uow.Tbl27Infraclasses.GetById(id);
            //    Tbl27Infraclass single = _context.Tbl27Infraclasses.FirstOrDefault(a => a.InfraclassId == id);
            return single;
        }
        public ObservableCollection<Tbl27Infraclass> GetLastInfraclassesDatasetOrderById()
        {
            var collection = _context.Tbl27Infraclasses
                .OrderBy(c => c.InfraclassId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl27Infraclass> { collection };
        }
        //Function
        public int InfraclassIdFromLegiosCollectionSelect(int id)
        {
            var coll = _context.Tbl30Legios
                .SingleOrDefault(p => p.LegioId == id);

            if (coll == null) return 0;
            return coll.InfraclassId;
        }
        public ObservableCollection<Tbl27Infraclass> GetConnectedDatasetsWithSubclassIdInTableInfraclass(int selectedId)
        {
            var collection = new ObservableCollection<Tbl27Infraclass>(_uow.Tbl27Infraclasses.Find(x => x.SubclassId == selectedId));
            return collection;
        }
        #endregion

        #region Copy Infraclass
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
        #endregion

        #region Delete Infraclass

        public void DeleteInfraclass(Tbl27Infraclass selected)
        {
            //   _uow.Tbl27Infraclasses.Remove(selected);
            _context.Tbl27Infraclasses.Remove(selected);
            //  _uow.Complete();
            _context.SaveChanges();
        }
        #endregion

        #region Save Infraclass
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
                UpdaterDate = DateTime.Now,
            };
            return home;
        }
        public void InfraclassSave(Tbl27Infraclass home, Tbl27Infraclass selected)
        {
            //_uow.BeginTransaction();
            if (selected.InfraclassId != 0) //update
                _uow.Tbl27Infraclasses.Update(home);
            else                        //add
                _uow.Tbl27Infraclasses.Add(home);

            _uow.Complete();
            //_uow.Commit();
        }
        #endregion

        #endregion

        #region Legio

        #region Get Legio

        public ObservableCollection<T> GetLegiosCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            var collection = int.TryParse(searchName, out var id)
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
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl30Legios
                .OrderBy(a => a.LegioName));
            return collection;
        }
        public ObservableCollection<T> GetLegiosCollectionFromInfraclassIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl30Legios
                .Where(e => e.InfraclassId == id)
                .OrderBy(k => k.LegioName));
            return collection;
        }
        public ObservableCollection<T> GetLegiosCollectionFromLegioIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl30Legios
                .Where(e => e.LegioId == id)
                .OrderBy(k => k.LegioName));
            return collection;
        }
        public Tbl30Legio GetLegioSingleByLegioId<T>(int id)
        {
            Tbl30Legio single = _uow.Tbl30Legios.GetById(id);
            //    Tbl30Legio single = _context.Tbl30Legios.FirstOrDefault(a => a.LegioId == id);
            return single;
        }
        public ObservableCollection<Tbl30Legio> GetLastLegiosDatasetOrderById()
        {
            var collection = _context.Tbl30Legios
                .OrderBy(c => c.LegioId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl30Legio> { collection };
        }
        //Function
        public int LegioIdFromOrdosCollectionSelect(int id)
        {
            var coll = _context.Tbl33Ordos
                .SingleOrDefault(p => p.OrdoId == id);

            if (coll == null) return 0;
            return coll.LegioId;
        }
        public ObservableCollection<Tbl30Legio> GetConnectedDatasetsWithInfraclassIdInTableLegio(int selectedId)
        {
            var collection = new ObservableCollection<Tbl30Legio>(_uow.Tbl30Legios.Find(x => x.InfraclassId == selectedId));
            return collection;
        }
        #endregion

        #region Copy Legio
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
        #endregion

        #region Delete Legio

        public void DeleteLegio(Tbl30Legio selected)
        {
            _uow.Tbl30Legios.Remove(selected);
            // _context.Tbl30Legios.Remove(selected);
            _uow.Complete();
            //  _context.SaveChanges();
        }
        #endregion

        #region Save Legio
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
                UpdaterDate = DateTime.Now,
            };
            return home;
        }
        public void LegioSave(Tbl30Legio home, Tbl30Legio selected)
        {
            //_uow.BeginTransaction();
            if (selected.LegioId != 0) //update
                _uow.Tbl30Legios.Update(home);
            else                        //add
                _uow.Tbl30Legios.Add(home);

            _uow.Complete();
            //_uow.Commit();
        }

        #endregion

        #endregion

        #region Ordo

        #region Get Ordo

        public ObservableCollection<T> GetOrdosCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            var collection = int.TryParse(searchName, out var id)
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
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl33Ordos
                .OrderBy(a => a.OrdoName));
            return collection;
        }
        public ObservableCollection<T> GetOrdosCollectionFromLegioIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl33Ordos
                .Where(e => e.LegioId == id)
                .OrderBy(k => k.OrdoName));
            return collection;
        }

        public ObservableCollection<T> GetOrdosCollectionFromOrdoIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl33Ordos
                .Where(e => e.OrdoId == id)
                .OrderBy(k => k.OrdoName));
            return collection;
        }

        public Tbl33Ordo GetOrdoSingleByOrdoId<T>(int id)
        {
            Tbl33Ordo single = _uow.Tbl33Ordos.GetById(id);
            //    Tbl33Ordo single = _context.Tbl33Ordos.FirstOrDefault(a => a.OrdoId == id);
            return single;
        }

        public ObservableCollection<Tbl33Ordo> GetLastOrdosDatasetOrderById()
        {
            var collection = _context.Tbl33Ordos
                .OrderBy(c => c.OrdoId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl33Ordo> { collection };
        }
        //Function
        public int OrdoIdFromSubordosCollectionSelect(int id)
        {
            var coll = _context.Tbl36Subordos
                .SingleOrDefault(p => p.SubordoId == id);

            if (coll == null) return 0;
            return coll.OrdoId;
        }

        public ObservableCollection<Tbl33Ordo> GetConnectedDatasetsWithLegioIdInTableOrdo(int selectedId)
        {
            var collection = new ObservableCollection<Tbl33Ordo>(_uow.Tbl33Ordos.Find(x => x.LegioId == selectedId));
            return collection;
        }
        #endregion

        #region Copy Ordo
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
        #endregion

        #region Delete Ordo

        public void DeleteOrdo(Tbl33Ordo selected)
        {
            //   _uow.Tbl33Ordos.Remove(selected);
            _context.Tbl33Ordos.Remove(selected);
            //  _uow.Complete();
            _context.SaveChanges();
        }
        #endregion

        #region Save Ordo
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
                UpdaterDate = DateTime.Now,
            };
            return home;
        }
        public void OrdoSave(Tbl33Ordo home, Tbl33Ordo selected)
        {
            //_uow.BeginTransaction();
            if (selected.OrdoId != 0) //update
                _uow.Tbl33Ordos.Update(home);
            else                        //add
                _uow.Tbl33Ordos.Add(home);

            _uow.Complete();
            //_uow.Commit();
        }


        #endregion

        #endregion

        #region Subordo

        #region Get Subordo
        public ObservableCollection<T> GetSubordosCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            var collection = int.TryParse(searchName, out var id)
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
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl36Subordos
                .OrderBy(a => a.SubordoName));
            return collection;
        }
        public ObservableCollection<T> GetSubordosCollectionFromOrdoIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl36Subordos
                .Where(e => e.OrdoId == id)
                .OrderBy(k => k.SubordoName));
            return collection;
        }
        public ObservableCollection<T> GetSubordosCollectionFromSubordoIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl36Subordos
                .Where(e => e.SubordoId == id)
                .OrderBy(k => k.SubordoName));
            return collection;
        }
        public Tbl36Subordo GetSubordoSingleBySubordoId<T>(int id)
        {
            Tbl36Subordo single = _uow.Tbl36Subordos.GetById(id);
            //    Tbl36Subordo single = _context.Tbl36Subordos.FirstOrDefault(a => a.SubordoId == id);
            return single;
        }
        public ObservableCollection<Tbl36Subordo> GetLastSubordosDatasetOrderById()
        {
            var collection = _context.Tbl36Subordos
                .OrderBy(c => c.SubordoId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl36Subordo> { collection };
        }
        //Function
        public int SubordoIdFromInfraordosCollectionSelect(int id)
        {
            var coll = _context.Tbl39Infraordos
                .SingleOrDefault(p => p.InfraordoId == id);

            if (coll == null) return 0;
            return coll.SubordoId;
        }
        public ObservableCollection<Tbl36Subordo> GetConnectedDatasetsWithOrdoIdInTableSubordo(int selectedId)
        {
            var collection = new ObservableCollection<Tbl36Subordo>(_uow.Tbl36Subordos.Find(x => x.OrdoId == selectedId));
            return collection;
        }
        #endregion

        #region Copy Subordo
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
        #endregion

        #region Delete Subordo
        public void DeleteSubordo(Tbl36Subordo selected)
        {
            _uow.Tbl36Subordos.Remove(selected);
            // _context.Tbl36Subordos.Remove(selected);
            _uow.Complete();
            // _context.SaveChanges();
        }
        #endregion

        #region Save Subordo
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
                UpdaterDate = DateTime.Now,
            };
            return home;
        }
        public void SubordoSave(Tbl36Subordo home, Tbl36Subordo selected)
        {
            //_uow.BeginTransaction();
            if (selected.SubordoId != 0) //update
                _uow.Tbl36Subordos.Update(home);
            else                        //add
                _uow.Tbl36Subordos.Add(home);

            _uow.Complete();
            //_uow.Commit();
        }
        #endregion

        #endregion

        #region Infraordo

        #region Get Infraordo
        public ObservableCollection<T> GetInfraordosCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            var collection = int.TryParse(searchName, out var id)
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
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl39Infraordos
                .OrderBy(a => a.InfraordoName));
            return collection;
        }
        public ObservableCollection<T> GetInfraordosCollectionFromSubordoIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl39Infraordos
                .Where(e => e.SubordoId == id)
                .OrderBy(k => k.InfraordoName));
            return collection;
        }
        public ObservableCollection<T> GetInfraordosCollectionFromInfraordoIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl39Infraordos
                .Where(e => e.InfraordoId == id)
                .OrderBy(k => k.InfraordoName));
            return collection;
        }
        public Tbl39Infraordo GetInfraordoSingleByInfraordoId<T>(int id)
        {
            Tbl39Infraordo single = _uow.Tbl39Infraordos.GetById(id);
            //    Tbl39Infraordo single = _context.Tbl39Infraordos.FirstOrDefault(a => a.InfraordoId == id);
            return single;
        }
        public ObservableCollection<Tbl39Infraordo> GetLastInfraordosDatasetOrderById()
        {
            var collection = _context.Tbl39Infraordos
                .OrderBy(c => c.InfraordoId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl39Infraordo> { collection };
        }
        //Function
        public int InfraordoIdFromSuperfamiliesCollectionSelect(int id)
        {
            var coll = _context.Tbl42Superfamilies
                .SingleOrDefault(p => p.SuperfamilyId == id);

            if (coll == null) return 0;
            return coll.InfraordoId;
        }
        public ObservableCollection<Tbl39Infraordo> GetConnectedDatasetsWithSubordoIdInTableInfraordo(int selectedId)
        {
            var collection = new ObservableCollection<Tbl39Infraordo>(_uow.Tbl39Infraordos.Find(x => x.SubordoId == selectedId));
            return collection;
        }
        #endregion

        #region Copy Infraordo
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
        #endregion

        #region Delete Infraordo
        public void DeleteInfraordo(Tbl39Infraordo selected)
        {
            _uow.Tbl39Infraordos.Remove(selected);
            // _context.Tbl39Infraordos.Remove(selected);
            _uow.Complete();
            //  _context.SaveChanges();
        }
        #endregion

        #region Save Infraordo
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
                UpdaterDate = DateTime.Now,
            };
            return home;
        }
        public void InfraordoSave(Tbl39Infraordo home, Tbl39Infraordo selected)
        {
            //_uow.BeginTransaction();
            if (selected.InfraordoId != 0) //update
                _uow.Tbl39Infraordos.Update(home);
            else                        //add
                _uow.Tbl39Infraordos.Add(home);

            _uow.Complete();
            //_uow.Commit();
        }
        #endregion

        #endregion

        #region Superfamily

        #region Get Superfamily
        public ObservableCollection<T> GetSuperfamiliesCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl42Superfamilies
                    .Find(e => e.SuperfamilyId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl42Superfamilies
                    .Find(e => e.SuperfamilyName.StartsWith(searchName))
                    .OrderBy(a => a.SuperfamilyName)
                );
            return collection;
        }
        private ObservableCollection<T> GetSuperfamiliesCollectionAllOrderBy<T>()
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl42Superfamilies
                .OrderBy(a => a.SuperfamilyName));
            return collection;
        }
        public ObservableCollection<T> GetSuperfamiliesCollectionFromInfraordoIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl42Superfamilies
                .Where(e => e.InfraordoId == id)
                .OrderBy(k => k.SuperfamilyName));
            return collection;
        }
        public ObservableCollection<T> GetSuperfamiliesCollectionFromSuperfamilyIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl42Superfamilies
                .Where(e => e.SuperfamilyId == id)
                .OrderBy(k => k.SuperfamilyName));
            return collection;
        }
        public Tbl42Superfamily GetSuperfamilySingleBySuperfamilyId<T>(int id)
        {
            Tbl42Superfamily single = _uow.Tbl42Superfamilies.GetById(id);
            //    Tbl42Superfamily single = _context.Tbl42Superfamilies.FirstOrDefault(a => a.SuperfamilyId == id);
            return single;
        }
        public ObservableCollection<Tbl42Superfamily> GetLastSuperfamiliesDatasetOrderById()
        {
            var collection = _context.Tbl42Superfamilies
                .OrderBy(c => c.SuperfamilyId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl42Superfamily> { collection };
        }
        //Function
        public int SuperfamilyIdFromFamiliesCollectionSelect(int id)
        {
            var coll = _context.Tbl45Families
                .SingleOrDefault(p => p.FamilyId == id);

            if (coll == null) return 0;
            return coll.SuperfamilyId;
        }
        public ObservableCollection<Tbl42Superfamily> GetConnectedDatasetsWithInfraordoIdInTableSuperfamily(int selectedId)
        {
            var collection = new ObservableCollection<Tbl42Superfamily>(_uow.Tbl42Superfamilies.Find(x => x.InfraordoId == selectedId));
            return collection;
        }
        #endregion

        #region Copy Superfamily
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
        #endregion

        #region Delete Superfamily
        public void DeleteSuperfamily(Tbl42Superfamily selected)
        {
            _uow.Tbl42Superfamilies.Remove(selected);
            //  _context.Tbl42Superfamilies.Remove(selected);
            _uow.Complete();
            //  _context.SaveChanges();
        }
        #endregion

        #region Save Superfamily
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
                UpdaterDate = DateTime.Now,
            };
            return home;
        }
        public void SuperfamilySave(Tbl42Superfamily home, Tbl42Superfamily selected)
        {
            //_uow.BeginTransaction();
            if (selected.SuperfamilyId != 0) //update
                _uow.Tbl42Superfamilies.Update(home);
            else                        //add
                _uow.Tbl42Superfamilies.Add(home);

            _uow.Complete();
            //_uow.Commit();
        }
        #endregion

        #endregion

        #region Family

        #region Get Family
        public ObservableCollection<T> GetFamiliesCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl45Families
                    .Find(e => e.FamilyId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl45Families
                    .Find(e => e.FamilyName.StartsWith(searchName))
                    .OrderBy(a => a.FamilyName)
                );
            return collection;
        }
        private ObservableCollection<T> GetFamiliesCollectionAllOrderBy<T>()
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl45Families
                .OrderBy(a => a.FamilyName));
            return collection;
        }
        public ObservableCollection<T> GetFamiliesCollectionFromSuperfamilyIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl45Families
                .Where(e => e.SuperfamilyId == id)
                .OrderBy(k => k.FamilyName));
            return collection;
        }
        public ObservableCollection<T> GetFamiliesCollectionFromFamilyIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl45Families
                .Where(e => e.FamilyId == id)
                .OrderBy(k => k.FamilyName));
            return collection;
        }
        public Tbl45Family GetFamilySingleByFamilyId<T>(int id)
        {
            Tbl45Family single = _uow.Tbl45Families.GetById(id);
            //    Tbl45Family single = _context.Tbl45Families.FirstOrDefault(a => a.FamilyId == id);
            return single;
        }
        public ObservableCollection<Tbl45Family> GetLastFamiliesDatasetOrderById()
        {
            var collection = _context.Tbl45Families
                .OrderBy(c => c.FamilyId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl45Family> { collection };
        }
        //Function
        public int FamilyIdFromSubfamiliesCollectionSelect(int id)
        {
            var coll = _context.Tbl48Subfamilies
                .SingleOrDefault(p => p.SubfamilyId == id);

            if (coll == null) return 0;
            return coll.FamilyId;
        }
        public ObservableCollection<Tbl45Family> GetConnectedDatasetsWithSuperfamilyIdInTableFamily(int selectedId)
        {
            var collection = new ObservableCollection<Tbl45Family>(_uow.Tbl45Families.Find(x => x.SuperfamilyId == selectedId));
            return collection;
        }
        #endregion

        #region Copy Family
        public ObservableCollection<Tbl45Family> CopyFamily(Tbl45Family selected)
        {
            var dataset = _uow.Tbl45Families.GetById(selected.FamilyId);
            var collection = new ObservableCollection<Tbl45Family>();

            collection.Insert(0, new Tbl45Family
            {
                FamilyName = CultRes.StringsRes.DatasetNew,
                SuperfamilyId = dataset.SuperfamilyId,
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
        #endregion

        #region Delete Family
        public void DeleteFamily(Tbl45Family selected)
        {
            _uow.Tbl45Families.Remove(selected);
            //  _context.Tbl45Families.Remove(selected);
            _uow.Complete();
            // _context.SaveChanges();
        }
        #endregion

        #region Save Family
        public Tbl45Family FamilyUpdate(Tbl45Family home, Tbl45Family selected)
        {
            if (home != null) //update
            {
                home.FamilyName = selected.FamilyName;
                home.SuperfamilyId = selected.SuperfamilyId;
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
        public Tbl45Family FamilyAdd(Tbl45Family selected)
        {
            var home = new Tbl45Family() //add new
            {
                FamilyName = selected.FamilyName,
                SuperfamilyId = selected.SuperfamilyId,
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
        public void FamilySave(Tbl45Family home, Tbl45Family selected)
        {
            //_uow.BeginTransaction();
            if (selected.FamilyId != 0) //update
                _uow.Tbl45Families.Update(home);
            else                        //add
                _uow.Tbl45Families.Add(home);

            _uow.Complete();
            //_uow.Commit();
        }


        #endregion

        #endregion

        #region Subfamily

        #region Get Subfamily
        public ObservableCollection<T> GetSubfamiliesCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl48Subfamilies
                    .Find(e => e.SubfamilyId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl48Subfamilies
                    .Find(e => e.SubfamilyName.StartsWith(searchName))
                    .OrderBy(a => a.SubfamilyName)
                );
            return collection;
        }
        private ObservableCollection<T> GetSubfamiliesCollectionAllOrderBy<T>()
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl48Subfamilies
                .OrderBy(a => a.SubfamilyName));
            return collection;
        }
        public ObservableCollection<T> GetSubfamiliesCollectionFromFamilyIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl48Subfamilies
                .Where(e => e.FamilyId == id)
                .OrderBy(k => k.SubfamilyName));
            return collection;
        }
        public ObservableCollection<T> GetSubfamiliesCollectionFromSubfamilyIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl48Subfamilies
                .Where(e => e.SubfamilyId == id)
                .OrderBy(k => k.SubfamilyName));
            return collection;
        }
        public Tbl48Subfamily GetSubfamilySingleBySubfamilyId<T>(int id)
        {
            Tbl48Subfamily single = _uow.Tbl48Subfamilies.GetById(id);
            //    Tbl48Subfamily single = _context.Tbl48Subfamilies.FirstOrDefault(a => a.SubfamilyId == id);
            return single;
        }
        public ObservableCollection<Tbl48Subfamily> GetLastSubfamiliesDatasetOrderById()
        {
            var collection = _context.Tbl48Subfamilies
                .OrderBy(c => c.SubfamilyId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl48Subfamily> { collection };
        }
        //Function
        public int SubfamilyIdFromInfrafamiliesCollectionSelect(int id)
        {
            var coll = _context.Tbl51Infrafamilies
                .SingleOrDefault(p => p.InfrafamilyId == id);

            if (coll == null) return 0;
            return coll.SubfamilyId;
        }
        public ObservableCollection<Tbl48Subfamily> GetConnectedDatasetsWithFamilyIdInTableSubfamily(int selectedId)
        {
            var collection = new ObservableCollection<Tbl48Subfamily>(_uow.Tbl48Subfamilies.Find(x => x.FamilyId == selectedId));
            return collection;
        }
        #endregion

        #region Copy Subfamily
        public ObservableCollection<Tbl48Subfamily> CopySubfamily(Tbl48Subfamily selected)
        {
            var dataset = _uow.Tbl48Subfamilies.GetById(selected.SubfamilyId);
            var collection = new ObservableCollection<Tbl48Subfamily>();

            collection.Insert(0, new Tbl48Subfamily
            {
                SubfamilyName = CultRes.StringsRes.DatasetNew,
                FamilyId = dataset.FamilyId,
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
        #endregion

        #region Delete Subfamily
        public void DeleteSubfamily(Tbl48Subfamily selected)
        {
            _uow.Tbl48Subfamilies.Remove(selected);
            // _context.Tbl48Subfamilies.Remove(selected);
            _uow.Complete();
            // _context.SaveChanges();
        }
        #endregion

        #region Save Subfamily
        public Tbl48Subfamily SubfamilyUpdate(Tbl48Subfamily home, Tbl48Subfamily selected)
        {
            if (home != null) //update
            {
                home.SubfamilyName = selected.SubfamilyName;
                home.FamilyId = selected.FamilyId;
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
        public Tbl48Subfamily SubfamilyAdd(Tbl48Subfamily selected)
        {
            var home = new Tbl48Subfamily() //add new
            {
                SubfamilyName = selected.SubfamilyName,
                FamilyId = selected.FamilyId,
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
        public void SubfamilySave(Tbl48Subfamily home, Tbl48Subfamily selected)
        {
            //_uow.BeginTransaction();
            if (selected.SubfamilyId != 0) //update
                _uow.Tbl48Subfamilies.Update(home);
            else                        //add
                _uow.Tbl48Subfamilies.Add(home);

            _uow.Complete();
            //_uow.Commit();
        }
        #endregion

        #endregion

        #region Infrafamily

        #region Get Infrafamily
        public ObservableCollection<T> GetInfrafamiliesCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl51Infrafamilies
                    .Find(e => e.InfrafamilyId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl51Infrafamilies
                    .Find(e => e.InfrafamilyName.StartsWith(searchName))
                    .OrderBy(a => a.InfrafamilyName)
                );
            return collection;
        }
        private ObservableCollection<T> GetInfrafamiliesCollectionAllOrderBy<T>()
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl51Infrafamilies
                .OrderBy(a => a.InfrafamilyName));
            return collection;
        }
        public ObservableCollection<T> GetInfrafamiliesCollectionFromSubfamilyIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl51Infrafamilies
                .Where(e => e.SubfamilyId == id)
                .OrderBy(k => k.InfrafamilyName));
            return collection;
        }
        public ObservableCollection<T> GetInfrafamiliesCollectionFromInfrafamilyIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl51Infrafamilies
                .Where(e => e.InfrafamilyId == id)
                .OrderBy(k => k.InfrafamilyName));
            return collection;
        }
        public Tbl51Infrafamily GetInfrafamilySingleByInfrafamilyId<T>(int id)
        {
            Tbl51Infrafamily single = _uow.Tbl51Infrafamilies.GetById(id);
            //    Tbl51Infrafamily single = _context.Tbl51Infrafamilies.FirstOrDefault(a => a.InfrafamilyId == id);
            return single;
        }
        public ObservableCollection<Tbl51Infrafamily> GetLastInfrafamiliesDatasetOrderById()
        {
            var collection = _context.Tbl51Infrafamilies
                .OrderBy(c => c.InfrafamilyId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl51Infrafamily> { collection };
        }
        //Function
        public int InfrafamilyIdFromSupertribussesCollectionSelect(int id)
        {
            var coll = _context.Tbl54Supertribusses
                .SingleOrDefault(p => p.SupertribusId == id);

            if (coll == null) return 0;
            return coll.InfrafamilyId;
        }
        public ObservableCollection<Tbl51Infrafamily> GetConnectedDatasetsWithSubfamilyIdInTableInfrafamily(int selectedId)
        {
            var collection = new ObservableCollection<Tbl51Infrafamily>(_uow.Tbl51Infrafamilies.Find(x => x.SubfamilyId == selectedId));
            return collection;
        }
        #endregion

        #region Copy Infrafamily
        public ObservableCollection<Tbl51Infrafamily> CopyInfrafamily(Tbl51Infrafamily selected)
        {
            var dataset = _uow.Tbl51Infrafamilies.GetById(selected.InfrafamilyId);
            var collection = new ObservableCollection<Tbl51Infrafamily>();

            collection.Insert(0, new Tbl51Infrafamily
            {
                InfrafamilyName = CultRes.StringsRes.DatasetNew,
                SubfamilyId = dataset.SubfamilyId,
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
        #endregion

        #region Delete Infrafamily

        public void DeleteInfrafamily(Tbl51Infrafamily selected)
        {
            _uow.Tbl51Infrafamilies.Remove(selected);
            // _context.Tbl51Infrafamilies.Remove(selected);
            _uow.Complete();
            //  _context.SaveChanges();
        }
        #endregion

        #region Save Infrafamily
        public Tbl51Infrafamily InfrafamilyUpdate(Tbl51Infrafamily home, Tbl51Infrafamily selected)
        {
            if (home != null) //update
            {
                home.InfrafamilyName = selected.InfrafamilyName;
                home.SubfamilyId = selected.SubfamilyId;
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
        public Tbl51Infrafamily InfrafamilyAdd(Tbl51Infrafamily selected)
        {
            var home = new Tbl51Infrafamily() //add new
            {
                InfrafamilyName = selected.InfrafamilyName,
                SubfamilyId = selected.SubfamilyId,
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
        public void InfrafamilySave(Tbl51Infrafamily home, Tbl51Infrafamily selected)
        {
            //_uow.BeginTransaction();
            if (selected.InfrafamilyId != 0) //update
                _uow.Tbl51Infrafamilies.Update(home);
            else                        //add
                _uow.Tbl51Infrafamilies.Add(home);

            _uow.Complete();
            //_uow.Commit();
        }
        #endregion

        #endregion

        #region Supertribus

        #region Get Supertribus
        public ObservableCollection<T> GetSupertribussesCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl54Supertribusses
                    .Find(e => e.SupertribusId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl54Supertribusses
                    .Find(e => e.SupertribusName.StartsWith(searchName))
                    .OrderBy(a => a.SupertribusName)
                );
            return collection;
        }
        private ObservableCollection<T> GetSupertribussesCollectionAllOrderBy<T>()
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl54Supertribusses
                .OrderBy(a => a.SupertribusName));
            return collection;
        }
        public ObservableCollection<T> GetSupertribussesCollectionFromInfrafamilyIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl54Supertribusses
                .Where(e => e.InfrafamilyId == id)
                .OrderBy(k => k.SupertribusName));
            return collection;
        }
        public ObservableCollection<T> GetSupertribussesCollectionFromSupertribusIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl54Supertribusses
                .Where(e => e.SupertribusId == id)
                .OrderBy(k => k.SupertribusName));
            return collection;
        }
        public Tbl54Supertribus GetSupertribusSingleBySupertribusId<T>(int id)
        {
            Tbl54Supertribus single = _uow.Tbl54Supertribusses.GetById(id);
            //    Tbl54Supertribus single = _context.Tbl54Supertribusses.FirstOrDefault(a => a.SupertribusId == id);
            return single;
        }
        public ObservableCollection<Tbl54Supertribus> GetLastSupertribussesDatasetOrderById()
        {
            var collection = _context.Tbl54Supertribusses
                .OrderBy(c => c.SupertribusId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl54Supertribus> { collection };
        }
        //Function
        public int SupertribusIdFromTribussesCollectionSelect(int id)
        {
            var coll = _context.Tbl57Tribusses
                .SingleOrDefault(p => p.TribusId == id);

            if (coll == null) return 0;
            return coll.SupertribusId;
        }
        public ObservableCollection<Tbl54Supertribus> GetConnectedDatasetsWithInfrafamilyIdInTableSupertribus(int selectedId)
        {
            var collection = new ObservableCollection<Tbl54Supertribus>(_uow.Tbl54Supertribusses.Find(x => x.InfrafamilyId == selectedId));
            return collection;
        }
        #endregion

        #region Copy Supertribus
        public ObservableCollection<Tbl54Supertribus> CopySupertribus(Tbl54Supertribus selected)
        {
            var dataset = _uow.Tbl54Supertribusses.GetById(selected.SupertribusId);
            var collection = new ObservableCollection<Tbl54Supertribus>();

            collection.Insert(0, new Tbl54Supertribus
            {
                SupertribusName = CultRes.StringsRes.DatasetNew,
                InfrafamilyId = dataset.InfrafamilyId,
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
        #endregion

        #region Delete Supertribus
        public void DeleteSupertribus(Tbl54Supertribus selected)
        {
            _uow.Tbl54Supertribusses.Remove(selected);
            // _context.Tbl54Supertribusses.Remove(selected);
            _uow.Complete();
            //  _context.SaveChanges();
        }
        #endregion

        #region Save Supertribus
        public Tbl54Supertribus SupertribusUpdate(Tbl54Supertribus home, Tbl54Supertribus selected)
        {
            if (home != null) //update
            {
                home.SupertribusName = selected.SupertribusName;
                home.InfrafamilyId = selected.InfrafamilyId;
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
        public Tbl54Supertribus SupertribusAdd(Tbl54Supertribus selected)
        {
            var home = new Tbl54Supertribus() //add new
            {
                SupertribusName = selected.SupertribusName,
                InfrafamilyId = selected.InfrafamilyId,
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
        public void SupertribusSave(Tbl54Supertribus home, Tbl54Supertribus selected)
        {
            //_uow.BeginTransaction();
            if (selected.SupertribusId != 0) //update
                _uow.Tbl54Supertribusses.Update(home);
            else                        //add
                _uow.Tbl54Supertribusses.Add(home);

            _uow.Complete();
            //_uow.Commit();
        }
        #endregion

        #endregion

        #region Tribus

        #region Get Tribus
        public ObservableCollection<T> GetTribussesCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl57Tribusses
                    .Find(e => e.TribusId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl57Tribusses
                    .Find(e => e.TribusName.StartsWith(searchName))
                    .OrderBy(a => a.TribusName)
                );
            return collection;
        }
        private ObservableCollection<T> GetTribussesCollectionAllOrderBy<T>()
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl57Tribusses
                .OrderBy(a => a.TribusName));
            return collection;
        }
        public ObservableCollection<T> GetTribussesCollectionFromSupertribusIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl57Tribusses
                .Where(e => e.SupertribusId == id)
                .OrderBy(k => k.TribusName));
            return collection;
        }
        public ObservableCollection<T> GetTribussesCollectionFromTribusIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl57Tribusses
                .Where(e => e.TribusId == id)
                .OrderBy(k => k.TribusName));
            return collection;
        }
        public Tbl57Tribus GetTribusSingleByTribusId<T>(int id)
        {
            Tbl57Tribus single = _uow.Tbl57Tribusses.GetById(id);
            //    Tbl57Tribus single = _context.Tbl57Tribusses.FirstOrDefault(a => a.TribusId == id);
            return single;
        }
        public ObservableCollection<Tbl57Tribus> GetLastTribussesDatasetOrderById()
        {
            var collection = _context.Tbl57Tribusses
                .OrderBy(c => c.TribusId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl57Tribus> { collection };
        }
        //Function
        public int TribusIdFromSubtribussesCollectionSelect(int id)
        {
            var coll = _context.Tbl60Subtribusses
                .SingleOrDefault(p => p.SubtribusId == id);

            if (coll == null) return 0;
            return coll.TribusId;
        }
        public ObservableCollection<Tbl57Tribus> GetConnectedDatasetsWithSupertribusIdInTableTribus(int selectedId)
        {
            var collection = new ObservableCollection<Tbl57Tribus>(_uow.Tbl57Tribusses.Find(x => x.SupertribusId == selectedId));
            return collection;
        }
        #endregion

        #region Copy Tribus
        public ObservableCollection<Tbl57Tribus> CopyTribus(Tbl57Tribus selected)
        {
            var dataset = _uow.Tbl57Tribusses.GetById(selected.TribusId);
            var collection = new ObservableCollection<Tbl57Tribus>();

            collection.Insert(0, new Tbl57Tribus
            {
                TribusName = CultRes.StringsRes.DatasetNew,
                SupertribusId = dataset.SupertribusId,
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
        #endregion

        #region Delete Tribus
        public void DeleteTribus(Tbl57Tribus selected)
        {
            _uow.Tbl57Tribusses.Remove(selected);
            // _context.Tbl57Tribusses.Remove(selected);
            _uow.Complete();
            // _context.SaveChanges();
        }
        #endregion

        #region Save Tribus
        public Tbl57Tribus TribusUpdate(Tbl57Tribus home, Tbl57Tribus selected)
        {
            if (home != null) //update
            {
                home.TribusName = selected.TribusName;
                home.SupertribusId = selected.SupertribusId;
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
        public Tbl57Tribus TribusAdd(Tbl57Tribus selected)
        {
            var home = new Tbl57Tribus() //add new
            {
                TribusName = selected.TribusName,
                SupertribusId = selected.SupertribusId,
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
        public void TribusSave(Tbl57Tribus home, Tbl57Tribus selected)
        {
            //_uow.BeginTransaction();
            if (selected.TribusId != 0) //update
                _uow.Tbl57Tribusses.Update(home);
            else                        //add
                _uow.Tbl57Tribusses.Add(home);

            _uow.Complete();
            //_uow.Commit();
        }
        #endregion

        #endregion

        #region Subtribus

        #region Get Subtribus
        public ObservableCollection<T> GetSubtribussesCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl60Subtribusses
                    .Find(e => e.SubtribusId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl60Subtribusses
                    .Find(e => e.SubtribusName.StartsWith(searchName))
                    .OrderBy(a => a.SubtribusName)
                );
            return collection;
        }
        private ObservableCollection<T> GetSubtribussesCollectionAllOrderBy<T>()
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl60Subtribusses
                .OrderBy(a => a.SubtribusName));
            return collection;
        }
        public ObservableCollection<T> GetSubtribussesCollectionFromTribusIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl60Subtribusses
                .Where(e => e.TribusId == id)
                .OrderBy(k => k.SubtribusName));
            return collection;
        }
        public ObservableCollection<T> GetSubtribussesCollectionFromSubtribusIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl60Subtribusses
                .Where(e => e.SubtribusId == id)
                .OrderBy(k => k.SubtribusName));
            return collection;
        }
        public Tbl60Subtribus GetSubtribusSingleBySubtribusId<T>(int id)
        {
            Tbl60Subtribus single = _uow.Tbl60Subtribusses.GetById(id);
            //    Tbl60Subtribus single = _context.Tbl60Subtribusses.FirstOrDefault(a => a.SubtribusId == id);
            return single;
        }
        public ObservableCollection<Tbl60Subtribus> GetLastSubtribussesDatasetOrderById()
        {
            var collection = _context.Tbl60Subtribusses
                .OrderBy(c => c.SubtribusId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl60Subtribus> { collection };
        }
        //Function
        public int SubtribusIdFromInfratribussesCollectionSelect(int id)
        {
            var coll = _context.Tbl63Infratribusses
                .SingleOrDefault(p => p.InfratribusId == id);

            if (coll == null) return 0;
            return coll.SubtribusId;
        }
        public ObservableCollection<Tbl60Subtribus> GetConnectedDatasetsWithTribusIdInTableSubtribus(int selectedId)
        {
            var collection = new ObservableCollection<Tbl60Subtribus>(_uow.Tbl60Subtribusses.Find(x => x.TribusId == selectedId));
            return collection;
        }
        #endregion

        #region Copy Subtribus
        public ObservableCollection<Tbl60Subtribus> CopySubtribus(Tbl60Subtribus selected)
        {
            var dataset = _uow.Tbl60Subtribusses.GetById(selected.SubtribusId);
            var collection = new ObservableCollection<Tbl60Subtribus>();

            collection.Insert(0, new Tbl60Subtribus
            {
                SubtribusName = CultRes.StringsRes.DatasetNew,
                TribusId = dataset.TribusId,
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
        #endregion

        #region Delete Subtribus
        public void DeleteSubtribus(Tbl60Subtribus selected)
        {
            _uow.Tbl60Subtribusses.Remove(selected);
            // _context.Tbl60Subtribusses.Remove(selected);
            _uow.Complete();
            //  _context.SaveChanges();
        }
        #endregion

        #region Save Subtribus
        public Tbl60Subtribus SubtribusUpdate(Tbl60Subtribus home, Tbl60Subtribus selected)
        {
            if (home != null) //update
            {
                home.SubtribusName = selected.SubtribusName;
                home.TribusId = selected.TribusId;
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
        public Tbl60Subtribus SubtribusAdd(Tbl60Subtribus selected)
        {
            var home = new Tbl60Subtribus() //add new
            {
                SubtribusName = selected.SubtribusName,
                TribusId = selected.TribusId,
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
        public void SubtribusSave(Tbl60Subtribus home, Tbl60Subtribus selected)
        {
            //_uow.BeginTransaction();
            if (selected.SubtribusId != 0) //update
                _uow.Tbl60Subtribusses.Update(home);
            else                        //add
                _uow.Tbl60Subtribusses.Add(home);

            _uow.Complete();
            //_uow.Commit();
        }

        #endregion

        #endregion

        #region Infratribus

        #region Get Infratribus
        public ObservableCollection<T> GetInfratribussesCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl63Infratribusses
                    .Find(e => e.InfratribusId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl63Infratribusses
                    .Find(e => e.InfratribusName.StartsWith(searchName))
                    .OrderBy(a => a.InfratribusName)
                );
            return collection;
        }
        private ObservableCollection<T> GetInfratribussesCollectionAllOrderBy<T>()
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl63Infratribusses
                .OrderBy(a => a.InfratribusName));
            return collection;
        }
        public ObservableCollection<T> GetInfratribussesCollectionFromSubtribusIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl63Infratribusses
                .Where(e => e.SubtribusId == id)
                .OrderBy(k => k.InfratribusName));
            return collection;
        }
        public ObservableCollection<T> GetInfratribussesCollectionFromInfratribusIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl63Infratribusses
                .Where(e => e.InfratribusId == id)
                .OrderBy(k => k.InfratribusName));
            return collection;
        }
        public Tbl63Infratribus GetInfratribusSingleByInfratribusId<T>(int id)
        {
            Tbl63Infratribus single = _uow.Tbl63Infratribusses.GetById(id);
            //    Tbl63Infratribus single = _context.Tbl63Infratribusses.FirstOrDefault(a => a.InfratribusId == id);
            return single;
        }
        public ObservableCollection<Tbl63Infratribus> GetLastInfratribussesDatasetOrderById()
        {
            var collection = _context.Tbl63Infratribusses
                .OrderBy(c => c.InfratribusId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl63Infratribus> { collection };
        }
        //Function
        public int InfratribusIdFromGenussesCollectionSelect(int id)
        {
            var coll = _context.Tbl66Genusses
                .SingleOrDefault(p => p.GenusId == id);

            if (coll == null) return 0;
            return coll.InfratribusId;
        }
        public ObservableCollection<Tbl63Infratribus> GetConnectedDatasetsWithSubtribusIdInTableInfratribus(int selectedId)
        {
            var collection = new ObservableCollection<Tbl63Infratribus>(_uow.Tbl63Infratribusses.Find(x => x.SubtribusId == selectedId));
            return collection;
        }
        #endregion

        #region Copy Infratribus
        public ObservableCollection<Tbl63Infratribus> CopyInfratribus(Tbl63Infratribus selected)
        {
            var dataset = _uow.Tbl63Infratribusses.GetById(selected.InfratribusId);
            var collection = new ObservableCollection<Tbl63Infratribus>();

            collection.Insert(0, new Tbl63Infratribus
            {
                InfratribusName = CultRes.StringsRes.DatasetNew,
                SubtribusId = dataset.SubtribusId,
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
        #endregion

        #region Delete Infratribus
        public void DeleteInfratribus(Tbl63Infratribus selected)
        {
            _uow.Tbl63Infratribusses.Remove(selected);
            // _context.Tbl63Infratribusses.Remove(selected);
            _uow.Complete();
            //  _context.SaveChanges();
        }
        #endregion

        #region Save Infratribus
        public Tbl63Infratribus InfratribusUpdate(Tbl63Infratribus home, Tbl63Infratribus selected)
        {
            if (home != null) //update
            {
                home.InfratribusName = selected.InfratribusName;
                home.SubtribusId = selected.SubtribusId;
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
        public Tbl63Infratribus InfratribusAdd(Tbl63Infratribus selected)
        {
            var home = new Tbl63Infratribus() //add new
            {
                InfratribusName = selected.InfratribusName,
                SubtribusId = selected.SubtribusId,
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
        public void InfratribusSave(Tbl63Infratribus home, Tbl63Infratribus selected)
        {
            //_uow.BeginTransaction();
            if (selected.InfratribusId != 0) //update
                _uow.Tbl63Infratribusses.Update(home);
            else                        //add
                _uow.Tbl63Infratribusses.Add(home);

            _uow.Complete();
            //_uow.Commit();
        }
        #endregion

        #endregion

        #region Genus

        #region Get Genus
        public ObservableCollection<T> GetGenussesCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl66Genusses
                    .Find(e => e.GenusId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl66Genusses
                    .Find(e => e.GenusName.StartsWith(searchName))
                    .OrderBy(a => a.GenusName)
                );
            return collection;
        }
        private ObservableCollection<T> GetGenussesCollectionAllOrderBy<T>()
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl66Genusses
                .OrderBy(a => a.GenusName));
            return collection;
        }
        public ObservableCollection<T> GetGenussesCollectionFromInfratribusIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl66Genusses
                .Where(e => e.InfratribusId == id)
                .OrderBy(k => k.GenusName));
            return collection;
        }
        public ObservableCollection<T> GetGenussesCollectionFromGenusIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl66Genusses
                .Where(e => e.GenusId == id)
                .OrderBy(k => k.GenusName));
            return collection;
        }
        public Tbl66Genus GetGenusSingleByGenusId<T>(int id)
        {
            Tbl66Genus single = _uow.Tbl66Genusses.GetById(id);
            //    Tbl66Genus single = _context.Tbl66Genusses.FirstOrDefault(a => a.GenusId == id);
            return single;
        }
        public ObservableCollection<Tbl66Genus> GetLastGenussesDatasetOrderById()
        {
            var collection = _context.Tbl66Genusses
                .OrderBy(c => c.GenusId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl66Genus> { collection };
        }
        //Function
        public int GenusIdFromFiSpeciessesCollectionSelect(int id)
        {
            var coll = _context.Tbl69FiSpeciesses
                .SingleOrDefault(p => p.FiSpeciesId == id);

            if (coll == null) return 0;
            return coll.GenusId;
        }
        public ObservableCollection<Tbl66Genus> GetConnectedDatasetsWithInfratribusIdInTableGenus(int selectedId)
        {
            var collection = new ObservableCollection<Tbl66Genus>(_uow.Tbl66Genusses.Find(x => x.InfratribusId == selectedId));
            return collection;
        }
        #endregion

        #region Copy Genus
        public ObservableCollection<Tbl66Genus> CopyGenus(Tbl66Genus selected)
        {
            var dataset = _uow.Tbl66Genusses.GetById(selected.GenusId);
            var collection = new ObservableCollection<Tbl66Genus>();

            collection.Insert(0, new Tbl66Genus
            {
                GenusName = CultRes.StringsRes.DatasetNew,
                InfratribusId = dataset.InfratribusId,
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
        #endregion

        #region Delete Genus
        public void DeleteGenus(Tbl66Genus selected)
        {
            _uow.Tbl66Genusses.Remove(selected);
            //  _context.Tbl66Genusses.Remove(selected);
            _uow.Complete();
            //  _context.SaveChanges();
        }
        #endregion

        #region Save Genus
        public Tbl66Genus GenusUpdate(Tbl66Genus home, Tbl66Genus selected)
        {
            if (home != null) //update
            {
                home.GenusName = selected.GenusName;
                home.InfratribusId = selected.InfratribusId;
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
        public Tbl66Genus GenusAdd(Tbl66Genus selected)
        {
            var home = new Tbl66Genus() //add new
            {
                GenusName = selected.GenusName,
                InfratribusId = selected.InfratribusId,
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
        public void GenusSave(Tbl66Genus home, Tbl66Genus selected)
        {
            //_uow.BeginTransaction();
            if (selected.GenusId != 0) //update
                _uow.Tbl66Genusses.Update(home);
            else                        //add
                _uow.Tbl66Genusses.Add(home);

            _uow.Complete();
            //_uow.Commit();
        }
        #endregion

        #endregion

        #region Speciesgroup

        #region Get Speciesgroup

        //----------------------------------------   Speciesgroup   ------------------------
        public ObservableCollection<T> GetSpeciesgroupsCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl68Speciesgroups
                    .Find(e => e.SpeciesgroupId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl68Speciesgroups
                    .Find(e => e.SpeciesgroupName.StartsWith(searchName))
                    .OrderBy(a => a.SpeciesgroupName)
                );
            return collection;
        }

        public ObservableCollection<T> GetSpeciesgroupsCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl68Speciesgroups
                .OrderBy(a => a.SpeciesgroupName)
                .ThenBy(a => a.Subspeciesgroup));
            return collection;
        }
        public ObservableCollection<T> GetSpeciesgroupsCollectionFromSpeciesgroupIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl68Speciesgroups
                .Where(e => e.SpeciesgroupId == id)
                .OrderBy(k => k.SpeciesgroupName));

            return collection;
        }
        public ObservableCollection<Tbl68Speciesgroup> GetLastSpeciesgroupsDatasetOrderById()
        {
            var collection = _context.Tbl68Speciesgroups
                .OrderBy(c => c.SpeciesgroupId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl68Speciesgroup> { collection };
        }

        //-------------------------------------- FiSpecies   -------------------------
        public ObservableCollection<T> GetFiSpeciessesCollectionFromSpeciesgroupIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl69FiSpeciesses
                .Where(e => e.SpeciesgroupId == id)
                .OrderBy(k => k.FiSpeciesName)
                .ThenBy(k => k.Subspecies)
                .ThenBy(k => k.Divers));
            return collection;
        }
        //-------------------------------------- PlSpecies   -------------------------
        public ObservableCollection<T> GetPlSpeciessesCollectionFromSpeciesgroupIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl72PlSpeciesses
                .Where(e => e.SpeciesgroupId == id)
                .OrderBy(k => k.PlSpeciesName)
                .ThenBy(k => k.Subspecies)
                .ThenBy(k => k.Divers));
            return collection;
        }


        #endregion

        #region Copy Speciesgroup

        public ObservableCollection<Tbl68Speciesgroup> CopySpeciesgroup(Tbl68Speciesgroup selected)
        {
            var dataset = _uow.Tbl68Speciesgroups.GetById(selected.SpeciesgroupId);
            var collection = new ObservableCollection<Tbl68Speciesgroup>();

            collection.Insert(0, new Tbl68Speciesgroup
            {
                SpeciesgroupName = CultRes.StringsRes.DatasetNew,
                Subspeciesgroup = dataset.Subspeciesgroup,
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



        #endregion

        #region Delete Speciesgroup

        //------------------------------ Speciesgroup --------------------------------------------------------------------------------------------
        public void DeleteSpeciesgroup(Tbl68Speciesgroup selected)
        {
            _uow.Tbl68Speciesgroups.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl69FiSpecies> SearchForConnectedDatasetsWithSpeciesgroupIdInTableFiSpecies(int selectedId)
        {
            var collection = new ObservableCollection<Tbl69FiSpecies>(_uow.Tbl69FiSpeciesses.Find(x => x.SpeciesgroupId == selectedId));
            return collection;
        }
        //public ObservableCollection<Tbl90Reference> DeleteDatasetsWithSpeciesgroupIdInTableReference(Tbl68Speciesgroup selected)
        //{
        //    var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SpeciesgroupId == selected.SpeciesgroupId));
        //    return collection;
        //}
        //public ObservableCollection<Tbl93Comment> DeleteDatasetsWithSpeciesgroupInTableComment(Tbl68Speciesgroup selected)
        //{
        //    var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SpeciesgroupId == selected.SpeciesgroupId));
        //    return collection;
        //}




        #endregion

        #region Save Speciesgroup 

        public Tbl68Speciesgroup SpeciesgroupUpdate(Tbl68Speciesgroup home, Tbl68Speciesgroup selected)
        {
            if (home != null) //update
            {
                home.SpeciesgroupName = selected.SpeciesgroupName;
                home.Subspeciesgroup = selected.Subspeciesgroup;
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
        public Tbl68Speciesgroup SpeciesgroupAdd(Tbl68Speciesgroup selected)
        {
            var home = new Tbl68Speciesgroup() //add new
            {
                SpeciesgroupName = selected.SpeciesgroupName,
                Subspeciesgroup = selected.Subspeciesgroup,
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
        public void SpeciesgroupSave(Tbl68Speciesgroup home, Tbl68Speciesgroup selected)
        {

            if (selected.SpeciesgroupId != 0) //update
            {
                _uow.Tbl68Speciesgroups.Update(home);
            }
            else                                //add
                _uow.Tbl68Speciesgroups.Add(home);
            _uow.Complete();
        }

        #endregion

        #endregion

        #region FiSpecies

        #region Get FiSpecies
        public ObservableCollection<T> GetFiSpeciessesCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl69FiSpeciesses
                    .Find(e => e.FiSpeciesId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_context.Tbl69FiSpeciesses
                    .Include(d => d.Tbl66Genusses)
                    .Where(e => e.Tbl66Genusses.GenusName.StartsWith(searchName))
                    .OrderBy(a => a.Tbl66Genusses.GenusName)
                    .ThenBy(a => a.FiSpeciesName)
                    .ThenBy(a => a.Subspecies)
                    .ThenBy(a => a.Divers)
                );
            return collection;
        }

        private ObservableCollection<T> GetFiSpeciessesCollectionAllOrderBy<T>()
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl69FiSpeciesses
                .Include(d => d.Tbl66Genusses)
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.FiSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers));
            return collection;
        }
        public ObservableCollection<T> GetFiSpeciessesCollectionFromGenusIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl69FiSpeciesses
                .Where(e => e.GenusId == id)
                .OrderBy(k => k.FiSpeciesName));
            return collection;
        }
        public ObservableCollection<T> GetFiSpeciessesCollectionFromFiSpeciesIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl69FiSpeciesses
                .Where(e => e.FiSpeciesId == id)
                .OrderBy(k => k.FiSpeciesName));
            return collection;
        }
        public Tbl69FiSpecies GetFiSpeciesSingleByFiSpeciesId<T>(int id)
        {
            Tbl69FiSpecies single = _uow.Tbl69FiSpeciesses.GetById(id);
            //    Tbl69FiSpecies single = _context.Tbl69FiSpeciesses.FirstOrDefault(a => a.FiSpeciesId == id);
            return single;
        }

        public Tbl69FiSpecies GetFiSpeciesSingleByFiSpeciesName<T>(string name)
        {
            Tbl69FiSpecies single = _uow.Tbl69FiSpeciesses.FirstOrDefault(p => p.FiSpeciesName == name);
            //  var animaliaRegnum = _uow.Tbl69FiSpeciesses.FirstOrDefault(p => p.FiSpeciesName == "Animalia#Regnum#");
            return single;
        }

        public ObservableCollection<Tbl69FiSpecies> GetLastFiSpeciessesDatasetOrderById()
        {
            var collection = _context.Tbl69FiSpeciesses
                .OrderBy(c => c.FiSpeciesId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl69FiSpecies> { collection };
        }
        //Function
        public int FiSpeciesIdFromNamesCollectionSelect(int id)
        {
            var coll = _context.Tbl78Names
                .SingleOrDefault(p => p.NameId == id);

            if (coll == null) return 0;
            return coll.FiSpeciesId;
        }
        public ObservableCollection<Tbl69FiSpecies> GetConnectedDatasetsWithGenusIdInTableFiSpecies(int selectedId)
        {
            var collection = new ObservableCollection<Tbl69FiSpecies>(_uow.Tbl69FiSpeciesses.Find(x => x.GenusId == selectedId));
            return collection;
        }
        #endregion

        #region Copy FiSpecies
        public ObservableCollection<Tbl69FiSpecies> CopyFiSpecies(Tbl69FiSpecies selected)
        {
            var dataset = _uow.Tbl69FiSpeciesses.GetById(selected.FiSpeciesId);
            var collection = new ObservableCollection<Tbl69FiSpecies>();

            collection.Insert(0, new Tbl69FiSpecies
            {
                FiSpeciesName = CultRes.StringsRes.DatasetNew,
                Subspecies = "",
                Divers = "",
                GenusId = dataset.GenusId,
                SpeciesgroupId = dataset.SpeciesgroupId,
                Valid = dataset.Valid,
                ValidYear = dataset.ValidYear,
                Author = dataset.Author,
                AuthorYear = dataset.AuthorYear,
                MemoSpecies = dataset.MemoSpecies,
                TradeName = dataset.TradeName,
                Importer = dataset.Importer,
                ImportingYear = dataset.ImportingYear,
                TypeSpecies = dataset.TypeSpecies,
                LNumber = dataset.LNumber,
                LOrigin = dataset.LOrigin,
                LdaOrigin = dataset.LdaOrigin,
                LdaNumber = dataset.LdaNumber,
                BasinLength = dataset.BasinLength,
                FishLength = dataset.FishLength,
                Karnivore = dataset.Karnivore,
                Herbivore = dataset.Herbivore,
                Limnivore = dataset.Limnivore,
                Omnivore = dataset.Omnivore,
                MemoFoods = dataset.MemoFoods,
                Difficult1 = dataset.Difficult1,
                Difficult2 = dataset.Difficult2,
                Difficult3 = dataset.Difficult3,
                Difficult4 = dataset.Difficult4,
                RegionTop = dataset.RegionTop,
                RegionMiddle = dataset.RegionMiddle,
                RegionBottom = dataset.RegionBottom,
                MemoRegion = dataset.MemoRegion,
                MemoTech = dataset.MemoTech,
                Ph1 = dataset.Ph1,
                Ph2 = dataset.Ph2,
                Temp1 = dataset.Temp1,
                Temp2 = dataset.Temp2,
                Hardness1 = dataset.Hardness1,
                Hardness2 = dataset.Hardness2,
                CarboHardness1 = dataset.CarboHardness1,
                CarboHardness2 = dataset.CarboHardness2,
                MemoHusbandry = dataset.MemoHusbandry,
                MemoBuilt = dataset.MemoBuilt,
                MemoColor = dataset.MemoColor,
                MemoSozial = dataset.MemoSozial,
                MemoDomorphism = dataset.MemoDomorphism,
                MemoSpecial = dataset.MemoSpecial,
                MemoBreeding = dataset.MemoBreeding
            });

            return collection;
        }
        #endregion

        #region Delete FiSpecies

        public void DeleteFiSpecies(Tbl69FiSpecies selected)
        {
            _uow.Tbl69FiSpeciesses.Remove(selected);
            //  _context.Tbl69FiSpeciesses.Remove(selected);
            _uow.Complete();
            //   _context.SaveChanges();
        }
        #endregion

        #region Save FiSpecies

        public Tbl69FiSpecies FiSpeciesUpdate(Tbl69FiSpecies home, Tbl69FiSpecies selected)
        {
            if (home != null) //update
            {
                home.FiSpeciesName = selected.FiSpeciesName;
                home.Subspecies = selected.Subspecies;
                home.Divers = selected.Divers;
                home.GenusId = selected.GenusId;
                home.SpeciesgroupId = selected.SpeciesgroupId;
                home.Valid = selected.Valid;
                home.ValidYear = selected.ValidYear;
                home.MemoSpecies = selected.MemoSpecies;
                home.TradeName = selected.TradeName;
                home.Author = selected.Author;
                home.AuthorYear = selected.AuthorYear;
                home.Importer = selected.Importer;
                home.ImportingYear = selected.ImportingYear;
                home.TypeSpecies = selected.TypeSpecies;
                home.LNumber = selected.LNumber;
                home.LOrigin = selected.LOrigin;
                home.LdaOrigin = selected.LdaOrigin;
                home.LdaNumber = selected.LdaNumber;
                home.BasinLength = selected.BasinLength;
                home.FishLength = selected.FishLength;
                home.Karnivore = selected.Karnivore;
                home.Herbivore = selected.Herbivore;
                home.Limnivore = selected.Limnivore;
                home.Omnivore = selected.Omnivore;
                home.MemoFoods = selected.MemoFoods;
                home.Difficult1 = selected.Difficult1;
                home.Difficult2 = selected.Difficult2;
                home.Difficult3 = selected.Difficult3;
                home.Difficult4 = selected.Difficult4;
                home.RegionTop = selected.RegionTop;
                home.RegionMiddle = selected.RegionMiddle;
                home.RegionBottom = selected.RegionBottom;
                home.MemoRegion = selected.MemoRegion;
                home.MemoTech = selected.MemoTech;
                home.Ph1 = selected.Ph1;
                home.Ph2 = selected.Ph2;
                home.Temp1 = selected.Temp1;
                home.Temp2 = selected.Temp2;
                home.Hardness1 = selected.Hardness1;
                home.Hardness2 = selected.Hardness2;
                home.CarboHardness1 = selected.CarboHardness1;
                home.CarboHardness2 = selected.CarboHardness2;
                home.MemoHusbandry = selected.MemoHusbandry;
                home.MemoBuilt = selected.MemoBuilt;
                home.MemoColor = selected.MemoColor;
                home.MemoSozial = selected.MemoSozial;
                home.MemoDomorphism = selected.MemoDomorphism;
                home.MemoSpecial = selected.MemoSpecial;
                home.MemoBreeding = selected.MemoBreeding;
                home.Updater = Environment.UserName;
                home.UpdaterDate = DateTime.Now;
            }
            return home;
        }
        public Tbl69FiSpecies FiSpeciesAdd(Tbl69FiSpecies selected)
        {
            var home = new Tbl69FiSpecies() //add new
            {
                FiSpeciesName = selected.FiSpeciesName,
                Subspecies = selected.Subspecies,
                Divers = selected.Divers,
                GenusId = selected.GenusId,
                SpeciesgroupId = selected.SpeciesgroupId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                MemoSpecies = selected.MemoSpecies,
                TradeName = selected.TradeName,
                Author = selected.Author,
                AuthorYear = selected.AuthorYear,
                Importer = selected.Importer,
                ImportingYear = selected.ImportingYear,
                TypeSpecies = selected.TypeSpecies,
                LNumber = selected.LNumber,
                LOrigin = selected.LOrigin,
                LdaOrigin = selected.LdaOrigin,
                LdaNumber = selected.LdaNumber,
                BasinLength = selected.BasinLength,
                FishLength = selected.FishLength,
                Karnivore = selected.Karnivore,
                Herbivore = selected.Herbivore,
                Limnivore = selected.Limnivore,
                Omnivore = selected.Omnivore,
                MemoFoods = selected.MemoFoods,
                Difficult1 = selected.Difficult1,
                Difficult2 = selected.Difficult2,
                Difficult3 = selected.Difficult3,
                Difficult4 = selected.Difficult4,
                RegionTop = selected.RegionTop,
                RegionMiddle = selected.RegionMiddle,
                RegionBottom = selected.RegionBottom,
                MemoRegion = selected.MemoRegion,
                MemoTech = selected.MemoTech,
                Ph1 = selected.Ph1,
                Ph2 = selected.Ph2,
                Temp1 = selected.Temp1,
                Temp2 = selected.Temp2,
                Hardness1 = selected.Hardness1,
                Hardness2 = selected.Hardness2,
                CarboHardness1 = selected.CarboHardness1,
                CarboHardness2 = selected.CarboHardness2,
                MemoHusbandry = selected.MemoHusbandry,
                MemoBuilt = selected.MemoBuilt,
                MemoColor = selected.MemoColor,
                MemoSozial = selected.MemoSozial,
                MemoDomorphism = selected.MemoDomorphism,
                MemoSpecial = selected.MemoSpecial,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
                MemoBreeding = selected.MemoBreeding,
            };
            return home;
        }
        public void FiSpeciesSave(Tbl69FiSpecies home, Tbl69FiSpecies selected)
        {
            //_uow.BeginTransaction();
            if (selected.FiSpeciesId != 0) //update
                _uow.Tbl69FiSpeciesses.Update(home);
            else                        //add
                _uow.Tbl69FiSpeciesses.Add(home);

            _uow.Complete();
            //_uow.Commit();
        }
        #endregion

        #region Delete Name, Image, Synonym, Geographic

        //------------------------------ Name --------------------------------------------------------------------------------------------
        public void DeleteName(Tbl78Name selected)
        {
            _uow.Tbl78Names.Remove(selected);
            _uow.Complete();
        }
        //------------------------------ Image --------------------------------------------------------------------------------------------
        public void DeleteImage(Tbl81Image selected)
        {
            _uow.Tbl81Images.Remove(selected);
            _uow.Complete();
        }
        //------------------------------ Synonym --------------------------------------------------------------------------------------------
        public void DeleteSynonym(Tbl84Synonym selected)
        {
            _uow.Tbl84Synonyms.Remove(selected);
            _uow.Complete();
        }
        //------------------------------ Geographic --------------------------------------------------------------------------------------------
        public void DeleteGeographic(Tbl87Geographic selected)
        {
            _uow.Tbl87Geographics.Remove(selected);
            _uow.Complete();
        }

        #endregion


        #endregion FiSpecies

        #region PlSpecies

        #region Get PlSpecies
        public ObservableCollection<T> GetPlSpeciessesCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl72PlSpeciesses
                    .Find(e => e.PlSpeciesId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_context.Tbl72PlSpeciesses
                    .Include(d => d.Tbl66Genusses)
                    .Where(e => e.Tbl66Genusses.GenusName.StartsWith(searchName))
                    .OrderBy(a => a.Tbl66Genusses.GenusName)
                    .ThenBy(a => a.PlSpeciesName)
                    .ThenBy(a => a.Subspecies)
                    .ThenBy(a => a.Divers)
                );
            return collection;
        }
        private ObservableCollection<T> GetPlSpeciessesCollectionAllOrderBy<T>()
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl72PlSpeciesses
                .Include(d => d.Tbl66Genusses)
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.PlSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers));
            return collection;
        }
        public ObservableCollection<T> GetPlSpeciessesCollectionFromGenusIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl72PlSpeciesses
                .Where(e => e.GenusId == id)
                .OrderBy(k => k.PlSpeciesName));
            return collection;
        }
        public ObservableCollection<T> GetPlSpeciessesCollectionFromPlSpeciesIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl72PlSpeciesses
                .Where(e => e.PlSpeciesId == id)
                .OrderBy(k => k.PlSpeciesName));
            return collection;
        }
        public Tbl72PlSpecies GetPlSpeciesSingleByPlSpeciesId<T>(int id)
        {
            Tbl72PlSpecies single = _uow.Tbl72PlSpeciesses.GetById(id);
            //    Tbl72PlSpecies single = _context.Tbl72PlSpeciesses.FirstOrDefault(a => a.PlSpeciesId == id);
            return single;
        }
        public Tbl72PlSpecies GetPlSpeciesSingleByPlSpeciesName<T>(string name)
        {
            Tbl72PlSpecies single = _uow.Tbl72PlSpeciesses.FirstOrDefault(p => p.PlSpeciesName == name);
            //  var plantaeRegnum = _uow.Tbl72PlSpeciesses.FirstOrDefault(p => p.PlSpeciesName == "Plantae#Regnum#");
            return single;
        }

        public ObservableCollection<Tbl72PlSpecies> GetLastPlSpeciessesDatasetOrderById()
        {
            var collection = _context.Tbl72PlSpeciesses
                .OrderBy(c => c.PlSpeciesId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl72PlSpecies> { collection };
        }
        //Function
        public int PlSpeciesIdFromNamesCollectionSelect(int id)
        {
            var coll = _context.Tbl78Names
                .SingleOrDefault(p => p.NameId == id);

            if (coll == null) return 0;
            return coll.PlSpeciesId;
        }
        public ObservableCollection<Tbl72PlSpecies> GetConnectedDatasetsWithGenusIdInTablePlSpecies(int selectedId)
        {
            var collection = new ObservableCollection<Tbl72PlSpecies>(_uow.Tbl72PlSpeciesses.Find(x => x.GenusId == selectedId));
            return collection;
        }
        #endregion

        #region Copy PlSpecies
        public ObservableCollection<Tbl72PlSpecies> CopyPlSpecies(Tbl72PlSpecies selected)
        {
            var dataset = _uow.Tbl72PlSpeciesses.GetById(selected.PlSpeciesId);
            var collection = new ObservableCollection<Tbl72PlSpecies>();

            collection.Insert(0, new Tbl72PlSpecies
            {
                PlSpeciesName = CultRes.StringsRes.DatasetNew,
                Subspecies = "",
                Divers = "",
                GenusId = dataset.GenusId,
                SpeciesgroupId = dataset.SpeciesgroupId,
                Valid = dataset.Valid,
                ValidYear = dataset.ValidYear,
                MemoSpecies = dataset.MemoSpecies,
                TradeName = dataset.TradeName,
                Author = dataset.Author,
                AuthorYear = dataset.AuthorYear,
                Importer = dataset.Importer,
                ImportingYear = dataset.ImportingYear,
                BasinHeight = dataset.BasinHeight,
                PlantLength = dataset.PlantLength,
                Difficult1 = dataset.Difficult1,
                Difficult2 = dataset.Difficult2,
                Difficult3 = dataset.Difficult3,
                Difficult4 = dataset.Difficult4,
                MemoTech = dataset.MemoTech,
                Ph1 = dataset.Ph1,
                Ph2 = dataset.Ph2,
                Temp1 = dataset.Temp1,
                Temp2 = dataset.Temp2,
                Hardness1 = dataset.Hardness1,
                Hardness2 = dataset.Hardness2,
                CarboHardness1 = dataset.CarboHardness1,
                CarboHardness2 = dataset.CarboHardness2,
                MemoBuilt = dataset.MemoBuilt,
                MemoColor = dataset.MemoColor,
                MemoReproduction = dataset.MemoReproduction,
                MemoCulture = dataset.MemoCulture,
                MemoGlobal = dataset.MemoGlobal
            });

            return collection;
        }
        #endregion

        #region Delete PlSpecies
        public void DeletePlSpecies(Tbl72PlSpecies selected)
        {
            _uow.Tbl72PlSpeciesses.Remove(selected);
            //  _context.Tbl72PlSpeciesses.Remove(selected);
            _uow.Complete();
            //  _context.SaveChanges();
        }
        #endregion

        #region Save PlSpecies
        public Tbl72PlSpecies PlSpeciesUpdate(Tbl72PlSpecies home, Tbl72PlSpecies selected)
        {
            if (home != null) //update
            {
                home.PlSpeciesName = selected.PlSpeciesName;
                home.Subspecies = selected.Subspecies;
                home.Divers = selected.Divers;
                home.GenusId = selected.GenusId;
                home.SpeciesgroupId = home.SpeciesgroupId;
                home.Valid = selected.Valid;
                home.ValidYear = selected.ValidYear;
                home.MemoSpecies = selected.MemoSpecies;
                home.TradeName = selected.TradeName;
                home.Author = selected.Author;
                home.AuthorYear = selected.AuthorYear;
                home.Importer = selected.Importer;
                home.ImportingYear = selected.ImportingYear;
                home.BasinHeight = selected.BasinHeight;
                home.PlantLength = selected.PlantLength;
                home.Difficult1 = selected.Difficult1;
                home.Difficult2 = selected.Difficult2;
                home.Difficult3 = selected.Difficult3;
                home.Difficult4 = selected.Difficult4;
                home.MemoTech = selected.MemoTech;
                home.Ph1 = selected.Ph1;
                home.Ph2 = selected.Ph2;
                home.Temp1 = selected.Temp1;
                home.Temp2 = selected.Temp2;
                home.Hardness1 = selected.Hardness1;
                home.Hardness2 = selected.Hardness2;
                home.CarboHardness1 = selected.CarboHardness1;
                home.CarboHardness2 = selected.CarboHardness2;
                home.MemoBuilt = selected.MemoBuilt;
                home.MemoColor = selected.MemoColor;
                home.MemoReproduction = selected.MemoReproduction;
                home.MemoCulture = selected.MemoCulture;
                home.MemoGlobal = selected.MemoGlobal;
                home.Updater = Environment.UserName;
                home.UpdaterDate = DateTime.Now;
            }
            return home;
        }
        public Tbl72PlSpecies PlSpeciesAdd(Tbl72PlSpecies selected)
        {
            var home = new Tbl72PlSpecies() //add new
            {
                PlSpeciesName = selected.PlSpeciesName,
                Subspecies = selected.Subspecies,
                Divers = selected.Divers,
                GenusId = selected.GenusId,
                SpeciesgroupId = selected.SpeciesgroupId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                MemoSpecies = selected.MemoSpecies,
                TradeName = selected.TradeName,
                Author = selected.Author,
                AuthorYear = selected.AuthorYear,
                Importer = selected.Importer,
                ImportingYear = selected.ImportingYear,
                BasinHeight = selected.BasinHeight,
                PlantLength = selected.PlantLength,
                Difficult1 = selected.Difficult1,
                Difficult2 = selected.Difficult2,
                Difficult3 = selected.Difficult3,
                Difficult4 = selected.Difficult4,
                MemoTech = selected.MemoTech,
                Ph1 = selected.Ph1,
                Ph2 = selected.Ph2,
                Temp1 = selected.Temp1,
                Temp2 = selected.Temp2,
                Hardness1 = selected.Hardness1,
                Hardness2 = selected.Hardness2,
                CarboHardness1 = selected.CarboHardness1,
                CarboHardness2 = selected.CarboHardness2,
                MemoBuilt = selected.MemoBuilt,
                MemoColor = selected.MemoColor,
                MemoReproduction = selected.MemoReproduction,
                MemoCulture = selected.MemoCulture,
                MemoGlobal = selected.MemoGlobal,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now
            };
            return home;
        }
        public void PlSpeciesSave(Tbl72PlSpecies home, Tbl72PlSpecies selected)
        {
            //_uow.BeginTransaction();
            if (selected.PlSpeciesId != 0) //update
                _uow.Tbl72PlSpeciesses.Update(home);
            else                        //add
                _uow.Tbl72PlSpeciesses.Add(home);

            _uow.Complete();
            //_uow.Commit();
        }


        #endregion

        #endregion

        #region Name

        #region Get Name
        public ObservableCollection<T> GetNamesCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl78Names
                    .Find(e => e.NameId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl78Names
                    .Find(e => e.NameName.StartsWith(searchName))
                    .OrderBy(a => a.NameName)
                );
            return collection;
        }
        public ObservableCollection<T> GetNamesCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl78Names
                .OrderBy(a => a.NameName));

            return collection;
        }
        public ObservableCollection<T> GetNamesCollectionFromFiSpeciesIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl78Names
                .Where(e => e.FiSpeciesId == id)
                .OrderBy(k => k.NameName));
            return collection;
        }
        public ObservableCollection<T> GetNamesCollectionFromPlSpeciesIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl78Names
                .Where(e => e.PlSpeciesId == id)
                .OrderBy(k => k.NameName));
            return collection;
        }

        public ObservableCollection<Tbl78Name> GetLastNamesDatasetOrderById()
        {
            var collection = _context.Tbl78Names
                .OrderBy(c => c.NameId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl78Name> { collection };
        }

        #endregion

        #region Copy Name
        public ObservableCollection<Tbl78Name> CopyName(Tbl78Name selected)
        {
            var dataset = _uow.Tbl78Names.GetById(selected.NameId);
            var collection = new ObservableCollection<Tbl78Name>();

            collection.Insert(0, new Tbl78Name
            {
                NameName = CultRes.StringsRes.DatasetNew,
                FiSpeciesId = dataset.FiSpeciesId,
                PlSpeciesId = dataset.PlSpeciesId,
                Valid = dataset.Valid,
                ValidYear = dataset.ValidYear,
                Language = dataset.Language,
                Info = dataset.Info,
                Memo = dataset.Memo
            });

            return collection;
        }
        #endregion

        #region Delete Name

        public ObservableCollection<Tbl78Name> SearchForConnectedDatasetsWithFiSpeciesIdInTableName(int selectedId)
        {
            var collection = new ObservableCollection<Tbl78Name>(_uow.Tbl78Names.Find(x => x.FiSpeciesId == selectedId));
            return collection;
        }

        public ObservableCollection<Tbl78Name> SearchForConnectedDatasetsWithPlSpeciesIdInTableName(int selectedId)
        {
            var collection = new ObservableCollection<Tbl78Name>(_uow.Tbl78Names.Find(x => x.PlSpeciesId == selectedId));
            return collection;
        }

        #endregion

        #region Save Name
        public Tbl78Name NameUpdate(Tbl78Name home, Tbl78Name selected)
        {
            if (home != null) //update
            {
                home.NameName = selected.NameName;
                home.FiSpeciesId = selected.FiSpeciesId;
                home.PlSpeciesId = selected.PlSpeciesId;
                home.Valid = selected.Valid;
                home.ValidYear = selected.ValidYear;
                home.Language = selected.Language;
                home.Info = selected.Info;
                home.Memo = selected.Memo;
                home.Updater = Environment.UserName;
                home.UpdaterDate = DateTime.Now;
            }
            return home;
        }
        public Tbl78Name NameAdd(Tbl78Name selected)
        {
            var home = new Tbl78Name() //add new
            {
                NameName = selected.NameName,
                FiSpeciesId = selected.FiSpeciesId,
                PlSpeciesId = selected.PlSpeciesId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Language = selected.Language,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now
            };
            return home;
        }
        public void NameSave(Tbl78Name home, Tbl78Name selected)
        {

            if (selected.NameId != 0) //update
            {
                _uow.Tbl78Names.Update(home);
            }
            else                                //add
                _uow.Tbl78Names.Add(home);
            _uow.Complete();
        }
        #endregion

        #endregion

        #region Image

        #region GetImage
        public ObservableCollection<T> GetImagesCollectionFromSearchIdOrderBy<T>(in int searchImageId)
        {
            ObservableCollection<T> collection;
            var i = searchImageId;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl81Images
                .Where(e => e.ImageId == i)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetImagesCollectionFromFiSpeciesIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl81Images
                .Where(e => e.FiSpeciesId == id)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetImagesCollectionFromPlSpeciesIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl81Images
                .Where(e => e.PlSpeciesId == id)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<Tbl81Image> GetLastImagesDatasetOrderById()
        {
            var collection = _context.Tbl81Images
                .OrderBy(c => c.ImageId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl81Image> { collection };
        }
        #endregion

        #region Copy Image
        public ObservableCollection<Tbl81Image> CopyImage(Tbl81Image selected)
        {
            var dataset = _uow.Tbl81Images.GetById(selected.ImageId);
            var collection = new ObservableCollection<Tbl81Image>();

            collection.Insert(0, new Tbl81Image
            {
                FiSpeciesId = dataset.FiSpeciesId,
                PlSpeciesId = dataset.PlSpeciesId,
                Valid = dataset.Valid,
                ValidYear = dataset.ValidYear,
                ShotDate = dataset.ShotDate,
                Info = dataset.Info,
                ImageData = dataset.ImageData,
                ImageMimeType = dataset.ImageMimeType,
                FilestreamId = Guid.NewGuid(),
                Memo = dataset.Memo
            });

            return collection;
        }
        #endregion

        #region DeleteImage

        public ObservableCollection<Tbl81Image> SearchForConnectedDatasetsWithFiSpeciesIdInTableImage(int selectedId)
        {
            var collection = new ObservableCollection<Tbl81Image>(_uow.Tbl81Images.Find(x => x.FiSpeciesId == selectedId));
            return collection;
        }

        public ObservableCollection<Tbl81Image> SearchForConnectedDatasetsWithPlSpeciesIdInTableImage(int selectedId)
        {
            var collection = new ObservableCollection<Tbl81Image>(_uow.Tbl81Images.Find(x => x.PlSpeciesId == selectedId));
            return collection;
        }



        #endregion

        #region Save Image
        public Tbl81Image ImageUpdate(Tbl81Image home, Tbl81Image selected)
        {
            if (home != null) //update
            {
                home.FiSpeciesId = selected.FiSpeciesId;
                home.PlSpeciesId = selected.PlSpeciesId;
                home.Valid = selected.Valid;
                home.ValidYear = selected.ValidYear;
                home.ShotDate = selected.ShotDate;
                home.Info = selected.Info;
                home.Memo = selected.Memo;
                home.ImageData = selected.ImageData;
                home.ImageMimeType = selected.ImageMimeType;
                if (SelectedPath != null) home.Filestream = LoadImageData(SelectedPath);
                home.FilestreamId = Guid.NewGuid();
                home.Updater = Environment.UserName;
                home.UpdaterDate = DateTime.Now;
            }
            return home;
        }
        public Tbl81Image ImageAdd(Tbl81Image selected)
        {
            var home = new Tbl81Image() //add new
            {
                FiSpeciesId = selected.FiSpeciesId,
                PlSpeciesId = selected.PlSpeciesId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                ShotDate = selected.ShotDate,
                Info = selected.Info,
                Memo = selected.Memo,
                ImageData = selected.ImageData, //empty
                ImageMimeType = selected.ImageMimeType,
                Filestream = LoadImageData(SelectedPath),
                FilestreamId = Guid.NewGuid(),
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now
            };
            return home;
        }
        private static byte[] LoadImageData(string filePath)
        {
            var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var br = new BinaryReader(fs);
            var imageBytes = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();
            return imageBytes;
        }
        public void ImageSave(Tbl81Image home, Tbl81Image selected)
        {

            if (selected.ImageId != 0) //update
            {
                _uow.Tbl81Images.Update(home);
            }
            else                                //add
                _uow.Tbl81Images.Add(home);
            _uow.Complete();
        }

        public static RelayCommand OpenCommand { get; set; }
        private string _selectedPath;

        public string SelectedPath
        {
            get => _selectedPath;
            set { _selectedPath = value; RaisePropertyChanged(""); }
        }
        #endregion

        #endregion

        #region Synonym

        #region Get Synonym
        public ObservableCollection<T> GetSynonymsCollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl84Synonyms
                    .Find(e => e.SynonymId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl84Synonyms
                    .Find(e => e.SynonymName.StartsWith(searchName))
                    .OrderBy(a => a.SynonymName)
                );
            return collection;
        }

        public ObservableCollection<T> GetSynonymsCollectionAllOrderBy<T>()
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl84Synonyms
                .OrderBy(a => a.SynonymName)
                .AsNoTracking());
            return collection;
        }

        public ObservableCollection<T> GetSynonymsCollectionFromFiSpeciesIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl84Synonyms
                .Where(e => e.FiSpeciesId == id)
                .OrderBy(k => k.SynonymName));
            return collection;
        }
        public ObservableCollection<T> GetSynonymsCollectionFromPlSpeciesIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl84Synonyms
                .Where(e => e.PlSpeciesId == id)
                .OrderBy(k => k.SynonymName));
            return collection;
        }
        public ObservableCollection<Tbl84Synonym> GetLastSynonymsDatasetOrderById()
        {
            var collection = _context.Tbl84Synonyms
                .OrderBy(c => c.SynonymId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl84Synonym> { collection };
        }


        #endregion

        #region Copy Synonym
        public ObservableCollection<Tbl84Synonym> CopySynonym(Tbl84Synonym selected)
        {
            var dataset = _uow.Tbl84Synonyms.GetById(selected.SynonymId);
            var collection = new ObservableCollection<Tbl84Synonym>();

            collection.Insert(0, new Tbl84Synonym
            {
                SynonymName = CultRes.StringsRes.DatasetNew,
                FiSpeciesId = dataset.FiSpeciesId,
                PlSpeciesId = dataset.PlSpeciesId,
                Valid = dataset.Valid,
                ValidYear = dataset.ValidYear,
                Author = dataset.Author,
                AuthorYear = dataset.AuthorYear,
                Info = dataset.Info,
                Memo = dataset.Memo
            });

            return collection;
        }
        #endregion

        #region Delete Synonym

        public ObservableCollection<Tbl84Synonym> SearchForConnectedDatasetsWithFiSpeciesIdInTableSynonym(int selectedId)
        {
            var collection = new ObservableCollection<Tbl84Synonym>(_uow.Tbl84Synonyms.Find(x => x.FiSpeciesId == selectedId));
            return collection;
        }

        public ObservableCollection<Tbl84Synonym> SearchForConnectedDatasetsWithPlSpeciesIdInTableSynonym(int selectedId)
        {
            var collection = new ObservableCollection<Tbl84Synonym>(_uow.Tbl84Synonyms.Find(x => x.PlSpeciesId == selectedId));
            return collection;
        }


        #endregion

        #region Save Synonym
        public Tbl84Synonym SynonymUpdate(Tbl84Synonym home, Tbl84Synonym selected)
        {
            if (home != null) //update
            {
                home.SynonymName = selected.SynonymName;
                home.FiSpeciesId = selected.FiSpeciesId;
                home.PlSpeciesId = selected.PlSpeciesId;
                home.Valid = selected.Valid;
                home.ValidYear = selected.ValidYear;
                home.Author = home.Author;
                home.AuthorYear = home.AuthorYear;
                home.Info = selected.Info;
                home.Memo = selected.Memo;
                home.Updater = Environment.UserName;
                home.UpdaterDate = DateTime.Now;
            }
            return home;
        }
        public Tbl84Synonym SynonymAdd(Tbl84Synonym selected)
        {
            var home = new Tbl84Synonym() //add new
            {
                SynonymName = selected.SynonymName,
                FiSpeciesId = selected.FiSpeciesId,
                PlSpeciesId = selected.PlSpeciesId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Author = selected.Author,
                AuthorYear = selected.AuthorYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now
            };
            return home;
        }
        public void SynonymSave(Tbl84Synonym home, Tbl84Synonym selected)
        {

            if (selected.SynonymId != 0) //update
            {
                _uow.Tbl84Synonyms.Update(home);
            }
            else                                //add
                _uow.Tbl84Synonyms.Add(home);
            _uow.Complete();
        }
        #endregion

        #endregion

        #region Geographic

        #region Get Geographic
        public ObservableCollection<T> GetGeographicsCollectionFromSearchIdOrderBy<T>(in int searchgeographicId)
        {
            ObservableCollection<T> collection;
            var i = searchgeographicId;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl87Geographics
                .Where(e => e.GeographicId == i)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetGeographicsCollectionFromFiSpeciesIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl87Geographics
                .Where(e => e.FiSpeciesId == id)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetGeographicsCollectionFromPlSpeciesIdOrderBy<T>(int id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl87Geographics
                .Where(e => e.PlSpeciesId == id)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<Tbl87Geographic> GetLastGeographicsDatasetOrderById()
        {
            var collection = _context.Tbl87Geographics
                .OrderBy(c => c.GeographicId)
                .AsNoTracking()
                .LastOrDefault();
            return new ObservableCollection<Tbl87Geographic> { collection };
        }

        #endregion

        #region Copy Geographic
        public ObservableCollection<Tbl87Geographic> CopyGeographic(Tbl87Geographic selected)
        {
            var dataset = _uow.Tbl87Geographics.GetById(selected.GeographicId);
            var collection = new ObservableCollection<Tbl87Geographic>();

            collection.Insert(0, new Tbl87Geographic
            {
                FiSpeciesId = dataset.FiSpeciesId,
                PlSpeciesId = dataset.PlSpeciesId,
                Address = dataset.Address,
                Continent = dataset.Continent,
                Country = dataset.Country,
                Http = dataset.Http,
                Latitude = dataset.Latitude,
                Longitude = dataset.Longitude,
                Latitude1 = dataset.Latitude1,
                Longitude1 = dataset.Longitude1,
                Latitude2 = dataset.Latitude2,
                Longitude2 = dataset.Longitude2,
                Latitude3 = dataset.Latitude3,
                Longitude3 = dataset.Longitude3,
                ZoomLevel = dataset.ZoomLevel,
                Valid = dataset.Valid,
                ValidYear = dataset.ValidYear,
                Author = dataset.Author,
                AuthorYear = dataset.AuthorYear,
                Info = dataset.Info,
                Memo = dataset.Memo
            });

            return collection;
        }
        #endregion

        #region Delete Geographic

        public ObservableCollection<Tbl87Geographic> SearchForConnectedDatasetsWithFiSpeciesIdInTableGeographic(int selectedId)
        {
            var collection = new ObservableCollection<Tbl87Geographic>(_uow.Tbl87Geographics.Find(x => x.FiSpeciesId == selectedId));
            return collection;
        }

        public ObservableCollection<Tbl87Geographic> SearchForConnectedDatasetsWithPlSpeciesIdInTableGeographic(int selectedId)
        {
            var collection = new ObservableCollection<Tbl87Geographic>(_uow.Tbl87Geographics.Find(x => x.PlSpeciesId == selectedId));
            return collection;
        }


        #endregion

        #region Save Geographic
        public Tbl87Geographic GeographicUpdate(Tbl87Geographic home, Tbl87Geographic selected)
        {
            if (home != null) //update
            {
                home.FiSpeciesId = selected.FiSpeciesId;
                home.PlSpeciesId = selected.PlSpeciesId;
                home.Address = selected.Address;
                home.Continent = selected.Continent;
                home.Country = selected.Country;
                home.Http = selected.Http;
                home.Latitude = selected.Latitude;
                home.Longitude = selected.Longitude;
                home.Latitude1 = selected.Latitude1;
                home.Longitude1 = selected.Longitude1;
                home.Latitude2 = selected.Latitude2;
                home.Longitude2 = selected.Longitude2;
                home.Latitude3 = selected.Latitude3;
                home.Longitude3 = selected.Longitude3;
                home.ZoomLevel = selected.ZoomLevel;
                home.Valid = selected.Valid;
                home.ValidYear = selected.ValidYear;
                home.Author = selected.Author;
                home.AuthorYear = selected.AuthorYear;
                home.Info = selected.Info;
                home.Memo = selected.Memo;
                home.Updater = Environment.UserName;
                home.UpdaterDate = DateTime.Now;
            }
            return home;
        }
        public Tbl87Geographic GeographicAdd(Tbl87Geographic selected)
        {
            var home = new Tbl87Geographic() //add new
            {
                FiSpeciesId = selected.FiSpeciesId,
                PlSpeciesId = selected.PlSpeciesId,
                CountId = RandomHelper.Randomnumber(),
                Address = selected.Address,
                Continent = selected.Continent,
                Country = selected.Country,
                Http = selected.Http,
                Latitude = selected.Latitude,
                Longitude = selected.Longitude,
                Latitude1 = selected.Latitude1,
                Longitude1 = selected.Longitude1,
                Latitude2 = selected.Latitude2,
                Longitude2 = selected.Longitude2,
                Latitude3 = selected.Latitude3,
                Longitude3 = selected.Longitude3,
                ZoomLevel = selected.ZoomLevel,
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Author = selected.Author,
                AuthorYear = selected.AuthorYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now
            };
            return home;
        }
        public void GeographicSave(Tbl87Geographic home, Tbl87Geographic selected)
        {
            if (selected.GeographicId != 0) //update
            {
                _uow.Tbl87Geographics.Update(home);
            }
            else                                //add
                _uow.Tbl87Geographics.Add(home);
            _uow.Complete();
        }
        #endregion

        #endregion


        #region References

        #region Reference Get
        public Tbl90Reference GetReferenceSingleByReferenceId<T>(int referenceId)
        {
            Tbl90Reference single = _uow.Tbl90References.GetById(referenceId);
            //    Tbl90Reference single = _context.Tbl90References.FirstOrDefault(a => a.ReferenceId == referenceId);
            return single;
        }

        public ObservableCollection<T> GetReferencesCollectionAllOrderBy<T>()
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .OrderBy(a => a.Info)
                .AsNoTracking());
            return collection;
        }

        #endregion

        #region Reference Copy
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
        public ObservableCollection<Tbl90Reference> CopyReferenceSuperfamily(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        SuperfamilyId = dataset.SuperfamilyId,
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
                        SuperfamilyId = dataset.SuperfamilyId,
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
                        SuperfamilyId = dataset.SuperfamilyId,
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
        public ObservableCollection<Tbl90Reference> CopyReferenceFamily(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        FamilyId = dataset.FamilyId,
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
                        FamilyId = dataset.FamilyId,
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
                        FamilyId = dataset.FamilyId,
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
        public ObservableCollection<Tbl90Reference> CopyReferenceSubfamily(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        SubfamilyId = dataset.SubfamilyId,
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
                        SubfamilyId = dataset.SubfamilyId,
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
                        SubfamilyId = dataset.SubfamilyId,
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
        public ObservableCollection<Tbl90Reference> CopyReferenceInfrafamily(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        InfrafamilyId = dataset.InfrafamilyId,
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
                        InfrafamilyId = dataset.InfrafamilyId,
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
                        InfrafamilyId = dataset.InfrafamilyId,
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
        public ObservableCollection<Tbl90Reference> CopyReferenceSupertribus(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        SupertribusId = dataset.SupertribusId,
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
                        SupertribusId = dataset.SupertribusId,
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
                        SupertribusId = dataset.SupertribusId,
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
        public ObservableCollection<Tbl90Reference> CopyReferenceTribus(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        TribusId = dataset.TribusId,
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
                        TribusId = dataset.TribusId,
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
                        TribusId = dataset.TribusId,
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
        public ObservableCollection<Tbl90Reference> CopyReferenceSubtribus(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        SubtribusId = dataset.SubtribusId,
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
                        SubtribusId = dataset.SubtribusId,
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
                        SubtribusId = dataset.SubtribusId,
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
        public ObservableCollection<Tbl90Reference> CopyReferenceInfratribus(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        InfratribusId = dataset.InfratribusId,
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
                        InfratribusId = dataset.InfratribusId,
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
                        InfratribusId = dataset.InfratribusId,
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
        public ObservableCollection<Tbl90Reference> CopyReferenceGenus(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        GenusId = dataset.GenusId,
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
                        GenusId = dataset.GenusId,
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
                        GenusId = dataset.GenusId,
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
        public ObservableCollection<Tbl90Reference> CopyReferenceFiSpecies(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        FiSpeciesId = dataset.FiSpeciesId,
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
                        FiSpeciesId = dataset.FiSpeciesId,
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
                        FiSpeciesId = dataset.FiSpeciesId,
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
        public ObservableCollection<Tbl90Reference> CopyReferencePlSpecies(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        PlSpeciesId = dataset.PlSpeciesId,
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
                        PlSpeciesId = dataset.PlSpeciesId,
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
                        PlSpeciesId = dataset.PlSpeciesId,
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

        #region Reference Delete
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithRegnumIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.RegnumId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithPhylumIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.PhylumId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithDivisionIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.DivisionId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithSubphylumIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SubphylumId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithSubdivisionIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SubdivisionId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithSuperclassIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SuperclassId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithClassIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.ClassId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithSubclassIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SubclassId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithInfraclassIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.InfraclassId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithLegioIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.LegioId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithOrdoIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.OrdoId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithSubordoIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SubordoId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithInfraordoIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.InfraordoId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithSuperfamilyIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SuperfamilyId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithFamilyIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.FamilyId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithSubfamilyIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SubfamilyId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithInfrafamilyIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.InfrafamilyId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithSupertribusIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SupertribusId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithTribusIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.TribusId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithSubtribusIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.SubtribusId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithInfratribusIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.InfratribusId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithGenusIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.GenusId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithFiSpeciesIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.FiSpeciesId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithPlSpeciesIdInTableReference(int selectedId)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.PlSpeciesId == selectedId));
            return collection;
        }

        //-----------------------DeleteReferences, DeleteReference -----------------------------------

        public void DeleteReferences(ObservableCollection<Tbl90Reference> coll)
        {
            foreach (var t in coll)
                _uow.Tbl90References.Remove(t);
            _uow.Complete();
        }
        public void DeleteReference(Tbl90Reference selected)
        {
            _uow.Tbl90References.Remove(selected);
            _uow.Complete();
        }

        #endregion

        #region Reference Save

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

        #endregion


        #region Reference Experts

        #region Reference Expert Get
        public ObservableCollection<T> GetReferenceExpertsCollectionFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RegnumId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromPhylumIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.PhylumId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromDivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.DivisionId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromSubphylumIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.SubphylumId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromSubdivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.SubdivisionId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromSuperclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.SuperclassId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromClassIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.ClassId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromSubclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.SubclassId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromInfraclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.InfraclassId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromLegioIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.LegioId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromOrdoIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.OrdoId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromSubordoIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.SubordoId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromInfraordoIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.InfraordoId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromSuperfamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.SuperfamilyId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromFamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.FamilyId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromSubfamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.SubfamilyId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromInfrafamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.InfrafamilyId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromSupertribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.SupertribusId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromTribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.TribusId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromSubtribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.SubtribusId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromInfratribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.InfratribusId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromGenusIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.GenusId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromFiSpeciesIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.FiSpeciesId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionFromPlSpeciesIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.PlSpeciesId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }

        public ObservableCollection<T> GetExpertsCollectionAllOrderBy<T>()
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90RefExperts
                .OrderBy(a => a.RefExpertName)
                .AsNoTracking());
            return collection;
        }

        public ObservableCollection<T> GetReferenceExpertsCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90RefExperts
                .OrderBy(a => a.RefExpertName));
            return collection;
        }

        #endregion

        #region Reference Expert Save
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
        public Tbl90Reference ReferenceExpertSuperfamilyUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.SuperfamilyId = selected.SuperfamilyId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertSuperfamilyAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                SuperfamilyId = selected.SuperfamilyId,
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
        public Tbl90Reference ReferenceExpertFamilyUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.FamilyId = selected.FamilyId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertFamilyAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                FamilyId = selected.FamilyId,
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
        public Tbl90Reference ReferenceExpertSubfamilyUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.SubfamilyId = selected.SubfamilyId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertSubfamilyAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                SubfamilyId = selected.SubfamilyId,
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
        public Tbl90Reference ReferenceExpertInfrafamilyUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.InfrafamilyId = selected.InfrafamilyId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertInfrafamilyAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                InfrafamilyId = selected.InfrafamilyId,
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
        public Tbl90Reference ReferenceExpertSupertribusUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.SupertribusId = selected.SupertribusId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertSupertribusAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                SupertribusId = selected.SupertribusId,
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
        public Tbl90Reference ReferenceExpertTribusUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.TribusId = selected.TribusId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertTribusAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                TribusId = selected.TribusId,
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
        public Tbl90Reference ReferenceExpertSubtribusUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.SubtribusId = selected.SubtribusId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertSubtribusAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                SubtribusId = selected.SubtribusId,
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
        public Tbl90Reference ReferenceExpertInfratribusUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.InfratribusId = selected.InfratribusId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertInfratribusAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                InfratribusId = selected.InfratribusId,
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
        public Tbl90Reference ReferenceExpertGenusUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.GenusId = selected.GenusId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertGenusAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                GenusId = selected.GenusId,
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
        public Tbl90Reference ReferenceExpertFiSpeciesUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.FiSpeciesId = selected.FiSpeciesId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertFiSpeciesAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                FiSpeciesId = selected.FiSpeciesId,
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
        public Tbl90Reference ReferenceExpertPlSpeciesUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.PlSpeciesId = selected.PlSpeciesId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertPlSpeciesAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                PlSpeciesId = selected.PlSpeciesId,
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

        #endregion

        #endregion

        #region Reference Sources

        #region Reference Source Get
        public ObservableCollection<T> GetReferenceSourcesCollectionFromRegnumIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RegnumId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromPhylumIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.PhylumId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromDivisionIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.DivisionId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromSubphylumIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.SubphylumId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromSubdivisionIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.SubdivisionId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromSuperclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.SuperclassId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromClassIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.ClassId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromSubclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.SubclassId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromInfraclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.InfraclassId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromLegioIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.LegioId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromOrdoIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.OrdoId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromSubordoIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.SubordoId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromInfraordoIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.InfraordoId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromSuperfamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.SuperfamilyId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromFamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.FamilyId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromSubfamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.SubfamilyId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromInfrafamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.InfrafamilyId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromSupertribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.SupertribusId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromTribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.TribusId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromSubtribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.SubtribusId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromInfratribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.InfratribusId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromGenusIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.GenusId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromFiSpeciesIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.FiSpeciesId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromPlSpeciesIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.PlSpeciesId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }


        public ObservableCollection<T> GetSourcesCollectionAllOrderBy<T>()
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90RefSources
                .OrderBy(a => a.RefSourceName)
                .AsNoTracking());
            return collection;
        }

        private ObservableCollection<T> GetReferenceSourcesCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90RefSources
                .OrderBy(a => a.RefSourceName));
            return collection;
        }


        #endregion

        #region Reference Source Save
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
        public Tbl90Reference ReferenceSourceSuperfamilyUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.SuperfamilyId = selected.SuperfamilyId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceSuperfamilyAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                SuperfamilyId = selected.SuperfamilyId,
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
        public Tbl90Reference ReferenceSourceFamilyUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.FamilyId = selected.FamilyId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceFamilyAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                FamilyId = selected.FamilyId,
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
        public Tbl90Reference ReferenceSourceSubfamilyUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.SubfamilyId = selected.SubfamilyId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceSubfamilyAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                SubfamilyId = selected.SubfamilyId,
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
        public Tbl90Reference ReferenceSourceInfrafamilyUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.InfrafamilyId = selected.InfrafamilyId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceInfrafamilyAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                InfrafamilyId = selected.InfrafamilyId,
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
        public Tbl90Reference ReferenceSourceSupertribusUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.SupertribusId = selected.SupertribusId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceSupertribusAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                SupertribusId = selected.SupertribusId,
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
        public Tbl90Reference ReferenceSourceTribusUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.TribusId = selected.TribusId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceTribusAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                TribusId = selected.TribusId,
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
        public Tbl90Reference ReferenceSourceSubtribusUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.SubtribusId = selected.SubtribusId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceSubtribusAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                SubtribusId = selected.SubtribusId,
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
        public Tbl90Reference ReferenceSourceInfratribusUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.InfratribusId = selected.InfratribusId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceInfratribusAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                InfratribusId = selected.InfratribusId,
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
        public Tbl90Reference ReferenceSourceGenusUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.GenusId = selected.GenusId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceGenusAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                GenusId = selected.GenusId,
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
        public Tbl90Reference ReferenceSourceFiSpeciesUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.FiSpeciesId = selected.FiSpeciesId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceFiSpeciesAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                FiSpeciesId = selected.FiSpeciesId,
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
        public Tbl90Reference ReferenceSourcePlSpeciesUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.PlSpeciesId = selected.PlSpeciesId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourcePlSpeciesAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                PlSpeciesId = selected.PlSpeciesId,
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

        #endregion

        #endregion

        #region Reference Authors

        #region Reference Author Get
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromRegnumIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RegnumId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromPhylumIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.PhylumId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromDivisionIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.DivisionId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromSubphylumIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.SubphylumId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromSubdivisionIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.SubdivisionId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromSuperclassIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.SuperclassId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromClassIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.ClassId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromSubclassIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.SubclassId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromInfraclassIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.InfraclassId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromLegioIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.LegioId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromOrdoIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.OrdoId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromSubordoIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.SubordoId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromInfraordoIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.InfraordoId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromSuperfamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.SuperfamilyId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromFamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.FamilyId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromSubfamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.SubfamilyId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromInfrafamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.InfrafamilyId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromSupertribusIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.SupertribusId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromTribusIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.TribusId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromSubtribusIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.SubtribusId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromInfratribusIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.InfratribusId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromGenusIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.GenusId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromFiSpeciesIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.FiSpeciesId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromPlSpeciesIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.PlSpeciesId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }


        public ObservableCollection<T> GetAuthorsCollectionAllOrderBy<T>()
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90RefAuthors
                .OrderBy(a => a.RefAuthorName)
                .AsNoTracking());
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

        #endregion

        #region Reference Author Save
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
        public Tbl90Reference ReferenceAuthorSuperfamilyUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.SuperfamilyId = selected.SuperfamilyId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorSuperfamilyAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                SuperfamilyId = selected.SuperfamilyId,
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
        public Tbl90Reference ReferenceAuthorFamilyUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.FamilyId = selected.FamilyId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorFamilyAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                FamilyId = selected.FamilyId,
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
        public Tbl90Reference ReferenceAuthorSubfamilyUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.SubfamilyId = selected.SubfamilyId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorSubfamilyAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                SubfamilyId = selected.SubfamilyId,
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
        public Tbl90Reference ReferenceAuthorInfrafamilyUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.InfrafamilyId = selected.InfrafamilyId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorInfrafamilyAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                InfrafamilyId = selected.SubfamilyId,
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
        public Tbl90Reference ReferenceAuthorSupertribusUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.SupertribusId = selected.SupertribusId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorSupertribusAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                SupertribusId = selected.SupertribusId,
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
        public Tbl90Reference ReferenceAuthorTribusUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.TribusId = selected.TribusId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorTribusAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                TribusId = selected.TribusId,
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
        public Tbl90Reference ReferenceAuthorSubtribusUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.SubtribusId = selected.SubtribusId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorSubtribusAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                SubtribusId = selected.SubtribusId,
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
        public Tbl90Reference ReferenceAuthorInfratribusUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.InfratribusId = selected.InfratribusId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorInfratribusAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                InfratribusId = selected.SubtribusId,
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
        public Tbl90Reference ReferenceAuthorGenusUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.GenusId = selected.GenusId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorGenusAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                GenusId = selected.GenusId,
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
        public Tbl90Reference ReferenceAuthorFiSpeciesUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.FiSpeciesId = selected.FiSpeciesId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorFiSpeciesAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                FiSpeciesId = selected.FiSpeciesId,
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
        public Tbl90Reference ReferenceAuthorPlSpeciesUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.PlSpeciesId = selected.PlSpeciesId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorPlSpeciesAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                PlSpeciesId = selected.PlSpeciesId,
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

        #endregion

        #endregion


        #region Comment

        #region Comment Get
        public ObservableCollection<T> GetCommentsCollectionFromRegnumIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.RegnumId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromPhylumIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.PhylumId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromDivisionIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.DivisionId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromSubphylumIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.SubphylumId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromSubdivisionIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.SubdivisionId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromSuperclassIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.SuperclassId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromClassIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.ClassId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromSubclassIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.SubclassId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromInfraclassIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.InfraclassId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromLegioIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.LegioId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromOrdoIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.OrdoId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromSubordoIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.SubordoId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromInfraordoIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.InfraordoId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromSuperfamilyIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.SuperfamilyId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromFamilyIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.FamilyId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromSubfamilyIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.SubfamilyId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromInfrafamilyIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.InfrafamilyId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromSupertribusIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.SupertribusId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromTribusIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.TribusId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromSubtribusIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.SubtribusId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromInfratribusIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.InfratribusId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromGenusIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.GenusId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromFiSpeciesIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.FiSpeciesId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromPlSpeciesIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.PlSpeciesId == id)
                .OrderBy(e => e.Info));
            return collection;
        }


        public ObservableCollection<T> GetCommentsCollectionAllOrderBy<T>()
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .OrderBy(a => a.Info)
                .AsNoTracking());
            return collection;
        }

        public Tbl93Comment GetCommentSingleByCommentId<T>(int commentId)
        {
            Tbl93Comment single = _uow.Tbl93Comments.GetById(commentId);
            //    Tbl93Comment single = _context.Tbl93Comments.FirstOrDefault(a => a.CommentId == commentId);
            return single;
        }


        #endregion

        #region Comment Copy
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
                case "Superfamily":
                    collection.Insert(0, new Tbl93Comment
                    {
                        SuperfamilyId = dataset.SuperfamilyId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Family":
                    collection.Insert(0, new Tbl93Comment
                    {
                        FamilyId = dataset.FamilyId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Subfamily":
                    collection.Insert(0, new Tbl93Comment
                    {
                        SubfamilyId = dataset.SubfamilyId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Infrafamily":
                    collection.Insert(0, new Tbl93Comment
                    {
                        InfrafamilyId = dataset.InfrafamilyId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Supertribus":
                    collection.Insert(0, new Tbl93Comment
                    {
                        SupertribusId = dataset.SupertribusId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Tribus":
                    collection.Insert(0, new Tbl93Comment
                    {
                        TribusId = dataset.TribusId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Subtribus":
                    collection.Insert(0, new Tbl93Comment
                    {
                        SubtribusId = dataset.SubtribusId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Infratribus":
                    collection.Insert(0, new Tbl93Comment
                    {
                        InfratribusId = dataset.InfratribusId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Genus":
                    collection.Insert(0, new Tbl93Comment
                    {
                        GenusId = dataset.GenusId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "FiSpecies":
                    collection.Insert(0, new Tbl93Comment
                    {
                        FiSpeciesId = dataset.FiSpeciesId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "PlSpecies":
                    collection.Insert(0, new Tbl93Comment
                    {
                        PlSpeciesId = dataset.PlSpeciesId,
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

        #region Comment Delete
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithRegnumIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.RegnumId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithPhylumIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.PhylumId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithDivisionIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.DivisionId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithSubphylumIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SubphylumId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithSubdivisionIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SubdivisionId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithSuperclassIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SuperclassId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithClassIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.ClassId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithSubclassIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SubclassId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithInfraclassIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.InfraclassId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithLegioIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.LegioId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithOrdoIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.OrdoId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithSubordoIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SubordoId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithInfraordoIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.InfraordoId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithSuperfamilyIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SuperfamilyId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithFamilyIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.FamilyId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithSubfamilyIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SubfamilyId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithInfrafamilyIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.InfrafamilyId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithSupertribusIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SupertribusId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithTribusIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.TribusId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithSubtribusIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.SubtribusId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithInfratribusIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.InfratribusId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithGenusIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.GenusId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithFiSpeciesIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.FiSpeciesId == selectedId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithPlSpeciesIdInTableComment(int selectedId)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.PlSpeciesId == selectedId));
            return collection;
        }

        //----------------------- DeleteComments,DeleteComment -----------------------------------

        public void DeleteComments(ObservableCollection<Tbl93Comment> coll)
        {
            foreach (var t in coll)
                _uow.Tbl93Comments.Remove(t);
            _uow.Complete();
        }
        public void DeleteComment(Tbl93Comment selected)
        {
            _uow.Tbl93Comments.Remove(selected);
            _uow.Complete();
        }

        #endregion

        #region Comment Save
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
        public Tbl93Comment CommentSuperfamilyUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.SuperfamilyId = selected.SuperfamilyId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentSuperfamilyAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                SuperfamilyId = selected.SuperfamilyId,
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
        public Tbl93Comment CommentFamilyUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.FamilyId = selected.FamilyId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentFamilyAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                FamilyId = selected.FamilyId,
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
        public Tbl93Comment CommentSubfamilyUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.SubfamilyId = selected.SubfamilyId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentSubfamilyAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                SubfamilyId = selected.SubfamilyId,
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
        public Tbl93Comment CommentInfrafamilyUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.InfrafamilyId = selected.InfrafamilyId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentInfrafamilyAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                InfrafamilyId = selected.InfrafamilyId,
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
        public Tbl93Comment CommentSupertribusUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.SupertribusId = selected.SupertribusId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentSupertribusAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                SupertribusId = selected.SupertribusId,
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
        public Tbl93Comment CommentTribusUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.TribusId = selected.TribusId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentTribusAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                TribusId = selected.TribusId,
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
        public Tbl93Comment CommentSubtribusUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.SubtribusId = selected.SubtribusId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentSubtribusAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                SubtribusId = selected.SubtribusId,
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
        public Tbl93Comment CommentInfratribusUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.InfratribusId = selected.InfratribusId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentInfratribusAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                InfratribusId = selected.InfratribusId,
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
        public Tbl93Comment CommentGenusUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.GenusId = selected.GenusId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentGenusAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                GenusId = selected.GenusId,
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
        public Tbl93Comment CommentFiSpeciesUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.FiSpeciesId = selected.FiSpeciesId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentFiSpeciesAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                FiSpeciesId = selected.FiSpeciesId,
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
        public Tbl93Comment CommentPlSpeciesUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.PlSpeciesId = selected.PlSpeciesId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentPlSpeciesAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                PlSpeciesId = selected.PlSpeciesId,
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


        public void CommentSave(Tbl93Comment home, Tbl93Comment selected)
        {
            if (selected.CommentId != 0)             //update
                _uow.Tbl93Comments.Update(home);
            else                                     //add
                _uow.Tbl93Comments.Add(home);

            _uow.Complete();
        }

        #endregion

        #endregion

        #region Country

        #region Get Country

        public ObservableCollection<T> GetCountriesCollectionAllOrderBy<T>()
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.TblCountries
                .OrderBy(a => a.Name)
                .AsNoTracking());
            return collection;
        }


        #endregion

        #endregion
    }

}
