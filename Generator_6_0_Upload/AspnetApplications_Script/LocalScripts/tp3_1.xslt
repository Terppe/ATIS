<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions" >
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[using System.Linq;
using ]]><xsl:value-of select="Namespace"/>.Domain.Models; <![CDATA[     ]]>
<xsl:if test="Table='AspnetApplications'">  
<![CDATA[using System;      ]]>
</xsl:if>   
<xsl:if test="Table='AspnetMemberships'">  
<![CDATA[using System;      ]]>
</xsl:if>   
<xsl:if test="Table='AspnetUsers'">  
<![CDATA[using System;      ]]>
</xsl:if>   
<![CDATA[// <!-- Interface Skriptdatum: ]]> <xsl:value-of select="DateTime"/>   <![CDATA[  -->  

namespace ]]><xsl:value-of select="Namespace"/>.Domain.Interfaces <![CDATA[    {         
    public interface I]]><xsl:value-of select="Table"/><![CDATA[Repository    {
        IQueryable<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[ { get; }                   
        IQueryable<]]><xsl:value-of select="LinqModel"/><![CDATA[> FindAll();
        IQueryable<]]><xsl:value-of select="LinqModel"/><![CDATA[> FindAllSort();          
   ]]> 
<xsl:choose>
<xsl:when test="Table ='GUID++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='AspnetApplications'">      
<![CDATA[        ]]><xsl:value-of select="LinqModel"/><![CDATA[ Get(Guid id);    ]]>
</xsl:when>
<xsl:when test="Table ='AspnetMemberships'">      
<![CDATA[        ]]><xsl:value-of select="LinqModel"/><![CDATA[ Get(Guid id);    ]]>
</xsl:when>
<xsl:when test="Table ='AspnetUsers'">      
<![CDATA[        ]]><xsl:value-of select="LinqModel"/><![CDATA[ Get(Guid id);    ]]>
</xsl:when>
<xsl:otherwise>  
<![CDATA[        ]]><xsl:value-of select="LinqModel"/><![CDATA[ Get(int id);   ]]>
</xsl:otherwise>    
</xsl:choose>        
<![CDATA[
        void Add(]]><xsl:value-of select="EntityAbl"/><![CDATA[);
        void Delete(]]><xsl:value-of select="EntityAbl"/><![CDATA[);
        void Save( );             ]]>  
<xsl:choose>
<xsl:when test="Table ='TblCounters'"> 
        int Counter();       
</xsl:when>
<xsl:otherwise>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Tbl03Regnums'"> 
</xsl:when>
<xsl:otherwise>  
</xsl:otherwise>    
</xsl:choose>
                                                                                                                                                                 
<xsl:if test="Table='Tbl03Regnums'">  
</xsl:if>   
<xsl:if test="Table='Tbl06Phylums'">   
</xsl:if>   
<xsl:if test="Table='Tbl09Divisions'">
</xsl:if>   
<xsl:if test="Table='Tbl12Subphylums'">
</xsl:if>   
<xsl:if test="Table='Tbl15Subdivisions'">
</xsl:if>   
<xsl:if test="Table='Tbl18Superclasses'">
</xsl:if>   
<xsl:if test="Table='Tbl21Classes'">
</xsl:if>   
<xsl:if test="Table='Tbl24Subclasses'">
</xsl:if>   
<xsl:if test="Table='Tbl27Infraclasses'">
</xsl:if>   
<xsl:if test="Table='Tbl30Legios'">
</xsl:if> 
<xsl:if test="Table='Tbl33Ordos'">
</xsl:if> 
<xsl:if test="Table='Tbl36Subordos'">
</xsl:if> 
<xsl:if test="Table='Tbl39Infraordos'">
</xsl:if>  
<xsl:if test="Table='Tbl42Superfamilies'">
</xsl:if> 
<xsl:if test="Table='Tbl45Families'">
</xsl:if> 
<xsl:if test="Table='Tbl48Subfamilies'">
</xsl:if> 
<xsl:if test="Table='Tbl51Infrafamilies'">
</xsl:if> 
<xsl:if test="Table='Tbl54Supertribusses'">
</xsl:if> 
<xsl:if test="Table='Tbl57Tribusses'">
</xsl:if> 
<xsl:if test="Table='Tbl60Subtribusses'">
</xsl:if> 
<xsl:if test="Table='Tbl63Infratribusses'">
</xsl:if> 
<xsl:if test="Table='Tbl66Genusses'">
</xsl:if> 
<xsl:if test="Table='Tbl69FiSpeciesses'"> 
</xsl:if>   <![CDATA[  }
}]]>   

</xsl:template>
</xsl:stylesheet>









