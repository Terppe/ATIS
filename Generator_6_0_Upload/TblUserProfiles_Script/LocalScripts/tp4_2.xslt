<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[using System;
using System.Collections.Generic;
using Atis.Domain.Models;

// <!-- ReportViewModel Skriptdatum: ]]> <xsl:value-of select="DateTime"/>  <![CDATA[  -->  

namespace ]]><xsl:value-of select="Namespace"/><![CDATA[.Domain.ViewModels.]]><xsl:value-of select="Table"/><![CDATA[     {    ]]>
<xsl:choose>
<xsl:when test="Table ='+++default Values+++++++++++++++'">
</xsl:when> 
<xsl:when test="Table ='AspnetApplications'">
   <![CDATA[ public class ReportViewModel                         {   ]]>
</xsl:when>  
<xsl:when test="Table ='AspnetMemberships'">
   <![CDATA[ public class ReportViewModel                         {   ]]>
</xsl:when>  
<xsl:when test="Table ='AspnetUsers'">
   <![CDATA[ public class ReportViewModel                         {   ]]>
</xsl:when>   
<xsl:when test="Table ='TblCounters'">
   <![CDATA[ public class ReportViewModel                         {     ]]>
</xsl:when>  
<xsl:otherwise>
   <![CDATA[ public class ReportViewModel                         {   
        // Constructor
        public ReportViewModel()     {
            // Define any default values here...
            Valid = false; 
        } ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Properties++++++++'">
</xsl:when>  
<xsl:otherwise>
   <![CDATA[      // Data properties  ]]>
    <xsl:if test="TableBK1 !='NULL'">
        <![CDATA[ public IEnumerable<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="TableBK1"/><![CDATA[ { get; set; }  ]]>                                 
    </xsl:if> 
    <xsl:if test="TableBK2 !='NULL'">
        <![CDATA[ public IEnumerable<]]><xsl:value-of select="LinqModelBK2"/><![CDATA[> ]]><xsl:value-of select="TableBK2"/><![CDATA[ { get; set; }  ]]>                                 
    </xsl:if> 
    <xsl:if test="TableBK3 !='NULL'">
        <![CDATA[ public IEnumerable<]]><xsl:value-of select="LinqModelBK3"/><![CDATA[> ]]><xsl:value-of select="TableBK3"/><![CDATA[ { get; set; }  ]]>                                 
    </xsl:if> 
    <xsl:if test="TableBK4 !='NULL'">
        <![CDATA[ public IEnumerable<]]><xsl:value-of select="LinqModelBK4"/><![CDATA[> ]]><xsl:value-of select="TableBK4"/><![CDATA[ { get; set; }  ]]>                                 
    </xsl:if> 
    <xsl:if test="TableBK5 !='NULL'">
        <![CDATA[ public IEnumerable<]]><xsl:value-of select="LinqModelBK5"/><![CDATA[> ]]><xsl:value-of select="TableBK5"/><![CDATA[ { get; set; }  ]]>                                 
    </xsl:if> 
    <xsl:if test="TableBK6 !='NULL'">
        <![CDATA[ public IEnumerable<]]><xsl:value-of select="LinqModelBK6"/><![CDATA[> ]]><xsl:value-of select="TableBK6"/><![CDATA[ { get; set; }  ]]>                                 
    </xsl:if> 
    <xsl:if test="TableBK7 !='NULL'">
        <![CDATA[ public IEnumerable<]]><xsl:value-of select="LinqModelBK7"/><![CDATA[> ]]><xsl:value-of select="TableBK7"/><![CDATA[ { get; set; }  ]]>                                 
    </xsl:if> 
    <xsl:if test="TableBK8 !='NULL'">
        <![CDATA[ public IEnumerable<]]><xsl:value-of select="LinqModelBK8"/><![CDATA[> ]]><xsl:value-of select="TableBK8"/><![CDATA[ { get; set; }  ]]>                                 
    </xsl:if> 
    <xsl:if test="TableBK9 !='NULL'">
        <![CDATA[ public IEnumerable<]]><xsl:value-of select="LinqModelBK9"/><![CDATA[> ]]><xsl:value-of select="TableBK9"/><![CDATA[ { get; set; }  ]]>                                 
    </xsl:if> 
    <xsl:if test="TableBK10 !='NULL'">
        <![CDATA[ public IEnumerable<]]><xsl:value-of select="LinqModelBK10"/><![CDATA[> ]]><xsl:value-of select="TableBK10"/><![CDATA[ { get; set; }  ]]>                                 
    </xsl:if> 

    <xsl:if test="TableFK1 !='NULL'">
        <![CDATA[ public IEnumerable<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[ { get; set; }  ]]>                                 
    </xsl:if> 
    <xsl:if test="TableFK2 !='NULL'">
        <![CDATA[ public IEnumerable<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[ { get; set; }  ]]>                                 
    </xsl:if> 
    <xsl:if test="TableFK3 !='NULL'">
        <![CDATA[ public IEnumerable<]]><xsl:value-of select="LinqModelFK3"/><![CDATA[> ]]><xsl:value-of select="TableFK3"/><![CDATA[ { get; set; }  ]]>                                 
    </xsl:if> 
<![CDATA[         public IEnumerable<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[ { get; set; }    ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Properties+++++++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='AspnetApplications'">      
</xsl:when>
<xsl:when test="Table ='AspnetMemberships'">      
</xsl:when>
<xsl:when test="Table ='AspnetUsers'">      
</xsl:when>
<xsl:when test="Table ='TblCounters'">  
   <![CDATA[     // Properties        ]]>      
</xsl:when>
<xsl:otherwise>  
   <![CDATA[     // Properties        

        public string Author { get; set; }
        public DateTime AuthorYear { get; set; }
        public bool Valid { get; set; }
        public string EngName { get; set; }
        public string GerName { get; set; }
        public string FraName { get; set; }
        public string PorName { get; set; }

        public int? CountID { get; set; }
        public string Synonym { get; set; }  ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='+++Filter Properties+++++++++++++++'">
</xsl:when>  
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose> 
  <xsl:if test="TableTK1 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDTK1"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameTK1"/><![CDATA[ { get; set; }   ]]>
     <xsl:if test="TableTK1 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
       <![CDATA[ public IList<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> ]]><xsl:value-of select="TableTK1"/><![CDATA[SearchResults { get; set; }  ]]>
  </xsl:if> 
  <xsl:if test="TableTK2 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDTK2"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameTK2"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableTK2 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
       <![CDATA[ public IList<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> ]]><xsl:value-of select="TableTK2"/><![CDATA[SearchResults { get; set; }    ]]>                         
  </xsl:if> 
  <xsl:if test="TableTK3 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDTK3"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameTK3"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableTK3 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
       <![CDATA[ public IList<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[> ]]><xsl:value-of select="TableTK3"/><![CDATA[SearchResults { get; set; }    ]]>                         
  </xsl:if> 
  <xsl:if test="TableTK4 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDTK4"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameTK4"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableTK4 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
       <![CDATA[ public IList<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[> ]]><xsl:value-of select="TableTK4"/><![CDATA[SearchResults { get; set; }    ]]>                         
  </xsl:if> 
  <xsl:if test="TableTK5 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDTK5"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameTK5"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableTK5 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
       <![CDATA[ public IList<]]><xsl:value-of select="LinqModelTK5"/><![CDATA[> ]]><xsl:value-of select="TableTK5"/><![CDATA[SearchResults { get; set; }    ]]>                         
  </xsl:if> 
  <xsl:if test="TableTK6 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDTK6"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameTK6"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableTK6 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
       <![CDATA[ public IList<]]><xsl:value-of select="LinqModelTK6"/><![CDATA[> ]]><xsl:value-of select="TableTK6"/><![CDATA[SearchResults { get; set; }    ]]>                         
  </xsl:if> 
  <xsl:if test="TableTK7 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDTK7"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameTK7"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableTK7 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
       <![CDATA[ public IList<]]><xsl:value-of select="LinqModelTK7"/><![CDATA[> ]]><xsl:value-of select="TableTK7"/><![CDATA[SearchResults { get; set; }    ]]>                         
  </xsl:if> 
  <xsl:if test="TableTK8 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDTK8"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameTK8"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableTK8 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
       <![CDATA[ public IList<]]><xsl:value-of select="LinqModelTK8"/><![CDATA[> ]]><xsl:value-of select="TableTK8"/><![CDATA[SearchResults { get; set; }    ]]>                         
  </xsl:if> 
  <xsl:if test="TableTK9 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDTK9"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameTK9"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableTK9 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
       <![CDATA[ public IList<]]><xsl:value-of select="LinqModelTK9"/><![CDATA[> ]]><xsl:value-of select="TableTK9"/><![CDATA[SearchResults { get; set; }    ]]>                         
  </xsl:if> 
  <xsl:if test="TableTK10 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDTK10"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameTK10"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableTK10 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
       <![CDATA[ public IList<]]><xsl:value-of select="LinqModelTK10"/><![CDATA[> ]]><xsl:value-of select="TableTK10"/><![CDATA[SearchResults { get; set; }    ]]>                         
  </xsl:if> 
  <xsl:if test="TableTK11 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDTK11"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameTK11"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableTK11 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
       <![CDATA[ public IList<]]><xsl:value-of select="LinqModelTK11"/><![CDATA[> ]]><xsl:value-of select="TableTK11"/><![CDATA[SearchResults { get; set; }    ]]>                         
  </xsl:if> 
  <xsl:if test="TableTK12 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDTK12"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameTK12"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableTK12 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
       <![CDATA[ public IList<]]><xsl:value-of select="LinqModelTK12"/><![CDATA[> ]]><xsl:value-of select="TableTK12"/><![CDATA[SearchResults { get; set; }    ]]>                         
  </xsl:if> 

<xsl:choose>
<xsl:when test="Table ='GUID++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='AspnetApplications'">   
</xsl:when>
<xsl:when test="Table ='AspnetMemberships'"> 
 <xsl:if test="TableFK1 !='NULL'">
       <![CDATA[ public Guid? ]]><xsl:value-of select="IDFK1"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK1"/><![CDATA[ { get; set; }   ]]>
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[SearchResults { get; set; }  ]]>
 </xsl:if> 
 <xsl:if test="TableFK2 !='NULL'">
       <![CDATA[ public Guid? ]]><xsl:value-of select="IDFK2"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK2"/><![CDATA[ { get; set; }    ]]>
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 
 <xsl:if test="TableFK3 !='NULL'">
       <![CDATA[ public Guid? ]]><xsl:value-of select="IDFK3"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK3"/><![CDATA[ { get; set; }    ]]>
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelFK3"/><![CDATA[> ]]><xsl:value-of select="TableFK3"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if>     
</xsl:when>
<xsl:when test="Table ='AspnetUsers'"> 
 <xsl:if test="TableFK1 !='NULL'">
       <![CDATA[ public Guid? ]]><xsl:value-of select="IDFK1"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK1"/><![CDATA[ { get; set; }   ]]>
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[SearchResults { get; set; }  ]]>
 </xsl:if> 
 <xsl:if test="TableFK2 !='NULL'">
       <![CDATA[ public Guid? ]]><xsl:value-of select="IDFK2"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK2"/><![CDATA[ { get; set; }    ]]>
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 
 <xsl:if test="TableFK3 !='NULL'">
       <![CDATA[ public Guid? ]]><xsl:value-of select="IDFK3"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameFK3"/><![CDATA[ { get; set; }    ]]>
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelFK3"/><![CDATA[> ]]><xsl:value-of select="TableFK3"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if>   
</xsl:when>
<xsl:otherwise>
 <xsl:if test="TableFK1 !='NULL'">
         <![CDATA[ public int? ]]><xsl:value-of select="IDFK1"/><![CDATA[ { get; set; }
          public string ]]><xsl:value-of select="NameFK1"/><![CDATA[ { get; set; }   ]]>
     <xsl:if test="TableFK1 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[SearchResults { get; set; }  ]]>
 </xsl:if> 
 <xsl:if test="TableFK2 !='NULL'">
         <![CDATA[ public int? ]]><xsl:value-of select="IDFK2"/><![CDATA[ { get; set; }
          public string ]]><xsl:value-of select="NameFK2"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK2 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 
 <xsl:if test="TableFK3 !='NULL'">
         <![CDATA[ public int? ]]><xsl:value-of select="IDFK3"/><![CDATA[ { get; set; }
          public string ]]><xsl:value-of select="NameFK3"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableFK3 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelFK3"/><![CDATA[> ]]><xsl:value-of select="TableFK3"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if>   
</xsl:otherwise>    
</xsl:choose>        

 <xsl:if test="TableBK1 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDBK1"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameBK1"/><![CDATA[ { get; set; }   ]]>
     <xsl:if test="TableBK1 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="TableBK1"/><![CDATA[SearchResults { get; set; }  ]]>
 </xsl:if> 
 <xsl:if test="TableBK2 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDBK2"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameBK2"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableBK2 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelBK2"/><![CDATA[> ]]><xsl:value-of select="TableBK2"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 
 <xsl:if test="TableBK3 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDBK3"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameBK3"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableBK3 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelBK3"/><![CDATA[> ]]><xsl:value-of select="TableBK3"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 
 <xsl:if test="TableBK4 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDBK4"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameBK4"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableBK4 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelBK4"/><![CDATA[> ]]><xsl:value-of select="TableBK4"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 
 <xsl:if test="TableBK5 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDBK5"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameBK5"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableBK5 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelBK5"/><![CDATA[> ]]><xsl:value-of select="TableBK5"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 
 <xsl:if test="TableBK6 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDBK6"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameBK6"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableBK6 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelBK6"/><![CDATA[> ]]><xsl:value-of select="TableBK6"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 
 <xsl:if test="TableBK7 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDBK7"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameBK7"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableBK7 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelBK7"/><![CDATA[> ]]><xsl:value-of select="TableBK7"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 
 <xsl:if test="TableBK8 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDBK8"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameBK8"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableBK8 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelBK8"/><![CDATA[> ]]><xsl:value-of select="TableBK8"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 
 <xsl:if test="TableBK9 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDBK9"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameBK9"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableBK9 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelBK9"/><![CDATA[> ]]><xsl:value-of select="TableBK9"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 
 <xsl:if test="TableBK10 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDBK10"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameBK10"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableBK10 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelBK10"/><![CDATA[> ]]><xsl:value-of select="TableBK10"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 
 <xsl:if test="TableBK11 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDBK11"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameBK11"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableBK11 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelBK11"/><![CDATA[> ]]><xsl:value-of select="TableBK11"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 
 <xsl:if test="TableBK12 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDBK12"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameBK12"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableBK12 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelBK12"/><![CDATA[> ]]><xsl:value-of select="TableBK12"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 
 <xsl:if test="TableBK13 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDBK13"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameBK13"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableBK13 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelBK13"/><![CDATA[> ]]><xsl:value-of select="TableBK13"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 
 <xsl:if test="TableBK14 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDBK14"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameBK14"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableBK14 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelBK14"/><![CDATA[> ]]><xsl:value-of select="TableBK14"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 
 <xsl:if test="TableBK15 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDBK15"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameBK15"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableBK15 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelBK15"/><![CDATA[> ]]><xsl:value-of select="TableBK15"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 
 <xsl:if test="TableBK16 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDBK16"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameBK16"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableBK16 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelBK16"/><![CDATA[> ]]><xsl:value-of select="TableBK16"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 
 <xsl:if test="TableBK17 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDBK17"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameBK17"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableBK17 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelBK17"/><![CDATA[> ]]><xsl:value-of select="TableBK17"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 
 <xsl:if test="TableBK18 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDBK18"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameBK18"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableBK18 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelBK18"/><![CDATA[> ]]><xsl:value-of select="TableBK18"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 
 <xsl:if test="TableBK19 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDBK19"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameBK19"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableBK19 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelBK19"/><![CDATA[> ]]><xsl:value-of select="TableBK19"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 
 <xsl:if test="TableBK20 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDBK20"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameBK20"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableBK20 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelBK20"/><![CDATA[> ]]><xsl:value-of select="TableBK20"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 
 <xsl:if test="TableBK21 !='NULL'">
       <![CDATA[ public int? ]]><xsl:value-of select="IDBK21"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="NameBK21"/><![CDATA[ { get; set; }    ]]>
     <xsl:if test="TableBK21 ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
      </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModelBK21"/><![CDATA[> ]]><xsl:value-of select="TableBK21"/><![CDATA[SearchResults { get; set; }    ]]>                         
 </xsl:if> 

<xsl:choose>
<xsl:when test="Table ='++Guid+Filter Prperties+++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='AspnetApplications'">  
        <![CDATA[ public Guid? ]]><xsl:value-of select="ID"/><![CDATA[ { get; set; }
         public string ]]><xsl:value-of select="Name"/><![CDATA[ { get; set; }   ]]>
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[SearchResults { get; set; }  ]]>    
</xsl:when>
<xsl:when test="Table ='AspnetMemberships'">  
        <![CDATA[ public Guid? ]]><xsl:value-of select="ID"/><![CDATA[ { get; set; }
         public string ]]><xsl:value-of select="Name"/><![CDATA[ { get; set; }   ]]>
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[SearchResults { get; set; }  ]]>    
</xsl:when>
<xsl:when test="Table ='AspnetUsers'">  
        <![CDATA[ public Guid? ]]><xsl:value-of select="ID"/><![CDATA[ { get; set; }
         public string ]]><xsl:value-of select="Name"/><![CDATA[ { get; set; }   ]]>
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[SearchResults { get; set; }  ]]>    
</xsl:when>
<xsl:otherwise>
      <![CDATA[  
        public int? ]]><xsl:value-of select="ID"/><![CDATA[ { get; set; }
        public string ]]><xsl:value-of select="Name"/><![CDATA[ { get; set; }  ]]>
       <xsl:if test="Table ='Tbl03Regnums'">
       <![CDATA[ public string Subregnum { get; set; }  ]]>
        </xsl:if>                               
        <![CDATA[ public IList<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[SearchResults { get; set; }  ]]>
</xsl:otherwise>    
</xsl:choose> 
 <![CDATA[   }
}]]>   

</xsl:template>
</xsl:stylesheet>




