using System.Windows.Controls;

namespace ATIS.Ui.Views.Database
{
    /// <summary>
    /// Interaktionslogik für Tbl03PhylumsView.xaml
    /// </summary>
    public partial class Tbl06PhylumsView : UserControl
    {
        public Tbl06PhylumsView()
        {
            DataContext = new Tbl06PhylumsViewModel();

            InitializeComponent();
        }
    }
}
