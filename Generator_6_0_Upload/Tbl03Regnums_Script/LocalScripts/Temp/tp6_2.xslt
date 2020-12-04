<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[using System; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;  ]]>

<xsl:choose>
<xsl:when test="Table ='namespace++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'"> 
</xsl:when>  
<xsl:otherwise>  <![CDATA[ 
using ATIS.Ui.Views.Report.]]><xsl:value-of select="LayoutTK1"/><![CDATA[;    ]]>   
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='namespace++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:otherwise>   <![CDATA[ 
using MahApps.Metro.Controls;

   //  Report]]><xsl:value-of select="Basis"/><![CDATA[Window.xaml.cs Skriptdatum: ]]> <xsl:value-of select="DateTime"/><![CDATA[      

namespace ATIS.Ui.Views.Report.]]><xsl:value-of select="Layout"/><![CDATA[
{  
    /// <summary>
    /// Interactionslogic for Report]]><xsl:value-of select="Basis"/><![CDATA[Window.xaml
    /// </summary>
    public partial class Report]]><xsl:value-of select="Basis"/><![CDATA[Window : MetroWindow
   {

        public Report]]><xsl:value-of select="Basis"/><![CDATA[Window(int un, string tab)
       {         
            //      Mouse.OverrideCursor = Cursors.Wait;

            DataContext = new ReportViewModel(un, tab);
            InitializeComponent();   

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //    WindowStartupLocation = WindowStartupLocation.Manual;

            //       Left = Settings.Default.Left + (Settings.Default.Width / 2) - (Width / 2);
            //       Top = Settings.Default.Top + (Settings.Default.Height / 2) - (Height / 2);
        }   

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintDocument(((IDocumentPaginatorSource)FlowDocument).DocumentPaginator,
                    "Flow Document Print Job");
            }
        } 

        private void Reader_LostFocus(object sender, RoutedEventArgs e)
        {
            Width = Reader.Width + 20;
        }   ]]>   
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Main Header ++++++++++++++++'"> 
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">   <![CDATA[ 
                  //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportPhylumWindow(id, "Tbl06Phylums");
            rp.Show();
        }
                   // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportDivisionWindow(id, "Tbl09Divisions");
            rp.Show();
        }  ]]>   
</xsl:when>  
<xsl:when test="Table ='Tbl06Phylums'">   <![CDATA[ 
                   //Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportRegnumWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                   //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportSubphylumWindow(id, "Tbl12Subphylums");
            rp.Show();
        }
  ]]>   
</xsl:when>  
<xsl:when test="Table ='Tbl09Divisions'">   <![CDATA[ 
                   // Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportRegnumWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                   // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportSubdivisionWindow(id, "Tbl09Divisions");
            rp.Show();
        }
  ]]>   
</xsl:when>  
<xsl:when test="Table ='Tbl12Subphylums'">   <![CDATA[ 
                   //Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportRegnumWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                  //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportPhylumWindow(id, "Tbl06Phylums");
            rp.Show();
        }

                   // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportSuperclassWindow(id, "Tbl18Superclasses");
            rp.Show();
        }
  ]]>   
</xsl:when>  
<xsl:when test="Table ='Tbl15Subdivisions'">   <![CDATA[ 
                  //  Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportRegnumWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                  // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportDivisionWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportSuperclassWindow(id, "Tbl18Superclasses");
            rp.Show();
        }
  ]]>   
</xsl:when>  

<xsl:when test="Table ='Tbl18Superclasses'">   <![CDATA[ 
                  //  Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportRegnumWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                  //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportPhylumWindow(id, "Tbl06Phylums");
            rp.Show();
        }

                  // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportDivisionWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportSubphylumWindow(id, "Tbl12Subphylums");
            rp.Show();
        }

                   // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportSubdivisionWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   // Tbl21Classes  -->
        private void HyperlinkClass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportClassWindow(id, "Tbl21Classes");
            rp.Show();
        }
  ]]>   
</xsl:when>  

<xsl:when test="Table ='Tbl21Classes'">   <![CDATA[ 
                  //  Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportTbl03RegnumsWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                  //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl06PhylumsWindow(id, "Tbl06Phylums");
            rp.Show();
        }

                  // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl09DivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl12SubphylumsWindow(id, "Tbl12Subphylums");
            rp.Show();
        }

                   // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl15SubdivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl18SuperclassesWindow(id, "Tbl18Superclasses");
            rp.Show();
        }

                   // Tbl24Subclasses  -->
        private void HyperlinkSubclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl24SubclassesWindow(id, "Tbl24Subclasses");
            rp.Show();
        }
  ]]>   
</xsl:when>  
<xsl:when test="Table ='Tbl24Subclasses'">   <![CDATA[ 
                  //  Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportTbl03RegnumsWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                  //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl06PhylumsWindow(id, "Tbl06Phylums");
            rp.Show();
        }

                  // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl09DivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl12SubphylumsWindow(id, "Tbl12Subphylums");
            rp.Show();
        }

                   // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl15SubdivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl18SuperclassesWindow(id, "Tbl18Superclasses");
            rp.Show();
        }

                   // Tbl21Classes  -->
        private void HyperlinkClass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl21ClassesWindow(id, "Tbl21Classes");
            rp.Show();
        }

                   // Tbl27Infraclasses  -->
        private void HyperlinkInfraclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl27InfraclassesWindow(id, "Tbl27Infraclasses");
            rp.Show();
        }
  ]]>   
</xsl:when>  
<xsl:when test="Table ='Tbl27Infraclasses'">   <![CDATA[ 
                  //  Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportTbl03RegnumsWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                  //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl06PhylumsWindow(id, "Tbl06Phylums");
            rp.Show();
        }

                  // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl09DivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl12SubphylumsWindow(id, "Tbl12Subphylums");
            rp.Show();
        }

                   // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl15SubdivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl18SuperclassesWindow(id, "Tbl18Superclasses");
            rp.Show();
        }

                   // Tbl21Classes  -->
        private void HyperlinkClass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl21ClassesWindow(id, "Tbl21Classes");
            rp.Show();
        }

                   // Tbl24Subclasses  -->
        private void HyperlinkSubclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl24SubclassesWindow(id, "Tbl24Subclasses");
            rp.Show();
        }

                   // Tbl30Legios  -->
        private void HyperlinkLegio_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl30LegiosWindow(id, "Tbl30Legios");
            rp.Show();
        }
  ]]>   
</xsl:when>  
<xsl:when test="Table ='Tbl30Legios'">   <![CDATA[ 
                  //  Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportTbl03RegnumsWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                  //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl06PhylumsWindow(id, "Tbl06Phylums");
            rp.Show();
        }

                  // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl09DivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl12SubphylumsWindow(id, "Tbl12Subphylums");
            rp.Show();
        }

                   // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl15SubdivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl18SuperclassesWindow(id, "Tbl18Superclasses");
            rp.Show();
        }

                   // Tbl21Classes  -->
        private void HyperlinkClass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl21ClassesWindow(id, "Tbl21Classes");
            rp.Show();
        }

                   // Tbl24Subclasses  -->
        private void HyperlinkSubclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl24SubclassesWindow(id, "Tbl24Subclasses");
            rp.Show();
        }

                   // Tbl27Infraclasses  -->
        private void HyperlinkInfraclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl27InfraclassesWindow(id, "Tbl27Infraclasses");
            rp.Show();
        }
                   // Tbl33Ordos  -->
        private void HyperlinkOrdo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl33OrdosWindow(id, "Tbl33Ordos");
            rp.Show();
        }
  ]]>   
</xsl:when>  
<xsl:when test="Table ='Tbl33Ordos'">   <![CDATA[ 
                  //  Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportTbl03RegnumsWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                  //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl06PhylumsWindow(id, "Tbl06Phylums");
            rp.Show();
        }

                  // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl09DivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl12SubphylumsWindow(id, "Tbl12Subphylums");
            rp.Show();
        }

                   // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl15SubdivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl18SuperclassesWindow(id, "Tbl18Superclasses");
            rp.Show();
        }

                   // Tbl21Classes  -->
        private void HyperlinkClass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl21ClassesWindow(id, "Tbl21Classes");
            rp.Show();
        }

                   // Tbl24Subclasses  -->
        private void HyperlinkSubclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl24SubclassesWindow(id, "Tbl24Subclasses");
            rp.Show();
        }

                   // Tbl27Infraclasses  -->
        private void HyperlinkInfraclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl27InfraclassesWindow(id, "Tbl27Infraclasses");
            rp.Show();
        }
                   // Tbl30Legios  -->
        private void HyperlinkLegio_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl30LegiosWindow(id, "Tbl30Legios");
            rp.Show();
        }
                   // Tbl36Subordos  -->
        private void HyperlinkSubordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl36SubordosWindow(id, "Tbl36Subordos");
            rp.Show();
        }
  ]]>   
</xsl:when>  
<xsl:when test="Table ='Tbl36Subordos'">   <![CDATA[ 
                  //  Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportTbl03RegnumsWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                  //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl06PhylumsWindow(id, "Tbl06Phylums");
            rp.Show();
        }

                  // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl09DivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl12SubphylumsWindow(id, "Tbl12Subphylums");
            rp.Show();
        }

                   // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl15SubdivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl18SuperclassesWindow(id, "Tbl18Superclasses");
            rp.Show();
        }

                   // Tbl21Classes  -->
        private void HyperlinkClass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl21ClassesWindow(id, "Tbl21Classes");
            rp.Show();
        }

                   // Tbl24Subclasses  -->
        private void HyperlinkSubclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl24SubclassesWindow(id, "Tbl24Subclasses");
            rp.Show();
        }

                   // Tbl27Infraclasses  -->
        private void HyperlinkInfraclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl27InfraclassesWindow(id, "Tbl27Infraclasses");
            rp.Show();
        }
                   // Tbl30Legios  -->
        private void HyperlinkLegio_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl30LegiosWindow(id, "Tbl30Legios");
            rp.Show();
        }
                   // Tbl33Ordos  -->
        private void HyperlinkOrdo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl33OrdosWindow(id, "Tbl33Ordos");
            rp.Show();
        }
                   // Tbl39Infraordos  -->
        private void HyperlinkInfraordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl39InfraordosWindow(id, "Tbl39Infraordos");
            rp.Show();
        }
  ]]>   
</xsl:when>  
<xsl:when test="Table ='Tbl39Infraordos'">   <![CDATA[ 
                  //  Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportTbl03RegnumsWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                  //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl06PhylumsWindow(id, "Tbl06Phylums");
            rp.Show();
        }

                  // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl09DivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl12SubphylumsWindow(id, "Tbl12Subphylums");
            rp.Show();
        }

                   // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl15SubdivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl18SuperclassesWindow(id, "Tbl18Superclasses");
            rp.Show();
        }

                   // Tbl21Classes  -->
        private void HyperlinkClass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl21ClassesWindow(id, "Tbl21Classes");
            rp.Show();
        }

                   // Tbl24Subclasses  -->
        private void HyperlinkSubclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl24SubclassesWindow(id, "Tbl24Subclasses");
            rp.Show();
        }

                   // Tbl27Infraclasses  -->
        private void HyperlinkInfraclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl27InfraclassesWindow(id, "Tbl27Infraclasses");
            rp.Show();
        }
                   // Tbl30Legios  -->
        private void HyperlinkLegio_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl30LegiosWindow(id, "Tbl30Legios");
            rp.Show();
        }
                   // Tbl33Ordos  -->
        private void HyperlinkOrdo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl33OrdosWindow(id, "Tbl33Ordos");
            rp.Show();
        }
                   // Tbl36Subordos  -->
        private void HyperlinkSubordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl36SubordosWindow(id, "Tbl36Subordos");
            rp.Show();
        }
                   // Tbl42Superfamilies  -->
        private void HyperlinkSuperfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl42SuperfamiliesWindow(id, "Tbl42Superfamilies");
            rp.Show();
        }
  ]]>   
</xsl:when>  
<xsl:when test="Table ='Tbl42Superfamilies'">   <![CDATA[ 
                  //  Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportTbl03RegnumsWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                  //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl06PhylumsWindow(id, "Tbl06Phylums");
            rp.Show();
        }

                  // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl09DivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl12SubphylumsWindow(id, "Tbl12Subphylums");
            rp.Show();
        }

                   // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl15SubdivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl18SuperclassesWindow(id, "Tbl18Superclasses");
            rp.Show();
        }

                   // Tbl21Classes  -->
        private void HyperlinkClass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl21ClassesWindow(id, "Tbl21Classes");
            rp.Show();
        }

                   // Tbl24Subclasses  -->
        private void HyperlinkSubclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl24SubclassesWindow(id, "Tbl24Subclasses");
            rp.Show();
        }

                   // Tbl27Infraclasses  -->
        private void HyperlinkInfraclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl27InfraclassesWindow(id, "Tbl27Infraclasses");
            rp.Show();
        }
                   // Tbl30Legios  -->
        private void HyperlinkLegio_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl30LegiosWindow(id, "Tbl30Legios");
            rp.Show();
        }
                   // Tbl33Ordos  -->
        private void HyperlinkOrdo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl33OrdosWindow(id, "Tbl33Ordos");
            rp.Show();
        }
                   // Tbl36Subordos  -->
        private void HyperlinkSubordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl36SubordosWindow(id, "Tbl36Subordos");
            rp.Show();
        }
                   // Tbl39Infraordos  -->
        private void HyperlinkInfraordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl39InfraordosWindow(id, "Tbl39Infraordos");
            rp.Show();
        }
                   // Tbl45Families  -->
        private void HyperlinkFamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl45FamiliesWindow(id, "Tbl45Families");
            rp.Show();
        }
  ]]>   
</xsl:when>  
<xsl:when test="Table ='Tbl45Families'">   <![CDATA[ 
                  //  Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportTbl03RegnumsWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                  //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl06PhylumsWindow(id, "Tbl06Phylums");
            rp.Show();
        }

                  // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl09DivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl12SubphylumsWindow(id, "Tbl12Subphylums");
            rp.Show();
        }

                   // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl15SubdivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl18SuperclassesWindow(id, "Tbl18Superclasses");
            rp.Show();
        }

                   // Tbl21Classes  -->
        private void HyperlinkClass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl21ClassesWindow(id, "Tbl21Classes");
            rp.Show();
        }

                   // Tbl24Subclasses  -->
        private void HyperlinkSubclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl24SubclassesWindow(id, "Tbl24Subclasses");
            rp.Show();
        }

                   // Tbl27Infraclasses  -->
        private void HyperlinkInfraclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl27InfraclassesWindow(id, "Tbl27Infraclasses");
            rp.Show();
        }
                   // Tbl30Legios  -->
        private void HyperlinkLegio_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl30LegiosWindow(id, "Tbl30Legios");
            rp.Show();
        }
                   // Tbl33Ordos  -->
        private void HyperlinkOrdo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl33OrdosWindow(id, "Tbl33Ordos");
            rp.Show();
        }
                   // Tbl36Subordos  -->
        private void HyperlinkSubordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl36SubordosWindow(id, "Tbl36Subordos");
            rp.Show();
        }
                   // Tbl39Infraordos  -->
        private void HyperlinkInfraordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl39InfraordosWindow(id, "Tbl39Infraordos");
            rp.Show();
        }
                   // Tbl42Superfamilies  -->
        private void HyperlinkSuperfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl42SuperfamiliesWindow(id, "Tbl42Superfamilies");
            rp.Show();
        }
                   // Tbl48Subfamilies  -->
        private void HyperlinkSubfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl48SubfamiliesWindow(id, "Tbl48Subfamilies");
            rp.Show();
        }
  ]]>   
</xsl:when>  
<xsl:when test="Table ='Tbl48Subfamilies'">   <![CDATA[ 
                  //  Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportTbl03RegnumsWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                  //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl06PhylumsWindow(id, "Tbl06Phylums");
            rp.Show();
        }

                  // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl09DivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl12SubphylumsWindow(id, "Tbl12Subphylums");
            rp.Show();
        }

                   // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl15SubdivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl18SuperclassesWindow(id, "Tbl18Superclasses");
            rp.Show();
        }

                   // Tbl21Classes  -->
        private void HyperlinkClass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl21ClassesWindow(id, "Tbl21Classes");
            rp.Show();
        }

                   // Tbl24Subclasses  -->
        private void HyperlinkSubclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl24SubclassesWindow(id, "Tbl24Subclasses");
            rp.Show();
        }

                   // Tbl27Infraclasses  -->
        private void HyperlinkInfraclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl27InfraclassesWindow(id, "Tbl27Infraclasses");
            rp.Show();
        }
                   // Tbl30Legios  -->
        private void HyperlinkLegio_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl30LegiosWindow(id, "Tbl30Legios");
            rp.Show();
        }
                   // Tbl33Ordos  -->
        private void HyperlinkOrdo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl33OrdosWindow(id, "Tbl33Ordos");
            rp.Show();
        }
                   // Tbl36Subordos  -->
        private void HyperlinkSubordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl36SubordosWindow(id, "Tbl36Subordos");
            rp.Show();
        }
                   // Tbl39Infraordos  -->
        private void HyperlinkInfraordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl39InfraordosWindow(id, "Tbl39Infraordos");
            rp.Show();
        }
                   // Tbl42Superfamilies  -->
        private void HyperlinkSuperfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl42SuperfamiliesWindow(id, "Tbl42Superfamilies");
            rp.Show();
        }
                   // Tbl45Families  -->
        private void HyperlinkFamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl45FamiliesWindow(id, "Tbl45Families");
            rp.Show();
        }
                   // Tbl51Infrafamilies  -->
        private void HyperlinkInfrafamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl51InfrafamiliesWindow(id, "Tbl51Infrafamilies");
            rp.Show();
        }
  ]]>   
</xsl:when>  
<xsl:when test="Table ='Tbl51Infrafamilies'">   <![CDATA[ 
                  //  Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportTbl03RegnumsWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                  //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl06PhylumsWindow(id, "Tbl06Phylums");
            rp.Show();
        }

                  // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl09DivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl12SubphylumsWindow(id, "Tbl12Subphylums");
            rp.Show();
        }

                   // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl15SubdivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl18SuperclassesWindow(id, "Tbl18Superclasses");
            rp.Show();
        }

                   // Tbl21Classes  -->
        private void HyperlinkClass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl21ClassesWindow(id, "Tbl21Classes");
            rp.Show();
        }

                   // Tbl24Subclasses  -->
        private void HyperlinkSubclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl24SubclassesWindow(id, "Tbl24Subclasses");
            rp.Show();
        }

                   // Tbl27Infraclasses  -->
        private void HyperlinkInfraclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl27InfraclassesWindow(id, "Tbl27Infraclasses");
            rp.Show();
        }
                   // Tbl30Legios  -->
        private void HyperlinkLegio_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl30LegiosWindow(id, "Tbl30Legios");
            rp.Show();
        }
                   // Tbl33Ordos  -->
        private void HyperlinkOrdo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl33OrdosWindow(id, "Tbl33Ordos");
            rp.Show();
        }
                   // Tbl36Subordos  -->
        private void HyperlinkSubordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl36SubordosWindow(id, "Tbl36Subordos");
            rp.Show();
        }
                   // Tbl39Infraordos  -->
        private void HyperlinkInfraordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl39InfraordosWindow(id, "Tbl39Infraordos");
            rp.Show();
        }
                   // Tbl42Superfamilies  -->
        private void HyperlinkSuperfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl42SuperfamiliesWindow(id, "Tbl42Superfamilies");
            rp.Show();
        }
                   // Tbl45Families  -->
        private void HyperlinkFamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl45FamiliesWindow(id, "Tbl45Families");
            rp.Show();
        }
                   // Tbl48Subfamilies  -->
        private void HyperlinkSubfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl48SubfamiliesWindow(id, "Tbl48Subfamilies");
            rp.Show();
        }
                   // Tbl54Supertribusses
        private void HyperlinkSupertribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl54SupertribussesWindow(id, "Tbl54Supertribusses");
            rp.Show();
        }
  ]]>   
</xsl:when>  
<xsl:when test="Table ='Tbl54Supertribusses'">   <![CDATA[ 
                  //  Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportTbl03RegnumsWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                  //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl06PhylumsWindow(id, "Tbl06Phylums");
            rp.Show();
        }

                  // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl09DivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl12SubphylumsWindow(id, "Tbl12Subphylums");
            rp.Show();
        }

                   // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl15SubdivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl18SuperclassesWindow(id, "Tbl18Superclasses");
            rp.Show();
        }

                   // Tbl21Classes  -->
        private void HyperlinkClass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl21ClassesWindow(id, "Tbl21Classes");
            rp.Show();
        }

                   // Tbl24Subclasses  -->
        private void HyperlinkSubclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl24SubclassesWindow(id, "Tbl24Subclasses");
            rp.Show();
        }

                   // Tbl27Infraclasses  -->
        private void HyperlinkInfraclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl27InfraclassesWindow(id, "Tbl27Infraclasses");
            rp.Show();
        }
                   // Tbl30Legios  -->
        private void HyperlinkLegio_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl30LegiosWindow(id, "Tbl30Legios");
            rp.Show();
        }
                   // Tbl33Ordos  -->
        private void HyperlinkOrdo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl33OrdosWindow(id, "Tbl33Ordos");
            rp.Show();
        }
                   // Tbl36Subordos  -->
        private void HyperlinkSubordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl36SubordosWindow(id, "Tbl36Subordos");
            rp.Show();
        }
                   // Tbl39Infraordos  -->
        private void HyperlinkInfraordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl39InfraordosWindow(id, "Tbl39Infraordos");
            rp.Show();
        }
                   // Tbl42Superfamilies  -->
        private void HyperlinkSuperfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl42SuperfamiliesWindow(id, "Tbl42Superfamilies");
            rp.Show();
        }
                   // Tbl45Families  -->
        private void HyperlinkFamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl45FamiliesWindow(id, "Tbl45Families");
            rp.Show();
        }
                   // Tbl48Subfamilies  -->
        private void HyperlinkSubfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl48SubfamiliesWindow(id, "Tbl48Subfamilies");
            rp.Show();
        }
                   // Tbl51Infrafamilies  -->
        private void HyperlinkInfrafamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl51InfrafamiliesWindow(id, "Tbl51Infrafamilies");
            rp.Show();
        }
                   // Tbl57Tribusses
        private void HyperlinkTribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl57TribussesWindow(id, "Tbl57Tribusses");
            rp.Show();
        }
  ]]>   
</xsl:when>  
<xsl:when test="Table ='Tbl57Tribusses'">   <![CDATA[ 
                  //  Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportTbl03RegnumsWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                  //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl06PhylumsWindow(id, "Tbl06Phylums");
            rp.Show();
        }

                  // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl09DivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl12SubphylumsWindow(id, "Tbl12Subphylums");
            rp.Show();
        }

                   // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl15SubdivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl18SuperclassesWindow(id, "Tbl18Superclasses");
            rp.Show();
        }

                   // Tbl21Classes  -->
        private void HyperlinkClass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl21ClassesWindow(id, "Tbl21Classes");
            rp.Show();
        }

                   // Tbl24Subclasses  -->
        private void HyperlinkSubclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl24SubclassesWindow(id, "Tbl24Subclasses");
            rp.Show();
        }

                   // Tbl27Infraclasses  -->
        private void HyperlinkInfraclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl27InfraclassesWindow(id, "Tbl27Infraclasses");
            rp.Show();
        }
                   // Tbl30Legios  -->
        private void HyperlinkLegio_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl30LegiosWindow(id, "Tbl30Legios");
            rp.Show();
        }
                   // Tbl33Ordos  -->
        private void HyperlinkOrdo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl33OrdosWindow(id, "Tbl33Ordos");
            rp.Show();
        }
                   // Tbl36Subordos  -->
        private void HyperlinkSubordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl36SubordosWindow(id, "Tbl36Subordos");
            rp.Show();
        }
                   // Tbl39Infraordos  -->
        private void HyperlinkInfraordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl39InfraordosWindow(id, "Tbl39Infraordos");
            rp.Show();
        }
                   // Tbl42Superfamilies  -->
        private void HyperlinkSuperfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl42SuperfamiliesWindow(id, "Tbl42Superfamilies");
            rp.Show();
        }
                   // Tbl45Families  -->
        private void HyperlinkFamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl45FamiliesWindow(id, "Tbl45Families");
            rp.Show();
        }
                   // Tbl48Subfamilies  -->
        private void HyperlinkSubfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl48SubfamiliesWindow(id, "Tbl48Subfamilies");
            rp.Show();
        }
                   // Tbl51Infrafamilies  -->
        private void HyperlinkInfrafamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl51InfrafamiliesWindow(id, "Tbl51Infrafamilies");
            rp.Show();
        }
                   // Tbl54Supertribusses
        private void HyperlinkSupertribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl54SupertribussesWindow(id, "Tbl54Supertribusses");
            rp.Show();
        }
                   // Tbl60Subtribusses
        private void HyperlinkSubtribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl60SubtribussesWindow(id, "Tbl60Subtribusses");
            rp.Show();
        }
  ]]>   
</xsl:when>  
<xsl:when test="Table ='Tbl60Subtribusses'">   <![CDATA[ 
                  //  Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportTbl03RegnumsWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                  //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl06PhylumsWindow(id, "Tbl06Phylums");
            rp.Show();
        }

                  // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl09DivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl12SubphylumsWindow(id, "Tbl12Subphylums");
            rp.Show();
        }

                   // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl15SubdivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl18SuperclassesWindow(id, "Tbl18Superclasses");
            rp.Show();
        }

                   // Tbl21Classes  -->
        private void HyperlinkClass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl21ClassesWindow(id, "Tbl21Classes");
            rp.Show();
        }

                   // Tbl24Subclasses  -->
        private void HyperlinkSubclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl24SubclassesWindow(id, "Tbl24Subclasses");
            rp.Show();
        }

                   // Tbl27Infraclasses  -->
        private void HyperlinkInfraclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl27InfraclassesWindow(id, "Tbl27Infraclasses");
            rp.Show();
        }
                   // Tbl30Legios  -->
        private void HyperlinkLegio_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl30LegiosWindow(id, "Tbl30Legios");
            rp.Show();
        }
                   // Tbl33Ordos  -->
        private void HyperlinkOrdo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl33OrdosWindow(id, "Tbl33Ordos");
            rp.Show();
        }
                   // Tbl36Subordos  -->
        private void HyperlinkSubordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl36SubordosWindow(id, "Tbl36Subordos");
            rp.Show();
        }
                   // Tbl39Infraordos  -->
        private void HyperlinkInfraordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl39InfraordosWindow(id, "Tbl39Infraordos");
            rp.Show();
        }
                   // Tbl42Superfamilies  -->
        private void HyperlinkSuperfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl42SuperfamiliesWindow(id, "Tbl42Superfamilies");
            rp.Show();
        }
                   // Tbl45Families  -->
        private void HyperlinkFamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl45FamiliesWindow(id, "Tbl45Families");
            rp.Show();
        }
                   // Tbl48Subfamilies  -->
        private void HyperlinkSubfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl48SubfamiliesWindow(id, "Tbl48Subfamilies");
            rp.Show();
        }
                   // Tbl51Infrafamilies  -->
        private void HyperlinkInfrafamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl51InfrafamiliesWindow(id, "Tbl51Infrafamilies");
            rp.Show();
        }
                   // Tbl54Supertribusses
        private void HyperlinkSupertribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl54SupertribussesWindow(id, "Tbl54Supertribusses");
            rp.Show();
        }
                   // Tbl57Tribusses
        private void HyperlinkTribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl57TribussesWindow(id, "Tbl57Tribusses");
            rp.Show();
        }
                   // Tbl63Infratribusses
        private void HyperlinkInfratribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl63InfratribussesWindow(id, "Tbl63Infratribusses");
            rp.Show();
        }
  ]]>   
</xsl:when>  
<xsl:when test="Table ='Tbl63Infratribusses'">   <![CDATA[ 
                  //  Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportTbl03RegnumsWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                  //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl06PhylumsWindow(id, "Tbl06Phylums");
            rp.Show();
        }

                  // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl09DivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl12SubphylumsWindow(id, "Tbl12Subphylums");
            rp.Show();
        }

                   // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl15SubdivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl18SuperclassesWindow(id, "Tbl18Superclasses");
            rp.Show();
        }

                   // Tbl21Classes  -->
        private void HyperlinkClass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl21ClassesWindow(id, "Tbl21Classes");
            rp.Show();
        }

                   // Tbl24Subclasses  -->
        private void HyperlinkSubclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl24SubclassesWindow(id, "Tbl24Subclasses");
            rp.Show();
        }

                   // Tbl27Infraclasses  -->
        private void HyperlinkInfraclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl27InfraclassesWindow(id, "Tbl27Infraclasses");
            rp.Show();
        }
                   // Tbl30Legios  -->
        private void HyperlinkLegio_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl30LegiosWindow(id, "Tbl30Legios");
            rp.Show();
        }
                   // Tbl33Ordos  -->
        private void HyperlinkOrdo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl33OrdosWindow(id, "Tbl33Ordos");
            rp.Show();
        }
                   // Tbl36Subordos  -->
        private void HyperlinkSubordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl36SubordosWindow(id, "Tbl36Subordos");
            rp.Show();
        }
                   // Tbl39Infraordos  -->
        private void HyperlinkInfraordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl39InfraordosWindow(id, "Tbl39Infraordos");
            rp.Show();
        }
                   // Tbl42Superfamilies  -->
        private void HyperlinkSuperfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl42SuperfamiliesWindow(id, "Tbl42Superfamilies");
            rp.Show();
        }
                   // Tbl45Families  -->
        private void HyperlinkFamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl45FamiliesWindow(id, "Tbl45Families");
            rp.Show();
        }
                   // Tbl48Subfamilies  -->
        private void HyperlinkSubfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl48SubfamiliesWindow(id, "Tbl48Subfamilies");
            rp.Show();
        }
                   // Tbl51Infrafamilies  -->
        private void HyperlinkInfrafamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl51InfrafamiliesWindow(id, "Tbl51Infrafamilies");
            rp.Show();
        }
                   // Tbl54Supertribusses
        private void HyperlinkSupertribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl54SupertribussesWindow(id, "Tbl54Supertribusses");
            rp.Show();
        }
                   // Tbl57Tribusses
        private void HyperlinkTribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl57TribussesWindow(id, "Tbl57Tribusses");
            rp.Show();
        }
                   // Tbl60Subtribusses
        private void HyperlinkSubtribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl60SubtribussesWindow(id, "Tbl60Subtribusses");
            rp.Show();
        }
                   // Tbl66Genusses
        private void HyperlinkGenus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl66GenussesWindow(id, "Tbl66Genusses");
            rp.Show();
        }
  ]]>   
</xsl:when>  
<xsl:when test="Table ='Tbl66Genusses'">   <![CDATA[ 
                  //  Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportTbl03RegnumsWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                  //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl06PhylumsWindow(id, "Tbl06Phylums");
            rp.Show();
        }

                  // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl09DivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl12SubphylumsWindow(id, "Tbl12Subphylums");
            rp.Show();
        }

                   // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl15SubdivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl18SuperclassesWindow(id, "Tbl18Superclasses");
            rp.Show();
        }

                   // Tbl21Classes  -->
        private void HyperlinkClass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl21ClassesWindow(id, "Tbl21Classes");
            rp.Show();
        }

                   // Tbl24Subclasses  -->
        private void HyperlinkSubclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl24SubclassesWindow(id, "Tbl24Subclasses");
            rp.Show();
        }

                   // Tbl27Infraclasses  -->
        private void HyperlinkInfraclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl27InfraclassesWindow(id, "Tbl27Infraclasses");
            rp.Show();
        }
                   // Tbl30Legios  -->
        private void HyperlinkLegio_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl30LegiosWindow(id, "Tbl30Legios");
            rp.Show();
        }
                   // Tbl33Ordos  -->
        private void HyperlinkOrdo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl33OrdosWindow(id, "Tbl33Ordos");
            rp.Show();
        }
                   // Tbl36Subordos  -->
        private void HyperlinkSubordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl36SubordosWindow(id, "Tbl36Subordos");
            rp.Show();
        }
                   // Tbl39Infraordos  -->
        private void HyperlinkInfraordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl39InfraordosWindow(id, "Tbl39Infraordos");
            rp.Show();
        }
                   // Tbl42Superfamilies  -->
        private void HyperlinkSuperfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl42SuperfamiliesWindow(id, "Tbl42Superfamilies");
            rp.Show();
        }
                   // Tbl45Families  -->
        private void HyperlinkFamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl45FamiliesWindow(id, "Tbl45Families");
            rp.Show();
        }
                   // Tbl48Subfamilies  -->
        private void HyperlinkSubfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl48SubfamiliesWindow(id, "Tbl48Subfamilies");
            rp.Show();
        }
                   // Tbl51Infrafamilies  -->
        private void HyperlinkInfrafamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl51InfrafamiliesWindow(id, "Tbl51Infrafamilies");
            rp.Show();
        }
                   // Tbl54Supertribusses
        private void HyperlinkSupertribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl54SupertribussesWindow(id, "Tbl54Supertribusses");
            rp.Show();
        }
                   // Tbl57Tribusses
        private void HyperlinkTribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl57TribussesWindow(id, "Tbl57Tribusses");
            rp.Show();
        }
                   // Tbl60Subtribusses
        private void HyperlinkSubtribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl60SubtribussesWindow(id, "Tbl60Subtribusses");
            rp.Show();
        }
                   // Tbl63Infratribusses
        private void HyperlinkInfratribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl63InfratribussesWindow(id, "Tbl63Infratribusses");
            rp.Show();
        }
                   // Tbl69FiSpeciesses
        private void HyperlinkFiSpecies_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl69FiSpeciessesWindow(id, "Tbl69FiSpeciesses");
            rp.Show();
        }
                   // Tbl72PlSpeciesses
        private void HyperlinkPlSpecies_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl72PlSpeciessesWindow(id, "Tbl72PlSpeciesses");
            rp.Show();
        }
  ]]>   
</xsl:when>  
<xsl:when test="Table ='Tbl69FiSpeciesses'">   <![CDATA[ 
                  //  Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportTbl03RegnumsWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                  //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl06PhylumsWindow(id, "Tbl06Phylums");
            rp.Show();
        }

                  // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl09DivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl12SubphylumsWindow(id, "Tbl12Subphylums");
            rp.Show();
        }

                   // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl15SubdivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl18SuperclassesWindow(id, "Tbl18Superclasses");
            rp.Show();
        }

                   // Tbl21Classes  -->
        private void HyperlinkClass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl21ClassesWindow(id, "Tbl21Classes");
            rp.Show();
        }

                   // Tbl24Subclasses  -->
        private void HyperlinkSubclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl24SubclassesWindow(id, "Tbl24Subclasses");
            rp.Show();
        }

                   // Tbl27Infraclasses  -->
        private void HyperlinkInfraclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl27InfraclassesWindow(id, "Tbl27Infraclasses");
            rp.Show();
        }
                   // Tbl30Legios  -->
        private void HyperlinkLegio_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl30LegiosWindow(id, "Tbl30Legios");
            rp.Show();
        }
                   // Tbl33Ordos  -->
        private void HyperlinkOrdo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl33OrdosWindow(id, "Tbl33Ordos");
            rp.Show();
        }
                   // Tbl36Subordos  -->
        private void HyperlinkSubordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl36SubordosWindow(id, "Tbl36Subordos");
            rp.Show();
        }
                   // Tbl39Infraordos  -->
        private void HyperlinkInfraordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl39InfraordosWindow(id, "Tbl39Infraordos");
            rp.Show();
        }
                   // Tbl42Superfamilies  -->
        private void HyperlinkSuperfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl42SuperfamiliesWindow(id, "Tbl42Superfamilies");
            rp.Show();
        }
                   // Tbl45Families  -->
        private void HyperlinkFamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl45FamiliesWindow(id, "Tbl45Families");
            rp.Show();
        }
                   // Tbl48Subfamilies  -->
        private void HyperlinkSubfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl48SubfamiliesWindow(id, "Tbl48Subfamilies");
            rp.Show();
        }
                   // Tbl51Infrafamilies  -->
        private void HyperlinkInfrafamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl51InfrafamiliesWindow(id, "Tbl51Infrafamilies");
            rp.Show();
        }
                   // Tbl54Supertribusses
        private void HyperlinkSupertribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl54SupertribussesWindow(id, "Tbl54Supertribusses");
            rp.Show();
        }
                   // Tbl57Tribusses
        private void HyperlinkTribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl57TribussesWindow(id, "Tbl57Tribusses");
            rp.Show();
        }
                   // Tbl60Subtribusses
        private void HyperlinkSubtribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl60SubtribussesWindow(id, "Tbl60Subtribusses");
            rp.Show();
        }
                   // Tbl63Infratribusses
        private void HyperlinkInfratribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl63InfratribussesWindow(id, "Tbl63Infratribusses");
            rp.Show();
        }
                   // Tbl66Genusses
        private void HyperlinkGenus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl66GenussesWindow(id, "Tbl66Genusses");
            rp.Show();
        }
  ]]>   
</xsl:when>  
<xsl:when test="Table ='Tbl72PlSpeciesses'">   <![CDATA[ 
                  //  Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportTbl03RegnumsWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                  //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl06PhylumsWindow(id, "Tbl06Phylums");
            rp.Show();
        }

                  // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl09DivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl12SubphylumsWindow(id, "Tbl12Subphylums");
            rp.Show();
        }

                   // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl15SubdivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl18SuperclassesWindow(id, "Tbl18Superclasses");
            rp.Show();
        }

                   // Tbl21Classes  -->
        private void HyperlinkClass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl21ClassesWindow(id, "Tbl21Classes");
            rp.Show();
        }

                   // Tbl24Subclasses  -->
        private void HyperlinkSubclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl24SubclassesWindow(id, "Tbl24Subclasses");
            rp.Show();
        }

                   // Tbl27Infraclasses  -->
        private void HyperlinkInfraclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl27InfraclassesWindow(id, "Tbl27Infraclasses");
            rp.Show();
        }
                   // Tbl30Legios  -->
        private void HyperlinkLegio_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl30LegiosWindow(id, "Tbl30Legios");
            rp.Show();
        }
                   // Tbl33Ordos  -->
        private void HyperlinkOrdo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl33OrdosWindow(id, "Tbl33Ordos");
            rp.Show();
        }
                   // Tbl36Subordos  -->
        private void HyperlinkSubordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl36SubordosWindow(id, "Tbl36Subordos");
            rp.Show();
        }
                   // Tbl39Infraordos  -->
        private void HyperlinkInfraordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl39InfraordosWindow(id, "Tbl39Infraordos");
            rp.Show();
        }
                   // Tbl42Superfamilies  -->
        private void HyperlinkSuperfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl42SuperfamiliesWindow(id, "Tbl42Superfamilies");
            rp.Show();
        }
                   // Tbl45Families  -->
        private void HyperlinkFamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl45FamiliesWindow(id, "Tbl45Families");
            rp.Show();
        }
                   // Tbl48Subfamilies  -->
        private void HyperlinkSubfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl48SubfamiliesWindow(id, "Tbl48Subfamilies");
            rp.Show();
        }
                   // Tbl51Infrafamilies  -->
        private void HyperlinkInfrafamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl51InfrafamiliesWindow(id, "Tbl51Infrafamilies");
            rp.Show();
        }
                   // Tbl54Supertribusses
        private void HyperlinkSupertribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl54SupertribussesWindow(id, "Tbl54Supertribusses");
            rp.Show();
        }
                   // Tbl57Tribusses
        private void HyperlinkTribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl57TribussesWindow(id, "Tbl57Tribusses");
            rp.Show();
        }
                   // Tbl60Subtribusses
        private void HyperlinkSubtribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl60SubtribussesWindow(id, "Tbl60Subtribusses");
            rp.Show();
        }
                   // Tbl63Infratribusses
        private void HyperlinkInfratribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl63InfratribussesWindow(id, "Tbl63Infratribusses");
            rp.Show();
        }
                   // Tbl66Genusses
        private void HyperlinkGenus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl66GenussesWindow(id, "Tbl66Genusses");
            rp.Show();
        }   ]]>   
</xsl:when>  

<xsl:otherwise>           
</xsl:otherwise>    
</xsl:choose>
             
<![CDATA[     }
}]]>   

</xsl:template>
</xsl:stylesheet>










