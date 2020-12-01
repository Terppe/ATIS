<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[  ]]>

<xsl:choose>
<xsl:when test="Table ='using++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when> 
<xsl:otherwise>    <![CDATA[ 
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ATIS.Ui.Views.Report.D03Regnum;
using ATIS.Ui.Views.Report.D06Phylum;
using BitMiracle.Docotic;

namespace ATIS.Ui.Views.Report
{
    public class ReportViewModel : ViewModelBase
    {
        #region "Private Data Members"
        private readonly AtisDbContext _context = new AtisDbContext();
        private readonly ReportBasicGet _extReportBasicGet = new ReportBasicGet();

        private readonly int _mainId;
        private readonly int _fishId;
        private readonly int _plantId;

        #endregion "Private Data Members"

        public ReportViewModel(int id, string tab)
        {
            LicenseManager.AddLicenseData("5IUML-K4LFW-CQ4J0-Y673N-72V88");
            //    BitMiracle.Docotic.LicenseManager.AddLicenseData("5IUML-K4LFW-CQ4J0-Y673N-72V88");

            //  Search for SubdivisionID of name Plantae#Regnum# 
            //         var plantaeRegnum = _businessLayer.SingleListTbl15SubdivisionsBySubdivisionName("Plantae#Regnum#");
            var plantaeRegnum = _context.Tbl15Subdivisions.FirstOrDefault(e => e.SubdivisionName == "Plantae#Regnum#");
            if (plantaeRegnum != null) _plantId = plantaeRegnum.SubdivisionId;

            //Search for SubphylumID of name Anaimalia#Regnum# 
            //    var animaliaRegnum = _businessLayer.SingleListTbl12SubphylumsBySubphylumName("Animalia#Regnum#");
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
                    //    case "Tbl09Divisions":
                    //        GetTbl09DivisionsById(id);
                    //        break;
                    //    case "Tbl12Subphylums":
                    //        GetTbl12SubphylumsById(id);
                    //        break;
                    //    case "Tbl15Subdivisions":
                    //        GetTbl15SubdivisionsById(id);
                    //        break;
                    //    case "Tbl18Superclasses":
                    //        GetTbl18SuperclassesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl21Classes":
                    //        GetTbl21ClassesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl24Subclasses":
                    //        GetTbl24SubclassesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl27Infraclasses":
                    //        GetTbl27InfraclassesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl30legios":
                    //        GetTbl30LegiosById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl33Ordos":
                    //        GetTbl33OrdosById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl36Subordos":
                    //        GetTbl36SubordosById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl39Infraordos":
                    //        GetTbl39InfraordosById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl42Superfamilies":
                    //        GetTbl42SuperfamiliesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl45Families":
                    //        GetTbl45FamiliesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl48Subfamilies":
                    //        GetTbl48SubfamiliesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl51Infrafamilies":
                    //        GetTbl51InfrafamiliesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl54Supertribusses":
                    //        GetTbl54SupertribussesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl57Tribusses":
                    //        GetTbl57TribussesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl60Subtribusses":
                    //        GetTbl60SubtribussesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl63Infratribusses":
                    //        GetTbl63InfratribussesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl66Genusses":
                    //        GetTbl66GenussesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl68Speciesgroups":
                    //        GetTbl68SpeciesgroupsById(id);
                    //        break;
                    //    case "Tbl69FiSpeciesses":
                    //        GetTbl69FiSpeciessesById(id, _fishId, _plantId);
                    //        break;
                    //    case "Tbl72PlSpeciesses":
                    //        GetTbl72PlSpeciessesById(id, _fishId, _plantId);
                    //        break;
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

        //		#region Methods  ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='List Top  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>  
<xsl:otherwise>  <![CDATA[ 
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
        //------------------------------------------------------------------------------

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
        //------------------------------------------------------------------------------
        //------------------------------------------------------------------------------
        public void GetTbl06PhylumsById(int id)
        {
            PhylumsCollection = _extReportBasicGet.CollPhylumsByPhylumId(id);
            //direct children
            SubphylumsCollection = _extReportBasicGet.CollSubphylumsByPhylumIdAndHash(id);
            //------------------------------------------------------------------------------
            //Function
            var regnumId = _extReportBasicGet.RegnumIdFromPhylumsCollectionSelect(id);
            //-----------------------------------------------------------------------------
            //ForeignKeyTable
            RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
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
        //------------------------------------------------------------------------------  
        //------------------------------------------------------------------------------  
        public void GetTbl09DivisionsById(int id)
        {
            DivisionsCollection = _extReportBasicGet.CollDivisionsByDivisionId(id);
            //direct children
            SubdivisionsCollection = _extReportBasicGet.CollSubdivisionsByDivisionIdAndHash(id);
            //------------------------------------------------------------------------------
            //Function
            var regnumId = _extReportBasicGet.RegnumIdFromDivisionsCollectionSelect(id);
            //-----------------------------------------------------------------------------
            //ForeignKeyTable
            RegnumsCollection = _extReportBasicGet.CollRegnumsByRegnumIdAndHash(regnumId);
            //------------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsByDivisionId(id);
            SourcesCollection = _extReportBasicGet.CollSourcesByDivisionId(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsByDivisionId(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsByDivisionId(id);
        }
        //------------------------------------------------------------------------------

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

        //------------------------------------------------------------------------------  ]]>
</xsl:otherwise>    
</xsl:choose> 

 


<xsl:choose>
<xsl:when test="Table ='using aktuelle Version pro Table von 09 bis 66    ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when> 
<xsl:otherwise>   <![CDATA[ 
        public void Get]]><xsl:value-of select="Table"/><![CDATA[ById(int id)
        {
            ]]><xsl:value-of select="Basiss"/><![CDATA[Collection = _extReportBasicGet.Coll]]><xsl:value-of select="Basiss"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[Id(id);
            //direct children
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[Collection = _extReportBasicGet.Coll]]><xsl:value-of select="BasissTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id);
            //------------------------------------------------------------------------------
            //Function
            var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id = _extReportBasicGet.]]><xsl:value-of select="BasisFK1"/><![CDATA[IdFrom]]><xsl:value-of select="Basiss"/><![CDATA[CollectionSelect(id);
            //-----------------------------------------------------------------------------
            //ForeignKeyTable
            ]]><xsl:value-of select="BasissFK1"/><![CDATA[Collection = _extReportBasicGet.Coll]]><xsl:value-of select="BasissFK1"/><![CDATA[ByRegnumIdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id);
            //------------------------------------------------------------------------------
            ExpertsCollection = _extReportBasicGet.CollExpertsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(id);
            SourcesCollection = _extReportBasicGet.CollSourcesBy]]><xsl:value-of select="Basis"/><![CDATA[Id(id);
            AuthorsCollection = _extReportBasicGet.CollAuthorsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(id);
            //------------------------------------------------------------------------------
            CommentsCollection = _extReportBasicGet.CollCommentsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(id);
        }
        //------------------------------------------------------------------------------

        private RelayCommand _pdf]]><xsl:value-of select="Basis"/><![CDATA[PrintCommand;
        public ICommand Pdf]]><xsl:value-of select="Basis"/><![CDATA[PrintCommand
        {
            get { return _pdf]]><xsl:value-of select="Basis"/><![CDATA[PrintCommand ??= new RelayCommand(delegate { CreatePdf]]><xsl:value-of select="Basis"/><![CDATA[Print(_mainId); }); }
        }

        private static void CreatePdf]]><xsl:value-of select="Basis"/><![CDATA[Print(int id)
        {
            const string use = "print";
            Report]]><xsl:value-of select="Basis"/><![CDATA[Pdf.CreateMainPdf(id, use);
        }
        private RelayCommand _pdf]]><xsl:value-of select="Basis"/><![CDATA[SaveCommand;
        public ICommand Pdf]]><xsl:value-of select="Basis"/><![CDATA[SaveCommand
        {
            get { return _pdf]]><xsl:value-of select="Basis"/><![CDATA[SaveCommand ??= new RelayCommand(delegate { CreatePdf]]><xsl:value-of select="Basis"/><![CDATA[Save(_mainId); }); }
        }

        private static void CreatePdf]]><xsl:value-of select="Basis"/><![CDATA[Save(int id)
        {
            const string use = "save";
            Report]]><xsl:value-of select="Basis"/><![CDATA[Pdf.CreateMainPdf(id, use);
        }
        //------------------------------------------------------------------------------  
        //------------------------------------------------------------------------------  
  ]]>      
</xsl:otherwise>    
</xsl:choose> 


<xsl:choose>
<xsl:when test="Table ='using++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when> 
<xsl:otherwise>   <![CDATA[ 
        #region "Private Properties"
        public string FilterText { get; set; }

        public int Id { get; set; }


        public ObservableCollection<Tbl03Regnum> RegnumsCollection { get; set; }
        public ObservableCollection<Tbl06Phylum> PhylumsCollection { get; set; }
        public ObservableCollection<Tbl09Division> DivisionsCollection { get; set; }
        public ObservableCollection<Tbl12Subphylum> SubphylumsCollection { get; set; }
        public ObservableCollection<Tbl15Subdivision> SubdivisionsCollection { get; set; }
        public ObservableCollection<Tbl18Superclass> SuperclassesCollection { get; set; }


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
}     ]]>
</xsl:otherwise>    
</xsl:choose> 



















  


</xsl:template>
</xsl:stylesheet>


