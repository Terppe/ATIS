using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using ATIS.Ui.Views.Report.D06Phylum;
using ATIS.Ui.Views.Report.D09Division;
using MahApps.Metro.Controls;

//  ReportRegnumWindow.xaml.cs Skriptdatum:  27.11.2020  12:32       

namespace ATIS.Ui.Views.Report.D03Regnum
{
    /// <summary>
    /// Interactionslogic for ReportRegnumWindow.xaml
    /// </summary>
    public partial class ReportRegnumWindow : MetroWindow
    {
        public ReportRegnumWindow(int un, string tab)
        {
            //      Mouse.OverrideCursor = Cursors.Wait;

            DataContext = new ReportViewModel(un, tab);
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
          //  WindowStartupLocation = WindowStartupLocation.Manual;

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
