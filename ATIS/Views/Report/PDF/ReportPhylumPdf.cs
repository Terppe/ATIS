using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using ATIS.Dal.Models;
using ATIS.Ui.Helper;
using iText.StyledXmlParser.Jsoup.Nodes;
using Microsoft.Win32;
using Spire.Pdf;
using Spire.Pdf.Graphics;

namespace ATIS.Ui.Views.Report.PDF
{
    public class ReportPhylumPdf : ViewModelBase
    {

        // Set up the fonts to be used on the pages
        //private static readonly Font LargeFont = new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD, BaseColor.BLACK);
        //private static readonly Font StandardFont = new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.BLACK);
        //private static readonly Font StandardBoldFont = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK);
        //private static readonly Font SmallFont = new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK);
        //private static readonly Font SmallBoldFont = new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.BLACK);
        //private static IBusinessLayer _businessLayer;
        //private static DbEntityException _entityException;

        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;


        //    Part 1    

        public static void CreateMainPdf(int id)
        {
            //Create a pdf document
            PdfDocument doc = new PdfDocument();

            //margin

            PdfUnitConvertor unitCvtr = new PdfUnitConvertor();
            PdfMargins margin = new PdfMargins();
            margin.Top = unitCvtr.ConvertUnits(2.54f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
            margin.Bottom = margin.Top;
            margin.Left = unitCvtr.ConvertUnits(3.17f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
            margin.Right = margin.Left;

            // Create one page
            PdfPageBase page = doc.Pages.Add(PdfPageSize.A4, margin);
            page.BackgroundColor = Color.Chocolate;

            //Draw page
            DrawPage(page);

            page = doc.Pages.Add(PdfPageSize.A4, margin);
            page.BackgroundColor = Color.Coral;

            //Draw page
            DrawPage(page);

            page = doc.Pages.Add(PdfPageSize.A3, margin, PdfPageRotateAngle.RotateAngle180, PdfPageOrientation.Landscape);
            page.BackgroundColor = Color.LightPink;

            //Draw page
            DrawPage(page);

            //create section
            PdfSection section = doc.Sections.Add();

            section.PageSettings.Size = PdfPageSize.A4;
            section.PageSettings.Margins = margin;

            page = section.Pages.Add();

            //Draw page
            DrawPage(page);

            //set background color
            page = section.Pages.Add();
            page.BackgroundColor = Color.LightSkyBlue;

            DrawPage(page);

            //Landscape

            section = doc.Sections.Add();
            section.PageSettings.Size = PdfPageSize.A4;
            section.PageSettings.Margins = margin;
            section.PageSettings.Orientation = PdfPageOrientation.Landscape;

            page = section.Pages.Add();
            DrawPage(page);

            //Rotate 90

            section = doc.Sections.Add();
            section.PageSettings.Size = PdfPageSize.A4;
            section.PageSettings.Margins = margin;
            section.PageSettings.Rotate = PdfPageRotateAngle.RotateAngle90;

            page = section.Pages.Add();
            DrawPage(page);

            //Rotate 180

            section = doc.Sections.Add();
            section.PageSettings.Size = PdfPageSize.A4;
            section.PageSettings.Margins = margin;
            section.PageSettings.Rotate = PdfPageRotateAngle.RotateAngle180;
            page = section.Pages.Add();

            DrawPage(page);

            //Save pdf file.

            doc.SaveToFile("PageSetup.pdf");

            doc.Close();


            //Launching the Pdf file.

            System.Diagnostics.Process.Start("PageSetup.pdf");

        }

        private static void DrawPage(PdfPageBase page)
        {
            float pageWidth = page.Canvas.ClientSize.Width;
            float y = 0;

            //title
            y = y + 5;
            PdfBrush brush2 = new PdfSolidBrush(Color.Black);
            PdfTrueTypeFont font2 = new PdfTrueTypeFont(new Font("Arial", 16f, FontStyle.Bold));
            PdfStringFormat format2 = new PdfStringFormat(PdfTextAlignment.Center);
            format2.CharacterSpacing = 1f;
            String text = "Summary of Science";
            page.Canvas.DrawString(text, font2, brush2, pageWidth / 2, y, format2);
            SizeF size = font2.MeasureString(text, format2);
            y = y + size.Height + 6;

            //icon

            PdfImage image = PdfImage.FromFile("Wikipedia_Science.png");
            page.Canvas.DrawImage(image, new PointF(pageWidth - image.PhysicalDimension.Width, y));
            float imageLeftSpace = pageWidth - image.PhysicalDimension.Width - 2;

            float imageBottom = image.PhysicalDimension.Height + y;

            //refenrence content

            PdfTrueTypeFont font3 = new PdfTrueTypeFont(new Font("Arial", 9f));
            PdfStringFormat format3 = new PdfStringFormat();
            format3.ParagraphIndent = font3.Size * 2;
            format3.MeasureTrailingSpaces = true;
            format3.LineSpacing = font3.Size * 1.5f;
            String text1 = "(All text and picture from ";
            String text2 = "Wikipedia";
            String text3 = ", the free encyclopedia)";
            page.Canvas.DrawString(text1, font3, brush2, 0, y, format3);

            size = font3.MeasureString(text1, format3);
            float x1 = size.Width;
            format3.ParagraphIndent = 0;
            PdfTrueTypeFont font4 = new PdfTrueTypeFont(new Font("Arial", 9f, FontStyle.Underline));
            PdfBrush brush3 = PdfBrushes.Blue;
            page.Canvas.DrawString(text2, font4, brush3, x1, y, format3);
            size = font4.MeasureString(text2, format3);
            x1 = x1 + size.Width;

            page.Canvas.DrawString(text3, font3, brush2, x1, y, format3);
            y = y + size.Height;


            //content

            PdfStringFormat format4 = new PdfStringFormat();
            text = System.IO.File.ReadAllText("Summary_of_Science.txt");
            PdfTrueTypeFont font5 = new PdfTrueTypeFont(new Font("Arial", 10f));
            format4.LineSpacing = font5.Size * 1.5f;
            PdfStringLayouter textLayouter = new PdfStringLayouter();
            float imageLeftBlockHeight = imageBottom - y;
            PdfStringLayoutResult result = textLayouter.Layout(text, font5, format4, new SizeF(imageLeftSpace, imageLeftBlockHeight));

            if (result.ActualSize.Height < imageBottom - y)
            {
                imageLeftBlockHeight = imageLeftBlockHeight + result.LineHeight;
                result = textLayouter.Layout(text, font5, format4, new SizeF(imageLeftSpace, imageLeftBlockHeight));
            }
            foreach (LineInfo line in result.Lines)
            {
                page.Canvas.DrawString(line.Text, font5, brush2, 0, y, format4);
                y = y + result.LineHeight;
            }

            PdfTextWidget textWidget = new PdfTextWidget(result.Remainder, font5, brush2);
            PdfTextLayout textLayout = new PdfTextLayout();
            textLayout.Break = PdfLayoutBreakType.FitPage;
            textLayout.Layout = PdfLayoutType.Paginate;
            RectangleF bounds = new RectangleF(new PointF(0, y), page.Canvas.ClientSize);
            textWidget.StringFormat = format4;
            textWidget.Draw(page, bounds, textLayout);

        }


        //public static void CreateMainPdf(int id)
        //{
        //    _businessLayer = new BusinessLayer.BusinessLayer();
        //    _entityException = new DbEntityException();

        //    var reportVm = new ReportViewModel();
        //    reportVm.GetTbl06PhylumsById(id);

        //    //From Database Tbl06Phylums
        //    _phylum = _businessLayer.SingleListTbl06PhylumsByPhylumId(id);
        //    _regnum = _businessLayer.SingleListTbl03RegnumsByRegnumId(_phylum.RegnumID);


        //    var sfd = new SaveFileDialog { Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*" };
        //    var saveResult = sfd.ShowDialog();
        //    if (saveResult != true) return;  //exit
        //    Document doc = null;

        //    try
        //    {

        //        doc = PdfHelper.HeaderMainPdf(sfd);
        //        // Add pages to the document
        //        PdfHelper.AddReportMain(doc);

        //        doc = AddTbl06PhylumsHaeder(doc);
        //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportTaxoNomen));
        //        doc = AddTbl06PhylumsTaxoNomenList(doc);
        //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportTaxoHiera));

        //        doc = AddHierarchyList(doc);

        //        if (reportVm.SubphylumsCollection.Count != 0)
        //            doc = AddTbl12SubphylumsChildrenList(doc, reportVm.SubphylumsCollection);

        //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportReferences));
        //        if (reportVm.ExpertsCollection.Count != 0)
        //            doc = PdfHelper.AddRefExpertList(doc, reportVm.ExpertsCollection);
        //        if (reportVm.SourcesCollection.Count != 0)
        //            doc = PdfHelper.AddRefSourceList(doc, reportVm.SourcesCollection);
        //        if (reportVm.AuthorsCollection.Count != 0)
        //            doc = PdfHelper.AddRefAuthorList(doc, reportVm.AuthorsCollection);
        //        PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportComments));
        //        if (reportVm.CommentsCollection.Count != 0)
        //            doc = PdfHelper.AddCommentList(doc, reportVm.CommentsCollection);

        //    }
        //    catch (DocumentException)
        //    {
        //        // Handle iTextSharp errors
        //    }
        //    finally
        //    {
        //        // Clean up
        //        doc?.Close();
        //        doc = null;
        //    }
        //}

        //private static Document AddTbl06PhylumsHaeder(Document doc)
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

        //    var author = PdfHelper.AuthorViewChangeWithoutString(_phylum.Author, _phylum.AuthorYear);

        //    table.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " " + author, LargeFont)) { Colspan = 4, Border = 0 });  // 1.-4. field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxonomicId + " " + Convert.ToString(_phylum.CountID), StandardFont)) { Colspan = 4, Border = 0 }); // 1.-4. field
        //    doc.Add(table);
        //    return doc;
        //}

        //private static Document AddTbl06PhylumsTaxoNomenList(Document doc)

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
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, StandardFont)) { Border = 0 });  // 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field


        //    table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportSynonyms, StandardFont)) { Border = 0 });  // 2. field
        //    table.AddCell(new PdfPCell(new Phrase(_phylum.Synonym, StandardFont)) { Border = 0 });  // 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //    table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. fField
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportCommonNames, StandardFont)) { Border = 0 });  // 2. field
        //    table.AddCell(new PdfPCell(new Phrase(_phylum.GerName + " " + CultRes.StringsRes.ReportGerman, StandardFont)) { Border = 0 });  // 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //    table.AddCell(new PdfPCell { Colspan = 2, Border = 0 });  // 1. + 2.  field
        //    table.AddCell(new PdfPCell(new Phrase(_phylum.EngName + " " + CultRes.StringsRes.ReportEnglish, StandardFont)) { Border = 0 });  // 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //    table.AddCell(new PdfPCell { Colspan = 2, Border = 0 });  // 1. + 2.  field
        //    table.AddCell(new PdfPCell(new Phrase(_phylum.FraName + " " + CultRes.StringsRes.ReportFrench, StandardFont)) { Border = 0 });  // 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //    table.AddCell(new PdfPCell { Colspan = 2, Border = 0 });  // 1. + 2.  field
        //    table.AddCell(new PdfPCell(new Phrase(_phylum.PorName + " " + CultRes.StringsRes.ReportPortuguese, StandardFont)) { Border = 0 });  // 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //    table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxoStatus, StandardBoldFont)) { Colspan = 2, Border = 0 });  // 2.- 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //    table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportCurrentStand, StandardFont)) { Border = 0 });  // 2. field
        //    table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_phylum.Valid), StandardFont)) { Border = 0 });  // 3. field
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
        //    table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_phylum.UpdaterDate, CultureInfo.InvariantCulture), StandardFont)) { Border = 0 });  // 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

        //    table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportInfo, StandardFont)) { Border = 0 });  // 2. field
        //    table.AddCell(new PdfPCell(new Phrase(_phylum.Info, StandardFont)) { Border = 0 });  // 3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

        //    table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemo, StandardFont)) { Border = 0 });  // 2. field
        //    table.AddCell(new PdfPCell(new Phrase(_phylum.Memo, StandardFont)) { Border = 0 });  // 3. field
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
        //            SpacingAfter = 0f
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
        //    //-----------------------------------------------------
        //    if (_phylum.PhylumName.Contains("#") == false)
        //    {
        //        var tablePhylum = new PdfPTable(4)
        //        {
        //            TotalWidth = 792f, //actual width of table in points
        //            LockedWidth = true,   //fix the absolute width of the table
        //            WidthPercentage = 100,
        //            HorizontalAlignment = 0,  //0=Left aLign, 1=Center
        //            SpacingBefore = 0f,
        //            SpacingAfter = 10f
        //        };
        //        tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

        //        var author = PdfHelper.AuthorViewChangeWithString(_phylum.Author, _phylum.AuthorYear);
        //        var names = PdfHelper.NamesViewChange(_phylum.GerName, _phylum.EngName, _phylum.FraName, _phylum.PorName);

        //        tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
        //        tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 });  // 2. field
        //        tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
        //        tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //        doc.Add(tablePhylum);
        //    }
        //    return doc;
        //}

        //private static Document AddTbl12SubphylumsChildrenList(Document doc, ObservableCollection<Tbl12Subphylum> tbl12SubphylumsList)
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

        //    table.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 2, Border = 0 });  // 1. -2. field
        //    table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDirectChild, SmallBoldFont)) { Border = 0 }); //   3. field
        //    table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

        //    foreach (var t in tbl12SubphylumsList)
        //    {
        //        var t1 = t.SubphylumName;

        //        var author = PdfHelper.AuthorViewChangeWithString(t.Author, t.AuthorYear);
        //        var names = PdfHelper.NamesViewChange(t.GerName, t.EngName, t.FraName, t.PorName);

        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.   field
        //        table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 });  // 2. field
        //        table.AddCell(new PdfPCell(new Phrase(t1 + " " + author + " " + names, SmallFont)) { Border = 0 });   // 3. field
        //        table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field
        //    }
        //    doc.Add(table);

        //    return doc;
        //}

    }
}
