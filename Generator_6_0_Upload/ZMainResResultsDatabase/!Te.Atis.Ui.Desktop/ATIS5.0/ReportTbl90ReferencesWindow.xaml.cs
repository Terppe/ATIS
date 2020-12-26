using System; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

   //  ReportTbl90ReferencesWindow.xaml.cs Skriptdatum:  29.11.2018  10:32     

namespace WPFUI.Views.Report 
{  

    /// <summary>
    /// Interactionslogic for ReportTbl90ReferencesWindow.xaml
    /// </summary>
    public partial class ReportTbl90ReferencesWindow : Window
   {

        public ReportTbl90ReferencesWindow(int un, string tab)
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

