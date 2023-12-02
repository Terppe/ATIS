
// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ATIS.WinUi.Helpers;
using ATIS.WinUi.ViewModels.Database;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;


//  Tbl68SpeciesgroupsPage.xaml.cs Skriptdatum:  14.04.2023  10:32     

namespace ATIS.WinUi.Views.Database;


/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Tbl68SpeciesgroupsPage : Page
{
    #region [Private Data Members]
    public Tbl68SpeciesgroupsViewModel ViewModel { get; } = null!;
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]

    public Tbl68SpeciesgroupsPage()
    {
        //  var permis = new Permission();
        if (Permission.RolePermission())
        {
            ViewModel = App.GetService<Tbl68SpeciesgroupsViewModel>();
            InitializeComponent();
        }
        else
        {
            _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        }
    }

    private void SpeciesgroupSearchAutoSuggestBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        SpeciesgroupSearchAutoSuggestBox.Visibility = Visibility.Visible;
        SpeciesgroupSearchAutoSuggestBox.Focus(FocusState.Programmatic);
    }

    private void TabView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (SpeciesgroupTabView.SelectedIndex)
        {
            case 0:
            {
                if (ViewModel.Tbl68SpeciesgroupsList is { Count: 0 })
                {
                    SpeciesgroupClearMethod();
                }
                break;
            }
            case 1:
            {
                if (ViewModel.Tbl69FiSpeciessesList is { Count: 0 })
                {
                        FiSpeciesClearMethod();
                }
                break;
            }
            case 2:
            {
                if (ViewModel.Tbl72PlSpeciessesList is { Count: 0 })
                {
                    PlSpeciesClearMethod();
                }
                break;
            }
        }
    }


    private void SpeciesgroupClearMethod()
    {
        SpeciesgroupId.Text = string.Empty;
        SpeciesgroupName.Text = string.Empty;
        SubspeciesgroupName.Text = string.Empty;
        GenusCombo.SelectedIndex = -1;
        ValidCheckSpeciesgroup.IsChecked = false;
        ValidYearTextSpeciesgroup.Text = string.Empty;
        AuthorTextSpeciesgroup.Text = string.Empty;
        AuthorYearTextSpeciesgroup.Text = string.Empty;
        InfoSpeciesgroup.Text = string.Empty;
        SynonymSpeciesgroup.Text = string.Empty;
        EngNameSpeciesgroup.Text = string.Empty;
        GerNameSpeciesgroup.Text = string.Empty;
        FraNameSpeciesgroup.Text = string.Empty;
        PorNameSpeciesgroup.Text = string.Empty;
        MemoSpeciesgroup.Text = string.Empty;
    }
    private void FiSpeciesClearMethod()
    {
        FiSpeciesId.Text = string.Empty;
        GenusCombo.SelectedIndex = -1;
        FiSpeciesName.Text = string.Empty;
        FiSpeciesSubspecies.Text = string.Empty;
        FiSpeciesDivers.Text = string.Empty;
        SpeciesgroupCombo.SelectedIndex = -1;
        ValidCheckFiSpecies.IsChecked = false;
        ValidYearTextFiSpecies.Text = string.Empty;
        AuthorTextFiSpecies.Text = string.Empty;
        AuthorYearTextFiSpecies.Text = string.Empty;
        FishLengthTextFiSpecies.Text = string.Empty;
        BasinLengthTextFiSpecies.Text = string.Empty;
        ImporterTextFiSpecies.Text = string.Empty;
        ImportingYearTextFiSpecies.Text = string.Empty;
        TradeNameFiSpecies.Text = string.Empty;
        MemoSpeciesFiSpecies.Text = string.Empty;
        KarnivoreCheckFiSpecies.IsChecked = false;
        HerbivoreCheckFiSpecies.IsChecked = false;
        LimnivoreCheckFiSpecies.IsChecked = false;
        OmnivoreCheckFiSpecies.IsChecked = false;
        MemoFoodsFiSpecies.Text = string.Empty;
        Difficult1CheckFiSpecies.IsChecked = false;
        Difficult2CheckFiSpecies.IsChecked = false;
        Difficult3CheckFiSpecies.IsChecked = false;
        Difficult4CheckFiSpecies.IsChecked = false;
        Ph1TextFiSpecies.Text = string.Empty;
        Ph2TextFiSpecies.Text = string.Empty;
        Temp1TextFiSpecies.Text = string.Empty;
        Temp2TextFiSpecies.Text = string.Empty;
        Hardness1TextFiSpecies.Text = string.Empty;
        Hardness2TextFiSpecies.Text = string.Empty;
        CarboHardness1TextFiSpecies.Text = string.Empty;
        CarboHardness2TextFiSpecies.Text = string.Empty;
        MemoTechFiSpecies.Text = string.Empty;
        MemoHusbandryFiSpecies.Text = string.Empty;
        MemoBreedingFiSpecies.Text = string.Empty;
        MemoBuiltFiSpecies.Text = string.Empty;
        MemoColorFiSpecies.Text = string.Empty;
        MemoSozialFiSpecies.Text = string.Empty;
        MemoDomorphismFiSpecies.Text = string.Empty;
        MemoSpecialFiSpecies.Text = string.Empty;
    }
    private void PlSpeciesClearMethod()
    {
        PlSpeciesId.Text = string.Empty;
        GenusCombo1.SelectedIndex = -1;
        PlSpeciesName.Text = string.Empty;
        PlSpeciesSubspecies.Text = string.Empty;
        PlSpeciesDivers.Text = string.Empty;
        SpeciesgroupCombo1.SelectedIndex = -1;
        ValidCheckPlSpecies.IsChecked = false;
        ValidYearTextPlSpecies.Text = string.Empty;
        AuthorTextPlSpecies.Text = string.Empty;
        AuthorYearTextPlSpecies.Text = string.Empty;
        PlantLengthTextPlSpecies.Text = string.Empty;
        BasinHeightTextPlSpecies.Text = string.Empty;
        ImporterTextPlSpecies.Text = string.Empty;
        ImportingYearTextPlSpecies.Text = string.Empty;
        TradeNamePlSpecies.Text = string.Empty;
        MemoSpeciesPlSpecies.Text = string.Empty;
        Difficult1CheckPlSpecies.IsChecked = false;
        Difficult2CheckPlSpecies.IsChecked = false;
        Difficult3CheckPlSpecies.IsChecked = false;
        Difficult4CheckPlSpecies.IsChecked = false;
        Ph1TextPlSpecies.Text = string.Empty;
        Ph2TextPlSpecies.Text = string.Empty;
        Temp1TextPlSpecies.Text = string.Empty;
        Temp2TextPlSpecies.Text = string.Empty;
        Hardness1TextPlSpecies.Text = string.Empty;
        Hardness2TextPlSpecies.Text = string.Empty;
        CarboHardness1TextPlSpecies.Text = string.Empty;
        CarboHardness2TextPlSpecies.Text = string.Empty;
        MemoTechPlSpecies.Text = string.Empty;
        MemoCulturePlSpecies.Text = string.Empty;
        MemoReproductionPlSpecies.Text = string.Empty;
        MemoBuiltPlSpecies.Text = string.Empty;
        MemoColorPlSpecies.Text = string.Empty;
        MemoGlobalPlSpecies.Text = string.Empty;
    }

}

