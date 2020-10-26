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
<xsl:if test="Table='Tbl78Names'">  
<![CDATA[using System.Collections.Generic;      ]]>
</xsl:if>   
<xsl:if test="Table='Tbl81Images'">  
<![CDATA[using System.Web.Helpers;      ]]>
<![CDATA[using System.Web;     ]]>
</xsl:if>   
<xsl:if test="Table='Tbl87Geographics'">  
<![CDATA[using Atis.Domain.Helpers;      ]]>
</xsl:if>   

<![CDATA[// <!-- Controller Skriptdatum: ]]> <xsl:value-of select="DateTime"/>  <![CDATA[  -->  

namespace ]]><xsl:value-of select="Namespace"/>.WebUI.Controllers <![CDATA[    {  
    [HandleError]
    public class ]]><xsl:value-of select="Table"/><![CDATA[Controller : LanguageController    { 
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
<xsl:when test="Table ='TblCounters++++++++++++++++'">      
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">      
</xsl:when>
<xsl:when test="Table ='aspnet_Memberships'">      
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">      
</xsl:when>
<xsl:when test="Table ='TblCounters'">  
   <![CDATA[        
         //----------------------------------------------------------------------

        //   [OutputCache(Duration = 10, VaryByParam = "none")]
        // GET: /]]><xsl:value-of select="LinqModel"/><![CDATA[/  ]]>      
</xsl:when>
<xsl:otherwise>  
   <![CDATA[      readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
         //----------------------------------------------------------------------

        //   [OutputCache(Duration = 10, VaryByParam = "none")]
        // GET: /]]><xsl:value-of select="LinqModel"/><![CDATA[/  ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='+++Index+++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='aspnet_Applications'">
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="Name"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, Guid? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                   
</xsl:when>  
<xsl:when test="Table ='aspnet_Membership'">
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="Name"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, string ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Name = null, Guid? ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, Guid? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                  
</xsl:when>  
<xsl:when test="Table ='aspnet_Users'">
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="Name"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, string ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Name = null, Guid? ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, Guid? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                  
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="Name"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                   
</xsl:when>  
<xsl:when test="Table ='Tbl68Speciesgroups'">
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="Name"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                   
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="Name"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, int? ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id = null, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                  
</xsl:when>  
<xsl:when test="Table ='Tbl69FiSpeciesses'">
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="ID"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, int? ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id = null, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                   
</xsl:when>  
<xsl:when test="Table ='Tbl72PlSpeciesses'">
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="ID"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, int? ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id = null, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                   
</xsl:when>  
<xsl:when test="Table ='Tbl78Names'">
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="ID"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, int? ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id = null, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                   
</xsl:when>  
<xsl:when test="Table ='Tbl81Images'">
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="ID"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 5, int? ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id = null, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                 
</xsl:when>  
<xsl:when test="Table ='Tbl84Synonyms'">
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="ID"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, int? ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id = null, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                   
</xsl:when>  
<xsl:when test="Table ='Tbl87Geographics'">
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="ID"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, int? ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[Id = null, int? ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[Id = null, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                  
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="Name"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 8, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                  
</xsl:when>  
<xsl:when test="Table ='Tbl90References'">
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
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="Name"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>   
</xsl:when>                                 
<xsl:when test="Table ='Tbl90RefSources'">
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="Name"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                   
</xsl:when>  
<xsl:when test="Table ='Tbl93Comments'">
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
        <![CDATA[public ActionResult Index(string sortBy = "]]><xsl:value-of select="Name"/><![CDATA[", bool ascending = true, int page = 1, int pageSize = 12, bool? valid = null, string ]]><xsl:value-of select="BasisSm"/><![CDATA[Name = null, int? ]]><xsl:value-of select="BasisSm"/><![CDATA[Id = null)  { ]]>                                   
</xsl:when>  
<xsl:otherwise>
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
<xsl:when test="Table ='aspnet_Applications'">
        <![CDATA[        
            }; ]]>
</xsl:when>  
<xsl:when test="Table ='aspnet_Memberships'">
        <![CDATA[        
            }; ]]>
</xsl:when>  
<xsl:when test="Table ='aspnet_Users'">
        <![CDATA[        
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
                                    Value = a.]]><xsl:value-of select="IDFK1"/><![CDATA[.ToString(),
                                }).ToList()
            };     ]]>                                   
</xsl:when>    
<xsl:when test="Table ='Tbl03Regnums'">
        <![CDATA[        Valid = valid.HasValue && valid.Value  
            }; ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl06Phylums'">
        <![CDATA[        
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
        <![CDATA[        
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
        <![CDATA[       
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
        <![CDATA[       
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
        <![CDATA[       
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
        <![CDATA[       
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
        <![CDATA[        
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
        <![CDATA[        
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
        <![CDATA[        
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
        <![CDATA[        
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
        <![CDATA[        
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
<xsl:otherwise>
        <![CDATA[        
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
<xsl:when test="Table ='Tbl81Images'">  
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
</xsl:when>
<xsl:when test="Table ='Tbl90References'">  
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">  
</xsl:when>
<xsl:otherwise> 
<![CDATA[            // Filter on ]]><xsl:value-of select="Name"/><![CDATA[
            if (!string.IsNullOrEmpty(]]><xsl:value-of select="BasisSm"/><![CDATA[Name))
                filteredResults = filteredResults.Where(a => a.]]><xsl:value-of select="Name"/><![CDATA[.StartsWith(]]><xsl:value-of select="BasisSm"/><![CDATA[Name));  ]]>    
</xsl:otherwise>    
</xsl:choose>
<![CDATA[            // Filter on ]]><xsl:value-of select="ID"/><![CDATA[
            if (]]><xsl:value-of select="BasisSm"/><![CDATA[Id.HasValue)
                filteredResults = filteredResults.Where(a => a.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);   ]]>

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
<xsl:when test="Table ='aspnet_Applications'">      
</xsl:when>
<xsl:when test="Table ='aspnet_Memberships'">      
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">      
</xsl:when>
<xsl:when test="Table ='TblCounters'">        
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
        //------------------------------------------------------------------------  ]]>
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
        //------------------------------------------------------------------------------  ]]>
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
        //------------------------------------------------------------------------------  ]]>
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
        //------------------------------------------------------------------------------  ]]>
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
        //------------------------------------------------------------------------------  ]]>
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
        //--------------------------------------------------------------------------

         
        private void ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue()  {

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
        //------------------------------------------------------------------------------  ]]>
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
        //------------------------------------------------------------------------  

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
        //--------------------------------------------------------------------------
       
        private void ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue()  {

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
        //------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">  
   <![CDATA[             
        private void ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue()  {

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
        //------------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
   <![CDATA[             
        private void ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue()  {

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
        //------------------------------------------------------------------------------  ]]>
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
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK2"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.]]><xsl:value-of select="TableFK2"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK2"/><![CDATA[, d.SourceYear
                         select new    {
                             d.]]><xsl:value-of select="IDFK2"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK2"/><![CDATA[ = d.]]><xsl:value-of select="NameFK2"/><![CDATA[ + " ##  " + d.SourceYear + " ##  " + d.Notes
                         };
            ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK2"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK2"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK3"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK3"/><![CDATA[Repository.]]><xsl:value-of select="TableFK3"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK3"/><![CDATA[, d.PublicationYear, d.Page
                         select new    {
                             d.]]><xsl:value-of select="IDFK3"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK3"/><![CDATA[ = d.]]><xsl:value-of select="NameFK3"/><![CDATA[ + " ##  " + d.PublicationYear + " ## " + d.Page + " ## " + d.ArticelTitle + " ## " + d.BookName
                         };
            ViewData["]]><xsl:value-of select="NameFK3"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK3"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK3"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

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
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK6"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK6"/><![CDATA[Repository.]]><xsl:value-of select="TableFK6"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK6"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK6"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK6"/><![CDATA[ = d.]]><xsl:value-of select="NameFK6"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK6"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK6"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK6"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK7"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK7"/><![CDATA[Repository.]]><xsl:value-of select="TableFK7"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK7"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK7"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK7"/><![CDATA[ = d.]]><xsl:value-of select="NameFK7"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK7"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK7"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK7"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK8"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK8"/><![CDATA[Repository.]]><xsl:value-of select="TableFK8"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK8"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK8"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK8"/><![CDATA[ = d.]]><xsl:value-of select="NameFK8"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK8"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK8"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK8"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK9"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK9"/><![CDATA[Repository.]]><xsl:value-of select="TableFK9"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK9"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK9"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK9"/><![CDATA[ = d.]]><xsl:value-of select="NameFK9"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK9"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK9"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK9"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK10"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK10"/><![CDATA[Repository.]]><xsl:value-of select="TableFK10"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK10"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK10"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK10"/><![CDATA[ = d.]]><xsl:value-of select="NameFK10"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK10"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK10"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK10"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK11"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK11"/><![CDATA[Repository.]]><xsl:value-of select="TableFK11"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK11"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK11"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK11"/><![CDATA[ = d.]]><xsl:value-of select="NameFK11"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK11"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK11"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK11"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK12"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK12"/><![CDATA[Repository.]]><xsl:value-of select="TableFK12"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK12"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK12"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK12"/><![CDATA[ = d.]]><xsl:value-of select="NameFK12"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK12"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK12"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK12"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK13"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK13"/><![CDATA[Repository.]]><xsl:value-of select="TableFK13"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK13"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK13"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK13"/><![CDATA[ = d.]]><xsl:value-of select="NameFK13"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK13"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK13"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK13"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK14"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK14"/><![CDATA[Repository.]]><xsl:value-of select="TableFK14"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK14"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK14"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK14"/><![CDATA[ = d.]]><xsl:value-of select="NameFK14"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK14"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK14"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK14"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK15"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK15"/><![CDATA[Repository.]]><xsl:value-of select="TableFK15"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK15"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK15"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK15"/><![CDATA[ = d.]]><xsl:value-of select="NameFK15"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK15"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK15"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK15"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK16"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK16"/><![CDATA[Repository.]]><xsl:value-of select="TableFK16"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK16"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK16"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK16"/><![CDATA[ = d.]]><xsl:value-of select="NameFK16"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK16"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK16"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK16"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK17"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK17"/><![CDATA[Repository.]]><xsl:value-of select="TableFK17"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK17"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK17"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK17"/><![CDATA[ = d.]]><xsl:value-of select="NameFK17"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK17"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK17"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK17"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK18"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK18"/><![CDATA[Repository.]]><xsl:value-of select="TableFK18"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK18"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK18"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK18"/><![CDATA[ = d.]]><xsl:value-of select="NameFK18"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK18"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK18"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK18"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK19"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK19"/><![CDATA[Repository.]]><xsl:value-of select="TableFK19"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK19"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK19"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK19"/><![CDATA[ = d.]]><xsl:value-of select="NameFK19"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK19"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK19"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK19"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK20"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK20"/><![CDATA[Repository.]]><xsl:value-of select="TableFK20"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK20"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK20"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK20"/><![CDATA[ = d.]]><xsl:value-of select="NameFK20"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK20"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK20"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK20"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK21"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK21"/><![CDATA[Repository.]]><xsl:value-of select="TableFK21"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK21"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK21"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK21"/><![CDATA[ = d.]]><xsl:value-of select="NameFK21"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK21"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK21"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK21"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK22"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK22"/><![CDATA[Repository.]]><xsl:value-of select="TableFK22"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK22"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK22"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK22"/><![CDATA[ = d.]]><xsl:value-of select="NameFK22"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK22"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK22"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK22"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK23"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK23"/><![CDATA[Repository.]]><xsl:value-of select="TableFK23"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK23"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK23"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK23"/><![CDATA[ = d.]]><xsl:value-of select="NameFK23"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK23"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK23"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK23"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK24"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK24"/><![CDATA[Repository.]]><xsl:value-of select="TableFK24"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK24"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK24"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK24"/><![CDATA[ = d.]]><xsl:value-of select="NameFK24"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK24"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK24"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK24"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK25"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK25"/><![CDATA[Repository.]]><xsl:value-of select="TableFK25"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK25"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK25"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK25"/><![CDATA[ = d.]]><xsl:value-of select="NameFK25"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK25"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK25"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK25"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK26"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK26"/><![CDATA[Repository.]]><xsl:value-of select="TableFK26"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK26"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK26"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK26"/><![CDATA[ = d.]]><xsl:value-of select="NameFK26"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK26"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK26"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK26"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK27"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK27"/><![CDATA[Repository.]]><xsl:value-of select="TableFK27"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK27"/><![CDATA[, d.Subregnum
                         select new    {
                             d.]]><xsl:value-of select="IDFK27"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK27"/><![CDATA[ = d.]]><xsl:value-of select="NameFK27"/><![CDATA[ + " " + d.Subregnum
                         };
            ViewData["]]><xsl:value-of select="NameFK27"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK27"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK27"/><![CDATA[");   
        }

        //----------------------------------------------------------------------------------------------
        private void TempDataMessage(]]><xsl:value-of select="EntityAbl"/><![CDATA[, string message)
        {
            var refID = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[;
            var refExpertID = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK1"/><![CDATA[;
            var refSourceID = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK2"/><![CDATA[;
            var refAuthorID = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK3"/><![CDATA[;
            var refExpert = string.Empty;
            var refSource = string.Empty;
            var refAuthor = string.Empty;
            if (refExpertID != null)
                { refExpert = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[; }
            if (refSourceID != null)
                { refSource = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="NameFK2"/><![CDATA[; }
            if (refAuthorID != null)
                { refAuthor = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK3"/><![CDATA[.]]><xsl:value-of select="NameFK3"/><![CDATA[; }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK27"/><![CDATA[ != null)    {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK27"/><![CDATA[.]]><xsl:value-of select="NameFK27"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK27"/><![CDATA[.Subregnum + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK27"/><![CDATA[.]]><xsl:value-of select="NameFK27"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK27"/><![CDATA[.Subregnum  + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK27"/><![CDATA[.]]><xsl:value-of select="NameFK27"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK27"/><![CDATA[.Subregnum  + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK26"/><![CDATA[ != null)    {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK26"/><![CDATA[.]]><xsl:value-of select="NameFK26"/><![CDATA[ + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK26"/><![CDATA[.]]><xsl:value-of select="NameFK26"/><![CDATA[ + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK26"/><![CDATA[.]]><xsl:value-of select="NameFK26"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK25"/><![CDATA[ != null)    {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK25"/><![CDATA[.]]><xsl:value-of select="NameFK25"/><![CDATA[ + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK25"/><![CDATA[.]]><xsl:value-of select="NameFK25"/><![CDATA[ + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK25"/><![CDATA[.]]><xsl:value-of select="NameFK25"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK24"/><![CDATA[ != null)    {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK24"/><![CDATA[.]]><xsl:value-of select="NameFK24"/><![CDATA[ + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK24"/><![CDATA[.]]><xsl:value-of select="NameFK24"/><![CDATA[ + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK24"/><![CDATA[.]]><xsl:value-of select="NameFK24"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK23"/><![CDATA[ != null)    {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK23"/><![CDATA[.]]><xsl:value-of select="NameFK23"/><![CDATA[ + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK23"/><![CDATA[.]]><xsl:value-of select="NameFK23"/><![CDATA[ + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK23"/><![CDATA[.]]><xsl:value-of select="NameFK23"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK22"/><![CDATA[ != null)    {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK22"/><![CDATA[.]]><xsl:value-of select="NameFK22"/><![CDATA[ + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK22"/><![CDATA[.]]><xsl:value-of select="NameFK22"/><![CDATA[ + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK22"/><![CDATA[.]]><xsl:value-of select="NameFK22"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK21"/><![CDATA[ != null)    {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK21"/><![CDATA[.]]><xsl:value-of select="NameFK21"/><![CDATA[ + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK21"/><![CDATA[.]]><xsl:value-of select="NameFK21"/><![CDATA[ + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK21"/><![CDATA[.]]><xsl:value-of select="NameFK21"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK20"/><![CDATA[ != null)    {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK20"/><![CDATA[.]]><xsl:value-of select="NameFK20"/><![CDATA[ + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK20"/><![CDATA[.]]><xsl:value-of select="NameFK20"/><![CDATA[ + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK20"/><![CDATA[.]]><xsl:value-of select="NameFK20"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK19"/><![CDATA[ != null)    {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK19"/><![CDATA[.]]><xsl:value-of select="NameFK19"/><![CDATA[ + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK19"/><![CDATA[.]]><xsl:value-of select="NameFK19"/><![CDATA[ + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK19"/><![CDATA[.]]><xsl:value-of select="NameFK19"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK18"/><![CDATA[ != null)    {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK18"/><![CDATA[.]]><xsl:value-of select="NameFK18"/><![CDATA[ + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK18"/><![CDATA[.]]><xsl:value-of select="NameFK18"/><![CDATA[ + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK18"/><![CDATA[.]]><xsl:value-of select="NameFK18"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK17"/><![CDATA[ != null)     {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK17"/><![CDATA[.]]><xsl:value-of select="NameFK17"/><![CDATA[ + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK17"/><![CDATA[.]]><xsl:value-of select="NameFK17"/><![CDATA[ + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK17"/><![CDATA[.]]><xsl:value-of select="NameFK17"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK16"/><![CDATA[ != null)    {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK16"/><![CDATA[.]]><xsl:value-of select="NameFK16"/><![CDATA[ + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK16"/><![CDATA[.]]><xsl:value-of select="NameFK16"/><![CDATA[ + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK16"/><![CDATA[.]]><xsl:value-of select="NameFK16"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK15"/><![CDATA[ != null)     {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK15"/><![CDATA[.]]><xsl:value-of select="NameFK15"/><![CDATA[ + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK15"/><![CDATA[.]]><xsl:value-of select="NameFK15"/><![CDATA[ + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK15"/><![CDATA[.]]><xsl:value-of select="NameFK15"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK14"/><![CDATA[ != null)     {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK14"/><![CDATA[.]]><xsl:value-of select="NameFK14"/><![CDATA[ + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK14"/><![CDATA[.]]><xsl:value-of select="NameFK14"/><![CDATA[ + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK14"/><![CDATA[.]]><xsl:value-of select="NameFK14"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK13"/><![CDATA[ != null)    {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK13"/><![CDATA[.]]><xsl:value-of select="NameFK13"/><![CDATA[ + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK13"/><![CDATA[.]]><xsl:value-of select="NameFK13"/><![CDATA[ + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK13"/><![CDATA[.]]><xsl:value-of select="NameFK13"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK12"/><![CDATA[ != null)    {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK12"/><![CDATA[.]]><xsl:value-of select="NameFK12"/><![CDATA[ + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK12"/><![CDATA[.]]><xsl:value-of select="NameFK12"/><![CDATA[ + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK12"/><![CDATA[.]]><xsl:value-of select="NameFK12"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK11"/><![CDATA[ != null)     {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK11"/><![CDATA[.]]><xsl:value-of select="NameFK11"/><![CDATA[ + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK11"/><![CDATA[.]]><xsl:value-of select="NameFK11"/><![CDATA[ + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK11"/><![CDATA[.]]><xsl:value-of select="NameFK11"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK10"/><![CDATA[ != null)    {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK10"/><![CDATA[.]]><xsl:value-of select="NameFK10"/><![CDATA[ + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK10"/><![CDATA[.]]><xsl:value-of select="NameFK10"/><![CDATA[ + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK10"/><![CDATA[.]]><xsl:value-of select="NameFK10"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK9"/><![CDATA[ != null)     {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK9"/><![CDATA[.]]><xsl:value-of select="NameFK9"/><![CDATA[ + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK9"/><![CDATA[.]]><xsl:value-of select="NameFK9"/><![CDATA[ + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK9"/><![CDATA[.]]><xsl:value-of select="NameFK9"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK8"/><![CDATA[ != null)     {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK8"/><![CDATA[.]]><xsl:value-of select="NameFK8"/><![CDATA[ + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK8"/><![CDATA[.]]><xsl:value-of select="NameFK8"/><![CDATA[ + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK8"/><![CDATA[.]]><xsl:value-of select="NameFK8"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK7"/><![CDATA[ != null)   {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK7"/><![CDATA[.]]><xsl:value-of select="NameFK7"/><![CDATA[ + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK7"/><![CDATA[.]]><xsl:value-of select="NameFK7"/><![CDATA[ + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK7"/><![CDATA[.]]><xsl:value-of select="NameFK7"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK6"/><![CDATA[ != null)    {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK6"/><![CDATA[.]]><xsl:value-of select="NameFK6"/><![CDATA[ + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK6"/><![CDATA[.]]><xsl:value-of select="NameFK6"/><![CDATA[ + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK6"/><![CDATA[.]]><xsl:value-of select="NameFK6"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK5"/><![CDATA[ != null)     {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="NameFK5"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.Divers + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="NameFK5"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.Divers + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="NameFK5"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.Divers + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK4"/><![CDATA[ != null)    {
                if (refExpertID != null)
                    TempData["message"] = refID + " / " + refExpert + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="NameFK4"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.Divers + " " + message;
                if (refSourceID != null)
                    TempData["message"] = refID + " / " + refSource + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="NameFK4"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.Divers + " " + message;
                if (refAuthorID != null)
                    TempData["message"] = refID + " / " + refAuthor + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="NameFK4"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.Divers + " " + message;
            }
        }
        //------------------------------------------------------------------------  ]]>
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
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK6"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK6"/><![CDATA[Repository.]]><xsl:value-of select="TableFK6"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK6"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK6"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK6"/><![CDATA[ = d.]]><xsl:value-of select="NameFK6"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK6"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK6"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK6"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK7"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK7"/><![CDATA[Repository.]]><xsl:value-of select="TableFK7"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK7"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK7"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK7"/><![CDATA[ = d.]]><xsl:value-of select="NameFK7"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK7"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK7"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK7"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK8"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK8"/><![CDATA[Repository.]]><xsl:value-of select="TableFK8"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK8"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK8"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK8"/><![CDATA[ = d.]]><xsl:value-of select="NameFK8"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK8"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK8"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK8"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK9"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK9"/><![CDATA[Repository.]]><xsl:value-of select="TableFK9"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK9"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK9"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK9"/><![CDATA[ = d.]]><xsl:value-of select="NameFK9"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK9"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK9"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK9"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK10"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK10"/><![CDATA[Repository.]]><xsl:value-of select="TableFK10"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK10"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK10"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK10"/><![CDATA[ = d.]]><xsl:value-of select="NameFK10"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK10"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK10"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK10"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK11"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK11"/><![CDATA[Repository.]]><xsl:value-of select="TableFK11"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK11"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK11"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK11"/><![CDATA[ = d.]]><xsl:value-of select="NameFK11"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK11"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK11"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK11"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK12"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK12"/><![CDATA[Repository.]]><xsl:value-of select="TableFK12"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK12"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK12"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK12"/><![CDATA[ = d.]]><xsl:value-of select="NameFK12"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK12"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK12"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK12"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK13"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK13"/><![CDATA[Repository.]]><xsl:value-of select="TableFK13"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK13"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK13"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK13"/><![CDATA[ = d.]]><xsl:value-of select="NameFK13"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK13"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK13"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK13"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK14"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK14"/><![CDATA[Repository.]]><xsl:value-of select="TableFK14"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK14"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK14"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK14"/><![CDATA[ = d.]]><xsl:value-of select="NameFK14"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK14"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK14"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK14"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK15"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK15"/><![CDATA[Repository.]]><xsl:value-of select="TableFK15"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK15"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK15"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK15"/><![CDATA[ = d.]]><xsl:value-of select="NameFK15"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK15"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK15"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK15"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK16"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK16"/><![CDATA[Repository.]]><xsl:value-of select="TableFK16"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK16"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK16"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK16"/><![CDATA[ = d.]]><xsl:value-of select="NameFK16"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK16"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK16"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK16"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK17"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK17"/><![CDATA[Repository.]]><xsl:value-of select="TableFK17"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK17"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK17"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK17"/><![CDATA[ = d.]]><xsl:value-of select="NameFK17"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK17"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK17"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK17"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK18"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK18"/><![CDATA[Repository.]]><xsl:value-of select="TableFK18"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK18"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK18"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK18"/><![CDATA[ = d.]]><xsl:value-of select="NameFK18"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK18"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK18"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK18"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK19"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK19"/><![CDATA[Repository.]]><xsl:value-of select="TableFK19"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK19"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK19"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK19"/><![CDATA[ = d.]]><xsl:value-of select="NameFK19"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK19"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK19"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK19"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK20"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK20"/><![CDATA[Repository.]]><xsl:value-of select="TableFK20"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK20"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK20"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK20"/><![CDATA[ = d.]]><xsl:value-of select="NameFK20"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK20"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK20"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK20"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK21"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK21"/><![CDATA[Repository.]]><xsl:value-of select="TableFK21"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK21"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK21"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK21"/><![CDATA[ = d.]]><xsl:value-of select="NameFK21"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK21"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK21"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK21"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK22"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK22"/><![CDATA[Repository.]]><xsl:value-of select="TableFK22"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK22"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK22"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK22"/><![CDATA[ = d.]]><xsl:value-of select="NameFK22"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK22"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK22"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK22"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK23"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK23"/><![CDATA[Repository.]]><xsl:value-of select="TableFK23"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK23"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK23"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK23"/><![CDATA[ = d.]]><xsl:value-of select="NameFK23"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK23"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK23"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK23"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK24"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK24"/><![CDATA[Repository.]]><xsl:value-of select="TableFK24"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK24"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK24"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK24"/><![CDATA[ = d.]]><xsl:value-of select="NameFK24"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK24"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK24"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK24"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK25"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK25"/><![CDATA[Repository.]]><xsl:value-of select="TableFK25"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK25"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK25"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK25"/><![CDATA[ = d.]]><xsl:value-of select="NameFK25"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK25"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK25"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK25"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK26"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK26"/><![CDATA[Repository.]]><xsl:value-of select="TableFK26"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK26"/><![CDATA[
                         select new    {
                             d.]]><xsl:value-of select="IDFK26"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK26"/><![CDATA[ = d.]]><xsl:value-of select="NameFK26"/><![CDATA[
                         };
            ViewData["]]><xsl:value-of select="NameFK26"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK26"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK26"/><![CDATA[");   
        }
        //------------------------------------------------------------------------  

        private void ViewData]]><xsl:value-of select="TableFK27"/><![CDATA[GetValue()  {
            var query = from d in ]]><xsl:value-of select="EntitysFK27"/><![CDATA[Repository.]]><xsl:value-of select="TableFK27"/><![CDATA[
                         orderby d.]]><xsl:value-of select="NameFK27"/><![CDATA[, d.Subregnum
                         select new    {
                             d.]]><xsl:value-of select="IDFK27"/><![CDATA[,
                             DDL]]><xsl:value-of select="NameFK27"/><![CDATA[ = d.]]><xsl:value-of select="NameFK27"/><![CDATA[ + " " + d.Subregnum
                         };
            ViewData["]]><xsl:value-of select="NameFK27"/><![CDATA[DDL"] = new SelectList(query.ToList(), "]]><xsl:value-of select="IDFK27"/><![CDATA[", "DDL]]><xsl:value-of select="NameFK27"/><![CDATA[");   
        }
        //----------------------------------------------------------------------------------------------
        private void TempDataMessage(]]><xsl:value-of select="EntityAbl"/><![CDATA[, string message)
        {
            var commentID = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[;
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK27"/><![CDATA[ != null)    {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK27"/><![CDATA[.]]><xsl:value-of select="NameFK27"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK27"/><![CDATA[.Subregnum + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK26"/><![CDATA[ != null)    {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK26"/><![CDATA[.]]><xsl:value-of select="NameFK26"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK25"/><![CDATA[ != null)    {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK25"/><![CDATA[.]]><xsl:value-of select="NameFK25"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK24"/><![CDATA[ != null)    {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK24"/><![CDATA[.]]><xsl:value-of select="NameFK24"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK23"/><![CDATA[ != null)    {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK23"/><![CDATA[.]]><xsl:value-of select="NameFK23"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK22"/><![CDATA[ != null)    {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK22"/><![CDATA[.]]><xsl:value-of select="NameFK22"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK21"/><![CDATA[ != null)    {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK21"/><![CDATA[.]]><xsl:value-of select="NameFK21"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK20"/><![CDATA[ != null)    {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK20"/><![CDATA[.]]><xsl:value-of select="NameFK20"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK19"/><![CDATA[ != null)    {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK19"/><![CDATA[.]]><xsl:value-of select="NameFK19"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK18"/><![CDATA[ != null)    {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK18"/><![CDATA[.]]><xsl:value-of select="NameFK18"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK17"/><![CDATA[ != null)     {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK17"/><![CDATA[.]]><xsl:value-of select="NameFK17"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK16"/><![CDATA[ != null)    {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK16"/><![CDATA[.]]><xsl:value-of select="NameFK16"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK15"/><![CDATA[ != null)     {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK15"/><![CDATA[.]]><xsl:value-of select="NameFK15"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK14"/><![CDATA[ != null)     {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK14"/><![CDATA[.]]><xsl:value-of select="NameFK14"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK13"/><![CDATA[ != null)    {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK13"/><![CDATA[.]]><xsl:value-of select="NameFK13"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK12"/><![CDATA[ != null)    {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK12"/><![CDATA[.]]><xsl:value-of select="NameFK12"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK11"/><![CDATA[ != null)     {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK11"/><![CDATA[.]]><xsl:value-of select="NameFK11"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK10"/><![CDATA[ != null)    {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK10"/><![CDATA[.]]><xsl:value-of select="NameFK10"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK9"/><![CDATA[ != null)     {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK9"/><![CDATA[.]]><xsl:value-of select="NameFK9"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK8"/><![CDATA[ != null)     {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK8"/><![CDATA[.]]><xsl:value-of select="NameFK8"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK7"/><![CDATA[ != null)   {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK7"/><![CDATA[.]]><xsl:value-of select="NameFK7"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK6"/><![CDATA[ != null)    {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK6"/><![CDATA[.]]><xsl:value-of select="NameFK6"/><![CDATA[ + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK5"/><![CDATA[ != null)     {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="NameFK5"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK5"/><![CDATA[.Divers + " " + message;
            }
            if (]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="IDFK4"/><![CDATA[ != null)    {
                    TempData["message"] = commentID + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="NameFK4"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK4"/><![CDATA[.Divers + " " + message;
            }
        }
        //------------------------------------------------------------------------  ]]>
</xsl:when>
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose> 

<![CDATA[        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Details/5   ]]> 
<xsl:choose>
<xsl:when test="Table ='GUID++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">      
<![CDATA[        public ActionResult Details(Guid id)    {   ]]>
</xsl:when>
<xsl:when test="Table ='aspnet_Memberships'">      
<![CDATA[        public ActionResult Details(Guid id)    {   ]]>
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">      
<![CDATA[        public ActionResult Details(Guid id)    {   ]]>
</xsl:when>
<xsl:otherwise>  
<![CDATA[        public ActionResult Details(int id)    {   ]]>
</xsl:otherwise>    
</xsl:choose>        
<![CDATA[            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);    
            return ]]><xsl:value-of select="Entity"/><![CDATA[ == null ? View("NotFound") : View("Details", ]]><xsl:value-of select="Entity"/><![CDATA[);
        }
        //------------------------------------------------------------------------  

        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        //   [OutputCache(Duration = 3600, VaryByParam = "none")]
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create()              {   
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[();   ]]>
<xsl:choose>
<xsl:when test="Table ='DDLName Create Get+++++++'">        
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
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewDataLanguagesGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewDataMimeTypesGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'"> 
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'"> 
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
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
<xsl:otherwise>           
    <xsl:if test="TableFK1 !='NULL'"><![CDATA[         ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK1"/><![CDATA[", "]]><xsl:value-of select="NameFK1"/><![CDATA[");    ]]>
    </xsl:if> 
    <xsl:if test="TableFK2 !='NULL'"><![CDATA[         ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK2"/><![CDATA[", "]]><xsl:value-of select="NameFK2"/><![CDATA[");      ]]>
    </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 
    <![CDATA[         return View(]]><xsl:value-of select="Entity"/><![CDATA[);
       }     

        // POST: /]]><xsl:value-of select="Table"/><![CDATA[/Create
        //   [OutputCache(Duration = 3600, VaryByParam = "none")]
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   ]]>
<xsl:choose>
<xsl:when test="Table ='HttpPostedFileBase++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">    
<![CDATA[        public ActionResult Create([Bind(Exclude = "]]><xsl:value-of select="ID"/><![CDATA[")]HttpPostedFileBase image)    {       ]]>
</xsl:when>
<xsl:otherwise>  
<![CDATA[        public ActionResult Create([Bind(Exclude = "]]><xsl:value-of select="ID"/><![CDATA[")]FormCollection collection)    {   
             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[();   ]]>
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
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewDataLanguagesGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
<![CDATA[             var ]]><xsl:value-of select="Entity"/><![CDATA[ = new ]]><xsl:value-of select="LinqModel"/><![CDATA[();   ]]>
<![CDATA[            var imageVideoToDatabase = new WebImage(image.InputStream);  ]]>

<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewDataMimeTypesGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'"> 
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewData["Countries"] = new SelectList(PhoneValidator.Countries, ]]><xsl:value-of select="Entity"/><![CDATA[.Country);   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90References'">  
<![CDATA[             var tables = new[] {
                         "]]><xsl:value-of select="EntitysFK4"/><![CDATA[Repository.]]><xsl:value-of select="TableFK4"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK5"/><![CDATA[Repository.]]><xsl:value-of select="TableFK5"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK6"/><![CDATA[Repository.]]><xsl:value-of select="TableFK6"/><![CDATA[",
                         "]]><xsl:value-of select="EntitysFK7"/><![CDATA[Repository.]]><xsl:value-of select="TableFK7"/><![CDATA["
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
                         "]]><xsl:value-of select="EntitysFK7"/><![CDATA[Repository.]]><xsl:value-of select="TableFK7"/><![CDATA["
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
<xsl:otherwise>           
    <xsl:if test="TableFK1 !='NULL'"><![CDATA[         ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK1"/><![CDATA[", "]]><xsl:value-of select="NameFK1"/><![CDATA[");         ]]>
    </xsl:if> 
    <xsl:if test="TableFK2 !='NULL'"><![CDATA[         ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK2"/><![CDATA[", "]]><xsl:value-of select="NameFK2"/><![CDATA[");         ]]>
    </xsl:if> 
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Counter+++++++++++++++++++++++'">        
</xsl:when><xsl:when test="Table ='aspnet_Applications'">  
    <![CDATA[         if (!TryUpdateModel(]]><xsl:value-of select="Entity"/><![CDATA[ ))
                 return View(]]><xsl:value-of select="Entity"/><![CDATA[);                         

             //Fill     ]]>          
</xsl:when>
<xsl:when test="Table ='aspnet_Memberships'">   
    <![CDATA[         if (!TryUpdateModel(]]><xsl:value-of select="Entity"/><![CDATA[ ))
                 return View(]]><xsl:value-of select="Entity"/><![CDATA[);                         

             //Fill     ]]>         
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">   
    <![CDATA[         if (!TryUpdateModel(]]><xsl:value-of select="Entity"/><![CDATA[ ))
                 return View(]]><xsl:value-of select="Entity"/><![CDATA[);                         

             //Fill     ]]>         
</xsl:when>
<xsl:when test="Table ='TblCounters'">  
    <![CDATA[         if (!TryUpdateModel(]]><xsl:value-of select="Entity"/><![CDATA[ ))
                 return View(]]><xsl:value-of select="Entity"/><![CDATA[);                         

             //Fill     ]]>      
</xsl:when>
<xsl:otherwise>  
    <![CDATA[         if (!TryUpdateModel(]]><xsl:value-of select="Entity"/><![CDATA[ ))
                 return View(]]><xsl:value-of select="Entity"/><![CDATA[);                         

             //Counter
             ]]><xsl:value-of select="Entity"/><![CDATA[.CountID =_tblCountersRepository.Counter();           
             //Fill     ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Save space instead NULL++++++++++++++++'">    
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">  
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.Subregnum == null)    {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.Subregnum = " ";
                } //fill with "" to show in DDL for Phylum and Division aso.  ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">  
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.Subspeciesgroup == null)    {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.Subspeciesgroup = " ";
                }  //fill with "" to show in DDL for FiSpecies and PlSpecies aso. ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">  
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies == null)    {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies = " ";
                } //fill with "" to show in DDL for names, Synonyms aso.  ]]>  
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.Divers == null)     {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.Divers = " ";
                } //fill with "" to show in DDL for names, Synonyms aso.  ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">   
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies == null)     {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies = " ";
                } //fill with "" to show in DDL for names, Synonyms aso.  ]]>  
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.Divers == null)    {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.Divers = " ";
                }  //fill with "" to show in DDL for names, Synonyms aso.  ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">    
<![CDATA[              ]]><xsl:value-of select="Entity"/><![CDATA[.Filestream = imageVideoToDatabase.GetBytes();  //Image    ]]>
<![CDATA[              ]]><xsl:value-of select="Entity"/><![CDATA[.FilestreamID = Guid.NewGuid();  ]]>               
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">   
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.ArticelTitle == null)     {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.ArticelTitle = " ";
                } //fill with "" to show in DDL   ]]>  
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.BookName == null)     {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.BookName = " ";
                }  //fill with "" to show in DDL  ]]>  
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.Page == null)    {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.Page = " ";
                }  //fill with "" to show in DDL  ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">   
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.Notes == null)     {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.Notes = " ";
                } //fill with "" to show in DDL   ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">   
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.SourceYear == null)     {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.SourceYear = " ";
                } //fill with "" to show in DDL   ]]>  
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.Notes == null)     {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.Notes = " ";
                } //fill with "" to show in DDL   ]]>  
</xsl:when>
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Writer+Updater+++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">   
     <![CDATA[     
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Add(]]><xsl:value-of select="Entity"/><![CDATA[);   
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>          
</xsl:when>
<xsl:when test="Table ='aspnet_Membership'">    
     <![CDATA[     
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Add(]]><xsl:value-of select="Entity"/><![CDATA[);   
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>         
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">    
     <![CDATA[     
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Add(]]><xsl:value-of select="Entity"/><![CDATA[);   
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>         
</xsl:when>
<xsl:when test="Table ='TblCounters'"> 
     <![CDATA[     
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Add(]]><xsl:value-of select="Entity"/><![CDATA[);   
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>       
</xsl:when>
<xsl:otherwise>  
     <![CDATA[     
            ]]><xsl:value-of select="Entity"/><![CDATA[.Writer = Environment.UserName;
            ]]><xsl:value-of select="Entity"/><![CDATA[.WriterDate = DateTime.Now;
            ]]><xsl:value-of select="Entity"/><![CDATA[.Updater = Environment.UserName;
            ]]><xsl:value-of select="Entity"/><![CDATA[.UpdaterDate = DateTime.Now;

            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Add(]]><xsl:value-of select="Entity"/><![CDATA[);   
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Tbl69FiSpeciesses'">    
         <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;    ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">    
         <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;    ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">    
        <![CDATA[   if (]]><xsl:value-of select="Entity"/><![CDATA[.PlSpeciesID == 2)  
              TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;    
            else                  
              TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="NameFK2"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;       ]]>
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">    
        <![CDATA[   if (]]><xsl:value-of select="Entity"/><![CDATA[.PlSpeciesID == 2)  
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
    <![CDATA[           TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + SharedRes.StringsRes.TempDataSavedMessage;   ]]>
</xsl:otherwise>    
</xsl:choose>   

<xsl:choose>
<xsl:when test="Table ='Tbl81Images'">    
<![CDATA[   
            //return RedirectToAction("Index");
            // return RedirectToAction("Create");
            return View(]]><xsl:value-of select="Entity"/><![CDATA[);    //redisplay same view not possible Filestream new one expected                     
        }  ]]>
</xsl:when>
<xsl:otherwise> 
<![CDATA[   
            //return RedirectToAction("Index");
            //return RedirectToAction("Create");
            return View(]]><xsl:value-of select="Entity"/><![CDATA[);    //redisplay same view                     
        }  ]]>  
</xsl:otherwise>   
</xsl:choose>   
 <![CDATA[         //------------------------------------------------------------------------

        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Edit/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]      ]]>
<xsl:choose>
<xsl:when test="Table ='GUID++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">      
<![CDATA[        public ActionResult Edit(Guid id)    {   ]]>
</xsl:when>
<xsl:when test="Table ='aspnet_Memberships'">      
<![CDATA[        public ActionResult Edit(Guid id)    {   ]]>
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">      
<![CDATA[        public ActionResult Edit(Guid id)    {   ]]>
</xsl:when>
<xsl:otherwise>  
<![CDATA[        public ActionResult Edit(int id)    {   ]]>
</xsl:otherwise>    
</xsl:choose>        
<![CDATA[             var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);    ]]>

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
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewDataLanguagesGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewDataMimeTypesGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'"> 
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
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
<xsl:otherwise>           
    <xsl:if test="TableFK1 !='NULL'"><![CDATA[         ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK1"/><![CDATA[", "]]><xsl:value-of select="NameFK1"/><![CDATA[");         ]]>
    </xsl:if> 
    <xsl:if test="TableFK2 !='NULL'"><![CDATA[         ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK2"/><![CDATA[", "]]><xsl:value-of select="NameFK2"/><![CDATA[");         ]]>
    </xsl:if> 
</xsl:otherwise>    
</xsl:choose>
        <![CDATA[     return View(]]><xsl:value-of select="Entity"/><![CDATA[);
        }

        //
        // POST: /]]><xsl:value-of select="Table"/><![CDATA[/Edit/5  
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   ]]>
<xsl:choose>
<xsl:when test="Table ='GUID+Image++++++++++++++++++'">      
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">   
<![CDATA[         public ActionResult Edit(Guid id,  FormCollection collection)   { ]]>     
</xsl:when>
<xsl:when test="Table ='aspnet_Memberships'">   
<![CDATA[         public ActionResult Edit(Guid id,  FormCollection collection)   { ]]>     
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">   
<![CDATA[         public ActionResult Edit(Guid id,  FormCollection collection)   { ]]>     
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">   
<![CDATA[         public ActionResult Edit(int id,  HttpPostedFileBase image)   { ]]>     
</xsl:when>
<xsl:otherwise>  
<![CDATA[         public ActionResult Edit(int id,  FormCollection collection)  {  ]]>
</xsl:otherwise>    
</xsl:choose> 
<![CDATA[          
            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);   ]]>

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
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewDataLanguagesGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
<![CDATA[             ViewDataMimeTypesGetValue(]]><xsl:value-of select="Entity"/><![CDATA[);  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'"> 
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
<![CDATA[             ViewData]]><xsl:value-of select="Table"/><![CDATA[GetValue();  ]]>
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
<xsl:otherwise>           
    <xsl:if test="TableFK1 !='NULL'"><![CDATA[         ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK1"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK1"/><![CDATA[", "]]><xsl:value-of select="NameFK1"/><![CDATA[");         ]]>
    </xsl:if> 
    <xsl:if test="TableFK2 !='NULL'"><![CDATA[         ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] = new SelectList(]]><xsl:value-of select="EntitysFK2"/><![CDATA[Repository.FindAllSort().ToList(), "]]><xsl:value-of select="IDFK2"/><![CDATA[", "]]><xsl:value-of select="NameFK2"/><![CDATA[");         ]]>
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
                    tbl81Image.Filestream = imageVideoToDatabase.GetBytes();  //Image   

                ]]><xsl:value-of select="Entity"/><![CDATA[.Updater = Environment.UserName;
                ]]><xsl:value-of select="Entity"/><![CDATA[.UpdaterDate = DateTime.Now;

                ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   
            }
            else  {
            if(!TryUpdateModel(]]><xsl:value-of select="Entity"/><![CDATA[))
                return View(]]><xsl:value-of select="Entity"/><![CDATA[);

                //Fill   
                ]]><xsl:value-of select="Entity"/><![CDATA[.Updater = Environment.UserName;
                ]]><xsl:value-of select="Entity"/><![CDATA[.UpdaterDate = DateTime.Now;

                ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   
            }   ]]>   
</xsl:when>
<xsl:otherwise>  
   <![CDATA[                         
            if(!TryUpdateModel(]]><xsl:value-of select="Entity"/><![CDATA[))
                return View(]]><xsl:value-of select="Entity"/><![CDATA[);
                
            //Fill     ]]>
</xsl:otherwise>    
</xsl:choose> 
<xsl:choose>
<xsl:when test="Table ='Save space instead NULL++++++++++++++++'">    
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">  
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.Subregnum == null)    {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.Subregnum = " ";
                } //fill with "" to show in DDL for Phylum and Division aso.  ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">  
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.Subspeciesgroup == null)     {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.Subspeciesgroup = " ";
                } //fill with "" to show in DDL for FiSpecies and PlSpecies aso.  ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">  
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies == null)    {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies = " ";
                } //fill with "" to show in DDL for names, Synonyms aso.  ]]>  
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.Divers == null)   {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.Divers = " ";
                }  //fill with "" to show in DDL for names, Synonyms aso. ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">   
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies == null)   {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies = " ";
                } //fill with "" to show in DDL for names, Synonyms aso.  ]]>  
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.Divers == null)     {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.Divers = " ";
                } //fill with "" to show in DDL for names, Synonyms aso.  ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">    
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">   
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.ArticelTitle == null)     {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.ArticelTitle = " ";
                } //fill with "" to show in DDL   ]]>  
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.BookName == null)     {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.BookName = " ";
                } //fill with "" to show in DDL   ]]>  
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.Page == null)     {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.Page = " ";
                } //fill with "" to show in DDL   ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">   
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.Notes == null)     {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.Notes = " ";
                } //fill with "" to show in DDL   ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">   
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.SourceYear == null)     {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.SourceYear = " ";
                } //fill with "" to show in DDL   ]]>  
 <![CDATA[               if (]]><xsl:value-of select="Entity"/><![CDATA[.Notes == null)     {    
                    ]]><xsl:value-of select="Entity"/><![CDATA[.Notes = " ";
                } //fill with "" to show in DDL   ]]>  
</xsl:when>
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Save+++++++++++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">       
     <![CDATA[                ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>   
</xsl:when>
<xsl:when test="Table ='aspnet_Membership'">       
     <![CDATA[                ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>   
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">       
     <![CDATA[                ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>   
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">        
</xsl:when>
<xsl:when test="Table ='TblCounters'">       
     <![CDATA[                ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>   
</xsl:when>
<xsl:otherwise>  
     <![CDATA[     
                ]]><xsl:value-of select="Entity"/><![CDATA[.Updater = Environment.UserName;
                ]]><xsl:value-of select="Entity"/><![CDATA[.UpdaterDate = DateTime.Now;

                ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   ]]>   
</xsl:otherwise> 
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Tbl69FiSpeciesses'">    
         <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;    ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">    
         <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;    ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">    
        <![CDATA[   if (]]><xsl:value-of select="Entity"/><![CDATA[.PlSpeciesID == 2)  
              TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;    
            else                  
              TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="NameFK2"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK2"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;       ]]>
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">    
        <![CDATA[   if (]]><xsl:value-of select="Entity"/><![CDATA[.PlSpeciesID == 2)  
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
    <![CDATA[       TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + SharedRes.StringsRes.TempDataSavedMessage;   ]]>
</xsl:otherwise>    
</xsl:choose>   <![CDATA[   
            return RedirectToAction("Index");         
        }
        //-----------------------------------------------------------------------------------------------------

        //
        // GET: /]]><xsl:value-of select="Table"/><![CDATA[/Delete/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]    ]]>
<xsl:choose>
<xsl:when test="Table ='GUID+++++++++++++++++++'">      
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">   
<![CDATA[         public ActionResult Delete(Guid id)   { ]]>     
</xsl:when>
<xsl:when test="Table ='aspnet_Memberships'">   
<![CDATA[         public ActionResult Delete(Guid id)   { ]]>     
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">   
<![CDATA[         public ActionResult Delete(Guid id)   { ]]>     
</xsl:when>
<xsl:otherwise>  
<![CDATA[         public ActionResult Delete(int id)  {  ]]>
</xsl:otherwise>    
</xsl:choose> 
<![CDATA[            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);   
            return ]]><xsl:value-of select="Entity"/><![CDATA[ == null ? View("NotFound") : View(]]><xsl:value-of select="Entity"/><![CDATA[);                      
        }

        //
        // POST: /]]><xsl:value-of select="Table"/><![CDATA[/Delete/5
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]     ]]>

<xsl:choose>
<xsl:when test="Table ='GUID+++++++++++++++++++'">      
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">   
<![CDATA[         public ActionResult Delete(Guid id, string confirmButton)    {    ]]>     
</xsl:when>
<xsl:when test="Table ='aspnet_Memberships'">   
<![CDATA[         public ActionResult Delete(Guid id, string confirmButton)    {    ]]>     
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">   
<![CDATA[         public ActionResult Delete(Guid id, string confirmButton)    {    ]]>     
</xsl:when>
<xsl:otherwise>  
<![CDATA[         public ActionResult Delete(int id, string confirmButton)    {  ]]>
</xsl:otherwise>    
</xsl:choose> 
<![CDATA[            var ]]><xsl:value-of select="Entity"/><![CDATA[ = ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Get(id);   

            if (]]><xsl:value-of select="Entity"/><![CDATA[ == null)
                return RedirectToAction("NotFound");
            
                ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Delete(]]><xsl:value-of select="Entity"/><![CDATA[);   ]]>
<xsl:choose>
<xsl:when test="Table ='Tbl69FiSpeciesses'">    
             <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;    ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">    
             <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " / " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Subspecies + " " + ]]><xsl:value-of select="Entity"/><![CDATA[.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;    ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">    
             <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " " + SharedRes.StringsRes.TempDataDeletedMessage;    ]]>
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">    
             <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " " + SharedRes.StringsRes.TempDataDeletedMessage;    ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">    
             <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " /  " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + SharedRes.StringsRes.TempDataDeletedMessage;    ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90References'">    
             <![CDATA[  TempDataMessage(]]><xsl:value-of select="Entity"/><![CDATA[, SharedRes.StringsRes.TempDataDeletedMessage);    ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">    
             <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " /  " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + SharedRes.StringsRes.TempDataDeletedMessage;    ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">    
             <![CDATA[  TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ + " /  " + ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + SharedRes.StringsRes.TempDataDeletedMessage;    ]]>
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">    
             <![CDATA[  TempDataMessage(]]><xsl:value-of select="Entity"/><![CDATA[, SharedRes.StringsRes.TempDataDeletedMessage);    ]]>
</xsl:when>
<xsl:otherwise>
    <![CDATA[            TempData["message"] = ]]><xsl:value-of select="Entity"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " " + SharedRes.StringsRes.TempDataDeletedMessage;   ]]>
</xsl:otherwise>    
</xsl:choose>   <![CDATA[   
                                               
            ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.Save();   
            return RedirectToAction("Index");
            //   return RedirectToAction("Create");
            // return View(]]><xsl:value-of select="Entity"/><![CDATA[); //redisplay same view
        }   ]]> 

        public ActionResult NotFound()    {
            throw new NotImplementedException();
        }
<![CDATA[  }
}]]>   

</xsl:template>
</xsl:stylesheet>











