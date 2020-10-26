<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Globalization;  
using ]]><xsl:value-of select="Namespace"/>.Domain.Models; <![CDATA[ 
using ]]><xsl:value-of select="Namespace"/>.Domain.Repositories; <![CDATA[ 
using ]]><xsl:value-of select="Namespace"/>.Domain.ViewModels<![CDATA[.]]><xsl:value-of select="Table"/><![CDATA[;  ]]>       
<xsl:if test="Table='AspnetUsers'">  
<![CDATA[using System.Web.Security;     ]]>
</xsl:if>   
<xsl:if test="Table='Tbl81Images'">  
<![CDATA[using System.Web.Helpers;      ]]>
<![CDATA[using System.Web;     ]]>
</xsl:if>   
<xsl:if test="Table='Tbl87Geographics'">  
<![CDATA[using Atis.Domain.Helpers;      ]]>
</xsl:if>  
<xsl:if test="Table='TblUserProfiles'">  
<![CDATA[using System.Web;     ]]>
<![CDATA[using System.Web.Helpers;      ]]>
<![CDATA[using Atis.Domain.Helpers;      ]]>
</xsl:if>    
<![CDATA[// <!-- Controller Skriptdatum: ]]> <xsl:value-of select="DateTime"/>  <![CDATA[  -->  

namespace ]]><xsl:value-of select="Namespace"/>.WebUI.Controllers <![CDATA[    {  
    [HandleError]
    public class ]]><xsl:value-of select="Table"/><![CDATA[Controller : LanguageBaseController    { 
        readonly ]]><xsl:value-of select="Table"/><![CDATA[Repository ]]><xsl:value-of select="Entitys"/><![CDATA[Repository = new ]]><xsl:value-of select="Table"/><![CDATA[Repository();   ]]>
    <xsl:if test="TableFK1 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository();   ]]>
    </xsl:if> 
    <xsl:if test="TableFK2 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK2"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK2"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK3 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK3"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK3"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK3"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK4 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK4"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK4"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK4"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK5 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK5"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK5"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK5"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK6 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK6"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK6"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK6"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK7 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK7"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK7"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK7"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK8 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK8"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK8"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK8"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK9 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK9"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK9"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK9"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK10 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK10"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK10"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK10"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK11 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK11"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK11"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK11"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK12 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK12"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK12"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK12"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK13 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK13"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK13"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK13"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK14 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK14"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK14"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK14"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK15 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK15"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK15"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK15"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK16 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK16"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK16"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK16"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK17 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK17"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK17"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK17"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK18 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK18"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK18"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK18"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK19 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK19"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK19"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK19"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK20 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK20"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK20"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK20"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK21 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK21"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK21"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK21"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK22 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK22"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK22"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK22"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK23 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK23"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK23"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK23"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK24 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK24"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK24"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK24"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK25 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK25"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK25"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK25"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK26 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK26"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK26"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK26"/><![CDATA[Repository();  ]]>
    </xsl:if> 
    <xsl:if test="TableFK27 !='NULL'">
       <![CDATA[ readonly  ]]><xsl:value-of select="TableFK27"/><![CDATA[Repository ]]><xsl:value-of select="EntitysFK27"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK27"/><![CDATA[Repository();  ]]>
    </xsl:if> 

<xsl:choose>
<xsl:when test="Table ='+++Index+++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='AspnetApplications'">
   <![CDATA[     //-----------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /]]><xsl:value-of select="LinqModel"/><![CDATA[/  ]]>           
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="Name"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, Guid? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                   
</xsl:when>  
<xsl:when test="Table ='AspnetMemberships'">
   <![CDATA[     //-----------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /]]><xsl:value-of select="LinqModel"/><![CDATA[/  ]]>           
        <![CDATA[public ActionResult Index(string sortBy = "Password", bool ascending = true, int page = 1, int pageSize = 12, Guid? ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id = null, Guid? ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id = null, string ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Name = null, string ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Name = null)  { ]]>                                  
</xsl:when>  
<xsl:when test="Table ='AspnetUsers'">
   <![CDATA[     //-----------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /]]><xsl:value-of select="LinqModel"/><![CDATA[/  ]]>           
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="Name"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, string ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Name = null, Guid? ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, Guid? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                  
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">
   <![CDATA[      readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /]]><xsl:value-of select="LinqModel"/><![CDATA[/  ]]>
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="Name"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                   
</xsl:when>  
<xsl:when test="Table ='Tbl68Speciesgroups'">
   <![CDATA[      readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /]]><xsl:value-of select="LinqModel"/><![CDATA[/  ]]>
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="Name"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                   
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">
   <![CDATA[      readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /]]><xsl:value-of select="LinqModel"/><![CDATA[/  ]]>
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="Name"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, int? ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id = null, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                  
</xsl:when>  
<xsl:when test="Table ='Tbl69FiSpeciesses'">
   <![CDATA[      readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /]]><xsl:value-of select="LinqModel"/><![CDATA[/  ]]>
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="ID"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, int? ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id = null, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                   
</xsl:when>  
<xsl:when test="Table ='Tbl72PlSpeciesses'">
   <![CDATA[      readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /]]><xsl:value-of select="LinqModel"/><![CDATA[/  ]]>
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="ID"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, int? ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id = null, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                   
</xsl:when>  
<xsl:when test="Table ='Tbl78Names'">
   <![CDATA[      readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /]]><xsl:value-of select="LinqModel"/><![CDATA[/  ]]>
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="ID"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, int? ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id = null, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                   
</xsl:when>  
<xsl:when test="Table ='Tbl81Images'">
   <![CDATA[      readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /]]><xsl:value-of select="LinqModel"/><![CDATA[/  ]]>
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="ID"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 5, int? ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id = null, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                 
</xsl:when>  
<xsl:when test="Table ='Tbl84Synonyms'">
   <![CDATA[      readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /]]><xsl:value-of select="LinqModel"/><![CDATA[/  ]]>
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="ID"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, int? ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id = null, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                   
</xsl:when>  
<xsl:when test="Table ='Tbl87Geographics'">
   <![CDATA[      readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /]]><xsl:value-of select="LinqModel"/><![CDATA[/  ]]>
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="ID"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, int? ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id = null, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                  
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">
   <![CDATA[      readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /]]><xsl:value-of select="LinqModel"/><![CDATA[/  ]]>
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="Name"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 8, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                  
</xsl:when>  
<xsl:when test="Table ='Tbl90References'">
   <![CDATA[      readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /]]><xsl:value-of select="LinqModel"/><![CDATA[/  ]]>
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="ID"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 8, int? ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id = null, 
                                         int? ]]><xsl:value-of select="BasisSmFK3"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK4"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK5"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK6"/><![CDATA[Id = null,
                                         int? ]]><xsl:value-of select="BasisSmFK7"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK8"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK9"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK10"/><![CDATA[Id = null,
                                         int? ]]><xsl:value-of select="BasisSmFK11"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK12"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK13"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK14"/><![CDATA[Id = null,
                                         int? ]]><xsl:value-of select="BasisSmFK15"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK16"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK17"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK18"/><![CDATA[Id = null,
                                         int? ]]><xsl:value-of select="BasisSmFK19"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK20"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK21"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK22"/><![CDATA[Id = null,
                                         int? ]]><xsl:value-of select="BasisSmFK23"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK24"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK25"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK26"/><![CDATA[Id = null,
                                         int? ]]><xsl:value-of select="BasisSmFK27"/><![CDATA[Id = null, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                  
</xsl:when> 
<xsl:when test="Table ='Tbl90RefExperts'">
   <![CDATA[      readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /]]><xsl:value-of select="LinqModel"/><![CDATA[/  ]]>
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="Name"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>   
</xsl:when>                                 
<xsl:when test="Table ='Tbl90RefSources'">
   <![CDATA[      readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /]]><xsl:value-of select="LinqModel"/><![CDATA[/  ]]>
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="Name"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                   
</xsl:when>  
<xsl:when test="Table ='Tbl93Comments'">
   <![CDATA[      readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /]]><xsl:value-of select="LinqModel"/><![CDATA[/  ]]>
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="ID"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, 
                                         int? ]]><xsl:value-of select="BasisSmFK4"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK5"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK6"/><![CDATA[Id = null,
                                         int? ]]><xsl:value-of select="BasisSmFK7"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK8"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK9"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK10"/><![CDATA[Id = null,
                                         int? ]]><xsl:value-of select="BasisSmFK11"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK12"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK13"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK14"/><![CDATA[Id = null,
                                         int? ]]><xsl:value-of select="BasisSmFK15"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK16"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK17"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK18"/><![CDATA[Id = null,
                                         int? ]]><xsl:value-of select="BasisSmFK19"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK20"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK21"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK22"/><![CDATA[Id = null,
                                         int? ]]><xsl:value-of select="BasisSmFK23"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK24"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK25"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK26"/><![CDATA[Id = null,
                                         int? ]]><xsl:value-of select="BasisSmFK27"/><![CDATA[Id = null, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                  
</xsl:when> 
<xsl:when test="Table ='TblCounters'">
   <![CDATA[        //-----------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /]]><xsl:value-of select="LinqModel"/><![CDATA[/  ]]>           
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="Name"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                   
</xsl:when>  
<xsl:when test="Table ='TblUserProfiles'">
   <![CDATA[      readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //-----------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /]]><xsl:value-of select="LinqModel"/><![CDATA[/  ]]>  
        [Authorize(Roles = "Administrator")]         
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="Name"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 5, Guid? userId = null, Guid? membershipId = null, string lastname = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                   
</xsl:when>  
<xsl:otherwise>
   <![CDATA[      readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /]]><xsl:value-of select="LinqModel"/><![CDATA[/  ]]>
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="Name"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, int? ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id = null, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)    { ]]>                                   
</xsl:otherwise>    
</xsl:choose>       <![CDATA[
            var model = new ListViewModel    {
                // Sorting-related properties
                SortBy = sortBy,
                SortAscending = ascending,

                // Paging-related properties
                CurrentPageIndex = page,
                PageSize = pageSize,    ]]>

<xsl:choose>
<xsl:when test="Table ='+++Filter Properties+++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='AspnetApplications'">
        <![CDATA[        
            }; ]]>
</xsl:when>  
<xsl:when test="Table ='AspnetMemberships'">
        <![CDATA[       //
                ]]><xsl:value-of select="IDFK1"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK1"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK1"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK1"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK1"/><![CDATA[)
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = a.]]><xsl:value-of select="NameFK1"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK1"/><![CDATA[.ToString(CultureInfo.InvariantCulture.ToString()),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK2"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK2"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.]]><xsl:value-of select="TableFK2"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="NameFK2"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK2"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK2"/><![CDATA[)
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text = a.]]><xsl:value-of select="NameFK2"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK2"/><![CDATA[.ToString(CultureInfo.InvariantCulture.ToString()),
                                }).ToList()
            };     ]]>                                                                               
</xsl:when>  
<xsl:when test="Table ='AspnetUsers'">
        <![CDATA[        //
                ]]><xsl:value-of select="IDFK1"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK1"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK1"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK1"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK1"/><![CDATA[)
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = a.]]><xsl:value-of select="NameFK1"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK1"/><![CDATA[.ToString(CultureInfo.InvariantCulture.ToString()),
                                }).ToList()
            };     ]]>                                   
</xsl:when>    
<xsl:when test="Table ='Tbl03Regnums'">
        <![CDATA[        Valid = valid.HasValue && valid.Value  
            }; ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl06Phylums'">
        <![CDATA[        //
                ]]><xsl:value-of select="IDFK1"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK1"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK1"/><![CDATA[,
                                    a.Subregnum,
                                    a.]]><xsl:value-of select="IDFK1"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK1"/><![CDATA[ + a.Subregnum)
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = string.Format("{0} {1}", a.]]><xsl:value-of select="NameFK1"/><![CDATA[, a.Subregnum),
                                    Value = a.]]><xsl:value-of select="IDFK1"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                Valid = valid.HasValue && valid.Value  
            };     ]]>                                   
</xsl:when>  
<xsl:when test="Table ='Tbl09Divisions'">
        <![CDATA[        //
                ]]><xsl:value-of select="IDFK1"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK1"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK1"/><![CDATA[,
                                    a.Subregnum,
                                    a.]]><xsl:value-of select="IDFK1"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK1"/><![CDATA[ + a.Subregnum)
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = string.Format("{0} {1}", a.]]><xsl:value-of select="NameFK1"/><![CDATA[, a.Subregnum),
                                    Value = a.]]><xsl:value-of select="IDFK1"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                Valid = valid.HasValue && valid.Value  
            };     ]]>                                   
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">
        <![CDATA[       //
                ]]><xsl:value-of select="IDFK1"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK1"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK1"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK1"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK1"/><![CDATA[)
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = a.]]><xsl:value-of select="NameFK1"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK1"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK2"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK2"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.]]><xsl:value-of select="TableFK2"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="NameFK2"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK2"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK2"/><![CDATA[)
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text = a.]]><xsl:value-of select="NameFK2"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK2"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                Valid = valid.HasValue && valid.Value  
            };     ]]>                                                                               
</xsl:when>  
<xsl:when test="Table ='Tbl68Speciesgroups'">
        <![CDATA[        Valid = valid.HasValue && valid.Value  
            }; ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl69FiSpeciesses'">
        <![CDATA[       //
                ]]><xsl:value-of select="IDFK1"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK1"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[.Select(a =>
                                new
                                {
                                    a.]]><xsl:value-of select="NameFK1"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK1"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK1"/><![CDATA[)
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = a.]]><xsl:value-of select="NameFK1"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK1"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                
                ]]><xsl:value-of select="IDFK2"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK2"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.]]><xsl:value-of select="TableFK2"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK2"/><![CDATA[,
                                    a.Subspeciesgroup,
                                    a.]]><xsl:value-of select="IDFK2"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK2"/><![CDATA[  + a.Subspeciesgroup)
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text = string.Format("{0} {1}", a.]]><xsl:value-of select="NameFK2"/><![CDATA[, a.Subspeciesgroup),
                                    Value = a.]]><xsl:value-of select="IDFK2"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                Valid = valid.HasValue && valid.Value  
            };     ]]>                                                                               
</xsl:when>  
<xsl:when test="Table ='Tbl72PlSpeciesses'">
        <![CDATA[       //
                ]]><xsl:value-of select="IDFK1"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK1"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="NameFK1"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK1"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK1"/><![CDATA[)
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = a.]]><xsl:value-of select="NameFK1"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK1"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                
                ]]><xsl:value-of select="IDFK2"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK2"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.]]><xsl:value-of select="TableFK2"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="NameFK2"/><![CDATA[,
                                    a.Subspeciesgroup,
                                    a.]]><xsl:value-of select="IDFK2"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK2"/><![CDATA[  + a.Subspeciesgroup)
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text = string.Format("{0} {1}", a.]]><xsl:value-of select="NameFK2"/><![CDATA[, a.Subspeciesgroup),
                                    Value = a.]]><xsl:value-of select="IDFK2"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                Valid = valid.HasValue && valid.Value  
            };     ]]>                                                                               
</xsl:when>  
<xsl:when test="Table ='Tbl78Names'">
        <![CDATA[       //
                ]]><xsl:value-of select="IDFK1"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK1"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[,
                                    a.]]><xsl:value-of select="NameFK1"/><![CDATA[,
                                    a.Subspecies,
                                    a.Divers,
                                    a.]]><xsl:value-of select="IDFK1"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameBK1"/><![CDATA[ + a.]]><xsl:value-of select="NameFK1"/><![CDATA[ + a.Subspecies + a.Divers)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = string.Format("{0} {1} {2} {3}", a.]]><xsl:value-of select="NameBK1"/><![CDATA[, a.]]><xsl:value-of select="NameFK1"/><![CDATA[, a.Subspecies, a.Divers),
                                    Value = a.]]><xsl:value-of select="IDFK1"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK2"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK2"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.]]><xsl:value-of select="TableFK2"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[,
                                    a.]]><xsl:value-of select="NameFK2"/><![CDATA[,
                                    a.Subspecies,
                                    a.Divers,
                                    a.]]><xsl:value-of select="IDFK2"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameBK1"/><![CDATA[ + a.]]><xsl:value-of select="NameFK2"/><![CDATA[ + a.Subspecies + a.Divers)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text = string.Format("{0} {1} {2} {3}", a.]]><xsl:value-of select="NameBK1"/><![CDATA[, a.]]><xsl:value-of select="NameFK2"/><![CDATA[, a.Subspecies, a.Divers),
                                    Value = a.]]><xsl:value-of select="IDFK2"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                Valid = valid.HasValue && valid.Value  
            };     ]]>                                                                               
</xsl:when>  
<xsl:when test="Table ='Tbl81Images'">
        <![CDATA[        //
                ]]><xsl:value-of select="IDFK1"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK1"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[,
                                    a.]]><xsl:value-of select="NameFK1"/><![CDATA[,
                                    a.Subspecies,
                                    a.Divers,
                                    a.]]><xsl:value-of select="IDFK1"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameBK1"/><![CDATA[ + a.]]><xsl:value-of select="NameFK1"/><![CDATA[ + a.Subspecies + a.Divers)                                                                
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text = string.Format("{0} {1} {2} {3}", a.]]><xsl:value-of select="NameBK1"/><![CDATA[, a.]]><xsl:value-of select="NameFK1"/><![CDATA[, a.Subspecies, a.Divers),
                                    Value = a.]]><xsl:value-of select="IDFK1"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK2"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK2"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.]]><xsl:value-of select="TableFK2"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[,
                                    a.]]><xsl:value-of select="NameFK2"/><![CDATA[,
                                    a.Subspecies,
                                    a.Divers,
                                    a.]]><xsl:value-of select="IDFK2"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameBK1"/><![CDATA[ + a.]]><xsl:value-of select="NameFK2"/><![CDATA[ + a.Subspecies + a.Divers)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text = string.Format("{0} {1} {2} {3}", a.]]><xsl:value-of select="NameBK1"/><![CDATA[, a.]]><xsl:value-of select="NameFK2"/><![CDATA[, a.Subspecies, a.Divers),
                                    Value = a.]]><xsl:value-of select="IDFK2"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                Valid = valid.HasValue && valid.Value  
            };     ]]>                                                                               
</xsl:when>  
<xsl:when test="Table ='Tbl84Synonyms'">
        <![CDATA[        //
                ]]><xsl:value-of select="IDFK1"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK1"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[,
                                    a.]]><xsl:value-of select="NameFK1"/><![CDATA[,
                                    a.Subspecies,
                                    a.Divers,
                                    a.]]><xsl:value-of select="IDFK1"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameBK1"/><![CDATA[ + a.]]><xsl:value-of select="NameFK1"/><![CDATA[ + a.Subspecies + a.Divers)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = string.Format("{0} {1} {2} {3}", a.]]><xsl:value-of select="NameBK1"/><![CDATA[, a.]]><xsl:value-of select="NameFK1"/><![CDATA[, a.Subspecies, a.Divers),
                                    Value = a.]]><xsl:value-of select="IDFK1"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK2"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK2"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.]]><xsl:value-of select="TableFK2"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[,
                                    a.]]><xsl:value-of select="NameFK2"/><![CDATA[,
                                    a.Subspecies,
                                    a.Divers,
                                    a.]]><xsl:value-of select="IDFK2"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameBK1"/><![CDATA[ + a.]]><xsl:value-of select="NameFK2"/><![CDATA[ + a.Subspecies + a.Divers)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text = string.Format("{0} {1} {2} {3}", a.]]><xsl:value-of select="NameBK1"/><![CDATA[, a.]]><xsl:value-of select="NameFK2"/><![CDATA[, a.Subspecies, a.Divers),
                                    Value = a.]]><xsl:value-of select="IDFK2"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                Valid = valid.HasValue && valid.Value  
            };     ]]>                                                                               
</xsl:when>  
<xsl:when test="Table ='Tbl87Geographics'">
        <![CDATA[        //
                ]]><xsl:value-of select="IDFK1"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK1"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[,
                                    a.]]><xsl:value-of select="NameFK1"/><![CDATA[,
                                    a.Subspecies,
                                    a.Divers,
                                    a.]]><xsl:value-of select="IDFK1"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameBK1"/><![CDATA[ + a.]]><xsl:value-of select="NameFK1"/><![CDATA[ + a.Subspecies + a.Divers)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text = string.Format("{0} {1} {2} {3}", a.]]><xsl:value-of select="NameBK1"/><![CDATA[, a.]]><xsl:value-of select="NameFK1"/><![CDATA[, a.Subspecies, a.Divers),
                                    Value = a.]]><xsl:value-of select="IDFK1"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK2"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK2"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.]]><xsl:value-of select="TableFK2"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[,
                                    a.]]><xsl:value-of select="NameFK2"/><![CDATA[,
                                    a.Subspecies,
                                    a.Divers,
                                    a.]]><xsl:value-of select="IDFK2"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameBK1"/><![CDATA[ + a.]]><xsl:value-of select="NameFK2"/><![CDATA[ + a.Subspecies + a.Divers)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text = string.Format("{0} {1} {2} {3}", a.]]><xsl:value-of select="NameBK1"/><![CDATA[, a.]]><xsl:value-of select="NameFK2"/><![CDATA[, a.Subspecies, a.Divers),
                                    Value = a.]]><xsl:value-of select="IDFK2"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                Valid = valid.HasValue && valid.Value  
            };     ]]>                                                                               
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">
        <![CDATA[        Valid = valid.HasValue && valid.Value  
            }; ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl90References'">
        <![CDATA[        //
                ]]><xsl:value-of select="IDFK1"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK1"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[.Select(a =>
                                new   {
                                    a.]]><xsl:value-of select="NameFK1"/><![CDATA[,
                                    a.Notes,
                                    a.]]><xsl:value-of select="IDFK1"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK1"/><![CDATA[ + a.Notes)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem   {
                                    Text = string.Format("{0} # {1}", a.]]><xsl:value-of select="NameFK1"/><![CDATA[, a.Notes),
                                    Value = a.]]><xsl:value-of select="IDFK1"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK2"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK2"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.]]><xsl:value-of select="TableFK2"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK2"/><![CDATA[,
                                    a.SourceYear,
                                    a.Notes,
                                    a.]]><xsl:value-of select="IDFK2"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK2"/><![CDATA[ + a.SourceYear)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem   {
                                    Text =  string.Format("{0} # {1} # {2}", a.]]><xsl:value-of select="NameFK2"/><![CDATA[, a.SourceYear, a.Notes),
                                    Value = a.]]><xsl:value-of select="IDFK2"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK3"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK3"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK3"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK3"/><![CDATA[Repository.]]><xsl:value-of select="TableFK3"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK3"/><![CDATA[,
                                    a.PublicationYear,
                                    a.ArticelTitle,
                                    a.BookName,
                                    a.]]><xsl:value-of select="IDFK3"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK3"/><![CDATA[ + a.PublicationYear + a.ArticelTitle + a.BookName)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem   {
                                    Text = string.Format("{0} # {1} # {2} # {3}", a.]]><xsl:value-of select="NameFK3"/><![CDATA[, a.PublicationYear, a.ArticelTitle, a.BookName),
                                    Value = a.]]><xsl:value-of select="IDFK3"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK4"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK4"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK4"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK4"/><![CDATA[Repository.]]><xsl:value-of select="TableFK4"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[,
                                    a.]]><xsl:value-of select="NameFK4"/><![CDATA[,
                                    a.Subspecies,
                                    a.Divers,
                                    a.]]><xsl:value-of select="IDFK4"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameBK1"/><![CDATA[ + a.]]><xsl:value-of select="NameFK4"/><![CDATA[ + a.Subspecies + a.Divers)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = string.Format("{0} {1} {2} {3}", a.]]><xsl:value-of select="NameBK1"/><![CDATA[, a.]]><xsl:value-of select="NameFK4"/><![CDATA[, a.Subspecies, a.Divers),
                                    Value = a.]]><xsl:value-of select="IDFK4"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK5"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK5"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK5"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK5"/><![CDATA[Repository.]]><xsl:value-of select="TableFK5"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[,
                                    a.]]><xsl:value-of select="NameFK5"/><![CDATA[,
                                    a.Subspecies,
                                    a.Divers,
                                    a.]]><xsl:value-of select="IDFK5"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameBK1"/><![CDATA[ + a.]]><xsl:value-of select="NameFK5"/><![CDATA[ + a.Subspecies + a.Divers)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = string.Format("{0} {1} {2} {3}", a.]]><xsl:value-of select="NameBK1"/><![CDATA[, a.]]><xsl:value-of select="NameFK5"/><![CDATA[, a.Subspecies, a.Divers),
                                    Value = a.]]><xsl:value-of select="IDFK5"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK6"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK6"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK6"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK6"/><![CDATA[Repository.]]><xsl:value-of select="TableFK6"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK6"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK6"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK6"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK6"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK6"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK7"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK7"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK7"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK7"/><![CDATA[Repository.]]><xsl:value-of select="TableFK7"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK7"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK7"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK7"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK7"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK7"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK8"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK8"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK8"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK8"/><![CDATA[Repository.]]><xsl:value-of select="TableFK8"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK8"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK8"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK8"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text =  a.]]><xsl:value-of select="NameFK8"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK8"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK9"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK9"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK9"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK9"/><![CDATA[Repository.]]><xsl:value-of select="TableFK9"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK9"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK9"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK9"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK9"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK9"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK10"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK10"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK10"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK10"/><![CDATA[Repository.]]><xsl:value-of select="TableFK10"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="NameFK10"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK10"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK10"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK10"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK10"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK11"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK11"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK11"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK11"/><![CDATA[Repository.]]><xsl:value-of select="TableFK11"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="NameFK11"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK11"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK11"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK11"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK11"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK12"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK12"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK12"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK12"/><![CDATA[Repository.]]><xsl:value-of select="TableFK12"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK12"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK12"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK12"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text =  a.]]><xsl:value-of select="NameFK12"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK12"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK13"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK13"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK13"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK13"/><![CDATA[Repository.]]><xsl:value-of select="TableFK13"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="NameFK13"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK13"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK13"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK13"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK13"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK14"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK14"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK14"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK14"/><![CDATA[Repository.]]><xsl:value-of select="TableFK14"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK14"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK14"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK14"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK14"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK14"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK15"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK15"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK15"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK15"/><![CDATA[Repository.]]><xsl:value-of select="TableFK15"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK15"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK15"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK15"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text =  a.]]><xsl:value-of select="NameFK15"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK15"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK16"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK16"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK16"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK16"/><![CDATA[Repository.]]><xsl:value-of select="TableFK16"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK16"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK16"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK16"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK16"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK16"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK17"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK17"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK17"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK17"/><![CDATA[Repository.]]><xsl:value-of select="TableFK17"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK17"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK17"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK17"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK17"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK17"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK18"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK18"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK18"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK18"/><![CDATA[Repository.]]><xsl:value-of select="TableFK18"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK18"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK18"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK18"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text =  a.]]><xsl:value-of select="NameFK18"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK18"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK19"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK19"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK19"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK19"/><![CDATA[Repository.]]><xsl:value-of select="TableFK19"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK19"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK19"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK19"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK19"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK19"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK20"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK20"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK20"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK20"/><![CDATA[Repository.]]><xsl:value-of select="TableFK20"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK20"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK20"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK20"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK20"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK20"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK21"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK21"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK21"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK21"/><![CDATA[Repository.]]><xsl:value-of select="TableFK21"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK21"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK21"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK21"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK21"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK21"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK22"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK22"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK22"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK22"/><![CDATA[Repository.]]><xsl:value-of select="TableFK22"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="NameFK22"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK22"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK22"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK22"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK22"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK23"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK23"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK23"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK23"/><![CDATA[Repository.]]><xsl:value-of select="TableFK23"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK23"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK23"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK23"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text =  a.]]><xsl:value-of select="NameFK23"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK23"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK24"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK24"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK24"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK24"/><![CDATA[Repository.]]><xsl:value-of select="TableFK24"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="NameFK24"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK24"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK24"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK24"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK24"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK25"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK25"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK25"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK25"/><![CDATA[Repository.]]><xsl:value-of select="TableFK25"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK25"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK25"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK25"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK25"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK25"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK26"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK26"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK26"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK26"/><![CDATA[Repository.]]><xsl:value-of select="TableFK26"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK26"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK26"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK26"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text =  a.]]><xsl:value-of select="NameFK26"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK26"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK27"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK27"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK27"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK27"/><![CDATA[Repository.]]><xsl:value-of select="TableFK27"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="NameFK27"/><![CDATA[,
                                    a.Subregnum,
                                    a.]]><xsl:value-of select="IDFK27"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK27"/><![CDATA[ + a.Subregnum)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text = string.Format("{0} {1}",  a.]]><xsl:value-of select="NameFK27"/><![CDATA[,  a.Subregnum),
                                    Value = a.]]><xsl:value-of select="IDFK27"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                Valid = valid.HasValue && valid.Value  
            };     ]]>                                                                               
</xsl:when>  
<xsl:when test="Table ='Tbl90RefExperts'">
        <![CDATA[        Valid = valid.HasValue && valid.Value  
            }; ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl90RefSources'">
        <![CDATA[        Valid = valid.HasValue && valid.Value  
            }; ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl93Comments'">
        <![CDATA[        //
                ]]><xsl:value-of select="IDFK4"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK4"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK4"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK4"/><![CDATA[Repository.]]><xsl:value-of select="TableFK4"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[,
                                    a.]]><xsl:value-of select="NameFK4"/><![CDATA[,
                                    a.Subspecies,
                                    a.Divers,
                                    a.]]><xsl:value-of select="IDFK4"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameBK1"/><![CDATA[ + a.]]><xsl:value-of select="NameFK4"/><![CDATA[ + a.Subspecies + a.Divers)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = string.Format("{0} {1} {2} {3}", a.]]><xsl:value-of select="NameBK1"/><![CDATA[, a.]]><xsl:value-of select="NameFK4"/><![CDATA[, a.Subspecies, a.Divers),
                                    Value = a.]]><xsl:value-of select="IDFK4"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK5"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK5"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK5"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK5"/><![CDATA[Repository.]]><xsl:value-of select="TableFK5"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[,
                                    a.]]><xsl:value-of select="NameFK5"/><![CDATA[,
                                    a.Subspecies,
                                    a.Divers,
                                    a.]]><xsl:value-of select="IDFK5"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameBK1"/><![CDATA[ + a.]]><xsl:value-of select="NameFK5"/><![CDATA[ + a.Subspecies + a.Divers)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = string.Format("{0} {1} {2} {3}", a.]]><xsl:value-of select="NameBK1"/><![CDATA[, a.]]><xsl:value-of select="NameFK5"/><![CDATA[, a.Subspecies, a.Divers),
                                    Value = a.]]><xsl:value-of select="IDFK5"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK6"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK6"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK6"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK6"/><![CDATA[Repository.]]><xsl:value-of select="TableFK6"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK6"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK6"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK6"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK6"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK6"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK7"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK7"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK7"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK7"/><![CDATA[Repository.]]><xsl:value-of select="TableFK7"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK7"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK7"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK7"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK7"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK7"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK8"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK8"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK8"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK8"/><![CDATA[Repository.]]><xsl:value-of select="TableFK8"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK8"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK8"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK8"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text =  a.]]><xsl:value-of select="NameFK8"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK8"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK9"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK9"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK9"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK9"/><![CDATA[Repository.]]><xsl:value-of select="TableFK9"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK9"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK9"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK9"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK9"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK9"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK10"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK10"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK10"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK10"/><![CDATA[Repository.]]><xsl:value-of select="TableFK10"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="NameFK10"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK10"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK10"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK10"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK10"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK11"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK11"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK11"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK11"/><![CDATA[Repository.]]><xsl:value-of select="TableFK11"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="NameFK11"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK11"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK11"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK11"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK11"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK12"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK12"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK12"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK12"/><![CDATA[Repository.]]><xsl:value-of select="TableFK12"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK12"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK12"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK12"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text =  a.]]><xsl:value-of select="NameFK12"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK12"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK13"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK13"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK13"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK13"/><![CDATA[Repository.]]><xsl:value-of select="TableFK13"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="NameFK13"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK13"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK13"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK13"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK13"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK14"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK14"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK14"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK14"/><![CDATA[Repository.]]><xsl:value-of select="TableFK14"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK14"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK14"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK14"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK14"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK14"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK15"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK15"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK15"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK15"/><![CDATA[Repository.]]><xsl:value-of select="TableFK15"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK15"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK15"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK15"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text =  a.]]><xsl:value-of select="NameFK15"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK15"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK16"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK16"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK16"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK16"/><![CDATA[Repository.]]><xsl:value-of select="TableFK16"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK16"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK16"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK16"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK16"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK16"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK17"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK17"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK17"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK17"/><![CDATA[Repository.]]><xsl:value-of select="TableFK17"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK17"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK17"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK17"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK17"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK17"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK18"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK18"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK18"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK18"/><![CDATA[Repository.]]><xsl:value-of select="TableFK18"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK18"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK18"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK18"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text =  a.]]><xsl:value-of select="NameFK18"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK18"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK19"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK19"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK19"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK19"/><![CDATA[Repository.]]><xsl:value-of select="TableFK19"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK19"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK19"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK19"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK19"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK19"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK20"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK20"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK20"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK20"/><![CDATA[Repository.]]><xsl:value-of select="TableFK20"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK20"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK20"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK20"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK20"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK20"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK21"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK21"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK21"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK21"/><![CDATA[Repository.]]><xsl:value-of select="TableFK21"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK21"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK21"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK21"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK21"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK21"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK22"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK22"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK22"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK22"/><![CDATA[Repository.]]><xsl:value-of select="TableFK22"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="NameFK22"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK22"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK22"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK22"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK22"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK23"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK23"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK23"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK23"/><![CDATA[Repository.]]><xsl:value-of select="TableFK23"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK23"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK23"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK23"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text =  a.]]><xsl:value-of select="NameFK23"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK23"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK24"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK24"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK24"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK24"/><![CDATA[Repository.]]><xsl:value-of select="TableFK24"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="NameFK24"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK24"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK24"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK24"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK24"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK25"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK25"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK25"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK25"/><![CDATA[Repository.]]><xsl:value-of select="TableFK25"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK25"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK25"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK25"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text =  a.]]><xsl:value-of select="NameFK25"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK25"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK26"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK26"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK26"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK26"/><![CDATA[Repository.]]><xsl:value-of select="TableFK26"/><![CDATA[.Select(a =>
                                new    {
                                    a.]]><xsl:value-of select="NameFK26"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK26"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK26"/><![CDATA[)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text =  a.]]><xsl:value-of select="NameFK26"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK26"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                ]]><xsl:value-of select="IDFK27"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK27"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK27"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK27"/><![CDATA[Repository.]]><xsl:value-of select="TableFK27"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="NameFK27"/><![CDATA[,
                                    a.Subregnum,
                                    a.]]><xsl:value-of select="IDFK27"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK27"/><![CDATA[ + a.Subregnum)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text = string.Format("{0} {1}",  a.]]><xsl:value-of select="NameFK27"/><![CDATA[,  a.Subregnum),
                                    Value = a.]]><xsl:value-of select="IDFK27"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                Valid = valid.HasValue && valid.Value  
            };     ]]>                                                                               
</xsl:when>  
<xsl:when test="Table ='TblCounters'">
        <![CDATA[         
            }; ]]>
</xsl:when>  
<xsl:when test="Table ='TblUserProfiles'">
        <![CDATA[       // 
                ]]><xsl:value-of select="IDFK1"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK1"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="NameFK1"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK1"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK1"/><![CDATA[)
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = a.]]><xsl:value-of select="NameFK1"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK1"/><![CDATA[.ToString(CultureInfo.InvariantCulture.ToString()),
                                }).ToList()
            };     ]]>                                   
</xsl:when>  
<xsl:otherwise>
        <![CDATA[       // 
                ]]><xsl:value-of select="IDFK1"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id,
                ]]><xsl:value-of select="BasisFK1"/><![CDATA[List = ]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[.Select(a =>
                                new     {
                                    a.]]><xsl:value-of select="NameFK1"/><![CDATA[,
                                    a.]]><xsl:value-of select="IDFK1"/><![CDATA[
                                }
                            )
                            .OrderBy(a => a.]]><xsl:value-of select="NameFK1"/><![CDATA[)
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = a.]]><xsl:value-of select="NameFK1"/><![CDATA[,
                                    Value = a.]]><xsl:value-of select="IDFK1"/><![CDATA[.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                Valid = valid.HasValue && valid.Value  
            };     ]]>                                   
</xsl:otherwise>    
</xsl:choose>       
        <![CDATA[    var filteredResults = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.]]><xsl:value-of select="Table"/><![CDATA[.AsQueryable();   ]]>
<xsl:choose>
<xsl:when test="Table ='Name+++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='AspnetMemberships'">  
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
</xsl:when>
<xsl:when test="Table ='Tbl90References'">  
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">  
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">  
<![CDATA[            // Filter on ]]><xsl:value-of select="Name"/><![CDATA[
            if (!string.IsNullOrEmpty(lastname))
                filteredResults = filteredResults.Where(a => a.]]><xsl:value-of select="Name"/><![CDATA[.StartsWith(lastname));  ]]>    
</xsl:when>
<xsl:otherwise> 
<![CDATA[            // Filter on ]]><xsl:value-of select="Name"/><![CDATA[
            if (!string.IsNullOrEmpty(]]><xsl:value-of select="BasisSm"/><![CDATA[Name))
                filteredResults = filteredResults.Where(a => a.]]><xsl:value-of select="Name"/><![CDATA[.StartsWith(]]><xsl:value-of select="BasisSm"/><![CDATA[Name));  ]]>    
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Filter+++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='AspnetMemberships'">  
</xsl:when>
<xsl:otherwise> 
<![CDATA[            // Filter on ]]><xsl:value-of select="ID"/><![CDATA[
            if (]]><xsl:value-of select="BasisSm"/><![CDATA[Id.HasValue)
                filteredResults = filteredResults.Where(a => a.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);   ]]>
</xsl:otherwise>    
</xsl:choose>

    <xsl:if test="TableFK1 !='NULL'">
        <![CDATA[   // Filter on ]]><xsl:value-of select="IDFK1"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK1"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id.Value);     ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK2 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK2"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK2"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK3 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK3"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK3"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK3"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK3"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK4 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK4"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK4"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK4"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK4"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK5 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK5"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK5"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK5"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK5"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK6 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK6"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK6"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK6"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK6"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK7 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK7"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK7"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK7"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK7"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK8 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK8"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK8"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK8"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK8"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK9 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK9"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK9"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK9"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK9"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK10 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK10"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK10"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK10"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK10"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK11 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK11"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK11"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK11"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK11"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK12 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK12"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK12"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK12"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK12"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK13 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK13"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK13"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK13"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK13"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK14 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK14"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK14"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK14"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK14"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK15 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK15"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK15"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK15"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK15"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK16 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK16"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK16"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK16"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK16"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK17 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK17"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK17"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK17"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK17"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK18 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK18"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK18"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK18"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK18"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK19 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK19"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK19"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK19"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK19"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK20 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK20"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK20"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK20"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK20"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK21 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK21"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK21"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK21"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK21"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK22 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK22"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK22"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK22"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK22"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK23 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK23"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK23"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK23"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK23"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK24 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK24"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK24"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK24"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK24"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK25 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK25"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK25"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK25"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK25"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK26 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK26"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK26"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK26"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK26"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 
    <xsl:if test="TableFK27 !='NULL'">
        <![CDATA[    // Filter on ]]><xsl:value-of select="IDFK27"/><![CDATA[ 
            if (]]><xsl:value-of select="BasisSmFK27"/><![CDATA[Id != null)
                filteredResults = filteredResults.Where(p => p.]]><xsl:value-of select="IDFK27"/><![CDATA[ == ]]><xsl:value-of select="BasisSmFK27"/><![CDATA[Id.Value);    ]]>                                   
    </xsl:if> 

<xsl:choose>
<xsl:when test="Table ='Valid++++++++++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='AspnetApplications'">      
</xsl:when>
<xsl:when test="Table ='AspnetMemberships'">      
</xsl:when>
<xsl:when test="Table ='AspnetUsers'">      
</xsl:when>
<xsl:when test="Table ='TblCounters'">        
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">        
</xsl:when>
<xsl:otherwise>  
<![CDATA[            //Filter on Valid
            if (valid != null && valid.Value)
                filteredResults = filteredResults.Where(p => p.Valid == true);   ]]>
</xsl:otherwise>    
</xsl:choose> 

<![CDATA[            // Determine the total number of FILTERED products being paged through (needed to compute PageCount)
            model.TotalRecordCount = filteredResults.Count();

            // Get the current page of sorted, filtered products
            model.]]><xsl:value-of select="Table"/><![CDATA[ = filteredResults
                                        .OrderBy(model.SortExpression)
                                        .Skip((model.CurrentPageIndex - 1) * model.PageSize)
                                        .Take(model.PageSize);

            return View(model);

        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>

<xsl:choose>
<xsl:when test="Table ='+++++++++INDEX+++++++++++++++++++++REGISTER+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">      
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">   
<![CDATA[        //
        // GET: /TblUserProfile/
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist, User")]
        public ActionResult IndexRegister(string sortBy = "LastName", bool ascending = true, int page = 1, int pageSize = 5, Guid? userId = null, Guid? membershipId = null, string lastname = null, int? userprofileId = null)    {
            var model = new ListViewModel    {
                // Sorting-related properties
                SortBy = sortBy,
                SortAscending = ascending,

                // Paging-related properties
                CurrentPageIndex = page,
                PageSize = pageSize,

                UserId = userId,
                UserList = _aspnetUsersRepository.AspnetUsers.Select(a =>
                                new    {
                                    a.UserName,
                                    a.UserId
                                }
                            )
                            .OrderBy(a => a.UserName)
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = a.UserName,
                                    Value = a.UserId.ToString(),
                                }).ToList()
            };

            var filteredResults = _tblUserProfilesRepository.TblUserProfiles.AsQueryable();

            // Filter on LastName
            if (!string.IsNullOrEmpty(lastname))
                filteredResults = filteredResults.Where(a => a.LastName.StartsWith(lastname));

            // Filter on UserProfileID
            if (userprofileId.HasValue)
                filteredResults = filteredResults.Where(a => a.UserProfileID == userprofileId);


            // Filter on UserId 
            if (userId != null)
                filteredResults = filteredResults.Where(p => p.UserId == userId.Value);


            // Determine the total number of FILTERED products being paged through (needed to compute PageCount)
            model.TotalRecordCount = filteredResults.Count();

            // Get the current page of sorted, filtered products
            model.TblUserProfiles = filteredResults
                                        .OrderBy(model.SortExpression)
                                        .Skip((model.CurrentPageIndex - 1) * model.PageSize)
                                        .Take(model.PageSize);

            return View(model);

        }
        //------------------------------------------------------------------------  ]]>     
</xsl:when>
<xsl:otherwise>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Functionen+++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">   
<![CDATA[           
        private void ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK1"/><![CDATA[, d.Subregnum
                         select new  {
                             d.]]><xsl:value-of select="IDFK1"/><![CDATA[,
                             DDLName = d.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + d.Subregnum
                         };
            ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK1"/><![CDATA[", "DDLName");
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">   
<![CDATA[           
        private void ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK1"/><![CDATA[, d.Subregnum
                         select new  {
                             d.]]><xsl:value-of select="IDFK1"/><![CDATA[,
                             DDLName = d.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + d.Subregnum
                         };
            ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK1"/><![CDATA[", "DDLName");
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">   
<![CDATA[           
        private void ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.]]><xsl:value-of select="TableFK2"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK2"/><![CDATA[, d.Subspeciesgroup
                         select new  {
                             d.]]><xsl:value-of select="IDFK2"/><![CDATA[,
                             DDLName = d.]]><xsl:value-of select="NameFK2"/><![CDATA[ + " " + d.Subspeciesgroup
                         };
            ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK2"/><![CDATA[", "DDLName");
            ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK1"/><![CDATA[", "]]><xsl:value-of select="NameFK1"/><![CDATA[");
        }       
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">   
<![CDATA[           
        private void ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.]]><xsl:value-of select="TableFK2"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK2"/><![CDATA[, d.Subspeciesgroup
                         select new  {
                             d.]]><xsl:value-of select="IDFK2"/><![CDATA[,
                             DDLName = d.]]><xsl:value-of select="NameFK2"/><![CDATA[ + " " + d.Subspeciesgroup
                         };
            ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK2"/><![CDATA[", "DDLName");
            ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK1"/><![CDATA[", "]]><xsl:value-of select="NameFK1"/><![CDATA[");
        }       
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">  
   <![CDATA[     
        private void ViewDataLanguagesGetValue(]]><xsl:value-of select="EntityAbl"/><![CDATA[)  {
            var languages = new[]  {
                                    "ENG",
                                    "GER",
                                    "FRA",
                                    "POR"
                                };

            ViewData["languages"] = new SelectList(languages, ]]><xsl:value-of select="Entity"/><![CDATA[.Language);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  
    
        private void ViewDataSpeciesGetValue()  {

            var query1 = from d in ]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[
                         orderby d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[, d.]]><xsl:value-of select="NameFK1"/><![CDATA[, d.Subspecies, d.Divers
                         select new  {
                             d.]]><xsl:value-of select="IDFK1"/><![CDATA[,
                             DDLFiName = d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + d.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + d.Subspecies + " " + d.Divers
                         };

            var query2 = from d in ]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.]]><xsl:value-of select="TableFK2"/><![CDATA[
                         orderby d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[, d.]]><xsl:value-of select="NameFK2"/><![CDATA[, d.Subspecies, d.Divers
                         select new  {
                             d.]]><xsl:value-of select="IDFK2"/><![CDATA[,
                             DDLPlName = d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + d.]]><xsl:value-of select="NameFK2"/><![CDATA[ + " " + d.Subspecies + " " + d.Divers
                         };
            ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] = new SelectList(query1.ToList(), "]]><xsl:value-of select="IDFK1"/><![CDATA[", "DDLFiName");
            ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] = new SelectList(query2.ToList(), "]]><xsl:value-of select="IDFK2"/><![CDATA[", "DDLPlName");
        }       
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
   <![CDATA[     //
        //Image or Video from Filestream to show
        public FileContentResult GetFilestream(int id)
        {
            var imageVideo = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.]]><xsl:value-of select="Table"/><![CDATA[.First(x => x.ImageID == id);
            if (imageVideo == null) throw new ArgumentNullException(SharedRes.StringsRes.ErrorNullException);
            //imageVideo darf nicht NULL sein passiert wenn Bild zu groß bei upload oder bei video
            return File(imageVideo.Filestream, imageVideo.ImageMimeType);  
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewDataMimeTypesGetValue(]]><xsl:value-of select="EntityAbl"/><![CDATA[)  {
            var mimeTypes = new[]  {
                                    "jpg",
                                    "JPG",
                                    "png",
                                    "bmp",
                                    "tiff",
                                    "gif",
                                    "icon",
                                    "jpeg",
                                    "wmf",
                                    "wmv",
                                    "mpg",
                                    "mp4",
                                    "avi",
                                    "mov",
                                    "swf",
                                    "flv"
                                };

            ViewData["mimeTypes"] = new SelectList(mimeTypes, ]]><xsl:value-of select="Entity"/><![CDATA[.ImageMimeType);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  
       
        private void ViewDataSpeciesGetValue()  {

            var query1 = from d in ]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[
                         orderby d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[, d.]]><xsl:value-of select="NameFK1"/><![CDATA[, d.Subspecies, d.Divers
                         select new  {
                             d.]]><xsl:value-of select="IDFK1"/><![CDATA[,
                             DDLFiName = d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + d.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + d.Subspecies + " " + d.Divers
                         };

            var query2 = from d in ]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.]]><xsl:value-of select="TableFK2"/><![CDATA[
                         orderby d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[, d.]]><xsl:value-of select="NameFK2"/><![CDATA[, d.Subspecies, d.Divers
                         select new  {
                             d.]]><xsl:value-of select="IDFK2"/><![CDATA[,
                             DDLPlName = d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + d.]]><xsl:value-of select="NameFK2"/><![CDATA[ + " " + d.Subspecies + " " + d.Divers
                         };
            ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] = new SelectList(query1.ToList(), "]]><xsl:value-of select="IDFK1"/><![CDATA[", "DDLFiName");
            ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] = new SelectList(query2.ToList(), "]]><xsl:value-of select="IDFK2"/><![CDATA[", "DDLPlName");
        }       
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">  
   <![CDATA[             
        private void ViewDataSpeciesGetValue()  {

            var query1 = from d in ]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[
                         orderby d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[, d.]]><xsl:value-of select="NameFK1"/><![CDATA[, d.Subspecies, d.Divers
                         select new  {
                             d.]]><xsl:value-of select="IDFK1"/><![CDATA[,
                             DDLFiName = d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + d.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + d.Subspecies + " " + d.Divers
                         };

            var query2 = from d in ]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.]]><xsl:value-of select="TableFK2"/><![CDATA[
                         orderby d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[, d.]]><xsl:value-of select="NameFK2"/><![CDATA[, d.Subspecies, d.Divers
                         select new  {
                             d.]]><xsl:value-of select="IDFK2"/><![CDATA[,
                             DDLPlName = d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + d.]]><xsl:value-of select="NameFK2"/><![CDATA[ + " " + d.Subspecies + " " + d.Divers
                         };
            ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] = new SelectList(query1.ToList(), "]]><xsl:value-of select="IDFK1"/><![CDATA[", "DDLFiName");
            ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] = new SelectList(query2.ToList(), "]]><xsl:value-of select="IDFK2"/><![CDATA[", "DDLPlName");
        }       
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
   <![CDATA[             
        private void ViewDataSpeciesGetValue()  {

            var query1 = from d in ]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[
                         orderby d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[, d.]]><xsl:value-of select="NameFK1"/><![CDATA[, d.Subspecies, d.Divers
                         select new  {
                             d.]]><xsl:value-of select="IDFK1"/><![CDATA[,
                             DDLFiName = d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + d.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + d.Subspecies + " " + d.Divers
                         };

            var query2 = from d in ]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.]]><xsl:value-of select="TableFK2"/><![CDATA[
                         orderby d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[, d.]]><xsl:value-of select="NameFK2"/><![CDATA[, d.Subspecies, d.Divers
                         select new  {
                             d.]]><xsl:value-of select="IDFK2"/><![CDATA[,
                             DDLPlName = d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + d.]]><xsl:value-of select="NameFK2"/><![CDATA[ + " " + d.Subspecies + " " + d.Divers
                         };
            ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] = new SelectList(query1.ToList(), "]]><xsl:value-of select="IDFK1"/><![CDATA[", "DDLFiName");
            ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] = new SelectList(query2.ToList(), "]]><xsl:value-of select="IDFK2"/><![CDATA[", "DDLPlName");
        }       
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90References'">  
   <![CDATA[     
        private void ViewData]]><xsl:value-of select="TableFK1"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK1"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK1"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK1"/><![CDATA[ = d.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " ##  " + d.Notes
                         };
            ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK1"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK1"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK2"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.]]><xsl:value-of select="TableFK2"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK2"/><![CDATA[, d.SourceYear
                         select new    {
                             d.]]><xsl:value-of select="IDFK2"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK2"/><![CDATA[ = d.]]><xsl:value-of select="NameFK2"/><![CDATA[ + " ##  " + d.SourceYear + " ##  " + d.Notes
                         };
            ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK2"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK2"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK3"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK3"/><![CDATA[Repository.]]><xsl:value-of select="TableFK3"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK3"/><![CDATA[, d.PublicationYear, d.Page
                         select new    {
                             d.]]><xsl:value-of select="IDFK3"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK3"/><![CDATA[ = d.]]><xsl:value-of select="NameFK3"/><![CDATA[ + " ##  " + d.PublicationYear + " ## " + d.Page + " ## " + d.ArticelTitle + " ## " + d.BookName
                         };
            ViewData["]]><xsl:value-of select="NameFK3"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK3"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK3"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewDataSpeciessesGetValue()    {
            var query1 = from d in ]]><xsl:value-of select="EntitysFK4"/><![CDATA[Repository.]]><xsl:value-of select="TableFK4"/><![CDATA[
                         orderby d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[, d.]]><xsl:value-of select="NameFK4"/><![CDATA[, d.Subspecies, d.Divers     
                         select new    {
                             d.]]><xsl:value-of select="IDFK4"/><![CDATA[,
                             DDLFiName = d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + d.]]><xsl:value-of select="NameFK4"/><![CDATA[ + " " + d.Subspecies + " " + d.Divers
                         };
            var query2 = from d in ]]><xsl:value-of select="EntitysFK5"/><![CDATA[Repository.]]><xsl:value-of select="TableFK5"/><![CDATA[
                         orderby d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[, d.]]><xsl:value-of select="NameFK5"/><![CDATA[, d.Subspecies, d.Divers     
                         select new    {
                             d.]]><xsl:value-of select="IDFK5"/><![CDATA[,
                             DDLPlName = d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + d.]]><xsl:value-of select="NameFK5"/><![CDATA[ + " " + d.Subspecies + " " + d.Divers
                         };
            ViewData["]]><xsl:value-of select="NameFK4"/><![CDATA[DDL"] = new SelectList(query1.ToList(), "]]><xsl:value-of select="IDFK4"/><![CDATA[", "DDLFiName");   
            ViewData["]]><xsl:value-of select="NameFK5"/><![CDATA[DDL"] = new SelectList(query2.ToList(), "]]><xsl:value-of select="IDFK5"/><![CDATA[", "DDLPlName");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK6"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK6"/><![CDATA[Repository.]]><xsl:value-of select="TableFK6"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK6"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK6"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK6"/><![CDATA[ = d.]]><xsl:value-of select="NameFK6"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK6"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK6"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK6"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK7"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK7"/><![CDATA[Repository.]]><xsl:value-of select="TableFK7"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK7"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK7"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK7"/><![CDATA[ = d.]]><xsl:value-of select="NameFK7"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK7"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK7"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK7"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK8"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK8"/><![CDATA[Repository.]]><xsl:value-of select="TableFK8"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK8"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK8"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK8"/><![CDATA[ = d.]]><xsl:value-of select="NameFK8"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK8"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK8"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK8"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK9"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK9"/><![CDATA[Repository.]]><xsl:value-of select="TableFK9"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK9"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK9"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK9"/><![CDATA[ = d.]]><xsl:value-of select="NameFK9"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK9"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK9"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK9"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK10"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK10"/><![CDATA[Repository.]]><xsl:value-of select="TableFK10"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK10"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK10"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK10"/><![CDATA[ = d.]]><xsl:value-of select="NameFK10"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK10"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK10"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK10"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK11"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK11"/><![CDATA[Repository.]]><xsl:value-of select="TableFK11"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK11"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK11"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK11"/><![CDATA[ = d.]]><xsl:value-of select="NameFK11"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK11"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK11"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK11"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK12"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK12"/><![CDATA[Repository.]]><xsl:value-of select="TableFK12"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK12"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK12"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK12"/><![CDATA[ = d.]]><xsl:value-of select="NameFK12"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK12"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK12"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK12"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK13"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK13"/><![CDATA[Repository.]]><xsl:value-of select="TableFK13"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK13"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK13"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK13"/><![CDATA[ = d.]]><xsl:value-of select="NameFK13"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK13"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK13"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK13"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK14"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK14"/><![CDATA[Repository.]]><xsl:value-of select="TableFK14"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK14"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK14"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK14"/><![CDATA[ = d.]]><xsl:value-of select="NameFK14"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK14"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK14"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK14"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK15"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK15"/><![CDATA[Repository.]]><xsl:value-of select="TableFK15"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK15"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK15"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK15"/><![CDATA[ = d.]]><xsl:value-of select="NameFK15"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK15"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK15"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK15"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK16"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK16"/><![CDATA[Repository.]]><xsl:value-of select="TableFK16"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK16"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK16"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK16"/><![CDATA[ = d.]]><xsl:value-of select="NameFK16"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK16"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK16"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK16"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK17"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK17"/><![CDATA[Repository.]]><xsl:value-of select="TableFK17"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK17"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK17"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK17"/><![CDATA[ = d.]]><xsl:value-of select="NameFK17"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK17"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK17"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK17"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK18"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK18"/><![CDATA[Repository.]]><xsl:value-of select="TableFK18"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK18"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK18"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK18"/><![CDATA[ = d.]]><xsl:value-of select="NameFK18"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK18"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK18"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK18"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK19"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK19"/><![CDATA[Repository.]]><xsl:value-of select="TableFK19"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK19"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK19"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK19"/><![CDATA[ = d.]]><xsl:value-of select="NameFK19"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK19"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK19"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK19"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK20"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK20"/><![CDATA[Repository.]]><xsl:value-of select="TableFK20"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK20"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK20"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK20"/><![CDATA[ = d.]]><xsl:value-of select="NameFK20"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK20"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK20"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK20"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK21"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK21"/><![CDATA[Repository.]]><xsl:value-of select="TableFK21"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK21"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK21"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK21"/><![CDATA[ = d.]]><xsl:value-of select="NameFK21"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK21"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK21"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK21"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK22"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK22"/><![CDATA[Repository.]]><xsl:value-of select="TableFK22"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK22"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK22"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK22"/><![CDATA[ = d.]]><xsl:value-of select="NameFK22"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK22"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK22"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK22"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK23"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK23"/><![CDATA[Repository.]]><xsl:value-of select="TableFK23"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK23"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK23"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK23"/><![CDATA[ = d.]]><xsl:value-of select="NameFK23"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK23"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK23"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK23"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK24"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK24"/><![CDATA[Repository.]]><xsl:value-of select="TableFK24"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK24"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK24"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK24"/><![CDATA[ = d.]]><xsl:value-of select="NameFK24"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK24"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK24"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK24"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK25"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK25"/><![CDATA[Repository.]]><xsl:value-of select="TableFK25"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK25"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK25"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK25"/><![CDATA[ = d.]]><xsl:value-of select="NameFK25"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK25"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK25"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK25"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK26"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK26"/><![CDATA[Repository.]]><xsl:value-of select="TableFK26"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK26"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK26"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK26"/><![CDATA[ = d.]]><xsl:value-of select="NameFK26"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK26"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK26"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK26"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK27"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK27"/><![CDATA[Repository.]]><xsl:value-of select="TableFK27"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK27"/><![CDATA[, d.Subregnum
                         select new    {
                             d.]]><xsl:value-of select="IDFK27"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK27"/><![CDATA[ = d.]]><xsl:value-of select="NameFK27"/><![CDATA[ + " " + d.Subregnum
                         };
            ViewData["]]><xsl:value-of select="NameFK27"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK27"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK27"/><![CDATA[");   
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void TempDataMessage(]]><xsl:value-of select="EntityAbl"/><![CDATA[, string message)      {
            var refId = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[;
            var refExpertId = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK1"/><![CDATA[;
            var refSourceId = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK2"/><![CDATA[;
            var refAuthorId = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK3"/><![CDATA[;
            var refExpert = string.Empty;
            var refSource = string.Empty;
            var refAuthor = string.Empty;
            if (refExpertId != null)
                { refExpert = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[; }
            if (refSourceId != null)
                { refSource = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="NameFK2"/><![CDATA[; }
            if (refAuthorId != null)
                { refAuthor = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK3"/><![CDATA[.]]><xsl:value-of select="NameFK3"/><![CDATA[; }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK27"/><![CDATA[ != null)    {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK27"/><![CDATA[.]]><xsl:value-of select="NameFK27"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK27"/><![CDATA[.Subregnum + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK27"/><![CDATA[.]]><xsl:value-of select="NameFK27"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK27"/><![CDATA[.Subregnum  + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK27"/><![CDATA[.]]><xsl:value-of select="NameFK27"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK27"/><![CDATA[.Subregnum  + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK26"/><![CDATA[ != null)    {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK26"/><![CDATA[.]]><xsl:value-of select="NameFK26"/><![CDATA[ + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK26"/><![CDATA[.]]><xsl:value-of select="NameFK26"/><![CDATA[ + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK26"/><![CDATA[.]]><xsl:value-of select="NameFK26"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK25"/><![CDATA[ != null)    {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK25"/><![CDATA[.]]><xsl:value-of select="NameFK25"/><![CDATA[ + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK25"/><![CDATA[.]]><xsl:value-of select="NameFK25"/><![CDATA[ + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK25"/><![CDATA[.]]><xsl:value-of select="NameFK25"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK24"/><![CDATA[ != null)    {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK24"/><![CDATA[.]]><xsl:value-of select="NameFK24"/><![CDATA[ + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK24"/><![CDATA[.]]><xsl:value-of select="NameFK24"/><![CDATA[ + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK24"/><![CDATA[.]]><xsl:value-of select="NameFK24"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK23"/><![CDATA[ != null)    {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK23"/><![CDATA[.]]><xsl:value-of select="NameFK23"/><![CDATA[ + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK23"/><![CDATA[.]]><xsl:value-of select="NameFK23"/><![CDATA[ + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK23"/><![CDATA[.]]><xsl:value-of select="NameFK23"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK22"/><![CDATA[ != null)    {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK22"/><![CDATA[.]]><xsl:value-of select="NameFK22"/><![CDATA[ + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK22"/><![CDATA[.]]><xsl:value-of select="NameFK22"/><![CDATA[ + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK22"/><![CDATA[.]]><xsl:value-of select="NameFK22"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK21"/><![CDATA[ != null)    {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK21"/><![CDATA[.]]><xsl:value-of select="NameFK21"/><![CDATA[ + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK21"/><![CDATA[.]]><xsl:value-of select="NameFK21"/><![CDATA[ + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK21"/><![CDATA[.]]><xsl:value-of select="NameFK21"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK20"/><![CDATA[ != null)    {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK20"/><![CDATA[.]]><xsl:value-of select="NameFK20"/><![CDATA[ + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK20"/><![CDATA[.]]><xsl:value-of select="NameFK20"/><![CDATA[ + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK20"/><![CDATA[.]]><xsl:value-of select="NameFK20"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK19"/><![CDATA[ != null)    {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK19"/><![CDATA[.]]><xsl:value-of select="NameFK19"/><![CDATA[ + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK19"/><![CDATA[.]]><xsl:value-of select="NameFK19"/><![CDATA[ + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK19"/><![CDATA[.]]><xsl:value-of select="NameFK19"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK18"/><![CDATA[ != null)    {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK18"/><![CDATA[.]]><xsl:value-of select="NameFK18"/><![CDATA[ + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK18"/><![CDATA[.]]><xsl:value-of select="NameFK18"/><![CDATA[ + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK18"/><![CDATA[.]]><xsl:value-of select="NameFK18"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK17"/><![CDATA[ != null)     {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK17"/><![CDATA[.]]><xsl:value-of select="NameFK17"/><![CDATA[ + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK17"/><![CDATA[.]]><xsl:value-of select="NameFK17"/><![CDATA[ + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK17"/><![CDATA[.]]><xsl:value-of select="NameFK17"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK16"/><![CDATA[ != null)    {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK16"/><![CDATA[.]]><xsl:value-of select="NameFK16"/><![CDATA[ + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK16"/><![CDATA[.]]><xsl:value-of select="NameFK16"/><![CDATA[ + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK16"/><![CDATA[.]]><xsl:value-of select="NameFK16"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK15"/><![CDATA[ != null)     {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK15"/><![CDATA[.]]><xsl:value-of select="NameFK15"/><![CDATA[ + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK15"/><![CDATA[.]]><xsl:value-of select="NameFK15"/><![CDATA[ + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK15"/><![CDATA[.]]><xsl:value-of select="NameFK15"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK14"/><![CDATA[ != null)     {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK14"/><![CDATA[.]]><xsl:value-of select="NameFK14"/><![CDATA[ + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK14"/><![CDATA[.]]><xsl:value-of select="NameFK14"/><![CDATA[ + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK14"/><![CDATA[.]]><xsl:value-of select="NameFK14"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK13"/><![CDATA[ != null)    {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK13"/><![CDATA[.]]><xsl:value-of select="NameFK13"/><![CDATA[ + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK13"/><![CDATA[.]]><xsl:value-of select="NameFK13"/><![CDATA[ + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK13"/><![CDATA[.]]><xsl:value-of select="NameFK13"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK12"/><![CDATA[ != null)    {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK12"/><![CDATA[.]]><xsl:value-of select="NameFK12"/><![CDATA[ + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK12"/><![CDATA[.]]><xsl:value-of select="NameFK12"/><![CDATA[ + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK12"/><![CDATA[.]]><xsl:value-of select="NameFK12"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK11"/><![CDATA[ != null)     {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK11"/><![CDATA[.]]><xsl:value-of select="NameFK11"/><![CDATA[ + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK11"/><![CDATA[.]]><xsl:value-of select="NameFK11"/><![CDATA[ + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK11"/><![CDATA[.]]><xsl:value-of select="NameFK11"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK10"/><![CDATA[ != null)    {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK10"/><![CDATA[.]]><xsl:value-of select="NameFK10"/><![CDATA[ + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK10"/><![CDATA[.]]><xsl:value-of select="NameFK10"/><![CDATA[ + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK10"/><![CDATA[.]]><xsl:value-of select="NameFK10"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK9"/><![CDATA[ != null)     {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK9"/><![CDATA[.]]><xsl:value-of select="NameFK9"/><![CDATA[ + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK9"/><![CDATA[.]]><xsl:value-of select="NameFK9"/><![CDATA[ + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK9"/><![CDATA[.]]><xsl:value-of select="NameFK9"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK8"/><![CDATA[ != null)     {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK8"/><![CDATA[.]]><xsl:value-of select="NameFK8"/><![CDATA[ + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK8"/><![CDATA[.]]><xsl:value-of select="NameFK8"/><![CDATA[ + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK8"/><![CDATA[.]]><xsl:value-of select="NameFK8"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK7"/><![CDATA[ != null)   {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK7"/><![CDATA[.]]><xsl:value-of select="NameFK7"/><![CDATA[ + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK7"/><![CDATA[.]]><xsl:value-of select="NameFK7"/><![CDATA[ + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK7"/><![CDATA[.]]><xsl:value-of select="NameFK7"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK6"/><![CDATA[ != null)    {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK6"/><![CDATA[.]]><xsl:value-of select="NameFK6"/><![CDATA[ + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK6"/><![CDATA[.]]><xsl:value-of select="NameFK6"/><![CDATA[ + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK6"/><![CDATA[.]]><xsl:value-of select="NameFK6"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK5"/><![CDATA[ != null)     {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="NameFK5"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.Divers + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="NameFK5"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.Divers + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="NameFK5"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.Divers + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK4"/><![CDATA[ != null)    {
                if (refExpertId != null)
                    TempData["message"] = refId + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="NameFK4"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.Divers + " " + message;
                if (refSourceId != null)
                    TempData["message"] = refId + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="NameFK4"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.Divers + " " + message;
                if (refAuthorId != null)
                    TempData["message"] = refId + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="NameFK4"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.Divers + " " + message;
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">  
   <![CDATA[     
        private void ViewDataSpeciessesGetValue()    {
            var query1 = from d in ]]><xsl:value-of select="EntitysFK4"/><![CDATA[Repository.]]><xsl:value-of select="TableFK4"/><![CDATA[
                         orderby d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[, d.]]><xsl:value-of select="NameFK4"/><![CDATA[, d.Subspecies, d.Divers     
                         select new    {
                             d.]]><xsl:value-of select="IDFK4"/><![CDATA[,
                             DDLFiName = d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + d.]]><xsl:value-of select="NameFK4"/><![CDATA[ + " " + d.Subspecies + " " + d.Divers
                         };
            var query2 = from d in ]]><xsl:value-of select="EntitysFK5"/><![CDATA[Repository.]]><xsl:value-of select="TableFK5"/><![CDATA[
                         orderby d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[, d.]]><xsl:value-of select="NameFK5"/><![CDATA[, d.Subspecies, d.Divers     
                         select new    {
                             d.]]><xsl:value-of select="IDFK5"/><![CDATA[,
                             DDLPlName = d.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + d.]]><xsl:value-of select="NameFK5"/><![CDATA[ + " " + d.Subspecies + " " + d.Divers
                         };
            ViewData["]]><xsl:value-of select="NameFK4"/><![CDATA[DDL"] = new SelectList(query1.ToList(), "]]><xsl:value-of select="IDFK4"/><![CDATA[", "DDLFiName");   
            ViewData["]]><xsl:value-of select="NameFK5"/><![CDATA[DDL"] = new SelectList(query2.ToList(), "]]><xsl:value-of select="IDFK5"/><![CDATA[", "DDLPlName");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK6"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK6"/><![CDATA[Repository.]]><xsl:value-of select="TableFK6"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK6"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK6"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK6"/><![CDATA[ = d.]]><xsl:value-of select="NameFK6"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK6"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK6"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK6"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK7"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK7"/><![CDATA[Repository.]]><xsl:value-of select="TableFK7"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK7"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK7"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK7"/><![CDATA[ = d.]]><xsl:value-of select="NameFK7"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK7"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK7"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK7"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK8"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK8"/><![CDATA[Repository.]]><xsl:value-of select="TableFK8"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK8"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK8"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK8"/><![CDATA[ = d.]]><xsl:value-of select="NameFK8"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK8"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK8"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK8"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK9"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK9"/><![CDATA[Repository.]]><xsl:value-of select="TableFK9"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK9"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK9"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK9"/><![CDATA[ = d.]]><xsl:value-of select="NameFK9"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK9"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK9"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK9"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK10"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK10"/><![CDATA[Repository.]]><xsl:value-of select="TableFK10"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK10"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK10"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK10"/><![CDATA[ = d.]]><xsl:value-of select="NameFK10"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK10"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK10"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK10"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK11"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK11"/><![CDATA[Repository.]]><xsl:value-of select="TableFK11"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK11"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK11"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK11"/><![CDATA[ = d.]]><xsl:value-of select="NameFK11"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK11"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK11"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK11"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK12"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK12"/><![CDATA[Repository.]]><xsl:value-of select="TableFK12"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK12"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK12"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK12"/><![CDATA[ = d.]]><xsl:value-of select="NameFK12"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK12"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK12"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK12"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK13"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK13"/><![CDATA[Repository.]]><xsl:value-of select="TableFK13"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK13"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK13"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK13"/><![CDATA[ = d.]]><xsl:value-of select="NameFK13"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK13"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK13"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK13"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK14"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK14"/><![CDATA[Repository.]]><xsl:value-of select="TableFK14"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK14"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK14"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK14"/><![CDATA[ = d.]]><xsl:value-of select="NameFK14"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK14"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK14"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK14"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK15"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK15"/><![CDATA[Repository.]]><xsl:value-of select="TableFK15"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK15"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK15"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK15"/><![CDATA[ = d.]]><xsl:value-of select="NameFK15"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK15"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK15"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK15"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK16"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK16"/><![CDATA[Repository.]]><xsl:value-of select="TableFK16"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK16"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK16"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK16"/><![CDATA[ = d.]]><xsl:value-of select="NameFK16"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK16"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK16"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK16"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK17"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK17"/><![CDATA[Repository.]]><xsl:value-of select="TableFK17"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK17"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK17"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK17"/><![CDATA[ = d.]]><xsl:value-of select="NameFK17"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK17"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK17"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK17"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK18"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK18"/><![CDATA[Repository.]]><xsl:value-of select="TableFK18"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK18"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK18"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK18"/><![CDATA[ = d.]]><xsl:value-of select="NameFK18"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK18"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK18"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK18"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK19"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK19"/><![CDATA[Repository.]]><xsl:value-of select="TableFK19"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK19"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK19"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK19"/><![CDATA[ = d.]]><xsl:value-of select="NameFK19"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK19"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK19"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK19"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK20"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK20"/><![CDATA[Repository.]]><xsl:value-of select="TableFK20"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK20"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK20"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK20"/><![CDATA[ = d.]]><xsl:value-of select="NameFK20"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK20"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK20"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK20"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK21"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK21"/><![CDATA[Repository.]]><xsl:value-of select="TableFK21"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK21"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK21"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK21"/><![CDATA[ = d.]]><xsl:value-of select="NameFK21"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK21"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK21"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK21"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK22"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK22"/><![CDATA[Repository.]]><xsl:value-of select="TableFK22"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK22"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK22"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK22"/><![CDATA[ = d.]]><xsl:value-of select="NameFK22"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK22"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK22"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK22"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK23"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK23"/><![CDATA[Repository.]]><xsl:value-of select="TableFK23"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK23"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK23"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK23"/><![CDATA[ = d.]]><xsl:value-of select="NameFK23"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK23"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK23"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK23"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK24"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK24"/><![CDATA[Repository.]]><xsl:value-of select="TableFK24"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK24"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK24"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK24"/><![CDATA[ = d.]]><xsl:value-of select="NameFK24"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK24"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK24"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK24"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK25"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK25"/><![CDATA[Repository.]]><xsl:value-of select="TableFK25"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK25"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK25"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK25"/><![CDATA[ = d.]]><xsl:value-of select="NameFK25"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK25"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK25"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK25"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK26"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK26"/><![CDATA[Repository.]]><xsl:value-of select="TableFK26"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK26"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK26"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK26"/><![CDATA[ = d.]]><xsl:value-of select="NameFK26"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK26"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK26"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK26"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK27"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK27"/><![CDATA[Repository.]]><xsl:value-of select="TableFK27"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK27"/><![CDATA[, d.Subregnum
                         select new    {
                             d.]]><xsl:value-of select="IDFK27"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK27"/><![CDATA[ = d.]]><xsl:value-of select="NameFK27"/><![CDATA[ + " " + d.Subregnum
                         };
            ViewData["]]><xsl:value-of select="NameFK27"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK27"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK27"/><![CDATA[");   
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void TempDataMessage(]]><xsl:value-of select="EntityAbl"/><![CDATA[, string message)       {
            var commentId = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[;
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK27"/><![CDATA[ != null)    {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK27"/><![CDATA[.]]><xsl:value-of select="NameFK27"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK27"/><![CDATA[.Subregnum + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK26"/><![CDATA[ != null)    {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK26"/><![CDATA[.]]><xsl:value-of select="NameFK26"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK25"/><![CDATA[ != null)    {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK25"/><![CDATA[.]]><xsl:value-of select="NameFK25"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK24"/><![CDATA[ != null)    {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK24"/><![CDATA[.]]><xsl:value-of select="NameFK24"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK23"/><![CDATA[ != null)    {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK23"/><![CDATA[.]]><xsl:value-of select="NameFK23"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK22"/><![CDATA[ != null)    {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK22"/><![CDATA[.]]><xsl:value-of select="NameFK22"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK21"/><![CDATA[ != null)    {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK21"/><![CDATA[.]]><xsl:value-of select="NameFK21"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK20"/><![CDATA[ != null)    {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK20"/><![CDATA[.]]><xsl:value-of select="NameFK20"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK19"/><![CDATA[ != null)    {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK19"/><![CDATA[.]]><xsl:value-of select="NameFK19"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK18"/><![CDATA[ != null)    {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK18"/><![CDATA[.]]><xsl:value-of select="NameFK18"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK17"/><![CDATA[ != null)     {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK17"/><![CDATA[.]]><xsl:value-of select="NameFK17"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK16"/><![CDATA[ != null)    {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK16"/><![CDATA[.]]><xsl:value-of select="NameFK16"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK15"/><![CDATA[ != null)     {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK15"/><![CDATA[.]]><xsl:value-of select="NameFK15"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK14"/><![CDATA[ != null)     {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK14"/><![CDATA[.]]><xsl:value-of select="NameFK14"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK13"/><![CDATA[ != null)    {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK13"/><![CDATA[.]]><xsl:value-of select="NameFK13"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK12"/><![CDATA[ != null)    {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK12"/><![CDATA[.]]><xsl:value-of select="NameFK12"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK11"/><![CDATA[ != null)     {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK11"/><![CDATA[.]]><xsl:value-of select="NameFK11"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK10"/><![CDATA[ != null)    {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK10"/><![CDATA[.]]><xsl:value-of select="NameFK10"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK9"/><![CDATA[ != null)     {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK9"/><![CDATA[.]]><xsl:value-of select="NameFK9"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK8"/><![CDATA[ != null)     {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK8"/><![CDATA[.]]><xsl:value-of select="NameFK8"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK7"/><![CDATA[ != null)   {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK7"/><![CDATA[.]]><xsl:value-of select="NameFK7"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK6"/><![CDATA[ != null)    {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK6"/><![CDATA[.]]><xsl:value-of select="NameFK6"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK5"/><![CDATA[ != null)     {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="NameFK5"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.Divers + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK4"/><![CDATA[ != null)    {
                    TempData["message"] = commentId + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="NameFK4"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.Divers + " " + message;
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">  
   <![CDATA[     //
        //Image or Video from Filestream to show
        public FileContentResult GetFilestream1(int id)       {
            var imageVideo = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.]]><xsl:value-of select="Table"/><![CDATA[.First(x => x.UserProfileID == id);
            if (imageVideo == null) throw new ArgumentNullException(SharedRes.StringsRes.ErrorNullException);
            //imageVideo darf nicht NULL sein passiert wenn Bild zu groß bei upload oder bei video
            return File(imageVideo.Filestream, imageVideo.ImageMimeType);  
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewDataMimeTypesGetValue(]]><xsl:value-of select="EntityAbl"/><![CDATA[)  {
            var mimeTypes = new[]  {
                                    "jpg",
                                    "JPG",
                                    "png",
                                    "bmp",
                                    "tiff",
                                    "gif",
                                    "icon",
                                    "jpeg",
                                    "wmf",
                                    "wmv",
                                    "mpg",
                                    "mp4",
                                    "avi",
                                    "mov",
                                    "swf",
                                    "flv"
                                };

            ViewData["mimeTypes"] = new SelectList(mimeTypes, ]]><xsl:value-of select="Entity"/><![CDATA[.ImageMimeType);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  
       
        private void ViewDataGenderGetValue(]]><xsl:value-of select="EntityAbl"/><![CDATA[)  {

            var genders = new[]  {

                ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.GenderMale,
                ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.GenderFemale
            };
            ViewData["genders"] = new SelectList(genders, tbluserprofile.Gender);
        }       
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  
       
        private void ViewDataTitleGetValue(]]><xsl:value-of select="EntityAbl"/><![CDATA[)  {

            var titles = new[]  {

                ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Title01,
                ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Title02,
                ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Title03,
                ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Title04,
                ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Title05,
                ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Title06,
                ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Title07
            };
            ViewData["titles"] = new SelectList(titles, tbluserprofile.Title);
        }       
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='GUID+++++++++Details+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='AspnetApplications'">    
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Details/5     
        public ActionResult Details(Guid id)    { 
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id); 
   
             return ]]><xsl:value-of select="Entity"/><![CDATA[ == null ? View("NotFound") : View("Details", ]]><xsl:value-of select="Entity"/><![CDATA[);
        }      ]]>
<![CDATA[        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------   ]]>
</xsl:when>
<xsl:when test="Table ='AspnetMemberships'">      
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Details/5     
        public ActionResult Details(Guid id)    { 
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);   
 
             return ]]><xsl:value-of select="Entity"/><![CDATA[ == null ? View("NotFound") : View("Details", ]]><xsl:value-of select="Entity"/><![CDATA[);
        }      ]]>
<![CDATA[        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------   ]]>
</xsl:when>
<xsl:when test="Table ='AspnetUsers'">      
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Details/5     
        public ActionResult Details(Guid id)    { 
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);   
 
             return ]]><xsl:value-of select="Entity"/><![CDATA[ == null ? View("NotFound") : View("Details", ]]><xsl:value-of select="Entity"/><![CDATA[);
        }      ]]>
<![CDATA[        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------   ]]>
</xsl:when>
<xsl:otherwise>  
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Details/5     
        public ActionResult Details(int id)    {  
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);   
 
             return ]]><xsl:value-of select="Entity"/><![CDATA[ == null ? View("NotFound") : View("Details", ]]><xsl:value-of select="Entity"/><![CDATA[);
        }      ]]>
<![CDATA[        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------   ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Info++++ROLE +++++++++++++++++AspnetUsers+++++++++++'">        
</xsl:when>
<xsl:when test="Table ='AspnetUsers'">   
<![CDATA[        //
        // Get:/AspnetUsers/Role/Edit
        [Authorize(Roles = "Administrator")]
        public ActionResult Role(Guid id )    {
            var aspnetUser = _aspnetUsersRepository.Get(id);

            return View(aspnetUser);
        }

        //
        // Post:/AspnetUsers/Role/Edit
        [HttpPost, Authorize(Roles = "Administrator")]
        public ActionResult Role(Guid id, FormCollection fc)    {
            var aspnetUser = _aspnetUsersRepository.Get(id);
            //Change Role for Administrator, Developer, Zoologist, Biologist, User 

            var userName = aspnetUser.UserName;

            if (Roles.IsUserInRole(userName, "Administrator"))
                Roles.RemoveUserFromRole(userName, "Administrator");
            if (Roles.IsUserInRole(userName, "Developer"))
                Roles.RemoveUserFromRole(userName, "Developer");
            if (Roles.IsUserInRole(userName, "Zoologist"))
                Roles.RemoveUserFromRole(userName, "Zoologist");
            if (Roles.IsUserInRole(userName, "Biologist"))
                Roles.RemoveUserFromRole(userName, "Biologist");
            if (Roles.IsUserInRole(userName, "User"))
                Roles.RemoveUserFromRole(userName, "User");

            var cb1 = fc["Administrator"];
            var cb2 = fc["Developer"];
            var cb3 = fc["Zoologist"];
            var cb4 = fc["Biologist"];
            var cb5 = fc["User"];

            if (cb1 == "true,false")   {
                Roles.AddUserToRole(userName, "Administrator");
            }
            if (cb2 == "true,false")    {
                Roles.AddUserToRole(userName, "Developer");
            }
            if (cb3 == "true,false")    {
                Roles.AddUserToRole(userName, "Zoologist");
            }
            if (cb4 == "true,false")   {
                Roles.AddUserToRole(userName, "Biologist");
            }
            if (cb5 == "true,false")   {
                Roles.AddUserToRole(userName, "User");
            }
            return RedirectToAction("Index");
        }
        //-------------------------------------------------------------------------    ]]>
</xsl:when>
<xsl:otherwise> 
       <![CDATA[   <!-- _Form.cshtml  Skriptdatum: ]]><xsl:value-of select="DateTime"/>  <![CDATA[  -->    ]]> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='DDLName +++++++++++++++++CREATE+++++++++++ GET+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='AspnetApplications'">   
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()              {   
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[();   

             return View(]]><xsl:value-of select="Entity"/><![CDATA[);
        }     ]]>
</xsl:when>
<xsl:when test="Table ='AspnetMemberships'">   
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()              {   
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[();   
                 ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK1"/><![CDATA[", "]]><xsl:value-of select="NameFK1"/><![CDATA[");    
                 ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK2"/><![CDATA[", "]]><xsl:value-of select="NameFK2"/><![CDATA[");    

             return View(]]><xsl:value-of select="Entity"/><![CDATA[);
        }     ]]>
</xsl:when>
<xsl:when test="Table ='AspnetUsers'">   
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()              {   
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[();   
                 ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK1"/><![CDATA[", "]]><xsl:value-of select="NameFK1"/><![CDATA[");    
             return View(]]><xsl:value-of select="Entity"/><![CDATA[);
        }     ]]>
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">   
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create()              {   
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[();   

             return View(]]><xsl:value-of select="Entity"/><![CDATA[);
        }     ]]>
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">   
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create(int ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id)              {   
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[ {]]><xsl:value-of select="IDFK1"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id};         
             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  

             return View(]]><xsl:value-of select="Entity"/><![CDATA[);
        }     ]]>
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">    
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create(int ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id)              {   
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[ {]]><xsl:value-of select="IDFK1"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id};         
             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  

             return View(]]><xsl:value-of select="Entity"/><![CDATA[);
        }     ]]>
</xsl:when>
<xsl:when test="Table ='Tbl18Superclasses'">    
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create(int ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id, int ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id)              {   
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[
                 {]]><xsl:value-of select="IDFK1"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id,         
                  ]]><xsl:value-of select="IDFK2"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id};         
                 ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK1"/><![CDATA[", "]]><xsl:value-of select="NameFK1"/><![CDATA[");    
                 ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK2"/><![CDATA[", "]]><xsl:value-of select="NameFK2"/><![CDATA[");    

             return View(]]><xsl:value-of select="Entity"/><![CDATA[);
        }     ]]>
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">    
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create()              {   
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[();   
             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  

             return View(]]><xsl:value-of select="Entity"/><![CDATA[);
        }     ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">    
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create()              {   
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[();   
             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  

             return View(]]><xsl:value-of select="Entity"/><![CDATA[);
        }     ]]>
</xsl:when>
<xsl:when test="Table ='Tbl78Names'"> 
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create(int ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id, int ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id)              {   
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[    {
                   ]]><xsl:value-of select="IDFK1"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id,         
                   ]]><xsl:value-of select="IDFK2"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id
             };         
             ViewDataSpeciesGetValue();     
             ViewDataLanguagesGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  

             return View(]]><xsl:value-of select="Entity"/><![CDATA[);
        }     ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create(int ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id, int ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id)              {   
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[    {
                   ]]><xsl:value-of select="IDFK1"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id,         
                   ]]><xsl:value-of select="IDFK2"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id
             };         
             ViewDataSpeciesGetValue();  
             ViewDataMimeTypesGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  

             return View(]]><xsl:value-of select="Entity"/><![CDATA[);
        }     ]]>
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'"> 
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create(int ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id, int ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id)              {   
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[    {
                   ]]><xsl:value-of select="IDFK1"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id,         
                   ]]><xsl:value-of select="IDFK2"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id
             };         
             ViewDataSpeciesGetValue();  

             return View(]]><xsl:value-of select="Entity"/><![CDATA[);
        }     ]]>
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'"> 
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create(int ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id, int ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id)              {   
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[    {
                   ]]><xsl:value-of select="IDFK1"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id,         
                   ]]><xsl:value-of select="IDFK2"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id
             };         
             ViewDataSpeciesGetValue();  
             ViewData["Countries"] = new SelectList(PhoneValidator.Countries, ]]><xsl:value-of select="Entity"/><![CDATA[.Country);   

             return View(]]><xsl:value-of select="Entity"/><![CDATA[);
        }     ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90References'">
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create(int ]]><xsl:value-of select="BasisSmFK4"/><![CDATA[Id, int ]]><xsl:value-of select="BasisSmFK5"/><![CDATA[Id)              {   
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[    {
                   ]]><xsl:value-of select="IDFK4"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK4"/><![CDATA[Id,         
                   ]]><xsl:value-of select="IDFK5"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK5"/><![CDATA[Id
             };         ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK1"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK2"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK3"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewDataSpeciessesGetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK6"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK7"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK8"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK9"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK10"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK11"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK12"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK13"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK14"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK15"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK16"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK17"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK18"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK19"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK20"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK21"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK22"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK23"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK24"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK25"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK26"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK27"/><![CDATA[GetValue();  

             return View(]]><xsl:value-of select="Entity"/><![CDATA[);
        }     ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">   
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create()              {   
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[();   

             return View(]]><xsl:value-of select="Entity"/><![CDATA[);
        }     ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">   
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create()              {   
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[();   

             return View(]]><xsl:value-of select="Entity"/><![CDATA[);
        }     ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">   
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create()              {   
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[();   

             return View(]]><xsl:value-of select="Entity"/><![CDATA[);
        }     ]]>
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create(int ]]><xsl:value-of select="BasisSmFK4"/><![CDATA[Id, int ]]><xsl:value-of select="BasisSmFK5"/><![CDATA[Id)              {   
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[    {
                   ]]><xsl:value-of select="IDFK4"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK4"/><![CDATA[Id,         
                   ]]><xsl:value-of select="IDFK5"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK5"/><![CDATA[Id
             };         ]]>
<![CDATA[             ViewDataSpeciessesGetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK6"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK7"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK8"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK9"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK10"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK11"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK12"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK13"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK14"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK15"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK16"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK17"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK18"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK19"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK20"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK21"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK22"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK23"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK24"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK25"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK26"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK27"/><![CDATA[GetValue();  

             return View(]]><xsl:value-of select="Entity"/><![CDATA[);
        }     ]]>
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">   
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()              {   
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[();   

            ViewData["UserNameDDL"] = new SelectList(_aspnetUsersRepository.FindAllSort().ToList(), "UserId", "UserName");
            ViewDataMimeTypesGetValue(tbluserprofile);
            ViewData["Countries"] = new SelectList(PhoneValidator.Countries, tbluserprofile.Country);
            ViewDataGenderGetValue(tbluserprofile);
            ViewDataTitleGetValue(tbluserprofile);

             return View(]]><xsl:value-of select="Entity"/><![CDATA[);
        }     ]]>
</xsl:when>
<xsl:otherwise>  
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create(int ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id)              {   
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[ {]]><xsl:value-of select="IDFK1"/><![CDATA[ = ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id};         ]]>  
    <xsl:if test="TableFK1 !='NULL'"><![CDATA[         ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK1"/><![CDATA[", "]]><xsl:value-of select="NameFK1"/><![CDATA[");    ]]>
    </xsl:if> 
    <xsl:if test="TableFK2 !='NULL'"><![CDATA[         ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK2"/><![CDATA[", "]]><xsl:value-of select="NameFK2"/><![CDATA[");      ]]>
    </xsl:if> 
<![CDATA[             return View(]]><xsl:value-of select="Entity"/><![CDATA[);
        }     ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='HttpPostedFileBase++++++POST++++++++++++++++++++++++++Create+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='AspnetApplications'">  
<![CDATA[        //       
        // POST: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [HttpPost, Authorize(Roles = "Administrator")]   ]]>
<![CDATA[        public ActionResult Create([Bind(Exclude = "]]><xsl:value-of select="ID"/><![CDATA[")]FormCollection collection)    {               ]]>
</xsl:when>
<xsl:when test="Table ='AspnetMemberships'">  
<![CDATA[        //       
        // POST: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [HttpPost, Authorize(Roles = "Administrator")]   ]]>
<![CDATA[        public ActionResult Create([Bind(Exclude = "]]><xsl:value-of select="ID"/><![CDATA[")]FormCollection collection)    {               ]]>
</xsl:when>
<xsl:when test="Table ='AspnetUsers'">  
<![CDATA[        //       
        // POST: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [HttpPost, Authorize(Roles = "Administrator")]   ]]>
<![CDATA[        public ActionResult Create([Bind(Exclude = "]]><xsl:value-of select="ID"/><![CDATA[")]FormCollection collection)    {               ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
<![CDATA[        //       
        // POST: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   ]]>  
<![CDATA[        public ActionResult Create([Bind(Exclude = "]]><xsl:value-of select="ID"/><![CDATA[")]HttpPostedFileBase image)    {       ]]>
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">  
<![CDATA[        //       
        // POST: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [HttpPost, Authorize(Roles = "Administrator")]   ]]>
<![CDATA[        public ActionResult Create([Bind(Exclude = "]]><xsl:value-of select="ID"/><![CDATA[")]HttpPostedFileBase image)    {               ]]>
</xsl:when>
<xsl:otherwise>  
<![CDATA[        //       
        // POST: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   ]]>
<![CDATA[        public ActionResult Create([Bind(Exclude = "]]><xsl:value-of select="ID"/><![CDATA[")]FormCollection collection)    {               ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Counter+Writer+Updater++++++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='AspnetApplications'">  
    <![CDATA[        var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[();   ]]>
</xsl:when>
<xsl:when test="Table ='AspnetMemberships'">   
    <![CDATA[        var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[();   ]]>
</xsl:when>
<xsl:when test="Table ='AspnetUsers'">   
    <![CDATA[        var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[();   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'"> 
    <![CDATA[        var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[       {
                CountID =_tblCountersRepository.Counter(),     
                FilestreamID = Guid.NewGuid(),                                  
                Writer = User.Identity.Name,
                WriterDate = DateTime.Now,
                Updater = User.Identity.Name,
                UpdaterDate = DateTime.Now  
            };  ]]>                          
</xsl:when>
<xsl:when test="Table ='TblCounters'">  
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
    <![CDATA[        var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[       {
                CountID =_tblCountersRepository.Counter(),     
                FilestreamID = Guid.NewGuid(),                                  
                Writer = User.Identity.Name,
                WriterDate = DateTime.Now,
                Updater = User.Identity.Name,
                UpdaterDate = DateTime.Now  
            };  ]]>                          
</xsl:when>
<xsl:otherwise>  
    <![CDATA[        var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[       {
                CountID =_tblCountersRepository.Counter(),                  
                Writer = User.Identity.Name,
                WriterDate = DateTime.Now,
                Updater = User.Identity.Name,
                UpdaterDate = DateTime.Now  
            };  ]]>                         
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='DDLName Create Post+++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">    
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">    
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">    
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">    
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">    
<![CDATA[             ViewDataSpeciesGetValue();  ]]>
<![CDATA[             ViewDataLanguagesGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
<![CDATA[            var imageVideoToDatabase = new WebImage(image.InputStream);  ]]>

<![CDATA[             ViewDataSpeciesGetValue();  ]]>
<![CDATA[             ViewDataMimeTypesGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'"> 
<![CDATA[             ViewDataSpeciesGetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
<![CDATA[             ViewDataSpeciesGetValue();  ]]>
<![CDATA[             ViewData["Countries"] = new SelectList(PhoneValidator.Countries, ]]><xsl:value-of select="Entity"/><![CDATA[.Country);   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90References'">  
<![CDATA[             var tables = new[] {
                         "]]><xsl:value-of select="EntitysFK4"/><![CDATA[Repository.]]><xsl:value-of select="TableFK4"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK5"/><![CDATA[Repository.]]><xsl:value-of select="TableFK5"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK6"/><![CDATA[Repository.]]><xsl:value-of select="TableFK6"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK7"/><![CDATA[Repository.]]><xsl:value-of select="TableFK7"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK8"/><![CDATA[Repository.]]><xsl:value-of select="TableFK8"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK9"/><![CDATA[Repository.]]><xsl:value-of select="TableFK9"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK10"/><![CDATA[Repository.]]><xsl:value-of select="TableFK10"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK11"/><![CDATA[Repository.]]><xsl:value-of select="TableFK11"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK12"/><![CDATA[Repository.]]><xsl:value-of select="TableFK12"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK13"/><![CDATA[Repository.]]><xsl:value-of select="TableFK13"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK14"/><![CDATA[Repository.]]><xsl:value-of select="TableFK14"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK15"/><![CDATA[Repository.]]><xsl:value-of select="TableFK15"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK16"/><![CDATA[Repository.]]><xsl:value-of select="TableFK16"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK17"/><![CDATA[Repository.]]><xsl:value-of select="TableFK17"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK18"/><![CDATA[Repository.]]><xsl:value-of select="TableFK18"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK19"/><![CDATA[Repository.]]><xsl:value-of select="TableFK19"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK20"/><![CDATA[Repository.]]><xsl:value-of select="TableFK20"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK21"/><![CDATA[Repository.]]><xsl:value-of select="TableFK21"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK22"/><![CDATA[Repository.]]><xsl:value-of select="TableFK22"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK23"/><![CDATA[Repository.]]><xsl:value-of select="TableFK23"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK24"/><![CDATA[Repository.]]><xsl:value-of select="TableFK24"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK25"/><![CDATA[Repository.]]><xsl:value-of select="TableFK25"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK26"/><![CDATA[Repository.]]><xsl:value-of select="TableFK26"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK27"/><![CDATA[Repository.]]><xsl:value-of select="TableFK27"/><![CDATA["
                 };         
            ViewData["tables"] = new SelectList(tables);  ]]>

<![CDATA[             ViewData]]><xsl:value-of select="TableFK1"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK2"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK3"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewDataSpeciessesGetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK6"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK7"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK8"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK9"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK10"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK11"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK12"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK13"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK14"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK15"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK16"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK17"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK18"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK19"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK20"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK21"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK22"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK23"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK24"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK25"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK26"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK27"/><![CDATA[GetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">  
<![CDATA[             var tables = new[] {
                         "]]><xsl:value-of select="EntitysFK4"/><![CDATA[Repository.]]><xsl:value-of select="TableFK4"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK5"/><![CDATA[Repository.]]><xsl:value-of select="TableFK5"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK6"/><![CDATA[Repository.]]><xsl:value-of select="TableFK6"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK7"/><![CDATA[Repository.]]><xsl:value-of select="TableFK7"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK8"/><![CDATA[Repository.]]><xsl:value-of select="TableFK8"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK9"/><![CDATA[Repository.]]><xsl:value-of select="TableFK9"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK10"/><![CDATA[Repository.]]><xsl:value-of select="TableFK10"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK11"/><![CDATA[Repository.]]><xsl:value-of select="TableFK11"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK12"/><![CDATA[Repository.]]><xsl:value-of select="TableFK12"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK13"/><![CDATA[Repository.]]><xsl:value-of select="TableFK13"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK14"/><![CDATA[Repository.]]><xsl:value-of select="TableFK14"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK15"/><![CDATA[Repository.]]><xsl:value-of select="TableFK15"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK16"/><![CDATA[Repository.]]><xsl:value-of select="TableFK16"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK17"/><![CDATA[Repository.]]><xsl:value-of select="TableFK17"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK18"/><![CDATA[Repository.]]><xsl:value-of select="TableFK18"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK19"/><![CDATA[Repository.]]><xsl:value-of select="TableFK19"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK20"/><![CDATA[Repository.]]><xsl:value-of select="TableFK20"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK21"/><![CDATA[Repository.]]><xsl:value-of select="TableFK21"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK22"/><![CDATA[Repository.]]><xsl:value-of select="TableFK22"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK23"/><![CDATA[Repository.]]><xsl:value-of select="TableFK23"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK24"/><![CDATA[Repository.]]><xsl:value-of select="TableFK24"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK25"/><![CDATA[Repository.]]><xsl:value-of select="TableFK25"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK26"/><![CDATA[Repository.]]><xsl:value-of select="TableFK26"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK27"/><![CDATA[Repository.]]><xsl:value-of select="TableFK27"/><![CDATA["
                 };          
            ViewData["tables"] = new SelectList(tables);  ]]>

<![CDATA[             ViewDataSpeciessesGetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK6"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK7"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK8"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK9"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK10"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK11"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK12"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK13"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK14"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK15"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK16"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK17"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK18"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK19"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK20"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK21"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK22"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK23"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK24"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK25"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK26"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK27"/><![CDATA[GetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">  
<![CDATA[            var imageVideoToDatabase = new WebImage(image.InputStream);  ]]>

<![CDATA[             ViewDataMimeTypesGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  
            ViewDataGenderGetValue(tbluserprofile);
            ViewDataTitleGetValue(tbluserprofile);

            ViewData["UserNameDDL"] = new SelectList(_aspnetUsersRepository.FindAllSort().ToList(), "UserId", "UserName");
            ViewData["Countries"] = new SelectList(PhoneValidator.Countries, tbluserprofile.Country);  ]]>
</xsl:when>
<xsl:otherwise>           
    <xsl:if test="TableFK1 !='NULL'"><![CDATA[            ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK1"/><![CDATA[", "]]><xsl:value-of select="NameFK1"/><![CDATA[");         ]]>
    </xsl:if> 
    <xsl:if test="TableFK2 !='NULL'"><![CDATA[            ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK2"/><![CDATA[", "]]><xsl:value-of select="NameFK2"/><![CDATA[");         ]]>
    </xsl:if> 
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='TryUpdateModel+++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>
<xsl:otherwise>  
    <![CDATA[        if (!TryUpdateModel(]]><xsl:value-of select="Entity"/><![CDATA[ ))
                return View(]]><xsl:value-of select="Entity"/><![CDATA[);                              ]]>      
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Save space instead NULL++++++++++++++++'">    
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">  
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.Subregnum == null)    {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.Subregnum = " ";    } //fill with "" to show in DDL for Phylum and Division aso.    
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Add(]]><xsl:value-of select="Entity"/><![CDATA[);   
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">  
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.Subspeciesgroup == null)    {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.Subspeciesgroup = " ";    }  //fill with "" to show in DDL for FiSpecies and PlSpecies aso. 
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Add(]]><xsl:value-of select="Entity"/><![CDATA[);   
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">  
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies == null)    {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies = " ";     } //fill with "" to show in DDL for names, Synonyms aso.  ]]>  
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.Divers == null)     {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.Divers = " ";     }   
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Add(]]><xsl:value-of select="Entity"/><![CDATA[);   
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">   
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies == null)     {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies = " ";     } //fill with "" to show in DDL for names, Synonyms aso.  ]]>  
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.Divers == null)    {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.Divers = " ";    }    
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Add(]]><xsl:value-of select="Entity"/><![CDATA[);   
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">    
<![CDATA[            //Fill
            ]]><xsl:value-of select="Entity"/><![CDATA[.Filestream = imageVideoToDatabase.GetBytes();  //Image    ]]>

<![CDATA[            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Add(]]><xsl:value-of select="Entity"/><![CDATA[);   
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>            
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">   
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.ArticelTitle == null)     {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.ArticelTitle = " ";     } //fill with "" to show in DDL   ]]>  
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.BookName == null)     {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.BookName = " ";     }   ]]>  
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.Page == null)    {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.Page = " ";      }    
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Add(]]><xsl:value-of select="Entity"/><![CDATA[);   
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">   
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.Notes == null)     {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.Notes = " ";    } //fill with "" to show in DDL   
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Add(]]><xsl:value-of select="Entity"/><![CDATA[);   
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">   
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.SourceYear == null)     {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.SourceYear = " ";     } //fill with "" to show in DDL   ]]>  
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.Notes == null)     {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.Notes = " ";     }    
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Add(]]><xsl:value-of select="Entity"/><![CDATA[);   
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">    
<![CDATA[            //Fill
            ]]><xsl:value-of select="Entity"/><![CDATA[.Filestream = imageVideoToDatabase.GetBytes();  //Image    ]]>

<![CDATA[            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Add(]]><xsl:value-of select="Entity"/><![CDATA[);   
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>            
</xsl:when>
<xsl:otherwise>
     <![CDATA[       //Add
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Add(]]><xsl:value-of select="Entity"/><![CDATA[);   
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='AspnetMemberships'">    
    <![CDATA[        TempData["message"] = SharedRes.StringsRes.TempDataSavedMessage;   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">    
      <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;    ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">    
      <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;    ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">    
        <![CDATA[    if (]]><xsl:value-of select="Entity"/><![CDATA[.PlSpeciesID == 2)  
               TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;    
            else                  
               TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="NameFK2"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;       ]]>
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">    
        <![CDATA[    if (]]><xsl:value-of select="Entity"/><![CDATA[.PlSpeciesID == 2)  
               TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;    
            else                  
               TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="NameFK2"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;       ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">    
         <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " /  " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + SharedRes.StringsRes.TempDataSavedMessage;    ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90References'">    
         <![CDATA[  TempDataMessage(]]><xsl:value-of select="Entity"/><![CDATA[, SharedRes.StringsRes.TempDataSavedMessage);    ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">    
         <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " /  " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + SharedRes.StringsRes.TempDataSavedMessage;    ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">    
         <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " /  " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + SharedRes.StringsRes.TempDataSavedMessage;    ]]>
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">    
         <![CDATA[  TempDataMessage(]]><xsl:value-of select="Entity"/><![CDATA[, SharedRes.StringsRes.TempDataSavedMessage);    ]]>
</xsl:when>
<xsl:otherwise>
    <![CDATA[        TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + SharedRes.StringsRes.TempDataSavedMessage;   ]]>
</xsl:otherwise>    
</xsl:choose>   

<xsl:choose>
<xsl:when test="Table ='Tbl81Images'">    
<![CDATA[   
            return View(]]><xsl:value-of select="Entity"/><![CDATA[);    //redisplay same view not possible Filestream new one expected                     
        }  ]]>
 <![CDATA[       //---------------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:otherwise> 
<![CDATA[   
            return View(]]><xsl:value-of select="Entity"/><![CDATA[);    //redisplay same view                     
        }  ]]>  
 <![CDATA[       //---------------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:otherwise>   
</xsl:choose>   

<xsl:choose>
<xsl:when test="Table ='+++++++++CREATE+++++++++GET+POST+++++++++++REGISTER+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">      
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">    
<![CDATA[        //
        // GET: /TblUserProfiles/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist, User")]
        public ActionResult CreateRegister()    {
            var tbluserprofile = new TblUserProfile();

            ViewDataMimeTypesGetValue(tbluserprofile);
            ViewData["Countries"] = new SelectList(PhoneValidator.Countries, tbluserprofile.Country);
            ViewDataGenderGetValue(tbluserprofile);
            ViewDataTitleGetValue(tbluserprofile);

            return View(tbluserprofile);
        }

        //
        // POST: /TblUserProfiles/Create
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist, User")]
        public ActionResult CreateRegister([Bind(Exclude = "UserProfileID")]HttpPostedFileBase image)   {
            var tbluserprofile = new TblUserProfile     {
                                         CountID = _tblCountersRepository.Counter(),
                                         FilestreamID = Guid.NewGuid(),
                                         Writer = User.Identity.Name,
                                         WriterDate = DateTime.Now,
                                         Updater = User.Identity.Name,
                                         UpdaterDate = DateTime.Now
                                     };
            var imageVideoToDatabase = new WebImage(image.InputStream);
            ViewDataMimeTypesGetValue(tbluserprofile);
            ViewData["Countries"] = new SelectList(PhoneValidator.Countries, tbluserprofile.Country);
            ViewDataGenderGetValue(tbluserprofile);
            ViewDataTitleGetValue(tbluserprofile);

            if (!TryUpdateModel(tbluserprofile))
                return View(tbluserprofile);

            var userId = (from p in _aspnetUsersRepository.AspnetUsers
                          where p.UserName == User.Identity.Name
                          select p.UserId).SingleOrDefault();

            tbluserprofile.UserId = userId;

            tbluserprofile.Filestream = imageVideoToDatabase.GetBytes(); //Image    

            _tblUserProfilesRepository.Add(tbluserprofile);
            _tblUserProfilesRepository.Save();

            TempData["message"] = tbluserprofile.LastName + " " + SharedRes.StringsRes.TempDataSavedMessage;

            return RedirectToAction("IndexRegister");
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:otherwise> 
</xsl:otherwise>   
</xsl:choose>   

<xsl:choose>
<xsl:when test="Table ='GUID++++++++++GET+++++EDIT++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='AspnetApplications'">  
 <![CDATA[       //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Edit/5
        [Authorize(Roles = "Administrator")]          
        public ActionResult Edit(Guid id)    {   
            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);    ]]>
</xsl:when>
<xsl:when test="Table ='AspnetMemberships'">   
 <![CDATA[       //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Edit/5
        [Authorize(Roles = "Administrator")]             
        public ActionResult Edit(Guid id)    {   
            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);    ]]>
</xsl:when>
<xsl:when test="Table ='AspnetUsers'">   
 <![CDATA[       //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Edit/5
        [Authorize(Roles = "Administrator")]             
        public ActionResult Edit(Guid id)    {   
            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);    ]]>
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">   
 <![CDATA[       //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Edit/5
        [Authorize(Roles = "Administrator")]             
        public ActionResult Edit(int id)    {   
            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);    ]]>
</xsl:when>
<xsl:otherwise> 
 <![CDATA[       //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Edit/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]           
        public ActionResult Edit(int id)    {   
            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);    ]]>
</xsl:otherwise>    
</xsl:choose>        

<xsl:choose>
<xsl:when test="Table ='DDLName Edit GET+++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">    
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">    
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">    
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">    
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">    
<![CDATA[             ViewDataSpeciesGetValue();  ]]>
<![CDATA[             ViewDataLanguagesGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
<![CDATA[             ViewDataSpeciesGetValue();  ]]>
<![CDATA[             ViewDataMimeTypesGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'"> 
<![CDATA[             ViewDataSpeciesGetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
<![CDATA[             ViewDataSpeciesGetValue();  ]]>
<![CDATA[             ViewData["Countries"] = new SelectList(PhoneValidator.Countries, ]]><xsl:value-of select="Entity"/><![CDATA[.Country);   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90References'">    
<![CDATA[             ViewData]]><xsl:value-of select="TableFK1"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK2"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK3"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewDataSpeciessesGetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK6"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK7"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK8"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK9"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK10"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK11"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK12"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK13"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK14"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK15"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK16"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK17"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK18"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK19"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK20"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK21"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK22"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK23"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK24"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK25"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK26"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK27"/><![CDATA[GetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">    
<![CDATA[             ViewDataSpeciessesGetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK6"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK7"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK8"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK9"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK10"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK11"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK12"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK13"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK14"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK15"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK16"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK17"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK18"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK19"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK20"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK21"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK22"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK23"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK24"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK25"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK26"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK27"/><![CDATA[GetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">  
<![CDATA[             ViewDataMimeTypesGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  ]]>
<![CDATA[             ViewData["Countries"] = new SelectList(PhoneValidator.Countries, tbluserprofile.Country);  ]]>
<![CDATA[             ViewDataGenderGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  ]]>
<![CDATA[             ViewDataTitleGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  ]]>
<![CDATA[             ViewData["UserNameDDL"] = new SelectList(_aspnetUsersRepository.FindAllSort().ToList(), "UserId", "UserName");  ]]>
</xsl:when>
<xsl:otherwise>           
    <xsl:if test="TableFK1 !='NULL'"><![CDATA[         ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK1"/><![CDATA[", "]]><xsl:value-of select="NameFK1"/><![CDATA[");         ]]>
    </xsl:if> 
    <xsl:if test="TableFK2 !='NULL'"><![CDATA[         ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK2"/><![CDATA[", "]]><xsl:value-of select="NameFK2"/><![CDATA[");         ]]>
    </xsl:if> 
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='return++++++++++++++++++'">      
</xsl:when>
<xsl:otherwise>  
        <![CDATA[    return View(]]><xsl:value-of select="Entity"/><![CDATA[);
        }  ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='GUID+Image+++++++++++POST+++++EDIT+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">      
</xsl:when>
<xsl:when test="Table ='AspnetApplications'">   
<![CDATA[        //
        // POST: /]]><xsl:value-of select="Table"/><![CDATA[/Edit/5  
        [HttpPost, Authorize(Roles = "Administrator")]   
         public ActionResult Edit(Guid id,  FormCollection collection)   {      
            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);   ]]>
</xsl:when>
<xsl:when test="Table ='AspnetMemberships'">   
<![CDATA[        //
        // POST: /]]><xsl:value-of select="Table"/><![CDATA[/Edit/5  
        [HttpPost, Authorize(Roles = "Administrator")]   
         public ActionResult Edit(Guid id,  FormCollection collection)   {  
            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);   ]]>   
</xsl:when>
<xsl:when test="Table ='AspnetUsers'">  
<![CDATA[        //
        // POST: /]]><xsl:value-of select="Table"/><![CDATA[/Edit/5  
        [HttpPost, Authorize(Roles = "Administrator")]    
         public ActionResult Edit(Guid id,  FormCollection collection)   {  
            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);   ]]>    
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">   
<![CDATA[        //
        // POST: /]]><xsl:value-of select="Table"/><![CDATA[/Edit/5  
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   
         public ActionResult Edit(int id,  HttpPostedFileBase image)   {  
            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);   ]]>   
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">   
<![CDATA[        //
        // POST: /]]><xsl:value-of select="Table"/><![CDATA[/Edit/5  
        [HttpPost, Authorize(Roles = "Administrator")]   
         public ActionResult Edit(int id,  HttpPostedFileBase image)   {  
            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);   ]]>   
</xsl:when>
<xsl:otherwise> 
<![CDATA[        //
        // POST: /]]><xsl:value-of select="Table"/><![CDATA[/Edit/5  
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   
        public ActionResult Edit(int id,  FormCollection collection)  {  
            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);   ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='DDLName Edit POST+++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">    
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">    
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">    
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">    
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">    
<![CDATA[             ViewDataSpeciesGetValue();  ]]>
<![CDATA[             ViewDataLanguagesGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
<![CDATA[            ViewDataSpeciesGetValue();  ]]>
<![CDATA[            ViewDataMimeTypesGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'"> 
<![CDATA[             ViewDataSpeciesGetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
<![CDATA[             ViewDataSpeciesGetValue();  ]]>
<![CDATA[             ViewData["Countries"] = new SelectList(PhoneValidator.Countries, ]]><xsl:value-of select="Entity"/><![CDATA[.Country);   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90References'">    
<![CDATA[             ViewData]]><xsl:value-of select="TableFK1"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK2"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK3"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewDataSpeciessesGetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK6"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK7"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK8"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK9"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK10"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK11"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK12"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK13"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK14"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK15"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK16"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK17"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK18"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK19"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK20"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK21"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK22"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK23"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK24"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK25"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK26"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK27"/><![CDATA[GetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">    
<![CDATA[             ViewDataSpeciessesGetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK6"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK7"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK8"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK9"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK10"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK11"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK12"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK13"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK14"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK15"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK16"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK17"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK18"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK19"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK20"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK21"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK22"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK23"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK24"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK25"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK26"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData]]><xsl:value-of select="TableFK27"/><![CDATA[GetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">  
<![CDATA[             ViewDataMimeTypesGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  ]]>
<![CDATA[             ViewData["Countries"] = new SelectList(PhoneValidator.Countries, tbluserprofile.Country);  ]]>
<![CDATA[             ViewDataGenderGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  ]]>
<![CDATA[             ViewDataTitleGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  ]]>
<![CDATA[             ViewData["UserNameDDL"] = new SelectList(_aspnetUsersRepository.FindAllSort().ToList(), "UserId", "UserName");  ]]>
</xsl:when>
<xsl:otherwise>           
    <xsl:if test="TableFK1 !='NULL'"><![CDATA[            ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK1"/><![CDATA[", "]]><xsl:value-of select="NameFK1"/><![CDATA[");         ]]>
    </xsl:if> 
    <xsl:if test="TableFK2 !='NULL'"><![CDATA[            ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK2"/><![CDATA[", "]]><xsl:value-of select="NameFK2"/><![CDATA[");         ]]>
    </xsl:if> 
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='TryUpdateModel+++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">     
<![CDATA[            if (image != null)  {
                var imageVideoToDatabase = new WebImage(image.InputStream);

                if(!TryUpdateModel(]]><xsl:value-of select="Entity"/><![CDATA[))
                    return View(]]><xsl:value-of select="Entity"/><![CDATA[);

                //Fill   
                if (tbl81Image != null)     {  
                    tbl81Image.Filestream = imageVideoToDatabase.GetBytes();  //Image   

                    ]]><xsl:value-of select="Entity"/><![CDATA[.Updater = User.Identity.Name;
                    ]]><xsl:value-of select="Entity"/><![CDATA[.UpdaterDate = DateTime.Now;
                }
                ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();                  
            }
            else  {
            if(!TryUpdateModel(]]><xsl:value-of select="Entity"/><![CDATA[))
                return View(]]><xsl:value-of select="Entity"/><![CDATA[);

                //Fill   
                ]]><xsl:value-of select="Entity"/><![CDATA[.Updater = User.Identity.Name;
                ]]><xsl:value-of select="Entity"/><![CDATA[.UpdaterDate = DateTime.Now;

                ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   
            }   ]]>   
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">     
<![CDATA[            if (image != null)  {
                var imageVideoToDatabase = new WebImage(image.InputStream);

                if(!TryUpdateModel(]]><xsl:value-of select="Entity"/><![CDATA[))
                    return View(]]><xsl:value-of select="Entity"/><![CDATA[);

                //Fill   
                ]]><xsl:value-of select="Entity"/><![CDATA[.Filestream = imageVideoToDatabase.GetBytes();  //Image   
                ]]><xsl:value-of select="Entity"/><![CDATA[.Updater = User.Identity.Name;
                ]]><xsl:value-of select="Entity"/><![CDATA[.UpdaterDate = DateTime.Now;
                
                ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();                  
            }
            else  {
            if(!TryUpdateModel(]]><xsl:value-of select="Entity"/><![CDATA[))
                return View(]]><xsl:value-of select="Entity"/><![CDATA[);

                //Fill   
                ]]><xsl:value-of select="Entity"/><![CDATA[.Updater = User.Identity.Name;
                ]]><xsl:value-of select="Entity"/><![CDATA[.UpdaterDate = DateTime.Now;

                ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   
            }   ]]>   
</xsl:when>
<xsl:otherwise>  
   <![CDATA[                         
            if(!TryUpdateModel(]]><xsl:value-of select="Entity"/><![CDATA[))
                return View(]]><xsl:value-of select="Entity"/><![CDATA[);    ]]>
</xsl:otherwise>    
</xsl:choose> 
<xsl:choose>
<xsl:when test="Table ='Save+ Space instead NULL++++++++++++++++'">    
</xsl:when>
<xsl:when test="Table ='AspnetApplications'">       
     <![CDATA[       //Save
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>   
</xsl:when>
<xsl:when test="Table ='AspnetMemberships'">       
     <![CDATA[       //Save
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>   
</xsl:when>
<xsl:when test="Table ='AspnetUsers'">       
     <![CDATA[       //Save
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>   
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">  
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.Subregnum == null)    {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.Subregnum = " ";    } //fill with "" to show in DDL for Phylum and Division aso.   
            ]]><xsl:value-of select="Entity"/><![CDATA[.Updater = User.Identity.Name;
            ]]><xsl:value-of select="Entity"/><![CDATA[.UpdaterDate = DateTime.Now;

            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>   
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">  
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.Subspeciesgroup == null)     {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.Subspeciesgroup = " ";      } //fill with "" to show in DDL for FiSpecies and PlSpecies aso.  
            ]]><xsl:value-of select="Entity"/><![CDATA[.Updater = User.Identity.Name;
            ]]><xsl:value-of select="Entity"/><![CDATA[.UpdaterDate = DateTime.Now;

            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>   
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">  
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies == null)    {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies = " ";      } //fill with "" to show in DDL for names, Synonyms aso.  ]]>  
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.Divers == null)   {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.Divers = " ";      }  //fill with "" to show in DDL for names, Synonyms aso. 
            ]]><xsl:value-of select="Entity"/><![CDATA[.Updater = User.Identity.Name;
            ]]><xsl:value-of select="Entity"/><![CDATA[.UpdaterDate = DateTime.Now;

            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>     
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">   
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies == null)   {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies = " ";      } //fill with "" to show in DDL for names, Synonyms aso.  ]]>  
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.Divers == null)     {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.Divers = " ";       } //fill with "" to show in DDL for names, Synonyms aso.  
            ]]><xsl:value-of select="Entity"/><![CDATA[.Updater = User.Identity.Name;
            ]]><xsl:value-of select="Entity"/><![CDATA[.UpdaterDate = DateTime.Now;

            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>    
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">    
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">   
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.ArticelTitle == null)     {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.ArticelTitle = " ";      } //fill with "" to show in DDL   ]]>  
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.BookName == null)     {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.BookName = " ";      }   ]]>  
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.Page == null)     {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.Page = " ";      }    
            ]]><xsl:value-of select="Entity"/><![CDATA[.Updater = User.Identity.Name;
            ]]><xsl:value-of select="Entity"/><![CDATA[.UpdaterDate = DateTime.Now;

            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>    
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">   
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.Notes == null)     {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.Notes = " ";       } //fill with "" to show in DDL   
            ]]><xsl:value-of select="Entity"/><![CDATA[.Updater = User.Identity.Name;
            ]]><xsl:value-of select="Entity"/><![CDATA[.UpdaterDate = DateTime.Now;

            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>     
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">   
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.SourceYear == null)     {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.SourceYear = " ";      } //fill with "" to show in DDL   ]]>  
 <![CDATA[           if (]]><xsl:value-of select="Entity"/><![CDATA[.Notes == null)     {    
                ]]><xsl:value-of select="Entity"/><![CDATA[.Notes = " ";      }   
            ]]><xsl:value-of select="Entity"/><![CDATA[.Updater = User.Identity.Name;
            ]]><xsl:value-of select="Entity"/><![CDATA[.UpdaterDate = DateTime.Now;

            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>   
</xsl:when>
<xsl:when test="Table ='TblCounters'">       
     <![CDATA[                ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>   
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">    
</xsl:when>
<xsl:otherwise>
     <![CDATA[       //Fill
            ]]><xsl:value-of select="Entity"/><![CDATA[.Updater = User.Identity.Name;
            ]]><xsl:value-of select="Entity"/><![CDATA[.UpdaterDate = DateTime.Now;

            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>   
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='AspnetMemberships'">    
         <![CDATA[  TempData["message"] = SharedRes.StringsRes.TempDataSavedMessage;    ]]>

<![CDATA[           return RedirectToAction("Index", ]]><xsl:value-of select="Entity"/><![CDATA[);         
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">    
         <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;    ]]>

<![CDATA[           return RedirectToAction("Index", ]]><xsl:value-of select="Entity"/><![CDATA[);         
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">    
         <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;    ]]>

<![CDATA[           return RedirectToAction("Index", ]]><xsl:value-of select="Entity"/><![CDATA[);         
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">    
        <![CDATA[   if (]]><xsl:value-of select="Entity"/><![CDATA[ != null && ]]><xsl:value-of select="Entity"/><![CDATA[.PlSpeciesID == 2)  
              TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;    
            else                  
              TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="NameFK2"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;       ]]>

<![CDATA[           return RedirectToAction("Index", ]]><xsl:value-of select="Entity"/><![CDATA[);         
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">    
        <![CDATA[   if (]]><xsl:value-of select="Entity"/><![CDATA[ != null && ]]><xsl:value-of select="Entity"/><![CDATA[.PlSpeciesID == 2)  
              TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;    
            else                  
              TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="NameFK2"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;       ]]>

<![CDATA[           return RedirectToAction("Index", ]]><xsl:value-of select="Entity"/><![CDATA[);         
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">    
         <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " /  " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + SharedRes.StringsRes.TempDataSavedMessage;    ]]>

<![CDATA[           return RedirectToAction("Index", ]]><xsl:value-of select="Entity"/><![CDATA[);         
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90References'">    
         <![CDATA[  TempDataMessage(]]><xsl:value-of select="Entity"/><![CDATA[, SharedRes.StringsRes.TempDataSavedMessage);    ]]>

<![CDATA[           return RedirectToAction("Index", ]]><xsl:value-of select="Entity"/><![CDATA[);         
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">    
         <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " /  " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + SharedRes.StringsRes.TempDataSavedMessage;    ]]>

<![CDATA[           return RedirectToAction("Index", ]]><xsl:value-of select="Entity"/><![CDATA[);         
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">    
         <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " /  " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + SharedRes.StringsRes.TempDataSavedMessage;    ]]>

<![CDATA[           return RedirectToAction("Index", ]]><xsl:value-of select="Entity"/><![CDATA[);         
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">    
         <![CDATA[  TempDataMessage(]]><xsl:value-of select="Entity"/><![CDATA[, SharedRes.StringsRes.TempDataSavedMessage);    ]]>

<![CDATA[           return RedirectToAction("Index", ]]><xsl:value-of select="Entity"/><![CDATA[);         
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:otherwise>
    <![CDATA[       TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + SharedRes.StringsRes.TempDataSavedMessage;   ]]>

<![CDATA[           return RedirectToAction("Index", ]]><xsl:value-of select="Entity"/><![CDATA[);         
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  ]]>
</xsl:otherwise>    
</xsl:choose>   

<xsl:choose>
<xsl:when test="Table ='+++++++++EDIT+++++++++++++++++++++REGISTER+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">      
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">  
<![CDATA[        //        
        // GET: /TblUserProfiles/Edit/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist, User")]
        public ActionResult EditRegister(int id)    {

            var tbluserprofile = _tblUserProfilesRepository.Get(id);
            ViewDataMimeTypesGetValue(tbluserprofile);
            ViewData["Countries"] = new SelectList(PhoneValidator.Countries, tbluserprofile.Country);
            ViewDataGenderGetValue(tbluserprofile);
            ViewDataTitleGetValue(tbluserprofile);

            ViewData["UserNameDDL"] = new SelectList(_aspnetUsersRepository.FindAllSort().ToList(), "UserId", "UserName");

            return View(tbluserprofile);
        }

        //
        // POST: /TblUserProfiles/Edit/5  
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist, User")]
        public ActionResult EditRegister(int id, HttpPostedFileBase image)    {

            var tbluserprofile = _tblUserProfilesRepository.Get(id);
            ViewDataMimeTypesGetValue(tbluserprofile);
            ViewData["UserNameDDL"] = new SelectList(_aspnetUsersRepository.FindAllSort().ToList(), "UserId", "UserName");
            ViewData["Countries"] = new SelectList(PhoneValidator.Countries, tbluserprofile.Country);
            ViewDataGenderGetValue(tbluserprofile);
            ViewDataTitleGetValue(tbluserprofile);

            if (image != null)
            {
                var imageVideoToDatabase = new WebImage(image.InputStream);

                if (!TryUpdateModel(tbluserprofile))
                    return View(tbluserprofile);

                //Fill     
                tbluserprofile.Filestream = imageVideoToDatabase.GetBytes();  //Image   

                tbluserprofile.Updater = User.Identity.Name;
                tbluserprofile.UpdaterDate = DateTime.Now;

                _tblUserProfilesRepository.Save();
            }
            else    {
                if (!TryUpdateModel(tbluserprofile))
                    return View(tbluserprofile);

                //Fill   
                tbluserprofile.Updater = User.Identity.Name;
                tbluserprofile.UpdaterDate = DateTime.Now;

                _tblUserProfilesRepository.Save();
            }

            TempData["message"] = tbluserprofile.LastName + " " + SharedRes.StringsRes.TempDataSavedMessage;

            return RedirectToAction("IndexRegister", tbluserprofile);
        }
        //-----------------------------------------------------------------------------------------------------           ]]>
</xsl:when>
<xsl:otherwise>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='GUID+++++++++GET+++++++++++++++++++++Delete+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">      
</xsl:when>
<xsl:when test="Table ='AspnetApplications'">   
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Delete/5
        [Authorize(Roles = "Administrator")]    
        public ActionResult Delete(Guid id)   {  
            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id); 
  
            return ]]><xsl:value-of select="Entity"/><![CDATA[ == null ? View("NotFound") : View(]]><xsl:value-of select="Entity"/><![CDATA[);                      
        }   ]]>    
</xsl:when>
<xsl:when test="Table ='AspnetMemberships'">   
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Delete/5
        [Authorize(Roles = "Administrator")]    
        public ActionResult Delete(Guid id)   {  
            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id); 
  
            return ]]><xsl:value-of select="Entity"/><![CDATA[ == null ? View("NotFound") : View(]]><xsl:value-of select="Entity"/><![CDATA[);                      
        }   ]]>    
</xsl:when>
<xsl:when test="Table ='AspnetUsers'">  
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Delete/5
        [Authorize(Roles = "Administrator")]    
        public ActionResult Delete(Guid id)   {     
            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id); 
  
            return ]]><xsl:value-of select="Entity"/><![CDATA[ == null ? View("NotFound") : View(]]><xsl:value-of select="Entity"/><![CDATA[);                      
        }   ]]>
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">  
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Delete/5
        [Authorize(Roles = "Administrator")]    
        public ActionResult Delete(int id)   {     
            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id); 
  
            return ]]><xsl:value-of select="Entity"/><![CDATA[ == null ? View("NotFound") : View(]]><xsl:value-of select="Entity"/><![CDATA[);                      
        }   ]]>
</xsl:when>
<xsl:otherwise>  
<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Delete/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]    
        public ActionResult Delete(int id)  {  
            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id); 
  
            return ]]><xsl:value-of select="Entity"/><![CDATA[ == null ? View("NotFound") : View(]]><xsl:value-of select="Entity"/><![CDATA[);                      
        }   ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='GUID+++++++++++++POST++++++++++++++++++++++++DELETE++++++++++++++++++++++++++++++++++++++++++++++++++'">      
</xsl:when>
<xsl:when test="Table ='AspnetApplications'">   
<![CDATA[        //
        // POST: /]]><xsl:value-of select="Table"/><![CDATA[/Delete/5
        [HttpPost, Authorize(Roles = "Administrator")]     
        public ActionResult Delete(Guid id, string confirmButton)    {    
            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);   

            if (]]><xsl:value-of select="Entity"/><![CDATA[ == null)
                return RedirectToAction("NotFound");
            
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Delete(]]><xsl:value-of select="Entity"/><![CDATA[);   ]]>    
</xsl:when>
<xsl:when test="Table ='AspnetMemberships'">   
<![CDATA[        //
        // POST: /]]><xsl:value-of select="Table"/><![CDATA[/Delete/5
        [HttpPost, Authorize(Roles = "Administrator")]     
        public ActionResult Delete(Guid id, string confirmButton)    {       
            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);   

            if (]]><xsl:value-of select="Entity"/><![CDATA[ == null)
                return RedirectToAction("NotFound");
            
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Delete(]]><xsl:value-of select="Entity"/><![CDATA[);   ]]> 
</xsl:when>
<xsl:when test="Table ='AspnetUsers'"> 
<![CDATA[        //
        // POST: /]]><xsl:value-of select="Table"/><![CDATA[/Delete/5
        [HttpPost, Authorize(Roles = "Administrator")]     
        public ActionResult Delete(Guid id, string confirmButton)    {       
            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);   

            if (]]><xsl:value-of select="Entity"/><![CDATA[ == null)
                return RedirectToAction("NotFound");
            
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Delete(]]><xsl:value-of select="Entity"/><![CDATA[);   ]]> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
<![CDATA[        //
        // POST: /]]><xsl:value-of select="Table"/><![CDATA[/Delete/5
        [HttpPost, Authorize(Roles = "Administrator")]    
        public ActionResult Delete(int id, string confirmButton)    {  
            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);   

            if (]]><xsl:value-of select="Entity"/><![CDATA[ == null)
                return RedirectToAction("NotFound");
            
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Delete(]]><xsl:value-of select="Entity"/><![CDATA[);   ]]>
</xsl:when>
<xsl:otherwise>  
<![CDATA[        //
        // POST: /]]><xsl:value-of select="Table"/><![CDATA[/Delete/5
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]    
        public ActionResult Delete(int id, string confirmButton)    {  
            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);   

            if (]]><xsl:value-of select="Entity"/><![CDATA[ == null)
                return RedirectToAction("NotFound");
            
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Delete(]]><xsl:value-of select="Entity"/><![CDATA[);   ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='AspnetMemberships'">    
         <![CDATA[  TempData["message"] = SharedRes.StringsRes.TempDataDeletedMessage;    ]]>
<![CDATA[            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();  
 
            return RedirectToAction("Index");
        }   ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">    
         <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;    ]]>
<![CDATA[            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();  
 
            return RedirectToAction("Index");
        }   ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">    
         <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;    ]]>
<![CDATA[            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();  
 
            return RedirectToAction("Index");
        }   ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">    
         <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " " + SharedRes.StringsRes.TempDataDeletedMessage;    ]]>
<![CDATA[            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();  
 
            return RedirectToAction("Index");
        }   ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">    
         <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " " + SharedRes.StringsRes.TempDataDeletedMessage;    ]]>
<![CDATA[            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();  
 
            return RedirectToAction("Index");
        }   ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">    
         <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " /  " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + SharedRes.StringsRes.TempDataDeletedMessage;    ]]>
<![CDATA[            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();  
 
            return RedirectToAction("Index");
        }   ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl90References'">    
         <![CDATA[  TempDataMessage(]]><xsl:value-of select="Entity"/><![CDATA[, SharedRes.StringsRes.TempDataDeletedMessage);    ]]>
<![CDATA[            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();  
 
            return RedirectToAction("Index");
        }   ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">    
         <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " /  " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + SharedRes.StringsRes.TempDataDeletedMessage;    ]]>
<![CDATA[            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();  
 
            return RedirectToAction("Index");
        }   ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">    
         <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " /  " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + SharedRes.StringsRes.TempDataDeletedMessage;    ]]>
<![CDATA[            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();  
 
            return RedirectToAction("Index");
        }   ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">    
         <![CDATA[  TempDataMessage(]]><xsl:value-of select="Entity"/><![CDATA[, SharedRes.StringsRes.TempDataDeletedMessage);    ]]>
<![CDATA[            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();  
 
            return RedirectToAction("Index");
        }   ]]> 
</xsl:when>
<xsl:otherwise>
    <![CDATA[        TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + SharedRes.StringsRes.TempDataDeletedMessage;   ]]>
<![CDATA[            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();  
 
            return RedirectToAction("Index");
        }   ]]> 
</xsl:otherwise>    
</xsl:choose>    

<xsl:choose>
<xsl:when test="Table ='+++++++++++++REGISTER+++++++++++++++++++++++DELETE++++++++++++++++++++++++++++++++++++++++++++++++++'">      
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
<![CDATA[        //-----------------------------------------------------------------------------------------------------           

        //
        // GET: /TblUserProfiles/Delete/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist, User")]
        public ActionResult DeleteRegister(int id)    {

            var tbluserprofile = _tblUserProfilesRepository.Get(id);
            return tbluserprofile == null ? View("NotFound") : View(tbluserprofile);
        }
  
        //
        // POST: /TblUserProfiles/Delete/5
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist, User")]
        public ActionResult DeleteRegister(int id, string confirmButton)    {
            var tbluserprofile = _tblUserProfilesRepository.Get(id);

            if (tbluserprofile == null)
                return RedirectToAction("NotFound");

            _tblUserProfilesRepository.Delete(tbluserprofile);

            TempData["message"] = tbluserprofile.LastName + " " + SharedRes.StringsRes.TempDataDeletedMessage;
            _tblUserProfilesRepository.Save();
            return RedirectToAction("IndexRegister");
        }  ]]>    
</xsl:when>
<xsl:otherwise>  
</xsl:otherwise>    
</xsl:choose> 
<![CDATA[        public ActionResult NotFound()    {
            throw new NotImplementedException();
        }
   }
}]]>   

</xsl:template>
</xsl:stylesheet>











