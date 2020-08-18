using System.Collections.ObjectModel;
using System.Linq;
using ATIS.Dal.Models;
using ATIS.Ui.Core;

namespace ATIS.Ui.Views.Database.SearchMethods
{
    public class BasicDatabase
    {
        private readonly AtisDbContext _context = new AtisDbContext();
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());

        public ObservableCollection<Tbl03Regnum> SearchNameReturnRegnumsCollection(string searchName)
        {
            var collection = new ObservableCollection<Tbl03Regnum>();

            switch (searchName)
            {
                case "":
                    return collection;
                case "*":
                    collection = new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums.GetAll());
                    break;
                default:
                    collection = int.TryParse(searchName, out var id)
                        ? new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums
                            .Find(e => e.RegnumId == id))
                        : new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums
                            .Find(e => e.RegnumName.StartsWith(searchName))
                            .OrderBy(a => a.RegnumName)
                            .ThenBy(a => a.Subregnum)
                        );
                    break;
            }

            return collection;
        }

        public ObservableCollection<Tbl06Phylum> SearchNameReturnPhylumsCollection(string searchName)
        {
            var collection = new ObservableCollection<Tbl06Phylum>();

            switch (searchName)
            {
                case "":
                    return collection;
                case "*":
                    collection = new ObservableCollection<Tbl06Phylum>(_uow.Tbl06Phylums.GetAll());
                    break;
                default:
                    collection = int.TryParse(searchName, out var id)
                        ? new ObservableCollection<Tbl06Phylum>(_uow.Tbl06Phylums
                            .Find(e => e.PhylumId == id))
                        : new ObservableCollection<Tbl06Phylum>(_uow.Tbl06Phylums.ListTbl06PhylumsOnlyAnimaliaOrderBy(searchName)
                        //.Find(e => e.PhylumName.StartsWith(searchName))
                        //.OrderBy(a => a.PhylumName)
                        );
                    break;
            }

            return collection;
        }




        public ObservableCollection<Tbl90Reference> SearchNameReturnReferenceCollection(string searchName, string name)
        {
            var collection = new ObservableCollection<Tbl90Reference>();

            switch (searchName)
            {
                case "":
                    return collection;
                case "*":
                    collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.GetAll());
                    break;
                default:
                    collection = ReferenceCollection(searchName, name);
                    break;
            }

            return collection;
        }

        private ObservableCollection<Tbl90Reference> ReferenceCollection(string searchName, string name)
        {
            ObservableCollection<Tbl90Reference> collection;
            switch (name)
            {
                case "expert":
                    {
                        collection = int.TryParse(searchName, out var id)
                            ? new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                                .Find(e => e.ReferenceId == id && e.RefAuthorId == null && e.RefSourceId == null))
                            : new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                                .Find(e => e.Info.StartsWith(searchName))
                            );
                        return collection;
                    }
                case "source":
                    {
                        collection = int.TryParse(searchName, out var id)
                            ? new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                                .Find(e => e.ReferenceId == id && e.RefAuthorId == null && e.RefExpertId == null))
                            : new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                                .Find(e => e.Info.StartsWith(searchName))
                            );
                        return collection;
                    }
                case "author":
                    {
                        collection = int.TryParse(searchName, out var id)
                            ? new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                                .Find(e => e.ReferenceId == id && e.RefSourceId == null && e.RefExpertId == null))
                            : new ObservableCollection<Tbl90Reference>(_uow.Tbl90References
                                .Find(e => e.Info.StartsWith(searchName))
                            );
                        return collection;
                    }
                default:
                    return null;
            }
        }

        public ObservableCollection<Tbl93Comment> SearchNameReturnCommentsCollection(string searchName)
        {
            var collection = new ObservableCollection<Tbl93Comment>();

            switch (searchName)
            {
                case "":
                    return collection;
                case "*":
                    collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.GetAll());
                    break;
                default:
                    collection = int.TryParse(searchName, out var id)
                        ? new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                            .Find(e => e.CommentId == id))
                        : new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                            .Find(e => e.Info.StartsWith(searchName))
                        );
                    break;
            }

            return collection;
        }

    }
}
