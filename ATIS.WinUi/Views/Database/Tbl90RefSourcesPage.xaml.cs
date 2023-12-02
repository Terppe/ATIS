using ATIS.WinUi.Helpers;
using ATIS.WinUi.ViewModels.Database;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ATIS.WinUi.Views.Database;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Tbl90RefSourcesPage : Page
{
    #region [Private Data Members]
    public Tbl90RefSourcesViewModel ViewModel { get; } = null!;
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]

    public Tbl90RefSourcesPage()
    {
        //  var permis = new Permission();
        if (Permission.RolePermission())
        {
            ViewModel = App.GetService<Tbl90RefSourcesViewModel>();
            InitializeComponent();
        }
        else
        {
            _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        }
    }

    private void RefSourceSearchAutoSuggestBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        RefSourceSearchAutoSuggestBox.Visibility = Visibility.Visible;
        RefSourceSearchAutoSuggestBox.Focus(FocusState.Programmatic);
    }
}
