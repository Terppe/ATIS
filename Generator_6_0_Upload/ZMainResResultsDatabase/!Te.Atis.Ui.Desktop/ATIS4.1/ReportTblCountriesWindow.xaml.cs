using System; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

   //  ReportTblCountriesWindow.xaml.cs Skriptdatum:   31.07.2018 12:32       

namespace WPFUI.Views.Report 
{  

    /// <summary>
    /// Interactionslogic for ReportTblCountriesWindow.xaml
    /// </summary>
    public partial class ReportTblCountriesWindow : Window
   {

        public ReportTblCountriesWindow(int un, string tab)
       {         
            DataContext = new ReportViewModel(un, tab);
            InitializeComponent();   
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

