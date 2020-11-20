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

            pdf.AddPage();
            _page = pdf.Pages[_pageCount + 1];
            _pdfPointY = 5;

            var txtExpertNameLeft = _page.AddTextBox("experts", new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtExpertNameLeft.HasBorder = false;
            txtExpertNameLeft.ReadOnly = true;
            txtExpertNameLeft.Font.SynthesizedBold = true;
            txtExpertNameLeft.FontSize = 8;
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
                txtSourceLeft.FontSize = 8;
                txtSourceLeft.Text = CultRes.StringsRes.ReportExperts;

                var txtSourceRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                    new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
                txtSourceRight.HasBorder = false;
                txtSourceRight.ReadOnly = true;
                txtSourceRight.FontSize = 8;
                txtSourceRight.Text = u1;
                _pdfPointY += _pdfSizeHeight;

                _z += 1;
                _z1 = _n + _z;
                //--------------------------------------------------
                var txtNotesLeft = _page.AddTextBox("notes" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                    new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                txtNotesLeft.HasBorder = false;
                txtNotesLeft.ReadOnly = true;
                txtNotesLeft.FontSize = 8;
                txtNotesLeft.Text = CultRes.StringsRes.ReportNotes;

                if (u2 != " ")
                {
                    var u2All = 0;
                    for (var i = 100; i < u2.Length; i += 100)
                    {
                        u2All += 16;
                    }

                    var txtNoteRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                        new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight + u2All));
                    txtNoteRight.HasBorder = false;
                    txtNoteRight.ReadOnly = true;
                    txtNoteRight.Multiline = true;
                    txtNoteRight.FontSize = 8;
                    txtNoteRight.Text = u2;
                    _pdfPointY += _pdfSizeHeight + u2All - 4;
                }
                else
                {
                    _pdfPointY += _pdfSizeHeight;
                }

                _z += 1;
                _z1 = _n + _z;
                //--------------------------------------------------------

                if (u3 != null)
                {
                    var txtInfoLeft = _page.AddTextBox("info" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                        new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                    txtInfoLeft.HasBorder = false;
                    txtInfoLeft.ReadOnly = true;
                    txtInfoLeft.Font.SynthesizedBold = false;
                    txtInfoLeft.FontSize = 8;
                    txtInfoLeft.Text = CultRes.StringsRes.ReportInfo;

                    var u3All = 0;
                    for (int i = 100; i < u3.Length; i += 100)
                    {
                        u3All += 16;
                    }

                    var txtInfoRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                        new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight + u3All));
                    txtInfoRight.HasBorder = false;
                    txtInfoRight.ReadOnly = true;
                    txtInfoRight.Multiline = true;
                    txtInfoRight.FontSize = 8;
                    txtInfoRight.Text = u3;
                    _pdfPointY += _pdfSizeHeight + u3All - 4;
                }
                else
                {
                    _pdfPointY += _pdfSizeHeight;
                }

                _z += 1;
                _z1 = _n + _z;
                //--------------------------------------------------------------------------------
                if (u4 != null)
                {
                    var txtMemoLeft = _page.AddTextBox("memo" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                        new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                    txtMemoLeft.HasBorder = false;
                    txtMemoLeft.ReadOnly = true;
                    txtMemoLeft.Font.SynthesizedBold = false;
                    txtMemoLeft.FontSize = 8;
                    txtMemoLeft.Text = CultRes.StringsRes.ReportMemo;

                    var u4All = 0;
                    for (int i = 100; i < u6.Length; i += 100)
                    {
                        u4All += 16;
                    }

                    var txtMemoRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                        new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight + u4All));
                    txtMemoRight.HasBorder = false;
                    txtMemoRight.ReadOnly = true;
                    txtMemoRight.Multiline = true;
                    txtMemoRight.FontSize = 8;
                    txtMemoRight.Text = u4;
                    _pdfPointY += _pdfSizeHeight + u4All - 4;
                }
                else
                {
                    _pdfPointY += _pdfSizeHeight;
                }

                _z += 1;
                _z1 = _n + _z;
                //--------------------------------------------------------------------------
                if (u5 != null)
                {
                    var txtValidLeft = _page.AddTextBox("Valid" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                        new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                    txtValidLeft.HasBorder = false;
                    txtValidLeft.ReadOnly = true;
                    txtValidLeft.Font.SynthesizedBold = false;
                    txtValidLeft.FontSize = 8;
                    txtValidLeft.Text = CultRes.StringsRes.ReportValid;

                    var txtValidRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                        new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
                    txtValidRight.HasBorder = false;
                    txtValidRight.ReadOnly = true;
                    txtValidRight.FontSize = 8;
                    txtValidRight.Text = u5;
                    _pdfPointY += _pdfSizeHeight;
                }
                else
                {
                    _pdfPointY += _pdfSizeHeight;
                }
                _z += 1;
                _z1 = _n + _z;
                //------------------------------------------------------------
                if (u6 != null)
                {
                    var txtValidYearLeft = _page.AddTextBox("validYear" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                        new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                    txtValidYearLeft.HasBorder = false;
                    txtValidYearLeft.ReadOnly = true;
                    txtValidYearLeft.Font.SynthesizedBold = false;
                    txtValidYearLeft.FontSize = 8;
                    txtValidYearLeft.Text = CultRes.StringsRes.ReportValidYear;

                    var txtValidYearRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                        new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
                    txtValidYearRight.HasBorder = false;
                    txtValidYearRight.ReadOnly = true;
                    txtValidYearRight.FontSize = 8;
                    txtValidYearRight.Text = u6;
                    _pdfPointY += _pdfSizeHeight;
                }
                else
                {
                    _pdfPointY += _pdfSizeHeight;
                }
                _z += 1;
                _z1 = _n + _z;
                //-------------------------------------------------------------------
                var txtInfo1Left = _page.AddTextBox("RefFor" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                    new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                txtInfo1Left.HasBorder = false;
                txtInfo1Left.ReadOnly = true;
                txtInfo1Left.Font.SynthesizedBold = false;
                txtInfo1Left.FontSize = 8;
                txtInfo1Left.Text = CultRes.StringsRes.ReportRefFor;

                if (u7 != " ")
                {
                    var u7All = 0;
                    for (int i = 100; i < u7.Length; i += 100)
                    {
                        u7All += 16;
                    }

                    var txtInfo1Right = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                        new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight + u7All));
                    txtInfo1Right.HasBorder = false;
                    txtInfo1Right.ReadOnly = true;
                    txtInfo1Right.Multiline = true;
                    txtInfo1Right.FontSize = 8;
                    txtInfo1Right.Text = u7;
                    _pdfPointY += _pdfSizeHeight + u7All;
                }
                else
                {
                    _pdfPointY += _pdfSizeHeight;
                }

                _z += 1;
                _z1 = _n + _z;
                //-------------------------------------------------------------------
                if (u8 != null)
                {
                    var txtMemo1Left = _page.AddTextBox("memo" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                        new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                    txtMemo1Left.HasBorder = false;
                    txtMemo1Left.ReadOnly = true;
                    txtMemo1Left.Font.SynthesizedBold = false;
                    txtMemo1Left.FontSize = 8;
                    txtMemo1Left.Text = CultRes.StringsRes.ReportMemo;

                    var uAll = 0;
                    for (var i = 100; i < u8.Length; i += 100)
                    {
                        uAll += 16;
                    }

                    var txtMemo1Right = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                        new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight + uAll));
                    txtMemo1Right.HasBorder = false;
                    txtMemo1Right.ReadOnly = true;
                    txtMemo1Right.FontSize = 8;
                    txtMemo1Right.Multiline = true;
                    txtMemo1Right.Text = u8;
                    _pdfPointY += _pdfSizeHeight + uAll;

                }
                else
                {
                    _pdfPointY += _pdfSizeHeight;
                }

                _z += 1;
                _z1 = _n + _z;
                //-------------------------------------------------------------------

                _pdfPointY += 10; //Abstand zum nächsten Datensatz

            }

            arreySize[0] = _pdfPointXLeft;
            arreySize[1] = _pdfPointY;
            arreySize[2] = _pdfPointXRight;
            arreySize[3] = _pdfSizeHeight;
            arreySize[4] = _pdfSizeWidthLeft;
            arreySize[5] = _pdfSizeWidthRight;
            arreySize[6] = _pageCount; 
            arreySize[7] = _move; 

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

            pdf.AddPage();
            _page = pdf.Pages[_pageCount + 1];
            _pdfPointY = 5;

            var txtExpertNameLeft = _page.AddTextBox("sources", new PdfPoint(_pdfPointXLeft + _move, _pdfPointY), 
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtExpertNameLeft.HasBorder = false;
            txtExpertNameLeft.ReadOnly = true;
            txtExpertNameLeft.Font.SynthesizedBold = true;
            txtExpertNameLeft.FontSize = 8;
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
                txtSourceLeft.FontSize = 8;
                txtSourceLeft.Text = CultRes.StringsRes.ReportSource;

                var txtSourceRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                    new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
                txtSourceRight.HasBorder = false;
                txtSourceRight.ReadOnly = true;
                txtSourceRight.FontSize = 8;
                txtSourceRight.Text = u1;
                _pdfPointY += _pdfSizeHeight;

                _z += 1;
                _z1 = _n + _z;
                //--------------------------------------------------
                //  TbRegister(u2);
                ReportLeftIfRightTextBox(CultRes.StringsRes.ReportAcquired, u2);
                ReportLeftIfRightTextBox(CultRes.StringsRes.ReportNotes, u3);
                ReportIfLeftRightTextBox(CultRes.StringsRes.ReportAuthor, u4);
                ReportIfLeftRightTextBox(CultRes.StringsRes.ReportInfo, u5);
                ReportIfLeftRightTextBox(CultRes.StringsRes.ReportMemo, u6); 
                ReportValidTextBox(CultRes.StringsRes.ReportValid, CultRes.StringsRes.ReportValidYear, u7, u8);
                //ReportIfLeftRightTextBox(CultRes.StringsRes.ReportValid, u7);
                //ReportIfLeftRightTextBox(CultRes.StringsRes.ReportValidYear, u8);
                ReportLeftIfRightTextBox(CultRes.StringsRes.ReportRefFor, u9);
                ReportIfLeftRightTextBox(CultRes.StringsRes.ReportMemo, u10);

                //var txtErfasstLeft = _page.AddTextBox("erfasst" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY), 
                //    new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                //txtErfasstLeft.HasBorder = false;
                //txtErfasstLeft.ReadOnly = true;
                //txtErfasstLeft.Font.SynthesizedBold = false;
                //txtErfasstLeft.FontSize = 8;
                //txtErfasstLeft.Text = CultRes.StringsRes.ReportAcquired;

                //if (u2 != string.Empty)
                //{
                //    var txtErfasstRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                //        new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
                //    txtErfasstRight.HasBorder = false;
                //    txtErfasstRight.ReadOnly = true;
                //    txtErfasstRight.FontSize = 8;
                //    txtErfasstRight.Text = u2;
                //    _pdfPointY += _pdfSizeHeight;
                //}
                //else
                //{
                //    _pdfPointY += _pdfSizeHeight;
                //}
                //_z += 1;
                //_z1 = _n + _z;
                //----------------------------------------------------------
                //  TbNotes(u3);
                //var txtNotesLeft = _page.AddTextBox("notes" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                //    new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                //txtNotesLeft.HasBorder = false;
                //txtNotesLeft.ReadOnly = true;
                //txtNotesLeft.FontSize = 8;
                //txtNotesLeft.Text = CultRes.StringsRes.ReportNotes;

                //if (u3 != " ")
                //{
                //    var u3All = 0;
                //    for (var i = 100; i < u3.Length; i += 100)
                //    {
                //        u3All += 16;
                //    }

                //    var txtNoteRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                //        new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight + u3All));
                //    txtNoteRight.HasBorder = false;
                //    txtNoteRight.ReadOnly = true;
                //    txtNoteRight.Multiline = true;
                //    txtNoteRight.FontSize = 8;
                //    txtNoteRight.Text = u3;
                //    _pdfPointY += _pdfSizeHeight + u3All -4;
                //}
                //else
                //{
                //    _pdfPointY += _pdfSizeHeight;
                //}

                //_z += 1;
                //_z1 = _n + _z;
                //--------------------------------------------------------

                //if (u4 != null)
                //{
                //    var txtAuthorLeft = _page.AddTextBox("author" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                //        new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                //    txtAuthorLeft.HasBorder = false;
                //    txtAuthorLeft.ReadOnly = true;
                //    txtAuthorLeft.Font.SynthesizedBold = false;
                //    txtAuthorLeft.FontSize = 8;
                //    txtAuthorLeft.Text = CultRes.StringsRes.ReportAuthor;

                //    var txtAuthorRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY), 
                //        new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
                //    txtAuthorRight.HasBorder = false;
                //    txtAuthorRight.ReadOnly = true;
                //    txtAuthorRight.FontSize = 8;
                //    txtAuthorRight.Text = u4;
                //    _pdfPointY += _pdfSizeHeight;
                //}
                //else
                //{
                //    _pdfPointY += _pdfSizeHeight;
                //}

                //_z += 1;
                //_z1 = _n + _z;
                //-----------------------------------------------------------------------------
                //  TbInfo(u5);
                //if (u5 != null)
                //{
                //    var txtInfoLeft = _page.AddTextBox("info" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                //        new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                //    txtInfoLeft.HasBorder = false;
                //    txtInfoLeft.ReadOnly = true;
                //    txtInfoLeft.Font.SynthesizedBold = false;
                //    txtInfoLeft.FontSize = 8;
                //    txtInfoLeft.Text = CultRes.StringsRes.ReportInfo;

                //    var uAll = 0;
                //    for (int i = 100; i < u5.Length; i += 100)
                //    {
                //        uAll += 16;
                //    }

                //    var txtInfoRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                //        new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight + uAll));
                //    txtInfoRight.HasBorder = false;
                //    txtInfoRight.ReadOnly = true;
                //    txtInfoRight.Multiline = true;
                //    txtInfoRight.FontSize = 8;
                //    txtInfoRight.Text = u5;
                //    _pdfPointY += _pdfSizeHeight + uAll - 4;
                //}
                //else
                //{
                //     _pdfPointY += _pdfSizeHeight;
                //}

                //_z += 1;
                //_z1 = _n + _z;

                //--------------------------------------------------------------------------------
                //  TbMemo(u6);

                //if (u6 != null)
                //{
                //    var txtMemoLeft = _page.AddTextBox("memo" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                //        new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                //    txtMemoLeft.HasBorder = false;
                //    txtMemoLeft.ReadOnly = true;
                //    txtMemoLeft.Font.SynthesizedBold = false;
                //    txtMemoLeft.FontSize = 8;
                //    txtMemoLeft.Text = CultRes.StringsRes.ReportMemo;

                //    var uAll = 0;
                //    for (int i = 100; i < u6.Length; i += 100)
                //    {
                //        uAll += 16;
                //    }

                //    var txtMemoRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY), 
                //        new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight + uAll));
                //    txtMemoRight.HasBorder = false;
                //    txtMemoRight.ReadOnly = true;
                //    txtMemoRight.Multiline = true;
                //    txtMemoRight.FontSize = 8;
                //    txtMemoRight.Text = u6;
                //    _pdfPointY += _pdfSizeHeight + uAll - 4;
                //}
                //else
                //{
                //    _pdfPointY += _pdfSizeHeight;
                //}

                //_z += 1;
                //_z1 = _n + _z;
                //--------------------------------------------------------------------------
                //        if (u7 != null)
                //        {
                //            var txtValidLeft = _page.AddTextBox("Valid" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                //                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                //            txtValidLeft.HasBorder = false;
                //            txtValidLeft.ReadOnly = true;
                //            txtValidLeft.Font.SynthesizedBold = false;
                //            txtValidLeft.FontSize = 8;
                //            txtValidLeft.Text = CultRes.StringsRes.ReportValid;

                //            var txtValidRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                //                new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
                //            txtValidRight.HasBorder = false;
                //            txtValidRight.ReadOnly = true;
                //            txtValidRight.FontSize = 8;
                //            txtValidRight.Text = u7;
                //            _pdfPointY += _pdfSizeHeight;
                //        }
                //        else
                //        {
                ////            _pdfPointY += _pdfSizeHeight;
                //        }
                //        _z += 1;
                //        _z1 = _n + _z;
                //------------------------------------------------------------
                //if (u8 != null)
                //{
                //    var txtValidYearLeft = _page.AddTextBox("validYear" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                //        new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                //    txtValidYearLeft.HasBorder = false;
                //    txtValidYearLeft.ReadOnly = true;
                //    txtValidYearLeft.Font.SynthesizedBold = false;
                //    txtValidYearLeft.FontSize = 8;
                //    txtValidYearLeft.Text = CultRes.StringsRes.ReportValidYear;

                //    var txtValidYearRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                //        new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
                //    txtValidYearRight.HasBorder = false;
                //    txtValidYearRight.ReadOnly = true;
                //    txtValidYearRight.FontSize = 8;
                //    txtValidYearRight.Text = u8;
                //    _pdfPointY += _pdfSizeHeight;
                //}
                //else
                //{
                //    _pdfPointY += _pdfSizeHeight;
                //}
                //_z += 1;
                //_z1 = _n + _z;

                //-------------------------------------------------------------------
          //      var txtInfo1Left = _page.AddTextBox("RefFor" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY), 
          //          new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
          //      txtInfo1Left.HasBorder = false;
          //      txtInfo1Left.ReadOnly = true;
          //      txtInfo1Left.Font.SynthesizedBold = false;
          //      txtInfo1Left.FontSize = 8;
          //      txtInfo1Left.Text = CultRes.StringsRes.ReportRefFor;

          //      if (u9 != " ")
          //      {
          //          var u9All = 0;
          //          for (int i = 100; i < u9.Length; i += 100)
          //          {
          //              u9All += 16;
          //          }

          //          var txtInfo1Right = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
          //              new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight + u9All));
          //          txtInfo1Right.HasBorder = false;
          //          txtInfo1Right.ReadOnly = true;
          //          txtInfo1Right.Multiline = true;
          //          txtInfo1Right.FontSize = 8;
          //          txtInfo1Right.Text = u9;
          ////          _pdfPointY += _pdfSizeHeight + u9All;
          //          _pdfPointY +=   u9All;
          //      }
          //      else
          //      {
          // //         _pdfPointY += _pdfSizeHeight;
          //      }

          //      _z += 1;
          //      _z1 = _n + _z;
                //-------------------------------------------------------------------
              //  TbMemo(u10);

                //if (u10 != null)
                //{
                //    var txtMemo1Left = _page.AddTextBox("memo" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                //        new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                //    txtMemo1Left.HasBorder = false;
                //    txtMemo1Left.ReadOnly = true;
                //    txtMemo1Left.Font.SynthesizedBold = false;
                //    txtMemo1Left.FontSize = 8;
                //    txtMemo1Left.Text = CultRes.StringsRes.ReportMemo;

                //    var uAll = 0;
                //    for (int i = 100; i < u10.Length; i += 100)
                //    {
                //        uAll += 16;
                //    }

                //    var txtMemo1Right = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY), 
                //        new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight + uAll));
                //    txtMemo1Right.HasBorder = false;
                //    txtMemo1Right.ReadOnly = true;
                //    txtMemo1Right.FontSize = 8;
                //    txtMemo1Right.Multiline = true;
                //    txtMemo1Right.Text = u10;
                //    _pdfPointY += _pdfSizeHeight + uAll;
                //}
                //else
                //{
                //    _pdfPointY += _pdfSizeHeight;
                //}

                //_z += 1;
                //_z1 = _n + _z;
                //-------------------------------------------------------------------

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

            pdf.AddPage();
            _page = pdf.Pages[_pageCount + 1];
            _pdfPointY = 5;

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
            //     _move = arreySize[7]; 

            pdf.AddPage();
            _page = pdf.Pages[_pageCount + 1];
            _pdfPointY = 5;

            ////PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportComments));
            var txtCommHeader = _page.AddTextBox("comments", new PdfPoint(_pdfPointXLeft, _pdfPointY),
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight + 16));
            txtCommHeader.HasBorder = false;
            txtCommHeader.ReadOnly = true;
            txtCommHeader.Font.SynthesizedBold = true;
            txtCommHeader.FontSize = 16;
            txtCommHeader.Text = "Comments";
            _pdfPointY += _pdfSizeHeight;

            arreySize[0] = _pdfPointXLeft;
            arreySize[1] = _pdfPointY;
            arreySize[2] = _pdfPointXRight;
            arreySize[3] = _pdfSizeHeight;
            arreySize[4] = _pdfSizeWidthLeft;
            arreySize[5] = _pdfSizeWidthRight;
            arreySize[6] = _pageCount;
            //     arreySize[7] = _move; //_move

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
            //     _move = arreySize[7]; 

            //if (commentsList.Count >= 3)
            //{
            //    pdf.AddPage();
            //    _page = pdf.Pages[_pageCount + 1];
            //    _pdfPointY = 5;
            //}

            arreySize[0] = _pdfPointXLeft;
            arreySize[1] = _pdfPointY;
            arreySize[2] = _pdfPointXRight;
            arreySize[3] = _pdfSizeHeight;
            arreySize[4] = _pdfSizeWidthLeft;
            arreySize[5] = _pdfSizeWidthRight;
            arreySize[6] = _pageCount;
            //     arreySize[7] = _move; //_move

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
        /// Function for References Expert
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="tbl90RefExpertsList"></param>
        /// <returns>doc</returns>
        //public static Document AddRefExpertList(Document doc, ObservableCollection<Tbl90Reference> tbl90RefExpertsList)
        //{
        //    var table = new PdfPTable(4)
        //    {
        //        TotalWidth = 792f, //actual width of table in points
        //        LockedWidth = true,
        //        WidthPercentage = 100,
        //        HorizontalAlignment = 0,  //0=Left aLign, 1=Center
        //        SpacingBefore = 0f,
        //        SpacingAfter = 10f   //fix the absolute width of the table
        //    };
        //    table.SetWidths(new[] { 0.03f, 0.90f, 1.80f, 1.00f });

        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 4, Border = 0 });  //Empty row

        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportExperts, SmallBoldFont)) { Colspan = 4, Border = 0 }); //   4.  field

        //    foreach (var u in tbl90RefExpertsList)
        //    {
        //        var u1 = u.Tbl90RefExperts.RefExpertName;
        //        var u2 = u.Tbl90RefExperts.Notes;
        //        var u3 = u.Tbl90RefExperts.Info;
        //        var u4 = u.Tbl90RefExperts.Memo;
        //        var u5 = Convert.ToString(u.Tbl90RefExperts.Valid);
        //        var u6 = u.Tbl90RefExperts.ValidYear;
        //        var u7 = u.Info;
        //        var u8 = u.Memo;

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportExpert, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u1, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportNotes, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u2, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  Field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportInfo, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u3, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  Field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemo, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u4, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 }); // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportValid, SmallFont)) { Border = 0 }); // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u5, SmallFont)) { Border = 0 }); // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 }); // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 }); // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportValidYear, SmallFont)) { Border = 0 }); // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u6, SmallFont)) { Border = 0 }); // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 }); // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportRefFor, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u7, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemo, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u8, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 4, Border = 0 });  //Empty row
        //    }

        //    doc.Add(table);
        //    return doc;
        //}

        /// <summary>
        /// Function for References Sources
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="tbl90RefSourcesList"></param>
        /// <returns>doc</returns>
        //public static Document AddRefSourceList(Document doc, ObservableCollection<Tbl90Reference> tbl90RefSourcesList)
        //{
        //    var table = new PdfPTable(4)
        //    {
        //        TotalWidth = 792f, //actual width of table in points
        //        LockedWidth = true,
        //        WidthPercentage = 100,
        //        HorizontalAlignment = 0,  //0=Left aLign, 1=Center
        //        SpacingBefore = 0f,
        //        SpacingAfter = 10f   //fix the absolute width of the table
        //    };
        //    table.SetWidths(new[] { 0.03f, 0.90f, 1.80f, 1.00f });

        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 4, Border = 0 });  //Empty row

        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportOtherSources, SmallBoldFont)) { Colspan = 4, Border = 0 }); //   4.  field

        //    foreach (var u in tbl90RefSourcesList)
        //    {
        //        var u1 = u.Tbl90RefSources.RefSourceName;
        //        var u2 = u.Tbl90RefSources.SourceYear;
        //        var u3 = u.Tbl90RefSources.Notes;
        //        var u4 = u.Tbl90RefSources.Author;
        //        var u5 = u.Tbl90RefSources.Info;
        //        var u6 = u.Tbl90RefSources.Memo;
        //        var u7 = Convert.ToString(u.Tbl90RefSources.Valid);
        //        var u8 = u.Tbl90RefSources.ValidYear;
        //        var u9 = u.Info;
        //        var u10 = u.Memo;

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 }); // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportSource, SmallFont)) { Border = 0 }); // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u1, SmallFont)) { Border = 0 }); // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 }); // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 }); // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportAcquired, SmallFont)) { Border = 0 }); // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u2, SmallFont)) { Border = 0 }); // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 }); // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 }); // 1.  Field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportNotes, SmallFont)) { Border = 0 }); // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u3, SmallFont)) { Border = 0 }); // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 }); // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 }); // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportAuthor, SmallFont)) { Border = 0 }); // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u4, SmallFont)) { Border = 0 }); // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 }); // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 }); // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportInfo, SmallFont)) { Border = 0 }); // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u5, SmallFont)) { Border = 0 }); // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 }); // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 }); // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemo, SmallFont)) { Border = 0 }); // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u6, SmallFont)) { Border = 0 }); // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 }); // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 }); // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportValid, SmallFont)) { Border = 0 }); // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u7, SmallFont)) { Border = 0 }); // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 }); // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 }); // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportValidYear, SmallFont)) { Border = 0 }); // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u8, SmallFont)) { Border = 0 }); // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 }); // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 }); // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportRefFor, SmallFont)) { Border = 0 }); // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u9, SmallFont)) { Border = 0 }); // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 }); // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 }); // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemo, SmallFont)) { Border = 0 }); // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u10, SmallFont)) { Border = 0 }); // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 }); // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 4, Border = 0 }); //Empty row                               
        //    }

        //    doc.Add(table);
        //    return doc;
        //}

        /// <summary>
        /// Function for Reference Author
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="tbl90RefAuthorsList"></param>
        /// <returns>doc</returns>
        //public static Document AddRefAuthorList(Document doc, ObservableCollection<Tbl90Reference> tbl90RefAuthorsList)
        //{
        //    var table = new PdfPTable(4)
        //    {
        //        TotalWidth = 792f, //actual width of table in points
        //        LockedWidth = true,
        //        WidthPercentage = 100,
        //        HorizontalAlignment = 0,  //0=Left aLign, 1=Center
        //        SpacingBefore = 0f,
        //        SpacingAfter = 10f   //fix the absolute width of the table
        //    };
        //    table.SetWidths(new[] { 0.03f, 0.90f, 1.80f, 1.00f });

        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 4, Border = 0 });  //Empty row

        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportPublications, SmallBoldFont)) { Colspan = 4, Border = 0 }); //   4.  field

        //    foreach (var t in tbl90RefAuthorsList)
        //    {
        //        var t1 = t.Tbl90RefAuthors.RefAuthorName;
        //        var t2 = t.Tbl90RefAuthors.PublicationYear;
        //        var t3 = t.Tbl90RefAuthors.ArticelTitle;
        //        var t4 = t.Tbl90RefAuthors.BookName;
        //        var t5 = t.Tbl90RefAuthors.Page1;
        //        var t6 = t.Tbl90RefAuthors.Publisher;
        //        var t7 = t.Tbl90RefAuthors.PublicationPlace;
        //        var t8 = t.Tbl90RefAuthors.ISBN;
        //        var t9 = t.Tbl90RefAuthors.Notes;
        //        var t10 = t.Tbl90RefAuthors.Info;
        //        var t11 = t.Tbl90RefAuthors.Memo;
        //        var t12 = Convert.ToString(t.Tbl90RefAuthors.Valid);
        //        var t13 = t.Tbl90RefAuthors.ValidYear;
        //        var t14 = t.Info;
        //        var t15 = t.Memo;

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportAuthorsEditors, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(t1, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportPublicationDate, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(t2, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  Field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportArticleTitle, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(t3, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportBookName, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(t4, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportPages, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(t5, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportPublisher, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(t6, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportPublicationPlace, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(t7, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportIsbn, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(t8, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportNotes, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(t9, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportInfo, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(t10, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemo, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(t11, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 }); // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportValid, SmallFont)) { Border = 0 }); // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(t12, SmallFont)) { Border = 0 }); // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 }); // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 }); // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportValidYear, SmallFont)) { Border = 0 }); // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(t13, SmallFont)) { Border = 0 }); // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 }); // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 }); // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportRefFor, SmallFont)) { Border = 0 }); // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(t14, SmallFont)) { Border = 0 }); // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 }); // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 }); // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemo, SmallFont)) { Border = 0 }); // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(t15, SmallFont)) { Border = 0 }); // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 }); // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 4, Border = 0 });  //Empty row
        //    }

        //    doc.Add(table);
        //    return doc;
        //}

        /// <summary>
        /// Function for Comments
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="tbl93CommentsList"></param>
        /// <returns>doc</returns>
        //public static Document AddCommentList(Document doc, ObservableCollection<Tbl93Comment> tbl93CommentsList)
        //{
        //    var table = new PdfPTable(4)
        //    {
        //        TotalWidth = 792f, //actual width of table in points
        //        LockedWidth = true,
        //        WidthPercentage = 100,
        //        HorizontalAlignment = 0,  //0=Left aLign, 1=Center
        //        SpacingBefore = 0f,
        //        SpacingAfter = 10f   //fix the absolute width of the table
        //    };
        //    table.SetWidths(new[] { 0.03f, 0.90f, 1.80f, 1.00f });

        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 4, Border = 0 });  //Empty row

        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportComments, SmallBoldFont)) { Colspan = 4, Border = 0 }); //   4.  field

        //    foreach (var t in tbl93CommentsList)
        //    {
        //        var t1 = t.Info;
        //        var t2 = t.Memo;

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportInfo, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(t1, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemo, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(t2, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 4, Border = 0 });  //Empty row
        //    }

        //    doc.Add(table);
        //    return doc;
        //}

        // <summary>
        // Add a paragraph object containing the specified element to the PDF document.
        // </summary>
        // <param name="doc">Document to which to add the paragraph.</param>
        // <param name="alignment">Alignment of the paragraph.</param>
        // <param name="font">Font to assign to the paragraph.</param>
        // <param name="content">Object that is the content of the paragraph.</param>       
        //public static void AddParagraph(Document doc, int alignment, Font font, IElement content)
        //{
        //    var paragraph = new Paragraph();
        //    paragraph.SetLeading(0f, 1.2f);
        //    paragraph.Alignment = alignment;
        //    paragraph.Font = font;
        //    paragraph.Add(content);
        //    doc.Add(paragraph);
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

        /// <summary>
        /// Function for Synonyms
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="tbl84SynonymsList"></param>
        /// <returns>doc</returns>
        //public static Document AddTbl84SynonymsList(Document doc, ObservableCollection<Tbl84Synonym> tbl84SynonymsList)
        //{
        //    var table = new PdfPTable(4)
        //    {
        //        TotalWidth = 792f, //actual width of table in points
        //        LockedWidth = true,
        //        WidthPercentage = 100,
        //        HorizontalAlignment = 0,  //0=Left aLign, 1=Center
        //        SpacingBefore = 0f,
        //        SpacingAfter = 10f   //fix the absolute width of the table
        //    };
        //    table.SetWidths(new[] { 0.03f, 0.90f, 1.80f, 1.00f });

        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 4, Border = 0 });  //Empty row

        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportSynonyms, StandardFont)) { Colspan = 4, Border = 0 }); //   4.  field

        //    foreach (var u in tbl84SynonymsList)
        //    {
        //        var u1 = u.SynonymName;

        //        var author = PdfHelper.AuthorViewChangeWithString(u.Author, u.AuthorYear);

        //        table.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 }); // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u1 + " " + author, SmallFont)) { Border = 0 }); // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field
        //    }

        //    doc.Add(table);
        //    return doc;
        //}

        /// <summary>
        /// Function for names
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="tbl78NamesList"></param>
        /// <returns>doc</returns>
        //public static Document AddTbl78NamesList(Document doc, ObservableCollection<Tbl78Name> tbl78NamesList)
        //{
        //    var table = new PdfPTable(4)
        //    {
        //        TotalWidth = 792f, //actual width of table in points
        //        LockedWidth = true,
        //        WidthPercentage = 100,
        //        HorizontalAlignment = 0,  //0=Left aLign, 1=Center
        //        SpacingBefore = 0f,
        //        SpacingAfter = 10f   //fix the absolute width of the table
        //    };
        //    table.SetWidths(new[] { 0.03f, 0.90f, 1.80f, 1.00f });

        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 4, Border = 0 });  //Empty row

        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportNames, StandardFont)) { Colspan = 4, Border = 0 }); //   4.  field

        //    foreach (var u in tbl78NamesList)
        //    {
        //        var u1 = u.NameName;
        //        var u2 = u.Language;

        //        table.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 }); // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u1 + " [ " + u2 + " ] ", SmallFont)) { Border = 0 }); // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field
        //    }

        //    doc.Add(table);
        //    return doc;
        //}

        /// <summary>
        /// Function for Images
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="tbl81ImagesList"></param>
        /// <returns>doc</returns>
        //public static Document AddTbl81ImagesList(Document doc, ObservableCollection<Tbl81Image> tbl81ImagesList)
        //{
        //    var table = new PdfPTable(4)
        //    {
        //        TotalWidth = 792f, //actual width of table in points
        //        LockedWidth = true,
        //        WidthPercentage = 100,
        //        HorizontalAlignment = 0,  //0=Left aLign, 1=Center
        //        SpacingBefore = 0f,
        //        SpacingAfter = 10f   //fix the absolute width of the table
        //    };
        //    table.SetWidths(new[] { 0.03f, 0.90f, 1.80f, 1.00f });

        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 4, Border = 0 });  //Empty row

        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportImages, StandardFont)) { Colspan = 4, Border = 0 }); //   4.  field

        //    foreach (var u in tbl81ImagesList)
        //    {
        //        var u1 = u.ShotDate;
        //        var u2 = u.ImageMimeType;
        //        var u3 = u.Info;
        //        var u4 = u.Memo;
        //        var u5 = u.Filestream;

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportImageShot, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(Convert.ToString(u1), SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportImageMimeType, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u2, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportInfo, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u3, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemo, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u4, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        var logoImage = Image.GetInstance(u5);
        //        logoImage.Alignment = Element.ALIGN_CENTER;
        //        //       logoImage.ScaleAbsolute(120f, 155.25f);
        //        logoImage.ScaleToFit(250f, 250f);
        //        logoImage.Border = Rectangle.BOX;
        //        logoImage.BorderColor = new BaseColor(Color.Yellow);
        //        logoImage.BorderWidth = 5f;

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 2, Border = 0 });  // 1.+ 2.  field
        //        table.AddCell(new PdfPCell(logoImage) { Colspan = 4, Border = 0 });   // 2. field
        //                                                                              //       table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 3.  field
        //                                                                              //     table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 4, Border = 0 });  //Empty row
        //    }

        //    doc.Add(table);
        //    return doc;
        //}

        /// <summary>
        /// Function for Geographic
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="tbl87GeographicsList"></param>
        /// <returns>doc</returns>
        //public static Document AddTbl87GeographicsList(Document doc, ObservableCollection<Tbl87Geographic> tbl87GeographicsList)
        //{
        //    var table = new PdfPTable(4)
        //    {
        //        TotalWidth = 792f, //actual width of table in points
        //        LockedWidth = true,
        //        WidthPercentage = 100,
        //        HorizontalAlignment = 0,  //0=Left aLign, 1=Center
        //        SpacingBefore = 0f,
        //        SpacingAfter = 10f   //fix the absolute width of the table
        //    };
        //    table.SetWidths(new[] { 0.03f, 0.90f, 1.80f, 1.00f });

        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 4, Border = 0 });  //Empty row

        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportGeographics, StandardFont)) { Colspan = 4, Border = 0 }); //   4.  field

        //    foreach (var u in tbl87GeographicsList)
        //    {
        //        var u1 = u.Continent;
        //        var u2 = u.Country;
        //        var u3 = u.Address;
        //        var u4 = u.Author;
        //        var u5 = u.AuthorYear;
        //        var u6 = u.ZoomLevel;
        //        var u7 = u.Latitude;
        //        var u8 = u.Longitude;
        //        var u9 = u.Latitude1;
        //        var u10 = u.Longitude1;
        //        var u11 = u.Latitude2;
        //        var u12 = u.Longitude2;
        //        var u13 = u.Latitude3;
        //        var u14 = u.Longitude3;
        //        var u15 = u.Info;
        //        var u16 = u.Memo;
        //        var u17 = u.Http;

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportContinent, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u1, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportCountry, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u2, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportAddress, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u3, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportAuthor, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u4 + " " + u5, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportZoomLevel, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(Convert.ToString(u6, CultureInfo.InvariantCulture), SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportLatitudeLongitude, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(Convert.ToString(u7, CultureInfo.InvariantCulture) + " / " + Convert.ToString(u8, CultureInfo.InvariantCulture), SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportLatitudeLongitude, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(Convert.ToString(u9, CultureInfo.InvariantCulture) + " / " + Convert.ToString(u10, CultureInfo.InvariantCulture), SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportLatitudeLongitude, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(Convert.ToString(u11, CultureInfo.InvariantCulture) + " / " + Convert.ToString(u12, CultureInfo.InvariantCulture), SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportLatitudeLongitude, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(Convert.ToString(u13, CultureInfo.InvariantCulture) + " / " + Convert.ToString(u14, CultureInfo.InvariantCulture), SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportHttp, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u17, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportInfo, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u15, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.  field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemo, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(u16, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field


        //        /*               var logoImage = Image.GetInstance(u5);
        //                       logoImage.Alignment = Element.ALIGN_CENTER;
        //                       //       logoImage.ScaleAbsolute(120f, 155.25f);
        //                       logoImage.ScaleToFit(250f, 250f);
        //                       logoImage.Border = Rectangle.BOX;
        //                       logoImage.BorderColor = new BaseColor(Color.Yellow);
        //                       logoImage.BorderWidth = 5f;

        //                       table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 2, Border = 0 });  // 1.+ 2.  field
        //                       table.AddCell(new PdfPCell(logoImage) { Colspan = 4, Border = 0 });   // 2. field
        //                                                                                             //       table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 3.  field
        //                                                                                             //     table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field
        //       */
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 4, Border = 0 });  //Empty row
        //    }

        //    doc.Add(table);
        //    return doc;
        //}

        //--------------------------------------------

        // <summary>
        // Use this method to write XMP data to a new PDF
        // </summary>
        // <param name="writer"></param>
        /*      static void CreateXmpMetadata(PdfWriter writer)
              {
                  // Set up the buffer to hold the XMP metadata
                  var buffer = new byte[65536];
                  var ms = new MemoryStream(buffer, true);
                  try
                  {
                      // XMP supports a number of different schemas, which are made available by iTextSharp.
                      // Here, the Dublin Core schema is chosen.
                      var dc = new iTextSharp.text.xml.xmp.DublinCoreSchema();

                      // Add Dublin Core attributes
                      var title = new iTextSharp.text.xml.xmp.LangAlt();
                      title.Add("x-default", "My Science Project");
                      dc.SetProperty(iTextSharp.text.xml.xmp.DublinCoreSchema.TITLE, title);
                      // Dublin Core allows multiple authors, so we create an XmpArray to hold the values
                      var author = new iTextSharp.text.xml.xmp.XmpArray(iTextSharp.text.xml.xmp.XmpArray.ORDERED);
                      author.Add("Rudolf Terppé");
                      dc.SetProperty(iTextSharp.text.xml.xmp.DublinCoreSchema.CREATOR, author);

                      // Multiple subjects are also possible, so another XmpArray is used
                      var subject = new iTextSharp.text.xml.xmp.XmpArray(iTextSharp.text.xml.xmp.XmpArray.UNORDERED);
                      subject.Add("paper airplanes");
                      subject.Add("science project");
                      dc.SetProperty(iTextSharp.text.xml.xmp.DublinCoreSchema.SUBJECT, subject);

                      // Create an XmpWriter using the MemoryStream defined earlier
                      var xmp = new iTextSharp.text.xml.xmp.XmpWriter(ms);

                      xmp.AddRdfDescription(dc); // Add the completed metadata definition to the XmpWriter

                      xmp.Close(); // This flushes the XMP metadata into the buffer

                      //---------------------------------------------------------------------------------

                      // Shrink the buffer to the correct size (discard empty elements of the byte array)
                      var bufsize = buffer.Length;
                      var bufcount = 0;
                      foreach (var b in buffer)
                      {
                          if (b == 0) break;
                          bufcount++;
                      }

                      var ms2 = new MemoryStream(buffer, 0, bufcount);
                      buffer = ms2.ToArray();
                      //---------------------------------------------------------------------------------

                      // Add all of the XMP metadata to the PDF doc that we're building
                      writer.XmpMetadata = buffer;
                  }
                  catch (Exception ex)
                  {
                      throw ex;
                  }
                  finally
                  {
                      ms.Close();
                      ms.Dispose();
                  }
              }


      */


        // <summary>
        // Add a blank page to the document.
        // </summary>
        // <param name="doc"></param>
        //static void AddPageWithInternalLinks(Document doc)
        //{
        //    // Generate links to be embedded in the page
        //    var researchAnchor = new Anchor("Research & Hypothesis\n\n", StandardFont) { Reference = "#research" };
        //    // this link references a named anchor within the document
        //    var graphAnchor = new Anchor("Graph\n\n", StandardFont) { Reference = "#graph" };
        //    var resultsAnchor = new Anchor("Results & Bibliography", StandardFont) { Reference = "#results" };

        //    // Add a new page to the document
        //    doc.NewPage();

        //    // Add heading text to the page
        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, new Chunk("TABLE OF CONTENTS\n\n\n\n\n"));

        //    // Add the links to the page
        //    AddParagraph(doc, Element.ALIGN_CENTER, StandardFont, researchAnchor);
        //    AddParagraph(doc, Element.ALIGN_CENTER, StandardFont, graphAnchor);
        //    AddParagraph(doc, Element.ALIGN_CENTER, StandardFont, resultsAnchor);
        //}

        // <summary>
        // Add a page that includes a bullet list.
        // </summary>
        // <param name="doc"></param>
        //static void AddPageWithBulletList(Document doc)
        //{
        //    // Add a new page to the document
        //    doc.NewPage();

        //    // The header at the top of the page is an anchor linked to by the table of contents.
        //    var contentsAnchor = new Anchor("RESEARCH\n\n", LargeFont) { Name = "research" };

        //    // Add the header anchor to the page
        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, contentsAnchor);

        //    // Create an unordered bullet list.  The 10f argument separates the bullet from the text by 10 points
        //    var list = new List(List.UNORDERED, 10f);
        //    list.SetListSymbol("\u2022"); // Set the bullet symbol (without this a hypen starts each list item)
        //    list.IndentationLeft = 20f; // Indent the list 20 points
        //    list.Add(new ListItem("Lift, thrust, drag, and gravity are the four forces that act on a plane.",
        //        StandardFont));
        //    list.Add(new ListItem("A plane should be light to help fight against gravity's pull to the ground.",
        //        StandardFont));
        //    list.Add(new ListItem(
        //        "Gravity will have less effect on a plane built from the lightest materials available.",
        //        StandardFont));
        //    list.Add(new ListItem("In order to fly well, airplanes must be stable.", StandardFont));
        //    list.Add(new ListItem("A plane that is unstable will either pitch up into a stall, or nose-dive.",
        //        StandardFont));
        //    doc.Add(list); // Add the list to the page

        //    // Add some white space and another heading
        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, new Chunk("\n\n\n"));
        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, new Chunk("HYPOTHESIS\n\n"));

        //    // Add some final text to the page
        //    AddParagraph(doc, Element.ALIGN_LEFT, StandardFont,
        //        new Chunk("Given five paper airplanes made out of newspaper, printer paper, construction paper, paper towel, and posterboard, the airplane made out of printer paper will fly the furthest."));
        //}

        // <summary>
        // Add a page that contains embedded hyperlinks to external resources
        // </summary>
        // <param name="doc"></param>
        //static void AddPageWithExternalLinks(Document doc)
        //{
        //    // Generate external links to be embedded in the page
        //    var bibliographyAnchor1 =
        //        new Anchor("http://teacher.scholastic.com/paperairplane/airplane.htm", StandardFont)
        //        {
        //            Reference = "http://teacher.scholastic.com/paperairplane/airplane.htm"
        //        };

        //    var bibliographyAnchor2 =
        //        new Anchor("http://www.eecs.berkeley.edu/Programs/doublex/spring02/paperairplane.html",
        //            StandardFont);

        //    bibliographyAnchor1.Reference =
        //        "http://www.eecs.berkeley.edu/Programs/doublex/spring02/paperairplane.html";

        //    var bibliographyAnchor3 =
        //        new Anchor("http://www.exo.net/~pauld/activities/flying/PaperAirplaneScience.html", StandardFont);

        //    bibliographyAnchor1.Reference = "http://www.exo.net/~pauld/activities/flying/PaperAirplaneScience.html";

        //    var bibliographyAnchor4 =
        //        new Anchor("http://www.littletoyairplanes.com/theoryofflight/02whyplanes.html", StandardFont)
        //        {
        //            Reference = "http://www.littletoyairplanes.com/theoryofflight/02whyplanes.html"
        //        };

        //    // The header at the top of the page is an anchor linked to by the table of contents.
        //    var contentsAnchor = new Anchor("RESULTS\n\n", LargeFont) { Name = "results" };

        //    // Add a new page to the document
        //    doc.NewPage();

        //    // Add text to the page
        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, contentsAnchor);
        //    AddParagraph(doc, Element.ALIGN_LEFT, StandardFont,
        //        new Chunk(
        //            "My hypothesis was incorrect.  The paper airplane made out of construction paper flew the furthest."));

        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, new Chunk("\n\n\n"));
        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, new Chunk("BIBLIOGRAPHY\n\n"));

        //    // Add the links to the page
        //    AddParagraph(doc, Element.ALIGN_LEFT, StandardFont, bibliographyAnchor1);
        //    AddParagraph(doc, Element.ALIGN_LEFT, StandardFont, bibliographyAnchor2);
        //    AddParagraph(doc, Element.ALIGN_LEFT, StandardFont, bibliographyAnchor3);
        //    AddParagraph(doc, Element.ALIGN_LEFT, StandardFont, bibliographyAnchor4);
        //}

        // <summary>
        // Add a page containing a single image.  Set the page size to match the image size.
        // </summary>
        // <param name="doc"></param>
        // <param name="imagePath"></param>
        //static void AddPageWithImage(Document doc, String imagePath)
        //{
        //    // Read the image file
        //    var image = Image.GetInstance(new Uri(imagePath));

        //    // Set the page size to the dimensions of the image BEFORE adding a new page to the document.
        //    // Pad the height a bit to leave room for the page header.
        //    var imageWidth = image.Width;

        //    var imageHeight = image.Height;
        //    doc.SetMargins(0, 0, 0, 0);
        //    doc.SetPageSize(new Rectangle(imageWidth, imageHeight + 100));

        //    // Add a new page
        //    doc.NewPage();

        //    // The header at the top of the page is an anchor linked to by the table of contents.
        //    var contentsAnchor = new Anchor("\nGRAPH\n\n", LargeFont) { Name = "graph" };

        //    // Add the anchor and image to the page
        //    AddParagraph(doc, Element.ALIGN_CENTER, LargeFont, contentsAnchor);
        //    doc.Add(image);
        //    image = null;

        //}


        /*     private ObservableCollection<Tbl90RefSource> _tbl90SourcesList;
             public ObservableCollection<Tbl90RefSource> Tbl90SourcesList
             {
                 get => _tbl90SourcesList;
                 set { _tbl90SourcesList = value; RaisePropertyChanged(); }
             }
     */
    }
}
