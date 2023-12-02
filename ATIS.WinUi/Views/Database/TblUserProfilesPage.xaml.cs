
// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ATIS.WinUi.Helpers;
using ATIS.WinUi.ViewModels.Database;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;


//  TblUserProfilesPage.xaml.cs Skriptdatum:   26.04.2023  10:32     

namespace ATIS.WinUi.Views.Database;


/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class TblUserProfilesPage : Page
{
    #region [Private Data Members]
    public TblUserProfilesViewModel ViewModel { get; } = null!;
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]

    public TblUserProfilesPage()
    {
        //  var permis = new Permission();
        if (Permission.RolePermission())
        {
            ViewModel = App.GetService<TblUserProfilesViewModel>();
            InitializeComponent();
        }
        else
        {
            _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        }
    }

    private void UserProfileSearchAutoSuggestBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        UserProfileSearchAutoSuggestBox.Visibility = Visibility.Visible;
        UserProfileSearchAutoSuggestBox.Focus(FocusState.Programmatic);
    }



}

