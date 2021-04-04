using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ATIS.Ui.Views.Report;
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
using ATIS.Ui.Views.Report.D69FiSpecies;
using ATIS.Ui.Views.Report.D69FiSpeciesSub;
using ATIS.Ui.Views.Report.D72PlSpecies;
using ATIS.Ui.Views.Report.D72PlSpeciesSub;

namespace ATIS.Ui.Views.Search
{
    /// <summary>
    /// Interaktionslogik für SearchBase.xaml
    /// </summary>
    public partial class SearchBase : UserControl
    {
        private readonly ReportBasicGet _extReportBasicGet = new ReportBasicGet();

        public SearchBase()
        {
            DataContext = new SearchQuickViewModel();
            InitializeComponent();
        }
        public SearchBase(string un)
        {
            DataContext = new SearchQuickViewModel(un);
            InitializeComponent();

        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
                //           printDialog.PrintDocument(((IDocumentPaginatorSource)FlowDocument).DocumentPaginator, "This is a Flow Document");
                printDialog.PrintVisual(LayoutRoot, "Landscape broken Grid print");

        }
        private void Tbl03RegnumsList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var rp = new Report.D03Regnum.ReportRegnumWindow(id, "Tbl03Regnums");
            rp.Show();
        }
        private void Tbl06PhylumsList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var rp1 = new Report.D06Phylum.ReportPhylumWindow(id, "Tbl06Phylums");
            rp1.Show();
        }
        private void Tbl12SubphylumsList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var rp = new Report.D12Subphylum.ReportSubphylumWindow(id, "Tbl12Subphylums");
            rp.Show();
        }
        private void Tbl09DivisionsList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var rp = new Report.D09Division.ReportDivisionWindow(id, "Tbl09Divisions");
            rp.Show();
        }
        private void Tbl15SubdivisionsList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var rp = new ReportSubdivisionWindow(id, "Tbl15Subdivisions");
            rp.Show();
        }
        private void Tbl18SuperclassesList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var rp = new ReportSuperclassWindow(id, "Tbl18Superclasses");
            rp.Show();
        }
        private void Tbl21ClassesList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var rp = new ReportClassWindow(id, "Tbl21Classes");
            rp.Show();
        }
        private void Tbl24SubclassesList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var rp = new ReportSubclassWindow(id, "Tbl24Subclasses");
            rp.Show();
        }
        private void Tbl27InfraclassesList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var rp = new ReportInfraclassWindow(id, "Tbl27Infraclasses");
            rp.Show();
        }
        private void Tbl30LegiosList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var rp = new ReportLegioWindow(id, "Tbl30Legios");
            rp.Show();
        }
        private void Tbl33OrdosList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var rp = new ReportOrdoWindow(id, "Tbl33Ordos");
            rp.Show();
        }
        private void Tbl36SubordosList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var rp = new ReportSubordoWindow(id, "Tbl36Subordos");
            rp.Show();
        }
        private void Tbl39InfraordosList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var rp = new ReportInfraordoWindow(id, "Tbl39Infraordos");
            rp.Show();
        }
        private void Tbl42SuperfamiliesList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var rp = new ReportSuperfamilyWindow(id, "Tbl42Superfamilies");
            rp.Show();
        }
        private void Tbl45FamiliesList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var rp = new ReportFamilyWindow(id, "Tbl45Families");
            rp.Show();
        }
        private void Tbl48SubfamiliesList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var rp = new ReportSubfamilyWindow(id, "Tbl48Subfamilies");
            rp.Show();
        }
        private void Tbl51InfrafamiliesList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var rp = new ReportInfrafamilyWindow(id, "Tbl51Infrafamilies");
            rp.Show();
        }
        private void Tbl54SupertribussesList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var rp = new ReportSupertribusWindow(id, "Tbl54Supertribusses");
            rp.Show();
        }
        private void Tbl57TribussesList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var rp = new ReportTribusWindow(id, "Tbl57Tribusses");
            rp.Show();
        }
        private void Tbl60SubtribussesList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var rp = new ReportSubtribusWindow(id, "Tbl60Subtribusses");
            rp.Show();
        }
        private void Tbl63InfratribussesList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var rp = new ReportInfratribusWindow(id, "Tbl63Infratribusses");
            rp.Show();
        }
        private void Tbl66GenussesList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var rp = new ReportGenusWindow(id, "Tbl66Genusses");
            rp.Show();
        }
        private void Tbl69FiSpeciessesList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var fiSpecies = _extReportBasicGet.GetFiSpeciesSingleByFiSpeciesId(id);
            var rp = new Window();

            if (string.IsNullOrEmpty(fiSpecies.Subspecies) && string.IsNullOrEmpty(fiSpecies.Divers))
            {
                rp = new ReportFiSpeciesWindow(id, "Tbl69FiSpeciesses");
            }
            else
            {
                rp = new ReportFiSpeciesSubWindow(id, "Tbl69FiSpeciesses");
            }

            rp.Show();
        }

        private void Tbl72PlSpeciessesList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lvItem = sender as ListViewItem;

            if (lvItem == null) return;
            var id = (dynamic)lvItem.Tag;
            var plSpecies = _extReportBasicGet.GetPlSpeciesSingleByPlSpeciesId(id);
            var rp = new Window();

            if (string.IsNullOrEmpty(plSpecies.Subspecies) && string.IsNullOrEmpty(plSpecies.Divers))
            {
                rp = new ReportPlSpeciesWindow(id, "Tbl72PlSpeciesses");
            }
            else
            {
                rp = new ReportPlSpeciesSubWindow(id, "Tbl72PlSpeciesses");
            }
            rp.Show();
        }

        public void Connect(int connectionId, object target)
        {
            throw new NotImplementedException();
        }
    }
}
