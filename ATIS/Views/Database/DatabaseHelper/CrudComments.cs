using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using Microsoft.EntityFrameworkCore;

namespace ATIS.Ui.Views.Database.DatabaseHelper
{
    public class CrudComments : ViewModelBase
    {
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();
        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();

        #region [ReferenceExperts]

        public ObservableCollection<Tbl93Comment> GetComments(string searchName)
        {
            return SearchNameReturnCommentsCollection(searchName);
        }
        public ObservableCollection<Tbl93Comment> AddComment(Tbl03Regnum selectedRegnum, ObservableCollection<Tbl93Comment> commentsCollection)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(selectedRegnum)) return commentsCollection;

            commentsCollection.Insert(0, new Tbl93Comment() { RegnumId = selectedRegnum.RegnumId, Info = "NewDataset" });

            return commentsCollection;
        }
        public ObservableCollection<Tbl93Comment> CopyComment(Tbl93Comment selectedComment, ObservableCollection<Tbl93Comment> commentsCollection)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(selectedComment)) return commentsCollection;

            if (selectedComment != null)
            {
                var comment = _uow.Tbl93Comments.GetById(selectedComment.CommentId);

                commentsCollection.Insert(0, new Tbl93Comment()
                {
                    RegnumId = comment.RegnumId,
                    Valid = comment.Valid,
                    ValidYear = comment.ValidYear,
                    Info = "NewDataset",
                    Memo = comment.Memo
                });
            }

            return commentsCollection;
        }
        public ObservableCollection<Tbl93Comment> DeleteComment(Tbl03Regnum selectedRegnum, Tbl93Comment selectedComment, ObservableCollection<Tbl93Comment> commentsCollection)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(selectedComment)) return commentsCollection;

            try
            {
                var comment = _uow.Tbl93Comments.GetById(selectedComment.CommentId);
                if (comment != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("DeleteQuestion" + " " + selectedComment.Info)) return commentsCollection;

                    _uow.Tbl93Comments.Remove(comment);
                    _uow.Complete();

                    _allMessageBoxes.InfoMessageBox("DeleteSuccess", selectedComment.Info);
                }
                else
                {
                    _allMessageBoxes.InfoMessageBox("Not To Delete", "DeleteCan" + " " + selectedComment.Info + " " + "DeleteCan1");
                }
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, "Error");
                //         Log.Error(e);
            }

            return commentsCollection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(x => x.RegnumId == selectedRegnum.RegnumId));
        }
        public ObservableCollection<Tbl93Comment> SaveComment(Tbl03Regnum selectedRegnum, Tbl93Comment selectedComment, ObservableCollection<Tbl93Comment> commentsCollection)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(selectedRegnum)) return commentsCollection;

            selectedComment.RegnumId = selectedRegnum.RegnumId;

            try
            {
                var comment = _uow.Tbl93Comments.GetById(selectedComment.CommentId);
                if (selectedComment.CommentId != 0)
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
                }
                else
                {
                    comment = new Tbl93Comment() //add new
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
                }

                try
                {
                    if (selectedComment.CommentId != 0) //update
                    {
                        if (comment != null) _uow.Tbl93Comments.Update(comment);
                    }
                    else                                //add
                    if (comment != null) _uow.Tbl93Comments.Add(comment);

                    _uow.Complete();

                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(), "FailedToSave");
                    //     Log.Error(e);
                    return commentsCollection;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, "Error");
                    //         Log.Error(e);
                    return commentsCollection;
                }
                _allMessageBoxes.InfoMessageBox("SaveSuccess", selectedComment.CommentId == 0
                    ? "DatasetNew"
                    : selectedComment.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, "Error");
                //         Log.Error(e);
            }

            return commentsCollection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                .Find(x => x.RegnumId == selectedRegnum.RegnumId));
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
                            .Find(e => e.CommentId == id ))
                        : new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
                            .Find(e => e.Info.StartsWith(searchName))
                        );
                    break;
            }

            return collection;
        }

        #endregion


    }
}
