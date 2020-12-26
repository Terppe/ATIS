using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using GalaSoft.MvvmLight;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;   

    
         //    ReportTbl21ClassesPdf Skriptdatum:  19.06.2018  18:32    

namespace Te.Atis.Ui.Desktop.Views.Report.PDF
{     
    
    public class ReportTbl21ClassesPdf : ViewModelBase
    {     
         
        // Set up the fonts to be used on the pages
        private static readonly Font LargeFont = new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD, BaseColor.BLACK);
        private static readonly Font StandardFont = new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.BLACK);
        private static readonly Font StandardBoldFont = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK);
        private static readonly Font SmallFont = new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK);
        private static readonly Font SmallBoldFont = new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.BLACK);   
        private static IBusinessLayer _businessLayer;
        private static DbEntityException _entityException;  
        
        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;
        private static Tbl09Division _division;
        private static Tbl12Subphylum _subphylum;
        private static Tbl15Subdivision _subdivision;
        private static Tbl18Superclass _superclass; 
        private static Tbl21Class _class;   
 

 //    Part 1    

        
        public static void CreateMainPdf(int id)
        {
            _businessLayer = new BusinessLayer.BusinessLayer();
            _entityException = new DbEntityException();

           var reportVm = new ReportViewModel();
            reportVm.GetTbl21ClassesById(id); 

            //From Database Tbl21Classes
            _class = _businessLayer.SingleListTbl21ClassesByClassId(id);
            _superclass = _businessLayer.SingleListTbl18SuperclassesBySuperclassId(_class.SuperclassID);     
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                _subdivision = _businessLayer.SingleListTbl15SubdivisionsBySubdivisionId(_superclass.SubdivisionID);     
                _division = _businessLayer.SingleListTbl09DivisionsByDivisionId(_subdivision.DivisionID);     
                _regnum = _businessLayer.SingleListTbl03RegnumsByRegnumId(_division.RegnumID);     
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                _subphylum = _businessLayer.SingleListTbl12SubphylumsBySubphylumId(_superclass.SubphylumID);     
                _phylum = _businessLayer.SingleListTbl06PhylumsByPhylumId(_subphylum.PhylumID);     
                _regnum = _businessLayer.SingleListTbl03RegnumsByRegnumId(_phylum.RegnumID);     
            }   
             

            var sfd = new SaveFileDialog { Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*" };
            var saveResult = sfd.ShowDialog();
            if (saveResult != true) return;  //exit
            Document doc = null; 

            try
            { 
             
                doc = PdfHelper.HeaderMainPdf(sfd);
                // Add pages to the document
                PdfHelper.AddReportMain(doc);

                doc = AddTbl21ClassesHaeder(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportTaxoNomen));
                doc = AddTbl21ClassesTaxoNomenList(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportTaxoHiera));

                doc = AddHierarchyList(doc); 
             
                if (reportVm.Tbl24SubclassesList.Count != 0)
                    doc = AddTbl24SubclassesChildrenList(doc, reportVm.Tbl24SubclassesList); 
          
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
          
        private static Document AddTbl21ClassesHaeder(Document doc)       
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
          
            table.AddCell(new PdfPCell(new Phrase(_class.ClassName + "  " + _class.Author + "  " + _class.AuthorYear, LargeFont)) { Colspan = 4, Border = 0 });  // 1.-4. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxonomicId + " " + Convert.ToString(_class.CountID), StandardFont)) { Colspan = 4, Border = 0 }); // 1.-4. field
            doc.Add(table);
            return doc;
        }  
          
        private static Document AddTbl21ClassesTaxoNomenList(Document doc)           
          
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
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Class, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field
  
          
            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportSynonyms, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_class.Synonym, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. fField
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportCommonNames, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_class.GerName + " " + CultRes.StringsRes.ReportGerman, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 2, Border = 0 });  // 1. + 2.  field
            table.AddCell(new PdfPCell(new Phrase(_class.EngName + " " + CultRes.StringsRes.ReportEnglish, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 2, Border = 0 });  // 1. + 2.  field
            table.AddCell(new PdfPCell(new Phrase(_class.FraName + " " + CultRes.StringsRes.ReportFrench, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 2, Border = 0 });  // 1. + 2.  field
            table.AddCell(new PdfPCell(new Phrase(_class.PorName + " " + CultRes.StringsRes.ReportPortuguese, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxoStatus, StandardBoldFont)) { Colspan = 2, Border = 0 });  // 2.- 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportCurrentStand, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_class.Valid), StandardFont)) { Border = 0 });  // 3. field
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
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_class.UpdaterDate, CultureInfo.InvariantCulture), StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportInfo, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_class.Info, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemo, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_class.Memo, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

            doc.Add(table);

            return doc;
        }  
                
        private static Document AddHierarchyList(Document doc)
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true, //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0, //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f
            };
            tableRegnum.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 }); // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont))  { Border = 0 }); // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum);
            }

            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                //-----------------------------------------------------
            if (_division.DivisionName.Contains("#") == false)
            {
                var tableDivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableDivision.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 }); // 2. field
                tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " - " + _division.Author + "  " + _division.GerName + " " + _division.EngName + " " + _division.FraName + " " + _division.PorName,  SmallFont))  { Border = 0 }); // 3. field
                tableDivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableDivision);
                }
                //-----------------------------------------------------
            if (_subdivision.SubdivisionName.Contains("#") == false)
            {
                var tableSubdivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubdivision.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubdivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableSubdivision.AddCell( new PdfPCell(new Phrase(CultRes.StringsRes.Subdivision, SmallFont)) { Border = 0 }); // 2. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(_subdivision.SubdivisionName + " - " + _subdivision.Author + "  " + _subdivision.GerName + " " + _subdivision.EngName + " " + _subdivision.FraName + " " + _subdivision.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubdivision);
                }
                //-----------------------------------------------------
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                //-----------------------------------------------------
            if (_phylum.PhylumName.Contains("#") == false)
            {
                var tablePhylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 }); // 2. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " - " + _phylum.Author + "  " + _phylum.GerName + " " + _phylum.EngName + " " + _phylum.FraName + " " + _phylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tablePhylum);
                }
                //-----------------------------------------------------
            if (_subphylum.SubphylumName.Contains("#") == false)
            {
                var tableSubphylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubphylum.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubphylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 }); // 2. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(_subphylum.SubphylumName + " - " + _subphylum.Author + "  " + _subphylum.GerName + " " + _subphylum.EngName + " " + _subphylum.FraName + " " + _subphylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubphylum);
                }
            }
            //-----------------------------------------------------
            if (_superclass.SuperclassName.Contains("#") == false)
            {
            var tableSuperclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperclass.SetWidths(new[] { 0.14f, 1.16f, 2.50f, 1.50f });

            tableSuperclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superclass, SmallFont)) { Border = 0 });  // 2. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(_superclass.SuperclassName + " - " + _superclass.Author + "  " + _superclass.GerName + " " + _superclass.EngName + " " + _superclass.FraName + " " + _superclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperclass);
            }
            //-----------------------------------------------------
            if (_class.ClassName.Contains("#") == false)
            {
            var tableClass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f
            };
            tableClass.SetWidths(new[] { 0.17f, 1.13f, 2.50f, 1.50f });

            tableClass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableClass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Class, SmallFont)) { Border = 0 });  // 2. field
            tableClass.AddCell(new PdfPCell(new Phrase(_class.ClassName + " - " + _class.Author + "  " + _class.GerName + " " + _class.EngName + " " + _class.FraName + " " + _class.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableClass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableClass);
            }
            return doc;
        }       
             
        private static Document AddTbl24SubclassesChildrenList(Document doc, ObservableCollection<Tbl24Subclass> tbl24SubclassesList)        
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
            
            table.SetWidths(new[] { 0.20f, 1.10f, 2.50f, 1.50f});     
             
            table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 2, Border = 0 });  // 1. -2. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDirectChild, SmallBoldFont)) { Border = 0 }); //   3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            foreach (var t in tbl24SubclassesList)   
            {
                var t1 = t.SubclassName;     
                var t2 = t.Author;
                var t3 = t.GerName;
                var t4 = t.EngName;
                var t5 = t.FraName;
                var t6 = t.PorName;

                table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.   field
                table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subclass, SmallFont)) { Border = 0 });  // 2. field
                table.AddCell(new PdfPCell(new Phrase(t1 + " - " + t2 + " " + t3 + " " + t4 + " " + t5 + " " + t6, SmallFont)) { Border = 0 });   // 3. field
                table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field
            }
            doc.Add(table);

            return doc;
        }   
 
   }
}   
