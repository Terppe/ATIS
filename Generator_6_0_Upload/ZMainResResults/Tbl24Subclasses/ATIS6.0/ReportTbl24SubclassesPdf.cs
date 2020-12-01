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

    
         //    ReportSubclassPdf Skriptdatum:  13.12.2019  18:32    

namespace ATIS.Ui.Views.Report.ListDetails
{     
    
    public class ReportSubclassPdf : ViewModelBase
    {     
         
        private static readonly ILog Log = LogManager.GetLogger(typeof(ReportSubclassPdf));
        private static readonly BasicGet ExtGet = new BasicGet();
        private static readonly ReportBasicGet ExtReportBasicGet = new ReportBasicGet();
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
        
            var subclassList = ExtGet.GetSubclasssCollectionOrderByFromSubclassId<Tbl24Subclass>(id).FirstOrDefault();    
        
            var infraclasssList = ExtGet.GetInfraclasssCollectionOrderByFromSubclassId<Tbl27Infraclass>(id);           
             
            var expertsList = ExtGet.GetReferenceExpertsCollectionOrderByFromSubclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(id);
            var sourcesList = ExtGet.GetReferenceSourcesCollectionOrderByFromSubclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(id);
            var authorsList = ExtGet.GetReferenceAuthorsCollectionOrderByFromSubclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(id);
            var commentsList = ExtGet.GetCommentsCollectionOrderByFromSubclassId<Tbl93Comment>(id);   

            try
            { 
         
                using (var pdf = new PdfDocument())
                {
                    _arrInts = PdfHelper.AddReportMain(pdf); 

                    AddSubclassHaeder(pdf, subclassList);
                    AddSubclassTaxoNomenList(pdf, subclassList);   
          
                        if (infraclasssList.Count != 0)
                        AddInfraclasssChildrenList(pdf, infraclasssList);      
          
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
             
        private static void AddSubclassHaeder(PdfDocument pdf, Tbl24Subclass tbl24SubclassList)
        {
            _page = pdf.Pages[_arrInts[6]];

            var textAusgabeAuthor = PdfHelper.AuthorViewChangeWithString(subclassList.Author, subclassList.AuthorYear);    
          
            var textAusgabeNameAuthor = subclassList.SubclassName + " " + textAusgabeAuthor;  
      
            _arrInts = PdfHelper.PdfTbBoldLeft("regnumName", _arrInts, true, textAusgabeNameAuthor, 2);

            _arrInts[1] += _arrInts[9]; //Distance to next TextBox

            _arrInts = PdfHelper.PdfTbBoldLeft("countId", _arrInts, false, CultRes.StringsRes.ReportTaxonomicId + subclassList.CountId, 0);

            _arrInts[1] += _arrInts[9] + 5; //Distance to next TextBox
        } 
          
        private static void AddSubclassTaxoNomenList(PdfDocument pdf, Tbl24Subclass tbl24SubclassList)         
          
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbBoldLeft("header2", _arrInts, true, CultRes.StringsRes.ReportTaxoNomen, 2);

            _arrInts[1] += _arrInts[9]; //Distance to next TextBox
            //----------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("kingdomLeft", _arrInts, false, CultRes.StringsRes.Subclass + ":", 0);  
          
            _arrInts = PdfHelper.PdfTbRight("kingdomRight", _arrInts, false, subclassList.SubclassName, 0);     
          
            //---------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("rankLeft", _arrInts, false, CultRes.StringsRes.ReportTaxoRank, 0);
            _arrInts = PdfHelper.PdfTbRight("rankRight", _arrInts, false, CultRes.StringsRes.Regnum, 0);
            //------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("synonymLeft", _arrInts, false, CultRes.StringsRes.ReportSynonyms, 0);
            //------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMtRight("synonymRight", _arrInts, subclassList.Synonym);

            _arrInts[1] += _arrInts[9]; //Distance to next TextBox

            //---------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("commNameLeft", _arrInts, false, CultRes.StringsRes.ReportCommonNames, 0);
            //---------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbRight("commNameGerRight", _arrInts, false, subclassList.GerName + " " + CultRes.StringsRes.ReportGerman, 0);
            _arrInts = PdfHelper.PdfTbRight("commNameEngRight", _arrInts, false, subclassList.EngName + " " + CultRes.StringsRes.ReportEnglish, 0);
            _arrInts = PdfHelper.PdfTbRight("commNameFraRight", _arrInts, false, subclassList.FraName + " " + CultRes.StringsRes.ReportFrench, 0);
            _arrInts = PdfHelper.PdfTbRight("commNameSpaRight", _arrInts, false, subclassList.PorName + " " + CultRes.StringsRes.ReportSpanish, 0);

            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox 
            //-------------------------------------------------------
            _arrInts = PdfHelper.PdfTbBoldMoveLeft("status", _arrInts, CultRes.StringsRes.ReportTaxoStatus, 0);
            //---------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("currStatusLeft", _arrInts, false, CultRes.StringsRes.ReportCurrentStand, 0);

            _arrInts = PdfHelper.PdfTbRight("currStatusRight", _arrInts, false, subclassList.Valid.ToString(), 0);

            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox

            //-----------------------------------------------------------
            _arrInts = PdfHelper.PdfTbBoldMoveLeft("quali", _arrInts, CultRes.StringsRes.ReportDataQualiIndicator, 0);
            //---------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("recordLeft", _arrInts, false, CultRes.StringsRes.ReportRecordUpdate, 0);

            _arrInts = PdfHelper.PdfTbRight("recordRight", _arrInts, false, Convert.ToString(subclassList.UpdaterDate, CultureInfo.InvariantCulture), 0);

            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox

            //-------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("infoLeft", _arrInts, false, CultRes.StringsRes.ReportInfo, 0);

            _arrInts = PdfHelper.PdfTbMtRight("infoRight", _arrInts, subclassList.Info);

            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox

            //-------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("memoLeft", _arrInts, false, CultRes.StringsRes.ReportMemo, 0);

            _arrInts = PdfHelper.PdfTbMtRight("memoRight", _arrInts, subclassList.Memo);

            _arrInts[1] += _arrInts[9]; //Distance to next TextBox
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
          
        private static void AddSubclassHierarchyList(PdfDocument pdf, Tbl24Subclass tbl24SubclassList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbMoveLeft("subclassLeft", _arrInts, false, CultRes.StringsRes.Subclass, 0);     
          
            var txtName = subclassList.SubclassName;        
          
            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, subclassList.Author,
                subclassList.AuthorYear, subclassList.GerName, subclassList.EngName, subclassList.FraName, subclassList.PorName);

            _arrInts = PdfHelper.PdfTbMtRight("subclassRight", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }      
             
        private static void AddInfraclasssChildrenList(PdfDocument pdf, ObservableCollection<Tbl27Infraclass> infraclasssList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbRight("childrenInfraclass", _arrInts, true, CultRes.StringsRes.ReportDirectChild, 1);

            _arrInts[1] += _arrInts[9] / 2; //Distance to next TextBox

            //------------------------------------------------------------------

            _n = "childInfraclass";
            _z = 1;
            _z1 = _n + _z;
            _arrInts[7] += _arrInts[7];   // move 4+4

            foreach (var t in infraclasssList)
            {
                var t1 = t.InfraclassName;
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

                _arrInts = PdfHelper.PdfTbMoveLeft("infraclassLeft" + _z1, _arrInts, false, CultRes.StringsRes.Infraclass, 0);

                var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(t1, t2, t3, t4, t5, t6, t7);

                if (tAllLength >= _arrInts[8])
                {

                    _arrInts = PdfHelper.PdfTbMtRight(_z1, _arrInts, textResult);

                    _arrInts[1] += _arrInts[3] / 2;  // 1/2 Fontheight Leerzeile
                }
                else
                {
                    _arrInts = PdfHelper.PdfTbRight(_z1, _arrInts, false, textResult, 0);
                }

                _z += 1;
                _z1 = _n + _z;
            }
            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox
        }   
 




   }
}   
