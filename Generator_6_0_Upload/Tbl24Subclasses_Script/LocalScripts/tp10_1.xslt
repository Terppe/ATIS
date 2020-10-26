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
    ViewBag.Title = ]]> <xsl:value-of select="Table"/><![CDATA[Res.StringsRes.ListOf]]><xsl:value-of select="Table"/><![CDATA[;
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
<xsl:when test="Table ='Tbl03Regnums'">  
        <![CDATA[   <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[</b>
            <input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" />        
            
            <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[</b>
            <input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[Name" name="]]><xsl:value-of select="BasisSm"/><![CDATA[Name" />   ]]>        
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">  
        <![CDATA[   <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[</b>
            <input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" />        
            
            <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[</b>
            <input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[Name" name="]]><xsl:value-of select="BasisSm"/><![CDATA[Name" />   ]]>        
</xsl:when>
<xsl:when test="Table ='Tbl18Superclasses'"> 
        <![CDATA[    <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[</b>
            <input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" />
            
            <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[</b>
            <input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[Name" name="]]><xsl:value-of select="BasisSm"/><![CDATA[Name" />           

            <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ </b>
            @Html.DropDownListFor(model => model.]]><xsl:value-of select="IDFK1"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK1"/><![CDATA[List, @SharedRes.StringsRes.DDLAll)                 

            <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK2"/><![CDATA[ </b>
            @Html.DropDownListFor(model => model.]]><xsl:value-of select="IDFK2"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK2"/><![CDATA[List, @SharedRes.StringsRes.DDLAll) ]]>                
</xsl:when>
<xsl:when test="Table ='Tbl78Names'"> 
        <![CDATA[    <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[</b>
            <input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" />
            
            <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[</b>
            <input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[Name" name="]]><xsl:value-of select="BasisSm"/><![CDATA[Name" />           

            <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ </b>
            @Html.DropDownListFor(model => model.]]><xsl:value-of select="IDFK1"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK1"/><![CDATA[List, @SharedRes.StringsRes.DDLAll)                 

            <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK2"/><![CDATA[ </b>
            @Html.DropDownListFor(model => model.]]><xsl:value-of select="IDFK2"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK2"/><![CDATA[List, @SharedRes.StringsRes.DDLAll) ]]>                
</xsl:when>
<xsl:when test="Table ='Tbl81Images'"> 
        <![CDATA[    <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[</b>
            <input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" />

            <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ </b>
            @Html.DropDownListFor(model => model.]]><xsl:value-of select="IDFK1"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK1"/><![CDATA[List, @SharedRes.StringsRes.DDLAll)                 

            <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK2"/><![CDATA[ </b>
            @Html.DropDownListFor(model => model.]]><xsl:value-of select="IDFK2"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK2"/><![CDATA[List, @SharedRes.StringsRes.DDLAll) ]]>                
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'"> 
        <![CDATA[    <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[</b>
            <input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" />
            
            <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[</b>
            <input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[Name" name="]]><xsl:value-of select="BasisSm"/><![CDATA[Name" />           

            <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ </b>
            @Html.DropDownListFor(model => model.]]><xsl:value-of select="IDFK1"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK1"/><![CDATA[List, @SharedRes.StringsRes.DDLAll)                 

            <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK2"/><![CDATA[ </b>
            @Html.DropDownListFor(model => model.]]><xsl:value-of select="IDFK2"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK2"/><![CDATA[List, @SharedRes.StringsRes.DDLAll) ]]>                
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
        <![CDATA[    <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[</b>
            <input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" />

            <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ </b>
            @Html.DropDownListFor(model => model.]]><xsl:value-of select="IDFK1"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK1"/><![CDATA[List, @SharedRes.StringsRes.DDLAll)                 

            <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK2"/><![CDATA[ </b>
            @Html.DropDownListFor(model => model.]]><xsl:value-of select="IDFK2"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK2"/><![CDATA[List, @SharedRes.StringsRes.DDLAll) ]]>                
</xsl:when>
<xsl:when test="Table ='Tbl90References'">  
        <![CDATA[    <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[</b>
            <input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" />

            <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ </b>
            @Html.DropDownListFor(model => model.]]><xsl:value-of select="IDFK1"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK1"/><![CDATA[List, @SharedRes.StringsRes.DDLAll)                 

            <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK2"/><![CDATA[ </b>
            @Html.DropDownListFor(model => model.]]><xsl:value-of select="IDFK2"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK2"/><![CDATA[List, @SharedRes.StringsRes.DDLAll) ]]>                
</xsl:when>
<xsl:otherwise> 
        <![CDATA[    <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[</b>
            <input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[id" name="]]><xsl:value-of select="BasisSm"/><![CDATA[id" />
            
            <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[</b>
            <input type="text" id="]]><xsl:value-of select="BasisSm"/><![CDATA[Name" name="]]><xsl:value-of select="BasisSm"/><![CDATA[Name" />           

            <b>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ </b>
            @Html.DropDownListFor(model => model.]]><xsl:value-of select="IDFK1"/><![CDATA[, Model.]]><xsl:value-of select="BasisFK1"/><![CDATA[List, @SharedRes.StringsRes.DDLAll) ]]>                
</xsl:otherwise>    
</xsl:choose>

<![CDATA[           <b>@SharedRes.StringsRes.Valid </b>
            @Html.CheckBoxFor(model => model.Valid) 

            @Html.Hidden("sortBy", Model.SortBy)
            @Html.Hidden("ascending", Model.SortAscending)

            <input type="submit" value="@SharedRes.StringsRes.Search" />
        </div>
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
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="IDFK1"/><![CDATA[" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ } })</th>                
        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="Name"/><![CDATA[" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ } })</th>                   
        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Subspecies" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Subspecies } })</th>                    
        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Divers" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Divers } })</th>    ]]>                
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">  
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="IDFK1"/><![CDATA[" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ } })</th>                
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
<xsl:otherwise> 
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="Name"/><![CDATA[" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[ } })</th>    ]]>                
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='GridHeader Author+Language+++++++++++++'">        
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
<xsl:when test="Table ='Tbl90References'">  
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Expert" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Expert } })</th>   ]]>
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Source" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Source } })</th>   ]]>
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "SourceYear" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.SourceYear } })</th>   ]]>
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Author" }, { "DisplayName", SharedRes.StringsRes.Author } })</th>   ]]>
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "PublicationYear" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.PublicationYear } })</th>   ]]>
</xsl:when>
<xsl:otherwise> 
<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Author" }, { "DisplayName", SharedRes.StringsRes.Author } })</th>
        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "AuthorYear" }, { "DisplayName", SharedRes.StringsRes.AuthorYear } })</th>  ]]>
</xsl:otherwise>    
</xsl:choose>

<![CDATA[        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "Valid" }, { "DisplayName", SharedRes.StringsRes.Valid } })</th>
        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "ValidYear" }, { "DisplayName", SharedRes.StringsRes.ValidYear } })</th>  ]]>
<xsl:choose>
<xsl:when test="Table ='GridHeader+++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">  
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">  
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="IDFK1"/><![CDATA[" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ } })</th>
        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="TableFK1"/><![CDATA[.Subregnum" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Subregnum } })</th>]]>                
</xsl:when>
<xsl:when test="Table ='Tbl18Superclasses'">  
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="IDFK1"/><![CDATA[" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ } })</th>
        <th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="IDFK2"/><![CDATA[" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK2"/><![CDATA[ } })</th> ]]>                
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
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "ImageData" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.ImageData } })</th> ]]>                
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="IDFK1"/><![CDATA[" }, { "DisplayName", SharedRes.StringsRes.FiPl } })</th> ]]>                
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">  
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="IDFK1"/><![CDATA[" }, { "DisplayName", SharedRes.StringsRes.FiPl } })</th> ]]>                
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="IDFK1"/><![CDATA[" }, { "DisplayName", SharedRes.StringsRes.FiPl } })</th> ]]>                
</xsl:when>
<xsl:when test="Table ='Tbl90References'">  
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="IDFK1"/><![CDATA[" }, { "DisplayName", SharedRes.StringsRes.FiPl } })</th> ]]>                
</xsl:when>
<xsl:otherwise> 
        <![CDATA[<th>@Html.Partial("_SmartLink", Model, new ViewDataDictionary { { "ColumnName", "]]><xsl:value-of select="IDFK1"/><![CDATA[" }, { "DisplayName", ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[ } })</th> ]]>                
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
<xsl:when test="Table ='Tbl03Regnums'">  
        <![CDATA[<td class="left">@item.]]><xsl:value-of select="Name"/><![CDATA[ </td>                   
        <td class="left">@item.Subregnum </td>   ]]>
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
<xsl:otherwise> 
        <![CDATA[<td class="left">@item.]]><xsl:value-of select="Name"/><![CDATA[ </td>    ]]>                
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Author+language++++++++++++'">        
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
<xsl:when test="Table ='Tbl90References'">  
</xsl:when>
<xsl:otherwise> 
        <![CDATA[<td class="left">@item.Author</td>       
        <td class="left">@item.AuthorYear</td>   ]]>     
</xsl:otherwise>    
</xsl:choose>

<![CDATA[ 
        <td class="right">@if (item.Valid == true) { 
                <img src="@Url.Content("~/Content/Images/Ok.png")" alt="@SharedRes.StringsRes.ValidYes" title="@SharedRes.StringsRes.ValidYes" />   } 
            else { 
                <img src="@Url.Content("~/Content/Images/cancel.png")" alt="@SharedRes.StringsRes.ValidNo" title="@SharedRes.StringsRes.ValidNo" />    } 
        </td>
        <td class="left">@item.ValidYear</td>   ]]>
<xsl:choose>
<xsl:when test="Table ='Grid+++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">  
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">  
        <![CDATA[<td class="left">@item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[  </td>
        <td class="left">@item.]]><xsl:value-of select="TableFK1"/><![CDATA[.Subregnum </td>   ]]>                
</xsl:when>
<xsl:when test="Table ='Tbl18Superclasses'">  
        <![CDATA[<td class="left">@item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[  </td>
        <td class="left">@item.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="NameFK2"/><![CDATA[  </td> ]]>                
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
        <td class="middel"><img src="@Url.Action("GetImage", "]]><xsl:value-of select="Table"/><![CDATA[", new {id = item.]]><xsl:value-of select="ID"/><![CDATA[ })"  alt="@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.ImageData" height="100" width="100" />    </td>             
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
<xsl:when test="Table ='Tbl90References'">  
        <![CDATA[
        @if (! item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[.Contains("#")) { 
        <td class="left">@item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ @item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ @item.]]><xsl:value-of select="TableFK1"/><![CDATA[.Subspecies @item.]]><xsl:value-of select="TableFK1"/><![CDATA[.Divers   </td>   }
            else  { 
        <td class="left">@item.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ @item.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="NameFK2"/><![CDATA[ @item.]]><xsl:value-of select="TableFK2"/><![CDATA[.Subspecies @item.]]><xsl:value-of select="TableFK2"/><![CDATA[.Divers   </td>    }    ]]>               
</xsl:when>
<xsl:otherwise> 
        <![CDATA[<td class="left">@item.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[  </td> ]]>                
</xsl:otherwise>    
</xsl:choose>
<![CDATA[     </tr>    
    }     
    <tr>    
        <td class="pager" colspan="9">   
            @Html.Partial("_Pager", Model)
        </td>
    </tr>
        
</table>

<div id="links">


    <table> 
        <tr>     ]]>
<xsl:choose>
<xsl:when test="Table ='Buttons+++++++++++'">        
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
<xsl:if test="TableTK3 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK3"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK3"/><![CDATA[")  |     
            </td>     ]]> 
</xsl:if>   
<xsl:if test="TableTK4 !='NULL'">
    <![CDATA[        <td>
                 @Html.ActionLink(SharedRes.StringsRes.ActionLnk]]><xsl:value-of select="BasisTK4"/><![CDATA[Right, "Index", "]]><xsl:value-of select="TableTK4"/><![CDATA[")  |     
            </td>     ]]> 
</xsl:if>   
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










