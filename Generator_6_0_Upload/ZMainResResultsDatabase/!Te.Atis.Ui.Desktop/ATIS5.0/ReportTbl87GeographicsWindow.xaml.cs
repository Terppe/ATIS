using System; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

   //  ReportTbl87GeographicsWindow.xaml.cs Skriptdatum:  13.11.2018  10:32     

namespace WPFUI.Views.Report 
{  

    /// <summary>
    /// Interactionslogic for ReportTbl87GeographicsWindow.xaml
    /// </summary>
    public partial class ReportTbl87GeographicsWindow : Window
   {

        public ReportTbl87GeographicsWindow(int un, string tab)
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

