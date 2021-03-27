using ATIS.Ui.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace ATIS.Ui.Views.Report
{
    public class ReportBasicGet : ViewModelBase
    {
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();

        #region Regnum

        public ObservableCollection<Tbl03Regnum> CollRegnumsByRegnumId(int id)
        {
            var collection = new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums
                .Find(e => e.RegnumId == id));
            return collection;
        }

        //direct children
        public ObservableCollection<Tbl06Phylum> CollPhylumsByRegnumIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl06Phylum>(_uow.Tbl06Phylums
                .Find(e => e.RegnumId == id &&
                           e.PhylumName.Contains("#") == false)
                .OrderBy(a => a.PhylumName));
            return collection;
        }

        public ObservableCollection<Tbl09Division> CollDivisionsByRegnumIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions
                .Find(e => e.RegnumId == id &&
                           e.DivisionName.Contains("#") == false)
                .OrderBy(a => a.DivisionName));
            return collection;
        }

        // References
        public ObservableCollection<Tbl90Reference> CollExpertsByRegnumId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.RegnumId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesByRegnumId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.RegnumId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsByRegnumId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.RegnumId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }

        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsByRegnumId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.RegnumId == id));
            return collection;
        }
        #endregion

        #region Phylum

        public ObservableCollection<Tbl06Phylum> CollPhylumsByPhylumId(int id)
        {
            var collection = new ObservableCollection<Tbl06Phylum>(_uow.Tbl06Phylums
                .Find(e => e.PhylumId == id));
            return collection;
        }

        //direct children
        public ObservableCollection<Tbl12Subphylum> CollSubphylumsByPhylumIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl12Subphylum>(_uow.Tbl12Subphylums
                .Find(e => e.PhylumId == id &&
                           e.SubphylumName.Contains("#") == false));
            return collection;
        }

        //Function
        public int RegnumIdFromPhylumsCollectionSelect(int id)
        {
            var dbset = _context.Tbl06Phylums
                .SingleOrDefault(p => p.PhylumId == id);

            if (dbset != null) return dbset.RegnumId;
            return 0;
        }

        // ForeignKey
        public ObservableCollection<Tbl03Regnum> CollRegnumsByRegnumIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums
                .Find(e => e.RegnumId == id &&
                           e.RegnumName.Contains("#") == false));
            return collection;
        }

        // References
        public ObservableCollection<Tbl90Reference> CollExpertsByPhylumId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
            .Include(a => a.Tbl90RefExperts)
            .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
            e.PhylumId == id &&
            e.RefAuthorId.HasValue == false &&
            e.RefSourceId.HasValue == false)
            .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesByPhylumId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
            .Include(a => a.Tbl90RefSources)
            .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
            e.PhylumId == id &&
            e.RefAuthorId.HasValue == false &&
            e.RefExpertId.HasValue == false)
            .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsByPhylumId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
            .Include(a => a.Tbl90RefAuthors)
            .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
            e.PhylumId == id &&
            e.RefSourceId.HasValue == false &&
            e.RefExpertId.HasValue == false)
            .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }

        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsByPhylumId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.PhylumId == id));
            return collection;
        }

        #endregion

        #region Division
        public ObservableCollection<Tbl09Division> CollDivisionsByDivisionId(int id)
        {
            var collection = new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions
                .Find(e => e.DivisionId == id));
            return collection;
        }
        //direct children
        public ObservableCollection<Tbl15Subdivision> CollSubdivisionsByDivisionIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl15Subdivision>(_uow.Tbl15Subdivisions
                .Find(e => e.DivisionId == id &&
                           e.SubdivisionName.Contains("#") == false));
            return collection;
        }
        //Function
        public int RegnumIdFromDivisionsCollectionSelect(int id)
        {
            var dbset = _context.Tbl09Divisions
                .SingleOrDefault(p => p.DivisionId == id);

            if (dbset != null) return dbset.RegnumId;
            return 0;
        }
        // ForeignKey from Phylum

        // References
        public ObservableCollection<Tbl90Reference> CollExpertsByDivisionId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.DivisionId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesByDivisionId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.DivisionId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsByDivisionId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.DivisionId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }

        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsByDivisionId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.DivisionId == id));
            return collection;
        }

        #endregion

        #region Subphylum

        public ObservableCollection<Tbl12Subphylum> CollSubphylumsBySubphylumId(int id)
        {
            var collection = new ObservableCollection<Tbl12Subphylum>(_uow.Tbl12Subphylums
                .Find(e => e.SubphylumId == id));
            return collection;
        }

        //direct children
        public ObservableCollection<Tbl18Superclass> CollSuperclassesBySubphylumIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl18Superclass>(_uow.Tbl18Superclasses
                .Find(e => e.SubphylumId == id &&
                           e.SuperclassName.Contains("#") == false));
            return collection;
        }
        //Function
        public int PhylumIdFromSubphylumsCollectionSelect(int id)
        {
            var dbset = _context.Tbl12Subphylums
                .SingleOrDefault(p => p.SubphylumId == id);

            if (dbset != null) return dbset.PhylumId;
            return 0;
        }

        // ForeignKey
        public ObservableCollection<Tbl06Phylum> CollPhylumsByPhylumIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl06Phylum>(_uow.Tbl06Phylums
                .Find(e => e.PhylumId == id &&
                           e.PhylumName.Contains("#") == false));
            return collection;
        }

        // References
        public ObservableCollection<Tbl90Reference> CollExpertsBySubphylumId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
            .Include(a => a.Tbl90RefExperts)
            .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
            e.SubphylumId == id &&
            e.RefAuthorId.HasValue == false &&
            e.RefSourceId.HasValue == false)
            .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesBySubphylumId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
            .Include(a => a.Tbl90RefSources)
            .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
            e.SubphylumId == id &&
            e.RefAuthorId.HasValue == false &&
            e.RefExpertId.HasValue == false)
            .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsBySubphylumId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
            .Include(a => a.Tbl90RefAuthors)
            .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
            e.SubphylumId == id &&
            e.RefSourceId.HasValue == false &&
            e.RefExpertId.HasValue == false)
            .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }

        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsBySubphylumId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.SubphylumId == id));
            return collection;
        }


        #endregion

        #region Subdivision

        public ObservableCollection<Tbl15Subdivision> CollSubdivisionsBySubdivisionId(int id)
        {
            var collection = new ObservableCollection<Tbl15Subdivision>(_uow.Tbl15Subdivisions
                .Find(e => e.SubdivisionId == id));
            return collection;
        }

        //direct children
        public ObservableCollection<Tbl18Superclass> CollSuperclassesBySubdivisionIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl18Superclass>(_uow.Tbl18Superclasses
                .Find(e => e.SubdivisionId == id &&
                           e.SuperclassName.Contains("#") == false));
            return collection;
        }
        //Function
        public int DivisionIdFromSubdivisionsCollectionSelect(int id)
        {
            var dbset = _context.Tbl15Subdivisions
                .SingleOrDefault(p => p.SubdivisionId == id);

            if (dbset == null) return 0;
            return dbset.DivisionId;
        }

        // ForeignKey
        public ObservableCollection<Tbl09Division> CollDivisionsByDivisionIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions
                .Find(e => e.DivisionId == id &&
                           e.DivisionName.Contains("#") == false));
            return collection;
        }

        // References
        public ObservableCollection<Tbl90Reference> CollExpertsBySubdivisionId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
            .Include(a => a.Tbl90RefExperts)
            .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
            e.SubdivisionId == id &&
            e.RefAuthorId.HasValue == false &&
            e.RefSourceId.HasValue == false)
            .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesBySubdivisionId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
            .Include(a => a.Tbl90RefSources)
            .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
            e.SubdivisionId == id &&
            e.RefAuthorId.HasValue == false &&
            e.RefExpertId.HasValue == false)
            .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsBySubdivisionId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
            .Include(a => a.Tbl90RefAuthors)
            .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
            e.SubdivisionId == id &&
            e.RefSourceId.HasValue == false &&
            e.RefExpertId.HasValue == false)
            .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }

        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsBySubdivisionId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.SubdivisionId == id));
            return collection;
        }
        #endregion

        #region Superclass
        public ObservableCollection<Tbl18Superclass> CollSuperclassesBySuperclassId(int id)
        {
            var collection = new ObservableCollection<Tbl18Superclass>(_uow.Tbl18Superclasses
                .Find(e => e.SuperclassId == id));
            return collection;
        }

        //direct children
        public ObservableCollection<Tbl21Class> CollClassesBySuperclassIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl21Class>(_uow.Tbl21Classes
                .Find(e => e.SuperclassId == id &&
                           e.ClassName.Contains("#") == false));
            return collection;
        }
        //Function
        public int SubdivisionIdFromSuperclassesCollectionSelect(int id)
        {
            var dbset = _context.Tbl18Superclasses
                .SingleOrDefault(p => p.SuperclassId == id);

            if (dbset != null) return dbset.SubdivisionId;
            return 0;
        }
        //Function
        public int SubphylumIdFromSuperclassesCollectionSelect(int id)
        {
            var dbset = _context.Tbl18Superclasses
                .SingleOrDefault(p => p.SuperclassId == id);

            if (dbset != null) return dbset.SubphylumId;
            return 0;
        }
        // ForeignKey 2x
        public ObservableCollection<Tbl12Subphylum> CollSubphylumsBySubphylumIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl12Subphylum>(_uow.Tbl12Subphylums
                .Find(e => e.SubphylumId == id &&
                           e.SubphylumName.Contains("#") == false));
            return collection;
        }
        public ObservableCollection<Tbl15Subdivision> CollSubdivisionsBySubdivisionIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl15Subdivision>(_uow.Tbl15Subdivisions
                .Find(e => e.SubdivisionId == id &&
                           e.SubdivisionName.Contains("#") == false));
            return collection;
        }

        // References
        public ObservableCollection<Tbl90Reference> CollExpertsBySuperclassId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
            .Include(a => a.Tbl90RefExperts)
            .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
            e.SuperclassId == id &&
            e.RefAuthorId.HasValue == false &&
            e.RefSourceId.HasValue == false)
            .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesBySuperclassId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
            .Include(a => a.Tbl90RefSources)
            .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
            e.SuperclassId == id &&
            e.RefAuthorId.HasValue == false &&
            e.RefExpertId.HasValue == false)
            .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsBySuperclassId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
            .Include(a => a.Tbl90RefAuthors)
            .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
            e.SuperclassId == id &&
            e.RefSourceId.HasValue == false &&
            e.RefExpertId.HasValue == false)
            .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }

        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsBySuperclassId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.SuperclassId == id));
            return collection;
        }

        #endregion

        #region Class
        public ObservableCollection<Tbl21Class> CollClassesByClassId(int id)
        {
            var collection = new ObservableCollection<Tbl21Class>(_uow.Tbl21Classes
                .Find(e => e.ClassId == id));
            return collection;
        }
        //direct children
        public ObservableCollection<Tbl24Subclass> CollSubclassesByClassIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl24Subclass>(_uow.Tbl24Subclasses
                .Find(e => e.ClassId == id &&
                           e.SubclassName.Contains("#") == false));
            return collection;
        }
        //Function
        public int SuperclassIdFromClassesCollectionSelect(int id)
        {
            var dbset = _context.Tbl21Classes
                .SingleOrDefault(p => p.ClassId == id);

            if (dbset != null) return dbset.SuperclassId;
            return 0;
        }
        // ForeignKey
        public ObservableCollection<Tbl18Superclass> CollSuperclassesBySuperclassIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl18Superclass>(_uow.Tbl18Superclasses
                .Find(e => e.SuperclassId == id &&
                           e.SuperclassName.Contains("#") == false));
            return collection;
        }
        // References
        public ObservableCollection<Tbl90Reference> CollExpertsByClassId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.ClassId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesByClassId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.ClassId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsByClassId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.ClassId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }

        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsByClassId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.ClassId == id));
            return collection;
        }

        #endregion

        #region Subclass
        public ObservableCollection<Tbl24Subclass> CollSubclassesBySubclassId(int id)
        {
            var collection = new ObservableCollection<Tbl24Subclass>(_uow.Tbl24Subclasses
                .Find(e => e.SubclassId == id));
            return collection;
        }
        //direct children
        public ObservableCollection<Tbl27Infraclass> CollInfraclassesBySubclassIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl27Infraclass>(_uow.Tbl27Infraclasses
                .Find(e => e.SubclassId == id &&
                           e.InfraclassName.Contains("#") == false));
            return collection;
        }
        //Function
        public int ClassIdFromSubclassesCollectionSelect(int id)
        {
            var dbset = _context.Tbl24Subclasses
                .SingleOrDefault(p => p.SubclassId == id);

            if (dbset != null) return dbset.ClassId;
            return 0;
        }
        // ForeignKey
        public ObservableCollection<Tbl21Class> CollClassesByClassIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl21Class>(_uow.Tbl21Classes
                .Find(e => e.ClassId == id &&
                           e.ClassName.Contains("#") == false));
            return collection;
        }
        // References
        public ObservableCollection<Tbl90Reference> CollExpertsBySubclassId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.SubclassId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesBySubclassId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.SubclassId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsBySubclassId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.SubclassId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsBySubclassId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.SubclassId == id));
            return collection;
        }
        #endregion

        #region Infraclass
        public ObservableCollection<Tbl27Infraclass> CollInfraclassesByInfraclassId(int id)
        {
            var collection = new ObservableCollection<Tbl27Infraclass>(_uow.Tbl27Infraclasses
                .Find(e => e.InfraclassId == id));
            return collection;
        }
        //direct children
        public ObservableCollection<Tbl30Legio> CollLegiosByInfraclassIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl30Legio>(_uow.Tbl30Legios
                .Find(e => e.InfraclassId == id &&
                           e.LegioName.Contains("#") == false));
            return collection;
        }
        //Function
        public int SubclassIdFromInfraclassesCollectionSelect(int id)
        {
            var dbset = _context.Tbl27Infraclasses
                .SingleOrDefault(p => p.InfraclassId == id);

            if (dbset != null) return dbset.SubclassId;
            return 0;
        }
        // ForeignKey
        public ObservableCollection<Tbl24Subclass> CollSubclassesBySubclassIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl24Subclass>(_uow.Tbl24Subclasses
                .Find(e => e.SubclassId == id &&
                           e.SubclassName.Contains("#") == false));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollExpertsByInfraclassId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.InfraclassId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesByInfraclassId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.InfraclassId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsByInfraclassId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.InfraclassId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsByInfraclassId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.InfraclassId == id));
            return collection;
        }
        #endregion

        #region Legio
        public ObservableCollection<Tbl30Legio> CollLegiosByLegioId(int id)
        {
            var collection = new ObservableCollection<Tbl30Legio>(_uow.Tbl30Legios
                .Find(e => e.LegioId == id));
            return collection;
        }
        public ObservableCollection<Tbl33Ordo> CollOrdosByLegioIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl33Ordo>(_uow.Tbl33Ordos
                .Find(e => e.LegioId == id &&
                           e.OrdoName.Contains("#") == false));
            return collection;
        }
        //Function
        public int InfraclassIdFromLegiosCollectionSelect(int id)
        {
            var dbset = _context.Tbl30Legios
                .SingleOrDefault(p => p.LegioId == id);

            if (dbset != null) return dbset.InfraclassId;
            return 0;
        }
        // ForeignKey
        public ObservableCollection<Tbl27Infraclass> CollInfraclassesByInfraclassIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl27Infraclass>(_uow.Tbl27Infraclasses
                .Find(e => e.InfraclassId == id &&
                           e.InfraclassName.Contains("#") == false));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollExpertsByLegioId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.LegioId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesByLegioId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.LegioId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsByLegioId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.LegioId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsByLegioId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.LegioId == id));
            return collection;
        }
        #endregion

        #region Ordo
        public ObservableCollection<Tbl33Ordo> CollOrdosByOrdoId(int id)
        {
            var collection = new ObservableCollection<Tbl33Ordo>(_uow.Tbl33Ordos
                .Find(e => e.OrdoId == id));
            return collection;
        }
        public ObservableCollection<Tbl36Subordo> CollSubordosByOrdoIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl36Subordo>(_uow.Tbl36Subordos
                .Find(e => e.OrdoId == id &&
                           e.SubordoName.Contains("#") == false));
            return collection;
        }
        //Function
        public int LegioIdFromOrdosCollectionSelect(int id)
        {
            var dbset = _context.Tbl33Ordos
                .SingleOrDefault(p => p.OrdoId == id);

            if (dbset != null) return dbset.LegioId;
            return 0;
        }
        // ForeignKey
        public ObservableCollection<Tbl30Legio> CollLegiosByLegioIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl30Legio>(_uow.Tbl30Legios
                .Find(e => e.LegioId == id &&
                           e.LegioName.Contains("#") == false));
            return collection;
        }
        //Reference
        public ObservableCollection<Tbl90Reference> CollExpertsByOrdoId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.OrdoId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesByOrdoId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.OrdoId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsByOrdoId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.OrdoId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsByOrdoId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.OrdoId == id));
            return collection;
        }
        #endregion

        #region Subordo
        public ObservableCollection<Tbl36Subordo> CollSubordosBySubordoId(int id)
        {
            var collection = new ObservableCollection<Tbl36Subordo>(_uow.Tbl36Subordos
                .Find(e => e.SubordoId == id));
            return collection;
        }
        public ObservableCollection<Tbl39Infraordo> CollInfraordosBySubordoIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl39Infraordo>(_uow.Tbl39Infraordos
                .Find(e => e.SubordoId == id &&
                           e.InfraordoName.Contains("#") == false));
            return collection;
        }
        //Function
        public int OrdoIdFromSubordosCollectionSelect(int id)
        {
            var dbset = _context.Tbl36Subordos
                .SingleOrDefault(p => p.SubordoId == id);

            if (dbset != null) return dbset.OrdoId;
            return 0;
        }
        // ForeignKey
        public ObservableCollection<Tbl33Ordo> CollOrdosByOrdoIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl33Ordo>(_uow.Tbl33Ordos
                .Find(e => e.OrdoId == id &&
                           e.OrdoName.Contains("#") == false));
            return collection;
        }
        //Reference
        public ObservableCollection<Tbl90Reference> CollExpertsBySubordoId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.SubordoId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesBySubordoId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.SubordoId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsBySubordoId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.SubordoId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsBySubordoId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.SubordoId == id));
            return collection;
        }
        #endregion

        #region Infraordo
        public ObservableCollection<Tbl39Infraordo> CollInfraordosByInfraordoId(int id)
        {
            var collection = new ObservableCollection<Tbl39Infraordo>(_uow.Tbl39Infraordos
                .Find(e => e.InfraordoId == id));
            return collection;
        }
        public ObservableCollection<Tbl42Superfamily> CollSuperfamiliesByInfraordoIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl42Superfamily>(_uow.Tbl42Superfamilies
                .Find(e => e.InfraordoId == id &&
                           e.SuperfamilyName.Contains("#") == false));
            return collection;
        }
        //Function
        public int SubordoIdFromInfraordosCollectionSelect(int id)
        {
            var dbset = _context.Tbl39Infraordos
                .SingleOrDefault(p => p.InfraordoId == id);

            if (dbset != null) return dbset.SubordoId;
            return 0;
        }
        // ForeignKey
        public ObservableCollection<Tbl36Subordo> CollSubordosBySubordoIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl36Subordo>(_uow.Tbl36Subordos
                .Find(e => e.SubordoId == id &&
                           e.SubordoName.Contains("#") == false));
            return collection;
        }
        //Reference
        public ObservableCollection<Tbl90Reference> CollExpertsByInfraordoId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.InfraordoId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesByInfraordoId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.InfraordoId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsByInfraordoId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.InfraordoId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsByInfraordoId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.InfraordoId == id));
            return collection;
        }
        #endregion

        #region Superfamily
        public ObservableCollection<Tbl42Superfamily> CollSuperfamiliesBySuperfamilyId(int id)
        {
            var collection = new ObservableCollection<Tbl42Superfamily>(_uow.Tbl42Superfamilies
                .Find(e => e.SuperfamilyId == id));
            return collection;
        }
        public ObservableCollection<Tbl45Family> CollFamiliesBySuperfamilyIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl45Family>(_uow.Tbl45Families
                .Find(e => e.SuperfamilyId == id &&
                           e.FamilyName.Contains("#") == false));
            return collection;
        }
        //Function
        public int InfraordoIdFromSuperfamiliesCollectionSelect(int id)
        {
            var dbset = _context.Tbl42Superfamilies
                .SingleOrDefault(p => p.SuperfamilyId == id);

            if (dbset != null) return dbset.InfraordoId;
            return 0;
        }
        // ForeignKey
        public ObservableCollection<Tbl39Infraordo> CollInfraordosByInfraordoIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl39Infraordo>(_uow.Tbl39Infraordos
                .Find(e => e.InfraordoId == id &&
                           e.InfraordoName.Contains("#") == false));
            return collection;
        }
        //Reference
        public ObservableCollection<Tbl90Reference> CollExpertsBySuperfamilyId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.SuperfamilyId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesBySuperfamilyId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.SuperfamilyId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsBySuperfamilyId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.SuperfamilyId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsBySuperfamilyId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.SuperfamilyId == id));
            return collection;
        }
        #endregion

        #region Family
        public ObservableCollection<Tbl45Family> CollFamiliesByFamilyId(int id)
        {
            var collection = new ObservableCollection<Tbl45Family>(_uow.Tbl45Families
                .Find(e => e.FamilyId == id));
            return collection;
        }
        public ObservableCollection<Tbl48Subfamily> CollSubfamiliesByFamilyIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl48Subfamily>(_uow.Tbl48Subfamilies
                .Find(e => e.FamilyId == id &&
                           e.SubfamilyName.Contains("#") == false));
            return collection;
        }
        //Function
        public int SuperfamilyIdFromFamiliesCollectionSelect(int id)
        {
            var dbset = _context.Tbl45Families
                .SingleOrDefault(p => p.FamilyId == id);

            if (dbset != null) return dbset.SuperfamilyId;
            return 0;
        }
        // ForeignKey
        public ObservableCollection<Tbl42Superfamily> CollSuperfamiliesBySuperfamilyIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl42Superfamily>(_uow.Tbl42Superfamilies
                .Find(e => e.SuperfamilyId == id &&
                           e.SuperfamilyName.Contains("#") == false));
            return collection;
        }
        //Reference
        public ObservableCollection<Tbl90Reference> CollExpertsByFamilyId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.FamilyId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesByFamilyId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.FamilyId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsByFamilyId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.FamilyId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsByFamilyId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.FamilyId == id));
            return collection;
        }
        #endregion

        #region Subfamily
        public ObservableCollection<Tbl48Subfamily> CollSubfamiliesBySubfamilyId(int id)
        {
            var collection = new ObservableCollection<Tbl48Subfamily>(_uow.Tbl48Subfamilies
                .Find(e => e.SubfamilyId == id));
            return collection;
        }
        public ObservableCollection<Tbl51Infrafamily> CollInfrafamiliesBySubfamilyIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl51Infrafamily>(_uow.Tbl51Infrafamilies
                .Find(e => e.SubfamilyId == id &&
                           e.InfrafamilyName.Contains("#") == false));
            return collection;
        }
        //Function
        public int FamilyIdFromSubfamiliesCollectionSelect(int id)
        {
            var dbset = _context.Tbl48Subfamilies
                .SingleOrDefault(p => p.SubfamilyId == id);

            if (dbset != null) return dbset.FamilyId;
            return 0;
        }
        // ForeignKey
        public ObservableCollection<Tbl45Family> CollFamiliesByFamilyIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl45Family>(_uow.Tbl45Families
                .Find(e => e.FamilyId == id &&
                           e.FamilyName.Contains("#") == false));
            return collection;
        }
        //Reference
        public ObservableCollection<Tbl90Reference> CollExpertsBySubfamilyId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.SubfamilyId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesBySubfamilyId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.SubfamilyId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsBySubfamilyId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.SubfamilyId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsBySubfamilyId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.SubfamilyId == id));
            return collection;
        }
        #endregion

        #region Infrafamily
        public ObservableCollection<Tbl51Infrafamily> CollInfrafamiliesByInfrafamilyId(int id)
        {
            var collection = new ObservableCollection<Tbl51Infrafamily>(_uow.Tbl51Infrafamilies
                .Find(e => e.InfrafamilyId == id));
            return collection;
        }
        public ObservableCollection<Tbl54Supertribus> CollSupertribussesByInfrafamilyIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl54Supertribus>(_uow.Tbl54Supertribusses
                .Find(e => e.InfrafamilyId == id &&
                           e.SupertribusName.Contains("#") == false));
            return collection;
        }
        //Function
        public int SubfamilyIdFromInfrafamiliesCollectionSelect(int id)
        {
            var dbset = _context.Tbl51Infrafamilies
                .SingleOrDefault(p => p.InfrafamilyId == id);

            if (dbset != null) return dbset.SubfamilyId;
            return 0;
        }
        // ForeignKey
        public ObservableCollection<Tbl48Subfamily> CollSubfamiliesBySubfamilyIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl48Subfamily>(_uow.Tbl48Subfamilies
                .Find(e => e.SubfamilyId == id &&
                           e.SubfamilyName.Contains("#") == false));
            return collection;
        }
        //Reference
        public ObservableCollection<Tbl90Reference> CollExpertsByInfrafamilyId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.InfrafamilyId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesByInfrafamilyId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.InfrafamilyId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsByInfrafamilyId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.InfrafamilyId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsByInfrafamilyId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.InfrafamilyId == id));
            return collection;
        }
        #endregion

        #region Supertribus
        public ObservableCollection<Tbl54Supertribus> CollSupertribussesBySupertribusId(int id)
        {
            var collection = new ObservableCollection<Tbl54Supertribus>(_uow.Tbl54Supertribusses
                .Find(e => e.SupertribusId == id));
            return collection;
        }
        public ObservableCollection<Tbl57Tribus> CollTribussesBySupertribusIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl57Tribus>(_uow.Tbl57Tribusses
                .Find(e => e.SupertribusId == id &&
                           e.TribusName.Contains("#") == false));
            return collection;
        }
        //Function
        public int InfrafamilyIdFromSupertribussesCollectionSelect(int id)
        {
            var dbset = _context.Tbl54Supertribusses
                .SingleOrDefault(p => p.SupertribusId == id);

            if (dbset != null) return dbset.InfrafamilyId;
            return 0;
        }
        // ForeignKey
        public ObservableCollection<Tbl51Infrafamily> CollInfrafamiliesByInfrafamilyIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl51Infrafamily>(_uow.Tbl51Infrafamilies
                .Find(e => e.InfrafamilyId == id &&
                           e.InfrafamilyName.Contains("#") == false));
            return collection;
        }
        //Reference
        public ObservableCollection<Tbl90Reference> CollExpertsBySupertribusId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.SupertribusId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesBySupertribusId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.SupertribusId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsBySupertribusId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.SupertribusId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsBySupertribusId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.SupertribusId == id));
            return collection;
        }
        #endregion

        #region Tribus
        public ObservableCollection<Tbl57Tribus> CollTribussesByTribusId(int id)
        {
            var collection = new ObservableCollection<Tbl57Tribus>(_uow.Tbl57Tribusses
                .Find(e => e.TribusId == id));
            return collection;
        }
        public ObservableCollection<Tbl60Subtribus> CollSubtribussesByTribusIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl60Subtribus>(_uow.Tbl60Subtribusses
                .Find(e => e.TribusId == id &&
                           e.SubtribusName.Contains("#") == false));
            return collection;
        }
        //Function
        public int SupertribusIdFromTribussesCollectionSelect(int id)
        {
            var dbset = _context.Tbl57Tribusses
                .SingleOrDefault(p => p.TribusId == id);

            if (dbset != null) return dbset.SupertribusId;
            return 0;
        }
        // ForeignKey
        public ObservableCollection<Tbl54Supertribus> CollSupertribussesBySupertribusIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl54Supertribus>(_uow.Tbl54Supertribusses
                .Find(e => e.SupertribusId == id &&
                           e.SupertribusName.Contains("#") == false));
            return collection;
        }
        //Reference
        public ObservableCollection<Tbl90Reference> CollExpertsByTribusId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.TribusId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesByTribusId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.TribusId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsByTribusId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.TribusId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsByTribusId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.TribusId == id));
            return collection;
        }
        #endregion

        #region Subtribus
        public ObservableCollection<Tbl60Subtribus> CollSubtribussesBySubtribusId(int id)
        {
            var collection = new ObservableCollection<Tbl60Subtribus>(_uow.Tbl60Subtribusses
                .Find(e => e.SubtribusId == id));
            return collection;
        }
        public ObservableCollection<Tbl63Infratribus> CollInfratribussesBySubtribusIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl63Infratribus>(_uow.Tbl63Infratribusses
                .Find(e => e.SubtribusId == id &&
                           e.InfratribusName.Contains("#") == false));
            return collection;
        }
        //Function
        public int TribusIdFromSubtribussesCollectionSelect(int id)
        {
            var dbset = _context.Tbl60Subtribusses
                .SingleOrDefault(p => p.SubtribusId == id);

            if (dbset != null) return dbset.TribusId;
            return 0;
        }
        // ForeignKey
        public ObservableCollection<Tbl57Tribus> CollTribussesByTribusIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl57Tribus>(_uow.Tbl57Tribusses
                .Find(e => e.TribusId == id &&
                           e.TribusName.Contains("#") == false));
            return collection;
        }
        //Reference
        public ObservableCollection<Tbl90Reference> CollExpertsBySubtribusId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.SubtribusId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesBySubtribusId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.SubtribusId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsBySubtribusId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.SubtribusId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsBySubtribusId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.SubtribusId == id));
            return collection;
        }
        #endregion

        #region Infratribus
        public ObservableCollection<Tbl63Infratribus> CollInfratribussesByInfratribusId(int id)
        {
            var collection = new ObservableCollection<Tbl63Infratribus>(_uow.Tbl63Infratribusses
                .Find(e => e.InfratribusId == id));
            return collection;
        }
        public ObservableCollection<Tbl66Genus> CollGenussesByInfratribusIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl66Genus>(_uow.Tbl66Genusses
                .Find(e => e.InfratribusId == id &&
                           e.GenusName.Contains("#") == false));
            return collection;
        }
        //Function
        public int SubtribusIdFromInfratribussesCollectionSelect(int id)
        {
            var dbset = _context.Tbl63Infratribusses
                .SingleOrDefault(p => p.InfratribusId == id);

            if (dbset != null) return dbset.SubtribusId;
            return 0;
        }
        // ForeignKey
        public ObservableCollection<Tbl60Subtribus> CollSubtribussesBySubtribusIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl60Subtribus>(_uow.Tbl60Subtribusses
                .Find(e => e.SubtribusId == id &&
                           e.SubtribusName.Contains("#") == false));
            return collection;
        }
        //Reference
        public ObservableCollection<Tbl90Reference> CollExpertsByInfratribusId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.InfratribusId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesByInfratribusId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.InfratribusId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsByInfratribusId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.InfratribusId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsByInfratribusId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.InfratribusId == id));
            return collection;
        }
        #endregion

        #region Genus
        public ObservableCollection<Tbl66Genus> CollGenussesByGenusId(int id)
        {
            var collection = new ObservableCollection<Tbl66Genus>(_uow.Tbl66Genusses
                .Find(e => e.GenusId == id));
            return collection;
        }
        public ObservableCollection<Tbl69FiSpecies> CollFiSpeciessesByGenusIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl69FiSpecies>(_uow.Tbl69FiSpeciesses
                .Find(e => e.GenusId == id &&
                           e.FiSpeciesName.Contains("#") == false));
            return collection;
        }
        public ObservableCollection<Tbl72PlSpecies> CollPlSpeciessesByGenusIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl72PlSpecies>(_uow.Tbl72PlSpeciesses
                .Find(e => e.GenusId == id &&
                           e.PlSpeciesName.Contains("#") == false));
            return collection;
        }
        //Function
        public int InfratribusIdFromGenussesCollectionSelect(int id)
        {
            var dbset = _context.Tbl66Genusses
                .SingleOrDefault(p => p.GenusId == id);

            if (dbset != null) return dbset.InfratribusId;
            return 0;
        }
        // ForeignKey
        public ObservableCollection<Tbl63Infratribus> CollInfratribussesByInfratribusIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl63Infratribus>(_uow.Tbl63Infratribusses
                .Find(e => e.InfratribusId == id &&
                           e.InfratribusName.Contains("#") == false));
            return collection;
        }
        //Reference
        public ObservableCollection<Tbl90Reference> CollExpertsByGenusId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.GenusId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesByGenusId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.GenusId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsByGenusId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.GenusId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsByGenusId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.GenusId == id));
            return collection;
        }
        #endregion

        #region FiSpecies
        public Tbl69FiSpecies GetFiSpeciesSingleByFiSpeciesId(int id)
        {
            var single = _uow.Tbl69FiSpeciesses.GetById(id);
            //    Tbl69FiSpecies single = _context.Tbl69FiSpeciesses.FirstOrDefault(a => a.FiSpeciesId == id);
            return single;
        }
        public ObservableCollection<Tbl69FiSpecies> CollFiSpeciessesByFiSpeciesId(int id)
        {
            var collection = new ObservableCollection<Tbl69FiSpecies>(_uow.Tbl69FiSpeciesses
                .Find(e => e.FiSpeciesId == id));
            return collection;
        }
        public ObservableCollection<Tbl69FiSpecies> CollFiSpeciessesByFiSpeciesNameAndNotEmptySubspeciesAndHash(string name)
        {
            var collection = new ObservableCollection<Tbl69FiSpecies>(_context.Tbl69FiSpeciesses
                .Include(d => d.Tbl66Genusses)
                .Where(e => e.FiSpeciesName == name &&
                           e.FiSpeciesName.Contains("#") == false &&
                           e.Subspecies != null)
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.FiSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers));
            return collection;
        }
        public ObservableCollection<Tbl69FiSpecies> CollFiSpeciessesByFiSpeciesNameAndSubspeciesAndDivers(string fiSpeciesName, string subspecies,
            string divers)
        {
            var collection = new ObservableCollection<Tbl69FiSpecies>(_context.Tbl69FiSpeciesses
                .Include(d => d.Tbl66Genusses)
                .Where(e => e.FiSpeciesName == fiSpeciesName && e.Subspecies == subspecies && e.Divers == divers)
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.FiSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers));
            return collection;
        }
        public ObservableCollection<Tbl78Name> CollNamesByFiSpeciesIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl78Name>(_context.Tbl78Names
                .Where(e => e.FiSpeciesId == id &&
                            e.NameName.Contains("#") == false)
                .OrderBy(r => r.NameName));
            return collection;
        }
        public ObservableCollection<Tbl84Synonym> CollSynonymsByFiSpeciesIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl84Synonym>(_context.Tbl84Synonyms
                .Where(e => e.FiSpeciesId == id &&
                            e.SynonymName.Contains("#") == false)
                .OrderBy(r => r.SynonymName));
            return collection;
        }
        //Function
        public int GenusIdFromFiSpeciessesCollectionSelect(int id)
        {
            var dbset = _context.Tbl69FiSpeciesses
                .SingleOrDefault(p => p.FiSpeciesId == id);

            if (dbset != null) return dbset.GenusId;
            return 0;
        }
        public ObservableCollection<Tbl66Genus> CollGenussesByGenusIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl66Genus>(_uow.Tbl66Genusses
                .Find(e => e.GenusId == id &&
                           e.GenusName.Contains("#") == false));
            return collection;
        }
        //Reference
        public ObservableCollection<Tbl90Reference> CollExpertsByFiSpeciesId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.FiSpeciesId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesByFiSpeciesId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.FiSpeciesId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsByFiSpeciesId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.FiSpeciesId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsByFiSpeciesId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.FiSpeciesId == id));
            return collection;
        }
        //Images
        public ObservableCollection<Tbl81Image> CollImagesByFiSpeciesId(int id)
        {
            var collection = new ObservableCollection<Tbl81Image>(_uow.Tbl81Images
                .Find(e => e.FiSpeciesId == id));
            return collection;
        }
        //Geographics
        public ObservableCollection<Tbl87Geographic> CollGeographicsByFiSpeciesId(int id)
        {
            var collection = new ObservableCollection<Tbl87Geographic>(_uow.Tbl87Geographics
                .Find(e => e.FiSpeciesId == id));
            return collection;
        }
        #endregion

        #region PlSpecies
        public Tbl72PlSpecies GetPlSpeciesSingleByPlSpeciesId(int id)
        {
            var single = _uow.Tbl72PlSpeciesses.GetById(id);
            //    Tbl72PlSpecies single = _context.Tbl72PlSpeciesses.FirstOrDefault(a => a.PlSpeciesId == id);
            return single;
        }
        public ObservableCollection<Tbl72PlSpecies> CollPlSpeciessesByPlSpeciesId(int id)
        {
            var collection = new ObservableCollection<Tbl72PlSpecies>(_uow.Tbl72PlSpeciesses
                .Find(e => e.PlSpeciesId == id));
            return collection;
        }
        public ObservableCollection<Tbl72PlSpecies> CollPlSpeciessesByPlSpeciesNameAndNotEmptySubspeciesAndHash(string name)
        {
            var collection = new ObservableCollection<Tbl72PlSpecies>(_context.Tbl72PlSpeciesses
                .Include(d => d.Tbl66Genusses)
                .Where(e => e.PlSpeciesName == name &&
                            e.PlSpeciesName.Contains("#") == false &&
                            e.Subspecies != null)
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.PlSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers));
            return collection;
        }
        public ObservableCollection<Tbl72PlSpecies> CollPlSpeciessesByPlSpeciesNameAndSubspeciesAndDivers(string fiSpeciesName, string subspecies,
            string divers)
        {
            var collection = new ObservableCollection<Tbl72PlSpecies>(_context.Tbl72PlSpeciesses
                .Include(d => d.Tbl66Genusses)
                .Where(e => e.PlSpeciesName == fiSpeciesName && e.Subspecies == subspecies && e.Divers == divers)
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.PlSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers));
            return collection;
        }
        public ObservableCollection<Tbl78Name> CollNamesByPlSpeciesIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl78Name>(_context.Tbl78Names
                .Where(e => e.PlSpeciesId == id &&
                            e.NameName.Contains("#") == false)
                .OrderBy(r => r.NameName));
            return collection;
        }
        public ObservableCollection<Tbl84Synonym> CollSynonymsByPlSpeciesIdAndHash(int id)
        {
            var collection = new ObservableCollection<Tbl84Synonym>(_context.Tbl84Synonyms
                .Where(e => e.PlSpeciesId == id &&
                            e.SynonymName.Contains("#") == false)
                .OrderBy(r => r.SynonymName));
            return collection;
        }
        //Function
        public int GenusIdFromPlSpeciessesCollectionSelect(int id)
        {
            var dbset = _context.Tbl72PlSpeciesses
                .SingleOrDefault(p => p.PlSpeciesId == id);

            if (dbset != null) return dbset.GenusId;
            return 0;
        }
        //public ObservableCollection<Tbl66Genus> CollGenussesByGenusIdAndHash(int id)
        //{
        //    var collection = new ObservableCollection<Tbl66Genus>(_uow.Tbl66Genusses
        //        .Find(e => e.GenusId == id &&
        //                   e.GenusName.Contains("#") == false));
        //    return collection;
        //}
        //Reference
        public ObservableCollection<Tbl90Reference> CollExpertsByPlSpeciesId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.PlSpeciesId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollSourcesByPlSpeciesId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.PlSpeciesId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> CollAuthorsByPlSpeciesId(int id)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.PlSpeciesId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        // Comments
        public ObservableCollection<Tbl93Comment> CollCommentsByPlSpeciesId(int id)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(e => e.PlSpeciesId == id));
            return collection;
        }
        //Images
        public ObservableCollection<Tbl81Image> CollImagesByPlSpeciesId(int id)
        {
            var collection = new ObservableCollection<Tbl81Image>(_uow.Tbl81Images
                .Find(e => e.PlSpeciesId == id));
            return collection;
        }
        //Geographics
        public ObservableCollection<Tbl87Geographic> CollGeographicsByPlSpeciesId(int id)
        {
            var collection = new ObservableCollection<Tbl87Geographic>(_uow.Tbl87Geographics
                .Find(e => e.PlSpeciesId == id));
            return collection;
        }
        #endregion
    }
}
