<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[@model Atis.Domain.ViewModels.]]><xsl:value-of select="Table"/><![CDATA[.ReportViewModel

       <!-- SearchIDResults Skriptdatum: ]]> <xsl:value-of select="DateTime"/>  <![CDATA[  -->

@{
    ViewBag.Title = "]]><xsl:value-of select="Table"/><![CDATA[SearchIDResults";
    Layout = "~/Views/Shared/_Layout.cshtml";
}  

<h3>@SharedRes.StringsRes.Report</h3>    ]]>

<xsl:choose>
<xsl:when test="Table ='Author++++++++++++++++++'">        
</xsl:when>
<xsl:otherwise>  
<![CDATA[@foreach (var result in Model.]]><xsl:value-of select="Table"/><![CDATA[SearchResults)  {  
    if ((result.Author != null) && (result.AuthorYear != null))    { 
        <h1>@result.]]><xsl:value-of select="Name"/><![CDATA[ @result.Author, @result.AuthorYear  </h1>  }  
    if ((result.Author != null) && (result.AuthorYear == null))    { 
        <h1>@result.]]><xsl:value-of select="Name"/><![CDATA[ @result.Author  </h1>  }
    if (result.Author == null)
        {   <h1>@result.]]><xsl:value-of select="Name"/><![CDATA[  </h1>  }
    <p>
        @SharedRes.StringsRes.ReportTaxoID 
        @result.CountID 
    </p>  ]]>    
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='ReportTaxoNomen++++++++++++++++++'">        
</xsl:when>
<xsl:otherwise>  
<![CDATA[    <h2>@SharedRes.StringsRes.ReportTaxoNomen</h2>   ]]>   
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Tbl03Regnums Name++++++++++++++++++'">        
</xsl:when>
<xsl:otherwise>  
<![CDATA[    if (Model.]]><xsl:value-of select="TableBK20"/><![CDATA[SearchResults != null)   {
        foreach (var result1 in Model.]]><xsl:value-of select="TableBK20"/><![CDATA[SearchResults.Where(result1 => result1.]]><xsl:value-of select="NameBK20"/><![CDATA[.Contains("#") == false))  {
            <table style="width: 95%" >
                <tr style="text-align: left;"><td style="width: 20%">@SharedRes.StringsRes.Regnum </td><td>@result1.]]><xsl:value-of select="NameBK20"/><![CDATA[</td></tr>   
            </table>
        }
    }   ]]> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='TaxoRank+Synonyms+CommonNames++++++++++++++++++'">        
</xsl:when>
<xsl:otherwise>  
<![CDATA[    <table style="width:95%"> 
        <tr style="text-align:left;"><td style="width:20%">@SharedRes.StringsRes.ReportTaxoRank </td><td>@SharedRes.StringsRes.Genus </td></tr>
        <tr style="text-align:left;"><td style="width:20%">@SharedRes.StringsRes.Synonyms </td><td>@result.Synonym </td></tr>
        <tr style="text-align:left;"><td style="width:20%">@SharedRes.StringsRes.CommonNames </td>
        @if (result.EngName != null) {            
            <td>@result.EngName [@SharedRes.StringsRes.EngName]</td> } </tr>
        @if (result.GerName != null)  {            
            <tr style="text-align:left;"><td style="width:20%"> </td><td>@result.GerName [@SharedRes.StringsRes.GerName]</td></tr> }
        @if (result.FraName != null) {                                                                                                                              
            <tr style="text-align:left;"><td style="width:20%"> </td><td>@result.FraName [@SharedRes.StringsRes.FraName]</td></tr> }
        @if (result.PorName != null) {                                                                                                                               
            <tr style="text-align:left;"><td style="width:20%"> </td><td>@result.PorName [@SharedRes.StringsRes.PorName]</td></tr> }                                                        
    </table>   ]]> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='TaxoStatus++++++++++++++++++'">        
</xsl:when>
<xsl:otherwise>  
<![CDATA[    <p><strong>@SharedRes.StringsRes.ReportTaxoStatus </strong></p>  
    <table style="width:95%">
        <tr style="text-align:left;"><td style="width:20%">@SharedRes.StringsRes.ReportCurrentStanding </td>
            <td>
                @if (result.Valid == true)    { 
	    <img src="@Url.Content("~/Content/Images/Ok.png")" alt="@SharedRes.StringsRes.ValidYes" title="@SharedRes.StringsRes.ValidYes" />  }
	else   { 
	    <img src="@Url.Content("~/Content/Images/cancel.png")" alt="@SharedRes.StringsRes.ValidNo" title="@SharedRes.StringsRes.ValidNo" />  } 
            </td> 
        </tr>
    </table>      ]]> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='ReportInfo++++++++++++++++++'">        
</xsl:when>
<xsl:otherwise>  
<![CDATA[    <p><strong>@SharedRes.StringsRes.ReportInfo </strong></p>  
    <table style="width:95%">			
        @if (result.Info != null) { 
            <tr style="text-align:left;"><td style="width:20%">@SharedRes.StringsRes.ReportInfo </td><td>@result.Info</td></tr> 
            <tr style="text-align:left;"><td style="width:20%">@SharedRes.StringsRes.ReportMemo </td><td>@result.Memo</td></tr> }		
    </table>   ]]> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='ReportTaxoHiers++++++++++++++++++'">        
</xsl:when>
<xsl:otherwise>  
<![CDATA[    <h2>@SharedRes.StringsRes.ReportTaxoHiera</h2>   ]]>   
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='ReportHira Tbl03Regnums++++++++++++++++++'">        
</xsl:when>
<xsl:otherwise>  
<![CDATA[    if (Model.]]><xsl:value-of select="TableBK20"/><![CDATA[SearchResults != null)  {
        foreach (var result1 in Model.]]><xsl:value-of select="TableBK20"/><![CDATA[SearchResults.Where(result1 => result1.]]><xsl:value-of select="NameBK20"/><![CDATA[.Contains("#") == false))  {
            <table style="width:95%" >
	<tr style="text-align:left;">
	    <td style="width:40%; padding-left:0px; " >@SharedRes.StringsRes.]]><xsl:value-of select="BasisBK20"/><![CDATA[ </td>
	    <td><b>@Html.ActionLink(result1.]]><xsl:value-of select="NameBK20"/><![CDATA[ + " " + result1.Subregnum, "]]><xsl:value-of select="TableBK20"/><![CDATA[SearchIDResults", new { id = result1.]]><xsl:value-of select="IDBK20"/><![CDATA[ })</b>       
	        @Html.Partial("_ReportInfosPartial", result1)
	    </td>
	</tr>
            </table>
        }
    }    ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='ReportHira Tbl06Phylums++++++++++++++++++'">        
</xsl:when>
<xsl:otherwise>  
<![CDATA[    if (Model.]]><xsl:value-of select="TableBK19"/><![CDATA[SearchResults != null)  {
        foreach (var result1 in Model.]]><xsl:value-of select="TableBK19"/><![CDATA[SearchResults.Where(result1 => result1.]]><xsl:value-of select="NameBK19"/><![CDATA[.Contains("#") == false))  {
            <table style="width:95%" >
	<tr style="text-align:left;">
	    <td style="width:40%; padding-left:10px; " >@SharedRes.StringsRes.]]><xsl:value-of select="BasisBK19"/><![CDATA[ </td>
	    <td><b>@Html.ActionLink(result1.]]><xsl:value-of select="NameBK19"/><![CDATA[, "]]><xsl:value-of select="TableBK19"/><![CDATA[SearchIDResults", new { id = result1.]]><xsl:value-of select="IDBK19"/><![CDATA[ })</b>       
	        @Html.Partial("_ReportInfosPartial", result1)
	    </td>
	</tr>
            </table>
        }
    }    ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='ReportHira Tbl09Divisions++++++++++++++++++'">        
</xsl:when>
<xsl:otherwise>  
<![CDATA[    if (Model.]]><xsl:value-of select="TableBK18"/><![CDATA[SearchResults != null)  {
        foreach (var result1 in Model.]]><xsl:value-of select="TableBK18"/><![CDATA[SearchResults.Where(result1 => result1.]]><xsl:value-of select="NameBK18"/><![CDATA[.Contains("#") == false))  {
            <table style="width:95%" >
	<tr style="text-align:left;">
	    <td style="width:40%; padding-left:10px; " >@SharedRes.StringsRes.]]><xsl:value-of select="BasisBK18"/><![CDATA[ </td>
	    <td><b>@Html.ActionLink(result1.]]><xsl:value-of select="NameBK18"/><![CDATA[, "]]><xsl:value-of select="TableBK18"/><![CDATA[SearchIDResults", new { id = result1.]]><xsl:value-of select="IDBK18"/><![CDATA[ })</b>       
	        @Html.Partial("_ReportInfosPartial", result1)
	    </td>
	</tr>
            </table>
        }
    }    ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='ReportHira Tbl12Subphylums++++++++++++++++++'">        
</xsl:when>
<xsl:otherwise>  
<![CDATA[    if (Model.]]><xsl:value-of select="TableBK17"/><![CDATA[SearchResults != null)  {
        foreach (var result1 in Model.]]><xsl:value-of select="TableBK17"/><![CDATA[SearchResults.Where(result1 => result1.]]><xsl:value-of select="NameBK17"/><![CDATA[.Contains("#") == false))  {
            <table style="width:95%" >
	<tr style="text-align:left;">
	    <td style="width:40%; padding-left:20px; " >@SharedRes.StringsRes.]]><xsl:value-of select="BasisBK17"/><![CDATA[ </td>
	    <td><b>@Html.ActionLink(result1.]]><xsl:value-of select="NameBK17"/><![CDATA[, "]]><xsl:value-of select="TableBK17"/><![CDATA[SearchIDResults", new { id = result1.]]><xsl:value-of select="IDBK17"/><![CDATA[ })</b>       
	        @Html.Partial("_ReportInfosPartial", result1)
	    </td>
	</tr>
            </table>
        }
    }    ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='ReportHira Tbl15Subdivisions++++++++++++++++++'">        
</xsl:when>
<xsl:otherwise>  
<![CDATA[    if (Model.]]><xsl:value-of select="TableBK16"/><![CDATA[SearchResults != null)  {
        foreach (var result1 in Model.]]><xsl:value-of select="TableBK16"/><![CDATA[SearchResults.Where(result1 => result1.]]><xsl:value-of select="NameBK16"/><![CDATA[.Contains("#") == false))  {
                <table style="width:95%" >
        	     <tr style="text-align:left;">
	               <td style="width:40%; padding-left:20px; " >@SharedRes.StringsRes.]]><xsl:value-of select="BasisBK16"/><![CDATA[ </td>
	               <td><b>@Html.ActionLink(result1.]]><xsl:value-of select="NameBK16"/><![CDATA[, "]]><xsl:value-of select="TableBK16"/><![CDATA[SearchIDResults", new { id = result1.]]><xsl:value-of select="IDBK16"/><![CDATA[ })</b>       
	                    @Html.Partial("_ReportInfosPartial", result1)
	               </td>
	            </tr>
                  </table>
        }
    }    ]]>
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

















