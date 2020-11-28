using System; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using ATIS.Ui.Views.Report.D03Regnum;
using ATIS.Ui.Views.Report.D06Phylum;
using MahApps.Metro.Controls;

   //  ReportGeographicWindow.xaml.cs Skriptdatum:  22.01.2019  10:32     

namespace ATIS.Ui.Views.Report.ListDetails
{  

    /// <summary>
    /// Interactionslogic for ReportGeographicWindow.xaml
    /// </summary>
    public partial class ReportGeographicWindow : MetroWindow
   {

        public ReportGeographicWindow(int un, string tab)
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


             
     }
}   

