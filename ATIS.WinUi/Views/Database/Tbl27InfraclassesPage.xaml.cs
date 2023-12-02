﻿
// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System.ComponentModel.Design;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.ViewModels.Database;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;


//  Tbl27InfraclassesPage.xaml.cs Skriptdatum:  30.03.2023  18:32     

namespace ATIS.WinUi.Views.Database;


/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Tbl27InfraclassesPage : Page
{
    #region [Private Data Members]
    public Tbl27InfraclassesViewModel ViewModel { get; } = null!;
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]

    public Tbl27InfraclassesPage()
    {
        //  var permis = new Permission();
        if (Permission.RolePermission())
        {
            ViewModel = App.GetService<Tbl27InfraclassesViewModel>();
            InitializeComponent();
        }
        else
        {
            _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        }
    }

    private void InfraclassSearchAutoSuggestBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        InfraclassSearchAutoSuggestBox.Visibility = Visibility.Visible;
        InfraclassSearchAutoSuggestBox.Focus(FocusState.Programmatic);
    }

    private void TabView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (InfraclassTabView.SelectedIndex)
        {
            case 0:
            {
                if (ViewModel.Tbl24SubclassesList is { Count: 0 })
                {
                    SubclassClearMethod();
                }
                break;
            }
            case 1:
            {
                if (ViewModel.Tbl27InfraclassesList is { Count: 0 })
                {
                    InfraclassClearMethod();
                }
                break;
            }
            case 2:
            {
                if (ViewModel.Tbl30LegiosList is { Count: 0 })
                {
                    LegioClearMethod();
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

    private void SubclassClearMethod()
    {
        SubclassId.Text = string.Empty;
        SubclassName.Text = string.Empty;
        ClassCombo.SelectedIndex = -1;
        ValidCheckSubclass.IsChecked = false;
        ValidYearTextSubclass.Text = string.Empty;
        AuthorTextSubclass.Text = string.Empty;
        AuthorYearTextSubclass.Text = string.Empty;
        InfoSubclass.Text = string.Empty;
        SynonymSubclass.Text = string.Empty;
        EngNameSubclass.Text = string.Empty;
        GerNameSubclass.Text = string.Empty;
        FraNameSubclass.Text = string.Empty;
        PorNameSubclass.Text = string.Empty;
        MemoSubclass.Text = string.Empty;
    }

    private void InfraclassClearMethod()
    {
        InfraclassId.Text = string.Empty;
        InfraclassName.Text = string.Empty;
        //   SubclassCombo.Text = string.Empty;
        SubclassCombo.SelectedIndex = -1;
        ValidCheckInfraclass.IsChecked = false;
        ValidYearTextInfraclass.Text = string.Empty;
        AuthorTextInfraclass.Text = string.Empty;
        AuthorYearTextInfraclass.Text = string.Empty;
        InfoInfraclass.Text = string.Empty;
        SynonymInfraclass.Text = string.Empty;
        EngNameInfraclass.Text = string.Empty;
        GerNameInfraclass.Text = string.Empty;
        FraNameInfraclass.Text = string.Empty;
        PorNameInfraclass.Text = string.Empty;
        MemoInfraclass.Text = string.Empty;
    }
    private void LegioClearMethod()
    {
        LegioId.Text = string.Empty;
        LegioName.Text = string.Empty;
        //  InfraclassCombo1.Text = string.Empty;
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

