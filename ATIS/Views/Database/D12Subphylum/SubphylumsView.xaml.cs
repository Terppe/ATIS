using System.Windows.Controls;

namespace ATIS.Ui.Views.Database.D12Subphylum
{
    /// <summary>
    /// Interaktionslogik für SubphylumsView.xaml
    /// </summary>
    public partial class SubphylumsView : UserControl
    {
        public SubphylumsView()
        {
            DataContext = new SubphylumsViewModel();

            InitializeComponent();
        }
    }
}
