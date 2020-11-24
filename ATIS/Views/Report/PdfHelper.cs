using System;
using System.Collections.ObjectModel;
using System.Windows.Shell;
using ATIS.Dal.Models;
using ATIS.Ui.Helper;
using BitMiracle.Docotic.Pdf;

namespace ATIS.Ui.Views.Report
{
    public class PdfHelper : ViewModelBase
    {
        private static int[] _arrHelperInts = new int[11];

        private static string _n;
        private static string _z1;

        //private static int _pdfPointXLeft;
        //private static int _pdfPointY;
        //private static int _pdfPointXRight;
        //private static int _pdfSizeHeight;
        //private static int _pdfSizeWidthLeft;
        //private static int _pdfSizeWidthRight;
        //private static int _pageCount;
        //private static int _move;
        //private static int _characterSize;
        //private static int _fontSize;
        private static int _z;

        private static PdfPage _page;

        public PdfHelper()
        {
        }

        public int[] AddReportMain(PdfDocument pdf, Tbl03Regnum regnumList)
        {
            //_pdfPointXLeft = arrayInts[0];
            //_pdfPointY = arrayInts[1];
            //_pdfPointXRight = arrayInts[2];
            //_pdfSizeHeight = arrayInts[3];
            //_pdfSizeWidthLeft = arrayInts[4];
            //_pdfSizeWidthRight = arrayInts[5];
            //_pageCount = arrayInts[6];
            //_move = arrayInts[7];
            //_characterSize = arrayInts[8];
            //_fontSize = arrayInts[9];
            //_z = arrayInts[10];
            _arrHelperInts[0] = 20; //_pdfPointXLeft
            _arrHelperInts[1] = 5; //_pdfPointY
            _arrHelperInts[2] = 150; //_pdfPointXRight
            _arrHelperInts[3] = 8; //_pdfSizeHeight
            _arrHelperInts[4] = 300; //_pdfSizeWidthLeft
            _arrHelperInts[5] = 430; //_pdfSizeWidthRight
            _arrHelperInts[6] = 0; //_pageCount
            _arrHelperInts[7] = 4; //_moveIn
            _arrHelperInts[8] = 95; //_characterSize
            _arrHelperInts[9] = 8; //_fontSize
            _arrHelperInts[10] = 0; //_z counter

            _page = pdf.Pages[_arrHelperInts[6]];

            var txtHeader = _page.AddTextBox("header", new PdfPoint(_arrHelperInts[0], _arrHelperInts[1]), 
                new PdfSize(_arrHelperInts[4], _arrHelperInts[3]));
            txtHeader.HasBorder = false;
            txtHeader.ReadOnly = true;
            txtHeader.Font.SynthesizedBold = true;
            txtHeader.FontSize = _arrHelperInts[9] + 8;
            txtHeader.Height = _arrHelperInts[9] + 12;
            txtHeader.Text = CultRes.StringsRes.Report;

            _arrHelperInts[1] += _arrHelperInts[3];
            _arrHelperInts[1] += _arrHelperInts[3] +12; //Distance to next TextBox

            //arrayInts[0] = _pdfPointXLeft;
            //arrayInts[1] = _pdfPointY;
            //arrayInts[2] = _pdfPointXRight;
            //arrayInts[3] = _pdfSizeHeight;
            //arrayInts[4] = _pdfSizeWidthLeft;
            //arrayInts[5] = _pdfSizeWidthRight;
            //arrayInts[6] = _pageCount;
            //arrayInts[7] = _move;
            //arrayInts[8] = _characterSize;
            //arrayInts[9] = _fontSize;
            //arrayInts[10] = _z;

            return _arrHelperInts;
        }

        public int[] AddReferencesHaeder(PdfDocument pdf, int[] arrayInts)
        {
            _arrHelperInts = arrayInts;
            //_pdfPointXLeft = arrayInts[0];
            //_pdfPointY = arrayInts[1];
            //_pdfPointXRight = arrayInts[2];
            //_pdfSizeHeight = arrayInts[3];
            //_pdfSizeWidthLeft = arrayInts[4];
            //_pdfSizeWidthRight = arrayInts[5];
            //_pageCount = arrayInts[6];
            //_move = arrayInts[7];
            //_characterSize = arrayInts[8];
            //_fontSize = arrayInts[9];
            //_z = arrayInts[10];

            _page = pdf.Pages[_arrHelperInts[6]];

            pdf.AddPage();
            _arrHelperInts[6] +=  1;
            _page = pdf.Pages[_arrHelperInts[6]];
            _arrHelperInts[1] = 5;

            PdfTbBoldLeft("referencesHeader", _arrHelperInts, CultRes.StringsRes.ReportReferences, 2);

            //var txtRefHeader = _page.AddTextBox("referencesHeader", new PdfPoint(ArrHelperInts[0], ArrHelperInts[1]),
            //    new PdfSize(ArrHelperInts[4], ArrHelperInts[3]));
            //txtRefHeader.HasBorder = false;
            //txtRefHeader.ReadOnly = true;
            //txtRefHeader.Font.SynthesizedBold = true;
            //txtRefHeader.FontSize = ArrHelperInts[9] + 2;
            //txtRefHeader.Text = CultRes.StringsRes.ReportReferences;
            //ArrHelperInts[1] += ArrHelperInts[3];

            _arrHelperInts[1] += _arrHelperInts[9] - 3; //Distance to next TextBox

            //arrayInts[0] = _pdfPointXLeft;
            //arrayInts[1] = _pdfPointY;
            //arrayInts[2] = _pdfPointXRight;
            //arrayInts[3] = _pdfSizeHeight;
            //arrayInts[4] = _pdfSizeWidthLeft;
            //arrayInts[5] = _pdfSizeWidthRight;
            //arrayInts[6] = _pageCount;
            //arrayInts[7] = _move;
            //arrayInts[8] = _characterSize;
            //arrayInts[9] = _fontSize;
            //arrayInts[10] = _z;

            return _arrHelperInts;
        }

        public int[] AddRefExpertsList(PdfDocument pdf, ObservableCollection<Tbl90Reference> expertsList, int[] arrayInts)
        {
            _arrHelperInts = arrayInts;
            //_pdfPointXLeft = arrayInts[0];
            //_pdfPointY = arrayInts[1];
            //_pdfPointXRight = arrayInts[2];
            //_pdfSizeHeight = arrayInts[3];
            //_pdfSizeWidthLeft = arrayInts[4];
            //_pdfSizeWidthRight = arrayInts[5];
            //_pageCount = arrayInts[6];
            //_move = arrayInts[7];
            //_characterSize = arrayInts[8];
            //_fontSize = arrayInts[9];
            //_z = arrayInts[10];

            _page = pdf.Pages[_arrHelperInts[6]];

            //pdf.AddPage();
            //_page = pdf.Pages[_arrHelperInts[6] + 1];
            _arrHelperInts[1] += 5;

            PdfTbBoldMoveLeft("experts", _arrHelperInts, CultRes.StringsRes.ReportExperts, 0);

            //var txtExpertNameLeft = _page.AddTextBox("experts", new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
            //    new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            //txtExpertNameLeft.HasBorder = false;
            //txtExpertNameLeft.ReadOnly = true;
            //txtExpertNameLeft.Font.SynthesizedBold = true;
            //txtExpertNameLeft.FontSize = _fontSize;
            //txtExpertNameLeft.Text = CultRes.StringsRes.ReportExperts;
            //_pdfPointY += _pdfSizeHeight;

             _n = "expert";
            _z = 1;
            _z1 = _n + _z;

            foreach (var u in expertsList)
            {
                var u1 = u.Tbl90RefExperts.RefExpertName;
                var u2 = u.Tbl90RefExperts.Notes;
                var u3 = u.Tbl90RefExperts.Info;
                var u4 = u.Tbl90RefExperts.Memo;
                var u5 = Convert.ToString(u.Tbl90RefExperts.Valid);
                var u6 = u.Tbl90RefExperts.ValidYear;
                var u7 = u.Info;
                var u8 = u.Memo;

                _arrHelperInts = PdfHelper.PdfTbLeft("expert" + _z1, _arrHelperInts, CultRes.StringsRes.ReportExpert);
                _arrHelperInts = PdfHelper.PdfTbRight(_z1, _arrHelperInts, u1);

                //var txtExpertLeft = _page.AddTextBox("expert" + _z1, new PdfPoint(ArrHelperInts[0] + ArrHelperInts[7], ArrHelperInts[1]),
                //    new PdfSize(ArrHelperInts[4], ArrHelperInts[3]));
                //txtExpertLeft.HasBorder = false;
                //txtExpertLeft.ReadOnly = true;
                //txtExpertLeft.Font.SynthesizedBold = false;
                //txtExpertLeft.FontSize = ArrHelperInts[9];
                //txtExpertLeft.Text = CultRes.StringsRes.ReportExpert;

                //var txtExpertRight = _page.AddTextBox(_z1, new PdfPoint(ArrHelperInts[2], ArrHelperInts[1]),
                //    new PdfSize(ArrHelperInts[5], ArrHelperInts[3]));
                //txtExpertRight.HasBorder = false;
                //txtExpertRight.ReadOnly = true;
                //txtExpertRight.FontSize = ArrHelperInts[9];
                //txtExpertRight.Text = u1;
                //ArrHelperInts[1] += ArrHelperInts[3];

                _z += 1;
                _z1 = _n + _z;
                //--------------------------------------------------
                ReportLeftIfRightTextBox(CultRes.StringsRes.ReportNotes, u2);
                ReportIfLeftRightTextBox(CultRes.StringsRes.ReportInfo, u3);
                ReportIfLeftRightTextBox(CultRes.StringsRes.ReportMemo, u4);
                ReportValidTextBox(CultRes.StringsRes.ReportValid, CultRes.StringsRes.ReportValidYear, u5, u6);
                ReportLeftIfRightTextBox(CultRes.StringsRes.ReportRefFor, u7);
                ReportIfLeftRightTextBox(CultRes.StringsRes.ReportMemo, u8);

                _arrHelperInts[1] += _arrHelperInts[6] + 2; //Abstand zum nächsten Datensatz

                //var author = PdfHelper.AuthorViewChangeWithString(t.Author, t.AuthorYear);

                
            }

            //arrayInts[0] = _pdfPointXLeft;
            //arrayInts[1] = _pdfPointY;
            //arrayInts[2] = _pdfPointXRight;
            //arrayInts[3] = _pdfSizeHeight;
            //arrayInts[4] = _pdfSizeWidthLeft;
            //arrayInts[5] = _pdfSizeWidthRight;
            //arrayInts[6] = _pageCount;
            //arrayInts[7] = _move;
            //arrayInts[8] = _characterSize;
            //arrayInts[9] = _fontSize;
            //arrayInts[10] = _z;

            return _arrHelperInts;
        }

        public int[] AddRefSourcesList(PdfDocument pdf, ObservableCollection<Tbl90Reference> sourcesList, int[] arrayInts)
        {
            _arrHelperInts = arrayInts;

            //_pdfPointXLeft = arrayInts[0];
            //_pdfPointY = arrayInts[1];
            //_pdfPointXRight = arrayInts[2];
            //_pdfSizeHeight = arrayInts[3];
            //_pdfSizeWidthLeft = arrayInts[4];
            //_pdfSizeWidthRight = arrayInts[5];
            //_pageCount = arrayInts[6];
            //_move = arrayInts[7];
            //_characterSize = arrayInts[8];
            //_fontSize = arrayInts[9];
            //_z = arrayInts[10];

            _page = pdf.Pages[_arrHelperInts[6]];

            //pdf.AddPage();
            //_page = pdf.Pages[_arrHelperInts[6] + 1];
            //_arrHelperInts[6] += 1;
            _arrHelperInts[1] += 5;

            PdfTbBoldMoveLeft("sources", _arrHelperInts, CultRes.StringsRes.ReportOtherSources, 0);

            //var txtSourceNameLeft = _page.AddTextBox("sources", new PdfPoint(_pdfPointXLeft + _move, _pdfPointY), 
            //    new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            //txtSourceNameLeft.HasBorder = false;
            //txtSourceNameLeft.ReadOnly = true;
            //txtSourceNameLeft.Font.SynthesizedBold = true;
            //txtSourceNameLeft.FontSize = _fontSize;
            //txtSourceNameLeft.Text = CultRes.StringsRes.ReportOtherSources;
            //_pdfPointY += _pdfSizeHeight;

            _n = "source";
            _z = 1;
            _z1 = _n + _z;

            foreach (var u in sourcesList)
            {
                var u1 = u.Tbl90RefSources.RefSourceName;
                var u2 = u.Tbl90RefSources.SourceYear;
                var u3 = u.Tbl90RefSources.Notes;
                var u4 = u.Tbl90RefSources.Author;
                var u5 = u.Tbl90RefSources.Info;
                var u6 = u.Tbl90RefSources.Memo;
                var u7 = Convert.ToString(u.Tbl90RefSources.Valid);
                var u8 = u.Tbl90RefSources.ValidYear;
                var u9 = u.Info;
                var u10 = u.Memo;

                _arrHelperInts = PdfHelper.PdfTbLeft("source" + _z1, _arrHelperInts, CultRes.StringsRes.ReportSource);
                _arrHelperInts = PdfHelper.PdfTbRight(_z1, _arrHelperInts, u1);

                //var txtSourceLeft = _page.AddTextBox("source" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                //    new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                //txtSourceLeft.HasBorder = false;
                //txtSourceLeft.ReadOnly = true;
                //txtSourceLeft.Font.SynthesizedBold = false;
                //txtSourceLeft.FontSize = _fontSize;
                //txtSourceLeft.Text = CultRes.StringsRes.ReportSource;

                //var txtSourceRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                //    new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
                //txtSourceRight.HasBorder = false;
                //txtSourceRight.ReadOnly = true;
                //txtSourceRight.FontSize = _fontSize;
                //txtSourceRight.Text = u1;
                //_pdfPointY += _pdfSizeHeight;

                _z += 1;
                _z1 = _n + _z;
                //--------------------------------------------------
                ReportLeftIfRightTextBox(CultRes.StringsRes.ReportAcquired, u2);
                ReportLeftIfRightTextBox(CultRes.StringsRes.ReportNotes, u3);
                ReportIfLeftRightTextBox(CultRes.StringsRes.ReportAuthor, u4);
                ReportIfLeftRightTextBox(CultRes.StringsRes.ReportInfo, u5);
                ReportIfLeftRightTextBox(CultRes.StringsRes.ReportMemo, u6); 
                ReportValidTextBox(CultRes.StringsRes.ReportValid, CultRes.StringsRes.ReportValidYear, u7, u8);
                ReportLeftIfRightTextBox(CultRes.StringsRes.ReportRefFor, u9);
                ReportIfLeftRightTextBox(CultRes.StringsRes.ReportMemo, u10);

         //       _pdfPointY += 10; //Abstand zum nächsten Datensatz
                _arrHelperInts[1] += _arrHelperInts[6] + 2; //Abstand zum nächsten Datensatz

                //var author = PdfHelper.AuthorViewChangeWithString(t.Author, t.AuthorYear);
            }
            //arrayInts[0] = _pdfPointXLeft;
            //arrayInts[1] = _pdfPointY;
            //arrayInts[2] = _pdfPointXRight;
            //arrayInts[3] = _pdfSizeHeight;
            //arrayInts[4] = _pdfSizeWidthLeft;
            //arrayInts[5] = _pdfSizeWidthRight;
            //arrayInts[6] = _pageCount;
            //arrayInts[7] = _move;
            //arrayInts[8] = _characterSize;
            //arrayInts[9] = _fontSize;
            //arrayInts[10] = _z;

            return _arrHelperInts;
        }

        public int[] AddRefAuthorsList(PdfDocument pdf, ObservableCollection<Tbl90Reference> authorsList, int[] arrayInts)
        {
            _arrHelperInts = arrayInts;

            //_pdfPointXLeft = arrayInts[0];
            //_pdfPointY = arrayInts[1];
            //_pdfPointXRight = arrayInts[2];
            //_pdfSizeHeight = arrayInts[3];
            //_pdfSizeWidthLeft = arrayInts[4];
            //_pdfSizeWidthRight = arrayInts[5];
            //_pageCount = arrayInts[6];
            //_move = arrayInts[7];
            //_characterSize = arrayInts[8];
            //_fontSize = arrayInts[9];
            //_z = arrayInts[10];

            _page = pdf.Pages[_arrHelperInts[6]];

            //pdf.AddPage();
            //_page = pdf.Pages[_arrHelperInts[6] + 1];
            //_arrHelperInts[6] += 1;
            _arrHelperInts[1] += 5;

            PdfTbBoldMoveLeft("authors", _arrHelperInts, CultRes.StringsRes.ReportPublications, 0);

            //var txtAuthorNameLeft = _page.AddTextBox("authors", new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
            //    new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            //txtAuthorNameLeft.HasBorder = false;
            //txtAuthorNameLeft.ReadOnly = true;
            //txtAuthorNameLeft.Font.SynthesizedBold = true;
            //txtAuthorNameLeft.FontSize = _fontSize;
            //txtAuthorNameLeft.Text = CultRes.StringsRes.ReportPublications;
            //_pdfPointY += _pdfSizeHeight;

            _n = "author";
            _z = 1;
            _z1 = _n + _z;

            foreach (var t in authorsList)
            {
                var t1 = t.Tbl90RefAuthors.RefAuthorName;
                var t2 = t.Tbl90RefAuthors.PublicationYear;
                var t3 = t.Tbl90RefAuthors.ArticelTitle;
                var t4 = t.Tbl90RefAuthors.BookName;
                var t5 = t.Tbl90RefAuthors.Page1;
                var t6 = t.Tbl90RefAuthors.Publisher;
                var t7 = t.Tbl90RefAuthors.PublicationPlace;
                var t8 = t.Tbl90RefAuthors.ISBN;
                var t9 = t.Tbl90RefAuthors.Notes;
                var t10 = t.Tbl90RefAuthors.Info;
                var t11 = t.Tbl90RefAuthors.Memo;
                var t12 = Convert.ToString(t.Tbl90RefAuthors.Valid);
                var t13 = t.Tbl90RefAuthors.ValidYear;
                var t14 = t.Info;
                var t15 = t.Memo;

                //_arrHelperInts = PdfHelper.PdfTbLeft("author" + _z1, _arrHelperInts, CultRes.StringsRes.ReportAuthorsEditors);
                //_arrHelperInts = PdfHelper.PdfTbRight(_z1, _arrHelperInts, t1);

                //--------------------------------------------------

                ReportIfLeftRightTextBox(CultRes.StringsRes.ReportAuthorsEditors, t1);
                ReportLeftIfRightTextBox(CultRes.StringsRes.ReportPublicationDate, t2);
                ReportLeftIfRightTextBox(CultRes.StringsRes.ReportArticleTitle, t3);
                ReportLeftIfRightTextBox(CultRes.StringsRes.ReportBookName, t4);
                ReportLeftIfRightTextBox(CultRes.StringsRes.ReportPages, t5);
                ReportLeftIfRightTextBox(CultRes.StringsRes.ReportPublisher, t6);
                ReportLeftIfRightTextBox(CultRes.StringsRes.ReportPublicationPlace, t7);
                ReportLeftIfRightTextBox(CultRes.StringsRes.ReportIsbn, t8);
                ReportLeftIfRightTextBox(CultRes.StringsRes.ReportNotes, t9);
                ReportIfLeftRightTextBox(CultRes.StringsRes.ReportInfo, t10);
                ReportIfLeftRightTextBox(CultRes.StringsRes.ReportMemo, t11);
                ReportValidTextBox(CultRes.StringsRes.ReportValid, CultRes.StringsRes.ReportValidYear, t12, t13);
                ReportLeftIfRightTextBox(CultRes.StringsRes.ReportRefFor, t14);
                ReportIfLeftRightTextBox(CultRes.StringsRes.ReportMemo, t15);

                _arrHelperInts[1] += _arrHelperInts[6] + 2; //Abstand zum nächsten Datensatz

                //var author = PdfHelper.AuthorViewChangeWithString(t.Author, t.AuthorYear);

            }
            //arrayInts[0] = _pdfPointXLeft;
            //arrayInts[1] = _pdfPointY;
            //arrayInts[2] = _pdfPointXRight;
            //arrayInts[3] = _pdfSizeHeight;
            //arrayInts[4] = _pdfSizeWidthLeft;
            //arrayInts[5] = _pdfSizeWidthRight;
            //arrayInts[6] = _pageCount;
            //arrayInts[7] = _move;
            //arrayInts[8] = _characterSize;
            //arrayInts[9] = _fontSize;
            //arrayInts[10] = _z;

            return _arrHelperInts;
        }

        public int[] AddCommentsHaeder(PdfDocument pdf, int[] arrayInts)
        {
            _arrHelperInts = arrayInts;

            //_pdfPointXLeft = arrayInts[0];
            //_pdfPointY = arrayInts[1];
            //_pdfPointXRight = arrayInts[2];
            //_pdfSizeHeight = arrayInts[3];
            //_pdfSizeWidthLeft = arrayInts[4];
            //_pdfSizeWidthRight = arrayInts[5];
            //_pageCount = arrayInts[6];
            //_move = arrayInts[7];
            //_characterSize = arrayInts[8];
            //_fontSize = arrayInts[9];
            //_z = arrayInts[10];

            _page = pdf.Pages[_arrHelperInts[6]];

            pdf.AddPage();
            _arrHelperInts[6] +=  1;
            _page = pdf.Pages[_arrHelperInts[6]];
            _arrHelperInts[1] = 5;

            PdfTbBoldLeft("commentsHeader", _arrHelperInts, CultRes.StringsRes.ReportComments, 2);

            //var txtCommHeader = _page.AddTextBox("commentsHeader", new PdfPoint(_pdfPointXLeft, _pdfPointY),
            //    new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            //txtCommHeader.HasBorder = false;
            //txtCommHeader.ReadOnly = true;
            //txtCommHeader.Font.SynthesizedBold = true;
            //txtCommHeader.FontSize = 10;
            //txtCommHeader.Text = CultRes.StringsRes.ReportComments;
            //_pdfPointY += _pdfSizeHeight;

            _arrHelperInts[1] += _arrHelperInts[9] - 3; //Distance to next TextBox

            //arrayInts[0] = _pdfPointXLeft;
            //arrayInts[1] = _pdfPointY;
            //arrayInts[2] = _pdfPointXRight;
            //arrayInts[3] = _pdfSizeHeight;
            //arrayInts[4] = _pdfSizeWidthLeft;
            //arrayInts[5] = _pdfSizeWidthRight;
            //arrayInts[6] = _pageCount;
            //arrayInts[7] = _move;
            //arrayInts[8] = _characterSize;
            //arrayInts[9] = _fontSize;
            //arrayInts[10] = _z;

            return _arrHelperInts;
        }

        public int[] AddCommentsList(PdfDocument pdf, ObservableCollection<Tbl93Comment> commentsList, int[] arrayInts)
        {
            _arrHelperInts = arrayInts;

            //_pdfPointXLeft = arrayInts[0];
            //_pdfPointY = arrayInts[1];
            //_pdfPointXRight = arrayInts[2];
            //_pdfSizeHeight = arrayInts[3];
            //_pdfSizeWidthLeft = arrayInts[4];
            //_pdfSizeWidthRight = arrayInts[5];
            //_pageCount = arrayInts[6];
            //_move = arrayInts[7];
            //_characterSize = arrayInts[8];
            //_fontSize = arrayInts[9];
            //_z = arrayInts[10];

            _page = pdf.Pages[_arrHelperInts[6]];

            //pdf.AddPage();
            //_page = pdf.Pages[_pageCount + 1];
            //_pdfPointY = 5;

            //var txtCommentHaeder = _page.AddTextBox("comments", new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
            //    new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            //txtCommentHaeder.HasBorder = false;
            //txtCommentHaeder.ReadOnly = true;
            //txtCommentHaeder.Font.SynthesizedBold = true;
            //txtCommentHaeder.FontSize = _fontSize;
            //txtCommentHaeder.Text = CultRes.StringsRes.ReportComments;
            //_pdfPointY += _pdfSizeHeight;

            _n = "comment";
            _z = 1;
            _z1 = _n + _z;
            //---------------------------------------------------------------------
            foreach (var t in commentsList)
            {
                var t1 = t.Info;
                var t2 = t.Memo;

                _arrHelperInts = PdfHelper.PdfTbLeft("comment" + _z1, _arrHelperInts, CultRes.StringsRes.ReportInfo);
                _arrHelperInts = PdfHelper.PdfTbRight(_z1, _arrHelperInts, t1);

            //    var txtCommentLeft = _page.AddTextBox("comment" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
            //    new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            //txtCommentLeft.HasBorder = false;
            //txtCommentLeft.ReadOnly = true;
            //txtCommentLeft.Font.SynthesizedBold = false;
            //txtCommentLeft.FontSize = _fontSize;
            //txtCommentLeft.Text = CultRes.StringsRes.ReportInfo;

            //var txtCommentRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
            //    new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
            //txtCommentRight.HasBorder = false;
            //txtCommentRight.ReadOnly = true;
            //txtCommentRight.FontSize = _fontSize;
            //txtCommentRight.Text = t1;
            //_pdfPointY += _pdfSizeHeight;

            _z += 1;
            _z1 = _n + _z;
            //---------------------------------------------------------
            ReportIfLeftRightTextBox(CultRes.StringsRes.ReportMemo, t2);

            _arrHelperInts[1] += _arrHelperInts[9] - 3; //Distance to next TextBox

                //var author = PdfHelper.AuthorViewChangeWithString(t.Author, t.AuthorYear);

            }
            //arrayInts[0] = _pdfPointXLeft;
            //arrayInts[1] = _pdfPointY;
            //arrayInts[2] = _pdfPointXRight;
            //arrayInts[3] = _pdfSizeHeight;
            //arrayInts[4] = _pdfSizeWidthLeft;
            //arrayInts[5] = _pdfSizeWidthRight;
            //arrayInts[6] = _pageCount;
            //arrayInts[7] = _move;
            //arrayInts[8] = _characterSize;
            //arrayInts[9] = _fontSize;
            //arrayInts[10] = _z;

            return _arrHelperInts;
        }

        //-----------------------------------------------------------------------------
        public static int[] PdfTbLeft(string tbName, int[] arrInts, string text)
        {
            var txtLeft = _page.AddTextBox(tbName, new PdfPoint(arrInts[0] + arrInts[7], arrInts[1]),
                new PdfSize(arrInts[4], arrInts[3]));
            txtLeft.HasBorder = false;
            txtLeft.ReadOnly = true;
            txtLeft.Font.SynthesizedBold = false;
            txtLeft.FontSize = arrInts[9];
            txtLeft.Text = text;

            return arrInts;
        }
        public static int[] PdfTbBoldLeft(string tbName, int[] arrInts, string text, int fontPlus)
        {
            var txtLeft = _page.AddTextBox(tbName, new PdfPoint(arrInts[0], arrInts[1]),
                new PdfSize(arrInts[4], arrInts[3]));
            txtLeft.HasBorder = false;
            txtLeft.ReadOnly = true;
            txtLeft.Font.SynthesizedBold = true;
            txtLeft.FontSize = arrInts[9] + fontPlus;
            txtLeft.Text = text;
            arrInts[1] += arrInts[3];

            return arrInts;
        }
        public static int[] PdfTbBoldMoveLeft(string tbName, int[] arrInts, string text, int fontPlus)
        {
            var txtLeft = _page.AddTextBox(tbName, new PdfPoint(arrInts[0] + arrInts[7], arrInts[1]),
                new PdfSize(arrInts[4], arrInts[3]));
            txtLeft.HasBorder = false;
            txtLeft.ReadOnly = true;
            txtLeft.Font.SynthesizedBold = true;
            txtLeft.FontSize = arrInts[9] + fontPlus;
            txtLeft.Text = text;
            arrInts[1] += arrInts[3];

            return arrInts;
        }
        public static int[] PdfTbRight(string tbName, int[] arrInts, string text)
        {
            var txtRight = _page.AddTextBox(tbName, new PdfPoint(arrInts[2], arrInts[1]),
                new PdfSize(arrInts[5], arrInts[3]));
            txtRight.HasBorder = false;
            txtRight.ReadOnly = true;
            txtRight.Font.SynthesizedBold = false;
            txtRight.FontSize = arrInts[9];
            txtRight.Text = text;
            arrInts[1] += arrInts[3];

            return arrInts;
        }
        public static int[] PdfTbMtRight(string tbName, int[] arrInts, string text)
        {

            if (text != null)
            {
                var fontHeight = 0;
                for (var i = arrInts[8]; i < text.Length; i += arrInts[8])
                {
                    fontHeight += arrInts[3] + arrInts[9];
                }

                var txtRight = _page.AddTextBox(tbName, new PdfPoint(arrInts[2], arrInts[1]),
                    new PdfSize(arrInts[5], arrInts[3] + fontHeight));
                txtRight.HasBorder = false;
                txtRight.Multiline = true;
                txtRight.ReadOnly = true;
                txtRight.Font.SynthesizedBold = false;
                txtRight.FontSize = arrInts[9];
                txtRight.Text = text;
                arrInts[1] += arrInts[3] + fontHeight - 8;  // -8 Leerzeile
            }
            else
            {
                arrInts[1] += arrInts[3];
            }

            return arrInts;
        }

        //-----------------------------------------------------------------------------
        public void ReportLeftIfRightTextBox(string textLeftSide, string textRightSite)
        {
            var txtLeft = _page.AddTextBox("basic0" + _z1, new PdfPoint(_arrHelperInts[0] + _arrHelperInts[7], _arrHelperInts[1]),
                new PdfSize(_arrHelperInts[4], _arrHelperInts[3]));
            txtLeft.HasBorder = false;
            txtLeft.ReadOnly = true;
            txtLeft.FontSize = _arrHelperInts[3];
            txtLeft.Text = textLeftSide;

            if (textRightSite == " " || textRightSite == null)
            {
                _arrHelperInts[1] += _arrHelperInts[3];
                _z += 1;
                _z1 = _n + _z;
                return;
            }
            
            if (textRightSite != " ")
            {
                var fontHeight = 0;
                for (var i = _arrHelperInts[8]; i < textRightSite.Length; i += _arrHelperInts[8])
                {
                    fontHeight += _arrHelperInts[3] + _arrHelperInts[9];
                }

                var txtRight = _page.AddTextBox(_z1, new PdfPoint(_arrHelperInts[2], _arrHelperInts[1]),
                    new PdfSize(_arrHelperInts[5], _arrHelperInts[3] + fontHeight));
                txtRight.HasBorder = false;
                txtRight.ReadOnly = true;
                txtRight.Multiline = true;
                txtRight.FontSize = _arrHelperInts[9];
                txtRight.Text = textRightSite;
                _arrHelperInts[1] += _arrHelperInts[3] + fontHeight;
            }
            else
            {
                _arrHelperInts[1] += _arrHelperInts[3];
            }
            _z += 1;
            _z1 = _n + _z;
        }
        public void ReportIfLeftRightTextBox(string textLeftSide, string textRightSite)
        {
            if (textRightSite == " " || textRightSite == null)
            {
                _z += 1;
                _z1 = _n + _z;
                return;
            }

            if (textRightSite != " ")
            {
                var txtLeft = _page.AddTextBox("basic1" + _z1, new PdfPoint(_arrHelperInts[0] + _arrHelperInts[7], _arrHelperInts[1]),
                    new PdfSize(_arrHelperInts[4], _arrHelperInts[3]));
                txtLeft.HasBorder = false;
                txtLeft.ReadOnly = true;
                txtLeft.Font.SynthesizedBold = false;
                txtLeft.FontSize = _arrHelperInts[9];
                txtLeft.Text = textLeftSide;

                var fontHeight = 0;
                for (var i = _arrHelperInts[8]; i < textRightSite.Length; i += _arrHelperInts[8])
                {
                    fontHeight += _arrHelperInts[3] + _arrHelperInts[9];
                }

                var txtRight = _page.AddTextBox(_z1, new PdfPoint(_arrHelperInts[2], _arrHelperInts[1]),
                    new PdfSize(_arrHelperInts[5], _arrHelperInts[3] + fontHeight));
                txtRight.HasBorder = false;
                txtRight.ReadOnly = true;
                txtRight.Multiline = true;
                txtRight.FontSize = _arrHelperInts[9];
                txtRight.Text = textRightSite;
                _arrHelperInts[1] += _arrHelperInts[3] + fontHeight; //- 4;
            }
            else
            {
                _arrHelperInts[1] += _arrHelperInts[3];
            }

            _z += 1;
            _z1 = _n + _z;

        }
        public void ReportValidTextBox(string textLeftSide1, string textLeftSide2, string textRightSite1, string textRightSite2)
        {
            if (textRightSite1 == " " || textRightSite1 == null)
            {
                _z += 1;
                _z1 = _n + _z;
                return;
            }

            if (textRightSite1 != " ")
            {
                var txtLeft = _page.AddTextBox("basic1" + _z1, new PdfPoint(_arrHelperInts[0] + _arrHelperInts[7], _arrHelperInts[1]),
                    new PdfSize(_arrHelperInts[4], _arrHelperInts[3]));
                txtLeft.HasBorder = false;
                txtLeft.ReadOnly = true;
                txtLeft.Font.SynthesizedBold = false;
                txtLeft.FontSize = _arrHelperInts[9];
                txtLeft.Text = textLeftSide1 + " / " + textLeftSide2;

                var txtRight = _page.AddTextBox(_z1, new PdfPoint(_arrHelperInts[2], _arrHelperInts[1]),
                    new PdfSize(_arrHelperInts[5], _arrHelperInts[3]));
                txtRight.HasBorder = false;
                txtRight.ReadOnly = true;
       //         txtInfoRight.Multiline = true;
                txtRight.FontSize = _arrHelperInts[9];
                txtRight.Text = textRightSite1 + " " + textRightSite2;
                _arrHelperInts[1] += _arrHelperInts[3]; //- 4;
            }
            else
            {
                _arrHelperInts[1] += _arrHelperInts[3];
            }

            _z += 1;
            _z1 = _n + _z;

        }

        /// <summary>
        /// Change View of Author - (xxx, 1111) or - xxx, 2222
        /// </summary>
        /// <param name = "author" ></ param >
        /// < param name="authorYear"></param>
        /// <returns></returns>
        public static string AuthorViewChangeWithString(string author, string authorYear)
        {
            if (string.IsNullOrEmpty(author)) return "";
            if (author.Contains("("))
            {
                var length = author.Length;
                author = "- " + author.Insert(length - 1, ", " + authorYear);
            }
            else
                author = "- " + author + ", " + authorYear;

            return author.Trim();
        }
        /// <summary>
        /// Change View of Foreign names
        /// </summary>
        /// <param name="gerName"></param>
        /// <param name="engName"></param>
        /// <param name="fraName"></param>
        /// <param name="porName"></param>
        /// <returns></returns>
        public static string NamesViewChange(string gerName, string engName, string fraName, string porName)  
        {
            var names = "";
            if (string.IsNullOrEmpty(gerName) && string.IsNullOrEmpty(engName) && string.IsNullOrEmpty(fraName) && string.IsNullOrEmpty(porName)) return names;
            //------------
            if (!string.IsNullOrEmpty(gerName) && string.IsNullOrEmpty(engName) && string.IsNullOrEmpty(fraName) && string.IsNullOrEmpty(porName))
                names = "- " + gerName;
            if (!string.IsNullOrEmpty(gerName) && !string.IsNullOrEmpty(engName) && string.IsNullOrEmpty(fraName) && string.IsNullOrEmpty(porName))
                names = "- " + gerName + ", " + engName;
            if (!string.IsNullOrEmpty(gerName) && !string.IsNullOrEmpty(engName) && !string.IsNullOrEmpty(fraName) && string.IsNullOrEmpty(porName))
                names = "- " + gerName + ", " + engName + ", " + fraName;
            if (!string.IsNullOrEmpty(gerName) && !string.IsNullOrEmpty(engName) && !string.IsNullOrEmpty(fraName) && !string.IsNullOrEmpty(porName))
                names = "- " + gerName + ", " + engName + ", " + fraName + ", " + porName;
            //------------
            if (!string.IsNullOrEmpty(gerName) && string.IsNullOrEmpty(engName) && !string.IsNullOrEmpty(fraName) && string.IsNullOrEmpty(porName))
                names = "- " + gerName + ", " + fraName;
            if (!string.IsNullOrEmpty(gerName) && string.IsNullOrEmpty(engName) && !string.IsNullOrEmpty(fraName) && !string.IsNullOrEmpty(porName))
                names = "- " + gerName + ", " + fraName + ", " + porName;
            //------------
            if (!string.IsNullOrEmpty(gerName) && string.IsNullOrEmpty(engName) && string.IsNullOrEmpty(fraName) && !string.IsNullOrEmpty(porName))
                names = "- " + gerName + ", " + porName;
            //------------

            if (string.IsNullOrEmpty(gerName) && !string.IsNullOrEmpty(engName) && string.IsNullOrEmpty(fraName) && string.IsNullOrEmpty(porName))
                names = "- " + engName;
            if (string.IsNullOrEmpty(gerName) && !string.IsNullOrEmpty(engName) && !string.IsNullOrEmpty(fraName) && string.IsNullOrEmpty(porName))
                names = "- " + engName + ", " + fraName;
            if (string.IsNullOrEmpty(gerName) && !string.IsNullOrEmpty(engName) && !string.IsNullOrEmpty(fraName) && !string.IsNullOrEmpty(porName))
                names = "- " + engName + ", " + fraName + ", " + porName;
            //------------
            if (string.IsNullOrEmpty(gerName) && string.IsNullOrEmpty(engName) && !string.IsNullOrEmpty(fraName) && string.IsNullOrEmpty(porName))
                names = "- " + fraName;
            if (string.IsNullOrEmpty(gerName) && string.IsNullOrEmpty(engName) && !string.IsNullOrEmpty(fraName) && !string.IsNullOrEmpty(porName))
                names = "- " + fraName + ", " + porName;
            //------------
            if (string.IsNullOrEmpty(gerName) && string.IsNullOrEmpty(engName) && string.IsNullOrEmpty(fraName) && !string.IsNullOrEmpty(porName))
                names = "- " + porName;
            //------------


            return names.Trim();
        }

        // Set up the fonts to be used on the pages

        //private static readonly Font LargeFont = new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD, BaseColor.BLACK);
        //private static readonly Font StandardFont = new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.BLACK);
        //private static readonly Font StandardBoldFont = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK);
        //private static readonly Font SmallFont = new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK);
        //private static readonly Font SmallBoldFont = new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.BLACK);


        //   public static Document HeaderMainPdf(SaveFileDialog sfd)
        //   {
        //       // Initialize the PDF document 
        //       // Set up the fonts to be used on the pages 

        //       //var margin = Utilities.MillimetersToPoints(Convert.ToSingle(5));
        //       //        var doc = new Document(PageSize.A4, margin, margin, margin, margin);
        // //      var doc = new Document(20.ToString());

        //       //PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));

        //       //// Set the margins and page size
        //       //SetStandardPageSize(doc);

        //       //// Open the document for writing content 
        //       //doc.Open();
        ////       return doc;
        //   }


        // <summary>
        // Set margins and page size for the document
        // </summary>
        // <param name="doc"></param>
        //public static void SetStandardPageSize(Document doc)
        //{
        //    // Set margins and page size for the document
        //    doc.SetMargins(10, 10, 10, 0);
        //    // There are a huge number of possible page sizes, including such sizes as
        //    // EXECUTIVE, POSTCARD, LEDGER, LEGAL, LETTER_LANDSCAPE, and NOTE
        //    doc.SetPageSize(new Rectangle(PageSize.LETTER.Width, PageSize.LETTER.Height));
        //}

        // <summary>
        // Add the header page to the document.  This shows an example of a page containing
        // both text and images.  The contents of the page are centered and the text is of
        // various sizes.
        // </summary>
        // <param name="doc"></param>
        //public static void AddReportMain(Document doc)
        //{
        //    // Write page content.  Note the use of fonts and alignment attributes.
        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, new Chunk("\n\n"));
        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, new Chunk(CultRes.StringsRes.Report));
        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, new Chunk("\n\n"));
        //    AddParagraph(doc, Element.ALIGN_CENTER, StandardFont, new Chunk(" Rudolf Terppé"));
        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, new Chunk("\n\n\n"));

        //    // Add a logo
        //    //    var appPath = Directory.GetCurrentDirectory();
        //    //    var logoImage = Image.GetInstance(appPath + "\\thT7UR41DT.jpg");
        //    //    logoImage.Alignment = Element.ALIGN_CENTER;
        //    //    doc.Add(logoImage);
        //    //    logoImage = null;

        //    // Write additional page content
        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, new Chunk("\n\n\n"));
        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, new Chunk(CultRes.StringsRes.ReportTitle));
        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, new Chunk("\n\n\n\n\n"));
        //    AddParagraph(doc, Element.ALIGN_CENTER, SmallFont, new Chunk(CultRes.StringsRes.ReportGenerated +
        //       DateTime.Now.Day + " " +
        //       CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month) + " " +
        //       DateTime.Now.Year + " " +
        //       DateTime.Now.ToShortTimeString()));
        //}

        // <summary>
        // Add the header page to the document.  This shows an example of a page containing
        // both text and images.  The contents of the page are centered and the text is of
        // various sizes.
        // </summary>
        // <param name="doc"></param>
        //public static void AddReportListMain(Document doc)
        //{
        //    // Write page content.  Note the use of fonts and alignment attributes.
        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, new Chunk("\n"));
        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, new Chunk(CultRes.StringsRes.List));
        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, new Chunk("\n"));
        //    AddParagraph(doc, Element.ALIGN_CENTER, StandardFont, new Chunk(" Rudolf Terppé"));
        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, new Chunk("\n\n"));

        //    // Add a logo
        //    //     var appPath = Directory.GetCurrentDirectory();
        //    //     var logoImage = Image.GetInstance(appPath + "\\thT7UR41DT.jpg");
        //    //     logoImage.Alignment = Element.ALIGN_CENTER;
        //    //     doc.Add(logoImage);
        //    //     logoImage = null;

        //    // Write additional page content
        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, new Chunk("\n"));
        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, new Chunk(CultRes.StringsRes.ReportTitle));
        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, new Chunk("\n"));
        //    AddParagraph(doc, Element.ALIGN_CENTER, SmallFont, new Chunk(CultRes.StringsRes.ReportGenerated +
        //                                                                 DateTime.Now.Day + " " +
        //                                                                 CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month) + " " +
        //                                                                 DateTime.Now.Year + " " +
        //                                                                 DateTime.Now.ToShortTimeString()));
        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, new Chunk("\n"));
        //}





        /// <summary>
        /// Change View of Author (xxx, 1111) or xxx, 2222
        /// </summary>
        /// <param name="author"></param>
        /// <param name="authorYear"></param>
        /// <returns></returns>
        //public static string AuthorViewChangeWithoutString(string author, string authorYear)
        //{
        //    if (author.IsBlank()) return "";
        //    if (author.Contains("("))
        //    {
        //        var length = author.Length;
        //        author = author.Insert(length - 1, ", " + authorYear);
        //    }
        //    else
        //        author = author + ", " + authorYear;

        //    return author;
        //}



    }
}
