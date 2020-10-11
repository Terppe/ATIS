using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using Microsoft.EntityFrameworkCore;

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
            var regnumIdFromPhylumsColl = _context.Tbl06Phylums
                .SingleOrDefault(p => p.PhylumId == id);

            if (regnumIdFromPhylumsColl == null) return 0;
            return regnumIdFromPhylumsColl.RegnumId;
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
            var regnumIdFromDivisionsColl = _context.Tbl09Divisions
                .SingleOrDefault(p => p.DivisionId == id);

            if (regnumIdFromDivisionsColl == null) return 0;
            return regnumIdFromDivisionsColl.RegnumId;
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
        //-----------------------Subphylum---------------------------------
        //--------------------------------------------------------------

        //-----------------------Subdivision---------------------------------
        //--------------------------------------------------------------

        //-----------------------Supoerclass---------------------------------
        //--------------------------------------------------------------

        //-----------------------Class---------------------------------
        //--------------------------------------------------------------


    }
}
