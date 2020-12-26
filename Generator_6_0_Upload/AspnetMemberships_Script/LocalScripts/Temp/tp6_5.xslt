<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[@model Atis.Domain.Models.]]><xsl:value-of select="LinqModel"/><![CDATA[      ]]>

<xsl:if test="Table='Tbl87Geographics'">
    <![CDATA[    @using System.Globalization      ]]>
</xsl:if> 

 <![CDATA[       <!-- Details Skriptdatum: ]]> <xsl:value-of select="DateTime"/>  <![CDATA[  -->   

@{
    ViewBag.Title = ]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Details;
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Details</h2>

    <div id="pagination">
        @Html.ActionLink(SharedRes.StringsRes.ActionLnkBack, "Index", null, new { style = "font-weight:bold" })
    </div>

<fieldset><legend>@SharedRes.StringsRes.Fields </legend>   

]]>

<xsl:if test="Table='Tbl87Geographics'">  
    <![CDATA[    <div id="mapDiv">    
              @Html.Partial("_Map"); 
        </div> ]]>
</xsl:if>   

<xsl:choose>
<xsl:when test="Table ='ID+CountID+++++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">     
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[  :</strong>
            @Model.]]><xsl:value-of select="ID"/><![CDATA[ 
        </p>     ]]>    
</xsl:when>
<xsl:when test="Table ='aspnet_Memberships'">     
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[  :</strong>
            @Model.]]><xsl:value-of select="ID"/><![CDATA[ 
        </p>     ]]>    
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">     
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[  :</strong>
            @Model.]]><xsl:value-of select="ID"/><![CDATA[ 
        </p>     ]]>    
</xsl:when>
<xsl:otherwise> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="ID"/><![CDATA[  :</strong>
            @Model.]]><xsl:value-of select="ID"/><![CDATA[ 
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@SharedRes.StringsRes.CountID  :</strong>
            @Model.CountID            
        </p>     ]]> 
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Name+Zusatzname oder Edit oder FK+++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Memberships'">   
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">   
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[  :</strong>
            @Model.]]><xsl:value-of select="Name"/><![CDATA[ 
            @Model.Subregnum 
        </p>    ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">   
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[  :</strong>
            @Model.]]><xsl:value-of select="Name"/><![CDATA[ 
            @Model.Subspeciesgroup 
        </p>    ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">   
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="IDFK1"/><![CDATA[  :</strong>      
            @Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[  
        </p>    ]]>
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[  :</strong>
            @Model.]]><xsl:value-of select="Name"/><![CDATA[ 
            @Model.Subspecies 
            @Model.Divers 
        </p>    ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="IDFK1"/><![CDATA[  :</strong>      
            @Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[  
        </p>    ]]>
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[  :</strong>
            @Model.]]><xsl:value-of select="Name"/><![CDATA[ 
            @Model.Subspecies 
            @Model.Divers 
        </p>    ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">   
    <![CDATA[    @if (!@Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ .Contains("#")) { ]]>
  <xsl:if test="TableFK1 !='NULL'">  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="IDFK1"/><![CDATA[  :</strong>      
            @Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ 
            @Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[  
            @Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.Subspecies  
            @Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.Divers  
        </p>    ]]>
  </xsl:if>  
    <![CDATA[    }
        else  { ]]>
  <xsl:if test="TableFK2 !='NULL'">  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="TableFK2"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="IDFK2"/><![CDATA[  :</strong>      
            @Model.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[  
            @Model.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="NameFK2"/><![CDATA[  
            @Model.]]><xsl:value-of select="TableFK2"/><![CDATA[.Subspecies  
            @Model.]]><xsl:value-of select="TableFK2"/><![CDATA[.Divers  
        </p>    ]]>
  </xsl:if>  
    <![CDATA[    } ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">   
    <![CDATA[    @if (!@Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ .Contains("#")) { ]]>
  <xsl:if test="TableFK1 !='NULL'">  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="IDFK1"/><![CDATA[  :</strong>      
            @Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ 
            @Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[  
            @Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.Subspecies  
            @Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.Divers  
        </p>    ]]>
  </xsl:if>  
    <![CDATA[    }
        else  { ]]>
  <xsl:if test="TableFK2 !='NULL'">  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="TableFK2"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="IDFK2"/><![CDATA[  :</strong>      
            @Model.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[  
            @Model.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="NameFK2"/><![CDATA[  
            @Model.]]><xsl:value-of select="TableFK2"/><![CDATA[.Subspecies  
            @Model.]]><xsl:value-of select="TableFK2"/><![CDATA[.Divers  
        </p>    ]]>
  </xsl:if>  
    <![CDATA[    } ]]>
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">   
    <![CDATA[    @if (!@Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ .Contains("#")) { ]]>
  <xsl:if test="TableFK1 !='NULL'">  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="IDFK1"/><![CDATA[  :</strong>      
            @Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ 
            @Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[  
            @Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.Subspecies  
            @Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.Divers  
        </p>    ]]>
  </xsl:if>  
    <![CDATA[    }
        else  { ]]>
  <xsl:if test="TableFK2 !='NULL'">  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="TableFK2"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="IDFK2"/><![CDATA[  :</strong>      
            @Model.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[  
            @Model.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="NameFK2"/><![CDATA[  
            @Model.]]><xsl:value-of select="TableFK2"/><![CDATA[.Subspecies  
            @Model.]]><xsl:value-of select="TableFK2"/><![CDATA[.Divers  
        </p>    ]]>
  </xsl:if>  
    <![CDATA[    } ]]>
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">   
    <![CDATA[    @if (!@Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ .Contains("#")) { ]]>
  <xsl:if test="TableFK1 !='NULL'">  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="IDFK1"/><![CDATA[  :</strong>      
            @Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[ 
            @Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[  
            @Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.Subspecies  
            @Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.Divers  
        </p>    ]]>
  </xsl:if>  
    <![CDATA[    }
        else  { ]]>
  <xsl:if test="TableFK2 !='NULL'">  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="TableFK2"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="IDFK2"/><![CDATA[  :</strong>      
            @Model.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="TableBK1"/><![CDATA[.]]><xsl:value-of select="NameBK1"/><![CDATA[  
            @Model.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="NameFK2"/><![CDATA[  
            @Model.]]><xsl:value-of select="TableFK2"/><![CDATA[.Subspecies  
            @Model.]]><xsl:value-of select="TableFK2"/><![CDATA[.Divers  
        </p>    ]]>
  </xsl:if>  
    <![CDATA[    } ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">   
</xsl:when>
<xsl:when test="Table ='Tbl90References'">   
  <xsl:if test="TableFK1 !='NULL'">  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="BasisFK1"/><![CDATA[  :</strong>      
            @Model.]]><xsl:value-of select="IDFK1"/><![CDATA[ 
        </p>    ]]>
  </xsl:if>  
  <xsl:if test="TableFK2 !='NULL'">  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="BasisFK2"/><![CDATA[  :</strong>      
            @Model.]]><xsl:value-of select="IDFK2"/><![CDATA[  
        </p>    ]]>
  </xsl:if>  
  <xsl:if test="TableFK3 !='NULL'">  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="BasisFK3"/><![CDATA[  :</strong>      
            @Model.]]><xsl:value-of select="IDFK3"/><![CDATA[  
        </p>    ]]>
  </xsl:if>  
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">   
  <xsl:if test="TableFK1 !='NULL'">  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="BasisFK1"/><![CDATA[  :</strong>      
            @Model.]]><xsl:value-of select="IDFK1"/><![CDATA[ 
        </p>    ]]>
  </xsl:if>  
  <xsl:if test="TableFK2 !='NULL'">  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="BasisFK2"/><![CDATA[  :</strong>      
            @Model.]]><xsl:value-of select="IDFK2"/><![CDATA[  
        </p>    ]]>
  </xsl:if>  
  <xsl:if test="TableFK3 !='NULL'">  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="BasisFK3"/><![CDATA[  :</strong>      
            @Model.]]><xsl:value-of select="IDFK3"/><![CDATA[  
        </p>    ]]>
  </xsl:if>  
</xsl:when>
<xsl:otherwise> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="Name"/><![CDATA[  :</strong>
            @Model.]]><xsl:value-of select="Name"/><![CDATA[ 
        </p>    ]]>  
  <xsl:if test="TableFK1 !='NULL'">  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="TableFK1"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[  :</strong>      
            @Model.]]><xsl:value-of select="TableFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ 
        </p>    ]]>
  </xsl:if>  
  <xsl:if test="TableFK2 !='NULL'">  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="TableFK2"/><![CDATA[Res.StringsRes.]]><xsl:value-of select="NameFK2"/><![CDATA[  :</strong>      
            @Model.]]><xsl:value-of select="TableFK2"/><![CDATA[.]]><xsl:value-of select="NameFK2"/><![CDATA[  
        </p>    ]]>
  </xsl:if>  
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Adresse+Language+++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">   
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.LoweredApplicationName  :</strong>
            @Model.LoweredApplicationName            
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Description  :</strong>
            @Model.Description            
        </p>     ]]>   
</xsl:when>
<xsl:when test="Table ='aspnet_Memberships'">   
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Password  :</strong>
            @Model.Password            
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.PasswordFormat  :</strong>
            @Model.PasswordFormat            
        </p>     ]]>   
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.PasswordSalt  :</strong>
            @Model.PasswordSalt           
        </p>     ]]>  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MobilePIN  :</strong>
            @Model.MobilePIN          
        </p>     ]]>  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Email  :</strong>
            @Model.Email          
        </p>     ]]>  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.LoweredEmail  :</strong>
            @Model.LoweredEmail          
        </p>     ]]>  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.PasswordQuestion  :</strong>
            @Model.PasswordQuestion            
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.PasswordAnswer  :</strong>
            @Model.PasswordAnswer            
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.IsApproved  :</strong>
            @Model.IsApproved            
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.IsLockedOut  :</strong>
            @Model.IsLockedOut           
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.CreateDate  :</strong>
            @Model.CreateDate            
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.LastLoginDate  :</strong>
            @Model.LastLoginDate            
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.LastPasswordChangedDate  :</strong>
            @Model.LastPasswordChangedDate            
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.LastLockoutDate  :</strong>
            @Model.LastLockoutDate            
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.FailedPasswordAttemptCount  :</strong>
            @Model.FailedPasswordAttemptCount            
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.FailedPasswordAttemptWindowStart  :</strong>
            @Model.FailedPasswordAttemptWindowStart           
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.FailedPasswordAnswerAttemptCount  :</strong>
            @Model.FailedPasswordAnswerAttemptCount            
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.FailedPasswordAnswerAttemptWindowStart  :</strong>
            @Model.FailedPasswordAnswerAttemptWindowStart           
        </p>     ]]>  
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">   
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.LoweredUserName  :</strong>
            @Model.LoweredUserName            
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MobileAlias  :</strong>
            @Model.MobileAlias            
        </p>     ]]>   
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.IsAnonymous  :</strong>
            @Model.IsAnonymous            
        </p>     ]]>   
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.LastActivityDate  :</strong>
            @Model.LastActivityDate          
        </p>     ]]>   
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Language  :</strong>
            @Model.Language            
        </p>     ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.ShotDate  :</strong>
            @Model.ShotDate             
            @Model.Filestream          
            @Model.FilestreamID           
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Filestream  :</strong>
            <img src="@Url.Action("GetFilestream", "]]><xsl:value-of select="Table"/><![CDATA[", new { id = Model.]]><xsl:value-of select="ID"/><![CDATA[})" alt="@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Filestream"  /> 
        </p>     ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Country  :</strong>
            @Model.Country            
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Address  :</strong>
            @Model.Address             
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Latitude  :</strong>
            @Convert.ToString(Model.Latitude, CultureInfo.InvariantCulture) 
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Longitude  :</strong>
            @Convert.ToString(Model.Longitude, CultureInfo.InvariantCulture) 
         </p>     ]]> 
</xsl:when>
<xsl:otherwise> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Booktitle+Notes++++++++'">        
</xsl:when>     
<xsl:when test="Table ='Tbl90RefAuthors'">     
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.ArticelTitle  :</strong>
            @Model.ArticelTitle             
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.BookName  :</strong>
            @Model.BookName             
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Page  :</strong>
            @Model.Page             
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Publisher  :</strong>
            @Model.Publisher             
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.PublicationPlace  :</strong>
            @Model.PublicationPlace            
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.ISBN  :</strong>
            @Model.ISBN            
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Notes  :</strong>
            @Model.Notes           
        </p>     ]]>    
</xsl:when>     
<xsl:when test="Table ='Tbl90RefExperts'">     
    <![CDATA[    <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Notes  :</strong>
            @Model.Notes           
        </p>     ]]>    
</xsl:when>     
<xsl:otherwise> 
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Valid+++++++++'">        
</xsl:when>   
<xsl:when test="Table ='aspnet_Applications'"> 
</xsl:when>
<xsl:when test="Table ='aspnet_Memberships'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">        
</xsl:when>  
<xsl:otherwise> 
    <![CDATA[    <p>
            <strong>@SharedRes.StringsRes.Valid  :</strong>
            @Html.DisplayFor(m => m.Valid)
            @Model.ValidYear 
        </p>     ]]> 
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Author+++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'"> 
</xsl:when>
<xsl:when test="Table ='aspnet_Memberships'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">        
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">        
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">        
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
<xsl:otherwise> 
    <![CDATA[    <p>
            <strong>@SharedRes.StringsRes.Author  :</strong>
            @Model.Author        
            @Model.AuthorYear             
        </p>     ]]> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Synonym+++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'"> 
</xsl:when>
<xsl:when test="Table ='aspnet_Memberships'">        
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
<xsl:otherwise> 
    <![CDATA[    <p>
            <strong>@SharedRes.StringsRes.Synonym  :</strong>
            @Model.Synonym 
        </p>     ]]> 
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Info+++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'"> 
</xsl:when>
<xsl:when test="Table ='aspnet_Memberships'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">        
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">        
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">        
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">        
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">        
</xsl:when>
<xsl:otherwise> 
    <![CDATA[    <p>
            <strong>@SharedRes.StringsRes.Info  :</strong>
            @Model.Info            
        </p>     ]]> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='EngName uso+++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'"> 
</xsl:when>
<xsl:when test="Table ='aspnet_Memberships'">        
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
<xsl:otherwise> 
    <![CDATA[    <p>
            <strong>@SharedRes.StringsRes.EngName  :</strong>
            @Model.EngName             
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@SharedRes.StringsRes.GerName  :</strong>
            @Model.GerName             
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@SharedRes.StringsRes.FraName  :</strong>
            @Model.FraName            
        </p>     ]]> 
    <![CDATA[    <p>
            <strong>@SharedRes.StringsRes.PorName  :</strong>
            @Model.PorName             
        </p>     ]]> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Memo+++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'"> 
</xsl:when>
<xsl:when test="Table ='aspnet_Memberships'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">        
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">        
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">        
</xsl:when>
<xsl:otherwise> 
    <![CDATA[    <p>
            <strong>@SharedRes.StringsRes.Memo  :</strong>
            @Model.Memo            
        </p>     ]]> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='FiSpecies+PlSpecies+++++++++++'">     
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">
     <![CDATA[        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoSpecies  :</strong>
            @Model.MemoSpecies             
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.TradeName  :</strong>
            @Model.TradeName            
        </p> 
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Importer  :</strong>
            @Model.Importer            
        </p> 
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.ImportingYear  :</strong>
            @Model.ImportingYear          
        </p> 
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.TypeSpecies  :</strong>
            @Model.TypeSpecies            
        </p> 
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.LNumber  :</strong>
            @Model.LNumber            
        </p> 
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.LOrigin  :</strong>
            @Model.LOrigin            
        </p> 
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.LDANumber  :</strong>
            @Model.LDANumber             
        </p> 
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.LDAOrigin  :</strong>
            @Model.LDAOrigin             
        </p> 
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.BasinLength  :</strong>
            @Model.BasinLength  cm           
        </p> 
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.FishLength  :</strong>
            @Model.FishLength  cm            
        </p> 
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Karnivore  :</strong>
            @Model.Karnivore 
        </p>   
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Herbivore  :</strong>
            @Model.Herbivore 
        </p>   
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Limnivore  :</strong>
            @Model.Limnivore 
        </p>   
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Omnivore  :</strong>
            @Model.Omnivore 
        </p>   
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoFoods  :</strong>
            @Model.MemoFoods             
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Difficult1  :</strong>
            @Model.Difficult1 
        </p>   
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Difficult2  :</strong>
            @Model.Difficult2 
        </p>   
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Difficult3  :</strong>
            @Model.Difficult3 
        </p>   
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Difficult4  :</strong>
            @Model.Difficult4
        </p>   
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.RegionTop  :</strong>
            @Model.RegionTop 
        </p>   
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.RegionMiddle  :</strong>
            @Model.RegionMiddle 
        </p>   
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.RegionBottom  :</strong>
            @Model.RegionBottom 
        </p>   
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoRegion  :</strong>
            @Model.MemoRegion             
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoTech  :</strong>
            @Model.MemoTech          
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Ph1  :</strong>
            @Model.Ph1             
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Ph2  :</strong>
            @Model.Ph2            
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Temp1  :</strong>
            @Model.Temp1  °C            
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Temp2  :</strong>
            @Model.Temp2  °C            
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Hardness1  :</strong>
            @Model.Hardness1  dGH            
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Hardness2  :</strong>
            @Model.Hardness2  dGH            
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.CarboHardness1  :</strong>
            @Model.CarboHardness1  KH            
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.CarboHardness2  :</strong>
            @Model.CarboHardness2  KH            
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoHusbandry  :</strong>
            @Model.MemoHusbandry             
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoBreeding  :</strong>
            @Model.MemoBreeding            
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoBuilt  :</strong>
            @Model.MemoBuilt          
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoColor  :</strong>
            @Model.MemoColor             
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoSozial  :</strong>
            @Model.MemoSozial             
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoDomorphism  :</strong>
            @Model.MemoDomorphism             
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoSpecial  :</strong>
            @Model.MemoSpecial             
        </p>         ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">
     <![CDATA[        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoSpecies  :</strong>
            @Model.MemoSpecies             
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.TradeName  :</strong>
            @Model.TradeName            
        </p> 
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Importer  :</strong>
            @Model.Importer             
        </p> 
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.ImportingYear  :</strong>
            @Model.ImportingYear             
        </p> 
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.BasinHeight  :</strong>
            @Model.BasinHeight  cm           
        </p> 
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.PlantLength  :</strong>
            @Model.PlantLength  cm            
        </p> 
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Difficult1  :</strong>
            @Model.Difficult1 
        </p>   
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Difficult2  :</strong>
            @Model.Difficult2 
        </p>   
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Difficult3  :</strong>
            @Model.Difficult3 
        </p>   
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Difficult4  :</strong>
            @Model.Difficult4 
        </p>   
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoTech  :</strong>
            @Model.MemoTech             
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Ph1  :</strong>
            @Model.Ph1            
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Ph2  :</strong>
            @Model.Ph2             
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Temp1  :</strong>
            @Model.Temp1  °C            
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Temp2  :</strong>
            @Model.Temp2  °C            
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Hardness1  :</strong>
            @Model.Hardness1  dGH            
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.Hardness2  :</strong>
            @Model.Hardness2  dGH            
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.CarboHardness1  :</strong>
            @Model.CarboHardness1  KH            
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.CarboHardness2  :</strong>
            @Model.CarboHardness2  KH            
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoCulture  :</strong>
            @Model.MemoCulture             
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoBuilt  :</strong>
            @Model.MemoBuilt             
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoColor  :</strong>
            @Model.MemoColor            
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoReproduction  :</strong>
            @Model.MemoReproduction             
        </p>      
        <p>
            <strong>@]]><xsl:value-of select="Table"/><![CDATA[Res.StringsRes.MemoGlobal  :</strong>
            @Model.MemoGlobal             
        </p>         ]]>  
</xsl:when>
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose>   

<xsl:choose>
<xsl:when test="Table ='Writer+Updater+++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'"> 
</xsl:when>
<xsl:when test="Table ='aspnet_Memberships'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">        
</xsl:when>
<xsl:otherwise> 
     <![CDATA[    <p>
            <strong>@SharedRes.StringsRes.Writer  :</strong>
            @Model.Writer  |
            @Model.WriterDate.ToShortDateString() 
            <strong>@@</strong>
            @Model.WriterDate.ToShortTimeString() 
        </p>  ]]>
    <![CDATA[    <p>        
            <strong>@SharedRes.StringsRes.Updater  :</strong>
            @Model.Updater  |
            @Model.UpdaterDate.ToShortDateString() 
            <strong>@@</strong>
            @Model.UpdaterDate.ToShortTimeString() 
        </p>  ]]>
</xsl:otherwise>    
</xsl:choose>   
<![CDATA[        <p></p>
</fieldset>
<p>
    @Html.ActionLink(SharedRes.StringsRes.ActionLnkEdit, "Edit", new { id = Model.]]><xsl:value-of select="ID"/><![CDATA[ }) |
    @Html.ActionLink(SharedRes.StringsRes.ActionLnkBack, "Index")
</p>
    <div class="pagination"></div>

   ]]> 
  

</xsl:template>
</xsl:stylesheet>











