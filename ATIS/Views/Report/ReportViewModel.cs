using ATIS.Ui.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Windows.Input;
using ATIS.Ui.Views.Report.D03Regnum;
using ATIS.Ui.Views.Report.D06Phylum;
using ATIS.Ui.Views.Report.D09Division;
using ATIS.Ui.Views.Report.D12Subphylum;
using ATIS.Ui.Views.Report.D15Subdivision;
using ATIS.Ui.Views.Report.D18Superclass;
using BitMiracle.Docotic;

namespace ATIS.Ui.Views.Report
{
    public class ReportViewModel : ViewModelBase
    {
        #region "Private Data Members"
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();
        private readonly ReportBasicGet _extReportBasicGet = new ReportBasicGet();
        private readonly CrudFunctions _extCrud = new CrudFunctions();

        private readonly int _mainId;
        private static int _fishId;
        private static int _plantId;
 
        #endregion "Private Data Members"

        public ReportViewModel(int id, string tab)
        {

            var key = ConfigurationManager.AppSettings["Pdf"];
            LicenseManager.AddLicenseData(key);
            //BitMiracle.Docotic.LicenseManager.AddLicenseData(key);

            //LicenseManager.AddLicenseData("5LX7Z-5GUF6-UUYTR-8YOQC-XGT2B");
            //BitMiracle.Docotic.LicenseManager.AddLicenseData("5LX7Z-5GUF6-UUYTR-8YOQC-XGT2B");

            var plantaeRegnum = _context.Tbl15Subdivisions.FirstOrDefault(e => e.SubdivisionName == "Plantae#Regnum#");
            if (plantaeRegnum != null) _plantId = plantaeRegnum.SubdivisionId;

            var animaliaRegnum = _context.Tbl12Subphylums.FirstOrDefault(e => e.SubphylumName == "Animalia#Regnum#");

            if (animaliaRegnum != null) _fishId = animaliaRegnum.SubphylumId;

            _mainId = id;

            switch (tab)
            {
                case "Tbl03Regnums":
                    GetTbl03RegnumsById(id);
                    break;
                case "Tbl06Phylums":
                    GetTbl06PhylumsById(id);
                    break;
                case "Tbl09Divisions":
                    GetTbl09DivisionsById(id);
                    break;
                case "Tbl12Subphylums":
                    GetTbl12SubphylumsById(id);
                    break;
                case "Tbl15Subdivisions":
                    GetTbl15SubdivisionsById(id);
                    break;
                case "Tbl18Superclasses":
                    GetTbl18SuperclassesById(id);
                    break;
                case "Tbl21Classes":
                    GetTbl21ClassesById(id);
                    break;
                case "Tbl24Subclasses":
                    GetTbl24SubclassesById(id);
                    break;
                case "Tbl27Infraclasses":
                    GetTbl27InfraclassesById(id);
                    break;
                case "Tbl30Legios":
                    GetTbl30LegiosById(id);
                    break;
                case "Tbl33Ordos":
                    GetTbl33OrdosById(id);
                    break;
                case "Tbl36Subordos":
                    GetTbl36SubordosById(id);
                    break;
                case "Tbl39Infraordos":
                    GetTbl39InfraordosById(id);
                    break;
                case "Tbl42Superfamilies":
                    GetTbl42SuperfamiliesById(id);
                    break;
                case "Tbl45Families":
                    GetTbl45FamiliesById(id);
                    break;
                case "Tbl48Subfamilies":
                    GetTbl48SubfamiliesById(id);
                    break;
                case "Tbl51Infrafamilies":
                    GetTbl51InfrafamiliesById(id);
                    break;
                case "Tbl54Supertribusses":
                    GetTbl54SupertribussesById(id);
                    break;
                case "Tbl57Tribusses":
                    GetTbl57TribussesById(id);
                    break;
                case "Tbl60Subtribusses":
                    GetTbl60SubtribussesById(id);
                    break;
                case "Tbl63Infratribusses":
                    GetTbl63InfratribussesById(id);
                    break;
                case "Tbl66Genusses":
                    GetTbl66GenussesById(id);
                    break;
                case "Tbl68Speciesgroups":
                    GetTbl68SpeciesgroupsById(id);
                    break;
                case "Tbl69FiSpeciesses":
                    GetTbl69FiSpeciessesById(id);
                    break;
                case "Tbl72PlSpeciesses":
                    GetTbl72PlSpeciessesById(id);
                    break;
                    //    case "Tbl78Names":
                    //        GetTbl78NamesById(id);
                    //        break;
                    //    case "Tbl84Synonyms":
                    //        GetTbl84SynonymsById(id);
                    //        break;

            }
        }

        public ReportViewModel()
        {

        }

        //		#region Methods

        #region Regnum

        public void GetTbl03RegnumsById(int id)
        {
            RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumId(id);

            //direct children
            PhylumsCollection = _extReportBasicGet.CollPhylumsByRegnumIdAndHash(id);
            DivisionsCollection = _extReportBasicGet.CollDivisionsByRegnumIdAndHash(id);
            //------------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsByRegnumId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesByRegnumId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsByRegnumId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsByRegnumId(id);
        }
        private RelayCommand _pdfRegnumPrintCommand;
        public ICommand PdfRegnumPrintCommand
        {
            get { return _pdfRegnumPrintCommand ??= new RelayCommand(delegate { CreatePdfRegnumPrint(_mainId); }); }
        }
        private static void CreatePdfRegnumPrint(int id)
        {
            const string use = "print";
            ReportRegnumPdf.CreateMainPdf(id, use);
        }
        private RelayCommand _pdfRegnumSaveCommand;
        public ICommand PdfRegnumSaveCommand
        {
            get { return _pdfRegnumSaveCommand ??= new RelayCommand(delegate { CreatePdfRegnumSave(_mainId); }); }
        }
        private static void CreatePdfRegnumSave(int id)
        {
            const string use = "save";
            ReportRegnumPdf.CreateMainPdf(id, use);
        }
        #endregion

        #region Phylum
        public void GetTbl06PhylumsById(int id)
        {
            PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumId(id);

            var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(id);
            RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);

            //direct children
            SubphylumsCollection = _extReportBasicGet.CollSubphylumsByPhylumIdAndHash(id);
            //------------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsByPhylumId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesByPhylumId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsByPhylumId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsByPhylumId(id);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfPhylumPrintCommand;
        public ICommand PdfPhylumPrintCommand
        {
            get { return _pdfPhylumPrintCommand ??= new RelayCommand(delegate { CreatePdfPhylumPrint(_mainId); }); }
        }
        private static void CreatePdfPhylumPrint(int id)
        {
            const string use = "print";
            ReportPhylumPdf.CreateMainPdf(id, use);
        }
        private RelayCommand _pdfPhylumSaveCommand;
        public ICommand PdfPhylumSaveCommand
        {
            get { return _pdfPhylumSaveCommand ??= new RelayCommand(delegate { CreatePdfPhylumSave(_mainId); }); }
        }
        private static void CreatePdfPhylumSave(int id)
        {
            const string use = "save";
            ReportPhylumPdf.CreateMainPdf(id, use);
        }
        #endregion

        #region Division

        public void GetTbl09DivisionsById(int id)
        {
            DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionId(id);
            var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(id);
            RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);

            //direct children
            SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsByDivisionIdAndHash(id);
            //------------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsByDivisionId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesByDivisionId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsByDivisionId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsByDivisionId(id);
        }
        private RelayCommand _pdfDivisionPrintCommand;
        public ICommand PdfDivisionPrintCommand
        {
            get { return _pdfDivisionPrintCommand ??= new RelayCommand(delegate { CreatePdfDivisionPrint(_mainId); }); }
        }
        private static void CreatePdfDivisionPrint(int id)
        {
            const string use = "print";
            ReportDivisionPdf.CreateMainPdf(id, use);
        }
        private RelayCommand _pdfDivisionSaveCommand;
        public ICommand PdfDivisionSaveCommand
        {
            get { return _pdfDivisionSaveCommand ??= new RelayCommand(delegate { CreatePdfDivisionSave(_mainId); }); }
        }

        private static void CreatePdfDivisionSave(int id)
        {
            const string use = "save";
            ReportDivisionPdf.CreateMainPdf(id, use);
        }
        #endregion

        #region Subphylum
        public void GetTbl12SubphylumsById(int id)
        {
            SubphylumsCollection = _extReportBasicGet.CollSubphylumsBySubphylumId(id);

            var phylumId = _extReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(id);
            PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumIdAndHash(phylumId);
            var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);
            RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);

            //direct children
            SuperclassesCollection = _extReportBasicGet.CollSuperclassesBySubphylumIdAndHash(id);
            //-----------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsBySubphylumId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesBySubphylumId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsBySubphylumId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsBySubphylumId(id);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfSubphylumPrintCommand;
        public ICommand PdfSubphylumPrintCommand
        {
            get { return _pdfSubphylumPrintCommand ??= new RelayCommand(delegate { CreatePdfSubphylumPrint(_mainId); }); }
        }
        private static void CreatePdfSubphylumPrint(int id)
        {
            const string use = "print";
            ReportSubphylumPdf.CreateMainPdf(id, use);
        }
        private RelayCommand _pdfSubphylumSaveCommand;
        public ICommand PdfSubphylumSaveCommand
        {
            get { return _pdfSubphylumSaveCommand ??= new RelayCommand(delegate { CreatePdfSubphylumSave(_mainId); }); }
        }
        private static void CreatePdfSubphylumSave(int id)
        {
            const string use = "save";
            ReportSubphylumPdf.CreateMainPdf(id, use);
        }
        #endregion

        #region Subdivision
        public void GetTbl15SubdivisionsById(int id)
        {
            SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsBySubdivisionId(id);

            var divisionId = _extReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(id);
            DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionIdAndHash(divisionId);
            var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);
            RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);

            //direct children
            SuperclassesCollection = _extReportBasicGet.CollSuperclassesBySubdivisionIdAndHash(id);
            //-----------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsBySubdivisionId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesBySubdivisionId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsBySubdivisionId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsBySubdivisionId(id);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfSubdivisionPrintCommand;
        public ICommand PdfSubdivisionPrintCommand
        {
            get { return _pdfSubdivisionPrintCommand ??= new RelayCommand(delegate { CreatePdfSubdivisionPrint(_mainId); }); }
        }
        private static void CreatePdfSubdivisionPrint(int id)
        {
            const string use = "print";
            ReportSubdivisionPdf.CreateMainPdf(id, use);
        }
        private RelayCommand _pdfSubdivisionSaveCommand;
        public ICommand PdfSubdivisionSaveCommand
        {
            get { return _pdfSubdivisionSaveCommand ??= new RelayCommand(delegate { CreatePdfSubdivisionSave(_mainId); }); }
        }
        private static void CreatePdfSubdivisionSave(int id)
        {
            const string use = "save";
            ReportSubdivisionPdf.CreateMainPdf(id, use);
        }
        #endregion

        #region Superclass
        public void GetTbl18SuperclassesById(int id)
        {
            SuperclassesCollection = _extReportBasicGet.CollSuperclassesBySuperclassId(id);
            var subphylumId = _extReportBasicGet.SubphylumIdFromSuperclassesCollectionSelect(id);
            var subdivisionId = _extReportBasicGet.SubdivisionIdFromSuperclassesCollectionSelect(id);

            if (subphylumId == _fishId)  //Basis #Subphylum#
            {
                SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsBySubdivisionIdAndHash(subdivisionId);
                var divisionId = _extReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(subdivisionId);
                DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionIdAndHash(divisionId);
                var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            if (subdivisionId == _plantId)  //Basis #Subdivision#
            {
                SubphylumsCollection = _extReportBasicGet.CollSubphylumsBySubphylumIdAndHash(subphylumId);
                var phylumId = _extReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(subphylumId);
                PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumIdAndHash(phylumId);
                var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }

            //direct children
            ClassesCollection = _extReportBasicGet.CollClassesBySuperclassIdAndHash(id);
            //-----------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsBySuperclassId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesBySuperclassId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsBySuperclassId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsBySuperclassId(id);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfSuperclassPrintCommand;
        public ICommand PdfSuperclassPrintCommand
        {
            get { return _pdfSuperclassPrintCommand ??= new RelayCommand(delegate { CreatePdfSuperclassPrint(_mainId); }); }
        }
        private static void CreatePdfSuperclassPrint(int id)
        {
            const string use = "print";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        private RelayCommand _pdfSuperclassSaveCommand;
        public ICommand PdfSuperclassSaveCommand
        {
            get { return _pdfSuperclassSaveCommand ??= new RelayCommand(delegate { CreatePdfSuperclassSave(_mainId); }); }
        }
        private static void CreatePdfSuperclassSave(int id)
        {
            const string use = "save";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        #endregion

        #region Class
        public void GetTbl21ClassesById(int id)
        {
            ClassesCollection = _extReportBasicGet.CollClassesByClassId(id);
            var superclassId = _extReportBasicGet.SuperclassIdFromClassesCollectionSelect(id);
            SuperclassesCollection = _extReportBasicGet.CollSuperclassesBySuperclassIdAndHash(superclassId);
            var subphylumId = _extReportBasicGet.SubphylumIdFromSuperclassesCollectionSelect(superclassId);
            var subdivisionId = _extReportBasicGet.SubdivisionIdFromSuperclassesCollectionSelect(superclassId);

            if (subphylumId == _fishId)  //Basis #Subphylum#
            {
                SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsBySubdivisionIdAndHash(subdivisionId);
                var divisionId = _extReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(subdivisionId);
                DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionIdAndHash(divisionId);
                var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            if (subdivisionId == _plantId)  //Basis #Subdivision#
            {
                SubphylumsCollection = _extReportBasicGet.CollSubphylumsBySubphylumIdAndHash(subphylumId);
                var phylumId = _extReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(subphylumId);
                PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumIdAndHash(phylumId);
                var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }

            //direct children
            SubclassesCollection = _extReportBasicGet.CollSubclassesByClassIdAndHash(id);
            //-----------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsByClassId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesByClassId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsByClassId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsByClassId(id);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfClassPrintCommand;
        public ICommand PdfClassPrintCommand
        {
            get { return _pdfClassPrintCommand ??= new RelayCommand(delegate { CreatePdfClassPrint(_mainId); }); }
        }
        private static void CreatePdfClassPrint(int id)
        {
            const string use = "print";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        private RelayCommand _pdfClassSaveCommand;
        public ICommand PdfClassSaveCommand
        {
            get { return _pdfClassSaveCommand ??= new RelayCommand(delegate { CreatePdfClassSave(_mainId); }); }
        }
        private static void CreatePdfClassSave(int id)
        {
            const string use = "save";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        #endregion

        #region Subclass
        public void GetTbl24SubclassesById(int id)
        {
            SubclassesCollection = _extReportBasicGet.CollSubclassesBySubclassId(id);
            var classId = _extReportBasicGet.ClassIdFromSubclassesCollectionSelect(id);
            ClassesCollection = _extReportBasicGet.CollClassesByClassIdAndHash(classId);
            var superclassId = _extReportBasicGet.SuperclassIdFromClassesCollectionSelect(classId);
            SuperclassesCollection = _extReportBasicGet.CollSuperclassesBySuperclassIdAndHash(superclassId);
            var subphylumId = _extReportBasicGet.SubphylumIdFromSuperclassesCollectionSelect(superclassId);
            var subdivisionId = _extReportBasicGet.SubdivisionIdFromSuperclassesCollectionSelect(superclassId);

            if (subphylumId == _fishId)  //Basis #Subphylum#
            {
                SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsBySubdivisionIdAndHash(subdivisionId);
                var divisionId = _extReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(subdivisionId);
                DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionIdAndHash(divisionId);
                var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            if (subdivisionId == _plantId)  //Basis #Subdivision#
            {
                SubphylumsCollection = _extReportBasicGet.CollSubphylumsBySubphylumIdAndHash(subphylumId);
                var phylumId = _extReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(subphylumId);
                PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumIdAndHash(phylumId);
                var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }

            //direct children
            InfraclassesCollection = _extReportBasicGet.CollInfraclassesBySubclassIdAndHash(id);
            //-----------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsBySubclassId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesBySubclassId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsBySubclassId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsBySubclassId(id);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfSubclassPrintCommand;
        public ICommand PdfSubclassPrintCommand
        {
            get { return _pdfSubclassPrintCommand ??= new RelayCommand(delegate { CreatePdfSubclassPrint(_mainId); }); }
        }
        private static void CreatePdfSubclassPrint(int id)
        {
            const string use = "print";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        private RelayCommand _pdfSubclassSaveCommand;
        public ICommand PdfSubclassSaveCommand
        {
            get { return _pdfSubclassSaveCommand ??= new RelayCommand(delegate { CreatePdfSubclassSave(_mainId); }); }
        }
        private static void CreatePdfSubclassSave(int id)
        {
            const string use = "save";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        #endregion

        #region Infraclass
        public void GetTbl27InfraclassesById(int id)
        {
            InfraclassesCollection = _extReportBasicGet.CollInfraclassesByInfraclassId(id);
            var subclassId = _extReportBasicGet.SubclassIdFromInfraclassesCollectionSelect(id);
            SubclassesCollection = _extReportBasicGet.CollSubclassesBySubclassIdAndHash(subclassId);
            var classId = _extReportBasicGet.ClassIdFromSubclassesCollectionSelect(subclassId);
            ClassesCollection = _extReportBasicGet.CollClassesByClassIdAndHash(classId);
            var superclassId = _extReportBasicGet.SuperclassIdFromClassesCollectionSelect(classId);
            SuperclassesCollection = _extReportBasicGet.CollSuperclassesBySuperclassIdAndHash(superclassId);
            var subphylumId = _extReportBasicGet.SubphylumIdFromSuperclassesCollectionSelect(superclassId);
            var subdivisionId = _extReportBasicGet.SubdivisionIdFromSuperclassesCollectionSelect(superclassId);

            if (subphylumId == _fishId)  //Basis #Subphylum#
            {
                SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsBySubdivisionIdAndHash(subdivisionId);
                var divisionId = _extReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(subdivisionId);
                DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionIdAndHash(divisionId);
                var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            if (subdivisionId == _plantId)  //Basis #Subdivision#
            {
                SubphylumsCollection = _extReportBasicGet.CollSubphylumsBySubphylumIdAndHash(subphylumId);
                var phylumId = _extReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(subphylumId);
                PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumIdAndHash(phylumId);
                var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            //direct children
            LegiosCollection = _extReportBasicGet.CollLegiosByInfraclassIdAndHash(id);
            //-----------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsByInfraclassId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesByInfraclassId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsByInfraclassId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsByInfraclassId(id);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfInfraclassPrintCommand;
        public ICommand PdfInfraclassPrintCommand
        {
            get { return _pdfInfraclassPrintCommand ??= new RelayCommand(delegate { CreatePdfInfraclassPrint(_mainId); }); }
        }
        private static void CreatePdfInfraclassPrint(int id)
        {
            const string use = "print";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        private RelayCommand _pdfInfraclassSaveCommand;
        public ICommand PdfInfraclassSaveCommand
        {
            get { return _pdfInfraclassSaveCommand ??= new RelayCommand(delegate { CreatePdfInfraclassSave(_mainId); }); }
        }
        private static void CreatePdfInfraclassSave(int id)
        {
            const string use = "save";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        #endregion

        #region Legio
        public void GetTbl30LegiosById(int id)
        {
            LegiosCollection = _extReportBasicGet.CollLegiosByLegioId(id);
            var infraclassId = _extReportBasicGet.InfraclassIdFromLegiosCollectionSelect(id);
            InfraclassesCollection = _extReportBasicGet.CollInfraclassesByInfraclassIdAndHash(infraclassId);
            var subclassId = _extReportBasicGet.SubclassIdFromInfraclassesCollectionSelect(infraclassId);
            SubclassesCollection = _extReportBasicGet.CollSubclassesBySubclassIdAndHash(subclassId);
            var classId = _extReportBasicGet.ClassIdFromSubclassesCollectionSelect(subclassId);
            ClassesCollection = _extReportBasicGet.CollClassesByClassIdAndHash(classId);
            var superclassId = _extReportBasicGet.SuperclassIdFromClassesCollectionSelect(classId);
            SuperclassesCollection = _extReportBasicGet.CollSuperclassesBySuperclassIdAndHash(superclassId);
            var subphylumId = _extReportBasicGet.SubphylumIdFromSuperclassesCollectionSelect(superclassId);
            var subdivisionId = _extReportBasicGet.SubdivisionIdFromSuperclassesCollectionSelect(superclassId);

            if (subphylumId == _fishId)  //Basis #Subphylum#
            {
                SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsBySubdivisionIdAndHash(subdivisionId);
                var divisionId = _extReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(subdivisionId);
                DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionIdAndHash(divisionId);
                var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            if (subdivisionId == _plantId)  //Basis #Subdivision#
            {
                SubphylumsCollection = _extReportBasicGet.CollSubphylumsBySubphylumIdAndHash(subphylumId);
                var phylumId = _extReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(subphylumId);
                PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumIdAndHash(phylumId);
                var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            //direct children
            OrdosCollection = _extReportBasicGet.CollOrdosByLegioIdAndHash(id);
            //-----------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsByLegioId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesByLegioId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsByLegioId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsByLegioId(id);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfLegioPrintCommand;
        public ICommand PdfLegioPrintCommand
        {
            get { return _pdfLegioPrintCommand ??= new RelayCommand(delegate { CreatePdfLegioPrint(_mainId); }); }
        }
        private static void CreatePdfLegioPrint(int id)
        {
            const string use = "print";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        private RelayCommand _pdfLegioSaveCommand;
        public ICommand PdfLegioSaveCommand
        {
            get { return _pdfLegioSaveCommand ??= new RelayCommand(delegate { CreatePdfLegioSave(_mainId); }); }
        }
        private static void CreatePdfLegioSave(int id)
        {
            const string use = "save";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        #endregion

        #region Ordo
        public void GetTbl33OrdosById(int id)
        {
            OrdosCollection = _extReportBasicGet.CollOrdosByOrdoId(id);
            var legioId = _extReportBasicGet.LegioIdFromOrdosCollectionSelect(id);
            LegiosCollection = _extReportBasicGet.CollLegiosByLegioIdAndHash(legioId);
            var infraclassId = _extReportBasicGet.InfraclassIdFromLegiosCollectionSelect(legioId);
            InfraclassesCollection = _extReportBasicGet.CollInfraclassesByInfraclassIdAndHash(infraclassId);
            var subclassId = _extReportBasicGet.SubclassIdFromInfraclassesCollectionSelect(infraclassId);
            SubclassesCollection = _extReportBasicGet.CollSubclassesBySubclassIdAndHash(subclassId);
            var classId = _extReportBasicGet.ClassIdFromSubclassesCollectionSelect(subclassId);
            ClassesCollection = _extReportBasicGet.CollClassesByClassIdAndHash(classId);
            var superclassId = _extReportBasicGet.SuperclassIdFromClassesCollectionSelect(classId);
            SuperclassesCollection = _extReportBasicGet.CollSuperclassesBySuperclassIdAndHash(superclassId);
            var subphylumId = _extReportBasicGet.SubphylumIdFromSuperclassesCollectionSelect(superclassId);
            var subdivisionId = _extReportBasicGet.SubdivisionIdFromSuperclassesCollectionSelect(superclassId);

            if (subphylumId == _fishId)  //Basis #Subphylum#
            {
                SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsBySubdivisionIdAndHash(subdivisionId);
                var divisionId = _extReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(subdivisionId);
                DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionIdAndHash(divisionId);
                var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            if (subdivisionId == _plantId)  //Basis #Subdivision#
            {
                SubphylumsCollection = _extReportBasicGet.CollSubphylumsBySubphylumIdAndHash(subphylumId);
                var phylumId = _extReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(subphylumId);
                PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumIdAndHash(phylumId);
                var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            //direct children
            SubordosCollection = _extReportBasicGet.CollSubordosByOrdoIdAndHash(id);
            //-----------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsByOrdoId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesByOrdoId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsByOrdoId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsByOrdoId(id);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfOrdoPrintCommand;
        public ICommand PdfOrdoPrintCommand
        {
            get { return _pdfOrdoPrintCommand ??= new RelayCommand(delegate { CreatePdfOrdoPrint(_mainId); }); }
        }
        private static void CreatePdfOrdoPrint(int id)
        {
            const string use = "print";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        private RelayCommand _pdfOrdoSaveCommand;
        public ICommand PdfOrdoSaveCommand
        {
            get { return _pdfOrdoSaveCommand ??= new RelayCommand(delegate { CreatePdfOrdoSave(_mainId); }); }
        }
        private static void CreatePdfOrdoSave(int id)
        {
            const string use = "save";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        #endregion

        #region Subordo
        public void GetTbl36SubordosById(int id)
        {
            SubordosCollection = _extReportBasicGet.CollSubordosBySubordoId(id);
            var ordoId = _extReportBasicGet.OrdoIdFromSubordosCollectionSelect(id);
            OrdosCollection = _extReportBasicGet.CollOrdosByOrdoIdAndHash(ordoId);
            var legioId = _extReportBasicGet.LegioIdFromOrdosCollectionSelect(ordoId);
            LegiosCollection = _extReportBasicGet.CollLegiosByLegioIdAndHash(legioId);
            var infraclassId = _extReportBasicGet.InfraclassIdFromLegiosCollectionSelect(legioId);
            InfraclassesCollection = _extReportBasicGet.CollInfraclassesByInfraclassIdAndHash(infraclassId);
            var subclassId = _extReportBasicGet.SubclassIdFromInfraclassesCollectionSelect(infraclassId);
            SubclassesCollection = _extReportBasicGet.CollSubclassesBySubclassIdAndHash(subclassId);
            var classId = _extReportBasicGet.ClassIdFromSubclassesCollectionSelect(subclassId);
            ClassesCollection = _extReportBasicGet.CollClassesByClassIdAndHash(classId);
            var superclassId = _extReportBasicGet.SuperclassIdFromClassesCollectionSelect(classId);
            SuperclassesCollection = _extReportBasicGet.CollSuperclassesBySuperclassIdAndHash(superclassId);
            var subphylumId = _extReportBasicGet.SubphylumIdFromSuperclassesCollectionSelect(superclassId);
            var subdivisionId = _extReportBasicGet.SubdivisionIdFromSuperclassesCollectionSelect(superclassId);

            if (subphylumId == _fishId)  //Basis #Subphylum#
            {
                SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsBySubdivisionIdAndHash(subdivisionId);
                var divisionId = _extReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(subdivisionId);
                DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionIdAndHash(divisionId);
                var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            if (subdivisionId == _plantId)  //Basis #Subdivision#
            {
                SubphylumsCollection = _extReportBasicGet.CollSubphylumsBySubphylumIdAndHash(subphylumId);
                var phylumId = _extReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(subphylumId);
                PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumIdAndHash(phylumId);
                var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            //direct children
            InfraordosCollection = _extReportBasicGet.CollInfraordosBySubordoIdAndHash(id);
            //-----------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsBySubordoId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesBySubordoId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsBySubordoId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsBySubordoId(id);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfSubordoPrintCommand;
        public ICommand PdfSubordoPrintCommand
        {
            get { return _pdfSubordoPrintCommand ??= new RelayCommand(delegate { CreatePdfSubordoPrint(_mainId); }); }
        }
        private static void CreatePdfSubordoPrint(int id)
        {
            const string use = "print";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        private RelayCommand _pdfSubordoSaveCommand;
        public ICommand PdfSubordoSaveCommand
        {
            get { return _pdfSubordoSaveCommand ??= new RelayCommand(delegate { CreatePdfSubordoSave(_mainId); }); }
        }
        private static void CreatePdfSubordoSave(int id)
        {
            const string use = "save";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        #endregion

        #region Infraordo
        public void GetTbl39InfraordosById(int id)
        {
            InfraordosCollection = _extReportBasicGet.CollInfraordosByInfraordoId(id);
            var subordoId = _extReportBasicGet.SubordoIdFromInfraordosCollectionSelect(id);
            SubordosCollection = _extReportBasicGet.CollSubordosBySubordoIdAndHash(subordoId);
            var ordoId = _extReportBasicGet.OrdoIdFromSubordosCollectionSelect(subordoId);
            OrdosCollection = _extReportBasicGet.CollOrdosByOrdoIdAndHash(ordoId);
            var legioId = _extReportBasicGet.LegioIdFromOrdosCollectionSelect(ordoId);
            LegiosCollection = _extReportBasicGet.CollLegiosByLegioIdAndHash(legioId);
            var infraclassId = _extReportBasicGet.InfraclassIdFromLegiosCollectionSelect(legioId);
            InfraclassesCollection = _extReportBasicGet.CollInfraclassesByInfraclassIdAndHash(infraclassId);
            var subclassId = _extReportBasicGet.SubclassIdFromInfraclassesCollectionSelect(infraclassId);
            SubclassesCollection = _extReportBasicGet.CollSubclassesBySubclassIdAndHash(subclassId);
            var classId = _extReportBasicGet.ClassIdFromSubclassesCollectionSelect(subclassId);
            ClassesCollection = _extReportBasicGet.CollClassesByClassIdAndHash(classId);
            var superclassId = _extReportBasicGet.SuperclassIdFromClassesCollectionSelect(classId);
            SuperclassesCollection = _extReportBasicGet.CollSuperclassesBySuperclassIdAndHash(superclassId);
            var subphylumId = _extReportBasicGet.SubphylumIdFromSuperclassesCollectionSelect(superclassId);
            var subdivisionId = _extReportBasicGet.SubdivisionIdFromSuperclassesCollectionSelect(superclassId);

            if (subphylumId == _fishId)  //Basis #Subphylum#
            {
                SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsBySubdivisionIdAndHash(subdivisionId);
                var divisionId = _extReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(subdivisionId);
                DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionIdAndHash(divisionId);
                var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            if (subdivisionId == _plantId)  //Basis #Subdivision#
            {
                SubphylumsCollection = _extReportBasicGet.CollSubphylumsBySubphylumIdAndHash(subphylumId);
                var phylumId = _extReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(subphylumId);
                PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumIdAndHash(phylumId);
                var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            //direct children
            SuperfamiliesCollection = _extReportBasicGet.CollSuperfamiliesByInfraordoIdAndHash(id);
            //-----------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsByInfraordoId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesByInfraordoId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsByInfraordoId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsByInfraordoId(id);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfInfraordoPrintCommand;
        public ICommand PdfInfraordoPrintCommand
        {
            get { return _pdfInfraordoPrintCommand ??= new RelayCommand(delegate { CreatePdfInfraordoPrint(_mainId); }); }
        }
        private static void CreatePdfInfraordoPrint(int id)
        {
            const string use = "print";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        private RelayCommand _pdfInfraordoSaveCommand;
        public ICommand PdfInfraordoSaveCommand
        {
            get { return _pdfInfraordoSaveCommand ??= new RelayCommand(delegate { CreatePdfInfraordoSave(_mainId); }); }
        }
        private static void CreatePdfInfraordoSave(int id)
        {
            const string use = "save";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        #endregion

        #region Superfamily
        public void GetTbl42SuperfamiliesById(int id)
        {
            SuperfamiliesCollection = _extReportBasicGet.CollSuperfamiliesBySuperfamilyId(id);
            var infraordoId = _extReportBasicGet.InfraordoIdFromSuperfamiliesCollectionSelect(id);
            InfraordosCollection = _extReportBasicGet.CollInfraordosByInfraordoIdAndHash(infraordoId);
            var subordoId = _extReportBasicGet.SubordoIdFromInfraordosCollectionSelect(infraordoId);
            SubordosCollection = _extReportBasicGet.CollSubordosBySubordoIdAndHash(subordoId);
            var ordoId = _extReportBasicGet.OrdoIdFromSubordosCollectionSelect(subordoId);
            OrdosCollection = _extReportBasicGet.CollOrdosByOrdoIdAndHash(ordoId);
            var legioId = _extReportBasicGet.LegioIdFromOrdosCollectionSelect(ordoId);
            LegiosCollection = _extReportBasicGet.CollLegiosByLegioIdAndHash(legioId);
            var infraclassId = _extReportBasicGet.InfraclassIdFromLegiosCollectionSelect(legioId);
            InfraclassesCollection = _extReportBasicGet.CollInfraclassesByInfraclassIdAndHash(infraclassId);
            var subclassId = _extReportBasicGet.SubclassIdFromInfraclassesCollectionSelect(infraclassId);
            SubclassesCollection = _extReportBasicGet.CollSubclassesBySubclassIdAndHash(subclassId);
            var classId = _extReportBasicGet.ClassIdFromSubclassesCollectionSelect(subclassId);
            ClassesCollection = _extReportBasicGet.CollClassesByClassIdAndHash(classId);
            var superclassId = _extReportBasicGet.SuperclassIdFromClassesCollectionSelect(classId);
            SuperclassesCollection = _extReportBasicGet.CollSuperclassesBySuperclassIdAndHash(superclassId);
            var subphylumId = _extReportBasicGet.SubphylumIdFromSuperclassesCollectionSelect(superclassId);
            var subdivisionId = _extReportBasicGet.SubdivisionIdFromSuperclassesCollectionSelect(superclassId);

            if (subphylumId == _fishId)  //Basis #Subphylum#
            {
                SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsBySubdivisionIdAndHash(subdivisionId);
                var divisionId = _extReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(subdivisionId);
                DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionIdAndHash(divisionId);
                var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            if (subdivisionId == _plantId)  //Basis #Subdivision#
            {
                SubphylumsCollection = _extReportBasicGet.CollSubphylumsBySubphylumIdAndHash(subphylumId);
                var phylumId = _extReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(subphylumId);
                PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumIdAndHash(phylumId);
                var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            //direct children
            FamiliesCollection = _extReportBasicGet.CollFamiliesBySuperfamilyIdAndHash(id);
            //-----------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsBySuperfamilyId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesBySuperfamilyId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsBySuperfamilyId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsBySuperfamilyId(id);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfSuperfamilyPrintCommand;
        public ICommand PdfSuperfamilyPrintCommand
        {
            get { return _pdfSuperfamilyPrintCommand ??= new RelayCommand(delegate { CreatePdfSuperfamilyPrint(_mainId); }); }
        }
        private static void CreatePdfSuperfamilyPrint(int id)
        {
            const string use = "print";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        private RelayCommand _pdfSuperfamilySaveCommand;
        public ICommand PdfSuperfamilySaveCommand
        {
            get { return _pdfSuperfamilySaveCommand ??= new RelayCommand(delegate { CreatePdfSuperfamilySave(_mainId); }); }
        }
        private static void CreatePdfSuperfamilySave(int id)
        {
            const string use = "save";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        #endregion

        #region Family
        public void GetTbl45FamiliesById(int id)
        {
            FamiliesCollection = _extReportBasicGet.CollFamiliesByFamilyId(id);
            var superfamilyId = _extReportBasicGet.SuperfamilyIdFromFamiliesCollectionSelect(id);
            SuperfamiliesCollection = _extReportBasicGet.CollSuperfamiliesBySuperfamilyIdAndHash(superfamilyId);
            var infraordoId = _extReportBasicGet.InfraordoIdFromSuperfamiliesCollectionSelect(superfamilyId);
            InfraordosCollection = _extReportBasicGet.CollInfraordosByInfraordoIdAndHash(infraordoId);
            var subordoId = _extReportBasicGet.SubordoIdFromInfraordosCollectionSelect(infraordoId);
            SubordosCollection = _extReportBasicGet.CollSubordosBySubordoIdAndHash(subordoId);
            var ordoId = _extReportBasicGet.OrdoIdFromSubordosCollectionSelect(subordoId);
            OrdosCollection = _extReportBasicGet.CollOrdosByOrdoIdAndHash(ordoId);
            var legioId = _extReportBasicGet.LegioIdFromOrdosCollectionSelect(ordoId);
            LegiosCollection = _extReportBasicGet.CollLegiosByLegioIdAndHash(legioId);
            var infraclassId = _extReportBasicGet.InfraclassIdFromLegiosCollectionSelect(legioId);
            InfraclassesCollection = _extReportBasicGet.CollInfraclassesByInfraclassIdAndHash(infraclassId);
            var subclassId = _extReportBasicGet.SubclassIdFromInfraclassesCollectionSelect(infraclassId);
            SubclassesCollection = _extReportBasicGet.CollSubclassesBySubclassIdAndHash(subclassId);
            var classId = _extReportBasicGet.ClassIdFromSubclassesCollectionSelect(subclassId);
            ClassesCollection = _extReportBasicGet.CollClassesByClassIdAndHash(classId);
            var superclassId = _extReportBasicGet.SuperclassIdFromClassesCollectionSelect(classId);
            SuperclassesCollection = _extReportBasicGet.CollSuperclassesBySuperclassIdAndHash(superclassId);
            var subphylumId = _extReportBasicGet.SubphylumIdFromSuperclassesCollectionSelect(superclassId);
            var subdivisionId = _extReportBasicGet.SubdivisionIdFromSuperclassesCollectionSelect(superclassId);

            if (subphylumId == _fishId)  //Basis #Subphylum#
            {
                SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsBySubdivisionIdAndHash(subdivisionId);
                var divisionId = _extReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(subdivisionId);
                DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionIdAndHash(divisionId);
                var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            if (subdivisionId == _plantId)  //Basis #Subdivision#
            {
                SubphylumsCollection = _extReportBasicGet.CollSubphylumsBySubphylumIdAndHash(subphylumId);
                var phylumId = _extReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(subphylumId);
                PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumIdAndHash(phylumId);
                var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            //direct children
            SubfamiliesCollection = _extReportBasicGet.CollSubfamiliesByFamilyIdAndHash(id);
            //-----------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsByFamilyId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesByFamilyId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsByFamilyId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsByFamilyId(id);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfFamilyPrintCommand;
        public ICommand PdfFamilyPrintCommand
        {
            get { return _pdfFamilyPrintCommand ??= new RelayCommand(delegate { CreatePdfFamilyPrint(_mainId); }); }
        }
        private static void CreatePdfFamilyPrint(int id)
        {
            const string use = "print";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        private RelayCommand _pdfFamilySaveCommand;
        public ICommand PdfFamilySaveCommand
        {
            get { return _pdfFamilySaveCommand ??= new RelayCommand(delegate { CreatePdfFamilySave(_mainId); }); }
        }
        private static void CreatePdfFamilySave(int id)
        {
            const string use = "save";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        #endregion

        #region Subfamily
        public void GetTbl48SubfamiliesById(int id)
        {
            SubfamiliesCollection = _extReportBasicGet.CollSubfamiliesBySubfamilyId(id);
            var familyId = _extReportBasicGet.FamilyIdFromSubfamiliesCollectionSelect(id);
            FamiliesCollection = _extReportBasicGet.CollFamiliesByFamilyIdAndHash(familyId);
            var superfamilyId = _extReportBasicGet.SuperfamilyIdFromFamiliesCollectionSelect(familyId);
            SuperfamiliesCollection = _extReportBasicGet.CollSuperfamiliesBySuperfamilyIdAndHash(superfamilyId);
            var infraordoId = _extReportBasicGet.InfraordoIdFromSuperfamiliesCollectionSelect(superfamilyId);
            InfraordosCollection = _extReportBasicGet.CollInfraordosByInfraordoIdAndHash(infraordoId);
            var subordoId = _extReportBasicGet.SubordoIdFromInfraordosCollectionSelect(infraordoId);
            SubordosCollection = _extReportBasicGet.CollSubordosBySubordoIdAndHash(subordoId);
            var ordoId = _extReportBasicGet.OrdoIdFromSubordosCollectionSelect(subordoId);
            OrdosCollection = _extReportBasicGet.CollOrdosByOrdoIdAndHash(ordoId);
            var legioId = _extReportBasicGet.LegioIdFromOrdosCollectionSelect(ordoId);
            LegiosCollection = _extReportBasicGet.CollLegiosByLegioIdAndHash(legioId);
            var infraclassId = _extReportBasicGet.InfraclassIdFromLegiosCollectionSelect(legioId);
            InfraclassesCollection = _extReportBasicGet.CollInfraclassesByInfraclassIdAndHash(infraclassId);
            var subclassId = _extReportBasicGet.SubclassIdFromInfraclassesCollectionSelect(infraclassId);
            SubclassesCollection = _extReportBasicGet.CollSubclassesBySubclassIdAndHash(subclassId);
            var classId = _extReportBasicGet.ClassIdFromSubclassesCollectionSelect(subclassId);
            ClassesCollection = _extReportBasicGet.CollClassesByClassIdAndHash(classId);
            var superclassId = _extReportBasicGet.SuperclassIdFromClassesCollectionSelect(classId);
            SuperclassesCollection = _extReportBasicGet.CollSuperclassesBySuperclassIdAndHash(superclassId);
            var subphylumId = _extReportBasicGet.SubphylumIdFromSuperclassesCollectionSelect(superclassId);
            var subdivisionId = _extReportBasicGet.SubdivisionIdFromSuperclassesCollectionSelect(superclassId);

            if (subphylumId == _fishId)  //Basis #Subphylum#
            {
                SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsBySubdivisionIdAndHash(subdivisionId);
                var divisionId = _extReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(subdivisionId);
                DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionIdAndHash(divisionId);
                var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            if (subdivisionId == _plantId)  //Basis #Subdivision#
            {
                SubphylumsCollection = _extReportBasicGet.CollSubphylumsBySubphylumIdAndHash(subphylumId);
                var phylumId = _extReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(subphylumId);
                PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumIdAndHash(phylumId);
                var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            //direct children
            InfrafamiliesCollection = _extReportBasicGet.CollInfrafamiliesBySubfamilyIdAndHash(id);
            //-----------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsBySubfamilyId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesBySubfamilyId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsBySubfamilyId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsBySubfamilyId(id);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfSubfamilyPrintCommand;
        public ICommand PdfSubfamilyPrintCommand
        {
            get { return _pdfSubfamilyPrintCommand ??= new RelayCommand(delegate { CreatePdfSubfamilyPrint(_mainId); }); }
        }
        private static void CreatePdfSubfamilyPrint(int id)
        {
            const string use = "print";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        private RelayCommand _pdfSubfamilySaveCommand;
        public ICommand PdfSubfamilySaveCommand
        {
            get { return _pdfSubfamilySaveCommand ??= new RelayCommand(delegate { CreatePdfSubfamilySave(_mainId); }); }
        }
        private static void CreatePdfSubfamilySave(int id)
        {
            const string use = "save";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        #endregion

        #region Infrafamily
        public void GetTbl51InfrafamiliesById(int id)
        {
            InfrafamiliesCollection = _extReportBasicGet.CollInfrafamiliesByInfrafamilyId(id);
            var subfamilyId = _extReportBasicGet.SubfamilyIdFromInfrafamiliesCollectionSelect(id);
            SubfamiliesCollection = _extReportBasicGet.CollSubfamiliesBySubfamilyIdAndHash(subfamilyId);
            var familyId = _extReportBasicGet.FamilyIdFromSubfamiliesCollectionSelect(subfamilyId);
            FamiliesCollection = _extReportBasicGet.CollFamiliesByFamilyIdAndHash(familyId);
            var superfamilyId = _extReportBasicGet.SuperfamilyIdFromFamiliesCollectionSelect(familyId);
            SuperfamiliesCollection = _extReportBasicGet.CollSuperfamiliesBySuperfamilyIdAndHash(superfamilyId);
            var infraordoId = _extReportBasicGet.InfraordoIdFromSuperfamiliesCollectionSelect(superfamilyId);
            InfraordosCollection = _extReportBasicGet.CollInfraordosByInfraordoIdAndHash(infraordoId);
            var subordoId = _extReportBasicGet.SubordoIdFromInfraordosCollectionSelect(infraordoId);
            SubordosCollection = _extReportBasicGet.CollSubordosBySubordoIdAndHash(subordoId);
            var ordoId = _extReportBasicGet.OrdoIdFromSubordosCollectionSelect(subordoId);
            OrdosCollection = _extReportBasicGet.CollOrdosByOrdoIdAndHash(ordoId);
            var legioId = _extReportBasicGet.LegioIdFromOrdosCollectionSelect(ordoId);
            LegiosCollection = _extReportBasicGet.CollLegiosByLegioIdAndHash(legioId);
            var infraclassId = _extReportBasicGet.InfraclassIdFromLegiosCollectionSelect(legioId);
            InfraclassesCollection = _extReportBasicGet.CollInfraclassesByInfraclassIdAndHash(infraclassId);
            var subclassId = _extReportBasicGet.SubclassIdFromInfraclassesCollectionSelect(infraclassId);
            SubclassesCollection = _extReportBasicGet.CollSubclassesBySubclassIdAndHash(subclassId);
            var classId = _extReportBasicGet.ClassIdFromSubclassesCollectionSelect(subclassId);
            ClassesCollection = _extReportBasicGet.CollClassesByClassIdAndHash(classId);
            var superclassId = _extReportBasicGet.SuperclassIdFromClassesCollectionSelect(classId);
            SuperclassesCollection = _extReportBasicGet.CollSuperclassesBySuperclassIdAndHash(superclassId);
            var subphylumId = _extReportBasicGet.SubphylumIdFromSuperclassesCollectionSelect(superclassId);
            var subdivisionId = _extReportBasicGet.SubdivisionIdFromSuperclassesCollectionSelect(superclassId);

            if (subphylumId == _fishId)  //Basis #Subphylum#
            {
                SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsBySubdivisionIdAndHash(subdivisionId);
                var divisionId = _extReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(subdivisionId);
                DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionIdAndHash(divisionId);
                var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            if (subdivisionId == _plantId)  //Basis #Subdivision#
            {
                SubphylumsCollection = _extReportBasicGet.CollSubphylumsBySubphylumIdAndHash(subphylumId);
                var phylumId = _extReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(subphylumId);
                PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumIdAndHash(phylumId);
                var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            //direct children
            SupertribussesCollection = _extReportBasicGet.CollSupertribussesByInfrafamilyIdAndHash(id);
            //-----------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsByInfrafamilyId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesByInfrafamilyId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsByInfrafamilyId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsByInfrafamilyId(id);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfInfrafamilyPrintCommand;
        public ICommand PdfInfrafamilyPrintCommand
        {
            get { return _pdfInfrafamilyPrintCommand ??= new RelayCommand(delegate { CreatePdfInfrafamilyPrint(_mainId); }); }
        }
        private static void CreatePdfInfrafamilyPrint(int id)
        {
            const string use = "print";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        private RelayCommand _pdfInfrafamilySaveCommand;
        public ICommand PdfInfrafamilySaveCommand
        {
            get { return _pdfInfrafamilySaveCommand ??= new RelayCommand(delegate { CreatePdfInfrafamilySave(_mainId); }); }
        }
        private static void CreatePdfInfrafamilySave(int id)
        {
            const string use = "save";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        #endregion

        #region Supertribus
        public void GetTbl54SupertribussesById(int id)
        {
            SupertribussesCollection = _extReportBasicGet.CollSupertribussesBySupertribusId(id);
            var infrafamilyId = _extReportBasicGet.InfrafamilyIdFromSupertribussesCollectionSelect(id);
            InfrafamiliesCollection = _extReportBasicGet.CollInfrafamiliesByInfrafamilyIdAndHash(infrafamilyId);
            var subfamilyId = _extReportBasicGet.SubfamilyIdFromInfrafamiliesCollectionSelect(infrafamilyId);
            SubfamiliesCollection = _extReportBasicGet.CollSubfamiliesBySubfamilyIdAndHash(subfamilyId);
            var familyId = _extReportBasicGet.FamilyIdFromSubfamiliesCollectionSelect(subfamilyId);
            FamiliesCollection = _extReportBasicGet.CollFamiliesByFamilyIdAndHash(familyId);
            var superfamilyId = _extReportBasicGet.SuperfamilyIdFromFamiliesCollectionSelect(familyId);
            SuperfamiliesCollection = _extReportBasicGet.CollSuperfamiliesBySuperfamilyIdAndHash(superfamilyId);
            var infraordoId = _extReportBasicGet.InfraordoIdFromSuperfamiliesCollectionSelect(superfamilyId);
            InfraordosCollection = _extReportBasicGet.CollInfraordosByInfraordoIdAndHash(infraordoId);
            var subordoId = _extReportBasicGet.SubordoIdFromInfraordosCollectionSelect(infraordoId);
            SubordosCollection = _extReportBasicGet.CollSubordosBySubordoIdAndHash(subordoId);
            var ordoId = _extReportBasicGet.OrdoIdFromSubordosCollectionSelect(subordoId);
            OrdosCollection = _extReportBasicGet.CollOrdosByOrdoIdAndHash(ordoId);
            var legioId = _extReportBasicGet.LegioIdFromOrdosCollectionSelect(ordoId);
            LegiosCollection = _extReportBasicGet.CollLegiosByLegioIdAndHash(legioId);
            var infraclassId = _extReportBasicGet.InfraclassIdFromLegiosCollectionSelect(legioId);
            InfraclassesCollection = _extReportBasicGet.CollInfraclassesByInfraclassIdAndHash(infraclassId);
            var subclassId = _extReportBasicGet.SubclassIdFromInfraclassesCollectionSelect(infraclassId);
            SubclassesCollection = _extReportBasicGet.CollSubclassesBySubclassIdAndHash(subclassId);
            var classId = _extReportBasicGet.ClassIdFromSubclassesCollectionSelect(subclassId);
            ClassesCollection = _extReportBasicGet.CollClassesByClassIdAndHash(classId);
            var superclassId = _extReportBasicGet.SuperclassIdFromClassesCollectionSelect(classId);
            SuperclassesCollection = _extReportBasicGet.CollSuperclassesBySuperclassIdAndHash(superclassId);
            var subphylumId = _extReportBasicGet.SubphylumIdFromSuperclassesCollectionSelect(superclassId);
            var subdivisionId = _extReportBasicGet.SubdivisionIdFromSuperclassesCollectionSelect(superclassId);

            if (subphylumId == _fishId)  //Basis #Subphylum#
            {
                SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsBySubdivisionIdAndHash(subdivisionId);
                var divisionId = _extReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(subdivisionId);
                DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionIdAndHash(divisionId);
                var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            if (subdivisionId == _plantId)  //Basis #Subdivision#
            {
                SubphylumsCollection = _extReportBasicGet.CollSubphylumsBySubphylumIdAndHash(subphylumId);
                var phylumId = _extReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(subphylumId);
                PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumIdAndHash(phylumId);
                var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            //direct children
            TribussesCollection = _extReportBasicGet.CollTribussesBySupertribusIdAndHash(id);
            //-----------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsBySupertribusId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesBySupertribusId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsBySupertribusId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsBySupertribusId(id);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfSupertribusPrintCommand;
        public ICommand PdfSupertribusPrintCommand
        {
            get { return _pdfSupertribusPrintCommand ??= new RelayCommand(delegate { CreatePdfSupertribusPrint(_mainId); }); }
        }
        private static void CreatePdfSupertribusPrint(int id)
        {
            const string use = "print";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        private RelayCommand _pdfSupertribusSaveCommand;
        public ICommand PdfSupertribusSaveCommand
        {
            get { return _pdfSupertribusSaveCommand ??= new RelayCommand(delegate { CreatePdfSupertribusSave(_mainId); }); }
        }
        private static void CreatePdfSupertribusSave(int id)
        {
            const string use = "save";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        #endregion

        #region Tribus
        public void GetTbl57TribussesById(int id)
        {
            TribussesCollection = _extReportBasicGet.CollTribussesByTribusId(id);
            var supertribusId = _extReportBasicGet.SupertribusIdFromTribussesCollectionSelect(id);
            SupertribussesCollection = _extReportBasicGet.CollSupertribussesBySupertribusIdAndHash(supertribusId);
            var infrafamilyId = _extReportBasicGet.InfrafamilyIdFromSupertribussesCollectionSelect(supertribusId);
            InfrafamiliesCollection = _extReportBasicGet.CollInfrafamiliesByInfrafamilyIdAndHash(infrafamilyId);
            var subfamilyId = _extReportBasicGet.SubfamilyIdFromInfrafamiliesCollectionSelect(infrafamilyId);
            SubfamiliesCollection = _extReportBasicGet.CollSubfamiliesBySubfamilyIdAndHash(subfamilyId);
            var familyId = _extReportBasicGet.FamilyIdFromSubfamiliesCollectionSelect(subfamilyId);
            FamiliesCollection = _extReportBasicGet.CollFamiliesByFamilyIdAndHash(familyId);
            var superfamilyId = _extReportBasicGet.SuperfamilyIdFromFamiliesCollectionSelect(familyId);
            SuperfamiliesCollection = _extReportBasicGet.CollSuperfamiliesBySuperfamilyIdAndHash(superfamilyId);
            var infraordoId = _extReportBasicGet.InfraordoIdFromSuperfamiliesCollectionSelect(superfamilyId);
            InfraordosCollection = _extReportBasicGet.CollInfraordosByInfraordoIdAndHash(infraordoId);
            var subordoId = _extReportBasicGet.SubordoIdFromInfraordosCollectionSelect(infraordoId);
            SubordosCollection = _extReportBasicGet.CollSubordosBySubordoIdAndHash(subordoId);
            var ordoId = _extReportBasicGet.OrdoIdFromSubordosCollectionSelect(subordoId);
            OrdosCollection = _extReportBasicGet.CollOrdosByOrdoIdAndHash(ordoId);
            var legioId = _extReportBasicGet.LegioIdFromOrdosCollectionSelect(ordoId);
            LegiosCollection = _extReportBasicGet.CollLegiosByLegioIdAndHash(legioId);
            var infraclassId = _extReportBasicGet.InfraclassIdFromLegiosCollectionSelect(legioId);
            InfraclassesCollection = _extReportBasicGet.CollInfraclassesByInfraclassIdAndHash(infraclassId);
            var subclassId = _extReportBasicGet.SubclassIdFromInfraclassesCollectionSelect(infraclassId);
            SubclassesCollection = _extReportBasicGet.CollSubclassesBySubclassIdAndHash(subclassId);
            var classId = _extReportBasicGet.ClassIdFromSubclassesCollectionSelect(subclassId);
            ClassesCollection = _extReportBasicGet.CollClassesByClassIdAndHash(classId);
            var superclassId = _extReportBasicGet.SuperclassIdFromClassesCollectionSelect(classId);
            SuperclassesCollection = _extReportBasicGet.CollSuperclassesBySuperclassIdAndHash(superclassId);
            var subphylumId = _extReportBasicGet.SubphylumIdFromSuperclassesCollectionSelect(superclassId);
            var subdivisionId = _extReportBasicGet.SubdivisionIdFromSuperclassesCollectionSelect(superclassId);

            if (subphylumId == _fishId)  //Basis #Subphylum#
            {
                SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsBySubdivisionIdAndHash(subdivisionId);
                var divisionId = _extReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(subdivisionId);
                DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionIdAndHash(divisionId);
                var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            if (subdivisionId == _plantId)  //Basis #Subdivision#
            {
                SubphylumsCollection = _extReportBasicGet.CollSubphylumsBySubphylumIdAndHash(subphylumId);
                var phylumId = _extReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(subphylumId);
                PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumIdAndHash(phylumId);
                var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            //direct children
            SubtribussesCollection = _extReportBasicGet.CollSubtribussesByTribusIdAndHash(id);
            //-----------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsByTribusId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesByTribusId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsByTribusId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsByTribusId(id);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfTribusPrintCommand;
        public ICommand PdfTribusPrintCommand
        {
            get { return _pdfTribusPrintCommand ??= new RelayCommand(delegate { CreatePdfTribusPrint(_mainId); }); }
        }
        private static void CreatePdfTribusPrint(int id)
        {
            const string use = "print";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        private RelayCommand _pdfTribusSaveCommand;
        public ICommand PdfTribusSaveCommand
        {
            get { return _pdfTribusSaveCommand ??= new RelayCommand(delegate { CreatePdfTribusSave(_mainId); }); }
        }
        private static void CreatePdfTribusSave(int id)
        {
            const string use = "save";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        #endregion

        #region Subtribus
        public void GetTbl60SubtribussesById(int id)
        {
            SubtribussesCollection = _extReportBasicGet.CollSubtribussesBySubtribusId(id);
            var tribusId = _extReportBasicGet.TribusIdFromSubtribussesCollectionSelect(id);
            TribussesCollection = _extReportBasicGet.CollTribussesByTribusIdAndHash(tribusId);
            var supertribusId = _extReportBasicGet.SupertribusIdFromTribussesCollectionSelect(tribusId);
            SupertribussesCollection = _extReportBasicGet.CollSupertribussesBySupertribusIdAndHash(supertribusId);
            var infrafamilyId = _extReportBasicGet.InfrafamilyIdFromSupertribussesCollectionSelect(supertribusId);
            InfrafamiliesCollection = _extReportBasicGet.CollInfrafamiliesByInfrafamilyIdAndHash(infrafamilyId);
            var subfamilyId = _extReportBasicGet.SubfamilyIdFromInfrafamiliesCollectionSelect(infrafamilyId);
            SubfamiliesCollection = _extReportBasicGet.CollSubfamiliesBySubfamilyIdAndHash(subfamilyId);
            var familyId = _extReportBasicGet.FamilyIdFromSubfamiliesCollectionSelect(subfamilyId);
            FamiliesCollection = _extReportBasicGet.CollFamiliesByFamilyIdAndHash(familyId);
            var superfamilyId = _extReportBasicGet.SuperfamilyIdFromFamiliesCollectionSelect(familyId);
            SuperfamiliesCollection = _extReportBasicGet.CollSuperfamiliesBySuperfamilyIdAndHash(superfamilyId);
            var infraordoId = _extReportBasicGet.InfraordoIdFromSuperfamiliesCollectionSelect(superfamilyId);
            InfraordosCollection = _extReportBasicGet.CollInfraordosByInfraordoIdAndHash(infraordoId);
            var subordoId = _extReportBasicGet.SubordoIdFromInfraordosCollectionSelect(infraordoId);
            SubordosCollection = _extReportBasicGet.CollSubordosBySubordoIdAndHash(subordoId);
            var ordoId = _extReportBasicGet.OrdoIdFromSubordosCollectionSelect(subordoId);
            OrdosCollection = _extReportBasicGet.CollOrdosByOrdoIdAndHash(ordoId);
            var legioId = _extReportBasicGet.LegioIdFromOrdosCollectionSelect(ordoId);
            LegiosCollection = _extReportBasicGet.CollLegiosByLegioIdAndHash(legioId);
            var infraclassId = _extReportBasicGet.InfraclassIdFromLegiosCollectionSelect(legioId);
            InfraclassesCollection = _extReportBasicGet.CollInfraclassesByInfraclassIdAndHash(infraclassId);
            var subclassId = _extReportBasicGet.SubclassIdFromInfraclassesCollectionSelect(infraclassId);
            SubclassesCollection = _extReportBasicGet.CollSubclassesBySubclassIdAndHash(subclassId);
            var classId = _extReportBasicGet.ClassIdFromSubclassesCollectionSelect(subclassId);
            ClassesCollection = _extReportBasicGet.CollClassesByClassIdAndHash(classId);
            var superclassId = _extReportBasicGet.SuperclassIdFromClassesCollectionSelect(classId);
            SuperclassesCollection = _extReportBasicGet.CollSuperclassesBySuperclassIdAndHash(superclassId);
            var subphylumId = _extReportBasicGet.SubphylumIdFromSuperclassesCollectionSelect(superclassId);
            var subdivisionId = _extReportBasicGet.SubdivisionIdFromSuperclassesCollectionSelect(superclassId);

            if (subphylumId == _fishId)  //Basis #Subphylum#
            {
                SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsBySubdivisionIdAndHash(subdivisionId);
                var divisionId = _extReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(subdivisionId);
                DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionIdAndHash(divisionId);
                var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            if (subdivisionId == _plantId)  //Basis #Subdivision#
            {
                SubphylumsCollection = _extReportBasicGet.CollSubphylumsBySubphylumIdAndHash(subphylumId);
                var phylumId = _extReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(subphylumId);
                PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumIdAndHash(phylumId);
                var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            //direct children
            InfratribussesCollection = _extReportBasicGet.CollInfratribussesBySubtribusIdAndHash(id);
            //-----------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsBySubtribusId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesBySubtribusId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsBySubtribusId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsBySubtribusId(id);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfSubtribusPrintCommand;
        public ICommand PdfSubtribusPrintCommand
        {
            get { return _pdfSubtribusPrintCommand ??= new RelayCommand(delegate { CreatePdfSubtribusPrint(_mainId); }); }
        }
        private static void CreatePdfSubtribusPrint(int id)
        {
            const string use = "print";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        private RelayCommand _pdfSubtribusSaveCommand;
        public ICommand PdfSubtribusSaveCommand
        {
            get { return _pdfSubtribusSaveCommand ??= new RelayCommand(delegate { CreatePdfSubtribusSave(_mainId); }); }
        }
        private static void CreatePdfSubtribusSave(int id)
        {
            const string use = "save";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        #endregion

        #region Infratribus
        public void GetTbl63InfratribussesById(int id)
        {
            InfratribussesCollection = _extReportBasicGet.CollInfratribussesByInfratribusId(id);
            var subtribusId = _extReportBasicGet.SubtribusIdFromInfratribussesCollectionSelect(id);
            SubtribussesCollection = _extReportBasicGet.CollSubtribussesBySubtribusIdAndHash(subtribusId);
            var tribusId = _extReportBasicGet.TribusIdFromSubtribussesCollectionSelect(subtribusId);
            TribussesCollection = _extReportBasicGet.CollTribussesByTribusIdAndHash(tribusId);
            var supertribusId = _extReportBasicGet.SupertribusIdFromTribussesCollectionSelect(tribusId);
            SupertribussesCollection = _extReportBasicGet.CollSupertribussesBySupertribusIdAndHash(supertribusId);
            var infrafamilyId = _extReportBasicGet.InfrafamilyIdFromSupertribussesCollectionSelect(supertribusId);
            InfrafamiliesCollection = _extReportBasicGet.CollInfrafamiliesByInfrafamilyIdAndHash(infrafamilyId);
            var subfamilyId = _extReportBasicGet.SubfamilyIdFromInfrafamiliesCollectionSelect(infrafamilyId);
            SubfamiliesCollection = _extReportBasicGet.CollSubfamiliesBySubfamilyIdAndHash(subfamilyId);
            var familyId = _extReportBasicGet.FamilyIdFromSubfamiliesCollectionSelect(subfamilyId);
            FamiliesCollection = _extReportBasicGet.CollFamiliesByFamilyIdAndHash(familyId);
            var superfamilyId = _extReportBasicGet.SuperfamilyIdFromFamiliesCollectionSelect(familyId);
            SuperfamiliesCollection = _extReportBasicGet.CollSuperfamiliesBySuperfamilyIdAndHash(superfamilyId);
            var infraordoId = _extReportBasicGet.InfraordoIdFromSuperfamiliesCollectionSelect(superfamilyId);
            InfraordosCollection = _extReportBasicGet.CollInfraordosByInfraordoIdAndHash(infraordoId);
            var subordoId = _extReportBasicGet.SubordoIdFromInfraordosCollectionSelect(infraordoId);
            SubordosCollection = _extReportBasicGet.CollSubordosBySubordoIdAndHash(subordoId);
            var ordoId = _extReportBasicGet.OrdoIdFromSubordosCollectionSelect(subordoId);
            OrdosCollection = _extReportBasicGet.CollOrdosByOrdoIdAndHash(ordoId);
            var legioId = _extReportBasicGet.LegioIdFromOrdosCollectionSelect(ordoId);
            LegiosCollection = _extReportBasicGet.CollLegiosByLegioIdAndHash(legioId);
            var infraclassId = _extReportBasicGet.InfraclassIdFromLegiosCollectionSelect(legioId);
            InfraclassesCollection = _extReportBasicGet.CollInfraclassesByInfraclassIdAndHash(infraclassId);
            var subclassId = _extReportBasicGet.SubclassIdFromInfraclassesCollectionSelect(infraclassId);
            SubclassesCollection = _extReportBasicGet.CollSubclassesBySubclassIdAndHash(subclassId);
            var classId = _extReportBasicGet.ClassIdFromSubclassesCollectionSelect(subclassId);
            ClassesCollection = _extReportBasicGet.CollClassesByClassIdAndHash(classId);
            var superclassId = _extReportBasicGet.SuperclassIdFromClassesCollectionSelect(classId);
            SuperclassesCollection = _extReportBasicGet.CollSuperclassesBySuperclassIdAndHash(superclassId);
            var subphylumId = _extReportBasicGet.SubphylumIdFromSuperclassesCollectionSelect(superclassId);
            var subdivisionId = _extReportBasicGet.SubdivisionIdFromSuperclassesCollectionSelect(superclassId);

            if (subphylumId == _fishId)  //Basis #Subphylum#
            {
                SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsBySubdivisionIdAndHash(subdivisionId);
                var divisionId = _extReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(subdivisionId);
                DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionIdAndHash(divisionId);
                var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            if (subdivisionId == _plantId)  //Basis #Subdivision#
            {
                SubphylumsCollection = _extReportBasicGet.CollSubphylumsBySubphylumIdAndHash(subphylumId);
                var phylumId = _extReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(subphylumId);
                PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumIdAndHash(phylumId);
                var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            //direct children
            GenussesCollection = _extReportBasicGet.CollGenussesByInfratribusIdAndHash(id);
            //-----------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsByInfratribusId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesByInfratribusId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsByInfratribusId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsByInfratribusId(id);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfInfratribusPrintCommand;
        public ICommand PdfInfratribusPrintCommand
        {
            get { return _pdfInfratribusPrintCommand ??= new RelayCommand(delegate { CreatePdfInfratribusPrint(_mainId); }); }
        }
        private static void CreatePdfInfratribusPrint(int id)
        {
            const string use = "print";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        private RelayCommand _pdfInfratribusSaveCommand;
        public ICommand PdfInfratribusSaveCommand
        {
            get { return _pdfInfratribusSaveCommand ??= new RelayCommand(delegate { CreatePdfInfratribusSave(_mainId); }); }
        }
        private static void CreatePdfInfratribusSave(int id)
        {
            const string use = "save";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        #endregion

        #region Genus
        public void GetTbl66GenussesById(int id)
        {
            GenussesCollection = _extReportBasicGet.CollGenussesByGenusId(id);
            var infratribusId = _extReportBasicGet.InfratribusIdFromGenussesCollectionSelect(id);
            InfratribussesCollection = _extReportBasicGet.CollInfratribussesByInfratribusIdAndHash(infratribusId);
            var subtribusId = _extReportBasicGet.SubtribusIdFromInfratribussesCollectionSelect(infratribusId);
            SubtribussesCollection = _extReportBasicGet.CollSubtribussesBySubtribusIdAndHash(subtribusId);
            var tribusId = _extReportBasicGet.TribusIdFromSubtribussesCollectionSelect(subtribusId);
            TribussesCollection = _extReportBasicGet.CollTribussesByTribusIdAndHash(tribusId);
            var supertribusId = _extReportBasicGet.SupertribusIdFromTribussesCollectionSelect(tribusId);
            SupertribussesCollection = _extReportBasicGet.CollSupertribussesBySupertribusIdAndHash(supertribusId);
            var infrafamilyId = _extReportBasicGet.InfrafamilyIdFromSupertribussesCollectionSelect(supertribusId);
            InfrafamiliesCollection = _extReportBasicGet.CollInfrafamiliesByInfrafamilyIdAndHash(infrafamilyId);
            var subfamilyId = _extReportBasicGet.SubfamilyIdFromInfrafamiliesCollectionSelect(infrafamilyId);
            SubfamiliesCollection = _extReportBasicGet.CollSubfamiliesBySubfamilyIdAndHash(subfamilyId);
            var familyId = _extReportBasicGet.FamilyIdFromSubfamiliesCollectionSelect(subfamilyId);
            FamiliesCollection = _extReportBasicGet.CollFamiliesByFamilyIdAndHash(familyId);
            var superfamilyId = _extReportBasicGet.SuperfamilyIdFromFamiliesCollectionSelect(familyId);
            SuperfamiliesCollection = _extReportBasicGet.CollSuperfamiliesBySuperfamilyIdAndHash(superfamilyId);
            var infraordoId = _extReportBasicGet.InfraordoIdFromSuperfamiliesCollectionSelect(superfamilyId);
            InfraordosCollection = _extReportBasicGet.CollInfraordosByInfraordoIdAndHash(infraordoId);
            var subordoId = _extReportBasicGet.SubordoIdFromInfraordosCollectionSelect(infraordoId);
            SubordosCollection = _extReportBasicGet.CollSubordosBySubordoIdAndHash(subordoId);
            var ordoId = _extReportBasicGet.OrdoIdFromSubordosCollectionSelect(subordoId);
            OrdosCollection = _extReportBasicGet.CollOrdosByOrdoIdAndHash(ordoId);
            var legioId = _extReportBasicGet.LegioIdFromOrdosCollectionSelect(ordoId);
            LegiosCollection = _extReportBasicGet.CollLegiosByLegioIdAndHash(legioId);
            var infraclassId = _extReportBasicGet.InfraclassIdFromLegiosCollectionSelect(legioId);
            InfraclassesCollection = _extReportBasicGet.CollInfraclassesByInfraclassIdAndHash(infraclassId);
            var subclassId = _extReportBasicGet.SubclassIdFromInfraclassesCollectionSelect(infraclassId);
            SubclassesCollection = _extReportBasicGet.CollSubclassesBySubclassIdAndHash(subclassId);
            var classId = _extReportBasicGet.ClassIdFromSubclassesCollectionSelect(subclassId);
            ClassesCollection = _extReportBasicGet.CollClassesByClassIdAndHash(classId);
            var superclassId = _extReportBasicGet.SuperclassIdFromClassesCollectionSelect(classId);
            SuperclassesCollection = _extReportBasicGet.CollSuperclassesBySuperclassIdAndHash(superclassId);
            var subphylumId = _extReportBasicGet.SubphylumIdFromSuperclassesCollectionSelect(superclassId);
            var subdivisionId = _extReportBasicGet.SubdivisionIdFromSuperclassesCollectionSelect(superclassId);

            if (subphylumId == _fishId)  //Basis #Subphylum#
            {
                SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsBySubdivisionIdAndHash(subdivisionId);
                var divisionId = _extReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(subdivisionId);
                DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionIdAndHash(divisionId);
                var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            if (subdivisionId == _plantId)  //Basis #Subdivision#
            {
                SubphylumsCollection = _extReportBasicGet.CollSubphylumsBySubphylumIdAndHash(subphylumId);
                var phylumId = _extReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(subphylumId);
                PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumIdAndHash(phylumId);
                var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            //direct children
            FiSpeciessesCollection = _extReportBasicGet.CollFiSpeciessesByGenusIdAndHash(id);
            //-----------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsByGenusId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesByGenusId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsByGenusId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsByGenusId(id);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfGenusPrintCommand;
        public ICommand PdfGenusPrintCommand
        {
            get { return _pdfGenusPrintCommand ??= new RelayCommand(delegate { CreatePdfGenusPrint(_mainId); }); }
        }
        private static void CreatePdfGenusPrint(int id)
        {
            const string use = "print";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        private RelayCommand _pdfGenusSaveCommand;
        public ICommand PdfGenusSaveCommand
        {
            get { return _pdfGenusSaveCommand ??= new RelayCommand(delegate { CreatePdfGenusSave(_mainId); }); }
        }
        private static void CreatePdfGenusSave(int id)
        {
            const string use = "save";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        #endregion

        #region Speciesgroup

        private void GetTbl68SpeciesgroupsById(int id)
        {

        }

        #endregion

        #region FiSpecies
        public void GetTbl69FiSpeciessesById(int id)
        {
            var fiSpecies = _extReportBasicGet.GetFiSpeciesSingleByFiSpeciesId(id);
            
            //direct children
            //special FiSpeciessesSubList with Subspecies empty  
            //     if (fiSpecies.Subspecies.IsNullOrEmpty())
            if (string.IsNullOrEmpty(fiSpecies.Subspecies))
            {
                FiSpeciessesCollection = _extReportBasicGet.CollFiSpeciessesByFiSpeciesId(id);
                FiSpeciessesSubCollection = _extReportBasicGet.CollFiSpeciessesByFiSpeciesNameAndNotEmptySubspeciesAndHash(fiSpecies.FiSpeciesName);
                FiSpeciessesFiSpeciesNameCollection = new ObservableCollection<Tbl69FiSpecies>();
            }
            else
            {
                FiSpeciessesFiSpeciesNameCollection = _extReportBasicGet.CollFiSpeciessesByFiSpeciesNameAndSubspeciesAndDivers(fiSpecies.FiSpeciesName, fiSpecies.Subspecies, fiSpecies.Divers);
                FiSpeciessesCollection = _extReportBasicGet.CollFiSpeciessesByFiSpeciesId(id);
                FiSpeciessesSubCollection = new ObservableCollection<Tbl69FiSpecies>();
            }
            NamesCollection = _extReportBasicGet.CollNamesByFiSpeciesIdAndHash(id);
            SynonymsCollection = _extReportBasicGet.CollSynonymsByFiSpeciesIdAndHash(id);

            var genusId = _extReportBasicGet.GenusIdFromFiSpeciessesCollectionSelect(id);
            GenussesCollection = _extReportBasicGet.CollGenussesByGenusIdAndHash(genusId);
            var infratribusId = _extReportBasicGet.InfratribusIdFromGenussesCollectionSelect(genusId);
            InfratribussesCollection = _extReportBasicGet.CollInfratribussesByInfratribusIdAndHash(infratribusId);
            var subtribusId = _extReportBasicGet.SubtribusIdFromInfratribussesCollectionSelect(infratribusId);
            SubtribussesCollection = _extReportBasicGet.CollSubtribussesBySubtribusIdAndHash(subtribusId);
            var tribusId = _extReportBasicGet.TribusIdFromSubtribussesCollectionSelect(subtribusId);
            TribussesCollection = _extReportBasicGet.CollTribussesByTribusIdAndHash(tribusId);
            var supertribusId = _extReportBasicGet.SupertribusIdFromTribussesCollectionSelect(tribusId);
            SupertribussesCollection = _extReportBasicGet.CollSupertribussesBySupertribusIdAndHash(supertribusId);
            var infrafamilyId = _extReportBasicGet.InfrafamilyIdFromSupertribussesCollectionSelect(supertribusId);
            InfrafamiliesCollection = _extReportBasicGet.CollInfrafamiliesByInfrafamilyIdAndHash(infrafamilyId);
            var subfamilyId = _extReportBasicGet.SubfamilyIdFromInfrafamiliesCollectionSelect(infrafamilyId);
            SubfamiliesCollection = _extReportBasicGet.CollSubfamiliesBySubfamilyIdAndHash(subfamilyId);
            var familyId = _extReportBasicGet.FamilyIdFromSubfamiliesCollectionSelect(subfamilyId);
            FamiliesCollection = _extReportBasicGet.CollFamiliesByFamilyIdAndHash(familyId);
            var superfamilyId = _extReportBasicGet.SuperfamilyIdFromFamiliesCollectionSelect(familyId);
            SuperfamiliesCollection = _extReportBasicGet.CollSuperfamiliesBySuperfamilyIdAndHash(superfamilyId);
            var infraordoId = _extReportBasicGet.InfraordoIdFromSuperfamiliesCollectionSelect(superfamilyId);
            InfraordosCollection = _extReportBasicGet.CollInfraordosByInfraordoIdAndHash(infraordoId);
            var subordoId = _extReportBasicGet.SubordoIdFromInfraordosCollectionSelect(infraordoId);
            SubordosCollection = _extReportBasicGet.CollSubordosBySubordoIdAndHash(subordoId);
            var ordoId = _extReportBasicGet.OrdoIdFromSubordosCollectionSelect(subordoId);
            OrdosCollection = _extReportBasicGet.CollOrdosByOrdoIdAndHash(ordoId);
            var legioId = _extReportBasicGet.LegioIdFromOrdosCollectionSelect(ordoId);
            LegiosCollection = _extReportBasicGet.CollLegiosByLegioIdAndHash(legioId);
            var infraclassId = _extReportBasicGet.InfraclassIdFromLegiosCollectionSelect(legioId);
            InfraclassesCollection = _extReportBasicGet.CollInfraclassesByInfraclassIdAndHash(infraclassId);
            var subclassId = _extReportBasicGet.SubclassIdFromInfraclassesCollectionSelect(infraclassId);
            SubclassesCollection = _extReportBasicGet.CollSubclassesBySubclassIdAndHash(subclassId);
            var classId = _extReportBasicGet.ClassIdFromSubclassesCollectionSelect(subclassId);
            ClassesCollection = _extReportBasicGet.CollClassesByClassIdAndHash(classId);
            var superclassId = _extReportBasicGet.SuperclassIdFromClassesCollectionSelect(classId);
            SuperclassesCollection = _extReportBasicGet.CollSuperclassesBySuperclassIdAndHash(superclassId);
            var subphylumId = _extReportBasicGet.SubphylumIdFromSuperclassesCollectionSelect(superclassId);
            var subdivisionId = _extReportBasicGet.SubdivisionIdFromSuperclassesCollectionSelect(superclassId);

            if (subphylumId == _fishId)  //Basis #Subphylum#
            {
                SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsBySubdivisionIdAndHash(subdivisionId);
                var divisionId = _extReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(subdivisionId);
                DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionIdAndHash(divisionId);
                var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            if (subdivisionId == _plantId)  //Basis #Subdivision#
            {
                SubphylumsCollection = _extReportBasicGet.CollSubphylumsBySubphylumIdAndHash(subphylumId);
                var phylumId = _extReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(subphylumId);
                PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumIdAndHash(phylumId);
                var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            //direct children
            FiSpeciessesCollection = _extReportBasicGet.CollFiSpeciessesByGenusIdAndHash(id);
            //-----------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsByFiSpeciesId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesByFiSpeciesId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsByFiSpeciesId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsByFiSpeciesId(id);
            //------------------------------------------------------------------------------
            ImagesCollection = _extReportBasicGet.CollImagesByFiSpeciesId(id);
            GeographicsCollection = _extReportBasicGet.CollGeographicsByFiSpeciesId(id);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfFiSpeciesPrintCommand;
        public ICommand PdfFiSpeciesPrintCommand
        {
            get { return _pdfFiSpeciesPrintCommand ??= new RelayCommand(delegate { CreatePdfFiSpeciesPrint(_mainId); }); }
        }
        private static void CreatePdfFiSpeciesPrint(int id)
        {
            const string use = "print";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        private RelayCommand _pdfFiSpeciesSaveCommand;
        public ICommand PdfFiSpeciesSaveCommand
        {
            get { return _pdfFiSpeciesSaveCommand ??= new RelayCommand(delegate { CreatePdfFiSpeciesSave(_mainId); }); }
        }
        private static void CreatePdfFiSpeciesSave(int id)
        {
            const string use = "save";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        #endregion

        #region PlSpecies
        public void GetTbl72PlSpeciessesById(int id)
        {
            var plSpecies = _extReportBasicGet.GetPlSpeciesSingleByPlSpeciesId(id);

            //direct children
            //special PlSpeciessesSubList with Subspecies empty  
            //     if (plSpecies.Subspecies.IsNullOrEmpty())
            if (string.IsNullOrEmpty(plSpecies.Subspecies))
            {
                PlSpeciessesCollection = _extReportBasicGet.CollPlSpeciessesByPlSpeciesId(id);
                PlSpeciessesSubCollection = _extReportBasicGet.CollPlSpeciessesByPlSpeciesNameAndNotEmptySubspeciesAndHash(plSpecies.PlSpeciesName);
                PlSpeciessesPlSpeciesNameCollection = new ObservableCollection<Tbl72PlSpecies>();
            }
            else
            {
                PlSpeciessesPlSpeciesNameCollection = _extReportBasicGet.CollPlSpeciessesByPlSpeciesNameAndSubspeciesAndDivers(plSpecies.PlSpeciesName, plSpecies.Subspecies, plSpecies.Divers);
                PlSpeciessesCollection = _extReportBasicGet.CollPlSpeciessesByPlSpeciesId(id);
                PlSpeciessesSubCollection = new ObservableCollection<Tbl72PlSpecies>();
            }
            NamesCollection = _extReportBasicGet.CollNamesByPlSpeciesIdAndHash(id);
            SynonymsCollection = _extReportBasicGet.CollSynonymsByPlSpeciesIdAndHash(id);

            var genusId = _extReportBasicGet.GenusIdFromPlSpeciessesCollectionSelect(id);
            GenussesCollection = _extReportBasicGet.CollGenussesByGenusIdAndHash(genusId);
            var infratribusId = _extReportBasicGet.InfratribusIdFromGenussesCollectionSelect(genusId);
            InfratribussesCollection = _extReportBasicGet.CollInfratribussesByInfratribusIdAndHash(infratribusId);
            var subtribusId = _extReportBasicGet.SubtribusIdFromInfratribussesCollectionSelect(infratribusId);
            SubtribussesCollection = _extReportBasicGet.CollSubtribussesBySubtribusIdAndHash(subtribusId);
            var tribusId = _extReportBasicGet.TribusIdFromSubtribussesCollectionSelect(subtribusId);
            TribussesCollection = _extReportBasicGet.CollTribussesByTribusIdAndHash(tribusId);
            var supertribusId = _extReportBasicGet.SupertribusIdFromTribussesCollectionSelect(tribusId);
            SupertribussesCollection = _extReportBasicGet.CollSupertribussesBySupertribusIdAndHash(supertribusId);
            var infrafamilyId = _extReportBasicGet.InfrafamilyIdFromSupertribussesCollectionSelect(supertribusId);
            InfrafamiliesCollection = _extReportBasicGet.CollInfrafamiliesByInfrafamilyIdAndHash(infrafamilyId);
            var subfamilyId = _extReportBasicGet.SubfamilyIdFromInfrafamiliesCollectionSelect(infrafamilyId);
            SubfamiliesCollection = _extReportBasicGet.CollSubfamiliesBySubfamilyIdAndHash(subfamilyId);
            var familyId = _extReportBasicGet.FamilyIdFromSubfamiliesCollectionSelect(subfamilyId);
            FamiliesCollection = _extReportBasicGet.CollFamiliesByFamilyIdAndHash(familyId);
            var superfamilyId = _extReportBasicGet.SuperfamilyIdFromFamiliesCollectionSelect(familyId);
            SuperfamiliesCollection = _extReportBasicGet.CollSuperfamiliesBySuperfamilyIdAndHash(superfamilyId);
            var infraordoId = _extReportBasicGet.InfraordoIdFromSuperfamiliesCollectionSelect(superfamilyId);
            InfraordosCollection = _extReportBasicGet.CollInfraordosByInfraordoIdAndHash(infraordoId);
            var subordoId = _extReportBasicGet.SubordoIdFromInfraordosCollectionSelect(infraordoId);
            SubordosCollection = _extReportBasicGet.CollSubordosBySubordoIdAndHash(subordoId);
            var ordoId = _extReportBasicGet.OrdoIdFromSubordosCollectionSelect(subordoId);
            OrdosCollection = _extReportBasicGet.CollOrdosByOrdoIdAndHash(ordoId);
            var legioId = _extReportBasicGet.LegioIdFromOrdosCollectionSelect(ordoId);
            LegiosCollection = _extReportBasicGet.CollLegiosByLegioIdAndHash(legioId);
            var infraclassId = _extReportBasicGet.InfraclassIdFromLegiosCollectionSelect(legioId);
            InfraclassesCollection = _extReportBasicGet.CollInfraclassesByInfraclassIdAndHash(infraclassId);
            var subclassId = _extReportBasicGet.SubclassIdFromInfraclassesCollectionSelect(infraclassId);
            SubclassesCollection = _extReportBasicGet.CollSubclassesBySubclassIdAndHash(subclassId);
            var classId = _extReportBasicGet.ClassIdFromSubclassesCollectionSelect(subclassId);
            ClassesCollection = _extReportBasicGet.CollClassesByClassIdAndHash(classId);
            var superclassId = _extReportBasicGet.SuperclassIdFromClassesCollectionSelect(classId);
            SuperclassesCollection = _extReportBasicGet.CollSuperclassesBySuperclassIdAndHash(superclassId);
            var subphylumId = _extReportBasicGet.SubphylumIdFromSuperclassesCollectionSelect(superclassId);
            var subdivisionId = _extReportBasicGet.SubdivisionIdFromSuperclassesCollectionSelect(superclassId);

            if (subphylumId == _fishId)  //Basis #Subphylum#
            {
                SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsBySubdivisionIdAndHash(subdivisionId);
                var divisionId = _extReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(subdivisionId);
                DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionIdAndHash(divisionId);
                var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            if (subdivisionId == _plantId)  //Basis #Subdivision#
            {
                SubphylumsCollection = _extReportBasicGet.CollSubphylumsBySubphylumIdAndHash(subphylumId);
                var phylumId = _extReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(subphylumId);
                PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumIdAndHash(phylumId);
                var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);
                RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            }
            //direct children
            PlSpeciessesCollection = _extReportBasicGet.CollPlSpeciessesByGenusIdAndHash(id);
            //-----------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsByPlSpeciesId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesByPlSpeciesId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsByPlSpeciesId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsByPlSpeciesId(id);
            //------------------------------------------------------------------------------
            ImagesCollection = _extReportBasicGet.CollImagesByPlSpeciesId(id);
            GeographicsCollection = _extReportBasicGet.CollGeographicsByPlSpeciesId(id);
        }
        //------------------------------------------------------------------------------
        private RelayCommand _pdfPlSpeciesPrintCommand;
        public ICommand PdfPlSpeciesPrintCommand
        {
            get { return _pdfPlSpeciesPrintCommand ??= new RelayCommand(delegate { CreatePdfPlSpeciesPrint(_mainId); }); }
        }
        private static void CreatePdfPlSpeciesPrint(int id)
        {
            const string use = "print";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        private RelayCommand _pdfPlSpeciesSaveCommand;
        public ICommand PdfPlSpeciesSaveCommand
        {
            get { return _pdfPlSpeciesSaveCommand ??= new RelayCommand(delegate { CreatePdfPlSpeciesSave(_mainId); }); }
        }
        private static void CreatePdfPlSpeciesSave(int id)
        {
            const string use = "save";
            ReportSuperclassPdf.CreateMainPdf(id, _fishId, _plantId, use);
        }
        #endregion


        #region "Private Properties"
        public string FilterText { get; set; }

        public int Id { get; set; }


        public ObservableCollection<Tbl03Regnum> RegnumsCollection { get; set; }
        public ObservableCollection<Tbl06Phylum> PhylumsCollection { get; set; }
        public ObservableCollection<Tbl09Division> DivisionsCollection { get; set; }
        public ObservableCollection<Tbl12Subphylum> SubphylumsCollection { get; set; }
        public ObservableCollection<Tbl15Subdivision> SubdivisionsCollection { get; set; }
        public ObservableCollection<Tbl18Superclass> SuperclassesCollection { get; set; }
        public ObservableCollection<Tbl21Class> ClassesCollection { get; set; }
        public ObservableCollection<Tbl24Subclass> SubclassesCollection { get; set; }
        public ObservableCollection<Tbl27Infraclass> InfraclassesCollection { get; set; }
        public ObservableCollection<Tbl30Legio> LegiosCollection { get; set; }
        public ObservableCollection<Tbl33Ordo> OrdosCollection { get; set; }
        public ObservableCollection<Tbl36Subordo> SubordosCollection { get; set; }
        public ObservableCollection<Tbl39Infraordo> InfraordosCollection { get; set; }
        public ObservableCollection<Tbl42Superfamily> SuperfamiliesCollection { get; set; }
        public ObservableCollection<Tbl45Family> FamiliesCollection { get; set; }
        public ObservableCollection<Tbl48Subfamily> SubfamiliesCollection { get; set; }
        public ObservableCollection<Tbl51Infrafamily> InfrafamiliesCollection { get; set; }
        public ObservableCollection<Tbl54Supertribus> SupertribussesCollection { get; set; }
        public ObservableCollection<Tbl57Tribus> TribussesCollection { get; set; }
        public ObservableCollection<Tbl60Subtribus> SubtribussesCollection { get; set; }
        public ObservableCollection<Tbl63Infratribus> InfratribussesCollection { get; set; }
        public ObservableCollection<Tbl66Genus> GenussesCollection { get; set; }
        public ObservableCollection<Tbl69FiSpecies> FiSpeciessesCollection { get; set; }
        public ObservableCollection<Tbl69FiSpecies> FiSpeciessesSubCollection { get; set; }
        public ObservableCollection<Tbl69FiSpecies> FiSpeciessesFiSpeciesNameCollection { get; set; }
        public ObservableCollection<Tbl72PlSpecies> PlSpeciessesCollection { get; set; }
        public ObservableCollection<Tbl72PlSpecies> PlSpeciessesSubCollection { get; set; }
        public ObservableCollection<Tbl72PlSpecies> PlSpeciessesPlSpeciesNameCollection { get; set; }


        public ObservableCollection<Tbl78Name> NamesCollection { get; set; }
        public ObservableCollection<Tbl81Image> ImagesCollection { get; set; }
        public ObservableCollection<Tbl84Synonym> SynonymsCollection { get; set; }
        public ObservableCollection<Tbl87Geographic> GeographicsCollection { get; set; }


        #endregion "Public Properties Tbl93Comment"

        #region "Public Properties Tbl90Author"
        public ObservableCollection<Tbl90Reference> AuthorsCollection { get; set; }

        public ObservableCollection<Tbl90Reference> Tbl90RefAuthorsList { get; set; }

        #endregion "Public Properties Tbl90Author"

        #region "Public Properties Tbl90Source"
        public ObservableCollection<Tbl90Reference> SourcesCollection { get; set; }

        public ObservableCollection<Tbl90Reference> Tbl90RefSourcesList { get; set; }


        #endregion "Public Properties Tbl90Source"

        #region "Public Properties Tbl90Expert"
        public ObservableCollection<Tbl90Reference> ExpertsCollection { get; set; }


        public ObservableCollection<Tbl90Reference> Tbl90RefExpertsList { get; set; }


        #endregion "Public Properties Tbl90Expert"

        #region "Public Properties Tbl93Comment"
        public ObservableCollection<Tbl93Comment> CommentsCollection { get; set; }


        #endregion "Public Properties Tbl93Comment"

    }

    #region Item Properties

    #endregion
}
