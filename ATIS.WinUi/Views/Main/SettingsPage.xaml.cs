using ATIS.WinUi.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace ATIS.WinUi.Views.Main;

// TODO: Set the URL for your privacy policy by updating SettingsPage_PrivacyTermsLink.NavigateUri in Resources.resw.
public sealed partial class SettingsPage : Page
{
    public SettingsViewModel ViewModel
    {
        get;
    }

    public SettingsPage()
    {
        ViewModel = App.GetService<SettingsViewModel>();
        InitializeComponent();

        LbVersion.Text = "Copyright © Rudolf Terppé | 2020-2023 | .NET Core Version: " + Environment.Version + " + OS-Version " + Environment.OSVersion;

    }

    private void ColorSpectrumShapeRadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ColorPicker.ColorSpectrumShape = ColorSpectrumShapeRadioButtons.SelectedItem switch
        {
            "Box" => ColorSpectrumShape.Box,
            _ => ColorSpectrumShape.Ring,
        };
    }
}
