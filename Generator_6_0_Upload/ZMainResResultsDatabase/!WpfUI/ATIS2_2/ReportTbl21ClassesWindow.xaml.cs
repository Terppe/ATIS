using System; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

   //  ReportTbl21ClassesWindow.xaml.cs Skriptdatum:  21.12.2017  18:32     

namespace WPFUI.Views.Report 
{  

    /// <summary>
    /// Interactionslogic for ReportTbl21ClassesWindow.xaml
    /// </summary>
    public partial class ReportTbl21ClassesWindow : Window
   {

        public ReportTbl21ClassesWindow(int un, string tab)
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

    
                  //  Tbl03Regnums  -->
        private void HyperlinkRegnum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportTbl03RegnumsWindow(id, "Tbl03Regnums");
            rp.Show();
        }

                  //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl06PhylumsWindow(id, "Tbl06Phylums");
            rp.Show();
        }

                  // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl09DivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl12SubphylumsWindow(id, "Tbl12Subphylums");
            rp.Show();
        }

                   // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl15SubdivisionsWindow(id, "Tbl09Divisions");
            rp.Show();
        }

                   // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl18SuperclassesWindow(id, "Tbl18Superclasses");
            rp.Show();
        }

                   // Tbl21Classes  -->
        private void HyperlinkClass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl21ClassesWindow(id, "Tbl21Classes");
            rp.Show();
        }
                   // Tbl24Subclasses  -->
        private void HyperlinkSubclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl24SubclassesWindow(id, "Tbl24Subclasses");
            rp.Show();
        }
     

             
     }
}   

