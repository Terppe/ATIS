<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[using System.Windows.Controls; 

   //  ]]><xsl:value-of select="Table"/><![CDATA[View.xaml.cs Skriptdatum: ]]> <xsl:value-of select="DateTime"/>  <![CDATA[   

namespace ]]><xsl:value-of select="Namespace"/>.WpfUi.View <![CDATA[
{  

    /// <summary>
    /// Interactionslogic for ]]><xsl:value-of select="Table"/><![CDATA[View.xaml
    /// </summary>
    public partial class ]]><xsl:value-of select="Table"/><![CDATA[View : UserControl
   {

        public ]]><xsl:value-of select="Table"/><![CDATA[View()
       {         
            InitializeComponent();   
        }                                                                                                                                                                 
  }
}]]>   

</xsl:template>
</xsl:stylesheet>











