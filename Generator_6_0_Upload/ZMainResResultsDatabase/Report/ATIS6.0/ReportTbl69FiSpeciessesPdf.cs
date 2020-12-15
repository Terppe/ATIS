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
using Microsoft.Win32;  

    
         //    ReportFiSpeciesPdf Skriptdatum:  15.12.2019  10:32    

namespace ATIS.Ui.Views.Report.D69FiSpecies
{     
    
    public class ReportFiSpeciesPdf : ViewModelBase
    {     
         
        private static readonly ILog Log = LogManager.GetLogger(typeof(ReportFiSpeciesPdf));
        private static readonly CrudFunctions ExtCrud = new CrudFunctions();
        private static readonly PdfHelper PdfHelper = new PdfHelper();
        private static string _n;
        private static string _z1;
        private static int _z;
        private static int[] _arrInts = new int[11];
        private static PdfPage _page;  
 

 //    Part 1    

         
        public static void CreateMainPdf(int id, string use)
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            log4net.Config.XmlConfigurator.Configure();


            //  LicenseManager.AddLicenseData("5IUML-K4LFW-CQ4J0-Y673N-72V88");
            //    BitMiracle.Docotic.LicenseManager.AddLicenseData("5IUML-K4LFW-CQ4J0-Y673N-72V88");      
            //-----------------------------------------------------------------------------     
        
            var fispeciesList = ExtCrud.GetFiSpeciessesCollectionFromFiSpeciesIdOrderBy<Tbl69FiSpecies>(id).FirstOrDefault();    
        
            //Child
            var namesList = ExtCrud.GetNamesCollectionFromFiSpeciesIdOrderBy<Tbl78Name>(id);           
             
            var expertsList = ExtCrud.GetReferenceExpertsCollectionFromFiSpeciesIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(id);
            var sourcesList = ExtCrud.GetReferenceSourcesCollectionFromFiSpeciesIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(id);
            var authorsList = ExtCrud.GetReferenceAuthorsCollectionFromFiSpeciesIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(id);
            var commentsList = ExtCrud.GetCommentsCollectionFromFiSpeciesIdOrderBy<Tbl93Comment>(id);   

            try
            { 
         
                    using var pdf = new PdfDocument();
                    _arrInts = PdfHelper.AddReportMain(pdf); 

                    AddFiSpeciesHaeder(pdf, fispeciesList);
                    AddFiSpeciesTaxoNomenList(pdf, fispeciesList);   
            
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
          
                    switch (use)
                    {
                        case "save":
                            {
                                var sfd = new SaveFileDialog { Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*" };
                                sfd.DefaultExt = ".pdf"; // Default file extension
                                sfd.InitialDirectory = @"C:\";
                                var saveResult = sfd.ShowDialog();
                                // Process save file dialog box results
                                if (saveResult != true) return;
                                // Save document
                                var filename = sfd.FileName;
                                pdf.Save(filename);
                                break;
                            }
                        case "print":
                            {
                                var pr = new PdfPrintDocument(pdf, PrintSize.FitPage);
                                pr.PrintDocument.Print();
                                break;
                            }
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
                //        if (pdf != null) pdf.Dispose();
                //     doc = null;
                Log.Error("Fehler");
            }
        }  
          
            var textAusgabeNameAuthor = fispeciesList.FiSpeciesName + " " + textAusgabeAuthor;  
           
            var author = PdfHelper.AuthorViewChangeWithoutString(_fispecies.Author, _fispecies.AuthorYear);

            table.AddCell(new PdfPCell(new Phrase(_fispecies.Tbl66Genusses.GenusName + " " + _fispecies.FiSpeciesName + " " + _fispecies.Subspecies + " " + _fispecies.Divers + " " + author, LargeFont)) { Colspan = 4, Border = 0 });   // 1.-4. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxonomicId + " " + Convert.ToString(_fispecies.CountID), StandardFont)) { Colspan = 4, Border = 0 }); // 1.-4. field
            doc.Add(table);
            return doc;
        }  
                
        private static Document AddTbl69FiSpeciessesTaxoNomenRankList(Document doc)           
          
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbBoldLeft("header2", _arrInts, true, CultRes.StringsRes.ReportTaxoNomen, 2);

            _arrInts[1] += _arrInts[9]; //Distance to next TextBox
            //----------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("kingdomLeft", _arrInts, false, CultRes.StringsRes.FiSpecies + ":", 0);  
          
            _arrInts = PdfHelper.PdfTbRight("kingdomRight", _arrInts, false, fispeciesList.FiSpeciesName, 0);     
                
            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxoRank, StandardBoldFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.FiSpecies, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(table);

            return doc;
        }       
               
        private static void AddRegnumHierarchyList(PdfDocument pdf, Tbl03Regnum regnumList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbBoldLeft("regnumHeader", _arrInts, true, CultRes.StringsRes.ReportTaxoHiera, 2);

            _arrInts[1] += _arrInts[9]; //Distance to next TextBox

            //---------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("regnumLeft", _arrInts, false, CultRes.StringsRes.Regnum, 0);

            var txtName = regnumList.RegnumName + " " + regnumList.Subregnum;

            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, regnumList.Author,
                regnumList.AuthorYear, regnumList.GerName, regnumList.EngName, regnumList.FraName, regnumList.PorName);

            _arrInts = PdfHelper.PdfTbMtRight("regnumRight", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }    
          
        private static void AddFiSpeciesHierarchyList(PdfDocument pdf, Tbl69FiSpecies fiSpeciesList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbMoveLeft("fispeciesLeft", _arrInts, false, CultRes.StringsRes.FiSpecies, 0);     
          
            var txtName = fispeciesList.FiSpeciesName;        
          
            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, fispeciesList.Author,
                fispeciesList.AuthorYear, fispeciesList.GerName, fispeciesList.EngName, fispeciesList.FraName, fispeciesList.PorName);

            _arrInts = PdfHelper.PdfTbMtRight("fispeciesRight", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }      
 
   }
}   
