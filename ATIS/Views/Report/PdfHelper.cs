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
        private static string _n;
        private static int _z;
        private static string _z1;

        private static int _pdfPointXLeft;
        private static int _pdfPointY;
        private static int _pdfPointXRight;
        private static int _pdfSizeHeight;
        private static int _pdfSizeWidthLeft;
        private static int _pdfSizeWidthRight;
        private static int _pageCount;
        private static int _move;
        private static int _characterSize;
        private static int _fontSize;

        private static PdfPage _page;

        public int[] AddReportMain(PdfDocument pdf, Tbl03Regnum regnumList, int[] arreySize)
        {
            _pdfPointXLeft = arreySize[0];
            _pdfPointY = arreySize[1];
            _pdfPointXRight = arreySize[2];
            _pdfSizeHeight = arreySize[3];
            _pdfSizeWidthLeft = arreySize[4];
            _pdfSizeWidthRight = arreySize[5];
            _pageCount = arreySize[6];

            _page = pdf.Pages[_pageCount];

            var txtHeader = _page.AddTextBox("header", new PdfPoint(_pdfPointXLeft, _pdfPointY), 
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtHeader.HasBorder = false;
            txtHeader.ReadOnly = true;
            txtHeader.Font.SynthesizedBold = true;
            txtHeader.FontSize = 16;
            txtHeader.Height = 20;
            txtHeader.Text = CultRes.StringsRes.Report;

            _pdfPointY += _pdfSizeHeight;
            _pdfPointY += 20; //Distance to next TextBox

            arreySize[0] = _pdfPointXLeft; 
            arreySize[1] = _pdfPointY; 
            arreySize[2] = _pdfPointXRight; 
            arreySize[3] = _pdfSizeHeight; 
            arreySize[4] = _pdfSizeWidthLeft; 
            arreySize[5] = _pdfSizeWidthRight;
            arreySize[6] = _pageCount; 

            return arreySize;
        }

        public int[] AddReferencesHaeder(PdfDocument pdf, int[] arreySize)
        {
            _pdfPointXLeft = arreySize[0];
            _pdfPointY = arreySize[1];
            _pdfPointXRight = arreySize[2];
            _pdfSizeHeight = arreySize[3];
            _pdfSizeWidthLeft = arreySize[4];
            _pdfSizeWidthRight = arreySize[5];
            _pageCount = arreySize[6];

            _page = pdf.Pages[_pageCount];

            //pdf.AddPage();
            //_page = pdf.Pages[_pageCount + 1];
            //_pdfPointY = 5;

            var txtRefHeader = _page.AddTextBox("references", new PdfPoint(_pdfPointXLeft, _pdfPointY),
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtRefHeader.HasBorder = false;
            txtRefHeader.ReadOnly = true;
            txtRefHeader.Font.SynthesizedBold = true;
            txtRefHeader.FontSize = 10;
            txtRefHeader.Text = CultRes.StringsRes.ReportReferences;
            _pdfPointY += _pdfSizeHeight;

            _pdfPointY += 5; //Distance to next TextBox

            arreySize[0] = _pdfPointXLeft;
            arreySize[1] = _pdfPointY;
            arreySize[2] = _pdfPointXRight;
            arreySize[3] = _pdfSizeHeight;
            arreySize[4] = _pdfSizeWidthLeft;
            arreySize[5] = _pdfSizeWidthRight;
            arreySize[6] = _pageCount;

            return arreySize;
        }

        public int[] AddRefExpertsList(PdfDocument pdf, ObservableCollection<Tbl90Reference> expertsList, int[] arreySize)
        {
            _pdfPointXLeft = arreySize[0];
            _pdfPointY = arreySize[1];
            _pdfPointXRight = arreySize[2];
            _pdfSizeHeight = arreySize[3];
            _pdfSizeWidthLeft = arreySize[4];
            _pdfSizeWidthRight = arreySize[5];
            _pageCount = arreySize[6];
            _move = arreySize[7];
            _characterSize = arreySize[8];
            _fontSize = arreySize[9];

            _page = pdf.Pages[_pageCount];

            //pdf.AddPage();
            //_page = pdf.Pages[_pageCount + 1];
            //_pdfPointY = 5;

            var txtExpertNameLeft = _page.AddTextBox("experts", new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtExpertNameLeft.HasBorder = false;
            txtExpertNameLeft.ReadOnly = true;
            txtExpertNameLeft.Font.SynthesizedBold = true;
            txtExpertNameLeft.FontSize = _fontSize;
            txtExpertNameLeft.Text = CultRes.StringsRes.ReportExperts;
            _pdfPointY += _pdfSizeHeight;

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

                var txtSourceLeft = _page.AddTextBox("expert" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                    new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                txtSourceLeft.HasBorder = false;
                txtSourceLeft.ReadOnly = true;
                txtSourceLeft.Font.SynthesizedBold = false;
                txtSourceLeft.FontSize = _fontSize;
                txtSourceLeft.Text = CultRes.StringsRes.ReportExpert;

                var txtSourceRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                    new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
                txtSourceRight.HasBorder = false;
                txtSourceRight.ReadOnly = true;
                txtSourceRight.FontSize = _fontSize;
                txtSourceRight.Text = u1;
                _pdfPointY += _pdfSizeHeight;

                _z += 1;
                _z1 = _n + _z;
                //--------------------------------------------------
                ReportLeftIfRightTextBox(CultRes.StringsRes.ReportNotes, u2);
                ReportIfLeftRightTextBox(CultRes.StringsRes.ReportInfo, u3);
                ReportIfLeftRightTextBox(CultRes.StringsRes.ReportMemo, u4);
                ReportValidTextBox(CultRes.StringsRes.ReportValid, CultRes.StringsRes.ReportValidYear, u5, u6);
                ReportLeftIfRightTextBox(CultRes.StringsRes.ReportRefFor, u7);
                ReportIfLeftRightTextBox(CultRes.StringsRes.ReportMemo, u8);

                _pdfPointY += 10; //Abstand zum nächsten Datensatz

                //var author = PdfHelper.AuthorViewChangeWithString(t.Author, t.AuthorYear);

                
            }

            arreySize[0] = _pdfPointXLeft;
            arreySize[1] = _pdfPointY;
            arreySize[2] = _pdfPointXRight;
            arreySize[3] = _pdfSizeHeight;
            arreySize[4] = _pdfSizeWidthLeft;
            arreySize[5] = _pdfSizeWidthRight;
            arreySize[6] = _pageCount;
            arreySize[7] = _move;
            arreySize[8] = _characterSize;
            arreySize[9] = _fontSize;

            return arreySize;
        }

        public int[] AddRefSourcesList(PdfDocument pdf, ObservableCollection<Tbl90Reference> sourcesList, int[] arreySize)
        {
            _pdfPointXLeft = arreySize[0];
            _pdfPointY = arreySize[1];
            _pdfPointXRight = arreySize[2];
            _pdfSizeHeight = arreySize[3];
            _pdfSizeWidthLeft = arreySize[4];
            _pdfSizeWidthRight = arreySize[5];
            _pageCount = arreySize[6];
            _move = arreySize[7];
            _characterSize = arreySize[8];
            _fontSize = arreySize[9];

            _page = pdf.Pages[_pageCount];

            //pdf.AddPage();
            //_page = pdf.Pages[_pageCount + 1];
            //_pdfPointY = 5;

            var txtExpertNameLeft = _page.AddTextBox("sources", new PdfPoint(_pdfPointXLeft + _move, _pdfPointY), 
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtExpertNameLeft.HasBorder = false;
            txtExpertNameLeft.ReadOnly = true;
            txtExpertNameLeft.Font.SynthesizedBold = true;
            txtExpertNameLeft.FontSize = _fontSize;
            txtExpertNameLeft.Text = CultRes.StringsRes.ReportOtherSources;
            _pdfPointY += _pdfSizeHeight;

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

                var txtSourceLeft = _page.AddTextBox("source" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                    new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                txtSourceLeft.HasBorder = false;
                txtSourceLeft.ReadOnly = true;
                txtSourceLeft.Font.SynthesizedBold = false;
                txtSourceLeft.FontSize = _fontSize;
                txtSourceLeft.Text = CultRes.StringsRes.ReportSource;

                var txtSourceRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                    new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
                txtSourceRight.HasBorder = false;
                txtSourceRight.ReadOnly = true;
                txtSourceRight.FontSize = _fontSize;
                txtSourceRight.Text = u1;
                _pdfPointY += _pdfSizeHeight;

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

                _pdfPointY += 10; //Abstand zum nächsten Datensatz

                //var author = PdfHelper.AuthorViewChangeWithString(t.Author, t.AuthorYear);
            }
            arreySize[0] = _pdfPointXLeft;
            arreySize[1] = _pdfPointY;
            arreySize[2] = _pdfPointXRight;
            arreySize[3] = _pdfSizeHeight;
            arreySize[4] = _pdfSizeWidthLeft;
            arreySize[5] = _pdfSizeWidthRight;
            arreySize[6] = _pageCount;
            arreySize[7] = _move;
            arreySize[8] = _characterSize;
            arreySize[9] = _fontSize;

            return arreySize;
        }

        public int[] AddRefAuthorsList(PdfDocument pdf, ObservableCollection<Tbl90Reference> authorsList, int[] arreySize)
        {
            _pdfPointXLeft = arreySize[0];
            _pdfPointY = arreySize[1];
            _pdfPointXRight = arreySize[2];
            _pdfSizeHeight = arreySize[3];
            _pdfSizeWidthLeft = arreySize[4];
            _pdfSizeWidthRight = arreySize[5];
            _pageCount = arreySize[6];
            _move = arreySize[7];
            _characterSize = arreySize[8];
            _fontSize = arreySize[9];

            _page = pdf.Pages[_pageCount];

            //pdf.AddPage();
            //_page = pdf.Pages[_pageCount + 1];
            //_pdfPointY = 5;

            var txtExpertNameLeft = _page.AddTextBox("authors", new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtExpertNameLeft.HasBorder = false;
            txtExpertNameLeft.ReadOnly = true;
            txtExpertNameLeft.Font.SynthesizedBold = true;
            txtExpertNameLeft.FontSize = _fontSize;
            txtExpertNameLeft.Text = CultRes.StringsRes.ReportPublications;
            _pdfPointY += _pdfSizeHeight;

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


                var txtSourceLeft = _page.AddTextBox("authors" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                    new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                txtSourceLeft.HasBorder = false;
                txtSourceLeft.ReadOnly = true;
                txtSourceLeft.Font.SynthesizedBold = false;
                txtSourceLeft.FontSize = _fontSize;
                txtSourceLeft.Text = CultRes.StringsRes.ReportAuthorsEditors;

                var txtSourceRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                    new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
                txtSourceRight.HasBorder = false;
                txtSourceRight.ReadOnly = true;
                txtSourceRight.FontSize = _fontSize;
                txtSourceRight.Text = t1;
                _pdfPointY += _pdfSizeHeight;

                _z += 1;
                _z1 = _n + _z;
                //--------------------------------------------------

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

                _pdfPointY += 10; //Abstand zum nächsten Datensatz

                //var author = PdfHelper.AuthorViewChangeWithString(t.Author, t.AuthorYear);

            }
            arreySize[0] = _pdfPointXLeft;
            arreySize[1] = _pdfPointY;
            arreySize[2] = _pdfPointXRight;
            arreySize[3] = _pdfSizeHeight;
            arreySize[4] = _pdfSizeWidthLeft;
            arreySize[5] = _pdfSizeWidthRight;
            arreySize[6] = _pageCount;
            arreySize[7] = _move;
            arreySize[8] = _characterSize;
            arreySize[9] = _fontSize;

            return arreySize;
        }

        public int[] AddCommentsHaeder(PdfDocument pdf, int[] arreySize)
        {
            _pdfPointXLeft = arreySize[0];
            _pdfPointY = arreySize[1];
            _pdfPointXRight = arreySize[2];
            _pdfSizeHeight = arreySize[3];
            _pdfSizeWidthLeft = arreySize[4];
            _pdfSizeWidthRight = arreySize[5];
            _pageCount = arreySize[6];
                 _move = arreySize[7];

                 _page = pdf.Pages[_pageCount];

            //pdf.AddPage();
            //_page = pdf.Pages[_pageCount + 1];
            //_pdfPointY = 5;


            var txtCommHeader = _page.AddTextBox("comments", new PdfPoint(_pdfPointXLeft, _pdfPointY),
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight + 16));
            txtCommHeader.HasBorder = false;
            txtCommHeader.ReadOnly = true;
            txtCommHeader.Font.SynthesizedBold = true;
            txtCommHeader.FontSize = 16;
            txtCommHeader.Text = CultRes.StringsRes.ReportComments;
            _pdfPointY += _pdfSizeHeight;

            arreySize[0] = _pdfPointXLeft;
            arreySize[1] = _pdfPointY;
            arreySize[2] = _pdfPointXRight;
            arreySize[3] = _pdfSizeHeight;
            arreySize[4] = _pdfSizeWidthLeft;
            arreySize[5] = _pdfSizeWidthRight;
            arreySize[6] = _pageCount;
                 arreySize[7] = _move; //_move

            return arreySize;

        }

        public int[] AddCommentsList(PdfDocument pdf, ObservableCollection<Tbl93Comment> commentsList, int[] arreySize)
        {
            _pdfPointXLeft = arreySize[0];
            _pdfPointY = arreySize[1];
            _pdfPointXRight = arreySize[2];
            _pdfSizeHeight = arreySize[3];
            _pdfSizeWidthLeft = arreySize[4];
            _pdfSizeWidthRight = arreySize[5];
            _pageCount = arreySize[6];
            _move = arreySize[7];
            _characterSize = arreySize[8];
            _fontSize = arreySize[9];

            _page = pdf.Pages[_pageCount];

            //pdf.AddPage();
            //_page = pdf.Pages[_pageCount + 1];
            //_pdfPointY = 5;

            var txtExpertNameLeft = _page.AddTextBox("comments", new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtExpertNameLeft.HasBorder = false;
            txtExpertNameLeft.ReadOnly = true;
            txtExpertNameLeft.Font.SynthesizedBold = true;
            txtExpertNameLeft.FontSize = _fontSize;
            txtExpertNameLeft.Text = CultRes.StringsRes.ReportComments;
            _pdfPointY += _pdfSizeHeight;

            _n = "comment";
            _z = 1;
            _z1 = _n + _z;
            //---------------------------------------------------------------------
            foreach (var t in commentsList)
            {
                var t1 = t.Info;
                var t2 = t.Memo;

            
            var txtSourceLeft = _page.AddTextBox("comment" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtSourceLeft.HasBorder = false;
            txtSourceLeft.ReadOnly = true;
            txtSourceLeft.Font.SynthesizedBold = false;
            txtSourceLeft.FontSize = _fontSize;
            txtSourceLeft.Text = CultRes.StringsRes.ReportInfo;

            var txtSourceRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
            txtSourceRight.HasBorder = false;
            txtSourceRight.ReadOnly = true;
            txtSourceRight.FontSize = _fontSize;
            txtSourceRight.Text = t1;
            _pdfPointY += _pdfSizeHeight;

            _z += 1;
            _z1 = _n + _z;
            //---------------------------------------------------------
            ReportIfLeftRightTextBox(CultRes.StringsRes.ReportMemo, t2);
 
            _pdfPointY += 10; //Abstand zum nächsten Datensatz

            //var author = PdfHelper.AuthorViewChangeWithString(t.Author, t.AuthorYear);

        }
        arreySize[0] = _pdfPointXLeft;
        arreySize[1] = _pdfPointY;
        arreySize[2] = _pdfPointXRight;
        arreySize[3] = _pdfSizeHeight;
        arreySize[4] = _pdfSizeWidthLeft;
        arreySize[5] = _pdfSizeWidthRight;
        arreySize[6] = _pageCount;
        arreySize[7] = _move;
        arreySize[8] = _characterSize;
        arreySize[9] = _fontSize;

        return arreySize;
        }


        public void ReportLeftIfRightTextBox(string textLeftSide, string textRightSite)
        {
            var txtLeft = _page.AddTextBox("basic0" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtLeft.HasBorder = false;
            txtLeft.ReadOnly = true;
            txtLeft.FontSize = _pdfSizeHeight;
            txtLeft.Text = textLeftSide;

            if (textRightSite == " " || textRightSite == null)
            {
                _pdfPointY += _pdfSizeHeight;
                _z += 1;
                _z1 = _n + _z;
                return;
            }
            
            if (textRightSite != " ")
            {
                var fontHeight = 0;
                for (var i = _characterSize; i < textRightSite.Length; i += _characterSize)
                {
                    fontHeight += _pdfSizeHeight + 8;
                }

                var txtNoteRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                    new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight + fontHeight));
                txtNoteRight.HasBorder = false;
                txtNoteRight.ReadOnly = true;
                txtNoteRight.Multiline = true;
                txtNoteRight.FontSize = _pdfSizeHeight;
                txtNoteRight.Text = textRightSite;
                _pdfPointY += _pdfSizeHeight + fontHeight;
            }
            else
            {
                _pdfPointY += _pdfSizeHeight;
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
                var txtInfoLeft = _page.AddTextBox("basic1" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                    new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                txtInfoLeft.HasBorder = false;
                txtInfoLeft.ReadOnly = true;
                txtInfoLeft.Font.SynthesizedBold = false;
                txtInfoLeft.FontSize = _pdfSizeHeight;
                txtInfoLeft.Text = textLeftSide;

                var fontHeight = 0;
                for (int i = _characterSize; i < textRightSite.Length; i += _characterSize)
                {
                    fontHeight += _pdfSizeHeight + 8;
                }

                var txtInfoRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                    new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight + fontHeight));
                txtInfoRight.HasBorder = false;
                txtInfoRight.ReadOnly = true;
                txtInfoRight.Multiline = true;
                txtInfoRight.FontSize = _pdfSizeHeight;
                txtInfoRight.Text = textRightSite;
                _pdfPointY += _pdfSizeHeight + fontHeight; //- 4;
            }
            else
            {
                _pdfPointY += _pdfSizeHeight;
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
                var txtInfoLeft = _page.AddTextBox("basic1" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                    new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                txtInfoLeft.HasBorder = false;
                txtInfoLeft.ReadOnly = true;
                txtInfoLeft.Font.SynthesizedBold = false;
                txtInfoLeft.FontSize = _pdfSizeHeight;
                txtInfoLeft.Text = textLeftSide1 + " / " + textLeftSide2;

                //var fontHeight = 0;
                //for (int i = _characterSize; i < textRightSite1.Length; i += _characterSize)
                //{
                //    fontHeight += _pdfSizeHeight;
                //}

                var txtInfoRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                    new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
                txtInfoRight.HasBorder = false;
                txtInfoRight.ReadOnly = true;
                txtInfoRight.Multiline = true;
                txtInfoRight.FontSize = _pdfSizeHeight;
                txtInfoRight.Text = textRightSite1 + " " + textRightSite2;
                _pdfPointY += _pdfSizeHeight; //- 4;
            }
            else
            {
                _pdfPointY += _pdfSizeHeight;
            }

            _z += 1;
            _z1 = _n + _z;

        }

        /// <summary>
        /// Change View of Author - (xxx, 1111) or - xxx, 2222
        /// </summary>
        /// <param name="author"></param>
        /// <param name="authorYear"></param>
        /// <returns></returns>
        //public static string AuthorViewChangeWithString(string author, string authorYear)
        //{
        //    if (author.IsBlank()) return "";
        //    if (author.Contains("("))
        //    {
        //        var length = author.Length;
        //        author = "- " + author.Insert(length - 1, ", " + authorYear);
        //    }
        //    else
        //        author = "- " + author + ", " + authorYear;

        //    return author.Trim();
        //}

        //public static string NamesViewChange(string gerName, string engName, string fraName, string porName)
        //{
        //    var names = "";
        //    if (gerName.IsBlank() && engName.IsBlank() && fraName.IsBlank() && porName.IsBlank()) return names;
        //    //------------
        //    if (!gerName.IsBlank() && engName.IsBlank() && fraName.IsBlank() && porName.IsBlank())
        //        names = "- " + gerName;
        //    if (!gerName.IsBlank() && !engName.IsBlank() && fraName.IsBlank() && porName.IsBlank())
        //        names = "- " + gerName + ", " + engName;
        //    if (!gerName.IsBlank() && !engName.IsBlank() && !fraName.IsBlank() && porName.IsBlank())
        //        names = "- " + gerName + ", " + engName + ", " + fraName;
        //    if (!gerName.IsBlank() && !engName.IsBlank() && !fraName.IsBlank() && !porName.IsBlank())
        //        names = "- " + gerName + ", " + engName + ", " + fraName + ", " + porName;
        //    //------------
        //    if (!gerName.IsBlank() && engName.IsBlank() && !fraName.IsBlank() && porName.IsBlank())
        //        names = "- " + gerName + ", " + fraName;
        //    if (!gerName.IsBlank() && engName.IsBlank() && !fraName.IsBlank() && !porName.IsBlank())
        //        names = "- " + gerName + ", " + fraName + ", " + porName;
        //    //------------
        //    if (!gerName.IsBlank() && engName.IsBlank() && fraName.IsBlank() && !porName.IsBlank())
        //        names = "- " + gerName + ", " + porName;
        //    //------------

        //    if (gerName.IsBlank() && !engName.IsBlank() && fraName.IsBlank() && porName.IsBlank())
        //        names = "- " + engName;
        //    if (gerName.IsBlank() && !engName.IsBlank() && !fraName.IsBlank() && porName.IsBlank())
        //        names = "- " + engName + ", " + fraName;
        //    if (gerName.IsBlank() && !engName.IsBlank() && !fraName.IsBlank() && !porName.IsBlank())
        //        names = "- " + engName + ", " + fraName + ", " + porName;
        //    //------------
        //    if (gerName.IsBlank() && engName.IsBlank() && !fraName.IsBlank() && porName.IsBlank())
        //        names = "- " + fraName;
        //    if (gerName.IsBlank() && engName.IsBlank() && !fraName.IsBlank() && !porName.IsBlank())
        //        names = "- " + fraName + ", " + porName;
        //    //------------
        //    if (gerName.IsBlank() && engName.IsBlank() && fraName.IsBlank() && !porName.IsBlank())
        //        names = "- " + porName;
        //    //------------


        //    return names.Trim();
        //}

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
