// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ATIS.WinUi.Helpers;
using ATIS.WinUi.ViewModels.Database;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;


//  Tbl90RefExpertsPage.xaml.cs Skriptdatum:  25.04.2023  10:32     

namespace ATIS.WinUi.Views.Database;


/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Tbl90RefExpertsPage : Page
{
    #region [Private Data Members]
    public Tbl90RefExpertsViewModel ViewModel { get; } = null!;
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]

    public Tbl90RefExpertsPage()
    {
        //  var permis = new Permission();
        if (Permission.RolePermission())
        {
            ViewModel = App.GetService<Tbl90RefExpertsViewModel>();
            InitializeComponent();
        }
        else
        {
            _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        }
    }

    private void RefExpertSearchAutoSuggestBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        RefExpertSearchAutoSuggestBox.Visibility = Visibility.Visible;
        RefExpertSearchAutoSuggestBox.Focus(FocusState.Programmatic);
    }



}