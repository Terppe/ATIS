using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
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

        //private void FlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var flipview = ((FlipView)sender);
        //    switch (flipview.SelectedIndex)
        //    {
        //        case 0:
        //            flipview.BannerText = "saltwaterfishes!";
        //            break;
        //        case 1:
        //            flipview.BannerText = "hippos!";
        //            break;
        //        case 2:
        //            flipview.BannerText = "aquarium!";
        //            break;
        //        case 3:
        //            flipview.BannerText = "home!";
        //            break;
        //        case 4:
        //            flipview.BannerText = "private!";
        //            break;
        //        case 5:
        //            flipview.BannerText = "settings!";
        //            break;
        //    }
        //}


        private void GitHubButton_OnClick(object sender, RoutedEventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = ConfigurationManager.AppSettings["GitHub"] ?? string.Empty,
                UseShellExecute = true
            };
            Process.Start(psi);
        }


        private void EmailButton_OnClick(object sender, RoutedEventArgs e)
        {
            //var body = "This is a body of a message";
            //var recipients = String.Join(",", "rudolf@terppe.de", "marion@terppe.de");
            //string mailto = $"mailto:{recipients}?Subject={"Subject of message"}&Body={body}";
            //mailto = Uri.EscapeUriString(mailto);
            //// System.Diagnostics.Process.Start(mailto);
            //System.Diagnostics.Process.Start(new ProcessStartInfo(mailto) { UseShellExecute = true });
            ////    Process.Start("mailto://rudolf@terppe.de");
            System.Diagnostics.Process.Start(new ProcessStartInfo("mailto:rudolf@terppe.de") { UseShellExecute = true });
        }

        private void DonateButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://pledgie.com/campaigns/31029");
        }

        private void ItisUSAButton_OnClick(object sender, RoutedEventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "https://itis.gov",
                UseShellExecute = true
            };
            Process.Start(psi);
        }
        private void ItisCandaButton_OnClick(object sender, RoutedEventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "http://www.cbif.gc.ca",
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void CoLifeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "http://www.catalogueoflife.org/annual-checklist/2015/",
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void EOLButton_OnClick(object sender, RoutedEventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "http://www.eol.org/",
                UseShellExecute = true
            };
            Process.Start(psi);
        }
        private void MexicoButton_OnClick(object sender, RoutedEventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "http://www.conabio.gob.mx/",
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void GlobalBioButton_OnClick(object sender, RoutedEventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "http://www.gbif.org/",
                UseShellExecute = true
            };
            Process.Start(psi);
        }
        //mexico  http://www.conabio.gob.mx/
        //Species 2000   http://www.sp2000.org/
        //Global Bio  http://www.gbif.org/
        //Catalog of Life  http://www.catalogueoflife.org/annual-checklist/2015/
        //EOL  http://www.eol.org/

        private void HelpLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Hyperlink thisLink = (Hyperlink)sender;
            string navigateUri = thisLink.NavigateUri.ToString();
            Process.Start(new ProcessStartInfo(navigateUri));
            e.Handled = true;
        }
    }
}
