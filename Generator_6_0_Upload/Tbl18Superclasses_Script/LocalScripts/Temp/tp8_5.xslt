<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions" >
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Views.Report.PDF;    ]]>

<![CDATA[// <!-- Interface Skriptdatum: ]]> <xsl:value-of select="DateTime"/>   <![CDATA[  -->  

namespace Te.Atis.Ui.Desktop.Views.Report   
{      ]]> 

<xsl:choose>
<xsl:when test="Table ='ReportViewModel++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">       <![CDATA[ 
     public void Get]]><xsl:value-of select="Table"/><![CDATA[ById(int id)
    {
        ]]><xsl:value-of select="Table"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="Table"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[Id(id));	  ]]> 
</xsl:when>
<xsl:otherwise>     <![CDATA[      
     public void Get]]><xsl:value-of select="Table"/><![CDATA[ById(int id)
    {
        ]]><xsl:value-of select="Table"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="Table"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[Id(id));		  ]]> 
</xsl:otherwise>    
</xsl:choose>  

<xsl:choose>
<xsl:when test="Table ='ReportViewModel 1  ++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        ]]><xsl:value-of select="TableTK2"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK2"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));	  ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	

        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		  ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	

        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		  ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl12Subphylums'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	

        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		

        var regnumPhylumId = RegnumIdTbl06PhylumsSelect(phylumSubphylumId);

        Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));  ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl15Subdivisions'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	

        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		

        var regnumDivisionId = RegnumIdTbl09DivisionsSelect(divisionSubdivisionId);

        Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId));    ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl18Superclasses'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK2"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	
        if (subdivisionSuperclassId == 1)  //special situation because SubphylumID = 48 and SubdivisionID = 1
        {
             var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	
             ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		
         
            var phylumSubphylumId = PhylumIdTbl12SubphylumsSelect(subphylumSuperclassId);
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumIdAndHash(phylumSubphylumId));

             var regnumPhylumId = RegnumIdTbl06PhylumsSelect(phylumSubphylumId);
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));
      }
      else
      {
         Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsBySubdivisionIdAndHash(subdivisionSuperclassId));

         var divisionSubdivisionId = DivisionIdTbl15SubdivisionsSelect(subdivisionSuperclassId);
         Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionIdAndHash(divisionSubdivisionId));

          var regnumDivisionId = RegnumIdTbl09DivisionsSelect(divisionSubdivisionId);
         Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId)); 
      }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl21Classes'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	
        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		

        var subdivisionSuperclassId = SubdivisionIdTbl18SuperclassesSelect(superclassClassId);
        if (subdivisionSuperclassId == 1)  //special situation because SubphylumID = 48 and SubdivisionID = 1
        {
            var subphylumSuperclassId = SubphylumIdTbl18SuperclassesSelect(id);
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumIdAndHash(subphylumSuperclassId));
         
            var phylumSubphylumId = PhylumIdTbl12SubphylumsSelect(subphylumSuperclassId);
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumIdAndHash(phylumSubphylumId));

             var regnumPhylumId = RegnumIdTbl06PhylumsSelect(phylumSubphylumId);
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));
      }
      else
      {
         Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsBySubdivisionIdAndHash(subdivisionSuperclassId));

         var divisionSubdivisionId = DivisionIdTbl15SubdivisionsSelect(subdivisionSuperclassId);
         Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionIdAndHash(divisionSubdivisionId));

          var regnumDivisionId = RegnumIdTbl09DivisionsSelect(divisionSubdivisionId);
         Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId)); 
      }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl24Subclasses'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	
        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		

        var superclassClassId = SuperclassIdTbl21ClassesSelect(classSubclassId);
        Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassIdAndHash(superclassClassId));

        var subdivisionSuperclassId = SubdivisionIdTbl18SuperclassesSelect(superclassClassId);
        if (subdivisionSuperclassId == 1)  //special situation because SubphylumID = 48 and SubdivisionID = 1
        {
            var subphylumSuperclassId = SubphylumIdTbl18SuperclassesSelect(id);
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumIdAndHash(subphylumSuperclassId));
         
            var phylumSubphylumId = PhylumIdTbl12SubphylumsSelect(subphylumSuperclassId);
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumIdAndHash(phylumSubphylumId));

             var regnumPhylumId = RegnumIdTbl06PhylumsSelect(phylumSubphylumId);
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));
      }
      else
      {
         Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsBySubdivisionIdAndHash(subdivisionSuperclassId));

         var divisionSubdivisionId = DivisionIdTbl15SubdivisionsSelect(subdivisionSuperclassId);
         Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionIdAndHash(divisionSubdivisionId));

          var regnumDivisionId = RegnumIdTbl09DivisionsSelect(divisionSubdivisionId);
         Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId)); 
      }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl27Infraclasses'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	
        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		

         var classSubclassId = ClassIdTbl24SubclassesSelect(subclassInfraclassId);
         Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassIdAndHash(classSubclassId));

        var superclassClassId = SuperclassIdTbl21ClassesSelect(classSubclassId);
        Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassIdAndHash(superclassClassId));

        var subdivisionSuperclassId = SubdivisionIdTbl18SuperclassesSelect(superclassClassId);
        if (subdivisionSuperclassId == 1)  //special situation because SubphylumID = 48 and SubdivisionID = 1
        {
            var subphylumSuperclassId = SubphylumIdTbl18SuperclassesSelect(id);
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumIdAndHash(subphylumSuperclassId));
         
            var phylumSubphylumId = PhylumIdTbl12SubphylumsSelect(subphylumSuperclassId);
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumIdAndHash(phylumSubphylumId));

             var regnumPhylumId = RegnumIdTbl06PhylumsSelect(phylumSubphylumId);
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));
      }
      else
      {
         Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsBySubdivisionIdAndHash(subdivisionSuperclassId));

         var divisionSubdivisionId = DivisionIdTbl15SubdivisionsSelect(subdivisionSuperclassId);
         Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionIdAndHash(divisionSubdivisionId));

          var regnumDivisionId = RegnumIdTbl09DivisionsSelect(divisionSubdivisionId);
         Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId)); 
      }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl30Legios'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	
        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		

         var subclassInfraclassId = SubclassIdTbl27InfraclassesSelect(infraclassLegioId);
         Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesBySubclassIdAndHash(subclassInfraclassId));

         var classSubclassId = ClassIdTbl24SubclassesSelect(subclassInfraclassId);
         Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassIdAndHash(classSubclassId));

        var superclassClassId = SuperclassIdTbl21ClassesSelect(classSubclassId);
        Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassIdAndHash(superclassClassId));

        var subdivisionSuperclassId = SubdivisionIdTbl18SuperclassesSelect(superclassClassId);
        if (subdivisionSuperclassId == 1)  //special situation because SubphylumID = 48 and SubdivisionID = 1
        {
            var subphylumSuperclassId = SubphylumIdTbl18SuperclassesSelect(id);
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumIdAndHash(subphylumSuperclassId));
         
            var phylumSubphylumId = PhylumIdTbl12SubphylumsSelect(subphylumSuperclassId);
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumIdAndHash(phylumSubphylumId));

             var regnumPhylumId = RegnumIdTbl06PhylumsSelect(phylumSubphylumId);
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));
      }
      else
      {
         Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsBySubdivisionIdAndHash(subdivisionSuperclassId));

         var divisionSubdivisionId = DivisionIdTbl15SubdivisionsSelect(subdivisionSuperclassId);
         Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionIdAndHash(divisionSubdivisionId));

          var regnumDivisionId = RegnumIdTbl09DivisionsSelect(divisionSubdivisionId);
         Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId)); 
      }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl33Ordos'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	
        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		

         var infraclassLegioId = InfraclassIdTbl30LegiosSelect(legioOrdoId);
         Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass>(_businessLayer.ListTbl27InfraclassesByInfraclassIdAndHash(infraclassLegioId));

         var subclassInfraclassId = SubclassIdTbl27InfraclassesSelect(infraclassLegioId);
         Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesBySubclassIdAndHash(subclassInfraclassId));

         var classSubclassId = ClassIdTbl24SubclassesSelect(subclassInfraclassId);
         Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassIdAndHash(classSubclassId));

        var superclassClassId = SuperclassIdTbl21ClassesSelect(classSubclassId);
        Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassIdAndHash(superclassClassId));

        var subdivisionSuperclassId = SubdivisionIdTbl18SuperclassesSelect(superclassClassId);
        if (subdivisionSuperclassId == 1)  //special situation because SubphylumID = 48 and SubdivisionID = 1
        {
            var subphylumSuperclassId = SubphylumIdTbl18SuperclassesSelect(id);
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumIdAndHash(subphylumSuperclassId));
         
            var phylumSubphylumId = PhylumIdTbl12SubphylumsSelect(subphylumSuperclassId);
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumIdAndHash(phylumSubphylumId));

             var regnumPhylumId = RegnumIdTbl06PhylumsSelect(phylumSubphylumId);
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));
      }
      else
      {
         Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsBySubdivisionIdAndHash(subdivisionSuperclassId));

         var divisionSubdivisionId = DivisionIdTbl15SubdivisionsSelect(subdivisionSuperclassId);
         Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionIdAndHash(divisionSubdivisionId));

          var regnumDivisionId = RegnumIdTbl09DivisionsSelect(divisionSubdivisionId);
         Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId)); 
      }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl36Subordos'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	
        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		

        var legioOrdoId = LegioIdTbl33OrdosSelect(ordoSubordoId);
        Tbl30LegiosList = new ObservableCollection<Tbl30Legio>(_businessLayer.ListTbl30LegiosByLegioIdAndHash(legioOrdoId));

         var infraclassLegioId = InfraclassIdTbl30LegiosSelect(legioOrdoId);
         Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass>(_businessLayer.ListTbl27InfraclassesByInfraclassIdAndHash(infraclassLegioId));

         var subclassInfraclassId = SubclassIdTbl27InfraclassesSelect(infraclassLegioId);
         Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesBySubclassIdAndHash(subclassInfraclassId));

         var classSubclassId = ClassIdTbl24SubclassesSelect(subclassInfraclassId);
         Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassIdAndHash(classSubclassId));

        var superclassClassId = SuperclassIdTbl21ClassesSelect(classSubclassId);
        Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassIdAndHash(superclassClassId));

        var subdivisionSuperclassId = SubdivisionIdTbl18SuperclassesSelect(superclassClassId);
        if (subdivisionSuperclassId == 1)  //special situation because SubphylumID = 48 and SubdivisionID = 1
        {
            var subphylumSuperclassId = SubphylumIdTbl18SuperclassesSelect(id);
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumIdAndHash(subphylumSuperclassId));
         
            var phylumSubphylumId = PhylumIdTbl12SubphylumsSelect(subphylumSuperclassId);
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumIdAndHash(phylumSubphylumId));

             var regnumPhylumId = RegnumIdTbl06PhylumsSelect(phylumSubphylumId);
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));
      }
      else
      {
         Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsBySubdivisionIdAndHash(subdivisionSuperclassId));

         var divisionSubdivisionId = DivisionIdTbl15SubdivisionsSelect(subdivisionSuperclassId);
         Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionIdAndHash(divisionSubdivisionId));

          var regnumDivisionId = RegnumIdTbl09DivisionsSelect(divisionSubdivisionId);
         Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId)); 
      }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl39Infraordos'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	
        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		

        var ordoSubordoId = OrdoIdTbl36SubordosSelect(subordoInfraordoId);
        Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>(_businessLayer.ListTbl33OrdosByOrdoIdAndHash(ordoSubordoId));

        var legioOrdoId = LegioIdTbl33OrdosSelect(ordoSubordoId);
        Tbl30LegiosList = new ObservableCollection<Tbl30Legio>(_businessLayer.ListTbl30LegiosByLegioIdAndHash(legioOrdoId));

         var infraclassLegioId = InfraclassIdTbl30LegiosSelect(legioOrdoId);
         Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass>(_businessLayer.ListTbl27InfraclassesByInfraclassIdAndHash(infraclassLegioId));

         var subclassInfraclassId = SubclassIdTbl27InfraclassesSelect(infraclassLegioId);
         Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesBySubclassIdAndHash(subclassInfraclassId));

         var classSubclassId = ClassIdTbl24SubclassesSelect(subclassInfraclassId);
         Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassIdAndHash(classSubclassId));

        var superclassClassId = SuperclassIdTbl21ClassesSelect(classSubclassId);
        Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassIdAndHash(superclassClassId));

        var subdivisionSuperclassId = SubdivisionIdTbl18SuperclassesSelect(superclassClassId);
        if (subdivisionSuperclassId == 1)  //special situation because SubphylumID = 48 and SubdivisionID = 1
        {
            var subphylumSuperclassId = SubphylumIdTbl18SuperclassesSelect(id);
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumIdAndHash(subphylumSuperclassId));
         
            var phylumSubphylumId = PhylumIdTbl12SubphylumsSelect(subphylumSuperclassId);
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumIdAndHash(phylumSubphylumId));

             var regnumPhylumId = RegnumIdTbl06PhylumsSelect(phylumSubphylumId);
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));
      }
      else
      {
         Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsBySubdivisionIdAndHash(subdivisionSuperclassId));

         var divisionSubdivisionId = DivisionIdTbl15SubdivisionsSelect(subdivisionSuperclassId);
         Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionIdAndHash(divisionSubdivisionId));

          var regnumDivisionId = RegnumIdTbl09DivisionsSelect(divisionSubdivisionId);
         Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId)); 
      }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl42Superfamilies'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	
        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		

        var subordoInfraordoId = SubordoIdTbl39InfraordosSelect(infraordoSuperfamilyId);
        Tbl36SubordosList = new ObservableCollection<Tbl36Subordo>(_businessLayer.ListTbl36SubordosBySubordoIdAndHash(subordoInfraordoId));

        var ordoSubordoId = OrdoIdTbl36SubordosSelect(subordoInfraordoId);
        Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>(_businessLayer.ListTbl33OrdosByOrdoIdAndHash(ordoSubordoId));

        var legioOrdoId = LegioIdTbl33OrdosSelect(ordoSubordoId);
        Tbl30LegiosList = new ObservableCollection<Tbl30Legio>(_businessLayer.ListTbl30LegiosByLegioIdAndHash(legioOrdoId));

         var infraclassLegioId = InfraclassIdTbl30LegiosSelect(legioOrdoId);
         Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass>(_businessLayer.ListTbl27InfraclassesByInfraclassIdAndHash(infraclassLegioId));

         var subclassInfraclassId = SubclassIdTbl27InfraclassesSelect(infraclassLegioId);
         Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesBySubclassIdAndHash(subclassInfraclassId));

         var classSubclassId = ClassIdTbl24SubclassesSelect(subclassInfraclassId);
         Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassIdAndHash(classSubclassId));

        var superclassClassId = SuperclassIdTbl21ClassesSelect(classSubclassId);
        Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassIdAndHash(superclassClassId));

        var subdivisionSuperclassId = SubdivisionIdTbl18SuperclassesSelect(superclassClassId);
        if (subdivisionSuperclassId == 1)  //special situation because SubphylumID = 48 and SubdivisionID = 1
        {
            var subphylumSuperclassId = SubphylumIdTbl18SuperclassesSelect(id);
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumIdAndHash(subphylumSuperclassId));
         
            var phylumSubphylumId = PhylumIdTbl12SubphylumsSelect(subphylumSuperclassId);
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumIdAndHash(phylumSubphylumId));

             var regnumPhylumId = RegnumIdTbl06PhylumsSelect(phylumSubphylumId);
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));
      }
      else
      {
         Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsBySubdivisionIdAndHash(subdivisionSuperclassId));

         var divisionSubdivisionId = DivisionIdTbl15SubdivisionsSelect(subdivisionSuperclassId);
         Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionIdAndHash(divisionSubdivisionId));

          var regnumDivisionId = RegnumIdTbl09DivisionsSelect(divisionSubdivisionId);
         Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId)); 
      }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl45Families'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	
        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		

        var infraordoSuperfamilyId = InfraordoIdTbl42SuperfamiliesSelect(superfamilyFamilyId);
        Tbl39InfraordosList = new ObservableCollection<Tbl39Infraordo>(_businessLayer.ListTbl39InfraordosByInfraordoIdAndHash(infraordoSuperfamilyId));

        var subordoInfraordoId = SubordoIdTbl39InfraordosSelect(infraordoSuperfamilyId);
        Tbl36SubordosList = new ObservableCollection<Tbl36Subordo>(_businessLayer.ListTbl36SubordosBySubordoIdAndHash(subordoInfraordoId));

        var ordoSubordoId = OrdoIdTbl36SubordosSelect(subordoInfraordoId);
        Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>(_businessLayer.ListTbl33OrdosByOrdoIdAndHash(ordoSubordoId));

        var legioOrdoId = LegioIdTbl33OrdosSelect(ordoSubordoId);
        Tbl30LegiosList = new ObservableCollection<Tbl30Legio>(_businessLayer.ListTbl30LegiosByLegioIdAndHash(legioOrdoId));

         var infraclassLegioId = InfraclassIdTbl30LegiosSelect(legioOrdoId);
         Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass>(_businessLayer.ListTbl27InfraclassesByInfraclassIdAndHash(infraclassLegioId));

         var subclassInfraclassId = SubclassIdTbl27InfraclassesSelect(infraclassLegioId);
         Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesBySubclassIdAndHash(subclassInfraclassId));

         var classSubclassId = ClassIdTbl24SubclassesSelect(subclassInfraclassId);
         Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassIdAndHash(classSubclassId));

        var superclassClassId = SuperclassIdTbl21ClassesSelect(classSubclassId);
        Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassIdAndHash(superclassClassId));

        var subdivisionSuperclassId = SubdivisionIdTbl18SuperclassesSelect(superclassClassId);
        if (subdivisionSuperclassId == 1)  //special situation because SubphylumID = 48 and SubdivisionID = 1
        {
            var subphylumSuperclassId = SubphylumIdTbl18SuperclassesSelect(id);
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumIdAndHash(subphylumSuperclassId));
         
            var phylumSubphylumId = PhylumIdTbl12SubphylumsSelect(subphylumSuperclassId);
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumIdAndHash(phylumSubphylumId));

             var regnumPhylumId = RegnumIdTbl06PhylumsSelect(phylumSubphylumId);
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));
      }
      else
      {
         Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsBySubdivisionIdAndHash(subdivisionSuperclassId));

         var divisionSubdivisionId = DivisionIdTbl15SubdivisionsSelect(subdivisionSuperclassId);
         Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionIdAndHash(divisionSubdivisionId));

          var regnumDivisionId = RegnumIdTbl09DivisionsSelect(divisionSubdivisionId);
         Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId)); 
      }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl48Subfamilies'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	
        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		

        var superfamilyFamilyId = SuperfamilyIdTbl45FamiliesSelect(familySubfamilyId);
        Tbl42SuperfamiliesList = new ObservableCollection<Tbl42Superfamily>(_businessLayer.ListTbl42SuperfamiliesBySuperfamilyIdAndHash(superfamilyFamilyId));

        var infraordoSuperfamilyId = InfraordoIdTbl42SuperfamiliesSelect(superfamilyFamilyId);
        Tbl39InfraordosList = new ObservableCollection<Tbl39Infraordo>(_businessLayer.ListTbl39InfraordosByInfraordoIdAndHash(infraordoSuperfamilyId));

        var subordoInfraordoId = SubordoIdTbl39InfraordosSelect(infraordoSuperfamilyId);
        Tbl36SubordosList = new ObservableCollection<Tbl36Subordo>(_businessLayer.ListTbl36SubordosBySubordoIdAndHash(subordoInfraordoId));

        var ordoSubordoId = OrdoIdTbl36SubordosSelect(subordoInfraordoId);
        Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>(_businessLayer.ListTbl33OrdosByOrdoIdAndHash(ordoSubordoId));

        var legioOrdoId = LegioIdTbl33OrdosSelect(ordoSubordoId);
        Tbl30LegiosList = new ObservableCollection<Tbl30Legio>(_businessLayer.ListTbl30LegiosByLegioIdAndHash(legioOrdoId));

         var infraclassLegioId = InfraclassIdTbl30LegiosSelect(legioOrdoId);
         Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass>(_businessLayer.ListTbl27InfraclassesByInfraclassIdAndHash(infraclassLegioId));

         var subclassInfraclassId = SubclassIdTbl27InfraclassesSelect(infraclassLegioId);
         Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesBySubclassIdAndHash(subclassInfraclassId));

         var classSubclassId = ClassIdTbl24SubclassesSelect(subclassInfraclassId);
         Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassIdAndHash(classSubclassId));

        var superclassClassId = SuperclassIdTbl21ClassesSelect(classSubclassId);
        Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassIdAndHash(superclassClassId));

        var subdivisionSuperclassId = SubdivisionIdTbl18SuperclassesSelect(superclassClassId);
        if (subdivisionSuperclassId == 1)  //special situation because SubphylumID = 48 and SubdivisionID = 1
        {
            var subphylumSuperclassId = SubphylumIdTbl18SuperclassesSelect(id);
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumIdAndHash(subphylumSuperclassId));
         
            var phylumSubphylumId = PhylumIdTbl12SubphylumsSelect(subphylumSuperclassId);
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumIdAndHash(phylumSubphylumId));

             var regnumPhylumId = RegnumIdTbl06PhylumsSelect(phylumSubphylumId);
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));
      }
      else
      {
         Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsBySubdivisionIdAndHash(subdivisionSuperclassId));

         var divisionSubdivisionId = DivisionIdTbl15SubdivisionsSelect(subdivisionSuperclassId);
         Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionIdAndHash(divisionSubdivisionId));

          var regnumDivisionId = RegnumIdTbl09DivisionsSelect(divisionSubdivisionId);
         Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId)); 
      }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl51Infrafamilies'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	
        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		

        var familySubfamilyId = FamilyIdTbl48SubfamiliesSelect(subfamilyInfrafamilyId);
        Tbl45FamiliesList = new ObservableCollection<Tbl45Family>(_businessLayer.ListTbl45FamiliesByFamilyIdAndHash(familySubfamilyId));

        var superfamilyFamilyId = SuperfamilyIdTbl45FamiliesSelect(familySubfamilyId);
        Tbl42SuperfamiliesList = new ObservableCollection<Tbl42Superfamily>(_businessLayer.ListTbl42SuperfamiliesBySuperfamilyIdAndHash(superfamilyFamilyId));

        var infraordoSuperfamilyId = InfraordoIdTbl42SuperfamiliesSelect(superfamilyFamilyId);
        Tbl39InfraordosList = new ObservableCollection<Tbl39Infraordo>(_businessLayer.ListTbl39InfraordosByInfraordoIdAndHash(infraordoSuperfamilyId));

        var subordoInfraordoId = SubordoIdTbl39InfraordosSelect(infraordoSuperfamilyId);
        Tbl36SubordosList = new ObservableCollection<Tbl36Subordo>(_businessLayer.ListTbl36SubordosBySubordoIdAndHash(subordoInfraordoId));

        var ordoSubordoId = OrdoIdTbl36SubordosSelect(subordoInfraordoId);
        Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>(_businessLayer.ListTbl33OrdosByOrdoIdAndHash(ordoSubordoId));

        var legioOrdoId = LegioIdTbl33OrdosSelect(ordoSubordoId);
        Tbl30LegiosList = new ObservableCollection<Tbl30Legio>(_businessLayer.ListTbl30LegiosByLegioIdAndHash(legioOrdoId));

         var infraclassLegioId = InfraclassIdTbl30LegiosSelect(legioOrdoId);
         Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass>(_businessLayer.ListTbl27InfraclassesByInfraclassIdAndHash(infraclassLegioId));

         var subclassInfraclassId = SubclassIdTbl27InfraclassesSelect(infraclassLegioId);
         Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesBySubclassIdAndHash(subclassInfraclassId));

         var classSubclassId = ClassIdTbl24SubclassesSelect(subclassInfraclassId);
         Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassIdAndHash(classSubclassId));

        var superclassClassId = SuperclassIdTbl21ClassesSelect(classSubclassId);
        Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassIdAndHash(superclassClassId));

        var subdivisionSuperclassId = SubdivisionIdTbl18SuperclassesSelect(superclassClassId);
        if (subdivisionSuperclassId == 1)  //special situation because SubphylumID = 48 and SubdivisionID = 1
        {
            var subphylumSuperclassId = SubphylumIdTbl18SuperclassesSelect(id);
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumIdAndHash(subphylumSuperclassId));
         
            var phylumSubphylumId = PhylumIdTbl12SubphylumsSelect(subphylumSuperclassId);
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumIdAndHash(phylumSubphylumId));

             var regnumPhylumId = RegnumIdTbl06PhylumsSelect(phylumSubphylumId);
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));
      }
      else
      {
         Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsBySubdivisionIdAndHash(subdivisionSuperclassId));

         var divisionSubdivisionId = DivisionIdTbl15SubdivisionsSelect(subdivisionSuperclassId);
         Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionIdAndHash(divisionSubdivisionId));

          var regnumDivisionId = RegnumIdTbl09DivisionsSelect(divisionSubdivisionId);
         Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId)); 
      }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl54Supertribusses'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	
        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		

        var subfamilyInfrafamilyId = SubfamilyIdTbl51InfrafamiliesSelect(infrafamilySupertribusId);
        Tbl48SubfamiliesList = new ObservableCollection<Tbl48Subfamily>(_businessLayer.ListTbl48SubfamiliesBySubfamilyIdAndHash(subfamilyInfrafamilyId));

        var familySubfamilyId = FamilyIdTbl48SubfamiliesSelect(subfamilyInfrafamilyId);
        Tbl45FamiliesList = new ObservableCollection<Tbl45Family>(_businessLayer.ListTbl45FamiliesByFamilyIdAndHash(familySubfamilyId));

        var superfamilyFamilyId = SuperfamilyIdTbl45FamiliesSelect(familySubfamilyId);
        Tbl42SuperfamiliesList = new ObservableCollection<Tbl42Superfamily>(_businessLayer.ListTbl42SuperfamiliesBySuperfamilyIdAndHash(superfamilyFamilyId));

        var infraordoSuperfamilyId = InfraordoIdTbl42SuperfamiliesSelect(superfamilyFamilyId);
        Tbl39InfraordosList = new ObservableCollection<Tbl39Infraordo>(_businessLayer.ListTbl39InfraordosByInfraordoIdAndHash(infraordoSuperfamilyId));

        var subordoInfraordoId = SubordoIdTbl39InfraordosSelect(infraordoSuperfamilyId);
        Tbl36SubordosList = new ObservableCollection<Tbl36Subordo>(_businessLayer.ListTbl36SubordosBySubordoIdAndHash(subordoInfraordoId));

        var ordoSubordoId = OrdoIdTbl36SubordosSelect(subordoInfraordoId);
        Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>(_businessLayer.ListTbl33OrdosByOrdoIdAndHash(ordoSubordoId));

        var legioOrdoId = LegioIdTbl33OrdosSelect(ordoSubordoId);
        Tbl30LegiosList = new ObservableCollection<Tbl30Legio>(_businessLayer.ListTbl30LegiosByLegioIdAndHash(legioOrdoId));

         var infraclassLegioId = InfraclassIdTbl30LegiosSelect(legioOrdoId);
         Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass>(_businessLayer.ListTbl27InfraclassesByInfraclassIdAndHash(infraclassLegioId));

         var subclassInfraclassId = SubclassIdTbl27InfraclassesSelect(infraclassLegioId);
         Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesBySubclassIdAndHash(subclassInfraclassId));

         var classSubclassId = ClassIdTbl24SubclassesSelect(subclassInfraclassId);
         Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassIdAndHash(classSubclassId));

        var superclassClassId = SuperclassIdTbl21ClassesSelect(classSubclassId);
        Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassIdAndHash(superclassClassId));

        var subdivisionSuperclassId = SubdivisionIdTbl18SuperclassesSelect(superclassClassId);
        if (subdivisionSuperclassId == 1)  //special situation because SubphylumID = 48 and SubdivisionID = 1
        {
            var subphylumSuperclassId = SubphylumIdTbl18SuperclassesSelect(id);
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumIdAndHash(subphylumSuperclassId));
         
            var phylumSubphylumId = PhylumIdTbl12SubphylumsSelect(subphylumSuperclassId);
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumIdAndHash(phylumSubphylumId));

             var regnumPhylumId = RegnumIdTbl06PhylumsSelect(phylumSubphylumId);
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));
      }
      else
      {
         Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsBySubdivisionIdAndHash(subdivisionSuperclassId));

         var divisionSubdivisionId = DivisionIdTbl15SubdivisionsSelect(subdivisionSuperclassId);
         Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionIdAndHash(divisionSubdivisionId));

          var regnumDivisionId = RegnumIdTbl09DivisionsSelect(divisionSubdivisionId);
         Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId)); 
      }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl57Tribusses'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	
        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		

        var infrafamilySupertribusId = InfrafamilyIdTbl54SupertribussesSelect(supertribusTribusId);
        Tbl51InfrafamiliesList = new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51InfrafamiliesByInfrafamilyIdAndHash(infrafamilySupertribusId));

        var subfamilyInfrafamilyId = SubfamilyIdTbl51InfrafamiliesSelect(infrafamilySupertribusId);
        Tbl48SubfamiliesList = new ObservableCollection<Tbl48Subfamily>(_businessLayer.ListTbl48SubfamiliesBySubfamilyIdAndHash(subfamilyInfrafamilyId));

        var familySubfamilyId = FamilyIdTbl48SubfamiliesSelect(subfamilyInfrafamilyId);
        Tbl45FamiliesList = new ObservableCollection<Tbl45Family>(_businessLayer.ListTbl45FamiliesByFamilyIdAndHash(familySubfamilyId));

        var superfamilyFamilyId = SuperfamilyIdTbl45FamiliesSelect(familySubfamilyId);
        Tbl42SuperfamiliesList = new ObservableCollection<Tbl42Superfamily>(_businessLayer.ListTbl42SuperfamiliesBySuperfamilyIdAndHash(superfamilyFamilyId));

        var infraordoSuperfamilyId = InfraordoIdTbl42SuperfamiliesSelect(superfamilyFamilyId);
        Tbl39InfraordosList = new ObservableCollection<Tbl39Infraordo>(_businessLayer.ListTbl39InfraordosByInfraordoIdAndHash(infraordoSuperfamilyId));

        var subordoInfraordoId = SubordoIdTbl39InfraordosSelect(infraordoSuperfamilyId);
        Tbl36SubordosList = new ObservableCollection<Tbl36Subordo>(_businessLayer.ListTbl36SubordosBySubordoIdAndHash(subordoInfraordoId));

        var ordoSubordoId = OrdoIdTbl36SubordosSelect(subordoInfraordoId);
        Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>(_businessLayer.ListTbl33OrdosByOrdoIdAndHash(ordoSubordoId));

        var legioOrdoId = LegioIdTbl33OrdosSelect(ordoSubordoId);
        Tbl30LegiosList = new ObservableCollection<Tbl30Legio>(_businessLayer.ListTbl30LegiosByLegioIdAndHash(legioOrdoId));

         var infraclassLegioId = InfraclassIdTbl30LegiosSelect(legioOrdoId);
         Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass>(_businessLayer.ListTbl27InfraclassesByInfraclassIdAndHash(infraclassLegioId));

         var subclassInfraclassId = SubclassIdTbl27InfraclassesSelect(infraclassLegioId);
         Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesBySubclassIdAndHash(subclassInfraclassId));

         var classSubclassId = ClassIdTbl24SubclassesSelect(subclassInfraclassId);
         Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassIdAndHash(classSubclassId));

        var superclassClassId = SuperclassIdTbl21ClassesSelect(classSubclassId);
        Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassIdAndHash(superclassClassId));

        var subdivisionSuperclassId = SubdivisionIdTbl18SuperclassesSelect(superclassClassId);
        if (subdivisionSuperclassId == 1)  //special situation because SubphylumID = 48 and SubdivisionID = 1
        {
            var subphylumSuperclassId = SubphylumIdTbl18SuperclassesSelect(id);
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumIdAndHash(subphylumSuperclassId));
         
            var phylumSubphylumId = PhylumIdTbl12SubphylumsSelect(subphylumSuperclassId);
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumIdAndHash(phylumSubphylumId));

             var regnumPhylumId = RegnumIdTbl06PhylumsSelect(phylumSubphylumId);
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));
      }
      else
      {
         Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsBySubdivisionIdAndHash(subdivisionSuperclassId));

         var divisionSubdivisionId = DivisionIdTbl15SubdivisionsSelect(subdivisionSuperclassId);
         Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionIdAndHash(divisionSubdivisionId));

          var regnumDivisionId = RegnumIdTbl09DivisionsSelect(divisionSubdivisionId);
         Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId)); 
      }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl60Subtribusses'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	
        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		

        var supertribusTribusId = SupertribusIdTbl57TribussesSelect(tribusSubtribusId);
        Tbl54SupertribussesList = new ObservableCollection<Tbl54Supertribus>(_businessLayer.ListTbl54SupertribussesBySupertribusIdAndHash(supertribusTribusId));

        var infrafamilySupertribusId = InfrafamilyIdTbl54SupertribussesSelect(supertribusTribusId);
        Tbl51InfrafamiliesList = new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51InfrafamiliesByInfrafamilyIdAndHash(infrafamilySupertribusId));

        var subfamilyInfrafamilyId = SubfamilyIdTbl51InfrafamiliesSelect(infrafamilySupertribusId);
        Tbl48SubfamiliesList = new ObservableCollection<Tbl48Subfamily>(_businessLayer.ListTbl48SubfamiliesBySubfamilyIdAndHash(subfamilyInfrafamilyId));

        var familySubfamilyId = FamilyIdTbl48SubfamiliesSelect(subfamilyInfrafamilyId);
        Tbl45FamiliesList = new ObservableCollection<Tbl45Family>(_businessLayer.ListTbl45FamiliesByFamilyIdAndHash(familySubfamilyId));

        var superfamilyFamilyId = SuperfamilyIdTbl45FamiliesSelect(familySubfamilyId);
        Tbl42SuperfamiliesList = new ObservableCollection<Tbl42Superfamily>(_businessLayer.ListTbl42SuperfamiliesBySuperfamilyIdAndHash(superfamilyFamilyId));

        var infraordoSuperfamilyId = InfraordoIdTbl42SuperfamiliesSelect(superfamilyFamilyId);
        Tbl39InfraordosList = new ObservableCollection<Tbl39Infraordo>(_businessLayer.ListTbl39InfraordosByInfraordoIdAndHash(infraordoSuperfamilyId));

        var subordoInfraordoId = SubordoIdTbl39InfraordosSelect(infraordoSuperfamilyId);
        Tbl36SubordosList = new ObservableCollection<Tbl36Subordo>(_businessLayer.ListTbl36SubordosBySubordoIdAndHash(subordoInfraordoId));

        var ordoSubordoId = OrdoIdTbl36SubordosSelect(subordoInfraordoId);
        Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>(_businessLayer.ListTbl33OrdosByOrdoIdAndHash(ordoSubordoId));

        var legioOrdoId = LegioIdTbl33OrdosSelect(ordoSubordoId);
        Tbl30LegiosList = new ObservableCollection<Tbl30Legio>(_businessLayer.ListTbl30LegiosByLegioIdAndHash(legioOrdoId));

         var infraclassLegioId = InfraclassIdTbl30LegiosSelect(legioOrdoId);
         Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass>(_businessLayer.ListTbl27InfraclassesByInfraclassIdAndHash(infraclassLegioId));

         var subclassInfraclassId = SubclassIdTbl27InfraclassesSelect(infraclassLegioId);
         Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesBySubclassIdAndHash(subclassInfraclassId));

         var classSubclassId = ClassIdTbl24SubclassesSelect(subclassInfraclassId);
         Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassIdAndHash(classSubclassId));

        var superclassClassId = SuperclassIdTbl21ClassesSelect(classSubclassId);
        Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassIdAndHash(superclassClassId));

        var subdivisionSuperclassId = SubdivisionIdTbl18SuperclassesSelect(superclassClassId);
        if (subdivisionSuperclassId == 1)  //special situation because SubphylumID = 48 and SubdivisionID = 1
        {
            var subphylumSuperclassId = SubphylumIdTbl18SuperclassesSelect(id);
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumIdAndHash(subphylumSuperclassId));
         
            var phylumSubphylumId = PhylumIdTbl12SubphylumsSelect(subphylumSuperclassId);
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumIdAndHash(phylumSubphylumId));

             var regnumPhylumId = RegnumIdTbl06PhylumsSelect(phylumSubphylumId);
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));
      }
      else
      {
         Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsBySubdivisionIdAndHash(subdivisionSuperclassId));

         var divisionSubdivisionId = DivisionIdTbl15SubdivisionsSelect(subdivisionSuperclassId);
         Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionIdAndHash(divisionSubdivisionId));

          var regnumDivisionId = RegnumIdTbl09DivisionsSelect(divisionSubdivisionId);
         Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId)); 
      }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl63Infratribusses'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	
        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		

        var tribusSubtribusId = TribusIdTbl60SubtribussesSelect(subtribusInfratribusId);
        Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57TribussesByTribusIdAndHash(tribusSubtribusId));

        var supertribusTribusId = SupertribusIdTbl57TribussesSelect(tribusSubtribusId);
        Tbl54SupertribussesList = new ObservableCollection<Tbl54Supertribus>(_businessLayer.ListTbl54SupertribussesBySupertribusIdAndHash(supertribusTribusId));

        var infrafamilySupertribusId = InfrafamilyIdTbl54SupertribussesSelect(supertribusTribusId);
        Tbl51InfrafamiliesList = new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51InfrafamiliesByInfrafamilyIdAndHash(infrafamilySupertribusId));

        var subfamilyInfrafamilyId = SubfamilyIdTbl51InfrafamiliesSelect(infrafamilySupertribusId);
        Tbl48SubfamiliesList = new ObservableCollection<Tbl48Subfamily>(_businessLayer.ListTbl48SubfamiliesBySubfamilyIdAndHash(subfamilyInfrafamilyId));

        var familySubfamilyId = FamilyIdTbl48SubfamiliesSelect(subfamilyInfrafamilyId);
        Tbl45FamiliesList = new ObservableCollection<Tbl45Family>(_businessLayer.ListTbl45FamiliesByFamilyIdAndHash(familySubfamilyId));

        var superfamilyFamilyId = SuperfamilyIdTbl45FamiliesSelect(familySubfamilyId);
        Tbl42SuperfamiliesList = new ObservableCollection<Tbl42Superfamily>(_businessLayer.ListTbl42SuperfamiliesBySuperfamilyIdAndHash(superfamilyFamilyId));

        var infraordoSuperfamilyId = InfraordoIdTbl42SuperfamiliesSelect(superfamilyFamilyId);
        Tbl39InfraordosList = new ObservableCollection<Tbl39Infraordo>(_businessLayer.ListTbl39InfraordosByInfraordoIdAndHash(infraordoSuperfamilyId));

        var subordoInfraordoId = SubordoIdTbl39InfraordosSelect(infraordoSuperfamilyId);
        Tbl36SubordosList = new ObservableCollection<Tbl36Subordo>(_businessLayer.ListTbl36SubordosBySubordoIdAndHash(subordoInfraordoId));

        var ordoSubordoId = OrdoIdTbl36SubordosSelect(subordoInfraordoId);
        Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>(_businessLayer.ListTbl33OrdosByOrdoIdAndHash(ordoSubordoId));

        var legioOrdoId = LegioIdTbl33OrdosSelect(ordoSubordoId);
        Tbl30LegiosList = new ObservableCollection<Tbl30Legio>(_businessLayer.ListTbl30LegiosByLegioIdAndHash(legioOrdoId));

         var infraclassLegioId = InfraclassIdTbl30LegiosSelect(legioOrdoId);
         Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass>(_businessLayer.ListTbl27InfraclassesByInfraclassIdAndHash(infraclassLegioId));

         var subclassInfraclassId = SubclassIdTbl27InfraclassesSelect(infraclassLegioId);
         Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesBySubclassIdAndHash(subclassInfraclassId));

         var classSubclassId = ClassIdTbl24SubclassesSelect(subclassInfraclassId);
         Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassIdAndHash(classSubclassId));

        var superclassClassId = SuperclassIdTbl21ClassesSelect(classSubclassId);
        Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassIdAndHash(superclassClassId));

        var subdivisionSuperclassId = SubdivisionIdTbl18SuperclassesSelect(superclassClassId);
        if (subdivisionSuperclassId == 1)  //special situation because SubphylumID = 48 and SubdivisionID = 1
        {
            var subphylumSuperclassId = SubphylumIdTbl18SuperclassesSelect(id);
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumIdAndHash(subphylumSuperclassId));
         
            var phylumSubphylumId = PhylumIdTbl12SubphylumsSelect(subphylumSuperclassId);
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumIdAndHash(phylumSubphylumId));

             var regnumPhylumId = RegnumIdTbl06PhylumsSelect(phylumSubphylumId);
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));
      }
      else
      {
         Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsBySubdivisionIdAndHash(subdivisionSuperclassId));

         var divisionSubdivisionId = DivisionIdTbl15SubdivisionsSelect(subdivisionSuperclassId);
         Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionIdAndHash(divisionSubdivisionId));

          var regnumDivisionId = RegnumIdTbl09DivisionsSelect(divisionSubdivisionId);
         Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId)); 
      }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl66Genusses'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        ]]><xsl:value-of select="TableTK2"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK2"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));
        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	
        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		

        var subtribusInfratribusId = SubtribusIdTbl63InfratribussesSelect(infratribusGenusId);
        Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus>(_businessLayer.ListTbl60SubtribussesBySubtribusIdAndHash(subtribusInfratribusId));

        var tribusSubtribusId = TribusIdTbl60SubtribussesSelect(subtribusInfratribusId);
        Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57TribussesByTribusIdAndHash(tribusSubtribusId));

        var supertribusTribusId = SupertribusIdTbl57TribussesSelect(tribusSubtribusId);
        Tbl54SupertribussesList = new ObservableCollection<Tbl54Supertribus>(_businessLayer.ListTbl54SupertribussesBySupertribusIdAndHash(supertribusTribusId));

        var infrafamilySupertribusId = InfrafamilyIdTbl54SupertribussesSelect(supertribusTribusId);
        Tbl51InfrafamiliesList = new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51InfrafamiliesByInfrafamilyIdAndHash(infrafamilySupertribusId));

        var subfamilyInfrafamilyId = SubfamilyIdTbl51InfrafamiliesSelect(infrafamilySupertribusId);
        Tbl48SubfamiliesList = new ObservableCollection<Tbl48Subfamily>(_businessLayer.ListTbl48SubfamiliesBySubfamilyIdAndHash(subfamilyInfrafamilyId));

        var familySubfamilyId = FamilyIdTbl48SubfamiliesSelect(subfamilyInfrafamilyId);
        Tbl45FamiliesList = new ObservableCollection<Tbl45Family>(_businessLayer.ListTbl45FamiliesByFamilyIdAndHash(familySubfamilyId));

        var superfamilyFamilyId = SuperfamilyIdTbl45FamiliesSelect(familySubfamilyId);
        Tbl42SuperfamiliesList = new ObservableCollection<Tbl42Superfamily>(_businessLayer.ListTbl42SuperfamiliesBySuperfamilyIdAndHash(superfamilyFamilyId));

        var infraordoSuperfamilyId = InfraordoIdTbl42SuperfamiliesSelect(superfamilyFamilyId);
        Tbl39InfraordosList = new ObservableCollection<Tbl39Infraordo>(_businessLayer.ListTbl39InfraordosByInfraordoIdAndHash(infraordoSuperfamilyId));

        var subordoInfraordoId = SubordoIdTbl39InfraordosSelect(infraordoSuperfamilyId);
        Tbl36SubordosList = new ObservableCollection<Tbl36Subordo>(_businessLayer.ListTbl36SubordosBySubordoIdAndHash(subordoInfraordoId));

        var ordoSubordoId = OrdoIdTbl36SubordosSelect(subordoInfraordoId);
        Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>(_businessLayer.ListTbl33OrdosByOrdoIdAndHash(ordoSubordoId));

        var legioOrdoId = LegioIdTbl33OrdosSelect(ordoSubordoId);
        Tbl30LegiosList = new ObservableCollection<Tbl30Legio>(_businessLayer.ListTbl30LegiosByLegioIdAndHash(legioOrdoId));

         var infraclassLegioId = InfraclassIdTbl30LegiosSelect(legioOrdoId);
         Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass>(_businessLayer.ListTbl27InfraclassesByInfraclassIdAndHash(infraclassLegioId));

         var subclassInfraclassId = SubclassIdTbl27InfraclassesSelect(infraclassLegioId);
         Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesBySubclassIdAndHash(subclassInfraclassId));

         var classSubclassId = ClassIdTbl24SubclassesSelect(subclassInfraclassId);
         Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassIdAndHash(classSubclassId));

        var superclassClassId = SuperclassIdTbl21ClassesSelect(classSubclassId);
        Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassIdAndHash(superclassClassId));

        var subdivisionSuperclassId = SubdivisionIdTbl18SuperclassesSelect(superclassClassId);
        if (subdivisionSuperclassId == 1)  //special situation because SubphylumID = 48 and SubdivisionID = 1
        {
            var subphylumSuperclassId = SubphylumIdTbl18SuperclassesSelect(id);
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumIdAndHash(subphylumSuperclassId));
         
            var phylumSubphylumId = PhylumIdTbl12SubphylumsSelect(subphylumSuperclassId);
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumIdAndHash(phylumSubphylumId));

             var regnumPhylumId = RegnumIdTbl06PhylumsSelect(phylumSubphylumId);
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));
      }
      else
      {
         Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsBySubdivisionIdAndHash(subdivisionSuperclassId));

         var divisionSubdivisionId = DivisionIdTbl15SubdivisionsSelect(subdivisionSuperclassId);
         Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionIdAndHash(divisionSubdivisionId));

          var regnumDivisionId = RegnumIdTbl09DivisionsSelect(divisionSubdivisionId);
         Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId)); 
      }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        ]]><xsl:value-of select="TableTK3"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK3"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));
        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	
        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		

        var infratribusGenusId = InfratribusIdTbl66GenussesSelect(genusFiSpeciesId);
        Tbl63InfratribussesList = new ObservableCollection<Tbl63Infratribus>(_businessLayer.ListTbl63InfratribussesByInfratribusIdAndHash(infratribusGenusId));

        var subtribusInfratribusId = SubtribusIdTbl63InfratribussesSelect(infratribusGenusId);
        Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus>(_businessLayer.ListTbl60SubtribussesBySubtribusIdAndHash(subtribusInfratribusId));

        var tribusSubtribusId = TribusIdTbl60SubtribussesSelect(subtribusInfratribusId);
        Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57TribussesByTribusIdAndHash(tribusSubtribusId));

        var supertribusTribusId = SupertribusIdTbl57TribussesSelect(tribusSubtribusId);
        Tbl54SupertribussesList = new ObservableCollection<Tbl54Supertribus>(_businessLayer.ListTbl54SupertribussesBySupertribusIdAndHash(supertribusTribusId));

        var infrafamilySupertribusId = InfrafamilyIdTbl54SupertribussesSelect(supertribusTribusId);
        Tbl51InfrafamiliesList = new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51InfrafamiliesByInfrafamilyIdAndHash(infrafamilySupertribusId));

        var subfamilyInfrafamilyId = SubfamilyIdTbl51InfrafamiliesSelect(infrafamilySupertribusId);
        Tbl48SubfamiliesList = new ObservableCollection<Tbl48Subfamily>(_businessLayer.ListTbl48SubfamiliesBySubfamilyIdAndHash(subfamilyInfrafamilyId));

        var familySubfamilyId = FamilyIdTbl48SubfamiliesSelect(subfamilyInfrafamilyId);
        Tbl45FamiliesList = new ObservableCollection<Tbl45Family>(_businessLayer.ListTbl45FamiliesByFamilyIdAndHash(familySubfamilyId));

        var superfamilyFamilyId = SuperfamilyIdTbl45FamiliesSelect(familySubfamilyId);
        Tbl42SuperfamiliesList = new ObservableCollection<Tbl42Superfamily>(_businessLayer.ListTbl42SuperfamiliesBySuperfamilyIdAndHash(superfamilyFamilyId));

        var infraordoSuperfamilyId = InfraordoIdTbl42SuperfamiliesSelect(superfamilyFamilyId);
        Tbl39InfraordosList = new ObservableCollection<Tbl39Infraordo>(_businessLayer.ListTbl39InfraordosByInfraordoIdAndHash(infraordoSuperfamilyId));

        var subordoInfraordoId = SubordoIdTbl39InfraordosSelect(infraordoSuperfamilyId);
        Tbl36SubordosList = new ObservableCollection<Tbl36Subordo>(_businessLayer.ListTbl36SubordosBySubordoIdAndHash(subordoInfraordoId));

        var ordoSubordoId = OrdoIdTbl36SubordosSelect(subordoInfraordoId);
        Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>(_businessLayer.ListTbl33OrdosByOrdoIdAndHash(ordoSubordoId));

        var legioOrdoId = LegioIdTbl33OrdosSelect(ordoSubordoId);
        Tbl30LegiosList = new ObservableCollection<Tbl30Legio>(_businessLayer.ListTbl30LegiosByLegioIdAndHash(legioOrdoId));

         var infraclassLegioId = InfraclassIdTbl30LegiosSelect(legioOrdoId);
         Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass>(_businessLayer.ListTbl27InfraclassesByInfraclassIdAndHash(infraclassLegioId));

         var subclassInfraclassId = SubclassIdTbl27InfraclassesSelect(infraclassLegioId);
         Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesBySubclassIdAndHash(subclassInfraclassId));

         var classSubclassId = ClassIdTbl24SubclassesSelect(subclassInfraclassId);
         Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassIdAndHash(classSubclassId));

        var superclassClassId = SuperclassIdTbl21ClassesSelect(classSubclassId);
        Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassIdAndHash(superclassClassId));

        var subdivisionSuperclassId = SubdivisionIdTbl18SuperclassesSelect(superclassClassId);
        if (subdivisionSuperclassId == 1)  //special situation because SubphylumID = 48 and SubdivisionID = 1
        {
            var subphylumSuperclassId = SubphylumIdTbl18SuperclassesSelect(id);
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumIdAndHash(subphylumSuperclassId));
         
            var phylumSubphylumId = PhylumIdTbl12SubphylumsSelect(subphylumSuperclassId);
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumIdAndHash(phylumSubphylumId));

             var regnumPhylumId = RegnumIdTbl06PhylumsSelect(phylumSubphylumId);
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));
      }
      else
      {
         Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsBySubdivisionIdAndHash(subdivisionSuperclassId));

         var divisionSubdivisionId = DivisionIdTbl15SubdivisionsSelect(subdivisionSuperclassId);
         Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionIdAndHash(divisionSubdivisionId));

          var regnumDivisionId = RegnumIdTbl09DivisionsSelect(divisionSubdivisionId);
         Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId)); 
      }      ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">       <![CDATA[ 
        //direct children
        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));

        ]]><xsl:value-of select="TableTK3"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK3"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(id));
        //------------------------------------------------------------------------------

         var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id = ]]><xsl:value-of select="BasisFK1"/><![CDATA[Id]]><xsl:value-of select="Table"/><![CDATA[Select(id);	
        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[By]]><xsl:value-of select="BasisFK1"/><![CDATA[IdAndHash(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[]]><xsl:value-of select="Basis"/><![CDATA[Id));		

        var infratribusGenusId = InfratribusIdTbl66GenussesSelect(genusPlSpeciesId);
        Tbl63InfratribussesList = new ObservableCollection<Tbl63Infratribus>(_businessLayer.ListTbl63InfratribussesByInfratribusIdAndHash(infratribusGenusId));

        var subtribusInfratribusId = SubtribusIdTbl63InfratribussesSelect(infratribusGenusId);
        Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus>(_businessLayer.ListTbl60SubtribussesBySubtribusIdAndHash(subtribusInfratribusId));

        var tribusSubtribusId = TribusIdTbl60SubtribussesSelect(subtribusInfratribusId);
        Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57TribussesByTribusIdAndHash(tribusSubtribusId));

        var supertribusTribusId = SupertribusIdTbl57TribussesSelect(tribusSubtribusId);
        Tbl54SupertribussesList = new ObservableCollection<Tbl54Supertribus>(_businessLayer.ListTbl54SupertribussesBySupertribusIdAndHash(supertribusTribusId));

        var infrafamilySupertribusId = InfrafamilyIdTbl54SupertribussesSelect(supertribusTribusId);
        Tbl51InfrafamiliesList = new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51InfrafamiliesByInfrafamilyIdAndHash(infrafamilySupertribusId));

        var subfamilyInfrafamilyId = SubfamilyIdTbl51InfrafamiliesSelect(infrafamilySupertribusId);
        Tbl48SubfamiliesList = new ObservableCollection<Tbl48Subfamily>(_businessLayer.ListTbl48SubfamiliesBySubfamilyIdAndHash(subfamilyInfrafamilyId));

        var familySubfamilyId = FamilyIdTbl48SubfamiliesSelect(subfamilyInfrafamilyId);
        Tbl45FamiliesList = new ObservableCollection<Tbl45Family>(_businessLayer.ListTbl45FamiliesByFamilyIdAndHash(familySubfamilyId));

        var superfamilyFamilyId = SuperfamilyIdTbl45FamiliesSelect(familySubfamilyId);
        Tbl42SuperfamiliesList = new ObservableCollection<Tbl42Superfamily>(_businessLayer.ListTbl42SuperfamiliesBySuperfamilyIdAndHash(superfamilyFamilyId));

        var infraordoSuperfamilyId = InfraordoIdTbl42SuperfamiliesSelect(superfamilyFamilyId);
        Tbl39InfraordosList = new ObservableCollection<Tbl39Infraordo>(_businessLayer.ListTbl39InfraordosByInfraordoIdAndHash(infraordoSuperfamilyId));

        var subordoInfraordoId = SubordoIdTbl39InfraordosSelect(infraordoSuperfamilyId);
        Tbl36SubordosList = new ObservableCollection<Tbl36Subordo>(_businessLayer.ListTbl36SubordosBySubordoIdAndHash(subordoInfraordoId));

        var ordoSubordoId = OrdoIdTbl36SubordosSelect(subordoInfraordoId);
        Tbl33OrdosList = new ObservableCollection<Tbl33Ordo>(_businessLayer.ListTbl33OrdosByOrdoIdAndHash(ordoSubordoId));

        var legioOrdoId = LegioIdTbl33OrdosSelect(ordoSubordoId);
        Tbl30LegiosList = new ObservableCollection<Tbl30Legio>(_businessLayer.ListTbl30LegiosByLegioIdAndHash(legioOrdoId));

         var infraclassLegioId = InfraclassIdTbl30LegiosSelect(legioOrdoId);
         Tbl27InfraclassesList = new ObservableCollection<Tbl27Infraclass>(_businessLayer.ListTbl27InfraclassesByInfraclassIdAndHash(infraclassLegioId));

         var subclassInfraclassId = SubclassIdTbl27InfraclassesSelect(infraclassLegioId);
         Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesBySubclassIdAndHash(subclassInfraclassId));

         var classSubclassId = ClassIdTbl24SubclassesSelect(subclassInfraclassId);
         Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassIdAndHash(classSubclassId));

        var superclassClassId = SuperclassIdTbl21ClassesSelect(classSubclassId);
        Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassIdAndHash(superclassClassId));

        var subdivisionSuperclassId = SubdivisionIdTbl18SuperclassesSelect(superclassClassId);
        if (subdivisionSuperclassId == 1)  //special situation because SubphylumID = 48 and SubdivisionID = 1
        {
            var subphylumSuperclassId = SubphylumIdTbl18SuperclassesSelect(id);
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumIdAndHash(subphylumSuperclassId));
         
            var phylumSubphylumId = PhylumIdTbl12SubphylumsSelect(subphylumSuperclassId);
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumIdAndHash(phylumSubphylumId));

             var regnumPhylumId = RegnumIdTbl06PhylumsSelect(phylumSubphylumId);
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumPhylumId));
      }
      else
      {
         Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsBySubdivisionIdAndHash(subdivisionSuperclassId));

         var divisionSubdivisionId = DivisionIdTbl15SubdivisionsSelect(subdivisionSuperclassId);
         Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09DivisionsByDivisionIdAndHash(divisionSubdivisionId));

          var regnumDivisionId = RegnumIdTbl09DivisionsSelect(divisionSubdivisionId);
         Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumIdAndHash(regnumDivisionId)); 
      }      ]]> 
</xsl:when>
<xsl:otherwise>      
</xsl:otherwise>    
</xsl:choose>  

<xsl:choose>
<xsl:when test="Table ='ReportViewModel 2++++++++++++++++++'">        
</xsl:when>
<xsl:otherwise>     <![CDATA[      
        //------------------------------------------------------------------------------
        Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefExpertsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(id));

        Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefSourcesBy]]><xsl:value-of select="Basis"/><![CDATA[Id(id));

        Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90RefAuthorsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(id));

        Tbl90ExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ExpertsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(id));

        Tbl90SourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90SourcesBy]]><xsl:value-of select="Basis"/><![CDATA[Id(id));

        Tbl90AuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90AuthorsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(id));

        Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(id));
    } 
    private RelayCommand _pdf]]><xsl:value-of select="Table"/><![CDATA[Command;
    public ICommand Pdf]]><xsl:value-of select="Table"/><![CDATA[Command
    {
        get { return _pdf]]><xsl:value-of select="Table"/><![CDATA[Command ?? (_pdf]]><xsl:value-of select="Table"/><![CDATA[Command = new RelayCommand(delegate { CreatePdf]]><xsl:value-of select="Table"/><![CDATA[(_mainId); })); }
    }

    private static void CreatePdf]]><xsl:value-of select="Table"/><![CDATA[(int id)
    {
        Report]]><xsl:value-of select="Table"/><![CDATA[Pdf.CreateMainPdf(id);
    }
    //------------------------------------------------------------------------------
    //------------------------------------------------------------------------------  		  ]]> 
</xsl:otherwise>    
</xsl:choose>  



<xsl:choose>
<xsl:when test="Table ='IBusinesslayer++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">       <![CDATA[ 
        IList<]]><xsl:value-of select="LinqModel"/><![CDATA[> List]]><xsl:value-of select="Table"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);
        IList<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);
        IList<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> List]]><xsl:value-of select="TableTK2"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);

        IList<Tbl90Reference> ListTbl90AuthorsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);
        IList<Tbl90Reference> ListTbl90SourcesBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);
        IList<Tbl90Reference> ListTbl90ExpertsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);

        IList<Tbl93Comment> ListTbl93CommentsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);  ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl18Superclasses'">       <![CDATA[ 
        IList<]]><xsl:value-of select="LinqModel"/><![CDATA[> List]]><xsl:value-of select="Table"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);
        IList<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);
        IList<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> List]]><xsl:value-of select="TableTK2"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);

        IList<Tbl90Reference> ListTbl90AuthorsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);
        IList<Tbl90Reference> ListTbl90SourcesBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);
        IList<Tbl90Reference> ListTbl90ExpertsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);

        IList<Tbl93Comment> ListTbl93CommentsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);  ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl66Genusses'">       <![CDATA[ 
        IList<]]><xsl:value-of select="LinqModel"/><![CDATA[> List]]><xsl:value-of select="Table"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);
        IList<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);
        IList<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> List]]><xsl:value-of select="TableTK2"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);

        IList<Tbl90Reference> ListTbl90AuthorsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);
        IList<Tbl90Reference> ListTbl90SourcesBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);
        IList<Tbl90Reference> ListTbl90ExpertsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);

        IList<Tbl93Comment> ListTbl93CommentsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);  ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">       <![CDATA[ 
        IList<]]><xsl:value-of select="LinqModel"/><![CDATA[> List]]><xsl:value-of select="Table"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);
        IList<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);
        IList<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[> List]]><xsl:value-of select="TableTK3"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);

        IList<Tbl90Reference> ListTbl90AuthorsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);
        IList<Tbl90Reference> ListTbl90SourcesBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);
        IList<Tbl90Reference> ListTbl90ExpertsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);

        IList<Tbl93Comment> ListTbl93CommentsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);  ]]> 
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">       <![CDATA[ 
        IList<]]><xsl:value-of select="LinqModel"/><![CDATA[> List]]><xsl:value-of select="Table"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);
        IList<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);
        IList<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[> List]]><xsl:value-of select="TableTK3"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);

        IList<Tbl90Reference> ListTbl90AuthorsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);
        IList<Tbl90Reference> ListTbl90SourcesBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);
        IList<Tbl90Reference> ListTbl90ExpertsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);

        IList<Tbl93Comment> ListTbl93CommentsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);  ]]> 
</xsl:when>
<xsl:otherwise>     <![CDATA[      
        IList<]]><xsl:value-of select="LinqModel"/><![CDATA[> List]]><xsl:value-of select="Table"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);

        IList<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);

        IList<Tbl90Reference> ListTbl90AuthorsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);
        IList<Tbl90Reference> ListTbl90SourcesBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);
        IList<Tbl90Reference> ListTbl90ExpertsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);

        IList<Tbl93Comment> ListTbl93CommentsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id);	  ]]> 
</xsl:otherwise>    
</xsl:choose>  

<xsl:choose>
<xsl:when test="Table ='Businesslayer++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">       <![CDATA[ 
		public IList<]]><xsl:value-of select="LinqModel"/><![CDATA[> List]]><xsl:value-of select="Table"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				]]><xsl:value-of select="Entitys"/><![CDATA[Repository.OrderBy(r => r.]]><xsl:value-of select="Name"/><![CDATA[ + r.Subregnum),
				p => p.]]><xsl:value-of select="TableTK1"/><![CDATA[, k => k.]]><xsl:value-of select="TableTK2"/><![CDATA[);
		}

		public IList<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return ]]><xsl:value-of select="EntitysTK1"/><![CDATA[Repository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id &&
				e.]]><xsl:value-of select="NameTK1"/><![CDATA[.Contains("#") == false,
				]]><xsl:value-of select="EntitysTK1"/><![CDATA[Repository.OrderBy(r => r.]]><xsl:value-of select="NameTK1"/><![CDATA[),
				p => p.]]><xsl:value-of select="TableTK3"/><![CDATA[, k => k.]]><xsl:value-of select="Table"/><![CDATA[);
		}

		public IList<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> List]]><xsl:value-of select="TableTK2"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return ]]><xsl:value-of select="EntitysTK2"/><![CDATA[Repository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id &&
				e.]]><xsl:value-of select="NameTK2"/><![CDATA[.Contains("#") == false,
				]]><xsl:value-of select="EntitysTK2"/><![CDATA[Repository.OrderBy(r => r.]]><xsl:value-of select="NameTK2"/><![CDATA[),
				p => p.]]><xsl:value-of select="TableTK4"/><![CDATA[, k => k.]]><xsl:value-of select="Table"/><![CDATA[);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------

		public IList<Tbl93Comment> ListTbl93CommentsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.]]><xsl:value-of select="TableTK1"/><![CDATA[, k => k.]]><xsl:value-of select="TableTK2"/><![CDATA[);
		}      ]]>
</xsl:when>
<xsl:when test="Table ='Tbl18Superclasses'">       <![CDATA[ 
		public IList<]]><xsl:value-of select="LinqModel"/><![CDATA[> List]]><xsl:value-of select="Table"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				]]><xsl:value-of select="Entitys"/><![CDATA[Repository.OrderBy(r => r.]]><xsl:value-of select="Name"/><![CDATA[ + r.Subregnum),
				p => p.]]><xsl:value-of select="TableTK1"/><![CDATA[, k => k.]]><xsl:value-of select="TableTK2"/><![CDATA[);
		}

		public IList<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return ]]><xsl:value-of select="EntitysTK1"/><![CDATA[Repository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id &&
				e.]]><xsl:value-of select="NameTK1"/><![CDATA[.Contains("#") == false,
				]]><xsl:value-of select="EntitysTK1"/><![CDATA[Repository.OrderBy(r => r.]]><xsl:value-of select="NameTK1"/><![CDATA[),
				p => p.]]><xsl:value-of select="TableTK3"/><![CDATA[, k => k.]]><xsl:value-of select="Table"/><![CDATA[);
		}

		public IList<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> List]]><xsl:value-of select="TableTK2"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return ]]><xsl:value-of select="EntitysTK2"/><![CDATA[Repository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id &&
				e.]]><xsl:value-of select="NameTK2"/><![CDATA[.Contains("#") == false,
				]]><xsl:value-of select="EntitysTK2"/><![CDATA[Repository.OrderBy(r => r.]]><xsl:value-of select="NameTK2"/><![CDATA[),
				p => p.]]><xsl:value-of select="TableTK4"/><![CDATA[, k => k.]]><xsl:value-of select="Table"/><![CDATA[);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------

		public IList<Tbl93Comment> ListTbl93CommentsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.]]><xsl:value-of select="TableTK1"/><![CDATA[, k => k.]]><xsl:value-of select="TableTK2"/><![CDATA[);
		}      ]]>
</xsl:when>
<xsl:when test="Table ='Tbl66Genusses'">       <![CDATA[ 
		public IList<]]><xsl:value-of select="LinqModel"/><![CDATA[> List]]><xsl:value-of select="Table"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				]]><xsl:value-of select="Entitys"/><![CDATA[Repository.OrderBy(r => r.]]><xsl:value-of select="Name"/><![CDATA[ + r.Subregnum),
				p => p.]]><xsl:value-of select="TableTK1"/><![CDATA[, k => k.]]><xsl:value-of select="TableTK2"/><![CDATA[);
		}

		public IList<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return ]]><xsl:value-of select="EntitysTK1"/><![CDATA[Repository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id &&
				e.]]><xsl:value-of select="NameTK1"/><![CDATA[.Contains("#") == false,
				]]><xsl:value-of select="EntitysTK1"/><![CDATA[Repository.OrderBy(r => r.]]><xsl:value-of select="NameTK1"/><![CDATA[),
				p => p.]]><xsl:value-of select="TableTK3"/><![CDATA[, k => k.]]><xsl:value-of select="Table"/><![CDATA[);
		}

		public IList<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> List]]><xsl:value-of select="TableTK2"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return ]]><xsl:value-of select="EntitysTK2"/><![CDATA[Repository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id &&
				e.]]><xsl:value-of select="NameTK2"/><![CDATA[.Contains("#") == false,
				]]><xsl:value-of select="EntitysTK2"/><![CDATA[Repository.OrderBy(r => r.]]><xsl:value-of select="NameTK2"/><![CDATA[),
				p => p.]]><xsl:value-of select="TableTK4"/><![CDATA[, k => k.]]><xsl:value-of select="Table"/><![CDATA[);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------

		public IList<Tbl93Comment> ListTbl93CommentsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.]]><xsl:value-of select="TableTK1"/><![CDATA[, k => k.]]><xsl:value-of select="TableTK2"/><![CDATA[);
		}      ]]>
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">       <![CDATA[ 
		public IList<]]><xsl:value-of select="LinqModel"/><![CDATA[> List]]><xsl:value-of select="Table"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				]]><xsl:value-of select="Entitys"/><![CDATA[Repository.OrderBy(r => r.]]><xsl:value-of select="Name"/><![CDATA[ + r.Subregnum),
				p => p.]]><xsl:value-of select="TableTK1"/><![CDATA[, k => k.]]><xsl:value-of select="TableTK2"/><![CDATA[);
		}

		public IList<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return ]]><xsl:value-of select="EntitysTK1"/><![CDATA[Repository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id &&
				e.]]><xsl:value-of select="NameTK1"/><![CDATA[.Contains("#") == false,
				]]><xsl:value-of select="EntitysTK1"/><![CDATA[Repository.OrderBy(r => r.]]><xsl:value-of select="NameTK1"/><![CDATA[),
				p => p.]]><xsl:value-of select="TableTK2"/><![CDATA[, k => k.]]><xsl:value-of select="Table"/><![CDATA[);
		}

		public IList<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[> List]]><xsl:value-of select="TableTK3"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return ]]><xsl:value-of select="EntitysTK3"/><![CDATA[Repository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id &&
				e.]]><xsl:value-of select="NameTK3"/><![CDATA[.Contains("#") == false,
				]]><xsl:value-of select="EntitysTK3"/><![CDATA[Repository.OrderBy(r => r.]]><xsl:value-of select="NameTK3"/><![CDATA[),
				p => p.]]><xsl:value-of select="TableTK4"/><![CDATA[, k => k.]]><xsl:value-of select="Table"/><![CDATA[);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------

		public IList<Tbl93Comment> ListTbl93CommentsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.]]><xsl:value-of select="TableTK1"/><![CDATA[, k => k.]]><xsl:value-of select="TableTK2"/><![CDATA[);
		}      ]]>
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">       <![CDATA[ 
		public IList<]]><xsl:value-of select="LinqModel"/><![CDATA[> List]]><xsl:value-of select="Table"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				]]><xsl:value-of select="Entitys"/><![CDATA[Repository.OrderBy(r => r.]]><xsl:value-of select="Name"/><![CDATA[ + r.Subregnum),
				p => p.]]><xsl:value-of select="TableTK1"/><![CDATA[, k => k.]]><xsl:value-of select="TableTK2"/><![CDATA[);
		}

		public IList<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return ]]><xsl:value-of select="EntitysTK1"/><![CDATA[Repository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id &&
				e.]]><xsl:value-of select="NameTK1"/><![CDATA[.Contains("#") == false,
				]]><xsl:value-of select="EntitysTK1"/><![CDATA[Repository.OrderBy(r => r.]]><xsl:value-of select="NameTK1"/><![CDATA[),
				p => p.]]><xsl:value-of select="TableTK2"/><![CDATA[, k => k.]]><xsl:value-of select="Table"/><![CDATA[);
		}

		public IList<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[> List]]><xsl:value-of select="TableTK3"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return ]]><xsl:value-of select="EntitysTK3"/><![CDATA[Repository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id &&
				e.]]><xsl:value-of select="NameTK3"/><![CDATA[.Contains("#") == false,
				]]><xsl:value-of select="EntitysTK3"/><![CDATA[Repository.OrderBy(r => r.]]><xsl:value-of select="NameTK3"/><![CDATA[),
				p => p.]]><xsl:value-of select="TableTK4"/><![CDATA[, k => k.]]><xsl:value-of select="Table"/><![CDATA[);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------

		public IList<Tbl93Comment> ListTbl93CommentsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.]]><xsl:value-of select="TableTK1"/><![CDATA[, k => k.]]><xsl:value-of select="TableTK2"/><![CDATA[);
		}      ]]>
</xsl:when>
<xsl:otherwise>     <![CDATA[      
		public IList<]]><xsl:value-of select="LinqModel"/><![CDATA[> List]]><xsl:value-of select="Table"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				]]><xsl:value-of select="Entitys"/><![CDATA[Repository.OrderBy(r => r.]]><xsl:value-of select="Name"/><![CDATA[ + r.Subregnum),
				p => p.]]><xsl:value-of select="TableTK1"/><![CDATA[, k => k.]]><xsl:value-of select="TableTK2"/><![CDATA[);
		}

		public IList<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[IdAndHash(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return ]]><xsl:value-of select="EntitysTK1"/><![CDATA[Repository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id &&
				e.]]><xsl:value-of select="NameTK1"/><![CDATA[.Contains("#") == false,
				]]><xsl:value-of select="EntitysTK1"/><![CDATA[Repository.OrderBy(r => r.]]><xsl:value-of select="NameTK1"/><![CDATA[),
				p => p.]]><xsl:value-of select="TableTK3"/><![CDATA[, k => k.]]><xsl:value-of select="Table"/><![CDATA[);
		}

	               //------------------------------------------------

		public IList<Tbl90Reference> ListTbl90AuthorsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90SourcesBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90ExpertsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

		public IList<Tbl90Reference> ListTbl90RefAuthorsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefAuthorID == e.Tbl90RefAuthors.RefAuthorID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefAuthors.RefAuthorName + r.Tbl90RefAuthors.ArticelTitle + r.Tbl90RefAuthors.BookName + r.Tbl90RefAuthors.Page1 + r.Tbl90RefAuthors.Publisher),
				p => p.Tbl90RefAuthors);
		}

		public IList<Tbl90Reference> ListTbl90RefSourcesBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefSourceID == e.Tbl90RefSources.RefSourceID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefSources.RefSourceName + r.Tbl90RefSources.SourceYear),
				p => p.Tbl90RefSources);
		}

		public IList<Tbl90Reference> ListTbl90RefExpertsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl90ReferencesRepository.ListWhereOrderByInclude(
				e => e.RefExpertID == e.Tbl90RefExperts.RefExpertID && e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl90ReferencesRepository.OrderBy(r => r.Tbl90RefExperts.RefExpertName),
				p => p.Tbl90RefExperts);
		}

	               //------------------------------------------------
 
		public IList<Tbl93Comment> ListTbl93CommentsBy]]><xsl:value-of select="Basis"/><![CDATA[Id(int ]]><xsl:value-of select="BasisSm"/><![CDATA[Id)
		{
			return _tbl93CommentsRepository.ListWhereOrderByInclude(
				e => e.]]><xsl:value-of select="ID"/><![CDATA[ == ]]><xsl:value-of select="BasisSm"/><![CDATA[Id,
				_tbl93CommentsRepository.OrderBy(r => r.Info),
				p => p.]]><xsl:value-of select="TableTK1"/><![CDATA[);
		}      ]]>
</xsl:otherwise>    
</xsl:choose>  

       <![CDATA[  
}]]>   

</xsl:template>
</xsl:stylesheet>










