using System;
using System.Collections.ObjectModel;
using System.Globalization;
using DAL;
using DAL.Models;
using GalaSoft.MvvmLight;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;   

    
         //    ReportTbl90ReferencesPdf Skriptdatum:  14.11.2017  10:32    

namespace WPFUI.Views.Report.PDF
{     
    
    public class ReportTbl90ReferencesPdf : ViewModelBase
    {     
         
        // Set up the fonts to be used on the pages
        private static readonly Font LargeFont = new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD, BaseColor.BLACK);
        private static readonly Font StandardFont = new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.BLACK);
        private static readonly Font StandardBoldFont = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK);
        private static readonly Font SmallFont = new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK);
        private static readonly Font SmallBoldFont = new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.BLACK);  
 

 //    Part 1    

             
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.GetTbl90ReferencesById(id); 
  
             

            var sfd = new SaveFileDialog { Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*" };
            var saveResult = sfd.ShowDialog();
            if (saveResult != true) return;  //exit
            Document doc = null; 

            try
            { 
             
                doc = PdfHelper.HeaderMainPdf(sfd);
                // Add pages to the document
                PdfHelper.AddReportMain(doc);

                doc = AddTbl90ReferencesHaeder(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportTaxoNomen));
                doc = AddTbl90ReferencesTaxoNomenList(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportTaxoHiera));

                doc = AddHierarchyList(doc); 
             
                if (reportVm.NULLList.Count != 0)
                    doc = AddNULLChildrenList(doc, reportVm.NULLList); 
          
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportReferences));
                if (reportVm.Tbl90ExpertsList.Count != 0)
                    doc = PdfHelper.AddRefExpertList(doc, reportVm.Tbl90RefExpertsList);
                if (reportVm.Tbl90SourcesList.Count != 0)
                    doc = PdfHelper.AddRefSourceList(doc, reportVm.Tbl90RefSourcesList);
                if (reportVm.Tbl90AuthorsList.Count != 0)
                    doc = PdfHelper.AddRefAuthorList(doc, reportVm.Tbl90RefAuthorsList);
           //     PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportGeographics));
           //     PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportImages));
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportComments));
                if (reportVm.Tbl93CommentsList.Count != 0)
                    doc = PdfHelper.AddCommentList(doc, reportVm.Tbl93CommentsList); 
          
            }
            catch (DocumentException)
            {
                // Handle iTextSharp errors
            }
            finally
            {
                // Clean up
                doc?.Close();
                doc = null;
            }
        }  
          
        private static Document AddTbl90ReferencesHaeder(Document doc)       
        {
            // Add a new page to the document
            doc.NewPage();

            var table = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 10f   //fix the absolute width of the table
            };
            table.SetWidths(new[] { 0.05f, 0.05f, 1.25f, 4.00f });  
          
            table.AddCell(new PdfPCell(new Phrase(_reference.ReferenceName + "  " + _reference.Author + "  " + _reference.AuthorYear, LargeFont)) { Colspan = 4, Border = 0 });  // 1.-4. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxonomicId + " " + Convert.ToString(_reference.CountID), StandardFont)) { Colspan = 4, Border = 0 }); // 1.-4. field
            doc.Add(table);
            return doc;
        }  
          
        private static Document AddTbl90ReferencesTaxoNomenList(Document doc)           
          
        {
            var table = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 10f   //fix the absolute width of the table
            };
            table.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });  
          
            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + " " +_regnum.Subregnum, StandardFont)) { Border = 0 });  // 3. field  
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field  
          
            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxoRank, StandardBoldFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Reference, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field
  
          
            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportSynonyms, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_reference.Synonym, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. fField
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportCommonNames, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_reference.GerName + " " + CultRes.StringsRes.ReportGerman, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 2, Border = 0 });  // 1. + 2.  field
            table.AddCell(new PdfPCell(new Phrase(_reference.EngName + " " + CultRes.StringsRes.ReportEnglish, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 2, Border = 0 });  // 1. + 2.  field
            table.AddCell(new PdfPCell(new Phrase(_reference.FraName + " " + CultRes.StringsRes.ReportFrench, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 2, Border = 0 });  // 1. + 2.  field
            table.AddCell(new PdfPCell(new Phrase(_reference.PorName + " " + CultRes.StringsRes.ReportPortuguese, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxoStatus, StandardBoldFont)) { Colspan = 2, Border = 0 });  // 2.- 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportCurrentStand, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_reference.Valid), StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 4, Border = 0 });  //Empty row

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDataQualiIndicator, StandardBoldFont)) { Colspan = 2, Border = 0 });  // 2.- 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportRecordCrediRate, StandardFont)) { Colspan = 2, Border = 0 });  // 2. - 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportGlobalSpecComp, StandardFont)) { Colspan = 2, Border = 0 });  // 2. - 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportRecordUpdate, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_reference.UpdaterDate, CultureInfo.InvariantCulture), StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportInfo, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_reference.Info, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemo, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_reference.Memo, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

            doc.Add(table);

            return doc;
        }  
             
        private static Document AddNULLChildrenList(Document doc, ObservableCollection<NULL> List)        
        {
            var table = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f   //fix the absolute width of the table
            };   
             
            table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 2, Border = 0 });  // 1. -2. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDirectChild, SmallBoldFont)) { Border = 0 }); //   3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            foreach (var t in List)   
            {
                var t1 = t.NULL;     
                var t2 = t.Author;
                var t3 = t.GerName;
                var t4 = t.EngName;
                var t5 = t.FraName;
                var t6 = t.PorName;

                table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.   field
                table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.NULL, SmallFont)) { Border = 0 });  // 2. field
                table.AddCell(new PdfPCell(new Phrase(t1 + " - " + t2 + " " + t3 + " " + t4 + " " + t5 + " " + t6, SmallFont)) { Border = 0 });   // 3. field
                table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field
            }
            doc.Add(table);

            return doc;
        }   
 
   }
}   
