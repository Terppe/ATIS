using System; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using ATIS.Ui.Views.Report.D03Regnum;
using ATIS.Ui.Views.Report.D06Phylum;
using MahApps.Metro.Controls;

   //  ReportInfraclassWindow.xaml.cs Skriptdatum:  10.12.2020  18:32     

namespace ATIS.Ui.Views.Report.D27Infraclass
{  

    /// <summary>
    /// Interactionslogic for ReportInfraclassWindow.xaml
    /// </summary>
    public partial class ReportInfraclassWindow : MetroWindow
   {

        public ReportInfraclassWindow(int un, string tab)
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

                   // Tbl30Legios  -->
        private void HyperlinkLegio_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl30LegiosWindow(id, "Tbl30Legios");
            rp.Show();
        }
     

             
     }
}   

