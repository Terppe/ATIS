using System.Configuration;
using System.Windows.Controls;
using System.Windows.Media;

namespace ATIS.Ui.Views.Main
{
    /// <summary>
    /// Interaktionslogik für FishesView.xaml
    /// </summary>
    public partial class FishesView : UserControl
    {
        public FishesView()
        {
            InitializeComponent();

            var background = ConfigurationManager.AppSettings.Get("BackgroundBrush");
            var conver = new BrushConverter();
            Background = (Brush)conver.ConvertFromString(background) as SolidColorBrush;

        }
    }
}
