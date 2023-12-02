// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using ATIS.WinUi.ViewModels.Database;
using ATIS.WinUi.Helpers;

//  Tbl06PhylumsPage.xaml.cs Skriptdatum:  21.03.2023  12:32     

namespace ATIS.WinUi.Views.Database;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Tbl06PhylumsPage : Page
{
    #region [Private Data Members]
    public Tbl06PhylumsViewModel ViewModel { get; } = null!;
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]

    public Tbl06PhylumsPage()
    {
        //  var permis = new Permission();
        if (Permission.RolePermission())
        {
            ViewModel = App.GetService<Tbl06PhylumsViewModel>();
            InitializeComponent();
        }
        else
        {
            _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        }
    }
    private void PhylumSearchAutoSuggestBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        PhylumSearchAutoSuggestBox.Visibility = Visibility.Visible;
        PhylumSearchAutoSuggestBox.Focus(FocusState.Programmatic);
    }

    private void TabView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (PhylumTabView.SelectedIndex)
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
                if (ViewModel.Tbl12SubphylumsList is { Count: 0 })
                {
                    SubphylumClearMethod();
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
        Subregnum.Text = string.Empty;
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
    private void SubphylumClearMethod()
    {
        SubphylumId.Text = string.Empty;
        SubphylumName.Text = string.Empty;
        //  PhylumCombo1.Text = string.Empty;
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
