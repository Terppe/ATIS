using System.Windows.Controls;
using MahApps.Metro.Controls;

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

        private void FlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var flipview = ((FlipView)sender);
            switch (flipview.SelectedIndex)
            {
                case 0:
                    flipview.BannerText = "saltwaterfishes!";
                    break;
                case 1:
                    flipview.BannerText = "hippos!";
                    break;
                case 2:
                    flipview.BannerText = "aquarium!";
                    break;
                case 3:
                    flipview.BannerText = "home!";
                    break;
                case 4:
                    flipview.BannerText = "private!";
                    break;
                case 5:
                    flipview.BannerText = "settings!";
                    break;
            }
        }

    }
}
