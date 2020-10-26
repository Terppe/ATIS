using System; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

   //  ReportTbl45FamiliesWindow.xaml.cs Skriptdatum:  31.03.2014  10:32     

namespace WPFUI.Views.Report 
{  

    /// <summary>
    /// Interactionslogic for ReportTbl45FamiliesWindow.xaml
    /// </summary>
    public partial class ReportTbl45FamiliesWindow : Window
   {

        public ReportTbl45FamiliesWindow(int un, string tab)
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

