using System.Configuration;
using System.Diagnostics;
using Windows.Devices.Input;
using ATIS.WinUi.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Mapsui.Tiling;
using System.Net;
using Mapsui;

namespace ATIS.WinUi.Views.Main;

public sealed partial class MainPage : Page
{

    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
        MapControl.Map.Layers.Add(OpenStreetMap.CreateTileLayer());
    }


    private void EmailButton_OnClick(object sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo("mailto:rudolf@terppe.de") { UseShellExecute = true });
    }

    private void GithubButton_OnClick(object sender, RoutedEventArgs e)
    {
        var psi = new ProcessStartInfo
        {
            FileName = ConfigurationManager.AppSettings["GitHub"] ?? string.Empty,
            UseShellExecute = true
        };
        Process.Start(psi);
    }

    private void ItisUsaButton_OnClick(object sender, RoutedEventArgs e)
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

    private void MexicoButton_OnClick(object sender, RoutedEventArgs e)
    {
        var psi = new ProcessStartInfo
        {
            FileName = ConfigurationManager.AppSettings["Mexico"]!,
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

    private void GlobalBioButton_OnClick(object sender, RoutedEventArgs e)
    {
        var psi = new ProcessStartInfo
        {
            FileName = ConfigurationManager.AppSettings["GlobalBio"]!,
            UseShellExecute = true
        };
        Process.Start(psi);
    }


}
