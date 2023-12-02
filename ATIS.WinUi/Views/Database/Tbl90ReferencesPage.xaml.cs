
// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System.ComponentModel.Design;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.ViewModels.Database;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;


//  Tbl90ReferencesPage.xaml.cs Skriptdatum:  26.04.2023  10:32     

namespace ATIS.WinUi.Views.Database;


/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Tbl90ReferencesPage : Page
{
    #region [Private Data Members]
    public Tbl90ReferencesViewModel ViewModel { get; } = null!;
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]

    public Tbl90ReferencesPage()
    {
        //  var permis = new Permission();
        if (Permission.RolePermission())
        {
            ViewModel = App.GetService<Tbl90ReferencesViewModel>();
            InitializeComponent();
        }
        else
        {
            _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        }
    }

    private void ReferenceSearchAutoSuggestBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        ReferenceSearchAutoSuggestBox.Visibility = Visibility.Visible;
        ReferenceSearchAutoSuggestBox.Focus(FocusState.Programmatic);
    }


}

