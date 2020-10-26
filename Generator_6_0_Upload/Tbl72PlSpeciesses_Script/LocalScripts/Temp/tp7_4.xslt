<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[@model Atis.Domain.Models.]]><xsl:value-of select="LinqModel"/><![CDATA[

       <!-- Edit Skriptdatum: ]]> <xsl:value-of select="DateTime"/>  <![CDATA[  -->

@{
    ViewBag.Title = ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.EditTop;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Edit</h2>

@Html.Partial("_Form");

   ]]> 
<xsl:choose>
<xsl:when test="Table ='Tbl03Regnums'">        
</xsl:when>
<xsl:otherwise>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Property Part 8.2  HeaderedContentControl Connected   TK1  Bottom 1 Tbl66Genusses + Tbl68Speciesgroups +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Property Part 8.2  HeaderedContentControl Connected   TK1  Bottom 1 Tbl66Genusses + Tbl68Speciesgroups +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">       
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">       
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">       
</xsl:when>
<xsl:when test="Table ='Tbl12Subphylums'">       
</xsl:when>
<xsl:when test="Table ='Tbl15Sundivisions'">       
</xsl:when>
<xsl:when test="Table ='Tbl18Superclasses'">       
</xsl:when>
<xsl:when test="Table ='Tbl21Classes'">       
</xsl:when>
<xsl:when test="Table ='Tbl24Subclasses'">       
</xsl:when>
<xsl:when test="Table ='Tbl27Infraclasses'">       
</xsl:when>
<xsl:when test="Table ='Tbl30Legios'">       
</xsl:when>
<xsl:when test="Table ='Tbl33Ordos'">       
</xsl:when>
<xsl:when test="Table ='Tbl36Subordoss'">       
</xsl:when>
<xsl:when test="Table ='Tbl39Infra0rdos'">       
</xsl:when>
<xsl:when test="Table ='Tbl42Superfamilies'">       
</xsl:when>
<xsl:when test="Table ='Tbl45Families'">       
</xsl:when>
<xsl:when test="Table ='Tbl48Subfamilies'">       
</xsl:when>
<xsl:when test="Table ='Tbl51Infrafamilies'">       
</xsl:when>
<xsl:when test="Table ='Tbl54Supertribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl57Tribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl60Subtribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl63Infratribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl66Genusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">       
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">       
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">       
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">  
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">  
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">             
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">             
</xsl:when>
<xsl:when test="Table ='Tbl90References'">    
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">             
</xsl:when>
<xsl:when test="Table ='TblCountries'">             
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">             
</xsl:when>
<xsl:otherwise>   
</xsl:otherwise>    
</xsl:choose>                                                          

<xsl:if test="Table='Tbl03Regnums'">  
</xsl:if>   
<xsl:if test="Table='Tbl06Phylums'">
</xsl:if>   
<xsl:if test="Table='Tbl16SubphylSubdivs'">
</xsl:if>   
<xsl:if test="Table='Tbl22Superclassis'">
</xsl:if>   
<xsl:if test="Table='Tbl25Classis'">
</xsl:if>   
<xsl:if test="Table='Tbl28Subclassis'">
</xsl:if>   
<xsl:if test="Table='Tbl31Infraclassis'">
</xsl:if> 
<xsl:if test="Table='Tbl34Legios'">
</xsl:if> 
<xsl:if test="Table='Tbl37Ordos'">
</xsl:if> 
<xsl:if test="Table='Tbl40Subordos'">
</xsl:if> 
<xsl:if test="Table='Tbl43Infraordos'">
</xsl:if>  
<xsl:if test="Table='Tbl46Superfamilias'">
</xsl:if> 
<xsl:if test="Table='Tbl49Familias'">
</xsl:if> 
<xsl:if test="Table='Tbl52Subfamilias'">
</xsl:if> 
<xsl:if test="Table='Tbl55Infrafamilias'">
</xsl:if> 
<xsl:if test="Table='Tbl58Supertribus'">
</xsl:if> 
<xsl:if test="Table='Tbl61Tribus'">
</xsl:if> 
<xsl:if test="Table='Tbl64Subtribus'">
</xsl:if> 
<xsl:if test="Table='Tbl67Infratribus'">
</xsl:if> 


</xsl:template>
</xsl:stylesheet>


















