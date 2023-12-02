﻿
// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ATIS.WinUi.Helpers;
using ATIS.WinUi.ViewModels.Database;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;


//  Tbl33OrdosPage.xaml.cs Skriptdatum:  31.03.2023  10:32     

namespace ATIS.WinUi.Views.Database;


/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Tbl33OrdosPage : Page
{
    #region [Private Data Members]
    public Tbl33OrdosViewModel ViewModel { get; } = null!;
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]

    public Tbl33OrdosPage()
    {
        //  var permis = new Permission();
        if (Permission.RolePermission())
        {
            ViewModel = App.GetService<Tbl33OrdosViewModel>();
            InitializeComponent();
        }
        else
        {
            _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        }
    }

    private void OrdoSearchAutoSuggestBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        OrdoSearchAutoSuggestBox.Visibility = Visibility.Visible;
        OrdoSearchAutoSuggestBox.Focus(FocusState.Programmatic);
    }

    private void TabView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (OrdoTabView.SelectedIndex)
        {
            case 0:
            {
                if (ViewModel.Tbl30LegiosList is { Count: 0 })
                {
                    LegioClearMethod();
                }
                break;
            }
            case 1:
            {
                if (ViewModel.Tbl33OrdosList is { Count: 0 })
                {
                    OrdoClearMethod();
                }
                break;
            }
            case 2:
            {
                if (ViewModel.Tbl36SubordosList is { Count: 0 })
                {
                    SubordoClearMethod();
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

    private void LegioClearMethod()
    {
        LegioId.Text = string.Empty;
        LegioName.Text = string.Empty;
        InfraclassCombo.SelectedIndex = -1;
        ValidCheckLegio.IsChecked = false;
        ValidYearTextLegio.Text = string.Empty;
        AuthorTextLegio.Text = string.Empty;
        AuthorYearTextLegio.Text = string.Empty;
        InfoLegio.Text = string.Empty;
        SynonymLegio.Text = string.Empty;
        EngNameLegio.Text = string.Empty;
        GerNameLegio.Text = string.Empty;
        FraNameLegio.Text = string.Empty;
        PorNameLegio.Text = string.Empty;
        MemoLegio.Text = string.Empty;
    }

    private void OrdoClearMethod()
    {
        OrdoId.Text = string.Empty;
        OrdoName.Text = string.Empty;
        //   LegioCombo.Text = string.Empty;
        LegioCombo.SelectedIndex = -1;
        ValidCheckOrdo.IsChecked = false;
        ValidYearTextOrdo.Text = string.Empty;
        AuthorTextOrdo.Text = string.Empty;
        AuthorYearTextOrdo.Text = string.Empty;
        InfoOrdo.Text = string.Empty;
        SynonymOrdo.Text = string.Empty;
        EngNameOrdo.Text = string.Empty;
        GerNameOrdo.Text = string.Empty;
        FraNameOrdo.Text = string.Empty;
        PorNameOrdo.Text = string.Empty;
        MemoOrdo.Text = string.Empty;
    }
    private void SubordoClearMethod()
    {
        SubordoId.Text = string.Empty;
        SubordoName.Text = string.Empty;
        //  OrdoCombo1.Text = string.Empty;
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

