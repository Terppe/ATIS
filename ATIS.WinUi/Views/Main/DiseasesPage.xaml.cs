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
using ATIS.WinUi.Helpers;
using ATIS.WinUi.ViewModels.Main;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ATIS.WinUi.Views.Main;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class DiseasesPage : Page
{
    public FishesViewModel ViewModel
    {
        get; set;
    }
    private readonly AllDialogs _allDialogs = new();

    public DiseasesPage()
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