<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Atis.Domain.Entities.]]><xsl:value-of select="LinqModel"/><![CDATA[>" %>

    <!-- Summary  Skriptdatum: ]]> <xsl:value-of select="DateTime"/>  <![CDATA[  -->

        <tr > 
            <td><%: Html.ActionLink(Model.]]><xsl:value-of select="Name"/><![CDATA[, "Edit",  new { Model.]]><xsl:value-of select="ID"/><![CDATA[ }) %></td>
            <td><%: Model.Subregnum %></td>
            <td><%: Html.CheckBoxFor(m => m.Valid) %></td>
            <td><%: Model.ValidYear %></td>
            <td><%: Model.Synonym %></td>
            <td><%: Model.Author %></td>
            <td><%: Model.AuthorYear %></td>  ]]> 
<xsl:choose>
<xsl:when test="Table ='Tbl03Regnums'">        
</xsl:when>
<xsl:otherwise>   <![CDATA[ 
             <td>
                <% using (Html.BeginForm("Details", "]]><xsl:value-of select="TableFK1"/><![CDATA[")) { %>
                    <%: Html.Hidden("]]><xsl:value-of select="IDFK1"/><![CDATA[", Model.]]><xsl:value-of select="IDFK1"/><![CDATA[) %>
                    <input type="submit" value="Details" />
                <% } %>
            </td>     ]]> 
</xsl:otherwise>    
</xsl:choose>    <![CDATA[ 
            <td>
                <% using (Html.BeginForm("Details", "]]><xsl:value-of select="Table"/><![CDATA[")) { %>
                    <%: Html.Hidden("]]><xsl:value-of select="ID"/><![CDATA[", Model.]]><xsl:value-of select="ID"/><![CDATA[) %>
                    <input type="submit" value="Details" />
                <% } %>
            </td>
            <td>
                <% using (Html.BeginForm("List", "]]><xsl:value-of select="TableTK1"/><![CDATA[")) { %>
                    <input type="submit" value="]]><xsl:value-of select="BasisTK1"/><![CDATA[s" />
                <% } %>
            </td>
            <td><%: Html.ActionLink(]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.ActionLnkDelete, "Delete", new { Model.]]><xsl:value-of select="ID"/><![CDATA[ })%> </td>
        </tr>
   ]]> 
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












