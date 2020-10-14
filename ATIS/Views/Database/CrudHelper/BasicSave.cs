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
        //---------------------------------------------------------
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
        //---------------------------------------------------------
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
        //---------------------------------------------------------
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
        //---------------------------------------------------------
        public Tbl93Comment CommentRegnumUpdate(Tbl93Comment comment, Tbl93Comment selectedComment)
        {
            if (comment != null) //update
            {
                comment.RegnumId = selectedComment.RegnumId;
                comment.Valid = selectedComment.Valid;
                comment.ValidYear = selectedComment.ValidYear;
                comment.Info = selectedComment.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selectedComment.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentRegnumAdd(Tbl93Comment selectedComment)
        {
            var comment = new Tbl93Comment //add new
            {
                RegnumId = selectedComment.RegnumId,
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
        //---------------------------------------------------------
        //---------------------------------------------------------
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
        //---------------------------------------------------------
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
        //---------------------------------------------------------
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
        //---------------------------------------------------------
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
        //---------------------------------------------------------
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
        //---------------------------------------------------------

        //---------------------------------------------------------
        public void RegnumSave(Tbl03Regnum regnum, Tbl03Regnum selectedRegnum)
        {
            if (selectedRegnum.RegnumId != 0)   //update
                _uow.Tbl03Regnums.Update(regnum);
            else                                //add
                _uow.Tbl03Regnums.Add(regnum);

            _uow.Complete();
        }
        //--------------------------------------------------------
        public void PhylumSave(Tbl06Phylum phylum, Tbl06Phylum selectedPhylum)
        {

            if (selectedPhylum.PhylumId != 0) //update
            {
                _uow.Tbl06Phylums.Update(phylum);
            }
            else                                //add
                _uow.Tbl06Phylums.Add(phylum);
            _uow.Complete();
        }
        //--------------------------------------------------------
        public void ReferenceExpertSave(Tbl90Reference reference, Tbl90Reference selectedReferenceExpert)
        {
            if (selectedReferenceExpert.ReferenceId != 0)   //update
                _uow.Tbl90References.Update(reference);
            else                                            //add
                _uow.Tbl90References.Add(reference);

            _uow.Complete();
        }
        public void ReferenceSourceSave(Tbl90Reference reference, Tbl90Reference selectedReferenceSource)
        {
            if (selectedReferenceSource.ReferenceId != 0)   //update
                _uow.Tbl90References.Update(reference);
            else                                            //add
                _uow.Tbl90References.Add(reference);

            _uow.Complete();
        }
        public void ReferenceAuthorSave(Tbl90Reference reference, Tbl90Reference selectedReferenceAuthor)
        {
            if (selectedReferenceAuthor.ReferenceId != 0)   //update
                _uow.Tbl90References.Update(reference);
            else                                            //add
                _uow.Tbl90References.Add(reference);

            _uow.Complete();
        }
        public void CommentSave(Tbl93Comment comment, Tbl93Comment selectedComment)
        {
            if (selectedComment.CommentId != 0)             //update
                _uow.Tbl93Comments.Update(comment);
            else                                            //add
                _uow.Tbl93Comments.Add(comment);

            _uow.Complete();
        }

    }
}
