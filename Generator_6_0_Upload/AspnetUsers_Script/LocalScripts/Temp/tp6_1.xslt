<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[@model Atis.Domain.Models.]]><xsl:value-of select="LinqModel"/><![CDATA[     ]]>

<xsl:choose>
<xsl:when test="Table ='JQuery+++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">  
<![CDATA[
<script type="text/javascript"> 
$(function()   {
    $('#]]><xsl:value-of select="Name"/><![CDATA[').focus();
});
</script> 
]]>      
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">  
<![CDATA[
<script type="text/javascript"> 
$(function()   {
    $('#]]><xsl:value-of select="Name"/><![CDATA[').focus();
});
</script> 
]]>      
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
</xsl:when>
<xsl:when test="Table ='Tbl90References'">  
<![CDATA[
<script type="text/javascript"> 
$(function()   {
    $('#]]><xsl:value-of select="IDFK4"/><![CDATA[').focus();
});
</script> 
]]>      
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">  
<![CDATA[
<script type="text/javascript"> 
$(function()   {
    $('#]]><xsl:value-of select="Name"/><![CDATA[').focus();
});
</script> 
]]>      
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">  
<![CDATA[
<script type="text/javascript"> 
$(function()   {
    $('#]]><xsl:value-of select="Name"/><![CDATA[').focus();
});
</script> 
]]>      
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">  
<![CDATA[
<script type="text/javascript"> 
$(function()   {
    $('#]]><xsl:value-of select="IDFK4"/><![CDATA[').focus();
});
</script> 
]]>      
</xsl:when>

<xsl:otherwise> 
<![CDATA[
<script type="text/javascript">
$(function()   {
    $('#]]><xsl:value-of select="ID"/><![CDATA[').focus();    
});
</script>
]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Header+++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">
<![CDATA[@{
    WebImage photo = null;
    if(IsPost)
    {
        photo = WebImage.GetImageFromRequest("Image");
    }
}     ]]>        
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">    
<![CDATA[ @using System.Globalization;

<script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
<script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>      ]]>    
</xsl:when>
<xsl:otherwise>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Info+++++++++++++++'">        
</xsl:when>
<xsl:otherwise> 
       <![CDATA[   <!-- _Form.cshtml  Skriptdatum: ]]><xsl:value-of select="DateTime"/>  <![CDATA[  -->    ]]> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Action+++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">     
<![CDATA[    <form action=""  method="post" enctype="multipart/form-data">     ]]>   
</xsl:when>
<xsl:otherwise>  
</xsl:otherwise>    
</xsl:choose> 
 
<xsl:choose>
<xsl:when test="Table ='Pagination+++++++++++++++'">        
</xsl:when>
<xsl:otherwise>  
     <![CDATA[   @using (Html.BeginForm()) {
        <div class="pagination">
            <p><input type="submit" value="@SharedRes.StringsRes.ButtonSave" />  
                @Html.ActionLink(SharedRes.StringsRes.ActionLnkBack, "Index", null, new { style = "font-weight:bold" })  
                @Html.ActionLink(SharedRes.StringsRes.ActionLnkClear, "Create", null, null)  
            </p> 
        </div>

        @Html.ValidationSummary() 
            <fieldset><legend>@SharedRes.StringsRes.Fields </legend>  ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Map+++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">    
          <![CDATA[    <div id="mapDiv">    
                    @Html.Partial("_Map")
              </div> ]]>    
</xsl:when>
<xsl:otherwise>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Style+++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl90References'">    
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">    
</xsl:when>
<xsl:otherwise>  
           <![CDATA[   <table><colgroup style="width:12em" span="2"></colgroup>   ]]>                                          
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Images+++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
        <![CDATA[             <tr>
                        <td><label for="Image">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.UploadImage</label> </td>
                        <td><input type="file" id="fileUpload" name="Image" size="72"/> </td>
                     </tr>      ]]>                
</xsl:when>
<xsl:otherwise> 
</xsl:otherwise>    
</xsl:choose>
                                     
<xsl:choose>
<xsl:when test="Table ='FK1 Viewdata+++++++++++++'">        
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
<xsl:when test="Table ='Tbl90References'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">    
</xsl:when>
<xsl:otherwise> 
    <xsl:if test="TableFK1 !='NULL'">
        <![CDATA[             <tr>
                         <td>@]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="IDFK1"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK1"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })          
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK1"/><![CDATA[) </td>
                     </tr>      ]]>                
    </xsl:if> 
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='FK2 Viewdata+++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl78Names'"> 
    <xsl:if test="TableFK1 !='NULL'">
        <![CDATA[             <tr>
                         <td>@]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="IDFK1"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK1"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] as SelectList, new { @class = "ddl15" })          
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK1"/><![CDATA[) </td>
                     </tr>      ]]>                
    </xsl:if>        
    <xsl:if test="TableFK2 !='NULL'">
        <![CDATA[             <tr>
                         <td>@]]><xsl:value-of select="TableFK2"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="IDFK2"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK2"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] as SelectList, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK2"/><![CDATA[) </td>
                     </tr>      ]]>                
    </xsl:if>        
</xsl:when>
<xsl:when test="Table ='Tbl81Images'"> 
    <xsl:if test="TableFK1 !='NULL'">
        <![CDATA[             <tr>
                         <td>@]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="IDFK1"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK1"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] as SelectList, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK1"/><![CDATA[) </td>
                     </tr>      ]]>                
    </xsl:if>         
    <xsl:if test="TableFK2 !='NULL'">
        <![CDATA[             <tr>
                         <td>@]]><xsl:value-of select="TableFK2"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="IDFK2"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK2"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] as SelectList, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK2"/><![CDATA[) </td>
                     </tr>      ]]>                
    </xsl:if>       
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'"> 
    <xsl:if test="TableFK1 !='NULL'">
        <![CDATA[             <tr>
                         <td>@]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="IDFK1"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK1"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] as SelectList, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK1"/><![CDATA[) </td>
                     </tr>      ]]>                
    </xsl:if>        
    <xsl:if test="TableFK2 !='NULL'">
        <![CDATA[             <tr>
                         <td>@]]><xsl:value-of select="TableFK2"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="IDFK2"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK2"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] as SelectList, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK2"/><![CDATA[) </td>
                     </tr>      ]]>                
    </xsl:if>        
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'"> 
    <xsl:if test="TableFK1 !='NULL'">
        <![CDATA[             <tr>
                         <td>@]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="IDFK1"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK1"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] as SelectList, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK1"/><![CDATA[) </td>
                     </tr>      ]]>                
    </xsl:if>        
    <xsl:if test="TableFK2 !='NULL'">
        <![CDATA[             <tr>
                         <td>@]]><xsl:value-of select="TableFK2"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="IDFK2"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK2"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] as SelectList, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK2"/><![CDATA[) </td>
                     </tr>      ]]>                
    </xsl:if>        
</xsl:when>
<xsl:when test="Table ='Tbl90References'"> 
    <xsl:if test="TableFK4 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK4"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK4"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK4"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK4"/><![CDATA[) </td>
                           ]]>                
    </xsl:if>        
    <xsl:if test="TableFK5 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK5"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK5"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK5"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK5"/><![CDATA[) </td>
                          ]]>  
    </xsl:if>        
    <xsl:if test="TableFK6 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK6"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK6"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK6"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK6"/><![CDATA[) </td>
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK7 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK7"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK7"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK7"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK7"/><![CDATA[) </td>
                           ]]>                
    </xsl:if>        
    <xsl:if test="TableFK8 !='NULL'">
        <![CDATA[             
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK8"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK8"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK8"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK8"/><![CDATA[) </td>
                          ]]>  
    </xsl:if>        
    <xsl:if test="TableFK9 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK9"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK9"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK9"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK9"/><![CDATA[) </td>
                         ]]>     
    </xsl:if>        
    <xsl:if test="TableFK10 !='NULL'">
        <![CDATA[             
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK10"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK10"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK10"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK10"/><![CDATA[) </td>
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK11 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK11"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK11"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK11"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK11"/><![CDATA[) </td>
                           ]]>                
    </xsl:if>        
    <xsl:if test="TableFK12 !='NULL'">
        <![CDATA[             
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK12"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK12"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK12"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK12"/><![CDATA[) </td>
                          ]]>  
    </xsl:if>        
    <xsl:if test="TableFK13 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK13"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK13"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK13"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK13"/><![CDATA[) </td>
                         ]]>     
    </xsl:if>        
    <xsl:if test="TableFK14 !='NULL'">
        <![CDATA[             
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK14"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK14"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK14"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK14"/><![CDATA[) </td>
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK15 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK15"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK15"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK15"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK15"/><![CDATA[) </td>
                           ]]>                
    </xsl:if>        
    <xsl:if test="TableFK16 !='NULL'">
        <![CDATA[             
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK16"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK16"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK16"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK16"/><![CDATA[) </td>
                          ]]>  
    </xsl:if>        
    <xsl:if test="TableFK17 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK17"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK17"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK17"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK17"/><![CDATA[) </td>
                         ]]>     
    </xsl:if>        
    <xsl:if test="TableFK18 !='NULL'">
        <![CDATA[             
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK18"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK18"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK18"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK18"/><![CDATA[) </td>
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK19 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK19"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK19"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK19"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK19"/><![CDATA[) </td>
                           ]]>                
    </xsl:if>        
    <xsl:if test="TableFK20 !='NULL'">
        <![CDATA[             
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK20"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK20"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK20"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK20"/><![CDATA[) </td>
                          ]]>  
    </xsl:if>        
    <xsl:if test="TableFK21 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK21"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK21"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK21"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK21"/><![CDATA[) </td>
                         ]]>     
    </xsl:if>        
    <xsl:if test="TableFK22 !='NULL'">
        <![CDATA[             
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK22"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK22"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK22"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK22"/><![CDATA[) </td>
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK23 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:25em">@Html.Hidden("Dummy", new { style = "width: 15em;" })</td>
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK23"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK23"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK23"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK23"/><![CDATA[) </td>
                           ]]>                
    </xsl:if>        
    <xsl:if test="TableFK25 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK25"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK25"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK25"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK25"/><![CDATA[) </td>
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK24 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:25em">@Html.Hidden("Dummy", new { style = "width: 15em;" })</td>
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK24"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK24"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK24"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK24"/><![CDATA[) </td>
                           ]]>                
    </xsl:if>        
    <xsl:if test="TableFK26 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK26"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK26"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK26"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK26"/><![CDATA[) </td>
                     </tr>    ]]>   
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK27 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:50.35em">@Html.Hidden("Dummy", new { style = "width: 15em;" })</td>           
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK27"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK27"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK27"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK27"/><![CDATA[) </td>
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>      
           <![CDATA[   <table><colgroup style="width:8em" span="2"></colgroup>   ]]>                                                                                            
    </xsl:if>        
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
    <xsl:if test="TableFK4 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK4"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK4"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK4"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK4"/><![CDATA[) </td>
                           ]]>                
    </xsl:if>        
    <xsl:if test="TableFK5 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK5"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK5"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK5"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK5"/><![CDATA[) </td>
                          ]]>  
    </xsl:if>        
    <xsl:if test="TableFK6 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK6"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK6"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK6"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK6"/><![CDATA[) </td>
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK7 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK7"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK7"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK7"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK7"/><![CDATA[) </td>
                           ]]>                
    </xsl:if>        
    <xsl:if test="TableFK8 !='NULL'">
        <![CDATA[             
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK8"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK8"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK8"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK8"/><![CDATA[) </td>
                          ]]>  
    </xsl:if>        
    <xsl:if test="TableFK9 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK9"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK9"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK9"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK9"/><![CDATA[) </td>
                         ]]>     
    </xsl:if>        
    <xsl:if test="TableFK10 !='NULL'">
        <![CDATA[             
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK10"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK10"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK10"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK10"/><![CDATA[) </td>
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK11 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK11"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK11"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK11"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK11"/><![CDATA[) </td>
                           ]]>                
    </xsl:if>        
    <xsl:if test="TableFK12 !='NULL'">
        <![CDATA[             
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK12"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK12"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK12"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK12"/><![CDATA[) </td>
                          ]]>  
    </xsl:if>        
    <xsl:if test="TableFK13 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK13"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK13"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK13"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK13"/><![CDATA[) </td>
                         ]]>     
    </xsl:if>        
    <xsl:if test="TableFK14 !='NULL'">
        <![CDATA[             
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK14"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK14"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK14"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK14"/><![CDATA[) </td>
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK15 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK15"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK15"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK15"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK15"/><![CDATA[) </td>
                           ]]>                
    </xsl:if>        
    <xsl:if test="TableFK16 !='NULL'">
        <![CDATA[             
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK16"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK16"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK16"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK16"/><![CDATA[) </td>
                          ]]>  
    </xsl:if>        
    <xsl:if test="TableFK17 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK17"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK17"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK17"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK17"/><![CDATA[) </td>
                         ]]>     
    </xsl:if>        
    <xsl:if test="TableFK18 !='NULL'">
        <![CDATA[             
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK18"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK18"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK18"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK18"/><![CDATA[) </td>
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK19 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK19"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK19"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK19"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK19"/><![CDATA[) </td>
                           ]]>                
    </xsl:if>        
    <xsl:if test="TableFK20 !='NULL'">
        <![CDATA[             
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK20"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK20"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK20"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK20"/><![CDATA[) </td>
                          ]]>  
    </xsl:if>        
    <xsl:if test="TableFK21 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK21"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK21"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK21"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK21"/><![CDATA[) </td>
                         ]]>     
    </xsl:if>        
    <xsl:if test="TableFK22 !='NULL'">
        <![CDATA[             
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK22"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK22"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK22"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK22"/><![CDATA[) </td>
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK23 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:26.15em">@Html.Hidden("Dummy", new { style = "width: 15em;" })</td>
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK23"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK23"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK23"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK23"/><![CDATA[) </td>
                           ]]>                
    </xsl:if>        
    <xsl:if test="TableFK25 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK25"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK25"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK25"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK25"/><![CDATA[) </td>
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK24 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:26.15em">@Html.Hidden("Dummy", new { style = "width: 15em;" })</td>
                         <td style="width:8em">@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK24"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK24"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK24"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK24"/><![CDATA[) </td>
                           ]]>                
    </xsl:if>        
    <xsl:if test="TableFK26 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK26"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK26"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK26"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK26"/><![CDATA[) </td>
                     </tr>    ]]>   
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK27 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:52.65em">@Html.Hidden("Dummy", new { style = "width: 15em;" })</td>           
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK27"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK27"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK27"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK27"/><![CDATA[) </td>
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>      
           <![CDATA[   <table><colgroup style="width:8em" span="2"></colgroup>   ]]>                                                                                            
    </xsl:if>        
</xsl:when>
<xsl:otherwise> 
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Info or Name+SubName+++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">  
         <![CDATA[            <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ </td>
                        <td>@Html.TextBoxFor(m => m.]]><xsl:value-of select="Name"/><![CDATA[, new { @class = "myclass", size = 72 }) 
                        @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="Name"/><![CDATA[) </td>
                    </tr>  ]]>  
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.LoweredApplicationName </td>
                        <td>@Html.TextBoxFor(m => m.LoweredApplicationName, new { @class = "myclass", size = 72 }) 
                        @Html.ValidationMessageFor(m => m.LoweredApplicationName) </td>
                    </tr>   ]]>
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Description </td>
                        <td>@Html.TextBoxFor(m => m.Description, new { @class = "myclass", size = 72 }) 
                        @Html.ValidationMessageFor(m => m.Description) </td>
                    </tr>   ]]>
</xsl:when>
<xsl:when test="Table ='aspnet_Membership'">  
         <![CDATA[            <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ </td>
                        <td>@Html.TextBoxFor(m => m.]]><xsl:value-of select="Name"/><![CDATA[, new { @class = "myclass", size = 72 }) 
                        @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="Name"/><![CDATA[) </td>
                    </tr>  ]]>  
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.LoweredApplicationName </td>
                        <td>@Html.TextBoxFor(m => m.LoweredApplicationName, new { @class = "myclass", size = 72 }) 
                        @Html.ValidationMessageFor(m => m.LoweredApplicationName) </td>
                    </tr>   ]]>
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Description </td>
                        <td>@Html.TextBoxFor(m => m.Description, new { @class = "myclass", size = 72 }) 
                        @Html.ValidationMessageFor(m => m.Description) </td>
                    </tr>   ]]>
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">  
         <![CDATA[            <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ </td>
                        <td>@Html.TextBoxFor(m => m.]]><xsl:value-of select="Name"/><![CDATA[, new { @class = "myclass", size = 72 }) 
                        @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="Name"/><![CDATA[) </td>
                    </tr>  ]]>  
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.LoweredUserName </td>
                        <td>@Html.TextBoxFor(m => m.LoweredUserName, new { @class = "myclass", size = 72 }) 
                        @Html.ValidationMessageFor(m => m.LoweredUserName) </td>
                    </tr>   ]]>
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MobileAlias </td>
                        <td>@Html.TextBoxFor(m => m.MobileAlias, new { @class = "myclass", size = 36 }) 
                        @Html.ValidationMessageFor(m => m.MobileAlias) </td>
                    </tr>   ]]>
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.IsAnonymous </td>
                        <td>@Html.EditorFor(m => m.IsAnonymous) 
                        @Html.ValidationMessageFor(m => m.IsAnonymous) </td>
                    </tr>   ]]>
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.LastActivityDate </td>
                        <td>@Html.TextBoxFor(m => m.LastActivityDate) 
                        @Html.ValidationMessageFor(m => m.LastActivityDate) </td>
                    </tr>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">  
         <![CDATA[            <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ </td>
                        <td>@Html.TextBoxFor(m => m.]]><xsl:value-of select="Name"/><![CDATA[, new { @class = "textbox36" }) 
                        @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="Name"/><![CDATA[) </td>
                    </tr>  ]]>  
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Subregnum </td>
                        <td>@Html.TextBoxFor(m => m.Subregnum, new { @class = "textbox36" }) 
                        @Html.ValidationMessageFor(m => m.Subregnum) </td>
                    </tr>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">   
         <![CDATA[            <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ </td>
                        <td>@Html.TextBoxFor(m => m.]]><xsl:value-of select="Name"/><![CDATA[, new { @class = "textbox36" }) 
                        @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="Name"/><![CDATA[) </td>
                    </tr>  ]]>
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Subspeciesgroup </td>
                        <td>@Html.TextBoxFor(m => m.Subspeciesgroup, new { @class = "textbox36" }) 
                        @Html.ValidationMessageFor(m => m.Subspeciesgroup)</td>
                    </tr>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">  
         <![CDATA[            <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ </td>
                        <td>@Html.TextBoxFor(m => m.]]><xsl:value-of select="Name"/><![CDATA[, new { @class = "textbox36" }) 
                        @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="Name"/><![CDATA[) </td>
                    </tr>  ]]>  
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Subspecies </td>
                        <td>@Html.TextBoxFor(m => m.Subspecies, new { @class = "textbox36" }) 
                        @Html.ValidationMessageFor(m => m.Subspecies) </td>
                    </tr>   ]]>
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Divers </td>
                        <td>@Html.TextBoxFor(m => m.Divers, new { @class = "textbox36" }) 
                        @Html.ValidationMessageFor(m => m.Divers)</td>
                    </tr>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">   
         <![CDATA[            <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ </td>
                        <td>@Html.TextBoxFor(m => m.]]><xsl:value-of select="Name"/><![CDATA[, new { @class = "textbox36" }) 
                        @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="Name"/><![CDATA[) </td>
                    </tr>  ]]>  
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Subspecies </td>
                        <td>@Html.TextBoxFor(m => m.Subspecies, new { @class = "textbox36" }) 
                        @Html.ValidationMessageFor(m => m.Subspecies) </td>
                    </tr>   ]]>
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Divers </td>
                        <td>@Html.TextBoxFor(m => m.Divers, new { @class = "textbox36" }) 
                        @Html.ValidationMessageFor(m => m.Divers) </td>
                    </tr>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">      
          <![CDATA[          <tr>
                        <td>@SharedRes.StringsRes.Info </td>
                        <td>@Html.TextBoxFor(m => m.Info, new { @class = "textbox72" }) 
                        @Html.ValidationMessageFor(m => m.Info) </td>
                    </tr>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">      
          <![CDATA[          <tr>
                        <td>@SharedRes.StringsRes.Info </td>
                        <td>@Html.TextBoxFor(m => m.Info, new { @class = "textbox72" }) 
                        @Html.ValidationMessageFor(m => m.Info) </td>
                    </tr>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">      
</xsl:when>
<xsl:when test="Table ='Tbl90References'">      
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">      
         <![CDATA[            <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ </td>
                        <td>@Html.TextBoxFor(m => m.]]><xsl:value-of select="Name"/><![CDATA[, new { @class = "textbox108" }) 
                        @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="Name"/><![CDATA[) </td>
                    </tr>  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">      
         <![CDATA[            <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ </td>
                        <td>@Html.TextBoxFor(m => m.]]><xsl:value-of select="Name"/><![CDATA[, new { @class = "textbox108" }) 
                        @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="Name"/><![CDATA[) 
                        @Html.TextBoxFor(m => m.SourceYear, new { @class = "textbox3" }) 
                        @Html.ValidationMessageFor(m => m.SourceYear) </td>
                    </tr>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">      
</xsl:when>
<xsl:otherwise> 
         <![CDATA[            <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ </td>
                        <td>@Html.TextBoxFor(m => m.]]><xsl:value-of select="Name"/><![CDATA[, new { @class = "textbox36" }) 
                        @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="Name"/><![CDATA[) </td>
                    </tr>  ]]>
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='FK2 Viewdata+++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl78Names'"> 
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'"> 
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90References'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">      
</xsl:when>
<xsl:otherwise> 
    <xsl:if test="TableFK2 !='NULL'">
        <![CDATA[             <tr>
                         <td>@]]><xsl:value-of select="TableFK2"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="IDFK2"/><![CDATA[ </td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK2"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK2"/><![CDATA[) </td>
                     </tr>      ]]>                
    </xsl:if> 
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Adresse+Language+++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">  
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Language </td>
                        <td>@Html.DropDownListFor(m => m.Language, ViewData["languages"] as SelectList, new { @class = "ddl" })
                        @Html.ValidationMessageFor(m => m.Language) </td>
                    </tr>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.ShotDate </td>
                        <td>@Html.TextBoxFor(m => m.ShotDate, new { @class = "textbox36" }) 
                        @Html.ValidationMessageFor(m => m.ShotDate) </td>
                    </tr>   ]]>
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.FilestreamID </td>
                        <td>@Html.TextBoxFor(m => m.FilestreamID, new { @class = "textbox72" })  
                        @Html.ValidationMessageFor(m => m.FilestreamID) </td>
                    </tr>   ]]>
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.ImageMimeType </td>
                        <td>@Html.DropDownListFor(m => m.ImageMimeType, ViewData["mimeTypes"] as SelectList, new { @class = "ddl" })          
                        @Html.ValidationMessageFor(m => m.ImageMimeType) </td>
                    </tr>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
           <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Country </td>
                        <td>@Html.DropDownListFor(m => m.Country, ViewData["Countries"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl15" })          
                        @Html.ValidationMessageFor(m => m.Country) </td>
                    </tr>   ]]>
           <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Address </td>            
                        <td>@Html.TextBoxFor(m => m.Address, new { @class = "textbox72" }) 
                        @Html.ValidationMessageFor(m => m.Address) </td>
                    </tr>   ]]>
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Latitude </td>
                        <td>@Html.TextBoxFor(m => m.Latitude, Convert.ToString(Model.Latitude, CultureInfo.InvariantCulture)) 
                        @Html.ValidationMessageFor(m => m.Latitude) </td>
                    </tr>   ]]>
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Longitude </td>
                        <td>@Html.TextBoxFor(m => m.Longitude,  Convert.ToString(Model.Longitude, CultureInfo.InvariantCulture)) 
                        @Html.ValidationMessageFor(m => m.Longitude) </td>
                    </tr>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90References'">  
          <![CDATA[          <tr>
                        <td>@SharedRes.StringsRes.Valid_Year </td>
                        <td>@Html.EditorFor(m => m.Valid) 
                        @Html.ValidationMessageFor(m => m.Valid)                                    
                        @Html.TextBoxFor(m => m.ValidYear, new { @class = "textbox3" }) 
                        @Html.ValidationMessageFor(m => m.ValidYear) </td>
                    </tr>   ]]>
    <xsl:if test="TableFK1 !='NULL'">
        <![CDATA[             <tr>
                         <td><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="BasisFK1"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK1"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK1"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl80" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK1"/><![CDATA[) </td>
                     </tr>      ]]>                
    </xsl:if> 
    <xsl:if test="TableFK2 !='NULL'">
        <![CDATA[             <tr>
                         <td><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="BasisFK2"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK2"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK2"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl80" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK2"/><![CDATA[) </td>
                     </tr>      ]]>                
    </xsl:if> 
    <xsl:if test="TableFK3 !='NULL'">
        <![CDATA[             <tr>
                         <td><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="BasisFK3"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK3"/><![CDATA[, ViewData["]]><xsl:value-of select="NameFK3"/><![CDATA[DDL"] as SelectList, SharedRes.StringsRes.DDLSelectOne, new { @class = "ddl80" })           
                         @Html.ValidationMessageFor(m => m.]]><xsl:value-of select="IDFK3"/><![CDATA[) </td>
                     </tr>      ]]>                
    </xsl:if> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">  
          <![CDATA[          <tr>
                        <td>@SharedRes.StringsRes.Valid_Year </td>
                        <td>@Html.EditorFor(m => m.Valid) 
                        @Html.ValidationMessageFor(m => m.Valid)                                    
                        @Html.TextBoxFor(m => m.ValidYear, new { @class = "textbox3" }) 
                        @Html.ValidationMessageFor(m => m.ValidYear) </td>
                    </tr>   ]]>
</xsl:when>
<xsl:otherwise> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Synonym+++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Membership'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">        
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
<xsl:when test="Table ='Tbl90References'">        
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">        
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">        
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">        
</xsl:when>
<xsl:when test="Table ='TblCounters'">        
</xsl:when>
<xsl:otherwise> 
          <![CDATA[          <tr>
                        <td>@SharedRes.StringsRes.Synonym </td>
                        <td>@Html.TextBoxFor(m => m.Synonym, new { @class = "textbox72" }) 
                        @Html.ValidationMessageFor(m => m.Synonym) </td>
                    </tr>   ]]>
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Valid+++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Membership'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">        
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">        
</xsl:when>
<xsl:when test="Table ='Tbl90References'">        
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">        
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">        
</xsl:when>
<xsl:when test="Table ='TblCounters'">        
</xsl:when>
<xsl:otherwise> 
          <![CDATA[          <tr>
                        <td>@SharedRes.StringsRes.Valid_Year </td>
                        <td>@Html.EditorFor(m => m.Valid) 
                        @Html.ValidationMessageFor(m => m.Valid)                                    
                        @Html.TextBoxFor(m => m.ValidYear, new { @class = "textbox3" }) 
                        @Html.ValidationMessageFor(m => m.ValidYear) </td>
                    </tr>   ]]>
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Author+Notes++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Membership'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">        
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">        
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">        
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">        
          <![CDATA[          <tr>
                        <td>@SharedRes.StringsRes.Author_Year </td>
                        <td>@Html.TextBoxFor(m => m.RefAuthorName, new { @class = "textbox108" }) 
                        @Html.ValidationMessageFor(m => m.RefAuthorName) 
                        @Html.TextBoxFor(m => m.PublicationYear, new { @class = "textbox3" }) 
                        @Html.ValidationMessageFor(m => m.PublicationYear) </td>
                    </tr>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90References'">        
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">        
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
           <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Notes </td>
                        <td>@Html.TextAreaFor(m => m.Notes, 6, 108, new { @class = "textbox" }) 
                        @Html.ValidationMessageFor(m => m.Notes) </td>
                    </tr>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">        
</xsl:when>
<xsl:when test="Table ='TblCounters'">        
</xsl:when>
<xsl:otherwise> 
          <![CDATA[          <tr>
                        <td>@SharedRes.StringsRes.Author_Year </td>
                        <td>@Html.TextBoxFor(m => m.Author, new { @class = "textbox72" }) 
                        @Html.ValidationMessageFor(m => m.Author) 
                        @Html.TextBoxFor(m => m.AuthorYear, new { @class = "textbox3" }) 
                        @Html.ValidationMessageFor(m => m.AuthorYear) </td>
                    </tr>   ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Info+++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Membership'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">        
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
<xsl:when test="Table ='Tbl90References'">   
          <![CDATA[          <tr>
                        <td>@SharedRes.StringsRes.Info </td>
                        <td>@Html.TextBoxFor(m => m.Info, new { @class = "textbox108" }) 
                        @Html.ValidationMessageFor(m => m.Info) </td>
                    </tr>   ]]>     
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">        
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">   
          <![CDATA[          <tr>
                        <td>@SharedRes.StringsRes.Info </td>
                        <td>@Html.TextBoxFor(m => m.Info, new { @class = "textbox108" }) 
                        @Html.ValidationMessageFor(m => m.Info) </td>
                    </tr>   ]]>     
</xsl:when>
<xsl:when test="Table ='TblCounters'">        
</xsl:when>
<xsl:otherwise> 
          <![CDATA[          <tr>
                        <td>@SharedRes.StringsRes.Info </td>
                        <td>@Html.TextBoxFor(m => m.Info, new { @class = "textbox72" }) 
                        @Html.ValidationMessageFor(m => m.Info) </td>
                    </tr>   ]]>
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Valid+++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Membership'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">        
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">    
          <![CDATA[          <tr>
                        <td>@SharedRes.StringsRes.Valid_Year </td>
                        <td>@Html.EditorFor(m => m.Valid) 
                        @Html.ValidationMessageFor(m => m.Valid)                                    
                        @Html.TextBoxFor(m => m.ValidYear, new { @class = "textbox3" }) 
                        @Html.ValidationMessageFor(m => m.ValidYear) </td>
                    </tr>   ]]>   
           <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.ArticelTitle </td>
                        <td>@Html.TextBoxFor(m => m.ArticelTitle, new { @class = "textbox108" }) 
                        @Html.ValidationMessageFor(m => m.ArticelTitle) </td>
                    </tr>   ]]>
           <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.BookName </td>
                        <td>@Html.TextBoxFor(m => m.BookName, new { @class = "textbox108" }) 
                        @Html.ValidationMessageFor(m => m.BookName) </td>
                    </tr>   ]]>
           <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Page </td>
                        <td>@Html.TextBoxFor(m => m.Page, new { @class = "textbox36" }) 
                        @Html.ValidationMessageFor(m => m.Page) </td>
                    </tr>   ]]>
           <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Publisher </td>
                        <td>@Html.TextBoxFor(m => m.Publisher, new { @class = "textbox108" }) 
                        @Html.ValidationMessageFor(m => m.Publisher) </td>
                    </tr>   ]]>
           <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.PublicationPlace </td>
                        <td>@Html.TextBoxFor(m => m.PublicationPlace, new { @class = "textbox108" }) 
                        @Html.ValidationMessageFor(m => m.PublicationPlace) </td>
                    </tr>   ]]>
           <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.ISBN </td>
                        <td>@Html.TextBoxFor(m => m.ISBN, new { @class = "textbox36" }) 
                        @Html.ValidationMessageFor(m => m.ISBN) </td>
                    </tr>   ]]>
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Notes </td>
                        <td>@Html.TextAreaFor(m => m.Notes, 6, 108, new { @class = "textbox" }) 
                        @Html.ValidationMessageFor(m => m.Notes) </td>
                    </tr>   ]]>     
          <![CDATA[          <tr>
                        <td>@SharedRes.StringsRes.Info </td>
                        <td>@Html.TextBoxFor(m => m.Info, new { @class = "textbox108" }) 
                        @Html.ValidationMessageFor(m => m.Info) </td>
                    </tr>   ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">    
          <![CDATA[          <tr>
                        <td>@SharedRes.StringsRes.Valid_Year </td>
                        <td>@Html.EditorFor(m => m.Valid) 
                        @Html.ValidationMessageFor(m => m.Valid)                                    
                        @Html.TextBoxFor(m => m.ValidYear, new { @class = "textbox3" }) 
                        @Html.ValidationMessageFor(m => m.ValidYear) </td>
                    </tr>   ]]>   
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Notes </td>
                        <td>@Html.TextAreaFor(m => m.Notes, 6, 108, new { @class = "textbox" }) 
                        @Html.ValidationMessageFor(m => m.Notes) </td>
                    </tr>   ]]>     
          <![CDATA[          <tr>
                        <td>@SharedRes.StringsRes.Info </td>
                        <td>@Html.TextBoxFor(m => m.Info, new { @class = "textbox108" }) 
                        @Html.ValidationMessageFor(m => m.Info) </td>
                    </tr>   ]]> 
</xsl:when>
<xsl:otherwise> 
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='EngName uso+++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Membership'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">        
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
<xsl:when test="Table ='Tbl90References'">        
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">        
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">        
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">        
</xsl:when>
<xsl:when test="Table ='TblCounters'">        
</xsl:when>
<xsl:otherwise> 
          <![CDATA[          <tr>
                        <td>@SharedRes.StringsRes.EngName </td>
                        <td>@Html.TextBoxFor(m => m.EngName, new { @class = "textbox72" }) 
                        @Html.ValidationMessageFor(m => m.EngName) </td>
                    </tr>   ]]>
          <![CDATA[          <tr>
                        <td>@SharedRes.StringsRes.GerName </td>
                        <td>@Html.TextBoxFor(m => m.GerName, new { @class = "textbox72" }) 
                        @Html.ValidationMessageFor(m => m.GerName) </td>
                    </tr>   ]]>
          <![CDATA[          <tr>
                        <td>@SharedRes.StringsRes.FraName </td>
                        <td>@Html.TextBoxFor(m => m.FraName, new { @class = "textbox72" }) 
                        @Html.ValidationMessageFor(m => m.FraName) </td>
                    </tr>   ]]>
          <![CDATA[          <tr>
                        <td>@SharedRes.StringsRes.PorName </td>
                        <td>@Html.TextBoxFor(m => m.PorName, new { @class = "textbox72" }) 
                        @Html.ValidationMessageFor(m => m.PorName) </td>
                    </tr>   ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Memo+++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Membership'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">        
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">        
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">        
</xsl:when>
<xsl:when test="Table ='TblCounters'">        
</xsl:when>
<xsl:otherwise> 
          <![CDATA[          <tr>
                        <td>@SharedRes.StringsRes.Memo </td>
                        <td>@Html.TextAreaFor(m => m.Memo, 6, 108, new { @class = "textbox" }) 
                        @Html.ValidationMessageFor(m => m.Memo) </td>
                    </tr>   ]]>     
</xsl:otherwise>    
</xsl:choose> 

 
 
<xsl:choose>
<xsl:when test="Table ='Tbl69FiSpeciesses'">     
          <![CDATA[          <tr>                   
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoSpecies </td>
                        <td>@Html.TextAreaFor(m => m.MemoSpecies, 6, 108, new { @class = "textbox" })
                        @Html.ValidationMessageFor(m => m.MemoSpecies) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.TradeName </td>
                        <td>@Html.TextBoxFor(m => m.TradeName, new { @class = "textbox72" }) 
                        @Html.ValidationMessageFor(m => m.TradeName) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Importer_Year </td>
                        <td>@Html.TextBoxFor(m => m.Importer, new { @class = "textbox72" }) 
                        <@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.ImportingYear 
                        <@Html.TextBoxFor(m => m.ImportingYear, new { @class = "textbox3" }) 
                        @Html.ValidationMessageFor(m => m.ImportingYear) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.TypeSpecies </td>
                        <td>@Html.EditorFor(m => m.TypeSpecies, new { @class = "textbox72" }) 
                        @Html.ValidationMessageFor(m => m.TypeSpecies) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.LDANumber_LDAOrigin </td>
                        <td>@Html.TextBoxFor(m => m.LNumber, new { @class = "textbox36" }) 
                        @Html.ValidationMessageFor(m => m.LNumber) 
                        @Html.TextBoxFor(m => m.LDANumber, new { @class = "textbox36" }) 
                        @Html.ValidationMessageFor(m => m.LDANumber) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.LNumber_LOrigin </td>
                        <td>@Html.TextBoxFor(m => m.LOrigin, new { @class = "textbox36" }) 
                        @Html.ValidationMessageFor(m => m.LOrigin) 
                        @Html.TextBoxFor(m => m.LDAOrigin, new { @class = "textbox36" }) 
                        @Html.ValidationMessageFor(m => m.LDAOrigin) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.BasinLength </td>
                        <td>@Html.TextBoxFor(m => m.BasinLength, new { @class = "textbox3" })  cm
                        @Html.ValidationMessageFor(m => m.BasinLength) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.FishLength </td>
                        <td>@Html.TextBoxFor(m => m.FishLength, new { @class = "textbox3" })  cm
                        @Html.ValidationMessageFor(m => m.FishLength) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Karnivore </td>
                        <td>@Html.EditorFor(m => m.Karnivore) 
                        @Html.ValidationMessageFor(m => m.Karnivore) 

                        @]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Herbivore 
                        @Html.EditorFor(m => m.Herbivore) 
                        @Html.ValidationMessageFor(m => m.Herbivore) 

                        @]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Limnivore 
                        @Html.EditorFor(m => m.Limnivore) 
                        @Html.ValidationMessageFor(m => m.Limnivore) 

                        @]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Omnivore 
                        @Html.EditorFor(m => m.Omnivore) 
                        @Html.ValidationMessageFor(m => m.Omnivore) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoFoods </td>
                        <td>@Html.TextAreaFor(m => m.MemoFoods, 6, 108, new { @class = "textbox" })
                        @Html.ValidationMessageFor(m => m.MemoFoods) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Difficult1 </td>
                        <td>@Html.EditorFor(m => m.Difficult1) 
                        @Html.ValidationMessageFor(m => m.Difficult1) 

                        @]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Difficult2 
                        @Html.EditorFor(m => m.Difficult2) 
                        @Html.ValidationMessageFor(m => m.Difficult2) 

                        @]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Difficult3 
                        @Html.EditorFor(m => m.Difficult3) 
                        @Html.ValidationMessageFor(m => m.Difficult3) 

                        @]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Difficult4 
                        @Html.EditorFor(m => m.Difficult4) 
                        @Html.ValidationMessageFor(m => m.Difficult4) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.RegionTop </td>
                        <td>@Html.EditorFor(m => m.RegionTop) 
                        @Html.ValidationMessageFor(m => m.RegionTop) 

                        @]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.RegionMiddle 
                        @Html.EditorFor(m => m.RegionMiddle) 
                        @Html.ValidationMessageFor(m => m.RegionMiddle) 

                        @]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.RegionBottom 
                        @Html.EditorFor(m => m.RegionBottom) 
                        @Html.ValidationMessageFor(m => m.RegionBottom) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoRegion </td>
                        <td>@Html.TextAreaFor(m => m.MemoRegion, 6, 108, new { @class = "textbox" })
                        @Html.ValidationMessageFor(m => m.MemoRegion) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoTech </td>
                        <td>@Html.TextAreaFor(m => m.MemoTech, 6, 108, new { @class = "textbox" })
                        @Html.ValidationMessageFor(m => m.MemoTech) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Ph1 </td>
                        <td>@Html.TextBoxFor(m => m.Ph1, new { @class = "textbox3" }) 
                        @Html.ValidationMessageFor(m => m.Ph1) 

                        @]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Ph2 
                        @Html.TextBoxFor(m => m.Ph2, new { @class = "textbox3" }) 
                        @Html.ValidationMessageFor(m => m.Ph2) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Temp1 </td>
                        <td>@Html.TextBoxFor(m => m.Temp1, new { @class = "textbox3" })  
                        @Html.ValidationMessageFor(m => m.Temp1) 

                        @]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Temp2 
                        @Html.TextBoxFor(m => m.Temp2, new { @class = "textbox3" })  °C
                        @Html.ValidationMessageFor(m => m.Temp2) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Hardness1 </td>
                        <td>@Html.TextBoxFor(m => m.Hardness1, new { @class = "textbox3" })  
                        @Html.ValidationMessageFor(m => m.Hardness1) 

                        @]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Hardness2 
                        @Html.TextBoxFor(m => m.Hardness2, new { @class = "textbox3" })  dGH
                        @Html.ValidationMessageFor(m => m.Hardness2) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.CarboHardness1 </td>
                        <td>@Html.TextBoxFor(m => m.CarboHardness1, new { @class = "textbox3" })  
                        @Html.ValidationMessageFor(m => m.CarboHardness1) 

                        @]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.CarboHardness2 
                        @Html.TextBoxFor(m => m.CarboHardness2, new { @class = "textbox3" })  KH
                        @Html.ValidationMessageFor(m => m.CarboHardness2) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoHusbandry </td>
                        <td>@Html.TextAreaFor(m => m.MemoHusbandry, 6, 108, new { @class = "textbox" })
                        @Html.ValidationMessageFor(m => m.MemoHusbandry) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoBreeding </td>
                        <td>@Html.TextAreaFor(m => m.MemoBreeding, 6, 108, new { @class = "textbox" })
                        @Html.ValidationMessageFor(m => m.MemoBreeding) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoBuilt </td>
                        <td>@Html.TextAreaFor(m => m.MemoBuilt, 6, 108, new { @class = "textbox" })
                        @Html.ValidationMessageFor(m => m.MemoBuilt) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoColor </td>
                        <td>@Html.TextAreaFor(m => m.MemoColor, 6, 108, new { @class = "textbox" })
                        @Html.ValidationMessageFor(m => m.MemoColor) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoSozial </td>
                        <td>@Html.TextAreaFor(m => m.MemoSozial, 6, 108, new { @class = "textbox" })
                        @Html.ValidationMessageFor(m => m.MemoSozial) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoDomorphism </td>
                        <td>@Html.TextAreaFor(m => m.MemoDomorphism, 6, 108, new { @class = "textbox" })
                        @Html.ValidationMessageFor(m => m.MemoDomorphism) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoSpecial </td>
                        <td>@Html.TextAreaFor(m => m.MemoSpecial, 6, 108, new { @class = "textbox" })
                        @Html.ValidationMessageFor(m => m.MemoSpecial) </td>
                    </tr>           ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">     
          <![CDATA[          <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoSpecies </td>
                        <td>@Html.TextAreaFor(m => m.MemoSpecies, 6, 108, new { @class = "textbox" })
                        @Html.ValidationMessageFor(m => m.MemoSpecies) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.TradeName </td>
                        <td>@Html.TextBoxFor(m => m.TradeName, new { @class = "textbox72" }) 
                        @Html.ValidationMessageFor(m => m.TradeName) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Importer_Year </td>
                        <td>@Html.TextBoxFor(m => m.Importer, new { @class = "textbox72" }) 
                        <@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.ImportingYear 
                        <@Html.TextBoxFor(m => m.ImportingYear, new { @class = "textbox3" }) 
                        @Html.ValidationMessageFor(m => m.ImportingYear) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.BasinHeight </td>
                        <td>@Html.TextBoxFor(m => m.BasinHeight, new { @class = "textbox3" })  cm
                        @Html.ValidationMessageFor(m => m.BasinHeight) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.PlantLength </td>
                        <td>@Html.TextBoxFor(m => m.PlantLength, new { @class = "textbox3" })  cm
                        @Html.ValidationMessageFor(m => m.PlantLength) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Difficult1 </td>
                        <td>@Html.EditorFor(m => m.Difficult1) 
                        @Html.ValidationMessageFor(m => m.Difficult1) 

                        @]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Difficult2 
                        @Html.EditorFor(m => m.Difficult2) 
                        @Html.ValidationMessageFor(m => m.Difficult2) 

                        @]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Difficult3 
                        @Html.EditorFor(m => m.Difficult3) 
                        @Html.ValidationMessageFor(m => m.Difficult3) 

                        @]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Difficult4 
                        @Html.EditorFor(m => m.Difficult4) 
                        @Html.ValidationMessageFor(m => m.Difficult4) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoTech </td>
                        <td>@Html.TextAreaFor(m => m.MemoTech, 6, 108, new { @class = "textbox" })
                        @Html.ValidationMessageFor(m => m.MemoTech) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Ph1 </td>
                        <td>@Html.TextBoxFor(m => m.Ph1, new { @class = "textbox3" }) 
                        @Html.ValidationMessageFor(m => m.Ph1) 

                        @]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Ph2 
                        @Html.TextBoxFor(m => m.Ph2, new { @class = "textbox3" }) 
                        @Html.ValidationMessageFor(m => m.Ph2) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Temp1 </td>
                        <td>@Html.TextBoxFor(m => m.Temp1, new { @class = "textbox3" })  
                        @Html.ValidationMessageFor(m => m.Temp1) 

                        @]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Temp2 
                        @Html.TextBoxFor(m => m.Temp2, new { @class = "textbox3" })  °C
                        @Html.ValidationMessageFor(m => m.Temp2) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Hardness1 </td>
                        <td>@Html.TextBoxFor(m => m.Hardness1, new { @class = "textbox3" })  
                        @Html.ValidationMessageFor(m => m.Hardness1) 

                        @]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Hardness2 
                        @Html.TextBoxFor(m => m.Hardness2, new { @class = "textbox3" })  dGH
                        @Html.ValidationMessageFor(m => m.Hardness2) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.CarboHardness1 </td>
                        <td>@Html.TextBoxFor(m => m.CarboHardness1, new { @class = "textbox3" })  
                        @Html.ValidationMessageFor(m => m.CarboHardness1) 

                        @]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.CarboHardness2 
                        @Html.TextBoxFor(m => m.CarboHardness2, new { @class = "textbox3" })  KH
                        @Html.ValidationMessageFor(m => m.CarboHardness2) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoBuilt </td>
                        <td>@Html.TextAreaFor(m => m.MemoBuilt, 6, 108, new { @class = "textbox" })
                        @Html.ValidationMessageFor(m => m.MemoBuilt) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoColor </td>
                        <td>@Html.TextAreaFor(m => m.MemoColor, 6, 108, new { @class = "textbox" })
                        @Html.ValidationMessageFor(m => m.MemoColor) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoReproduction </td>
                        <td>@Html.TextAreaFor(m => m.MemoReproduction, 6, 108, new { @class = "textbox" })
                        @Html.ValidationMessageFor(m => m.MemoReproduction) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoCulture </td>
                        <td>@Html.TextAreaFor(m => m.MemoCulture, 6, 108, new { @class = "textbox" })
                        @Html.ValidationMessageFor(m => m.MemoCulture) </td>
                    </tr>    
                    <tr>
                        <td>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoGlobal </td>              
                        <td>@Html.TextAreaFor(m => m.MemoGlobal, 6, 108, new { @class = "textbox" })
                        @Html.ValidationMessageFor(m => m.MemoGlobal) </td>
                    </tr>           ]]>  
</xsl:when>
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose>        
 <![CDATA[                  </table> 
        <p></p>            
        <div class="pagination">
            <p><input type="submit" value="@SharedRes.StringsRes.ButtonSave" />  
                @Html.ActionLink(SharedRes.StringsRes.ActionLnkBack, "Index", null, new { style = "font-weight:bold" })  
            </p>  
        </div>
        </fieldset>
}   ]]>

    
<xsl:choose>
<xsl:when test="Table ='Tbl81Images'">     
<![CDATA[        </form>  ]]>
</xsl:when>
<xsl:otherwise>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='JQuery+++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">        
<![CDATA[
<script type="text/javascript"> 
$(function()   {
    $('#FiSpeciesID').focus();    
});

        $(document).ready(function () {
            EnableMapMouseClickCallback();

            $("#Address").blur(function () {
                //If it's time to look for an address, 
                // clear out the Lat and Lon
                $("#Latitude").val("0");
                $("#Longitude").val("0");
                var address = jQuery.trim($("#Address").val());
                if (address.length < 1)
                    return;
                FindAddressOnMap(address);
            });
        });
</script> 
]]>
</xsl:when>
<xsl:otherwise> 
</xsl:otherwise>    
</xsl:choose> 


</xsl:template>
</xsl:stylesheet>





