using ATIS.WinUi.Helpers;
using ATIS.WinUi.ViewModels.Main;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ATIS.WinUi.Views.Main;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class PlantsPage : Page
{
    public PlantsViewModel ViewModel
    {
        get; set;
    }
    private readonly AllDialogs _allDialogs = new();

    public PlantsPage()
    {
        //var permis = new Permission();
        //if (permis.RolePermission())
        {
            ViewModel = App.GetService<PlantsViewModel>();
            InitializeComponent();
        }
        //else
        //{
        //    _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        //}
    }
}