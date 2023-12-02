
// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ATIS.WinUi.Helpers;
using ATIS.WinUi.ViewModels.Database;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;


//  Tbl15SubdivisionsPage.xaml.cs Skriptdatum:  28.03.2023  12:32     

namespace ATIS.WinUi.Views.Database;


/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Tbl15SubdivisionsPage : Page
{
    #region [Private Data Members]
    public Tbl15SubdivisionsViewModel ViewModel { get; } = null!;
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]

    public Tbl15SubdivisionsPage()
    {
        //  var permis = new Permission();
        if (Permission.RolePermission())
        {
            ViewModel = App.GetService<Tbl15SubdivisionsViewModel>();
            InitializeComponent();
        }
        else
        {
            _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        }
    }

    private void SubdivisionSearchAutoSuggestBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        SubdivisionSearchAutoSuggestBox.Visibility = Visibility.Visible;
        SubdivisionSearchAutoSuggestBox.Focus(FocusState.Programmatic);
    }

    private void TabView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (SubdivisionTabView.SelectedIndex)
        {
            case 0:
            {
                if (ViewModel.Tbl09DivisionsList is { Count: 0 })
                {
                    DivisionClearMethod();
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

    private void DivisionClearMethod()
    {
        DivisionId.Text = string.Empty;
        DivisionName.Text = string.Empty;
        RegnumCombo.SelectedIndex = -1;
        ValidCheckDivision.IsChecked = false;
        ValidYearTextDivision.Text = string.Empty;
        AuthorTextDivision.Text = string.Empty;
        AuthorYearTextDivision.Text = string.Empty;
        InfoDivision.Text = string.Empty;
        SynonymDivision.Text = string.Empty;
        EngNameDivision.Text = string.Empty;
        GerNameDivision.Text = string.Empty;
        FraNameDivision.Text = string.Empty;
        PorNameDivision.Text = string.Empty;
        MemoDivision.Text = string.Empty;
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
        //   SubdivisionCombo1.Text = string.Empty;
        SubdivisionCombo.SelectedIndex = -1;
        SubphylumCombo.SelectedIndex = -1;
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

