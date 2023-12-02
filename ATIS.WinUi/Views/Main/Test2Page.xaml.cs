using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using ATIS.WinUi.Maps;
using ATIS.WinUi.ViewModels.Main;
using CommunityToolkit.WinUI.UI.Controls.TextToolbarSymbols;
using Mapsui.Extensions;
using Mapsui.Tiling;
using Mapsui;
using Mapsui.Providers.Wms;
using Mapsui.UI.WinUI;
using ATIS.WinUi.Helpers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ATIS.WinUi.Views.Main;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Test2Page : Page
{
    public Test2ViewModel ViewModel
    {
        get; set;
    }
    private readonly AllDialogs _allDialogs = new();

    public Test2Page()
    {
        //var permis = new Permission();
        //if (permis.RolePermission())
        {
            ViewModel = App.GetService<Test2ViewModel>();
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
