<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[using System.Linq; 
using ]]><xsl:value-of select="Namespace"/><![CDATA[.WpfUi.Interfaces;
using ]]><xsl:value-of select="Namespace"/>.WpfUi.Model; <![CDATA[   ]]>

<![CDATA[// <!-- Repository Skriptdatum: ]]> <xsl:value-of select="DateTime"/>  <![CDATA[  -->  

namespace ]]><xsl:value-of select="Namespace"/>.WpfUi.Repositories <![CDATA[     {  
    public class ]]><xsl:value-of select="Table"/><![CDATA[Repository : I]]><xsl:value-of select="Table"/><![CDATA[Repository    {
        private readonly ]]><xsl:value-of select="RegisterContext"/><![CDATA[ _entities = new ]]><xsl:value-of select="RegisterContext"/><![CDATA[();

        public IQueryable<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[     {         
            get { return _entities.]]><xsl:value-of select="Table"/><![CDATA[; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<]]><xsl:value-of select="LinqModel"/><![CDATA[> FindAll()    {         
            return _entities.]]><xsl:value-of select="Table"/><![CDATA[; 
        }   ]]>
<xsl:choose>
<xsl:when test="Table ='AspnetMemberships'">
   <![CDATA[      public IQueryable<]]><xsl:value-of select="LinqModel"/><![CDATA[> FindAllSort()    {
            return from d in _entities.]]><xsl:value-of select="Table"/> <![CDATA[
                   orderby d.Password
                   select d;
        }     ]]>                                                                                                                                                              
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">
   <![CDATA[      public IQueryable<]]><xsl:value-of select="LinqModel"/><![CDATA[> FindAllSort()    {
            return from d in _entities.]]><xsl:value-of select="Table"/> <![CDATA[
                   orderby d.]]><xsl:value-of select="Name"/><![CDATA[, d.Subregnum
                   select d;
        }     ]]>                                                                                                                                                              
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">
   <![CDATA[      public IQueryable<]]><xsl:value-of select="LinqModel"/><![CDATA[> FindAllSort()    {
            return from d in _entities.]]><xsl:value-of select="Table"/><![CDATA[
                   orderby d.]]><xsl:value-of select="Name"/><![CDATA[, d.Subspeciesgroup
                   select d;
        }     ]]>                                                                                                                                                              
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">
   <![CDATA[      public IQueryable<]]><xsl:value-of select="LinqModel"/><![CDATA[> FindAllSort()    {
            return from d in _entities.]]><xsl:value-of select="Table"/><![CDATA[
                   orderby d.]]><xsl:value-of select="Name"/><![CDATA[, d.Subspecies, d.Divers
                   select d;
        }     ]]>                                                                                                                                                              
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">
   <![CDATA[      public IQueryable<]]><xsl:value-of select="LinqModel"/><![CDATA[> FindAllSort()    {
            return from d in _entities.]]><xsl:value-of select="Table"/><![CDATA[
                   orderby d.]]><xsl:value-of select="Name"/><![CDATA[, d.Subspecies, d.Divers
                   select d;
        }     ]]>                                                                                                                                                              
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">
   <![CDATA[      public IQueryable<]]><xsl:value-of select="LinqModel"/><![CDATA[> FindAllSort()    {
            return from d in _entities.]]><xsl:value-of select="Table"/><![CDATA[
                   orderby d.FiSpeciesID
                   select d;
        }     ]]>                                                                                                                                                              
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">
   <![CDATA[      public IQueryable<]]><xsl:value-of select="LinqModel"/><![CDATA[> FindAllSort()    {
            return from d in _entities.]]><xsl:value-of select="Table"/><![CDATA[
                   orderby d.FiSpeciesID
                   select d;
        }     ]]>                                                                                                                                                              
</xsl:when>
<xsl:when test="Table ='Tbl90References'">
   <![CDATA[      public IQueryable<]]><xsl:value-of select="LinqModel"/><![CDATA[> FindAllSort()    {
            return from d in _entities.]]><xsl:value-of select="Table"/><![CDATA[
                   orderby d.FiSpeciesID
                   select d;
        }     ]]>                                                                                                                                                              
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">
   <![CDATA[      public IQueryable<]]><xsl:value-of select="LinqModel"/><![CDATA[> FindAllSort()    {
            return from d in _entities.]]><xsl:value-of select="Table"/><![CDATA[
                   orderby d.FiSpeciesID
                   select d;
        }     ]]>                                                                                                                                                              
</xsl:when>
<xsl:otherwise>
      <![CDATA[      public IQueryable<]]><xsl:value-of select="LinqModel"/><![CDATA[> FindAllSort()    {
            return from d in _entities.]]><xsl:value-of select="Table"/><![CDATA[
                   orderby d.]]><xsl:value-of select="Name"/><![CDATA[
                   select d;
        }     ]]>                                                                                                                                                              
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='GUID++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='AspnetApplications'">      
  <![CDATA[        public ]]><xsl:value-of select="LinqModel"/><![CDATA[ Get(Guid id)        {
            return _entities.]]><xsl:value-of select="Table"/><![CDATA[.FirstOrDefault(d => d.]]><xsl:value-of select="ID"/><![CDATA[ == id);
        }          ]]>
</xsl:when>
<xsl:when test="Table ='AspnetMemberships'">      
  <![CDATA[        public ]]><xsl:value-of select="LinqModel"/><![CDATA[ Get(Guid id)        {
            return _entities.]]><xsl:value-of select="Table"/><![CDATA[.FirstOrDefault(d => d.]]><xsl:value-of select="ID"/><![CDATA[ == id);
        }          ]]>
</xsl:when>
<xsl:when test="Table ='AspnetUsers'">      
  <![CDATA[        public ]]><xsl:value-of select="LinqModel"/><![CDATA[ Get(Guid id)        {
            return _entities.]]><xsl:value-of select="Table"/><![CDATA[.FirstOrDefault(d => d.]]><xsl:value-of select="ID"/><![CDATA[ == id);
        }          ]]>
</xsl:when>
<xsl:otherwise>  
  <![CDATA[        public ]]><xsl:value-of select="LinqModel"/><![CDATA[ Get(int id)        {
            return _entities.]]><xsl:value-of select="Table"/><![CDATA[.FirstOrDefault(d => d.]]><xsl:value-of select="ID"/><![CDATA[ == id);
        }          ]]>
</xsl:otherwise>    
</xsl:choose>        
<![CDATA[
         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(]]><xsl:value-of select="EntityAbl"/><![CDATA[)    {
            _entities.]]><xsl:value-of select="Table"/><![CDATA[.Add(]]><xsl:value-of select="Entity"/><![CDATA[);           
        }

        public void Delete(]]><xsl:value-of select="EntityAbl"/><![CDATA[)    {
            _entities.]]><xsl:value-of select="Table"/><![CDATA[.Remove(]]><xsl:value-of select="Entity"/><![CDATA[);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   ]]>
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
<xsl:if test="Table='TblCounters'">
  <![CDATA[      /// <summary>
        /// Counter für alle Tabellen
        /// </summary>
        /// <returns></returns>
        public int Counter()  {
            var count = (from p in Entities.]]><xsl:value-of select="Table"/><![CDATA[
                         select p).FirstOrDefault();
            count.Zahl = count.Zahl + 1;
            var iCount = Convert.ToInt32(count.Zahl);
            iCount = iCount + 1;
      //      Add(count);
            Save(); //save new number into tblCounters
            
            return iCount;
        }        ]]>   
</xsl:if> 
  <![CDATA[  }
}]]>   

</xsl:template>
</xsl:stylesheet>









