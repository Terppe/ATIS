using System; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Te.Atis.Ui.Desktop.Properties;

   //  ReportTbl90RefSourcesWindow.xaml.cs Skriptdatum:   29.11.2018  10:32     

namespace Te.Atis.Ui.Desktop.Views.Report 
{  

    /// <summary>
    /// Interactionslogic for ReportTbl90RefSourcesWindow.xaml
    /// </summary>
    public partial class ReportTbl90RefSourcesWindow : Window
   {

        public ReportTbl90RefSourcesWindow(int un, string tab)
       {         
            DataContext = new ReportViewModel(un, tab);
            InitializeComponent();   

            WindowStartupLocation = WindowStartupLocation.Manual;

            Left = Settings.Default.Left + (Settings.Default.Width / 2) - (Width / 2);
            Top = Settings.Default.Top + (Settings.Default.Height / 2) - (Height / 2);
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
            Width = Reader.Width + 10;
        }


             
     }
}   

