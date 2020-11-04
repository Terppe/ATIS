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
                            collection = RegnumAllCollection<T>();
                            break;
                        case "phylum":
                            collection = PhylumAllCollection<T>();
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
                        case "expert":
                            collection = ReferenceExpertAllCollection<T>();
                            break;
                        case "source":
                            collection = ReferenceSourceAllCollection<T>();
                            break;
                        case "author":
                            collection = ReferenceAuthorAllCollection<T>();
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
                    collection = RegnumAllCollection<T>();
                    break;
                case "phylum":
                    collection = PhylumAllCollection<T>();
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
                case "expert": 
                    collection = ReferenceExpertAllCollection<T>();
                    break;
                case "source":
                    collection = ReferenceSourceAllCollection<T>();
                    break;
                case "author":
                    collection = ReferenceAuthorAllCollection<T>();
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

        //-----------------------Regnum---------------------------------
        private ObservableCollection<T> RegnumAllCollection<T>()
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
        public ObservableCollection<T> GetRegnumsCollectionOrderByFromRegnumId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl03Regnums
                .Where(e => e.RegnumId == id)
                .OrderBy(k => k.RegnumName)
                .ThenBy(k => k.Subregnum));

             return collection;
        }

        
        //-----------------------Phylum---------------------------------
        private ObservableCollection<T> PhylumAllCollection<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl06Phylums
                .OrderBy(a=>a.PhylumName));
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

        //-----------------------Division--------------------------------
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

        //-----------------------Subphylum--------------------------------
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
                : new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl12Subphylums.Find(e=>e.SubphylumName.StartsWith(searchName)));
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

        //-----------------------Subdivision--------------------------------
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

        //-----------------------Superclass--------------------------------
        public ObservableCollection<T> GetSuperclassesCollectionOrderByFromSuperclassId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl18Superclasses
                .Where(e => e.SuperclassId == id)
                .OrderBy(k => k.SuperclassName));

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



        //-----------------------Reference--------------------------------
        private ObservableCollection<T> ReferenceAllCollection<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl90References.GetAll());
            return collection;
        }
        private ObservableCollection<T> ReferenceExpertAllCollection<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90RefExperts
                .OrderBy(a=>a.RefExpertName));
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

        public ObservableCollection<T> GetReferenceExpertsCollectionOrderByFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RegnumId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionOrderByFromRegnumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RegnumId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionOrderByFromRegnumIdAndRefSourceIdIsNullAndRefExpertIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RegnumId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }

        //--------------------------------------------------------

        private ObservableCollection<T> ReferenceSourceAllCollection<T>()
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


        //--------------------------------------------------------
        private ObservableCollection<T> ReferenceAuthorAllCollection<T>()
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


        //----------------------------------------------------------
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

        //-----------------------------------------------------------
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


        
        //------------------------Comment-------------------------------
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
        public ObservableCollection<T> GetCommentsCollectionOrderByFromRegnumId<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.RegnumId == id)
                .OrderBy(e => e.Info));
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
        public ObservableCollection<T> GetCommentsCollectionOrderByFromSubphylumId<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.SubphylumId == id)
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
        public ObservableCollection<T> GetCommentsCollectionOrderByFromSubdivisionId<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.SubdivisionId == id)
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


    }
}
