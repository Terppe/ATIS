using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ATIS.Ui.Views.Search
{
    public class SearchBasicGet : ViewModelBase
    {
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();

        public ObservableCollection<T> SearchFilterTextReturnCollection<T>(string searchName, string name)
        {
            var collection = new ObservableCollection<T>();

            switch (name)
            {
                case "regnum":
                    collection = RegnumsFilterCollection<T>(searchName);
                    break;
                case "phylum":
                    collection = PhylumsFilterCollection<T>(searchName);
                    break;
                case "subphylum":
                    collection = SubphylumsFilterCollection<T>(searchName);
                    break;
                case "division":
                    collection = DivisionsFilterCollection<T>(searchName);
                    break;
                case "subdivision":
                    collection = SubdivisionsFilterCollection<T>(searchName);
                    break;
                case "superclass":
                    collection = SuperclassesFilterCollection<T>(searchName);
                    break;
                case "class":
                    collection = ClassesFilterCollection<T>(searchName);
                    break;
                case "subclass":
                    collection = SubclassesFilterCollection<T>(searchName);
                    break;
                case "infraclass":
                    collection = InfraclassesFilterCollection<T>(searchName);
                    break;
                case "legio":
                    collection = LegiosFilterCollection<T>(searchName);
                    break;
                case "ordo":
                    collection = OrdosFilterCollection<T>(searchName);
                    break;
                case "subordo":
                    collection = SubordosFilterCollection<T>(searchName);
                    break;
                case "infraordo":
                    collection = InfraordosFilterCollection<T>(searchName);
                    break;
                case "superfamily":
                    collection = SuperfamiliesFilterCollection<T>(searchName);
                    break;
                case "family":
                    collection = FamiliesFilterCollection<T>(searchName);
                    break;
                case "subfamily":
                    collection = SubfamiliesFilterCollection<T>(searchName);
                    break;
                case "infrafamily":
                    collection = InfrafamiliesFilterCollection<T>(searchName);
                    break;
                case "supertribus":
                    collection = SupertribussesFilterCollection<T>(searchName);
                    break;
                case "tribus":
                    collection = TribussesFilterCollection<T>(searchName);
                    break;
                case "subtribus":
                    collection = SubtribussesFilterCollection<T>(searchName);
                    break;
                case "infratribus":
                    collection = InfratribussesFilterCollection<T>(searchName);
                    break;
                case "genus":
                    collection = GenussesFilterCollection<T>(searchName);
                    break;
                case "fispecies":
                    collection = FiSpeciessesFilterCollection<T>(searchName);
                    break;
                case "plspecies":
                    collection = PlSpeciessesFilterCollection<T>(searchName);
                    break;
                case "name":
                    collection = NamesFilterCollection<T>(searchName);
                    break;
                case "synonym":
                    collection = SynonymsFilterCollection<T>(searchName);
                    break;
            }

            return collection;
        }

        //-----------------------Regnum---------------------------------

        public ObservableCollection<T> RegnumsFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl03Regnums
                .Find(e => e.RegnumName.StartsWith(filterText) &&
                           e.RegnumName.Contains("#") == false ||
                           e.Subregnum.Contains(filterText) ||
                           e.EngName.Contains(filterText) ||
                           e.GerName.Contains(filterText) ||
                           e.FraName.Contains(filterText) ||
                           e.PorName.Contains(filterText)
                )
                .OrderBy(e => e.RegnumName)
                .ThenBy(e => e.Subregnum));

            return collection;
        }
        //-----------------------Phylum---------------------------------

        public ObservableCollection<T> PhylumsFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl06Phylums
                .Find(e => e.PhylumName.StartsWith(filterText) &&
                           e.PhylumName.Contains("#") == false ||
                           e.EngName.Contains(filterText) ||
                           e.GerName.Contains(filterText) ||
                           e.FraName.Contains(filterText) ||
                           e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.PhylumName));

            return collection;
        }
        //-----------------------Subphylum---------------------------------

        public ObservableCollection<T> SubphylumsFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl12Subphylums
                .Find(e => e.SubphylumName.StartsWith(filterText) &&
                           e.SubphylumName.Contains("#") == false ||
                           e.EngName.Contains(filterText) ||
                           e.GerName.Contains(filterText) ||
                           e.FraName.Contains(filterText) ||
                           e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.SubphylumName));

            return collection;
        }
        //-----------------------Division---------------------------------

        public ObservableCollection<T> DivisionsFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl09Divisions
                .Find(e => e.DivisionName.StartsWith(filterText) &&
                           e.DivisionName.Contains("#") == false ||
                           e.EngName.Contains(filterText) ||
                           e.GerName.Contains(filterText) ||
                           e.FraName.Contains(filterText) ||
                           e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.DivisionName));

            return collection;
        }
        //-----------------------Subdivision---------------------------------

        public ObservableCollection<T> SubdivisionsFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl15Subdivisions
                .Find(e => e.SubdivisionName.StartsWith(filterText) &&
                           e.SubdivisionName.Contains("#") == false ||
                           e.EngName.Contains(filterText) ||
                           e.GerName.Contains(filterText) ||
                           e.FraName.Contains(filterText) ||
                           e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.SubdivisionName));

            return collection;
        }
        //-----------------------Superclass---------------------------------

        public ObservableCollection<T> SuperclassesFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl18Superclasses
                .Find(e => e.SuperclassName.StartsWith(filterText) &&
                           e.SuperclassName.Contains("#") == false ||
                           e.EngName.Contains(filterText) ||
                           e.GerName.Contains(filterText) ||
                           e.FraName.Contains(filterText) ||
                           e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.SuperclassName));

            return collection;
        }
        //-----------------------Class---------------------------------

        public ObservableCollection<T> ClassesFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl21Classes
                .Find(e => e.ClassName.StartsWith(filterText) &&
                           e.ClassName.Contains("#") == false ||
                           e.EngName.Contains(filterText) ||
                           e.GerName.Contains(filterText) ||
                           e.FraName.Contains(filterText) ||
                           e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.ClassName));

            return collection;
        }
        //-----------------------Subclass---------------------------------

        public ObservableCollection<T> SubclassesFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl24Subclasses
                .Find(e => e.SubclassName.StartsWith(filterText) &&
                           e.SubclassName.Contains("#") == false ||
                           e.EngName.Contains(filterText) ||
                           e.GerName.Contains(filterText) ||
                           e.FraName.Contains(filterText) ||
                           e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.SubclassName));

            return collection;
        }
        //-----------------------Infraclass---------------------------------

        public ObservableCollection<T> InfraclassesFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl27Infraclasses
                .Find(e => e.InfraclassName.StartsWith(filterText) &&
                           e.InfraclassName.Contains("#") == false ||
                           e.EngName.Contains(filterText) ||
                           e.GerName.Contains(filterText) ||
                           e.FraName.Contains(filterText) ||
                           e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.InfraclassName));

            return collection;
        }
        //-----------------------Legio---------------------------------

        public ObservableCollection<T> LegiosFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl30Legios
                .Find(e => e.LegioName.StartsWith(filterText) &&
                           e.LegioName.Contains("#") == false ||
                           e.EngName.Contains(filterText) ||
                           e.GerName.Contains(filterText) ||
                           e.FraName.Contains(filterText) ||
                           e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.LegioName));

            return collection;
        }
        //-----------------------Ordo---------------------------------

        public ObservableCollection<T> OrdosFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl33Ordos
                .Find(e => e.OrdoName.StartsWith(filterText) &&
                           e.OrdoName.Contains("#") == false ||
                           e.EngName.Contains(filterText) ||
                           e.GerName.Contains(filterText) ||
                           e.FraName.Contains(filterText) ||
                           e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.OrdoName));

            return collection;
        }
        //-----------------------Subordo---------------------------------

        public ObservableCollection<T> SubordosFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl36Subordos
                .Find(e => e.SubordoName.StartsWith(filterText) &&
                           e.SubordoName.Contains("#") == false ||
                           e.EngName.Contains(filterText) ||
                           e.GerName.Contains(filterText) ||
                           e.FraName.Contains(filterText) ||
                           e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.SubordoName));

            return collection;
        }
        //-----------------------Infraordo---------------------------------

        public ObservableCollection<T> InfraordosFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl39Infraordos
                .Find(e => e.InfraordoName.StartsWith(filterText) &&
                           e.InfraordoName.Contains("#") == false ||
                           e.EngName.Contains(filterText) ||
                           e.GerName.Contains(filterText) ||
                           e.FraName.Contains(filterText) ||
                           e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.InfraordoName));

            return collection;
        }
        //-----------------------Superfamily---------------------------------

        public ObservableCollection<T> SuperfamiliesFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl42Superfamilies
                .Find(e => e.SuperfamilyName.StartsWith(filterText) &&
                           e.SuperfamilyName.Contains("#") == false ||
                           e.EngName.Contains(filterText) ||
                           e.GerName.Contains(filterText) ||
                           e.FraName.Contains(filterText) ||
                           e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.SuperfamilyName));

            return collection;
        }
        //-----------------------Family---------------------------------

        public ObservableCollection<T> FamiliesFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl45Families
                .Find(e => e.FamilyName.StartsWith(filterText) &&
                           e.FamilyName.Contains("#") == false ||
                           e.EngName.Contains(filterText) ||
                           e.GerName.Contains(filterText) ||
                           e.FraName.Contains(filterText) ||
                           e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.FamilyName));

            return collection;
        }
        //-----------------------Subfamily---------------------------------

        public ObservableCollection<T> SubfamiliesFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl48Subfamilies
                .Find(e => e.SubfamilyName.StartsWith(filterText) &&
                           e.SubfamilyName.Contains("#") == false ||
                           e.EngName.Contains(filterText) ||
                           e.GerName.Contains(filterText) ||
                           e.FraName.Contains(filterText) ||
                           e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.SubfamilyName));

            return collection;
        }
        //-----------------------Infrafamily---------------------------------

        public ObservableCollection<T> InfrafamiliesFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl51Infrafamilies
                .Find(e => e.InfrafamilyName.StartsWith(filterText) &&
                           e.InfrafamilyName.Contains("#") == false ||
                           e.EngName.Contains(filterText) ||
                           e.GerName.Contains(filterText) ||
                           e.FraName.Contains(filterText) ||
                           e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.InfrafamilyName));

            return collection;
        }
        //-----------------------Supertribus---------------------------------

        public ObservableCollection<T> SupertribussesFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl54Supertribusses
                .Find(e => e.SupertribusName.StartsWith(filterText) &&
                           e.SupertribusName.Contains("#") == false ||
                           e.EngName.Contains(filterText) ||
                           e.GerName.Contains(filterText) ||
                           e.FraName.Contains(filterText) ||
                           e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.SupertribusName));

            return collection;
        }
        //-----------------------Tribus---------------------------------

        public ObservableCollection<T> TribussesFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl57Tribusses
                .Find(e => e.TribusName.StartsWith(filterText) &&
                           e.TribusName.Contains("#") == false ||
                           e.EngName.Contains(filterText) ||
                           e.GerName.Contains(filterText) ||
                           e.FraName.Contains(filterText) ||
                           e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.TribusName));

            return collection;
        }
        //-----------------------Subtribus---------------------------------

        public ObservableCollection<T> SubtribussesFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl60Subtribusses
                .Find(e => e.SubtribusName.StartsWith(filterText) &&
                           e.SubtribusName.Contains("#") == false ||
                           e.EngName.Contains(filterText) ||
                           e.GerName.Contains(filterText) ||
                           e.FraName.Contains(filterText) ||
                           e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.SubtribusName));

            return collection;
        }
        //-----------------------Infratribus---------------------------------

        public ObservableCollection<T> InfratribussesFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl63Infratribusses
                .Find(e => e.InfratribusName.StartsWith(filterText) &&
                           e.InfratribusName.Contains("#") == false ||
                           e.EngName.Contains(filterText) ||
                           e.GerName.Contains(filterText) ||
                           e.FraName.Contains(filterText) ||
                           e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.InfratribusName));

            return collection;
        }
        //-----------------------Genus---------------------------------

        public ObservableCollection<T> GenussesFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl66Genusses
                .Find(e => e.GenusName.StartsWith(filterText) &&
                           e.GenusName.Contains("#") == false ||
                           e.EngName.Contains(filterText) ||
                           e.GerName.Contains(filterText) ||
                           e.FraName.Contains(filterText) ||
                           e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.GenusName));

            return collection;
        }
        //-----------------------FiSpecies---------------------------------

        public ObservableCollection<T> FiSpeciessesFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl69FiSpeciesses
                .Include(a => a.Tbl66Genusses)
                .Where(e => e.FiSpeciesName.StartsWith(filterText) &&
                            e.FiSpeciesName.Contains("#") == false ||
                            e.Tbl66Genusses.GenusName.Contains(filterText) ||
                            e.Subspecies.Contains(filterText) ||
                            e.Divers.Contains(filterText) ||
                            e.TradeName.Contains(filterText) ||
                            e.Author.Contains(filterText)
                )
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.FiSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers)
            );

            return collection;
        }

        //-----------------------PlSpecies---------------------------------

        public ObservableCollection<T> PlSpeciessesFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl72PlSpeciesses
                .Include(a => a.Tbl66Genusses)
                .Where(e => e.PlSpeciesName.StartsWith(filterText) &&
                           e.PlSpeciesName.Contains("#") == false ||
                           e.Tbl66Genusses.GenusName.Contains(filterText) ||
                           e.Subspecies.Contains(filterText) ||
                           e.Divers.Contains(filterText) ||
                           e.TradeName.Contains(filterText) ||
                           e.Author.Contains(filterText)
                )
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.PlSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers)
            );

            return collection;
        }
        //-----------------------Name---------------------------------

        public ObservableCollection<T> NamesFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl78Names
                .Find(e => e.NameName.StartsWith(filterText) &&
                           e.Tbl69FiSpeciesses.FiSpeciesId == e.FiSpeciesId &&
                           e.Tbl72PlSpeciesses.PlSpeciesId == e.PlSpeciesId
                )
                .OrderBy(a => a.NameName));

            return collection;
        }
        //-----------------------Synonym---------------------------------

        public ObservableCollection<T> SynonymsFilterCollection<T>(string filterText)
        {
            var collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl84Synonyms
                .Find(e => e.SynonymName.StartsWith(filterText) &&
                                        e.Tbl69FiSpeciesses.FiSpeciesId == e.FiSpeciesId &&
                                        e.Tbl72PlSpeciesses.PlSpeciesId == e.PlSpeciesId
                )
                .OrderBy(a => a.SynonymName));

            return collection;
        }

    }
}
