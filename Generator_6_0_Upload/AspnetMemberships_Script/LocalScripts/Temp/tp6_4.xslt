<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[@model Atis.Domain.Models.]]><xsl:value-of select="LinqModel"/><![CDATA[    	

       <!-- Delete  Skriptdatum: ]]> <xsl:value-of select="DateTime"/>  <![CDATA[  -->        

@{
    ViewBag.Title = ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Delete;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Delete</h2>

    <div>
        <p>@SharedRes.StringsRes.DeleteMessage      ]]> 
<xsl:choose>
<xsl:when test="Table ='aspnet_Memberships'">  
<![CDATA[             <i>@Model.UserId / @Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="NameFK2"/><![CDATA[ ?</i> ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
<![CDATA[             <i>@Model.]]><xsl:value-of select="ID"/><![CDATA[  ?</i>   ]]>      
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
<![CDATA[             <i>@Model.]]><xsl:value-of select="ID"/><![CDATA[  ?</i>   ]]>      
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">  
<![CDATA[             <i>@Model.]]><xsl:value-of select="ID"/><![CDATA[  /  @Model.]]><xsl:value-of select="Name"/><![CDATA[  ?</i>   ]]>      
</xsl:when>
<xsl:when test="Table ='Tbl90References'">  

<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK27"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK27"/><![CDATA[.]]><xsl:value-of select="NameFK27"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK26"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK26"/><![CDATA[.]]><xsl:value-of select="NameFK26"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK25"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK25"/><![CDATA[.]]><xsl:value-of select="NameFK25"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK24"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK24"/><![CDATA[.]]><xsl:value-of select="NameFK24"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK23"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK23"/><![CDATA[.]]><xsl:value-of select="NameFK23"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK22"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK22"/><![CDATA[.]]><xsl:value-of select="NameFK22"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK21"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK21"/><![CDATA[.]]><xsl:value-of select="NameFK21"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK20"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK20"/><![CDATA[.]]><xsl:value-of select="NameFK20"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK19"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK19"/><![CDATA[.]]><xsl:value-of select="NameFK19"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK18"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK18"/><![CDATA[.]]><xsl:value-of select="NameFK18"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK17"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK17"/><![CDATA[.]]><xsl:value-of select="NameFK17"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK16"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK16"/><![CDATA[.]]><xsl:value-of select="NameFK16"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK15"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK15"/><![CDATA[.]]><xsl:value-of select="NameFK15"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK14"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK14"/><![CDATA[.]]><xsl:value-of select="NameFK14"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK13"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK13"/><![CDATA[.]]><xsl:value-of select="NameFK13"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK12"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK12"/><![CDATA[.]]><xsl:value-of select="NameFK12"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK11"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK11"/><![CDATA[.]]><xsl:value-of select="NameFK11"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK10"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK10"/><![CDATA[.]]><xsl:value-of select="NameFK10"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK9"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK9"/><![CDATA[.]]><xsl:value-of select="NameFK9"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK8"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK8"/><![CDATA[.]]><xsl:value-of select="NameFK8"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK7"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK7"/><![CDATA[.]]><xsl:value-of select="NameFK7"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK6"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK6"/><![CDATA[.]]><xsl:value-of select="NameFK6"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK4"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="TableFK6"/><![CDATA[.]]><xsl:value-of select="NameFK6"/><![CDATA[ @Model.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="NameFK4"/><![CDATA[ @Model.]]><xsl:value-of select="TableFK4"/><![CDATA[.Subspecies @Model.]]><xsl:value-of select="TableFK4"/><![CDATA[.Divers ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK5"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="TableFK6"/><![CDATA[.]]><xsl:value-of select="NameFK6"/><![CDATA[ @Model.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="NameFK5"/><![CDATA[ @Model.]]><xsl:value-of select="TableFK5"/><![CDATA[.Subspecies @Model.]]><xsl:value-of select="TableFK5"/><![CDATA[.Divers ?</p>}  ]]>

</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">  
<![CDATA[             <i>@Model.]]><xsl:value-of select="ID"/><![CDATA[  /  @Model.]]><xsl:value-of select="Name"/><![CDATA[  ?</i>   ]]>      
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">  
<![CDATA[             <i>@Model.]]><xsl:value-of select="ID"/><![CDATA[  /  @Model.]]><xsl:value-of select="Name"/><![CDATA[  ?</i>   ]]>      
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">  
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK27"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK27"/><![CDATA[.]]><xsl:value-of select="NameFK27"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK26"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK26"/><![CDATA[.]]><xsl:value-of select="NameFK26"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK25"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK25"/><![CDATA[.]]><xsl:value-of select="NameFK25"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK24"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK24"/><![CDATA[.]]><xsl:value-of select="NameFK24"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK23"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK23"/><![CDATA[.]]><xsl:value-of select="NameFK23"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK22"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK22"/><![CDATA[.]]><xsl:value-of select="NameFK22"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK21"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK21"/><![CDATA[.]]><xsl:value-of select="NameFK21"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK20"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK20"/><![CDATA[.]]><xsl:value-of select="NameFK20"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK19"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK19"/><![CDATA[.]]><xsl:value-of select="NameFK19"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK18"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK18"/><![CDATA[.]]><xsl:value-of select="NameFK18"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK17"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK17"/><![CDATA[.]]><xsl:value-of select="NameFK17"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK16"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK16"/><![CDATA[.]]><xsl:value-of select="NameFK16"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK15"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK15"/><![CDATA[.]]><xsl:value-of select="NameFK15"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK14"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK14"/><![CDATA[.]]><xsl:value-of select="NameFK14"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK13"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK13"/><![CDATA[.]]><xsl:value-of select="NameFK13"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK12"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK12"/><![CDATA[.]]><xsl:value-of select="NameFK12"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK11"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK11"/><![CDATA[.]]><xsl:value-of select="NameFK11"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK10"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK10"/><![CDATA[.]]><xsl:value-of select="NameFK10"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK9"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK9"/><![CDATA[.]]><xsl:value-of select="NameFK9"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK8"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK8"/><![CDATA[.]]><xsl:value-of select="NameFK8"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK7"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK7"/><![CDATA[.]]><xsl:value-of select="NameFK7"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK6"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK6"/><![CDATA[.]]><xsl:value-of select="NameFK6"/><![CDATA[ ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK4"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="TableFK6"/><![CDATA[.]]><xsl:value-of select="NameFK6"/><![CDATA[ @Model.]]><xsl:value-of select="TableFK4"/><![CDATA[.]]><xsl:value-of select="NameFK4"/><![CDATA[ @Model.]]><xsl:value-of select="TableFK4"/><![CDATA[.Subspecies @Model.]]><xsl:value-of select="TableFK4"/><![CDATA[.Divers ?</p>}  ]]>
<![CDATA[            @if (Model.]]><xsl:value-of select="IDFK5"/><![CDATA[ != null)
                {<p>@Model.]]><xsl:value-of select="ID"/><![CDATA[ / @Model.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="TableFK6"/><![CDATA[.]]><xsl:value-of select="NameFK6"/><![CDATA[ @Model.]]><xsl:value-of select="TableFK5"/><![CDATA[.]]><xsl:value-of select="NameFK5"/><![CDATA[ @Model.]]><xsl:value-of select="TableFK5"/><![CDATA[.Subspecies @Model.]]><xsl:value-of select="TableFK5"/><![CDATA[.Divers ?</p>}  ]]>
</xsl:when>
<xsl:otherwise>  
<![CDATA[             <i>@Model.]]><xsl:value-of select="Name"/><![CDATA[  ?</i>   ]]>
</xsl:otherwise>    
</xsl:choose> 
  <![CDATA[       </p>     	   
    </div>
@using (Html.BeginForm()) {
    <p>
            <input name="confirmButton" type="submit" value="@SharedRes.StringsRes.ButtonDelete" /> 
        @Html.ActionLink(SharedRes.StringsRes.ActionLnkBack, "Index")

    </p>
}

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



















