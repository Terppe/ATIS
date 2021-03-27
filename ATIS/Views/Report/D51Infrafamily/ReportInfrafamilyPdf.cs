using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using ATIS.Ui.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using BitMiracle.Docotic;
using BitMiracle.Docotic.Pdf;
using Microsoft.Win32;


//    ReportInfrafamilyPdf Skriptdatum:  07.01.2021  10:32    

namespace ATIS.Ui.Views.Report.D51Infrafamily
{

    public class ReportInfrafamilyPdf : ViewModelBase
    {

        private static readonly CrudFunctions ExtCrud = new CrudFunctions();
        private static readonly PdfHelper PdfHelper = new PdfHelper();
        private static string _n;
        private static string _z1;
        private static int _z;
        private static int[] _arrInts = new int[11];
        private static PdfPage _page;

        private static Tbl03Regnum _regnumSingleList;
        private static Tbl06Phylum _phylumSingleList;
        private static Tbl09Division _divisionSingleList;
        private static Tbl12Subphylum _subphylumSingleList;
        private static Tbl15Subdivision _subdivisionSingleList;
        private static Tbl18Superclass _superclassSingleList;
        private static Tbl21Class _classSingleList;
        private static Tbl24Subclass _subclassSingleList;
        private static Tbl27Infraclass _infraclassSingleList;
        private static Tbl30Legio _legioSingleList;
        private static Tbl33Ordo _ordoSingleList;
        private static Tbl36Subordo _subordoSingleList;
        private static Tbl39Infraordo _infraordoSingleList;
        private static Tbl42Superfamily _superfamilySingleList;
        private static Tbl45Family _familySingleList;
        private static Tbl48Subfamily _subfamilySingleList;
        private static Tbl51Infrafamily _infrafamilySingleList;


        //    Part 1    


        public static void CreateMainPdf(int id, int fishId, int plantId, string use)

        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            var key = ConfigurationManager.AppSettings["Pdf"];
            LicenseManager.AddLicenseData(key);
            //BitMiracle.Docotic.LicenseManager.AddLicenseData(key);

            //LicenseManager.AddLicenseData("5LX7Z-5GUF6-UUYTR-8YOQC-XGT2B");
            //BitMiracle.Docotic.LicenseManager.AddLicenseData("5LX7Z-5GUF6-UUYTR-8YOQC-XGT2B");
            //-----------------------------------------------------------------------------     

            _infrafamilySingleList = ExtCrud.GetInfrafamilySingleByInfrafamilyId<Tbl51Infrafamily>(id);
            _subfamilySingleList = ExtCrud.GetSubfamilySingleBySubfamilyId<Tbl48Subfamily>(_infrafamilySingleList.SubfamilyId);
            _familySingleList = ExtCrud.GetFamilySingleByFamilyId<Tbl45Family>(_subfamilySingleList.FamilyId);
            _superfamilySingleList = ExtCrud.GetSuperfamilySingleBySuperfamilyId<Tbl42Superfamily>(_familySingleList.SuperfamilyId);
            _infraordoSingleList = ExtCrud.GetInfraordoSingleByInfraordoId<Tbl39Infraordo>(_superfamilySingleList.InfraordoId);
            _subordoSingleList = ExtCrud.GetSubordoSingleBySubordoId<Tbl36Subordo>(_infraordoSingleList.SubordoId);
            _ordoSingleList = ExtCrud.GetOrdoSingleByOrdoId<Tbl33Ordo>(_subordoSingleList.OrdoId);
            _legioSingleList = ExtCrud.GetLegioSingleByLegioId<Tbl30Legio>(_legioSingleList.LegioId);
            _infraclassSingleList = ExtCrud.GetInfraclassSingleByInfraclassId<Tbl27Infraclass>(_legioSingleList.InfraclassId);
            _subclassSingleList = ExtCrud.GetSubclassSingleBySubclassId<Tbl24Subclass>(_infraclassSingleList.SubclassId);
            _classSingleList = ExtCrud.GetClassSingleByClassId<Tbl21Class>(_subclassSingleList.ClassId);
            _superclassSingleList = ExtCrud.GetSuperclassSingleBySuperclassId<Tbl18Superclass>(_classSingleList.SuperclassId);
            if (_superclassSingleList.SubphylumId == fishId)  //Basis #Subphylum#
            {
                _subdivisionSingleList = ExtCrud.GetSubdivisionSingleBySubdivisionId<Tbl15Subdivision>(_superclassSingleList.SubdivisionId);
                _divisionSingleList = ExtCrud.GetDivisionSingleByDivisionId<Tbl09Division>(_subdivisionSingleList.DivisionId);
                _regnumSingleList = ExtCrud.GetRegnumSingleByRegnumId<Tbl03Regnum>(_divisionSingleList.RegnumId);
            }
            if (_superclassSingleList.SubdivisionId == plantId)  //Basis #Subdivision#
            {
                _subphylumSingleList = ExtCrud.GetSubphylumSingleBySubphylumId<Tbl12Subphylum>(_superclassSingleList.SubphylumId);
                _phylumSingleList = ExtCrud.GetPhylumSingleByPhylumId<Tbl06Phylum>(_subphylumSingleList.PhylumId);
                _regnumSingleList = ExtCrud.GetRegnumSingleByRegnumId<Tbl03Regnum>(_phylumSingleList.RegnumId);
            }

            //Children
            var supertribussList = ExtCrud.GetSupertribussesCollectionFromInfrafamilyIdOrderBy<Tbl54Supertribus>(id);

            //------------------------------------------------------   
            var expertsList = ExtCrud.GetReferenceExpertsCollectionFromInfrafamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(id);
            var sourcesList = ExtCrud.GetReferenceSourcesCollectionFromInfrafamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(id);
            var authorsList = ExtCrud.GetReferenceAuthorsCollectionFromInfrafamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(id);
            var commentsList = ExtCrud.GetCommentsCollectionFromInfrafamilyIdOrderBy<Tbl93Comment>(id);

            try
            {

                using var pdf = new PdfDocument();
                _arrInts = PdfHelper.AddReportMain(pdf);

                AddInfrafamilyHaeder(pdf, _infrafamilySingleList);
                AddInfrafamilyTaxoNomenList(pdf, _infrafamilySingleList, _regnumSingleList);

                if (_regnumSingleList != null)
                    AddRegnumHierarchyList(pdf, _regnumSingleList);
                if (_phylumSingleList != null)
                    AddPhylumHierarchyList(pdf, _phylumSingleList);
                if (_divisionSingleList != null)
                    AddDivisionHierarchyList(pdf, _divisionSingleList);
                if (_subphylumSingleList != null)
                    AddSubphylumHierarchyList(pdf, _subphylumSingleList);
                if (_subdivisionSingleList != null)
                    AddSubdivisionHierarchyList(pdf, _subdivisionSingleList);
                if (_superclassSingleList != null)
                    AddSuperclassHierarchyList(pdf, _superclassSingleList);
                if (_classSingleList != null)
                    AddClassHierarchyList(pdf, _classSingleList);
                if (_subclassSingleList != null)
                    AddSubclassHierarchyList(pdf, _subclassSingleList);
                if (_infraclassSingleList != null)
                    AddInfraclassHierarchyList(pdf, _infraclassSingleList);
                if (_legioSingleList != null)
                    AddLegioHierarchyList(pdf, _legioSingleList);
                if (_ordoSingleList != null)
                    AddOrdoHierarchyList(pdf, _ordoSingleList);
                if (_subordoSingleList != null)
                    AddSubordoHierarchyList(pdf, _subordoSingleList);
                if (_infraordoSingleList != null)
                    AddInfraordoHierarchyList(pdf, _infraordoSingleList);
                if (_superfamilySingleList != null)
                    AddSuperfamilyHierarchyList(pdf, _superfamilySingleList);
                if (_familySingleList != null)
                    AddFamilyHierarchyList(pdf, _familySingleList);
                if (_subfamilySingleList != null)
                    AddSubfamilyHierarchyList(pdf, _subfamilySingleList);

                AddInfrafamilyHierarchyList(pdf, _infrafamilySingleList);

                if (supertribussList.Count != 0)
                    AddSupertribussChildrenList(pdf, supertribussList);

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
                            sfd.InitialDirectory = @"C:\Temp\";
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
                SimpleLog.Error(e.Message);
            }
            finally
            {
                // Clean up
                //        if (pdf != null) pdf.Dispose();
                //     doc = null;
                SimpleLog.Error("Fehler");
            }
        }

        private static void AddInfrafamilyHaeder(PdfDocument pdf, Tbl51Infrafamily infrafamilyList)
        {
            _page = pdf.Pages[_arrInts[6]];

            var textAusgabeAuthor = PdfHelper.AuthorViewChangeWithString(infrafamilyList.Author, infrafamilyList.AuthorYear);

            var textAusgabeNameAuthor = infrafamilyList.InfrafamilyName + " " + textAusgabeAuthor;

            _arrInts = PdfHelper.PdfTbBoldLeft("infrafamilyName", _arrInts, true, textAusgabeNameAuthor, 2);

            _arrInts[1] += _arrInts[9]; //Distance to next TextBox

            _arrInts = PdfHelper.PdfTbBoldLeft("countId", _arrInts, false, CultRes.StringsRes.ReportTaxonomicId + infrafamilyList.CountId, 0);

            _arrInts[1] += _arrInts[9] + 5; //Distance to next TextBox
        }

        private static void AddInfrafamilyTaxoNomenList(PdfDocument pdf, Tbl51Infrafamily infrafamilyList, Tbl03Regnum regnumList)

        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbBoldLeft("header2", _arrInts, true, CultRes.StringsRes.ReportTaxoNomen, 2);

            _arrInts[1] += _arrInts[9]; //Distance to next TextBox
            //----------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("kingdomLeft", _arrInts, false, CultRes.StringsRes.Regnum + ":", 0);

            _arrInts = PdfHelper.PdfTbRight("kingdomRight", _arrInts, false, regnumList.RegnumName, 0);

            //---------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("rankLeft", _arrInts, false, CultRes.StringsRes.ReportTaxoRank, 0);
            _arrInts = PdfHelper.PdfTbRight("rankRight", _arrInts, false, CultRes.StringsRes.Infrafamily, 0);
            //------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("synonymLeft", _arrInts, false, CultRes.StringsRes.ReportSynonyms, 0);
            //------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMtRight("synonymRight", _arrInts, infrafamilyList.Synonym);
            _arrInts[1] += _arrInts[9]; //Distance to next TextBox
            //--------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("commNameLeft", _arrInts, false, CultRes.StringsRes.ReportCommonNames, 0);
            //---------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbRight("commNameGerRight", _arrInts, false, infrafamilyList.GerName + " " + CultRes.StringsRes.ReportGerman, 0);
            _arrInts = PdfHelper.PdfTbRight("commNameEngRight", _arrInts, false, infrafamilyList.EngName + " " + CultRes.StringsRes.ReportEnglish, 0);
            _arrInts = PdfHelper.PdfTbRight("commNameFraRight", _arrInts, false, infrafamilyList.FraName + " " + CultRes.StringsRes.ReportFrench, 0);
            _arrInts = PdfHelper.PdfTbRight("commNameSpaRight", _arrInts, false, infrafamilyList.PorName + " " + CultRes.StringsRes.ReportSpanish, 0);
            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox 
            //-------------------------------------------------------
            _arrInts = PdfHelper.PdfTbBoldMoveLeft("status", _arrInts, CultRes.StringsRes.ReportTaxoStatus, 0);
            //---------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("currStatusLeft", _arrInts, false, CultRes.StringsRes.ReportCurrentStand, 0);
            _arrInts = PdfHelper.PdfTbRight("currStatusRight", _arrInts, false, infrafamilyList.Valid.ToString(), 0);
            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox
            //-----------------------------------------------------------
            _arrInts = PdfHelper.PdfTbBoldMoveLeft("quali", _arrInts, CultRes.StringsRes.ReportDataQualiIndicator, 0);
            //---------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("recordLeft", _arrInts, false, CultRes.StringsRes.ReportRecordUpdate, 0);
            _arrInts = PdfHelper.PdfTbRight("recordRight", _arrInts, false, Convert.ToString(infrafamilyList.UpdaterDate, CultureInfo.InvariantCulture), 0);
            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox
            //-------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("infoLeft", _arrInts, false, CultRes.StringsRes.ReportInfo, 0);
            _arrInts = PdfHelper.PdfTbMtRight("infoRight", _arrInts, infrafamilyList.Info);
            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox
            //-------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("memoLeft", _arrInts, false, CultRes.StringsRes.ReportMemo, 0);
            _arrInts = PdfHelper.PdfTbMtRight("memoRight", _arrInts, infrafamilyList.Memo);
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
        private static void AddPhylumHierarchyList(PdfDocument pdf, Tbl06Phylum phylumList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbMoveLeft("phylumLeft", _arrInts, false, CultRes.StringsRes.Phylum, 0);

            var txtName = phylumList.PhylumName;

            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, phylumList.Author,
                phylumList.AuthorYear, phylumList.GerName, phylumList.EngName, phylumList.FraName, phylumList.PorName);

            _arrInts = PdfHelper.PdfTbMtRight("phylumRight", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }
        private static void AddDivisionHierarchyList(PdfDocument pdf, Tbl09Division divisionList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbMoveLeft("divisionLeft", _arrInts, false, CultRes.StringsRes.Division, 0);

            var txtName = divisionList.DivisionName;

            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, divisionList.Author,
                divisionList.AuthorYear, divisionList.GerName, divisionList.EngName, divisionList.FraName, divisionList.PorName);

            _arrInts = PdfHelper.PdfTbMtRight("divisionRight", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }
        private static void AddSubphylumHierarchyList(PdfDocument pdf, Tbl12Subphylum subphylumList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbMoveLeft("subphylumLeft", _arrInts, false, CultRes.StringsRes.Subphylum, 0);

            var txtName = subphylumList.SubphylumName;

            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, subphylumList.Author,
                subphylumList.AuthorYear, subphylumList.GerName, subphylumList.EngName, subphylumList.FraName, subphylumList.PorName);

            _arrInts = PdfHelper.PdfTbMtRight("subphylumRight", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }
        private static void AddSubdivisionHierarchyList(PdfDocument pdf, Tbl15Subdivision subdivisionList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbMoveLeft("subdivisionLeft", _arrInts, false, CultRes.StringsRes.Subdivision, 0);

            var txtName = subdivisionList.SubdivisionName;

            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, subdivisionList.Author,
                subdivisionList.AuthorYear, subdivisionList.GerName, subdivisionList.EngName, subdivisionList.FraName, subdivisionList.PorName);

            _arrInts = PdfHelper.PdfTbMtRight("subdivisionRight", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }
        private static void AddSuperclassHierarchyList(PdfDocument pdf, Tbl18Superclass superclassList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbMoveLeft("superclassLeft", _arrInts, false, CultRes.StringsRes.Superclass, 0);

            var txtName = superclassList.SuperclassName;

            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, superclassList.Author,
                superclassList.AuthorYear, superclassList.GerName, superclassList.EngName, superclassList.FraName, superclassList.PorName);

            _arrInts = PdfHelper.PdfTbMtRight("superclassRight", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }
        private static void AddClassHierarchyList(PdfDocument pdf, Tbl21Class classList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbMoveLeft("classeLeft", _arrInts, false, CultRes.StringsRes.Class, 0);

            var txtName = classList.ClassName;

            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, classList.Author,
                classList.AuthorYear, classList.GerName, classList.EngName, classList.FraName, classList.PorName);

            _arrInts = PdfHelper.PdfTbMtRight("classeRight", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }
        private static void AddSubclassHierarchyList(PdfDocument pdf, Tbl24Subclass subclassList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbMoveLeft("subclassLeft", _arrInts, false, CultRes.StringsRes.Subclass, 0);

            var txtName = subclassList.SubclassName;

            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, subclassList.Author,
                subclassList.AuthorYear, subclassList.GerName, subclassList.EngName, subclassList.FraName, subclassList.PorName);

            _arrInts = PdfHelper.PdfTbMtRight("subclassRight", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }
        private static void AddInfraclassHierarchyList(PdfDocument pdf, Tbl27Infraclass infraclassList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbMoveLeft("infraclassLeft", _arrInts, false, CultRes.StringsRes.Infraclass, 0);

            var txtName = infraclassList.InfraclassName;

            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, infraclassList.Author,
                infraclassList.AuthorYear, infraclassList.GerName, infraclassList.EngName, infraclassList.FraName, infraclassList.PorName);

            _arrInts = PdfHelper.PdfTbMtRight("infraclassRight", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }
        private static void AddLegioHierarchyList(PdfDocument pdf, Tbl30Legio legioList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbMoveLeft("legioLeft", _arrInts, false, CultRes.StringsRes.Legio, 0);

            var txtName = legioList.LegioName;

            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, legioList.Author,
                legioList.AuthorYear, legioList.GerName, legioList.EngName, legioList.FraName, legioList.PorName);

            _arrInts = PdfHelper.PdfTbMtRight("legioRight", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }
        private static void AddOrdoHierarchyList(PdfDocument pdf, Tbl33Ordo ordoList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbMoveLeft("ordoLeft", _arrInts, false, CultRes.StringsRes.Ordo, 0);

            var txtName = ordoList.OrdoName;

            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, ordoList.Author,
                ordoList.AuthorYear, ordoList.GerName, ordoList.EngName, ordoList.FraName, ordoList.PorName);

            _arrInts = PdfHelper.PdfTbMtRight("ordoRight", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }
        private static void AddSubordoHierarchyList(PdfDocument pdf, Tbl36Subordo subordoList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbMoveLeft("subordoLeft", _arrInts, false, CultRes.StringsRes.Subordo, 0);

            var txtName = subordoList.SubordoName;

            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, subordoList.Author,
                subordoList.AuthorYear, subordoList.GerName, subordoList.EngName, subordoList.FraName, subordoList.PorName);

            _arrInts = PdfHelper.PdfTbMtRight("subordoRight", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }
        private static void AddInfraordoHierarchyList(PdfDocument pdf, Tbl39Infraordo infraordoList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbMoveLeft("infraordoLeft", _arrInts, false, CultRes.StringsRes.Infraordo, 0);

            var txtName = infraordoList.InfraordoName;

            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, infraordoList.Author,
                infraordoList.AuthorYear, infraordoList.GerName, infraordoList.EngName, infraordoList.FraName, infraordoList.PorName);

            _arrInts = PdfHelper.PdfTbMtRight("infraordoRight", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }
        private static void AddSuperfamilyHierarchyList(PdfDocument pdf, Tbl42Superfamily superfamilyList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbMoveLeft("superfamilyLeft", _arrInts, false, CultRes.StringsRes.Superfamily, 0);

            var txtName = superfamilyList.SuperfamilyName;

            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, superfamilyList.Author,
                superfamilyList.AuthorYear, superfamilyList.GerName, superfamilyList.EngName, superfamilyList.FraName, superfamilyList.PorName);

            _arrInts = PdfHelper.PdfTbMtRight("superfamilyRight", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }
        private static void AddFamilyHierarchyList(PdfDocument pdf, Tbl45Family familyList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbMoveLeft("familyLeft", _arrInts, false, CultRes.StringsRes.Family, 0);

            var txtName = familyList.FamilyName;

            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, familyList.Author,
                familyList.AuthorYear, familyList.GerName, familyList.EngName, familyList.FraName, familyList.PorName);

            _arrInts = PdfHelper.PdfTbMtRight("familyRight", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }
        private static void AddSubfamilyHierarchyList(PdfDocument pdf, Tbl48Subfamily subfamilyList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbMoveLeft("subfamilyLeft", _arrInts, false, CultRes.StringsRes.Subfamily, 0);

            var txtName = subfamilyList.SubfamilyName;

            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, subfamilyList.Author,
                subfamilyList.AuthorYear, subfamilyList.GerName, subfamilyList.EngName, subfamilyList.FraName, subfamilyList.PorName);

            _arrInts = PdfHelper.PdfTbMtRight("subfamilyRight", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }

        private static void AddInfrafamilyHierarchyList(PdfDocument pdf, Tbl51Infrafamily infrafamilyList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbMoveLeft("infrafamilyLeft", _arrInts, false, CultRes.StringsRes.Infrafamily, 0);

            var txtName = infrafamilyList.InfrafamilyName;

            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, infrafamilyList.Author,
                infrafamilyList.AuthorYear, infrafamilyList.GerName, infrafamilyList.EngName, infrafamilyList.FraName, infrafamilyList.PorName);

            _arrInts = PdfHelper.PdfTbMtRight("infrafamilyRight", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }

        private static void AddSupertribussChildrenList(PdfDocument pdf, ObservableCollection<Tbl54Supertribus> supertribussList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbRight("childrenSupertribus", _arrInts, true, CultRes.StringsRes.ReportDirectChild, 1);

            _arrInts[1] += _arrInts[9] / 2; //Distance to next TextBox

            //------------------------------------------------------------------

            _n = "childSupertribus";
            _z = 1;
            _z1 = _n + _z;
            _arrInts[7] += _arrInts[7];   // move 4+4

            foreach (var t in supertribussList)
            {
                var t1 = t.SupertribusName;
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

                _arrInts = PdfHelper.PdfTbMoveLeft("supertribusLeft" + _z1, _arrInts, false, CultRes.StringsRes.Supertribus, 0);

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
