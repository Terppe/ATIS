using System;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;

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

            LbVersion.Content = ".NET Core Version: " + Environment.Version;
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
            Process.Start(new ProcessStartInfo("mailto:rudolf@terppe.de") { UseShellExecute = true });
        }

        private void DonateButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://pledgie.com/campaigns/31029");
        }

        private void ItisUSAButton_OnClick(object sender, RoutedEventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = ConfigurationManager.AppSettings["ItisUSA"]!,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
        private void ItisCanadaButton_OnClick(object sender, RoutedEventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = ConfigurationManager.AppSettings["ItisCanada"]!,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void CoLifeButton_OnClick(object sender, RoutedEventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = ConfigurationManager.AppSettings["CoLife"]!,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void EOLButton_OnClick(object sender, RoutedEventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = ConfigurationManager.AppSettings["EOL"]!,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
        private void MexicoButton_OnClick(object sender, RoutedEventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = ConfigurationManager.AppSettings["Mexico"]!,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void GlobalBioButton_OnClick(object sender, RoutedEventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = ConfigurationManager.AppSettings["GlobalBio"]!,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void HelpLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            var thisLink = (Hyperlink)sender;
            var navigateUri = thisLink.NavigateUri.ToString();
            Process.Start(new ProcessStartInfo(navigateUri));
            e.Handled = true;
        }
    }
}
