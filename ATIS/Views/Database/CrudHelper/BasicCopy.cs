using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using System.Collections.ObjectModel;

namespace ATIS.Ui.Views.Database.CrudHelper
{
    public class BasicCopy : ViewModelBase
    {
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());

        public ObservableCollection<Tbl03Regnum> CopyRegnum(Tbl03Regnum selected)
        {
            var dataset = _uow.Tbl03Regnums.GetById(selected.RegnumId);
            var collection = new ObservableCollection<Tbl03Regnum>();

            collection.Insert(0, new Tbl03Regnum
            {
                RegnumName = CultRes.StringsRes.DatasetNew,
                Subregnum = dataset.Subregnum,
                Valid = dataset.Valid,
                ValidYear = dataset.ValidYear,
                Synonym = dataset.Synonym,
                Author = dataset.Author,
                AuthorYear = dataset.AuthorYear,
                Info = dataset.Info,
                EngName = dataset.EngName,
                GerName = dataset.GerName,
                FraName = dataset.FraName,
                PorName = dataset.PorName,
                Memo = dataset.Memo
            });

            return collection;
        }
        //----------------------------------------------------------
        public ObservableCollection<Tbl06Phylum> CopyPhylum(Tbl06Phylum selected)
        {
            var dataset = _uow.Tbl06Phylums.GetById(selected.PhylumId);
            var collection = new ObservableCollection<Tbl06Phylum>();

            collection.Insert(0, new Tbl06Phylum
            {
                PhylumName = CultRes.StringsRes.DatasetNew,
                RegnumId = dataset.RegnumId,
                Valid = dataset.Valid,
                ValidYear = dataset.ValidYear,
                Synonym = dataset.Synonym,
                Author = dataset.Author,
                AuthorYear = dataset.AuthorYear,
                Info = dataset.Info,
                EngName = dataset.EngName,
                GerName = dataset.GerName,
                FraName = dataset.FraName,
                PorName = dataset.PorName,
                Memo = dataset.Memo
            });

            return collection;
        }
        //----------------------------------------------------------
        public ObservableCollection<Tbl09Division> CopyDivision(Tbl09Division selected)
        {
            var dataset = _uow.Tbl09Divisions.GetById(selected.DivisionId);
            var collection = new ObservableCollection<Tbl09Division>();

            collection.Insert(0, new Tbl09Division
            {
                DivisionName = CultRes.StringsRes.DatasetNew,
                RegnumId = dataset.RegnumId,
                Valid = dataset.Valid,
                ValidYear = dataset.ValidYear,
                Synonym = dataset.Synonym,
                Author = dataset.Author,
                AuthorYear = dataset.AuthorYear,
                Info = dataset.Info,
                EngName = dataset.EngName,
                GerName = dataset.GerName,
                FraName = dataset.FraName,
                PorName = dataset.PorName,
                Memo = dataset.Memo
            });

            return collection;
        }
        //----------------------------------------------------------
        public ObservableCollection<Tbl12Subphylum> CopySubphylum(Tbl12Subphylum selected)
        {
            var dataset = _uow.Tbl12Subphylums.GetById(selected.SubphylumId);
            var collection = new ObservableCollection<Tbl12Subphylum>();

            collection.Insert(0, new Tbl12Subphylum
            {
                SubphylumName = CultRes.StringsRes.DatasetNew,
                PhylumId = dataset.PhylumId,
                Valid = dataset.Valid,
                ValidYear = dataset.ValidYear,
                Synonym = dataset.Synonym,
                Author = dataset.Author,
                AuthorYear = dataset.AuthorYear,
                Info = dataset.Info,
                EngName = dataset.EngName,
                GerName = dataset.GerName,
                FraName = dataset.FraName,
                PorName = dataset.PorName,
                Memo = dataset.Memo
            });

            return collection;
        }
        //----------------------------------------------------------
        public ObservableCollection<Tbl15Subdivision> CopySubdivision(Tbl15Subdivision selected)
        {
            var dataset = _uow.Tbl15Subdivisions.GetById(selected.SubdivisionId);
            var collection = new ObservableCollection<Tbl15Subdivision>();

            collection.Insert(0, new Tbl15Subdivision
            {
                SubdivisionName = CultRes.StringsRes.DatasetNew,
                DivisionId = dataset.DivisionId,
                Valid = dataset.Valid,
                ValidYear = dataset.ValidYear,
                Synonym = dataset.Synonym,
                Author = dataset.Author,
                AuthorYear = dataset.AuthorYear,
                Info = dataset.Info,
                EngName = dataset.EngName,
                GerName = dataset.GerName,
                FraName = dataset.FraName,
                PorName = dataset.PorName,
                Memo = dataset.Memo
            });

            return collection;
        }
        //----------------------------------------------------------
        public ObservableCollection<Tbl18Superclass> CopySuperclass(Tbl18Superclass selected)
        {
            var dataset = _uow.Tbl18Superclasses.GetById(selected.SuperclassId);
            var collection = new ObservableCollection<Tbl18Superclass>();

            collection.Insert(0, new Tbl18Superclass
            {
                SuperclassName = CultRes.StringsRes.DatasetNew,
                SubphylumId = dataset.SubphylumId,
                SubdivisionId = dataset.SubdivisionId,
                Valid = dataset.Valid,
                ValidYear = dataset.ValidYear,
                Synonym = dataset.Synonym,
                Author = dataset.Author,
                AuthorYear = dataset.AuthorYear,
                Info = dataset.Info,
                EngName = dataset.EngName,
                GerName = dataset.GerName,
                FraName = dataset.FraName,
                PorName = dataset.PorName,
                Memo = dataset.Memo
            });

            return collection;
        }
        //----------------------------------------------------------
        public ObservableCollection<Tbl21Class> CopyClass(Tbl21Class selected)
        {
            var dataset = _uow.Tbl21Classes.GetById(selected.ClassId);
            var collection = new ObservableCollection<Tbl21Class>();

            collection.Insert(0, new Tbl21Class
            {
                ClassName = CultRes.StringsRes.DatasetNew,
                SuperclassId = dataset.SuperclassId,
                Valid = dataset.Valid,
                ValidYear = dataset.ValidYear,
                Synonym = dataset.Synonym,
                Author = dataset.Author,
                AuthorYear = dataset.AuthorYear,
                Info = dataset.Info,
                EngName = dataset.EngName,
                GerName = dataset.GerName,
                FraName = dataset.FraName,
                PorName = dataset.PorName,
                Memo = dataset.Memo
            });

            return collection;
        }
        //----------------------------------------------------------

        public ObservableCollection<Tbl24Subclass> CopySubclass(Tbl24Subclass selected)
        {
            var dataset = _uow.Tbl24Subclasses.GetById(selected.SubclassId);
            var collection = new ObservableCollection<Tbl24Subclass>();

            collection.Insert(0, new Tbl24Subclass
            {
                SubclassName = CultRes.StringsRes.DatasetNew,
                ClassId = dataset.ClassId,
                Valid = dataset.Valid,
                ValidYear = dataset.ValidYear,
                Synonym = dataset.Synonym,
                Author = dataset.Author,
                AuthorYear = dataset.AuthorYear,
                Info = dataset.Info,
                EngName = dataset.EngName,
                GerName = dataset.GerName,
                FraName = dataset.FraName,
                PorName = dataset.PorName,
                Memo = dataset.Memo
            });

            return collection;
        }

        public ObservableCollection<Tbl27Infraclass> CopyInfraclass(Tbl27Infraclass selected)
        {
            var dataset = _uow.Tbl27Infraclasses.GetById(selected.InfraclassId);
            var collection = new ObservableCollection<Tbl27Infraclass>();

            collection.Insert(0, new Tbl27Infraclass
            {
                InfraclassName = CultRes.StringsRes.DatasetNew,
                SubclassId = dataset.SubclassId,
                Valid = dataset.Valid,
                ValidYear = dataset.ValidYear,
                Synonym = dataset.Synonym,
                Author = dataset.Author,
                AuthorYear = dataset.AuthorYear,
                Info = dataset.Info,
                EngName = dataset.EngName,
                GerName = dataset.GerName,
                FraName = dataset.FraName,
                PorName = dataset.PorName,
                Memo = dataset.Memo
            });

            return collection;
        }

        public ObservableCollection<Tbl30Legio> CopyLegio(Tbl30Legio selected)
        {
            var dataset = _uow.Tbl30Legios.GetById(selected.LegioId);
            var collection = new ObservableCollection<Tbl30Legio>();

            collection.Insert(0, new Tbl30Legio
            {
                LegioName = CultRes.StringsRes.DatasetNew,
                InfraclassId = dataset.InfraclassId,
                Valid = dataset.Valid,
                ValidYear = dataset.ValidYear,
                Synonym = dataset.Synonym,
                Author = dataset.Author,
                AuthorYear = dataset.AuthorYear,
                Info = dataset.Info,
                EngName = dataset.EngName,
                GerName = dataset.GerName,
                FraName = dataset.FraName,
                PorName = dataset.PorName,
                Memo = dataset.Memo
            });

            return collection;
        }






        //----------------------------------------------------------------------------------------
        public ObservableCollection<Tbl90Reference> CopyReferenceRegnum(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        RegnumId = dataset.RegnumId,
                        RefExpertId = dataset.RefExpertId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Source":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        RegnumId = dataset.RegnumId,
                        RefSourceId = dataset.RefSourceId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Author":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        RegnumId = dataset.RegnumId,
                        RefAuthorId = dataset.RefAuthorId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
            }

            return collection;
        }
        //----------------------------------------------------------
        public ObservableCollection<Tbl90Reference> CopyReferencePhylum(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        PhylumId = dataset.PhylumId,
                        RefExpertId = dataset.RefExpertId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Source":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        PhylumId = dataset.PhylumId,
                        RefSourceId = dataset.RefSourceId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Author":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        PhylumId = dataset.PhylumId,
                        RefAuthorId = dataset.RefAuthorId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
            }

            return collection;
        }
        //----------------------------------------------------------
        public ObservableCollection<Tbl90Reference> CopyReferenceDivision(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        DivisionId = dataset.DivisionId,
                        RefExpertId = dataset.RefExpertId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Source":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        DivisionId = dataset.DivisionId,
                        RefSourceId = dataset.RefSourceId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Author":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        DivisionId = dataset.DivisionId,
                        RefAuthorId = dataset.RefAuthorId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
            }

            return collection;
        }
        //----------------------------------------------------------
        public ObservableCollection<Tbl90Reference> CopyReferenceSubphylum(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        SubphylumId = dataset.SubphylumId,
                        RefExpertId = dataset.RefExpertId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Source":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        SubphylumId = dataset.SubphylumId,
                        RefSourceId = dataset.RefSourceId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Author":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        SubphylumId = dataset.SubphylumId,
                        RefAuthorId = dataset.RefAuthorId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
            }

            return collection;
        }
        //----------------------------------------------------------
        public ObservableCollection<Tbl90Reference> CopyReferenceSubdivision(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        SubdivisionId = dataset.SubdivisionId,
                        RefExpertId = dataset.RefExpertId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Source":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        SubdivisionId = dataset.SubdivisionId,
                        RefSourceId = dataset.RefSourceId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Author":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        SubdivisionId = dataset.SubdivisionId,
                        RefAuthorId = dataset.RefAuthorId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
            }

            return collection;
        }
        //----------------------------------------------------------
        public ObservableCollection<Tbl90Reference> CopyReferenceSuperclass(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        SuperclassId = dataset.SuperclassId,
                        RefExpertId = dataset.RefExpertId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Source":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        SuperclassId = dataset.SuperclassId,
                        RefSourceId = dataset.RefSourceId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Author":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        SuperclassId = dataset.SuperclassId,
                        RefAuthorId = dataset.RefAuthorId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
            }

            return collection;
        }
        //----------------------------------------------------------
        public ObservableCollection<Tbl90Reference> CopyReferenceClass(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        ClassId = dataset.ClassId,
                        RefExpertId = dataset.RefExpertId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Source":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        ClassId = dataset.ClassId,
                        RefSourceId = dataset.RefSourceId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Author":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        ClassId = dataset.ClassId,
                        RefAuthorId = dataset.RefAuthorId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
            }

            return collection;
        }
        //--------------------------Copy Subclass --------------------------------

        public ObservableCollection<Tbl90Reference> CopyReferenceSubclass(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        SubclassId = dataset.SubclassId,
                        RefExpertId = dataset.RefExpertId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Source":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        SubclassId = dataset.SubclassId,
                        RefSourceId = dataset.RefSourceId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Author":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        SubclassId = dataset.SubclassId,
                        RefAuthorId = dataset.RefAuthorId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
            }

            return collection;
        }

        // -----------Copy Infraclass Reference --------------
        public ObservableCollection<Tbl90Reference> CopyReferenceInfraclass(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        InfraclassId = dataset.InfraclassId,
                        RefExpertId = dataset.RefExpertId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Source":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        InfraclassId = dataset.InfraclassId,
                        RefSourceId = dataset.RefSourceId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Author":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        InfraclassId = dataset.InfraclassId,
                        RefAuthorId = dataset.RefAuthorId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
            }

            return collection;
        }

        //----------------------------------------------------------

        public ObservableCollection<Tbl93Comment> CopyComment(Tbl93Comment selected, string name)
        {
            var dataset = _uow.Tbl93Comments.GetById(selected.CommentId);
            var collection = new ObservableCollection<Tbl93Comment>();
            switch (name)
            {
                case "Regnum":
                    collection.Insert(0, new Tbl93Comment
                    {
                        RegnumId = dataset.RegnumId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Phylum":
                    collection.Insert(0, new Tbl93Comment
                    {
                        PhylumId = dataset.PhylumId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Division":
                    collection.Insert(0, new Tbl93Comment
                    {
                        DivisionId = dataset.DivisionId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Subphylum":
                    collection.Insert(0, new Tbl93Comment
                    {
                        SubphylumId = dataset.SubphylumId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Subdivision":
                    collection.Insert(0, new Tbl93Comment
                    {
                        SubdivisionId = dataset.SubdivisionId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Superclass":
                    collection.Insert(0, new Tbl93Comment
                    {
                        SuperclassId = dataset.SuperclassId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
            }

            return collection;
        }
        //-----------------------------------------------------------
    }
}
