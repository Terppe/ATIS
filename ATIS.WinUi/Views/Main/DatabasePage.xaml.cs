using Microsoft.UI.Xaml.Controls;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.ViewModels.Main;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ATIS.WinUi.Views.Main;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class DatabasePage : Page
{
    public DatabaseViewModel ViewModel { get; set; }
    private readonly AllDialogs _allDialogs = new();

    public DatabasePage()
    {
        //var permis = new Permission();
        //if (permis.RolePermission())
        {
            ViewModel = App.GetService<DatabaseViewModel>();
            InitializeComponent();
        }
        //else
        //{
        //    _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        //}
    }

    ////  //  show the dialog
    ////    var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
    //}
    //private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
    //{
    //    // WpfMessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.AppCloseQuestion, MessageBoxButton.OK, MessageBoxImage.Warning);
    //}


}