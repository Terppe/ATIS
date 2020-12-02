<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[  ]]>

<xsl:choose>
<xsl:when test="Table ='namespace+++++++++++++++++++++++++++++++++Delete+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:otherwise>   <![CDATA[ 
        //------------------------------Search with ]]><xsl:value-of select="Basis"/><![CDATA[Id----------------------------
        public ObservableCollection<]]><xsl:value-of select="TableTK1"/><![CDATA[> SearchForConnectedDatasetsWith]]><xsl:value-of select="Basis"/><![CDATA[IdInTable]]><xsl:value-of select="BasisTK1"/><![CDATA[(]]><xsl:value-of select="LinqModel"/><![CDATA[ selected)
        {
            var collection = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_uow.]]><xsl:value-of select="TableTK1"/><![CDATA[.Find(x => x.]]><xsl:value-of select="Basis"/><![CDATA[Id == selected.]]><xsl:value-of select="Basis"/><![CDATA[Id));
            return collection;
        }
        //-------------------------------Search with ]]><xsl:value-of select="BasisTK1"/><![CDATA[Id------------------------------------------------
        public ObservableCollection<Tbl21Class> SearchForConnectedDatasetsWithSuperclassIdInTableClass(Tbl18Superclass selected)
        {
            var collection = new ObservableCollection<Tbl21Class>(_uow.Tbl21Classes.Find(x => x.SuperclassId == selected.SuperclassId));
            return collection;
        }
  ]]>   
</xsl:otherwise>    
</xsl:choose> 


</xsl:template>
</xsl:stylesheet>








