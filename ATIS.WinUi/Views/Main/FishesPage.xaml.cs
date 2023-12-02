using Microsoft.UI.Xaml.Controls;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.ViewModels.Main;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ATIS.WinUi.Views.Main;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class FishesPage : Page
{
    public FishesViewModel ViewModel
    {
        get; set;
    }
    private readonly AllDialogs _allDialogs = new();

    public FishesPage()
    {
        //var permis = new Permission();
        //if (permis.RolePermission())
        {
            ViewModel = App.GetService<FishesViewModel>();
            InitializeComponent();
        }
        //else
        //{
        //    _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        //}
    }
}