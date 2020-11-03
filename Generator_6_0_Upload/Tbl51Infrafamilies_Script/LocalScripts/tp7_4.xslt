<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[  ]]>
   

<xsl:choose>
<xsl:when test="Table ='++++++Abgeleitet von++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:otherwise>   <![CDATA[ 
        public ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> SearchForConnectedDatasetsWith]]><xsl:value-of select="Basis"/><![CDATA[IdInTable]]><xsl:value-of select="BasisTK1"/><![CDATA[(]]><xsl:value-of select="LinqModel"/><![CDATA[ selected)
        {
            var collection = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_uow.]]><xsl:value-of select="TableTK1"/><![CDATA[.Find(x => x.]]><xsl:value-of select="Basis"/><![CDATA[Id == selected.]]><xsl:value-of select="Basis"/><![CDATA[Id));
            return collection;
        }


        public ObservableCollection<Tbl90Reference> DeleteDatasetsWith]]><xsl:value-of select="Basis"/><![CDATA[IdInTableReference(]]><xsl:value-of select="LinqModel"/><![CDATA[ selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.]]><xsl:value-of select="Basis"/><![CDATA[Id == selected.]]><xsl:value-of select="Basis"/><![CDATA[Id));
            return collection;
        }

        public ObservableCollection<Tbl93Comment> DeleteDatasetsWith]]><xsl:value-of select="Basis"/><![CDATA[IdInTableComment(]]><xsl:value-of select="LinqModel"/><![CDATA[ selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.]]><xsl:value-of select="Basis"/><![CDATA[Id == selected.]]><xsl:value-of select="Basis"/><![CDATA[Id));
            return collection;
        }

        public void Delete]]><xsl:value-of select="Basis"/><![CDATA[(]]><xsl:value-of select="LinqModel"/><![CDATA[ selected)
        {
            _uow.]]><xsl:value-of select="Table"/><![CDATA[.Remove(selected);
            _uow.Complete();
        }

        public ObservableCollection<T> Get]]><xsl:value-of select="BasissFK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="BasisFK1"/><![CDATA[Id<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.]]><xsl:value-of select="TableFK1"/><![CDATA[
                .Where(e => e.]]><xsl:value-of select="BasisFK1"/><![CDATA[Id == id)
                .OrderBy(k => k.]]><xsl:value-of select="BasisFK1"/><![CDATA[Name));

            return collection;
        }

        public ObservableCollection<T> Get]]><xsl:value-of select="Basiss"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.]]><xsl:value-of select="Table"/><![CDATA[
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id)
                .OrderBy(k => k.]]><xsl:value-of select="Basis"/><![CDATA[Name));
            return collection;
        }

        public ObservableCollection<T> Get]]><xsl:value-of select="BasissTK1"/><![CDATA[CollectionOrderByFromSubphylumId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.]]><xsl:value-of select="TableTK1"/><![CDATA[
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id)
                .OrderBy(k => k.]]><xsl:value-of select="BasisTK1"/><![CDATA[Name));
            return collection;
        }

        public ObservableCollection<T> GetReferenceExpertsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefSourceIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }

        public ObservableCollection<T> GetReferenceSourcesCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefExpertIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }

        public ObservableCollection<T> GetReferenceAuthorsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefSourceIdIsNullAndRefExpertIdIsNull<T>(int id)   
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }

        public ObservableCollection<T> GetCommentsCollectionOrderByFromSubphylumId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id)
                .OrderBy(k => k.Info));
            return collection;
        }

   ]]>
</xsl:otherwise>    
</xsl:choose> 




</xsl:template>
</xsl:stylesheet>










