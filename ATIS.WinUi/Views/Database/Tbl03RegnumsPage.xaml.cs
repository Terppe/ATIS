// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ATIS.WinUi.Helpers;
using ATIS.WinUi.ViewModels.Database;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;

//  Tbl03RegnumsPage.xaml.cs Skriptdatum:  26.03.2023  12:32       

namespace ATIS.WinUi.Views.Database;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Tbl03RegnumsPage : Page
{
    #region [Private Data Members]
    public Tbl03RegnumsViewModel ViewModel { get; } = null!;
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]

    public Tbl03RegnumsPage()
    {
        //  var permis = new Permission();
        if (Permission.RolePermission())
        {
            ViewModel = App.GetService<Tbl03RegnumsViewModel>();
            InitializeComponent();
        }
        else
        {
            _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        }
    }

    private void RegnumSearchAutoSuggestBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        RegnumSearchAutoSuggestBox.Visibility = Visibility.Visible;
        RegnumSearchAutoSuggestBox.Focus(FocusState.Programmatic);
    }

     private void TabView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (RegnumTabView.SelectedIndex)
        {
            case 0:
            {
                if (ViewModel.Tbl03RegnumsList is { Count: 0 })
                {
                    RegnumClearMethod();
                }
                break;
            }
            case 1:
            {
                if (ViewModel.Tbl06PhylumsList is { Count: 0 })
                {
                    PhylumClearMethod();
                }
                break;
            }
            case 2:
            {
                if (ViewModel.Tbl09DivisionsList is { Count: 0 })
                {
                    DivisionClearMethod();
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

    private void RegnumClearMethod()
    {
        RegnumId.Text = string.Empty;
        RegnumName.Text = string.Empty;
        ValidCheckRegnum.IsChecked = false;
        ValidYearTextRegnum.Text = string.Empty;
        AuthorTextRegnum.Text = string.Empty;
        AuthorYearTextRegnum.Text = string.Empty;
        InfoRegnum.Text = string.Empty;
        SynonymRegnum.Text = string.Empty;
        EngNameRegnum.Text = string.Empty;
        GerNameRegnum.Text = string.Empty;
        FraNameRegnum.Text = string.Empty;
        PorNameRegnum.Text = string.Empty;
        MemoRegnum.Text = string.Empty;
    }
    private void PhylumClearMethod()
    {
        PhylumId.Text = string.Empty;
        PhylumName.Text = string.Empty;
      //  RegnumCombo.Text = string.Empty;
        RegnumCombo.SelectedIndex = -1;
        ValidCheckPhylum.IsChecked = false;
        ValidYearTextPhylum.Text = string.Empty;
        AuthorTextPhylum.Text = string.Empty;
        AuthorYearTextPhylum.Text = string.Empty;
        InfoPhylum.Text = string.Empty;
        SynonymPhylum.Text = string.Empty;
        EngNamePhylum.Text = string.Empty;
        GerNamePhylum.Text = string.Empty;
        FraNamePhylum.Text = string.Empty;
        PorNamePhylum.Text = string.Empty;
        MemoPhylum.Text = string.Empty;
    }
    private void DivisionClearMethod()
    {
        DivisionId.Text = string.Empty;
        DivisionName.Text = string.Empty;
        //  RegnumCombo1.Text = string.Empty;
        RegnumCombo1.SelectedIndex = -1;
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