using System.Collections.ObjectModel;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;

namespace ATIS.Ui.Views.Database.SearchMethods
{
    public class BasicCopy : ViewModelBase
    {
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());

        public ObservableCollection<Tbl03Regnum> CopyRegnum(Tbl03Regnum selected)
        {
            var regnum = _uow.Tbl03Regnums.GetById(selected.RegnumId);
            var collection = new ObservableCollection<Tbl03Regnum>();

            collection.Insert(0, new Tbl03Regnum
            {
                RegnumName = CultRes.StringsRes.DatasetNew,
                Subregnum = regnum.Subregnum,
                Valid = regnum.Valid,
                ValidYear = regnum.ValidYear,
                Synonym = regnum.Synonym,
                Author = regnum.Author,
                AuthorYear = regnum.AuthorYear,
                Info = regnum.Info,
                EngName = regnum.EngName,
                GerName = regnum.GerName,
                FraName = regnum.FraName,
                PorName = regnum.PorName,
                Memo = regnum.Memo
            });

            return collection;
        }
        //----------------------------------------------------------
        public ObservableCollection<Tbl06Phylum> CopyPhylum(Tbl06Phylum selected)
        {
            var phylum = _uow.Tbl06Phylums.GetById(selected.PhylumId);
            var collection = new ObservableCollection<Tbl06Phylum>();

            collection.Insert(0, new Tbl06Phylum
            {
                PhylumName = CultRes.StringsRes.DatasetNew,
                Valid = phylum.Valid,
                ValidYear = phylum.ValidYear,
                Synonym = phylum.Synonym,
                Author = phylum.Author,
                AuthorYear = phylum.AuthorYear,
                Info = phylum.Info,
                EngName = phylum.EngName,
                GerName = phylum.GerName,
                FraName = phylum.FraName,
                PorName = phylum.PorName,
                Memo = phylum.Memo
            });

            return collection;
        }
        //----------------------------------------------------------
        public ObservableCollection<Tbl90Reference> CopyReferenceRegnum(Tbl90Reference selected, string refer)
        {
            var reference = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        RegnumId = reference.RegnumId,
                        RefExpertId = reference.RefExpertId,
                        Valid = reference.Valid,
                        ValidYear = reference.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = reference.Memo
                    });
                    break;
                case "Source":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        RegnumId = reference.RegnumId,
                        RefSourceId = reference.RefSourceId,
                        Valid = reference.Valid,
                        ValidYear = reference.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = reference.Memo
                    });
                    break;
                case "Author":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        RegnumId = reference.RegnumId,
                        RefAuthorId = reference.RefAuthorId,
                        Valid = reference.Valid,
                        ValidYear = reference.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = reference.Memo
                    });
                    break;
            }

            return collection;
        }
        //----------------------------------------------------------
        public ObservableCollection<Tbl90Reference> CopyReferencePhylum(Tbl90Reference selected, string refer)
        {
            var reference = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        PhylumId = reference.PhylumId,
                        RefExpertId = reference.RefExpertId,
                        Valid = reference.Valid,
                        ValidYear = reference.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = reference.Memo
                    });
                    break;
                case "Source":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        PhylumId = reference.PhylumId,
                        RefSourceId = reference.RefSourceId,
                        Valid = reference.Valid,
                        ValidYear = reference.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = reference.Memo
                    });
                    break;
                case "Author":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        PhylumId = reference.PhylumId,
                        RefAuthorId = reference.RefAuthorId,
                        Valid = reference.Valid,
                        ValidYear = reference.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = reference.Memo
                    });
                    break;
            }

            return collection;
        }
        //----------------------------------------------------------
        public ObservableCollection<Tbl93Comment> CopyComment(Tbl93Comment selected, string name)
        {
            var comment = _uow.Tbl93Comments.GetById(selected.CommentId);
            var collection = new ObservableCollection<Tbl93Comment>();
            switch (name)
            {
                case "Regnum":
                    collection.Insert(0, new Tbl93Comment
                    {
                        RegnumId = comment.RegnumId,
                        Valid = comment.Valid,
                        ValidYear = comment.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = comment.Memo
                    });
                    break;
                case "Phylum":
                    collection.Insert(0, new Tbl93Comment
                    {
                        PhylumId = comment.PhylumId,
                        Valid = comment.Valid,
                        ValidYear = comment.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = comment.Memo
                    });
                    break;
            }

            return collection;
        }
        //-----------------------------------------------------------
    }
}
