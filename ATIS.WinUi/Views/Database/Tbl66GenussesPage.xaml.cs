// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ATIS.WinUi.Helpers;
using ATIS.WinUi.ViewModels.Database;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

//  Tbl66GenussesPage.xaml.cs Skriptdatum:  01.04.2023  10:32     

namespace ATIS.WinUi.Views.Database;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Tbl66GenussesPage : Page
{
    #region [Private Data Members]
    public Tbl66GenussesViewModel ViewModel { get; } = null!;
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]

    public Tbl66GenussesPage()
    {
        //  var permis = new Permission();
        if (Permission.RolePermission())
        {
            ViewModel = App.GetService<Tbl66GenussesViewModel>();
            InitializeComponent();
        }
        else
        {
            _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        }
    }

    private void GenusSearchAutoSuggestBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        GenusSearchAutoSuggestBox.Visibility = Visibility.Visible;
        GenusSearchAutoSuggestBox.Focus(FocusState.Programmatic);
    }

    private void TabView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (GenusTabView.SelectedIndex)
        {
            case 0:
            {
                if (ViewModel.Tbl63InfratribussesList is { Count: 0 })
                {
                    InfratribusClearMethod();
                }
                break;
            }
            case 1:
            {
                if (ViewModel.Tbl66GenussesList is { Count: 0 })
                {
                    GenusClearMethod();
                }
                break;
            }
            case 2:
            {
                if (ViewModel.Tbl69FiSpeciessesList is { Count: 0 })
                {
                    FiSpeciesClearMethod();
                }
                break;
            }
            case 3:
            {
                if (ViewModel.Tbl72PlSpeciessesList is { Count: 0 })
                {
                    PlSpeciesClearMethod();
                }
                break;
            }
            case 4:
            {
                if (ViewModel.Tbl90ReferenceExpertsList is { Count: 0 })
                {
                    ReferenceExpertClearMethod();
                }
                if (ViewModel.Tbl90ReferenceSourcesList is { Count: 0 })
                {
                    ReferenceSourceClearMethod();
                }
                if (ViewModel.Tbl90ReferenceAuthorsList is { Count: 0 })
                {
                    ReferenceAuthorClearMethod();
                }
                break;
            }
            case 5:
            {
                if (ViewModel.Tbl93CommentsList is { Count: 0 })
                {
                    CommentClearMethod();
                }
                break;
            }
        }
    }

    private void InfratribusClearMethod()
    {
        InfratribusId.Text = string.Empty;
        InfratribusName.Text = string.Empty;
        SubtribusCombo.SelectedIndex = -1;
        ValidCheckInfratribus.IsChecked = false;
        ValidYearTextInfratribus.Text = string.Empty;
        AuthorTextInfratribus.Text = string.Empty;
        AuthorYearTextInfratribus.Text = string.Empty;
        InfoInfratribus.Text = string.Empty;
        SynonymInfratribus.Text = string.Empty;
        EngNameInfratribus.Text = string.Empty;
        GerNameInfratribus.Text = string.Empty;
        FraNameInfratribus.Text = string.Empty;
        PorNameInfratribus.Text = string.Empty;
        MemoInfratribus.Text = string.Empty;
    }
    private void GenusClearMethod()
    {
        GenusId.Text = string.Empty;
        GenusName.Text = string.Empty;
        InfratribusCombo.SelectedIndex = -1;
        ValidCheckGenus.IsChecked = false;
        ValidYearTextGenus.Text = string.Empty;
        AuthorTextGenus.Text = string.Empty;
        AuthorYearTextGenus.Text = string.Empty;
        InfoGenus.Text = string.Empty;
        SynonymGenus.Text = string.Empty;
        EngNameGenus.Text = string.Empty;
        GerNameGenus.Text = string.Empty;
        FraNameGenus.Text = string.Empty;
        PorNameGenus.Text = string.Empty;
        MemoGenus.Text = string.Empty;
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

    private void ReferenceExpertClearMethod()
    {
        ReferenceIdExpert.Text = string.Empty;
        ReferenceExpertId.Text = string.Empty;
        ReferenceExpertCombo.SelectedIndex = -1;
        ValidCheckReferenceExpert.IsChecked = false;
        ValidYearTextReferenceExpert.Text = string.Empty;
        InfoReferenceExpert.Text = string.Empty;
        MemoReferenceExpert.Text = string.Empty;
    }
    private void ReferenceSourceClearMethod()
    {
        ReferenceIdSource.Text = string.Empty;
        ReferenceSourceId.Text = string.Empty;
        ReferenceSourceCombo.SelectedIndex = -1;
        ValidCheckReferenceSource.IsChecked = false;
        ValidYearTextReferenceSource.Text = string.Empty;
        InfoReferenceSource.Text = string.Empty;
        MemoReferenceSource.Text = string.Empty;
    }
    private void ReferenceAuthorClearMethod()
    {
        ReferenceIdAuthor.Text = string.Empty;
        ReferenceAuthorId.Text = string.Empty;
        ReferenceAuthorCombo.SelectedIndex = -1;
        ValidCheckReferenceAuthor.IsChecked = false;
        ValidYearTextReferenceAuthor.Text = string.Empty;
        InfoReferenceAuthor.Text = string.Empty;
        MemoReferenceAuthor.Text = string.Empty;
    }
    private void CommentClearMethod()
    {
        CommentId.Text = string.Empty;
        ValidCheckComment.IsChecked = false;
        ValidYearTextComment.Text = string.Empty;
        InfoComment.Text = string.Empty;
        MemoComment.Text = string.Empty;
    }


}

