<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Atis.Domain.Models;

// <!-- ListViewModel Skriptdatum: ]]> <xsl:value-of select="DateTime"/>  <![CDATA[  -->  

namespace ]]><xsl:value-of select="Namespace"/><![CDATA[.Domain.ViewModels.]]><xsl:value-of select="Table"/><![CDATA[
{    ]]>
<xsl:choose>
<xsl:when test="Table ='+++default Values+++++++++++++++'">
</xsl:when> 
<xsl:when test="Table ='AspnetApplications'">   
   <![CDATA[ public class ListViewModel                     
    {   
        // Constructor
        public ListViewModel()
        {
            // Define any default values here...
            PageSize = 10;
            NumericPageCount = 10;
        } ]]>   
</xsl:when>
<xsl:when test="Table ='AspnetMemberships'">     
   <![CDATA[ public class ListViewModel                     
    {   
        // Constructor
        public ListViewModel()
        {
            // Define any default values here...
            PageSize = 10;
            NumericPageCount = 10;
        } ]]> 
</xsl:when>
<xsl:when test="Table ='AspnetUsers'">     
   <![CDATA[ public class ListViewModel                     
    {   
        // Constructor
        public ListViewModel()
        {
            // Define any default values here...
            PageSize = 10;
            NumericPageCount = 10;
        } ]]> 
</xsl:when> 
<xsl:otherwise>
   <![CDATA[ public class ListViewModel                     
    {   ]]>
<![CDATA[        // Constructor
        public ListViewModel()
        {
            // Define any default values here...
            PageSize = 10;
            NumericPageCount = 10;
            Valid = false; 
        } ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Properties++++++++'">
</xsl:when>  
<xsl:otherwise>
   <![CDATA[     // Data properties  
         public IEnumerable<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[ { get; set; }    ]]>
</xsl:otherwise>    
</xsl:choose> 
   <![CDATA[   
        // Sorting-related properties
        public string SortBy { get; set; }
        public bool SortAscending { get; set; }
        public string SortExpression
        {
            get
            {
                return SortAscending ? SortBy + " asc" : SortBy + " desc";
            }
        }

        // Paging-related properties
        public int CurrentPageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRecordCount { get; set; }
        public int PageCount
        {
            get
            {
                return Math.Max(TotalRecordCount / PageSize, 1);
            }
        }
        public int NumericPageCount { get; set; }

        // Filtering-related properties    ]]>

<xsl:choose>
<xsl:when test="Table ='+++Filter Prperties+++++++++++++++'">
</xsl:when>  
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose>    

<xsl:choose>
<xsl:when test="Table ='GUID++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='AspnetApplications'">      
</xsl:when>
<xsl:when test="Table ='AspnetMemberships'">      
</xsl:when>
<xsl:when test="Table ='AspnetUsers'">      
</xsl:when>
<xsl:otherwise>  
       <![CDATA[ public bool Valid { get; set; }  ]]>                     
</xsl:otherwise>    
</xsl:choose>        

<xsl:choose>
<xsl:when test="Table ='GUID++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='AspnetApplications'">      
</xsl:when>
<xsl:when test="Table ='AspnetMemberships'">    
  <xsl:if test="TableFK1 !='NULL'">
       <![CDATA[ public Guid? ]]><xsl:value-of select="IDFK1"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK1"/><![CDATA[ { get; set; }   ]]>
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK1"/><![CDATA[List { get; set; }  ]]> 
  </xsl:if> 
  <xsl:if test="TableFK2 !='NULL'">
       <![CDATA[ public Guid? ]]><xsl:value-of select="IDFK2"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK2"/><![CDATA[ { get; set; }    ]]>
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK2"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK3 !='NULL'">
       <![CDATA[ public Guid? ]]><xsl:value-of select="IDFK3"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK3"/><![CDATA[ { get; set; }    ]]>
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK3"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if>       
</xsl:when>
<xsl:when test="Table ='AspnetUsers'">  
  <xsl:if test="TableFK1 !='NULL'">
       <![CDATA[ public Guid? ]]><xsl:value-of select="IDFK1"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK1"/><![CDATA[ { get; set; }   ]]>
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK1"/><![CDATA[List { get; set; }  ]]> 
  </xsl:if> 
  <xsl:if test="TableFK2 !='NULL'">
       <![CDATA[ public Guid? ]]><xsl:value-of select="IDFK2"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK2"/><![CDATA[ { get; set; }    ]]>
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK2"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK3 !='NULL'">
       <![CDATA[ public Guid? ]]><xsl:value-of select="IDFK3"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK3"/><![CDATA[ { get; set; }    ]]>
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK3"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if>     
</xsl:when>
<xsl:otherwise>  
  <xsl:if test="TableFK1 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK1"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK1"/><![CDATA[ { get; set; }   ]]>
     <xsl:if test="TableFK1 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK1 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK1"/><![CDATA[List { get; set; }  ]]> 
  </xsl:if> 
  <xsl:if test="TableFK2 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK2"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK2"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK2 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK2 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK2"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK3 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK3"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK3"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK3 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK3 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK3"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose>        
   
  <xsl:if test="TableFK4 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK4"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK4"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK4 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK4 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK4"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK5 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK5"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK5"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK5 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK5 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK5"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK6 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK6"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK6"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK6 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK6 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK6"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK7 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK7"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK7"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK7 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK7 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK7"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK8 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK8"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK8"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK8 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK8 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK8"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK9 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK9"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK9"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK9 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK9 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK9"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK10 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK10"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK10"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK10 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK10 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK10"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK11 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK11"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK11"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK11 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK11 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK11"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK12 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK12"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK12"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK12 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK12 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK12"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK13 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK13"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK13"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK13 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK13 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK13"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK14 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK14"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK14"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK14 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK14 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK14"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK15 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK15"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK15"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK15 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK15 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK15"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK16 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK16"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK16"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK16 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK16 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK16"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK17 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK17"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK17"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK17 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK17 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK17"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK18 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK18"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK18"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK18 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK18 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK18"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK19 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK19"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK19"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK19 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK19 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK19"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK20 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK20"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK20"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK20 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK20 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK20"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK21 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK21"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK21"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK21 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK21 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK21"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK22 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK22"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK22"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK22 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK22 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK22"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK23 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK23"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK23"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK23 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK23 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK23"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK24 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK24"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK24"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK24 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK24 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK24"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK25 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK25"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK25"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK25 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK25 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK25"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK26 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK26"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK26"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK26 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK26 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK26"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 
  <xsl:if test="TableFK27 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDFK27"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK27"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK27 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>  
     <xsl:if test="TableFK27 ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
      </xsl:if>  
       <![CDATA[ public IEnumerable<SelectListItem>]]><xsl:value-of select="BasisFK27"/><![CDATA[List { get; set; }  ]]>                
  </xsl:if> 

<xsl:choose>
<xsl:when test="Table ='GUID++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='AspnetApplications'">      
      <![CDATA[  
        public Guid? ]]><xsl:value-of select="ID"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="Name"/><![CDATA[ { get; set; }  ]]>
</xsl:when>
<xsl:when test="Table ='AspnetMemberships'">      
      <![CDATA[  
        public Guid? ]]><xsl:value-of select="ID"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="Name"/><![CDATA[ { get; set; }  ]]>
</xsl:when>
<xsl:when test="Table ='AspnetUsers'">      
      <![CDATA[  
        public Guid? ]]><xsl:value-of select="ID"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="Name"/><![CDATA[ { get; set; }  ]]>
</xsl:when>
<xsl:otherwise>  
      <![CDATA[  
        public int? ]]><xsl:value-of select="ID"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="Name"/><![CDATA[ { get; set; }  ]]>
</xsl:otherwise>    
</xsl:choose>        

    <xsl:if test="Table ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
    </xsl:if>
    <xsl:if test="Table ='Tbl68Speciesgroups'">
       <![CDATA[ public string Subspeciesgroup { get; set; }  ]]>
    </xsl:if>
    <![CDATA[}
}]]>   

</xsl:template>
</xsl:stylesheet>



