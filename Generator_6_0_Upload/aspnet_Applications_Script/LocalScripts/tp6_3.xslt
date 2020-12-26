<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Atis.WpfUi.Model;
using Atis.WpfUi.Repositories;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

//    ]]><xsl:value-of select="LinqModel"/><![CDATA[ViewModel Skriptdatum: ]]> <xsl:value-of select="DateTime"/>  <![CDATA[  

namespace ]]><xsl:value-of select="Namespace"/><![CDATA[.WpfUi.ViewModel
{  ]]> 

<xsl:choose>
<xsl:when test="Table ='++++++Abgeleitet von++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">
   <![CDATA[ 
    public class ]]><xsl:value-of select="Table"/><![CDATA[ViewModel : ViewModelBase                     
    {     ]]>
</xsl:when>  
<xsl:otherwise>
   <![CDATA[ 
    public class ]]><xsl:value-of select="Table"/><![CDATA[ViewModel : ]]><xsl:value-of select="TableFK1"/><![CDATA[ViewModel                     
    {     ]]>
</xsl:otherwise>    
</xsl:choose> 
   
<xsl:choose>
<xsl:when test="Table ='Data Members Top+++++++++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl03Regnums'">       <![CDATA[ 
        #region "Private Data Members"

        protected readonly ]]><xsl:value-of select="Table"/><![CDATA[Repository ]]><xsl:value-of select="Table"/><![CDATA[Repository = new ]]><xsl:value-of select="Table"/><![CDATA[Repository();  ]]> 
  <xsl:if test="TableFK1 !='NULL'">       <![CDATA[ 
        protected readonly ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository();  ]]> 
  </xsl:if> 
  <xsl:if test="TableTK1 !='NULL'">       <![CDATA[  
        protected readonly ]]><xsl:value-of select="TableTK1"/><![CDATA[Repository ]]><xsl:value-of select="TableTK1"/><![CDATA[Repository = new ]]><xsl:value-of select="TableTK1"/><![CDATA[Repository();  ]]> 
  </xsl:if> 
  <xsl:if test="TableTK2 !='NULL'">       <![CDATA[  
        protected readonly ]]><xsl:value-of select="TableTK2"/><![CDATA[Repository ]]><xsl:value-of select="TableTK2"/><![CDATA[Repository = new ]]><xsl:value-of select="TableTK2"/><![CDATA[Repository();  ]]> 
  </xsl:if> 
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">       <![CDATA[ 
        #region "Private Data Members"   ]]>
  <xsl:if test="TableFK2 !='NULL'">       <![CDATA[ 
        protected readonly ]]><xsl:value-of select="TableFK2"/><![CDATA[Repository ]]><xsl:value-of select="TableFK2"/><![CDATA[Repository = new ]]><xsl:value-of select="TableFK2"/><![CDATA[Repository();  ]]> 
  </xsl:if> 
  <xsl:if test="TableTK1 !='NULL'">       <![CDATA[  
        protected readonly ]]><xsl:value-of select="TableTK1"/><![CDATA[Repository ]]><xsl:value-of select="TableTK1"/><![CDATA[Repository = new ]]><xsl:value-of select="TableTK1"/><![CDATA[Repository();  ]]> 
  </xsl:if> 
  <xsl:if test="TableTK2 !='NULL'">       <![CDATA[  
        protected readonly ]]><xsl:value-of select="TableTK2"/><![CDATA[Repository ]]><xsl:value-of select="TableTK2"/><![CDATA[Repository = new ]]><xsl:value-of select="TableTK2"/><![CDATA[Repository();  ]]> 
  </xsl:if> 
</xsl:when>  
<xsl:otherwise>
  <xsl:if test="TableTK1 !='NULL'">       <![CDATA[  
      #region "Private Data Members"

        protected readonly ]]><xsl:value-of select="TableTK1"/><![CDATA[Repository ]]><xsl:value-of select="TableTK1"/><![CDATA[Repository = new ]]><xsl:value-of select="TableTK1"/><![CDATA[Repository();  ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Data Members Top+++++++++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl03Regnums'">   <![CDATA[ 
        protected readonly Tbl90ReferencesRepository Tbl90ReferencesRepository = new Tbl90ReferencesRepository();
        protected readonly Tbl90RefAuthorsRepository Tbl90RefAuthorsRepository = new Tbl90RefAuthorsRepository();
        protected readonly Tbl90RefSourcesRepository Tbl90RefSourcesRepository = new Tbl90RefSourcesRepository();
        protected readonly Tbl90RefExpertsRepository Tbl90RefExpertsRepository = new Tbl90RefExpertsRepository();
        protected readonly Tbl93CommentsRepository Tbl93CommentsRepository = new Tbl93CommentsRepository();
        protected readonly TblCountersRepository TblCountersRepository = new TblCountersRepository();

        #endregion "Private Data Members"         ]]>   
</xsl:when>  
<xsl:otherwise>       <![CDATA[ 
          #endregion "Private Data Members"         ]]>   
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Constructor+++++++++++++++'">
</xsl:when> 
<xsl:otherwise>   <![CDATA[ 
        #region "Constructor"

        public ]]><xsl:value-of select="Table"/><![CDATA[ViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
            }
        }

        #endregion "Constructor"        ]]>   
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands Basic  Top+++++++++++++++'">
</xsl:when>  
<xsl:otherwise>      <![CDATA[ 
        #region "Public Commands Basic ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private RelayCommand _get]]><xsl:value-of select="Basis"/><![CDATA[ByNameCommand;
        public new ICommand Get]]><xsl:value-of select="Basis"/><![CDATA[ByNameCommand
        {
            get { return _get]]><xsl:value-of select="Basis"/><![CDATA[ByNameCommand ?? (_get]]><xsl:value-of select="Basis"/><![CDATA[ByNameCommand = new RelayCommand(Get]]><xsl:value-of select="Basis"/><![CDATA[ByName)); }
        }

        private void Get]]><xsl:value-of select="Basis"/><![CDATA[ByName()
        { ]]>  
</xsl:otherwise>    
</xsl:choose>    

<xsl:choose>
<xsl:when test="Table ='Public Commands Basic  Top+++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">  <![CDATA[ 
            Tbl03RegnumsList =
                 new ObservableCollection<Tbl03Regnum>((from regnum in Tbl03RegnumsRepository.Tbl03Regnums
                                                        where regnum.RegnumName.StartsWith(SearchRegnumName)
                                                        orderby regnum.RegnumName, regnum.Subregnum
                                                        select regnum));

            Tbl03RegnumsAllList =
                 new ObservableCollection<Tbl03Regnum>((from reg in Tbl03RegnumsRepository.Tbl03Regnums
                                                        orderby reg.RegnumName, reg.Subregnum
                                                        select reg));
 ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl06Phylums'">  <![CDATA[ 
            ]]><xsl:value-of select="Table"/><![CDATA[List =
                 new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>((from x in ]]><xsl:value-of select="Table"/><![CDATA[Repository.]]><xsl:value-of select="Table"/><![CDATA[
                                                        where x.]]><xsl:value-of select="Name"/><![CDATA[.StartsWith(Search]]><xsl:value-of select="Name"/><![CDATA[)
                                                        orderby x.]]><xsl:value-of select="Name"/><![CDATA[
                                                        select x));

            ]]><xsl:value-of select="Table"/><![CDATA[AllList =
                 new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>((from y in ]]><xsl:value-of select="Table"/><![CDATA[Repository.]]><xsl:value-of select="Table"/><![CDATA[
                                                        orderby y.]]><xsl:value-of select="Name"/><![CDATA[
                                                        select y));

            ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList =
                 new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>((from z in ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[
                                                        orderby z.]]><xsl:value-of select="NameFK1"/><![CDATA[, z.Subregnum
                                                        select z));
 ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl09Divisions'">  <![CDATA[ 
            ]]><xsl:value-of select="Table"/><![CDATA[List =
                 new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>((from x in ]]><xsl:value-of select="Table"/><![CDATA[Repository.]]><xsl:value-of select="Table"/><![CDATA[
                                                        where x.]]><xsl:value-of select="Name"/><![CDATA[.StartsWith(Search]]><xsl:value-of select="Name"/><![CDATA[)
                                                        orderby x.]]><xsl:value-of select="Name"/><![CDATA[
                                                        select x));

            ]]><xsl:value-of select="Table"/><![CDATA[AllList =
                 new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>((from y in ]]><xsl:value-of select="Table"/><![CDATA[Repository.]]><xsl:value-of select="Table"/><![CDATA[
                                                        orderby y.]]><xsl:value-of select="Name"/><![CDATA[
                                                        select y));

            ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList =
                 new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>((from z in ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[
                                                        orderby z.]]><xsl:value-of select="NameFK1"/><![CDATA[, z.Subregnum
                                                        select z));
 ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl12Subphylums'">  <![CDATA[ 
            ]]><xsl:value-of select="Table"/><![CDATA[List =
                 new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>((from x in ]]><xsl:value-of select="Table"/><![CDATA[Repository.]]><xsl:value-of select="Table"/><![CDATA[
                                                        where x.]]><xsl:value-of select="Name"/><![CDATA[.StartsWith(Search]]><xsl:value-of select="Name"/><![CDATA[)
                                                        orderby x.]]><xsl:value-of select="Name"/><![CDATA[
                                                        select x));

            ]]><xsl:value-of select="Table"/><![CDATA[AllList =
                 new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>((from y in ]]><xsl:value-of select="Table"/><![CDATA[Repository.]]><xsl:value-of select="Table"/><![CDATA[
                                                        orderby y.]]><xsl:value-of select="Name"/><![CDATA[
                                                        select y));

            ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList =
                 new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>((from z in ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[
                                                        orderby z.]]><xsl:value-of select="NameFK1"/><![CDATA[
                                                        select z));

            ]]><xsl:value-of select="TableBK1"/><![CDATA[AllList =
                 new ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[>((from z in ]]><xsl:value-of select="TableBK1"/><![CDATA[Repository.]]><xsl:value-of select="TableBK1"/><![CDATA[
                                                        orderby z.]]><xsl:value-of select="NameBK1"/><![CDATA[, z.Subregnum
                                                        select z));
 ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl15Subdivisions'">  <![CDATA[ 
            ]]><xsl:value-of select="Table"/><![CDATA[List =
                 new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>((from x in ]]><xsl:value-of select="Table"/><![CDATA[Repository.]]><xsl:value-of select="Table"/><![CDATA[
                                                        where x.]]><xsl:value-of select="Name"/><![CDATA[.StartsWith(Search]]><xsl:value-of select="Name"/><![CDATA[)
                                                        orderby x.]]><xsl:value-of select="Name"/><![CDATA[
                                                        select x));

            ]]><xsl:value-of select="Table"/><![CDATA[AllList =
                 new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>((from y in ]]><xsl:value-of select="Table"/><![CDATA[Repository.]]><xsl:value-of select="Table"/><![CDATA[
                                                        orderby y.]]><xsl:value-of select="Name"/><![CDATA[
                                                        select y));

            ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList =
                 new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>((from z in ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[
                                                        orderby z.]]><xsl:value-of select="NameFK1"/><![CDATA[
                                                        select z));

            ]]><xsl:value-of select="TableBK1"/><![CDATA[AllList =
                 new ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[>((from z in ]]><xsl:value-of select="TableBK1"/><![CDATA[Repository.]]><xsl:value-of select="TableBK1"/><![CDATA[
                                                        orderby z.]]><xsl:value-of select="NameBK1"/><![CDATA[, z.Subregnum
                                                        select z));
 ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">  <![CDATA[ 
            ]]><xsl:value-of select="Table"/><![CDATA[List =
                 new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>((from x in ]]><xsl:value-of select="Table"/><![CDATA[Repository.]]><xsl:value-of select="Table"/><![CDATA[
                                                        where x.]]><xsl:value-of select="Name"/><![CDATA[.StartsWith(Search]]><xsl:value-of select="Name"/><![CDATA[)
                                                        orderby x.]]><xsl:value-of select="Name"/><![CDATA[
                                                        select x));

            ]]><xsl:value-of select="Table"/><![CDATA[AllList =
                 new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>((from y in ]]><xsl:value-of select="Table"/><![CDATA[Repository.]]><xsl:value-of select="Table"/><![CDATA[
                                                        orderby y.]]><xsl:value-of select="Name"/><![CDATA[
                                                        select y));

            ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList =
                 new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>((from z in ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[
                                                        orderby z.]]><xsl:value-of select="NameFK1"/><![CDATA[
                                                        select z));

            ]]><xsl:value-of select="TableFK2"/><![CDATA[AllList =
                 new ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[>((from z in ]]><xsl:value-of select="TableFK2"/><![CDATA[Repository.]]><xsl:value-of select="TableFK2"/><![CDATA[
                                                        orderby z.]]><xsl:value-of select="NameFK2"/><![CDATA[
                                                        select z));

            ]]><xsl:value-of select="TableBK1"/><![CDATA[AllList =
                 new ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[>((from z in ]]><xsl:value-of select="TableBK1"/><![CDATA[Repository.]]><xsl:value-of select="TableBK1"/><![CDATA[
                                                        orderby z.]]><xsl:value-of select="NameBK1"/><![CDATA[
                                                        select z));

            ]]><xsl:value-of select="TableBK2"/><![CDATA[AllList =
                 new ObservableCollection<]]><xsl:value-of select="LinqModelBK2"/><![CDATA[>((from z in ]]><xsl:value-of select="TableBK2"/><![CDATA[Repository.]]><xsl:value-of select="TableBK2"/><![CDATA[
                                                        orderby z.]]><xsl:value-of select="NameBK2"/><![CDATA[
                                                        select z));
 ]]> 
</xsl:when>  
<xsl:otherwise>     <![CDATA[ 
            ]]><xsl:value-of select="Table"/><![CDATA[List =
                 new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>((from x in ]]><xsl:value-of select="Table"/><![CDATA[Repository.]]><xsl:value-of select="Table"/><![CDATA[
                                                        where x.]]><xsl:value-of select="Name"/><![CDATA[.StartsWith(Search]]><xsl:value-of select="Name"/><![CDATA[)
                                                        orderby x.]]><xsl:value-of select="Name"/><![CDATA[
                                                        select x));

            ]]><xsl:value-of select="Table"/><![CDATA[AllList =
                 new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>((from y in ]]><xsl:value-of select="Table"/><![CDATA[Repository.]]><xsl:value-of select="Table"/><![CDATA[
                                                        orderby y.]]><xsl:value-of select="Name"/><![CDATA[
                                                        select y));

            ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList =
                 new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>((from z in ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[
                                                        orderby z.]]><xsl:value-of select="NameFK1"/><![CDATA[
                                                        select z));

           ]]>   
  <xsl:if test="TableBK1 !='NULL'">      <![CDATA[ 
            ]]><xsl:value-of select="TableBK1"/><![CDATA[AllList =
                 new ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[>((from z in ]]><xsl:value-of select="TableBK1"/><![CDATA[Repository.]]><xsl:value-of select="TableBK1"/><![CDATA[
                                                        orderby z.]]><xsl:value-of select="NameBK1"/><![CDATA[
                                                        select z));
      ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose>    

<xsl:choose>
<xsl:when test="Table ='Public Commands Basic  Top+++++++++++++++'">
</xsl:when>  
<xsl:otherwise>      <![CDATA[ 
            Tbl90AuthorsAllList =
                 new ObservableCollection<Tbl90RefAuthor>((from auth in Tbl90RefAuthorsRepository.Tbl90RefAuthors
                                                        orderby auth.RefAuthorName, auth.BookName, auth.Page
                                                        select auth));

            Tbl90SourcesAllList =
                new ObservableCollection<Tbl90RefSource>((from sour in Tbl90RefSourcesRepository.Tbl90RefSources
                                                        orderby sour.RefSourceName
                                                        select sour));

            Tbl90ExpertsAllList =
                new ObservableCollection<Tbl90RefExpert>((from exp in Tbl90RefExpertsRepository.Tbl90RefExperts
                                                        orderby exp.RefExpertName
                                                        select exp));

            //All List to null
            Tbl93CommentsList = null;
            Tbl90RefExpertsList = null;
            Tbl90RefAuthorsList = null;
            Tbl90RefSourcesList = null;

 ]]> 
</xsl:otherwise>    
</xsl:choose>    

<xsl:choose>
<xsl:when test="Table ='Public Commands Basic ListView +++++++++++++++'">            
</xsl:when> 
<xsl:when test="Table ='Tbl03Regnums'">  
       <![CDATA[         Tbl06PhylumsList = null; ]]>                
       <![CDATA[         Tbl09DivisionsList = null;   ]]>                    
</xsl:when>   
<xsl:when test="Table ='Tbl18Superclasses'">  
       <![CDATA[         Tbl06PhylumsList = null; ]]>                
       <![CDATA[         Tbl09DivisionsList = null;   ]]>                    
       <![CDATA[         Tbl12SubphylumsList = null;   ]]>                    
       <![CDATA[         Tbl15SubdivisionsList = null;   ]]>                    
</xsl:when>   
<xsl:otherwise> 
  <xsl:if test="TableFK1 !='NULL'">
       <![CDATA[         ]]><xsl:value-of select="TableFK1"/><![CDATA[List = null;  ]]>                
  </xsl:if>     
  <xsl:if test="TableTK1 !='NULL'">
       <![CDATA[         ]]><xsl:value-of select="TableTK1"/><![CDATA[List = null;  ]]>   
             
  </xsl:if>     
</xsl:otherwise>    
</xsl:choose>    

<xsl:choose>
<xsl:when test="Table ='Public Commands Add Top+++++++++++++++'">            
</xsl:when> 
<xsl:when test="Table ='Tbl03Regnums'">  
    <![CDATA[ 
            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            if (]]><xsl:value-of select="Basiss"/><![CDATA[View != null)
                ]]><xsl:value-of select="Basiss"/><![CDATA[View.CurrentChanged += ]]><xsl:value-of select="Entity"/><![CDATA[View_CurrentChanged;                   
            RaisePropertyChanged("Current]]><xsl:value-of select="LinqModel"/><![CDATA[");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _add]]><xsl:value-of select="Basis"/><![CDATA[Command;
        public ICommand Add]]><xsl:value-of select="Basis"/><![CDATA[Command
        {
            get { return _add]]><xsl:value-of select="Basis"/><![CDATA[Command ?? (_add]]><xsl:value-of select="Basis"/><![CDATA[Command = new RelayCommand(Add]]><xsl:value-of select="Basis"/><![CDATA[)); }
        }

        private void Add]]><xsl:value-of select="Basis"/><![CDATA[()
        {
            if (]]><xsl:value-of select="Table"/><![CDATA[List == null)
                ]]><xsl:value-of select="Table"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>();
            ]]><xsl:value-of select="Table"/><![CDATA[List.Add(new ]]><xsl:value-of select="LinqModel"/><![CDATA[{ ]]><xsl:value-of select="Name"/><![CDATA[= "New " });
            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            if (]]><xsl:value-of select="Basiss"/><![CDATA[View != null)
                ]]><xsl:value-of select="Basiss"/><![CDATA[View.CurrentChanged += ]]><xsl:value-of select="Entity"/><![CDATA[View_CurrentChanged;
            RaisePropertyChanged("Current]]><xsl:value-of select="LinqModel"/><![CDATA[");
        }
        //---------------------------------------------------------------------------------------
 ]]> </xsl:when>    
<xsl:otherwise> 
    <![CDATA[ 
            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            if (]]><xsl:value-of select="Basiss"/><![CDATA[View != null)
                ]]><xsl:value-of select="Basiss"/><![CDATA[View.CurrentChanged += ]]><xsl:value-of select="Entity"/><![CDATA[View_CurrentChanged;                   
            RaisePropertyChanged("Current]]><xsl:value-of select="LinqModel"/><![CDATA[");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _add]]><xsl:value-of select="Basis"/><![CDATA[Command;
        public new ICommand Add]]><xsl:value-of select="Basis"/><![CDATA[Command
        {
            get { return _add]]><xsl:value-of select="Basis"/><![CDATA[Command ?? (_add]]><xsl:value-of select="Basis"/><![CDATA[Command = new RelayCommand(Add]]><xsl:value-of select="Basis"/><![CDATA[)); }
        }

        private void Add]]><xsl:value-of select="Basis"/><![CDATA[()
        {
            if (]]><xsl:value-of select="Table"/><![CDATA[List == null)
                ]]><xsl:value-of select="Table"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>();
            ]]><xsl:value-of select="Table"/><![CDATA[List.Add(new ]]><xsl:value-of select="LinqModel"/><![CDATA[{ ]]><xsl:value-of select="Name"/><![CDATA[= "New " });
            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            if (]]><xsl:value-of select="Basiss"/><![CDATA[View != null)
                ]]><xsl:value-of select="Basiss"/><![CDATA[View.CurrentChanged += ]]><xsl:value-of select="Entity"/><![CDATA[View_CurrentChanged;
            RaisePropertyChanged("Current]]><xsl:value-of select="LinqModel"/><![CDATA[");
        }
        //---------------------------------------------------------------------------------------
 ]]> 
</xsl:otherwise>    
</xsl:choose>    

<xsl:choose>
<xsl:when test="Table ='Public Commands Basic Delete and Save  Top+++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">  <![CDATA[ 
        private RelayCommand _deleteRegnumCommand;
        public ICommand DeleteRegnumCommand
        {
            get { return _deleteRegnumCommand ?? (_deleteRegnumCommand = new RelayCommand(DeleteRegnum)); }
        }

        private void DeleteRegnum()
        {
            try
            {
                var regnum = Tbl03RegnumsRepository.Tbl03Regnums.FirstOrDefault(x => x.RegnumID == CurrentTbl03Regnum.RegnumID);
                if (regnum != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl03RegnumsRepository.Delete(regnum);
                    Tbl03RegnumsRepository.Save();
                    MessageBox.Show(CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum + " was deleted successfully");
                    GetRegnumByName(); //Refresh
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum + " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveRegnumCommand;
        public ICommand SaveRegnumCommand
        {
            get { return _saveRegnumCommand ?? (_saveRegnumCommand = new RelayCommand(SaveRegnum)); }
        }

        private void SaveRegnum()
        {
            try
            {
                var regnum = Tbl03RegnumsRepository.Tbl03Regnums.FirstOrDefault(x => x.RegnumID == CurrentTbl03Regnum.RegnumID);
                if (CurrentTbl03Regnum == null)
                {
                    MessageBox.Show("regnum-subregnum was not found");
                }
                else
                {
                    if (CurrentTbl03Regnum.RegnumID != 0)
                    {
                        if (regnum != null) //update
                        {
                            regnum.Updater = Environment.UserName;
                            regnum.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl03RegnumsRepository.Add(new Tbl03Regnum()
                        {
                            RegnumName = CurrentTbl03Regnum.RegnumName,
                            Subregnum = CurrentTbl03Regnum.Subregnum,
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl03Regnum.Valid,
                            ValidYear = CurrentTbl03Regnum.ValidYear,
                            Synonym = CurrentTbl03Regnum.Synonym,
                            Author = CurrentTbl03Regnum.Author,
                            AuthorYear = CurrentTbl03Regnum.AuthorYear,
                            Info = CurrentTbl03Regnum.Info,
                            EngName = CurrentTbl03Regnum.EngName,
                            GerName = CurrentTbl03Regnum.GerName,
                            FraName = CurrentTbl03Regnum.FraName,
                            PorName = CurrentTbl03Regnum.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl03Regnum.Memo
                        });
                    }
                    {
                        Tbl03RegnumsRepository.Save();
                        MessageBox.Show(CurrentTbl03Regnum.RegnumName + " " + CurrentTbl03Regnum.Subregnum +
                                        " was successfully saved ");
                        GetRegnumByName();  //Refresh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
 ]]> 
</xsl:when>  
<xsl:otherwise>   <![CDATA[    
        private RelayCommand _delete]]><xsl:value-of select="Basis"/><![CDATA[Command;
        public new ICommand Delete]]><xsl:value-of select="Basis"/><![CDATA[Command
        {
            get { return _delete]]><xsl:value-of select="Basis"/><![CDATA[Command ?? (_delete]]><xsl:value-of select="Basis"/><![CDATA[Command = new RelayCommand(Delete]]><xsl:value-of select="Basis"/><![CDATA[)); }
        }

        private void Delete]]><xsl:value-of select="Basis"/><![CDATA[()
        {
            try
            {
                var ]]><xsl:value-of select="BasisSm"/><![CDATA[= ]]><xsl:value-of select="Table"/><![CDATA[Repository.]]><xsl:value-of select="Table"/><![CDATA[.FirstOrDefault(x => x.]]><xsl:value-of select="ID"/><![CDATA[== Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[);
                if (]]><xsl:value-of select="BasisSm"/><![CDATA[!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    ]]><xsl:value-of select="Table"/><![CDATA[Repository.Delete(]]><xsl:value-of select="BasisSm"/><![CDATA[);
                    ]]><xsl:value-of select="Table"/><![CDATA[Repository.Save();
                    MessageBox.Show(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[ + " was deleted successfully");
                    Get]]><xsl:value-of select="Basis"/><![CDATA[ByName(); //Refresh
                }
                else
                {
                    MessageBox.Show("Only " + Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------------------------
        private RelayCommand _save]]><xsl:value-of select="Basis"/><![CDATA[Command;
        public new ICommand Save]]><xsl:value-of select="Basis"/><![CDATA[Command
        {
            get { return _save]]><xsl:value-of select="Basis"/><![CDATA[Command ?? (_save]]><xsl:value-of select="Basis"/><![CDATA[Command = new RelayCommand(Save]]><xsl:value-of select="Basis"/><![CDATA[)); }
        }

        private void Save]]><xsl:value-of select="Basis"/><![CDATA[()
        {
            try
            {
                var ]]><xsl:value-of select="BasisSm"/><![CDATA[= ]]><xsl:value-of select="Table"/><![CDATA[Repository.]]><xsl:value-of select="Table"/><![CDATA[.FirstOrDefault(x => x.]]><xsl:value-of select="ID"/><![CDATA[== Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[);
                if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ == null)
                {
                    MessageBox.Show("]]><xsl:value-of select="BasisSm"/><![CDATA[ was not found");
                }
                else
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[!= 0)
                    {
                        if (]]><xsl:value-of select="BasisSm"/><![CDATA[!= null) //update
                        {
                            ]]><xsl:value-of select="BasisSm"/><![CDATA[.Updater = Environment.UserName;
                            ]]><xsl:value-of select="BasisSm"/><![CDATA[.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        ]]><xsl:value-of select="Table"/><![CDATA[Repository.Add(new ]]><xsl:value-of select="LinqModel"/><![CDATA[
                        {
                            ]]><xsl:value-of select="IDFK1"/><![CDATA[= Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="IDFK1"/><![CDATA[,              
                            ]]><xsl:value-of select="Name"/><![CDATA[= Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.Valid,
                            ValidYear = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.ValidYear,
                            Synonym = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.Synonym,
                            Author = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.Author,
                            AuthorYear = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.AuthorYear,
                            Info = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.Info,
                            EngName = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.EngName,
                            GerName = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.GerName,
                            FraName = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.FraName,
                            PorName = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.Memo
                        });
                    }
                    {
                        ]]><xsl:value-of select="Table"/><![CDATA[Repository.Save();
                        MessageBox.Show(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[+  " was successfully saved ");
                        Get]]><xsl:value-of select="Basis"/><![CDATA[ByName();  //Refresh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
 ]]> 
</xsl:otherwise>    
</xsl:choose>    

<xsl:choose>
<xsl:when test="Table ='Public Commands Connect   FK1  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">    
</xsl:when>  
<xsl:when test="Table ='Tbl06Phylums'">    
       <![CDATA[ 
        #region "Public Commands Connect <== ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["                 

        private RelayCommand _get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByNameCommand;
        public new ICommand Get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByNameCommand
        {
            get { return _get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByNameCommand ?? (_get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByNameCommand = new RelayCommand(Get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByName)); }
        }

        private void Get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByName()
        {
            ]]><xsl:value-of select="TableFK1"/><![CDATA[List =
                new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>((from ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[ in ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[
                                                       where ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[.StartsWith(Search]]><xsl:value-of select="BasisFK1"/><![CDATA[Name)
                                                       orderby ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[, ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[.Subregnum
                                                       select ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[));

            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK1"/><![CDATA[List);
            if (]]><xsl:value-of select="BasissFK1"/><![CDATA[View != null)
                ]]><xsl:value-of select="BasissFK1"/><![CDATA[View.CurrentChanged += ]]><xsl:value-of select="EntityFK1"/><![CDATA[View_CurrentChanged;                   
            RaisePropertyChanged("Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _add]]><xsl:value-of select="BasisFK1"/><![CDATA[Command;
        public new ICommand Add]]><xsl:value-of select="BasisFK1"/><![CDATA[Command
        {
            get { return _add]]><xsl:value-of select="BasisFK1"/><![CDATA[Command ?? (_add]]><xsl:value-of select="BasisFK1"/><![CDATA[Command = new RelayCommand(Add]]><xsl:value-of select="BasisFK1"/><![CDATA[)); }
        }

        private void Add]]><xsl:value-of select="BasisFK1"/><![CDATA[()
        {
            if (]]><xsl:value-of select="TableFK1"/><![CDATA[List == null)
                ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>();
            ]]><xsl:value-of select="TableFK1"/><![CDATA[List.Add(new ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[{ ]]><xsl:value-of select="NameFK1"/><![CDATA[= "New " });                   
            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK1"/><![CDATA[List);
            if (]]><xsl:value-of select="BasissFK1"/><![CDATA[View != null)
                ]]><xsl:value-of select="BasissFK1"/><![CDATA[View.CurrentChanged += ]]><xsl:value-of select="EntityFK1"/><![CDATA[View_CurrentChanged;
            RaisePropertyChanged("Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[");
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _delete]]><xsl:value-of select="BasisFK1"/><![CDATA[Command;
        public new ICommand Delete]]><xsl:value-of select="BasisFK1"/><![CDATA[Command
        {
            get { return _delete]]><xsl:value-of select="BasisFK1"/><![CDATA[Command ?? (_delete]]><xsl:value-of select="BasisFK1"/><![CDATA[Command = new RelayCommand(Delete]]><xsl:value-of select="BasisFK1"/><![CDATA[)); }
        }

        private void Delete]]><xsl:value-of select="BasisFK1"/><![CDATA[()
        {
            try
            {
                var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[= ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[.FirstOrDefault(x => x.]]><xsl:value-of select="IDFK1"/><![CDATA[== Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="IDFK1"/><![CDATA[);
                if (]]><xsl:value-of select="BasisSmFK1"/><![CDATA[!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Subregnum, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.Delete(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[);
                    ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.Save();
                    MessageBox.Show(Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[+ " " + Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Subregnum + " was deleted successfully");
                    if (Search]]><xsl:value-of select="BasisFK1"/><![CDATA[Name == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        Get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[+ " " + Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Subregnum + " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _save]]><xsl:value-of select="BasisFK1"/><![CDATA[Command;   
        public new ICommand Save]]><xsl:value-of select="BasisFK1"/><![CDATA[Command
        {
            get { return _save]]><xsl:value-of select="BasisFK1"/><![CDATA[Command ?? (_save]]><xsl:value-of select="BasisFK1"/><![CDATA[Command = new RelayCommand(Save]]><xsl:value-of select="BasisFK1"/><![CDATA[)); }
        }

        private void Save]]><xsl:value-of select="BasisFK1"/><![CDATA[()
        {
            try
            {
                var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[= ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[.FirstOrDefault(x => x.]]><xsl:value-of select="IDFK1"/><![CDATA[== Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="IDFK1"/><![CDATA[);
                if (Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ == null)
                {
                    MessageBox.Show("regnum- subregnum was not found");
                }
                else
                {
                    if (Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="IDFK1"/><![CDATA[!= 0)
                    {
                        if (]]><xsl:value-of select="BasisSmFK1"/><![CDATA[!= null) //update
                        {
                            ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[.Updater = Environment.UserName;
                            ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.Add(new ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[()
                        {
                            ]]><xsl:value-of select="NameFK1"/><![CDATA[= Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[,              
                            Subregnum= Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Subregnum,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Valid,
                            ValidYear = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.ValidYear,
                            Synonym = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Synonym,
                            Author = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Author,
                            AuthorYear = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.AuthorYear,
                            Info = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Info,
                            EngName = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.EngName,
                            GerName = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.GerName,
                            FraName = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.FraName,
                            PorName = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Memo
                        });
                    }
                    {
                        ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.Save();
                        MessageBox.Show(Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[+ " " + Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Subregnum + " was successfully saved ");
                           if (Search]]><xsl:value-of select="NameFK1"/><![CDATA[ == null)
                         {
                             GetConnectedTablesById(); //refresh doubleClick                    
                         }
                         else
                         {
                             Get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByName(); //search
                         }                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
   ]]>                                       
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">    
       <![CDATA[ 
        #region "Public Commands Connect <== ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["                 

        private RelayCommand _get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByNameCommand;
        public new ICommand Get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByNameCommand
        {
            get { return _get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByNameCommand ?? (_get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByNameCommand = new RelayCommand(Get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByName)); }
        }

        private void Get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByName()
        {
            ]]><xsl:value-of select="TableFK1"/><![CDATA[List =
                new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>((from ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[ in ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[
                                                       where ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[.StartsWith(Search]]><xsl:value-of select="BasisFK1"/><![CDATA[Name)
                                                       orderby ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[, ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[.Subregnum
                                                       select ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[));

            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK1"/><![CDATA[List);
            if (]]><xsl:value-of select="BasissFK1"/><![CDATA[View != null)
                ]]><xsl:value-of select="BasissFK1"/><![CDATA[View.CurrentChanged += ]]><xsl:value-of select="EntityFK1"/><![CDATA[View_CurrentChanged;                   
            RaisePropertyChanged("Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _add]]><xsl:value-of select="BasisFK1"/><![CDATA[Command;
        public new ICommand Add]]><xsl:value-of select="BasisFK1"/><![CDATA[Command
        {
            get { return _add]]><xsl:value-of select="BasisFK1"/><![CDATA[Command ?? (_add]]><xsl:value-of select="BasisFK1"/><![CDATA[Command = new RelayCommand(Add]]><xsl:value-of select="BasisFK1"/><![CDATA[)); }
        }

        private void Add]]><xsl:value-of select="BasisFK1"/><![CDATA[()
        {
            if (]]><xsl:value-of select="TableFK1"/><![CDATA[List == null)
                ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>();
            ]]><xsl:value-of select="TableFK1"/><![CDATA[List.Add(new ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[{ ]]><xsl:value-of select="NameFK1"/><![CDATA[= "New " });                   
            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK1"/><![CDATA[List);
            if (]]><xsl:value-of select="BasissFK1"/><![CDATA[View != null)
                ]]><xsl:value-of select="BasissFK1"/><![CDATA[View.CurrentChanged += ]]><xsl:value-of select="EntityFK1"/><![CDATA[View_CurrentChanged;
            RaisePropertyChanged("Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[");
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _delete]]><xsl:value-of select="BasisFK1"/><![CDATA[Command;
        public new ICommand Delete]]><xsl:value-of select="BasisFK1"/><![CDATA[Command
        {
            get { return _delete]]><xsl:value-of select="BasisFK1"/><![CDATA[Command ?? (_delete]]><xsl:value-of select="BasisFK1"/><![CDATA[Command = new RelayCommand(Delete]]><xsl:value-of select="BasisFK1"/><![CDATA[)); }
        }

        private void Delete]]><xsl:value-of select="BasisFK1"/><![CDATA[()
        {
            try
            {
                var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[= ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[.FirstOrDefault(x => x.]]><xsl:value-of select="IDFK1"/><![CDATA[== Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="IDFK1"/><![CDATA[);
                if (]]><xsl:value-of select="BasisSmFK1"/><![CDATA[!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[ + " " + Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Subregnum, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.Delete(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[);
                    ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.Save();
                    MessageBox.Show(Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[+ " " + Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Subregnum + " was deleted successfully");
                    if (Search]]><xsl:value-of select="BasisFK1"/><![CDATA[Name == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        Get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[+ " " + Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Subregnum + " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _save]]><xsl:value-of select="BasisFK1"/><![CDATA[Command;   
        public new ICommand Save]]><xsl:value-of select="BasisFK1"/><![CDATA[Command
        {
            get { return _save]]><xsl:value-of select="BasisFK1"/><![CDATA[Command ?? (_save]]><xsl:value-of select="BasisFK1"/><![CDATA[Command = new RelayCommand(Save]]><xsl:value-of select="BasisFK1"/><![CDATA[)); }
        }

        private void Save]]><xsl:value-of select="BasisFK1"/><![CDATA[()
        {
            try
            {
                var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[= ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[.FirstOrDefault(x => x.]]><xsl:value-of select="IDFK1"/><![CDATA[== Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="IDFK1"/><![CDATA[);
                if (Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ == null)
                {
                    MessageBox.Show("regnum- subregnum was not found");
                }
                else
                {
                    if (Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="IDFK1"/><![CDATA[!= 0)
                    {
                        if (]]><xsl:value-of select="BasisSmFK1"/><![CDATA[!= null) //update
                        {
                            ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[.Updater = Environment.UserName;
                            ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.Add(new ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[()
                        {
                            ]]><xsl:value-of select="NameFK1"/><![CDATA[= Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[,              
                            Subregnum= Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Subregnum,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Valid,
                            ValidYear = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.ValidYear,
                            Synonym = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Synonym,
                            Author = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Author,
                            AuthorYear = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.AuthorYear,
                            Info = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Info,
                            EngName = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.EngName,
                            GerName = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.GerName,
                            FraName = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.FraName,
                            PorName = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Memo
                        });
                    }
                    {
                        ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.Save();
                        MessageBox.Show(Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[+ " " + Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Subregnum + " was successfully saved ");
                           if (Search]]><xsl:value-of select="NameFK1"/><![CDATA[ == null)
                         {
                             GetConnectedTablesById(); //refresh doubleClick                    
                         }
                         else
                         {
                             Get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByName(); //search
                         }                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
   ]]>                                       
</xsl:when>
<xsl:otherwise>  
  <xsl:if test="TableFK1 !='NULL'">
       <![CDATA[ 
        #region "Public Commands Connect <== ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["                 

        private RelayCommand _get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByNameCommand;
        public new ICommand Get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByNameCommand
        {
            get { return _get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByNameCommand ?? (_get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByNameCommand = new RelayCommand(Get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByName)); }
        }

        private void Get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByName()
        {
            ]]><xsl:value-of select="TableFK1"/><![CDATA[List =
                new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>((from ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[ in ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[
                                                       where ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[.StartsWith(Search]]><xsl:value-of select="BasisFK1"/><![CDATA[Name)
                                                       orderby ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[
                                                       select ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[));

            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK1"/><![CDATA[List);
            if (]]><xsl:value-of select="BasissFK1"/><![CDATA[View != null)
                ]]><xsl:value-of select="BasissFK1"/><![CDATA[View.CurrentChanged += ]]><xsl:value-of select="EntityFK1"/><![CDATA[View_CurrentChanged;                   
            RaisePropertyChanged("Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _add]]><xsl:value-of select="BasisFK1"/><![CDATA[Command;
        public new ICommand Add]]><xsl:value-of select="BasisFK1"/><![CDATA[Command
        {
            get { return _add]]><xsl:value-of select="BasisFK1"/><![CDATA[Command ?? (_add]]><xsl:value-of select="BasisFK1"/><![CDATA[Command = new RelayCommand(Add]]><xsl:value-of select="BasisFK1"/><![CDATA[)); }
        }

        private void Add]]><xsl:value-of select="BasisFK1"/><![CDATA[()
        {
            if (]]><xsl:value-of select="TableFK1"/><![CDATA[List == null)
                ]]><xsl:value-of select="TableFK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>();
            ]]><xsl:value-of select="TableFK1"/><![CDATA[List.Add(new ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[{ ]]><xsl:value-of select="NameFK1"/><![CDATA[= "New " });                   
            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK1"/><![CDATA[List);
            if (]]><xsl:value-of select="BasissFK1"/><![CDATA[View != null)
                ]]><xsl:value-of select="BasissFK1"/><![CDATA[View.CurrentChanged += ]]><xsl:value-of select="EntityFK1"/><![CDATA[View_CurrentChanged;
            RaisePropertyChanged("Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[");
        }
        //----------------------------------------------------------------------------------------------------------
        private RelayCommand _delete]]><xsl:value-of select="BasisFK1"/><![CDATA[Command;
        public ICommand ]]><xsl:value-of select="BasisFK1"/><![CDATA[PhylumCommand
        {
            get { return _delete]]><xsl:value-of select="BasisFK1"/><![CDATA[Command ?? (_delete]]><xsl:value-of select="BasisFK1"/><![CDATA[Command = new RelayCommand(Delete]]><xsl:value-of select="BasisFK1"/><![CDATA[)); }
        }

        private void Delete]]><xsl:value-of select="BasisFK1"/><![CDATA[()
        {
            try
            {
                var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[= ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[.FirstOrDefault(x => x.]]><xsl:value-of select="IDFK1"/><![CDATA[== Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="IDFK1"/><![CDATA[);
                if (]]><xsl:value-of select="BasisSmFK1"/><![CDATA[!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.Delete(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[);
                    ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.Save();
                    MessageBox.Show(Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[+ " was deleted successfully");
                    if (Search]]><xsl:value-of select="BasisFK1"/><![CDATA[Name == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        Get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _save]]><xsl:value-of select="BasisFK1"/><![CDATA[Command;   
        public new ICommand Save]]><xsl:value-of select="BasisFK1"/><![CDATA[Command
        {
            get { return _save]]><xsl:value-of select="BasisFK1"/><![CDATA[Command ?? (_save]]><xsl:value-of select="BasisFK1"/><![CDATA[Command = new RelayCommand(Save]]><xsl:value-of select="BasisFK1"/><![CDATA[)); }
        }

        private void Save]]><xsl:value-of select="BasisFK1"/><![CDATA[()
        {
            try
            {
                var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[= ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[.FirstOrDefault(x => x.]]><xsl:value-of select="IDFK1"/><![CDATA[== Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="IDFK1"/><![CDATA[);
                if (Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ == null)
                {
                    MessageBox.Show("]]><xsl:value-of select="BasisSmFK1"/><![CDATA[ was not found");
                }
                else
                {
                    if (Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="IDFK1"/><![CDATA[!= 0)
                    {
                        if (]]><xsl:value-of select="BasisSmFK1"/><![CDATA[!= null) //update
                        {
                            ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[.Updater = Environment.UserName;
                            ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.Add(new ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[()
                        {
                            ]]><xsl:value-of select="NameFK1"/><![CDATA[= Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Valid,
                            ValidYear = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.ValidYear,
                            Synonym = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Synonym,
                            Author = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Author,
                            AuthorYear = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.AuthorYear,
                            Info = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Info,
                            EngName = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.EngName,
                            GerName = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.GerName,
                            FraName = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.FraName,
                            PorName = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Memo
                        });
                    }
                    {
                        ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.Save();
                        MessageBox.Show(Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[+  " was successfully saved ");
                        Get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByName();  //Refresh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
   ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose>    

<xsl:choose>
<xsl:when test="Table ='Public Commands Connect   TK1  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:otherwise>  
  <xsl:if test="TableTK1 !='NULL'">
       <![CDATA[ 
        #region "Public Commands Connect ==> ]]><xsl:value-of select="LinqModelTK1"/><![CDATA["                 

        private RelayCommand _get]]><xsl:value-of select="BasisTK1"/><![CDATA[ByNameCommand;
        public ICommand Get]]><xsl:value-of select="BasisTK1"/><![CDATA[ByNameCommand
        {
            get { return _get]]><xsl:value-of select="BasisTK1"/><![CDATA[ByNameCommand ?? (_get]]><xsl:value-of select="BasisTK1"/><![CDATA[ByNameCommand = new RelayCommand(Get]]><xsl:value-of select="BasisTK1"/><![CDATA[ByName)); }
        }

        private void Get]]><xsl:value-of select="BasisTK1"/><![CDATA[ByName()
        {
            ]]><xsl:value-of select="TableTK1"/><![CDATA[List =
                new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>((from ]]><xsl:value-of select="BasisSmTK1"/><![CDATA[ in ]]><xsl:value-of select="TableTK1"/><![CDATA[Repository.]]><xsl:value-of select="TableTK1"/><![CDATA[
                                                       where ]]><xsl:value-of select="BasisSmTK1"/><![CDATA[.]]><xsl:value-of select="NameTK1"/><![CDATA[.StartsWith(Search]]><xsl:value-of select="BasisTK1"/><![CDATA[Name)
                                                       orderby ]]><xsl:value-of select="BasisSmTK1"/><![CDATA[.]]><xsl:value-of select="NameTK1"/><![CDATA[
                                                       select ]]><xsl:value-of select="BasisSmTK1"/><![CDATA[));

            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
            if (]]><xsl:value-of select="BasissTK1"/><![CDATA[View != null)
                ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.CurrentChanged += ]]><xsl:value-of select="EntityTK1"/><![CDATA[View_CurrentChanged;                   
            RaisePropertyChanged("Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _add]]><xsl:value-of select="BasisTK1"/><![CDATA[Command;
        public ICommand Add]]><xsl:value-of select="BasisTK1"/><![CDATA[Command
        {
            get { return _add]]><xsl:value-of select="BasisTK1"/><![CDATA[Command ?? (_add]]><xsl:value-of select="BasisTK1"/><![CDATA[Command = new RelayCommand(Add]]><xsl:value-of select="BasisTK1"/><![CDATA[)); }
        }

        private void Add]]><xsl:value-of select="BasisTK1"/><![CDATA[()
        {
            if (]]><xsl:value-of select="TableTK1"/><![CDATA[List == null)
                ]]><xsl:value-of select="TableTK1"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>();
            ]]><xsl:value-of select="TableTK1"/><![CDATA[List.Add(new ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[{ ]]><xsl:value-of select="NameTK1"/><![CDATA[= "New " });                   
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
            if (]]><xsl:value-of select="BasissTK1"/><![CDATA[View != null)
                ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.CurrentChanged += ]]><xsl:value-of select="EntityTK1"/><![CDATA[View_CurrentChanged;
            RaisePropertyChanged("Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[");
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _delete]]><xsl:value-of select="BasisTK1"/><![CDATA[Command;
        public ICommand Delete]]><xsl:value-of select="BasisTK1"/><![CDATA[Command
        {
            get { return _delete]]><xsl:value-of select="BasisTK1"/><![CDATA[Command ?? (_delete]]><xsl:value-of select="BasisTK1"/><![CDATA[Command = new RelayCommand(Delete]]><xsl:value-of select="BasisTK1"/><![CDATA[)); }
        }

        private void Delete]]><xsl:value-of select="BasisTK1"/><![CDATA[()
        {
            try
            {
                var ]]><xsl:value-of select="BasisSmTK1"/><![CDATA[ = ]]><xsl:value-of select="TableTK1"/><![CDATA[Repository.]]><xsl:value-of select="TableTK1"/><![CDATA[.FirstOrDefault(x => x.]]><xsl:value-of select="IDTK1"/><![CDATA[== Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="IDTK1"/><![CDATA[);
                if (]]><xsl:value-of select="BasisSmTK1"/><![CDATA[ != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="NameTK1"/><![CDATA[, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    ]]><xsl:value-of select="TableTK1"/><![CDATA[Repository.Delete(]]><xsl:value-of select="BasisSmTK1"/><![CDATA[);
                    ]]><xsl:value-of select="TableTK1"/><![CDATA[Repository.Save();
                    MessageBox.Show(Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="NameTK1"/><![CDATA[+ " was deleted successfully");
                    if (Search]]><xsl:value-of select="BasisTK1"/><![CDATA[Name == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        Get]]><xsl:value-of select="BasisTK1"/><![CDATA[ByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="NameTK1"/><![CDATA[+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _save]]><xsl:value-of select="BasisTK1"/><![CDATA[Command;   
        public ICommand Save]]><xsl:value-of select="BasisTK1"/><![CDATA[Command
        {
            get { return _save]]><xsl:value-of select="BasisTK1"/><![CDATA[Command ?? (_save]]><xsl:value-of select="BasisTK1"/><![CDATA[Command = new RelayCommand(Save]]><xsl:value-of select="BasisTK1"/><![CDATA[)); }
        }

        private void Save]]><xsl:value-of select="BasisTK1"/><![CDATA[()
        {
            try
            {
                var ]]><xsl:value-of select="BasisSmTK1"/><![CDATA[ = ]]><xsl:value-of select="TableTK1"/><![CDATA[Repository.]]><xsl:value-of select="TableTK1"/><![CDATA[.FirstOrDefault(x => x.]]><xsl:value-of select="IDTK1"/><![CDATA[== Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="IDTK1"/><![CDATA[);
                if (Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ == null)
                {
                    MessageBox.Show("]]><xsl:value-of select="BasisSmTK1"/><![CDATA[ was not found");
                }
                else
                {
                    if (Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="IDTK1"/><![CDATA[!= 0)
                    {
                        if (]]><xsl:value-of select="BasisSmTK1"/><![CDATA[!= null) //update
                        {
                            ]]><xsl:value-of select="BasisSmTK1"/><![CDATA[.Updater = Environment.UserName;
                            ]]><xsl:value-of select="BasisSmTK1"/><![CDATA[.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        ]]><xsl:value-of select="TableTK1"/><![CDATA[Repository.Add(new ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[
                        {
                            ]]><xsl:value-of select="ID"/><![CDATA[= Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[,              
                            ]]><xsl:value-of select="NameTK1"/><![CDATA[= Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="NameTK1"/><![CDATA[,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.Valid,
                            ValidYear = Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.ValidYear,
                            Synonym = Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.Synonym,
                            Author = Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.Author,
                            AuthorYear = Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.AuthorYear,
                            Info = Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.Info,
                            EngName = Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.EngName,
                            GerName = Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.GerName,
                            FraName = Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.FraName,
                            PorName = Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.Memo
                        });
                    }
                    {
                        ]]><xsl:value-of select="TableTK1"/><![CDATA[Repository.Save();
                        MessageBox.Show(Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="NameTK1"/><![CDATA[+  " was successfully saved ");
                        if (Search]]><xsl:value-of select="BasisTK1"/><![CDATA[Name == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            Get]]><xsl:value-of select="BasisTK1"/><![CDATA[ByName(); //search
                        }       
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
   ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose>    

<xsl:choose>
<xsl:when test="Table ='Public Commands Connect   TK2  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:otherwise>  
  <xsl:if test="TableTK2 !='NULL'">
       <![CDATA[ 
        #region "Public Commands Connect ==> ]]><xsl:value-of select="LinqModelTK2"/><![CDATA["                 

        private RelayCommand _get]]><xsl:value-of select="BasisTK2"/><![CDATA[ByNameCommand;
        public ICommand Get]]><xsl:value-of select="BasisTK2"/><![CDATA[ByNameCommand
        {
            get { return _get]]><xsl:value-of select="BasisTK2"/><![CDATA[ByNameCommand ?? (_get]]><xsl:value-of select="BasisTK2"/><![CDATA[ByNameCommand = new RelayCommand(Get]]><xsl:value-of select="BasisTK2"/><![CDATA[ByName)); }
        }

        private void Get]]><xsl:value-of select="BasisTK2"/><![CDATA[ByName()
        {
            ]]><xsl:value-of select="TableTK2"/><![CDATA[List =
                new ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[>((from ]]><xsl:value-of select="BasisSmTK2"/><![CDATA[ in ]]><xsl:value-of select="TableTK2"/><![CDATA[Repository.]]><xsl:value-of select="TableTK2"/><![CDATA[
                                                       where ]]><xsl:value-of select="BasisSmTK2"/><![CDATA[.]]><xsl:value-of select="NameTK2"/><![CDATA[.StartsWith(Search]]><xsl:value-of select="BasisTK2"/><![CDATA[Name)
                                                       orderby ]]><xsl:value-of select="BasisSmTK2"/><![CDATA[.]]><xsl:value-of select="NameTK2"/><![CDATA[
                                                       select ]]><xsl:value-of select="BasisSmTK2"/><![CDATA[));

            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK2"/><![CDATA[List);
            if (]]><xsl:value-of select="BasissTK2"/><![CDATA[View != null)
                ]]><xsl:value-of select="BasissTK2"/><![CDATA[View.CurrentChanged += ]]><xsl:value-of select="EntityTK2"/><![CDATA[View_CurrentChanged;                   
            RaisePropertyChanged("Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[");
        }
        //------------------------------------------------------------------------------
        private RelayCommand _add]]><xsl:value-of select="BasisTK2"/><![CDATA[Command;
        public ICommand Add]]><xsl:value-of select="BasisTK2"/><![CDATA[Command
        {
            get { return _add]]><xsl:value-of select="BasisTK2"/><![CDATA[Command ?? (_add]]><xsl:value-of select="BasisTK2"/><![CDATA[Command = new RelayCommand(Add]]><xsl:value-of select="BasisTK2"/><![CDATA[)); }
        }

        private void Add]]><xsl:value-of select="BasisTK2"/><![CDATA[()
        {
            if (]]><xsl:value-of select="TableTK2"/><![CDATA[List == null)
                ]]><xsl:value-of select="TableTK2"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[>();
            ]]><xsl:value-of select="TableTK2"/><![CDATA[List.Add(new ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[{ ]]><xsl:value-of select="NameTK2"/><![CDATA[= "New " });                   
            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK2"/><![CDATA[List);
            if (]]><xsl:value-of select="BasissTK2"/><![CDATA[View != null)
                ]]><xsl:value-of select="BasissTK2"/><![CDATA[View.CurrentChanged += ]]><xsl:value-of select="EntityTK2"/><![CDATA[View_CurrentChanged;
            RaisePropertyChanged("Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[");
        }
        //---------------------------------------------------------------------------------------
        private RelayCommand _delete]]><xsl:value-of select="BasisTK2"/><![CDATA[Command;
        public ICommand Delete]]><xsl:value-of select="BasisTK2"/><![CDATA[Command
        {
            get { return _delete]]><xsl:value-of select="BasisTK2"/><![CDATA[Command ?? (_delete]]><xsl:value-of select="BasisTK2"/><![CDATA[Command = new RelayCommand(Delete]]><xsl:value-of select="BasisTK2"/><![CDATA[)); }
        }

        private void Delete]]><xsl:value-of select="BasisTK2"/><![CDATA[()
        {
            try
            {
                var ]]><xsl:value-of select="BasisSmTK2"/><![CDATA[= ]]><xsl:value-of select="TableTK2"/><![CDATA[Repository.]]><xsl:value-of select="TableTK2"/><![CDATA[.FirstOrDefault(x => x.]]><xsl:value-of select="IDTK2"/><![CDATA[== Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="IDTK2"/><![CDATA[);
                if (]]><xsl:value-of select="BasisSmTK2"/><![CDATA[!= null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="NameTK2"/><![CDATA[, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    ]]><xsl:value-of select="TableTK2"/><![CDATA[Repository.Delete(]]><xsl:value-of select="BasisSmTK2"/><![CDATA[);
                    ]]><xsl:value-of select="TableTK2"/><![CDATA[Repository.Save();
                    MessageBox.Show(Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="NameTK2"/><![CDATA[+ " was deleted successfully");
                    if (Search]]><xsl:value-of select="BasisTK2"/><![CDATA[Name == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        Get]]><xsl:value-of select="BasisTK2"/><![CDATA[ByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="NameTK2"/><![CDATA[+ " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _save]]><xsl:value-of select="BasisTK2"/><![CDATA[Command;   
        public ICommand Save]]><xsl:value-of select="BasisTK2"/><![CDATA[Command
        {
            get { return _save]]><xsl:value-of select="BasisTK2"/><![CDATA[Command ?? (_save]]><xsl:value-of select="BasisTK2"/><![CDATA[Command = new RelayCommand(Save]]><xsl:value-of select="BasisTK2"/><![CDATA[)); }
        }

        private void Save]]><xsl:value-of select="BasisTK2"/><![CDATA[()
        {
            try
            {
                var ]]><xsl:value-of select="BasisSmTK2"/><![CDATA[= ]]><xsl:value-of select="TableTK2"/><![CDATA[Repository.]]><xsl:value-of select="TableTK2"/><![CDATA[.FirstOrDefault(x => x.]]><xsl:value-of select="IDTK2"/><![CDATA[== Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="IDTK2"/><![CDATA[);
                if (Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[ == null)
                {
                    MessageBox.Show("]]><xsl:value-of select="BasisSmTK2"/><![CDATA[ was not found");
                }
                else
                {
                    if (Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="IDTK2"/><![CDATA[!= 0)
                    {
                        if (]]><xsl:value-of select="BasisSmTK2"/><![CDATA[!= null) //update
                        {
                            ]]><xsl:value-of select="BasisSmTK2"/><![CDATA[.Updater = Environment.UserName;
                            ]]><xsl:value-of select="BasisSmTK2"/><![CDATA[.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        ]]><xsl:value-of select="TableTK2"/><![CDATA[Repository.Add(new ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[
                        {
                            ]]><xsl:value-of select="ID"/><![CDATA[= Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[,              
                            ]]><xsl:value-of select="NameTK2"/><![CDATA[= Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="NameTK2"/><![CDATA[,              
                            CountID = TblCountersRepository.Counter(),
                            Valid = Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.Valid,
                            ValidYear = Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.ValidYear,
                            Synonym = Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.Synonym,
                            Author = Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.Author,
                            AuthorYear = Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.AuthorYear,
                            Info = Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.Info,
                            EngName = Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.EngName,
                            GerName = Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.GerName,
                            FraName = Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.FraName,
                            PorName = Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.Memo
                        });
                    }
                    {
                        ]]><xsl:value-of select="TableTK2"/><![CDATA[Repository.Save();
                        MessageBox.Show(Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="NameTK2"/><![CDATA[+  " was successfully saved ");              
                        if (Search]]><xsl:value-of select="BasisTK2"/><![CDATA[Name == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            Get]]><xsl:value-of select="BasisTK2"/><![CDATA[ByName(); //search
                        }       
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
   ]]> 
  </xsl:if>
</xsl:otherwise>    
</xsl:choose>    

<xsl:choose>
<xsl:when test="Table ='Public Commands Reference and Comment+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">
   <![CDATA[ 
        #region "Public Commands Connect ==> Tbl90RefAuthor"

        private RelayCommand _getRefAuthorByNameCommand;
        public ICommand GetRefAuthorByNameCommand
        {
            get { return _getRefAuthorByNameCommand ?? (_getRefAuthorByNameCommand = new RelayCommand(GetRefAuthorByName)); }
        }

        public void GetRefAuthorByName()
        {
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from auth in Tbl90ReferencesRepository.Tbl90References
                                                          where auth.Tbl90RefAuthors.RefAuthorName.StartsWith(SearchRefAuthorName)
                                                            && auth.Tbl90RefExperts == null
                                                            && auth.Tbl90RefSources == null
                                                          orderby auth.Tbl90RefAuthors.RefAuthorName, auth.Tbl90RefAuthors.BookName, auth.Tbl90RefAuthors.Page1
                                                          select auth));

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            if (RefAuthorsView != null)
                RefAuthorsView.CurrentChanged += tbl90RefAuthorView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefAuthor");
        }

        //----------------------------------------------------
        private RelayCommand _addRefAuthorCommand;
        public ICommand AddRefAuthorCommand
        {
            get { return _addRefAuthorCommand ?? (_addRefAuthorCommand = new RelayCommand(AddRefAuthor)); }
        }

        public void AddRefAuthor()
        {
            if (Tbl90RefAuthorsList == null)
                Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>();
            Tbl90RefAuthorsList.Add(new Tbl90Reference { Info = "New " });
            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            if (RefAuthorsView != null)
                RefAuthorsView.CurrentChanged += tbl90RefAuthorView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefAuthor");
        }

        //---------------------------------------------------------------------------------------

        private RelayCommand _deleteRefAuthorCommand;
        public ICommand DeleteRefAuthorCommand
        {
            get { return _deleteRefAuthorCommand ?? (_deleteRefAuthorCommand = new RelayCommand(DeleteRefAuthor)); }
        }

        public void DeleteRefAuthor()
        {
            try
            {
                var refAuthor = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefAuthor.ReferenceID);
                if (refAuthor != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl90RefAuthor.Info, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl90ReferencesRepository.Delete(refAuthor);
                    Tbl90ReferencesRepository.Save();
                    MessageBox.Show(CurrentTbl90RefAuthor.Info + " was deleted successfully");
                    if (SearchRefAuthorName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetRefAuthorByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl90RefAuthor.Info + " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------

        private RelayCommand _saveRefAuthorCommand;
        public ICommand SaveRefAuthorCommand
        {
            get { return _saveRefAuthorCommand ?? (_saveRefAuthorCommand = new RelayCommand(SaveRefAuthor)); }
        }

        public void SaveRefAuthor()
        {
            try
            {
                var refAuthor = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefAuthor.ReferenceID);
                if (CurrentTbl90RefAuthor == null)
                {
                    MessageBox.Show("reference Author was not found");
                }
                else
                {
                    if (CurrentTbl90RefAuthor.ReferenceID != 0)
                    {
                        if (refAuthor != null)
                        {
                            refAuthor.Updater = Environment.UserName;
                            refAuthor.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl90ReferencesRepository.Add(new Tbl90Reference()
                        {
                            RefAuthorID = CurrentTbl90RefAuthor.RefAuthorID,
                            ]]><xsl:value-of select="ID"/><![CDATA[= CurrentTbl90RefAuthor.]]><xsl:value-of select="ID"/><![CDATA[,
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl90RefAuthor.Valid,
                            ValidYear = CurrentTbl90RefAuthor.ValidYear,
                            Info = CurrentTbl90RefAuthor.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefAuthor.Memo
                        });
                    }
                    {
                        Tbl90ReferencesRepository.Save();
                        MessageBox.Show(CurrentTbl90RefAuthor.Info + " was successfully saved ");
                        if (SearchRefAuthorName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetRefAuthorByName(); //search
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"

        #region "Public Commands Connect ==> Tbl90RefSource"

        private RelayCommand _getRefSourceByNameCommand;
        public ICommand GetRefSourceByNameCommand
        {
            get { return _getRefSourceByNameCommand ?? (_getRefSourceByNameCommand = new RelayCommand(GetRefSourceByName)); }
        }

        public void GetRefSourceByName()
        {
            Tbl90RefSourcesList =
                new ObservableCollection<Tbl90Reference>((from refe in Tbl90ReferencesRepository.Tbl90References
                                                          where refe.Tbl90RefSources.RefSourceName.StartsWith(SearchRefSourceName)
                                                          && refe.Tbl90RefExperts == null
                                                          && refe.Tbl90RefAuthors == null
                                                          orderby refe.Tbl90RefSources.RefSourceName, refe.Tbl90RefSources.Author, refe.Tbl90RefSources.SourceYear
                                                          select refe));

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            if (RefSourcesView != null)
                RefSourcesView.CurrentChanged += tbl90RefSourceView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefSource");
        }

        //----------------------------------------------------
        private RelayCommand _addRefSourceCommand;
        public ICommand AddRefSourceCommand
        {
            get { return _addRefSourceCommand ?? (_addRefSourceCommand = new RelayCommand(AddRefSource)); }
        }

        public void AddRefSource()
        {
            if (Tbl90RefSourcesList == null)
                Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>();
            Tbl90RefSourcesList.Add(new Tbl90Reference { Info = "New " });
            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            if (RefSourcesView != null)
                RefSourcesView.CurrentChanged += tbl90RefSourceView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefSource");
        }

        //---------------------------------------------------------------------------------------

        private RelayCommand _deleteRefSourceCommand;
        public ICommand DeleteRefSourceCommand
        {
            get { return _deleteRefSourceCommand ?? (_deleteRefSourceCommand = new RelayCommand(DeleteRefSource)); }
        }

        public void DeleteRefSource()
        {
            try
            {
                var refSource = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefSource.ReferenceID);
                if (refSource != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl90RefSource.Info, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl90ReferencesRepository.Delete(refSource);
                    Tbl90ReferencesRepository.Save();
                    MessageBox.Show(CurrentTbl90RefSource.Info + " was deleted successfully");
                    if (SearchRefSourceName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetRefSourceByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl90RefSource.Info + " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveRefSourceCommand;
        public ICommand SaveRefSourceCommand
        {
            get { return _saveRefSourceCommand ?? (_saveRefSourceCommand = new RelayCommand(SaveRefSource)); }
        }

        private void SaveRefSource()
        {
            try
            {
                var refSource = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefSource.ReferenceID);
                if (CurrentTbl90RefSource == null)
                {
                    MessageBox.Show("reference Source was not found");
                }
                else
                {
                    if (CurrentTbl90RefSource.ReferenceID != 0)
                    {
                        if (refSource != null)
                        {
                            refSource.Updater = Environment.UserName;
                            refSource.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl90ReferencesRepository.Add(new Tbl90Reference()
                        {
                            RefSourceID = CurrentTbl90RefSource.RefSourceID,
                            ]]><xsl:value-of select="ID"/><![CDATA[= CurrentTbl90RefSource.]]><xsl:value-of select="ID"/><![CDATA[,
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl90RefSource.Valid,
                            ValidYear = CurrentTbl90RefSource.ValidYear,
                            Info = CurrentTbl90RefSource.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefSource.Memo
                        });
                    }
                    {
                        Tbl90ReferencesRepository.Save();
                        MessageBox.Show(CurrentTbl90RefSource.Info + " was successfully saved ");
                        if (SearchRefSourceName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetRefSourceByName(); //search
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"

        #region "Public Commands Connect ==> Tbl90RefExpert"

        private RelayCommand _getRefExpertByNameCommand;
        public ICommand GetRefExpertByNameCommand
        {
            get { return _getRefExpertByNameCommand ?? (_getRefExpertByNameCommand = new RelayCommand(GetRefExpertByName)); }
        }

        public void GetRefExpertByName()
        {
            Tbl90RefExpertsList =
                new ObservableCollection<Tbl90Reference>((from refe in Tbl90ReferencesRepository.Tbl90References
                                                          where refe.Tbl90RefExperts.RefExpertName.StartsWith(SearchRefExpertName)
                                                          && refe.Tbl90RefSources == null
                                                          && refe.Tbl90RefAuthors == null
                                                          orderby refe.Tbl90RefExperts.RefExpertName, refe.Tbl90RefExperts.Info
                                                          select refe));

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            if (RefExpertsView != null)
                RefExpertsView.CurrentChanged += tbl90RefExpertView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefExpert");
        }

        //----------------------------------------------------
        private RelayCommand _addRefExpertCommand;
        public ICommand AddRefExpertCommand
        {
            get { return _addRefExpertCommand ?? (_addRefExpertCommand = new RelayCommand(AddRefExpert)); }
        }

        public void AddRefExpert()
        {
            if (Tbl90RefExpertsList == null)
                Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>();
            Tbl90RefExpertsList.Add(new Tbl90Reference { Info = "New " });
            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            if (RefExpertsView != null)
                RefExpertsView.CurrentChanged += tbl90RefExpertView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefExpert");
        }

        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteRefExpertCommand;
        public ICommand DeleteRefExpertCommand
        {
            get { return _deleteRefExpertCommand ?? (_deleteRefExpertCommand = new RelayCommand(DeleteRefExpert)); }
        }

        public void DeleteRefExpert()
        {
            try
            {
                var refExpert = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefExpert.ReferenceID);
                if (refExpert != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl90RefExpert.Info, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl90ReferencesRepository.Delete(refExpert);
                    Tbl90ReferencesRepository.Save();
                    MessageBox.Show(CurrentTbl90RefExpert.Info + " was deleted successfully");
                    if (SearchRefExpertName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetRefExpertByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl90RefExpert.Info + " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveRefExpertCommand;
        public ICommand SaveRefExpertCommand
        {
            get { return _saveRefExpertCommand ?? (_saveRefExpertCommand = new RelayCommand(SaveRefExpert)); }
        }

        private void SaveRefExpert()
        {
            try
            {
                var refExpert = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefExpert.ReferenceID);
                if (CurrentTbl90RefExpert == null)
                {
                    MessageBox.Show("reference Expert was not found");
                }
                else
                {
                    if (CurrentTbl90RefExpert.ReferenceID != 0)
                    {
                        if (refExpert != null)
                        {
                            refExpert.Updater = Environment.UserName;
                            refExpert.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl90ReferencesRepository.Add(new Tbl90Reference
                        {
                            RefExpertID = CurrentTbl90RefExpert.RefExpertID,
                            ]]><xsl:value-of select="ID"/><![CDATA[= CurrentTbl90RefExpert.]]><xsl:value-of select="ID"/><![CDATA[,
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl90RefExpert.Valid,
                            ValidYear = CurrentTbl90RefExpert.ValidYear,
                            Info = CurrentTbl90RefExpert.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefExpert.Memo
                        });
                    }
                    {
                        Tbl90ReferencesRepository.Save();
                        MessageBox.Show(CurrentTbl90RefExpert.Info + " was successfully saved ");
                        if (SearchRefExpertName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetRefExpertByName(); //search
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"

        #region "Public Commands Connect ==> Tbl93Comment"

        private RelayCommand _getCommentByNameCommand;
        public ICommand GetCommentByNameCommand
        {
            get { return _getCommentByNameCommand ?? (_getCommentByNameCommand = new RelayCommand(GetCommentByName)); }
        }

        public void GetCommentByName()
        {
            Tbl93CommentsList =
                new ObservableCollection<Tbl93Comment>((from comment in Tbl93CommentsRepository.Tbl93Comments
                                                        where comment.Info.StartsWith(SearchCommentInfo)
                                                        select comment));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
        }
        //------------------------------------------------------------------------------

        private RelayCommand _addCommentCommand;
        public ICommand AddCommentCommand
        {
            get { return _addCommentCommand ?? (_addCommentCommand = new RelayCommand(AddComment)); }
        }

        public void AddComment()
        {
            if (Tbl93CommentsList == null)
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment>();
            Tbl93CommentsList.Add(new Tbl93Comment { Info = "New " });

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
        }

        //---------------------------------------------------------------------------------------

        private RelayCommand _deleteCommentCommand;
        public ICommand DeleteCommentCommand
        {
            get { return _deleteCommentCommand ?? (_deleteCommentCommand = new RelayCommand(DeleteComment)); }
        }

        private void DeleteComment()
        {
            try
            {
                var comment = Tbl93CommentsRepository.Tbl93Comments.FirstOrDefault(x => x.CommentID == CurrentTbl93Comment.CommentID);
                if (comment != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this CommentID "
                                        + CurrentTbl93Comment.CommentID
                                         , "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl93CommentsRepository.Delete(comment);
                    Tbl93CommentsRepository.Save();
                    MessageBox.Show("CommentID" + CurrentTbl93Comment.CommentID +
                          " was successfully deleted");
                    if (SearchCommentInfo == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetCommentByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only CommentID " + CurrentTbl93Comment.CommentID +
                          " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------

        private RelayCommand _saveCommentCommand;
        public ICommand SaveCommentCommand
        {
            get { return _saveCommentCommand ?? (_saveCommentCommand = new RelayCommand(SaveComment)); }
        }

        private void SaveComment()
        {
            try
            {
                var comment = Tbl93CommentsRepository.Tbl93Comments.FirstOrDefault(x => x.CommentID == CurrentTbl93Comment.CommentID);
                if (CurrentTbl93Comment == null)
                {
                    MessageBox.Show("comment was not found");
                }
                else
                {
                    if (CurrentTbl93Comment.CommentID != 0)
                    {
                        if (comment != null)
                        {
                            comment.Updater = Environment.UserName;
                            comment.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl93CommentsRepository.Add(new Tbl93Comment()
                        {
                            ]]><xsl:value-of select="ID"/><![CDATA[= CurrentTbl93Comment.]]><xsl:value-of select="ID"/><![CDATA[,                
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl93Comment.Valid,
                            ValidYear = CurrentTbl93Comment.ValidYear,
                            Info = CurrentTbl93Comment.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl93Comment.Memo
                        });
                    }
                    {
                        Tbl93CommentsRepository.Save();
                        MessageBox.Show("CommentID" + CurrentTbl93Comment.CommentID +
                                        " was successfully saved");
                        if (SearchCommentInfo == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetCommentByName(); //search
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
 ]]> 
</xsl:when>  
<xsl:otherwise>   
   <![CDATA[ 
        #region "Public Commands Connect ==> Tbl90RefAuthor"

        private RelayCommand _getRefAuthorByNameCommand;
        public new ICommand GetRefAuthorByNameCommand
        {
            get { return _getRefAuthorByNameCommand ?? (_getRefAuthorByNameCommand = new RelayCommand(GetRefAuthorByName)); }
        }

        //----------------------------------------------------
        private RelayCommand _addRefAuthorCommand;
        public new ICommand AddRefAuthorCommand
        {
            get { return _addRefAuthorCommand ?? (_addRefAuthorCommand = new RelayCommand(AddRefAuthor)); }
        }

        //---------------------------------------------------------------------------------------

        private RelayCommand _deleteRefAuthorCommand;
        public new ICommand DeleteRefAuthorCommand
        {
            get { return _deleteRefAuthorCommand ?? (_deleteRefAuthorCommand = new RelayCommand(DeleteRefAuthor)); }
        }

        public new void DeleteRefAuthor()
        {
            try
            {
                var refAuthor = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefAuthor.ReferenceID);
                if (refAuthor != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl90RefAuthor.Info, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl90ReferencesRepository.Delete(refAuthor);
                    Tbl90ReferencesRepository.Save();
                    MessageBox.Show(CurrentTbl90RefAuthor.Info + " was deleted successfully");
                    if (SearchRefAuthorName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetRefAuthorByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl90RefAuthor.Info + " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------

        private RelayCommand _saveRefAuthorCommand;
        public new ICommand SaveRefAuthorCommand
        {
            get { return _saveRefAuthorCommand ?? (_saveRefAuthorCommand = new RelayCommand(SaveRefAuthor)); }
        }

        public new void SaveRefAuthor()
        {
            try
            {
                var refAuthor = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefAuthor.ReferenceID);
                if (CurrentTbl90RefAuthor == null)
                {
                    MessageBox.Show("reference Author was not found");
                }
                else
                {
                    if (CurrentTbl90RefAuthor.ReferenceID != 0)
                    {
                        if (refAuthor != null)
                        {
                            refAuthor.Updater = Environment.UserName;
                            refAuthor.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl90ReferencesRepository.Add(new Tbl90Reference()
                        {
                            RefAuthorID = CurrentTbl90RefAuthor.RefAuthorID,
                            ]]><xsl:value-of select="ID"/><![CDATA[= CurrentTbl90RefAuthor.]]><xsl:value-of select="ID"/><![CDATA[,
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl90RefAuthor.Valid,
                            ValidYear = CurrentTbl90RefAuthor.ValidYear,
                            Info = CurrentTbl90RefAuthor.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefAuthor.Memo
                        });
                    }
                    {
                        Tbl90ReferencesRepository.Save();
                        MessageBox.Show(CurrentTbl90RefAuthor.Info + " was successfully saved ");
                        if (SearchRefAuthorName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetRefAuthorByName(); //search
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"

        #region "Public Commands Connect ==> Tbl90RefSource"

        private RelayCommand _getRefSourceByNameCommand;
        public new ICommand GetRefSourceByNameCommand
        {
            get { return _getRefSourceByNameCommand ?? (_getRefSourceByNameCommand = new RelayCommand(GetRefSourceByName)); }
        }

        //----------------------------------------------------
        private RelayCommand _addRefSourceCommand;
        public new ICommand AddRefSourceCommand
        {
            get { return _addRefSourceCommand ?? (_addRefSourceCommand = new RelayCommand(AddRefSource)); }
        }

        //---------------------------------------------------------------------------------------

        private RelayCommand _deleteRefSourceCommand;
        public new ICommand DeleteRefSourceCommand
        {
            get { return _deleteRefSourceCommand ?? (_deleteRefSourceCommand = new RelayCommand(DeleteRefSource)); }
        }

        public new void DeleteRefSource()
        {
            try
            {
                var refSource = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefSource.ReferenceID);
                if (refSource != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl90RefSource.Info, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl90ReferencesRepository.Delete(refSource);
                    Tbl90ReferencesRepository.Save();
                    MessageBox.Show(CurrentTbl90RefSource.Info + " was deleted successfully");
                    if (SearchRefSourceName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetRefSourceByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl90RefSource.Info + " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveRefSourceCommand;
        public new ICommand SaveRefSourceCommand
        {
            get { return _saveRefSourceCommand ?? (_saveRefSourceCommand = new RelayCommand(SaveRefSource)); }
        }

        public new void SaveRefSource()
        {
            try
            {
                var refSource = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefSource.ReferenceID);
                if (CurrentTbl90RefSource == null)
                {
                    MessageBox.Show("reference Source was not found");
                }
                else
                {
                    if (CurrentTbl90RefSource.ReferenceID != 0)
                    {
                        if (refSource != null)
                        {
                            refSource.Updater = Environment.UserName;
                            refSource.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl90ReferencesRepository.Add(new Tbl90Reference()
                        {
                            RefSourceID = CurrentTbl90RefSource.RefSourceID,
                            ]]><xsl:value-of select="ID"/><![CDATA[= CurrentTbl90RefSource.]]><xsl:value-of select="ID"/><![CDATA[,
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl90RefSource.Valid,
                            ValidYear = CurrentTbl90RefSource.ValidYear,
                            Info = CurrentTbl90RefSource.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefSource.Memo
                        });
                    }
                    {
                        Tbl90ReferencesRepository.Save();
                        MessageBox.Show(CurrentTbl90RefSource.Info + " was successfully saved ");
                        if (SearchRefSourceName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetRefSourceByName(); //search
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"

        #region "Public Commands Connect ==> Tbl90RefExpert"

        private RelayCommand _getRefExpertByNameCommand;
        public new ICommand GetRefExpertByNameCommand
        {
            get { return _getRefExpertByNameCommand ?? (_getRefExpertByNameCommand = new RelayCommand(GetRefExpertByName)); }
        }

        //----------------------------------------------------
        private RelayCommand _addRefExpertCommand;
        public new ICommand AddRefExpertCommand
        {
            get { return _addRefExpertCommand ?? (_addRefExpertCommand = new RelayCommand(AddRefExpert)); }
        }

        //---------------------------------------------------------------------------------------
        private RelayCommand _deleteRefExpertCommand;
        public new ICommand DeleteRefExpertCommand
        {
            get { return _deleteRefExpertCommand ?? (_deleteRefExpertCommand = new RelayCommand(DeleteRefExpert)); }
        }

        public new void DeleteRefExpert()
        {
            try
            {
                var refExpert = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefExpert.ReferenceID);
                if (refExpert != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this "
                                        + CurrentTbl90RefExpert.Info, "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl90ReferencesRepository.Delete(refExpert);
                    Tbl90ReferencesRepository.Save();
                    MessageBox.Show(CurrentTbl90RefExpert.Info + " was deleted successfully");
                    if (SearchRefExpertName == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetRefExpertByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only " + CurrentTbl90RefExpert.Info + " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveRefExpertCommand;
        public new ICommand SaveRefExpertCommand
        {
            get { return _saveRefExpertCommand ?? (_saveRefExpertCommand = new RelayCommand(SaveRefExpert)); }
        }

        private void SaveRefExpert()
        {
            try
            {
                var refExpert = Tbl90ReferencesRepository.Tbl90References.FirstOrDefault(x => x.ReferenceID == CurrentTbl90RefExpert.ReferenceID);
                if (CurrentTbl90RefExpert == null)
                {
                    MessageBox.Show("reference Expert was not found");
                }
                else
                {
                    if (CurrentTbl90RefExpert.ReferenceID != 0)
                    {
                        if (refExpert != null)
                        {
                            refExpert.Updater = Environment.UserName;
                            refExpert.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl90ReferencesRepository.Add(new Tbl90Reference
                        {
                            RefExpertID = CurrentTbl90RefExpert.RefExpertID,
                            ]]><xsl:value-of select="ID"/><![CDATA[= CurrentTbl90RefExpert.]]><xsl:value-of select="ID"/><![CDATA[,
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl90RefExpert.Valid,
                            ValidYear = CurrentTbl90RefExpert.ValidYear,
                            Info = CurrentTbl90RefExpert.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefExpert.Memo
                        });
                    }
                    {
                        Tbl90ReferencesRepository.Save();
                        MessageBox.Show(CurrentTbl90RefExpert.Info + " was successfully saved ");
                        if (SearchRefExpertName == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetRefExpertByName(); //search
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"

        #region "Public Commands Connect ==> Tbl93Comment"

        private RelayCommand _getCommentByNameCommand;
        public new ICommand GetCommentByNameCommand
        {
            get { return _getCommentByNameCommand ?? (_getCommentByNameCommand = new RelayCommand(GetCommentByName)); }
        }

        //------------------------------------------------------------------------------

        private RelayCommand _addCommentCommand;
        public new ICommand AddCommentCommand
        {
            get { return _addCommentCommand ?? (_addCommentCommand = new RelayCommand(AddComment)); }
        }

        //---------------------------------------------------------------------------------------

        private RelayCommand _deleteCommentCommand;
        public new ICommand DeleteCommentCommand
        {
            get { return _deleteCommentCommand ?? (_deleteCommentCommand = new RelayCommand(DeleteComment)); }
        }

        public new void DeleteComment()
        {
            try
            {
                var comment = Tbl93CommentsRepository.Tbl93Comments.FirstOrDefault(x => x.CommentID == CurrentTbl93Comment.CommentID);
                if (comment != null)
                {
                    if (MessageBox.Show("Are you really shure to delete this CommentID "
                                        + CurrentTbl93Comment.CommentID
                                         , "Question", MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl93CommentsRepository.Delete(comment);
                    Tbl93CommentsRepository.Save();
                    MessageBox.Show("CommentID" + CurrentTbl93Comment.CommentID +
                          " was successfully deleted");
                    if (SearchCommentInfo == null)
                    {
                        GetConnectedTablesById(); //refresh doubleClick                    
                    }
                    else
                    {
                        GetCommentByName(); //search
                    }
                }
                else
                {
                    MessageBox.Show("Only CommentID " + CurrentTbl93Comment.CommentID +
                          " can be deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------

        private RelayCommand _saveCommentCommand;
        public new ICommand SaveCommentCommand
        {
            get { return _saveCommentCommand ?? (_saveCommentCommand = new RelayCommand(SaveComment)); }
        }

        public new void SaveComment()
        {
            try
            {
                var comment = Tbl93CommentsRepository.Tbl93Comments.FirstOrDefault(x => x.CommentID == CurrentTbl93Comment.CommentID);
                if (CurrentTbl93Comment == null)
                {
                    MessageBox.Show("comment was not found");
                }
                else
                {
                    if (CurrentTbl93Comment.CommentID != 0)
                    {
                        if (comment != null)
                        {
                            comment.Updater = Environment.UserName;
                            comment.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl93CommentsRepository.Add(new Tbl93Comment()
                        {
                            ]]><xsl:value-of select="ID"/><![CDATA[= CurrentTbl93Comment.]]><xsl:value-of select="ID"/><![CDATA[,                
                            CountID = TblCountersRepository.Counter(),
                            Valid = CurrentTbl93Comment.Valid,
                            ValidYear = CurrentTbl93Comment.ValidYear,
                            Info = CurrentTbl93Comment.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl93Comment.Memo
                        });
                    }
                    {
                        Tbl93CommentsRepository.Save();
                        MessageBox.Show("CommentID" + CurrentTbl93Comment.CommentID +
                                        " was successfully saved");
                        if (SearchCommentInfo == null)
                        {
                            GetConnectedTablesById(); //refresh doubleClick                    
                        }
                        else
                        {
                            GetCommentByName(); //search
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"
 ]]> 
</xsl:otherwise>    
</xsl:choose>    

<xsl:choose>
<xsl:when test="Table ='++++++DoubleClick+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">
   <![CDATA[  
        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand
        {
            get { return _getConnectedTablesCommand ?? (_getConnectedTablesCommand = new RelayCommand(GetConnectedTablesById)); }
        }

        private void GetConnectedTablesById()
        {
            //Clear Search-TextBox
            SearchPhylumName = null;
            SearchDivisionName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;


            Tbl06PhylumsList =
                new ObservableCollection<Tbl06Phylum>((from phy in Tbl06PhylumsRepository.Tbl06Phylums
                                                       where phy.RegnumID == CurrentTbl03Regnum.RegnumID
                                                       orderby phy.Tbl03Regnums.RegnumName, phy.Tbl03Regnums.Subregnum
                                                       select phy));

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            if (PhylumsView != null)
                PhylumsView.CurrentChanged += tbl06PhylumView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl06Phylums");
            //-----------------------------------------------------------------------------------
            Tbl09DivisionsList =
                new ObservableCollection<Tbl09Division>((from div in Tbl09DivisionsRepository.Tbl09Divisions
                                                         where div.RegnumID == CurrentTbl03Regnum.RegnumID
                                                         orderby div.Tbl03Regnums.RegnumName, div.Tbl03Regnums.Subregnum
                                                         select div));

            DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            if (DivisionsView != null)
                DivisionsView.CurrentChanged += tbl09DivisionView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl09Division");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.RegnumID == CurrentTbl03Regnum.RegnumID
                                                          && refAu.Tbl90RefExperts == null
                                                          && refAu.Tbl90RefSources == null
                                                          orderby refAu.Tbl90RefAuthors.RefAuthorName, refAu.Tbl90RefAuthors.BookName, refAu.Tbl90RefAuthors.Page1
                                                          select refAu));

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            if (RefAuthorsView != null)
                RefAuthorsView.CurrentChanged += tbl90RefAuthorView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefAuthor");
            //--------------------------------------------------------------------------------------
            Tbl90RefSourcesList =
                new ObservableCollection<Tbl90Reference>((from refSo in Tbl90ReferencesRepository.Tbl90References
                                                          where refSo.RegnumID == CurrentTbl03Regnum.RegnumID
                                                          && refSo.Tbl90RefExperts == null
                                                          && refSo.Tbl90RefAuthors == null
                                                          orderby refSo.Tbl90RefSources.RefSourceName
                                                          select refSo));

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            if (RefSourcesView != null)
                RefSourcesView.CurrentChanged += tbl90RefSourceView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefSource");
            //--------------------------------------------------------------------------------------
            Tbl90RefExpertsList =
                new ObservableCollection<Tbl90Reference>((from refEx in Tbl90ReferencesRepository.Tbl90References
                                                          where refEx.RegnumID == CurrentTbl03Regnum.RegnumID
                                                          && refEx.Tbl90RefAuthors == null
                                                          && refEx.Tbl90RefSources == null
                                                          orderby refEx.Tbl90RefExperts.RefExpertName
                                                          select refEx));

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            if (RefExpertsView != null)
                RefExpertsView.CurrentChanged += tbl90RefExpertView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefExpert");
            //-----------------------------------------------------------------------------------
            Tbl93CommentsList =
                new ObservableCollection<Tbl93Comment>((from comm in Tbl93CommentsRepository.Tbl93Comments
                                                        where comm.RegnumID == CurrentTbl03Regnum.RegnumID
                                                        orderby comm.Info
                                                        select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
            //--------------------------------------------------------------


        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl06Phylums'">
   <![CDATA[  
        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public new ICommand GetConnectedTablesCommand
        {
            get { return _getConnectedTablesCommand ?? (_getConnectedTablesCommand = new RelayCommand(GetConnectedTablesById)); }
        }

        public void GetConnectedTablesById()
        {
            //Clear Search-TextBox
            SearchRegnumName = null;
            SearchSubphylumName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            Tbl03RegnumsList =
                new ObservableCollection<Tbl03Regnum>((from reg in Tbl03RegnumsRepository.Tbl03Regnums
                                           where reg.RegnumID == CurrentTbl06Phylum.RegnumID
                                           orderby reg.RegnumName, reg.Subregnum
                                           select reg));

            RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            if (RegnumsView != null)
                RegnumsView.CurrentChanged += tbl03RegnumView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl03Regnums");           
            //-----------------------------------------------------------------------------------
             Tbl12SubphylumsList =
                 new ObservableCollection<Tbl12Subphylum>((from subph in Tbl12SubphylumsRepository.Tbl12Subphylums
                                                           where subph.PhylumID == CurrentTbl06Phylum.PhylumID
                                                           orderby subph.Tbl06Phylums.PhylumName
                                                           select subph));


             SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
             if (SubphylumsView != null)
                 SubphylumsView.CurrentChanged += tbl12SubphylumView_CurrentChanged;
             RaisePropertyChanged("CurrentTbl12Subphylum"); 
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                           where refAu.PhylumID == CurrentTbl06Phylum.PhylumID
                                                          && refAu.Tbl90RefExperts == null
                                                          && refAu.Tbl90RefSources == null
                                                          orderby refAu.Tbl90RefAuthors.RefAuthorName, refAu.Tbl90RefAuthors.BookName, refAu.Tbl90RefAuthors.Page1
                                                          select refAu));

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            if (RefAuthorsView != null)
                RefAuthorsView.CurrentChanged += tbl90RefAuthorView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefAuthor");
            //--------------------------------------------------------------------------------------
            Tbl90RefSourcesList =
                new ObservableCollection<Tbl90Reference>((from refSo in Tbl90ReferencesRepository.Tbl90References
                                                           where refSo.PhylumID == CurrentTbl06Phylum.PhylumID
                                                          && refSo.Tbl90RefExperts == null
                                                          && refSo.Tbl90RefAuthors == null
                                                          orderby refSo.Tbl90RefSources.RefSourceName
                                                          select refSo));

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            if (RefSourcesView != null)
                RefSourcesView.CurrentChanged += tbl90RefSourceView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefSource");
            //--------------------------------------------------------------------------------------
            Tbl90RefExpertsList =
                new ObservableCollection<Tbl90Reference>((from refEx in Tbl90ReferencesRepository.Tbl90References
                                                           where refEx.PhylumID == CurrentTbl06Phylum.PhylumID
                                                          && refEx.Tbl90RefAuthors == null
                                                          && refEx.Tbl90RefSources == null
                                                          orderby refEx.Tbl90RefExperts.RefExpertName
                                                          select refEx));

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            if (RefExpertsView != null)
                RefExpertsView.CurrentChanged += tbl90RefExpertView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefExpert");
            //-----------------------------------------------------------------------------------
            Tbl93CommentsList =
                new ObservableCollection<Tbl93Comment>((from comm in Tbl93CommentsRepository.Tbl93Comments
                                                           where comm.PhylumID == CurrentTbl06Phylum.PhylumID
                                                           orderby comm.Info
                                                           select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
            //--------------------------------------------------------------


        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl09Divisions'">
   <![CDATA[  
        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public new ICommand GetConnectedTablesCommand
        {
            get { return _getConnectedTablesCommand ?? (_getConnectedTablesCommand = new RelayCommand(GetConnectedTablesById)); }
        }

        public void GetConnectedTablesById()
        {
            //Clear Search-TextBox
            SearchRegnumName = null;
            SearchSubdivisionName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            Tbl03RegnumsList =
                new ObservableCollection<Tbl03Regnum>((from reg in Tbl03RegnumsRepository.Tbl03Regnums
                                           where reg.RegnumID == CurrentTbl09Division.RegnumID
                                           orderby reg.RegnumName, reg.Subregnum
                                           select reg));

            RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            if (RegnumsView != null)
                RegnumsView.CurrentChanged += tbl03RegnumView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl03Regnums");           
            //-----------------------------------------------------------------------------------
             Tbl15SubdivisionsList =
                 new ObservableCollection<Tbl15Subdivision>((from subdiv in Tbl15SubdivisionsRepository.Tbl15Subdivisions
                                                           where subdiv.DivisionID == CurrentTbl09Division.DivisionID
                                                           orderby subdiv.Tbl09Divisions.DivisionName
                                                           select subdiv));


             SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
             if (SubdivisionsView != null)
                 SubdivisionsView.CurrentChanged += tbl15SubdivisionView_CurrentChanged;
             RaisePropertyChanged("CurrentTbl15Subdivision"); 
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                           where refAu.DivisionID == CurrentTbl09Division.DivisionID
                                                          && refAu.Tbl90RefExperts == null
                                                          && refAu.Tbl90RefSources == null
                                                          orderby refAu.Tbl90RefAuthors.RefAuthorName, refAu.Tbl90RefAuthors.BookName, refAu.Tbl90RefAuthors.Page1
                                                          select refAu));

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            if (RefAuthorsView != null)
                RefAuthorsView.CurrentChanged += tbl90RefAuthorView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefAuthor");
            //--------------------------------------------------------------------------------------
            Tbl90RefSourcesList =
                new ObservableCollection<Tbl90Reference>((from refSo in Tbl90ReferencesRepository.Tbl90References
                                                           where refSo.DivisionID == CurrentTbl09Division.DivisionID
                                                          && refSo.Tbl90RefExperts == null
                                                          && refSo.Tbl90RefAuthors == null
                                                          orderby refSo.Tbl90RefSources.RefSourceName
                                                          select refSo));

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            if (RefSourcesView != null)
                RefSourcesView.CurrentChanged += tbl90RefSourceView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefSource");
            //--------------------------------------------------------------------------------------
            Tbl90RefExpertsList =
                new ObservableCollection<Tbl90Reference>((from refEx in Tbl90ReferencesRepository.Tbl90References
                                                           where refEx.DivisionID == CurrentTbl09Division.DivisionID
                                                          && refEx.Tbl90RefAuthors == null
                                                          && refEx.Tbl90RefSources == null
                                                          orderby refEx.Tbl90RefExperts.RefExpertName
                                                          select refEx));

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            if (RefExpertsView != null)
                RefExpertsView.CurrentChanged += tbl90RefExpertView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefExpert");
            //-----------------------------------------------------------------------------------
            Tbl93CommentsList =
                new ObservableCollection<Tbl93Comment>((from comm in Tbl93CommentsRepository.Tbl93Comments
                                                           where comm.DivisionID == CurrentTbl09Division.DivisionID
                                                           orderby comm.Info
                                                           select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
            //--------------------------------------------------------------


        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">
   <![CDATA[  
        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public new ICommand GetConnectedTablesCommand
        {
            get { return _getConnectedTablesCommand ?? (_getConnectedTablesCommand = new RelayCommand(GetConnectedTablesById)); }
        }

        public new void GetConnectedTablesById()
        {
            //Clear Search-TextBox                                  
            SearchSubphylumName = null;
            SearchSubdivisionName = null;
            SearchClassName = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            //-----------------------------------------------------------------------------------
            Tbl12SubphylumsList =
                new ObservableCollection<Tbl12Subphylum>((from subphy in Tbl12SubphylumsRepository.Tbl12Subphylums
                                                          where subphy.SubphylumID == CurrentTbl18Superclass.SubphylumID
                                                          orderby subphy.SubphylumName
                                                          select subphy));


            SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            if (SubphylumsView != null)
                SubphylumsView.CurrentChanged += tbl12SubphylumView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl12Subphylum");
            //-----------------------------------------------------------------------------------
            Tbl15SubdivisionsList =
                new ObservableCollection<Tbl15Subdivision>((from subdivision in Tbl15SubdivisionsRepository.Tbl15Subdivisions
                                                            where subdivision.SubdivisionID == CurrentTbl18Superclass.SubdivisionID
                                                            orderby subdivision.SubdivisionName
                                                            select subdivision));

            SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            if (SubdivisionsView != null)
                SubdivisionsView.CurrentChanged += tbl15SubdivisionView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl15Subdivisions");
            //-----------------------------------------------------------------------------------
            Tbl21ClassesList =
                new ObservableCollection<Tbl21Class>((from classe in Tbl21ClassesRepository.Tbl21Classes
                                                      where classe.SuperclassID == CurrentTbl18Superclass.SuperclassID
                                                      orderby classe.Tbl18Superclasses.SuperclassName
                                                      select classe));


            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            if (ClassesView != null)
                ClassesView.CurrentChanged += tbl21ClassView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl21Classes");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.SuperclassID == CurrentTbl18Superclass.SuperclassID
                                                          && refAu.Tbl90RefExperts == null
                                                          && refAu.Tbl90RefSources == null
                                                          orderby refAu.Tbl90RefAuthors.RefAuthorName, refAu.Tbl90RefAuthors.BookName, refAu.Tbl90RefAuthors.Page1
                                                          select refAu));

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            if (RefAuthorsView != null)
                RefAuthorsView.CurrentChanged += tbl90RefAuthorView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefAuthor");
            //--------------------------------------------------------------------------------------
            Tbl90RefSourcesList =
                new ObservableCollection<Tbl90Reference>((from refSo in Tbl90ReferencesRepository.Tbl90References
                                                          where refSo.SuperclassID == CurrentTbl18Superclass.SuperclassID
                                                          && refSo.Tbl90RefExperts == null
                                                          && refSo.Tbl90RefAuthors == null
                                                          orderby refSo.Tbl90RefSources.RefSourceName
                                                          select refSo));

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            if (RefSourcesView != null)
                RefSourcesView.CurrentChanged += tbl90RefSourceView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefSource");
            //--------------------------------------------------------------------------------------
            Tbl90RefExpertsList =
                new ObservableCollection<Tbl90Reference>((from refEx in Tbl90ReferencesRepository.Tbl90References
                                                          where refEx.SuperclassID == CurrentTbl18Superclass.SuperclassID
                                                          && refEx.Tbl90RefAuthors == null
                                                          && refEx.Tbl90RefSources == null
                                                          orderby refEx.Tbl90RefExperts.RefExpertName
                                                          select refEx));

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            if (RefExpertsView != null)
                RefExpertsView.CurrentChanged += tbl90RefExpertView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefExpert");
            //-----------------------------------------------------------------------------------
            Tbl93CommentsList =
                new ObservableCollection<Tbl93Comment>((from comm in Tbl93CommentsRepository.Tbl93Comments
                                                        where comm.SuperclassID == CurrentTbl18Superclass.SuperclassID
                                                        orderby comm.Info
                                                        select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
            //--------------------------------------------------------------


        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   ]]>
</xsl:when>  
<xsl:otherwise>
   <![CDATA[  
        #region "Public Commands Connected Tables by DoubleClick"                                         

        private RelayCommand _getConnectedTablesCommand;
        public new ICommand GetConnectedTablesCommand
        {
            get { return _getConnectedTablesCommand ?? (_getConnectedTablesCommand = new RelayCommand(GetConnectedTablesById)); }
        }

        public new void GetConnectedTablesById()
        {
            //Clear Search-TextBox                                  
            Search]]><xsl:value-of select="BasisFK1"/><![CDATA[Name = null;                       
            Search]]><xsl:value-of select="BasisTK1"/><![CDATA[Name = null;
            SearchCommentInfo = null;
            SearchRefExpertName = null;
            SearchRefSourceName = null;
            SearchRefAuthorName = null;

            ]]><xsl:value-of select="TableFK1"/><![CDATA[List =
                new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>((from ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[ in ]]><xsl:value-of select="TableFK1"/><![CDATA[Repository.]]><xsl:value-of select="TableFK1"/><![CDATA[
                                                       where ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[.]]><xsl:value-of select="IDFK1"/><![CDATA[== Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="IDFK1"/><![CDATA[
                                                       orderby ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[.]]><xsl:value-of select="NameFK1"/><![CDATA[
                                                       select ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[));

            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK1"/><![CDATA[List);
            if (]]><xsl:value-of select="BasissFK1"/><![CDATA[View != null)
                ]]><xsl:value-of select="BasissFK1"/><![CDATA[View.CurrentChanged += ]]><xsl:value-of select="EntityFK1"/><![CDATA[View_CurrentChanged;
            RaisePropertyChanged("Current]]><xsl:value-of select="TableFK1"/><![CDATA[");
            //-----------------------------------------------------------------------------------
            ]]><xsl:value-of select="TableTK1"/><![CDATA[List =
                new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>((from ]]><xsl:value-of select="BasisSmTK1"/><![CDATA[ in ]]><xsl:value-of select="TableTK1"/><![CDATA[Repository.]]><xsl:value-of select="TableTK1"/><![CDATA[
                                                       where ]]><xsl:value-of select="BasisSmTK1"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[== Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[
                                                       orderby ]]><xsl:value-of select="BasisSmTK1"/><![CDATA[.]]><xsl:value-of select="Table"/><![CDATA[.]]><xsl:value-of select="Name"/><![CDATA[
                                                       select ]]><xsl:value-of select="BasisSmTK1"/><![CDATA[));


            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
            if (]]><xsl:value-of select="BasissTK1"/><![CDATA[View != null)
                ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.CurrentChanged += ]]><xsl:value-of select="EntityTK1"/><![CDATA[View_CurrentChanged;
            RaisePropertyChanged("Current]]><xsl:value-of select="TableTK1"/><![CDATA[");
            //-----------------------------------------------------------------------------------
            Tbl90RefAuthorsList =
                new ObservableCollection<Tbl90Reference>((from refAu in Tbl90ReferencesRepository.Tbl90References
                                                          where refAu.]]><xsl:value-of select="ID"/><![CDATA[== Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[
                                                          && refAu.Tbl90RefExperts == null
                                                          && refAu.Tbl90RefSources == null
                                                          orderby refAu.Tbl90RefAuthors.RefAuthorName, refAu.Tbl90RefAuthors.BookName, refAu.Tbl90RefAuthors.Page1
                                                          select refAu));

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            if (RefAuthorsView != null)
                RefAuthorsView.CurrentChanged += tbl90RefAuthorView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefAuthor");
            //--------------------------------------------------------------------------------------
            Tbl90RefSourcesList =
                new ObservableCollection<Tbl90Reference>((from refSo in Tbl90ReferencesRepository.Tbl90References
                                                          where refSo.]]><xsl:value-of select="ID"/><![CDATA[== Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[
                                                          && refSo.Tbl90RefExperts == null
                                                          && refSo.Tbl90RefAuthors == null
                                                          orderby refSo.Tbl90RefSources.RefSourceName
                                                          select refSo));

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            if (RefSourcesView != null)
                RefSourcesView.CurrentChanged += tbl90RefSourceView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefSource");
            //--------------------------------------------------------------------------------------
            Tbl90RefExpertsList =
                new ObservableCollection<Tbl90Reference>((from refEx in Tbl90ReferencesRepository.Tbl90References
                                                          where refEx.]]><xsl:value-of select="ID"/><![CDATA[== Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[
                                                          && refEx.Tbl90RefAuthors == null
                                                          && refEx.Tbl90RefSources == null
                                                          orderby refEx.Tbl90RefExperts.RefExpertName
                                                          select refEx));

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            if (RefExpertsView != null)
                RefExpertsView.CurrentChanged += tbl90RefExpertView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl90RefExpert");
            //-----------------------------------------------------------------------------------
            Tbl93CommentsList =
                new ObservableCollection<Tbl93Comment>((from comm in Tbl93CommentsRepository.Tbl93Comments
                                                          where comm.]]><xsl:value-of select="ID"/><![CDATA[== Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[
                                                        orderby comm.Info
                                                        select comm));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            if (CommentsView != null)
                CommentsView.CurrentChanged += tbl93CommentView_CurrentChanged;
            RaisePropertyChanged("CurrentTbl93Comment");
            //--------------------------------------------------------------

        }

        #endregion "Public Commands Connected Tables by DoubleClick"
   ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Open Detail Items++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">   <![CDATA[  
        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        public int SelectedMainTabIndex
        {
            get { return _selectedMainTabIndex; }
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value;
                RaisePropertyChanged("SelectedMainTabIndex");
                if (_selectedMainTabIndex == 0)
                    SelectedDetailTabIndex = 0;
                if (_selectedMainTabIndex == 1)
                    SelectedDetailTabIndex = 1;
                if (_selectedMainTabIndex == 2)
                    SelectedDetailTabIndex = 2;
                if (_selectedMainTabIndex == 3)
                    SelectedDetailTabIndex = 3;
            }
        }

        private int _selectedMainSubTabIndex;
        public int SelectedMainSubTabIndex
        {
            get { return _selectedMainSubTabIndex; }
            set
            {
                if (value == _selectedMainSubTabIndex) return;
                _selectedMainSubTabIndex = value;
                RaisePropertyChanged("SelectedMainSubTabIndex");
                if (_selectedMainSubTabIndex == 0)
                    SelectedDetailSubTabIndex = 0;
                if (_selectedMainSubTabIndex == 1)
                    SelectedDetailSubTabIndex = 1;
                if (_selectedMainSubTabIndex == 2)
                    SelectedDetailSubTabIndex = 2;
            }
        }

        private int _selectedDetailTabIndex;
        public int SelectedDetailTabIndex
        {
            get { return _selectedDetailTabIndex; }
            set
            {
                if (value == _selectedDetailTabIndex) return;
                _selectedDetailTabIndex = value;
                RaisePropertyChanged("SelectedDetailTabIndex");
                if (_selectedDetailTabIndex == 0)
                    SelectedMainTabIndex = 0;
                if (_selectedDetailTabIndex == 1)
                    SelectedMainTabIndex = 1;
                if (_selectedDetailTabIndex == 2)
                    SelectedMainTabIndex = 2;
                if (_selectedDetailTabIndex == 3)
                    SelectedMainTabIndex = 3;
            }
        }

        private int _selectedDetailSubTabIndex;
        public int SelectedDetailSubTabIndex
        {
            get { return _selectedDetailSubTabIndex; }
            set
            {
                if (value == _selectedDetailSubTabIndex) return;
                _selectedDetailSubTabIndex = value;
                RaisePropertyChanged("SelectedDetailSubTabIndex");
                if (_selectedDetailSubTabIndex == 0)
                    SelectedMainSubTabIndex = 0;
                if (_selectedDetailSubTabIndex == 1)
                    SelectedMainSubTabIndex = 1;
                if (_selectedDetailSubTabIndex == 2)
                    SelectedMainSubTabIndex = 2;
            }
        }
        #endregion "Public Commands to open Detail TabItems"
]]>
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">   <![CDATA[  
        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        public new int SelectedMainTabIndex
        {
            get { return _selectedMainTabIndex; }
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value;
                RaisePropertyChanged("SelectedMainTabIndex");
                if (_selectedMainTabIndex == 0)
                    SelectedDetailTabIndex = 0;
                if (_selectedMainTabIndex == 1)
                    SelectedDetailTabIndex = 1;
                if (_selectedMainTabIndex == 2)
                    SelectedDetailTabIndex = 2;
                if (_selectedMainTabIndex == 3)
                    SelectedDetailTabIndex = 3;
                if (_selectedMainTabIndex == 4)
                    SelectedDetailTabIndex = 4;
            }
        }

        private int _selectedMainSubTabIndex;
        public new int SelectedMainSubTabIndex
        {
            get { return _selectedMainSubTabIndex; }
            set
            {
                if (value == _selectedMainSubTabIndex) return;
                _selectedMainSubTabIndex = value;
                RaisePropertyChanged("SelectedMainSubTabIndex");
                if (_selectedMainSubTabIndex == 0)
                    SelectedDetailSubTabIndex = 0;
                if (_selectedMainSubTabIndex == 1)
                    SelectedDetailSubTabIndex = 1;
                if (_selectedMainSubTabIndex == 2)
                    SelectedDetailSubTabIndex = 2;
            }
        }

        private int _selectedDetailTabIndex;
        public new int SelectedDetailTabIndex
        {
            get { return _selectedDetailTabIndex; }
            set
            {
                if (value == _selectedDetailTabIndex) return;
                _selectedDetailTabIndex = value;
                RaisePropertyChanged("SelectedDetailTabIndex");
                if (_selectedDetailTabIndex == 0)
                    SelectedMainTabIndex = 0;
                if (_selectedDetailTabIndex == 1)
                    SelectedMainTabIndex = 1;
                if (_selectedDetailTabIndex == 2)
                    SelectedMainTabIndex = 2;
                if (_selectedDetailTabIndex == 3)
                    SelectedMainTabIndex = 3;
                if (_selectedDetailTabIndex == 4)
                    SelectedMainTabIndex = 4;
            }
        }

        private int _selectedDetailSubTabIndex;
        public new int SelectedDetailSubTabIndex
        {
            get { return _selectedDetailSubTabIndex; }
            set
            {
                if (value == _selectedDetailSubTabIndex) return;
                _selectedDetailSubTabIndex = value;
                RaisePropertyChanged("SelectedDetailSubTabIndex");
                if (_selectedDetailSubTabIndex == 0)
                    SelectedMainSubTabIndex = 0;
                if (_selectedDetailSubTabIndex == 1)
                    SelectedMainSubTabIndex = 1;
                if (_selectedDetailSubTabIndex == 2)
                    SelectedMainSubTabIndex = 2;
            }
        }
        #endregion "Public Commands to open Detail TabItems"
]]>
</xsl:when>  
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Properties Basis++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">
   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        public ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[
        {
            get
            {
                if (]]><xsl:value-of select="Basiss"/><![CDATA[View != null)
                    return ]]><xsl:value-of select="Basiss"/><![CDATA[View.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;
                return null;
            }
        }
        //--------------------------------------------
        private string _search]]><xsl:value-of select="Name"/><![CDATA[;
        public string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get { return _search]]><xsl:value-of select="Name"/><![CDATA[; }
            set
            {
                if (value == _search]]><xsl:value-of select="Name"/><![CDATA[) return;
                _search]]><xsl:value-of select="Name"/><![CDATA[ = value;
                RaisePropertyChanged("Search]]><xsl:value-of select="Name"/><![CDATA[");
            }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get { return ]]><xsl:value-of select="Entitys"/><![CDATA[List; }
            set
            {
                if (]]><xsl:value-of select="Entitys"/><![CDATA[List == value) return;
                ]]><xsl:value-of select="Entitys"/><![CDATA[List = value;
                RaisePropertyChanged("]]><xsl:value-of select="Table"/><![CDATA[List");
            }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get { return ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; }
            set
            {
                if (]]><xsl:value-of select="Entitys"/><![CDATA[AllList == value) return;
                ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value;
                RaisePropertyChanged("]]><xsl:value-of select="Table"/><![CDATA[AllList");
            }
        }

        #endregion "Public Properties"
   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">
   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        public new ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        public new ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[
        {
            get
            {
                if (]]><xsl:value-of select="Basiss"/><![CDATA[View != null)
                    return ]]><xsl:value-of select="Basiss"/><![CDATA[View.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;
                return null;
            }
        }
        //--------------------------------------------
        private string _search]]><xsl:value-of select="Name"/><![CDATA[;
        public new string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get { return _search]]><xsl:value-of select="Name"/><![CDATA[; }
            set
            {
                if (value == _search]]><xsl:value-of select="Name"/><![CDATA[) return;
                _search]]><xsl:value-of select="Name"/><![CDATA[ = value;
                RaisePropertyChanged("Search]]><xsl:value-of select="Name"/><![CDATA[");
            }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get { return ]]><xsl:value-of select="Entitys"/><![CDATA[List; }
            set
            {
                if (]]><xsl:value-of select="Entitys"/><![CDATA[List == value) return;
                ]]><xsl:value-of select="Entitys"/><![CDATA[List = value;
                RaisePropertyChanged("]]><xsl:value-of select="Table"/><![CDATA[List");

                //Clear Search-TextBox
                Search]]><xsl:value-of select="NameFK1"/><![CDATA[ = null;                                
                Search]]><xsl:value-of select="NameFK2"/><![CDATA[ = null;                                
                Search]]><xsl:value-of select="NameTK1"/><![CDATA[ = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get { return ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; }
            set
            {
                if (]]><xsl:value-of select="Entitys"/><![CDATA[AllList == value) return;
                ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value;
                RaisePropertyChanged("]]><xsl:value-of select="Table"/><![CDATA[AllList");
            }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[AllList
        {
            get { return ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList; }
            set
            {
                if (]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList == value) return;
                ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList = value;
                RaisePropertyChanged("]]><xsl:value-of select="TableFK2"/><![CDATA[AllList");
            }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelBK2"/><![CDATA[> ]]><xsl:value-of select="EntitysBK2"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModelBK2"/><![CDATA[> ]]><xsl:value-of select="TableBK2"/><![CDATA[AllList
        {
            get { return ]]><xsl:value-of select="EntitysBK2"/><![CDATA[AllList; }
            set
            {
                if (]]><xsl:value-of select="EntitysBK2"/><![CDATA[AllList == value) return;
                ]]><xsl:value-of select="EntitysBK2"/><![CDATA[AllList = value;
                RaisePropertyChanged("]]><xsl:value-of select="TableBK2"/><![CDATA[AllList");
            }
        }

        #endregion "Public Properties"
   ]]>
</xsl:when>  
<xsl:otherwise>
   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        public new ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        public new ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[
        {
            get
            {
                if (]]><xsl:value-of select="Basiss"/><![CDATA[View != null)
                    return ]]><xsl:value-of select="Basiss"/><![CDATA[View.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;
                return null;
            }
        }
        //--------------------------------------------
        private string _search]]><xsl:value-of select="Name"/><![CDATA[;
        public new string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get { return _search]]><xsl:value-of select="Name"/><![CDATA[; }
            set
            {
                if (value == _search]]><xsl:value-of select="Name"/><![CDATA[) return;
                _search]]><xsl:value-of select="Name"/><![CDATA[ = value;
                RaisePropertyChanged("Search]]><xsl:value-of select="Name"/><![CDATA[");
            }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get { return ]]><xsl:value-of select="Entitys"/><![CDATA[List; }
            set
            {
                if (]]><xsl:value-of select="Entitys"/><![CDATA[List == value) return;
                ]]><xsl:value-of select="Entitys"/><![CDATA[List = value;
                RaisePropertyChanged("]]><xsl:value-of select="Table"/><![CDATA[List");

                //Clear Search-TextBox
                Search]]><xsl:value-of select="NameFK1"/><![CDATA[ = null;                                
                Search]]><xsl:value-of select="NameTK1"/><![CDATA[ = null;
                SearchCommentInfo = null;
                SearchRefExpertName = null;
                SearchRefSourceName = null;
                SearchRefAuthorName = null;
            }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get { return ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; }
            set
            {
                if (]]><xsl:value-of select="Entitys"/><![CDATA[AllList == value) return;
                ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value;
                RaisePropertyChanged("]]><xsl:value-of select="Table"/><![CDATA[AllList");
            }
        }

        #endregion "Public Properties"
   ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Properties Connect FK1++++++++'">
</xsl:when> 
<xsl:otherwise>
  <xsl:if test="TableFK1 !='NULL'">
       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["

        public new ICollectionView ]]><xsl:value-of select="BasissFK1"/><![CDATA[View;
        public new ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[
        {
            get
            {
                if (]]><xsl:value-of select="BasissFK1"/><![CDATA[View != null)
                    return ]]><xsl:value-of select="BasissFK1"/><![CDATA[View.CurrentItem as ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _search]]><xsl:value-of select="NameFK1"/><![CDATA[;
        public new string Search]]><xsl:value-of select="NameFK1"/><![CDATA[
        {
            get { return _search]]><xsl:value-of select="NameFK1"/><![CDATA[; }
            set
            {
                if (value == _search]]><xsl:value-of select="NameFK1"/><![CDATA[) return;
                _search]]><xsl:value-of select="NameFK1"/><![CDATA[ = value;
                RaisePropertyChanged("Search]]><xsl:value-of select="NameFK1"/><![CDATA[");
            }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List;
        public new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[List
        {
            get { return ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List; }
            set
            {
                if (]]><xsl:value-of select="EntitysFK1"/><![CDATA[List == value) return;
                ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List = value;
                RaisePropertyChanged("]]><xsl:value-of select="TableFK1"/><![CDATA[List");
            }
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Properties Connect FK2++++++++'">
</xsl:when> 
<xsl:otherwise>
  <xsl:if test="TableFK2 !='NULL'">
       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK2"/><![CDATA["

        public new ICollectionView ]]><xsl:value-of select="BasissFK2"/><![CDATA[View;
        public new ]]><xsl:value-of select="LinqModelFK2"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[
        {
            get
            {
                if (]]><xsl:value-of select="BasissFK2"/><![CDATA[View != null)
                    return ]]><xsl:value-of select="BasissFK2"/><![CDATA[View.CurrentItem as ]]><xsl:value-of select="LinqModelFK2"/><![CDATA[;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _search]]><xsl:value-of select="NameFK2"/><![CDATA[;
        public new string Search]]><xsl:value-of select="NameFK2"/><![CDATA[
        {
            get { return _search]]><xsl:value-of select="NameFK2"/><![CDATA[; }
            set
            {
                if (value == _search]]><xsl:value-of select="NameFK2"/><![CDATA[) return;
                _search]]><xsl:value-of select="NameFK2"/><![CDATA[ = value;
                RaisePropertyChanged("Search]]><xsl:value-of select="NameFK2"/><![CDATA[");
            }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List;
        public new ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[List
        {
            get { return ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List; }
            set
            {
                if (]]><xsl:value-of select="EntitysFK2"/><![CDATA[List == value) return;
                ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List = value;
                RaisePropertyChanged("]]><xsl:value-of select="TableFK2"/><![CDATA[List");
            }
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Properties Connect TK1++++++++'">
</xsl:when> 
<xsl:otherwise>
  <xsl:if test="TableTK1 !='NULL'">
       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelTK1"/><![CDATA["

        public ICollectionView ]]><xsl:value-of select="BasissTK1"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[
        {
            get
            {
                if (]]><xsl:value-of select="BasissTK1"/><![CDATA[View != null)
                    return ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.CurrentItem as ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _search]]><xsl:value-of select="NameTK1"/><![CDATA[;
        public string Search]]><xsl:value-of select="NameTK1"/><![CDATA[
        {
            get { return _search]]><xsl:value-of select="NameTK1"/><![CDATA[; }
            set
            {
                if (value == _search]]><xsl:value-of select="NameTK1"/><![CDATA[) return;
                _search]]><xsl:value-of select="NameTK1"/><![CDATA[ = value;
                RaisePropertyChanged("Search]]><xsl:value-of select="NameTK1"/><![CDATA[");
            }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> ]]><xsl:value-of select="EntitysTK1"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> ]]><xsl:value-of select="TableTK1"/><![CDATA[List
        {
            get { return ]]><xsl:value-of select="EntitysTK1"/><![CDATA[List; }
            set
            {
                if (]]><xsl:value-of select="EntitysTK1"/><![CDATA[List == value) return;
                ]]><xsl:value-of select="EntitysTK1"/><![CDATA[List = value;
                RaisePropertyChanged("]]><xsl:value-of select="TableTK1"/><![CDATA[List");
            }
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Properties Connect TK2++++++++'">
</xsl:when> 
<xsl:otherwise>
  <xsl:if test="TableTK2 !='NULL'">
       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelTK2"/><![CDATA["

        public ICollectionView ]]><xsl:value-of select="BasissTK2"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[ Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[
        {
            get
            {
                if (]]><xsl:value-of select="BasissTK2"/><![CDATA[View != null)
                    return ]]><xsl:value-of select="BasissTK2"/><![CDATA[View.CurrentItem as ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[;
                return null;
            }
        }
        //--------------------------------------------                                               

        private string _search]]><xsl:value-of select="NameTK2"/><![CDATA[;
        public string Search]]><xsl:value-of select="NameTK2"/><![CDATA[
        {
            get { return _search]]><xsl:value-of select="NameTK2"/><![CDATA[; }
            set
            {
                if (value == _search]]><xsl:value-of select="NameTK2"/><![CDATA[) return;
                _search]]><xsl:value-of select="NameTK2"/><![CDATA[ = value;
                RaisePropertyChanged("Search]]><xsl:value-of select="NameTK2"/><![CDATA[");
            }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> ]]><xsl:value-of select="EntitysTK2"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> ]]><xsl:value-of select="TableTK2"/><![CDATA[List
        {
            get { return ]]><xsl:value-of select="EntitysTK2"/><![CDATA[List; }
            set
            {
                if (]]><xsl:value-of select="EntitysTK2"/><![CDATA[List == value) return;
                ]]><xsl:value-of select="EntitysTK2"/><![CDATA[List = value;
                RaisePropertyChanged("]]><xsl:value-of select="TableTK2"/><![CDATA[List");
            }
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='+++Properties References ++++++++'">
</xsl:when> 
 <xsl:when test="Table ='Tbl03Regnums'">
   <![CDATA[  
        #region "Public Properties Tbl90Author"

        private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList;
        public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
        {
            get { return _tbl90AuthorsAllList; }
            set
            {
                if (_tbl90AuthorsAllList == value) return;
                _tbl90AuthorsAllList = value;
                RaisePropertyChanged("Tbl90AuthorsAllList");
            }
        }

        #endregion "Public Properties Tbl90Author"

        #region "Public Properties Tbl90Source"

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList;
        public ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
        {

            get { return _tbl90SourcesAllList; }
            set
            {
                if (_tbl90SourcesAllList == value) return;
                _tbl90SourcesAllList = value;
                RaisePropertyChanged("Tbl90SourcesAllList");
            }
        }

        #endregion "Public Properties Tbl90Sourcer"

        #region "Public Properties Tbl90Expert"

        private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList;
        public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
        {
            get { return _tbl90ExpertsAllList; }
            set
            {
                if (_tbl90ExpertsAllList == value) return;
                _tbl90ExpertsAllList = value;
                RaisePropertyChanged("Tbl90ExpertsAllList");
            }
        }

        #endregion "Public Properties Tbl90Sourcer"

        #region "Public Properties Tbl90RefAuthor"

        public ICollectionView RefAuthorsView;
        public Tbl90Reference CurrentTbl90RefAuthor
        {
            get
            {
                if (RefAuthorsView == null) return null;
                return RefAuthorsView.CurrentItem as Tbl90Reference;
            }
        }

        //--------------------------------------------

        private string _searchRefAuthorName;
        public string SearchRefAuthorName
        {
            get { return _searchRefAuthorName; }
            set
            {
                if (value == _searchRefAuthorName) return;
                _searchRefAuthorName = value;
                RaisePropertyChanged("SearchRefAuthorName");
            }
        }

        private ObservableCollection<Tbl90Reference> _tbl90RefAuthorsList;
        public ObservableCollection<Tbl90Reference> Tbl90RefAuthorsList
        {
            get { return _tbl90RefAuthorsList; }
            set
            {
                if (_tbl90RefAuthorsList == value) return;
                _tbl90RefAuthorsList = value;
                RaisePropertyChanged("Tbl90RefAuthorsList");
            }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90RefSource"

        public ICollectionView RefSourcesView;
        public Tbl90Reference CurrentTbl90RefSource
        {
            get
            {
                if (RefSourcesView != null)
                    return RefSourcesView.CurrentItem as Tbl90Reference;
                return null;
            }
        }

        //--------------------------------------------

        private string _searchRefSourceName;
        public string SearchRefSourceName
        {
            get { return _searchRefSourceName; }
            set
            {
                if (value == _searchRefSourceName) return;
                _searchRefSourceName = value;
                RaisePropertyChanged("SearchRefSourceName");
            }
        }

        private ObservableCollection<Tbl90Reference> _tbl90RefSourcesList;
        public ObservableCollection<Tbl90Reference> Tbl90RefSourcesList
        {
            get { return _tbl90RefSourcesList; }
            set
            {
                if (_tbl90RefSourcesList == value) return;
                _tbl90RefSourcesList = value;
                RaisePropertyChanged("Tbl90RefSourcesList");
            }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90RefExpert"

        public ICollectionView RefExpertsView;
        public Tbl90Reference CurrentTbl90RefExpert
        {
            get
            {
                if (RefExpertsView != null)
                    return RefExpertsView.CurrentItem as Tbl90Reference;
                return null;
            }
        }

        //--------------------------------------------

        private string _searchRefExpertName;
        public string SearchRefExpertName
        {
            get { return _searchRefExpertName; }
            set
            {
                if (value == _searchRefExpertName) return;
                _searchRefExpertName = value;
                RaisePropertyChanged("SearchRefExpertName");
            }
        }

        private ObservableCollection<Tbl90Reference> _tbl90RefExpertsList;
        public ObservableCollection<Tbl90Reference> Tbl90RefExpertsList
        {
            get { return _tbl90RefExpertsList; }
            set
            {
                if (_tbl90RefExpertsList == value) return;
                _tbl90RefExpertsList = value;
                RaisePropertyChanged("Tbl90RefExpertsList");
            }
        }

        #endregion "Public Properties"
   ]]>
</xsl:when>  
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='+++Properties Comments ++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">
   <![CDATA[  
        #region "Public Properties Tbl93Comment"

        public ICollectionView CommentsView;
        public Tbl93Comment CurrentTbl93Comment
        {
            get
            {
                if (CommentsView != null)
                    return CommentsView.CurrentItem as Tbl93Comment;
                return null;
            }
        }

        //--------------------------------------------

        private string _searchCommentInfo;
        public string SearchCommentInfo
        {
            get { return _searchCommentInfo; }
            set
            {
                if (value == _searchCommentInfo) return;
                _searchCommentInfo = value;
                RaisePropertyChanged("SearchCommentInfo");
            }
        }

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get { return _tbl93CommentsList; }
            set
            {
                if (_tbl93CommentsList == value) return;
                _tbl93CommentsList = value;
                RaisePropertyChanged("Tbl93CommentsList");
            }
        }

        #endregion "Public Properties"
   ]]>
</xsl:when>  
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='+++Private Methods ++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">
   <![CDATA[  
        #region "Private Methods"

        public void ]]><xsl:value-of select="Entity"/><![CDATA[View_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("Current]]><xsl:value-of select="LinqModel"/><![CDATA[");
        }

   ]]>
</xsl:when>  
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='+++Private Methods Connect FK1++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">
  <xsl:if test="TableFK1 !='NULL'">      <![CDATA[ 
        public void ]]><xsl:value-of select="EntityFK1"/><![CDATA[View_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[");
        }
  ]]> 
  </xsl:if> 
</xsl:when>  
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='+++Private Methods Connect TK1++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl03Regnums'">
  <xsl:if test="TableTK1 !='NULL'">       <![CDATA[ 
        public void ]]><xsl:value-of select="EntityTK1"/><![CDATA[View_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[");
        }
  ]]> 
  </xsl:if> 
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">      <![CDATA[  
        #region "Private Methods"

        public void ]]><xsl:value-of select="EntityFK2"/><![CDATA[View_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[");
        }

        public void ]]><xsl:value-of select="EntityTK1"/><![CDATA[View_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[");
        }
        #endregion "Private Methods"
   ]]>
</xsl:when>   
<xsl:otherwise>
  <xsl:if test="TableTK1 !='NULL'">       <![CDATA[ 
        #region "Private Methods"

        public void ]]><xsl:value-of select="EntityTK1"/><![CDATA[View_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[");
        }
        #endregion "Private Methods"
  ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='+++Private Methods Connect TK2++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">
  <xsl:if test="TableTK2 !='NULL'">       <![CDATA[ 
        public void ]]><xsl:value-of select="EntityTK2"/><![CDATA[View_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[");
        }
  ]]> 
  </xsl:if> 
</xsl:when>   
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='+++Private Methods Connect reference and Comment++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl03Regnums'">
   <![CDATA[  

        public void tbl90RefAuthorView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl90RefAuthor");
        }

        public void tbl90RefSourceView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl90RefSource");
        }

        public void tbl90RefExpertView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl90RefExpert");
        }

        public void tbl90AuthorView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl90Author");
        }

        public void tbl90SourceView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl90Source");
        }

        private void tbl90ExpertView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl90Expert");
        }

        public void tbl93CommentView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl93Comment");
        }

        #endregion "Private Methods"
   ]]>
</xsl:when>    
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose> 

   
    <![CDATA[}
}]]>   
</xsl:template>
</xsl:stylesheet>




