<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition">


<xsl:choose>
<xsl:when test="Table ='List Top  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>  
<xsl:when test="Table ='Tbl87Geographics'">     <![CDATA[ 
        #region ]]><xsl:value-of select="Table"/><![CDATA[  ---------------------------------------

        public ObservableCollection<Tbl87Geographic> GetValueTbl87GeographicsList(string searchGeographicInfo)
        {
            Tbl87GeographicsList = new ObservableCollection<Tbl87Geographic>
               (from x in _tbl87GeographicsRepository.GetAll()
                where x.Info.StartsWith(searchGeographicInfo) ]]>
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">     <![CDATA[ 
        #region ]]><xsl:value-of select="Table"/><![CDATA[  ---------------------------------------

        public ObservableCollection<Tbl81Image> GetValueTbl81ImagesList(string searchImageInfo)
        {
            Tbl81ImagesList = new ObservableCollection<Tbl81Image>
               (from x in _tbl81ImagesRepository.GetAll()
                where x.Info.StartsWith(searchImageInfo) ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">     <![CDATA[ 
        #region ]]><xsl:value-of select="Table"/><![CDATA[  ---------------------------------------

        public ObservableCollection<Tbl90Reference> GetValueTbl90RefAuthorsList(string searchRefAuthorName)
        {
            Tbl90RefAuthorsList = new ObservableCollection<Tbl90Reference>
                (from x in _tbl90ReferencesRepository.GetAll()
                 where x.Tbl90RefAuthors.RefAuthorName.StartsWith(searchRefAuthorName)
                     && x.Tbl90RefExperts == null
                     && x.Tbl90RefSources == null   ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">     <![CDATA[ 
        #region ]]><xsl:value-of select="Table"/><![CDATA[  ---------------------------------------

        public ObservableCollection<Tbl90Reference> GetValueTbl90RefExpertsList(string searchRefExpertName)
        {
            Tbl90RefExpertsList = new ObservableCollection<Tbl90Reference>
                (from x in tbl90ReferencesRepository.GetAll()
                 where x.Tbl90RefExperts.RefExpertName.StartsWith(searchRefExpertName)
                     && x.Tbl90RefSources == null
                     && x.Tbl90RefAuthors == null  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">     <![CDATA[ 
        #region ]]><xsl:value-of select="Table"/><![CDATA[  ---------------------------------------

        public ObservableCollection<Tbl90Reference> GetValueTbl90RefSourcesList(string searchRefSourceName)
        {
            Tbl90RefSourcesList = new ObservableCollection<Tbl90Reference>
                (from x in _tbl90ReferencesRepository.GetAll()
                 where x.Tbl90RefSources.RefSourceName.StartsWith(searchRefSourceName)
                     && x.Tbl90RefExperts == null
                     && x.Tbl90RefAuthors == null  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">     <![CDATA[ 
        #region ]]><xsl:value-of select="Table"/><![CDATA[  ---------------------------------------

        public ObservableCollection<Tbl93Comment> GetValueTbl93CommentsList(string searchCommentInfo)
        {
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>
               (from x in _tbl93CommentsRepository.GetAll()
                where x.Info.StartsWith(searchCommentInfo) ]]>
</xsl:when>
<xsl:when test="Table ='TblCountries'">     <![CDATA[ 
        #region ]]><xsl:value-of select="Table"/><![CDATA[  ---------------------------------------

        public ObservableCollection<TblCountry> GetValueTblCountriesList(string searchCountryName)
        {
            TblCountriesList = new ObservableCollection<TblCountry>
               (from x in _tblCountryRepository.GetAll()
                where x.Name.StartsWith(searchCountryName)   ]]>
</xsl:when>
<xsl:otherwise>  <![CDATA[ 
        #region ]]><xsl:value-of select="Table"/><![CDATA[  ---------------------------------------

        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> GetValue]]><xsl:value-of select="Table"/><![CDATA[List(string search]]><xsl:value-of select="Name"/><![CDATA[)
        {
            ]]> <xsl:value-of select="Table"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>
                (from x in ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.GetAll()
                 where x.]]><xsl:value-of select="Name"/><![CDATA[.StartsWith(search]]><xsl:value-of select="Name"/><![CDATA[) ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='List Middle  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">     <![CDATA[ 
                 orderby x.]]><xsl:value-of select="Name"/><![CDATA[, x.Subregnum   ]]>     
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">     <![CDATA[ 
                 orderby x.]]><xsl:value-of select="Name"/><![CDATA[, x.Subspeciesgroup   ]]>     
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">     <![CDATA[ 
                 orderby x.Tbl66Genusses.GenusName, x.]]><xsl:value-of select="Name"/><![CDATA[, x.Subspecies, x.Divers   ]]>     
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">     <![CDATA[ 
                 orderby x.Tbl66Genusses.GenusName, x.]]><xsl:value-of select="Name"/><![CDATA[, x.Subspecies, x.Divers   ]]>     
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">     <![CDATA[ 
                 orderby x.Info  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">     <![CDATA[ 
                 orderby x.Info  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">     <![CDATA[ 
                 orderby x.Tbl90RefAuthors.RefAuthorName, x.Tbl90RefAuthors.BookName, x.Tbl90RefAuthors.Page1  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">     <![CDATA[ 
                 orderby x.Tbl90RefExperts.RefExpertName, x.Tbl90RefExperts.Info ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">     <![CDATA[ 
                 orderby x.Tbl90RefSources.RefSourceName, x.Tbl90RefSources.Author, x.Tbl90RefSources.SourceYear  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">     <![CDATA[ 
                 orderby x.Info  ]]>
</xsl:when>
<xsl:when test="Table ='TblCountries'">     <![CDATA[ 
                 orderby x.Name  ]]>
</xsl:when>
<xsl:otherwise>  <![CDATA[ 
                 orderby x.]]><xsl:value-of select="Name"/><![CDATA[   ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='List Bottom   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>  
<xsl:otherwise>  <![CDATA[ 
                 select x);
            return ]]><xsl:value-of select="Table"/><![CDATA[List;
        }  ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='AllList Top  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>  
<xsl:when test="Table ='AllList Top  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>  
<xsl:when test="Table ='Tbl81Images'">     <![CDATA[ 
        public ObservableCollection<Tbl81Image> GetValueTbl81ImagesAllList()
        {
            Tbl81ImagesAllList = new ObservableCollection<Tbl81Image>
               (from z in _tbl81ImagesRepository.GetAll()  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">     <![CDATA[ 
        public ObservableCollection<Tbl87Geographic> GetValueTbl87GeographicsAllList()
        {
            Tbl87GeographicsAllList = new ObservableCollection<Tbl87Geographic>
               (from z in _tbl87GeographicsRepository.GetAll()  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">  
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">  
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">  
</xsl:when>
<xsl:when test="Table ='TblCountries'">     <![CDATA[ 
        public ObservableCollection<TblCountry> GetValueTblCountriesAllList()
        {
            TblCountriesAllList = new ObservableCollection<TblCountry>
               (from z in _tblCountryRepository.GetAll()   ]]>
</xsl:when>
<xsl:otherwise>  <![CDATA[ 
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> GetValue]]><xsl:value-of select="Table"/><![CDATA[AllList()
        {
            ]]> <xsl:value-of select="Table"/><![CDATA[AllList = new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>
                (from z in ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.GetAll()  ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='AllList Middle  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">     <![CDATA[ 
                 orderby z.]]><xsl:value-of select="Name"/><![CDATA[, z.Subregnum   ]]>     
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">     <![CDATA[ 
                 orderby z.]]><xsl:value-of select="Name"/><![CDATA[, z.Subspeciesgroup   ]]>     
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">     <![CDATA[ 
                 orderby z.Tbl66Genusses.GenusName, z.]]><xsl:value-of select="Name"/><![CDATA[, z.Subspecies, z.Divers   ]]>     
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">     <![CDATA[ 
                 orderby z.Tbl66Genusses.GenusName, z.]]><xsl:value-of select="Name"/><![CDATA[, z.Subspecies, z.Divers   ]]>     
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">     <![CDATA[ 
                 orderby z.Info  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">     <![CDATA[ 
                 orderby z.Info  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">    
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">  
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">  
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">     <![CDATA[ 
                 orderby z.Info  ]]>
</xsl:when>
<xsl:when test="Table ='TblCountries'">     <![CDATA[ 
                 orderby z.Name  ]]>
</xsl:when>
<xsl:otherwise>  <![CDATA[ 
                 orderby z.]]><xsl:value-of select="Name"/><![CDATA[ ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='AllList Bottom  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">    
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">  
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">  
</xsl:when>
<xsl:otherwise>  <![CDATA[ 
                 select z);
            return ]]> <xsl:value-of select="Table"/><![CDATA[AllList;
        }  ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='CurrentList Top  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>  
<xsl:when test="Table ='CurrentList Top  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">     <![CDATA[ 
        public ObservableCollection<Tbl90References> GetValueTbl90RefAuthorsList(int currentId)
        {
            Tbl90RefAuthorsList = new ObservableCollection<Tbl90References>
                (from y in _tbl90ReferencesRepository.GetAll()
                 where y.ReferenceID == currentId
                        && y.Tbl90RefExperts == null
                        && y.Tbl90RefSources == null  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">     <![CDATA[ 
        public ObservableCollection<Tbl90References> GetValueTbl90RefExpertsList(int currentId)          
        {
            Tbl90RefExpertsList = new ObservableCollection<Tbl90References>
                (from y in _tbl90ReferencesRepository.GetAll()
                 where y.ReferenceID == currentId
                        && y.Tbl90RefAuthors == null
                        && y.Tbl90RefSources == null  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">     <![CDATA[ 
        public ObservableCollection<Tbl90References> GetValueTbl90RefSourcesList(int currentId)
        {
            Tbl90RefSourcesList = new ObservableCollection<Tbl90References>
                (from y in _tbl90ReferencesRepository.GetAll()
                 where y.ReferenceID == currentId
                        && y.Tbl90RefExperts == null
                        && y.Tbl90RefAuthors == null  ]]>
</xsl:when>
<xsl:when test="Table ='TblCountries'">     <![CDATA[ 
        public ObservableCollection<TblCountry> GetValueTblCountriesList(int currentId)
        {
            TblCountriesList = new ObservableCollection<TblCountry>
                 (from y in tblCountryRepository.GetAll()
                  where y.CountryID == currentId  ]]>
</xsl:when>
<xsl:otherwise>  <![CDATA[ 
        public ObservableCollection<]]> <xsl:value-of select="LinqModel"/><![CDATA[> GetValue]]><xsl:value-of select="Table"/><![CDATA[List(int currentId)
        {
            ]]> <xsl:value-of select="Table"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>
                  (from y in ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.GetAll()
                   where y.]]><xsl:value-of select="ID"/><![CDATA[ == currentId ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='CurrentList Middle  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">     <![CDATA[ 
                 orderby y.]]><xsl:value-of select="Name"/><![CDATA[, y.Subregnum   ]]>     
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">     <![CDATA[ 
                 orderby y.]]><xsl:value-of select="Name"/><![CDATA[, y.Subspeciesgroup   ]]>     
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">     <![CDATA[ 
                 orderby y.Tbl66Genusses.GenusName, y.]]><xsl:value-of select="Name"/><![CDATA[, y.Subspecies, y.Divers   ]]>     
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">     <![CDATA[ 
                 orderby y.Tbl66Genusses.GenusName, y.]]><xsl:value-of select="Name"/><![CDATA[, y.Subspecies, y.Divers   ]]>     
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">     <![CDATA[ 
                 orderby y.Info  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">     <![CDATA[ 
                 orderby y.Info  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">     <![CDATA[ 
                 orderby y.Tbl90RefAuthors.RefAuthorName, y.Tbl90RefAuthors.BookName, y.Tbl90RefAuthors.Page1  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">     <![CDATA[ 
                 orderby y.Tbl90RefExperts.RefExpertName, y.Tbl90RefExperts.Info ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">     <![CDATA[ 
                 orderby y.Tbl90RefSources.RefSourceName, y.Tbl90RefSources.Author, y.Tbl90RefSources.SourceYear  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">     <![CDATA[ 
                 orderby y.Info  ]]>
</xsl:when>
<xsl:when test="Table ='TblCountries'">     <![CDATA[ 
                 orderby y.Name  ]]>
</xsl:when>
<xsl:otherwise>  <![CDATA[ 
                   orderby y.]]><xsl:value-of select="Name"/><![CDATA[ ]]>
</xsl:otherwise>    
</xsl:choose> 
<xsl:choose>
<xsl:when test="Table ='CurrentList Bottom  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>  
<xsl:otherwise>  <![CDATA[ 
                   select y);
            return ]]><xsl:value-of select="Table"/><![CDATA[List;
        } ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='LastList    ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>  
<xsl:when test="Table ='LastList    ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">     <![CDATA[ 
        public ObservableCollection<Tbl90References> GetValueTbl90RefAuthorsList()
        {
            Tbl90RefAuthorsList = new ObservableCollection<Tbl90References>
                { new ObservableCollection<Tbl90References>
                    (from x in _tbl90ReferencesRepository.GetAll()  select x).LastOrDefault()
                };
            return Tbl90RefAuthorsList;
        }

        #endregion  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">     <![CDATA[ 
        public ObservableCollection<Tbl90References> GetValueTbl90RefExpertsList()
        {
            Tbl90RefExpertsList = new ObservableCollection<Tbl90References>
                { new ObservableCollection<Tbl90References>
                    (from x in _tbl90ReferencesRepository.GetAll()  select x).LastOrDefault()
                };
            return Tbl90RefExpertsList;
        }

        #endregion  ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">     <![CDATA[ 
        public ObservableCollection<Tbl90References> GetValueTbl90RefSourcesList()
        {
            Tbl90RefSourcesList = new ObservableCollection<Tbl90References>
                { new ObservableCollection<Tbl90References>
                    (from x in tbl90ReferencesRepository.GetAll()  select x).LastOrDefault()
                };
            return Tbl90RefSourcesList;
        }

        #endregion  ]]>
</xsl:when>
<xsl:when test="Table ='TblCountries'">     <![CDATA[ 
        public ObservableCollection<TblCountry> GetValueTblCountriesList()
        {
            TblCountriesList = new ObservableCollection<TblCountry>
                { new ObservableCollection<TblCountry>
                    (from x in _tblCountryRepository.GetAll() select x).LastOrDefault()
                };
            return TblCountriesList;
        }

        #endregion   ]]>
</xsl:when>
<xsl:otherwise>  <![CDATA[ 
        public ObservableCollection<]]> <xsl:value-of select="LinqModel"/><![CDATA[> GetValue]]><xsl:value-of select="Table"/><![CDATA[List()
        {
            ]]> <xsl:value-of select="Table"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>
                { new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>
                    (from x in ]]><xsl:value-of select="Entitys"/><![CDATA[Repository.GetAll() select x).LastOrDefault()
                };
            return ]]><xsl:value-of select="Table"/><![CDATA[List;
        }

        #endregion  ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Tbl90  List+++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">     <![CDATA[ 
        #region Tbl90Authors  ---------------------------------------

        public ObservableCollection<Tbl90RefAuthors> GetValueTbl90AuthorsList(string searchRefAuthorName)
        {
            Tbl90AuthorsList = new ObservableCollection<Tbl90RefAuthors>
               (from x in _tbl90RefAuthorsRepository.GetAll()
                where x.RefAuthorName.StartsWith(searchRefAuthorName)
                orderby x.RefAuthorName, x.BookName, x.Page1
                select x);
            return Tbl90AuthorsList;
        }


        public ObservableCollection<Tbl90RefAuthors> GetValueTbl90AuthorsAllList()
        {
            Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthors>
                (from z in _tbl90RefAuthorsRepository.GetAll()
                 orderby z.RefAuthorName, z.BookName, z.Page1
                 select z);
            return Tbl90AuthorsAllList;
        }

        public ObservableCollection<Tbl90RefAuthors> GetValueTbl90AuthorsList(int currentId)
        {
            Tbl90AuthorsList = new ObservableCollection<Tbl90RefAuthors>
                 (from y in _tbl90RefAuthorsRepository.GetAll()
                  where y.RefAuthorID == currentId
                  orderby y.RefAuthorName, y.BookName, y.Page1
                  select y);
            return Tbl90AuthorsList;
        }

        public ObservableCollection<Tbl90RefAuthors> GetValueTbl90AuthorsList()
        {
            Tbl90AuthorsList = new ObservableCollection<Tbl90RefAuthors>
                { new ObservableCollection<Tbl90RefAuthors>
                    (from x in _tbl90RefAuthorsRepository.GetAll()  select x).LastOrDefault()
                };
            return Tbl90AuthorsList;
        }

        #endregion     ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">     <![CDATA[ 
        #region Tbl90Experts  ---------------------------------------

        public ObservableCollection<Tbl90RefExperts> GetValueTbl90ExpertsList(string searchRefExpertName)
        {
            Tbl90ExpertsList = new ObservableCollection<Tbl90RefExperts>
               (from x in _tbl90RefExpertsRepository.GetAll()
                where x.RefExpertName.StartsWith(searchRefExpertName)
                orderby x.RefExpertName, x.Info
                select x);
            return Tbl90ExpertsList;
        }

        public ObservableCollection<Tbl90RefExperts> GetValueTbl90ExpertsAllList()
        {
            Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExperts>
               (from z in _tbl90RefExpertsRepository.GetAll()
                orderby z.RefExpertName, z.Info
                select z);
            return Tbl90ExpertsAllList;
        }

        public ObservableCollection<Tbl90RefExperts> GetValueTbl90ExpertsList(int currentId)
        {
            Tbl90ExpertsList = new ObservableCollection<Tbl90RefExperts>
                 (from y in _tbl90RefExpertsRepository.GetAll()
                  where y.RefExpertID == currentId
                  orderby y.RefExpertName, y.Info
                  select y);
            return Tbl90ExpertsList;
        }

        public ObservableCollection<Tbl90RefExperts> GetValueTbl90ExpertsList()
        {
            Tbl90ExpertsList = new ObservableCollection<Tbl90RefExperts>
                { new ObservableCollection<Tbl90RefExperts>
                    (from x in _tbl90RefExpertsRepository.GetAll() select x).LastOrDefault()
                };
            return Tbl90ExpertsList;
        }

        #endregion          ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">     <![CDATA[ 
        #region Tbl90Sources  ---------------------------------------


        public ObservableCollection<Tbl90RefSources> GetValueTbl90SourcesList(string searchRefSourceName)
        {
            Tbl90SourcesList = new ObservableCollection<Tbl90RefSources>
               (from x in _tbl90RefSourcesRepository.GetAll()
                where x.RefSourceName.StartsWith(searchRefSourceName)
                orderby x.RefSourceName, x.Author, x.SourceYear
                select x);
            return Tbl90SourcesList;
        }

        public ObservableCollection<Tbl90RefSources> GetValueTbl90SourcesAllList()
        {
            Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSources>
               (from z in _tbl90RefSourcesRepository.GetAll()
                orderby z.RefSourceName, z.Author, z.SourceYear
                select z);
            return Tbl90SourcesAllList;
        }

        public ObservableCollection<Tbl90RefSources> GetValueTbl90SourcesList(int currentId)
        {
            Tbl90SourcesList = new ObservableCollection<Tbl90RefSources>
                 (from y in _tbl90RefSourcesRepository.GetAll()
                  where y.RefSourceID == currentId
                  orderby y.RefSourceName, y.Author, y.SourceYear
                  select y);
            return Tbl90SourcesList;
        }

        public ObservableCollection<Tbl90RefSources> GetValueTbl90SourcesList()
        {
            Tbl90SourcesList = new ObservableCollection<Tbl90RefSources>
                { new ObservableCollection<Tbl90RefSources>
                    (from x in _tbl90RefSourcesV.GetAll() select x).LastOrDefault()
                };
            return Tbl90SourcesList;
        }

        #endregion    ]]>
</xsl:when>
<xsl:otherwise>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Property  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">     <![CDATA[ 
        #region "Public Property  Tbl90RefAuthor"

        private ObservableCollection< Tbl90Reference>  _tbl90RefAuthorsList;
        public ObservableCollection< Tbl90Reference>  Tbl90RefAuthorsList
        {
            get { return  _tbl90RefAuthorsList; }
            set {  _tbl90RefAuthorsList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"     ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">     <![CDATA[ 
        #region "Public Property  Tbl90RefExpert"

        private ObservableCollection<Tbl90Reference> _tbl90RefExpertsList;
        public ObservableCollection<Tbl90Reference> Tbl90RefExpertsList
        {
            get { return _tbl90RefExpertsList; }
            set { _tbl90RefExpertsList = value; RaisePropertyChanged(); }
        }


        #endregion "Public Properties"    ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">     <![CDATA[ 
        #region "Public Property  Tbl90RefSource"

        private ObservableCollection<Tbl90Reference> _tbl90RefSourcesList;
        public ObservableCollection<Tbl90Reference> Tbl90RefSourcesList
        {
            get { return _tbl90RefSourcesList; }
            set { _tbl90RefSourcesList = value; RaisePropertyChanged(); }
        }


        #endregion "Public Properties"     ]]>
</xsl:when>
<xsl:when test="Table ='TblCountries'">     <![CDATA[ 
        #region "Public Property  TblCountry"

        private ObservableCollection<TblCountry> _tblCountriesList;
        public ObservableCollection<TblCountry> TblCountriesList
        {
            get { return _tblCountriesList; }
            set { _tblCountriesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<TblCountry> _tblCountriesAllList;
        public ObservableCollection<TblCountry> TblCountriesAllList
        {
            get { return _tblCountriesAllList; }
            set { _tblCountriesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"  ]]>
</xsl:when>
<xsl:otherwise>  <![CDATA[ 
        #region "Public Property ]]> <xsl:value-of select="LinqModel"/><![CDATA["

        private ObservableCollection<]]> <xsl:value-of select="LinqModel"/><![CDATA[> ]]> <xsl:value-of select="Entitys"/><![CDATA[List;
        public ObservableCollection<]]> <xsl:value-of select="LinqModel"/><![CDATA[> ]]> <xsl:value-of select="Table"/><![CDATA[List
        {
            get { return ]]> <xsl:value-of select="Entitys"/><![CDATA[List; }
            set { ]]> <xsl:value-of select="Entitys"/><![CDATA[List = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<]]> <xsl:value-of select="LinqModel"/><![CDATA[> ]]> <xsl:value-of select="Entitys"/><![CDATA[AllList;
        public  ObservableCollection<]]> <xsl:value-of select="LinqModel"/><![CDATA[> ]]> <xsl:value-of select="Table"/><![CDATA[AllList
        {
            get { return ]]> <xsl:value-of select="Entitys"/><![CDATA[AllList; }
            set { ]]> <xsl:value-of select="Entitys"/><![CDATA[AllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties" ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Tbl90  Properties+++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">     <![CDATA[ 
        #region "Public Properties Tbl90Author"

        private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsList;
        public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsList
        {
            get { return _tbl90AuthorsList; }
            set { _tbl90AuthorsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList;
        public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
        {
            get { return _tbl90AuthorsAllList; }
            set { _tbl90AuthorsAllList = value; RaisePropertyChanged(); }
        }

        #endregion     ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">     <![CDATA[ 
        #region "Public Property  Tbl90Expert"

        private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsList;
        public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsList
        {
            get { return _tbl90ExpertsList; }
            set { _tbl90ExpertsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList;
        public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
        {
            get { return _tbl90ExpertsAllList; }
            set { _tbl90ExpertsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"    ]]>
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">     <![CDATA[ 
        #region "Public Property  Tbl90RefSource"

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesList;
        public ObservableCollection<Tbl90RefSource> Tbl90SourcesList
        {
            get { return _tbl90SourcesList; }
            set { _tbl90SourcesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList;
        public ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
        {
            get { return _tbl90SourcesAllList; }
            set { _tbl90SourcesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"    ]]>
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

