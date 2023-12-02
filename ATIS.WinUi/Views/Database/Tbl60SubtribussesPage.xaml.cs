
// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ATIS.WinUi.Helpers;
using ATIS.WinUi.ViewModels.Database;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;


//  Tbl60SubtribussesPage.xaml.cs Skriptdatum:  01.04.2023  10:32     

namespace ATIS.WinUi.Views.Database;


/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Tbl60SubtribussesPage : Page
{
    #region [Private Data Members]
    public Tbl60SubtribussesViewModel ViewModel { get; } = null!;
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]

    public Tbl60SubtribussesPage()
    {
        //  var permis = new Permission();
        if (Permission.RolePermission())
        {
            ViewModel = App.GetService<Tbl60SubtribussesViewModel>();
            InitializeComponent();
        }
        else
        {
            _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        }
    }

    private void SubtribusSearchAutoSuggestBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        SubtribusSearchAutoSuggestBox.Visibility = Visibility.Visible;
        SubtribusSearchAutoSuggestBox.Focus(FocusState.Programmatic);
    }

    private void TabView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (SubtribusTabView.SelectedIndex)
        {
            case 0:
            {
                if (ViewModel.Tbl57TribussesList is { Count: 0 })
                {
                    TribusClearMethod();
                }
                break;
            }
            case 1:
            {
                if (ViewModel.Tbl60SubtribussesList is { Count: 0 })
                {
                    SubtribusClearMethod();
                }
                break;
            }
            case 2:
            {
                if (ViewModel.Tbl63InfratribussesList is { Count: 0 })
                {
                    InfratribusClearMethod();
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

    private void TribusClearMethod()
    {
        TribusId.Text = string.Empty;
        TribusName.Text = string.Empty;
        SupertribusCombo.SelectedIndex = -1;
        ValidCheckTribus.IsChecked = false;
        ValidYearTextTribus.Text = string.Empty;
        AuthorTextTribus.Text = string.Empty;
        AuthorYearTextTribus.Text = string.Empty;
        InfoTribus.Text = string.Empty;
        SynonymTribus.Text = string.Empty;
        EngNameTribus.Text = string.Empty;
        GerNameTribus.Text = string.Empty;
        FraNameTribus.Text = string.Empty;
        PorNameTribus.Text = string.Empty;
        MemoTribus.Text = string.Empty;
    }

    private void SubtribusClearMethod()
    {
        SubtribusId.Text = string.Empty;
        SubtribusName.Text = string.Empty;
        //   TribusCombo.Text = string.Empty;
        TribusCombo.SelectedIndex = -1;
        ValidCheckSubtribus.IsChecked = false;
        ValidYearTextSubtribus.Text = string.Empty;
        AuthorTextSubtribus.Text = string.Empty;
        AuthorYearTextSubtribus.Text = string.Empty;
        InfoSubtribus.Text = string.Empty;
        SynonymSubtribus.Text = string.Empty;
        EngNameSubtribus.Text = string.Empty;
        GerNameSubtribus.Text = string.Empty;
        FraNameSubtribus.Text = string.Empty;
        PorNameSubtribus.Text = string.Empty;
        MemoSubtribus.Text = string.Empty;
    }
    private void InfratribusClearMethod()
    {
        InfratribusId.Text = string.Empty;
        InfratribusName.Text = string.Empty;
        //  SubtribusCombo1.Text = string.Empty;
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

