using System;
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
        private static int _z;
        private static int[] _arrInts = new int[11];
        private static PdfPage _page;

        //    Part 1    

        public static void CreateMainPdf(int id)
        {
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
                    _arrInts = PdfHelper.AddReportMain(pdf);

                    AddRegnumHaeder(pdf, regnumList);
                    AddRegnumTaxoNomenList(pdf, regnumList);
                    AddRegnumHierarchyList(pdf, regnumList);

                    if (phylumsList.Count != 0)
                        AddPhylumsChildrenList(pdf, phylumsList);
                    if (divisionsList.Count != 0)
                        AddDivisionsChildrenList(pdf, divisionsList);

                    if (expertsList.Count != 0 || sourcesList.Count != 0 || authorsList.Count != 0)
                    {
                         _arrInts = PdfHelper.AddReferencesHaeder(pdf, _arrInts);
                    }

                    if (expertsList.Count != 0)
                        _arrInts = PdfHelper.AddRefExpertsList(pdf, expertsList, _arrInts);
                    if (sourcesList.Count != 0)
                    {
                         _arrInts = PdfHelper.AddRefSourcesList(pdf, sourcesList, _arrInts);
                    }

                    if (authorsList.Count != 0)
                    {
                         _arrInts = PdfHelper.AddRefAuthorsList(pdf, authorsList, _arrInts);
                    }

                    if (commentsList.Count != 0)
                    {
                         _arrInts = PdfHelper.AddCommentsHaeder(pdf, _arrInts);
                    }

                    if (commentsList.Count != 0)
                        _arrInts = PdfHelper.AddCommentsList(pdf, commentsList, _arrInts);

                    pdf.Save(pathToFile);
               

                    //var sfd = new SaveFileDialog { Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*" };
                    //var saveResult = sfd.ShowDialog();
                    //if (saveResult != true) return; //exit
                }
            }
            catch (Exception e)
            {
                // Handle  errors
                Log.Error(e);
            }
            finally
            {
                // Clean up
                //        if (doc != null) doc.Dispose();
                //     doc = null;
                Log.Error("Fehler");

            }
        }

        private static void AddRegnumHaeder(PdfDocument pdf, Tbl03Regnum regnumList )
        {
            _page = pdf.Pages[_arrInts[6]];

            var textAusgabeAuthor = PdfHelper.AuthorViewChangeWithString(regnumList.Author, regnumList.AuthorYear);

            var txtName = _page.AddTextBox("regnumName", new PdfPoint(_arrInts[0], _arrInts[1]),
                new PdfSize(_arrInts[5], _arrInts[3]));
            txtName.HasBorder = false;
            txtName.ReadOnly = true;
            txtName.Font.SynthesizedBold = true;
            txtName.FontSize = _arrInts[9] + 2;
            txtName.Text = regnumList.RegnumName + " " + regnumList.Subregnum + " " + textAusgabeAuthor;
            _arrInts[1] += _arrInts[3];

            _arrInts[1] += _arrInts[9] - 6; //Distance to next TextBox

            var txtCountId = _page.AddTextBox("countId", new PdfPoint(_arrInts[0], _arrInts[1]), 
                new PdfSize(_arrInts[5], _arrInts[3]));
            txtCountId.HasBorder = false;
            txtCountId.ReadOnly = true;
            txtCountId.Font.SynthesizedBold = false;
            txtCountId.FontSize = _arrInts[9] - 1;
            txtCountId.Text = CultRes.StringsRes.ReportTaxonomicId + regnumList.CountId;

            _arrInts[1] += _arrInts[9] + 5; //Distance to next TextBox
        }
        private static void AddRegnumTaxoNomenList(PdfDocument pdf, Tbl03Regnum regnumList)
        { 
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbBoldLeft("header2", _arrInts, CultRes.StringsRes.ReportTaxoNomen, 2);

            _arrInts[1] += _arrInts[9] - 6; //Distance to next TextBox

            //----------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbLeft("kingdomLeft", _arrInts, CultRes.StringsRes.Regnum + ":");
            _arrInts = PdfHelper.PdfTbRight("kingdomRight", _arrInts, regnumList.RegnumName + " " + regnumList.Subregnum);
            //---------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbLeft("rankLeft", _arrInts, CultRes.StringsRes.ReportTaxoRank); 
            _arrInts = PdfHelper.PdfTbRight("rankRight", _arrInts, CultRes.StringsRes.Regnum);
            //------------------------------------------------------
            _arrInts = PdfHelper.PdfTbLeft("synonymLeft", _arrInts, CultRes.StringsRes.ReportSynonyms);
            //------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMtRight("synonymRight", _arrInts, regnumList.Synonym);

            _arrInts[1] += 8; //Distance to next TextBox

            //---------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbLeft("commNameLeft", _arrInts, CultRes.StringsRes.ReportCommonNames);
            //---------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbRight("commNameGerRight", _arrInts, regnumList.GerName + " " + CultRes.StringsRes.ReportGerman);
            _arrInts = PdfHelper.PdfTbRight("commNameEngRight", _arrInts, regnumList.EngName + " " + CultRes.StringsRes.ReportEnglish);
            _arrInts = PdfHelper.PdfTbRight("commNameFraRight", _arrInts, regnumList.FraName + " " + CultRes.StringsRes.ReportFrench);
            _arrInts = PdfHelper.PdfTbRight("commNameSpaRight", _arrInts, regnumList.PorName + " " + CultRes.StringsRes.ReportPortuguese);

            _arrInts[1] += 5; //Distance to next TextBox

            //-------------------------------------------------------
            _arrInts = PdfHelper.PdfTbBoldMoveLeft("status", _arrInts, CultRes.StringsRes.ReportTaxoStatus, 1);
            //---------------------------------------------------------
            _arrInts = PdfHelper.PdfTbLeft("currStatusLeft", _arrInts, CultRes.StringsRes.ReportCurrentStand);

            _arrInts = PdfHelper.PdfTbRight("currStatusRight", _arrInts, regnumList.Valid.ToString());

            _arrInts[1] += _arrInts[9] - 6; //Distance to next TextBox

            //-----------------------------------------------------------
            _arrInts = PdfHelper.PdfTbBoldMoveLeft("quali", _arrInts, CultRes.StringsRes.ReportDataQualiIndicator, 1);
            //---------------------------------------------------------
            _arrInts = PdfHelper.PdfTbLeft("recordLeft", _arrInts, CultRes.StringsRes.ReportRecordUpdate);

            _arrInts = PdfHelper.PdfTbRight("recordRight", _arrInts, Convert.ToString(regnumList.UpdaterDate, CultureInfo.InvariantCulture));

            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox

            //-------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbLeft("infoLeft", _arrInts, CultRes.StringsRes.ReportInfo);

            _arrInts = PdfHelper.PdfTbMtRight("infoRight", _arrInts, regnumList.Info);

            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox

            //-------------------------------------------------------
            _arrInts = PdfHelper.PdfTbLeft("memoLeft", _arrInts, CultRes.StringsRes.ReportMemo);

            _arrInts = PdfHelper.PdfTbMtRight("memoRight", _arrInts, regnumList.Memo);

            _arrInts[1] += _arrInts[9]; //Distance to next TextBox
        }
        private static void AddRegnumHierarchyList(PdfDocument pdf, Tbl03Regnum regnumList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbBoldLeft("header3", _arrInts, CultRes.StringsRes.ReportTaxoHiera, 2);

            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox

            //---------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbLeft("regnumLeft", _arrInts, CultRes.StringsRes.Regnum);

            var txtNameAndSub = regnumList.RegnumName + " " + regnumList.Subregnum;
            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtNameAndSub, regnumList.Author,
                regnumList.AuthorYear, regnumList.GerName, regnumList.EngName, regnumList.FraName, regnumList.PorName);

             _arrInts = PdfHelper.PdfTbRight("regnumRight", _arrInts, textResult);


            _arrInts[1] += _arrInts[9] - 4; //Distance to next TextBox
        }
        private static void AddPhylumsChildrenList(PdfDocument pdf, ObservableCollection<Tbl06Phylum> phylumsList)
        {
            _page = pdf.Pages[_arrInts[6]];

            var txtChildren = _page.AddTextBox("childrenPhylum", new PdfPoint(_arrInts[2], _arrInts[1]),
                new PdfSize(_arrInts[5], _arrInts[3]));
            txtChildren.HasBorder = false;
            txtChildren.ReadOnly = true;
            txtChildren.Font.SynthesizedBold = true;
            txtChildren.FontSize = _arrInts[9] + 1;
            txtChildren.Text = CultRes.StringsRes.ReportDirectChild;
            _arrInts[1] += _arrInts[3];

            _arrInts[1] += _arrInts[9] - 4; //Distance to next TextBox

            //------------------------------------------------------------------

            _n = "childPhylum";
            _z = 1;
            _z1 = _n + _z;
            _arrInts[7] += + _arrInts[7];   // move 4+4

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

                _arrInts = PdfHelper.PdfTbLeft("phylumLeft" + _z1, _arrInts, CultRes.StringsRes.Phylum);

                var phylumAll = 0;
                for (int i = _arrInts[8]; i < tAllLength; i += _arrInts[8])
                {
                    phylumAll += _arrInts[9] + _arrInts[9];
                }

                var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(t1, t2, t3, t4, t5, t6, t7);

                if (tAllLength >= _arrInts[8])
                {
                    var txtPhylumNameRight = _page.AddTextBox(_z1, new PdfPoint(_arrInts[2], _arrInts[1]),
                        new PdfSize(_arrInts[5], _arrInts[3] + phylumAll));
                    txtPhylumNameRight.HasBorder = false;
                    txtPhylumNameRight.ReadOnly = true;
                    txtPhylumNameRight.Multiline = true;
                    txtPhylumNameRight.FontSize = _arrInts[9];
           //         txtPhylumNameRight.Text = t1 + " - " + t2 + ", " + t3 + " - " + t4 + " " + t5 + " " + t6 + " " + t7;
                    txtPhylumNameRight.Text = textResult;
                    _arrInts[1] += _arrInts[3] + phylumAll -4 ;  // -8 Leerzeile
                }
                else
                {
                    var txtPhylumNameRight = _page.AddTextBox(_z1, new PdfPoint(_arrInts[2], _arrInts[1]),
                        new PdfSize(_arrInts[5], _arrInts[3]));
                    txtPhylumNameRight.HasBorder = false;
                    txtPhylumNameRight.ReadOnly = true;
                    txtPhylumNameRight.FontSize = _arrInts[9];
        //            txtPhylumNameRight.Text = t1 + " - " + t2 + ", " + t3 + " - " + t4 + " " + t5 + " " + t6 + " " + t7;
                    txtPhylumNameRight.Text = textResult;
                    _arrInts[1] += _arrInts[3];
                }

                _z += 1;
                _z1 = _n + _z;
            }
            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox
        }
        private static void AddDivisionsChildrenList(PdfDocument pdf, ObservableCollection<Tbl09Division> divisionsList)
        {
            _page = pdf.Pages[_arrInts[6]];

            var txtChildren = _page.AddTextBox("childrenDivision", new PdfPoint(_arrInts[2], _arrInts[1]),
                new PdfSize(_arrInts[5], _arrInts[3]));
            txtChildren.HasBorder = false;
            txtChildren.ReadOnly = true;
            txtChildren.Font.SynthesizedBold = true;
            txtChildren.FontSize = _arrInts[9] + 1;
            txtChildren.Text = CultRes.StringsRes.ReportDirectChild;
            _arrInts[1] += _arrInts[3];

            _arrInts[1] += _arrInts[9] - 4; //Distance to next TextBox

            //------------------------------------------------------------------

            _n = "childDivision";
            _z = 1;
            _z1 = _n + _z;
            _arrInts[7] += +_arrInts[7];   // move 4+4

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

                _arrInts = PdfHelper.PdfTbLeft("divisionLeft" + _z1, _arrInts, CultRes.StringsRes.Division);

                var divisionAll = 0;
                for (int i = _arrInts[8]; i < tAllLength; i += _arrInts[8])
                {
                    divisionAll += _arrInts[9] + _arrInts[9];
                }

                var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(t1, t2, t3, t4, t5, t6, t7);

                if (tAllLength >= _arrInts[8])
                {
                    var txtDivisionNameRight = _page.AddTextBox(_z1, new PdfPoint(_arrInts[2], _arrInts[1]),
                        new PdfSize(_arrInts[5], _arrInts[3] + divisionAll));
                    txtDivisionNameRight.HasBorder = false;
                    txtDivisionNameRight.ReadOnly = true;
                    txtDivisionNameRight.Multiline = true;
                    txtDivisionNameRight.FontSize = _arrInts[9];
      //              txtDivisionNameRight.Text = t1 + " - " + t2 + ", " + t3 + " - " + t4 + " " + t5 + " " + t6 + " " + t7;
                    txtDivisionNameRight.Text = textResult;
                    _arrInts[1] += _arrInts[3] + divisionAll - 4;  // -8 Leerzeile
                }
                else
                {
                    var txtDivisionNameRight = _page.AddTextBox(_z1, new PdfPoint(_arrInts[2], _arrInts[1]),
                        new PdfSize(_arrInts[5], _arrInts[3]));
                    txtDivisionNameRight.HasBorder = false;
                    txtDivisionNameRight.ReadOnly = true;
                    txtDivisionNameRight.FontSize = _arrInts[9];
         //           txtDivisionNameRight.Text = t1 + " - " + t2 + ", " + t3 + " - " + t4 + " " + t5 + " " + t6 + " " + t7;
                    txtDivisionNameRight.Text = textResult;
                    _arrInts[1] += _arrInts[3];
                }

                _z += 1;
                _z1 = _n + _z;
            }
            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox
        }
    }

}

 
     

