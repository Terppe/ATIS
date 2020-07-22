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

            Tbl90References = new Tbl90ReferenceRepository(_context);
            Tbl90RefExperts = new Tbl90RefExpertRepository(_context);
            Tbl90RefSources = new Tbl90RefSourceRepository(_context);
            Tbl90RefAuthors = new Tbl90RefAuthorRepository(_context);
            Tbl93Comments = new Tbl93CommentRepository(_context);
        }


        public ITbl03RegnumRepository Tbl03Regnums { get; }
        public ITbl06PhylumRepository Tbl06Phylums { get; }
        public ITbl09DivisionRepository Tbl09Divisions { get; }
        public ITbl12SubphylumRepository Tbl12Subphylums { get; }
        public ITbl15SubdivisionRepository Tbl15Subdivisions { get; }
        public ITbl18SuperclassRepository Tbl18Superclasses { get; }

        public ITbl90ReferenceRepository Tbl90References { get; }
        public ITbl90RefExpertRepository Tbl90RefExperts { get; }
        public ITbl90RefSourceRepository Tbl90RefSources { get; }
        public ITbl90RefAuthorRepository Tbl90RefAuthors { get; }
        public ITbl93CommentRepository Tbl93Comments { get; }

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
