using System; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

   //  ReportTblCountersWindow.xaml.cs Skriptdatum:  3.1.2012  12:32       

namespace WPFUI.Views.Report 
{  

    /// <summary>
    /// Interactionslogic for ReportTblCountersWindow.xaml
    /// </summary>
    public partial class ReportTblCountersWindow : Window
   {

        public ReportTblCountersWindow(int un, string tab)
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

