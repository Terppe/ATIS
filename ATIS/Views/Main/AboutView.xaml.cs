using System.Configuration;
using System.Windows.Controls;
using System.Windows.Media;

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

            var background = ConfigurationManager.AppSettings.Get("BackgroundBrush");
            var conver = new BrushConverter();
            Background = (Brush)conver.ConvertFromString(background) as SolidColorBrush;

        }
    }
}
