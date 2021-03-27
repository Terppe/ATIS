using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using MahApps.Metro.Controls;


using ATIS.Ui.Views.Report.D03Regnum;
using ATIS.Ui.Views.Report.D06Phylum;
using ATIS.Ui.Views.Report.D09Division;
using ATIS.Ui.Views.Report.D12Subphylum;
using ATIS.Ui.Views.Report.D15Subdivision;
using ATIS.Ui.Views.Report.D18Superclass;
using ATIS.Ui.Views.Report.D24Subclass;



//  ReportClassWindow.xaml.cs Skriptdatum:  07.01.2021  18:32     

namespace ATIS.Ui.Views.Report.D21Class
{

    /// <summary>
    /// Interactionslogic for ReportClassWindow.xaml
    /// </summary>
    public partial class ReportClassWindow : MetroWindow
    {

        public ReportClassWindow(int un, string tab)
        {
            DataContext = new ReportViewModel(un, tab);
            InitializeComponent();

            var left = Convert.ToInt16(ConfigurationManager.AppSettings["Left"]);
            var top = Convert.ToInt16(ConfigurationManager.AppSettings["Top"]);
            var height = Convert.ToInt16(ConfigurationManager.AppSettings["Height"]);
            var width = Convert.ToInt16(ConfigurationManager.AppSettings["Width"]);

            WindowStartupLocation = WindowStartupLocation.Manual;

            Left = left + (width / 2) - (Width / 2);
            Top = top + (height / 2) - (Height / 2);
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
        }


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
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportPhylumWindow(id, "Tbl06Phylums");
            rp.Show();
        }

        // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportDivisionWindow(id, "Tbl09Divisions");
            rp.Show();
        }

        //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportSubphylumWindow(id, "Tbl12Subphylums");
            rp.Show();
        }

        // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportSubdivisionWindow(id, "Tbl09Divisions");
            rp.Show();
        }

        // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportSuperclassWindow(id, "Tbl18Superclasses");
            rp.Show();
        }

        // Tbl24Subclasses  -->
        private void HyperlinkSubclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportSubclassWindow(id, "Tbl24Subclasses");
            rp.Show();
        }



    }
}

