using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using ATIS.Dal.Models;
using ATIS.Ui.Helper;
using ATIS.Ui.Views.Database.CrudHelper;
using BitMiracle.Docotic;
using BitMiracle.Docotic.Pdf;
using log4net;

namespace ATIS.Ui.Views.Report.D03Regnum
{
    public class ReportRegnumPdf : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ReportRegnumPdf));
        private static readonly BasicGet ExtGet = new BasicGet();

        private static readonly PdfHelper PdfHelper = new PdfHelper();
        private static string _n;
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
        private static int _z;

        private static int[] _arrInts = new int[11];
        private static int[] _arrHelperInts = new int[11];

        private static PdfPage _page;

    //    private static int _tAllLength;

        //    Part 1    

        public static void CreateMainPdf(int id)
        {
            //_arrInts[0] = 20; //_pdfPointXLeft
            //_arrInts[1] = 5; //_pdfPointY
            //_arrInts[2] = 150; //_pdfPointXRight
            //_arrInts[3] = 8; //_pdfSizeHeight
            //_arrInts[4] = 300; //_pdfSizeWidthLeft
            //_arrInts[5] = 430; //_pdfSizeWidthRight
            //_arrInts[6] = 0; //_pageCount
            //_arrInts[7] = 4; //_move
            //_arrInts[8] = 95; //_characterSize
            //_arrInts[9] = 8; //_fontSize
            //_arrInts[10] = 0; //_z

            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.


            LicenseManager.AddLicenseData("5IUML-K4LFW-CQ4J0-Y673N-72V88");
            //    BitMiracle.Docotic.LicenseManager.AddLicenseData("5IUML-K4LFW-CQ4J0-Y673N-72V88");

            const string pathToFile = @"..\..\..\..\..\..\..\..\RudiPDFTest.pdf";

            //var sfd = new SaveFileDialog {Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*"};
            //var saveResult = sfd.ShowDialog();
            //if (saveResult != true) return; //exit
            //     PdfDocument doc = null;

            var regnumList = ExtGet.GetRegnumsCollectionOrderByFromRegnumId<Tbl03Regnum>(id).FirstOrDefault();
            var phylumsList = ExtGet.GetPhylumsCollectionOrderByFromRegnumId<Tbl06Phylum>(id);
            var divisionsList = ExtGet.GetDivisionsCollectionOrderByFromRegnumId<Tbl09Division>(id);
            var expertsList = ExtGet.GetReferenceExpertsCollectionOrderByFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(id);
            var sourcesList = ExtGet.GetReferenceSourcesCollectionOrderByFromRegnumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(id);
            var authorsList = ExtGet.GetReferenceAuthorsCollectionOrderByFromRegnumIdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(id);
            var commentsList = ExtGet.GetCommentsCollectionOrderByFromRegnumId<Tbl93Comment>(id);

            try
            {
                using (PdfDocument pdf = new PdfDocument())
                {
                    _arrInts = PdfHelper.AddReportMain(pdf, regnumList);

                    AddRegnumHaeder(pdf, regnumList);
                    AddRegnumTaxoNomenList(pdf, regnumList);
                    //_arrInts = AddRegnumHierarchyList(pdf, regnumList, _arrInts);

                    // if (phylumsList.Count != 0)
                    //     _arrInts = AddPhylumsChildrenList(pdf, phylumsList, _arrInts);
                    //if (divisionsList.Count != 0)
                    //    _arrInts = AddDivisionsChildrenList(pdf, divisionsList, _arrInts);

                    //if (expertsList.Count != 0 || sourcesList.Count != 0 || authorsList.Count != 0)
                    //{
                    //    pdf.AddPage();
                    //    _page = pdf.Pages[_pageCount + 1];
                    //    //_pdfPointY = 5;

                    //    _arrInts = PdfHelper.AddReferencesHaeder(pdf, _arrInts);
                    //}

                    //if (expertsList.Count != 0)
                    //    _arrInts = PdfHelper.AddRefExpertsList(pdf, expertsList, _arrInts);
                    //if (sourcesList.Count != 0)
                    //{
                    //    pdf.AddPage();
                    //    _page = pdf.Pages[_pageCount + 1];
                    //    //_pdfPointY = 5;

                    //    _arrInts = PdfHelper.AddRefSourcesList(pdf, sourcesList, _arrInts);
                    //}

                    //if (authorsList.Count != 0)
                    //{
                    //    pdf.AddPage();
                    //    _page = pdf.Pages[_pageCount + 1];
                    //    //_pdfPointY = 5;

                    //    _arrInts = PdfHelper.AddRefAuthorsList(pdf, authorsList, _arrInts);
                    //}

                    //if (commentsList.Count != 0)
                    //{
                    //    pdf.AddPage();
                    //    _page = pdf.Pages[_pageCount + 1];
                    //    //_pdfPointY = 5;

                    //    _arrInts = PdfHelper.AddCommentsHaeder(pdf, _arrInts);
                    //}

                    //if (commentsList.Count != 0)
                    //    _arrInts = PdfHelper.AddCommentsList(pdf, commentsList, _arrInts);

                    pdf.Save(pathToFile);
               

                    //var sfd = new SaveFileDialog { Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*" };
                    //var saveResult = sfd.ShowDialog();
                    //if (saveResult != true) return; //exit
                }
            }
            catch (Exception)
            {
                // Handle  errors
            }
            finally
            {
                // Clean up
        //        if (doc != null) doc.Dispose();
           //     doc = null;
            }
        }

        private static int[] AddRegnumHaeder(PdfDocument pdf, Tbl03Regnum regnumList )
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

            _page = pdf.Pages[_arrInts[6]];

            if (regnumList.Author != null)
            {
                var txtName = _page.AddTextBox("regnumName", new PdfPoint(_arrInts[0], _arrInts[1]), 
                    new PdfSize(_arrInts[5], _arrInts[3]));
                txtName.HasBorder = false;
                txtName.ReadOnly = true;
                txtName.Font.SynthesizedBold = true;
                txtName.FontSize = _arrInts[9] + 2;
                txtName.Text = regnumList.RegnumName + " " + regnumList.Subregnum + " " + regnumList.Author + "," + regnumList.AuthorYear;
                _arrInts[1] += _arrInts[3];
            }
            else
            {
                var txtName = _page.AddTextBox("regnumName", new PdfPoint(_arrInts[0], _arrInts[1]), 
                    new PdfSize(_arrInts[5], _arrInts[3]));
                txtName.HasBorder = false;
                txtName.ReadOnly = true;
                txtName.Font.SynthesizedBold = true;
                txtName.FontSize = _arrInts[9] + 2;
                txtName.Text = regnumList.RegnumName + " " + regnumList.Subregnum;
                _arrInts[1] += _arrInts[3];
            }
            _arrInts[1] += _arrInts[9] - 6; //Distance to next TextBox

            var txtCountId = _page.AddTextBox("countId", new PdfPoint(_arrInts[0], _arrInts[1]), 
                new PdfSize(_arrInts[5], _arrInts[3]));
            txtCountId.HasBorder = false;
            txtCountId.ReadOnly = true;
            txtCountId.Font.SynthesizedBold = false;
            txtCountId.FontSize = _arrInts[9] - 1;
            txtCountId.Text = CultRes.StringsRes.ReportTaxonomicId + regnumList.CountId;

            _arrInts[1] += _arrInts[3];
            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox

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

            return _arrInts;
        }
        private static int[] AddRegnumTaxoNomenList(PdfDocument pdf, Tbl03Regnum regnumList, int[] arrayInts)
        {
            _pdfPointXLeft = arrayInts[0];
            _pdfPointY = arrayInts[1];
            _pdfPointXRight = arrayInts[2];
            _pdfSizeHeight = arrayInts[3];
            _pdfSizeWidthLeft = arrayInts[4];
            _pdfSizeWidthRight = arrayInts[5];
            _pageCount = arrayInts[6];
            _move = _arrInts[7];
            _characterSize = _arrInts[8];
            _fontSize = _arrInts[9];
            _z = _arrInts[10];

            _page = pdf.Pages[_pageCount];

            var txtHeader2 = _page.AddTextBox("header2", new PdfPoint(_pdfPointXLeft, _pdfPointY), 
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtHeader2.HasBorder = false;
            txtHeader2.ReadOnly = true;
            txtHeader2.Font.SynthesizedBold = true;
            txtHeader2.FontSize = 10;
            txtHeader2.Text = CultRes.StringsRes.ReportTaxoNomen;
            _pdfPointY += _pdfSizeHeight;
            _pdfPointY += 2; //Distance to next TextBox

            //----------------------------------------------------------------
            var txtNameLeft = _page.AddTextBox("kingdomLeft", new PdfPoint(_pdfPointXLeft + _move, _pdfPointY), 
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtNameLeft.HasBorder = false;
            txtNameLeft.ReadOnly = true;
            txtNameLeft.Font.SynthesizedBold = false;
            txtNameLeft.FontSize = 8;
            txtNameLeft.Text = CultRes.StringsRes.Regnum + ":";
         //   _pdfPointY += _pdfSizeHeight;

            var txtNameRight = _page.AddTextBox("kingdomRight", new PdfPoint(_pdfPointXRight, _pdfPointY), 
                new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
            txtNameRight.HasBorder = false;
            txtNameRight.ReadOnly = true;
            txtNameRight.FontSize = 8;
            txtNameRight.Text = regnumList.RegnumName + " " + regnumList.Subregnum;
            _pdfPointY += _pdfSizeHeight;
            //---------------------------------------------------------------
            var txtRankLeft = _page.AddTextBox("rankLeft", new PdfPoint(_pdfPointXLeft + _move, _pdfPointY), 
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtRankLeft.HasBorder = false;
            txtRankLeft.ReadOnly = true;
            txtRankLeft.FontSize = 8;
            txtRankLeft.Text = CultRes.StringsRes.ReportTaxoRank;
       //     _pdfPointY += _pdfSizeHeight;

            var txtRankRight = _page.AddTextBox("rankRight", new PdfPoint(_pdfPointXRight, _pdfPointY), 
                new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
            txtRankRight.HasBorder = false;
            txtRankRight.ReadOnly = true;
            txtRankRight.FontSize = 8;
            txtRankRight.Text = CultRes.StringsRes.Regnum;
            _pdfPointY += _pdfSizeHeight;
            //------------------------------------------------------
            var txtSynonymLeft = _page.AddTextBox("synonymLeft", new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtSynonymLeft.HasBorder = false;
            txtSynonymLeft.ReadOnly = true;
            txtSynonymLeft.FontSize = 8;
            txtSynonymLeft.Text = CultRes.StringsRes.ReportSynonyms;

            if (regnumList.Synonym != null)
            {
                var synonymAll = 0;
  
                for (int i = 100; i < regnumList.Synonym.Length; i+= 100)
                {
                    synonymAll += 16;
                }

                var txtSynonymRight = _page.AddTextBox("synonymRight", new PdfPoint(_pdfPointXRight, _pdfPointY),
                    new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight + synonymAll));
                txtSynonymRight.HasBorder = false;
                txtSynonymRight.Multiline = true;
                txtSynonymRight.ReadOnly = true;
                txtSynonymRight.FontSize = 8;
                txtSynonymRight.Text = regnumList.Synonym;
                _pdfPointY += _pdfSizeHeight + synonymAll;   
            }
            else
            {
                _pdfPointY += _pdfSizeHeight;
            }
            //---------------------------------------------------------------
            var txtCommNameLeft = _page.AddTextBox("commNameLeft", new PdfPoint(_pdfPointXLeft + _move, _pdfPointY), 
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtCommNameLeft.HasBorder = false;
            txtCommNameLeft.ReadOnly = true;
            txtCommNameLeft.FontSize = 8;
            txtCommNameLeft.Text = CultRes.StringsRes.ReportCommonNames;
        //    _pdfPointY += _pdfSizeHeight;

            var txtCommNameGerRight = _page.AddTextBox("commNameGerRight", new PdfPoint(_pdfPointXRight, _pdfPointY), 
                new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
            txtCommNameGerRight.HasBorder = false;
            txtCommNameGerRight.ReadOnly = true;
            txtCommNameGerRight.FontSize = 8;
            txtCommNameGerRight.Text = regnumList.GerName + " " + CultRes.StringsRes.ReportGerman;
            _pdfPointY += _pdfSizeHeight;

            var txtCommNameEngRight = _page.AddTextBox("commNameEngRight", new PdfPoint(_pdfPointXRight, _pdfPointY), 
                new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
            txtCommNameEngRight.HasBorder = false;
            txtCommNameEngRight.ReadOnly = true;
            txtCommNameEngRight.FontSize = 8;
            txtCommNameEngRight.Text = regnumList.EngName + " " + CultRes.StringsRes.ReportEnglish;
            _pdfPointY += _pdfSizeHeight;

            var txtCommNameFraRight = _page.AddTextBox("commNameFraRight", new PdfPoint(_pdfPointXRight, _pdfPointY), 
                new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
            txtCommNameFraRight.HasBorder = false;
            txtCommNameFraRight.ReadOnly = true;
            txtCommNameFraRight.FontSize = 8;
            txtCommNameFraRight.Text = regnumList.FraName + " " + CultRes.StringsRes.ReportFrench;
            _pdfPointY += _pdfSizeHeight;

            var txtCommNameSpaRight = _page.AddTextBox("commNameSpaRight", new PdfPoint(_pdfPointXRight, _pdfPointY), 
                new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
            txtCommNameSpaRight.HasBorder = false;
            txtCommNameSpaRight.ReadOnly = true;
            txtCommNameSpaRight.FontSize = 8;
            txtCommNameSpaRight.Text = regnumList.PorName + " " + CultRes.StringsRes.ReportPortuguese;
            _pdfPointY += _pdfSizeHeight;
            _pdfPointY += 5; //Distance to next TextBox

            //-------------------------------------------------------
            var txtStatus = _page.AddTextBox("status", new PdfPoint(_pdfPointXLeft + _move, _pdfPointY), 
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtStatus.HasBorder = false;
            txtStatus.ReadOnly = true;
            txtStatus.Font.SynthesizedBold = true;
            txtStatus.FontSize = 9;
            txtStatus.Text = CultRes.StringsRes.ReportTaxoStatus;
            _pdfPointY += _pdfSizeHeight;
            //---------------------------------------------------------
            var txtCurrStatusLeft = _page.AddTextBox("currStatusLeft", new PdfPoint(_pdfPointXLeft + _move, _pdfPointY), 
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtCurrStatusLeft.HasBorder = false;
            txtCurrStatusLeft.ReadOnly = true;
            txtCurrStatusLeft.Font.SynthesizedBold = false;
            txtCurrStatusLeft.FontSize = 8;
            txtCurrStatusLeft.Text = CultRes.StringsRes.ReportCurrentStand;
        //    _pdfPointY += _pdfSizeHeight;

            if (regnumList.Valid != null)
            {
                var txtCurrStatusRight = _page.AddTextBox("currStatusRight", new PdfPoint(_pdfPointXRight, _pdfPointY), 
                    new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
                txtCurrStatusRight.HasBorder = false;
                txtCurrStatusRight.ReadOnly = true;
                txtCurrStatusRight.FontSize = 8;
                txtCurrStatusRight.Text = regnumList.Valid.ToString();
                _pdfPointY += _pdfSizeHeight;
            }
            else
            {
                _pdfPointY += _pdfSizeHeight;
            }
            _pdfPointY += 2; //Distance to next TextBox

            //-----------------------------------------------------------
            var txtQuali = _page.AddTextBox("quali", new PdfPoint(_pdfPointXLeft + _move, _pdfPointY), 
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtQuali.HasBorder = false;
            txtQuali.Font.SynthesizedBold = true;
            txtQuali.ReadOnly = true;
            txtQuali.FontSize = 9;
            txtQuali.Text = CultRes.StringsRes.ReportDataQualiIndicator;
            _pdfPointY += _pdfSizeHeight;
            //---------------------------------------------------------
            var txtRecordLeft = _page.AddTextBox("recordLeft", new PdfPoint(_pdfPointXLeft + _move, _pdfPointY), 
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtRecordLeft.HasBorder = false;
            txtRecordLeft.Font.SynthesizedBold = false;
            txtRecordLeft.ReadOnly = true;
            txtRecordLeft.FontSize = 8;
            txtRecordLeft.Text = CultRes.StringsRes.ReportRecordUpdate;
            //      _pdfPointY += _pdfSizeHeight;

            var txtRecordRight = _page.AddTextBox("recordRight", new PdfPoint(_pdfPointXRight, _pdfPointY),
                new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
            txtRecordRight.HasBorder = false;
            txtRecordRight.ReadOnly = true;
            txtRecordRight.FontSize = 8;
            txtRecordRight.Text = Convert.ToString(regnumList.UpdaterDate, CultureInfo.InvariantCulture);
            _pdfPointY += _pdfSizeHeight;

            _pdfPointY += 5; //Distance to next TextBox

            //-------------------------------------------------------------
            var txtInfoLeft = _page.AddTextBox("infoLeft", new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtInfoLeft.HasBorder = false;
            txtInfoLeft.ReadOnly = true;
            txtInfoLeft.FontSize = 8;
            txtInfoLeft.Text = CultRes.StringsRes.ReportInfo;


            if (regnumList.Info != null)
            {
                var infoAll = 0;
                for (int i = 100; i < regnumList.Info.Length; i += 100)
                {
                    infoAll += 16;
                }

                var txtInfoRight = _page.AddTextBox("infoRight", new PdfPoint(_pdfPointXRight, _pdfPointY),
                    new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight + infoAll));
                txtInfoRight.HasBorder = false;
                txtInfoRight.Multiline = true;
                txtInfoRight.ReadOnly = true;
                txtInfoRight.FontSize = 8;
                txtInfoRight.Text = regnumList.Info;
                _pdfPointY += _pdfSizeHeight + infoAll - 8;  // -8 Leerzeile
            }
            else
            {
                _pdfPointY += _pdfSizeHeight;
            }
            _pdfPointY += 5; //Distance to next TextBox

            //-------------------------------------------------------
            var txtMemoLeft = _page.AddTextBox("memoLeft", new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtMemoLeft.HasBorder = false;
            txtMemoLeft.ReadOnly = true;
            txtMemoLeft.FontSize = 8;
            txtMemoLeft.Text = CultRes.StringsRes.ReportMemo;

            if (regnumList.Memo != null)
            {
                var memoAll = 0;
                for (int i = 100; i < regnumList.Memo.Length; i += 100)
                {
                    memoAll += 16;
                }

                var txtMemoRight = _page.AddTextBox("memoRight", new PdfPoint(_pdfPointXRight, _pdfPointY),
                    new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight + memoAll));
                txtMemoRight.HasBorder = false;
                txtMemoRight.Multiline = true;
                txtMemoRight.ReadOnly = true;
                txtMemoRight.FontSize = 8;
                txtMemoRight.Text = regnumList.Memo;
                _pdfPointY += _pdfSizeHeight + memoAll;  // -8 Leerzeile
            }
            else
            {
                _pdfPointY += _pdfSizeHeight;
            }
            //------------------------------------------------------------
            _pdfPointY += 8; //Distance to next TextBox

            arrayInts[0] = _pdfPointXLeft;
            arrayInts[1] = _pdfPointY;
            arrayInts[2] = _pdfPointXRight;
            arrayInts[3] = _pdfSizeHeight;
            arrayInts[4] = _pdfSizeWidthLeft;
            arrayInts[5] = _pdfSizeWidthRight;
            arrayInts[6] = _pageCount; 
            arrayInts[7] = _move; //_move

            return arrayInts;
        }
        private static int[] AddRegnumHierarchyList(PdfDocument pdf, Tbl03Regnum regnumList, int[] arrayInts)
        {
            _pdfPointXLeft = arrayInts[0];
            _pdfPointY = arrayInts[1];
            _pdfPointXRight = arrayInts[2];
            _pdfSizeHeight = arrayInts[3];
            _pdfSizeWidthLeft = arrayInts[4];
            _pdfSizeWidthRight = arrayInts[5];
            _pageCount = arrayInts[6];
            _move = _arrInts[7];
            _characterSize = _arrInts[8];
            _fontSize = _arrInts[9];
            _z = _arrInts[10];

            _page = pdf.Pages[_pageCount];

            var txtHeader3 = _page.AddTextBox("header3", new PdfPoint(_pdfPointXLeft, _pdfPointY), 
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtHeader3.HasBorder = false;
            txtHeader3.ReadOnly = true;
            txtHeader3.Font.SynthesizedBold = true;
            txtHeader3.FontSize = 10;
            txtHeader3.Text = CultRes.StringsRes.ReportTaxoHiera;
            _pdfPointY += _pdfSizeHeight;
            _pdfPointY += 5; //Distance to next TextBox

            //---------------------------------------------------------------
            var txtRegnumNameLeft = _page.AddTextBox("regnumLeft", new PdfPoint(_pdfPointXLeft + _move, _pdfPointY), 
                new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
            txtRegnumNameLeft.HasBorder = false;
            txtRegnumNameLeft.ReadOnly = true;
            txtRegnumNameLeft.Font.SynthesizedBold = false;
            txtRegnumNameLeft.FontSize = 8;
            txtRegnumNameLeft.Text = CultRes.StringsRes.Regnum;
         //   _pdfPointY += _pdfSizeHeight;

            var txtRegnumNameRight = _page.AddTextBox("regnumRight", new PdfPoint(_pdfPointXRight, _pdfPointY), 
                new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
            txtRegnumNameRight.HasBorder = false;
            txtRegnumNameRight.ReadOnly = true;
            txtRegnumNameRight.Multiline = true;
            txtRegnumNameRight.FontSize = 8;
            txtRegnumNameRight.Text = regnumList.RegnumName + " " + regnumList.Subregnum + " - " + regnumList.Author + ", " + regnumList.AuthorYear + " - " + regnumList.GerName + " " + regnumList.EngName + " " + regnumList.FraName + " " + regnumList.PorName;
            _pdfPointY += _pdfSizeHeight;

            _pdfPointY += 4; //Distance to next TextBox

            arrayInts[0] = _pdfPointXLeft;
            arrayInts[1] = _pdfPointY;
            arrayInts[2] = _pdfPointXRight;
            arrayInts[3] = _pdfSizeHeight;
            arrayInts[4] = _pdfSizeWidthLeft;
            arrayInts[5] = _pdfSizeWidthRight;
            arrayInts[6] = _pageCount;
            arrayInts[7] = _move;
            return arrayInts;
        }
        private static int[] AddPhylumsChildrenList(PdfDocument pdf, ObservableCollection<Tbl06Phylum> phylumsList, int[] arrayInts)
        {
            _pdfPointXLeft = arrayInts[0];
            _pdfPointY = arrayInts[1];
            _pdfPointXRight = arrayInts[2];
            _pdfSizeHeight = arrayInts[3];
            _pdfSizeWidthLeft = arrayInts[4];
            _pdfSizeWidthRight = arrayInts[5];
            _pageCount = arrayInts[6];
            _move = _arrInts[7];
            _characterSize = _arrInts[8];
            _fontSize = _arrInts[9];
            _z = _arrInts[10];

            _page = pdf.Pages[_pageCount];

            var txtChildren = _page.AddTextBox("childrenPhylum", new PdfPoint(_pdfPointXRight, _pdfPointY), 
                new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
            txtChildren.HasBorder = false;
            txtChildren.ReadOnly = true;
            txtChildren.Font.SynthesizedBold = true;
            txtChildren.FontSize = 9;
            txtChildren.Text = CultRes.StringsRes.ReportDirectChild;
            _pdfPointY += _pdfSizeHeight;

            _pdfPointY += 4; //Distance to next TextBox

            //------------------------------------------------------------------

            _n = "childPhylum";
            _z = 1;
            _z1 = _n + _z;
            _move += +_move;   // 4+4

            foreach (var t in phylumsList)
            {
                var t1 = t.PhylumName;
                var tAllLength = 0;

                if (t1 != null) tAllLength = t1.Length;
                var t2 = t.Author;
                if (t2 != null) tAllLength += t2.Length;
                var t3 = t.AuthorYear; 
                if (t3 != null) tAllLength += t3.Length;
                var t4 = t.GerName;
                if (t4 != null) tAllLength += t4.Length;
                var t5 = t.EngName;
                if (t5 != null) tAllLength += t5.Length;
                var t6 = t.FraName;
                if (t6 != null) tAllLength += t6.Length;
                var t7 = t.PorName;
                if (t7 != null) tAllLength += t7.Length;

                var txtPhylumNameLeft = _page.AddTextBox("phylumLeft" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                    new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                txtPhylumNameLeft.HasBorder = false;
                txtPhylumNameLeft.ReadOnly = true;
                txtPhylumNameLeft.Font.SynthesizedBold = false;
                txtPhylumNameLeft.FontSize = 8;
                txtPhylumNameLeft.Text = CultRes.StringsRes.Phylum;

                var phylumAll = 0;
                for (int i = 100; i < tAllLength; i += 100)
                {
                    phylumAll += 16;
                }

                if (tAllLength >= 100)
                {
                    var txtPhylumNameRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                        new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight + phylumAll));
                    txtPhylumNameRight.HasBorder = false;
                    txtPhylumNameRight.ReadOnly = true;
                    txtPhylumNameRight.Multiline = true;
                    txtPhylumNameRight.FontSize = 8;
                    txtPhylumNameRight.Text = t1 + " - " + t2 + ", " + t3 + " - " + t4 + " " + t5 + " " + t6 + " " + t7;
                    _pdfPointY += _pdfSizeHeight + phylumAll -4 ;  // -8 Leerzeile
                }
                else
                {
                    var txtPhylumNameRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                        new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
                    txtPhylumNameRight.HasBorder = false;
                    txtPhylumNameRight.ReadOnly = true;
                    txtPhylumNameRight.FontSize = 8;
                    txtPhylumNameRight.Text = t1 + " - " + t2 + ", " + t3 + " - " + t4 + " " + t5 + " " + t6 + " " + t7;
                    _pdfPointY += _pdfSizeHeight;
                }

                _z += 1;
                _z1 = _n + _z;
                //var author = PdfHelper.AuthorViewChangeWithString(t.Author, t.AuthorYear);
                //var names = PdfHelper.NamesViewChange(t.GerName, t.EngName, t.FraName, t.PorName);
            }

            _pdfPointY += 5; //Distance to next TextBox

            arrayInts[0] = _pdfPointXLeft;
            arrayInts[1] = _pdfPointY;
            arrayInts[2] = _pdfPointXRight;
            arrayInts[3] = _pdfSizeHeight;
            arrayInts[4] = _pdfSizeWidthLeft;
            arrayInts[5] = _pdfSizeWidthRight;
            arrayInts[6] = _pageCount;
            arrayInts[7] = _move; 

            return arrayInts;
        }
        private static int[] AddDivisionsChildrenList(PdfDocument pdf, ObservableCollection<Tbl09Division> divisionsList, int[] arrayInts)
        {
            _pdfPointXLeft = arrayInts[0];
            _pdfPointY = arrayInts[1];
            _pdfPointXRight = arrayInts[2];
            _pdfSizeHeight = arrayInts[3];
            _pdfSizeWidthLeft = arrayInts[4];
            _pdfSizeWidthRight = arrayInts[5];
            _pageCount = arrayInts[6];
            _move = _arrInts[7];
            _characterSize = _arrInts[8];
            _fontSize = _arrInts[9];
            _z = _arrInts[10];

            _page = pdf.Pages[_pageCount];

            var txtChildren = _page.AddTextBox("childrenDivision", new PdfPoint(_pdfPointXRight, _pdfPointY),
                new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
            txtChildren.HasBorder = false;
            txtChildren.ReadOnly = true;
            txtChildren.Font.SynthesizedBold = true;
            txtChildren.FontSize = 9;
            txtChildren.Text = CultRes.StringsRes.ReportDirectChild;
            _pdfPointY += _pdfSizeHeight;

            _pdfPointY += 4; //Distance to next TextBox

            //------------------------------------------------------------------

            _n = "childDivision";
            _z = 1;
            _z1 = _n + _z;
            _move += +_move;   // 4+4

            foreach (var t in divisionsList)
            {
                var t1 = t.DivisionName;
                var tAllLength = 0;

                if (t1 != null) tAllLength = t1.Length;
                var t2 = t.Author;
                if (t2 != null) tAllLength += t2.Length;
                var t3 = t.AuthorYear;
                if (t3 != null) tAllLength += t3.Length;
                var t4 = t.GerName;
                if (t4 != null) tAllLength += t4.Length;
                var t5 = t.EngName;
                if (t5 != null) tAllLength += t5.Length;
                var t6 = t.FraName;
                if (t6 != null) tAllLength += t6.Length;
                var t7 = t.PorName;
                if (t7 != null) tAllLength += t7.Length;

                var txtDivisionNameLeft = _page.AddTextBox("divisionLeft" + _z1, new PdfPoint(_pdfPointXLeft + _move, _pdfPointY),
                    new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
                txtDivisionNameLeft.HasBorder = false;
                txtDivisionNameLeft.ReadOnly = true;
                txtDivisionNameLeft.Font.SynthesizedBold = false;
                txtDivisionNameLeft.FontSize = 8;
                txtDivisionNameLeft.Text = CultRes.StringsRes.Division;

                var divisionAll = 0;
                for (int i = 100; i < tAllLength; i += 100)
                {
                    divisionAll += 16;
                }

                if (tAllLength >= 100)
                {
                    var txtDivisionNameRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                        new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight + divisionAll));
                    txtDivisionNameRight.HasBorder = false;
                    txtDivisionNameRight.ReadOnly = true;
                    txtDivisionNameRight.Multiline = true;
                    txtDivisionNameRight.FontSize = 8;
                    txtDivisionNameRight.Text = t1 + " - " + t2 + ", " + t3 + " - " + t4 + " " + t5 + " " + t6 + " " + t7;
                    _pdfPointY += _pdfSizeHeight + divisionAll - 4;  // -8 Leerzeile
                }
                else
                {
                    var txtDivissionNameRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
                        new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
                    txtDivissionNameRight.HasBorder = false;
                    txtDivissionNameRight.ReadOnly = true;
                    txtDivissionNameRight.FontSize = 8;
                    txtDivissionNameRight.Text = t1 + " - " + t2 + ", " + t3 + " - " + t4 + " " + t5 + " " + t6 + " " + t7;
                    _pdfPointY += _pdfSizeHeight;
                }

                _z += 1;
                _z1 = _n + _z;
                //var author = PdfHelper.AuthorViewChangeWithString(t.Author, t.AuthorYear);
                //var names = PdfHelper.NamesViewChange(t.GerName, t.EngName, t.FraName, t.PorName);
            }

            _pdfPointY += 5; //Distance to next TextBox

            arrayInts[0] = _pdfPointXLeft;
            arrayInts[1] = _pdfPointY;
            arrayInts[2] = _pdfPointXRight;
            arrayInts[3] = _pdfSizeHeight;
            arrayInts[4] = _pdfSizeWidthLeft;
            arrayInts[5] = _pdfSizeWidthRight;
            arrayInts[6] = _pageCount;
            arrayInts[7] = _move;

            return arrayInts;
        }

        //private static void AddReferencesHaeder(PdfDocument pdf)
        //{
        //    pdf.AddPage();
        //    _page = pdf.Pages[_pageCount + 1];
        //    _pdfPointY = 5;

        //    //PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportReferences));
        //    var txtRefHeader = _page.AddTextBox("references", new PdfPoint(_pdfPointXLeft, _pdfPointY),
        //        new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight + 16));
        //    txtRefHeader.HasBorder = false;
        //    txtRefHeader.ReadOnly = true;
        //    txtRefHeader.Font.SynthesizedBold = true;
        //    txtRefHeader.FontSize = 16;
        //    txtRefHeader.Text = "References";
        //    _pdfPointY += _pdfSizeHeight;
        //}

        //private static void AddRefExpertList(PdfDocument pdf, ObservableCollection<Tbl90Reference> expertsList)
        //{
        //    if (expertsList.Count >= 3)
        //    {
        //        pdf.AddPage();
        //        _page = pdf.Pages[_pageCount + 1];
        //        _pdfPointY = 5;
        //    }
        //}

        //private static int[] AddRefSourceList(PdfDocument pdf, ObservableCollection<Tbl90Reference> sourcesList, int[] _arrInts)
        //{
        //    if (sourcesList.Count >= 3)
        //    {
        //        pdf.AddPage();
        //        _page = pdf.Pages[_pageCount + 1];
        //        _pdfPointY = 5;
        //    }
        //    var txtExpertNameLeft = _page.AddTextBox("experts", new PdfPoint(_pdfPointXLeft, _pdfPointY), new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
        //    txtExpertNameLeft.HasBorder = false;
        //    txtExpertNameLeft.ReadOnly = true;
        //    txtExpertNameLeft.Font.SynthesizedBold = false;
        //    txtExpertNameLeft.FontSize = 10;
        //    txtExpertNameLeft.Text = "Andere Quelle(n)";
        //    _pdfPointY += _pdfSizeHeight;


        //    _n = "source";
        //    _z = 1;
        //    _z1 = _n + _z;
        //    //      _v2 = 0;
        //    foreach (var t in sourcesList)
        //    {
        //        var t1 = t.Tbl90RefSources.RefSourceName;
        //        var t2 = t.Tbl90RefSources.SourceYear;
        //        var t3 = t.Tbl90RefSources.Notes;
        //        var t4 = t.Tbl90RefSources.Author;
        //        var t5 = t.Tbl90RefSources.Memo;
        //        var t6 = t.Valid;
        //        var t7 = t.ValidYear;
        //        var t8 = t.Info;
        //        var t9 = t.Memo;

        //        var txtExperLeft = _page.AddTextBox("expert" + _z1, new PdfPoint(_pdfPointXLeft, _pdfPointY),
        //            new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
        //        txtExperLeft.HasBorder = false;
        //        txtExperLeft.ReadOnly = true;
        //        txtExperLeft.Font.SynthesizedBold = false;
        //        txtExperLeft.FontSize = 10;
        //        txtExperLeft.Text = "Quelle";
        //        //   _pdfPointY += _pdfSizeHeight;

        //        if (t1 != null)
        //        {
        //            var txtSourceRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
        //                new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight + 30));
        //            txtSourceRight.HasBorder = false;
        //            txtSourceRight.ReadOnly = true;
        //            txtSourceRight.Multiline = true;
        //            txtSourceRight.FontSize = 10;
        //            txtSourceRight.Text = t1;
        //            _pdfPointY += _pdfSizeHeight;
        //        }
        //        else
        //        {
        //            _pdfPointY += _pdfSizeHeight;
        //        }

        //        _z += 1;
        //        _z1 = _n + _z;

        //        var txtErfasstLeft = _page.AddTextBox("erfasst" + _z1, new PdfPoint(_pdfPointXLeft, _pdfPointY), new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
        //        txtErfasstLeft.HasBorder = false;
        //        txtErfasstLeft.ReadOnly = true;
        //        txtErfasstLeft.Font.SynthesizedBold = false;
        //        txtErfasstLeft.FontSize = 10;
        //        txtErfasstLeft.Text = "Erfasst";
        //        //   _pdfPointY += _pdfSizeHeight;

        //        if (t2 != null)
        //        {
        //            var txtErfasstRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
        //                new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
        //            txtErfasstRight.HasBorder = false;
        //            txtErfasstRight.ReadOnly = true;
        //            txtErfasstRight.FontSize = 10;
        //            txtErfasstRight.Text = t2;
        //            _pdfPointY += _pdfSizeHeight;
        //        }
        //        else
        //        {
        //            _pdfPointY += _pdfSizeHeight;
        //        }
        //        _z += 1;
        //        _z1 = _n + _z;

        //        var txtNotesLeft = _page.AddTextBox("notes" + _z1, new PdfPoint(_pdfPointXLeft, _pdfPointY), new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
        //        txtNotesLeft.HasBorder = false;
        //        txtNotesLeft.ReadOnly = true;
        //        txtNotesLeft.Font.SynthesizedBold = false;
        //        txtNotesLeft.FontSize = 10;
        //        txtNotesLeft.Text = "Berichte";
        //        //   _pdfPointY += _pdfSizeHeight;

        //        if (t3 != null)
        //        {

        //            var txtNoteRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
        //                new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight + 30));
        //            txtNoteRight.HasBorder = false;
        //            txtNoteRight.ReadOnly = true;
        //            txtNoteRight.Multiline = true;
        //            txtNoteRight.FontSize = 10;
        //            txtNoteRight.Text = t3;
        //            _pdfPointY += _pdfSizeHeight;
        //        }
        //        else
        //        {
        //            _pdfPointY += _pdfSizeHeight;
        //        }

        //        _z += 1;
        //        _z1 = _n + _z;

        //        var txtAuthorLeft = _page.AddTextBox("author" + _z1, new PdfPoint(_pdfPointXLeft, _pdfPointY), new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
        //        txtAuthorLeft.HasBorder = false;
        //        txtAuthorLeft.ReadOnly = true;
        //        txtAuthorLeft.Font.SynthesizedBold = false;
        //        txtAuthorLeft.FontSize = 10;
        //        txtAuthorLeft.Text = "Autor";
        //        //   _pdfPointY += _pdfSizeHeight;

        //        if (t4 != null)
        //        {
        //            var txtAuthorRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY), new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
        //            txtAuthorRight.HasBorder = false;
        //            txtAuthorRight.ReadOnly = true;
        //            txtAuthorRight.FontSize = 10;
        //            txtAuthorRight.Text = t4;
        //            _pdfPointY += _pdfSizeHeight;
        //        }
        //        else
        //        {
        //            _pdfPointY += _pdfSizeHeight;
        //        }
        //        _z += 1;
        //        _z1 = _n + _z;

        //        var txtMemoLeft = _page.AddTextBox("memo" + _z1, new PdfPoint(_pdfPointXLeft, _pdfPointY), new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
        //        txtMemoLeft.HasBorder = false;
        //        txtMemoLeft.ReadOnly = true;
        //        txtMemoLeft.Font.SynthesizedBold = false;
        //        txtMemoLeft.FontSize = 10;
        //        txtMemoLeft.Text = "Memo";
        //        //   _pdfPointY += _pdfSizeHeight;

        //        if (t5 != null)
        //        {
        //            var txtMemoRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
        //                new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight + 30));
        //            txtMemoRight.HasBorder = false;
        //            txtMemoRight.ReadOnly = true;
        //            txtMemoRight.Multiline = true;
        //            txtMemoRight.FontSize = 10;
        //            txtMemoRight.Text = t5;
        //            _pdfPointY += _pdfSizeHeight;
        //        }
        //        else
        //        {
        //            _pdfPointY += _pdfSizeHeight;
        //        }
        //        _z += 1;
        //        _z1 = _n + _z;

        //        var txtValidLeft = _page.AddTextBox("Valid" + _z1, new PdfPoint(_pdfPointXLeft, _pdfPointY), new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
        //        txtValidLeft.HasBorder = false;
        //        txtValidLeft.ReadOnly = true;
        //        txtValidLeft.Font.SynthesizedBold = false;
        //        txtValidLeft.FontSize = 10;
        //        txtValidLeft.Text = "Gültig";
        //        //   _pdfPointY += _pdfSizeHeight;

        //        if (t6 != null)
        //        {
        //            var txtValidRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY), new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
        //            txtValidRight.HasBorder = false;
        //            txtValidRight.ReadOnly = true;
        //            txtValidRight.FontSize = 10;
        //            txtValidRight.Text = t6.ToString();
        //            _pdfPointY += _pdfSizeHeight;
        //        }
        //        else
        //        {
        //            _pdfPointY += _pdfSizeHeight;
        //        }
        //        _z += 1;
        //        _z1 = _n + _z;

        //        var txtValidYearLeft = _page.AddTextBox("ValidYear" + _z1, new PdfPoint(_pdfPointXLeft, _pdfPointY), new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
        //        txtValidYearLeft.HasBorder = false;
        //        txtValidYearLeft.ReadOnly = true;
        //        txtValidYearLeft.Font.SynthesizedBold = false;
        //        txtValidYearLeft.FontSize = 10;
        //        txtValidYearLeft.Text = "Year";
        //        //   _pdfPointY += _pdfSizeHeight;

        //        if (t7 != null)
        //        {
        //            var txtValidYearRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
        //                new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
        //            txtValidYearRight.HasBorder = false;
        //            txtValidYearRight.ReadOnly = true;
        //            txtValidYearRight.FontSize = 10;
        //            txtValidYearRight.Text = t7;
        //            _pdfPointY += _pdfSizeHeight;
        //        }
        //        else
        //        {
        //            _pdfPointY += _pdfSizeHeight;
        //        }
        //        _z += 1;
        //        _z1 = _n + _z;

        //        var txtBezugLeft = _page.AddTextBox("bezug" + _z1, new PdfPoint(_pdfPointXLeft, _pdfPointY), new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
        //        txtBezugLeft.HasBorder = false;
        //        txtBezugLeft.ReadOnly = true;
        //        txtBezugLeft.Font.SynthesizedBold = false;
        //        txtBezugLeft.FontSize = 10;
        //        txtBezugLeft.Text = "Bezug auf";
        //        //   _pdfPointY += _pdfSizeHeight;

        //        if (t8 != null)
        //        {
        //            var txtBezugRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
        //                new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight));
        //            txtBezugRight.HasBorder = false;
        //            txtBezugRight.ReadOnly = true;
        //            txtBezugRight.FontSize = 10;
        //            txtBezugRight.Text = t8;
        //            _pdfPointY += _pdfSizeHeight;
        //        }
        //        else
        //        {
        //            _pdfPointY += _pdfSizeHeight;
        //        }
        //        _z += 1;
        //        _z1 = _n + _z;

        //        var txtSourceMemoLeft = _page.AddTextBox("sourceMemo" + _z1, new PdfPoint(_pdfPointXLeft, _pdfPointY), new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight));
        //        txtSourceMemoLeft.HasBorder = false;
        //        txtSourceMemoLeft.ReadOnly = true;
        //        txtSourceMemoLeft.Font.SynthesizedBold = false;
        //        txtSourceMemoLeft.FontSize = 10;
        //        txtSourceMemoLeft.Text = "Memo";
        //        //   _pdfPointY += _pdfSizeHeight;

        //        if (t9 != null)
        //        {
        //            var txtSourceMemoRight = _page.AddTextBox(_z1, new PdfPoint(_pdfPointXRight, _pdfPointY),
        //                new PdfSize(_pdfSizeWidthRight, _pdfSizeHeight + 30));
        //            txtSourceMemoRight.HasBorder = false;
        //            txtSourceMemoRight.ReadOnly = true;
        //            txtSourceMemoRight.Multiline = true;
        //            txtSourceMemoRight.FontSize = 10;
        //            txtSourceMemoRight.Text = t9;
        //            _pdfPointY += _pdfSizeHeight;
        //        }
        //        else
        //        {
        //            _pdfPointY += _pdfSizeHeight;
        //        }
        //        _z += 1;
        //        _z1 = _n + _z;

        //        //var author = PdfHelper.AuthorViewChangeWithString(t.Author, t.AuthorYear);
        //        //var names = PdfHelper.NamesViewChange(t.GerName, t.EngName, t.FraName, t.PorName);
        //    }
        //}
        //private static void AddRefAuthorList(PdfDocument pdf, ObservableCollection<Tbl90Reference> authorsList, int[] _arrInts)
        //{
        //    if (authorsList.Count >= 3)
        //    {
        //        pdf.AddPage();
        //        _page = pdf.Pages[_pageCount + 1];
        //        _pdfPointY = 5;
        //    }
        //}

        //private static void AddCommentsHaeder(PdfDocument pdf)
        //{
        //    ////PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportComments));
        //    var txtCommHeader = _page.AddTextBox("comments", new PdfPoint(_pdfPointXLeft, _pdfPointY),
        //        new PdfSize(_pdfSizeWidthLeft, _pdfSizeHeight + 16));
        //    txtCommHeader.HasBorder = false;
        //    txtCommHeader.ReadOnly = true;
        //    txtCommHeader.Font.SynthesizedBold = true;
        //    txtCommHeader.FontSize = 16;
        //    txtCommHeader.Text = "Comments";
        //    _pdfPointY += _pdfSizeHeight;

        //}

        //private static void AddCommentList(PdfDocument pdf, ObservableCollection<Tbl93Comment> commentsList)
        //{
        //    if (commentsList.Count >= 3)
        //    {
        //        pdf.AddPage();
        //        _page = pdf.Pages[_pageCount + 1];
        //        _pdfPointY = 5;
        //    }
        //}


        //private ObservableCollection<Tbl06Phylum> _tbl06PhylumsList;
        //public ObservableCollection<Tbl06Phylum> Tbl06PhylumsList
        //{
        //    get => _tbl06PhylumsList;
        //    set { _tbl06PhylumsList = value; RaisePropertyChanged(""); }
        //}

        //private ObservableCollection<Tbl03Regnum> _tbl03RegnumsList;
        //public static ObservableCollection<Tbl03Regnum> Tbl03RegnumsList
        //{
        //    get => _tbl03RegnumsList;
        //    set { _tbl03RegnumsList = value; RaisePropertyChanged(""); }
        //}

        //public static ObservableCollection<Tbl06Phylum> phylumsList { get; set; }
        public IList<Tbl03Regnum> Tbl03RegnumsSearchResults { get; set; }



        ////public static PdfDocument HeaderMainPdf(SaveFileDialog sfd)
        ////{

        ////    // Initialize the PDF document 
        ////    // Set up the fonts to be used on the pages 
        ////    //  var margin = Utilities.MillimetersToPoints(Convert.ToSingle(5));
        ////    // var doc = new PdfDocument(PageSize.A4, margin, margin, margin, margin);

        ////    //  PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
        ////    var pdf = new PdfDocument();
        ////    // Set the margins and page size
        ////    SetStandardPageSize(pdf);

        ////    // Open the document for writing content 
        //// //   doc.OnOpenDocument();
        ////    return pdf;
        ////}

        public static void SetStandardPageSize(PdfDocument doc)
        {
            // Set margins and page size for the document
            //   doc.SetMargins(10, 10, 10, 0);
            // There are a huge number of possible page sizes, including such sizes as
            // EXECUTIVE, POSTCARD, LEDGER, LEGAL, LETTER_LANDSCAPE, and NOTE
            //  doc.SetPageSize(new Rectangle(PageSize.LETTER.Width, PageSize.LETTER.Height));
        }

        //using (var pdf = new PdfDocument())
        //{

        //    AddReportMain(pdf);





        //    drawTableBody(canvas);
        //    // AddPage method adds a new page to the end of the PDF document
        //    PdfPage secondPage = pdf.AddPage();
        //    secondPage.Canvas.DrawString("Second page");

        //    PdfPage lastPage = pdf.InsertPage(pdf.PageCount);
        //    lastPage.Canvas.DrawString("Last page");

        //    //for (int i = 0; i < pdf.PageCount; ++i)
        //    //{
        //    //    PdfPage page = pdf.Pages[i];
        //    //    DrawHeader(page, font);
        //    //    DrawFooter(i, page, font);
        //    //}
        //    var regnumList = _uow.Tbl03Regnums.GetById(id);

        //    //       IEnumerable regnumList = _uow.Tbl03Regnums.Find(e => e.RegnumId == id);
        //    //  IEnumerable regnumList = _uow.Tbl03Regnums.Find(e => e.RegnumId == id);

        //    PdfTextBox txtId = pdf.Pages[0].AddTextBox("regnumId", new PdfPoint(20, 50), new PdfSize(200, 40));
        //    txtId.HasBorder = false;
        //    txtId.ReadOnly = true;
        //    txtId.FontSize = 20;
        //    txtId.Text = regnumList.RegnumId.ToString();

        //    pdf.Info.Author = "Sample Browser application";

        //    PdfTextBox txtName = pdf.Pages[0].AddTextBox("regnumName", new PdfPoint(20, 50), new PdfSize(200, 40));
        //    txtName.HasBorder = false;
        //    txtName.ReadOnly = true;
        //    txtName.FontSize = 20;
        //    txtName.Text = regnumList.RegnumName;


        //    //if (regnumList != null)
        //    //    foreach (string regnumName in regnumList())
        //    //    {
        //    //        textBox.Text = regnumName;

        //    //      //  pdf.Save(clientName + ".pdf");
        //    //    }

        //    pdf.Save(pathToFile);
        //}

        //      Process.Start(pathToFile);

    }

    //private static void AddReportMain(PdfDocument pdf)
    //    {
    //        PdfFont font = pdf.AddFont(PdfBuiltInFont.Helvetica);

    //        PdfCanvas canvas = pdf.Pages[0].Canvas;
    //        double scale = pdf.Pages[0].Resolution / 150;
    //        canvas.ScaleTransform(scale, scale);

    //        canvas.Brush.Color = new PdfRgbColor(0, 255, 0);
    //        canvas.DrawRectangle(new PdfRectangle(50, 450, 1150, 150), PdfDrawMode.Fill);

    //        // There is already one page in PDF document after creation
    //        PdfPage firstPage = pdf.Pages[0];
    //        firstPage.Canvas.DrawString("This is the auto-created first page");

    //        pdf.Info.Author = "Sample Browser application";
    //        pdf.Info.Subject = "Document metadata";
    //        pdf.Info.Title = "Custom title goes here";
    //        pdf.Info.Keywords = "pdf, Docotic.Pdf";

    //        DrawHeader(firstPage, font);




    //        PdfImage image = pdf.AddImage("Privat.jpg", new PdfRgbColor(255, 0, 255));
    //        canvas.DrawImage(image, 300, 200);

    //    }
    //    private static void DrawHeader(PdfPage page, PdfFont font)
    //    {
    //        const string headerText = "Report";

    //        var canvas = page.Canvas;
    //        canvas.Font = font;
    //        canvas.FontSize = 30;

    //        double textWidth = canvas.GetTextWidth(headerText);
    //        double rotatedPageWidth = GetRotatedPageSize(page).Width;
    //        var headerPosition = new PdfPoint((rotatedPageWidth - textWidth) / 2, 10);


    //        ShowTextAtRotatedPage(headerText, headerPosition, page);
    //    }

    //    private static void drawTableBody(PdfCanvas canvas)
    //    {
    //        PdfPoint bodyLeftCorner = new PdfPoint(m_leftTableCorner.X, m_leftTableCorner.Y + RowHeight);
    //        PdfRectangle firstCellBounds = new PdfRectangle(bodyLeftCorner, new PdfSize(m_columnWidths[0], RowHeight * 2));
    //        canvas.DrawRectangle(firstCellBounds);
    //        canvas.DrawString("Docotic.Pdf", firstCellBounds, PdfTextAlign.Center, PdfVerticalAlign.Center);

    //        PdfRectangle[,] cells = new PdfRectangle[2, 2]
    //        {
    //            {
    //                new PdfRectangle(bodyLeftCorner.X + m_columnWidths[0], bodyLeftCorner.Y, m_columnWidths[1], RowHeight),
    //                new PdfRectangle(bodyLeftCorner.X + m_columnWidths[0] + m_columnWidths[1], bodyLeftCorner.Y, m_columnWidths[2], RowHeight)
    //            },
    //            {
    //                new PdfRectangle(bodyLeftCorner.X + m_columnWidths[0], bodyLeftCorner.Y + RowHeight, m_columnWidths[1], RowHeight),
    //                new PdfRectangle(bodyLeftCorner.X + m_columnWidths[0] + m_columnWidths[1], bodyLeftCorner.Y + RowHeight, m_columnWidths[2], RowHeight)
    //            }
    //        };

    //        foreach (PdfRectangle rect in cells)
    //            canvas.DrawRectangle(rect, PdfDrawMode.Stroke);

    //        canvas.DrawString("Application License", cells[0, 0], PdfTextAlign.Center, PdfVerticalAlign.Center);
    //        canvas.DrawString("For end-user applications", cells[0, 1], PdfTextAlign.Center, PdfVerticalAlign.Center);
    //        canvas.DrawString("Server License", cells[1, 0], PdfTextAlign.Center, PdfVerticalAlign.Center);
    //        canvas.DrawString("For server-based services", cells[1, 1], PdfTextAlign.Center, PdfVerticalAlign.Center);
    //    }
    //    private static void DrawFooter(int pageIndex, PdfPage page, PdfFont font)
    //    {
    //        var canvas = page.Canvas;
    //        canvas.Font = font;
    //        canvas.FontSize = 14;

    //        PdfSize rotatedPageSize = GetRotatedPageSize(page);
    //        var paddingFromCorner = new PdfSize(30, 30);
    //        var positionRightBottom = new PdfPoint(
    //            rotatedPageSize.Width - paddingFromCorner.Width,
    //            rotatedPageSize.Height - paddingFromCorner.Height
    //        );

    //        ShowTextAtRotatedPage((pageIndex + 1).ToString(), positionRightBottom, page);
    //    }

    //    private static PdfSize GetRotatedPageSize(PdfPage page)
    //    {
    //        if (page.Rotation == PdfRotation.Rotate90 || page.Rotation == PdfRotation.Rotate270)
    //            return new PdfSize(page.Height, page.Width);

    //        return new PdfSize(page.Width, page.Height);
    //    }

    //    private static void ShowTextAtRotatedPage(string text, PdfPoint position, PdfPage page)
    //    {
    //        PdfCanvas canvas = page.Canvas;
    //        if (page.Rotation == PdfRotation.None)
    //        {
    //            canvas.DrawString(position.X, position.Y, text);
    //            return;
    //        }

    //        canvas.SaveState();

    //        switch (page.Rotation)
    //        {
    //            case PdfRotation.Rotate90:
    //                canvas.TranslateTransform(position.Y, page.Height - position.X);
    //                break;

    //            case PdfRotation.Rotate180:
    //                canvas.TranslateTransform(page.Width - position.X, page.Height - position.Y);
    //                break;

    //            case PdfRotation.Rotate270:
    //                canvas.TranslateTransform(page.Width - position.Y, position.X);
    //                break;
    //        }
    //        canvas.RotateTransform((int)page.Rotation * 90);
    //        canvas.DrawString(0, 0, text);

    //        canvas.RestoreState();
    //    }



    //    private static FontStyle GetFontStyle(PdfFont font)
    //    {
    //        FontStyle fontStyle = FontStyle.Regular;

    //        if (font.Bold)
    //            fontStyle |= FontStyle.Bold;

    //        if (font.Italic)
    //            fontStyle |= FontStyle.Italic;

    //        return fontStyle;
    //    }

    //    private static void SetBrush(PdfBrush dst, PdfBrushInfo src)
    //    {
    //        PdfColor color = src.Color;
    //        if (color != null)
    //            dst.Color = color;

    //        dst.Opacity = src.Opacity;

    //        var pattern = src.Pattern;
    //        if (pattern != null)
    //            dst.Pattern = pattern;
    //    }
    //    private static void SetPen(PdfPen dst, PdfPenInfo src)
    //    {
    //        PdfColor color = src.Color;
    //        if (color != null)
    //            dst.Color = color;

    //        var pattern = src.Pattern;
    //        if (pattern != null)
    //            dst.Pattern = pattern;

    //        dst.DashPattern = src.DashPattern;
    //        dst.EndCap = src.EndCap;
    //        dst.LineJoin = src.LineJoin;
    //        dst.MiterLimit = src.MiterLimit;
    //        dst.Opacity = src.Opacity;
    //        dst.Width = src.Width;
    //    }

    //    private static void AppendPath(PdfCanvas target, PdfPath path)
    //    {
    //        foreach (PdfSubpath subpath in path.Subpaths)
    //        {
    //            foreach (PdfPathSegment segment in subpath.Segments)
    //            {
    //                switch (segment.Type)
    //                {
    //                    case PdfPathSegmentType.Point:
    //                        target.CurrentPosition = ((PdfPointSegment)segment).Value;
    //                        break;

    //                    case PdfPathSegmentType.Line:
    //                        PdfLineSegment line = (PdfLineSegment)segment;
    //                        target.CurrentPosition = line.Start;
    //                        target.AppendLineTo(line.End);
    //                        break;

    //                    case PdfPathSegmentType.Bezier:
    //                        PdfBezierSegment bezier = (PdfBezierSegment)segment;
    //                        target.CurrentPosition = bezier.Start;
    //                        target.AppendCurveTo(bezier.FirstControl, bezier.SecondControl, bezier.End);
    //                        break;

    //                    case PdfPathSegmentType.Rectangle:
    //                        target.AppendRectangle(((PdfRectangleSegment)segment).Bounds);
    //                        break;

    //                    case PdfPathSegmentType.CloseSubpath:
    //                        target.ClosePath();
    //                        break;
    //                }
    //            }
    //        }
    //    }

    //    private static void DrawPath(PdfCanvas target, PdfPath path)
    //    {
    //        switch (path.PaintMode)
    //        {
    //            case PdfDrawMode.Fill:
    //                target.FillPath(path.FillMode.Value);
    //                break;

    //            case PdfDrawMode.FillAndStroke:
    //                target.FillAndStrokePath(path.FillMode.Value);
    //                break;

    //            case PdfDrawMode.Stroke:
    //                target.StrokePath();
    //                break;

    //            default:
    //                target.ResetPath();
    //                break;
    //        }
    //    }

    //    private static void DrawText(PdfCanvas target, PdfTextData td)
    //    {
    //        target.TextRenderingMode = td.RenderingMode;
    //        SetBrush(target.Brush, td.Brush);
    //        SetPen(target.Pen, td.Pen);

    //        target.TextPosition = PdfPoint.Empty;
    //        target.FontSize = td.FontSize;
    //        target.Font = td.Font;
    //        target.CharacterSpacing = td.CharacterSpacing;
    //        target.WordSpacing = td.WordSpacing;
    //        target.TextHorizontalScaling = td.HorizontalScaling;

    //        target.TranslateTransform(td.Position.X, td.Position.Y);
    //        target.Transform(td.TransformationMatrix);

    //        if (!td.Text.Contains("have"))
    //            target.DrawString(td.GetCharacterCodes());
    //        else
    //            target.DrawString(td.Text.Replace("have", "has"));
    //    }


        //using (PdfDocument pdf = new PdfDocument())
        //{
        //    PdfCanvas canvas = pdf.Pages[0].Canvas;
        //    DrawHeader(canvas);
        //    DrawTableBody(canvas);


        //    pdf.Info.Author = "Sample Browser application";
        //    pdf.Info.Subject = "Document metadata";
        //    pdf.Info.Title = "Custom title goes here";
        //    pdf.Info.Keywords = "pdf, Docotic.Pdf";


        //    pdf.ViewerPreferences.CenterWindow = true;
        //    pdf.ViewerPreferences.FitWindow = true;
        //    pdf.ViewerPreferences.HideMenuBar = true;
        //    pdf.ViewerPreferences.HideToolBar = true;
        //    pdf.ViewerPreferences.HideWindowUI = true;

        //    pdf.Info.Title = "Test title";
        //    pdf.ViewerPreferences.DisplayTitle = true;

        //    PdfPage page = pdf.Pages[0];
        //    page.Canvas.DrawString(10, 50, "Check document properties");

        //    pdf.Save(pathToFile);
    }

 
            //           Process.Start(pathToFile);

            //save the pdf document
            //pdfDoc.SaveToFile(@"..\..\..\sample.pdf");
            //launch the pdf document
            //   System.Diagnostics.Process.Start(@"sample.pdf");

            //       ////--------------------------------------------------------------------

            //       //Create a pdf document
            //       PdfDocument doc = new PdfDocument();

            //       //margin

            //       PdfUnitConvertor unitCvtr = new PdfUnitConvertor();
            //       PdfMargins margin = new PdfMargins();
            //       margin.Top = unitCvtr.ConvertUnits(2.54f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
            //       margin.Bottom = margin.Top;
            //       margin.Left = unitCvtr.ConvertUnits(3.17f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
            //       margin.Right = margin.Left;

            //       // Create one page
            //       PdfPageBase page = doc.Pages.Add(PdfPageSize.A4, margin);
            //       page.BackgroundColor = Color.Chocolate;

            //       //Draw page
            //       DrawPage(page);

            //       page = doc.Pages.Add(PdfPageSize.A4, margin);
            //       page.BackgroundColor = Color.Coral;

            //       //Draw page
            //       DrawPage(page);

            //       page = doc.Pages.Add(PdfPageSize.A3, margin, PdfPageRotateAngle.RotateAngle180, PdfPageOrientation.Landscape);
            //       page.BackgroundColor = Color.LightPink;

            //       //Draw page
            ////       DrawPage(page);

            //       //create section
            //       PdfSection section = doc.Sections.Add();

            //       section.PageSettings.Size = PdfPageSize.A4;
            //       section.PageSettings.Margins = margin;

            //       page = section.Pages.Add();

            //       //Draw page
            //       DrawPage(page);

            //       //set background color
            //       page = section.Pages.Add();
            //       page.BackgroundColor = Color.LightSkyBlue;

            //       DrawPage(page);

            //       //Landscape

            //       section = doc.Sections.Add();
            //       section.PageSettings.Size = PdfPageSize.A4;
            //       section.PageSettings.Margins = margin;
            //       section.PageSettings.Orientation = PdfPageOrientation.Landscape;

            //       page = section.Pages.Add();
            //       DrawPage(page);

            //       //Rotate 90

            //       section = doc.Sections.Add();
            //       section.PageSettings.Size = PdfPageSize.A4;
            //       section.PageSettings.Margins = margin;
            //       section.PageSettings.Rotate = PdfPageRotateAngle.RotateAngle90;

            //       page = section.Pages.Add();
            //       DrawPage(page);

            //       //Rotate 180

            //       section = doc.Sections.Add();
            //       section.PageSettings.Size = PdfPageSize.A4;
            //       section.PageSettings.Margins = margin;
            //       section.PageSettings.Rotate = PdfPageRotateAngle.RotateAngle180;
            //       page = section.Pages.Add();

            //       DrawPage(page);

            //       //Save pdf file.

            //       doc.SaveToFile("PageSetup.pdf");

            //       doc.Close();


            //       //Launching the Pdf file.

            //       System.Diagnostics.Process.Start("PageSetup.pdf");

        
        //private static void DrawHeader(PdfCanvas canvas)
        //{
        //    canvas.SaveState();
        //    canvas.Brush.Color = new PdfGrayColor(75);
        //    PdfRectangle headerBounds = new PdfRectangle(m_leftTableCorner, new PdfSize(TableWidth, RowHeight));
        //    canvas.DrawRectangle(headerBounds, PdfDrawMode.FillAndStroke);

        //    PdfRectangle[] cellBounds = new PdfRectangle[3]
        //    {
        //        new PdfRectangle(m_leftTableCorner.X, m_leftTableCorner.Y, m_columnWidths[0], RowHeight),
        //        new PdfRectangle(m_leftTableCorner.X + m_columnWidths[0], m_leftTableCorner.Y, m_columnWidths[1], RowHeight),
        //        new PdfRectangle(m_leftTableCorner.X + m_columnWidths[0] + m_columnWidths[1], m_leftTableCorner.Y, m_columnWidths[2], RowHeight)
        //    };

        //    for (int i = 1; i <= 2; ++i)
        //    {
        //        canvas.CurrentPosition = new PdfPoint(cellBounds[i].Left, cellBounds[i].Top);
        //        canvas.DrawLineTo(canvas.CurrentPosition.X, canvas.CurrentPosition.Y + RowHeight);
        //    }

        //    canvas.Brush.Color = new PdfGrayColor(0);
        //    canvas.DrawString("Project", cellBounds[0], PdfTextAlign.Center, PdfVerticalAlign.Center);
        //    canvas.DrawString("License", cellBounds[1], PdfTextAlign.Center, PdfVerticalAlign.Center);
        //    canvas.DrawString("Description", cellBounds[2], PdfTextAlign.Center, PdfVerticalAlign.Center);

        //    canvas.RestoreState();
        //}

        //private static void DrawTableBody(PdfCanvas canvas)
        //{
        //    PdfPoint bodyLeftCorner = new PdfPoint(m_leftTableCorner.X, m_leftTableCorner.Y + RowHeight);
        //    PdfRectangle firstCellBounds = new PdfRectangle(bodyLeftCorner, new PdfSize(m_columnWidths[0], RowHeight * 2));
        //    canvas.DrawRectangle(firstCellBounds);
        //    canvas.DrawString("Docotic.Pdf", firstCellBounds, PdfTextAlign.Center, PdfVerticalAlign.Center);

        //    PdfRectangle[,] cells = new PdfRectangle[2, 2]
        //    {
        //        {
        //            new PdfRectangle(bodyLeftCorner.X + m_columnWidths[0], bodyLeftCorner.Y, m_columnWidths[1], RowHeight),
        //            new PdfRectangle(bodyLeftCorner.X + m_columnWidths[0] + m_columnWidths[1], bodyLeftCorner.Y, m_columnWidths[2], RowHeight)
        //        },
        //        {
        //            new PdfRectangle(bodyLeftCorner.X + m_columnWidths[0], bodyLeftCorner.Y + RowHeight, m_columnWidths[1], RowHeight),
        //            new PdfRectangle(bodyLeftCorner.X + m_columnWidths[0] + m_columnWidths[1], bodyLeftCorner.Y + RowHeight, m_columnWidths[2], RowHeight)
        //        }
        //    };

        //    foreach (PdfRectangle rect in cells)
        //        canvas.DrawRectangle(rect, PdfDrawMode.Stroke);

        //    canvas.DrawString("Application License", cells[0, 0], PdfTextAlign.Center, PdfVerticalAlign.Center);
        //    canvas.DrawString("For end-user applications", cells[0, 1], PdfTextAlign.Center, PdfVerticalAlign.Center);
        //    canvas.DrawString("Server License", cells[1, 0], PdfTextAlign.Center, PdfVerticalAlign.Center);
        //    canvas.DrawString("For server-based services", cells[1, 1], PdfTextAlign.Center, PdfVerticalAlign.Center);
        //}


        //public static void CreateMainPdf(int id)
        //  {
        ////           _businessLayer = new BusinessLayer.BusinessLayer();
        ////           _entityException = new DbEntityException();

        //           var reportVm = new ReportViewModel();
        //           reportVm.GetTbl03RegnumsById(id);

        //           //From Database Tbl03Regnums
        // //          _regnum = _businessLayer.SingleListTbl03RegnumsByRegnumId(id);


        //           var sfd = new SaveFileDialog { Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*" };
        //           var saveResult = sfd.ShowDialog();
        //           if (saveResult != true) return;  //exit
        //           Document doc = null;

        //           try
        //           {

        //    doc = PdfHelper.HeaderMainPdf(sfd);
        //    // Add pages to the document
        //    PdfHelper.AddReportMain(doc);

        //    doc = AddTbl03RegnumsHaeder(doc);
        //    PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportTaxoNomen));
        //    doc = AddTbl03RegnumsTaxoNomenList(doc);
        //    PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportTaxoHiera));

        //    doc = AddHierarchyList(doc);

        //    if (reportVm.PhylumsCollection.Count != 0)
        //        doc = AddTbl06PhylumsChildrenList(doc, reportVm.PhylumsCollection);
        //    if (reportVm.DivisionsCollection.Count != 0)
        //        doc = AddTbl09DivisionsChildrenList(doc, reportVm.DivisionsCollection);

        //    PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportReferences));
        //    if (reportVm.ExpertsCollection.Count != 0)
        //        doc = PdfHelper.AddRefExpertList(doc, reportVm.ExpertsCollection);
        //    if (reportVm.SourcesCollection.Count != 0)
        //        doc = PdfHelper.AddRefSourceList(doc, reportVm.SourcesCollection);
        //    if (reportVm.AuthorsCollection.Count != 0)
        //        doc = PdfHelper.AddRefAuthorList(doc, reportVm.AuthorsCollection);
        //    PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportComments));
        //    if (reportVm.CommentsCollection.Count != 0)
        //        doc = PdfHelper.AddCommentList(doc, reportVm.CommentsCollection);

        //}
        //catch (DocumentException)
        //{
        //    // Handle iTextSharp errors
        //}
        //finally
        //{
        //    // Clean up
        //    doc?.Close();
        //    doc = null;
        //}
        // }

        //private static Document AddTbl03RegnumsHaeder(Document doc)
        //{
        //    // Add a new page to the document
        //    doc.NewPage();

        //    var table = new PdfPTable(4)
        //    {
        //        TotalWidth = 792f, //actual width of table in points
        //        LockedWidth = true,
        //        WidthPercentage = 100,
        //        HorizontalAlignment = 0,  //0=Left aLign, 1=Center
        //        SpacingBefore = 10f,
        //        SpacingAfter = 10f   //fix the absolute width of the table
        //    };
        //    table.SetWidths(new[] { 0.05f, 0.05f, 1.25f, 4.00f });

        //    var author = PdfHelper.AuthorViewChangeWithoutString(_regnum.Author, _regnum.AuthorYear);

        //    table.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + " " + _regnum.Subregnum + " " + author, LargeFont)) { Colspan = 4, Border = 0 });  // 1.-4. field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxonomicId + " " + Convert.ToString(_regnum.CountID), StandardFont)) { Colspan = 4, Border = 0 }); // 1.-4. field
        //    doc.Add(table);
        //    return doc;
        //}

        //private static Document AddTbl03RegnumsTaxoNomenList(Document doc)

        //{
        //    var table = new PdfPTable(4)
        //    {
        //        TotalWidth = 792f, //actual width of table in points
        //        LockedWidth = true,
        //        WidthPercentage = 100,
        //        HorizontalAlignment = 0,  //0=Left aLign, 1=Center
        //        SpacingBefore = 10f,
        //        SpacingAfter = 10f   //fix the absolute width of the table
        //    };
        //    table.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });

        //    table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, StandardFont)) { Border = 0 });  // 2. field
        //    table.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + " " + _regnum.Subregnum, StandardFont)) { Border = 0 });  // 3. field  
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field     

        //    table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxoRank, StandardBoldFont)) { Border = 0 });  // 2. field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, StandardFont)) { Border = 0 });  // 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field


        //    table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportSynonyms, StandardFont)) { Border = 0 });  // 2. field
        //    table.AddCell(new PdfPCell(new Phrase(_regnum.Synonym, StandardFont)) { Border = 0 });  // 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //    table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. fField
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportCommonNames, StandardFont)) { Border = 0 });  // 2. field
        //    table.AddCell(new PdfPCell(new Phrase(_regnum.GerName + " " + CultRes.StringsRes.ReportGerman, StandardFont)) { Border = 0 });  // 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //    table.AddCell(new PdfPCell { Colspan = 2, Border = 0 });  // 1. + 2.  field
        //    table.AddCell(new PdfPCell(new Phrase(_regnum.EngName + " " + CultRes.StringsRes.ReportEnglish, StandardFont)) { Border = 0 });  // 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //    table.AddCell(new PdfPCell { Colspan = 2, Border = 0 });  // 1. + 2.  field
        //    table.AddCell(new PdfPCell(new Phrase(_regnum.FraName + " " + CultRes.StringsRes.ReportFrench, StandardFont)) { Border = 0 });  // 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //    table.AddCell(new PdfPCell { Colspan = 2, Border = 0 });  // 1. + 2.  field
        //    table.AddCell(new PdfPCell(new Phrase(_regnum.PorName + " " + CultRes.StringsRes.ReportPortuguese, StandardFont)) { Border = 0 });  // 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //    table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxoStatus, StandardBoldFont)) { Colspan = 2, Border = 0 });  // 2.- 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //    table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportCurrentStand, StandardFont)) { Border = 0 });  // 2. field
        //    table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_regnum.Valid), StandardFont)) { Border = 0 });  // 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 4, Border = 0 });  //Empty row

        //    table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDataQualiIndicator, StandardBoldFont)) { Colspan = 2, Border = 0 });  // 2.- 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //    table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportRecordCrediRate, StandardFont)) { Colspan = 2, Border = 0 });  // 2. - 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

        //    table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportGlobalSpecComp, StandardFont)) { Colspan = 2, Border = 0 });  // 2. - 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

        //    table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportRecordUpdate, StandardFont)) { Border = 0 });  // 2. field
        //    table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_regnum.UpdaterDate, CultureInfo.InvariantCulture), StandardFont)) { Border = 0 });  // 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

        //    table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportInfo, StandardFont)) { Border = 0 });  // 2. field
        //    table.AddCell(new PdfPCell(new Phrase(_regnum.Info, StandardFont)) { Border = 0 });  // 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

        //    table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemo, StandardFont)) { Border = 0 });  // 2. field
        //    table.AddCell(new PdfPCell(new Phrase(_regnum.Memo, StandardFont)) { Border = 0 });  // 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

        //    doc.Add(table);

        //    return doc;
        //}

        //private static Document AddHierarchyList(Document doc)
        //{
        //    if (_regnum.RegnumName.Contains("#") == false)
        //    {
        //        var tableRegnum = new PdfPTable(4)
        //        {
        //            TotalWidth = 792f, //actual width of table in points
        //            LockedWidth = true,   //fix the absolute width of the table
        //            WidthPercentage = 100,
        //            HorizontalAlignment = 0,  //0=Left aLign, 1=Center
        //            SpacingBefore = 10f,
        //            SpacingAfter = 10f
        //        };
        //        tableRegnum.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });

        //        var author = PdfHelper.AuthorViewChangeWithString(_regnum.Author, _regnum.AuthorYear);
        //        var names = PdfHelper.NamesViewChange(_regnum.GerName, _regnum.EngName, _regnum.FraName, _regnum.PorName);

        //        tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
        //        tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 });  // 2. field
        //        tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + " " + _regnum.Subregnum + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
        //        tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        doc.Add(tableRegnum);
        //    }
        //    return doc;
        //}

        //private static Document AddTbl06PhylumsChildrenList(Document doc, ObservableCollection<Tbl06Phylum> tbl06PhylumsList)
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

        //    table.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 2, Border = 0 });  // 1. -2. field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDirectChild, SmallBoldFont)) { Border = 0 }); //   3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //    foreach (var t in tbl06PhylumsList)
        //    {
        //        var t1 = t.PhylumName;

        //        var author = PdfHelper.AuthorViewChangeWithString(t.Author, t.AuthorYear);
        //        var names = PdfHelper.NamesViewChange(t.GerName, t.EngName, t.FraName, t.PorName);

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.   field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(t1 + " " + author + " " + names, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field
        //    }
        //    doc.Add(table);

        //    return doc;
        //}

        //private static Document AddTbl09DivisionsChildrenList(Document doc, ObservableCollection<Tbl09Division> tbl09DivisionsList)
        //{
        //    var table = new PdfPTable(3)
        //    {
        //        TotalWidth = 792f, //actual width of table in points
        //        LockedWidth = true,
        //        WidthPercentage = 100,
        //        HorizontalAlignment = 0,  //0=Left aLign, 1=Center
        //        SpacingBefore = 0f,
        //        SpacingAfter = 10f   //fix the absolute width of the table
        //    };
        //    table.SetWidths(new[] { 0.08f, 1.22f, 4.00f });

        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 2, Border = 0 });  // 1. -2. field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDirectChild, SmallBoldFont)) { Border = 0 }); //   3. field

        //    foreach (var t in tbl09DivisionsList)
        //    {
        //        var t1 = t.DivisionName;

        //        var author = PdfHelper.AuthorViewChangeWithString(t.Author, t.AuthorYear);
        //        var names = PdfHelper.NamesViewChange(t.GerName, t.EngName, t.FraName, t.PorName);

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.   field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(t1 + " " + author + " " + names, SmallFont)) { Border = 0 });   // 3. field
        //    }
        //    doc.Add(table);

        //    return doc;
        //}

        // }
        // }
    

