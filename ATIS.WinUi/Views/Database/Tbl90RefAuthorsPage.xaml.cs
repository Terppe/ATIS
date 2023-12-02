
// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ATIS.WinUi.Helpers;
using ATIS.WinUi.ViewModels.Database;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;


//  Tbl90RefAuthorsPage.xaml.cs Skriptdatum:  24.04.2023  10:32     

namespace ATIS.WinUi.Views.Database;


/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Tbl90RefAuthorsPage : Page
{
    #region [Private Data Members]
    public Tbl90RefAuthorsViewModel ViewModel { get; } = null!;
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]

    public Tbl90RefAuthorsPage()
    {
        //  var permis = new Permission();
        if (Permission.RolePermission())
        {
            ViewModel = App.GetService<Tbl90RefAuthorsViewModel>();
            InitializeComponent();
        }
        else
        {
            _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        }
    }

    private void RefAuthorSearchAutoSuggestBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        RefAuthorSearchAutoSuggestBox.Visibility = Visibility.Visible;
        RefAuthorSearchAutoSuggestBox.Focus(FocusState.Programmatic);
    }

}

