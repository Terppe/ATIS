using System; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using ATIS.Ui.Views.Report.D03Regnum;
using ATIS.Ui.Views.Report.D06Phylum;
using MahApps.Metro.Controls;

   //  ReportSynonymWindow.xaml.cs Skriptdatum:  13.11.2018  10:32     

namespace ATIS.Ui.Views.Report.ListDetails
{  

    /// <summary>
    /// Interactionslogic for ReportSynonymWindow.xaml
    /// </summary>
    public partial class ReportSynonymWindow : MetroWindow
   {

        public ReportSynonymWindow(int un, string tab)
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
        }


             
     }
}   

