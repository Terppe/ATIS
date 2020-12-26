<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[using System;
using System.Collections.ObjectModel;
using System.Globalization;
using DAL;
using DAL.Models;
using GalaSoft.MvvmLight;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;   ]]>

<xsl:choose>
<xsl:when test="Table ='namespace++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:otherwise>   <![CDATA[ 
         //    Report]]><xsl:value-of select="Table"/><![CDATA[Pdf Skriptdatum: ]]> <xsl:value-of select="DateTime"/>  <![CDATA[  

namespace WPFUI.Views.Report.PDF
{  ]]>   
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Abgeleitet von++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:otherwise>   <![CDATA[ 
    public class Report]]><xsl:value-of select="Table"/><![CDATA[Pdf : ViewModelBase
    {     ]]>
</xsl:otherwise>    
</xsl:choose> 
   
<xsl:choose>
<xsl:when test="Table ='Data Members Top+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when> 
<xsl:otherwise>        <![CDATA[ 
        // Set up the fonts to be used on the pages
        private static readonly Font LargeFont = new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD, BaseColor.BLACK);
        private static readonly Font StandardFont = new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.BLACK);
        private static readonly Font StandardBoldFont = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK);
        private static readonly Font SmallFont = new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK);
        private static readonly Font SmallBoldFont = new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.BLACK); ]]> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Data Members Top 1 +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">       <![CDATA[ 
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>();  

        private static Tbl03Regnum _regnum;  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl06Phylums'">       <![CDATA[ 
        private static readonly Repository<Tbl06Phylum, int> Tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>(); 

        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl09Divisions'">       <![CDATA[ 
        private static readonly Repository<Tbl09Division, int> Tbl09DivisionsRepository = new Repository<Tbl09Division, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>(); 

        private static Tbl03Regnum _regnum;
        private static Tbl09Division _division; ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl12Subphylums'">       <![CDATA[ 
        private static readonly Repository<Tbl12Subphylum, int> Tbl12SubphylumsRepository = new Repository<Tbl12Subphylum, int>();
        private static readonly Repository<Tbl06Phylum, int> Tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>();

        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;
        private static Tbl12Subphylum _subphylum;   ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl15Subdivisions'">       <![CDATA[ 
        private static readonly Repository<Tbl15Subdivision, int> Tbl15SubdivisionsRepository = new Repository<Tbl15Subdivision, int>();
        private static readonly Repository<Tbl09Division, int> Tbl09DivisionsRepository = new Repository<Tbl09Division, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>(); 

        private static Tbl03Regnum _regnum;
        private static Tbl09Division _division;
        private static Tbl15Subdivision _subdivision;  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">       <![CDATA[ 
        private static readonly Repository<Tbl18Superclass, int> Tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();
        private static readonly Repository<Tbl15Subdivision, int> Tbl15SubdivisionsRepository = new Repository<Tbl15Subdivision, int>();
        private static readonly Repository<Tbl12Subphylum, int> Tbl12SubphylumsRepository = new Repository<Tbl12Subphylum, int>();
        private static readonly Repository<Tbl09Division, int> Tbl09DivisionsRepository = new Repository<Tbl09Division, int>();
        private static readonly Repository<Tbl06Phylum, int> Tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>(); 

        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;
        private static Tbl09Division _division;
        private static Tbl12Subphylum _subphylum;
        private static Tbl15Subdivision _subdivision;
        private static Tbl18Superclass _superclass;  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl21Classes'">       <![CDATA[ 
        private static readonly Repository<Tbl21Class, int> Tbl21ClassesRepository = new Repository<Tbl21Class, int>();
        private static readonly Repository<Tbl18Superclass, int> Tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();
        private static readonly Repository<Tbl15Subdivision, int> Tbl15SubdivisionsRepository = new Repository<Tbl15Subdivision, int>();
        private static readonly Repository<Tbl12Subphylum, int> Tbl12SubphylumsRepository = new Repository<Tbl12Subphylum, int>();
        private static readonly Repository<Tbl09Division, int> Tbl09DivisionsRepository = new Repository<Tbl09Division, int>();
        private static readonly Repository<Tbl06Phylum, int> Tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>(); 

        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;
        private static Tbl09Division _division;
        private static Tbl12Subphylum _subphylum;
        private static Tbl15Subdivision _subdivision;
        private static Tbl18Superclass _superclass; 
        private static Tbl21Class _class;  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl24Subclasses'">       <![CDATA[ 
        private static readonly Repository<Tbl24Subclass, int> Tbl24SubclassesRepository = new Repository<Tbl24Subclass, int>();
        private static readonly Repository<Tbl21Class, int> Tbl21ClassesRepository = new Repository<Tbl21Class, int>();
        private static readonly Repository<Tbl18Superclass, int> Tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();
        private static readonly Repository<Tbl15Subdivision, int> Tbl15SubdivisionsRepository = new Repository<Tbl15Subdivision, int>();
        private static readonly Repository<Tbl12Subphylum, int> Tbl12SubphylumsRepository = new Repository<Tbl12Subphylum, int>();
        private static readonly Repository<Tbl09Division, int> Tbl09DivisionsRepository = new Repository<Tbl09Division, int>();
        private static readonly Repository<Tbl06Phylum, int> Tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>(); 

        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;
        private static Tbl09Division _division;
        private static Tbl12Subphylum _subphylum;
        private static Tbl15Subdivision _subdivision;
        private static Tbl18Superclass _superclass; 
        private static Tbl21Class _class;   
        private static Tbl24Subclass _subclass;  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl27Infraclasses'">       <![CDATA[ 
        private static readonly Repository<Tbl27Infraclass, int> Tbl27InfraclassesRepository = new Repository<Tbl27Infraclass, int>();
        private static readonly Repository<Tbl24Subclass, int> Tbl24SubclassesRepository = new Repository<Tbl24Subclass, int>();
        private static readonly Repository<Tbl21Class, int> Tbl21ClassesRepository = new Repository<Tbl21Class, int>();
        private static readonly Repository<Tbl18Superclass, int> Tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();
        private static readonly Repository<Tbl15Subdivision, int> Tbl15SubdivisionsRepository = new Repository<Tbl15Subdivision, int>();
        private static readonly Repository<Tbl12Subphylum, int> Tbl12SubphylumsRepository = new Repository<Tbl12Subphylum, int>();
        private static readonly Repository<Tbl09Division, int> Tbl09DivisionsRepository = new Repository<Tbl09Division, int>();
        private static readonly Repository<Tbl06Phylum, int> Tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>(); 

        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;
        private static Tbl09Division _division;
        private static Tbl12Subphylum _subphylum;
        private static Tbl15Subdivision _subdivision;
        private static Tbl18Superclass _superclass; 
        private static Tbl21Class _class;   
        private static Tbl24Subclass _subclass;  
        private static Tbl27Infraclass _infraclass;  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl30Legios'">       <![CDATA[ 
        private static readonly Repository<Tbl30Legio, int> Tbl30LegiosRepository = new Repository<Tbl30Legio, int>();
        private static readonly Repository<Tbl27Infraclass, int> Tbl27InfraclassesRepository = new Repository<Tbl27Infraclass, int>();
        private static readonly Repository<Tbl24Subclass, int> Tbl24SubclassesRepository = new Repository<Tbl24Subclass, int>();
        private static readonly Repository<Tbl21Class, int> Tbl21ClassesRepository = new Repository<Tbl21Class, int>();
        private static readonly Repository<Tbl18Superclass, int> Tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();
        private static readonly Repository<Tbl15Subdivision, int> Tbl15SubdivisionsRepository = new Repository<Tbl15Subdivision, int>();
        private static readonly Repository<Tbl12Subphylum, int> Tbl12SubphylumsRepository = new Repository<Tbl12Subphylum, int>();
        private static readonly Repository<Tbl09Division, int> Tbl09DivisionsRepository = new Repository<Tbl09Division, int>();
        private static readonly Repository<Tbl06Phylum, int> Tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>(); 

        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;
        private static Tbl09Division _division;
        private static Tbl12Subphylum _subphylum;
        private static Tbl15Subdivision _subdivision;
        private static Tbl18Superclass _superclass; 
        private static Tbl21Class _class;   
        private static Tbl24Subclass _subclass;  
        private static Tbl27Infraclass _infraclass; 
        private static Tbl30Legio _legio;  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl33Ordos'">       <![CDATA[ 
        private static readonly Repository<Tbl33Ordo, int> Tbl33OrdosRepository = new Repository<Tbl33Ordo, int>();
        private static readonly Repository<Tbl30Legio, int> Tbl30LegiosRepository = new Repository<Tbl30Legio, int>();
        private static readonly Repository<Tbl27Infraclass, int> Tbl27InfraclassesRepository = new Repository<Tbl27Infraclass, int>();
        private static readonly Repository<Tbl24Subclass, int> Tbl24SubclassesRepository = new Repository<Tbl24Subclass, int>();
        private static readonly Repository<Tbl21Class, int> Tbl21ClassesRepository = new Repository<Tbl21Class, int>();
        private static readonly Repository<Tbl18Superclass, int> Tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();
        private static readonly Repository<Tbl15Subdivision, int> Tbl15SubdivisionsRepository = new Repository<Tbl15Subdivision, int>();
        private static readonly Repository<Tbl12Subphylum, int> Tbl12SubphylumsRepository = new Repository<Tbl12Subphylum, int>();
        private static readonly Repository<Tbl09Division, int> Tbl09DivisionsRepository = new Repository<Tbl09Division, int>();
        private static readonly Repository<Tbl06Phylum, int> Tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>(); 

        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;
        private static Tbl09Division _division;
        private static Tbl12Subphylum _subphylum;
        private static Tbl15Subdivision _subdivision;
        private static Tbl18Superclass _superclass; 
        private static Tbl21Class _class;   
        private static Tbl24Subclass _subclass;  
        private static Tbl27Infraclass _infraclass; 
        private static Tbl30Legio _legio;  
        private static Tbl33Ordo _ordo;  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl36Subordos'">       <![CDATA[ 
        private static readonly Repository<Tbl36Subordo, int> Tbl36SubordosRepository = new Repository<Tbl36Subordo, int>();
        private static readonly Repository<Tbl33Ordo, int> Tbl33OrdosRepository = new Repository<Tbl33Ordo, int>();
        private static readonly Repository<Tbl30Legio, int> Tbl30LegiosRepository = new Repository<Tbl30Legio, int>();
        private static readonly Repository<Tbl27Infraclass, int> Tbl27InfraclassesRepository = new Repository<Tbl27Infraclass, int>();
        private static readonly Repository<Tbl24Subclass, int> Tbl24SubclassesRepository = new Repository<Tbl24Subclass, int>();
        private static readonly Repository<Tbl21Class, int> Tbl21ClassesRepository = new Repository<Tbl21Class, int>();
        private static readonly Repository<Tbl18Superclass, int> Tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();
        private static readonly Repository<Tbl15Subdivision, int> Tbl15SubdivisionsRepository = new Repository<Tbl15Subdivision, int>();
        private static readonly Repository<Tbl12Subphylum, int> Tbl12SubphylumsRepository = new Repository<Tbl12Subphylum, int>();
        private static readonly Repository<Tbl09Division, int> Tbl09DivisionsRepository = new Repository<Tbl09Division, int>();
        private static readonly Repository<Tbl06Phylum, int> Tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>(); 

        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;
        private static Tbl09Division _division;
        private static Tbl12Subphylum _subphylum;
        private static Tbl15Subdivision _subdivision;
        private static Tbl18Superclass _superclass; 
        private static Tbl21Class _class;   
        private static Tbl24Subclass _subclass;  
        private static Tbl27Infraclass _infraclass; 
        private static Tbl30Legio _legio;  
        private static Tbl33Ordo _ordo;   
        private static Tbl36Subordo _subordo;  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl39Infraordos'">       <![CDATA[ 
        private static readonly Repository<Tbl39Infraordo, int> Tbl39InfraordosRepository = new Repository<Tbl39Infraordo, int>();
        private static readonly Repository<Tbl36Subordo, int> Tbl36SubordosRepository = new Repository<Tbl36Subordo, int>();
        private static readonly Repository<Tbl33Ordo, int> Tbl33OrdosRepository = new Repository<Tbl33Ordo, int>();
        private static readonly Repository<Tbl30Legio, int> Tbl30LegiosRepository = new Repository<Tbl30Legio, int>();
        private static readonly Repository<Tbl27Infraclass, int> Tbl27InfraclassesRepository = new Repository<Tbl27Infraclass, int>();
        private static readonly Repository<Tbl24Subclass, int> Tbl24SubclassesRepository = new Repository<Tbl24Subclass, int>();
        private static readonly Repository<Tbl21Class, int> Tbl21ClassesRepository = new Repository<Tbl21Class, int>();
        private static readonly Repository<Tbl18Superclass, int> Tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();
        private static readonly Repository<Tbl15Subdivision, int> Tbl15SubdivisionsRepository = new Repository<Tbl15Subdivision, int>();
        private static readonly Repository<Tbl12Subphylum, int> Tbl12SubphylumsRepository = new Repository<Tbl12Subphylum, int>();
        private static readonly Repository<Tbl09Division, int> Tbl09DivisionsRepository = new Repository<Tbl09Division, int>();
        private static readonly Repository<Tbl06Phylum, int> Tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>(); 

        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;
        private static Tbl09Division _division;
        private static Tbl12Subphylum _subphylum;
        private static Tbl15Subdivision _subdivision;
        private static Tbl18Superclass _superclass; 
        private static Tbl21Class _class;   
        private static Tbl24Subclass _subclass;  
        private static Tbl27Infraclass _infraclass; 
        private static Tbl30Legio _legio;  
        private static Tbl33Ordo _ordo;   
        private static Tbl36Subordo _subordo;   
        private static Tbl39Infraordo _infraordo;  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl42Superfamilies'">       <![CDATA[ 
        private static readonly Repository<Tbl42Superfamily, int> Tbl42SuperfamiliesRepository = new Repository<Tbl42Superfamily, int>();
        private static readonly Repository<Tbl39Infraordo, int> Tbl39InfraordosRepository = new Repository<Tbl39Infraordo, int>();
        private static readonly Repository<Tbl36Subordo, int> Tbl36SubordosRepository = new Repository<Tbl36Subordo, int>();
        private static readonly Repository<Tbl33Ordo, int> Tbl33OrdosRepository = new Repository<Tbl33Ordo, int>();
        private static readonly Repository<Tbl30Legio, int> Tbl30LegiosRepository = new Repository<Tbl30Legio, int>();
        private static readonly Repository<Tbl27Infraclass, int> Tbl27InfraclassesRepository = new Repository<Tbl27Infraclass, int>();
        private static readonly Repository<Tbl24Subclass, int> Tbl24SubclassesRepository = new Repository<Tbl24Subclass, int>();
        private static readonly Repository<Tbl21Class, int> Tbl21ClassesRepository = new Repository<Tbl21Class, int>();
        private static readonly Repository<Tbl18Superclass, int> Tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();
        private static readonly Repository<Tbl15Subdivision, int> Tbl15SubdivisionsRepository = new Repository<Tbl15Subdivision, int>();
        private static readonly Repository<Tbl12Subphylum, int> Tbl12SubphylumsRepository = new Repository<Tbl12Subphylum, int>();
        private static readonly Repository<Tbl09Division, int> Tbl09DivisionsRepository = new Repository<Tbl09Division, int>();
        private static readonly Repository<Tbl06Phylum, int> Tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>(); 

        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;
        private static Tbl09Division _division;
        private static Tbl12Subphylum _subphylum;
        private static Tbl15Subdivision _subdivision;
        private static Tbl18Superclass _superclass; 
        private static Tbl21Class _class;   
        private static Tbl24Subclass _subclass;  
        private static Tbl27Infraclass _infraclass; 
        private static Tbl30Legio _legio;  
        private static Tbl33Ordo _ordo;   
        private static Tbl36Subordo _subordo;   
        private static Tbl39Infraordo _infraordo;  
        private static Tbl42Superfamily _superfamily;  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl45Families'">       <![CDATA[ 
        private static readonly Repository<Tbl45Family, int> Tbl45FamiliesRepository = new Repository<Tbl45Family, int>();
        private static readonly Repository<Tbl42Superfamily, int> Tbl42SuperfamiliesRepository = new Repository<Tbl42Superfamily, int>();
        private static readonly Repository<Tbl39Infraordo, int> Tbl39InfraordosRepository = new Repository<Tbl39Infraordo, int>();
        private static readonly Repository<Tbl36Subordo, int> Tbl36SubordosRepository = new Repository<Tbl36Subordo, int>();
        private static readonly Repository<Tbl33Ordo, int> Tbl33OrdosRepository = new Repository<Tbl33Ordo, int>();
        private static readonly Repository<Tbl30Legio, int> Tbl30LegiosRepository = new Repository<Tbl30Legio, int>();
        private static readonly Repository<Tbl27Infraclass, int> Tbl27InfraclassesRepository = new Repository<Tbl27Infraclass, int>();
        private static readonly Repository<Tbl24Subclass, int> Tbl24SubclassesRepository = new Repository<Tbl24Subclass, int>();
        private static readonly Repository<Tbl21Class, int> Tbl21ClassesRepository = new Repository<Tbl21Class, int>();
        private static readonly Repository<Tbl18Superclass, int> Tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();
        private static readonly Repository<Tbl15Subdivision, int> Tbl15SubdivisionsRepository = new Repository<Tbl15Subdivision, int>();
        private static readonly Repository<Tbl12Subphylum, int> Tbl12SubphylumsRepository = new Repository<Tbl12Subphylum, int>();
        private static readonly Repository<Tbl09Division, int> Tbl09DivisionsRepository = new Repository<Tbl09Division, int>();
        private static readonly Repository<Tbl06Phylum, int> Tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>(); 

        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;
        private static Tbl09Division _division;
        private static Tbl12Subphylum _subphylum;
        private static Tbl15Subdivision _subdivision;
        private static Tbl18Superclass _superclass; 
        private static Tbl21Class _class;   
        private static Tbl24Subclass _subclass;  
        private static Tbl27Infraclass _infraclass; 
        private static Tbl30Legio _legio;  
        private static Tbl33Ordo _ordo;   
        private static Tbl36Subordo _subordo;   
        private static Tbl39Infraordo _infraordo;  
        private static Tbl42Superfamily _superfamily;  
        private static Tbl45Family _family;  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl48Subfamilies'">       <![CDATA[ 
        private static readonly Repository<Tbl48Subfamily, int> Tbl48SubfamiliesRepository = new Repository<Tbl48Subfamily, int>();
        private static readonly Repository<Tbl45Family, int> Tbl45FamiliesRepository = new Repository<Tbl45Family, int>();
        private static readonly Repository<Tbl42Superfamily, int> Tbl42SuperfamiliesRepository = new Repository<Tbl42Superfamily, int>();
        private static readonly Repository<Tbl39Infraordo, int> Tbl39InfraordosRepository = new Repository<Tbl39Infraordo, int>();
        private static readonly Repository<Tbl36Subordo, int> Tbl36SubordosRepository = new Repository<Tbl36Subordo, int>();
        private static readonly Repository<Tbl33Ordo, int> Tbl33OrdosRepository = new Repository<Tbl33Ordo, int>();
        private static readonly Repository<Tbl30Legio, int> Tbl30LegiosRepository = new Repository<Tbl30Legio, int>();
        private static readonly Repository<Tbl27Infraclass, int> Tbl27InfraclassesRepository = new Repository<Tbl27Infraclass, int>();
        private static readonly Repository<Tbl24Subclass, int> Tbl24SubclassesRepository = new Repository<Tbl24Subclass, int>();
        private static readonly Repository<Tbl21Class, int> Tbl21ClassesRepository = new Repository<Tbl21Class, int>();
        private static readonly Repository<Tbl18Superclass, int> Tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();
        private static readonly Repository<Tbl15Subdivision, int> Tbl15SubdivisionsRepository = new Repository<Tbl15Subdivision, int>();
        private static readonly Repository<Tbl12Subphylum, int> Tbl12SubphylumsRepository = new Repository<Tbl12Subphylum, int>();
        private static readonly Repository<Tbl09Division, int> Tbl09DivisionsRepository = new Repository<Tbl09Division, int>();
        private static readonly Repository<Tbl06Phylum, int> Tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>(); 

        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;
        private static Tbl09Division _division;
        private static Tbl12Subphylum _subphylum;
        private static Tbl15Subdivision _subdivision;
        private static Tbl18Superclass _superclass; 
        private static Tbl21Class _class;   
        private static Tbl24Subclass _subclass;  
        private static Tbl27Infraclass _infraclass; 
        private static Tbl30Legio _legio;  
        private static Tbl33Ordo _ordo;   
        private static Tbl36Subordo _subordo;   
        private static Tbl39Infraordo _infraordo;  
        private static Tbl42Superfamily _superfamily;  
        private static Tbl45Family _family;  
        private static Tbl48Subfamily _subfamily;  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl51Infrafamilies'">       <![CDATA[ 
        private static readonly Repository<Tbl51Infrafamily, int> Tbl51InfrafamiliesRepository = new Repository<Tbl51Infrafamily, int>();
        private static readonly Repository<Tbl48Subfamily, int> Tbl48SubfamiliesRepository = new Repository<Tbl48Subfamily, int>();
        private static readonly Repository<Tbl45Family, int> Tbl45FamiliesRepository = new Repository<Tbl45Family, int>();
        private static readonly Repository<Tbl42Superfamily, int> Tbl42SuperfamiliesRepository = new Repository<Tbl42Superfamily, int>();
        private static readonly Repository<Tbl39Infraordo, int> Tbl39InfraordosRepository = new Repository<Tbl39Infraordo, int>();
        private static readonly Repository<Tbl36Subordo, int> Tbl36SubordosRepository = new Repository<Tbl36Subordo, int>();
        private static readonly Repository<Tbl33Ordo, int> Tbl33OrdosRepository = new Repository<Tbl33Ordo, int>();
        private static readonly Repository<Tbl30Legio, int> Tbl30LegiosRepository = new Repository<Tbl30Legio, int>();
        private static readonly Repository<Tbl27Infraclass, int> Tbl27InfraclassesRepository = new Repository<Tbl27Infraclass, int>();
        private static readonly Repository<Tbl24Subclass, int> Tbl24SubclassesRepository = new Repository<Tbl24Subclass, int>();
        private static readonly Repository<Tbl21Class, int> Tbl21ClassesRepository = new Repository<Tbl21Class, int>();
        private static readonly Repository<Tbl18Superclass, int> Tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();
        private static readonly Repository<Tbl15Subdivision, int> Tbl15SubdivisionsRepository = new Repository<Tbl15Subdivision, int>();
        private static readonly Repository<Tbl12Subphylum, int> Tbl12SubphylumsRepository = new Repository<Tbl12Subphylum, int>();
        private static readonly Repository<Tbl09Division, int> Tbl09DivisionsRepository = new Repository<Tbl09Division, int>();
        private static readonly Repository<Tbl06Phylum, int> Tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>(); 

        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;
        private static Tbl09Division _division;
        private static Tbl12Subphylum _subphylum;
        private static Tbl15Subdivision _subdivision;
        private static Tbl18Superclass _superclass; 
        private static Tbl21Class _class;   
        private static Tbl24Subclass _subclass;  
        private static Tbl27Infraclass _infraclass; 
        private static Tbl30Legio _legio;  
        private static Tbl33Ordo _ordo;   
        private static Tbl36Subordo _subordo;   
        private static Tbl39Infraordo _infraordo;  
        private static Tbl42Superfamily _superfamily;  
        private static Tbl45Family _family;  
        private static Tbl48Subfamily _subfamily;  
        private static Tbl51Infrafamily _infrafamily;  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl54Supertribusses'">       <![CDATA[ 
        private static readonly Repository<Tbl54Supertribus, int> Tbl54SupertribussesRepository = new Repository<Tbl54Supertribus, int>();
        private static readonly Repository<Tbl51Infrafamily, int> Tbl51InfrafamiliesRepository = new Repository<Tbl51Infrafamily, int>();
        private static readonly Repository<Tbl48Subfamily, int> Tbl48SubfamiliesRepository = new Repository<Tbl48Subfamily, int>();
        private static readonly Repository<Tbl45Family, int> Tbl45FamiliesRepository = new Repository<Tbl45Family, int>();
        private static readonly Repository<Tbl42Superfamily, int> Tbl42SuperfamiliesRepository = new Repository<Tbl42Superfamily, int>();
        private static readonly Repository<Tbl39Infraordo, int> Tbl39InfraordosRepository = new Repository<Tbl39Infraordo, int>();
        private static readonly Repository<Tbl36Subordo, int> Tbl36SubordosRepository = new Repository<Tbl36Subordo, int>();
        private static readonly Repository<Tbl33Ordo, int> Tbl33OrdosRepository = new Repository<Tbl33Ordo, int>();
        private static readonly Repository<Tbl30Legio, int> Tbl30LegiosRepository = new Repository<Tbl30Legio, int>();
        private static readonly Repository<Tbl27Infraclass, int> Tbl27InfraclassesRepository = new Repository<Tbl27Infraclass, int>();
        private static readonly Repository<Tbl24Subclass, int> Tbl24SubclassesRepository = new Repository<Tbl24Subclass, int>();
        private static readonly Repository<Tbl21Class, int> Tbl21ClassesRepository = new Repository<Tbl21Class, int>();
        private static readonly Repository<Tbl18Superclass, int> Tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();
        private static readonly Repository<Tbl15Subdivision, int> Tbl15SubdivisionsRepository = new Repository<Tbl15Subdivision, int>();
        private static readonly Repository<Tbl12Subphylum, int> Tbl12SubphylumsRepository = new Repository<Tbl12Subphylum, int>();
        private static readonly Repository<Tbl09Division, int> Tbl09DivisionsRepository = new Repository<Tbl09Division, int>();
        private static readonly Repository<Tbl06Phylum, int> Tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>(); 

        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;
        private static Tbl09Division _division;
        private static Tbl12Subphylum _subphylum;
        private static Tbl15Subdivision _subdivision;
        private static Tbl18Superclass _superclass; 
        private static Tbl21Class _class;   
        private static Tbl24Subclass _subclass;  
        private static Tbl27Infraclass _infraclass; 
        private static Tbl30Legio _legio;  
        private static Tbl33Ordo _ordo;   
        private static Tbl36Subordo _subordo;   
        private static Tbl39Infraordo _infraordo;  
        private static Tbl42Superfamily _superfamily;  
        private static Tbl45Family _family;  
        private static Tbl48Subfamily _subfamily;  
        private static Tbl51Infrafamily _infrafamily;  
        private static Tbl54Supertribus _supertribus;  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl57Tribusses'">       <![CDATA[ 
        private static readonly Repository<Tbl57Tribus, int> Tbl57TribussesRepository = new Repository<Tbl57Tribus, int>();
        private static readonly Repository<Tbl54Supertribus, int> Tbl54SupertribussesRepository = new Repository<Tbl54Supertribus, int>();
        private static readonly Repository<Tbl51Infrafamily, int> Tbl51InfrafamiliesRepository = new Repository<Tbl51Infrafamily, int>();
        private static readonly Repository<Tbl48Subfamily, int> Tbl48SubfamiliesRepository = new Repository<Tbl48Subfamily, int>();
        private static readonly Repository<Tbl45Family, int> Tbl45FamiliesRepository = new Repository<Tbl45Family, int>();
        private static readonly Repository<Tbl42Superfamily, int> Tbl42SuperfamiliesRepository = new Repository<Tbl42Superfamily, int>();
        private static readonly Repository<Tbl39Infraordo, int> Tbl39InfraordosRepository = new Repository<Tbl39Infraordo, int>();
        private static readonly Repository<Tbl36Subordo, int> Tbl36SubordosRepository = new Repository<Tbl36Subordo, int>();
        private static readonly Repository<Tbl33Ordo, int> Tbl33OrdosRepository = new Repository<Tbl33Ordo, int>();
        private static readonly Repository<Tbl30Legio, int> Tbl30LegiosRepository = new Repository<Tbl30Legio, int>();
        private static readonly Repository<Tbl27Infraclass, int> Tbl27InfraclassesRepository = new Repository<Tbl27Infraclass, int>();
        private static readonly Repository<Tbl24Subclass, int> Tbl24SubclassesRepository = new Repository<Tbl24Subclass, int>();
        private static readonly Repository<Tbl21Class, int> Tbl21ClassesRepository = new Repository<Tbl21Class, int>();
        private static readonly Repository<Tbl18Superclass, int> Tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();
        private static readonly Repository<Tbl15Subdivision, int> Tbl15SubdivisionsRepository = new Repository<Tbl15Subdivision, int>();
        private static readonly Repository<Tbl12Subphylum, int> Tbl12SubphylumsRepository = new Repository<Tbl12Subphylum, int>();
        private static readonly Repository<Tbl09Division, int> Tbl09DivisionsRepository = new Repository<Tbl09Division, int>();
        private static readonly Repository<Tbl06Phylum, int> Tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>(); 

        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;
        private static Tbl09Division _division;
        private static Tbl12Subphylum _subphylum;
        private static Tbl15Subdivision _subdivision;
        private static Tbl18Superclass _superclass; 
        private static Tbl21Class _class;   
        private static Tbl24Subclass _subclass;  
        private static Tbl27Infraclass _infraclass; 
        private static Tbl30Legio _legio;  
        private static Tbl33Ordo _ordo;   
        private static Tbl36Subordo _subordo;   
        private static Tbl39Infraordo _infraordo;  
        private static Tbl42Superfamily _superfamily;  
        private static Tbl45Family _family;  
        private static Tbl48Subfamily _subfamily;  
        private static Tbl51Infrafamily _infrafamily;  
        private static Tbl54Supertribus _supertribus;  
        private static Tbl57Tribus _tribus;  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl60Subtribusses'">       <![CDATA[ 
        private static readonly Repository<Tbl60Subtribus, int> Tbl60SubtribussesRepository = new Repository<Tbl60Subtribus, int>();
        private static readonly Repository<Tbl57Tribus, int> Tbl57TribussesRepository = new Repository<Tbl57Tribus, int>();
        private static readonly Repository<Tbl54Supertribus, int> Tbl54SupertribussesRepository = new Repository<Tbl54Supertribus, int>();
        private static readonly Repository<Tbl51Infrafamily, int> Tbl51InfrafamiliesRepository = new Repository<Tbl51Infrafamily, int>();
        private static readonly Repository<Tbl48Subfamily, int> Tbl48SubfamiliesRepository = new Repository<Tbl48Subfamily, int>();
        private static readonly Repository<Tbl45Family, int> Tbl45FamiliesRepository = new Repository<Tbl45Family, int>();
        private static readonly Repository<Tbl42Superfamily, int> Tbl42SuperfamiliesRepository = new Repository<Tbl42Superfamily, int>();
        private static readonly Repository<Tbl39Infraordo, int> Tbl39InfraordosRepository = new Repository<Tbl39Infraordo, int>();
        private static readonly Repository<Tbl36Subordo, int> Tbl36SubordosRepository = new Repository<Tbl36Subordo, int>();
        private static readonly Repository<Tbl33Ordo, int> Tbl33OrdosRepository = new Repository<Tbl33Ordo, int>();
        private static readonly Repository<Tbl30Legio, int> Tbl30LegiosRepository = new Repository<Tbl30Legio, int>();
        private static readonly Repository<Tbl27Infraclass, int> Tbl27InfraclassesRepository = new Repository<Tbl27Infraclass, int>();
        private static readonly Repository<Tbl24Subclass, int> Tbl24SubclassesRepository = new Repository<Tbl24Subclass, int>();
        private static readonly Repository<Tbl21Class, int> Tbl21ClassesRepository = new Repository<Tbl21Class, int>();
        private static readonly Repository<Tbl18Superclass, int> Tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();
        private static readonly Repository<Tbl15Subdivision, int> Tbl15SubdivisionsRepository = new Repository<Tbl15Subdivision, int>();
        private static readonly Repository<Tbl12Subphylum, int> Tbl12SubphylumsRepository = new Repository<Tbl12Subphylum, int>();
        private static readonly Repository<Tbl09Division, int> Tbl09DivisionsRepository = new Repository<Tbl09Division, int>();
        private static readonly Repository<Tbl06Phylum, int> Tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>(); 

        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;
        private static Tbl09Division _division;
        private static Tbl12Subphylum _subphylum;
        private static Tbl15Subdivision _subdivision;
        private static Tbl18Superclass _superclass; 
        private static Tbl21Class _class;   
        private static Tbl24Subclass _subclass;  
        private static Tbl27Infraclass _infraclass; 
        private static Tbl30Legio _legio;  
        private static Tbl33Ordo _ordo;   
        private static Tbl36Subordo _subordo;   
        private static Tbl39Infraordo _infraordo;  
        private static Tbl42Superfamily _superfamily;  
        private static Tbl45Family _family;  
        private static Tbl48Subfamily _subfamily;  
        private static Tbl51Infrafamily _infrafamily;  
        private static Tbl54Supertribus _supertribus;  
        private static Tbl57Tribus _tribus;  
        private static Tbl60Subtribus _subtribus;  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl63Infratribusses'">       <![CDATA[ 
        private static readonly Repository<Tbl63Infratribus, int> Tbl63InfratribussesRepository = new Repository<Tbl63Infratribus, int>();
        private static readonly Repository<Tbl60Subtribus, int> Tbl60SubtribussesRepository = new Repository<Tbl60Subtribus, int>();
        private static readonly Repository<Tbl57Tribus, int> Tbl57TribussesRepository = new Repository<Tbl57Tribus, int>();
        private static readonly Repository<Tbl54Supertribus, int> Tbl54SupertribussesRepository = new Repository<Tbl54Supertribus, int>();
        private static readonly Repository<Tbl51Infrafamily, int> Tbl51InfrafamiliesRepository = new Repository<Tbl51Infrafamily, int>();
        private static readonly Repository<Tbl48Subfamily, int> Tbl48SubfamiliesRepository = new Repository<Tbl48Subfamily, int>();
        private static readonly Repository<Tbl45Family, int> Tbl45FamiliesRepository = new Repository<Tbl45Family, int>();
        private static readonly Repository<Tbl42Superfamily, int> Tbl42SuperfamiliesRepository = new Repository<Tbl42Superfamily, int>();
        private static readonly Repository<Tbl39Infraordo, int> Tbl39InfraordosRepository = new Repository<Tbl39Infraordo, int>();
        private static readonly Repository<Tbl36Subordo, int> Tbl36SubordosRepository = new Repository<Tbl36Subordo, int>();
        private static readonly Repository<Tbl33Ordo, int> Tbl33OrdosRepository = new Repository<Tbl33Ordo, int>();
        private static readonly Repository<Tbl30Legio, int> Tbl30LegiosRepository = new Repository<Tbl30Legio, int>();
        private static readonly Repository<Tbl27Infraclass, int> Tbl27InfraclassesRepository = new Repository<Tbl27Infraclass, int>();
        private static readonly Repository<Tbl24Subclass, int> Tbl24SubclassesRepository = new Repository<Tbl24Subclass, int>();
        private static readonly Repository<Tbl21Class, int> Tbl21ClassesRepository = new Repository<Tbl21Class, int>();
        private static readonly Repository<Tbl18Superclass, int> Tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();
        private static readonly Repository<Tbl15Subdivision, int> Tbl15SubdivisionsRepository = new Repository<Tbl15Subdivision, int>();
        private static readonly Repository<Tbl12Subphylum, int> Tbl12SubphylumsRepository = new Repository<Tbl12Subphylum, int>();
        private static readonly Repository<Tbl09Division, int> Tbl09DivisionsRepository = new Repository<Tbl09Division, int>();
        private static readonly Repository<Tbl06Phylum, int> Tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>(); 

        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;
        private static Tbl09Division _division;
        private static Tbl12Subphylum _subphylum;
        private static Tbl15Subdivision _subdivision;
        private static Tbl18Superclass _superclass; 
        private static Tbl21Class _class;   
        private static Tbl24Subclass _subclass;  
        private static Tbl27Infraclass _infraclass; 
        private static Tbl30Legio _legio;  
        private static Tbl33Ordo _ordo;   
        private static Tbl36Subordo _subordo;   
        private static Tbl39Infraordo _infraordo;  
        private static Tbl42Superfamily _superfamily;  
        private static Tbl45Family _family;  
        private static Tbl48Subfamily _subfamily;  
        private static Tbl51Infrafamily _infrafamily;  
        private static Tbl54Supertribus _supertribus;  
        private static Tbl57Tribus _tribus;  
        private static Tbl60Subtribus _subtribus;  
        private static Tbl63Infratribus _infratribus;  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl66Genusses'">       <![CDATA[ 
        private static readonly Repository<Tbl66Genus, int> Tbl66GenussesRepository = new Repository<Tbl66Genus, int>();
        private static readonly Repository<Tbl63Infratribus, int> Tbl63InfratribussesRepository = new Repository<Tbl63Infratribus, int>();
        private static readonly Repository<Tbl60Subtribus, int> Tbl60SubtribussesRepository = new Repository<Tbl60Subtribus, int>();
        private static readonly Repository<Tbl57Tribus, int> Tbl57TribussesRepository = new Repository<Tbl57Tribus, int>();
        private static readonly Repository<Tbl54Supertribus, int> Tbl54SupertribussesRepository = new Repository<Tbl54Supertribus, int>();
        private static readonly Repository<Tbl51Infrafamily, int> Tbl51InfrafamiliesRepository = new Repository<Tbl51Infrafamily, int>();
        private static readonly Repository<Tbl48Subfamily, int> Tbl48SubfamiliesRepository = new Repository<Tbl48Subfamily, int>();
        private static readonly Repository<Tbl45Family, int> Tbl45FamiliesRepository = new Repository<Tbl45Family, int>();
        private static readonly Repository<Tbl42Superfamily, int> Tbl42SuperfamiliesRepository = new Repository<Tbl42Superfamily, int>();
        private static readonly Repository<Tbl39Infraordo, int> Tbl39InfraordosRepository = new Repository<Tbl39Infraordo, int>();
        private static readonly Repository<Tbl36Subordo, int> Tbl36SubordosRepository = new Repository<Tbl36Subordo, int>();
        private static readonly Repository<Tbl33Ordo, int> Tbl33OrdosRepository = new Repository<Tbl33Ordo, int>();
        private static readonly Repository<Tbl30Legio, int> Tbl30LegiosRepository = new Repository<Tbl30Legio, int>();
        private static readonly Repository<Tbl27Infraclass, int> Tbl27InfraclassesRepository = new Repository<Tbl27Infraclass, int>();
        private static readonly Repository<Tbl24Subclass, int> Tbl24SubclassesRepository = new Repository<Tbl24Subclass, int>();
        private static readonly Repository<Tbl21Class, int> Tbl21ClassesRepository = new Repository<Tbl21Class, int>();
        private static readonly Repository<Tbl18Superclass, int> Tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();
        private static readonly Repository<Tbl15Subdivision, int> Tbl15SubdivisionsRepository = new Repository<Tbl15Subdivision, int>();
        private static readonly Repository<Tbl12Subphylum, int> Tbl12SubphylumsRepository = new Repository<Tbl12Subphylum, int>();
        private static readonly Repository<Tbl09Division, int> Tbl09DivisionsRepository = new Repository<Tbl09Division, int>();
        private static readonly Repository<Tbl06Phylum, int> Tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>(); 

        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;
        private static Tbl09Division _division;
        private static Tbl12Subphylum _subphylum;
        private static Tbl15Subdivision _subdivision;
        private static Tbl18Superclass _superclass; 
        private static Tbl21Class _class;   
        private static Tbl24Subclass _subclass;  
        private static Tbl27Infraclass _infraclass; 
        private static Tbl30Legio _legio;  
        private static Tbl33Ordo _ordo;   
        private static Tbl36Subordo _subordo;   
        private static Tbl39Infraordo _infraordo;  
        private static Tbl42Superfamily _superfamily;  
        private static Tbl45Family _family;  
        private static Tbl48Subfamily _subfamily;  
        private static Tbl51Infrafamily _infrafamily;  
        private static Tbl54Supertribus _supertribus;  
        private static Tbl57Tribus _tribus;  
        private static Tbl60Subtribus _subtribus;  
        private static Tbl63Infratribus _infratribus;   
        private static Tbl66Genus _genus;  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl69FiSpeciesses'">       <![CDATA[ 
        private static readonly Repository<Tbl69FiSpecies, int> Tbl69FiSpeciessesRepository = new Repository<Tbl69FiSpecies, int>();
        private static readonly Repository<Tbl66Genus, int> Tbl66GenussesRepository = new Repository<Tbl66Genus, int>();
        private static readonly Repository<Tbl63Infratribus, int> Tbl63InfratribussesRepository = new Repository<Tbl63Infratribus, int>();
        private static readonly Repository<Tbl60Subtribus, int> Tbl60SubtribussesRepository = new Repository<Tbl60Subtribus, int>();
        private static readonly Repository<Tbl57Tribus, int> Tbl57TribussesRepository = new Repository<Tbl57Tribus, int>();
        private static readonly Repository<Tbl54Supertribus, int> Tbl54SupertribussesRepository = new Repository<Tbl54Supertribus, int>();
        private static readonly Repository<Tbl51Infrafamily, int> Tbl51InfrafamiliesRepository = new Repository<Tbl51Infrafamily, int>();
        private static readonly Repository<Tbl48Subfamily, int> Tbl48SubfamiliesRepository = new Repository<Tbl48Subfamily, int>();
        private static readonly Repository<Tbl45Family, int> Tbl45FamiliesRepository = new Repository<Tbl45Family, int>();
        private static readonly Repository<Tbl42Superfamily, int> Tbl42SuperfamiliesRepository = new Repository<Tbl42Superfamily, int>();
        private static readonly Repository<Tbl39Infraordo, int> Tbl39InfraordosRepository = new Repository<Tbl39Infraordo, int>();
        private static readonly Repository<Tbl36Subordo, int> Tbl36SubordosRepository = new Repository<Tbl36Subordo, int>();
        private static readonly Repository<Tbl33Ordo, int> Tbl33OrdosRepository = new Repository<Tbl33Ordo, int>();
        private static readonly Repository<Tbl30Legio, int> Tbl30LegiosRepository = new Repository<Tbl30Legio, int>();
        private static readonly Repository<Tbl27Infraclass, int> Tbl27InfraclassesRepository = new Repository<Tbl27Infraclass, int>();
        private static readonly Repository<Tbl24Subclass, int> Tbl24SubclassesRepository = new Repository<Tbl24Subclass, int>();
        private static readonly Repository<Tbl21Class, int> Tbl21ClassesRepository = new Repository<Tbl21Class, int>();
        private static readonly Repository<Tbl18Superclass, int> Tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();
        private static readonly Repository<Tbl15Subdivision, int> Tbl15SubdivisionsRepository = new Repository<Tbl15Subdivision, int>();
        private static readonly Repository<Tbl12Subphylum, int> Tbl12SubphylumsRepository = new Repository<Tbl12Subphylum, int>();
        private static readonly Repository<Tbl09Division, int> Tbl09DivisionsRepository = new Repository<Tbl09Division, int>();
        private static readonly Repository<Tbl06Phylum, int> Tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>(); 

        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;
        private static Tbl09Division _division;
        private static Tbl12Subphylum _subphylum;
        private static Tbl15Subdivision _subdivision;
        private static Tbl18Superclass _superclass; 
        private static Tbl21Class _class;   
        private static Tbl24Subclass _subclass;  
        private static Tbl27Infraclass _infraclass; 
        private static Tbl30Legio _legio;  
        private static Tbl33Ordo _ordo;   
        private static Tbl36Subordo _subordo;   
        private static Tbl39Infraordo _infraordo;  
        private static Tbl42Superfamily _superfamily;  
        private static Tbl45Family _family;  
        private static Tbl48Subfamily _subfamily;  
        private static Tbl51Infrafamily _infrafamily;  
        private static Tbl54Supertribus _supertribus;  
        private static Tbl57Tribus _tribus;  
        private static Tbl60Subtribus _subtribus;  
        private static Tbl63Infratribus _infratribus;   
        private static Tbl66Genus _genus;  
        private static Tbl69FiSpecies _fispecies;  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl72PlSpeciesses'">       <![CDATA[ 
        private static readonly Repository<Tbl72PlSpecies, int> Tbl72PlSpeciessesRepository = new Repository<Tbl72PlSpecies, int>();
        private static readonly Repository<Tbl66Genus, int> Tbl66GenussesRepository = new Repository<Tbl66Genus, int>();
        private static readonly Repository<Tbl63Infratribus, int> Tbl63InfratribussesRepository = new Repository<Tbl63Infratribus, int>();
        private static readonly Repository<Tbl60Subtribus, int> Tbl60SubtribussesRepository = new Repository<Tbl60Subtribus, int>();
        private static readonly Repository<Tbl57Tribus, int> Tbl57TribussesRepository = new Repository<Tbl57Tribus, int>();
        private static readonly Repository<Tbl54Supertribus, int> Tbl54SupertribussesRepository = new Repository<Tbl54Supertribus, int>();
        private static readonly Repository<Tbl51Infrafamily, int> Tbl51InfrafamiliesRepository = new Repository<Tbl51Infrafamily, int>();
        private static readonly Repository<Tbl48Subfamily, int> Tbl48SubfamiliesRepository = new Repository<Tbl48Subfamily, int>();
        private static readonly Repository<Tbl45Family, int> Tbl45FamiliesRepository = new Repository<Tbl45Family, int>();
        private static readonly Repository<Tbl42Superfamily, int> Tbl42SuperfamiliesRepository = new Repository<Tbl42Superfamily, int>();
        private static readonly Repository<Tbl39Infraordo, int> Tbl39InfraordosRepository = new Repository<Tbl39Infraordo, int>();
        private static readonly Repository<Tbl36Subordo, int> Tbl36SubordosRepository = new Repository<Tbl36Subordo, int>();
        private static readonly Repository<Tbl33Ordo, int> Tbl33OrdosRepository = new Repository<Tbl33Ordo, int>();
        private static readonly Repository<Tbl30Legio, int> Tbl30LegiosRepository = new Repository<Tbl30Legio, int>();
        private static readonly Repository<Tbl27Infraclass, int> Tbl27InfraclassesRepository = new Repository<Tbl27Infraclass, int>();
        private static readonly Repository<Tbl24Subclass, int> Tbl24SubclassesRepository = new Repository<Tbl24Subclass, int>();
        private static readonly Repository<Tbl21Class, int> Tbl21ClassesRepository = new Repository<Tbl21Class, int>();
        private static readonly Repository<Tbl18Superclass, int> Tbl18SuperclassesRepository = new Repository<Tbl18Superclass, int>();
        private static readonly Repository<Tbl15Subdivision, int> Tbl15SubdivisionsRepository = new Repository<Tbl15Subdivision, int>();
        private static readonly Repository<Tbl12Subphylum, int> Tbl12SubphylumsRepository = new Repository<Tbl12Subphylum, int>();
        private static readonly Repository<Tbl09Division, int> Tbl09DivisionsRepository = new Repository<Tbl09Division, int>();
        private static readonly Repository<Tbl06Phylum, int> Tbl06PhylumsRepository = new Repository<Tbl06Phylum, int>();
        private static readonly Repository<Tbl03Regnum, int> Tbl03RegnumsRepository = new Repository<Tbl03Regnum, int>(); 

        private static Tbl03Regnum _regnum;
        private static Tbl06Phylum _phylum;
        private static Tbl09Division _division;
        private static Tbl12Subphylum _subphylum;
        private static Tbl15Subdivision _subdivision;
        private static Tbl18Superclass _superclass; 
        private static Tbl21Class _class;   
        private static Tbl24Subclass _subclass;  
        private static Tbl27Infraclass _infraclass; 
        private static Tbl30Legio _legio;  
        private static Tbl33Ordo _ordo;   
        private static Tbl36Subordo _subordo;   
        private static Tbl39Infraordo _infraordo;  
        private static Tbl42Superfamily _superfamily;  
        private static Tbl45Family _family;  
        private static Tbl48Subfamily _subfamily;  
        private static Tbl51Infrafamily _infrafamily;  
        private static Tbl54Supertribus _supertribus;  
        private static Tbl57Tribus _tribus;  
        private static Tbl60Subtribus _subtribus;  
        private static Tbl63Infratribus _infratribus;   
        private static Tbl66Genus _genus;  
        private static Tbl72PlSpecies _plspecies;  ]]> 
</xsl:when>  
<xsl:otherwise>   
</xsl:otherwise>    
</xsl:choose> 

<![CDATA[ //    Part 1    ]]>

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  Haeder CreateMainPdf  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl03Regnums
            _regnum = Tbl03RegnumsRepository.Get(id);  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl06Phylums'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl06Phylums
            _phylum = Tbl06PhylumsRepository.Get(id);
            _regnum = Tbl03RegnumsRepository.Get(_phylum.RegnumID);  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl09Divisions'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl09Divisions
            _division = Tbl09DivisionsRepository.Get(id);
            _regnum = Tbl03RegnumsRepository.Get(_division.RegnumID);  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl12Subphylums'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl12Subphylums
            _subphylum = Tbl12SubphylumsRepository.Get(id);
            _phylum = Tbl06PhylumsRepository.Get(_subphylum.PhylumID);
            _regnum = Tbl03RegnumsRepository.Get(_phylum.RegnumID);  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl15Subdivisions'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl15Subdivisions
            _subdivision = Tbl15SubdivisionsRepository.Get(id);
            _division = Tbl09DivisionsRepository.Get(_subdivision.DivisionID);
            _regnum = Tbl03RegnumsRepository.Get(_division.RegnumID);  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl18Superclasses
            _superclass = Tbl18SuperclassesRepository.Get(id);
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                _subdivision = Tbl15SubdivisionsRepository.Get(_superclass.SubdivisionID);
                _division = Tbl09DivisionsRepository.Get(_subdivision.DivisionID);
                _regnum = Tbl03RegnumsRepository.Get(_division.RegnumID);
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                _subphylum = Tbl12SubphylumsRepository.Get(_superclass.SubphylumID);
                _phylum = Tbl06PhylumsRepository.Get(_subphylum.PhylumID);
                _regnum = Tbl03RegnumsRepository.Get(_phylum.RegnumID);
            } ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl21Classes'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl21Classes
            _class = Tbl21ClassesRepository.Get(id);
            _superclass = Tbl18SuperclassesRepository.Get(_class.SuperclassID);
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                _subdivision = Tbl15SubdivisionsRepository.Get(_superclass.SubdivisionID);
                _division = Tbl09DivisionsRepository.Get(_subdivision.DivisionID);
                _regnum = Tbl03RegnumsRepository.Get(_division.RegnumID);
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                _subphylum = Tbl12SubphylumsRepository.Get(_superclass.SubphylumID);
                _phylum = Tbl06PhylumsRepository.Get(_subphylum.PhylumID);
                _regnum = Tbl03RegnumsRepository.Get(_phylum.RegnumID);
            }  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl24Subclasses'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl24Subclasses
            _subclass = Tbl24SubclassesRepository.Get(id);
            _class = Tbl21ClassesRepository.Get(_subclass.ClassID);
            _superclass = Tbl18SuperclassesRepository.Get(_class.SuperclassID);
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                _subdivision = Tbl15SubdivisionsRepository.Get(_superclass.SubdivisionID);
                _division = Tbl09DivisionsRepository.Get(_subdivision.DivisionID);
                _regnum = Tbl03RegnumsRepository.Get(_division.RegnumID);
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                _subphylum = Tbl12SubphylumsRepository.Get(_superclass.SubphylumID);
                _phylum = Tbl06PhylumsRepository.Get(_subphylum.PhylumID);
                _regnum = Tbl03RegnumsRepository.Get(_phylum.RegnumID);
            }  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl27Infraclasses'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl27Infraclasses
            _infraclass = Tbl27InfraclassesRepository.Get(id);
            _subclass = Tbl24SubclassesRepository.Get(_infraclass.SubclassID);
            _class = Tbl21ClassesRepository.Get(_subclass.ClassID);
            _superclass = Tbl18SuperclassesRepository.Get(_class.SuperclassID);
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                _subdivision = Tbl15SubdivisionsRepository.Get(_superclass.SubdivisionID);
                _division = Tbl09DivisionsRepository.Get(_subdivision.DivisionID);
                _regnum = Tbl03RegnumsRepository.Get(_division.RegnumID);
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                _subphylum = Tbl12SubphylumsRepository.Get(_superclass.SubphylumID);
                _phylum = Tbl06PhylumsRepository.Get(_subphylum.PhylumID);
                _regnum = Tbl03RegnumsRepository.Get(_phylum.RegnumID);
            }  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl30Legios'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl30Legios
            _legio = Tbl30LegiosRepository.Get(id);
            _infraclass = Tbl27InfraclassesRepository.Get(_legio.InfraclassID);
            _subclass = Tbl24SubclassesRepository.Get(_infraclass.SubclassID);
            _class = Tbl21ClassesRepository.Get(_subclass.ClassID);
            _superclass = Tbl18SuperclassesRepository.Get(_class.SuperclassID);
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                _subdivision = Tbl15SubdivisionsRepository.Get(_superclass.SubdivisionID);
                _division = Tbl09DivisionsRepository.Get(_subdivision.DivisionID);
                _regnum = Tbl03RegnumsRepository.Get(_division.RegnumID);
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                _subphylum = Tbl12SubphylumsRepository.Get(_superclass.SubphylumID);
                _phylum = Tbl06PhylumsRepository.Get(_subphylum.PhylumID);
                _regnum = Tbl03RegnumsRepository.Get(_phylum.RegnumID);
            }  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl33Ordos'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl33Ordos
            _ordo = Tbl33OrdosRepository.Get(id);
            _legio = Tbl30LegiosRepository.Get(_ordo.LegioID);
            _infraclass = Tbl27InfraclassesRepository.Get(_legio.InfraclassID);
            _subclass = Tbl24SubclassesRepository.Get(_infraclass.SubclassID);
            _class = Tbl21ClassesRepository.Get(_subclass.ClassID);
            _superclass = Tbl18SuperclassesRepository.Get(_class.SuperclassID);
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                _subdivision = Tbl15SubdivisionsRepository.Get(_superclass.SubdivisionID);
                _division = Tbl09DivisionsRepository.Get(_subdivision.DivisionID);
                _regnum = Tbl03RegnumsRepository.Get(_division.RegnumID);
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                _subphylum = Tbl12SubphylumsRepository.Get(_superclass.SubphylumID);
                _phylum = Tbl06PhylumsRepository.Get(_subphylum.PhylumID);
                _regnum = Tbl03RegnumsRepository.Get(_phylum.RegnumID);
            }  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl36Subordos'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl36Subordos
            _subordo = Tbl36SubordosRepository.Get(id);
            _ordo = Tbl33OrdosRepository.Get(_subordo.OrdoID);
            _legio = Tbl30LegiosRepository.Get(_ordo.LegioID);
            _infraclass = Tbl27InfraclassesRepository.Get(_legio.InfraclassID);
            _subclass = Tbl24SubclassesRepository.Get(_infraclass.SubclassID);
            _class = Tbl21ClassesRepository.Get(_subclass.ClassID);
            _superclass = Tbl18SuperclassesRepository.Get(_class.SuperclassID);
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                _subdivision = Tbl15SubdivisionsRepository.Get(_superclass.SubdivisionID);
                _division = Tbl09DivisionsRepository.Get(_subdivision.DivisionID);
                _regnum = Tbl03RegnumsRepository.Get(_division.RegnumID);
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                _subphylum = Tbl12SubphylumsRepository.Get(_superclass.SubphylumID);
                _phylum = Tbl06PhylumsRepository.Get(_subphylum.PhylumID);
                _regnum = Tbl03RegnumsRepository.Get(_phylum.RegnumID);
            }  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl39Infraordos'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl39Infraordos
            _infraordo = Tbl39InfraordosRepository.Get(id);
            _subordo = Tbl36SubordosRepository.Get(_infraordo.SubordoID);
            _ordo = Tbl33OrdosRepository.Get(_subordo.OrdoID);
            _legio = Tbl30LegiosRepository.Get(_ordo.LegioID);
            _infraclass = Tbl27InfraclassesRepository.Get(_legio.InfraclassID);
            _subclass = Tbl24SubclassesRepository.Get(_infraclass.SubclassID);
            _class = Tbl21ClassesRepository.Get(_subclass.ClassID);
            _superclass = Tbl18SuperclassesRepository.Get(_class.SuperclassID);
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                _subdivision = Tbl15SubdivisionsRepository.Get(_superclass.SubdivisionID);
                _division = Tbl09DivisionsRepository.Get(_subdivision.DivisionID);
                _regnum = Tbl03RegnumsRepository.Get(_division.RegnumID);
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                _subphylum = Tbl12SubphylumsRepository.Get(_superclass.SubphylumID);
                _phylum = Tbl06PhylumsRepository.Get(_subphylum.PhylumID);
                _regnum = Tbl03RegnumsRepository.Get(_phylum.RegnumID);
            }  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl42Superfamilies'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl42Superfamilies
            _superfamily = Tbl42SuperfamiliesRepository.Get(id);
            _infraordo = Tbl39InfraordosRepository.Get(_superfamily.InfraordoID);
            _subordo = Tbl36SubordosRepository.Get(_infraordo.SubordoID);
            _ordo = Tbl33OrdosRepository.Get(_subordo.OrdoID);
            _legio = Tbl30LegiosRepository.Get(_ordo.LegioID);
            _infraclass = Tbl27InfraclassesRepository.Get(_legio.InfraclassID);
            _subclass = Tbl24SubclassesRepository.Get(_infraclass.SubclassID);
            _class = Tbl21ClassesRepository.Get(_subclass.ClassID);
            _superclass = Tbl18SuperclassesRepository.Get(_class.SuperclassID);
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                _subdivision = Tbl15SubdivisionsRepository.Get(_superclass.SubdivisionID);
                _division = Tbl09DivisionsRepository.Get(_subdivision.DivisionID);
                _regnum = Tbl03RegnumsRepository.Get(_division.RegnumID);
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                _subphylum = Tbl12SubphylumsRepository.Get(_superclass.SubphylumID);
                _phylum = Tbl06PhylumsRepository.Get(_subphylum.PhylumID);
                _regnum = Tbl03RegnumsRepository.Get(_phylum.RegnumID);
            }  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl45Families'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl45Families
            _family = Tbl45FamiliesRepository.Get(id);
            _superfamily = Tbl42SuperfamiliesRepository.Get(_family.SuperfamilyID);
            _infraordo = Tbl39InfraordosRepository.Get(_superfamily.InfraordoID);
            _subordo = Tbl36SubordosRepository.Get(_infraordo.SubordoID);
            _ordo = Tbl33OrdosRepository.Get(_subordo.OrdoID);
            _legio = Tbl30LegiosRepository.Get(_ordo.LegioID);
            _infraclass = Tbl27InfraclassesRepository.Get(_legio.InfraclassID);
            _subclass = Tbl24SubclassesRepository.Get(_infraclass.SubclassID);
            _class = Tbl21ClassesRepository.Get(_subclass.ClassID);
            _superclass = Tbl18SuperclassesRepository.Get(_class.SuperclassID);
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                _subdivision = Tbl15SubdivisionsRepository.Get(_superclass.SubdivisionID);
                _division = Tbl09DivisionsRepository.Get(_subdivision.DivisionID);
                _regnum = Tbl03RegnumsRepository.Get(_division.RegnumID);
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                _subphylum = Tbl12SubphylumsRepository.Get(_superclass.SubphylumID);
                _phylum = Tbl06PhylumsRepository.Get(_subphylum.PhylumID);
                _regnum = Tbl03RegnumsRepository.Get(_phylum.RegnumID);
            }  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl48Subfamilies'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl48Subfamilies
            _subfamily = Tbl48SubfamiliesRepository.Get(id);
            _family = Tbl45FamiliesRepository.Get(_subfamily.FamilyID);
            _superfamily = Tbl42SuperfamiliesRepository.Get(_family.SuperfamilyID);
            _infraordo = Tbl39InfraordosRepository.Get(_superfamily.InfraordoID);
            _subordo = Tbl36SubordosRepository.Get(_infraordo.SubordoID);
            _ordo = Tbl33OrdosRepository.Get(_subordo.OrdoID);
            _legio = Tbl30LegiosRepository.Get(_ordo.LegioID);
            _infraclass = Tbl27InfraclassesRepository.Get(_legio.InfraclassID);
            _subclass = Tbl24SubclassesRepository.Get(_infraclass.SubclassID);
            _class = Tbl21ClassesRepository.Get(_subclass.ClassID);
            _superclass = Tbl18SuperclassesRepository.Get(_class.SuperclassID);
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                _subdivision = Tbl15SubdivisionsRepository.Get(_superclass.SubdivisionID);
                _division = Tbl09DivisionsRepository.Get(_subdivision.DivisionID);
                _regnum = Tbl03RegnumsRepository.Get(_division.RegnumID);
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                _subphylum = Tbl12SubphylumsRepository.Get(_superclass.SubphylumID);
                _phylum = Tbl06PhylumsRepository.Get(_subphylum.PhylumID);
                _regnum = Tbl03RegnumsRepository.Get(_phylum.RegnumID);
            }  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl51Infrafamilies'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl51Infrafamilies
            _infrafamily = Tbl51InfrafamiliesRepository.Get(id);
            _subfamily = Tbl48SubfamiliesRepository.Get(_infrafamily.SubfamilyID);
            _family = Tbl45FamiliesRepository.Get(_subfamily.FamilyID);
            _superfamily = Tbl42SuperfamiliesRepository.Get(_family.SuperfamilyID);
            _infraordo = Tbl39InfraordosRepository.Get(_superfamily.InfraordoID);
            _subordo = Tbl36SubordosRepository.Get(_infraordo.SubordoID);
            _ordo = Tbl33OrdosRepository.Get(_subordo.OrdoID);
            _legio = Tbl30LegiosRepository.Get(_ordo.LegioID);
            _infraclass = Tbl27InfraclassesRepository.Get(_legio.InfraclassID);
            _subclass = Tbl24SubclassesRepository.Get(_infraclass.SubclassID);
            _class = Tbl21ClassesRepository.Get(_subclass.ClassID);
            _superclass = Tbl18SuperclassesRepository.Get(_class.SuperclassID);
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                _subdivision = Tbl15SubdivisionsRepository.Get(_superclass.SubdivisionID);
                _division = Tbl09DivisionsRepository.Get(_subdivision.DivisionID);
                _regnum = Tbl03RegnumsRepository.Get(_division.RegnumID);
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                _subphylum = Tbl12SubphylumsRepository.Get(_superclass.SubphylumID);
                _phylum = Tbl06PhylumsRepository.Get(_subphylum.PhylumID);
                _regnum = Tbl03RegnumsRepository.Get(_phylum.RegnumID);
            }  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl54Supertribusses'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl54Supertribusses
            _supertribus = Tbl54SupertribussesRepository.Get(id);
            _infrafamily = Tbl51InfrafamiliesRepository.Get(_supertribus.InfrafamilyID);
            _subfamily = Tbl48SubfamiliesRepository.Get(_infrafamily.SubfamilyID);
            _family = Tbl45FamiliesRepository.Get(_subfamily.FamilyID);
            _superfamily = Tbl42SuperfamiliesRepository.Get(_family.SuperfamilyID);
            _infraordo = Tbl39InfraordosRepository.Get(_superfamily.InfraordoID);
            _subordo = Tbl36SubordosRepository.Get(_infraordo.SubordoID);
            _ordo = Tbl33OrdosRepository.Get(_subordo.OrdoID);
            _legio = Tbl30LegiosRepository.Get(_ordo.LegioID);
            _infraclass = Tbl27InfraclassesRepository.Get(_legio.InfraclassID);
            _subclass = Tbl24SubclassesRepository.Get(_infraclass.SubclassID);
            _class = Tbl21ClassesRepository.Get(_subclass.ClassID);
            _superclass = Tbl18SuperclassesRepository.Get(_class.SuperclassID);
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                _subdivision = Tbl15SubdivisionsRepository.Get(_superclass.SubdivisionID);
                _division = Tbl09DivisionsRepository.Get(_subdivision.DivisionID);
                _regnum = Tbl03RegnumsRepository.Get(_division.RegnumID);
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                _subphylum = Tbl12SubphylumsRepository.Get(_superclass.SubphylumID);
                _phylum = Tbl06PhylumsRepository.Get(_subphylum.PhylumID);
                _regnum = Tbl03RegnumsRepository.Get(_phylum.RegnumID);
            }  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl57Tribusses'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl57Tribusses
            _tribus = Tbl57TribussesRepository.Get(id);
            _supertribus = Tbl54SupertribussesRepository.Get(_tribus.SupertribusID);
            _infrafamily = Tbl51InfrafamiliesRepository.Get(_supertribus.InfrafamilyID);
            _subfamily = Tbl48SubfamiliesRepository.Get(_infrafamily.SubfamilyID);
            _family = Tbl45FamiliesRepository.Get(_subfamily.FamilyID);
            _superfamily = Tbl42SuperfamiliesRepository.Get(_family.SuperfamilyID);
            _infraordo = Tbl39InfraordosRepository.Get(_superfamily.InfraordoID);
            _subordo = Tbl36SubordosRepository.Get(_infraordo.SubordoID);
            _ordo = Tbl33OrdosRepository.Get(_subordo.OrdoID);
            _legio = Tbl30LegiosRepository.Get(_ordo.LegioID);
            _infraclass = Tbl27InfraclassesRepository.Get(_legio.InfraclassID);
            _subclass = Tbl24SubclassesRepository.Get(_infraclass.SubclassID);
            _class = Tbl21ClassesRepository.Get(_subclass.ClassID);
            _superclass = Tbl18SuperclassesRepository.Get(_class.SuperclassID);
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                _subdivision = Tbl15SubdivisionsRepository.Get(_superclass.SubdivisionID);
                _division = Tbl09DivisionsRepository.Get(_subdivision.DivisionID);
                _regnum = Tbl03RegnumsRepository.Get(_division.RegnumID);
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                _subphylum = Tbl12SubphylumsRepository.Get(_superclass.SubphylumID);
                _phylum = Tbl06PhylumsRepository.Get(_subphylum.PhylumID);
                _regnum = Tbl03RegnumsRepository.Get(_phylum.RegnumID);
            }  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl60Subtribusses'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl60Subtribusses
            _subtribus = Tbl60SubtribussesRepository.Get(id);
            _tribus = Tbl57TribussesRepository.Get(_subtribus.TribusID);
            _supertribus = Tbl54SupertribussesRepository.Get(_tribus.SupertribusID);
            _infrafamily = Tbl51InfrafamiliesRepository.Get(_supertribus.InfrafamilyID);
            _subfamily = Tbl48SubfamiliesRepository.Get(_infrafamily.SubfamilyID);
            _family = Tbl45FamiliesRepository.Get(_subfamily.FamilyID);
            _superfamily = Tbl42SuperfamiliesRepository.Get(_family.SuperfamilyID);
            _infraordo = Tbl39InfraordosRepository.Get(_superfamily.InfraordoID);
            _subordo = Tbl36SubordosRepository.Get(_infraordo.SubordoID);
            _ordo = Tbl33OrdosRepository.Get(_subordo.OrdoID);
            _legio = Tbl30LegiosRepository.Get(_ordo.LegioID);
            _infraclass = Tbl27InfraclassesRepository.Get(_legio.InfraclassID);
            _subclass = Tbl24SubclassesRepository.Get(_infraclass.SubclassID);
            _class = Tbl21ClassesRepository.Get(_subclass.ClassID);
            _superclass = Tbl18SuperclassesRepository.Get(_class.SuperclassID);
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                _subdivision = Tbl15SubdivisionsRepository.Get(_superclass.SubdivisionID);
                _division = Tbl09DivisionsRepository.Get(_subdivision.DivisionID);
                _regnum = Tbl03RegnumsRepository.Get(_division.RegnumID);
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                _subphylum = Tbl12SubphylumsRepository.Get(_superclass.SubphylumID);
                _phylum = Tbl06PhylumsRepository.Get(_subphylum.PhylumID);
                _regnum = Tbl03RegnumsRepository.Get(_phylum.RegnumID);
            }  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl63Infratribusses'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl63Infratribusses
            _infratribus = Tbl63InfratribussesRepository.Get(id);
            _subtribus = Tbl60SubtribussesRepository.Get(_infratribus.SubtribusID);
            _tribus = Tbl57TribussesRepository.Get(_subtribus.TribusID);
            _supertribus = Tbl54SupertribussesRepository.Get(_tribus.SupertribusID);
            _infrafamily = Tbl51InfrafamiliesRepository.Get(_supertribus.InfrafamilyID);
            _subfamily = Tbl48SubfamiliesRepository.Get(_infrafamily.SubfamilyID);
            _family = Tbl45FamiliesRepository.Get(_subfamily.FamilyID);
            _superfamily = Tbl42SuperfamiliesRepository.Get(_family.SuperfamilyID);
            _infraordo = Tbl39InfraordosRepository.Get(_superfamily.InfraordoID);
            _subordo = Tbl36SubordosRepository.Get(_infraordo.SubordoID);
            _ordo = Tbl33OrdosRepository.Get(_subordo.OrdoID);
            _legio = Tbl30LegiosRepository.Get(_ordo.LegioID);
            _infraclass = Tbl27InfraclassesRepository.Get(_legio.InfraclassID);
            _subclass = Tbl24SubclassesRepository.Get(_infraclass.SubclassID);
            _class = Tbl21ClassesRepository.Get(_subclass.ClassID);
            _superclass = Tbl18SuperclassesRepository.Get(_class.SuperclassID);
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                _subdivision = Tbl15SubdivisionsRepository.Get(_superclass.SubdivisionID);
                _division = Tbl09DivisionsRepository.Get(_subdivision.DivisionID);
                _regnum = Tbl03RegnumsRepository.Get(_division.RegnumID);
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                _subphylum = Tbl12SubphylumsRepository.Get(_superclass.SubphylumID);
                _phylum = Tbl06PhylumsRepository.Get(_subphylum.PhylumID);
                _regnum = Tbl03RegnumsRepository.Get(_phylum.RegnumID);
            }  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl66Genusses'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl66Genusses
            _genus = Tbl66GenussesRepository.Get(id);
            _infratribus = Tbl63InfratribussesRepository.Get(_genus.InfratribusID);
            _subtribus = Tbl60SubtribussesRepository.Get(_infratribus.SubtribusID);
            _tribus = Tbl57TribussesRepository.Get(_subtribus.TribusID);
            _supertribus = Tbl54SupertribussesRepository.Get(_tribus.SupertribusID);
            _infrafamily = Tbl51InfrafamiliesRepository.Get(_supertribus.InfrafamilyID);
            _subfamily = Tbl48SubfamiliesRepository.Get(_infrafamily.SubfamilyID);
            _family = Tbl45FamiliesRepository.Get(_subfamily.FamilyID);
            _superfamily = Tbl42SuperfamiliesRepository.Get(_family.SuperfamilyID);
            _infraordo = Tbl39InfraordosRepository.Get(_superfamily.InfraordoID);
            _subordo = Tbl36SubordosRepository.Get(_infraordo.SubordoID);
            _ordo = Tbl33OrdosRepository.Get(_subordo.OrdoID);
            _legio = Tbl30LegiosRepository.Get(_ordo.LegioID);
            _infraclass = Tbl27InfraclassesRepository.Get(_legio.InfraclassID);
            _subclass = Tbl24SubclassesRepository.Get(_infraclass.SubclassID);
            _class = Tbl21ClassesRepository.Get(_subclass.ClassID);
            _superclass = Tbl18SuperclassesRepository.Get(_class.SuperclassID);
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                _subdivision = Tbl15SubdivisionsRepository.Get(_superclass.SubdivisionID);
                _division = Tbl09DivisionsRepository.Get(_subdivision.DivisionID);
                _regnum = Tbl03RegnumsRepository.Get(_division.RegnumID);
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                _subphylum = Tbl12SubphylumsRepository.Get(_superclass.SubphylumID);
                _phylum = Tbl06PhylumsRepository.Get(_subphylum.PhylumID);
                _regnum = Tbl03RegnumsRepository.Get(_phylum.RegnumID);
            }  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl69FiSpeciesses'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl69FiSpeciesses
            _fispecies = Tbl69FiSpeciessesRepository.Get(id);
            _genus = Tbl66GenussesRepository.Get(_fispecies.GenusID);
            _infratribus = Tbl63InfratribussesRepository.Get(_genus.InfratribusID);
            _subtribus = Tbl60SubtribussesRepository.Get(_infratribus.SubtribusID);
            _tribus = Tbl57TribussesRepository.Get(_subtribus.TribusID);
            _supertribus = Tbl54SupertribussesRepository.Get(_tribus.SupertribusID);
            _infrafamily = Tbl51InfrafamiliesRepository.Get(_supertribus.InfrafamilyID);
            _subfamily = Tbl48SubfamiliesRepository.Get(_infrafamily.SubfamilyID);
            _family = Tbl45FamiliesRepository.Get(_subfamily.FamilyID);
            _superfamily = Tbl42SuperfamiliesRepository.Get(_family.SuperfamilyID);
            _infraordo = Tbl39InfraordosRepository.Get(_superfamily.InfraordoID);
            _subordo = Tbl36SubordosRepository.Get(_infraordo.SubordoID);
            _ordo = Tbl33OrdosRepository.Get(_subordo.OrdoID);
            _legio = Tbl30LegiosRepository.Get(_ordo.LegioID);
            _infraclass = Tbl27InfraclassesRepository.Get(_legio.InfraclassID);
            _subclass = Tbl24SubclassesRepository.Get(_infraclass.SubclassID);
            _class = Tbl21ClassesRepository.Get(_subclass.ClassID);
            _superclass = Tbl18SuperclassesRepository.Get(_class.SuperclassID);
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                _subdivision = Tbl15SubdivisionsRepository.Get(_superclass.SubdivisionID);
                _division = Tbl09DivisionsRepository.Get(_subdivision.DivisionID);
                _regnum = Tbl03RegnumsRepository.Get(_division.RegnumID);
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                _subphylum = Tbl12SubphylumsRepository.Get(_superclass.SubphylumID);
                _phylum = Tbl06PhylumsRepository.Get(_subphylum.PhylumID);
                _regnum = Tbl03RegnumsRepository.Get(_phylum.RegnumID);
            }  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl72PlSpeciesses'">       <![CDATA[ 
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 

            //From Database Tbl72PlSpeciesses
            _plspecies = Tbl72PlSpeciessesRepository.Get(id);
            _genus = Tbl66GenussesRepository.Get(_plspecies.GenusID);
            _infratribus = Tbl63InfratribussesRepository.Get(_genus.InfratribusID);
            _subtribus = Tbl60SubtribussesRepository.Get(_infratribus.SubtribusID);
            _tribus = Tbl57TribussesRepository.Get(_subtribus.TribusID);
            _supertribus = Tbl54SupertribussesRepository.Get(_tribus.SupertribusID);
            _infrafamily = Tbl51InfrafamiliesRepository.Get(_supertribus.InfrafamilyID);
            _subfamily = Tbl48SubfamiliesRepository.Get(_infrafamily.SubfamilyID);
            _family = Tbl45FamiliesRepository.Get(_subfamily.FamilyID);
            _superfamily = Tbl42SuperfamiliesRepository.Get(_family.SuperfamilyID);
            _infraordo = Tbl39InfraordosRepository.Get(_superfamily.InfraordoID);
            _subordo = Tbl36SubordosRepository.Get(_infraordo.SubordoID);
            _ordo = Tbl33OrdosRepository.Get(_subordo.OrdoID);
            _legio = Tbl30LegiosRepository.Get(_ordo.LegioID);
            _infraclass = Tbl27InfraclassesRepository.Get(_legio.InfraclassID);
            _subclass = Tbl24SubclassesRepository.Get(_infraclass.SubclassID);
            _class = Tbl21ClassesRepository.Get(_subclass.ClassID);
            _superclass = Tbl18SuperclassesRepository.Get(_class.SuperclassID);
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                _subdivision = Tbl15SubdivisionsRepository.Get(_superclass.SubdivisionID);
                _division = Tbl09DivisionsRepository.Get(_subdivision.DivisionID);
                _regnum = Tbl03RegnumsRepository.Get(_division.RegnumID);
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                _subphylum = Tbl12SubphylumsRepository.Get(_superclass.SubphylumID);
                _phylum = Tbl06PhylumsRepository.Get(_subphylum.PhylumID);
                _regnum = Tbl03RegnumsRepository.Get(_phylum.RegnumID);
            }  ]]> 
</xsl:when>  
<xsl:otherwise>      <![CDATA[       
        public static void CreateMainPdf(int id)
        {
           var reportVm = new ReportViewModel();
            reportVm.Get]]><xsl:value-of select="Table"/><![CDATA[ById(id); 
  ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  Haeder CreateMainPdf  Top  2a ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:otherwise>       <![CDATA[      

            var sfd = new SaveFileDialog { Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*" };
            var saveResult = sfd.ShowDialog();
            if (saveResult != true) return;  //exit
            Document doc = null; 

            try
            { ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  Haeder CreateMainPdf  Top   2 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">        <![CDATA[        
                doc = PdfHelper.HeaderMainPdf(sfd);
                // Add pages to the document
                PdfHelper.AddReportMain(doc);

                doc = AddTbl69FiSpeciessesHaeder(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportTaxoNomen));

                doc = AddTbl69FiSpeciessesTaxoNomenRankList(doc);
                if (reportVm.Tbl84SynonymsList.Count != 0)
                    doc = PdfHelper.AddTbl84SynonymsList(doc, reportVm.Tbl84SynonymsList);
                if (reportVm.Tbl78NamesList.Count != 0)
                    doc = PdfHelper.AddTbl78NamesList(doc, reportVm.Tbl78NamesList);
                doc = AddTbl69FiSpeciessesTaxoNomenStatusList(doc);
                doc = AddTbl69FiSpeciessesSpecificationList(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportTaxoHiera));

                doc = AddHierarchyList(doc);  
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportReferences));
                if (reportVm.Tbl90ExpertsList.Count != 0)
                    doc = PdfHelper.AddRefExpertList(doc, reportVm.Tbl90RefExpertsList);
                if (reportVm.Tbl90SourcesList.Count != 0)
                    doc = PdfHelper.AddRefSourceList(doc, reportVm.Tbl90RefSourcesList);
                if (reportVm.Tbl90AuthorsList.Count != 0)
                    doc = PdfHelper.AddRefAuthorList(doc, reportVm.Tbl90RefAuthorsList); ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">        <![CDATA[        
                doc = PdfHelper.HeaderMainPdf(sfd);
                // Add pages to the document
                PdfHelper.AddReportMain(doc);

                doc = AddTbl72PlSpeciessesHaeder(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportTaxoNomen));

                doc = AddTbl72PlSpeciessesTaxoNomenRankList(doc);
                if (reportVm.Tbl84SynonymsList.Count != 0)
                    doc = PdfHelper.AddTbl84SynonymsList(doc, reportVm.Tbl84SynonymsList);
                if (reportVm.Tbl78NamesList.Count != 0)
                    doc = PdfHelper.AddTbl78NamesList(doc, reportVm.Tbl78NamesList);
                doc = AddTbl72PlSpeciessesTaxoNomenStatusList(doc);
                doc = AddTbl72PlSpeciessesSpecificationList(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportTaxoHiera));

                doc = AddHierarchyList(doc);  
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportReferences));
                if (reportVm.Tbl90ExpertsList.Count != 0)
                    doc = PdfHelper.AddRefExpertList(doc, reportVm.Tbl90RefExpertsList);
                if (reportVm.Tbl90SourcesList.Count != 0)
                    doc = PdfHelper.AddRefSourceList(doc, reportVm.Tbl90RefSourcesList);
                if (reportVm.Tbl90AuthorsList.Count != 0)
                    doc = PdfHelper.AddRefAuthorList(doc, reportVm.Tbl90RefAuthorsList); ]]>
</xsl:when>
<xsl:otherwise>       <![CDATA[      
                doc = PdfHelper.HeaderMainPdf(sfd);
                // Add pages to the document
                PdfHelper.AddReportMain(doc);

                doc = Add]]><xsl:value-of select="Table"/><![CDATA[Haeder(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportTaxoNomen));
                doc = Add]]><xsl:value-of select="Table"/><![CDATA[TaxoNomenList(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportTaxoHiera));

                doc = AddHierarchyList(doc); ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  Haeder CreateMainPdf  Top 3 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">        <![CDATA[        
                if (reportVm.]]><xsl:value-of select="TableTK1"/><![CDATA[List.Count != 0)
                    doc = Add]]><xsl:value-of select="TableTK1"/><![CDATA[ChildrenList(doc, reportVm.]]><xsl:value-of select="TableTK1"/><![CDATA[List);
                if (reportVm.]]><xsl:value-of select="TableTK2"/><![CDATA[List.Count != 0)
                    doc = Add]]><xsl:value-of select="TableTK2"/><![CDATA[ChildrenList(doc, reportVm.]]><xsl:value-of select="TableTK2"/><![CDATA[List); ]]>
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">     
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">     
</xsl:when>
<xsl:otherwise>       <![CDATA[      
                if (reportVm.]]><xsl:value-of select="TableTK1"/><![CDATA[List.Count != 0)
                    doc = Add]]><xsl:value-of select="TableTK1"/><![CDATA[ChildrenList(doc, reportVm.]]><xsl:value-of select="TableTK1"/><![CDATA[List); ]]>
</xsl:otherwise>    
</xsl:choose> 


<xsl:choose>
<xsl:when test="Table ='Public Commands 1  Haeder CreateMainPdf  Top 4 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">        <![CDATA[    
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportGeographics));
                if (reportVm.Tbl87GeographicsList.Count != 0)
                    doc = PdfHelper.AddTbl87GeographicsList(doc, reportVm.Tbl87GeographicsList);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportImages));
                if (reportVm.Tbl81ImagesList.Count != 0)
                    doc = PdfHelper.AddTbl81ImagesList(doc, reportVm.Tbl81ImagesList);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportComments));
                if (reportVm.Tbl93CommentsList.Count != 0)
                    doc = PdfHelper.AddCommentList(doc, reportVm.Tbl93CommentsList);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportTechnic));
                doc = AddTbl69FiSpeciessesTechnicList(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportFood));
                doc = AddTbl69FiSpeciessesFoodList(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportGu));
                doc = AddTbl69FiSpeciessesGuList(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportSozial));
                doc = AddTbl69FiSpeciessesSozialList(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportHusbandry));
                doc = AddTbl69FiSpeciessesHusbandryList(doc);         ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">        <![CDATA[    
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportGeographics));
                if (reportVm.Tbl87GeographicsList.Count != 0)
                    doc = PdfHelper.AddTbl87GeographicsList(doc, reportVm.Tbl87GeographicsList);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportImages));
                if (reportVm.Tbl81ImagesList.Count != 0)
                    doc = PdfHelper.AddTbl81ImagesList(doc, reportVm.Tbl81ImagesList);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportComments));
                if (reportVm.Tbl93CommentsList.Count != 0)
                    doc = PdfHelper.AddCommentList(doc, reportVm.Tbl93CommentsList);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportTechnic));
                doc = AddTbl72PlSpeciessesTechnicList(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportReproduction));
                doc = AddTbl72PlSpeciessesReproductionList(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportGlobal));
                doc = AddTbl72PlSpeciessesGlobalList(doc);
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportCulture));
                doc = AddTbl72PlSpeciessesCultureList(doc);   ]]>
</xsl:when>
<xsl:otherwise>    <![CDATA[      
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportReferences));
                if (reportVm.Tbl90ExpertsList.Count != 0)
                    doc = PdfHelper.AddRefExpertList(doc, reportVm.Tbl90RefExpertsList);
                if (reportVm.Tbl90SourcesList.Count != 0)
                    doc = PdfHelper.AddRefSourceList(doc, reportVm.Tbl90RefSourcesList);
                if (reportVm.Tbl90AuthorsList.Count != 0)
                    doc = PdfHelper.AddRefAuthorList(doc, reportVm.Tbl90RefAuthorsList);
           //     PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportGeographics));
           //     PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportImages));
                PdfHelper.AddParagraph(doc, Element.ALIGN_LEFT, LargeFont, new Chunk(CultRes.StringsRes.ReportComments));
                if (reportVm.Tbl93CommentsList.Count != 0)
                    doc = PdfHelper.AddCommentList(doc, reportVm.Tbl93CommentsList); ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  Haeder CreateMainPdf  Top 4 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:otherwise>    <![CDATA[      
            }
            catch (DocumentException)
            {
                // Handle iTextSharp errors
            }
            finally
            {
                // Clean up
                doc?.Close();
                doc = null;
            }
        }  ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddTbl..Haeder  Top 1 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:otherwise>    <![CDATA[      
        private static Document Add]]><xsl:value-of select="Table"/><![CDATA[Haeder(Document doc)       
        {
            // Add a new page to the document
            doc.NewPage();

            var table = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 10f   //fix the absolute width of the table
            };
            table.SetWidths(new[] { 0.05f, 0.05f, 1.25f, 4.00f });  ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddTbl..Haeder  Top 2 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">        <![CDATA[        
            table.AddCell(new PdfPCell(new Phrase(_]]><xsl:value-of select="BasisSm"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + "  " + _]]><xsl:value-of select="BasisSm"/><![CDATA[.Subregnum + "  " + _]]><xsl:value-of select="BasisSm"/><![CDATA[.Author + "  " + _]]><xsl:value-of select="BasisSm"/><![CDATA[.AuthorYear, LargeFont)) { Colspan = 4, Border = 0 });  // 1.-4. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxonomicId + " " + Convert.ToString(_]]><xsl:value-of select="BasisSm"/><![CDATA[.CountID), StandardFont)) { Colspan = 4, Border = 0 }); // 1.-4. field
            doc.Add(table);
            return doc;
        }  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">        <![CDATA[        
            table.AddCell(new PdfPCell(new Phrase(_]]><xsl:value-of select="BasisSm"/><![CDATA[.Tbl66Genusses.GenusName + "  " + _]]><xsl:value-of select="BasisSm"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + "  " + _]]><xsl:value-of select="BasisSm"/><![CDATA[.Subspecies + "  " + _]]><xsl:value-of select="BasisSm"/><![CDATA[.Divers + "  " + _]]><xsl:value-of select="BasisSm"/><![CDATA[.Author + "  " + _]]><xsl:value-of select="BasisSm"/><![CDATA[.AuthorYear, LargeFont)) { Colspan = 4, Border = 0 });  // 1.-4. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxonomicId + " " + Convert.ToString(_]]><xsl:value-of select="BasisSm"/><![CDATA[.CountID), StandardFont)) { Colspan = 4, Border = 0 }); // 1.-4. field
            doc.Add(table);
            return doc;
        }  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">        <![CDATA[        
            table.AddCell(new PdfPCell(new Phrase(_]]><xsl:value-of select="BasisSm"/><![CDATA[.Tbl66Genusses.GenusName + "  " + _]]><xsl:value-of select="BasisSm"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + "  " + _]]><xsl:value-of select="BasisSm"/><![CDATA[.Subspecies + "  " + _]]><xsl:value-of select="BasisSm"/><![CDATA[.Divers + "  " + _]]><xsl:value-of select="BasisSm"/><![CDATA[.Author + "  " + _]]><xsl:value-of select="BasisSm"/><![CDATA[.AuthorYear, LargeFont)) { Colspan = 4, Border = 0 });  // 1.-4. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxonomicId + " " + Convert.ToString(_]]><xsl:value-of select="BasisSm"/><![CDATA[.CountID), StandardFont)) { Colspan = 4, Border = 0 }); // 1.-4. field
            doc.Add(table);
            return doc;
        }  ]]>
</xsl:when>
<xsl:otherwise>    <![CDATA[      
            table.AddCell(new PdfPCell(new Phrase(_]]><xsl:value-of select="BasisSm"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + "  " + _]]><xsl:value-of select="BasisSm"/><![CDATA[.Author + "  " + _]]><xsl:value-of select="BasisSm"/><![CDATA[.AuthorYear, LargeFont)) { Colspan = 4, Border = 0 });  // 1.-4. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxonomicId + " " + Convert.ToString(_]]><xsl:value-of select="BasisSm"/><![CDATA[.CountID), StandardFont)) { Colspan = 4, Border = 0 }); // 1.-4. field
            doc.Add(table);
            return doc;
        }  ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddTblTaxoNomenList  Top 1A ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">        <![CDATA[        
        private static Document Add]]><xsl:value-of select="Table"/><![CDATA[TaxoNomenRankList(Document doc)           ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">        <![CDATA[        
        private static Document Add]]><xsl:value-of select="Table"/><![CDATA[TaxoNomenRankList(Document doc)           ]]>
</xsl:when>
<xsl:otherwise>    <![CDATA[      
        private static Document Add]]><xsl:value-of select="Table"/><![CDATA[TaxoNomenList(Document doc)           ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddTblTaxoNomenList  Top 1A ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:otherwise>    <![CDATA[      
        {
            var table = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 10f   //fix the absolute width of the table
            };
            table.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });  ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddTblTaxoNomenList  Top 2 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:otherwise>    <![CDATA[      
            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + " " +_regnum.Subregnum, StandardFont)) { Border = 0 });  // 3. field  
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field  ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddTblTaxoNomenList  Top 3 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">        <![CDATA[        
            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxoRank, StandardBoldFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.]]><xsl:value-of select="Basis"/><![CDATA[, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(table);

            return doc;
        }       ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">        <![CDATA[        
            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxoRank, StandardBoldFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.]]><xsl:value-of select="Basis"/><![CDATA[, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(table);

            return doc;
        }     ]]>
</xsl:when>
<xsl:otherwise>    <![CDATA[      
            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxoRank, StandardBoldFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.]]><xsl:value-of select="Basis"/><![CDATA[, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field
  ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddTblTaxoNomenList  Top 3 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">        <![CDATA[        
        private static Document AddTbl69FiSpeciessesTaxoNomenStatusList(Document doc)
        {
            var table = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 10f   //fix the absolute width of the table
            };
            table.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });
            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxoStatus, StandardBoldFont)) { Colspan = 2, Border = 0 });  // 2.- 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportCurrentStand, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Valid), StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 4, Border = 0 });  //Empty row

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDataQualiIndicator, StandardBoldFont)) { Colspan = 2, Border = 0 });  // 2.- 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportRecordCrediRate, StandardFont)) { Colspan = 2, Border = 0 });  // 2. - 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportGlobalSpecComp, StandardFont)) { Colspan = 2, Border = 0 });  // 2. - 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportRecordUpdate, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.UpdaterDate, CultureInfo.InvariantCulture), StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

            doc.Add(table);

            return doc;
        }
        private static Document AddTbl69FiSpeciessesSpecificationList(Document doc)
        {
            var table = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 10f   //fix the absolute width of the table
            };
            table.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTradeName, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.TradeName, StandardFont)) { Border = 0 });  // 3. field  
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field  

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemoSpecies, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.MemoSpecies, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. fField
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportImporterWithYear, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.Importer + " /  " + _fispecies.ImportingYear, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. fField
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportLNumberLOrigin, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.LNumber + " /  " + _fispecies.LOrigin, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. fField
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportLdaNumberLdaOrigin, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.LDANumber + " /  " + _fispecies.LDAOrigin, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. fField
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportBasinLength, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.BasinLength + " cm  " , StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. fField
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportFishLength, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.FishLength + " cm  " , StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. fField
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemoSpecial, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.MemoSpecial, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(table);

            return doc;
        }       ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">        <![CDATA[        
        private static Document AddTbl72PlSpeciessesTaxoNomenStatusList(Document doc)
        {
            var table = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 10f   //fix the absolute width of the table
            };
            table.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxoStatus, StandardBoldFont)) { Colspan = 2, Border = 0 });  // 2.- 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportCurrentStand, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_plspecies.Valid), StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 4, Border = 0 });  //Empty row

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDataQualiIndicator, StandardBoldFont)) { Colspan = 2, Border = 0 });  // 2.- 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportRecordCrediRate, StandardFont)) { Colspan = 2, Border = 0 });  // 2. - 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportGlobalSpecComp, StandardFont)) { Colspan = 2, Border = 0 });  // 2. - 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportRecordUpdate, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_plspecies.UpdaterDate, CultureInfo.InvariantCulture), StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

            doc.Add(table);

            return doc;
        }
        private static Document AddTbl72PlSpeciessesSpecificationList(Document doc)
        {
            var table = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 10f   //fix the absolute width of the table
            };
            table.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTradeName, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_plspecies.TradeName, StandardFont)) { Border = 0 });  // 3. field  
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field  

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemoSpecies, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_plspecies.MemoSpecies, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. fField
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportImporterWithYear, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_plspecies.Importer + " /  " + _plspecies.ImportingYear, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. fField
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportBasinHeight, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_plspecies.BasinHeight+ " cm  " , StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. fField
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportPlantLength, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_plspecies.PlantLength + " cm  " , StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. fField
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemoGlobal, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_plspecies.MemoGlobal, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(table);

            return doc;
        }     ]]>
</xsl:when>
<xsl:otherwise>    <![CDATA[      
            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportSynonyms, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_]]><xsl:value-of select="BasisSm"/><![CDATA[.Synonym, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. fField
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportCommonNames, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_]]><xsl:value-of select="BasisSm"/><![CDATA[.GerName + " " + CultRes.StringsRes.ReportGerman, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 2, Border = 0 });  // 1. + 2.  field
            table.AddCell(new PdfPCell(new Phrase(_]]><xsl:value-of select="BasisSm"/><![CDATA[.EngName + " " + CultRes.StringsRes.ReportEnglish, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 2, Border = 0 });  // 1. + 2.  field
            table.AddCell(new PdfPCell(new Phrase(_]]><xsl:value-of select="BasisSm"/><![CDATA[.FraName + " " + CultRes.StringsRes.ReportFrench, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 2, Border = 0 });  // 1. + 2.  field
            table.AddCell(new PdfPCell(new Phrase(_]]><xsl:value-of select="BasisSm"/><![CDATA[.PorName + " " + CultRes.StringsRes.ReportPortuguese, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTaxoStatus, StandardBoldFont)) { Colspan = 2, Border = 0 });  // 2.- 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportCurrentStand, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_]]><xsl:value-of select="BasisSm"/><![CDATA[.Valid), StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 4, Border = 0 });  //Empty row

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDataQualiIndicator, StandardBoldFont)) { Colspan = 2, Border = 0 });  // 2.- 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportRecordCrediRate, StandardFont)) { Colspan = 2, Border = 0 });  // 2. - 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportGlobalSpecComp, StandardFont)) { Colspan = 2, Border = 0 });  // 2. - 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportRecordUpdate, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_]]><xsl:value-of select="BasisSm"/><![CDATA[.UpdaterDate, CultureInfo.InvariantCulture), StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportInfo, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_]]><xsl:value-of select="BasisSm"/><![CDATA[.Info, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemo, StandardFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_]]><xsl:value-of select="BasisSm"/><![CDATA[.Memo, StandardFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4. field

            doc.Add(table);

            return doc;
        }  ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddHierarchyList Top 1A  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)         
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 10f   
            };
            tableRegnum.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 });  // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum);
            }
            return doc;
        } ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)         
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f   
          };
            tableRegnum.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 });  // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum); 
            }
            //-----------------------------------------------------
            if (_phylum.PhylumName.Contains("#") == false)
            {
            var tablePhylum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f   
            };
            tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

            tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 });  // 2. field
            tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " - " + _phylum.Author + "  " + _phylum.GerName + " " + _phylum.EngName + " " + _phylum.FraName + " " + _phylum.PorName, SmallFont)) { Border = 0 });  // 3. field
            tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tablePhylum);
            }
            return doc;
        }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)         
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f   
            };
            tableRegnum.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 });  // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum); 
            }
            //-----------------------------------------------------
            if (_division.DivisionName.Contains("#") == false)
            {
            var tableDivision = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f   
            };
            tableDivision.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

            tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. . field
            tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 });  // 2. field
            tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " - " + _division.Author + "  " + _division.GerName + " " + _division.EngName + " " + _division.FraName + " " + _division.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableDivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableDivision);
            }
            return doc;
        }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl12Subphylums'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)         
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f   
            };
            tableRegnum.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 });  // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum); 
            }
            //-----------------------------------------------------
            if (_phylum.PhylumName.Contains("#") == false)
            {
            var tablePhylum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f   
            };
            tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

            tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. . field
            tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 });  // 2. field
            tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " - " + _phylum.Author + "  " + _phylum.GerName + " " + _phylum.EngName + " " + _phylum.FraName + " " + _phylum.PorName, SmallFont)) { Border = 0 });  // 3. field
            tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tablePhylum);
            }
            //-----------------------------------------------------
            if (_subphylum.SubphylumName.Contains("#") == false)
            {
            var tableSubphylum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f   
            };
            tableSubphylum.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

            tableSubphylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1. . field
            tableSubphylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 });  // 2. field
            tableSubphylum.AddCell(new PdfPCell(new Phrase(_subphylum.SubphylumName + " - " + _subphylum.Author + "  " + _subphylum.GerName + " " + _subphylum.EngName + " " + _subphylum.FraName + " " + _subphylum.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubphylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubphylum);
            }
            return doc;
        }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl15Subdivisions'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)         
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f   
            };
            tableRegnum.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 });  // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum); 
            }
            //-----------------------------------------------------
            if (_division.DivisionName.Contains("#") == false)
            {
            var tableDivision = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f   
            };
            tableDivision.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

            tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 });  // 2. field
            tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " - " + _division.Author + "  " + _division.GerName + " " + _division.EngName + " " + _division.FraName + " " + _division.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableDivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableDivision);
            }
            //-----------------------------------------------------
            if (_subdivision.SubdivisionName.Contains("#") == false)
            {
            var tableSubdivision = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f   
            };
            tableSubdivision.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

            tableSubdivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubdivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subdivision, SmallFont)) { Border = 0 });  // 2. field
            tableSubdivision.AddCell(new PdfPCell(new Phrase(_subdivision.SubdivisionName + " - " + _subdivision.Author + "  " + _subdivision.GerName + " " + _subdivision.EngName + " " + _subdivision.FraName + " " + _subdivision.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubdivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubdivision);
            }
            return doc;
        }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl18Superclasses'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true, //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0, //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f
            };
            tableRegnum.SetWidths(new[] {0.05f, 1.25f, 2.50f, 1.50f});

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 }); // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont))  { Border = 0 }); // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum);
            }
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                //-----------------------------------------------------
            if (_division.DivisionName.Contains("#") == false)
            {
                var tableDivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableDivision.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 }); // 2. field
                tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " - " + _division.Author + "  " + _division.GerName + " " + _division.EngName + " " + _division.FraName + " " + _division.PorName,  SmallFont))  { Border = 0 }); // 3. field
                tableDivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableDivision);
                 }
                //-----------------------------------------------------
            if (_subdivision.SubdivisionName.Contains("#") == false)
            {
                var tableSubdivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubdivision.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubdivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableSubdivision.AddCell( new PdfPCell(new Phrase(CultRes.StringsRes.Subdivision, SmallFont)) { Border = 0 }); // 2. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(_subdivision.SubdivisionName + " - " + _subdivision.Author + "  " + _subdivision.GerName + " " + _subdivision.EngName + " " + _subdivision.FraName + " " + _subdivision.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubdivision);
                }
                //-----------------------------------------------------
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                //-----------------------------------------------------
                if (_phylum.PhylumName.Contains("#") == false)
                {
                var tablePhylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 }); // 2. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " - " + _phylum.Author + "  " + _phylum.GerName + " " + _phylum.EngName + " " + _phylum.FraName + " " + _phylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tablePhylum);
                }
                //-----------------------------------------------------
            if (_subphylum.SubphylumName.Contains("#") == false)
            {
                var tableSubphylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubphylum.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubphylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 }); // 2. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(_subphylum.SubphylumName + " - " + _subphylum.Author + "  " + _subphylum.GerName + " " + _subphylum.EngName + " " + _subphylum.FraName + " " + _subphylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubphylum);
                }
            }
            //-----------------------------------------------------
            if (_superclass.SuperclassName.Contains("#") == false)
            {
            var tableSuperclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f
            };
            tableSuperclass.SetWidths(new[] { 0.14f, 1.16f, 2.50f, 1.50f });

            tableSuperclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superclass, SmallFont)) { Border = 0 });  // 2. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(_superclass.SuperclassName + " - " + _superclass.Author + "  " + _superclass.GerName + " " + _superclass.EngName + " " + _superclass.FraName + " " + _superclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperclass);
            }

            return doc;
        }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl21Classes'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true, //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0, //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f
            };
            tableRegnum.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 }); // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont))  { Border = 0 }); // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum);
            }

            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                //-----------------------------------------------------
            if (_division.DivisionName.Contains("#") == false)
            {
                var tableDivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableDivision.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 }); // 2. field
                tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " - " + _division.Author + "  " + _division.GerName + " " + _division.EngName + " " + _division.FraName + " " + _division.PorName,  SmallFont))  { Border = 0 }); // 3. field
                tableDivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableDivision);
                }
                //-----------------------------------------------------
            if (_subdivision.SubdivisionName.Contains("#") == false)
            {
                var tableSubdivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubdivision.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubdivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableSubdivision.AddCell( new PdfPCell(new Phrase(CultRes.StringsRes.Subdivision, SmallFont)) { Border = 0 }); // 2. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(_subdivision.SubdivisionName + " - " + _subdivision.Author + "  " + _subdivision.GerName + " " + _subdivision.EngName + " " + _subdivision.FraName + " " + _subdivision.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubdivision);
                }
                //-----------------------------------------------------
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                //-----------------------------------------------------
            if (_phylum.PhylumName.Contains("#") == false)
            {
                var tablePhylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 }); // 2. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " - " + _phylum.Author + "  " + _phylum.GerName + " " + _phylum.EngName + " " + _phylum.FraName + " " + _phylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tablePhylum);
                }
                //-----------------------------------------------------
            if (_subphylum.SubphylumName.Contains("#") == false)
            {
                var tableSubphylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubphylum.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubphylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 }); // 2. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(_subphylum.SubphylumName + " - " + _subphylum.Author + "  " + _subphylum.GerName + " " + _subphylum.EngName + " " + _subphylum.FraName + " " + _subphylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubphylum);
                }
            }
            //-----------------------------------------------------
            if (_superclass.SuperclassName.Contains("#") == false)
            {
            var tableSuperclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperclass.SetWidths(new[] { 0.14f, 1.16f, 2.50f, 1.50f });

            tableSuperclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superclass, SmallFont)) { Border = 0 });  // 2. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(_superclass.SuperclassName + " - " + _superclass.Author + "  " + _superclass.GerName + " " + _superclass.EngName + " " + _superclass.FraName + " " + _superclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperclass);
            }
            //-----------------------------------------------------
            if (_class.ClassName.Contains("#") == false)
            {
            var tableClass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f
            };
            tableClass.SetWidths(new[] { 0.17f, 1.13f, 2.50f, 1.50f });

            tableClass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableClass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Class, SmallFont)) { Border = 0 });  // 2. field
            tableClass.AddCell(new PdfPCell(new Phrase(_class.ClassName + " - " + _class.Author + "  " + _class.GerName + " " + _class.EngName + " " + _class.FraName + " " + _class.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableClass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableClass);
            }
            return doc;
        }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl24Subclasses'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true, //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0, //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f
            };
            tableRegnum.SetWidths(new[] {0.05f, 1.25f, 2.50f, 1.50f });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 }); // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont))  { Border = 0 }); // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum);
            }
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                //-----------------------------------------------------
            if (_division.DivisionName.Contains("#") == false)
            {
                var tableDivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableDivision.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 }); // 2. field
                tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " - " + _division.Author + "  " + _division.GerName + " " + _division.EngName + " " + _division.FraName + " " + _division.PorName,  SmallFont))  { Border = 0 }); // 3. field
                tableDivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableDivision);
                }
                //-----------------------------------------------------
            if (_subdivision.SubdivisionName.Contains("#") == false)
            {
                var tableSubdivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubdivision.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubdivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableSubdivision.AddCell( new PdfPCell(new Phrase(CultRes.StringsRes.Subdivision, SmallFont)) { Border = 0 }); // 2. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(_subdivision.SubdivisionName + " - " + _subdivision.Author + "  " + _subdivision.GerName + " " + _subdivision.EngName + " " + _subdivision.FraName + " " + _subdivision.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubdivision);
                }
                //-----------------------------------------------------
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                //-----------------------------------------------------
                if (_phylum.PhylumName.Contains("#") == false)
                {
                var tablePhylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 }); // 2. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " - " + _phylum.Author + "  " + _phylum.GerName + " " + _phylum.EngName + " " + _phylum.FraName + " " + _phylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tablePhylum);
                }
                //-----------------------------------------------------
            if (_subphylum.SubphylumName.Contains("#") == false)
            {
                var tableSubphylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubphylum.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubphylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 }); // 2. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(_subphylum.SubphylumName + " - " + _subphylum.Author + "  " + _subphylum.GerName + " " + _subphylum.EngName + " " + _subphylum.FraName + " " + _subphylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubphylum);
                }
            }
            //-----------------------------------------------------
            if (_superclass.SuperclassName.Contains("#") == false)
            {
            var tableSuperclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperclass.SetWidths(new[] { 0.14f, 1.16f, 2.50f, 1.50f });

            tableSuperclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superclass, SmallFont)) { Border = 0 });  // 2. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(_superclass.SuperclassName + " - " + _superclass.Author + "  " + _superclass.GerName + " " + _superclass.EngName + " " + _superclass.FraName + " " + _superclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperclass);
            }
            //-----------------------------------------------------
            if (_class.ClassName.Contains("#") == false)
            {
            var tableClass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableClass.SetWidths(new[] { 0.17f, 1.13f, 2.50f, 1.50f });

            tableClass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableClass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Class, SmallFont)) { Border = 0 });  // 2. field
            tableClass.AddCell(new PdfPCell(new Phrase(_class.ClassName + " - " + _class.Author + "  " + _class.GerName + " " + _class.EngName + " " + _class.FraName + " " + _class.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableClass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableClass);
            }
            //-----------------------------------------------------
            if (_subclass.SubclassName.Contains("#") == false)
            {
            var tableSubclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f
            };
            tableSubclass.SetWidths(new[] { 0.20f, 1.10f, 2.50f, 1.50f });

            tableSubclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subclass, SmallFont)) { Border = 0 });  // 2. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(_subclass.SubclassName + " - " + _subclass.Author + "  " + _subclass.GerName + " " + _subclass.EngName + " " + _subclass.FraName + " " + _subclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubclass);
            }
            return doc;
        }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl27Infraclasses'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true, //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0, //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f
            };
            tableRegnum.SetWidths(new[] {0.05f, 1.25f, 2.50f, 1.50f  });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 }); // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont))  { Border = 0 }); // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum);
            }
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                //-----------------------------------------------------
            if (_division.DivisionName.Contains("#") == false)
            {
                var tableDivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableDivision.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 }); // 2. field
                tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " - " + _division.Author + "  " + _division.GerName + " " + _division.EngName + " " + _division.FraName + " " + _division.PorName,  SmallFont))  { Border = 0 }); // 3. field
                tableDivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableDivision);
                }
                //-----------------------------------------------------
            if (_subdivision.SubdivisionName.Contains("#") == false)
            {
                var tableSubdivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubdivision.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubdivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableSubdivision.AddCell( new PdfPCell(new Phrase(CultRes.StringsRes.Subdivision, SmallFont)) { Border = 0 }); // 2. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(_subdivision.SubdivisionName + " - " + _subdivision.Author + "  " + _subdivision.GerName + " " + _subdivision.EngName + " " + _subdivision.FraName + " " + _subdivision.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubdivision);
                }
                //-----------------------------------------------------
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                //-----------------------------------------------------
                if (_phylum.PhylumName.Contains("#") == false)
                {
                var tablePhylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 }); // 2. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " - " + _phylum.Author + "  " + _phylum.GerName + " " + _phylum.EngName + " " + _phylum.FraName + " " + _phylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tablePhylum);
                }
                //-----------------------------------------------------
            if (_subphylum.SubphylumName.Contains("#") == false)
            {
                var tableSubphylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubphylum.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubphylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 }); // 2. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(_subphylum.SubphylumName + " - " + _subphylum.Author + "  " + _subphylum.GerName + " " + _subphylum.EngName + " " + _subphylum.FraName + " " + _subphylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubphylum);
                }
            }
            //-----------------------------------------------------
            if (_superclass.SuperclassName.Contains("#") == false)
            {
            var tableSuperclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperclass.SetWidths(new[] { 0.14f, 1.16f, 2.50f, 1.50f });

            tableSuperclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superclass, SmallFont)) { Border = 0 });  // 2. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(_superclass.SuperclassName + " - " + _superclass.Author + "  " + _superclass.GerName + " " + _superclass.EngName + " " + _superclass.FraName + " " + _superclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperclass);
            }
            //-----------------------------------------------------
            if (_class.ClassName.Contains("#") == false)
            {
            var tableClass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableClass.SetWidths(new[] { 0.17f, 1.13f, 2.50f, 1.50f });

            tableClass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableClass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Class, SmallFont)) { Border = 0 });  // 2. field
            tableClass.AddCell(new PdfPCell(new Phrase(_class.ClassName + " - " + _class.Author + "  " + _class.GerName + " " + _class.EngName + " " + _class.FraName + " " + _class.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableClass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableClass);
            }
            //-----------------------------------------------------
            if (_subclass.SubclassName.Contains("#") == false)
            {
            var tableSubclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubclass.SetWidths(new[] { 0.20f, 1.10f, 2.50f, 1.50f });

            tableSubclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subclass, SmallFont)) { Border = 0 });  // 2. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(_subclass.SubclassName + " - " + _subclass.Author + "  " + _subclass.GerName + " " + _subclass.EngName + " " + _subclass.FraName + " " + _subclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubclass);
            }
            //-----------------------------------------------------
            if (_infraclass.InfraclassName.Contains("#") == false)
            {
            var tableInfraclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f
            };
            tableInfraclass.SetWidths(new[] { 0.23f, 1.07f, 2.50f, 1.50f });

            tableInfraclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraclass, SmallFont)) { Border = 0 });  // 2. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(_infraclass.InfraclassName + " - " + _infraclass.Author + "  " + _infraclass.GerName + " " + _infraclass.EngName + " " + _infraclass.FraName + " " + _infraclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraclass);
            }
            return doc;
        }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl30Legios'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true, //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0, //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f
            };
            tableRegnum.SetWidths(new[] {0.05f, 1.25f, 2.50f, 1.50f  });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 }); // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont))  { Border = 0 }); // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum);
            }
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                //-----------------------------------------------------
            if (_division.DivisionName.Contains("#") == false)
            {
                var tableDivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableDivision.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 }); // 2. field
                tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " - " + _division.Author + "  " + _division.GerName + " " + _division.EngName + " " + _division.FraName + " " + _division.PorName,  SmallFont))  { Border = 0 }); // 3. field
                tableDivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableDivision);
                }
                //-----------------------------------------------------
            if (_subdivision.SubdivisionName.Contains("#") == false)
            {
                var tableSubdivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubdivision.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubdivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableSubdivision.AddCell( new PdfPCell(new Phrase(CultRes.StringsRes.Subdivision, SmallFont)) { Border = 0 }); // 2. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(_subdivision.SubdivisionName + " - " + _subdivision.Author + "  " + _subdivision.GerName + " " + _subdivision.EngName + " " + _subdivision.FraName + " " + _subdivision.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubdivision);
                }
                //-----------------------------------------------------
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                //-----------------------------------------------------
                if (_phylum.PhylumName.Contains("#") == false)
                {
                var tablePhylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 }); // 2. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " - " + _phylum.Author + "  " + _phylum.GerName + " " + _phylum.EngName + " " + _phylum.FraName + " " + _phylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tablePhylum);
                }
                //-----------------------------------------------------
            if (_subphylum.SubphylumName.Contains("#") == false)
            {
                var tableSubphylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubphylum.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubphylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 }); // 2. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(_subphylum.SubphylumName + " - " + _subphylum.Author + "  " + _subphylum.GerName + " " + _subphylum.EngName + " " + _subphylum.FraName + " " + _subphylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubphylum);
                }
            }
            //-----------------------------------------------------
            if (_superclass.SuperclassName.Contains("#") == false)
            {
            var tableSuperclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperclass.SetWidths(new[] { 0.14f, 1.16f, 2.50f, 1.50f });

            tableSuperclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superclass, SmallFont)) { Border = 0 });  // 2. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(_superclass.SuperclassName + " - " + _superclass.Author + "  " + _superclass.GerName + " " + _superclass.EngName + " " + _superclass.FraName + " " + _superclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperclass);
            }
            //-----------------------------------------------------
            if (_class.ClassName.Contains("#") == false)
            {
            var tableClass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableClass.SetWidths(new[] { 0.17f, 1.13f, 2.50f, 1.50f });

            tableClass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableClass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Class, SmallFont)) { Border = 0 });  // 2. field
            tableClass.AddCell(new PdfPCell(new Phrase(_class.ClassName + " - " + _class.Author + "  " + _class.GerName + " " + _class.EngName + " " + _class.FraName + " " + _class.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableClass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableClass);
            }
            //-----------------------------------------------------
            if (_subclass.SubclassName.Contains("#") == false)
            {
            var tableSubclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubclass.SetWidths(new[] { 0.20f, 1.10f, 2.50f, 1.50f });

            tableSubclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subclass, SmallFont)) { Border = 0 });  // 2. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(_subclass.SubclassName + " - " + _subclass.Author + "  " + _subclass.GerName + " " + _subclass.EngName + " " + _subclass.FraName + " " + _subclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubclass);
            }
            //-----------------------------------------------------
            if (_infraclass.InfraclassName.Contains("#") == false)
            {
            var tableInfraclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraclass.SetWidths(new[] { 0.23f, 1.07f, 2.50f, 1.50f });

            tableInfraclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraclass, SmallFont)) { Border = 0 });  // 2. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(_infraclass.InfraclassName + " - " + _infraclass.Author + "  " + _infraclass.GerName + " " + _infraclass.EngName + " " + _infraclass.FraName + " " + _infraclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraclass);
            } 
            //-----------------------------------------------------
            if (_legio.LegioName.Contains("#") == false)
            {
            var tableLegio = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f
            };
            tableLegio.SetWidths(new[] { 0.26f, 1.04f, 2.50f, 1.50f });

            tableLegio.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableLegio.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Legio, SmallFont)) { Border = 0 });  // 2. field
            tableLegio.AddCell(new PdfPCell(new Phrase(_legio.LegioName + " - " + _legio.Author + "  " + _legio.GerName + " " + _legio.EngName + " " + _legio.FraName + " " + _legio.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableLegio.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableLegio);
            }
            return doc;
        }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl33Ordos'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true, //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0, //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f
            };
            tableRegnum.SetWidths(new[] {0.05f, 1.25f, 2.50f, 1.50f  });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 }); // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont))  { Border = 0 }); // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum);
            }
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                //-----------------------------------------------------
            if (_division.DivisionName.Contains("#") == false)
            {
                var tableDivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableDivision.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 }); // 2. field
                tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " - " + _division.Author + "  " + _division.GerName + " " + _division.EngName + " " + _division.FraName + " " + _division.PorName,  SmallFont))  { Border = 0 }); // 3. field
                tableDivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableDivision);
                }
                //-----------------------------------------------------
            if (_subdivision.SubdivisionName.Contains("#") == false)
            {
                var tableSubdivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubdivision.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubdivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableSubdivision.AddCell( new PdfPCell(new Phrase(CultRes.StringsRes.Subdivision, SmallFont)) { Border = 0 }); // 2. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(_subdivision.SubdivisionName + " - " + _subdivision.Author + "  " + _subdivision.GerName + " " + _subdivision.EngName + " " + _subdivision.FraName + " " + _subdivision.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubdivision);
                }
                //-----------------------------------------------------
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                //-----------------------------------------------------
                if (_phylum.PhylumName.Contains("#") == false)
                {
                var tablePhylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 }); // 2. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " - " + _phylum.Author + "  " + _phylum.GerName + " " + _phylum.EngName + " " + _phylum.FraName + " " + _phylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tablePhylum);
                }
                //-----------------------------------------------------
            if (_subphylum.SubphylumName.Contains("#") == false)
            {
                var tableSubphylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubphylum.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubphylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 }); // 2. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(_subphylum.SubphylumName + " - " + _subphylum.Author + "  " + _subphylum.GerName + " " + _subphylum.EngName + " " + _subphylum.FraName + " " + _subphylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubphylum);
                }
            }
            //-----------------------------------------------------
            if (_superclass.SuperclassName.Contains("#") == false)
            {
            var tableSuperclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperclass.SetWidths(new[] { 0.14f, 1.16f, 2.50f, 1.50f });

            tableSuperclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superclass, SmallFont)) { Border = 0 });  // 2. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(_superclass.SuperclassName + " - " + _superclass.Author + "  " + _superclass.GerName + " " + _superclass.EngName + " " + _superclass.FraName + " " + _superclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperclass);
            }
            //-----------------------------------------------------
            if (_class.ClassName.Contains("#") == false)
            {
            var tableClass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableClass.SetWidths(new[] { 0.17f, 1.13f, 2.50f, 1.50f });

            tableClass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableClass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Class, SmallFont)) { Border = 0 });  // 2. field
            tableClass.AddCell(new PdfPCell(new Phrase(_class.ClassName + " - " + _class.Author + "  " + _class.GerName + " " + _class.EngName + " " + _class.FraName + " " + _class.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableClass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableClass);
            }
            //-----------------------------------------------------
            if (_subclass.SubclassName.Contains("#") == false)
            {
            var tableSubclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubclass.SetWidths(new[] { 0.20f, 1.10f, 2.50f, 1.50f });

            tableSubclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subclass, SmallFont)) { Border = 0 });  // 2. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(_subclass.SubclassName + " - " + _subclass.Author + "  " + _subclass.GerName + " " + _subclass.EngName + " " + _subclass.FraName + " " + _subclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubclass);
            }
            //-----------------------------------------------------
            if (_infraclass.InfraclassName.Contains("#") == false)
            {
            var tableInfraclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraclass.SetWidths(new[] { 0.23f, 1.07f, 2.50f, 1.50f });

            tableInfraclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraclass, SmallFont)) { Border = 0 });  // 2. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(_infraclass.InfraclassName + " - " + _infraclass.Author + "  " + _infraclass.GerName + " " + _infraclass.EngName + " " + _infraclass.FraName + " " + _infraclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraclass);
            }
            //-----------------------------------------------------
            if (_legio.LegioName.Contains("#") == false)
            {
            var tableLegio = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableLegio.SetWidths(new[] { 0.26f, 1.04f, 2.50f, 1.50f });

            tableLegio.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableLegio.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Legio, SmallFont)) { Border = 0 });  // 2. field
            tableLegio.AddCell(new PdfPCell(new Phrase(_legio.LegioName + " - " + _legio.Author + "  " + _legio.GerName + " " + _legio.EngName + " " + _legio.FraName + " " + _legio.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableLegio.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableLegio);
            }
            //-----------------------------------------------------
            if (_ordo.OrdoName.Contains("#") == false)
            {
            var tableOrdo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f
            };
            tableOrdo.SetWidths(new[] { 0.29f, 1.01f, 2.50f, 1.50f });

            tableOrdo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableOrdo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Ordo, SmallFont)) { Border = 0 });  // 2. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(_ordo.OrdoName + " - " + _ordo.Author + "  " + _ordo.GerName + " " + _ordo.EngName + " " + _ordo.FraName + " " + _ordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableOrdo);
            }
            return doc;
        }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl36Subordos'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true, //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0, //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f
            };
            tableRegnum.SetWidths(new[] {0.05f, 1.25f, 2.50f, 1.50f  });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 }); // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont))  { Border = 0 }); // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum);
            }
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                //-----------------------------------------------------
            if (_division.DivisionName.Contains("#") == false)
            {
                var tableDivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableDivision.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 }); // 2. field
                tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " - " + _division.Author + "  " + _division.GerName + " " + _division.EngName + " " + _division.FraName + " " + _division.PorName,  SmallFont))  { Border = 0 }); // 3. field
                tableDivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableDivision);
                }
                //-----------------------------------------------------
            if (_subdivision.SubdivisionName.Contains("#") == false)
            {
                var tableSubdivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubdivision.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubdivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableSubdivision.AddCell( new PdfPCell(new Phrase(CultRes.StringsRes.Subdivision, SmallFont)) { Border = 0 }); // 2. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(_subdivision.SubdivisionName + " - " + _subdivision.Author + "  " + _subdivision.GerName + " " + _subdivision.EngName + " " + _subdivision.FraName + " " + _subdivision.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubdivision);
                }
                //-----------------------------------------------------
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                //-----------------------------------------------------
                if (_phylum.PhylumName.Contains("#") == false)
                {
                var tablePhylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 }); // 2. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " - " + _phylum.Author + "  " + _phylum.GerName + " " + _phylum.EngName + " " + _phylum.FraName + " " + _phylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tablePhylum);
                }
                //-----------------------------------------------------
            if (_subphylum.SubphylumName.Contains("#") == false)
            {
                var tableSubphylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubphylum.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubphylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 }); // 2. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(_subphylum.SubphylumName + " - " + _subphylum.Author + "  " + _subphylum.GerName + " " + _subphylum.EngName + " " + _subphylum.FraName + " " + _subphylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubphylum);
                }
            }
            //-----------------------------------------------------
            if (_superclass.SuperclassName.Contains("#") == false)
            {
            var tableSuperclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperclass.SetWidths(new[] { 0.14f, 1.16f, 2.50f, 1.50f });

            tableSuperclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superclass, SmallFont)) { Border = 0 });  // 2. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(_superclass.SuperclassName + " - " + _superclass.Author + "  " + _superclass.GerName + " " + _superclass.EngName + " " + _superclass.FraName + " " + _superclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperclass);
            }
            //-----------------------------------------------------
            if (_class.ClassName.Contains("#") == false)
            {
            var tableClass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableClass.SetWidths(new[] { 0.17f, 1.13f, 2.50f, 1.50f });

            tableClass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableClass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Class, SmallFont)) { Border = 0 });  // 2. field
            tableClass.AddCell(new PdfPCell(new Phrase(_class.ClassName + " - " + _class.Author + "  " + _class.GerName + " " + _class.EngName + " " + _class.FraName + " " + _class.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableClass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableClass);
            }
            //-----------------------------------------------------
            if (_subclass.SubclassName.Contains("#") == false)
            {
            var tableSubclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubclass.SetWidths(new[] { 0.20f, 1.10f, 2.50f, 1.50f });

            tableSubclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subclass, SmallFont)) { Border = 0 });  // 2. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(_subclass.SubclassName + " - " + _subclass.Author + "  " + _subclass.GerName + " " + _subclass.EngName + " " + _subclass.FraName + " " + _subclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubclass);
             }
            //-----------------------------------------------------
            if (_infraclass.InfraclassName.Contains("#") == false)
            {
            var tableInfraclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraclass.SetWidths(new[] { 0.23f, 1.07f, 2.50f, 1.50f });

            tableInfraclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraclass, SmallFont)) { Border = 0 });  // 2. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(_infraclass.InfraclassName + " - " + _infraclass.Author + "  " + _infraclass.GerName + " " + _infraclass.EngName + " " + _infraclass.FraName + " " + _infraclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraclass);
            }
            //-----------------------------------------------------
            if (_legio.LegioName.Contains("#") == false)
            {
            var tableLegio = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableLegio.SetWidths(new[] { 0.26f, 1.04f, 2.50f, 1.50f });

            tableLegio.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableLegio.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Legio, SmallFont)) { Border = 0 });  // 2. field
            tableLegio.AddCell(new PdfPCell(new Phrase(_legio.LegioName + " - " + _legio.Author + "  " + _legio.GerName + " " + _legio.EngName + " " + _legio.FraName + " " + _legio.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableLegio.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableLegio);
            }
            //-----------------------------------------------------
            if (_ordo.OrdoName.Contains("#") == false)
            {
            var tableOrdo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableOrdo.SetWidths(new[] { 0.29f, 1.01f, 2.50f, 1.50f });

            tableOrdo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableOrdo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Ordo, SmallFont)) { Border = 0 });  // 2. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(_ordo.OrdoName + " - " + _ordo.Author + "  " + _ordo.GerName + " " + _ordo.EngName + " " + _ordo.FraName + " " + _ordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableOrdo);
            }
            //-----------------------------------------------------
            if (_subordo.SubordoName.Contains("#") == false)
            {
            var tableSubordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f
            };
            tableSubordo.SetWidths(new[] { 0.32f, 0.98f, 2.50f, 1.50f });

            tableSubordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subordo, SmallFont)) { Border = 0 });  // 2. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(_subordo.SubordoName + " - " + _subordo.Author + "  " + _subordo.GerName + " " + _subordo.EngName + " " + _subordo.FraName + " " + _subordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubordo);
            }
            return doc;
        }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl39Infraordos'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true, //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0, //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f
            };
            tableRegnum.SetWidths(new[] {0.05f, 1.25f, 2.50f, 1.50f  });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 }); // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont))  { Border = 0 }); // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum);
            }
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {

                //-----------------------------------------------------
            if (_division.DivisionName.Contains("#") == false)
            {
                var tableDivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableDivision.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 }); // 2. field
                tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " - " + _division.Author + "  " + _division.GerName + " " + _division.EngName + " " + _division.FraName + " " + _division.PorName,  SmallFont))  { Border = 0 }); // 3. field
                tableDivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableDivision);
                }
                //-----------------------------------------------------
            if (_subdivision.SubdivisionName.Contains("#") == false)
            {
                var tableSubdivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubdivision.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubdivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableSubdivision.AddCell( new PdfPCell(new Phrase(CultRes.StringsRes.Subdivision, SmallFont)) { Border = 0 }); // 2. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(_subdivision.SubdivisionName + " - " + _subdivision.Author + "  " + _subdivision.GerName + " " + _subdivision.EngName + " " + _subdivision.FraName + " " + _subdivision.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubdivision);
                }
                //-----------------------------------------------------
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                //-----------------------------------------------------
                if (_phylum.PhylumName.Contains("#") == false)
                {
                var tablePhylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 }); // 2. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " - " + _phylum.Author + "  " + _phylum.GerName + " " + _phylum.EngName + " " + _phylum.FraName + " " + _phylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tablePhylum);
                }
                //-----------------------------------------------------
            if (_subphylum.SubphylumName.Contains("#") == false)
            {
                var tableSubphylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubphylum.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubphylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 }); // 2. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(_subphylum.SubphylumName + " - " + _subphylum.Author + "  " + _subphylum.GerName + " " + _subphylum.EngName + " " + _subphylum.FraName + " " + _subphylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubphylum);
                }
            }
            //-----------------------------------------------------
            if (_superclass.SuperclassName.Contains("#") == false)
            {
            var tableSuperclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperclass.SetWidths(new[] { 0.14f, 1.16f, 2.50f, 1.50f });

            tableSuperclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superclass, SmallFont)) { Border = 0 });  // 2. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(_superclass.SuperclassName + " - " + _superclass.Author + "  " + _superclass.GerName + " " + _superclass.EngName + " " + _superclass.FraName + " " + _superclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperclass);
            }
            //-----------------------------------------------------
            if (_class.ClassName.Contains("#") == false)
            {
            var tableClass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableClass.SetWidths(new[] { 0.17f, 1.13f, 2.50f, 1.50f });

            tableClass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableClass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Class, SmallFont)) { Border = 0 });  // 2. field
            tableClass.AddCell(new PdfPCell(new Phrase(_class.ClassName + " - " + _class.Author + "  " + _class.GerName + " " + _class.EngName + " " + _class.FraName + " " + _class.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableClass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableClass);
            }
            //-----------------------------------------------------
            if (_subclass.SubclassName.Contains("#") == false)
            {
            var tableSubclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubclass.SetWidths(new[] { 0.20f, 1.10f, 2.50f, 1.50f });

            tableSubclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subclass, SmallFont)) { Border = 0 });  // 2. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(_subclass.SubclassName + " - " + _subclass.Author + "  " + _subclass.GerName + " " + _subclass.EngName + " " + _subclass.FraName + " " + _subclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubclass);
            }
            //-----------------------------------------------------
            if (_infraclass.InfraclassName.Contains("#") == false)
            {
            var tableInfraclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraclass.SetWidths(new[] { 0.23f, 1.07f, 2.50f, 1.50f });

            tableInfraclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraclass, SmallFont)) { Border = 0 });  // 2. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(_infraclass.InfraclassName + " - " + _infraclass.Author + "  " + _infraclass.GerName + " " + _infraclass.EngName + " " + _infraclass.FraName + " " + _infraclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraclass);
            }
            //-----------------------------------------------------
            if (_legio.LegioName.Contains("#") == false)
            {
            var tableLegio = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableLegio.SetWidths(new[] { 0.26f, 1.04f, 2.50f, 1.50f });

            tableLegio.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableLegio.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Legio, SmallFont)) { Border = 0 });  // 2. field
            tableLegio.AddCell(new PdfPCell(new Phrase(_legio.LegioName + " - " + _legio.Author + "  " + _legio.GerName + " " + _legio.EngName + " " + _legio.FraName + " " + _legio.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableLegio.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableLegio);
            }
            //-----------------------------------------------------
            if (_ordo.OrdoName.Contains("#") == false)
            {
            var tableOrdo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableOrdo.SetWidths(new[] { 0.29f, 1.01f, 2.50f, 1.50f });

            tableOrdo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableOrdo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Ordo, SmallFont)) { Border = 0 });  // 2. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(_ordo.OrdoName + " - " + _ordo.Author + "  " + _ordo.GerName + " " + _ordo.EngName + " " + _ordo.FraName + " " + _ordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableOrdo);
            }
            //-----------------------------------------------------
            if (_subordo.SubordoName.Contains("#") == false)
            {
            var tableSubordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubordo.SetWidths(new[] { 0.32f, 0.98f, 2.50f, 1.50f });

            tableSubordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subordo, SmallFont)) { Border = 0 });  // 2. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(_subordo.SubordoName + " - " + _subordo.Author + "  " + _subordo.GerName + " " + _subordo.EngName + " " + _subordo.FraName + " " + _subordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubordo);
            }
            //-----------------------------------------------------
            if (_infraordo.InfraordoName.Contains("#") == false)
            {
            var tableInfraordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f
            };
            tableInfraordo.SetWidths(new[] { 0.35f, 0.95f, 2.50f, 1.50f });

            tableInfraordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraordo, SmallFont)) { Border = 0 });  // 2. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(_infraordo.InfraordoName + " - " + _infraordo.Author + "  " + _infraordo.GerName + " " + _infraordo.EngName + " " + _infraordo.FraName + " " + _infraordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraordo);
            }
            return doc;
        }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl42Superfamilies'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true, //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0, //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f
            };
            tableRegnum.SetWidths(new[] {0.05f, 1.25f, 2.50f, 1.50f  });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 }); // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont))  { Border = 0 }); // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum);
            }
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                //-----------------------------------------------------
             if (_division.DivisionName.Contains("#") == false)
            {
                var tableDivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableDivision.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 }); // 2. field
                tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " - " + _division.Author + "  " + _division.GerName + " " + _division.EngName + " " + _division.FraName + " " + _division.PorName,  SmallFont))  { Border = 0 }); // 3. field
                tableDivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableDivision);
                }
                //-----------------------------------------------------
            if (_subdivision.SubdivisionName.Contains("#") == false)
            {
                var tableSubdivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubdivision.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubdivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableSubdivision.AddCell( new PdfPCell(new Phrase(CultRes.StringsRes.Subdivision, SmallFont)) { Border = 0 }); // 2. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(_subdivision.SubdivisionName + " - " + _subdivision.Author + "  " + _subdivision.GerName + " " + _subdivision.EngName + " " + _subdivision.FraName + " " + _subdivision.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubdivision);
                }
                //-----------------------------------------------------
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                //-----------------------------------------------------
                if (_phylum.PhylumName.Contains("#") == false)
                {
                var tablePhylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 }); // 2. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " - " + _phylum.Author + "  " + _phylum.GerName + " " + _phylum.EngName + " " + _phylum.FraName + " " + _phylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tablePhylum);
                }
                //-----------------------------------------------------
            if (_subphylum.SubphylumName.Contains("#") == false)
            {
                var tableSubphylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubphylum.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubphylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 }); // 2. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(_subphylum.SubphylumName + " - " + _subphylum.Author + "  " + _subphylum.GerName + " " + _subphylum.EngName + " " + _subphylum.FraName + " " + _subphylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubphylum);
                }
            }
            //-----------------------------------------------------
            if (_superclass.SuperclassName.Contains("#") == false)
            {
            var tableSuperclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperclass.SetWidths(new[] { 0.14f, 1.16f, 2.50f, 1.50f });

            tableSuperclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superclass, SmallFont)) { Border = 0 });  // 2. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(_superclass.SuperclassName + " - " + _superclass.Author + "  " + _superclass.GerName + " " + _superclass.EngName + " " + _superclass.FraName + " " + _superclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperclass);
            }
            //-----------------------------------------------------
            if (_class.ClassName.Contains("#") == false)
            {
            var tableClass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableClass.SetWidths(new[] { 0.17f, 1.13f, 2.50f, 1.50f });

            tableClass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableClass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Class, SmallFont)) { Border = 0 });  // 2. field
            tableClass.AddCell(new PdfPCell(new Phrase(_class.ClassName + " - " + _class.Author + "  " + _class.GerName + " " + _class.EngName + " " + _class.FraName + " " + _class.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableClass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableClass);
            }
            //-----------------------------------------------------
            if (_subclass.SubclassName.Contains("#") == false)
            {
            var tableSubclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubclass.SetWidths(new[] { 0.20f, 1.10f, 2.50f, 1.50f });

            tableSubclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subclass, SmallFont)) { Border = 0 });  // 2. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(_subclass.SubclassName + " - " + _subclass.Author + "  " + _subclass.GerName + " " + _subclass.EngName + " " + _subclass.FraName + " " + _subclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubclass);
            }
            //-----------------------------------------------------
            if (_infraclass.InfraclassName.Contains("#") == false)
            {
            var tableInfraclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraclass.SetWidths(new[] { 0.23f, 1.07f, 2.50f, 1.50f });

            tableInfraclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraclass, SmallFont)) { Border = 0 });  // 2. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(_infraclass.InfraclassName + " - " + _infraclass.Author + "  " + _infraclass.GerName + " " + _infraclass.EngName + " " + _infraclass.FraName + " " + _infraclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraclass);
            }
            //-----------------------------------------------------
            if (_legio.LegioName.Contains("#") == false)
            {
            var tableLegio = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableLegio.SetWidths(new[] { 0.26f, 1.04f, 2.50f, 1.50f });

            tableLegio.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableLegio.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Legio, SmallFont)) { Border = 0 });  // 2. field
            tableLegio.AddCell(new PdfPCell(new Phrase(_legio.LegioName + " - " + _legio.Author + "  " + _legio.GerName + " " + _legio.EngName + " " + _legio.FraName + " " + _legio.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableLegio.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableLegio);
            }
            //-----------------------------------------------------
            if (_ordo.OrdoName.Contains("#") == false)
            {
            var tableOrdo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableOrdo.SetWidths(new[] { 0.29f, 1.01f, 2.50f, 1.50f });

            tableOrdo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableOrdo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Ordo, SmallFont)) { Border = 0 });  // 2. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(_ordo.OrdoName + " - " + _ordo.Author + "  " + _ordo.GerName + " " + _ordo.EngName + " " + _ordo.FraName + " " + _ordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableOrdo);
            }
            //-----------------------------------------------------
            if (_subordo.SubordoName.Contains("#") == false)
            {
            var tableSubordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubordo.SetWidths(new[] { 0.32f, 0.98f, 2.50f, 1.50f });

            tableSubordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subordo, SmallFont)) { Border = 0 });  // 2. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(_subordo.SubordoName + " - " + _subordo.Author + "  " + _subordo.GerName + " " + _subordo.EngName + " " + _subordo.FraName + " " + _subordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubordo);
            }
            //-----------------------------------------------------
            if (_infraordo.InfraordoName.Contains("#") == false)
            {
            var tableInfraordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraordo.SetWidths(new[] { 0.35f, 0.95f, 2.50f, 1.50f });

            tableInfraordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraordo, SmallFont)) { Border = 0 });  // 2. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(_infraordo.InfraordoName + " - " + _infraordo.Author + "  " + _infraordo.GerName + " " + _infraordo.EngName + " " + _infraordo.FraName + " " + _infraordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraordo);
            }
            //-----------------------------------------------------
            if (_superfamily.SuperfamilyName.Contains("#") == false)
            {
            var tableSuperfamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f
            };
            tableSuperfamily.SetWidths(new[] { 0.38f, 0.92f, 2.50f, 1.50f });

            tableSuperfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(_superfamily.SuperfamilyName + " - " + _superfamily.Author + "  " + _superfamily.GerName + " " + _superfamily.EngName + " " + _superfamily.FraName + " " + _superfamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperfamily);
            }
            return doc;
        }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl45Families'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true, //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0, //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f
            };
            tableRegnum.SetWidths(new[] {0.05f, 1.25f, 2.50f, 1.50f  });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 }); // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont))  { Border = 0 }); // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum);
            }
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                //-----------------------------------------------------
            if (_division.DivisionName.Contains("#") == false)
            {
                var tableDivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableDivision.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 }); // 2. field
                tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " - " + _division.Author + "  " + _division.GerName + " " + _division.EngName + " " + _division.FraName + " " + _division.PorName,  SmallFont))  { Border = 0 }); // 3. field
                tableDivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableDivision);
                 }
                //-----------------------------------------------------
            if (_subdivision.SubdivisionName.Contains("#") == false)
            {
                var tableSubdivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubdivision.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubdivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableSubdivision.AddCell( new PdfPCell(new Phrase(CultRes.StringsRes.Subdivision, SmallFont)) { Border = 0 }); // 2. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(_subdivision.SubdivisionName + " - " + _subdivision.Author + "  " + _subdivision.GerName + " " + _subdivision.EngName + " " + _subdivision.FraName + " " + _subdivision.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubdivision);
                }
                //-----------------------------------------------------
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                //-----------------------------------------------------
                 if (_phylum.PhylumName.Contains("#") == false)
                {
                var tablePhylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 }); // 2. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " - " + _phylum.Author + "  " + _phylum.GerName + " " + _phylum.EngName + " " + _phylum.FraName + " " + _phylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tablePhylum);
                }
                //-----------------------------------------------------
            if (_subphylum.SubphylumName.Contains("#") == false)
            {
                var tableSubphylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubphylum.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubphylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 }); // 2. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(_subphylum.SubphylumName + " - " + _subphylum.Author + "  " + _subphylum.GerName + " " + _subphylum.EngName + " " + _subphylum.FraName + " " + _subphylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubphylum);
                }
            }
            //-----------------------------------------------------
            if (_superclass.SuperclassName.Contains("#") == false)
            {
            var tableSuperclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperclass.SetWidths(new[] { 0.14f, 1.16f, 2.50f, 1.50f });

            tableSuperclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superclass, SmallFont)) { Border = 0 });  // 2. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(_superclass.SuperclassName + " - " + _superclass.Author + "  " + _superclass.GerName + " " + _superclass.EngName + " " + _superclass.FraName + " " + _superclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperclass);
            }
            //-----------------------------------------------------
            if (_class.ClassName.Contains("#") == false)
            {
            var tableClass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableClass.SetWidths(new[] { 0.17f, 1.13f, 2.50f, 1.50f });

            tableClass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableClass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Class, SmallFont)) { Border = 0 });  // 2. field
            tableClass.AddCell(new PdfPCell(new Phrase(_class.ClassName + " - " + _class.Author + "  " + _class.GerName + " " + _class.EngName + " " + _class.FraName + " " + _class.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableClass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableClass);
            }
            //-----------------------------------------------------
            if (_subclass.SubclassName.Contains("#") == false)
            {
            var tableSubclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubclass.SetWidths(new[] { 0.20f, 1.10f, 2.50f, 1.50f });

            tableSubclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subclass, SmallFont)) { Border = 0 });  // 2. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(_subclass.SubclassName + " - " + _subclass.Author + "  " + _subclass.GerName + " " + _subclass.EngName + " " + _subclass.FraName + " " + _subclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubclass);
            }
            //-----------------------------------------------------
            if (_infraclass.InfraclassName.Contains("#") == false)
            {
            var tableInfraclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraclass.SetWidths(new[] { 0.23f, 1.07f, 2.50f, 1.50f });

            tableInfraclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraclass, SmallFont)) { Border = 0 });  // 2. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(_infraclass.InfraclassName + " - " + _infraclass.Author + "  " + _infraclass.GerName + " " + _infraclass.EngName + " " + _infraclass.FraName + " " + _infraclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraclass);
            }
            //-----------------------------------------------------
            if (_legio.LegioName.Contains("#") == false)
            {
            var tableLegio = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableLegio.SetWidths(new[] { 0.26f, 1.04f, 2.50f, 1.50f });

            tableLegio.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableLegio.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Legio, SmallFont)) { Border = 0 });  // 2. field
            tableLegio.AddCell(new PdfPCell(new Phrase(_legio.LegioName + " - " + _legio.Author + "  " + _legio.GerName + " " + _legio.EngName + " " + _legio.FraName + " " + _legio.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableLegio.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableLegio);
            }
            //-----------------------------------------------------
            if (_ordo.OrdoName.Contains("#") == false)
            {
            var tableOrdo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableOrdo.SetWidths(new[] { 0.29f, 1.01f, 2.50f, 1.50f });

            tableOrdo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableOrdo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Ordo, SmallFont)) { Border = 0 });  // 2. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(_ordo.OrdoName + " - " + _ordo.Author + "  " + _ordo.GerName + " " + _ordo.EngName + " " + _ordo.FraName + " " + _ordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableOrdo);
            }
            //-----------------------------------------------------
            if (_subordo.SubordoName.Contains("#") == false)
            {
            var tableSubordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubordo.SetWidths(new[] { 0.32f, 0.98f, 2.50f, 1.50f });

            tableSubordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subordo, SmallFont)) { Border = 0 });  // 2. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(_subordo.SubordoName + " - " + _subordo.Author + "  " + _subordo.GerName + " " + _subordo.EngName + " " + _subordo.FraName + " " + _subordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubordo);
            }
            //-----------------------------------------------------
            if (_infraordo.InfraordoName.Contains("#") == false)
            {
            var tableInfraordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraordo.SetWidths(new[] { 0.35f, 0.95f, 2.50f, 1.50f });

            tableInfraordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraordo, SmallFont)) { Border = 0 });  // 2. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(_infraordo.InfraordoName + " - " + _infraordo.Author + "  " + _infraordo.GerName + " " + _infraordo.EngName + " " + _infraordo.FraName + " " + _infraordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraordo);
            }
            //-----------------------------------------------------
            if (_superfamily.SuperfamilyName.Contains("#") == false)
            {
            var tableSuperfamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperfamily.SetWidths(new[] { 0.38f, 0.92f, 2.50f, 1.50f });

            tableSuperfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(_superfamily.SuperfamilyName + " - " + _superfamily.Author + "  " + _superfamily.GerName + " " + _superfamily.EngName + " " + _superfamily.FraName + " " + _superfamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperfamily);
            }
            //-----------------------------------------------------
            if (_family.FamilyName.Contains("#") == false)
            {
            var tableFamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f
            };
            tableFamily.SetWidths(new[] { 0.41f, 0.89f, 2.50f, 1.50f });

            tableFamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableFamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Family, SmallFont)) { Border = 0 });  // 2. field
            tableFamily.AddCell(new PdfPCell(new Phrase(_family.FamilyName + " - " + _family.Author + "  " + _family.GerName + " " + _family.EngName + " " + _family.FraName + " " + _family.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableFamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableFamily);
            }
            return doc;
        }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl48Subfamilies'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true, //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0, //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f
            };
            tableRegnum.SetWidths(new[] {0.05f, 1.25f, 2.50f, 1.50f  });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 }); // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont))  { Border = 0 }); // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum);
            }
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                //-----------------------------------------------------
            if (_division.DivisionName.Contains("#") == false)
            {
                var tableDivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableDivision.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 }); // 2. field
                tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " - " + _division.Author + "  " + _division.GerName + " " + _division.EngName + " " + _division.FraName + " " + _division.PorName,  SmallFont))  { Border = 0 }); // 3. field
                tableDivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableDivision);
                }
                //-----------------------------------------------------
            if (_subdivision.SubdivisionName.Contains("#") == false)
            {
                var tableSubdivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubdivision.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubdivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableSubdivision.AddCell( new PdfPCell(new Phrase(CultRes.StringsRes.Subdivision, SmallFont)) { Border = 0 }); // 2. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(_subdivision.SubdivisionName + " - " + _subdivision.Author + "  " + _subdivision.GerName + " " + _subdivision.EngName + " " + _subdivision.FraName + " " + _subdivision.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubdivision);
                }
                //-----------------------------------------------------
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                //-----------------------------------------------------
                if (_phylum.PhylumName.Contains("#") == false)
                {
                var tablePhylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 }); // 2. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " - " + _phylum.Author + "  " + _phylum.GerName + " " + _phylum.EngName + " " + _phylum.FraName + " " + _phylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tablePhylum);
                }
                //-----------------------------------------------------
            if (_subphylum.SubphylumName.Contains("#") == false)
            {
                var tableSubphylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubphylum.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubphylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 }); // 2. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(_subphylum.SubphylumName + " - " + _subphylum.Author + "  " + _subphylum.GerName + " " + _subphylum.EngName + " " + _subphylum.FraName + " " + _subphylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubphylum);
                 }
            }
            //-----------------------------------------------------
            if (_superclass.SuperclassName.Contains("#") == false)
            {
            var tableSuperclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperclass.SetWidths(new[] { 0.14f, 1.16f, 2.50f, 1.50f });

            tableSuperclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superclass, SmallFont)) { Border = 0 });  // 2. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(_superclass.SuperclassName + " - " + _superclass.Author + "  " + _superclass.GerName + " " + _superclass.EngName + " " + _superclass.FraName + " " + _superclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperclass);
             }
            //-----------------------------------------------------
            if (_class.ClassName.Contains("#") == false)
            {
            var tableClass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableClass.SetWidths(new[] { 0.17f, 1.13f, 2.50f, 1.50f });

            tableClass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableClass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Class, SmallFont)) { Border = 0 });  // 2. field
            tableClass.AddCell(new PdfPCell(new Phrase(_class.ClassName + " - " + _class.Author + "  " + _class.GerName + " " + _class.EngName + " " + _class.FraName + " " + _class.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableClass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableClass);
            }
            //-----------------------------------------------------
            if (_subclass.SubclassName.Contains("#") == false)
            {
            var tableSubclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubclass.SetWidths(new[] { 0.20f, 1.10f, 2.50f, 1.50f });

            tableSubclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subclass, SmallFont)) { Border = 0 });  // 2. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(_subclass.SubclassName + " - " + _subclass.Author + "  " + _subclass.GerName + " " + _subclass.EngName + " " + _subclass.FraName + " " + _subclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubclass);
            }
            //-----------------------------------------------------
            if (_infraclass.InfraclassName.Contains("#") == false)
            {
            var tableInfraclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraclass.SetWidths(new[] { 0.23f, 1.07f, 2.50f, 1.50f });

            tableInfraclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraclass, SmallFont)) { Border = 0 });  // 2. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(_infraclass.InfraclassName + " - " + _infraclass.Author + "  " + _infraclass.GerName + " " + _infraclass.EngName + " " + _infraclass.FraName + " " + _infraclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraclass);
            }
            //-----------------------------------------------------
            if (_legio.LegioName.Contains("#") == false)
            {
            var tableLegio = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableLegio.SetWidths(new[] { 0.26f, 1.04f, 2.50f, 1.50f });

            tableLegio.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableLegio.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Legio, SmallFont)) { Border = 0 });  // 2. field
            tableLegio.AddCell(new PdfPCell(new Phrase(_legio.LegioName + " - " + _legio.Author + "  " + _legio.GerName + " " + _legio.EngName + " " + _legio.FraName + " " + _legio.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableLegio.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableLegio);
            }
            //-----------------------------------------------------
            if (_ordo.OrdoName.Contains("#") == false)
            {
            var tableOrdo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableOrdo.SetWidths(new[] { 0.29f, 1.01f, 2.50f, 1.50f });

            tableOrdo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableOrdo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Ordo, SmallFont)) { Border = 0 });  // 2. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(_ordo.OrdoName + " - " + _ordo.Author + "  " + _ordo.GerName + " " + _ordo.EngName + " " + _ordo.FraName + " " + _ordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableOrdo);
            }
            //-----------------------------------------------------
            if (_subordo.SubordoName.Contains("#") == false)
            {
            var tableSubordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubordo.SetWidths(new[] { 0.32f, 0.98f, 2.50f, 1.50f });

            tableSubordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subordo, SmallFont)) { Border = 0 });  // 2. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(_subordo.SubordoName + " - " + _subordo.Author + "  " + _subordo.GerName + " " + _subordo.EngName + " " + _subordo.FraName + " " + _subordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubordo);
            }
            //-----------------------------------------------------
            if (_infraordo.InfraordoName.Contains("#") == false)
            {
            var tableInfraordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraordo.SetWidths(new[] { 0.35f, 0.95f, 2.50f, 1.50f });

            tableInfraordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraordo, SmallFont)) { Border = 0 });  // 2. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(_infraordo.InfraordoName + " - " + _infraordo.Author + "  " + _infraordo.GerName + " " + _infraordo.EngName + " " + _infraordo.FraName + " " + _infraordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraordo);
            }
            //-----------------------------------------------------
            if (_superfamily.SuperfamilyName.Contains("#") == false)
            {
            var tableSuperfamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperfamily.SetWidths(new[] { 0.38f, 0.92f, 2.50f, 1.50f });

            tableSuperfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(_superfamily.SuperfamilyName + " - " + _superfamily.Author + "  " + _superfamily.GerName + " " + _superfamily.EngName + " " + _superfamily.FraName + " " + _superfamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperfamily);
            }
            //-----------------------------------------------------
            if (_family.FamilyName.Contains("#") == false)
            {
            var tableFamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableFamily.SetWidths(new[] { 0.41f, 0.89f, 2.50f, 1.50f });

            tableFamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableFamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Family, SmallFont)) { Border = 0 });  // 2. field
            tableFamily.AddCell(new PdfPCell(new Phrase(_family.FamilyName + " - " + _family.Author + "  " + _family.GerName + " " + _family.EngName + " " + _family.FraName + " " + _family.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableFamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableFamily);
            }
            //-----------------------------------------------------
            if (_subfamily.SubfamilyName.Contains("#") == false)
            {
            var tableSubfamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f
            };
            tableSubfamily.SetWidths(new[] { 0.44f, 0.86f, 2.50f, 1.50f });

            tableSubfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(_subfamily.SubfamilyName + " - " + _subfamily.Author + "  " + _subfamily.GerName + " " + _subfamily.EngName + " " + _subfamily.FraName + " " + _subfamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubfamily);
            }
            return doc;
        }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl51Infrafamilies'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true, //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0, //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f
            };
            tableRegnum.SetWidths(new[] {0.05f, 1.25f, 2.50f, 1.50f  });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 }); // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont))  { Border = 0 }); // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum);
            }
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                //-----------------------------------------------------
            if (_division.DivisionName.Contains("#") == false)
            {
                var tableDivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableDivision.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 }); // 2. field
                tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " - " + _division.Author + "  " + _division.GerName + " " + _division.EngName + " " + _division.FraName + " " + _division.PorName,  SmallFont))  { Border = 0 }); // 3. field
                tableDivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableDivision);
                }
                //-----------------------------------------------------
            if (_subdivision.SubdivisionName.Contains("#") == false)
            {
                var tableSubdivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubdivision.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubdivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableSubdivision.AddCell( new PdfPCell(new Phrase(CultRes.StringsRes.Subdivision, SmallFont)) { Border = 0 }); // 2. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(_subdivision.SubdivisionName + " - " + _subdivision.Author + "  " + _subdivision.GerName + " " + _subdivision.EngName + " " + _subdivision.FraName + " " + _subdivision.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubdivision);
                }
                //-----------------------------------------------------
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                //-----------------------------------------------------
                if (_phylum.PhylumName.Contains("#") == false)
                {
                var tablePhylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 }); // 2. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " - " + _phylum.Author + "  " + _phylum.GerName + " " + _phylum.EngName + " " + _phylum.FraName + " " + _phylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tablePhylum);
                }
                //-----------------------------------------------------
            if (_subphylum.SubphylumName.Contains("#") == false)
            {
                var tableSubphylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubphylum.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubphylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 }); // 2. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(_subphylum.SubphylumName + " - " + _subphylum.Author + "  " + _subphylum.GerName + " " + _subphylum.EngName + " " + _subphylum.FraName + " " + _subphylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubphylum);
                }
            }
            //-----------------------------------------------------
            if (_superclass.SuperclassName.Contains("#") == false)
            {
            var tableSuperclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperclass.SetWidths(new[] { 0.14f, 1.16f, 2.50f, 1.50f });

            tableSuperclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superclass, SmallFont)) { Border = 0 });  // 2. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(_superclass.SuperclassName + " - " + _superclass.Author + "  " + _superclass.GerName + " " + _superclass.EngName + " " + _superclass.FraName + " " + _superclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperclass);
            }
            //-----------------------------------------------------
            if (_class.ClassName.Contains("#") == false)
            {
            var tableClass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableClass.SetWidths(new[] { 0.17f, 1.13f, 2.50f, 1.50f });

            tableClass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableClass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Class, SmallFont)) { Border = 0 });  // 2. field
            tableClass.AddCell(new PdfPCell(new Phrase(_class.ClassName + " - " + _class.Author + "  " + _class.GerName + " " + _class.EngName + " " + _class.FraName + " " + _class.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableClass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableClass);
            }
            //-----------------------------------------------------
            if (_subclass.SubclassName.Contains("#") == false)
            {
            var tableSubclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubclass.SetWidths(new[] { 0.20f, 1.10f, 2.50f, 1.50f });

            tableSubclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subclass, SmallFont)) { Border = 0 });  // 2. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(_subclass.SubclassName + " - " + _subclass.Author + "  " + _subclass.GerName + " " + _subclass.EngName + " " + _subclass.FraName + " " + _subclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubclass);
             }
            //-----------------------------------------------------
            if (_infraclass.InfraclassName.Contains("#") == false)
            {
            var tableInfraclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraclass.SetWidths(new[] { 0.23f, 1.07f, 2.50f, 1.50f });

            tableInfraclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraclass, SmallFont)) { Border = 0 });  // 2. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(_infraclass.InfraclassName + " - " + _infraclass.Author + "  " + _infraclass.GerName + " " + _infraclass.EngName + " " + _infraclass.FraName + " " + _infraclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraclass);
            }
            //-----------------------------------------------------
            if (_legio.LegioName.Contains("#") == false)
            {
            var tableLegio = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableLegio.SetWidths(new[] { 0.26f, 1.04f, 2.50f, 1.50f });

            tableLegio.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableLegio.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Legio, SmallFont)) { Border = 0 });  // 2. field
            tableLegio.AddCell(new PdfPCell(new Phrase(_legio.LegioName + " - " + _legio.Author + "  " + _legio.GerName + " " + _legio.EngName + " " + _legio.FraName + " " + _legio.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableLegio.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableLegio);
            }
            //-----------------------------------------------------
            if (_ordo.OrdoName.Contains("#") == false)
            {
            var tableOrdo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableOrdo.SetWidths(new[] { 0.29f, 1.01f, 2.50f, 1.50f });

            tableOrdo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableOrdo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Ordo, SmallFont)) { Border = 0 });  // 2. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(_ordo.OrdoName + " - " + _ordo.Author + "  " + _ordo.GerName + " " + _ordo.EngName + " " + _ordo.FraName + " " + _ordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableOrdo);
            }
            //-----------------------------------------------------
            if (_subordo.SubordoName.Contains("#") == false)
            {
            var tableSubordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubordo.SetWidths(new[] { 0.32f, 0.98f, 2.50f, 1.50f });

            tableSubordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subordo, SmallFont)) { Border = 0 });  // 2. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(_subordo.SubordoName + " - " + _subordo.Author + "  " + _subordo.GerName + " " + _subordo.EngName + " " + _subordo.FraName + " " + _subordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubordo);
            }
            //-----------------------------------------------------
            if (_infraordo.InfraordoName.Contains("#") == false)
            {
            var tableInfraordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraordo.SetWidths(new[] { 0.35f, 0.95f, 2.50f, 1.50f });

            tableInfraordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraordo, SmallFont)) { Border = 0 });  // 2. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(_infraordo.InfraordoName + " - " + _infraordo.Author + "  " + _infraordo.GerName + " " + _infraordo.EngName + " " + _infraordo.FraName + " " + _infraordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraordo);
            }
            //-----------------------------------------------------
            if (_superfamily.SuperfamilyName.Contains("#") == false)
            {
            var tableSuperfamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperfamily.SetWidths(new[] { 0.38f, 0.92f, 2.50f, 1.50f });

            tableSuperfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(_superfamily.SuperfamilyName + " - " + _superfamily.Author + "  " + _superfamily.GerName + " " + _superfamily.EngName + " " + _superfamily.FraName + " " + _superfamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperfamily);
            }
            //-----------------------------------------------------
            if (_family.FamilyName.Contains("#") == false)
            {
            var tableFamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableFamily.SetWidths(new[] { 0.41f, 0.89f, 2.50f, 1.50f });

            tableFamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableFamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Family, SmallFont)) { Border = 0 });  // 2. field
            tableFamily.AddCell(new PdfPCell(new Phrase(_family.FamilyName + " - " + _family.Author + "  " + _family.GerName + " " + _family.EngName + " " + _family.FraName + " " + _family.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableFamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableFamily);
            }
            //-----------------------------------------------------
            if (_subfamily.SubfamilyName.Contains("#") == false)
            {
            var tableSubfamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubfamily.SetWidths(new[] { 0.44f, 0.86f, 2.50f, 1.50f });

            tableSubfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(_subfamily.SubfamilyName + " - " + _subfamily.Author + "  " + _subfamily.GerName + " " + _subfamily.EngName + " " + _subfamily.FraName + " " + _subfamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubfamily);
            }
            //-----------------------------------------------------
            if (_infrafamily.InfrafamilyName.Contains("#") == false)
            {
            var tableInfrafamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f
            };
            tableInfrafamily.SetWidths(new[] { 0.47f, 0.83f, 2.50f, 1.50f });

            tableInfrafamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infrafamily, SmallFont)) { Border = 0 });  // 2. field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(_infrafamily.InfrafamilyName + " - " + _infrafamily.Author + "  " + _infrafamily.GerName + " " + _infrafamily.EngName + " " + _infrafamily.FraName + " " + _infrafamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfrafamily);
            }
            return doc;
        }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl54Supertribusses'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true, //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0, //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f
            };
            tableRegnum.SetWidths(new[] {0.05f, 1.25f, 2.50f, 1.50f  });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 }); // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont))  { Border = 0 }); // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum);
            }
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                //-----------------------------------------------------
            if (_division.DivisionName.Contains("#") == false)
            {
                var tableDivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableDivision.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 }); // 2. field
                tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " - " + _division.Author + "  " + _division.GerName + " " + _division.EngName + " " + _division.FraName + " " + _division.PorName,  SmallFont))  { Border = 0 }); // 3. field
                tableDivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableDivision);
                }
                //-----------------------------------------------------
            if (_subdivision.SubdivisionName.Contains("#") == false)
            {
                var tableSubdivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubdivision.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubdivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableSubdivision.AddCell( new PdfPCell(new Phrase(CultRes.StringsRes.Subdivision, SmallFont)) { Border = 0 }); // 2. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(_subdivision.SubdivisionName + " - " + _subdivision.Author + "  " + _subdivision.GerName + " " + _subdivision.EngName + " " + _subdivision.FraName + " " + _subdivision.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubdivision);
                }
                //-----------------------------------------------------
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                //-----------------------------------------------------
                if (_phylum.PhylumName.Contains("#") == false)
                {
                var tablePhylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 }); // 2. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " - " + _phylum.Author + "  " + _phylum.GerName + " " + _phylum.EngName + " " + _phylum.FraName + " " + _phylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tablePhylum);
                }
                //-----------------------------------------------------
            if (_subphylum.SubphylumName.Contains("#") == false)
            {
                var tableSubphylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubphylum.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubphylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 }); // 2. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(_subphylum.SubphylumName + " - " + _subphylum.Author + "  " + _subphylum.GerName + " " + _subphylum.EngName + " " + _subphylum.FraName + " " + _subphylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubphylum);
                }
            }
            //-----------------------------------------------------
            if (_superclass.SuperclassName.Contains("#") == false)
            {
            var tableSuperclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperclass.SetWidths(new[] { 0.14f, 1.16f, 2.50f, 1.50f });

            tableSuperclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superclass, SmallFont)) { Border = 0 });  // 2. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(_superclass.SuperclassName + " - " + _superclass.Author + "  " + _superclass.GerName + " " + _superclass.EngName + " " + _superclass.FraName + " " + _superclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperclass);
            }
            //-----------------------------------------------------
            if (_class.ClassName.Contains("#") == false)
            {
            var tableClass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableClass.SetWidths(new[] { 0.17f, 1.13f, 2.50f, 1.50f });

            tableClass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableClass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Class, SmallFont)) { Border = 0 });  // 2. field
            tableClass.AddCell(new PdfPCell(new Phrase(_class.ClassName + " - " + _class.Author + "  " + _class.GerName + " " + _class.EngName + " " + _class.FraName + " " + _class.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableClass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableClass);
            }
            //-----------------------------------------------------
            if (_subclass.SubclassName.Contains("#") == false)
            {
            var tableSubclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubclass.SetWidths(new[] { 0.20f, 1.10f, 2.50f, 1.50f });

            tableSubclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subclass, SmallFont)) { Border = 0 });  // 2. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(_subclass.SubclassName + " - " + _subclass.Author + "  " + _subclass.GerName + " " + _subclass.EngName + " " + _subclass.FraName + " " + _subclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubclass);
             }
            //-----------------------------------------------------
            if (_infraclass.InfraclassName.Contains("#") == false)
            {
            var tableInfraclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraclass.SetWidths(new[] { 0.23f, 1.07f, 2.50f, 1.50f });

            tableInfraclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraclass, SmallFont)) { Border = 0 });  // 2. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(_infraclass.InfraclassName + " - " + _infraclass.Author + "  " + _infraclass.GerName + " " + _infraclass.EngName + " " + _infraclass.FraName + " " + _infraclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraclass);
            }
            //-----------------------------------------------------
            if (_legio.LegioName.Contains("#") == false)
            {
            var tableLegio = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableLegio.SetWidths(new[] { 0.26f, 1.04f, 2.50f, 1.50f });

            tableLegio.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableLegio.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Legio, SmallFont)) { Border = 0 });  // 2. field
            tableLegio.AddCell(new PdfPCell(new Phrase(_legio.LegioName + " - " + _legio.Author + "  " + _legio.GerName + " " + _legio.EngName + " " + _legio.FraName + " " + _legio.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableLegio.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableLegio);
            }
            //-----------------------------------------------------
            if (_ordo.OrdoName.Contains("#") == false)
            {
            var tableOrdo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableOrdo.SetWidths(new[] { 0.29f, 1.01f, 2.50f, 1.50f });

            tableOrdo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableOrdo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Ordo, SmallFont)) { Border = 0 });  // 2. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(_ordo.OrdoName + " - " + _ordo.Author + "  " + _ordo.GerName + " " + _ordo.EngName + " " + _ordo.FraName + " " + _ordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableOrdo);
            }
            //-----------------------------------------------------
            if (_subordo.SubordoName.Contains("#") == false)
            {
            var tableSubordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubordo.SetWidths(new[] { 0.32f, 0.98f, 2.50f, 1.50f });

            tableSubordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subordo, SmallFont)) { Border = 0 });  // 2. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(_subordo.SubordoName + " - " + _subordo.Author + "  " + _subordo.GerName + " " + _subordo.EngName + " " + _subordo.FraName + " " + _subordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubordo);
            }
            //-----------------------------------------------------
            if (_infraordo.InfraordoName.Contains("#") == false)
            {
            var tableInfraordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraordo.SetWidths(new[] { 0.35f, 0.95f, 2.50f, 1.50f });

            tableInfraordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraordo, SmallFont)) { Border = 0 });  // 2. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(_infraordo.InfraordoName + " - " + _infraordo.Author + "  " + _infraordo.GerName + " " + _infraordo.EngName + " " + _infraordo.FraName + " " + _infraordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraordo);
            }
            //-----------------------------------------------------
            if (_superfamily.SuperfamilyName.Contains("#") == false)
            {
            var tableSuperfamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperfamily.SetWidths(new[] { 0.38f, 0.92f, 2.50f, 1.50f });

            tableSuperfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(_superfamily.SuperfamilyName + " - " + _superfamily.Author + "  " + _superfamily.GerName + " " + _superfamily.EngName + " " + _superfamily.FraName + " " + _superfamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperfamily);
            }
            //-----------------------------------------------------
            if (_family.FamilyName.Contains("#") == false)
            {
            var tableFamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableFamily.SetWidths(new[] { 0.41f, 0.89f, 2.50f, 1.50f });

            tableFamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableFamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Family, SmallFont)) { Border = 0 });  // 2. field
            tableFamily.AddCell(new PdfPCell(new Phrase(_family.FamilyName + " - " + _family.Author + "  " + _family.GerName + " " + _family.EngName + " " + _family.FraName + " " + _family.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableFamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableFamily);
            }
            //-----------------------------------------------------
            if (_subfamily.SubfamilyName.Contains("#") == false)
            {
            var tableSubfamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubfamily.SetWidths(new[] { 0.44f, 0.86f, 2.50f, 1.50f });

            tableSubfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(_subfamily.SubfamilyName + " - " + _subfamily.Author + "  " + _subfamily.GerName + " " + _subfamily.EngName + " " + _subfamily.FraName + " " + _subfamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubfamily);
            }
            //-----------------------------------------------------
            if (_infrafamily.InfrafamilyName.Contains("#") == false)
            {
            var tableInfrafamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfrafamily.SetWidths(new[] { 0.47f, 0.83f, 2.50f, 1.50f });

            tableInfrafamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infrafamily, SmallFont)) { Border = 0 });  // 2. field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(_infrafamily.InfrafamilyName + " - " + _infrafamily.Author + "  " + _infrafamily.GerName + " " + _infrafamily.EngName + " " + _infrafamily.FraName + " " + _infrafamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfrafamily);
            }
            //-----------------------------------------------------
            if (_supertribus.SupertribusName.Contains("#") == false)
            {
            var tableSupertribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f
            };
            tableSupertribus.SetWidths(new[] { 0.50f, 0.80f, 2.50f, 1.50f });

            tableSupertribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Supertribus, SmallFont)) { Border = 0 });  // 2. field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(_supertribus.SupertribusName + " - " + _supertribus.Author + "  " + _supertribus.GerName + " " + _supertribus.EngName + " " + _supertribus.FraName + " " + _supertribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSupertribus);
            }
            return doc;
        }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl57Tribusses'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true, //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0, //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f
            };
            tableRegnum.SetWidths(new[] {0.05f, 1.25f, 2.50f, 1.50f  });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 }); // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont))  { Border = 0 }); // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum);
            }
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                //-----------------------------------------------------
            if (_division.DivisionName.Contains("#") == false)
            {
                var tableDivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableDivision.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 }); // 2. field
                tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " - " + _division.Author + "  " + _division.GerName + " " + _division.EngName + " " + _division.FraName + " " + _division.PorName,  SmallFont))  { Border = 0 }); // 3. field
                tableDivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableDivision);
                }
                //-----------------------------------------------------
            if (_subdivision.SubdivisionName.Contains("#") == false)
            {
                var tableSubdivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubdivision.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubdivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableSubdivision.AddCell( new PdfPCell(new Phrase(CultRes.StringsRes.Subdivision, SmallFont)) { Border = 0 }); // 2. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(_subdivision.SubdivisionName + " - " + _subdivision.Author + "  " + _subdivision.GerName + " " + _subdivision.EngName + " " + _subdivision.FraName + " " + _subdivision.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubdivision);
                }
                //-----------------------------------------------------
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                //-----------------------------------------------------
                if (_phylum.PhylumName.Contains("#") == false)
                {
                var tablePhylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 }); // 2. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " - " + _phylum.Author + "  " + _phylum.GerName + " " + _phylum.EngName + " " + _phylum.FraName + " " + _phylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tablePhylum);
                }
                //-----------------------------------------------------
            if (_subphylum.SubphylumName.Contains("#") == false)
            {
                var tableSubphylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubphylum.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubphylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 }); // 2. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(_subphylum.SubphylumName + " - " + _subphylum.Author + "  " + _subphylum.GerName + " " + _subphylum.EngName + " " + _subphylum.FraName + " " + _subphylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubphylum);
                }
            }
            //-----------------------------------------------------
            if (_superclass.SuperclassName.Contains("#") == false)
            {
            var tableSuperclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperclass.SetWidths(new[] { 0.14f, 1.16f, 2.50f, 1.50f });

            tableSuperclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superclass, SmallFont)) { Border = 0 });  // 2. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(_superclass.SuperclassName + " - " + _superclass.Author + "  " + _superclass.GerName + " " + _superclass.EngName + " " + _superclass.FraName + " " + _superclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperclass);
            }
            //-----------------------------------------------------
            if (_class.ClassName.Contains("#") == false)
            {
            var tableClass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableClass.SetWidths(new[] { 0.17f, 1.13f, 2.50f, 1.50f });

            tableClass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableClass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Class, SmallFont)) { Border = 0 });  // 2. field
            tableClass.AddCell(new PdfPCell(new Phrase(_class.ClassName + " - " + _class.Author + "  " + _class.GerName + " " + _class.EngName + " " + _class.FraName + " " + _class.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableClass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableClass);
            }
            //-----------------------------------------------------
            if (_subclass.SubclassName.Contains("#") == false)
            {
            var tableSubclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubclass.SetWidths(new[] { 0.20f, 1.10f, 2.50f, 1.50f });

            tableSubclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subclass, SmallFont)) { Border = 0 });  // 2. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(_subclass.SubclassName + " - " + _subclass.Author + "  " + _subclass.GerName + " " + _subclass.EngName + " " + _subclass.FraName + " " + _subclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubclass);
             }
            //-----------------------------------------------------
            if (_infraclass.InfraclassName.Contains("#") == false)
            {
            var tableInfraclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraclass.SetWidths(new[] { 0.23f, 1.07f, 2.50f, 1.50f });

            tableInfraclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraclass, SmallFont)) { Border = 0 });  // 2. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(_infraclass.InfraclassName + " - " + _infraclass.Author + "  " + _infraclass.GerName + " " + _infraclass.EngName + " " + _infraclass.FraName + " " + _infraclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraclass);
            }
            //-----------------------------------------------------
            if (_legio.LegioName.Contains("#") == false)
            {
            var tableLegio = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableLegio.SetWidths(new[] { 0.26f, 1.04f, 2.50f, 1.50f });

            tableLegio.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableLegio.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Legio, SmallFont)) { Border = 0 });  // 2. field
            tableLegio.AddCell(new PdfPCell(new Phrase(_legio.LegioName + " - " + _legio.Author + "  " + _legio.GerName + " " + _legio.EngName + " " + _legio.FraName + " " + _legio.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableLegio.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableLegio);
            }
            //-----------------------------------------------------
            if (_ordo.OrdoName.Contains("#") == false)
            {
            var tableOrdo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableOrdo.SetWidths(new[] { 0.29f, 1.01f, 2.50f, 1.50f });

            tableOrdo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableOrdo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Ordo, SmallFont)) { Border = 0 });  // 2. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(_ordo.OrdoName + " - " + _ordo.Author + "  " + _ordo.GerName + " " + _ordo.EngName + " " + _ordo.FraName + " " + _ordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableOrdo);
            }
            //-----------------------------------------------------
            if (_subordo.SubordoName.Contains("#") == false)
            {
            var tableSubordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubordo.SetWidths(new[] { 0.32f, 0.98f, 2.50f, 1.50f });

            tableSubordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subordo, SmallFont)) { Border = 0 });  // 2. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(_subordo.SubordoName + " - " + _subordo.Author + "  " + _subordo.GerName + " " + _subordo.EngName + " " + _subordo.FraName + " " + _subordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubordo);
            }
            //-----------------------------------------------------
            if (_infraordo.InfraordoName.Contains("#") == false)
            {
            var tableInfraordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraordo.SetWidths(new[] { 0.35f, 0.95f, 2.50f, 1.50f });

            tableInfraordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraordo, SmallFont)) { Border = 0 });  // 2. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(_infraordo.InfraordoName + " - " + _infraordo.Author + "  " + _infraordo.GerName + " " + _infraordo.EngName + " " + _infraordo.FraName + " " + _infraordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraordo);
            }
            //-----------------------------------------------------
            if (_superfamily.SuperfamilyName.Contains("#") == false)
            {
            var tableSuperfamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperfamily.SetWidths(new[] { 0.38f, 0.92f, 2.50f, 1.50f });

            tableSuperfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(_superfamily.SuperfamilyName + " - " + _superfamily.Author + "  " + _superfamily.GerName + " " + _superfamily.EngName + " " + _superfamily.FraName + " " + _superfamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperfamily);
            }
            //-----------------------------------------------------
            if (_family.FamilyName.Contains("#") == false)
            {
            var tableFamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableFamily.SetWidths(new[] { 0.41f, 0.89f, 2.50f, 1.50f });

            tableFamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableFamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Family, SmallFont)) { Border = 0 });  // 2. field
            tableFamily.AddCell(new PdfPCell(new Phrase(_family.FamilyName + " - " + _family.Author + "  " + _family.GerName + " " + _family.EngName + " " + _family.FraName + " " + _family.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableFamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableFamily);
            }
            //-----------------------------------------------------
            if (_subfamily.SubfamilyName.Contains("#") == false)
            {
            var tableSubfamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubfamily.SetWidths(new[] { 0.44f, 0.86f, 2.50f, 1.50f });

            tableSubfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(_subfamily.SubfamilyName + " - " + _subfamily.Author + "  " + _subfamily.GerName + " " + _subfamily.EngName + " " + _subfamily.FraName + " " + _subfamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubfamily);
            }
            //-----------------------------------------------------
            if (_infrafamily.InfrafamilyName.Contains("#") == false)
            {
            var tableInfrafamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfrafamily.SetWidths(new[] { 0.47f, 0.83f, 2.50f, 1.50f });

            tableInfrafamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infrafamily, SmallFont)) { Border = 0 });  // 2. field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(_infrafamily.InfrafamilyName + " - " + _infrafamily.Author + "  " + _infrafamily.GerName + " " + _infrafamily.EngName + " " + _infrafamily.FraName + " " + _infrafamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfrafamily);
            }
            //-----------------------------------------------------
            if (_supertribus.SupertribusName.Contains("#") == false)
            {
            var tablesupertribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tablesupertribus.SetWidths(new[] { 0.50f, 0.80f, 2.50f, 1.50f });

            tablesupertribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tablesupertribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Supertribus, SmallFont)) { Border = 0 });  // 2. field
            tablesupertribus.AddCell(new PdfPCell(new Phrase(_supertribus.SupertribusName + " - " + _supertribus.Author + "  " + _supertribus.GerName + " " + _supertribus.EngName + " " + _supertribus.FraName + " " + _supertribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tablesupertribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tablesupertribus);
            }
            //-----------------------------------------------------
            if (_tribus.TribusName.Contains("#") == false)
            {
            var tabletribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f
            };
            tabletribus.SetWidths(new[] { 0.53f, 0.77f, 2.50f, 1.50f });

            tabletribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tabletribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Tribus, SmallFont)) { Border = 0 });  // 2. field
            tabletribus.AddCell(new PdfPCell(new Phrase(_tribus.TribusName + " - " + _tribus.Author + "  " + _tribus.GerName + " " + _tribus.EngName + " " + _tribus.FraName + " " + _tribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tabletribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tabletribus);
            }
            return doc;
        }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl60Subtribusses'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true, //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0, //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f
            };
            tableRegnum.SetWidths(new[] {0.05f, 1.25f, 2.50f, 1.50f  });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 }); // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont))  { Border = 0 }); // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum);
            }
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                //-----------------------------------------------------
            if (_division.DivisionName.Contains("#") == false)
            {
                var tableDivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableDivision.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 }); // 2. field
                tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " - " + _division.Author + "  " + _division.GerName + " " + _division.EngName + " " + _division.FraName + " " + _division.PorName,  SmallFont))  { Border = 0 }); // 3. field
                tableDivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableDivision);
                }
                //-----------------------------------------------------
            if (_subdivision.SubdivisionName.Contains("#") == false)
            {
                var tableSubdivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubdivision.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubdivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableSubdivision.AddCell( new PdfPCell(new Phrase(CultRes.StringsRes.Subdivision, SmallFont)) { Border = 0 }); // 2. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(_subdivision.SubdivisionName + " - " + _subdivision.Author + "  " + _subdivision.GerName + " " + _subdivision.EngName + " " + _subdivision.FraName + " " + _subdivision.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubdivision);
                }
                //-----------------------------------------------------
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                //-----------------------------------------------------
                if (_phylum.PhylumName.Contains("#") == false)
                {
                var tablePhylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 }); // 2. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " - " + _phylum.Author + "  " + _phylum.GerName + " " + _phylum.EngName + " " + _phylum.FraName + " " + _phylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tablePhylum);
                }
                //-----------------------------------------------------
            if (_subphylum.SubphylumName.Contains("#") == false)
            {
                var tableSubphylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubphylum.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubphylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 }); // 2. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(_subphylum.SubphylumName + " - " + _subphylum.Author + "  " + _subphylum.GerName + " " + _subphylum.EngName + " " + _subphylum.FraName + " " + _subphylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubphylum);
                }
            }
            //-----------------------------------------------------
            if (_superclass.SuperclassName.Contains("#") == false)
            {
            var tableSuperclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperclass.SetWidths(new[] { 0.14f, 1.16f, 2.50f, 1.50f });

            tableSuperclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superclass, SmallFont)) { Border = 0 });  // 2. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(_superclass.SuperclassName + " - " + _superclass.Author + "  " + _superclass.GerName + " " + _superclass.EngName + " " + _superclass.FraName + " " + _superclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperclass);
            }
            //-----------------------------------------------------
            if (_class.ClassName.Contains("#") == false)
            {
            var tableClass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableClass.SetWidths(new[] { 0.17f, 1.13f, 2.50f, 1.50f });

            tableClass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableClass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Class, SmallFont)) { Border = 0 });  // 2. field
            tableClass.AddCell(new PdfPCell(new Phrase(_class.ClassName + " - " + _class.Author + "  " + _class.GerName + " " + _class.EngName + " " + _class.FraName + " " + _class.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableClass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableClass);
            }
            //-----------------------------------------------------
            if (_subclass.SubclassName.Contains("#") == false)
            {
            var tableSubclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubclass.SetWidths(new[] { 0.20f, 1.10f, 2.50f, 1.50f });

            tableSubclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subclass, SmallFont)) { Border = 0 });  // 2. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(_subclass.SubclassName + " - " + _subclass.Author + "  " + _subclass.GerName + " " + _subclass.EngName + " " + _subclass.FraName + " " + _subclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubclass);
             }
            //-----------------------------------------------------
            if (_infraclass.InfraclassName.Contains("#") == false)
            {
            var tableInfraclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraclass.SetWidths(new[] { 0.23f, 1.07f, 2.50f, 1.50f });

            tableInfraclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraclass, SmallFont)) { Border = 0 });  // 2. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(_infraclass.InfraclassName + " - " + _infraclass.Author + "  " + _infraclass.GerName + " " + _infraclass.EngName + " " + _infraclass.FraName + " " + _infraclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraclass);
            }
            //-----------------------------------------------------
            if (_legio.LegioName.Contains("#") == false)
            {
            var tableLegio = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableLegio.SetWidths(new[] { 0.26f, 1.04f, 2.50f, 1.50f });

            tableLegio.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableLegio.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Legio, SmallFont)) { Border = 0 });  // 2. field
            tableLegio.AddCell(new PdfPCell(new Phrase(_legio.LegioName + " - " + _legio.Author + "  " + _legio.GerName + " " + _legio.EngName + " " + _legio.FraName + " " + _legio.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableLegio.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableLegio);
            }
            //-----------------------------------------------------
            if (_ordo.OrdoName.Contains("#") == false)
            {
            var tableOrdo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableOrdo.SetWidths(new[] { 0.29f, 1.01f, 2.50f, 1.50f });

            tableOrdo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableOrdo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Ordo, SmallFont)) { Border = 0 });  // 2. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(_ordo.OrdoName + " - " + _ordo.Author + "  " + _ordo.GerName + " " + _ordo.EngName + " " + _ordo.FraName + " " + _ordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableOrdo);
            }
            //-----------------------------------------------------
            if (_subordo.SubordoName.Contains("#") == false)
            {
            var tableSubordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubordo.SetWidths(new[] { 0.32f, 0.98f, 2.50f, 1.50f });

            tableSubordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subordo, SmallFont)) { Border = 0 });  // 2. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(_subordo.SubordoName + " - " + _subordo.Author + "  " + _subordo.GerName + " " + _subordo.EngName + " " + _subordo.FraName + " " + _subordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubordo);
            }
            //-----------------------------------------------------
            if (_infraordo.InfraordoName.Contains("#") == false)
            {
            var tableInfraordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraordo.SetWidths(new[] { 0.35f, 0.95f, 2.50f, 1.50f });

            tableInfraordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraordo, SmallFont)) { Border = 0 });  // 2. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(_infraordo.InfraordoName + " - " + _infraordo.Author + "  " + _infraordo.GerName + " " + _infraordo.EngName + " " + _infraordo.FraName + " " + _infraordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraordo);
            }
            //-----------------------------------------------------
            if (_superfamily.SuperfamilyName.Contains("#") == false)
            {
            var tableSuperfamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperfamily.SetWidths(new[] { 0.38f, 0.92f, 2.50f, 1.50f });

            tableSuperfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(_superfamily.SuperfamilyName + " - " + _superfamily.Author + "  " + _superfamily.GerName + " " + _superfamily.EngName + " " + _superfamily.FraName + " " + _superfamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperfamily);
            }
            //-----------------------------------------------------
            if (_family.FamilyName.Contains("#") == false)
            {
            var tableFamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableFamily.SetWidths(new[] { 0.41f, 0.89f, 2.50f, 1.50f });

            tableFamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableFamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Family, SmallFont)) { Border = 0 });  // 2. field
            tableFamily.AddCell(new PdfPCell(new Phrase(_family.FamilyName + " - " + _family.Author + "  " + _family.GerName + " " + _family.EngName + " " + _family.FraName + " " + _family.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableFamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableFamily);
            }
            //-----------------------------------------------------
            if (_subfamily.SubfamilyName.Contains("#") == false)
            {
            var tableSubfamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubfamily.SetWidths(new[] { 0.44f, 0.86f, 2.50f, 1.50f });

            tableSubfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(_subfamily.SubfamilyName + " - " + _subfamily.Author + "  " + _subfamily.GerName + " " + _subfamily.EngName + " " + _subfamily.FraName + " " + _subfamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubfamily);
            }
            //-----------------------------------------------------
            if (_infrafamily.InfrafamilyName.Contains("#") == false)
            {
            var tableInfrafamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfrafamily.SetWidths(new[] { 0.47f, 0.83f, 2.50f, 1.50f });

            tableInfrafamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infrafamily, SmallFont)) { Border = 0 });  // 2. field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(_infrafamily.InfrafamilyName + " - " + _infrafamily.Author + "  " + _infrafamily.GerName + " " + _infrafamily.EngName + " " + _infrafamily.FraName + " " + _infrafamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfrafamily);
            }
            //-----------------------------------------------------
            if (_supertribus.SupertribusName.Contains("#") == false)
            {
            var tableSupertribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSupertribus.SetWidths(new[] { 0.50f, 0.80f, 2.50f, 1.50f });

            tableSupertribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Supertribus, SmallFont)) { Border = 0 });  // 2. field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(_supertribus.SupertribusName + " - " + _supertribus.Author + "  " + _supertribus.GerName + " " + _supertribus.EngName + " " + _supertribus.FraName + " " + _supertribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSupertribus);
            }
            //-----------------------------------------------------
            if (_tribus.TribusName.Contains("#") == false)
            {
            var tabletribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tabletribus.SetWidths(new[] { 0.53f, 0.77f, 2.50f, 1.50f });

            tabletribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tabletribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Tribus, SmallFont)) { Border = 0 });  // 2. field
            tabletribus.AddCell(new PdfPCell(new Phrase(_tribus.TribusName + " - " + _tribus.Author + "  " + _tribus.GerName + " " + _tribus.EngName + " " + _tribus.FraName + " " + _tribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tabletribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tabletribus);
            }
            //-----------------------------------------------------
            if (_subtribus.SubtribusName.Contains("#") == false)
            {
            var tablesubtribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f
            };
            tablesubtribus.SetWidths(new[] { 0.56f, 0.74f, 2.50f, 1.50f });

            tablesubtribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tablesubtribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subtribus, SmallFont)) { Border = 0 });  // 2. field
            tablesubtribus.AddCell(new PdfPCell(new Phrase(_subtribus.SubtribusName + " - " + _subtribus.Author + "  " + _subtribus.GerName + " " + _subtribus.EngName + " " + _subtribus.FraName + " " + _subtribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tablesubtribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tablesubtribus);
            }
            return doc;
        }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl63Infratribusses'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true, //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0, //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f
            };
            tableRegnum.SetWidths(new[] {0.05f, 1.25f, 2.50f, 1.50f  });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 }); // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont))  { Border = 0 }); // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum);
            }
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                //-----------------------------------------------------
            if (_division.DivisionName.Contains("#") == false)
            {
                var tableDivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableDivision.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 }); // 2. field
                tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " - " + _division.Author + "  " + _division.GerName + " " + _division.EngName + " " + _division.FraName + " " + _division.PorName,  SmallFont))  { Border = 0 }); // 3. field
                tableDivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableDivision);
                }
                //-----------------------------------------------------
            if (_subdivision.SubdivisionName.Contains("#") == false)
            {
                var tableSubdivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubdivision.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubdivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableSubdivision.AddCell( new PdfPCell(new Phrase(CultRes.StringsRes.Subdivision, SmallFont)) { Border = 0 }); // 2. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(_subdivision.SubdivisionName + " - " + _subdivision.Author + "  " + _subdivision.GerName + " " + _subdivision.EngName + " " + _subdivision.FraName + " " + _subdivision.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubdivision);
                }
                //-----------------------------------------------------
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                //-----------------------------------------------------
                if (_phylum.PhylumName.Contains("#") == false)
                {
                var tablePhylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 }); // 2. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " - " + _phylum.Author + "  " + _phylum.GerName + " " + _phylum.EngName + " " + _phylum.FraName + " " + _phylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tablePhylum);
                }
                //-----------------------------------------------------
            if (_subphylum.SubphylumName.Contains("#") == false)
            {
                var tableSubphylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubphylum.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubphylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 }); // 2. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(_subphylum.SubphylumName + " - " + _subphylum.Author + "  " + _subphylum.GerName + " " + _subphylum.EngName + " " + _subphylum.FraName + " " + _subphylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubphylum);
                }
            }
            //-----------------------------------------------------
            if (_superclass.SuperclassName.Contains("#") == false)
            {
            var tableSuperclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperclass.SetWidths(new[] { 0.14f, 1.16f, 2.50f, 1.50f });

            tableSuperclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superclass, SmallFont)) { Border = 0 });  // 2. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(_superclass.SuperclassName + " - " + _superclass.Author + "  " + _superclass.GerName + " " + _superclass.EngName + " " + _superclass.FraName + " " + _superclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperclass);
            }
            //-----------------------------------------------------
            if (_class.ClassName.Contains("#") == false)
            {
            var tableClass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableClass.SetWidths(new[] { 0.17f, 1.13f, 2.50f, 1.50f });

            tableClass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableClass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Class, SmallFont)) { Border = 0 });  // 2. field
            tableClass.AddCell(new PdfPCell(new Phrase(_class.ClassName + " - " + _class.Author + "  " + _class.GerName + " " + _class.EngName + " " + _class.FraName + " " + _class.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableClass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableClass);
            }
            //-----------------------------------------------------
            if (_subclass.SubclassName.Contains("#") == false)
            {
            var tableSubclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubclass.SetWidths(new[] { 0.20f, 1.10f, 2.50f, 1.50f });

            tableSubclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subclass, SmallFont)) { Border = 0 });  // 2. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(_subclass.SubclassName + " - " + _subclass.Author + "  " + _subclass.GerName + " " + _subclass.EngName + " " + _subclass.FraName + " " + _subclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubclass);
             }
            //-----------------------------------------------------
            if (_infraclass.InfraclassName.Contains("#") == false)
            {
            var tableInfraclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraclass.SetWidths(new[] { 0.23f, 1.07f, 2.50f, 1.50f });

            tableInfraclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraclass, SmallFont)) { Border = 0 });  // 2. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(_infraclass.InfraclassName + " - " + _infraclass.Author + "  " + _infraclass.GerName + " " + _infraclass.EngName + " " + _infraclass.FraName + " " + _infraclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraclass);
            }
            //-----------------------------------------------------
            if (_legio.LegioName.Contains("#") == false)
            {
            var tableLegio = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableLegio.SetWidths(new[] { 0.26f, 1.04f, 2.50f, 1.50f });

            tableLegio.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableLegio.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Legio, SmallFont)) { Border = 0 });  // 2. field
            tableLegio.AddCell(new PdfPCell(new Phrase(_legio.LegioName + " - " + _legio.Author + "  " + _legio.GerName + " " + _legio.EngName + " " + _legio.FraName + " " + _legio.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableLegio.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableLegio);
            }
            //-----------------------------------------------------
            if (_ordo.OrdoName.Contains("#") == false)
            {
            var tableOrdo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableOrdo.SetWidths(new[] { 0.29f, 1.01f, 2.50f, 1.50f });

            tableOrdo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableOrdo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Ordo, SmallFont)) { Border = 0 });  // 2. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(_ordo.OrdoName + " - " + _ordo.Author + "  " + _ordo.GerName + " " + _ordo.EngName + " " + _ordo.FraName + " " + _ordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableOrdo);
            }
            //-----------------------------------------------------
            if (_subordo.SubordoName.Contains("#") == false)
            {
            var tableSubordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubordo.SetWidths(new[] { 0.32f, 0.98f, 2.50f, 1.50f });

            tableSubordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subordo, SmallFont)) { Border = 0 });  // 2. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(_subordo.SubordoName + " - " + _subordo.Author + "  " + _subordo.GerName + " " + _subordo.EngName + " " + _subordo.FraName + " " + _subordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubordo);
            }
            //-----------------------------------------------------
            if (_infraordo.InfraordoName.Contains("#") == false)
            {
            var tableInfraordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraordo.SetWidths(new[] { 0.35f, 0.95f, 2.50f, 1.50f });

            tableInfraordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraordo, SmallFont)) { Border = 0 });  // 2. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(_infraordo.InfraordoName + " - " + _infraordo.Author + "  " + _infraordo.GerName + " " + _infraordo.EngName + " " + _infraordo.FraName + " " + _infraordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraordo);
            }
            //-----------------------------------------------------
            if (_superfamily.SuperfamilyName.Contains("#") == false)
            {
            var tableSuperfamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperfamily.SetWidths(new[] { 0.38f, 0.92f, 2.50f, 1.50f });

            tableSuperfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(_superfamily.SuperfamilyName + " - " + _superfamily.Author + "  " + _superfamily.GerName + " " + _superfamily.EngName + " " + _superfamily.FraName + " " + _superfamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperfamily);
            }
            //-----------------------------------------------------
            if (_family.FamilyName.Contains("#") == false)
            {
            var tableFamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableFamily.SetWidths(new[] { 0.41f, 0.89f, 2.50f, 1.50f });

            tableFamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableFamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Family, SmallFont)) { Border = 0 });  // 2. field
            tableFamily.AddCell(new PdfPCell(new Phrase(_family.FamilyName + " - " + _family.Author + "  " + _family.GerName + " " + _family.EngName + " " + _family.FraName + " " + _family.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableFamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableFamily);
            }
            //-----------------------------------------------------
            if (_subfamily.SubfamilyName.Contains("#") == false)
            {
            var tableSubfamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubfamily.SetWidths(new[] { 0.44f, 0.86f, 2.50f, 1.50f });

            tableSubfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(_subfamily.SubfamilyName + " - " + _subfamily.Author + "  " + _subfamily.GerName + " " + _subfamily.EngName + " " + _subfamily.FraName + " " + _subfamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubfamily);
            }
            //-----------------------------------------------------
            if (_infrafamily.InfrafamilyName.Contains("#") == false)
            {
            var tableInfrafamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfrafamily.SetWidths(new[] { 0.47f, 0.83f, 2.50f, 1.50f });

            tableInfrafamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infrafamily, SmallFont)) { Border = 0 });  // 2. field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(_infrafamily.InfrafamilyName + " - " + _infrafamily.Author + "  " + _infrafamily.GerName + " " + _infrafamily.EngName + " " + _infrafamily.FraName + " " + _infrafamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfrafamily);
            }
            //-----------------------------------------------------
            if (_supertribus.SupertribusName.Contains("#") == false)
            {
            var tableSupertribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSupertribus.SetWidths(new[] { 0.50f, 0.80f, 2.50f, 1.50f });

            tableSupertribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Supertribus, SmallFont)) { Border = 0 });  // 2. field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(_supertribus.SupertribusName + " - " + _supertribus.Author + "  " + _supertribus.GerName + " " + _supertribus.EngName + " " + _supertribus.FraName + " " + _supertribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSupertribus);
            }
            //-----------------------------------------------------
            if (_tribus.TribusName.Contains("#") == false)
            {
            var tabletribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tabletribus.SetWidths(new[] { 0.53f, 0.77f, 2.50f, 1.50f });

            tabletribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tabletribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Tribus, SmallFont)) { Border = 0 });  // 2. field
            tabletribus.AddCell(new PdfPCell(new Phrase(_tribus.TribusName + " - " + _tribus.Author + "  " + _tribus.GerName + " " + _tribus.EngName + " " + _tribus.FraName + " " + _tribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tabletribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tabletribus);
            }
            //-----------------------------------------------------
            if (_subtribus.SubtribusName.Contains("#") == false)
            {
            var tablesubtribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tablesubtribus.SetWidths(new[] { 0.56f, 0.74f, 2.50f, 1.50f });

            tablesubtribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tablesubtribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subtribus, SmallFont)) { Border = 0 });  // 2. field
            tablesubtribus.AddCell(new PdfPCell(new Phrase(_subtribus.SubtribusName + " - " + _subtribus.Author + "  " + _subtribus.GerName + " " + _subtribus.EngName + " " + _subtribus.FraName + " " + _subtribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tablesubtribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tablesubtribus);
            }
            //-----------------------------------------------------
            if (_infratribus.InfratribusName.Contains("#") == false)
            {
            var tableInfratribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f
            };
            tableInfratribus.SetWidths(new[] { 0.59f, 0.71f, 2.50f, 1.50f });

            tableInfratribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfratribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infratribus, SmallFont)) { Border = 0 });  // 2. field
            tableInfratribus.AddCell(new PdfPCell(new Phrase(_infratribus.InfratribusName + " - " + _infratribus.Author + "  " + _infratribus.GerName + " " + _infratribus.EngName + " " + _infratribus.FraName + " " + _infratribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfratribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfratribus);
            }
            return doc;
        }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl66Genusses'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true, //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0, //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f
            };
            tableRegnum.SetWidths(new[] {0.05f, 1.25f, 2.50f, 1.50f  });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 }); // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont))  { Border = 0 }); // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum);
            }
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                //-----------------------------------------------------
            if (_division.DivisionName.Contains("#") == false)
            {
                var tableDivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableDivision.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 }); // 2. field
                tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " - " + _division.Author + "  " + _division.GerName + " " + _division.EngName + " " + _division.FraName + " " + _division.PorName,  SmallFont))  { Border = 0 }); // 3. field
                tableDivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableDivision);
                }
                //-----------------------------------------------------
            if (_subdivision.SubdivisionName.Contains("#") == false)
            {
                var tableSubdivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubdivision.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubdivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableSubdivision.AddCell( new PdfPCell(new Phrase(CultRes.StringsRes.Subdivision, SmallFont)) { Border = 0 }); // 2. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(_subdivision.SubdivisionName + " - " + _subdivision.Author + "  " + _subdivision.GerName + " " + _subdivision.EngName + " " + _subdivision.FraName + " " + _subdivision.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubdivision);
                }
                //-----------------------------------------------------
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                //-----------------------------------------------------
                if (_phylum.PhylumName.Contains("#") == false)
                {
                var tablePhylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 }); // 2. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " - " + _phylum.Author + "  " + _phylum.GerName + " " + _phylum.EngName + " " + _phylum.FraName + " " + _phylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tablePhylum);
                }
                //-----------------------------------------------------
            if (_subphylum.SubphylumName.Contains("#") == false)
            {
                var tableSubphylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubphylum.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubphylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 }); // 2. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(_subphylum.SubphylumName + " - " + _subphylum.Author + "  " + _subphylum.GerName + " " + _subphylum.EngName + " " + _subphylum.FraName + " " + _subphylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubphylum);
                }
            }
            //-----------------------------------------------------
            if (_superclass.SuperclassName.Contains("#") == false)
            {
            var tableSuperclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperclass.SetWidths(new[] { 0.14f, 1.16f, 2.50f, 1.50f });

            tableSuperclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superclass, SmallFont)) { Border = 0 });  // 2. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(_superclass.SuperclassName + " - " + _superclass.Author + "  " + _superclass.GerName + " " + _superclass.EngName + " " + _superclass.FraName + " " + _superclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperclass);
            }
            //-----------------------------------------------------
            if (_class.ClassName.Contains("#") == false)
            {
            var tableClass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableClass.SetWidths(new[] { 0.17f, 1.13f, 2.50f, 1.50f });

            tableClass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableClass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Class, SmallFont)) { Border = 0 });  // 2. field
            tableClass.AddCell(new PdfPCell(new Phrase(_class.ClassName + " - " + _class.Author + "  " + _class.GerName + " " + _class.EngName + " " + _class.FraName + " " + _class.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableClass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableClass);
            }
            //-----------------------------------------------------
            if (_subclass.SubclassName.Contains("#") == false)
            {
            var tableSubclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubclass.SetWidths(new[] { 0.20f, 1.10f, 2.50f, 1.50f });

            tableSubclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subclass, SmallFont)) { Border = 0 });  // 2. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(_subclass.SubclassName + " - " + _subclass.Author + "  " + _subclass.GerName + " " + _subclass.EngName + " " + _subclass.FraName + " " + _subclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubclass);
             }
            //-----------------------------------------------------
            if (_infraclass.InfraclassName.Contains("#") == false)
            {
            var tableInfraclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraclass.SetWidths(new[] { 0.23f, 1.07f, 2.50f, 1.50f });

            tableInfraclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraclass, SmallFont)) { Border = 0 });  // 2. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(_infraclass.InfraclassName + " - " + _infraclass.Author + "  " + _infraclass.GerName + " " + _infraclass.EngName + " " + _infraclass.FraName + " " + _infraclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraclass);
            }
            //-----------------------------------------------------
            if (_legio.LegioName.Contains("#") == false)
            {
            var tableLegio = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableLegio.SetWidths(new[] { 0.26f, 1.04f, 2.50f, 1.50f });

            tableLegio.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableLegio.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Legio, SmallFont)) { Border = 0 });  // 2. field
            tableLegio.AddCell(new PdfPCell(new Phrase(_legio.LegioName + " - " + _legio.Author + "  " + _legio.GerName + " " + _legio.EngName + " " + _legio.FraName + " " + _legio.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableLegio.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableLegio);
            }
            //-----------------------------------------------------
            if (_ordo.OrdoName.Contains("#") == false)
            {
            var tableOrdo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableOrdo.SetWidths(new[] { 0.29f, 1.01f, 2.50f, 1.50f });

            tableOrdo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableOrdo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Ordo, SmallFont)) { Border = 0 });  // 2. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(_ordo.OrdoName + " - " + _ordo.Author + "  " + _ordo.GerName + " " + _ordo.EngName + " " + _ordo.FraName + " " + _ordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableOrdo);
            }
            //-----------------------------------------------------
            if (_subordo.SubordoName.Contains("#") == false)
            {
            var tableSubordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubordo.SetWidths(new[] { 0.32f, 0.98f, 2.50f, 1.50f });

            tableSubordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subordo, SmallFont)) { Border = 0 });  // 2. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(_subordo.SubordoName + " - " + _subordo.Author + "  " + _subordo.GerName + " " + _subordo.EngName + " " + _subordo.FraName + " " + _subordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubordo);
            }
            //-----------------------------------------------------
            if (_infraordo.InfraordoName.Contains("#") == false)
            {
            var tableInfraordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraordo.SetWidths(new[] { 0.35f, 0.95f, 2.50f, 1.50f });

            tableInfraordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraordo, SmallFont)) { Border = 0 });  // 2. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(_infraordo.InfraordoName + " - " + _infraordo.Author + "  " + _infraordo.GerName + " " + _infraordo.EngName + " " + _infraordo.FraName + " " + _infraordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraordo);
            }
            //-----------------------------------------------------
            if (_superfamily.SuperfamilyName.Contains("#") == false)
            {
            var tableSuperfamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperfamily.SetWidths(new[] { 0.38f, 0.92f, 2.50f, 1.50f });

            tableSuperfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(_superfamily.SuperfamilyName + " - " + _superfamily.Author + "  " + _superfamily.GerName + " " + _superfamily.EngName + " " + _superfamily.FraName + " " + _superfamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperfamily);
            }
            //-----------------------------------------------------
            if (_family.FamilyName.Contains("#") == false)
            {
            var tableFamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableFamily.SetWidths(new[] { 0.41f, 0.89f, 2.50f, 1.50f });

            tableFamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableFamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Family, SmallFont)) { Border = 0 });  // 2. field
            tableFamily.AddCell(new PdfPCell(new Phrase(_family.FamilyName + " - " + _family.Author + "  " + _family.GerName + " " + _family.EngName + " " + _family.FraName + " " + _family.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableFamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableFamily);
            }
            //-----------------------------------------------------
            if (_subfamily.SubfamilyName.Contains("#") == false)
            {
            var tableSubfamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubfamily.SetWidths(new[] { 0.44f, 0.86f, 2.50f, 1.50f });

            tableSubfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(_subfamily.SubfamilyName + " - " + _subfamily.Author + "  " + _subfamily.GerName + " " + _subfamily.EngName + " " + _subfamily.FraName + " " + _subfamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubfamily);
            }
            //-----------------------------------------------------
            if (_infrafamily.InfrafamilyName.Contains("#") == false)
            {
            var tableInfrafamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfrafamily.SetWidths(new[] { 0.47f, 0.83f, 2.50f, 1.50f });

            tableInfrafamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infrafamily, SmallFont)) { Border = 0 });  // 2. field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(_infrafamily.InfrafamilyName + " - " + _infrafamily.Author + "  " + _infrafamily.GerName + " " + _infrafamily.EngName + " " + _infrafamily.FraName + " " + _infrafamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfrafamily);
            }
            //-----------------------------------------------------
            if (_supertribus.SupertribusName.Contains("#") == false)
            {
            var tableSupertribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSupertribus.SetWidths(new[] { 0.50f, 0.80f, 2.50f, 1.50f });

            tableSupertribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Supertribus, SmallFont)) { Border = 0 });  // 2. field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(_supertribus.SupertribusName + " - " + _supertribus.Author + "  " + _supertribus.GerName + " " + _supertribus.EngName + " " + _supertribus.FraName + " " + _supertribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSupertribus);
            }
            //-----------------------------------------------------
            if (_tribus.TribusName.Contains("#") == false)
            {
            var tabletribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tabletribus.SetWidths(new[] { 0.53f, 0.77f, 2.50f, 1.50f });

            tabletribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tabletribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Tribus, SmallFont)) { Border = 0 });  // 2. field
            tabletribus.AddCell(new PdfPCell(new Phrase(_tribus.TribusName + " - " + _tribus.Author + "  " + _tribus.GerName + " " + _tribus.EngName + " " + _tribus.FraName + " " + _tribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tabletribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tabletribus);
            }
            //-----------------------------------------------------
            if (_subtribus.SubtribusName.Contains("#") == false)
            {
            var tablesubtribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tablesubtribus.SetWidths(new[] { 0.56f, 0.74f, 2.50f, 1.50f });

            tablesubtribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tablesubtribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subtribus, SmallFont)) { Border = 0 });  // 2. field
            tablesubtribus.AddCell(new PdfPCell(new Phrase(_subtribus.SubtribusName + " - " + _subtribus.Author + "  " + _subtribus.GerName + " " + _subtribus.EngName + " " + _subtribus.FraName + " " + _subtribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tablesubtribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tablesubtribus);
            }
            //-----------------------------------------------------
            if (_infratribus.InfratribusName.Contains("#") == false)
            {
            var tableInfratribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfratribus.SetWidths(new[] { 0.59f, 0.71f, 2.50f, 1.50f });

            tableInfratribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfratribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infratribus, SmallFont)) { Border = 0 });  // 2. field
            tableInfratribus.AddCell(new PdfPCell(new Phrase(_infratribus.InfratribusName + " - " + _infratribus.Author + "  " + _infratribus.GerName + " " + _infratribus.EngName + " " + _infratribus.FraName + " " + _infratribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfratribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfratribus);
            }
            //-----------------------------------------------------
            if (_genus.GenusName.Contains("#") == false)
            {
            var tableGenus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f
            };
            tableGenus.SetWidths(new[] { 0.62f, 0.68f, 2.50f, 1.50f });

            tableGenus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableGenus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Genus, SmallFont)) { Border = 0 });  // 2. field
            tableGenus.AddCell(new PdfPCell(new Phrase(_genus.GenusName + " - " + _genus.Author + "  " + _genus.GerName + " " + _genus.EngName + " " + _genus.FraName + " " + _genus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableGenus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableGenus);
            }
            return doc;
        }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true, //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0, //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f
            };
            tableRegnum.SetWidths(new[] {0.05f, 1.25f, 2.50f, 1.50f  });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 }); // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont))  { Border = 0 }); // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum);
            }
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                //-----------------------------------------------------
            if (_division.DivisionName.Contains("#") == false)
            {
                var tableDivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableDivision.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 }); // 2. field
                tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " - " + _division.Author + "  " + _division.GerName + " " + _division.EngName + " " + _division.FraName + " " + _division.PorName,  SmallFont))  { Border = 0 }); // 3. field
                tableDivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableDivision);
                }
                //-----------------------------------------------------
            if (_subdivision.SubdivisionName.Contains("#") == false)
            {
                var tableSubdivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubdivision.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubdivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableSubdivision.AddCell( new PdfPCell(new Phrase(CultRes.StringsRes.Subdivision, SmallFont)) { Border = 0 }); // 2. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(_subdivision.SubdivisionName + " - " + _subdivision.Author + "  " + _subdivision.GerName + " " + _subdivision.EngName + " " + _subdivision.FraName + " " + _subdivision.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubdivision);
                }
                //-----------------------------------------------------
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                //-----------------------------------------------------
                if (_phylum.PhylumName.Contains("#") == false)
                {
                var tablePhylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 }); // 2. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " - " + _phylum.Author + "  " + _phylum.GerName + " " + _phylum.EngName + " " + _phylum.FraName + " " + _phylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tablePhylum);
                }
                //-----------------------------------------------------
            if (_subphylum.SubphylumName.Contains("#") == false)
            {
                var tableSubphylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubphylum.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubphylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 }); // 2. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(_subphylum.SubphylumName + " - " + _subphylum.Author + "  " + _subphylum.GerName + " " + _subphylum.EngName + " " + _subphylum.FraName + " " + _subphylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubphylum);
                }
            }
            //-----------------------------------------------------
            if (_superclass.SuperclassName.Contains("#") == false)
            {
            var tableSuperclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperclass.SetWidths(new[] { 0.14f, 1.16f, 2.50f, 1.50f });

            tableSuperclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superclass, SmallFont)) { Border = 0 });  // 2. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(_superclass.SuperclassName + " - " + _superclass.Author + "  " + _superclass.GerName + " " + _superclass.EngName + " " + _superclass.FraName + " " + _superclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperclass);
            }
            //-----------------------------------------------------
            if (_class.ClassName.Contains("#") == false)
            {
            var tableClass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableClass.SetWidths(new[] { 0.17f, 1.13f, 2.50f, 1.50f });

            tableClass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableClass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Class, SmallFont)) { Border = 0 });  // 2. field
            tableClass.AddCell(new PdfPCell(new Phrase(_class.ClassName + " - " + _class.Author + "  " + _class.GerName + " " + _class.EngName + " " + _class.FraName + " " + _class.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableClass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableClass);
            }
            //-----------------------------------------------------
            if (_subclass.SubclassName.Contains("#") == false)
            {
            var tableSubclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubclass.SetWidths(new[] { 0.20f, 1.10f, 2.50f, 1.50f });

            tableSubclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subclass, SmallFont)) { Border = 0 });  // 2. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(_subclass.SubclassName + " - " + _subclass.Author + "  " + _subclass.GerName + " " + _subclass.EngName + " " + _subclass.FraName + " " + _subclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubclass);
             }
            //-----------------------------------------------------
            if (_infraclass.InfraclassName.Contains("#") == false)
            {
            var tableInfraclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraclass.SetWidths(new[] { 0.23f, 1.07f, 2.50f, 1.50f });

            tableInfraclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraclass, SmallFont)) { Border = 0 });  // 2. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(_infraclass.InfraclassName + " - " + _infraclass.Author + "  " + _infraclass.GerName + " " + _infraclass.EngName + " " + _infraclass.FraName + " " + _infraclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraclass);
            }
            //-----------------------------------------------------
            if (_legio.LegioName.Contains("#") == false)
            {
            var tableLegio = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableLegio.SetWidths(new[] { 0.26f, 1.04f, 2.50f, 1.50f });

            tableLegio.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableLegio.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Legio, SmallFont)) { Border = 0 });  // 2. field
            tableLegio.AddCell(new PdfPCell(new Phrase(_legio.LegioName + " - " + _legio.Author + "  " + _legio.GerName + " " + _legio.EngName + " " + _legio.FraName + " " + _legio.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableLegio.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableLegio);
            }
            //-----------------------------------------------------
            if (_ordo.OrdoName.Contains("#") == false)
            {
            var tableOrdo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableOrdo.SetWidths(new[] { 0.29f, 1.01f, 2.50f, 1.50f });

            tableOrdo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableOrdo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Ordo, SmallFont)) { Border = 0 });  // 2. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(_ordo.OrdoName + " - " + _ordo.Author + "  " + _ordo.GerName + " " + _ordo.EngName + " " + _ordo.FraName + " " + _ordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableOrdo);
            }
            //-----------------------------------------------------
            if (_subordo.SubordoName.Contains("#") == false)
            {
            var tableSubordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubordo.SetWidths(new[] { 0.32f, 0.98f, 2.50f, 1.50f });

            tableSubordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subordo, SmallFont)) { Border = 0 });  // 2. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(_subordo.SubordoName + " - " + _subordo.Author + "  " + _subordo.GerName + " " + _subordo.EngName + " " + _subordo.FraName + " " + _subordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubordo);
            }
            //-----------------------------------------------------
            if (_infraordo.InfraordoName.Contains("#") == false)
            {
            var tableInfraordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraordo.SetWidths(new[] { 0.35f, 0.95f, 2.50f, 1.50f });

            tableInfraordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraordo, SmallFont)) { Border = 0 });  // 2. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(_infraordo.InfraordoName + " - " + _infraordo.Author + "  " + _infraordo.GerName + " " + _infraordo.EngName + " " + _infraordo.FraName + " " + _infraordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraordo);
            }
            //-----------------------------------------------------
            if (_superfamily.SuperfamilyName.Contains("#") == false)
            {
            var tableSuperfamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperfamily.SetWidths(new[] { 0.38f, 0.92f, 2.50f, 1.50f });

            tableSuperfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(_superfamily.SuperfamilyName + " - " + _superfamily.Author + "  " + _superfamily.GerName + " " + _superfamily.EngName + " " + _superfamily.FraName + " " + _superfamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperfamily);
            }
            //-----------------------------------------------------
            if (_family.FamilyName.Contains("#") == false)
            {
            var tableFamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableFamily.SetWidths(new[] { 0.41f, 0.89f, 2.50f, 1.50f });

            tableFamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableFamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Family, SmallFont)) { Border = 0 });  // 2. field
            tableFamily.AddCell(new PdfPCell(new Phrase(_family.FamilyName + " - " + _family.Author + "  " + _family.GerName + " " + _family.EngName + " " + _family.FraName + " " + _family.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableFamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableFamily);
            }
            //-----------------------------------------------------
            if (_subfamily.SubfamilyName.Contains("#") == false)
            {
            var tableSubfamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubfamily.SetWidths(new[] { 0.44f, 0.86f, 2.50f, 1.50f });

            tableSubfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(_subfamily.SubfamilyName + " - " + _subfamily.Author + "  " + _subfamily.GerName + " " + _subfamily.EngName + " " + _subfamily.FraName + " " + _subfamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubfamily);
            }
            //-----------------------------------------------------
            if (_infrafamily.InfrafamilyName.Contains("#") == false)
            {
            var tableInfrafamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfrafamily.SetWidths(new[] { 0.47f, 0.83f, 2.50f, 1.50f });

            tableInfrafamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infrafamily, SmallFont)) { Border = 0 });  // 2. field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(_infrafamily.InfrafamilyName + " - " + _infrafamily.Author + "  " + _infrafamily.GerName + " " + _infrafamily.EngName + " " + _infrafamily.FraName + " " + _infrafamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfrafamily);
            }
            //-----------------------------------------------------
            if (_supertribus.SupertribusName.Contains("#") == false)
            {
            var tableSupertribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSupertribus.SetWidths(new[] { 0.50f, 0.80f, 2.50f, 1.50f });

            tableSupertribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Supertribus, SmallFont)) { Border = 0 });  // 2. field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(_supertribus.SupertribusName + " - " + _supertribus.Author + "  " + _supertribus.GerName + " " + _supertribus.EngName + " " + _supertribus.FraName + " " + _supertribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSupertribus);
            }
            //-----------------------------------------------------
            if (_tribus.TribusName.Contains("#") == false)
            {
            var tabletribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tabletribus.SetWidths(new[] { 0.53f, 0.77f, 2.50f, 1.50f });

            tabletribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tabletribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Tribus, SmallFont)) { Border = 0 });  // 2. field
            tabletribus.AddCell(new PdfPCell(new Phrase(_tribus.TribusName + " - " + _tribus.Author + "  " + _tribus.GerName + " " + _tribus.EngName + " " + _tribus.FraName + " " + _tribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tabletribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tabletribus);
            }
            //-----------------------------------------------------
            if (_subtribus.SubtribusName.Contains("#") == false)
            {
            var tablesubtribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tablesubtribus.SetWidths(new[] { 0.56f, 0.74f, 2.50f, 1.50f });

            tablesubtribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tablesubtribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subtribus, SmallFont)) { Border = 0 });  // 2. field
            tablesubtribus.AddCell(new PdfPCell(new Phrase(_subtribus.SubtribusName + " - " + _subtribus.Author + "  " + _subtribus.GerName + " " + _subtribus.EngName + " " + _subtribus.FraName + " " + _subtribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tablesubtribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tablesubtribus);
            }
            //-----------------------------------------------------
            if (_infratribus.InfratribusName.Contains("#") == false)
            {
            var tableInfratribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfratribus.SetWidths(new[] { 0.59f, 0.71f, 2.50f, 1.50f });

            tableInfratribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfratribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infratribus, SmallFont)) { Border = 0 });  // 2. field
            tableInfratribus.AddCell(new PdfPCell(new Phrase(_infratribus.InfratribusName + " - " + _infratribus.Author + "  " + _infratribus.GerName + " " + _infratribus.EngName + " " + _infratribus.FraName + " " + _infratribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfratribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfratribus);
            }
            //-----------------------------------------------------
            if (_genus.GenusName.Contains("#") == false)
            {
            var tableGenus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableGenus.SetWidths(new[] { 0.62f, 0.68f, 2.50f, 1.50f });

            tableGenus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableGenus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Genus, SmallFont)) { Border = 0 });  // 2. field
            tableGenus.AddCell(new PdfPCell(new Phrase(_genus.GenusName + " - " + _genus.Author + "  " + _genus.GerName + " " + _genus.EngName + " " + _genus.FraName + " " + _genus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableGenus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableGenus);
            }
            //-----------------------------------------------------
            if (_fispecies.FiSpeciesName.Contains("#") == false)
            {
            var tableFiSpecies = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f
            };
            tableFiSpecies.SetWidths(new[] { 0.65f, 0.65f, 2.50f, 1.50f });

            tableFiSpecies.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableFiSpecies.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.FiSpecies, SmallFont)) { Border = 0 });  // 2. field
            tableFiSpecies.AddCell(new PdfPCell(new Phrase(_fispecies.FiSpeciesName + "  " + _fispecies.Subspecies + "  " +_fispecies.Divers + "  -  " +_fispecies.Author, SmallFont)) { Border = 0 });  // 3. field
            tableFiSpecies.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableFiSpecies);
            }
            return doc;
        }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">        <![CDATA[        
        private static Document AddHierarchyList(Document doc)
        {
            if (_regnum.RegnumName.Contains("#") == false)
            {
            var tableRegnum = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true, //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0, //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 0f
            };
            tableRegnum.SetWidths(new[] {0.05f, 1.25f, 2.50f, 1.50f  });

            tableRegnum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
            tableRegnum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Regnum, SmallFont)) { Border = 0 }); // 2. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(_regnum.RegnumName + "  " + _regnum.Subregnum + " - " + _regnum.Author + "  " + _regnum.GerName + " " + _regnum.EngName + " " + _regnum.FraName + " " + _regnum.PorName, SmallFont))  { Border = 0 }); // 3. field
            tableRegnum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableRegnum);
            }
            if (_superclass.SubphylumID == 10)  //Basis #Subphylum#
            {
                //-----------------------------------------------------
            if (_division.DivisionName.Contains("#") == false)
            {
                var tableDivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableDivision.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tableDivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableDivision.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Division, SmallFont)) { Border = 0 }); // 2. field
                tableDivision.AddCell(new PdfPCell(new Phrase(_division.DivisionName + " - " + _division.Author + "  " + _division.GerName + " " + _division.EngName + " " + _division.FraName + " " + _division.PorName,  SmallFont))  { Border = 0 }); // 3. field
                tableDivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableDivision);
                }
                //-----------------------------------------------------
            if (_subdivision.SubdivisionName.Contains("#") == false)
            {
                var tableSubdivision = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubdivision.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubdivision.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1.  field
                tableSubdivision.AddCell( new PdfPCell(new Phrase(CultRes.StringsRes.Subdivision, SmallFont)) { Border = 0 }); // 2. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(_subdivision.SubdivisionName + " - " + _subdivision.Author + "  " + _subdivision.GerName + " " + _subdivision.EngName + " " + _subdivision.FraName + " " + _subdivision.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubdivision.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubdivision);
                }
                //-----------------------------------------------------
            }
            if (_superclass.SubdivisionID == 1)  //Basis #Subdivision#
            {
                //-----------------------------------------------------
                if (_phylum.PhylumName.Contains("#") == false)
                {
                var tablePhylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tablePhylum.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });

                tablePhylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tablePhylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Phylum, SmallFont)) { Border = 0 }); // 2. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(_phylum.PhylumName + " - " + _phylum.Author + "  " + _phylum.GerName + " " + _phylum.EngName + " " + _phylum.FraName + " " + _phylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tablePhylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tablePhylum);
                }
                //-----------------------------------------------------
            if (_subphylum.SubphylumName.Contains("#") == false)
            {
                var tableSubphylum = new PdfPTable(4)
                {
                    TotalWidth = 792f, //actual width of table in points
                    LockedWidth = true, //fix the absolute width of the table
                    WidthPercentage = 100,
                    HorizontalAlignment = 0, //0=Left aLign, 1=Center
                    SpacingBefore = 0f,
                    SpacingAfter = 0f
                };
                tableSubphylum.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });

                tableSubphylum.AddCell(new PdfPCell { Colspan = 1, Border = 0 }); // 1. . field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subphylum, SmallFont)) { Border = 0 }); // 2. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(_subphylum.SubphylumName + " - " + _subphylum.Author + "  " + _subphylum.GerName + " " + _subphylum.EngName + " " + _subphylum.FraName + " " + _subphylum.PorName, SmallFont)) { Border = 0 });  // 3. field
                tableSubphylum.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

                doc.Add(tableSubphylum);
                }
            }
            //-----------------------------------------------------
            if (_superclass.SuperclassName.Contains("#") == false)
            {
            var tableSuperclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperclass.SetWidths(new[] { 0.14f, 1.16f, 2.50f, 1.50f });

            tableSuperclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superclass, SmallFont)) { Border = 0 });  // 2. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(_superclass.SuperclassName + " - " + _superclass.Author + "  " + _superclass.GerName + " " + _superclass.EngName + " " + _superclass.FraName + " " + _superclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperclass);
            }
            //-----------------------------------------------------
            if (_class.ClassName.Contains("#") == false)
            {
            var tableClass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableClass.SetWidths(new[] { 0.17f, 1.13f, 2.50f, 1.50f });

            tableClass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableClass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Class, SmallFont)) { Border = 0 });  // 2. field
            tableClass.AddCell(new PdfPCell(new Phrase(_class.ClassName + " - " + _class.Author + "  " + _class.GerName + " " + _class.EngName + " " + _class.FraName + " " + _class.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableClass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableClass);
            }
            //-----------------------------------------------------
            if (_subclass.SubclassName.Contains("#") == false)
            {
            var tableSubclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubclass.SetWidths(new[] { 0.20f, 1.10f, 2.50f, 1.50f });

            tableSubclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subclass, SmallFont)) { Border = 0 });  // 2. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(_subclass.SubclassName + " - " + _subclass.Author + "  " + _subclass.GerName + " " + _subclass.EngName + " " + _subclass.FraName + " " + _subclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubclass);
             }
            //-----------------------------------------------------
            if (_infraclass.InfraclassName.Contains("#") == false)
            {
            var tableInfraclass = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraclass.SetWidths(new[] { 0.23f, 1.07f, 2.50f, 1.50f });

            tableInfraclass.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraclass, SmallFont)) { Border = 0 });  // 2. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(_infraclass.InfraclassName + " - " + _infraclass.Author + "  " + _infraclass.GerName + " " + _infraclass.EngName + " " + _infraclass.FraName + " " + _infraclass.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraclass.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraclass);
            }
            //-----------------------------------------------------
            if (_legio.LegioName.Contains("#") == false)
            {
            var tableLegio = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableLegio.SetWidths(new[] { 0.26f, 1.04f, 2.50f, 1.50f });

            tableLegio.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableLegio.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Legio, SmallFont)) { Border = 0 });  // 2. field
            tableLegio.AddCell(new PdfPCell(new Phrase(_legio.LegioName + " - " + _legio.Author + "  " + _legio.GerName + " " + _legio.EngName + " " + _legio.FraName + " " + _legio.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableLegio.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableLegio);
            }
            //-----------------------------------------------------
            if (_ordo.OrdoName.Contains("#") == false)
            {
            var tableOrdo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableOrdo.SetWidths(new[] { 0.29f, 1.01f, 2.50f, 1.50f });

            tableOrdo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableOrdo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Ordo, SmallFont)) { Border = 0 });  // 2. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(_ordo.OrdoName + " - " + _ordo.Author + "  " + _ordo.GerName + " " + _ordo.EngName + " " + _ordo.FraName + " " + _ordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableOrdo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableOrdo);
            }
            //-----------------------------------------------------
            if (_subordo.SubordoName.Contains("#") == false)
            {
            var tableSubordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubordo.SetWidths(new[] { 0.32f, 0.98f, 2.50f, 1.50f });

            tableSubordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subordo, SmallFont)) { Border = 0 });  // 2. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(_subordo.SubordoName + " - " + _subordo.Author + "  " + _subordo.GerName + " " + _subordo.EngName + " " + _subordo.FraName + " " + _subordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubordo);
            }
            //-----------------------------------------------------
            if (_infraordo.InfraordoName.Contains("#") == false)
            {
            var tableInfraordo = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfraordo.SetWidths(new[] { 0.35f, 0.95f, 2.50f, 1.50f });

            tableInfraordo.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infraordo, SmallFont)) { Border = 0 });  // 2. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(_infraordo.InfraordoName + " - " + _infraordo.Author + "  " + _infraordo.GerName + " " + _infraordo.EngName + " " + _infraordo.FraName + " " + _infraordo.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfraordo.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfraordo);
            }
            //-----------------------------------------------------
            if (_superfamily.SuperfamilyName.Contains("#") == false)
            {
            var tableSuperfamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSuperfamily.SetWidths(new[] { 0.38f, 0.92f, 2.50f, 1.50f });

            tableSuperfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Superfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(_superfamily.SuperfamilyName + " - " + _superfamily.Author + "  " + _superfamily.GerName + " " + _superfamily.EngName + " " + _superfamily.FraName + " " + _superfamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSuperfamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSuperfamily);
            }
            //-----------------------------------------------------
            if (_family.FamilyName.Contains("#") == false)
            {
            var tableFamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableFamily.SetWidths(new[] { 0.41f, 0.89f, 2.50f, 1.50f });

            tableFamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableFamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Family, SmallFont)) { Border = 0 });  // 2. field
            tableFamily.AddCell(new PdfPCell(new Phrase(_family.FamilyName + " - " + _family.Author + "  " + _family.GerName + " " + _family.EngName + " " + _family.FraName + " " + _family.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableFamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableFamily);
            }
            //-----------------------------------------------------
            if (_subfamily.SubfamilyName.Contains("#") == false)
            {
            var tableSubfamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSubfamily.SetWidths(new[] { 0.44f, 0.86f, 2.50f, 1.50f });

            tableSubfamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subfamily, SmallFont)) { Border = 0 });  // 2. field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(_subfamily.SubfamilyName + " - " + _subfamily.Author + "  " + _subfamily.GerName + " " + _subfamily.EngName + " " + _subfamily.FraName + " " + _subfamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSubfamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSubfamily);
            }
            //-----------------------------------------------------
            if (_infrafamily.InfrafamilyName.Contains("#") == false)
            {
            var tableInfrafamily = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfrafamily.SetWidths(new[] { 0.47f, 0.83f, 2.50f, 1.50f });

            tableInfrafamily.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infrafamily, SmallFont)) { Border = 0 });  // 2. field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(_infrafamily.InfrafamilyName + " - " + _infrafamily.Author + "  " + _infrafamily.GerName + " " + _infrafamily.EngName + " " + _infrafamily.FraName + " " + _infrafamily.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfrafamily.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfrafamily);
            }
            //-----------------------------------------------------
            if (_supertribus.SupertribusName.Contains("#") == false)
            {
            var tableSupertribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableSupertribus.SetWidths(new[] { 0.50f, 0.80f, 2.50f, 1.50f });

            tableSupertribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Supertribus, SmallFont)) { Border = 0 });  // 2. field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(_supertribus.SupertribusName + " - " + _supertribus.Author + "  " + _supertribus.GerName + " " + _supertribus.EngName + " " + _supertribus.FraName + " " + _supertribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableSupertribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableSupertribus);
            }
            //-----------------------------------------------------
            if (_tribus.TribusName.Contains("#") == false)
            {
            var tabletribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tabletribus.SetWidths(new[] { 0.53f, 0.77f, 2.50f, 1.50f });

            tabletribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tabletribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Tribus, SmallFont)) { Border = 0 });  // 2. field
            tabletribus.AddCell(new PdfPCell(new Phrase(_tribus.TribusName + " - " + _tribus.Author + "  " + _tribus.GerName + " " + _tribus.EngName + " " + _tribus.FraName + " " + _tribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tabletribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tabletribus);
            }
            //-----------------------------------------------------
            if (_subtribus.SubtribusName.Contains("#") == false)
            {
            var tablesubtribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tablesubtribus.SetWidths(new[] { 0.56f, 0.74f, 2.50f, 1.50f });

            tablesubtribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tablesubtribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Subtribus, SmallFont)) { Border = 0 });  // 2. field
            tablesubtribus.AddCell(new PdfPCell(new Phrase(_subtribus.SubtribusName + " - " + _subtribus.Author + "  " + _subtribus.GerName + " " + _subtribus.EngName + " " + _subtribus.FraName + " " + _subtribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tablesubtribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tablesubtribus);
            }
            //-----------------------------------------------------
            if (_infratribus.InfratribusName.Contains("#") == false)
            {
            var tableInfratribus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableInfratribus.SetWidths(new[] { 0.59f, 0.71f, 2.50f, 1.50f });

            tableInfratribus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableInfratribus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Infratribus, SmallFont)) { Border = 0 });  // 2. field
            tableInfratribus.AddCell(new PdfPCell(new Phrase(_infratribus.InfratribusName + " - " + _infratribus.Author + "  " + _infratribus.GerName + " " + _infratribus.EngName + " " + _infratribus.FraName + " " + _infratribus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableInfratribus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableInfratribus);
            }
            //-----------------------------------------------------
            if (_genus.GenusName.Contains("#") == false)
            {
            var tableGenus = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 0f
            };
            tableGenus.SetWidths(new[] { 0.62f, 0.68f, 2.50f, 1.50f });

            tableGenus.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tableGenus.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.Genus, SmallFont)) { Border = 0 });  // 2. field
            tableGenus.AddCell(new PdfPCell(new Phrase(_genus.GenusName + " - " + _genus.Author + "  " + _genus.GerName + " " + _genus.EngName + " " + _genus.FraName + " " + _genus.PorName, SmallFont)) { Border = 0 });  // 3. field
            tableGenus.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tableGenus);
            }
            //-----------------------------------------------------
            if (_plspecies.PlSpeciesName.Contains("#") == false)
            {
            var tablePlSpecies = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,   //fix the absolute width of the table
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f
            };
            tablePlSpecies.SetWidths(new[] { 0.65f, 0.65f, 2.50f, 1.50f });

            tablePlSpecies.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            tablePlSpecies.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.PlSpecies, SmallFont)) { Border = 0 });  // 2. field
            tablePlSpecies.AddCell(new PdfPCell(new Phrase(_plspecies.PlSpeciesName + "  " + _plspecies.Subspecies + "  " +_plspecies.Divers + "  -  " +_plspecies.Author, SmallFont)) { Border = 0 });  // 3. field
            tablePlSpecies.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(tablePlSpecies);
            }
            return doc;
        }      ]]> 
</xsl:when>
<xsl:otherwise>    
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddTbl..  Top 1  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl66Genusses'">       <![CDATA[      
        private static Document AddTbl69FiSpeciessesChildrenList(Document doc, ObservableCollection<Tbl69FiSpecies> tbl69FiSpeciessesList)
        {
            var table = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f   //fix the absolute width of the table
            };

            table.SetWidths(new[] { 0.65f, 0.65f, 2.50f, 1.50f });

            table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 2, Border = 0 });  // 1. -2. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDirectChild, SmallBoldFont)) { Border = 0 }); //   3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            foreach (var t in tbl69FiSpeciessesList)
            {
                var t1 = t.FiSpeciesName;
                var t2 = t.Subspecies;
                var t3 = t.Divers;
                var t4 = t.Author;

                table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.   field
                table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.FiSpecies, SmallFont)) { Border = 0 });  // 2. field
                table.AddCell(new PdfPCell(new Phrase(t1 + " - " + t2 + " " + t3 + " " + t4, SmallFont)) { Border = 0 });   // 3. field
                table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field
            }
            doc.Add(table);

            return doc;
        }

        private static Document AddTbl72PlSpeciessesChildrenList(Document doc, ObservableCollection<Tbl72PlSpecies> tbl72PlSpeciessesList)
        {
            var table = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f   //fix the absolute width of the table
            };
            table.SetWidths(new[] { 0.65f, 0.65f, 2.50f, 1.50f });

            table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 2, Border = 0 });  // 1. -2. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDirectChild, SmallBoldFont)) { Border = 0 }); //   3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            foreach (var t in tbl72PlSpeciessesList)
            {
                var t1 = t.PlSpeciesName;
                var t2 = t.Subspecies;
                var t3 = t.Divers;
                var t4 = t.Author;

                table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.   field
                table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.PlSpecies, SmallFont)) { Border = 0 });  // 2. field
                table.AddCell(new PdfPCell(new Phrase(t1 + " - " + t2 + " " + t3 + " " + t4, SmallFont)) { Border = 0 });   // 3. field
                table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field 
            }
            doc.Add(table);

            return doc;
        }   ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">       <![CDATA[      
        private static Document AddTbl69FiSpeciessesTechnicList(Document doc)
        {
            var table = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 10f   //fix the absolute width of the table
            };
            table.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportRegionTop, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.RegionTop), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportRegionMiddle, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.RegionMiddle), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportRegionBottom, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.RegionBottom), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDifficult1, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Difficult1), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDifficult2, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Difficult2), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDifficult3, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Difficult3), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDifficult4, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Difficult4), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportBasinLength, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.BasinLength), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportFishLength, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.FishLength), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportLNumberLOrigin, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.LNumber + " / " + _fispecies.LOrigin, SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportLdaNumberLdaOrigin, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.LDANumber + " / " + _fispecies.LDAOrigin, SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportPh12, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Ph1) + " - " + Convert.ToString(_fispecies.Ph2), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTemp12, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Temp1) + " - " + Convert.ToString(_fispecies.Temp2), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportHardness12, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Hardness1) + " - " + Convert.ToString(_fispecies.Hardness2), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportCarboHardness12, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.CarboHardness1) + " - " + Convert.ToString(_fispecies.CarboHardness2), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field


            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemoTechnic, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.MemoTech, SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(table);

            return doc;
        }

        private static Document AddTbl69FiSpeciessesFoodList(Document doc)
        {
            var table = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 10f   //fix the absolute width of the table
            };
            table.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportKarnivore, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Karnivore), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportLimnivore, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Limnivore), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportHerbivore, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Herbivore), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportOmnivore, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_fispecies.Omnivore), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemoFood, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.MemoFoods, SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(table);

            return doc;
        }

        private static Document AddTbl69FiSpeciessesGuList(Document doc)
        {
            var table = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 10f   //fix the absolute width of the table
            };
            table.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemoDimorphism, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.MemoDomorphism, SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(table);

            return doc;
        }
        private static Document AddTbl69FiSpeciessesSozialList(Document doc)
        {
            var table = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 10f   //fix the absolute width of the table
            };
            table.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportSozial, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.MemoSozial, SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(table);

            return doc;
        }
        private static Document AddTbl69FiSpeciessesHusbandryList(Document doc)
        {
            var table = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 10f   //fix the absolute width of the table
            };
            table.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportHusbandry, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_fispecies.MemoHusbandry, SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(table);

            return doc;
        }    ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">       <![CDATA[      
        private static Document AddTbl72PlSpeciessesTechnicList(Document doc)
        {
            var table = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 10f   //fix the absolute width of the table
            };
            table.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportBasinHeight, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_plspecies.BasinHeight), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportPlantLength, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_plspecies.PlantLength), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportPh12, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_plspecies.Ph1) + " - " + Convert.ToString(_plspecies.Ph2), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportTemp12, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_plspecies.Temp1) + " - " + Convert.ToString(_plspecies.Temp2), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportHardness12, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_plspecies.Hardness1) + " - " + Convert.ToString(_plspecies.Hardness2), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportCarboHardness12, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(Convert.ToString(_plspecies.CarboHardness1) + " - " + Convert.ToString(_plspecies.CarboHardness2), SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field


            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemoTechnic, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_plspecies.MemoTech, SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(table);

            return doc;
        }
        private static Document AddTbl72PlSpeciessesReproductionList(Document doc)
        {
            var table = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 10f   //fix the absolute width of the table
            };
            table.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemoReproduction, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_plspecies.MemoReproduction, SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(table);

            return doc;
        }
        private static Document AddTbl72PlSpeciessesGlobalList(Document doc)
        {
            var table = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 10f   //fix the absolute width of the table
            };
            table.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportGlobal, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_plspecies.MemoGlobal, SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(table);

            return doc;
        }
        private static Document AddTbl72PlSpeciessesCultureList(Document doc)
        {
            var table = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 10f,
                SpacingAfter = 10f   //fix the absolute width of the table
            };
            table.SetWidths(new[] { 0.05f, 1.25f, 2.50f, 1.50f });

            table.AddCell(new PdfPCell { Colspan = 1, Border = 0 });  // 1.  field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportMemoCulture, SmallFont)) { Border = 0 });  // 2. field
            table.AddCell(new PdfPCell(new Phrase(_plspecies.MemoCulture, SmallFont)) { Border = 0 });  // 3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            doc.Add(table);

            return doc;
        }  ]]> 
</xsl:when>
<xsl:otherwise>   
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddTbl.. FK1 ChildrenList Top 1  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl66Genusses'">   
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">   
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">   
</xsl:when>
<xsl:otherwise>       <![CDATA[      
        private static Document Add]]><xsl:value-of select="TableTK1"/><![CDATA[ChildrenList(Document doc, ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> ]]><xsl:value-of select="TranslateTK1"/><![CDATA[List)        
        {
            var table = new PdfPTable(4)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f   //fix the absolute width of the table
            };  ]]> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddTbl.. FK1 ChildrenList Top 2  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">       <![CDATA[      
            table.SetWidths(new[] { 0.08f, 1.22f, 2.50f, 1.50f });  ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">      <![CDATA[      
            table.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });  ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">      <![CDATA[      
            table.SetWidths(new[] { 0.11f, 1.19f, 2.50f, 1.50f });  ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl12Subphylums'">      <![CDATA[      
            table.SetWidths(new[] { 0.14f, 1.16f, 2.50f, 1.50f });    ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl15Subdivisions'">      <![CDATA[      
            table.SetWidths(new[] { 0.14f, 1.16f, 2.50f, 1.50f });    ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl18Superclasses'">      <![CDATA[      
            table.SetWidths(new[] { 0.17f, 1.13f, 2.50f, 1.50f });    ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl21Classes'">      <![CDATA[      
            table.SetWidths(new[] { 0.20f, 1.10f, 2.50f, 1.50f});    ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl24Subclasses'">      <![CDATA[      
            table.SetWidths(new[] { 0.23f, 1.07f, 2.50f, 1.50f });    ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl27Infraclasses'">      <![CDATA[      
            table.SetWidths(new[] { 0.26f, 1.04f, 2.50f, 1.50f });    ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl30Legios'">      <![CDATA[      
            table.SetWidths(new[] { 0.29f, 1.01f, 2.50f, 1.50f });    ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl33Ordos'">      <![CDATA[      
            table.SetWidths(new[] { 0.32f, 0.98f, 2.50f, 1.50f });    ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl36Subordos'">      <![CDATA[      
            table.SetWidths(new[] { 0.35f, 0.95f, 2.50f, 1.50f });    ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl39Infraordos'">      <![CDATA[      
            table.SetWidths(new[] { 0.38f, 0.92f, 2.50f, 1.50f });    ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl42Superfamilies'">      <![CDATA[      
            table.SetWidths(new[] { 0.41f, 0.89f, 2.50f, 1.50f });    ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl45Families'">      <![CDATA[      
            table.SetWidths(new[] { 0.44f, 0.86f, 2.50f, 1.50f });    ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl48Subfamilies'">      <![CDATA[      
            table.SetWidths(new[] { 0.47f, 0.83f, 2.50f, 1.50f });    ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl51Infrafamilies'">      <![CDATA[      
            table.SetWidths(new[] { 0.50f, 0.80f, 2.50f, 1.50f });    ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl54Supertribusses'">      <![CDATA[      
            table.SetWidths(new[] { 0.53f, 0.77f, 2.50f, 1.50f });    ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl57Tribusses'">      <![CDATA[      
            table.SetWidths(new[] { 0.56f, 0.74f, 2.50f, 1.50f });    ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl60Subtribusses'">      <![CDATA[      
            table.SetWidths(new[] { 0.59f, 0.71f, 2.50f, 1.50f });    ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl63Infratribusses'">      <![CDATA[      
            table.SetWidths(new[] { 0.62f, 0.68f, 2.50f, 1.50f });    ]]> 
</xsl:when>
<xsl:otherwise>   
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddTbl.. FK1 ChildrenList Top 3  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl66Genusses'">   
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">   
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">   
</xsl:when>
<xsl:otherwise>       <![CDATA[      
            table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 2, Border = 0 });  // 1. -2. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDirectChild, SmallBoldFont)) { Border = 0 }); //   3. field
            table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field

            foreach (var t in ]]><xsl:value-of select="TranslateTK1"/><![CDATA[List)   
            {
                var t1 = t.]]><xsl:value-of select="NameTK1"/><![CDATA[;     
                var t2 = t.Author;
                var t3 = t.GerName;
                var t4 = t.EngName;
                var t5 = t.FraName;
                var t6 = t.PorName;

                table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.   field
                table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.]]><xsl:value-of select="BasisTK1"/><![CDATA[, SmallFont)) { Border = 0 });  // 2. field
                table.AddCell(new PdfPCell(new Phrase(t1 + " - " + t2 + " " + t3 + " " + t4 + " " + t5 + " " + t6, SmallFont)) { Border = 0 });   // 3. field
                table.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });  // 4.  field
            }
            doc.Add(table);

            return doc;
        }  ]]> 
</xsl:otherwise>    
</xsl:choose> 


<xsl:choose>
<xsl:when test="Table ='Public Commands 1  AddTbl.. FK2 ChildrenList Top 2  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">        <![CDATA[        
        private static Document Add]]><xsl:value-of select="TableTK2"/><![CDATA[ChildrenList(Document doc, ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> ]]><xsl:value-of select="TranslateTK2"/><![CDATA[List)        
        {
            var table = new PdfPTable(3)
            {
                TotalWidth = 792f, //actual width of table in points
                LockedWidth = true,
                WidthPercentage = 100,
                HorizontalAlignment = 0,  //0=Left aLign, 1=Center
                SpacingBefore = 0f,
                SpacingAfter = 10f   //fix the absolute width of the table
            };
            table.SetWidths(new[] { 0.08f, 1.22f, 4.00f }); 

            table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 2, Border = 0 });  // 1. -2. field
            table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.ReportDirectChild, SmallBoldFont)) { Border = 0 }); //   3. field

            foreach (var t in ]]><xsl:value-of select="TranslateTK2"/><![CDATA[List)
            {
                var t1 = t.]]><xsl:value-of select="NameTK2"/><![CDATA[;
                var t2 = t.Author;
                var t3 = t.GerName;
                var t4 = t.EngName;
                var t5 = t.FraName;
                var t6 = t.PorName;

                table.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 1, Border = 0 });  // 1.   field
                table.AddCell(new PdfPCell(new Phrase(CultRes.StringsRes.]]><xsl:value-of select="BasisTK1"/><![CDATA[, SmallFont)) { Border = 0 });  // 2. field
                table.AddCell(new PdfPCell(new Phrase(t1 + " - " + t2 + " " + t3 + " " + t4 + " " + t5 + " " + t6, SmallFont)) { Border = 0 });   // 3. field
            }
            doc.Add(table);

            return doc;
        }  ]]> 
</xsl:when>
<xsl:otherwise>           
</xsl:otherwise>    
</xsl:choose> 
   <![CDATA[}
}]]>   
</xsl:template>
</xsl:stylesheet>






