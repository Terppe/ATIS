using System.Reflection.Metadata;
using ATIS.Ui.Helper;
//using iText.StyledXmlParser.Jsoup.Nodes;
using Microsoft.Win32;

namespace ATIS.Ui.Views.Report.PDF
{
    public class PdfHelper : ViewModelBase
    {
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
