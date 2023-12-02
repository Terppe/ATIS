using ATIS.WinUi.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ATIS.WinUi.ViewModels.Database;


//  Tbl69FiSpeciessesPage.xaml.cs Skriptdatum:  04.04.2023  10:32     

namespace ATIS.WinUi.Views.Database;


/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Tbl69FiSpeciessesPage : Page
{
    #region [Private Data Members]
    public Tbl69FiSpeciessesViewModel ViewModel { get; } = null!;
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]

    public Tbl69FiSpeciessesPage()
    {
      //  var permis = new Permission();
        if (Permission.RolePermission())
        {
            ViewModel = App.GetService<Tbl69FiSpeciessesViewModel>();
            InitializeComponent();
        }
        else
        {
            _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        }
    }

    private void FiSpeciesSearchAutoSuggestBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        FiSpeciesSearchAutoSuggestBox.Visibility = Visibility.Visible;
        FiSpeciesSearchAutoSuggestBox.Focus(FocusState.Programmatic);
    }

    private void TabView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (FiSpeciesTabView.SelectedIndex)
        {
            case 0:
                {
                    if (ViewModel.Tbl66GenussesList is { Count: 0 })
                    {
                        GenusClearMethod();
                    }
                    break;
                }
            case 1:
                {
                    if (ViewModel.Tbl68SpeciesgroupsList is { Count: 0 })
                    {
                        SpeciesgroupClearMethod();
                    }
                    break;
                }
            case 2:
                {
                    if (ViewModel.Tbl69FiSpeciessesList is { Count: 0 })
                    {
                        FiSpeciesClearMethod();
                    }
                    break;
                }
            case 3:
                {
                    if (ViewModel.Tbl78NamesList is { Count: 0 })
                    {
                        NameClearMethod();
                    }
                    break;
                }
            case 4:
                {
                    if (ViewModel.Tbl81ImagesList is { Count: 0 })
                    {
                        ImageClearMethod();
                    }
                    break;
                }
            case 5:
                {
                    if (ViewModel.Tbl84SynonymsList is { Count: 0 })
                    {
                        SynonymClearMethod();
                    }
                    break;
                }
            case 6:
                {
                    if (ViewModel.Tbl87GeographicsList is { Count: 0 })
                    {
                        GeographicClearMethod();
                    }
                    break;
                }
            case 7:
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
            case 8:
                {
                    if (ViewModel.Tbl93CommentsList is { Count: 0 })
                    {
                        CommentClearMethod();
                    }
                    break;
                }
        }
    }

    private void GenusClearMethod()
    {
        GenusId.Text = string.Empty;
        GenusName.Text = string.Empty;
        InfratribusCombo.SelectedIndex = -1;
        ValidCheckGenus.IsChecked = false;
        ValidYearTextGenus.Text = string.Empty;
        AuthorTextGenus.Text = string.Empty;
        AuthorYearTextGenus.Text = string.Empty;
        InfoGenus.Text = string.Empty;
        SynonymGenus.Text = string.Empty;
        EngNameGenus.Text = string.Empty;
        GerNameGenus.Text = string.Empty;
        FraNameGenus.Text = string.Empty;
        PorNameGenus.Text = string.Empty;
        MemoGenus.Text = string.Empty;
    }
    private void SpeciesgroupClearMethod()
    {
        SpeciesgroupId.Text = string.Empty;
        SpeciesgroupName.Text = string.Empty;
        SubspeciesgroupName.Text = string.Empty;
        ValidCheckSpeciesgroup.IsChecked = false;
        ValidYearTextSpeciesgroup.Text = string.Empty;
        AuthorTextSpeciesgroup.Text = string.Empty;
        AuthorYearTextSpeciesgroup.Text = string.Empty;
        InfoSpeciesgroup.Text = string.Empty;
        SynonymSpeciesgroup.Text = string.Empty;
        EngNameSpeciesgroup.Text = string.Empty;
        GerNameSpeciesgroup.Text = string.Empty;
        FraNameSpeciesgroup.Text = string.Empty;
        PorNameSpeciesgroup.Text = string.Empty;
        MemoSpeciesgroup.Text = string.Empty;

    }
    private void FiSpeciesClearMethod()
    {
        FiSpeciesId.Text = string.Empty;
        GenusCombo.SelectedIndex = -1;
        FiSpeciesName.Text = string.Empty;
        FiSpeciesSubspecies.Text = string.Empty;
        FiSpeciesDivers.Text = string.Empty;
        SpeciesgroupCombo.SelectedIndex = -1;
        ValidCheckFiSpecies.IsChecked = false;
        ValidYearTextFiSpecies.Text = string.Empty;
        AuthorTextFiSpecies.Text = string.Empty;
        AuthorYearTextFiSpecies.Text = string.Empty;
        FishLengthTextFiSpecies.Text = string.Empty;
        BasinLengthTextFiSpecies.Text = string.Empty;
        ImporterTextFiSpecies.Text = string.Empty;
        ImportingYearTextFiSpecies.Text = string.Empty;
        TradeNameFiSpecies.Text = string.Empty;
        MemoSpeciesFiSpecies.Text = string.Empty;
        KarnivoreCheckFiSpecies.IsChecked = false;
        HerbivoreCheckFiSpecies.IsChecked = false;
        LimnivoreCheckFiSpecies.IsChecked = false;
        OmnivoreCheckFiSpecies.IsChecked = false;
        MemoFoodsFiSpecies.Text = string.Empty;
        Difficult1CheckFiSpecies.IsChecked = false;
        Difficult2CheckFiSpecies.IsChecked = false;
        Difficult3CheckFiSpecies.IsChecked = false;
        Difficult4CheckFiSpecies.IsChecked = false;
        Ph1TextFiSpecies.Text = string.Empty;
        Ph2TextFiSpecies.Text = string.Empty;
        Temp1TextFiSpecies.Text = string.Empty;
        Temp2TextFiSpecies.Text = string.Empty;
        Hardness1TextFiSpecies.Text = string.Empty;
        Hardness2TextFiSpecies.Text = string.Empty;
        CarboHardness1TextFiSpecies.Text = string.Empty;
        CarboHardness2TextFiSpecies.Text = string.Empty;
        MemoTechFiSpecies.Text = string.Empty;
        MemoHusbandryFiSpecies.Text = string.Empty;
        MemoBreedingFiSpecies.Text = string.Empty;
        MemoBuiltFiSpecies.Text = string.Empty;
        MemoColorFiSpecies.Text = string.Empty;
        MemoSozialFiSpecies.Text = string.Empty;
        MemoDomorphismFiSpecies.Text = string.Empty;
        MemoSpecialFiSpecies.Text = string.Empty;
    }
    private void NameClearMethod()
    {
        NameId.Text = string.Empty;
        NameName.Text = string.Empty;
        ValidCheckName.IsChecked = false;
        ValidYearTextName.Text = string.Empty;
        InfoName.Text = string.Empty;
        SynonymName.Text = string.Empty;
        MemoName.Text = string.Empty;
    }
    private void ImageClearMethod()
    {
        ImageId.Text = string.Empty;
        ImageMimeTypeCombo.SelectedIndex = -1;
        ValidCheckImage.IsChecked = false;
        ValidYearTextImage.Text = string.Empty;
        AuthorTextShotDate.Text = string.Empty;
        InfoImage.Text = string.Empty;
        SelectedPathImage.Text = string.Empty;
        Image1.Source = null;
    }
    private void SynonymClearMethod()
    {
        SynonymId.Text = string.Empty;
        SynonymName.Text = string.Empty;
        ValidCheckSynonym.IsChecked = false;
        ValidYearTextSynonym.Text = string.Empty;
        AuthorTextSynonym.Text = string.Empty;
        AuthorYearTextSynonym.Text = string.Empty;
        InfoSynonym.Text = string.Empty;
        MemoSynonym.Text = string.Empty;
    }
    private void GeographicClearMethod()
    {
        GeographicId.Text = string.Empty;
        ComboContinent.SelectedIndex = -1;
        ComboCountry.SelectedIndex = -1;
        ValidCheckGeographic.IsChecked = false;
        ValidYearTextGeographic.Text = string.Empty;
        AddressGeographic.Text = string.Empty;
        InfoGeographic.Text = string.Empty;
        ImageGeographic.Source = null;
        AuthorTextGeographic.Text = string.Empty;
        AuthorYearTextGeographic.Text = string.Empty;
        HttpGeographic.Text = string.Empty;
        MemoGeographic.Text = string.Empty;
        ZoomLevelGeographic.Text = string.Empty;
        Latitude1Geographic.Text = string.Empty;
        Latitude2Geographic.Text = string.Empty;
        Latitude3Geographic.Text = string.Empty;
        Latitude4Geographic.Text = string.Empty;
        Longitude1Geographic.Text = string.Empty;
        Longitude2Geographic.Text = string.Empty;
        Longitude3Geographic.Text = string.Empty;
        Longitude4Geographic.Text = string.Empty;
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

