using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using MahApps.Metro.Controls;
using ATIS.Ui.Views.Report.D06Phylum;
using ATIS.Ui.Views.Report.D09Division;

//  ReportRegnumWindow.xaml.cs Skriptdatum:  04.01.2021  12:32       

namespace ATIS.Ui.Views.Report.D03Regnum
{
    /// <summary>
    /// Interactionslogic for ReportRegnumWindow.xaml
    /// </summary>
    public partial class ReportRegnumWindow : MetroWindow
    {
        public ReportRegnumWindow(int un, string tab)
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
    }
}
