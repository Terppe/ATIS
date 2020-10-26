<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[using System.Linq;
using ]]><xsl:value-of select="Namespace"/>.Domain.Models; <![CDATA[ 

// <!-- Interface Skriptdatum: ]]> <xsl:value-of select="DateTime"/>  <![CDATA[  -->  

namespace ]]><xsl:value-of select="Namespace"/>.Domain.Interfaces <![CDATA[ 
{         
    public interface I]]><xsl:value-of select="Table"/><![CDATA[Repository
    {
        IQueryable<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[ { get; }                   
        IQueryable<]]><xsl:value-of select="LinqModel"/><![CDATA[> FindAll();
        IQueryable<]]><xsl:value-of select="LinqModel"/><![CDATA[> FindAllSort();          

        ]]><xsl:value-of select="LinqModel"/><![CDATA[ Get(int id);

        void Add(]]><xsl:value-of select="EntityAbl"/><![CDATA[);
        void Delete(]]><xsl:value-of select="EntityAbl"/><![CDATA[);
        void Save( );             ]]>  

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
</xsl:if>   <![CDATA[  }
}]]>   

</xsl:template>
</xsl:stylesheet>











