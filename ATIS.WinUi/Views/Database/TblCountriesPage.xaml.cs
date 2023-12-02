
// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ATIS.WinUi.Helpers;
using ATIS.WinUi.ViewModels.Database;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;


//  TblCountriesPage.xaml.cs Skriptdatum:   26.04.2023 12:32       

namespace ATIS.WinUi.Views.Database;


/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class TblCountriesPage : Page
{
    #region [Private Data Members]
    public TblCountriesViewModel ViewModel { get; } = null!;
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]

    public TblCountriesPage()
    {
        //  var permis = new Permission();
        if (Permission.RolePermission())
        {
            ViewModel = App.GetService<TblCountriesViewModel>();
            InitializeComponent();
        }
        else
        {
            _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        }
    }

    private void CountrySearchAutoSuggestBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        CountrySearchAutoSuggestBox.Visibility = Visibility.Visible;
        CountrySearchAutoSuggestBox.Focus(FocusState.Programmatic);
    }

}

