using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using MahApps.Metro.Controls;


using ATIS.Ui.Views.Report.D03Regnum;
using ATIS.Ui.Views.Report.D06Phylum;
using ATIS.Ui.Views.Report.D09Division;
using ATIS.Ui.Views.Report.D12Subphylum;
using ATIS.Ui.Views.Report.D15Subdivision;
using ATIS.Ui.Views.Report.D18Superclass;
using ATIS.Ui.Views.Report.D21Class;
using ATIS.Ui.Views.Report.D24Subclass;
using ATIS.Ui.Views.Report.D27Infraclass;
using ATIS.Ui.Views.Report.D30Legio;
using ATIS.Ui.Views.Report.D33Ordo;
using ATIS.Ui.Views.Report.D36Subordo;
using ATIS.Ui.Views.Report.D39Infraordo;
using ATIS.Ui.Views.Report.D42Superfamily;
using ATIS.Ui.Views.Report.D45Family;
using ATIS.Ui.Views.Report.D48Subfamily;
using ATIS.Ui.Views.Report.D51Infrafamily;
using ATIS.Ui.Views.Report.D54Supertribus;
using ATIS.Ui.Views.Report.D57Tribus;
using ATIS.Ui.Views.Report.D60Subtribus;
using ATIS.Ui.Views.Report.D63Infratribus;
using ATIS.Ui.Views.Report.D66Genus;
using ATIS.Ui.Views.Report.D72PlSpeciesSub;


//  ReportPlSpeciesWindow.xaml.cs Skriptdatum:  07.01.2021  12:32     

namespace ATIS.Ui.Views.Report.D72PlSpecies
{

    /// <summary>
    /// Interactionslogic for ReportPlSpeciesWindow.xaml
    /// </summary>
    public partial class ReportPlSpeciesWindow : MetroWindow
    {

        public ReportPlSpeciesWindow(int un, string tab)
        {
            DataContext = new ReportViewModel(un, tab);
            InitializeComponent();

            var left = Convert.ToInt16(ConfigurationManager.AppSettings["Left"]);
            var top = Convert.ToInt16(ConfigurationManager.AppSettings["Top"]);
            var height = Convert.ToInt16(ConfigurationManager.AppSettings["Height"]);
            var width = Convert.ToInt16(ConfigurationManager.AppSettings["Width"]);

            WindowStartupLocation = WindowStartupLocation.Manual;

            Left = left + (width / 2) - (Width / 2);
            Top = top + (height / 2) - (Height / 2);
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
            var rp = new ReportRegnumWindow(id, "Tbl03Regnums");
            rp.Show();
        }

        //Tbl06Phylums  -->
        private void HyperlinkPhylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportPhylumWindow(id, "Tbl06Phylums");
            rp.Show();
        }

        // Tbl09Divisions  -->
        private void HyperlinkDivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportDivisionWindow(id, "Tbl09Divisions");
            rp.Show();
        }

        //Tbl12Subphylums  -->
        private void HyperlinkSubphylum_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportSubphylumWindow(id, "Tbl12Subphylums");
            rp.Show();
        }

        // Tbl15Subdivisions  -->
        private void HyperlinkSubdivision_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportSubdivisionWindow(id, "Tbl09Divisions");
            rp.Show();
        }

        // Tbl18Superclasses  -->
        private void HyperlinkSuperclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportSuperclassWindow(id, "Tbl18Superclasses");
            rp.Show();
        }

        // Tbl21Classes  -->
        private void HyperlinkClass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportClassWindow(id, "Tbl21Classes");
            rp.Show();
        }

        // Tbl24Subclasses  -->
        private void HyperlinkSubclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportSubclassWindow(id, "Tbl24Subclasses");
            rp.Show();
        }

        // Tbl27Infraclasses  -->
        private void HyperlinkInfraclass_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportInfraclassWindow(id, "Tbl27Infraclasses");
            rp.Show();
        }
        // Tbl30Legios  -->
        private void HyperlinkLegio_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportLegioWindow(id, "Tbl30Legios");
            rp.Show();
        }
        // Tbl33Ordos  -->
        private void HyperlinkOrdo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportOrdoWindow(id, "Tbl33Ordos");
            rp.Show();
        }
        // Tbl36Subordos  -->
        private void HyperlinkSubordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportSubordoWindow(id, "Tbl36Subordos");
            rp.Show();
        }
        // Tbl39Infraordos  -->
        private void HyperlinkInfraordo_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportInfraordoWindow(id, "Tbl39Infraordos");
            rp.Show();
        }
        // Tbl42Superfamilies  -->
        private void HyperlinkSuperfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportSuperfamilyWindow(id, "Tbl42Superfamilies");
            rp.Show();
        }
        // Tbl45Families  -->
        private void HyperlinkFamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportFamilyWindow(id, "Tbl45Families");
            rp.Show();
        }
        // Tbl48Subfamilies  -->
        private void HyperlinkSubfamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportSubfamilyWindow(id, "Tbl48Subfamilies");
            rp.Show();
        }
        // Tbl51Infrafamilies  -->
        private void HyperlinkInfrafamily_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportInfrafamilyWindow(id, "Tbl51Infrafamilies");
            rp.Show();
        }
        // Tbl54Supertribusses
        private void HyperlinkSupertribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportSupertribusWindow(id, "Tbl54Supertribusses");
            rp.Show();
        }
        // Tbl57Tribusses
        private void HyperlinkTribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportTribusWindow(id, "Tbl57Tribusses");
            rp.Show();
        }
        // Tbl60Subtribusses
        private void HyperlinkSubtribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportSubtribusWindow(id, "Tbl60Subtribusses");
            rp.Show();
        }
        // Tbl63Infratribusses
        private void HyperlinkInfratribus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportInfratribusWindow(id, "Tbl63Infratribusses");
            rp.Show();
        }
        // Tbl66Genusses
        private void HyperlinkGenus_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportGenusWindow(id, "Tbl66Genusses");
            rp.Show();
        }

        //Tbl72PlSpeciesses
        private void HyperlinkPlSpecies_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportPlSpeciesWindow(id, "Tbl72PlSpeciesses");
            rp.Show();
        }
        private void HyperlinkPlSpeciesSub_Click(object sender, RoutedEventArgs e)
        {
            var tagValue = ((Hyperlink)sender).Tag;
            var id = Convert.ToInt32(tagValue);
            var rp = new ReportPlSpeciesSubWindow(id, "Tbl72PlSpeciessesSub");
            rp.Show();
        }

    }
}

