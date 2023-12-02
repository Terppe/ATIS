
// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ATIS.WinUi.Helpers;
using ATIS.WinUi.ViewModels.Database;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;


//  Tbl45FamiliesPage.xaml.cs Skriptdatum:  31.03.2023  10:32     

namespace ATIS.WinUi.Views.Database;


/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Tbl45FamiliesPage : Page
{
    #region [Private Data Members]
    public Tbl45FamiliesViewModel ViewModel { get; } = null!;
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]

    public Tbl45FamiliesPage()
    {
        //  var permis = new Permission();
        if (Permission.RolePermission())
        {
            ViewModel = App.GetService<Tbl45FamiliesViewModel>();
            InitializeComponent();
        }
        else
        {
            _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        }
    }

    private void FamilySearchAutoSuggestBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        FamilySearchAutoSuggestBox.Visibility = Visibility.Visible;
        FamilySearchAutoSuggestBox.Focus(FocusState.Programmatic);
    }

    private void TabView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (FamilyTabView.SelectedIndex)
        {
            case 0:
            {
                if (ViewModel.Tbl42SuperfamiliesList is { Count: 0 })
                {
                    SuperfamilyClearMethod();
                }
                break;
            }
            case 1:
            {
                if (ViewModel.Tbl45FamiliesList is { Count: 0 })
                {
                    FamilyClearMethod();
                }
                break;
            }
            case 2:
            {
                if (ViewModel.Tbl48SubfamiliesList is { Count: 0 })
                {
                    SubfamilyClearMethod();
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

    private void SuperfamilyClearMethod()
    {
        SuperfamilyId.Text = string.Empty;
        SuperfamilyName.Text = string.Empty;
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

    private void FamilyClearMethod()
    {
        FamilyId.Text = string.Empty;
        FamilyName.Text = string.Empty;
        //   SuperfamilyCombo.Text = string.Empty;
        SuperfamilyCombo.SelectedIndex = -1;
        ValidCheckFamily.IsChecked = false;
        ValidYearTextFamily.Text = string.Empty;
        AuthorTextFamily.Text = string.Empty;
        AuthorYearTextFamily.Text = string.Empty;
        InfoFamily.Text = string.Empty;
        SynonymFamily.Text = string.Empty;
        EngNameFamily.Text = string.Empty;
        GerNameFamily.Text = string.Empty;
        FraNameFamily.Text = string.Empty;
        PorNameFamily.Text = string.Empty;
        MemoFamily.Text = string.Empty;
    }
    private void SubfamilyClearMethod()
    {
        SubfamilyId.Text = string.Empty;
        SubfamilyName.Text = string.Empty;
        //  FamilyCombo1.Text = string.Empty;
        FamilyCombo.SelectedIndex = -1;
        ValidCheckSubfamily.IsChecked = false;
        ValidYearTextSubfamily.Text = string.Empty;
        AuthorTextSubfamily.Text = string.Empty;
        AuthorYearTextSubfamily.Text = string.Empty;
        InfoSubfamily.Text = string.Empty;
        SynonymSubfamily.Text = string.Empty;
        EngNameSubfamily.Text = string.Empty;
        GerNameSubfamily.Text = string.Empty;
        FraNameSubfamily.Text = string.Empty;
        PorNameSubfamily.Text = string.Empty;
        MemoSubfamily.Text = string.Empty;
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

