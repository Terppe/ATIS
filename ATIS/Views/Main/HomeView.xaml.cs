using System.Windows.Controls;

namespace ATIS.Ui.Views.Main
{
    /// <summary>
    /// Interaktionslogik für HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            DataContext = new HomeViewModel();

            InitializeComponent();

            LbVersion.Content = ".NET Core Version: " + System.Environment.Version;

        }
    }
}
