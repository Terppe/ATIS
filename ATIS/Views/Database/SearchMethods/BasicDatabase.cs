using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ATIS.Dal.Models;
using ATIS.Ui.Core;

namespace ATIS.Ui.Views.Database.SearchMethods
{
    public class BasicDatabase
    {
        private readonly AtisDbContext _context = new AtisDbContext();
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());

        internal ObservableCollection<T> SearchNameAndIdReturnCollection<T>(string searchName, string name)
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
                        case "expert":
                        case "source":
                        case "author":
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

        //-----------------------Regnum---------------------------------
        private ObservableCollection<T> RegnumAllCollection<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl03Regnums.GetAll());
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
        //-----------------------Phylum---------------------------------
        private ObservableCollection<T> PhylumAllCollection<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl06Phylums.GetAll());
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
        //-----------------------Division--------------------------------
        //-----------------------Subphylum--------------------------------
        //-----------------------Subdivision--------------------------------

        //-----------------------Reference--------------------------------
        private ObservableCollection<T> ReferenceAllCollection<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_uow.Tbl90References.GetAll());
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
        //--------------------------------------------------------
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
        //--------------------------------------------------------
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
        //--------------------------------------------------------
    }
}
