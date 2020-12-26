<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[@model Atis.Domain.ViewModels.]]><xsl:value-of select="Table"/><![CDATA[.ListViewModel

        <!-- Index  Skriptdatum: ]]> <xsl:value-of select="DateTime"/><![CDATA[   -->    ]]>
<xsl:if test="Table='Tbl81Images'">  
<![CDATA[@{
    WebImage photo = null;
    if(IsPost)
    {
        photo = WebImage.GetImageFromRequest("Image");
    }
}     ]]>
</xsl:if>   <![CDATA[ 

@{
    ViewBag.Title = ]]> <xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Index;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Index</h2>

@using (Html.BeginForm("Index", "]]><xsl:value-of select="Table"/><![CDATA[", FormMethod.Get))              
{
    <fieldset class="filtering">
        <legend>@SharedRes.StringsRes.Filter</legend>  
        <div> 
            <p></p>     ]]>    
<xsl:choose>
<xsl:when test="Table ='ID and Name+++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">  
         <![CDATA[   <table>   ]]>                                          
        <![CDATA[        <tr>  ]]>
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" /></td>          ]]>  
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[name" name="]]><xsl:value-of select="BasisSm"/><![CDATA[name" /></td>          ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">  
         <![CDATA[   <table>   ]]>                                          
        <![CDATA[        <tr>  ]]>
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" /></td>          ]]>  
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[name" name="]]><xsl:value-of select="BasisSm"/><![CDATA[name" /></td>          ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">  
         <![CDATA[   <table>   ]]>                                          
        <![CDATA[        <tr>  ]]>
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" /></td>          ]]>  
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[name" name="]]><xsl:value-of select="BasisSm"/><![CDATA[name" /></td>          ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl18Superclasses'"> 
         <![CDATA[   <table>   ]]>                                          
        <![CDATA[        <tr>  ]]>
        <![CDATA[            <td style="width:9em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" /></td>          ]]>  
        <![CDATA[            <td style="width:13em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[name" name="]]><xsl:value-of select="BasisSm"/><![CDATA[name" /></td>          ]]>  
        <![CDATA[        </tr>  ]]>
         <![CDATA[   </table>   ]]>                                          
         <![CDATA[   <table>   ]]>                                          
        <![CDATA[        <tr>  ]]>            
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ </strong></td>
                    <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK1"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK1"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      ]]>
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="TableFK2"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK2"/><![CDATA[ </strong></td>
                    <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK2"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK2"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      ]]>
</xsl:when>
<xsl:when test="Table ='Tbl78Names'"> 
         <![CDATA[   <table>   ]]>                                          
        <![CDATA[        <tr>  ]]>
        <![CDATA[            <td style="width:7.9em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" /></td>          ]]>  
        <![CDATA[            <td style="width:11.9em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[name" name="]]><xsl:value-of select="BasisSm"/><![CDATA[name" /></td>          ]]>  
        <![CDATA[        </tr>  ]]>
         <![CDATA[   </table>   ]]>                                          
         <![CDATA[   <table>   ]]>                                          
        <![CDATA[        <tr>  ]]>            
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ </strong></td>
                    <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK1"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK1"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      ]]>
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="TableFK2"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK2"/><![CDATA[ </strong></td>
                    <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK2"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK2"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      ]]>      
</xsl:when>
<xsl:when test="Table ='Tbl81Images'"> 
         <![CDATA[   <table>   ]]>                                          
        <![CDATA[        <tr>  ]]>
        <![CDATA[            <td style="width:7.9em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" /></td>          ]]>  
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ </strong></td>
                    <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK1"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK1"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      ]]>
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="TableFK2"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK2"/><![CDATA[ </strong></td>
                    <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK2"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK2"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      ]]>          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'"> 
         <![CDATA[   <table>   ]]>                                          
        <![CDATA[        <tr>  ]]>
        <![CDATA[            <td style="width:7.9em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" /></td>          ]]>  
        <![CDATA[            <td style="width:11.9em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[name" name="]]><xsl:value-of select="BasisSm"/><![CDATA[name" /></td>          ]]>  
        <![CDATA[        </tr>  ]]>
         <![CDATA[   </table>   ]]>                                          
         <![CDATA[   <table>   ]]>                                          
        <![CDATA[        <tr>  ]]>            
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ </strong></td>
                    <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK1"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK1"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      ]]>
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="TableFK2"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK2"/><![CDATA[ </strong></td>
                    <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK2"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK2"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      ]]>      
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
         <![CDATA[   <table>   ]]>                                          
        <![CDATA[        <tr>  ]]>
        <![CDATA[            <td style="width:7.9em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" /></td>          ]]>  
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ </strong></td>
                    <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK1"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK1"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      ]]>
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="TableFK2"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK2"/><![CDATA[ </strong></td>
                    <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK2"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK2"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      ]]>          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">  
         <![CDATA[   <table>   ]]>                                          
        <![CDATA[        <tr>  ]]>
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" /></td>          ]]>  
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[name" name="]]><xsl:value-of select="BasisSm"/><![CDATA[name" /></td>          ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl90References'">  
    <xsl:if test="TableFK1 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="BasisFK1"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK1"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK1"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl80" })     </td>      
                     </tr>    ]]>                
    </xsl:if>        
    <xsl:if test="TableFK2 !='NULL'">
        <![CDATA[          <tr>   
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="BasisFK2"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK2"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK2"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl80" })    </td>       
                     </tr>   ]]>  
    </xsl:if>        
    <xsl:if test="TableFK3 !='NULL'">
        <![CDATA[             <tr>
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="BasisFK3"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK3"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK3"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl80" })      </td>     
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK4 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK4"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK4"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK4"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      
                         ]]>                
    </xsl:if>        
    <xsl:if test="TableFK5 !='NULL'">
        <![CDATA[            
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK5"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK5"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK5"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })    </td>       
                       ]]>  
    </xsl:if>        
    <xsl:if test="TableFK6 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK6"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK6"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK6"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })      </td>     
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK7 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK7"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK7"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK7"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      
                         ]]>                
    </xsl:if>        
    <xsl:if test="TableFK8 !='NULL'">
        <![CDATA[            
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK8"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK8"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK8"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })    </td>       
                       ]]>  
    </xsl:if>        
    <xsl:if test="TableFK9 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK9"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK9"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK9"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })      </td>     
                           ]]>  
    </xsl:if>        
    <xsl:if test="TableFK10 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK10"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK10"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK10"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })      </td>     
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK11 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK11"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK11"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK11"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      
                         ]]>                
    </xsl:if>        
    <xsl:if test="TableFK12 !='NULL'">
        <![CDATA[            
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK12"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK12"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK12"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })    </td>       
                       ]]>  
    </xsl:if>        
    <xsl:if test="TableFK13 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK13"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK13"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK13"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })      </td>     
                           ]]>  
    </xsl:if>        
    <xsl:if test="TableFK14 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK14"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK14"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK14"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })      </td>     
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK15 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK15"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK15"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK15"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      
                         ]]>                
    </xsl:if>        
    <xsl:if test="TableFK16 !='NULL'">
        <![CDATA[            
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK16"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK16"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK16"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })    </td>       
                       ]]>  
    </xsl:if>        
    <xsl:if test="TableFK17 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK17"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK17"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK17"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })      </td>     
                           ]]>  
    </xsl:if>        
    <xsl:if test="TableFK18 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK18"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK18"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK18"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })      </td>     
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>   
    <xsl:if test="TableFK19 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK19"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK19"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK19"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      
                         ]]>                
    </xsl:if>        
    <xsl:if test="TableFK20 !='NULL'">
        <![CDATA[            
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK20"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK20"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK20"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })    </td>       
                       ]]>  
    </xsl:if>        
    <xsl:if test="TableFK21 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK21"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK21"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK21"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })      </td>     
                           ]]>  
    </xsl:if>        
    <xsl:if test="TableFK22 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK22"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK22"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK22"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })      </td>     
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK23 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK23"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK23"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK23"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      
                         ]]>                
    </xsl:if>        
    <xsl:if test="TableFK25 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK25"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK25"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK25"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })      </td>     
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK24 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK24"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK24"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK24"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      
                         ]]>                
    </xsl:if>        
    <xsl:if test="TableFK26 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK26"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK26"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK26"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })      </td>     
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK27 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK27"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK27"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK27"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      
                         ]]>                
    </xsl:if>        
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[ </strong></td>
                         <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" /></td>             ]]>      
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">  
         <![CDATA[   <table>   ]]>                                          
        <![CDATA[        <tr>  ]]>
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" /></td>          ]]>  
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[name" name="]]><xsl:value-of select="BasisSm"/><![CDATA[name" /></td>          ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">  
         <![CDATA[   <table>   ]]>                                          
        <![CDATA[        <tr>  ]]>
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" /></td>          ]]>  
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[name" name="]]><xsl:value-of select="BasisSm"/><![CDATA[name" /></td>          ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">  
    <xsl:if test="TableFK4 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK4"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK4"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK4"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      
                         ]]>                
    </xsl:if>        
    <xsl:if test="TableFK5 !='NULL'">
        <![CDATA[            
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK5"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK5"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK5"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })    </td>       
                       ]]>  
    </xsl:if>        
    <xsl:if test="TableFK6 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK6"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK6"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK6"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })      </td>     
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK7 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK7"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK7"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK7"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      
                         ]]>                
    </xsl:if>        
    <xsl:if test="TableFK8 !='NULL'">
        <![CDATA[            
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK8"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK8"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK8"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })    </td>       
                       ]]>  
    </xsl:if>        
    <xsl:if test="TableFK9 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK9"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK9"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK9"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })      </td>     
                           ]]>  
    </xsl:if>        
    <xsl:if test="TableFK10 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK10"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK10"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK10"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })      </td>     
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK11 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK11"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK11"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK11"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      
                         ]]>                
    </xsl:if>        
    <xsl:if test="TableFK12 !='NULL'">
        <![CDATA[            
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK12"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK12"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK12"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })    </td>       
                       ]]>  
    </xsl:if>        
    <xsl:if test="TableFK13 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK13"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK13"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK13"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })      </td>     
                           ]]>  
    </xsl:if>        
    <xsl:if test="TableFK14 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK14"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK14"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK14"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })      </td>     
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK15 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK15"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK15"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK15"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      
                         ]]>                
    </xsl:if>        
    <xsl:if test="TableFK16 !='NULL'">
        <![CDATA[            
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK16"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK16"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK16"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })    </td>       
                       ]]>  
    </xsl:if>        
    <xsl:if test="TableFK17 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK17"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK17"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK17"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })      </td>     
                           ]]>  
    </xsl:if>        
    <xsl:if test="TableFK18 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK18"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK18"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK18"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })      </td>     
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>   
    <xsl:if test="TableFK19 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK19"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK19"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK19"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      
                         ]]>                
    </xsl:if>        
    <xsl:if test="TableFK20 !='NULL'">
        <![CDATA[            
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK20"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK20"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK20"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })    </td>       
                       ]]>  
    </xsl:if>        
    <xsl:if test="TableFK21 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK21"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK21"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK21"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })      </td>     
                           ]]>  
    </xsl:if>        
    <xsl:if test="TableFK22 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK22"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK22"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK22"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })      </td>     
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK23 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK23"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK23"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK23"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      
                         ]]>                
    </xsl:if>        
    <xsl:if test="TableFK25 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK25"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK25"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK25"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })      </td>     
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK24 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK24"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK24"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK24"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      
                         ]]>                
    </xsl:if>        
    <xsl:if test="TableFK26 !='NULL'">
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK26"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK26"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK26"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })      </td>     
                     </tr>      ]]>  
           <![CDATA[   </table>   ]]>                                                        
    </xsl:if>        
    <xsl:if test="TableFK27 !='NULL'">
           <![CDATA[   <table>   ]]>                                          
        <![CDATA[             <tr>
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.DDL]]><xsl:value-of select="NameFK27"/><![CDATA[ </strong></td>
                         <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK27"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK27"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl15" })     </td>      
                         ]]>                
    </xsl:if>        
        <![CDATA[             
                         <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[ </strong></td>
                         <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" /></td>             ]]>      
</xsl:when>
<xsl:when test="Table ='TblCounters'">  
         <![CDATA[   <table>   ]]>                                          
        <![CDATA[        <tr>  ]]>
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" /></td>          ]]>  
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[name" name="]]><xsl:value-of select="BasisSm"/><![CDATA[name" /></td>          ]]>  
</xsl:when>
<xsl:otherwise> 
         <![CDATA[   <table>   ]]>                                          
        <![CDATA[        <tr>  ]]>
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" /></td>          ]]>  
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ </strong></td>
                    <td><input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[name" name="]]><xsl:value-of select="BasisSm"/><![CDATA[name" /></td>          ]]>  
        <![CDATA[            <td style="width:8em"><strong>@]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ </strong></td>
                    <td>@Html.DropDownListFor(m => m.]]><xsl:value-of select="IDFK1"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK1"/><![CDATA[List, SharedRes.StringsRes.DDLAll, new { @class = "ddl" })     </td>      ]]>
</xsl:otherwise>    
</xsl:choose>
 
<xsl:choose>
<xsl:when test="Table ='Valid++++++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">      
</xsl:when>
<xsl:when test="Table ='aspnet_Membership'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">        
</xsl:when>
<xsl:when test="Table ='TblCounters'">        
<![CDATA[                    <td style="width:4em"><strong>@SharedRes.StringsRes.Valid </strong></td>
                    @Html.Hidden("sortBy", Model.SortBy)
                    @Html.Hidden("ascending", Model.SortAscending)

                    <td><input type="submit" value="@SharedRes.StringsRes.Search" /></td> 
                </tr>   
            </table>   ]]>     
</xsl:when>
<xsl:otherwise>  
<![CDATA[                    <td style="width:4em"><strong>@SharedRes.StringsRes.Valid </strong></td>
                    <td>@Html.EditorFor(m => m.Valid)</td>

                    @Html.Hidden("sortBy", Model.SortBy)
                    @Html.Hidden("ascending", Model.SortAscending)

                    <td><input type="submit" value="@SharedRes.StringsRes.Search" /></td> 
                </tr>   
            </table>   ]]>     
</xsl:otherwise>    
</xsl:choose> 
<![CDATA[        </div>
    </fieldset>
}

<table class="grid" style="width: 95%">                
    <tr>
        <th></th>   ]]>
<xsl:choose>
<xsl:when test="Table ='GridHeader name+++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">  
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="Name"/><![CDATA[" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ } })</th>                   
        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Subregnum" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Subregnum } })</th>    ]]>                
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">  
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="Name"/><![CDATA[" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ } })</th>                   
        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Subspeciesgroup" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Subspeciesgroup } })</th>    ]]>                
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">  
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="IDFK1"/><![CDATA[" }, { "DisplayName", ]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ } })</th>                
        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="Name"/><![CDATA[" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ } })</th>                   
        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Subspecies" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Subspecies } })</th>                    
        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Divers" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Divers } })</th>    ]]>                
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">  
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="IDFK1"/><![CDATA[" }, { "DisplayName", ]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ } })</th>                
        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="Name"/><![CDATA[" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ } })</th>                   
        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Subspecies" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Subspecies } })</th>                    
        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Divers" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Divers } })</th>    ]]>                
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
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="Name"/><![CDATA[" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ } })</th>    ]]>                
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='GridHeader Author+Language+++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">  
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "LoweredApplicationName" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.LoweredApplicationName } })</th>   ]]>
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Description" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Description } })</th>   ]]>
</xsl:when>
<xsl:when test="Table ='aspnet_Membership'">  
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "LoweredApplicationName" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.LoweredApplicationName } })</th>   ]]>
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Description" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Description } })</th>   ]]>
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">  
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "LoweredUserName" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.LoweredUserName } })</th>   ]]>
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "MobileAlias" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MobileAlias } })</th>   ]]>
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "IsAnonymous" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.IsAnonymous } })</th>   ]]>
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "LastActivityDate" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.LastActivityDate } })</th>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">  
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Language" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Language } })</th>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Country" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Country } })</th>   ]]>
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Address" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Address } })</th>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">  
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "PublicationYear" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.PublicationYear } })</th>   ]]>
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "ArticelTitle" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.ArticelTitle } })</th>   ]]>
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "BookName" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.BookName } })</th>   ]]>
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Page" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Page } })</th>   ]]>
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Publisher" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Publisher } })</th>   ]]>
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Notes" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Notes } })</th>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90References'">  
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "RefExpertID" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.RefExpert } })</th>   ]]>
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "RefSourceID" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.RefSource } })</th>   ]]>
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "SourceYear" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.SourceYear } })</th>   ]]>
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "RefAuthorID" }, { "DisplayName", SharedRes.StringsRes.Author } })</th>   ]]>
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "PublicationYear" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.PublicationYear } })</th>   ]]>
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Page" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Page } })</th>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">  
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Notes" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Notes } })</th>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">  
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "SourceYear" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.SourceYear } })</th>   ]]>
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Notes" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Notes } })</th>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">  
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Memo" }, { "DisplayName", SharedRes.StringsRes.Memo } })</th>   ]]>
</xsl:when>
<xsl:when test="Table ='TblCounters'">  
</xsl:when>
<xsl:otherwise> 
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Author" }, { "DisplayName", SharedRes.StringsRes.Author } })</th>
        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "AuthorYear" }, { "DisplayName", SharedRes.StringsRes.AuthorYear } })</th>  ]]>
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Valid Header+++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">      
</xsl:when>
<xsl:when test="Table ='aspnet_Membership'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">        
</xsl:when>
<xsl:when test="Table ='TblCounters'">        
</xsl:when>
<xsl:otherwise> 
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Valid" }, { "DisplayName", SharedRes.StringsRes.Valid } })</th>
        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "ValidYear" }, { "DisplayName", SharedRes.StringsRes.ValidYear } })</th>  ]]> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='GridHeader+++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">      
</xsl:when>
<xsl:when test="Table ='aspnet_Membership'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">        
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">  
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">  
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="IDFK1"/><![CDATA[" }, { "DisplayName", ]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ } })</th>
        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="TableFK1"/><![CDATA[.Subregnum" }, { "DisplayName", ]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.Subregnum } })</th>]]>                
</xsl:when>
<xsl:when test="Table ='Tbl18Superclasses'">  
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="IDFK1"/><![CDATA[" }, { "DisplayName", ]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ } })</th>
        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="IDFK2"/><![CDATA[" }, { "DisplayName", ]]><xsl:value-of select="TableFK2"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK2"/><![CDATA[ } })</th> ]]>                
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">  
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">  
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">  
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">  
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="IDFK1"/><![CDATA[" }, { "DisplayName", SharedRes.StringsRes.FiPl } })</th>   ]]>           
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Filestream" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Filestream } })</th> ]]>                
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="IDFK1"/><![CDATA[" }, { "DisplayName", SharedRes.StringsRes.FiPl } })</th> ]]>                
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">  
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="IDFK1"/><![CDATA[" }, { "DisplayName", SharedRes.StringsRes.FiPl } })</th> ]]>                
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="IDFK1"/><![CDATA[" }, { "DisplayName", SharedRes.StringsRes.FiPl } })</th> ]]>                
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">  
</xsl:when>
<xsl:when test="Table ='Tbl90References'">  
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Taxo } })</th> ]]>                
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">  
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">  
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">  
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Taxo } })</th> ]]>                
</xsl:when>
<xsl:when test="Table ='TblCounters'">  
</xsl:when>
<xsl:otherwise> 
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="IDFK1"/><![CDATA[" }, { "DisplayName", ]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ } })</th> ]]>                
</xsl:otherwise>    
</xsl:choose>
<![CDATA[     </tr>

    @foreach (var item in Model.]]><xsl:value-of select="Table"/><![CDATA[) { 
    
    <tr>
        <td class="left">
            @Html.ActionLink(SharedRes.StringsRes.ActionLnkDelete, "Delete", new { id = item.]]><xsl:value-of select="ID"/><![CDATA[ }) | 
            @Html.ActionLink(SharedRes.StringsRes.ActionLnkEdit, "Edit", new { id = item.]]><xsl:value-of select="ID"/><![CDATA[ }) |
            @Html.ActionLink(SharedRes.StringsRes.ActionLnkDetails, "Details", new { id = item.]]><xsl:value-of select="ID"/><![CDATA[ }) </td>           ]]>

<xsl:choose>
<xsl:when test="Table ='Grid name+++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">  
        <![CDATA[<td class="left">@item.]]><xsl:value-of select="Name"/><![CDATA[ </td>                   
        <td class="left">@item.LoweredApplicationName </td>   
        <td class="left">@item.Description </td>   ]]>
</xsl:when>
<xsl:when test="Table ='aspnet_Membership'">  
        <![CDATA[<td class="left">@item.]]><xsl:value-of select="Name"/><![CDATA[ </td>                   
        <td class="left">@item.LoweredApplicationName </td>   
        <td class="left">@item.Description </td>   ]]>
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">  
        <![CDATA[<td class="left">@item.]]><xsl:value-of select="Name"/><![CDATA[ </td>                   
        <td class="left">@item.LoweredUserName </td>   
        <td class="left">@item.MobileAlias </td>   
        <td class="left">@item.IsAnonymous </td>   
        <td class="left">@item.LastActivityDate </td>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">  
        <![CDATA[<td class="left">@item.]]><xsl:value-of select="Name"/><![CDATA[ </td>                   
        <td class="left">@item.Subregnum </td>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">  
        <![CDATA[<td class="left">@item.]]><xsl:value-of select="Name"/><![CDATA[</td>     ]]>              
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">  
        <![CDATA[<td class="left">@item.]]><xsl:value-of select="Name"/><![CDATA[</td>     ]]>              
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">  
        <![CDATA[<td class="left">@item.]]><xsl:value-of select="Name"/><![CDATA[ </td>                   
        <td class="left">@item.Subspeciesgroup </td>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">  
        <![CDATA[<td class="left">@item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[  </td>                
        <td class="left">@item.]]><xsl:value-of select="Name"/><![CDATA[ </td>
        <td class="left">@item.Subspecies </td>
        <td class="left">@item.Divers </td>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">  
        <![CDATA[<td class="left">@item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[  </td>                
        <td class="left">@item.]]><xsl:value-of select="Name"/><![CDATA[ </td>
        <td class="left">@item.Subspecies </td>
        <td class="left">@item.Divers </td>   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
</xsl:when>
<xsl:when test="Table ='Tbl90References'">  
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">  
        <![CDATA[<td class="left">@item.Memo</td>       ]]>
</xsl:when>
<xsl:when test="Table ='TblCounters'">  
</xsl:when>
<xsl:otherwise> 
        <![CDATA[<td class="left">@item.]]><xsl:value-of select="Name"/><![CDATA[ </td>    ]]>                
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Author+language++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Membership'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">        
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">  
        <![CDATA[<td class="left">@item.Language</td>       ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
        <![CDATA[<td class="left">@item.Country</td>       ]]>
        <![CDATA[<td class="left">@item.Address</td>       ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">  
        <![CDATA[<td class="align-middle">@item.PublicationYear</td>       ]]>
        <![CDATA[<td class="left">@item.ArticelTitle</td>       ]]>
        <![CDATA[<td class="left">@item.BookName</td>       ]]>
        <![CDATA[<td class="left">@item.Page</td>       ]]>
        <![CDATA[<td class="left">@item.Publisher</td>       ]]>
        <![CDATA[<td class="left">@item.Notes</td>       ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90References'">  
        <![CDATA[<td class="left">@if (item.]]><xsl:value-of select="IDFK1"/><![CDATA[ != null)       ]]>
        <![CDATA[    {@item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[}</td>       ]]>
        <![CDATA[<td class="left">@if (item.]]><xsl:value-of select="IDFK2"/><![CDATA[ != null)       ]]>
        <![CDATA[    {@item.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="NameFK2"/><![CDATA[}</td>       ]]>
        <![CDATA[<td class="left">@if (item.]]><xsl:value-of select="IDFK2"/><![CDATA[ != null)       ]]>
        <![CDATA[    {@item.]]><xsl:value-of select="TableFK2"/><![CDATA[.SourceYear}</td>       ]]>
        <![CDATA[<td class="left">@if (item.]]><xsl:value-of select="IDFK3"/><![CDATA[ != null)       ]]>
        <![CDATA[    {@item.]]><xsl:value-of select="TableFK3"/><![CDATA[.]]><xsl:value-of select="NameFK3"/><![CDATA[}</td>       ]]>
        <![CDATA[<td class="left">@if (item.]]><xsl:value-of select="IDFK3"/><![CDATA[ != null)       ]]>
        <![CDATA[    {@item.]]><xsl:value-of select="TableFK3"/><![CDATA[.PublicationYear}</td>       ]]>
        <![CDATA[<td class="left">@if (item.]]><xsl:value-of select="IDFK3"/><![CDATA[ != null)       ]]>
        <![CDATA[    {@item.]]><xsl:value-of select="TableFK3"/><![CDATA[.Page}</td>       ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">  
        <![CDATA[<td class="left">@item.Notes</td>       ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">  
        <![CDATA[<td class="align-middle">@item.SourceYear</td>       ]]>
        <![CDATA[<td class="left">@item.Notes</td>       ]]>
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">  
</xsl:when>
<xsl:when test="Table ='TblCounters'">  
</xsl:when>
<xsl:otherwise> 
        <![CDATA[<td class="left">@item.Author</td>       
        <td class="align-middle">@item.AuthorYear</td>   ]]>     
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Valid++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Membership'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">        
</xsl:when>
<xsl:when test="Table ='TblCounters'">        
</xsl:when>
<xsl:otherwise>  
<![CDATA[ 
        <td class="align-middle">
        @if (item.Valid == true) { 
                <img src="@Url.Content("~/Content/Images/Ok.png")" alt="@SharedRes.StringsRes.ValidYes" title="@SharedRes.StringsRes.ValidYes" />   } 
            else { 
                <img src="@Url.Content("~/Content/Images/cancel.png")" alt="@SharedRes.StringsRes.ValidNo" title="@SharedRes.StringsRes.ValidNo" />   
           } 
         </td>
        <td class="align-middle">@item.ValidYear</td>   ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Grid+++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Membership'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">        
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">  
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">  
        <![CDATA[<td class="left">@item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[  @item.]]><xsl:value-of select="TableFK1"/><![CDATA[.Subregnum</td> ]]>
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">  
        <![CDATA[<td class="left">@item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[  @item.]]><xsl:value-of select="TableFK1"/><![CDATA[.Subregnum</td>  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl18Superclasses'">  
        <![CDATA[@if (!item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[.StartsWith("#"))  {
            <td class="left">@item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[  </td>
            <td class="left"></td>  }
        else {
            <td class="left"></td>  
            <td class="left">@item.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="NameFK2"/><![CDATA[  </td>  } ]]>   
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">  
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">  
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">  
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">  
        <![CDATA[
        @if (! item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[.Contains("#")) { 
        <td class="left">@item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ @item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ @item.]]><xsl:value-of select="TableFK1"/><![CDATA[.Subspecies @item.]]><xsl:value-of select="TableFK1"/><![CDATA[.Divers   </td>   }
            else  { 
        <td class="left">@item.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ @item.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="NameFK2"/><![CDATA[ @item.]]><xsl:value-of select="TableFK2"/><![CDATA[.Subspecies @item.]]><xsl:value-of select="TableFK2"/><![CDATA[.Divers   </td>    }    ]]>              
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
        <![CDATA[
        <td class="align-middle"><img src="@Url.Action("GetFilestream", "]]><xsl:value-of select="Table"/><![CDATA[", new {id = item.]]><xsl:value-of select="ID"/><![CDATA[ })"  alt="@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Filestream" height="100" width="100" />    </td>             
        @if (! item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[.Contains("#")) { 
        <td class="left">@item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ @item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ @item.]]><xsl:value-of select="TableFK1"/><![CDATA[.Subspecies @item.]]><xsl:value-of select="TableFK1"/><![CDATA[.Divers   </td>   }
            else  { 
        <td class="left">@item.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ @item.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="NameFK2"/><![CDATA[ @item.]]><xsl:value-of select="TableFK2"/><![CDATA[.Subspecies @item.]]><xsl:value-of select="TableFK2"/><![CDATA[.Divers   </td>    }    ]]>                         
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">  
        <![CDATA[
        @if (! item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[.Contains("#")) { 
        <td class="left">@item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ @item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ @item.]]><xsl:value-of select="TableFK1"/><![CDATA[.Subspecies @item.]]><xsl:value-of select="TableFK1"/><![CDATA[.Divers   </td>   }
            else  { 
        <td class="left">@item.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ @item.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="NameFK2"/><![CDATA[ @item.]]><xsl:value-of select="TableFK2"/><![CDATA[.Subspecies @item.]]><xsl:value-of select="TableFK2"/><![CDATA[.Divers   </td>    }    ]]>               
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
        <![CDATA[
        @if (! item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[.Contains("#")) { 
        <td class="left">@item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ @item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ @item.]]><xsl:value-of select="TableFK1"/><![CDATA[.Subspecies @item.]]><xsl:value-of select="TableFK1"/><![CDATA[.Divers   </td>   }
            else  { 
        <td class="left">@item.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ @item.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="NameFK2"/><![CDATA[ @item.]]><xsl:value-of select="TableFK2"/><![CDATA[.Subspecies @item.]]><xsl:value-of select="TableFK2"/><![CDATA[.Divers   </td>    }    ]]>               
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">
</xsl:when>
<xsl:when test="Table ='Tbl90References'">  
        <![CDATA[ <td class="left">
        @if (item.]]><xsl:value-of select="IDFK4"/><![CDATA[ != null)
            {   <p>@item.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="TableFK6"/><![CDATA[.]]><xsl:value-of select="NameFK6"/><![CDATA[ @item.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="NameFK4"/><![CDATA[  @item.]]><xsl:value-of select="TableFK4"/><![CDATA[.Subspecies @item.]]><xsl:value-of select="TableFK4"/><![CDATA[.Divers   </p>}
        @if (item.]]><xsl:value-of select="IDFK5"/><![CDATA[ != null)
            {   <p>@item.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="TableFK6"/><![CDATA[.]]><xsl:value-of select="NameFK6"/><![CDATA[ @item.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="NameFK5"/><![CDATA[  @item.]]><xsl:value-of select="TableFK5"/><![CDATA[.Subspecies @item.]]><xsl:value-of select="TableFK5"/><![CDATA[.Divers   </p>}
        @if (item.]]><xsl:value-of select="IDFK6"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK6"/><![CDATA[.]]><xsl:value-of select="NameFK6"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK7"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK7"/><![CDATA[.]]><xsl:value-of select="NameFK7"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK8"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK8"/><![CDATA[.]]><xsl:value-of select="NameFK8"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK9"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK9"/><![CDATA[.]]><xsl:value-of select="NameFK9"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK10"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK10"/><![CDATA[.]]><xsl:value-of select="NameFK10"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK11"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK11"/><![CDATA[.]]><xsl:value-of select="NameFK11"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK12"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK12"/><![CDATA[.]]><xsl:value-of select="NameFK12"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK13"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK13"/><![CDATA[.]]><xsl:value-of select="NameFK13"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK14"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK14"/><![CDATA[.]]><xsl:value-of select="NameFK14"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK15"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK15"/><![CDATA[.]]><xsl:value-of select="NameFK15"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK16"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK16"/><![CDATA[.]]><xsl:value-of select="NameFK16"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK17"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK17"/><![CDATA[.]]><xsl:value-of select="NameFK17"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK18"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK18"/><![CDATA[.]]><xsl:value-of select="NameFK18"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK19"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK19"/><![CDATA[.]]><xsl:value-of select="NameFK19"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK20"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK20"/><![CDATA[.]]><xsl:value-of select="NameFK20"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK21"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK21"/><![CDATA[.]]><xsl:value-of select="NameFK21"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK22"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK22"/><![CDATA[.]]><xsl:value-of select="NameFK22"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK23"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK23"/><![CDATA[.]]><xsl:value-of select="NameFK23"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK24"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK24"/><![CDATA[.]]><xsl:value-of select="NameFK24"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK25"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK25"/><![CDATA[.]]><xsl:value-of select="NameFK25"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK26"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK26"/><![CDATA[.]]><xsl:value-of select="NameFK26"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK27"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK27"/><![CDATA[.]]><xsl:value-of select="NameFK27"/><![CDATA[   }
       </td>  ]]>               
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">  
        <![CDATA[ <td class="left">
        @if (item.]]><xsl:value-of select="IDFK4"/><![CDATA[ != null)
            {   <p>@item.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="TableFK6"/><![CDATA[.]]><xsl:value-of select="NameFK6"/><![CDATA[ @item.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="NameFK4"/><![CDATA[  @item.]]><xsl:value-of select="TableFK4"/><![CDATA[.Subspecies @item.]]><xsl:value-of select="TableFK4"/><![CDATA[.Divers   </p>}
        @if (item.]]><xsl:value-of select="IDFK5"/><![CDATA[ != null)
            {   <p>@item.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="TableFK6"/><![CDATA[.]]><xsl:value-of select="NameFK6"/><![CDATA[ @item.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="NameFK5"/><![CDATA[  @item.]]><xsl:value-of select="TableFK5"/><![CDATA[.Subspecies @item.]]><xsl:value-of select="TableFK5"/><![CDATA[.Divers   </p>}
        @if (item.]]><xsl:value-of select="IDFK6"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK6"/><![CDATA[.]]><xsl:value-of select="NameFK6"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK7"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK7"/><![CDATA[.]]><xsl:value-of select="NameFK7"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK8"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK8"/><![CDATA[.]]><xsl:value-of select="NameFK8"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK9"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK9"/><![CDATA[.]]><xsl:value-of select="NameFK9"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK10"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK10"/><![CDATA[.]]><xsl:value-of select="NameFK10"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK11"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK11"/><![CDATA[.]]><xsl:value-of select="NameFK11"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK12"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK12"/><![CDATA[.]]><xsl:value-of select="NameFK12"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK13"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK13"/><![CDATA[.]]><xsl:value-of select="NameFK13"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK14"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK14"/><![CDATA[.]]><xsl:value-of select="NameFK14"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK15"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK15"/><![CDATA[.]]><xsl:value-of select="NameFK15"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK16"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK16"/><![CDATA[.]]><xsl:value-of select="NameFK16"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK17"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK17"/><![CDATA[.]]><xsl:value-of select="NameFK17"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK18"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK18"/><![CDATA[.]]><xsl:value-of select="NameFK18"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK19"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK19"/><![CDATA[.]]><xsl:value-of select="NameFK19"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK20"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK20"/><![CDATA[.]]><xsl:value-of select="NameFK20"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK21"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK21"/><![CDATA[.]]><xsl:value-of select="NameFK21"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK22"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK22"/><![CDATA[.]]><xsl:value-of select="NameFK22"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK23"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK23"/><![CDATA[.]]><xsl:value-of select="NameFK23"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK24"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK24"/><![CDATA[.]]><xsl:value-of select="NameFK24"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK25"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK25"/><![CDATA[.]]><xsl:value-of select="NameFK25"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK26"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK26"/><![CDATA[.]]><xsl:value-of select="NameFK26"/><![CDATA[   }
        @if (item.]]><xsl:value-of select="IDFK27"/><![CDATA[ != null)
            {   @item.]]><xsl:value-of select="TableFK27"/><![CDATA[.]]><xsl:value-of select="NameFK27"/><![CDATA[   }
       </td>  ]]>               
</xsl:when>
<xsl:when test="Table ='TblCounters'">  
</xsl:when>
<xsl:otherwise> 
        <![CDATA[<td class="left">@item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[  </td> ]]>                
</xsl:otherwise>    
</xsl:choose>
<![CDATA[    </tr>    
    }     
    <tr>       ]]> 
<xsl:choose>
<xsl:when test="Table ='Pager++++++++++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">   
<![CDATA[        <td class="pager" colspan="10">   ]]>     
</xsl:when>
<xsl:when test="Table ='Tbl90References'">   
<![CDATA[        <td class="pager" colspan="12">   ]]>     
</xsl:when>
<xsl:otherwise>  
<![CDATA[        <td class="pager" colspan="9">   ]]>
</xsl:otherwise>    
</xsl:choose> 
<![CDATA[           @Html.Partial("_Pager", Model)
        </td>
    </tr>       
</table>
<div id="links">
    <table> 
        <tr>     ]]>
<xsl:choose>
<xsl:when test="Table ='Buttons FK+++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl90References'">        
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">        
</xsl:when>
<xsl:otherwise> 
  <xsl:if test="TableFK1 !='NULL'">
    <![CDATA[         <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisFK1"/><![CDATA[Left, "Index", "]]><xsl:value-of select="TableFK1"/><![CDATA[")  |
             </td>       ]]> 
  </xsl:if> 
  <xsl:if test="TableFK2 !='NULL'">
    <![CDATA[         <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisFK2"/><![CDATA[Left, "Index", "]]><xsl:value-of select="TableFK2"/><![CDATA[")  |     
             </td>     ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose>
<xsl:choose>
<xsl:when test="Table ='Buttons nach Basis TK+++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'"> 
  <xsl:if test="TableTK1 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK1"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK1"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if> 
  <xsl:if test="TableTK2 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK2"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK2"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'"> 
  <xsl:if test="TableTK1 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK1"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK1"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if> 
  <xsl:if test="TableTK2 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK2"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK2"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'"> 
  <xsl:if test="TableTK1 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK1"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK1"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if> 
  <xsl:if test="TableTK2 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK2"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK2"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
</xsl:when>
<xsl:when test="Table ='Tbl66Genusses'"> 
  <xsl:if test="TableTK1 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK1"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK1"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if> 
  <xsl:if test="TableTK2 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK2"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK2"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'"> 
  <xsl:if test="TableTK1 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK1"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK1"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK1"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableTK1"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if> 
  <xsl:if test="TableTK2 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK2"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK2"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK2"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableTK2"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
  <xsl:if test="TableTK3 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK3"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK3"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK3"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableTK3"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
  <xsl:if test="TableTK4 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK4"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK4"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK4"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableTK4"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'"> 
  <xsl:if test="TableTK1 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK1"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK1"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK1"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableTK1"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if> 
  <xsl:if test="TableTK2 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK2"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK2"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK2"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableTK2"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
  <xsl:if test="TableTK3 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK3"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK3"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK3"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableTK3"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
  <xsl:if test="TableTK4 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK4"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK4"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK4"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableTK4"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
</xsl:when>
<xsl:when test="Table ='Tbl78Names'"> 
  <xsl:if test="TableTK1 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK1"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK1"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK1"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableTK1"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if> 
  <xsl:if test="TableTK2 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK2"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK2"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK2"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableTK2"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
  <xsl:if test="TableTK3 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK3"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK3"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK3"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableTK3"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
</xsl:when>
<xsl:when test="Table ='Tbl81Images'"> 
  <xsl:if test="TableTK1 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK1"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK1"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK1"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableTK1"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if> 
  <xsl:if test="TableTK2 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK2"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK2"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK2"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableTK2"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
  <xsl:if test="TableTK3 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK3"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK3"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK3"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableTK3"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'"> 
  <xsl:if test="TableTK1 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK1"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK1"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK1"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableTK1"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if> 
  <xsl:if test="TableTK2 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK2"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK2"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK2"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableTK2"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
  <xsl:if test="TableTK3 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK3"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK3"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK3"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableTK3"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'"> 
  <xsl:if test="TableTK1 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK1"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK1"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK1"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableTK1"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if> 
  <xsl:if test="TableTK2 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK2"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK2"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK2"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableTK2"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
  <xsl:if test="TableTK3 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK3"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK3"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK3"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableTK3"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
  <xsl:if test="TableTK1 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK1"/><![CDATA[Left, "Index", "]]><xsl:value-of select="TableTK1"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if> 
  <xsl:if test="TableTK2 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK2"/><![CDATA[Left, "Index", "]]><xsl:value-of select="TableTK2"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
</xsl:when>
<xsl:when test="Table ='Tbl90References'"> 
  <xsl:if test="TableFK4 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisFK4"/><![CDATA[Left, "Index", "]]><xsl:value-of select="TableFK4"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if> 
  <xsl:if test="TableFK5 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisFK5"/><![CDATA[Left, "Index", "]]><xsl:value-of select="TableFK5"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
  <xsl:if test="TableFK1 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisFK1"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableFK1"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisFK1"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableFK1"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
  <xsl:if test="TableFK2 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisFK2"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableFK2"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisFK2"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableFK2"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
  <xsl:if test="TableFK3 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisFK3"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableFK3"/><![CDATA[")  |     
            </td>     ]]> 
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisFK3"/><![CDATA[CreateRight, "Create", "]]><xsl:value-of select="TableFK3"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
  <xsl:if test="TableTK1 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK1"/><![CDATA[Left, "Index", "]]><xsl:value-of select="TableTK1"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if> 
  <xsl:if test="TableTK2 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK2"/><![CDATA[Left, "Index", "]]><xsl:value-of select="TableTK2"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
  <xsl:if test="TableTK1 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK1"/><![CDATA[Left, "Index", "]]><xsl:value-of select="TableTK1"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if> 
  <xsl:if test="TableTK2 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK2"/><![CDATA[Left, "Index", "]]><xsl:value-of select="TableTK2"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
  <xsl:if test="TableFK4 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisFK4"/><![CDATA[Left, "Index", "]]><xsl:value-of select="TableFK4"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if> 
  <xsl:if test="TableFK5 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisFK5"/><![CDATA[Left, "Index", "]]><xsl:value-of select="TableFK5"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if>        
</xsl:when>
<xsl:otherwise> 
  <xsl:if test="TableTK1 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK1"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK1"/><![CDATA[")  |     
            </td>     ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose>
<xsl:choose>
<xsl:when test="Table ='ActionReference+++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">     
<![CDATA[           <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnkReferenceRight, "Index", "Tbl90References")  |     
            </td>                       
            <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnkReferenceCreateRight, "Create", "Tbl90References")  |     
            </td>           ]]>   
<![CDATA[           <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnkRefExpertRight, "Index", "Tbl90RefExperts")  |     
            </td>                       
            <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnkRefExpertCreateRight, "Create", "Tbl90RefExperts")  |     
            </td>           ]]>   
<![CDATA[           <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnkRefSourceRight, "Index", "Tbl90RefSources")  |     
            </td>                       
            <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnkRefSourceCreateRight, "Create", "Tbl90RefSources")  |     
            </td>           ]]>   
</xsl:when>
<xsl:when test="Table ='Tbl90References'">     
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">     
<![CDATA[           <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnkReferenceRight, "Index", "Tbl90References")  |     
            </td>                       
            <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnkReferenceCreateRight, "Create", "Tbl90References")  |     
            </td>           ]]>   
<![CDATA[           <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnkRefAuthorRight, "Index", "Tbl90RefAuthors")  |     
            </td>                       
            <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnkRefAuthorCreateRight, "Create", "Tbl90RefAuthors")  |     
            </td>           ]]>   
<![CDATA[           <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnkRefSourceRight, "Index", "Tbl90RefSources")  |     
            </td>                       
            <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnkRefSourceCreateRight, "Create", "Tbl90RefSources")  |     
            </td>           ]]>   
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">     
<![CDATA[           <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnkReferenceRight, "Index", "Tbl90References")  |     
            </td>                       
            <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnkReferenceCreateRight, "Create", "Tbl90References")  |     
            </td>           ]]>   
<![CDATA[           <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnkRefAuthorRight, "Index", "Tbl90RefAuthors")  |     
            </td>                       
            <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnkRefAuthorCreateRight, "Create", "Tbl90RefAuthors")  |     
            </td>           ]]>   
<![CDATA[           <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnkRefExpertRight, "Index", "Tbl90RefExperts")  |     
            </td>                       
            <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnkRefExpertCreateRight, "Create", "Tbl90RefExperts")  |     
            </td>           ]]>   
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">     
</xsl:when>
<xsl:when test="Table ='TblCounters'">     
</xsl:when>
<xsl:otherwise>  
<![CDATA[           <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnkReferenceRight, "Index", "Tbl90References")  |     
            </td>                       
            <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnkReferenceCreateRight, "Create", "Tbl90References")  |     
            </td>           ]]>   
</xsl:otherwise>    
</xsl:choose> 
<xsl:choose>
<xsl:when test="Table ='Action Comment++++++++++++++++++'">        
</xsl:when>
<xsl:otherwise> 
<![CDATA[           <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnkCommentRight, "Index", "Tbl93Comments")  |     
            </td>                       
            <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnkCommentCreateRight, "Create", "Tbl93Comments")  |     
            </td>           ]]>    
</xsl:otherwise>    
</xsl:choose> 

 <![CDATA[ 
            <td>    
                @Html.ActionLink(SharedRes.StringsRes.ActionLnkCreate, "Create", null, new { style = "font-weight:bold" })    
            </td>
        </tr> 
    </table>
</div>

  ]]> 


</xsl:template>
</xsl:stylesheet>










