using System;
using System.Collections.ObjectModel;
using System.Globalization;
using GalaSoft.MvvmLight;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.BusinessLayer;   

        
        using Tyrrrz.Extensions;   
    
         //    ReportTbl69FiSpeciessesPdf Skriptdatum:  15.12.2019  10:32    

namespace Te.Atis.Ui.Desktop.Views.Report.PDF
{     
    
    public class ReportTbl69FiSpeciessesPdf : ViewModelBase
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
        private static Tbl66Genus _genus;  
        private static Tbl69FiSpecies _fispecies;   
 

 //    Part 1    

        
        public static void CreateMainPdf(int id, int fishId, int plantId )
        {
            _businessLayer = new BusinessLayer.BusinessLayer();
            _entityException = new DbEntityException();

           var reportVm = new ReportViewModel();
            reportVm.GetTbl69FiSpeciessesById(id, fishId, plantId); 

            //From Database Tbl69FiSpeciesses
            _fispecies = _businessLayer.SingleListTbl69FiSpeciessesByFiSpeciesId(id);
            _genus = _businessLayer.SingleListTbl66GenussesByGenusId(_fispecies.GenusID);     
            _infratribus = _businessLayer.SingleListTbl63InfratribussesByInfratribusId(_genus.InfratribusID);     
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
            if (_superclass.SubphylumID == fishId)  //Basis #Subphylum#
            {
                _subdivision = _businessLayer.SingleListTbl15SubdivisionsBySubdivisionId(_superclass.SubdivisionID);     
                _division = _businessLayer.SingleListTbl09DivisionsByDivisionId(_subdivision.DivisionID);     
                _regnum = _businessLayer.SingleListTbl03RegnumsByRegnumId(_division.RegnumID);     
            }
            if (_superclass.SubdivisionID == plantId)  //Basis #Subdivision#
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

                doc = AddTbl69FiSpeciessesHaeder(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportTaxoNomen));

                doc = AddTbl69FiSpeciessesTaxoNomenRankList(doc);
                if (reportVm.Tbl84SynonymsList.Count != 0)
                    doc = PdfHelper.AddTbl84SynonymsList(doc, reportVm.Tbl84SynonymsList);
                if (reportVm.Tbl78NamesList.Count != 0)
                    doc = PdfHelper.AddTbl78NamesList(doc, reportVm.Tbl78NamesList);
                doc = AddTbl69FiSpeciessesTaxoNomenStatusList(doc);
                doc = AddTbl69FiSpeciessesSpecificationList(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportTaxoHiera));

                doc = AddHierarchyList(doc);  

                if (reportVm.Tbl69FiSpeciessesSubList.Count != 0)
                    doc = AddTbl69FiSpeciessesChildrenList(doc, reportVm.Tbl69FiSpeciessesSubList);

                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportReferences));
                if (reportVm.Tbl90ExpertsList.Count != 0)
                    doc = PdfHelper.AddRefExpertList(doc, reportVm.Tbl90ExpertsList);
                if (reportVm.Tbl90SourcesList.Count != 0)
                    doc = PdfHelper.AddRefSourceList(doc, reportVm.Tbl90SourcesList);
                if (reportVm.Tbl90AuthorsList.Count != 0)
                    doc = PdfHelper.AddRefAuthorList(doc, reportVm.Tbl90AuthorsList);
                //     PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportGeographics));
                //     PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportImages)); 
            
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportGeographics));
                if (reportVm.Tbl87GeographicsList.Count != 0)
                    doc = PdfHelper.AddTbl87GeographicsList(doc, reportVm.Tbl87GeographicsList);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportImages));
                if (reportVm.Tbl81ImagesList.Count != 0)
                    doc = PdfHelper.AddTbl81ImagesList(doc, reportVm.Tbl81ImagesList);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportComments));
                if (reportVm.Tbl93CommentsList.Count != 0)
                    doc = PdfHelper.AddCommentList(doc, reportVm.Tbl93CommentsList);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportTechnic));
                doc = AddTbl69FiSpeciessesTechnicList(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportFood));
                doc = AddTbl69FiSpeciessesFoodList(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportGu));
                doc = AddTbl69FiSpeciessesGuList(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportSozial));
                doc = AddTbl69FiSpeciessesSozialList(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportHusbandry));
                doc = AddTbl69FiSpeciessesHusbandryList(doc);         
          
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
          
        private static Document AddTbl69FiSpeciessesHaeder(Document doc)       
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
           
            var author = PdfHelper.AuthorViewChangeWithoutString(_fispecies.Author, _fispecies.AuthorYear);

            table.AddCell(new PdfPCell(new Phrase(_fispecies.Tbl66Genusses.GenusName + " " + _fispecies.FiSpeciesName + " " + _fispecies.Subspecies + " " + _fispecies.Divers + " " + author, LargeFont)) { Colspan = 4, Border = 0 });   // 1.-4. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxonomicId + " " + Convert.ToString(_fispecies.CountID), StandardFont)) { Colspan = 4, Border = 0 }); // 1.-4. field
            doc.Add(table);
            return doc;
        }  
                
        private static Document AddTbl69FiSpeciessesTaxoNomenRankList(Document doc)           
          
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
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.FiSpecies, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(table);

            return doc;
        }       
                
        private static Document AddTbl69FiSpeciessesTaxoNomenStatusList(Document doc)
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
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxoStatus, StandardBoldFont)) { Colspan = 2, Border = 0 });  // 2.- 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportCurrentStand, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Valid), StandardFont)) { Border = 0 });  // 3. field
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
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.UpdaterDate, CultureInfo.InvariantCulture), StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

            doc.Add(table);

            return doc;
        }
        private static Document AddTbl69FiSpeciessesSpecificationList(Document doc)
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
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTradeName, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.TradeName, StandardFont)) { Border = 0 });  // 3. field  
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field  

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemoSpecies, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.MemoSpecies, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. fField
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportImporterWithYear, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.Importer + " /  " + _fispecies.ImportingYear, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. fField
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportLNumberLOrigin, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.LNumber + " /  " + _fispecies.LOrigin, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. fField
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportLdaNumberLdaOrigin, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.LDANumber + " /  " + _fispecies.LDAOrigin, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. fField
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportBasinLength, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.BasinLength + " cm  " , StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. fField
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportFishLength, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.FishLength + " cm  " , StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. fField
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemoSpecial, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.MemoSpecial, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

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
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f   
            };
            tableRegnum.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });

            var author = PdfHelper.AuthorViewChangeWithString(_regnum.Author, _regnum.AuthorYear);
            var names = PdfHelper.NamesViewChange(_regnum.GerName, _regnum.EngName, _regnum.FraName, _regnum.PorName);

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 });  // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + " " + _regnum.Subregnum + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
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

            var author = PdfHelper.AuthorViewChangeWithString(_division.Author, _division.AuthorYear);
            var names = PdfHelper.NamesViewChange(_division.GerName, _division.EngName, _division.FraName, _division.PorName);

            tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. . field
            tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 });  // 2. field
            tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " " + author+ " " + names, SmallFont)) { Border = 0 });  // 3. field
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

            var author = PdfHelper.AuthorViewChangeWithString(_subdivision.Author, _subdivision.AuthorYear);
            var names = PdfHelper.NamesViewChange(_subdivision.GerName, _subdivision.EngName, _subdivision.FraName, _division.PorName);

            tableSubdivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubdivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subdivision, SmallFont)) { Border = 0 });  // 2. field
            tableSubdivision.AddCell(new PdfPCell(new Phrase(_subdivision.SubdivisionName + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
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

            var author = PdfHelper.AuthorViewChangeWithString(_phylum.Author, _phylum.AuthorYear);
            var names = PdfHelper.NamesViewChange(_phylum.GerName, _phylum.EngName, _phylum.FraName, _phylum.PorName);

            tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 });  // 2. field
            tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
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

            var author = PdfHelper.AuthorViewChangeWithString(_subphylum.Author, _subphylum.AuthorYear);
            var names = PdfHelper.NamesViewChange(_subphylum.GerName, _subphylum.EngName, _subphylum.FraName, _subphylum.PorName);

            tableSubphylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. . field
            tableSubphylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 });  // 2. field
            tableSubphylum.AddCell(new PdfPCell(new Phrase(_subphylum.SubphylumName + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
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

            var author = PdfHelper.AuthorViewChangeWithString(_superclass.Author, _superclass.AuthorYear);
            var names = PdfHelper.NamesViewChange(_superclass.GerName, _superclass.EngName, _superclass.FraName, _superclass.PorName);

            tableSuperclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superclass, SmallFont)) { Border = 0 });  // 2. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(_superclass.SuperclassName + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
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

            var author = PdfHelper.AuthorViewChangeWithString(_class.Author, _class.AuthorYear);
            var names = PdfHelper.NamesViewChange(_class.GerName, _class.EngName, _class.FraName, _class.PorName);

            tableClass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableClass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Class, SmallFont)) { Border = 0 });  // 2. field
            tableClass.AddCell(new PdfPCell(new Phrase(_class.ClassName + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
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

            var author = PdfHelper.AuthorViewChangeWithString(_subclass.Author, _subclass.AuthorYear);
            var names = PdfHelper.NamesViewChange(_subclass.GerName, _subclass.EngName, _subclass.FraName, _subclass.PorName);

            tableSubclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subclass, SmallFont)) { Border = 0 });  // 2. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(_subclass.SubclassName + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
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

            var author = PdfHelper.AuthorViewChangeWithString(_infraclass.Author, _infraclass.AuthorYear);
            var names = PdfHelper.NamesViewChange(_infraclass.GerName, _infraclass.EngName, _infraclass.FraName, _infraclass.PorName);

            tableInfraclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraclass, SmallFont)) { Border = 0 });  // 2. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(_infraclass.InfraclassName + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
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

            var author = PdfHelper.AuthorViewChangeWithString(_legio.Author, _legio.AuthorYear);
            var names = PdfHelper.NamesViewChange(_legio.GerName, _legio.EngName, _legio.FraName, _legio.PorName);

            tableLegio.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableLegio.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Legio, SmallFont)) { Border = 0 });  // 2. field
            tableLegio.AddCell(new PdfPCell(new Phrase(_legio.LegioName + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
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

            var author = PdfHelper.AuthorViewChangeWithString(_ordo.Author, _ordo.AuthorYear);
            var names = PdfHelper.NamesViewChange(_ordo.GerName, _ordo.EngName, _ordo.FraName, _ordo.PorName);

            tableOrdo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableOrdo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Ordo, SmallFont)) { Border = 0 });  // 2. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(_ordo.OrdoName + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
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

            var author = PdfHelper.AuthorViewChangeWithString(_subordo.Author, _subordo.AuthorYear);
            var names = PdfHelper.NamesViewChange(_subordo.GerName, _subordo.EngName, _subordo.FraName, _subordo.PorName);

            tableSubordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subordo, SmallFont)) { Border = 0 });  // 2. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(_subordo.SubordoName + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
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

            var author = PdfHelper.AuthorViewChangeWithString(_infraordo.Author, _infraordo.AuthorYear);
            var names = PdfHelper.NamesViewChange(_infraordo.GerName, _infraordo.EngName, _infraordo.FraName, _infraordo.PorName);

            tableInfraordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraordo, SmallFont)) { Border = 0 });  // 2. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(_infraordo.InfraordoName + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
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

            var author = PdfHelper.AuthorViewChangeWithString(_superfamily.Author, _superfamily.AuthorYear);
            var names = PdfHelper.NamesViewChange(_superfamily.GerName, _superfamily.EngName, _superfamily.FraName, _superfamily.PorName);

            tableSuperfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(_superfamily.SuperfamilyName + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
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

            var author = PdfHelper.AuthorViewChangeWithString(_family.Author, _family.AuthorYear);
            var names = PdfHelper.NamesViewChange(_family.GerName, _family.EngName, _family.FraName, _family.PorName);

            tableFamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableFamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Family, SmallFont)) { Border = 0 });  // 2. field
            tableFamily.AddCell(new PdfPCell(new Phrase(_family.FamilyName + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
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

            var author = PdfHelper.AuthorViewChangeWithString(_subfamily.Author, _subfamily.AuthorYear);
            var names = PdfHelper.NamesViewChange(_subfamily.GerName, _subfamily.EngName, _subfamily.FraName, _subfamily.PorName);

            tableSubfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(_subfamily.SubfamilyName + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
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

            var author = PdfHelper.AuthorViewChangeWithString(_infrafamily.Author, _infrafamily.AuthorYear);
            var names = PdfHelper.NamesViewChange(_infrafamily.GerName, _infrafamily.EngName, _infrafamily.FraName, _infrafamily.PorName);

            tableInfrafamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infrafamily, SmallFont)) { Border = 0 });  // 2. field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(_infrafamily.InfrafamilyName + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
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

            var author = PdfHelper.AuthorViewChangeWithString(_supertribus.Author, _supertribus.AuthorYear);
            var names = PdfHelper.NamesViewChange(_supertribus.GerName, _supertribus.EngName, _supertribus.FraName, _supertribus.PorName);

            tableSupertribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Supertribus, SmallFont)) { Border = 0 });  // 2. field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(_supertribus.SupertribusName + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
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

            var author = PdfHelper.AuthorViewChangeWithString(_tribus.Author, _tribus.AuthorYear);
            var names = PdfHelper.NamesViewChange(_tribus.GerName, _tribus.EngName, _tribus.FraName, _tribus.PorName);

            tabletribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tabletribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Tribus, SmallFont)) { Border = 0 });  // 2. field
            tabletribus.AddCell(new PdfPCell(new Phrase(_tribus.TribusName + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
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

            var author = PdfHelper.AuthorViewChangeWithString(_subtribus.Author, _subtribus.AuthorYear);
            var names = PdfHelper.NamesViewChange(_subtribus.GerName, _subtribus.EngName, _subtribus.FraName, _subtribus.PorName);

            tablesubtribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tablesubtribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subtribus, SmallFont)) { Border = 0 });  // 2. field
            tablesubtribus.AddCell(new PdfPCell(new Phrase(_subtribus.SubtribusName + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
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
                SpacingAfter = 0f
            };
            tableInfratribus.SetWidths(new[] { 0.59f, 0.71f, 2.50f, 1.50f });

            var author = PdfHelper.AuthorViewChangeWithString(_infratribus.Author, _infratribus.AuthorYear);
            var names = PdfHelper.NamesViewChange(_infratribus.GerName, _infratribus.EngName, _infratribus.FraName, _infratribus.PorName);

            tableInfratribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfratribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infratribus, SmallFont)) { Border = 0 });  // 2. field
            tableInfratribus.AddCell(new PdfPCell(new Phrase(_infratribus.InfratribusName + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
            tableInfratribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfratribus);
            }
            //-----------------------------------------------------
            if (_genus.GenusName.Contains("#") == false)
            {
            var tableGenus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableGenus.SetWidths(new[] { 0.62f, 0.68f, 2.50f, 1.50f });

            var author = PdfHelper.AuthorViewChangeWithString(_genus.Author, _genus.AuthorYear);
            var names = PdfHelper.NamesViewChange(_genus.GerName, _genus.EngName, _genus.FraName, _genus.PorName);

            tableGenus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableGenus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Genus, SmallFont)) { Border = 0 });  // 2. field
            tableGenus.AddCell(new PdfPCell(new Phrase(_genus.GenusName + " " + author + " " + names, SmallFont)) { Border = 0 });  // 3. field
            tableGenus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableGenus);
            }
            //-----------------------------------------------------
            //       if (_fispecies.Subspecies.IsNullOrEmpty() == false)
            if (string.IsNullOrEmpty(_fispecies.Subspecies) == false)
            {
            var tableFiSpecies = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f
            };
            tableFiSpecies.SetWidths(new[] { 0.65f, 0.65f, 2.50f, 1.50f });

            var author = PdfHelper.AuthorViewChangeWithString(_fispecies.Author, _fispecies.AuthorYear);

            tableFiSpecies.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableFiSpecies.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.FiSpecies, SmallFont)) { Border = 0 });  // 2. field
            tableFiSpecies.AddCell(new PdfPCell(new Phrase(_fispecies.Tbl66Genusses.GenusName + " " + _fispecies.FiSpeciesName + " " + author, SmallFont)) { Border = 0 });  // 3. field
            tableFiSpecies.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableFiSpecies);
            
                var tableSubspecies = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true,   //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 10f
                };
                tableSubspecies.SetWidths(new[] { 0.68f, 0.62f, 2.50f, 1.50f });

                var author1 = PdfHelper.AuthorViewChangeWithString(_fispecies.Author, _fispecies.AuthorYear);

                tableSubspecies.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
                tableSubspecies.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subspecies, SmallFont)) { Border = 0 });  // 2. field
                tableSubspecies.AddCell(new PdfPCell(new Phrase(_fispecies.Tbl66Genusses.GenusName + " " + _fispecies.FiSpeciesName + " " + _fispecies.Subspecies + " " + _fispecies.Divers + " " + author1, SmallFont)) { Border = 0 });  // 3. field
                tableSubspecies.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubspecies);
            }
            else
            {
                var tableFiSpecies = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true,   //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 10f
                };
                tableFiSpecies.SetWidths(new[] { 0.65f, 0.65f, 2.50f, 1.50f });

                var author2 = PdfHelper.AuthorViewChangeWithString(_fispecies.Author, _fispecies.AuthorYear);

                tableFiSpecies.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
                tableFiSpecies.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.FiSpecies, SmallFont)) { Border = 0 });  // 2. field
                tableFiSpecies.AddCell(new PdfPCell(new Phrase(_fispecies.Tbl66Genusses.GenusName + " " + _fispecies.FiSpeciesName + " " + author2 , SmallFont)) { Border = 0 });  // 3. field
                tableFiSpecies.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableFiSpecies);
            }

            return doc;
        }

        private static Document AddTbl69FiSpeciessesChildrenList(Document doc, ObservableCollection<Tbl69FiSpecies> tbl69FiSpeciessesSubList)
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

            table.SetWidths(new[] { 0.68f, 0.62f, 2.50f, 1.50f });

            table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 2, Border = 0 });  // 1. -2. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDirectChild, SmallBoldFont)) { Border = 0 }); //   3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            foreach (var t in tbl69FiSpeciessesSubList)
            {
                var t1 = t.Tbl66Genusses.GenusName;
                var t2 = t.FiSpeciesName;
                var t3 = t.Subspecies;
                var t4 = t.Divers;

                var author = PdfHelper.AuthorViewChangeWithString(t.Author, t.AuthorYear);

                table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.   field
                table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subspecies, SmallFont)) { Border = 0 });  // 2. field
                table.AddCell(new PdfPCell(new Phrase(t1 + " " + t2 + " " + t3 + " " + t4 + " " + author, SmallFont)) { Border = 0 });   // 3. field
                table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field
            }
            doc.Add(table);

            return doc;
        }       
             
        private static Document AddTbl69FiSpeciessesTechnicList(Document doc)
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
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportRegionTop, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.RegionTop), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportRegionMiddle, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.RegionMiddle), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportRegionBottom, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.RegionBottom), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDifficult1, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Difficult1), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDifficult2, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Difficult2), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDifficult3, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Difficult3), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDifficult4, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Difficult4), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportBasinLength, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.BasinLength), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportFishLength, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.FishLength), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportLNumberLOrigin, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.LNumber + " / " + _fispecies.LOrigin, SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportLdaNumberLdaOrigin, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.LDANumber + " / " + _fispecies.LDAOrigin, SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportPh12, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Ph1) + " - " + Convert.ToString(_fispecies.Ph2), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTemp12, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Temp1) + " - " + Convert.ToString(_fispecies.Temp2), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportHardness12, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Hardness1) + " - " + Convert.ToString(_fispecies.Hardness2), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportCarboHardness12, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.CarboHardness1) + " - " + Convert.ToString(_fispecies.CarboHardness2), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field


            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemoTechnic, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.MemoTech, SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(table);

            return doc;
        }

        private static Document AddTbl69FiSpeciessesFoodList(Document doc)
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
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportKarnivore, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Karnivore), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportLimnivore, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Limnivore), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportHerbivore, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Herbivore), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportOmnivore, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Omnivore), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemoFood, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.MemoFoods, SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(table);

            return doc;
        }

        private static Document AddTbl69FiSpeciessesGuList(Document doc)
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
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemoDimorphism, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.MemoDomorphism, SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(table);

            return doc;
        }
        private static Document AddTbl69FiSpeciessesSozialList(Document doc)
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
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportSozial, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.MemoSozial, SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(table);

            return doc;
        }
        private static Document AddTbl69FiSpeciessesHusbandryList(Document doc)
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
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportHusbandry, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.MemoHusbandry, SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(table);

            return doc;
        }     
 
   }
}   
