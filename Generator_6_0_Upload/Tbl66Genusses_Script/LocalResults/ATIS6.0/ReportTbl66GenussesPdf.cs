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

    
         //    ReportGenusPdf Skriptdatum:  12.12.2019  10:32    

namespace ATIS.Ui.Views.Report.ListDetails
{     
    
    public class ReportGenusPdf : ViewModelBase
    {     
         
        private static readonly ILog Log = LogManager.GetLogger(typeof(ReportGenusPdf));
        private static readonly BasicGet ExtGet = new BasicGet();
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

            var genusList = ExtGet.GetGenussCollectionOrderByFromGenusId<Tbl66Genus>(id).FirstOrDefault();    
        
            var fispeciessList = ExtGet.GetFiSpeciessCollectionOrderByFromGenusId<Tbl69FiSpecies>(id);           
             
            var expertsList = ExtGet.GetReferenceExpertsCollectionOrderByFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(id);
            var sourcesList = ExtGet.GetReferenceSourcesCollectionOrderByFromRegnumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(id);
            var authorsList = ExtGet.GetReferenceAuthorsCollectionOrderByFromRegnumIdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(id);
            var commentsList = ExtGet.GetCommentsCollectionOrderByFromRegnumId<Tbl93Comment>(id);   

            try
            { 
         
                using (var pdf = new PdfDocument())
                {
                    _arrInts = PdfHelper.AddReportMain(pdf); 

                    AddGenusHaeder(pdf, genusList);
                    AddGenusTaxoNomenList(pdf, genusList);
                    AddGenusHierarchyList(pdf, genusList);       
          
                        if (fispeciessList.Count != 0)
                        AddFiSpeciessChildrenList(pdf, fispeciessList);      
          
                    if (expertsList.Count != 0 || sourcesList.Count != 0 || authorsList.Count != 0)
                        _arrInts = PdfHelper.AddReferencesHaeder(pdf, _arrInts);

                    if (expertsList.Count != 0)
                        _arrInts = PdfHelper.AddRefExpertsList(pdf, expertsList, _arrInts);
                    if (sourcesList.Count != 0)
                        _arrInts = PdfHelper.AddRefSourcesList(pdf, sourcesList, _arrInts);
                    if (authorsList.Count != 0)
                        _arrInts = PdfHelper.AddRefAuthorsList(pdf, authorsList, _arrInts);

                    if (commentsList.Count != 0)
                        _arrInts = PdfHelper.AddCommentsHaeder(pdf, _arrInts);

                    if (commentsList.Count != 0)
                        _arrInts = PdfHelper.AddCommentsList(pdf, commentsList, _arrInts); 
          
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
             
        private static void AddGenusHaeder(PdfDocument pdf, Tbl66Genus tbl66GenusList)
        {
            _page = pdf.Pages[_arrInts[6]];

            var textAusgabeAuthor = PdfHelper.AuthorViewChangeWithString(genusList.Author, genusList.AuthorYear);    
          
            var textAusgabeNameAuthor = genusList.GenusName + " " + textAusgabeAuthor;  
      
            _arrInts = PdfHelper.PdfTbBoldLeft("regnumName", _arrInts, true, textAusgabeNameAuthor, 2);

            _arrInts[1] += _arrInts[9]; //Distance to next TextBox

            _arrInts = PdfHelper.PdfTbBoldLeft("countId", _arrInts, false, CultRes.StringsRes.ReportTaxonomicId + genusList.CountId, 0);

            _arrInts[1] += _arrInts[9] + 5; //Distance to next TextBox
        } 
          
        private static void AddGenusTaxoNomenList(PdfDocument pdf, Tbl66Genus tbl66GenusList)         
          
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbBoldLeft("header2", _arrInts, true, CultRes.StringsRes.ReportTaxoNomen, 2);

            _arrInts[1] += _arrInts[9]; //Distance to next TextBox
            //----------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("kingdomLeft", _arrInts, false, CultRes.StringsRes.Genus + ":", 0);  
          
            _arrInts = PdfHelper.PdfTbRight("kingdomRight", _arrInts, false, genusList.GenusName, 0);     
          
            //---------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("rankLeft", _arrInts, false, CultRes.StringsRes.ReportTaxoRank, 0);
            _arrInts = PdfHelper.PdfTbRight("rankRight", _arrInts, false, CultRes.StringsRes.Regnum, 0);
            //------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("synonymLeft", _arrInts, false, CultRes.StringsRes.ReportSynonyms, 0);
            //------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMtRight("synonymRight", _arrInts, genusList.Synonym);

            _arrInts[1] += _arrInts[9]; //Distance to next TextBox

            //---------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("commNameLeft", _arrInts, false, CultRes.StringsRes.ReportCommonNames, 0);
            //---------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbRight("commNameGerRight", _arrInts, false, genusList.GerName + " " + CultRes.StringsRes.ReportGerman, 0);
            _arrInts = PdfHelper.PdfTbRight("commNameEngRight", _arrInts, false, genusList.EngName + " " + CultRes.StringsRes.ReportEnglish, 0);
            _arrInts = PdfHelper.PdfTbRight("commNameFraRight", _arrInts, false, genusList.FraName + " " + CultRes.StringsRes.ReportFrench, 0);
            _arrInts = PdfHelper.PdfTbRight("commNameSpaRight", _arrInts, false, genusList.PorName + " " + CultRes.StringsRes.ReportSpanish, 0);

            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox 
            //-------------------------------------------------------
            _arrInts = PdfHelper.PdfTbBoldMoveLeft("status", _arrInts, CultRes.StringsRes.ReportTaxoStatus, 0);
            //---------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("currStatusLeft", _arrInts, false, CultRes.StringsRes.ReportCurrentStand, 0);

            _arrInts = PdfHelper.PdfTbRight("currStatusRight", _arrInts, false, genusList.Valid.ToString(), 0);

            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox

            //-----------------------------------------------------------
            _arrInts = PdfHelper.PdfTbBoldMoveLeft("quali", _arrInts, CultRes.StringsRes.ReportDataQualiIndicator, 0);
            //---------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("recordLeft", _arrInts, false, CultRes.StringsRes.ReportRecordUpdate, 0);

            _arrInts = PdfHelper.PdfTbRight("recordRight", _arrInts, false, Convert.ToString(genusList.UpdaterDate, CultureInfo.InvariantCulture), 0);

            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox

            //-------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("infoLeft", _arrInts, false, CultRes.StringsRes.ReportInfo, 0);

            _arrInts = PdfHelper.PdfTbMtRight("infoRight", _arrInts, genusList.Info);

            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox

            //-------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("memoLeft", _arrInts, false, CultRes.StringsRes.ReportMemo, 0);

            _arrInts = PdfHelper.PdfTbMtRight("memoRight", _arrInts, genusList.Memo);

            _arrInts[1] += _arrInts[9]; //Distance to next TextBox
       }       
          
        private static void AddGenusHierarchyList(PdfDocument pdf, Tbl66Genus tbl66GenusList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbBoldLeft("header3", _arrInts, true, CultRes.StringsRes.ReportTaxoHiera, 2);

            _arrInts[1] += _arrInts[9]; //Distance to next TextBox

            //---------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("genusLeft", _arrInts, false, CultRes.StringsRes.Genus, 0);     
          
            var txtName = genusList.GenusName;        
          
            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, genusList.Author,
                genusList.AuthorYear, genusList.GerName, genusList.EngName, genusList.FraName, genusList.PorName);

            //      _arrInts = PdfHelper.PdfTbRight("genusRight", _arrInts, false, textResult, 0);
            _arrInts = PdfHelper.PdfTbMtRight("genusRight", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }      
 




   }
}   
