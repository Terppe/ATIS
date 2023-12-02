
// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ATIS.WinUi.Helpers;
using ATIS.WinUi.ViewModels.Database;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;


//  Tbl39InfraordosPage.xaml.cs Skriptdatum:  31.03.2023  10:32     

namespace ATIS.WinUi.Views.Database;


/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Tbl39InfraordosPage : Page
{
    #region [Private Data Members]
    public Tbl39InfraordosViewModel ViewModel { get; } = null!;
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]

    public Tbl39InfraordosPage()
    {
        //  var permis = new Permission();
        if (Permission.RolePermission())
        {
            ViewModel = App.GetService<Tbl39InfraordosViewModel>();
            InitializeComponent();
        }
        else
        {
            _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        }
    }

    private void InfraordoSearchAutoSuggestBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        InfraordoSearchAutoSuggestBox.Visibility = Visibility.Visible;
        InfraordoSearchAutoSuggestBox.Focus(FocusState.Programmatic);
    }

    private void TabView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (InfraordoTabView.SelectedIndex)
        {
            case 0:
            {
                if (ViewModel.Tbl36SubordosList is { Count: 0 })
                {
                    SubordoClearMethod();
                }
                break;
            }
            case 1:
            {
                if (ViewModel.Tbl39InfraordosList is { Count: 0 })
                {
                    InfraordoClearMethod();
                }
                break;
            }
            case 2:
            {
                if (ViewModel.Tbl42SuperfamiliesList is { Count: 0 })
                {
                    SuperfamilyClearMethod();
                }
                break;
            }
            case 3:
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
            case 4:
            {
                if (ViewModel.Tbl93CommentsList is { Count: 0 })
                {
                    CommentClearMethod();
                }
                break;
            }
        }
    }

    private void SubordoClearMethod()
    {
        SubordoId.Text = string.Empty;
        SubordoName.Text = string.Empty;
        OrdoCombo.SelectedIndex = -1;
        ValidCheckSubordo.IsChecked = false;
        ValidYearTextSubordo.Text = string.Empty;
        AuthorTextSubordo.Text = string.Empty;
        AuthorYearTextSubordo.Text = string.Empty;
        InfoSubordo.Text = string.Empty;
        SynonymSubordo.Text = string.Empty;
        EngNameSubordo.Text = string.Empty;
        GerNameSubordo.Text = string.Empty;
        FraNameSubordo.Text = string.Empty;
        PorNameSubordo.Text = string.Empty;
        MemoSubordo.Text = string.Empty;
    }

    private void InfraordoClearMethod()
    {
        InfraordoId.Text = string.Empty;
        InfraordoName.Text = string.Empty;
        //   SubordoCombo.Text = string.Empty;
        SubordoCombo.SelectedIndex = -1;
        ValidCheckInfraordo.IsChecked = false;
        ValidYearTextInfraordo.Text = string.Empty;
        AuthorTextInfraordo.Text = string.Empty;
        AuthorYearTextInfraordo.Text = string.Empty;
        InfoInfraordo.Text = string.Empty;
        SynonymInfraordo.Text = string.Empty;
        EngNameInfraordo.Text = string.Empty;
        GerNameInfraordo.Text = string.Empty;
        FraNameInfraordo.Text = string.Empty;
        PorNameInfraordo.Text = string.Empty;
        MemoInfraordo.Text = string.Empty;
    }
    private void SuperfamilyClearMethod()
    {
        SuperfamilyId.Text = string.Empty;
        SuperfamilyName.Text = string.Empty;
        //  InfraordoCombo1.Text = string.Empty;
        InfraordoCombo.SelectedIndex = -1;
        ValidCheckSuperfamily.IsChecked = false;
        ValidYearTextSuperfamily.Text = string.Empty;
        AuthorTextSuperfamily.Text = string.Empty;
        AuthorYearTextSuperfamily.Text = string.Empty;
        InfoSuperfamily.Text = string.Empty;
        SynonymSuperfamily.Text = string.Empty;
        EngNameSuperfamily.Text = string.Empty;
        GerNameSuperfamily.Text = string.Empty;
        FraNameSuperfamily.Text = string.Empty;
        PorNameSuperfamily.Text = string.Empty;
        MemoSuperfamily.Text = string.Empty;
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

