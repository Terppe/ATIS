<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[using System;
using System.Data.Linq.Mapping;
using System.Web.Mvc;
using Atis.Domain.Helpers;      
using System.ComponentModel.DataAnnotations;    ]]>

<![CDATA[// <!-- Entities Skriptdatum: ]]> <xsl:value-of select="DateTime"/>  <![CDATA[  -->  

namespace ]]><xsl:value-of select="Namespace"/>.Domain.Models <![CDATA[    {  
    [MetadataType(typeof(]]><xsl:value-of select="LinqModel"/><![CDATA[Validation))]
    public partial class ]]><xsl:value-of select="LinqModel"/><![CDATA[
    {   ]]>
<xsl:if test="Table='Tbl87Geographics'">    <![CDATA[
        /* public bool IsHostedBy(string userName)   {
     return String.Equals(HostedById ?? HostedBy, userName, StringComparison.Ordinal);
 }

 public bool IsUserRegistered(string userName)   {
     return RSVPs.Any(r => r.AttendeeNameId == userName || (r.AttendeeNameId == null && r.AttendeeName == userName));
 }

 [UIHint("LocationDetail")]
 public LocationDetail Location   {
     get   {
         return new LocationDetail() { Latitude = this.Latitude, Longitude = this.Longitude, Title = this.Title, Address = this.Address };
     }
     set   {
         this.Latitude = value.Latitude;
         this.Longitude = value.Longitude;
         this.Title = value.Title;
         this.Address = value.Address;
     }
 }
 */
  ]]>                                                                                                                                                                                                                                                                                       
</xsl:if>   

    }     <![CDATA[

    public class ]]><xsl:value-of select="LinqModel"/><![CDATA[Validation    {               ]]>
  <![CDATA[      [HiddenInput(DisplayValue = false)]   
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]  ]]> 
<xsl:choose>
<xsl:when test="Table ='GUID++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">      
<![CDATA[        public Guid ]]><xsl:value-of select="ID"/><![CDATA[ { get; set; }     ]]>
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">      
<![CDATA[        public Guid ]]><xsl:value-of select="ID"/><![CDATA[ { get; set; }     ]]>
</xsl:when>
<xsl:otherwise>  
<![CDATA[        public int ]]><xsl:value-of select="ID"/><![CDATA[ { get; set; }   ]]>
</xsl:otherwise>    
</xsl:choose>        
<xsl:choose>
<xsl:when test="Table ='Name+++++++++++++++++++++++++++++++'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  <![CDATA[
        [Column]
        public byte[] ImageData { get; set; }                 

        [ScaffoldColumn(false)]
        [Column]
        public string ImageMimeType { get; set; }   ]]>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90References'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCounters'"> 
</xsl:when>
<xsl:otherwise>     <![CDATA[ 
        [Required(ErrorMessageResourceName = "Required]]><xsl:value-of select="Name"/><![CDATA[", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [LocalizedDisplayName("Name]]><xsl:value-of select="LinqModel"/><![CDATA[", NameResourceType = typeof(SharedRes.StringsRes))]
        [StringLength(100, ErrorMessageResourceName = "StringLength_100", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string ]]><xsl:value-of select="Name"/><![CDATA[ { get; set; }   ]]>
</xsl:otherwise>    
</xsl:choose>  
 
<xsl:choose>
<xsl:when test="Table ='aspnet_Applications'">  
<![CDATA[        [StringLength(256, ErrorMessageResourceName = "LoweredApplicationNameStringLength_256", ErrorMessageResourceType = typeof(StringsRes))]
        [Column]
        public string LoweredApplicationName { get; set; }   
                                                                                                                                                                                                                                                                                       
        [StringLength(256, ErrorMessageResourceName = "DescriptionStringLength_256", ErrorMessageResourceType = typeof(StringsRes))]
        [Column]
        public string Description { get; set; }   ]]>                                                                                                                                                                                                                                                                                       
</xsl:when>
<xsl:when test="Table ='aspnet_Membership'">  
<![CDATA[        [StringLength(256, ErrorMessageResourceName = "LoweredApplicationNameStringLength_256", ErrorMessageResourceType = typeof(StringsRes))]
        [Column]
        public string LoweredApplicationName { get; set; }   
                                                                                                                                                                                                                                                                                       
        [StringLength(256, ErrorMessageResourceName = "DescriptionStringLength_256", ErrorMessageResourceType = typeof(StringsRes))]
        [Column]
        public string Description { get; set; }   ]]>                                                                                                                                                                                                                                                                                       
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">  
<![CDATA[        [StringLength(256, ErrorMessageResourceName = "LoweredUserNameStringLength_256", ErrorMessageResourceType = typeof(StringsRes))]
        [Column]
        public string LoweredUserName { get; set; }   
                                                                                                                                                                                                                                                                                       
        [StringLength(16, ErrorMessageResourceName = "MobileAliasStringLength_16", ErrorMessageResourceType = typeof(StringsRes))]
        [Column]
        public string MobileAlias { get; set; }    

        [Column]
        public DateTime LastActivityDate { get; set; }     
          
        [Column]
        public bool IsAnonymous { get; set; }      ]]>                                                                                                                                                                                                                                                                         
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">  
<![CDATA[        [StringLength(100, ErrorMessageResourceName = "StringLength_100", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string Subregnum { get; set; }   ]]>                                                                                                                                                                                                                                                                                             
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">  
<![CDATA[        [StringLength(100, ErrorMessageResourceName = "StringLength_100", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string Subspeciesgroup { get; set; }   ]]>                                                                                                                                                                                                                                                                                       
</xsl:when>
<xsl:when test="Table ='TblCounters'">  
<![CDATA[        [Column]
        public string ]]><xsl:value-of select="Name"/><![CDATA[ { get; set; }   ]]>
</xsl:when>
<xsl:otherwise>  
</xsl:otherwise>    
</xsl:choose> 
    <xsl:for-each select="//Fields/Field"><xsl:choose><xsl:when test="Name='CountID'">  <![CDATA[
        [HiddenInput(DisplayValue = false)]
        [Required(ErrorMessageResourceName = "RequiredCountID", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public int CountID { get; set; }   ]]>
    </xsl:when></xsl:choose></xsl:for-each>     
    <xsl:for-each select="//Fields/Field"><xsl:choose><xsl:when test="Name='Valid'">  <![CDATA[
        [Column]
        public bool? Valid { get; set; }    ]]>
    </xsl:when></xsl:choose></xsl:for-each>     
    <xsl:for-each select="//Fields/Field"><xsl:choose><xsl:when test="Name='ValidYear'">  <![CDATA[
        [StringLength(4, MinimumLength=4, ErrorMessageResourceName = "StringLengthYear_4", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string ValidYear { get; set; }    ]]>
    </xsl:when></xsl:choose></xsl:for-each>     
    <xsl:for-each select="//Fields/Field"><xsl:choose><xsl:when test="Name='Synonym'">  <![CDATA[
        [DataType(DataType.MultilineText)]
        [Column]
        public string Synonym { get; set; }     ]]>
    </xsl:when></xsl:choose></xsl:for-each>     
    <xsl:for-each select="//Fields/Field"><xsl:choose><xsl:when test="Name='Author'">  <![CDATA[
        [StringLength(100, ErrorMessageResourceName = "StringLength_100", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string Author { get; set; }     ]]>
    </xsl:when></xsl:choose></xsl:for-each>     
    <xsl:for-each select="//Fields/Field"><xsl:choose><xsl:when test="Name='AuthorYear'">  <![CDATA[
        [StringLength(4, MinimumLength=4, ErrorMessageResourceName = "StringLengthYear_4", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string AuthorYear { get; set; }     ]]>
    </xsl:when></xsl:choose></xsl:for-each>     
    <xsl:for-each select="//Fields/Field"><xsl:choose><xsl:when test="Name='Info'">  <![CDATA[
        [StringLength(100, ErrorMessageResourceName = "StringLength_100", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string Info { get; set; }     ]]>
    </xsl:when></xsl:choose></xsl:for-each>     
    <xsl:for-each select="//Fields/Field"><xsl:choose><xsl:when test="Name='EngName'">  <![CDATA[
        [StringLength(200, ErrorMessageResourceName = "StringLength_200", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string EngName { get; set; }     ]]>
    </xsl:when></xsl:choose></xsl:for-each>     
    <xsl:for-each select="//Fields/Field"><xsl:choose><xsl:when test="Name='GerName'">  <![CDATA[
        [StringLength(200, ErrorMessageResourceName = "StringLength_200", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string GerName { get; set; }     ]]>
    </xsl:when></xsl:choose></xsl:for-each>     
    <xsl:for-each select="//Fields/Field"><xsl:choose><xsl:when test="Name='FraName'">  <![CDATA[
        [StringLength(200, ErrorMessageResourceName = "StringLength_200", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string FraName { get; set; }     ]]>
    </xsl:when></xsl:choose></xsl:for-each>     
    <xsl:for-each select="//Fields/Field"><xsl:choose><xsl:when test="Name='PorName'">  <![CDATA[
        [StringLength(200, ErrorMessageResourceName = "StringLength_200", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string PorName { get; set; }     ]]>
    </xsl:when></xsl:choose></xsl:for-each>          
    <xsl:for-each select="//Fields/Field"><xsl:choose><xsl:when test="Name='Writer'">  <![CDATA[
        [HiddenInput(DisplayValue = false)]
        [Column]
        public string Writer { get; set; }     

        [HiddenInput(DisplayValue = false)]
        [Column]
        public DateTime WriterDate { get; set; }     

        [HiddenInput(DisplayValue = false)]
        [Column]
        public string Updater { get; set; }     

        [HiddenInput(DisplayValue = false)]
        [Column]
        public DateTime UpdaterDate { get; set; }     ]]>  
    </xsl:when></xsl:choose></xsl:for-each>           
    <xsl:for-each select="//Fields/Field"><xsl:choose><xsl:when test="Name='Memo'">  <![CDATA[
        [DataType(DataType.MultilineText)]
        [Column]
        public string Memo { get; set; }             ]]>     
    </xsl:when></xsl:choose></xsl:for-each>     
   
<xsl:choose>
<xsl:when test="Table ='GUID++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='aspnet_Applications'">      
    <xsl:if test="TableFK1 !='NULL'"><![CDATA[        
        [Column]
        public Guid ]]><xsl:value-of select="IDFK1"/><![CDATA[ { get; set; }       ]]>   
    </xsl:if> 
    <xsl:if test="TableFK2 !='NULL'"><![CDATA[    
        [Column]
        public Guid ]]><xsl:value-of select="IDFK2"/><![CDATA[ { get; set; }       ]]>   
    </xsl:if> 
</xsl:when>
<xsl:when test="Table ='aspnet_Users'">      
    <xsl:if test="TableFK1 !='NULL'"><![CDATA[        
        [Column]
        public Guid ]]><xsl:value-of select="IDFK1"/><![CDATA[ { get; set; }       ]]>   
    </xsl:if> 
    <xsl:if test="TableFK2 !='NULL'"><![CDATA[    
        [Column]
        public Guid ]]><xsl:value-of select="IDFK2"/><![CDATA[ { get; set; }       ]]>   
    </xsl:if> 
</xsl:when>
<xsl:otherwise>  
    <xsl:if test="TableFK1 !='NULL'"><![CDATA[        
        [Column]
        public int ]]><xsl:value-of select="IDFK1"/><![CDATA[ { get; set; }       ]]>   
    </xsl:if> 
    <xsl:if test="TableFK2 !='NULL'"><![CDATA[    
        [Column]
        public int ]]><xsl:value-of select="IDFK2"/><![CDATA[ { get; set; }       ]]>   
    </xsl:if> 
</xsl:otherwise>    
</xsl:choose>        

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
<xsl:if test="Table='Tbl68Speciesgroups'">
</xsl:if>   
<xsl:if test="Table='Tbl69FiSpeciesses'">     <![CDATA[
        [StringLength(100, ErrorMessageResourceName = "StringLength_100", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string Subspecies { get; set; }   
                                                                                                                                                                                                                                                                                      
        [StringLength(100, ErrorMessageResourceName = "StringLength_100", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string Divers { get; set; } 

        [StringLength(60, ErrorMessageResourceName = "StringLength_60", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string Importer { get; set; }     

        [Column]
        public string ImportingYear { get; set; }     

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoSpecies { get; set; }                

        [StringLength(100, ErrorMessageResourceName = "StringLength_100", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string TradeName { get; set; } 

        [Column]
        public bool TypeSpecies { get; set; }    

        [StringLength(10, ErrorMessageResourceName = "StringLength_10", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string LNumber { get; set; }    
 
        [StringLength(50, ErrorMessageResourceName = "StringLength_50", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string LOrigin { get; set; }     

        [StringLength(10, ErrorMessageResourceName = "StringLength_10", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string LDANumber { get; set; }    
 
        [StringLength(50, ErrorMessageResourceName = "StringLength_50", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string LDAOrigin { get; set; }     

        [Column]
        public int BasinLength { get; set; }   

        [Column]
        public decimal FishLength { get; set; }   

        [Column]
        public bool Karnivore { get; set; }    

        [Column]
        public bool Herbivore { get; set; }    

        [Column]
        public bool Limnivore { get; set; }    

        [Column]
        public bool Omnivore { get; set; }    

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoFoods { get; set; }                

        [Column]
        public bool Difficult1 { get; set; }    

        [Column]
        public bool Difficult2 { get; set; }    

        [Column]
        public bool Difficult3 { get; set; }    

        [Column]
        public bool Difficult4 { get; set; }    

        [Column]
        public bool RegionTop { get; set; }    

        [Column]
        public bool RegionMiddle { get; set; }    

        [Column]
        public bool RegionBottom { get; set; }    

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoRegion { get; set; }                

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoTech { get; set; }                

        [Column]
        public decimal Ph1 { get; set; }   

        [Column]
        public decimal Ph2 { get; set; }   

        [Column]
        public int Temp1 { get; set; }   

        [Column]
        public int Temp2 { get; set; }   

        [Column]
        public int Hardness1 { get; set; }   

        [Column]
        public int Hardness2 { get; set; }   

        [Column]
        public int CarboHardness1 { get; set; }   

        [Column]
        public int CarboHardness2 { get; set; }   

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoHusbandry { get; set; }                

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoBreeding { get; set; }                

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoBuilt { get; set; }                

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoColor { get; set; }     
           
        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoSozial { get; set; }                

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoDomorphism { get; set; }                

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoSpecial { get; set; }                  ]]>                                                                                                                                                                                                                                                                                       
</xsl:if> 
<xsl:if test="Table='Tbl72PlSpeciesses'">     <![CDATA[
        [StringLength(100, ErrorMessageResourceName = "StringLength_100", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string Subspecies { get; set; }   
                                                                                                                                                                                                                                                                                      
        [StringLength(100, ErrorMessageResourceName = "StringLength_100", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string Divers { get; set; } 

        [StringLength(60, ErrorMessageResourceName = "StringLength_60", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string Importer { get; set; }     

        [Column]
        public string ImportingYear { get; set; }     

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoSpecies { get; set; }                

        [StringLength(100, ErrorMessageResourceName = "StringLength_100", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string TradeName { get; set; } 

        [Column]
        public int BasinHeight { get; set; }   

        [Column]
        public decimal PlantLength { get; set; }   

        [Column]
        public bool Difficult1 { get; set; }    

        [Column]
        public bool Difficult2 { get; set; }    

        [Column]
        public bool Difficult3 { get; set; }    

        [Column]
        public bool Difficult4 { get; set; }    

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoTech { get; set; }                

        [Column]
        public decimal Ph1 { get; set; }   

        [Column]
        public decimal Ph2 { get; set; }   

        [Column]
        public int Temp1 { get; set; }   

        [Column]
        public int Temp2 { get; set; }   

        [Column]
        public int Hardness1 { get; set; }   

        [Column]
        public int Hardness2 { get; set; }   

        [Column]
        public int CarboHardness1 { get; set; }   

        [Column]
        public int CarboHardness2 { get; set; }   

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoBuilt { get; set; }                

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoColor { get; set; }     
           
        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoReproduction { get; set; }                

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoCulture { get; set; }                

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoGlobal { get; set; }                  ]]>                                                                                                                                                                                                                                                                                       
</xsl:if> 
<xsl:if test="Table='Tbl78Names'">
    <![CDATA[        [Column]
        public string Language { get; set; }   ]]>                                                                                                                                                                                                                                                                                       
</xsl:if>   
<xsl:if test="Table='Tbl81Images'">
    <![CDATA[    [Column]
        public DateTime ShotDate { get; set; }   ]]>                                                                                                                                                                                                                                                                                       
    <![CDATA[    [Column]
        public Guid FilestreamID { get; set; }   ]]>                                                                                                                                                                                                                                                                                       
    <![CDATA[    [Column]
        public byte[] Filestream { get; set; }    ]]>                                                                                                                                                                                                                                                                                       
</xsl:if>   
<xsl:if test="Table='Tbl87Geographics'">
    <![CDATA[    [Column]
        public string Address { get; set; }   ]]>

    <![CDATA[    [Column]
        public string Country { get; set; }   ]]>

    <![CDATA[    [Column]
        public double Latitude { get; set; }   ]]>

    <![CDATA[    [Column]
        public double Longitude { get; set; }   ]]>
</xsl:if>   
<xsl:if test="Table='Tbl90References'">
</xsl:if>   

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
</xsl:if> 
  <![CDATA[  }
}]]>   


</xsl:template>
</xsl:stylesheet>








