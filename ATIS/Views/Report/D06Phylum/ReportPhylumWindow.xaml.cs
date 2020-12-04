using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using ATIS.Ui.Views.Report.D12Subphylum;
using MahApps.Metro.Controls;

//  ReportPhylumWindow.xaml.cs Skriptdatum:  28.11.2020  12:32     

namespace ATIS.Ui.Views.Report.D06Phylum
{

    /// <summary>
    /// Interactionslogic for ReportPhylumWindow.xaml
    /// </summary>
    public partial class ReportPhylumWindow : MetroWindow
    {

        public ReportPhylumWindow(int un, string tab)
        {
            //      Mouse.OverrideCursor = Cursors.Wait;

            DataContext = new ReportViewModel(un, tab);
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //    WindowStartupLocation = WindowStartupLocation.Manuel;

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
            var id = Convert.ToInt32(tagValue);
                 var rp = new ReportSubphylumWindow(id, "Tbl12Subphylums");
                 rp.Show();
        }



    }
}

