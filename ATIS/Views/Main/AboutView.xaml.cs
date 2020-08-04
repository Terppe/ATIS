using System.Windows.Controls;

namespace ATIS.Ui.Views.Main
{
    /// <summary>
    /// Interaktionslogik für AboutView.xaml
    /// </summary>
    public partial class AboutView : UserControl
    {
        public AboutView()
        {
            DataContext = new AboutViewModel();

            InitializeComponent();
        }
    }
}
