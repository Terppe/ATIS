﻿// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ATIS.WinUi.Helpers;
using ATIS.WinUi.ViewModels.Database;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;


//  Tbl18SuperclassesPage.xaml.cs Skriptdatum:  29.03.2023  12:32     

namespace ATIS.WinUi.Views.Database;


/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Tbl18SuperclassesPage : Page
{
    #region [Private Data Members]
    public Tbl18SuperclassesViewModel ViewModel { get; } = null!;
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]

    public Tbl18SuperclassesPage()
    {
        //  var permis = new Permission();
        if (Permission.RolePermission())
        {
            ViewModel = App.GetService<Tbl18SuperclassesViewModel>();
            InitializeComponent();
        }
        else
        {
            _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        }
    }

    private void SuperclassSearchAutoSuggestBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        SuperclassSearchAutoSuggestBox.Visibility = Visibility.Visible;
        SuperclassSearchAutoSuggestBox.Focus(FocusState.Programmatic);
    }

    private void TabView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (SuperclassTabView.SelectedIndex)
        {
            case 0:
            {
                if (ViewModel.Tbl12SubphylumsList is { Count: 0 })
                {
                    SubphylumClearMethod();
                }
                break;
            }
            case 1:
            {
                if (ViewModel.Tbl15SubdivisionsList is { Count: 0 })
                {
                    SubdivisionClearMethod();
                }
                break;
            }
            case 2:
            {
                if (ViewModel.Tbl18SuperclassesList is { Count: 0 })
                {
                    SuperclassClearMethod();
                }
                break;
            }
            case 3:
            {
                if (ViewModel.Tbl21ClassesList is { Count: 0 })
                {
                    ClassClearMethod();
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

    private void SubphylumClearMethod()
    {
        SubphylumId.Text = string.Empty;
        SubphylumName.Text = string.Empty;
        PhylumCombo.SelectedIndex = -1;
        ValidCheckSubphylum.IsChecked = false;
        ValidYearTextSubphylum.Text = string.Empty;
        AuthorTextSubphylum.Text = string.Empty;
        AuthorYearTextSubphylum.Text = string.Empty;
        InfoSubphylum.Text = string.Empty;
        SynonymSubphylum.Text = string.Empty;
        EngNameSubphylum.Text = string.Empty;
        GerNameSubphylum.Text = string.Empty;
        FraNameSubphylum.Text = string.Empty;
        PorNameSubphylum.Text = string.Empty;
        MemoSubphylum.Text = string.Empty;
    }

    private void SubdivisionClearMethod()
    {
        SubdivisionId.Text = string.Empty;
        SubdivisionName.Text = string.Empty;
        //   DivisionCombo.Text = string.Empty;
        DivisionCombo.SelectedIndex = -1;
        ValidCheckSubdivision.IsChecked = false;
        ValidYearTextSubdivision.Text = string.Empty;
        AuthorTextSubdivision.Text = string.Empty;
        AuthorYearTextSubdivision.Text = string.Empty;
        InfoSubdivision.Text = string.Empty;
        SynonymSubdivision.Text = string.Empty;
        EngNameSubdivision.Text = string.Empty;
        GerNameSubdivision.Text = string.Empty;
        FraNameSubdivision.Text = string.Empty;
        PorNameSubdivision.Text = string.Empty;
        MemoSubdivision.Text = string.Empty;
    }

    private void SuperclassClearMethod()
    {
        SuperclassId.Text = string.Empty;
        SuperclassName.Text = string.Empty;
        //   SubphylumCombo.Text = string.Empty;
        SubphylumCombo.SelectedIndex = -1;
        SubdivisionCombo.SelectedIndex = -1;
        ValidCheckSuperclass.IsChecked = false;
        ValidYearTextSuperclass.Text = string.Empty;
        AuthorTextSuperclass.Text = string.Empty;
        AuthorYearTextSuperclass.Text = string.Empty;
        InfoSuperclass.Text = string.Empty;
        SynonymSuperclass.Text = string.Empty;
        EngNameSuperclass.Text = string.Empty;
        GerNameSuperclass.Text = string.Empty;
        FraNameSuperclass.Text = string.Empty;
        PorNameSuperclass.Text = string.Empty;
        MemoSuperclass.Text = string.Empty;
    }
    private void ClassClearMethod()
    {
        ClassId.Text = string.Empty;
        ClassName.Text = string.Empty;
        //  SuperclassCombo.Text = string.Empty;
        SuperclassCombo.SelectedIndex = -1;
        ValidCheckClass.IsChecked = false;
        ValidYearTextClass.Text = string.Empty;
        AuthorTextClass.Text = string.Empty;
        AuthorYearTextClass.Text = string.Empty;
        InfoClass.Text = string.Empty;
        SynonymClass.Text = string.Empty;
        EngNameClass.Text = string.Empty;
        GerNameClass.Text = string.Empty;
        FraNameClass.Text = string.Empty;
        PorNameClass.Text = string.Empty;
        MemoClass.Text = string.Empty;
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
