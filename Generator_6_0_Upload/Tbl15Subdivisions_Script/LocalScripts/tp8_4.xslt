<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using ATIS.Dal.Models;
using ATIS.Ui.Helper;
using ATIS.Ui.Views.Database.CrudHelper;
using BitMiracle.Docotic;
using BitMiracle.Docotic.Pdf;
using log4net;
using Microsoft.Win32;  ]]>

<xsl:choose>
<xsl:when test="Table ='namespace++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:otherwise>   <![CDATA[ 
         //    Report]]><xsl:value-of select="Basis"/><![CDATA[Pdf Skriptdatum: ]]> <xsl:value-of select="DateTime"/>  <![CDATA[  

namespace ATIS.Ui.Views.Report.]]><xsl:value-of select="Layout"/><![CDATA[
{  ]]>   
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Abgeleitet von++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:otherwise>   <![CDATA[ 
    public class Report]]><xsl:value-of select="Basis"/><![CDATA[Pdf : ViewModelBase
    {     ]]>
</xsl:otherwise>    
</xsl:choose> 
   
<xsl:choose>
<xsl:when test="Table ='Data Members Top+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl03Regnums'">       <![CDATA[ 
        private static readonly ILog Log = LogManager.GetLogger(typeof(Report]]><xsl:value-of select="Basis"/><![CDATA[Pdf));
        private static readonly BasicGet ExtGet = new BasicGet();
        private static readonly PdfHelper PdfHelper = new PdfHelper();
        private static string _n;
        private static string _z1;
        private static int _z;
        private static int[] _arrInts = new int[11];
        private static PdfPage _page; ]]> 
</xsl:when>  
<xsl:otherwise>        <![CDATA[ 
        private static readonly ILog Log = LogManager.GetLogger(typeof(Report]]><xsl:value-of select="Basis"/><![CDATA[Pdf));
        private static readonly BasicGet ExtGet = new BasicGet();
        private static readonly ReportBasicGet ExtReportBasicGet = new ReportBasicGet();
        private static readonly PdfHelper PdfHelper = new PdfHelper();
        private static string _n;
        private static string _z1;
        private static int _z;
        private static int[] _arrInts = new int[11];
        private static PdfPage _page; ]]> 
</xsl:otherwise>    
</xsl:choose> 

<![CDATA[ //    Part 1    ]]>

<xsl:choose>
<xsl:when test="Table ='Data Members Top 1+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when> 
<xsl:otherwise>        <![CDATA[ 
        public static void CreateMainPdf(int id, string use)
        {
            // NOTE: 
            // When used in trial mode, the library imposes some restrictions.
            // Please visit http://bitmiracle.com/pdf-library/trial-restrictions.aspx
            // for more information.

            log4net.Config.XmlConfigurator.Configure();


            //  LicenseManager.AddLicenseData("5IUML-K4LFW-CQ4J0-Y673N-72V88");
            //    BitMiracle.Docotic.LicenseManager.AddLicenseData("5IUML-K4LFW-CQ4J0-Y673N-72V88");      
            //-----------------------------------------------------------------------------    ]]> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Data Members Top 11+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when> 
<xsl:otherwise>       <![CDATA[ 
            var ]]><xsl:value-of select="BasisSm"/><![CDATA[List = ExtGet.Get]]><xsl:value-of select="Basis"/><![CDATA[sCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<]]><xsl:value-of select="LinqModel"/><![CDATA[>(id).FirstOrDefault();   ]]> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Data Members Top 2+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl03Regnums'">       <![CDATA[ 
            //Children
            var ]]><xsl:value-of select="BasisSmTK1"/><![CDATA[sList = ExtGet.Get]]><xsl:value-of select="BasissTK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(id);
            var ]]><xsl:value-of select="BasisSmTK2"/><![CDATA[sList = ExtGet.Get]]><xsl:value-of select="BasissTK2"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[>(id);  ]]> 
</xsl:when>  
<xsl:otherwise>       <![CDATA[ 
            //Child
            var ]]><xsl:value-of select="BasisSmTK1"/><![CDATA[sList = ExtGet.Get]]><xsl:value-of select="BasissTK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(id);   ]]>        
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Data Members Top 2+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl06Phylums'">       <![CDATA[ 
            //Funktion
            var regnumId = ExtReportBasicGet.RegnumIdFromPhylumsCollectionSelect(id);
            //ForeignKeyTable
            var regnumList = ExtGet.GetRegnumsCollectionOrderByFromRegnumId<Tbl03Regnum>(regnumId).FirstOrDefault();     ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl09Divisions'">       <![CDATA[ 
            //Funktion
            var regnumId = ExtReportBasicGet.RegnumIdFromDivisionsCollectionSelect(id);
            //ForeignKeyTable
            var regnumList = ExtGet.GetRegnumsCollectionOrderByFromRegnumId<Tbl03Regnum>(regnumId).FirstOrDefault();      ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl12Subphylums'">       <![CDATA[ 
            //Function
            var phylumId = ExtReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(id);
            //ForeignKeyTable
            var phylumList = ExtGet.GetPhylumsCollectionOrderByFromPhylumId<Tbl06Phylum>(phylumId).FirstOrDefault();
            //Function
            var regnumId = ExtReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);
            //ForeignKeyTable
            var regnumList = ExtGet.GetRegnumsCollectionOrderByFromRegnumId<Tbl03Regnum>(regnumId).FirstOrDefault();  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl15Subdivisions'">       <![CDATA[ 
            //Function
            var divisionId = ExtReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(id);
            //ForeignKeyTable
            var divisionList = ExtGet.GetDivisionsCollectionOrderByFromDivisionId<Tbl09Division>(divisionId).FirstOrDefault();
            //Function
            var regnumId = ExtReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);
            //ForeignKeyTable
            var regnumList = ExtGet.GetRegnumsCollectionOrderByFromRegnumId<Tbl03Regnum>(regnumId).FirstOrDefault();  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">       <![CDATA[ 
            //Function
            var subphylumId = ExtReportBasicGet.SubphylumIdFromSuperclassesCollectionSelect(id);
            //ForeignKeyTable
            var subphylumList = ExtGet.GetSubphylumsCollectionOrderByFromSubphylumId<Tbl12Subphylum>(subphylumId).FirstOrDefault();
            //Function
            var phylumId = ExtReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(subphylumId);
            //ForeignKeyTable
            var phylumList = ExtGet.GetPhylumsCollectionOrderByFromPhylumId<Tbl06Phylum>(phylumId).FirstOrDefault();
            //Function
            var regnumId = ExtReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);

            //Function
            var subdivisionId = ExtReportBasicGet.SubdivisionIdFromSuperclassesCollectionSelect(id);
            //ForeignKeyTable
            var subdivisionList = ExtGet.GetSubdivisionsCollectionOrderByFromSubdivisionId<Tbl15Subdivision>(subdivisionId).FirstOrDefault();
            //Function
            var divisionId = ExtReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(subdivisionId);
            //ForeignKeyTable
            var divisionList = ExtGet.GetDivisionsCollectionOrderByFromDivisionId<Tbl09Division>(divisionId).FirstOrDefault();
            //Function
            var regnumId = ExtReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);

            //ForeignKeyTable
            var regnumList = ExtGet.GetRegnumsCollectionOrderByFromRegnumId<Tbl03Regnum>(regnumId).FirstOrDefault();  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl21Classes'">       <![CDATA[ 
            //Function
            var superclassId = ExtReportBasicGet.SuperclassIdFromClassesCollectionSelect(id);
            //ForeignKeyTable
            var superclassList = ExtGet.GetSuperclassesCollectionOrderByFromSuperclassId<Tbl18Superclass>(superclassId).FirstOrDefault();

            //Function
            var subphylumId = ExtReportBasicGet.SubphylumIdFromSuperclassesCollectionSelect(id);
            //ForeignKeyTable
            var subphylumList = ExtGet.GetSubphylumsCollectionOrderByFromSubphylumId<Tbl12Subphylum>(subphylumId).FirstOrDefault();
            //Function
            var phylumId = ExtReportBasicGet.PhylumIdFromSubphylumsCollectionSelect(subphylumId);
            //ForeignKeyTable
            var phylumList = ExtGet.GetPhylumsCollectionOrderByFromPhylumId<Tbl06Phylum>(phylumId).FirstOrDefault();
            //Function
            var regnumId = ExtReportBasicGet.RegnumIdFromPhylumsCollectionSelect(phylumId);

            //Function
            var subdivisionId = ExtReportBasicGet.SubdivisionIdFromSuperclassesCollectionSelect(id);
            //ForeignKeyTable
            var subdivisionList = ExtGet.GetSubdivisionsCollectionOrderByFromSubdivisionId<Tbl15Subdivision>(subdivisionId).FirstOrDefault();
            //Function
            var divisionId = ExtReportBasicGet.DivisionIdFromSubdivisionsCollectionSelect(subdivisionId);
            //ForeignKeyTable
            var divisionList = ExtGet.GetDivisionsCollectionOrderByFromDivisionId<Tbl09Division>(divisionId).FirstOrDefault();
            //Function
            var regnumId = ExtReportBasicGet.RegnumIdFromDivisionsCollectionSelect(divisionId);

            //ForeignKeyTable
            var regnumList = ExtGet.GetRegnumsCollectionOrderByFromRegnumId<Tbl03Regnum>(regnumId).FirstOrDefault();  ]]> 
</xsl:when>  

<xsl:otherwise>     
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  Haeder CreateMainPdf  Top  2a ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:otherwise>       <![CDATA[      
            var expertsList = ExtGet.GetReferenceExpertsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(id);
            var sourcesList = ExtGet.GetReferenceSourcesCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(id);
            var authorsList = ExtGet.GetReferenceAuthorsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(id);
            var commentsList = ExtGet.GetCommentsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<Tbl93Comment>(id);   

            try
            { ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Data Members Top 4+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when> 
<xsl:otherwise>        <![CDATA[ 
                using (var pdf = new PdfDocument())
                {
                    _arrInts = PdfHelper.AddReportMain(pdf); 

                    Add]]><xsl:value-of select="Basis"/><![CDATA[Haeder(pdf, ]]><xsl:value-of select="BasisSm"/><![CDATA[List);
                    Add]]><xsl:value-of select="Basis"/><![CDATA[TaxoNomenList(pdf, ]]><xsl:value-of select="BasisSm"/><![CDATA[List);  ]]> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Data Members Top 44+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl03Regnums'">        <![CDATA[    
                    AddRegnumHierarchyList(pdf, regnumList);  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">        <![CDATA[   
                    if (regnumList != null)
                        AddRegnumHierarchyList(pdf, regnumList);
                     AddPhylumHierarchyList(pdf, phylumList); ]]>
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">        <![CDATA[  
                    if (regnumList != null)
                        AddRegnumHierarchyList(pdf, regnumList);
                      AddDivisionHierarchyList(pdf, divisionList);  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl12Subphylums'">        <![CDATA[    
                    if (regnumList != null)
                        AddRegnumHierarchyList(pdf, regnumList);
                    if (phylumList != null)
                        AddPhylumHierarchyList(pdf, phylumList);
                    AddSubphylumHierarchyList(pdf, subphylumList);  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl15Subdivisions'">        <![CDATA[    
                    if (regnumList != null)
                        AddRegnumHierarchyList(pdf, regnumList);
                    if (divisionList != null)
                        AddDivisionHierarchyList(pdf, divisionList);
                    AddSubdivisionHierarchyList(pdf, subdivisionList);  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl18Superclasses'">        <![CDATA[    
                    if (regnumList != null)
                        AddRegnumHierarchyList(pdf, regnumList);
                    if (phylumList != null)
                        AddPhylumHierarchyList(pdf, phylumList);
                    if (divisionList != null)
                        AddDivisionHierarchyList(pdf, divisionList);
                    if (subphylumList != null)
                        AddSubdivisionHierarchyList(pdf, subphylumList);
                    if (subdivisionList!= null)
                        AddSubdivisionHierarchyList(pdf, subdivisionList);
                    AddSuperclassHierarchyList(pdf, superclassList); ]]>
</xsl:when>
<xsl:when test="Table ='Tbl21Classes'">        <![CDATA[    
                    if (regnumList != null)
                        AddRegnumHierarchyList(pdf, regnumList);
                    if (phylumList != null)
                        AddPhylumHierarchyList(pdf, phylumList);
                    if (divisionList != null)
                        AddDivisionHierarchyList(pdf, divisionList);
                    if (subphylumList != null)
                        AddSubdivisionHierarchyList(pdf, subphylumList);
                    if (subdivisionList!= null)
                        AddSubdivisionHierarchyList(pdf, subdivisionList);
                    if (superclassList != null)
                        AddSuperclassHierarchyList(pdf, superclassList);
                    AddClassHierarchyList(pdf, ClassList); ]]>
</xsl:when>
<xsl:otherwise>     
</xsl:otherwise>    
</xsl:choose> 


<xsl:choose>
<xsl:when test="Table ='Public Commands 1  Haeder CreateMainPdf  Top 3 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">        <![CDATA[    
                        if (]]><xsl:value-of select="BasisSmTK1"/><![CDATA[sList.Count != 0)
                        Add]]><xsl:value-of select="BasisTK1"/><![CDATA[sChildrenList(pdf, ]]><xsl:value-of select="BasisSmTK1"/><![CDATA[sList);
                    if (]]><xsl:value-of select="BasisSmTK2"/><![CDATA[sList.Count != 0)
                        Add]]><xsl:value-of select="BasisTK2"/><![CDATA[sChildrenList(pdf, ]]><xsl:value-of select="BasisSmTK2"/><![CDATA[sList); ]]>
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">     
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">     
</xsl:when>
<xsl:otherwise>      <![CDATA[    
                        if (]]><xsl:value-of select="BasisSmTK1"/><![CDATA[sList.Count != 0)
                        Add]]><xsl:value-of select="BasisTK1"/><![CDATA[sChildrenList(pdf, ]]><xsl:value-of select="BasisSmTK1"/><![CDATA[sList);     ]]> 
</xsl:otherwise>    
</xsl:choose> 


<xsl:choose>
<xsl:when test="Table ='Public Commands 1  Haeder CreateMainPdf  Top 4 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">        <![CDATA[    
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
                doc = AddTbl69FiSpeciessesHusbandryList(doc);         ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">        <![CDATA[    
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
                doc = AddTbl72PlSpeciessesTechnicList(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportReproduction));
                doc = AddTbl72PlSpeciessesReproductionList(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportGlobal));
                doc = AddTbl72PlSpeciessesGlobalList(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportCulture));
                doc = AddTbl72PlSpeciessesCultureList(doc);   ]]>
</xsl:when>
<xsl:otherwise>    <![CDATA[      
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
                        _arrInts = PdfHelper.AddCommentsList(pdf, commentsList, _arrInts); ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  Haeder CreateMainPdf  Top 4 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:otherwise>    <![CDATA[      
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
        }  ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  Haeder CreateMainPdf  Top 3 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">     
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">     
</xsl:when>
<xsl:otherwise>         <![CDATA[    
        private static void Add]]><xsl:value-of select="Basis"/><![CDATA[Haeder(PdfDocument pdf, ]]><xsl:value-of select="EntityAbl"/><![CDATA[List)
        {
            _page = pdf.Pages[_arrInts[6]];

            var textAusgabeAuthor = PdfHelper.AuthorViewChangeWithString(]]><xsl:value-of select="BasisSm"/><![CDATA[List.Author, ]]><xsl:value-of select="BasisSm"/><![CDATA[List.AuthorYear);    ]]>
</xsl:otherwise>    
</xsl:choose> 


<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddTbl..Haeder  Top 1 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">      <![CDATA[      
            var textAusgabeNameAuthor = regnumList.]]><xsl:value-of select="Basis"/><![CDATA[Name + " " + regnumList.Subregnum + " " + textAusgabeAuthor;  ]]>
</xsl:when>
<xsl:otherwise>    <![CDATA[      
            var textAusgabeNameAuthor = ]]><xsl:value-of select="BasisSm"/><![CDATA[List.]]><xsl:value-of select="Basis"/><![CDATA[Name + " " + textAusgabeAuthor;  ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddTbl..Haeder  Top 2 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">        <![CDATA[   
            var author = PdfHelper.AuthorViewChangeWithoutString(_fispecies.Author, _fispecies.AuthorYear);

            table.AddCell(new PdfPCell(new Phrase(_fispecies.Tbl66Genusses.GenusName + " " + _fispecies.FiSpeciesName + " " + _fispecies.Subspecies + " " + _fispecies.Divers + " " + author, LargeFont)) { Colspan = 4, Border = 0 });   // 1.-4. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxonomicId + " " + Convert.ToString(_]]><xsl:value-of select="BasisSm"/><![CDATA[.CountID), StandardFont)) { Colspan = 4, Border = 0 }); // 1.-4. field
            doc.Add(table);
            return doc;
        }  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">        <![CDATA[     
            var author = PdfHelper.AuthorViewChangeWithoutString(_plspecies.Author, _plspecies.AuthorYear);

            table.AddCell(new PdfPCell(new Phrase(_plspecies.Tbl66Genusses.GenusName + " " + _plspecies.PlSpeciesName + " " + _plspecies.Subspecies + " " + _plspecies.Divers + " " + author, LargeFont)) { Colspan = 4, Border = 0 });   // 1.-4. field
              table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxonomicId + " " + Convert.ToString(_]]><xsl:value-of select="BasisSm"/><![CDATA[.CountID), StandardFont)) { Colspan = 4, Border = 0 }); // 1.-4. field
            doc.Add(table);
            return doc;
        }  ]]>
</xsl:when>
<xsl:otherwise>    <![CDATA[  
            _arrInts = PdfHelper.PdfTbBoldLeft("regnumName", _arrInts, true, textAusgabeNameAuthor, 2);

            _arrInts[1] += _arrInts[9]; //Distance to next TextBox

            _arrInts = PdfHelper.PdfTbBoldLeft("countId", _arrInts, false, CultRes.StringsRes.ReportTaxonomicId + ]]><xsl:value-of select="BasisSm"/><![CDATA[List.CountId, 0);

            _arrInts[1] += _arrInts[9] + 5; //Distance to next TextBox
        } ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddTblTaxoNomenList  Top 1A ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">        <![CDATA[        
        private static Document Add]]><xsl:value-of select="Table"/><![CDATA[TaxoNomenRankList(Document doc)           ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">        <![CDATA[        
        private static Document Add]]><xsl:value-of select="Table"/><![CDATA[TaxoNomenRankList(Document doc)           ]]>
</xsl:when>
<xsl:otherwise>    <![CDATA[      
        private static void Add]]><xsl:value-of select="Basis"/><![CDATA[TaxoNomenList(PdfDocument pdf, ]]><xsl:value-of select="EntityAbl"/><![CDATA[List)         ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddTblTaxoNomenList  Top 1A ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:otherwise>    <![CDATA[      
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbBoldLeft("header2", _arrInts, true, CultRes.StringsRes.ReportTaxoNomen, 2);

            _arrInts[1] += _arrInts[9]; //Distance to next TextBox
            //----------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("kingdomLeft", _arrInts, false, CultRes.StringsRes.]]><xsl:value-of select="Basis"/><![CDATA[ + ":", 0);  ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddTblTaxoNomenList  Top 2 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">        <![CDATA[        
            _arrInts = PdfHelper.PdfTbRight("kingdomRight", _arrInts, false, ]]><xsl:value-of select="BasisSm"/><![CDATA[List.]]><xsl:value-of select="Basis"/><![CDATA[Name + " " + ]]><xsl:value-of select="BasisSm"/><![CDATA[List.Subregnum, 0);      ]]>
</xsl:when>
<xsl:otherwise>    <![CDATA[      
            _arrInts = PdfHelper.PdfTbRight("kingdomRight", _arrInts, false, ]]><xsl:value-of select="BasisSm"/><![CDATA[List.]]><xsl:value-of select="Basis"/><![CDATA[Name, 0);  ]]>   
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddTblTaxoNomenList  Top 3 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">        <![CDATA[        
            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxoRank, StandardBoldFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.]]><xsl:value-of select="Basis"/><![CDATA[, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(table);

            return doc;
        }       ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">        <![CDATA[        
            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxoRank, StandardBoldFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.]]><xsl:value-of select="Basis"/><![CDATA[, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(table);

            return doc;
        }     ]]>
</xsl:when>
<xsl:otherwise>    <![CDATA[      
            //---------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("rankLeft", _arrInts, false, CultRes.StringsRes.ReportTaxoRank, 0);
            _arrInts = PdfHelper.PdfTbRight("rankRight", _arrInts, false, CultRes.StringsRes.Regnum, 0);
            //------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("synonymLeft", _arrInts, false, CultRes.StringsRes.ReportSynonyms, 0);
            //------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMtRight("synonymRight", _arrInts, ]]><xsl:value-of select="BasisSm"/><![CDATA[List.Synonym);

            _arrInts[1] += _arrInts[9]; //Distance to next TextBox

            //---------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("commNameLeft", _arrInts, false, CultRes.StringsRes.ReportCommonNames, 0);
            //---------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbRight("commNameGerRight", _arrInts, false, ]]><xsl:value-of select="BasisSm"/><![CDATA[List.GerName + " " + CultRes.StringsRes.ReportGerman, 0);
            _arrInts = PdfHelper.PdfTbRight("commNameEngRight", _arrInts, false, ]]><xsl:value-of select="BasisSm"/><![CDATA[List.EngName + " " + CultRes.StringsRes.ReportEnglish, 0);
            _arrInts = PdfHelper.PdfTbRight("commNameFraRight", _arrInts, false, ]]><xsl:value-of select="BasisSm"/><![CDATA[List.FraName + " " + CultRes.StringsRes.ReportFrench, 0);
            _arrInts = PdfHelper.PdfTbRight("commNameSpaRight", _arrInts, false, ]]><xsl:value-of select="BasisSm"/><![CDATA[List.PorName + " " + CultRes.StringsRes.ReportSpanish, 0);

            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox 
            //-------------------------------------------------------
            _arrInts = PdfHelper.PdfTbBoldMoveLeft("status", _arrInts, CultRes.StringsRes.ReportTaxoStatus, 0);
            //---------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("currStatusLeft", _arrInts, false, CultRes.StringsRes.ReportCurrentStand, 0);

            _arrInts = PdfHelper.PdfTbRight("currStatusRight", _arrInts, false, ]]><xsl:value-of select="BasisSm"/><![CDATA[List.Valid.ToString(), 0);

            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox

            //-----------------------------------------------------------
            _arrInts = PdfHelper.PdfTbBoldMoveLeft("quali", _arrInts, CultRes.StringsRes.ReportDataQualiIndicator, 0);
            //---------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("recordLeft", _arrInts, false, CultRes.StringsRes.ReportRecordUpdate, 0);

            _arrInts = PdfHelper.PdfTbRight("recordRight", _arrInts, false, Convert.ToString(]]><xsl:value-of select="BasisSm"/><![CDATA[List.UpdaterDate, CultureInfo.InvariantCulture), 0);

            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox

            //-------------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("infoLeft", _arrInts, false, CultRes.StringsRes.ReportInfo, 0);

            _arrInts = PdfHelper.PdfTbMtRight("infoRight", _arrInts, ]]><xsl:value-of select="BasisSm"/><![CDATA[List.Info);

            _arrInts[1] += _arrInts[9] - 3; //Distance to next TextBox

            //-------------------------------------------------------
            _arrInts = PdfHelper.PdfTbMoveLeft("memoLeft", _arrInts, false, CultRes.StringsRes.ReportMemo, 0);

            _arrInts = PdfHelper.PdfTbMtRight("memoRight", _arrInts, ]]><xsl:value-of select="BasisSm"/><![CDATA[List.Memo);

            _arrInts[1] += _arrInts[9]; //Distance to next TextBox
       }       ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddHierarchyList Top 1 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:otherwise>       <![CDATA[        
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
        } ]]>   
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddHierarchyList Top 1 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">     
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">     
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">     
</xsl:when>
<xsl:when test="Table ='Tbl12Subphylums'">    <![CDATA[        
        private static void AddPhylumHierarchyList(PdfDocument pdf, Tbl06Phylum phylumList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbMoveLeft("phylumLeft", _arrInts, false, CultRes.StringsRes.Phylum, 0);

            var txtName = phylumList.PhylumName;

            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, phylumList.Author,
                phylumList.AuthorYear, phylumList.GerName, phylumList.EngName, phylumList.FraName, phylumList.PorName);

            _arrInts = PdfHelper.PdfTbMtRight("phylumRight", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }    ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl15Subdivisions'">     <![CDATA[  
        private static void AddDivisionHierarchyList(PdfDocument pdf, Tbl09Division divisionList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbMoveLeft("divisionLeft", _arrInts, false, CultRes.StringsRes.Division, 0);

            var txtName = divisionList.DivisionName;

            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, divisionList.Author,
                divisionList.AuthorYear, divisionList.GerName, divisionList.EngName, divisionList.FraName, divisionList.PorName);

            _arrInts = PdfHelper.PdfTbMtRight("divisionRight", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }    ]]> 
</xsl:when>
<xsl:otherwise>    
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddHierarchyList Top 1 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">     
</xsl:when>
<xsl:otherwise>    <![CDATA[      
        private static void Add]]><xsl:value-of select="Basis"/><![CDATA[HierarchyList(PdfDocument pdf, ]]><xsl:value-of select="EntityAbl"/><![CDATA[List)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbMoveLeft("]]><xsl:value-of select="BasisSm"/><![CDATA[Left", _arrInts, false, CultRes.StringsRes.]]><xsl:value-of select="Basis"/><![CDATA[, 0);  ]]>   
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddHierarchyList Top 2 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">    
</xsl:when>
<xsl:otherwise>    <![CDATA[      
            var txtName = ]]><xsl:value-of select="BasisSm"/><![CDATA[List.]]><xsl:value-of select="Basis"/><![CDATA[Name;     ]]>   
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddHierarchyList Top 3 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">     
</xsl:when>
<xsl:otherwise>    <![CDATA[      
            var textResult = PdfHelper.NamesAuthorsForeignNamesViewChange(txtName, ]]><xsl:value-of select="BasisSm"/><![CDATA[List.Author,
                ]]><xsl:value-of select="BasisSm"/><![CDATA[List.AuthorYear, ]]><xsl:value-of select="BasisSm"/><![CDATA[List.GerName, ]]><xsl:value-of select="BasisSm"/><![CDATA[List.EngName, ]]><xsl:value-of select="BasisSm"/><![CDATA[List.FraName, ]]><xsl:value-of select="BasisSm"/><![CDATA[List.PorName);

            _arrInts = PdfHelper.PdfTbMtRight("]]><xsl:value-of select="BasisSm"/><![CDATA[Right", _arrInts, textResult);

            _arrInts[1] += _arrInts[9] + 2; //Distance to next TextBox
        }   ]]>   
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddTbl.. TK1 ChildrenList Top 1  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl66Genusses'">   
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">   
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">   
</xsl:when>
<xsl:otherwise>       <![CDATA[      
        private static void Add]]><xsl:value-of select="BasisTK1"/><![CDATA[sChildrenList(PdfDocument pdf, ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> ]]><xsl:value-of select="BasisSmTK1"/><![CDATA[sList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbRight("children]]><xsl:value-of select="BasisTK1"/><![CDATA[", _arrInts, true, CultRes.StringsRes.ReportDirectChild, 1);

            _arrInts[1] += _arrInts[9] / 2; //Distance to next TextBox

            //------------------------------------------------------------------

            _n = "child]]><xsl:value-of select="BasisTK1"/><![CDATA[";
            _z = 1;
            _z1 = _n + _z;
            _arrInts[7] += _arrInts[7];   // move 4+4

            foreach (var t in ]]><xsl:value-of select="BasisSmTK1"/><![CDATA[sList)
            {
                var t1 = t.]]><xsl:value-of select="BasisTK1"/><![CDATA[Name;
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

                _arrInts = PdfHelper.PdfTbMoveLeft("]]><xsl:value-of select="BasisSmTK1"/><![CDATA[Left" + _z1, _arrInts, false, CultRes.StringsRes.]]><xsl:value-of select="BasisTK1"/><![CDATA[, 0);

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
        }  ]]> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddTbl.. TK2 ChildrenList Top 1  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">        <![CDATA[      
        private static void Add]]><xsl:value-of select="BasisTK2"/><![CDATA[sChildrenList(PdfDocument pdf, ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> ]]><xsl:value-of select="BasisSmTK2"/><![CDATA[sList)
        {
            _page = pdf.Pages[_arrInts[6]];

            _arrInts = PdfHelper.PdfTbRight("children]]><xsl:value-of select="BasisTK2"/><![CDATA[", _arrInts, true, CultRes.StringsRes.ReportDirectChild, 1);

            _arrInts[1] += _arrInts[9] / 2; //Distance to next TextBox

            //------------------------------------------------------------------

            _n = "child]]><xsl:value-of select="BasisTK2"/><![CDATA[";
            _z = 1;
            _z1 = _n + _z;
            _arrInts[7] += _arrInts[7];   // move 4+4

            foreach (var t in ]]><xsl:value-of select="BasisSmTK2"/><![CDATA[sList)
            {
                var t1 = t.]]><xsl:value-of select="BasisTK2"/><![CDATA[Name;
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

                _arrInts = PdfHelper.PdfTbMoveLeft("]]><xsl:value-of select="BasisSmTK2"/><![CDATA[Left" + _z1, _arrInts, false, CultRes.StringsRes.]]><xsl:value-of select="BasisTK2"/><![CDATA[, 0);

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
        }  ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">   
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">   
</xsl:when>
<xsl:otherwise>   
</xsl:otherwise>    
</xsl:choose> 
   <![CDATA[}
}]]>   
</xsl:template>
</xsl:stylesheet>






