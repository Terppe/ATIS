using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ATIS.WinUi.ViewModels.Authentication;
using CommunityToolkit.Mvvm.DependencyInjection;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ATIS.WinUi.Controls;

public sealed partial class RegisterControl : UserControl
{
    public AuthenticationViewModel ViewModel
    {
        get;
    }

    public RegisterControl()
    {
        this.InitializeComponent();
        ViewModel = App.GetService<AuthenticationViewModel>();
        InitializeComponent();
    }

    private void Content_TextChanged(object sender, RoutedEventArgs e)
    {
        if (!TbEmail.Text.Equals(string.Empty) && TxtPass.Password.Equals(TxtConfirmPass.Password))
            BtRegisterSave.IsEnabled = true;
        else
            BtRegisterSave.IsEnabled = false;
    }
}