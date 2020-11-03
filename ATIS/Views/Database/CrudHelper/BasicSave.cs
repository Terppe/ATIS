using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using System;

namespace ATIS.Ui.Views.Database.CrudHelper
{
    public class BasicSave : ViewModelBase
    {
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());

        public Tbl03Regnum RegnumUpdate(Tbl03Regnum regnum, Tbl03Regnum selectedRegnum)
        {
            if (regnum != null) //update
            {
                regnum.RegnumName = selectedRegnum.RegnumName;
                regnum.Subregnum = selectedRegnum.Subregnum;
                regnum.Valid = selectedRegnum.Valid;
                regnum.ValidYear = selectedRegnum.ValidYear;
                regnum.Author = selectedRegnum.Author;
                regnum.AuthorYear = selectedRegnum.AuthorYear;
                regnum.Info = selectedRegnum.Info;
                regnum.Synonym = selectedRegnum.Synonym;
                regnum.EngName = selectedRegnum.EngName;
                regnum.GerName = selectedRegnum.GerName;
                regnum.FraName = selectedRegnum.FraName;
                regnum.PorName = selectedRegnum.PorName;
                regnum.Memo = selectedRegnum.Memo;
                regnum.Updater = Environment.UserName;
                regnum.UpdaterDate = DateTime.Now;
            }
            return regnum;
        }
        public Tbl03Regnum RegnumAdd(Tbl03Regnum selectedRegnum)
        {
            var regnum = new Tbl03Regnum() //add new
            {
                RegnumName = selectedRegnum.RegnumName,
                Subregnum = selectedRegnum.Subregnum,
                CountId = RandomHelper.Randomnumber(),
                Valid = selectedRegnum.Valid,
                ValidYear = selectedRegnum.ValidYear,
                Author = selectedRegnum.Author,
                AuthorYear = selectedRegnum.AuthorYear,
                Info = selectedRegnum.Info,
                Synonym = selectedRegnum.Synonym,
                EngName = selectedRegnum.EngName,
                GerName = selectedRegnum.GerName,
                FraName = selectedRegnum.FraName,
                PorName = selectedRegnum.PorName,
                Memo = selectedRegnum.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return regnum;
        }
        public Tbl90Reference ReferenceExpertRegnumUpdate(Tbl90Reference reference, Tbl90Reference selectedReferenceExpert)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selectedReferenceExpert.RefExpertId;
                reference.RegnumId = selectedReferenceExpert.RegnumId;
                reference.Valid = selectedReferenceExpert.Valid;
                reference.ValidYear = selectedReferenceExpert.ValidYear;
                reference.Info = selectedReferenceExpert.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selectedReferenceExpert.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertRegnumAdd(Tbl90Reference selectedReferenceExpert)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selectedReferenceExpert.RefExpertId,
                RegnumId = selectedReferenceExpert.RegnumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selectedReferenceExpert.Valid,
                ValidYear = selectedReferenceExpert.ValidYear,
                Info = selectedReferenceExpert.Info,
                Memo = selectedReferenceExpert.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl90Reference ReferenceSourceRegnumUpdate(Tbl90Reference reference, Tbl90Reference selectedReferenceSource)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selectedReferenceSource.RefSourceId;
                reference.RegnumId = selectedReferenceSource.RegnumId;
                reference.Valid = selectedReferenceSource.Valid;
                reference.ValidYear = selectedReferenceSource.ValidYear;
                reference.Info = selectedReferenceSource.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selectedReferenceSource.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceRegnumAdd(Tbl90Reference selectedReferenceSource)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selectedReferenceSource.RefSourceId,
                RegnumId = selectedReferenceSource.RegnumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selectedReferenceSource.Valid,
                ValidYear = selectedReferenceSource.ValidYear,
                Info = selectedReferenceSource.Info,
                Memo = selectedReferenceSource.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl90Reference ReferenceAuthorRegnumUpdate(Tbl90Reference reference, Tbl90Reference selectedReferenceAuthor)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selectedReferenceAuthor.RefAuthorId;
                reference.RegnumId = selectedReferenceAuthor.RegnumId;
                reference.Valid = selectedReferenceAuthor.Valid;
                reference.ValidYear = selectedReferenceAuthor.ValidYear;
                reference.Info = selectedReferenceAuthor.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selectedReferenceAuthor.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorRegnumAdd(Tbl90Reference selectedReferenceAuthor)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selectedReferenceAuthor.RefAuthorId,
                RegnumId = selectedReferenceAuthor.RegnumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selectedReferenceAuthor.Valid,
                ValidYear = selectedReferenceAuthor.ValidYear,
                Info = selectedReferenceAuthor.Info,
                Memo = selectedReferenceAuthor.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl93Comment CommentRegnumUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.RegnumId = selected.RegnumId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentRegnumAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                RegnumId = selected.RegnumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return comment;
        }
        public void RegnumSave(Tbl03Regnum home, Tbl03Regnum selected)
        {
            if (selected.RegnumId != 0)   //update
                _uow.Tbl03Regnums.Update(home);
            else                                //add
                _uow.Tbl03Regnums.Add(home);

            _uow.Complete();
        }

        //------------------Phylum---------------------------------------
        public Tbl06Phylum PhylumUpdate(Tbl06Phylum phylum, Tbl06Phylum selectedPhylum)
        {
            if (phylum != null) //update
            {
                phylum.PhylumName = selectedPhylum.PhylumName;
                phylum.RegnumId = selectedPhylum.RegnumId;
                phylum.Valid = selectedPhylum.Valid;
                phylum.ValidYear = selectedPhylum.ValidYear;
                phylum.Author = selectedPhylum.Author;
                phylum.AuthorYear = selectedPhylum.AuthorYear;
                phylum.Info = selectedPhylum.Info;
                phylum.Synonym = selectedPhylum.Synonym;
                phylum.EngName = selectedPhylum.EngName;
                phylum.GerName = selectedPhylum.GerName;
                phylum.FraName = selectedPhylum.FraName;
                phylum.PorName = selectedPhylum.PorName;
                phylum.Memo = selectedPhylum.Memo;
                phylum.Updater = Environment.UserName;
                phylum.UpdaterDate = DateTime.Now;
            }
            return phylum;
        }
        public Tbl06Phylum PhylumAdd(Tbl06Phylum selectedPhylum)
        {
            var phylum = new Tbl06Phylum() //add new
            {
                PhylumName = selectedPhylum.PhylumName,
                RegnumId = selectedPhylum.RegnumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selectedPhylum.Valid,
                ValidYear = selectedPhylum.ValidYear,
                Author = selectedPhylum.Author,
                AuthorYear = selectedPhylum.AuthorYear,
                Info = selectedPhylum.Info,
                Synonym = selectedPhylum.Synonym,
                EngName = selectedPhylum.EngName,
                GerName = selectedPhylum.GerName,
                FraName = selectedPhylum.FraName,
                PorName = selectedPhylum.PorName,
                Memo = selectedPhylum.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return phylum;
        }
        public Tbl90Reference ReferenceExpertPhylumUpdate(Tbl90Reference reference, Tbl90Reference selectedReferenceExpert)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selectedReferenceExpert.RefExpertId;
                reference.PhylumId = selectedReferenceExpert.PhylumId;
                reference.Valid = selectedReferenceExpert.Valid;
                reference.ValidYear = selectedReferenceExpert.ValidYear;
                reference.Info = selectedReferenceExpert.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selectedReferenceExpert.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertPhylumAdd(Tbl90Reference selectedReferenceExpert)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selectedReferenceExpert.RefExpertId,
                PhylumId = selectedReferenceExpert.PhylumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selectedReferenceExpert.Valid,
                ValidYear = selectedReferenceExpert.ValidYear,
                Info = selectedReferenceExpert.Info,
                Memo = selectedReferenceExpert.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl90Reference ReferenceSourcePhylumUpdate(Tbl90Reference reference, Tbl90Reference selectedReferenceSource)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selectedReferenceSource.RefSourceId;
                reference.PhylumId = selectedReferenceSource.PhylumId;
                reference.Valid = selectedReferenceSource.Valid;
                reference.ValidYear = selectedReferenceSource.ValidYear;
                reference.Info = selectedReferenceSource.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selectedReferenceSource.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourcePhylumAdd(Tbl90Reference selectedReferenceSource)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selectedReferenceSource.RefSourceId,
                PhylumId = selectedReferenceSource.PhylumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selectedReferenceSource.Valid,
                ValidYear = selectedReferenceSource.ValidYear,
                Info = selectedReferenceSource.Info,
                Memo = selectedReferenceSource.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl90Reference ReferenceAuthorPhylumUpdate(Tbl90Reference reference, Tbl90Reference selectedReferenceAuthor)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selectedReferenceAuthor.RefAuthorId;
                reference.PhylumId = selectedReferenceAuthor.PhylumId;
                reference.Valid = selectedReferenceAuthor.Valid;
                reference.ValidYear = selectedReferenceAuthor.ValidYear;
                reference.Info = selectedReferenceAuthor.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selectedReferenceAuthor.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorPhylumAdd(Tbl90Reference selectedReferenceAuthor)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selectedReferenceAuthor.RefAuthorId,
                PhylumId = selectedReferenceAuthor.PhylumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selectedReferenceAuthor.Valid,
                ValidYear = selectedReferenceAuthor.ValidYear,
                Info = selectedReferenceAuthor.Info,
                Memo = selectedReferenceAuthor.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl93Comment CommentPhylumUpdate(Tbl93Comment comment, Tbl93Comment selectedComment)
        {
            if (comment != null) //update
            {
                comment.PhylumId = selectedComment.PhylumId;
                comment.Valid = selectedComment.Valid;
                comment.ValidYear = selectedComment.ValidYear;
                comment.Info = selectedComment.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selectedComment.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentPhylumAdd(Tbl93Comment selectedComment)
        {
            var comment = new Tbl93Comment //add new
            {
                PhylumId = selectedComment.PhylumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selectedComment.Valid,
                ValidYear = selectedComment.ValidYear,
                Info = selectedComment.Info,
                Memo = selectedComment.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return comment;
        }
        public void PhylumSave(Tbl06Phylum home, Tbl06Phylum selected)
        {

            if (selected.PhylumId != 0) //update
            {
                _uow.Tbl06Phylums.Update(home);
            }
            else                                //add
                _uow.Tbl06Phylums.Add(home);
            _uow.Complete();
        }

        //------------------Division---------------------------------------
        public Tbl09Division DivisionUpdate(Tbl09Division home, Tbl09Division selected)
        {
            if (home != null) //update
            {
                home.DivisionName = selected.DivisionName;
                home.RegnumId = selected.RegnumId;
                home.Valid = selected.Valid;
                home.ValidYear = selected.ValidYear;
                home.Author = selected.Author;
                home.AuthorYear = selected.AuthorYear;
                home.Info = selected.Info;
                home.Synonym = selected.Synonym;
                home.EngName = selected.EngName;
                home.GerName = selected.GerName;
                home.FraName = selected.FraName;
                home.PorName = selected.PorName;
                home.Memo = selected.Memo;
                home.Updater = Environment.UserName;
                home.UpdaterDate = DateTime.Now;
            }
            return home;
        }
        public Tbl09Division DivisionAdd(Tbl09Division selected)
        {
            var res = new Tbl09Division() //add new
            {
                DivisionName = selected.DivisionName,
                RegnumId = selected.RegnumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Author = selected.Author,
                AuthorYear = selected.AuthorYear,
                Info = selected.Info,
                Synonym = selected.Synonym,
                EngName = selected.EngName,
                GerName = selected.GerName,
                FraName = selected.FraName,
                PorName = selected.PorName,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return res;
        }
        public Tbl90Reference ReferenceExpertDivisionUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.DivisionId = selected.DivisionId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertDivisionAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                DivisionId = selected.DivisionId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl90Reference ReferenceSourceDivisionUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.DivisionId = selected.DivisionId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceDivisionAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                DivisionId = selected.DivisionId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl90Reference ReferenceAuthorDivisionUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.DivisionId = selected.DivisionId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorDivisionAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                DivisionId = selected.DivisionId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl93Comment CommentDivisionUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.DivisionId = selected.DivisionId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentDivisionAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                DivisionId = selected.DivisionId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return comment;
        }
        public void DivisionSave(Tbl09Division home, Tbl09Division selected)
        {
            if (selected.DivisionId != 0) //update
            {
                _uow.Tbl09Divisions.Update(home);
            }
            else                                //add
                _uow.Tbl09Divisions.Add(home);
            _uow.Complete();
        }

        //------------------Subphylum---------------------------------------
        public Tbl12Subphylum SubphylumUpdate(Tbl12Subphylum home, Tbl12Subphylum selected)
        {
            if (home != null) //update
            {
                home.SubphylumName = selected.SubphylumName;
                home.PhylumId = selected.PhylumId;
                home.Valid = selected.Valid;
                home.ValidYear = selected.ValidYear;
                home.Author = selected.Author;
                home.AuthorYear = selected.AuthorYear;
                home.Info = selected.Info;
                home.Synonym = selected.Synonym;
                home.EngName = selected.EngName;
                home.GerName = selected.GerName;
                home.FraName = selected.FraName;
                home.PorName = selected.PorName;
                home.Memo = selected.Memo;
                home.Updater = Environment.UserName;
                home.UpdaterDate = DateTime.Now;
            }
            return home;
        }
        public Tbl12Subphylum SubphylumAdd(Tbl12Subphylum selected)
        {
            var home = new Tbl12Subphylum() //add new
            {
                SubphylumName = selected.SubphylumName,
                PhylumId = selected.PhylumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Author = selected.Author,
                AuthorYear = selected.AuthorYear,
                Info = selected.Info,
                Synonym = selected.Synonym,
                EngName = selected.EngName,
                GerName = selected.GerName,
                FraName = selected.FraName,
                PorName = selected.PorName,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now
            };
            return home;
        }
        public Tbl90Reference ReferenceExpertSubphylumUpdate(Tbl90Reference reference, Tbl90Reference selectedReferenceExpert)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selectedReferenceExpert.RefExpertId;
                reference.SubphylumId = selectedReferenceExpert.SubphylumId;
                reference.Valid = selectedReferenceExpert.Valid;
                reference.ValidYear = selectedReferenceExpert.ValidYear;
                reference.Info = selectedReferenceExpert.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selectedReferenceExpert.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertSubphylumAdd(Tbl90Reference selectedReferenceExpert)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selectedReferenceExpert.RefExpertId,
                SubphylumId = selectedReferenceExpert.SubphylumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selectedReferenceExpert.Valid,
                ValidYear = selectedReferenceExpert.ValidYear,
                Info = selectedReferenceExpert.Info,
                Memo = selectedReferenceExpert.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl90Reference ReferenceSourceSubphylumUpdate(Tbl90Reference reference, Tbl90Reference selectedReferenceSource)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selectedReferenceSource.RefSourceId;
                reference.SubphylumId = selectedReferenceSource.SubphylumId;
                reference.Valid = selectedReferenceSource.Valid;
                reference.ValidYear = selectedReferenceSource.ValidYear;
                reference.Info = selectedReferenceSource.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selectedReferenceSource.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceSubphylumAdd(Tbl90Reference selectedReferenceSource)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selectedReferenceSource.RefSourceId,
                SubphylumId = selectedReferenceSource.SubphylumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selectedReferenceSource.Valid,
                ValidYear = selectedReferenceSource.ValidYear,
                Info = selectedReferenceSource.Info,
                Memo = selectedReferenceSource.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl90Reference ReferenceAuthorSubphylumUpdate(Tbl90Reference reference, Tbl90Reference selectedReferenceAuthor)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selectedReferenceAuthor.RefAuthorId;
                reference.SubphylumId = selectedReferenceAuthor.SubphylumId;
                reference.Valid = selectedReferenceAuthor.Valid;
                reference.ValidYear = selectedReferenceAuthor.ValidYear;
                reference.Info = selectedReferenceAuthor.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selectedReferenceAuthor.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorSubphylumAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                SubphylumId = selected.SubphylumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl93Comment CommentSubphylumUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.SubphylumId = selected.SubphylumId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentSubphylumAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                SubphylumId = selected.SubphylumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return comment;
        }
        public void SubphylumSave(Tbl12Subphylum home, Tbl12Subphylum selected)
        {

            if (selected.SubphylumId != 0) //update
            {
                _uow.Tbl12Subphylums.Update(home);
            }
            else                                //add
                _uow.Tbl12Subphylums.Add(home);
            _uow.Complete();
        }

        //------------------Subdivision---------------------------------------
        public void SubdivisionSave(Tbl15Subdivision home, Tbl15Subdivision selected)
        {

            if (selected.SubdivisionId != 0) //update
            {
                _uow.Tbl15Subdivisions.Update(home);
            }
            else                                //add
                _uow.Tbl15Subdivisions.Add(home);
            _uow.Complete();
        }

        //------------------Superclass---------------------------------------
        public Tbl18Superclass SuperclassUpdate(Tbl18Superclass home, Tbl18Superclass selected)
        {
            if (home != null) //update
            {
                home.SuperclassName = selected.SuperclassName;
                home.SubphylumId = selected.SubphylumId;
                home.SubdivisionId = selected.SubdivisionId;
                home.Valid = selected.Valid;
                home.ValidYear = selected.ValidYear;
                home.Author = selected.Author;
                home.AuthorYear = selected.AuthorYear;
                home.Info = selected.Info;
                home.Synonym = selected.Synonym;
                home.EngName = selected.EngName;
                home.GerName = selected.GerName;
                home.FraName = selected.FraName;
                home.PorName = selected.PorName;
                home.Memo = selected.Memo;
                home.Updater = Environment.UserName;
                home.UpdaterDate = DateTime.Now;
            }
            return home;
        }
        public Tbl18Superclass SuperclassAdd(Tbl18Superclass selected)
        {
            var home = new Tbl18Superclass() //add new
            {
                SuperclassName = selected.SuperclassName,
                SubphylumId = selected.SubphylumId,
                SubdivisionId = selected.SubdivisionId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Author = selected.Author,
                AuthorYear = selected.AuthorYear,
                Info = selected.Info,
                Synonym = selected.Synonym,
                EngName = selected.EngName,
                GerName = selected.GerName,
                FraName = selected.FraName,
                PorName = selected.PorName,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now
            };
            return home;
        }
        public Tbl90Reference ReferenceExpertSuperclassUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.SuperclassId = selected.SuperclassId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertSuperclassAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                SuperclassId = selected.SuperclassId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl90Reference ReferenceSourceSuperclassUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.SuperclassId = selected.SuperclassId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceSuperclassAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                SuperclassId = selected.SuperclassId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl90Reference ReferenceAuthorSuperclassUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.SuperclassId = selected.SuperclassId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorSuperclassAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                SuperclassId = selected.SuperclassId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl93Comment CommentSuperclassUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.SuperclassId = selected.SuperclassId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentSuperclassAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                SuperclassId = selected.SuperclassId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return comment;
        }
        public void SuperclassSave(Tbl18Superclass home, Tbl18Superclass selected)
        {

            if (selected.SuperclassId != 0) //update
            {
                _uow.Tbl18Superclasses.Update(home);
            }
            else                                //add
                _uow.Tbl18Superclasses.Add(home);
            _uow.Complete();
        }

        //-------------------Reference-------------------------------------
        public void ReferenceExpertSave(Tbl90Reference home, Tbl90Reference selected)
        {
            if (selected.ReferenceId != 0)   //update
                _uow.Tbl90References.Update(home);
            else                                            //add
                _uow.Tbl90References.Add(home);

            _uow.Complete();
        }
        public void ReferenceSourceSave(Tbl90Reference home, Tbl90Reference selected)
        {
            if (selected.ReferenceId != 0)   //update
                _uow.Tbl90References.Update(home);
            else                                            //add
                _uow.Tbl90References.Add(home);

            _uow.Complete();
        }
        public void ReferenceAuthorSave(Tbl90Reference home, Tbl90Reference selected)
        {
            if (selected.ReferenceId != 0)   //update
                _uow.Tbl90References.Update(home);
            else                                            //add
                _uow.Tbl90References.Add(home);

            _uow.Complete();
        }
        //-------------------Comment-------------------------------------
        public void CommentSave(Tbl93Comment home, Tbl93Comment selected)
        {
            if (selected.CommentId != 0)             //update
                _uow.Tbl93Comments.Update(home);
            else                                            //add
                _uow.Tbl93Comments.Add(home);

            _uow.Complete();
        }

    }
}
