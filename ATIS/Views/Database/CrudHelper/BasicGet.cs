using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ATIS.Ui.Views.Database.CrudHelper
{
    public class BasicGet : ViewModelBase
    {
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();

        public ObservableCollection<T> SearchNameAndIdReturnCollection<T>(string searchName, string name)
        {
            var collection = new ObservableCollection<T>();

            switch (searchName)
            {
                case "":
                    return collection;
                case "*":
                    switch (name)
                    {
                        case "regnum":
                            collection = GetRegnumCollectionAllOrderBy<T>();
                            break;
                        case "phylum":
                            collection = GetPhylumCollectionAllOrderBy<T>();
                            break;
                        case "division":
                            collection = DivisionAllCollection<T>();
                            break;
                        case "subphylum":
                            collection = SubphylumAllCollection<T>();
                            break;
                        case "subdivision":
                            collection = SubdivisionAllCollection<T>();
                            break;
                        case "superclass":
                            collection = GetSuperclassCollectionAllOrderBy<T>();
                            break;
                        case "classe":
                            collection = GetClassesCollectionAllOrderBy<T>();
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
                        case "reference":
                            collection = ReferenceAllCollection<T>();
                            break;
                        case "comment":
                            collection = CommentAllCollection<T>();
                            break;
                    }
                    break;
                default:
                    switch (name)
                    {
                        case "regnum":
                            collection = RegnumNameIdCollection<T>(searchName);
                            break;
                        case "phylum":
                            collection = PhylumNameIdCollection<T>(searchName);
                            break;
                        case "subphylum":
                            collection = SubphylumNameIdCollection<T>(searchName);
                            break;
                        case "division":
                            collection = DivisionNameIdCollection<T>(searchName);
                            break;
                        case "subdivision":
                            collection = SubdivisionNameIdCollection<T>(searchName);
                            break;
                        case "superclass":
                            collection = GetSuperclassesCollectionOrderByFromSuperclassNameStartsWithOrSuperclassId<T>(searchName);
                            break;
                        case "classe":
                            collection = GetClassesCollectionOrderByFromClassNameStartsWithOrClassId<T>(searchName);
                            break;
                        case "expert":
                            collection = ReferenceExpertNameIdCollection<T>(searchName);
                            break;
                        case "source":
                            collection = ReferenceSourceNameIdCollection<T>(searchName);
                            break;
                        case "author":
                            collection = ReferenceAuthorNameIdCollection<T>(searchName);
                            break;
                        case "comment":
                            collection = CommentNameIdCollection<T>(searchName);
                            break;
                    }
                    break;
            }
            return collection;
        }

        public ObservableCollection<T> AllCollection<T>(string name)
        {
            var collection = new ObservableCollection<T>();

            switch (name)
            {
                case "regnum":
                    collection = GetRegnumCollectionAllOrderBy<T>();
                    break;
                case "phylum":
                    collection = GetPhylumCollectionAllOrderBy<T>();
                    break;
                case "subphylum":
                    collection = SubphylumAllCollection<T>();
                    break;
                case "division":
                    collection = DivisionAllCollection<T>();
                    break;
                case "subdivision":
                    collection = SubdivisionAllCollection<T>();
                    break;
                case "superclass":
                    collection = GetSuperclassCollectionAllOrderBy<T>();
                    break;
                case "classe":
                    collection = GetClassesCollectionAllOrderBy<T>();
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
                case "reference":
                    collection = ReferenceAllCollection<T>();
                    break;
                case "comment":
                    collection = CommentAllCollection<T>();
                    break;
            }
            return collection;
        }

        #region Regnum

        //--------------------------------Regnum -------------------------
        private ObservableCollection<T> GetRegnumCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl03Regnums
                .OrderBy(a => a.RegnumName)
                .ThenBy(a => a.Subregnum));
            return collection;
        }
        private ObservableCollection<T> RegnumNameIdCollection<T>(string searchName)
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
        public ObservableCollection<T> GetRegnumsCollectionFromRegnumIdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl03Regnums
                .Where(e => e.RegnumId == id)
                .OrderBy(k => k.RegnumName)
                .ThenBy(k => k.Subregnum));

            return collection;
        }


        public ObservableCollection<T> GetReferenceExpertsCollectionFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RegnumId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionFromRegnumIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RegnumId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionFromRegnumIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RegnumId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionFromRegnumIdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.RegnumId == id)
                .OrderBy(e => e.Info));
            return collection;
        }
        #endregion

        #region Phylum

        //--------------------------------Phylum------------------------
        private ObservableCollection<T> GetPhylumCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl06Phylums
                .OrderBy(a => a.PhylumName));
            return collection;
        }
        private ObservableCollection<T> PhylumNameIdCollection<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl06Phylums
                    .Find(e => e.PhylumId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl06Phylums.ListTbl06PhylumsOnlyAnimaliaOrderBy(searchName));
            return collection;
        }
        public ObservableCollection<T> GetPhylumsCollectionOrderByFromPhylumId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl06Phylums
                .Where(e => e.PhylumId == id)
                .OrderBy(k => k.PhylumName));

            return collection;
        }
        public ObservableCollection<T> GetPhylumsCollectionOrderByFromRegnumId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl06Phylums
                .Where(e => e.RegnumId == id)
                .OrderBy(k => k.PhylumName));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionOrderByFromPhylumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.PhylumId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionOrderByFromPhylumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.PhylumId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionOrderByFromPhylumIdAndRefSourceIdIsNullAndRefExpertIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.PhylumId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionOrderByFromPhylumId<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.PhylumId == id)
                .OrderBy(k => k.Info));
            return collection;
        }
        #endregion

        #region Subphylum
        //----------------------------------Subphylum----------------------
        private ObservableCollection<T> SubphylumAllCollection<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl12Subphylums
                .OrderBy(a => a.SubphylumName));
            return collection;
        }
        private ObservableCollection<T> SubphylumNameIdCollection<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl12Subphylums
                    .Find(e => e.SubphylumId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl12Subphylums.Find(e => e.SubphylumName.StartsWith(searchName)));
            return collection;
        }
        public ObservableCollection<T> GetSubphylumsCollectionOrderByFromSubphylumId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl12Subphylums
                .Where(e => e.SubphylumId == id)
                .OrderBy(k => k.SubphylumName));

            return collection;
        }
 
        public ObservableCollection<T> GetSubphylumsCollectionOrderByFromPhylumId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl12Subphylums
                .Where(e => e.PhylumId == id)
                .OrderBy(k => k.SubphylumName));
            return collection;
        }

        public ObservableCollection<T> GetReferenceExpertsCollectionOrderByFromSubphylumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.SubphylumId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionOrderByFromSubphylumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.SubphylumId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionOrderByFromSubphylumIdAndRefSourceIdIsNullAndRefExpertIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.SubphylumId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionOrderByFromSubphylumId<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.SubphylumId == id)
                .OrderBy(k => k.Info));
            return collection;
        }
        #endregion

        #region Division

        //---------------------------------Division-------------------------
        private ObservableCollection<T> DivisionAllCollection<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl09Divisions
                .OrderBy(a => a.DivisionName));
            return collection;
        }
        private ObservableCollection<T> DivisionNameIdCollection<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl09Divisions
                    .Find(e => e.DivisionId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl09Divisions.ListTbl09DivisionsOnlyPlantaeOrderBy(searchName));
            return collection;
        }
        public ObservableCollection<T> GetDivisionsCollectionOrderByFromDivisionId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl09Divisions
                .Where(e => e.DivisionId == id)
                .OrderBy(k => k.DivisionName));

            return collection;
        }
        public ObservableCollection<T> GetDivisionsCollectionOrderByFromRegnumId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl09Divisions
                .Where(e => e.RegnumId == id)
                .OrderBy(k => k.DivisionName));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionOrderByFromDivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.DivisionId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionOrderByFromDivisionIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.DivisionId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionOrderByFromDivisionIdAndRefSourceIdIsNullAndRefExpertIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.DivisionId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionOrderByFromDivisionId<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.DivisionId == id)
                .OrderBy(k => k.Info));
            return collection;
        }
        #endregion

        #region Subdivision

        //----------------------------------Subdivision-------------------------
        private ObservableCollection<T> SubdivisionAllCollection<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl15Subdivisions
                .OrderBy(a => a.SubdivisionName));
            return collection;
        }
        private ObservableCollection<T> SubdivisionNameIdCollection<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl15Subdivisions
                    .Find(e => e.SubdivisionId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl15Subdivisions.Find(e => e.SubdivisionName.StartsWith(searchName)));
            return collection;
        }
        public ObservableCollection<T> GetSubdivisionsCollectionOrderByFromSubdivisionId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl15Subdivisions
                .Where(e => e.SubdivisionId == id)
                .OrderBy(k => k.SubdivisionName));

            return collection;
        }
        public ObservableCollection<T> GetSubdivisionsCollectionOrderByFromDivisionId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl15Subdivisions
                .Where(e => e.DivisionId == id)
                .OrderBy(k => k.SubdivisionName));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionOrderByFromSubdivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.SubdivisionId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionOrderByFromSubdivisionIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.SubdivisionId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionOrderByFromSubdivisionIdAndRefSourceIdIsNullAndRefExpertIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.SubdivisionId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionOrderByFromSubdivisionId<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.SubdivisionId == id)
                .OrderBy(k => k.Info));
            return collection;
        }
        #endregion

        #region Superclass

        //----------------------------------Superclass----------------------------
        private ObservableCollection<T> GetSuperclassCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl18Superclasses
                .OrderBy(a => a.SuperclassName));
            return collection;
        }
        private ObservableCollection<T> GetSuperclassesCollectionOrderByFromSuperclassNameStartsWithOrSuperclassId<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl18Superclasses
                    .Find(e => e.SuperclassId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl18Superclasses.Find(e => e.SuperclassName.StartsWith(searchName)));
            return collection;
        }
        
        public ObservableCollection<T> GetSuperclassesCollectionOrderByFromSubdivisionId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl18Superclasses
                .Where(e => e.SubdivisionId == id)
                .OrderBy(k => k.SuperclassName));
            return collection;
        }
 
        public ObservableCollection<T> GetSuperclassesCollectionOrderByFromSubphylumId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl18Superclasses
                .Where(e => e.SubphylumId == id)
                .OrderBy(k => k.SuperclassName));
            return collection;
        }
        public ObservableCollection<T> GetSuperclassesCollectionOrderByFromSuperclassId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl18Superclasses
                .Where(e => e.SuperclassId == id)
                .OrderBy(k => k.SuperclassName));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionOrderByFromSuperclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.SuperclassId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionOrderByFromSuperclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.SuperclassId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionOrderByFromSuperclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.SuperclassId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionOrderByFromSuperclassId<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.SuperclassId == id)
                .OrderBy(k => k.Info));
            return collection;
        }
        #endregion

        #region Class

        //------------------------------------ Class  -------------------------
        private ObservableCollection<T> GetClassesCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl21Classes
                .OrderBy(a => a.ClassName));
            return collection;
        }

        private ObservableCollection<T> GetClassesCollectionOrderByFromClassNameStartsWithOrClassId<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl21Classes
                    .Find(e => e.ClassId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl21Classes.Find(e => e.ClassName.StartsWith(searchName)));
            return collection;
        }
        public ObservableCollection<T> GetClassesCollectionOrderByFromClassId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl21Classes
                .Where(e => e.ClassId == id)
                .OrderBy(k => k.ClassName));
            return collection;
        }

        public ObservableCollection<T> GetClassesCollectionOrderByFromSuperclassId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl21Classes
                .Where(e => e.SuperclassId == id)
                .OrderBy(k => k.ClassName));

            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionOrderByFromClassIdAndRefSourceIdIsNullAndRefExpertIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.ClassId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionOrderByFromClassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.ClassId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionOrderByFromClassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.ClassId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionOrderByFromClassId<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.ClassId == id)
                .OrderBy(k => k.Info));
            return collection;
        }
        #endregion

        #region Subclass

        //----------------------------------Subclass------------------------------
        public ObservableCollection<T> GetSubclassesCollectionOrderByFromSubclassId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl24Subclasses
                .Where(e => e.SubclassId == id)
                .OrderBy(k => k.SubclassName));
            return collection;
        }

        public ObservableCollection<T> GetSubclassesCollectionOrderByFromClassId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl24Subclasses
                .Where(e => e.ClassId == id)
                .OrderBy(k => k.SubclassName));
            return collection;
        }

        private ObservableCollection<T> GetSubclassesCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl24Subclasses
                .OrderBy(a => a.SubclassName));
            return collection;
        }
        private ObservableCollection<T> GetSubclassesCollectionOrderByFromSubclassNameStartsWithOrSubclassId<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl24Subclasses
                    .Find(e => e.SubclassId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl24Subclasses.Find(e => e.SubclassName.StartsWith(searchName)));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionOrderByFromSubclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.SubclassId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionOrderByFromSubclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.SubclassId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionOrderByFromSubclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.SubclassId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionOrderByFromSubclassId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.SubclassId == id)
                .OrderBy(k => k.Info));
            return collection;
        }


        #endregion

        #region Infraclass

        private ObservableCollection<T> GetInfraclassesCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl27Infraclasses
                .OrderBy(a => a.InfraclassName));
            return collection;
        }

        public ObservableCollection<T> GetInfraclassesCollectionOrderByFromSubclassId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl27Infraclasses
                .Where(e => e.SubclassId == id)
                .OrderBy(k => k.InfraclassName));
            return collection;
        }

        public ObservableCollection<T> GetInfraclassesCollectionOrderByFromSubphylumId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl27Infraclasses
                .Where(e => e.SubclassId == id)
                .OrderBy(k => k.InfraclassName));
            return collection;
        }
        private ObservableCollection<T> GetInfraclassesCollectionOrderByFromInfraclassNameStartsWithOrInfraclassId<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl27Infraclasses
                    .Find(e => e.InfraclassId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl27Infraclasses.Find(e => e.InfraclassName.StartsWith(searchName)));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionOrderByFromInfraclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.InfraclassId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionOrderByFromInfraclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.InfraclassId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionOrderByFromInfraclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.InfraclassId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionOrderByFromInfraclassId<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.InfraclassId == id)
                .OrderBy(k => k.Info));
            return collection;
        }



        #endregion

        #region Legio

        public ObservableCollection<T> GetLegiosCollectionOrderByFromInfraclassId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl30Legios
                .Where(e => e.InfraclassId == id)
                .OrderBy(k => k.LegioName));
            return collection;
        }


        #endregion
        //-----------------------Reference--------------------------------
        private ObservableCollection<T> ReferenceAllCollection<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl90References.GetAll());
            return collection;
        }
        private ObservableCollection<T> GetReferenceExpertsCollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90RefExperts
                .OrderBy(a => a.RefExpertName));
            return collection;
        }
        private ObservableCollection<T> ReferenceExpertNameIdCollection<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl90References
                    .Find(e => e.ReferenceId == id && e.RefAuthorId == null && e.RefSourceId == null))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl90References
                    .Find(e => e.Info.StartsWith(searchName))
                );
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
        private ObservableCollection<T> ReferenceAuthorNameIdCollection<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl90References
                    .Find(e => e.ReferenceId == id && e.RefSourceId == null && e.RefExpertId == null))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl90References
                    .Find(e => e.Info.StartsWith(searchName))
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
        private ObservableCollection<T> ReferenceSourceNameIdCollection<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl90References
                    .Find(e => e.ReferenceId == id && e.RefAuthorId == null && e.RefExpertId == null))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl90References
                    .Find(e => e.Info.StartsWith(searchName))
                );
            return collection;
        }

        //------------------------ Comment -------------------------------
        private ObservableCollection<T> CommentAllCollection<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl93Comments.GetAll());
            return collection;
        }
        private ObservableCollection<T> CommentNameIdCollection<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl93Comments
                    .Find(e => e.CommentId == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl93Comments
                    .Find(e => e.Info.StartsWith(searchName))
                );
            return collection;
        }

    }
}
