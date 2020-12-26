using System; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

   //  ReportTbl69FiSpeciessesWindow.xaml.cs Skriptdatum:  12.11.2017  10:32     

namespace WPFUI.Views.Report 
{  

    /// <summary>
    /// Interactionslogic for ReportTbl69FiSpeciessesWindow.xaml
    /// </summary>
    public partial class ReportTbl69FiSpeciessesWindow : Window
   {

        public ReportTbl69FiSpeciessesWindow(int un, string tab)
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

                   // Tbl27Infraclasses  -->
        private void HyperlinkInfraclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl27InfraclassesWindow(id, "Tbl27Infraclasses");
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
                   // Tbl33Ordos  -->
        private void HyperlinkOrdo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl33OrdosWindow(id, "Tbl33Ordos");
            rp.Show();
        }
                   // Tbl36Subordos  -->
        private void HyperlinkSubordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl36SubordosWindow(id, "Tbl36Subordos");
            rp.Show();
        }
                   // Tbl39Infraordos  -->
        private void HyperlinkInfraordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl39InfraordosWindow(id, "Tbl39Infraordos");
            rp.Show();
        }
                   // Tbl42Superfamilies  -->
        private void HyperlinkSuperfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl42SuperfamiliesWindow(id, "Tbl42Superfamilies");
            rp.Show();
        }
                   // Tbl45Families  -->
        private void HyperlinkFamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl45FamiliesWindow(id, "Tbl45Families");
            rp.Show();
        }
                   // Tbl48Subfamilies  -->
        private void HyperlinkSubfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl48SubfamiliesWindow(id, "Tbl48Subfamilies");
            rp.Show();
        }
                   // Tbl51Infrafamilies  -->
        private void HyperlinkInfrafamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl51InfrafamiliesWindow(id, "Tbl51Infrafamilies");
            rp.Show();
        }
                   // Tbl54Supertribusses
        private void HyperlinkSupertribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl54SupertribussesWindow(id, "Tbl54Supertribusses");
            rp.Show();
        }
                   // Tbl57Tribusses
        private void HyperlinkTribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl57TribussesWindow(id, "Tbl57Tribusses");
            rp.Show();
        }
                   // Tbl60Subtribusses
        private void HyperlinkSubtribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl60SubtribussesWindow(id, "Tbl60Subtribusses");
            rp.Show();
        }
                   // Tbl63Infratribusses
        private void HyperlinkInfratribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl63InfratribussesWindow(id, "Tbl63Infratribusses");
            rp.Show();
        }
                   // Tbl66Genusses
        private void HyperlinkGenus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl66GenussesWindow(id, "Tbl66Genusses");
            rp.Show();
        }
                   // Tbl69FiSpeciesses
        private void HyperlinkFiSpecies_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id= Convert.ToInt32(tagValue);
            var rp = new ReportTbl69FiSpeciessesWindow(id, "Tbl69FiSpeciesses");
            rp.Show();
        }
     

             
     }
}   

