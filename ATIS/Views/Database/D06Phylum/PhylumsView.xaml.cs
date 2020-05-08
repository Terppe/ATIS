using System.Windows.Controls;

namespace ATIS.Ui.Views.Database.D06Phylum
{
    /// <summary>
    /// Interaktionslogik für PhylumsView.xaml
    /// </summary>
    public partial class PhylumsView : UserControl
    {
        public PhylumsView()
        {
            DataContext = new PhylumsViewModel();

            InitializeComponent();
        }
    }
}
