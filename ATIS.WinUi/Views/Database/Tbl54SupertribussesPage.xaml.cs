
// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ATIS.WinUi.Helpers;
using ATIS.WinUi.ViewModels.Database;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;


//  Tbl54SupertribussesPage.xaml.cs Skriptdatum:  01.04.2023  10:32     

namespace ATIS.WinUi.Views.Database;


/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Tbl54SupertribussesPage : Page
{
    #region [Private Data Members]
    public Tbl54SupertribussesViewModel ViewModel { get; } = null!;
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]

    public Tbl54SupertribussesPage()
    {
        //  var permis = new Permission();
        if (Permission.RolePermission())
        {
            ViewModel = App.GetService<Tbl54SupertribussesViewModel>();
            InitializeComponent();
        }
        else
        {
            _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        }
    }

    private void SupertribusSearchAutoSuggestBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        SupertribusSearchAutoSuggestBox.Visibility = Visibility.Visible;
        SupertribusSearchAutoSuggestBox.Focus(FocusState.Programmatic);
    }

    private void TabView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (SupertribusTabView.SelectedIndex)
        {
            case 0:
            {
                if (ViewModel.Tbl51InfrafamiliesList is { Count: 0 })
                {
                    InfrafamilyClearMethod();
                }
                break;
            }
            case 1:
            {
                if (ViewModel.Tbl54SupertribussesList is { Count: 0 })
                {
                    SupertribusClearMethod();
                }
                break;
            }
            case 2:
            {
                if (ViewModel.Tbl57TribussesList is { Count: 0 })
                {
                    TribusClearMethod();
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

    private void InfrafamilyClearMethod()
    {
        InfrafamilyId.Text = string.Empty;
        InfrafamilyName.Text = string.Empty;
        SubfamilyCombo.SelectedIndex = -1;
        ValidCheckInfrafamily.IsChecked = false;
        ValidYearTextInfrafamily.Text = string.Empty;
        AuthorTextInfrafamily.Text = string.Empty;
        AuthorYearTextInfrafamily.Text = string.Empty;
        InfoInfrafamily.Text = string.Empty;
        SynonymInfrafamily.Text = string.Empty;
        EngNameInfrafamily.Text = string.Empty;
        GerNameInfrafamily.Text = string.Empty;
        FraNameInfrafamily.Text = string.Empty;
        PorNameInfrafamily.Text = string.Empty;
        MemoInfrafamily.Text = string.Empty;
    }

    private void SupertribusClearMethod()
    {
        SupertribusId.Text = string.Empty;
        SupertribusName.Text = string.Empty;
        //   InfrafamilyCombo.Text = string.Empty;
        InfrafamilyCombo.SelectedIndex = -1;
        ValidCheckSupertribus.IsChecked = false;
        ValidYearTextSupertribus.Text = string.Empty;
        AuthorTextSupertribus.Text = string.Empty;
        AuthorYearTextSupertribus.Text = string.Empty;
        InfoSupertribus.Text = string.Empty;
        SynonymSupertribus.Text = string.Empty;
        EngNameSupertribus.Text = string.Empty;
        GerNameSupertribus.Text = string.Empty;
        FraNameSupertribus.Text = string.Empty;
        PorNameSupertribus.Text = string.Empty;
        MemoSupertribus.Text = string.Empty;
    }
    private void TribusClearMethod()
    {
        TribusId.Text = string.Empty;
        TribusName.Text = string.Empty;
        //  SupertribusCombo1.Text = string.Empty;
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

