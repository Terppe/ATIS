using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;

namespace ATIS.Ui.Views.Search
{
    /// <summary>
    /// Interaktionslogik für SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : MetroWindow
    {
        public SearchWindow()
        {

        }

        public SearchWindow(SearchQuickViewModel viewModel)
        {
            //    DataContext = new SearchQuickViewModel();
            DataContext = viewModel;
            InitializeComponent();
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
                //           printDialog.PrintDocument(((IDocumentPaginatorSource)FlowDocument).DocumentPaginator, "This is a Flow Document");
                printDialog.PrintVisual(LayoutRoot, "Landscape broken Grid print");

        }

        public string FilterText { get; set; }



    }
}