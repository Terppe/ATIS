using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using GalaSoft.MvvmLight;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;   

    
         //    ReportTbl63InfratribussesPdf Skriptdatum:  08.11.2018  10:32    

namespace Te.Atis.Ui.Desktop.Views.Report.PDF
{     
    
    public class ReportTbl63InfratribussesPdf : ViewModelBase
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
        private static Tbl24Subclass _subclass;  
        private static Tbl27Infraclass _infraclass; 
        private static Tbl30Legio _legio;  
        private static Tbl33Ordo _ordo;   
        private static Tbl36Subordo _subordo;   
        private static Tbl39Infraordo _infraordo;  
        private static Tbl42Superfamily _superfamily;  
        private static Tbl45Family _family;  
        private static Tbl48Subfamily _subfamily;  
        private static Tbl51Infrafamily _infrafamily;  
        private static Tbl54Supertribus _supertribus;  
        private static Tbl57Tribus _tribus;  
        private static Tbl60Subtribus _subtribus;  
        private static Tbl63Infratribus _infratribus;   
 

 //    Part 1    

        
        public static void CreateMainPdf(int id)
        {
            _businessLayer = new BusinessLayer.BusinessLayer();
            _entityException = new DbEntityException();

           var reportVm = new ReportViewModel();
            reportVm.GetTbl63InfratribussesById(id); 

            //From Database Tbl63Infratribusses
            _infratribus = _businessLayer.SingleListTbl63InfratribussesByInfratribusId(id);
            _subtribus = _businessLayer.SingleListTbl60SubtribussesBySubtribusId(_infratribus.SubtribusID);     
            _tribus = _businessLayer.SingleListTbl57TribussesByTribusId(_subtribus.TribusID);     
            _supertribus = _businessLayer.SingleListTbl54SupertribussesBySupertribusId(_tribus.SupertribusID);     
            _infrafamily = _businessLayer.SingleListTbl51InfrafamiliesByInfrafamilyId(_supertribus.InfrafamilyID);     
            _subfamily = _businessLayer.SingleListTbl48SubfamiliesBySubfamilyId(_infrafamily.SubfamilyID);     
            _family = _businessLayer.SingleListTbl45FamiliesByFamilyId(_subfamily.FamilyID);     
            _superfamily = _businessLayer.SingleListTbl42SuperfamiliesBySuperfamilyId(_family.SuperfamilyID);     
            _infraordo = _businessLayer.SingleListTbl39InfraordosByInfraordoId(_superfamily.InfraordoID);     
            _subordo = _businessLayer.SingleListTbl36SubordosBySubordoId(_infraordo.SubordoID);     
            _ordo = _businessLayer.SingleListTbl33OrdosByOrdoId(_subordo.OrdoID);     
            _legio = _businessLayer.SingleListTbl30LegiosByLegioId(_ordo.LegioID);     
            _infraclass = _businessLayer.SingleListTbl27InfraclassesByInfraclassId(_legio.InfraclassID);     
            _subclass = _businessLayer.SingleListTbl24SubclassesBySubclassId(_infraclass.SubclassID);     
            _class = _businessLayer.SingleListTbl21ClassesByClassId(_subclass.ClassID);     
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

                doc = AddTbl63InfratribussesHaeder(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportTaxoNomen));
                doc = AddTbl63InfratribussesTaxoNomenList(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportTaxoHiera));

                doc = AddHierarchyList(doc); 
             
                if (reportVm.Tbl66GenussesList.Count != 0)
                    doc = AddTbl66GenussesChildrenList(doc, reportVm.Tbl66GenussesList); 
          
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
          
        private static Document AddTbl63InfratribussesHaeder(Document doc)       
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
          
            table.AddCell(new PdfPCell(new Phrase(_infratribus.InfratribusName + "  " + _infratribus.Author + "  " + _infratribus.AuthorYear, LargeFont)) { Colspan = 4, Border = 0 });  // 1.-4. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxonomicId + " " + Convert.ToString(_infratribus.CountID), StandardFont)) { Colspan = 4, Border = 0 }); // 1.-4. field
            doc.Add(table);
            return doc;
        }  
          
        private static Document AddTbl63InfratribussesTaxoNomenList(Document doc)           
          
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
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infratribus, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field
  
          
            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportSynonyms, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_infratribus.Synonym, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. fField
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportCommonNames, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_infratribus.GerName + " " + CultRes.StringsRes.ReportGerman, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 2, Border = 0 });  // 1. + 2.  field
            table.AddCell(new PdfPCell(new Phrase(_infratribus.EngName + " " + CultRes.StringsRes.ReportEnglish, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 2, Border = 0 });  // 1. + 2.  field
            table.AddCell(new PdfPCell(new Phrase(_infratribus.FraName + " " + CultRes.StringsRes.ReportFrench, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 2, Border = 0 });  // 1. + 2.  field
            table.AddCell(new PdfPCell(new Phrase(_infratribus.PorName + " " + CultRes.StringsRes.ReportPortuguese, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxoStatus, StandardBoldFont)) { Colspan = 2, Border = 0 });  // 2.- 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportCurrentStand, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_infratribus.Valid), StandardFont)) { Border = 0 });  // 3. field
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
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_infratribus.UpdaterDate, CultureInfo.InvariantCulture), StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportInfo, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_infratribus.Info, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemo, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_infratribus.Memo, StandardFont)) { Border = 0 });  // 3. field
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
            tableRegnum.SetWidths(new[] {0.05f, 1.25f, 2.50f, 1.50f  });

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
                SpacingAfter = 0f
            };
            tableClass.SetWidths(new[] { 0.17f, 1.13f, 2.50f, 1.50f });

            tableClass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableClass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Class, SmallFont)) { Border = 0 });  // 2. field
            tableClass.AddCell(new PdfPCell(new Phrase(_class.ClassName + " - " + _class.Author + "  " + _class.GerName + " " + _class.EngName + " " + _class.FraName + " " + _class.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableClass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableClass);
            }
            //-----------------------------------------------------
            if (_subclass.SubclassName.Contains("#") == false)
            {
            var tableSubclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubclass.SetWidths(new[] { 0.20f, 1.10f, 2.50f, 1.50f });

            tableSubclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subclass, SmallFont)) { Border = 0 });  // 2. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(_subclass.SubclassName + " - " + _subclass.Author + "  " + _subclass.GerName + " " + _subclass.EngName + " " + _subclass.FraName + " " + _subclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubclass);
             }
            //-----------------------------------------------------
            if (_infraclass.InfraclassName.Contains("#") == false)
            {
            var tableInfraclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraclass.SetWidths(new[] { 0.23f, 1.07f, 2.50f, 1.50f });

            tableInfraclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraclass, SmallFont)) { Border = 0 });  // 2. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(_infraclass.InfraclassName + " - " + _infraclass.Author + "  " + _infraclass.GerName + " " + _infraclass.EngName + " " + _infraclass.FraName + " " + _infraclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraclass);
            }
            //-----------------------------------------------------
            if (_legio.LegioName.Contains("#") == false)
            {
            var tableLegio = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableLegio.SetWidths(new[] { 0.26f, 1.04f, 2.50f, 1.50f });

            tableLegio.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableLegio.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Legio, SmallFont)) { Border = 0 });  // 2. field
            tableLegio.AddCell(new PdfPCell(new Phrase(_legio.LegioName + " - " + _legio.Author + "  " + _legio.GerName + " " + _legio.EngName + " " + _legio.FraName + " " + _legio.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableLegio.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableLegio);
            }
            //-----------------------------------------------------
            if (_ordo.OrdoName.Contains("#") == false)
            {
            var tableOrdo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableOrdo.SetWidths(new[] { 0.29f, 1.01f, 2.50f, 1.50f });

            tableOrdo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableOrdo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Ordo, SmallFont)) { Border = 0 });  // 2. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(_ordo.OrdoName + " - " + _ordo.Author + "  " + _ordo.GerName + " " + _ordo.EngName + " " + _ordo.FraName + " " + _ordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableOrdo);
            }
            //-----------------------------------------------------
            if (_subordo.SubordoName.Contains("#") == false)
            {
            var tableSubordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubordo.SetWidths(new[] { 0.32f, 0.98f, 2.50f, 1.50f });

            tableSubordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subordo, SmallFont)) { Border = 0 });  // 2. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(_subordo.SubordoName + " - " + _subordo.Author + "  " + _subordo.GerName + " " + _subordo.EngName + " " + _subordo.FraName + " " + _subordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubordo);
            }
            //-----------------------------------------------------
            if (_infraordo.InfraordoName.Contains("#") == false)
            {
            var tableInfraordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraordo.SetWidths(new[] { 0.35f, 0.95f, 2.50f, 1.50f });

            tableInfraordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraordo, SmallFont)) { Border = 0 });  // 2. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(_infraordo.InfraordoName + " - " + _infraordo.Author + "  " + _infraordo.GerName + " " + _infraordo.EngName + " " + _infraordo.FraName + " " + _infraordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraordo);
            }
            //-----------------------------------------------------
            if (_superfamily.SuperfamilyName.Contains("#") == false)
            {
            var tableSuperfamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperfamily.SetWidths(new[] { 0.38f, 0.92f, 2.50f, 1.50f });

            tableSuperfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(_superfamily.SuperfamilyName + " - " + _superfamily.Author + "  " + _superfamily.GerName + " " + _superfamily.EngName + " " + _superfamily.FraName + " " + _superfamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperfamily);
            }
            //-----------------------------------------------------
            if (_family.FamilyName.Contains("#") == false)
            {
            var tableFamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableFamily.SetWidths(new[] { 0.41f, 0.89f, 2.50f, 1.50f });

            tableFamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableFamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Family, SmallFont)) { Border = 0 });  // 2. field
            tableFamily.AddCell(new PdfPCell(new Phrase(_family.FamilyName + " - " + _family.Author + "  " + _family.GerName + " " + _family.EngName + " " + _family.FraName + " " + _family.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableFamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableFamily);
            }
            //-----------------------------------------------------
            if (_subfamily.SubfamilyName.Contains("#") == false)
            {
            var tableSubfamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubfamily.SetWidths(new[] { 0.44f, 0.86f, 2.50f, 1.50f });

            tableSubfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(_subfamily.SubfamilyName + " - " + _subfamily.Author + "  " + _subfamily.GerName + " " + _subfamily.EngName + " " + _subfamily.FraName + " " + _subfamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubfamily);
            }
            //-----------------------------------------------------
            if (_infrafamily.InfrafamilyName.Contains("#") == false)
            {
            var tableInfrafamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfrafamily.SetWidths(new[] { 0.47f, 0.83f, 2.50f, 1.50f });

            tableInfrafamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infrafamily, SmallFont)) { Border = 0 });  // 2. field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(_infrafamily.InfrafamilyName + " - " + _infrafamily.Author + "  " + _infrafamily.GerName + " " + _infrafamily.EngName + " " + _infrafamily.FraName + " " + _infrafamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfrafamily);
            }
            //-----------------------------------------------------
            if (_supertribus.SupertribusName.Contains("#") == false)
            {
            var tableSupertribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSupertribus.SetWidths(new[] { 0.50f, 0.80f, 2.50f, 1.50f });

            tableSupertribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Supertribus, SmallFont)) { Border = 0 });  // 2. field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(_supertribus.SupertribusName + " - " + _supertribus.Author + "  " + _supertribus.GerName + " " + _supertribus.EngName + " " + _supertribus.FraName + " " + _supertribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSupertribus);
            }
            //-----------------------------------------------------
            if (_tribus.TribusName.Contains("#") == false)
            {
            var tabletribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tabletribus.SetWidths(new[] { 0.53f, 0.77f, 2.50f, 1.50f });

            tabletribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tabletribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Tribus, SmallFont)) { Border = 0 });  // 2. field
            tabletribus.AddCell(new PdfPCell(new Phrase(_tribus.TribusName + " - " + _tribus.Author + "  " + _tribus.GerName + " " + _tribus.EngName + " " + _tribus.FraName + " " + _tribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tabletribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tabletribus);
            }
            //-----------------------------------------------------
            if (_subtribus.SubtribusName.Contains("#") == false)
            {
            var tablesubtribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tablesubtribus.SetWidths(new[] { 0.56f, 0.74f, 2.50f, 1.50f });

            tablesubtribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tablesubtribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subtribus, SmallFont)) { Border = 0 });  // 2. field
            tablesubtribus.AddCell(new PdfPCell(new Phrase(_subtribus.SubtribusName + " - " + _subtribus.Author + "  " + _subtribus.GerName + " " + _subtribus.EngName + " " + _subtribus.FraName + " " + _subtribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tablesubtribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tablesubtribus);
            }
            //-----------------------------------------------------
            if (_infratribus.InfratribusName.Contains("#") == false)
            {
            var tableInfratribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f
            };
            tableInfratribus.SetWidths(new[] { 0.59f, 0.71f, 2.50f, 1.50f });

            tableInfratribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfratribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infratribus, SmallFont)) { Border = 0 });  // 2. field
            tableInfratribus.AddCell(new PdfPCell(new Phrase(_infratribus.InfratribusName + " - " + _infratribus.Author + "  " + _infratribus.GerName + " " + _infratribus.EngName + " " + _infratribus.FraName + " " + _infratribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfratribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfratribus);
            }
            return doc;
        }       
             
        private static Document AddTbl66GenussesChildrenList(Document doc, ObservableCollection<Tbl66Genus> tbl66GenussesList)        
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
            
            table.SetWidths(new[] { 0.62f, 0.68f, 2.50f, 1.50f });     
             
            table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 2, Border = 0 });  // 1. -2. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDirectChild, SmallBoldFont)) { Border = 0 }); //   3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            foreach (var t in tbl66GenussesList)   
            {
                var t1 = t.GenusName;     
                var t2 = t.Author;
                var t3 = t.GerName;
                var t4 = t.EngName;
                var t5 = t.FraName;
                var t6 = t.PorName;

                table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.   field
                table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Genus, SmallFont)) { Border = 0 });  // 2. field
                table.AddCell(new PdfPCell(new Phrase(t1 + " - " + t2 + " " + t3 + " " + t4 + " " + t5 + " " + t6, SmallFont)) { Border = 0 });   // 3. field
                table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field
            }
            doc.Add(table);

            return doc;
        }   
 
   }
}   
