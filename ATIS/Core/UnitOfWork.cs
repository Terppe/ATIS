using ATIS.Ui.Core.Interfaces_UOW;
using ATIS.Ui.Core.Repositories_UOW;

namespace ATIS.Ui.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AtisDbContext _context;

        public UnitOfWork(AtisDbContext context)
        {
            _context = context;
            Tbl03Regnums = new Tbl03RegnumRepository(_context);
            Tbl06Phylums = new Tbl06PhylumRepository(_context);
            Tbl09Divisions = new Tbl09DivisionRepository(_context);
            Tbl12Subphylums = new Tbl12SubphylumRepository(_context);
            Tbl15Subdivisions = new Tbl15SubdivisionRepository(_context);
            Tbl18Superclasses = new Tbl18SuperclassRepository(_context);
            Tbl21Classes = new Tbl21ClassRepository(_context);
            Tbl24Subclasses = new Tbl24SubclassRepository(_context);
            Tbl27Infraclasses = new Tbl27InfraclassRepository(_context);
            Tbl30Legios = new Tbl30LegioRepository(_context);
            Tbl33Ordos = new Tbl33OrdoRepository(_context);
            Tbl36Subordos = new Tbl36SubordoRepository(_context);
            Tbl39Infraordos = new Tbl39InfraordoRepository(_context);
            Tbl42Superfamilies = new Tbl42SuperfamilyRepository(_context);
            Tbl45Families = new Tbl45FamilyRepository(_context);
            Tbl48Subfamilies = new Tbl48SubfamilyRepository(_context);
            Tbl51Infrafamilies = new Tbl51InfrafamilyRepository(_context);
            Tbl54Supertribusses = new Tbl54SupertribusRepository(_context);
            Tbl57Tribusses = new Tbl57TribusRepository(_context);
            Tbl60Subtribusses = new Tbl60SubtribusRepository(_context);
            Tbl63Infratribusses = new Tbl63InfratribusRepository(_context);
            Tbl66Genusses = new Tbl66GenusRepository(_context);
            Tbl68Speciesgroups = new Tbl68SpeciesgroupRepository(_context);
            Tbl69FiSpeciesses = new Tbl69FiSpeciesRepository(_context);
            Tbl72PlSpeciesses = new Tbl72PlSpeciesRepository(_context);
            Tbl78Names = new Tbl78NameRepository(_context);
            Tbl81Images = new Tbl81ImageRepository(_context);
            Tbl84Synonyms = new Tbl84SynonymRepository(_context);
            Tbl87Geographics = new Tbl87GeographicRepository(_context);

            Tbl90References = new Tbl90ReferenceRepository(_context);
            Tbl90RefExperts = new Tbl90RefExpertRepository(_context);
            Tbl90RefSources = new Tbl90RefSourceRepository(_context);
            Tbl90RefAuthors = new Tbl90RefAuthorRepository(_context);
            Tbl93Comments = new Tbl93CommentRepository(_context);
            TblCountries = new TblCountryRepository(_context);
            TblUserProfiles = new TblUserProfileRepository(_context);
        }


        public ITbl03RegnumRepository Tbl03Regnums { get; }
        public ITbl06PhylumRepository Tbl06Phylums { get; }
        public ITbl09DivisionRepository Tbl09Divisions { get; }
        public ITbl12SubphylumRepository Tbl12Subphylums { get; }
        public ITbl15SubdivisionRepository Tbl15Subdivisions { get; }
        public ITbl18SuperclassRepository Tbl18Superclasses { get; }
        public ITbl21ClassRepository Tbl21Classes { get; }
        public ITbl24SubclassRepository Tbl24Subclasses { get; }
        public ITbl27InfraclassRepository Tbl27Infraclasses { get; }
        public ITbl30LegioRepository Tbl30Legios { get; }
        public ITbl33OrdoRepository Tbl33Ordos { get; }
        public ITbl36SubordoRepository Tbl36Subordos { get; }
        public ITbl39InfraordoRepository Tbl39Infraordos { get; }
        public ITbl42SuperfamilyRepository Tbl42Superfamilies { get; }
        public ITbl45FamilyRepository Tbl45Families { get; }
        public ITbl48SubfamilyRepository Tbl48Subfamilies { get; }
        public ITbl51InfrafamilyRepository Tbl51Infrafamilies { get; }
        public ITbl54SupertribusRepository Tbl54Supertribusses { get; }
        public ITbl57TribusRepository Tbl57Tribusses { get; }
        public ITbl60SubtribusRepository Tbl60Subtribusses { get; }
        public ITbl63InfratribusRepository Tbl63Infratribusses { get; }
        public ITbl66GenusRepository Tbl66Genusses { get; }
        public ITbl68SpeciesgroupRepository Tbl68Speciesgroups { get; }
        public ITbl69FiSpeciesRepository Tbl69FiSpeciesses { get; }
        public ITbl72PlSpeciesRepository Tbl72PlSpeciesses { get; }
        public ITbl78NameRepository Tbl78Names { get; }
        public ITbl81ImageRepository Tbl81Images { get; }
        public ITbl84SynonymRepository Tbl84Synonyms { get; }
        public ITbl87GeographicRepository Tbl87Geographics { get; }

        public ITbl90ReferenceRepository Tbl90References { get; }
        public ITbl90RefExpertRepository Tbl90RefExperts { get; }
        public ITbl90RefSourceRepository Tbl90RefSources { get; }
        public ITbl90RefAuthorRepository Tbl90RefAuthors { get; }
        public ITbl93CommentRepository Tbl93Comments { get; }
        public ITblCountryRepository TblCountries { get; }
        public ITblUserProfileRepository TblUserProfiles { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
