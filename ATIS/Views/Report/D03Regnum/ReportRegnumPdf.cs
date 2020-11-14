using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Windows;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using ATIS.Ui.Views.Database.CrudHelper;
using ATIS.Ui.Views.Report.PDF;
using BitMiracle.Docotic;
using BitMiracle.Docotic.Pdf;
using log4net;
using Microsoft.Win32;
using FontStyle = System.Drawing.FontStyle;

namespace ATIS.Ui.Views.Report.D03Regnum
{
    public class ReportRegnumPdf : ViewModelBase
    {

        // Set up the fonts to be used on the pages
        private static readonly Font LargeBoldFont = new Font(FontFamily.GenericSerif, 16, FontStyle.Bold);
        private static readonly Font StandardFont = new Font(FontFamily.GenericSerif, 12, FontStyle.Regular);
        private static readonly Font StandardBoldFont = new Font(FontFamily.GenericSerif, 12, FontStyle.Bold);
        private static readonly Font SmallFont = new Font(FontFamily.GenericSerif, 10, FontStyle.Regular);
        private static readonly Font SmallBoldFont = new Font(FontFamily.GenericSerif, 10, FontStyle.Bold);
        //   private static readonly Font LargeFont = new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD, BaseColor.BLACK);
        //   private static readonly Font StandardFont = new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.BLACK);
        //  private static readonly Font SmallFont = new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK);
       // private static readonly Font SmallBoldFont = new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.BLACK);
        //private static IBusinessLayer _businessLayer;
        //private static DbEntityException _entityException;

        private static IEnumerable<Tbl03Regnum> _regnum;
   //     private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly ILog Log = LogManager.GetLogger(typeof(ReportRegnumPdf));
        private static readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private static readonly AtisDbContext _context = new AtisDbContext();
        private static readonly BasicGet _extGet = new BasicGet();

        private const double TableWidth = 600.0;
        private const double RowHeight = 30.0;
        private static readonly PdfPoint MLeftTableCorner = new PdfPoint(10, 50);
        private static readonly double[] MColumnWidths = new double[3] { 100, 100, 200 };

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

            // var tbl03RegnumsList = _extGet.GetRegnumsCollectionOrderByFromRegnumId<Tbl03Regnum>(id);
     //       var regnumsList = _uow.Tbl03Regnums.GetById(id);
            var regnumsList = _extGet.GetRegnumsCollectionOrderByFromRegnumId<Tbl03Regnum>(id).FirstOrDefault();
            var phylumsList = _extGet.GetPhylumsCollectionOrderByFromRegnumId<Tbl06Phylum>(id);
            var divisionsList = _extGet.GetDivisionsCollectionOrderByFromRegnumId<Tbl09Division>(id);
            var expertsList = _extGet.GetReferenceExpertsCollectionOrderByFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(id);
            var sourcesList = _extGet.GetReferenceSourcesCollectionOrderByFromRegnumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(id);
            var authorsList = _extGet.GetReferenceAuthorsCollectionOrderByFromRegnumIdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(id);
            var commentsList = _extGet.GetCommentsCollectionOrderByFromRegnumId<Tbl93Comment>(id);



            try
            {
                using (PdfDocument pdf = new PdfDocument())
                {
                     AddReportMain(pdf, regnumsList);
                     AddRegnumTaxoNomenList(pdf, regnumsList);
                     AddHierarchyList(pdf, regnumsList);

                    if (phylumsList.Count != 0)
                        AddPhylumsChildrenList(pdf, phylumsList);

                    if (divisionsList.Count != 0)
                        AddDivisionsChildrenList(pdf, divisionsList);

                    //PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportReferences));
 
                    if (expertsList.Count != 0)
                        AddRefExpertList(pdf, expertsList);
                    if (sourcesList.Count != 0)
                        AddRefSourceList(pdf, sourcesList);
                    if (authorsList.Count != 0)
                        AddRefAuthorList(pdf, authorsList);
                    //PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportComments));
                    if (commentsList.Count != 0)
                        AddCommentList(pdf, commentsList);


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



        private static void AddReportMain(PdfDocument pdf, Tbl03Regnum regnumList)
        {
            var page = pdf.Pages[0];

            var txtHeader = pdf.Pages[0].AddTextBox("header", new PdfPoint(10, 10), new PdfSize(300, 30));
            txtHeader.HasBorder = false;
            txtHeader.ReadOnly = true;
            txtHeader.Font.SynthesizedBold = true;
            txtHeader.FontSize = 24;
            txtHeader.Text = "ATIS REPORT";

            if (regnumList.Author != null)
            {
                var txtName = pdf.Pages[0].AddTextBox("regnumName", new PdfPoint(10, 50), new PdfSize(580, 30));
                txtName.HasBorder = false;
                txtName.ReadOnly = true;
                txtName.Font.SynthesizedBold = false;
                txtName.FontSize = 16;
                txtName.Text = regnumList.RegnumName + " " + regnumList.Subregnum + " " + regnumList.Author + "," + regnumList.AuthorYear;
            }
            else
            {
                var txtName = pdf.Pages[0].AddTextBox("regnumName", new PdfPoint(10, 50), new PdfSize(580, 30));
                txtName.HasBorder = false;
                txtName.ReadOnly = true;
                txtName.Font.SynthesizedBold = false;
                txtName.FontSize = 16;
                txtName.Text = regnumList.RegnumName + " " + regnumList.Subregnum;
            }

            var txtCountId = pdf.Pages[0].AddTextBox("countId", new PdfPoint(10, 80), new PdfSize(580, 15));
            txtCountId.HasBorder = false;
            txtCountId.ReadOnly = true;
            txtCountId.FontSize = 10;
            txtCountId.Text = "Taxonomic Serial No.: " + regnumList.CountId.ToString();
        }

        private static void AddRegnumTaxoNomenList(PdfDocument pdf, Tbl03Regnum regnumList)
        {
            var page = pdf.Pages[0];

            var txtHeader2 = pdf.Pages[0].AddTextBox("header2", new PdfPoint(10, 120), new PdfSize(300, 30));
            txtHeader2.HasBorder = false;
            txtHeader2.ReadOnly = true;
            txtHeader2.Font.SynthesizedBold = true;
            txtHeader2.FontSize = 16;
            txtHeader2.Text = "Taxonomy and Nomenclature";

            var txtNameLeft = pdf.Pages[0].AddTextBox("kingdomLeft", new PdfPoint(10, 150), new PdfSize(200, 15));
            txtNameLeft.HasBorder = false;
            txtNameLeft.ReadOnly = true;
            txtNameLeft.Font.SynthesizedBold = false;
            txtNameLeft.FontSize = 10;
            txtNameLeft.Text = CultRes.StringsRes.Regnum + ":";

            var txtNameRight = pdf.Pages[0].AddTextBox("kingdomRight", new PdfPoint(200, 150), new PdfSize(380, 15));
            txtNameRight.HasBorder = false;
            txtNameRight.ReadOnly = true;
            txtNameRight.FontSize = 10;
            txtNameRight.Text = regnumList.RegnumName + " " + regnumList.Subregnum;

            var txtRankLeft = pdf.Pages[0].AddTextBox("rankLeft", new PdfPoint(10, 165), new PdfSize(200, 15));
            txtRankLeft.HasBorder = false;
            txtRankLeft.ReadOnly = true;
            txtRankLeft.FontSize = 10;
            txtRankLeft.Text = "Taxonomic Rank:";

            var txtRankRight = pdf.Pages[0].AddTextBox("rankRight", new PdfPoint(200, 165), new PdfSize(380, 15));
            txtRankRight.HasBorder = false;
            txtRankRight.ReadOnly = true;
            txtRankRight.FontSize = 10;
            txtRankRight.Text = CultRes.StringsRes.Regnum;

            var txtSynonymLeft = pdf.Pages[0].AddTextBox("synonymLeft", new PdfPoint(10, 180), new PdfSize(200, 15));
            txtSynonymLeft.HasBorder = false;
            txtSynonymLeft.ReadOnly = true;
            txtSynonymLeft.FontSize = 10;
            txtSynonymLeft.Text = "Synonym(s):";

            if (regnumList.Synonym != null)
            {
                var txtSynonymRight = pdf.Pages[0].AddTextBox("synonymRight", new PdfPoint(200, 180), new PdfSize(380, 35));
                txtSynonymRight.HasBorder = false;
                txtSynonymRight.Multiline = true;
                txtSynonymRight.ReadOnly = true;
                txtSynonymRight.Scrollable = true;
                txtSynonymRight.FontSize = 10;
                txtSynonymRight.Text = regnumList.Synonym;
            }

            var txtCommNameLeft = pdf.Pages[0].AddTextBox("commNameLeft", new PdfPoint(10, 215), new PdfSize(200, 15));
            txtCommNameLeft.HasBorder = false;
            txtCommNameLeft.ReadOnly = true;
            txtCommNameLeft.FontSize = 10;
            txtCommNameLeft.Text = "Common Name(s):";

            var txtCommNameGerRight = pdf.Pages[0].AddTextBox("commNameGerRight", new PdfPoint(200, 215), new PdfSize(380, 15));
            txtCommNameGerRight.HasBorder = false;
            txtCommNameGerRight.ReadOnly = true;
            txtCommNameGerRight.FontSize = 10;
            txtCommNameGerRight.Text = regnumList.GerName + " [German]";

            var txtCommNameEngRight = pdf.Pages[0].AddTextBox("commNameEngRight", new PdfPoint(200, 230), new PdfSize(380, 15));
            txtCommNameEngRight.HasBorder = false;
            txtCommNameEngRight.ReadOnly = true;
            txtCommNameEngRight.FontSize = 10;
            txtCommNameEngRight.Text = regnumList.EngName + " [English]";

            var txtCommNameFraRight = pdf.Pages[0].AddTextBox("commNameFraRight", new PdfPoint(200, 245), new PdfSize(380, 15));
            txtCommNameFraRight.HasBorder = false;
            txtCommNameFraRight.ReadOnly = true;
            txtCommNameFraRight.FontSize = 10;
            txtCommNameFraRight.Text = regnumList.FraName + " [French]";

            var txtCommNameSpaRight = pdf.Pages[0].AddTextBox("commNameSpaRight", new PdfPoint(200, 260), new PdfSize(380, 15));
            txtCommNameSpaRight.HasBorder = false;
            txtCommNameSpaRight.ReadOnly = true;
            txtCommNameSpaRight.FontSize = 10;
            txtCommNameSpaRight.Text = regnumList.PorName + " [Spanish]";

            var txtStatus = pdf.Pages[0].AddTextBox("status", new PdfPoint(10, 290), new PdfSize(300, 15));
            txtStatus.HasBorder = false;
            txtStatus.ReadOnly = true;
            txtStatus.Font.SynthesizedBold = true;
            txtStatus.FontSize = 12;
            txtStatus.Text = "Taxonomic Status:";

            var txtCurrStatus = pdf.Pages[0].AddTextBox("currStatus", new PdfPoint(10, 305), new PdfSize(300, 15));
            txtCurrStatus.HasBorder = false;
            txtCurrStatus.ReadOnly = true;
            txtCurrStatus.Font.SynthesizedBold = false;
            txtCurrStatus.FontSize = 10;
            txtCurrStatus.Text = "Current Standing:";

            if (regnumList.Valid != null)
            {
                var txtCurrStatusRight =
                    pdf.Pages[0].AddTextBox("currStatusRight", new PdfPoint(200, 305), new PdfSize(380, 15));
                txtCurrStatusRight.HasBorder = false;
                txtCurrStatusRight.ReadOnly = true;
                txtCurrStatusRight.FontSize = 10;
                txtCurrStatusRight.Text = regnumList.Valid.ToString();
            }

            var txtQuali = pdf.Pages[0].AddTextBox("quali", new PdfPoint(10, 330), new PdfSize(300, 15));
            txtQuali.HasBorder = false;
            txtQuali.ReadOnly = true;
            txtQuali.FontSize = 12;
            txtQuali.Text = "Taxonomic Status:";

            var txtRecord = pdf.Pages[0].AddTextBox("record", new PdfPoint(10, 345), new PdfSize(300, 15));
            txtRecord.HasBorder = false;
            txtRecord.ReadOnly = true;
            txtRecord.FontSize = 10;
            txtRecord.Text = "Latest Record Review:";

            if (regnumList.ValidYear != null)
            {
                var txtRecordRight =
                    pdf.Pages[0].AddTextBox("recordRight", new PdfPoint(200, 345), new PdfSize(380, 15));
                txtRecordRight.HasBorder = false;
                txtRecordRight.ReadOnly = true;
                txtRecordRight.FontSize = 10;
                txtRecordRight.Text = regnumList.ValidYear;
            }

            var txtInfo = pdf.Pages[0].AddTextBox("info", new PdfPoint(10, 360), new PdfSize(300, 15));
            txtInfo.HasBorder = false;
            txtInfo.ReadOnly = true;
            txtInfo.FontSize = 10;
            txtInfo.Text = "Information:";

            if (regnumList.Info != null)
            {
                var txtInfoRight = pdf.Pages[0].AddTextBox("infoRight", new PdfPoint(200, 360), new PdfSize(380, 35));
                txtInfoRight.HasBorder = false;
                txtInfoRight.Multiline = true;
                txtInfoRight.ReadOnly = true;
                txtInfoRight.Scrollable = true;
                txtInfoRight.FontSize = 10;
                txtInfoRight.Text = regnumList.Info;
            }

            var txtMemo = pdf.Pages[0].AddTextBox("memo", new PdfPoint(10, 395), new PdfSize(300, 15));
            txtMemo.HasBorder = false;
            txtMemo.ReadOnly = true;
            txtMemo.FontSize = 10;
            txtMemo.Text = "Memo:";

            if (regnumList.Info != null)
            {
                var txtMemoRight = pdf.Pages[0].AddTextBox("memoRight", new PdfPoint(200, 395), new PdfSize(380, 35));
                txtMemoRight.HasBorder = false;
                txtMemoRight.Multiline = true;
                txtMemoRight.ReadOnly = true;
                txtMemoRight.Scrollable = true;
                txtMemoRight.FontSize = 10;
                txtMemoRight.Text = regnumList.Memo;
            }


        }

        private static void AddHierarchyList(PdfDocument pdf, Tbl03Regnum regnumList)
        {
            var page = pdf.Pages[0];

            var txtHeader3 = pdf.Pages[0].AddTextBox("header3", new PdfPoint(10, 430), new PdfSize(300, 30));
            txtHeader3.HasBorder = false;
            txtHeader3.ReadOnly = true;
            txtHeader3.Font.SynthesizedBold = true;
            txtHeader3.FontSize = 16;
            txtHeader3.Text = "Taxonomic Hierarchy";

            var txtRegnumNameLeft = pdf.Pages[0].AddTextBox("regnumLeft", new PdfPoint(10, 460), new PdfSize(200, 15));
            txtRegnumNameLeft.HasBorder = false;
            txtRegnumNameLeft.ReadOnly = true;
            txtRegnumNameLeft.Font.SynthesizedBold = false;
            txtRegnumNameLeft.FontSize = 10;
            txtRegnumNameLeft.Text = CultRes.StringsRes.Regnum + ":";

            var txtRegnumNameRight = pdf.Pages[0].AddTextBox("regnumRight", new PdfPoint(200, 460), new PdfSize(380, 15));
            txtRegnumNameRight.HasBorder = false;
            txtRegnumNameRight.ReadOnly = true;
            txtRegnumNameRight.FontSize = 10;
            txtRegnumNameRight.Text = regnumList.RegnumName + " " + regnumList.Subregnum + " " + regnumList.Author + ", " + regnumList.AuthorYear + "-" + regnumList.GerName + " " + regnumList.EngName + " " + regnumList.FraName + " " + regnumList.PorName;

        }

        private static void AddPhylumsChildrenList(PdfDocument pdf, ObservableCollection<Tbl06Phylum> phylumsList)
        {
            var txtPhylumNameLeft = pdf.Pages[0].AddTextBox("phylumLeft", new PdfPoint(10+3, 475), new PdfSize(200, 15));
            txtPhylumNameLeft.HasBorder = false;
            txtPhylumNameLeft.ReadOnly = true;
            txtPhylumNameLeft.FontSize = 10;
            txtPhylumNameLeft.Text = CultRes.StringsRes.Phylum + ":";

            var txtChildren = pdf.Pages[0].AddTextBox("children", new PdfPoint(200, 490), new PdfSize(380, 15));
            txtChildren.HasBorder = false;
            txtChildren.ReadOnly = true;
            txtChildren.Font.SynthesizedBold = true;
            txtChildren.FontSize = 12;
            txtChildren.Text = "Direct Children: ";

            int z = 1;
            string z1 = z.ToString();
            int z2 = 0;
            foreach (var t in phylumsList)
            {
                var t1 = t.PhylumName;
                var t2 = t.Author;
                var t3 = t.AuthorYear;
                //var t4 = t.GerName;
                //var t5 = t.EngName;
                //var t6 = t.FraName;
                //var t7 = t.PorName;

                var txtPhylumNameRight = pdf.Pages[0].AddTextBox(z1, new PdfPoint(200, 515+z2), new PdfSize(380, 15));
                txtPhylumNameRight.HasBorder = false;
                txtPhylumNameRight.ReadOnly = true;
                txtPhylumNameRight.Font.SynthesizedBold = false;
                txtPhylumNameRight.FontSize = 10;
                txtPhylumNameRight.Text = t1 + " " + t2 + "," + t3 ;

                z = z + 1; 
                z1 = z.ToString();
                z2 = z2 + 15;
                //var author = PdfHelper.AuthorViewChangeWithString(t.Author, t.AuthorYear);
                //var names = PdfHelper.NamesViewChange(t.GerName, t.EngName, t.FraName, t.PorName);
            }
        }

        private static void AddDivisionsChildrenList(PdfDocument pdf, ObservableCollection<Tbl09Division> divisionsList)
        {
            throw new NotImplementedException();
        }
        private static void AddRefExpertList(PdfDocument pdf, ObservableCollection<Tbl90Reference> expertsList)
        {
            throw new NotImplementedException();
        }
        private static void AddRefSourceList(PdfDocument pdf, ObservableCollection<Tbl90Reference> sourcesList)
        {
            throw new NotImplementedException();
        }
        private static void AddRefAuthorList(PdfDocument pdf, ObservableCollection<Tbl90Reference> authorsList)
        {
            throw new NotImplementedException();
        }
        private static void AddCommentList(PdfDocument pdf, ObservableCollection<Tbl93Comment> commentsList)
        {
            throw new NotImplementedException();
        }


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

        //  public static ObservableCollection<Tbl06Phylum> phylumsList { get; set; }
        //     public IList<Tbl03Regnum> Tbl03RegnumsSearchResults { get; set; }

        public static PdfDocument HeaderMainPdf(SaveFileDialog sfd)
        {

            // Initialize the PDF document 
            // Set up the fonts to be used on the pages 
            //  var margin = Utilities.MillimetersToPoints(Convert.ToSingle(5));
            // var doc = new PdfDocument(PageSize.A4, margin, margin, margin, margin);

            //  PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
            var pdf = new PdfDocument();
            // Set the margins and page size
            SetStandardPageSize(pdf);

            // Open the document for writing content 
         //   doc.OnOpenDocument();
            return pdf;
        }

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
    

