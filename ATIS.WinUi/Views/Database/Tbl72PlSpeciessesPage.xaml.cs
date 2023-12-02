
using ATIS.WinUi.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ATIS.WinUi.ViewModels.Database;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;


//  Tbl72PlSpeciessesPage.xaml.cs Skriptdatum:  12.04.2023  12:32     

namespace ATIS.WinUi.Views.Database;


/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Tbl72PlSpeciessesPage : Page
{
    #region [Private Data Members]
    public Tbl72PlSpeciessesViewModel ViewModel { get; } = null!;
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]

    public Tbl72PlSpeciessesPage()
    {
        //  var permis = new Permission();
        if (Permission.RolePermission())
        {
            ViewModel = App.GetService<Tbl72PlSpeciessesViewModel>();
            InitializeComponent();
        }
        else
        {
            _allDialogs.WarningNoPermissionMessageDialogAsync("").ConfigureAwait(false);   //no permission
        }
    }

    private void PlSpeciesSearchAutoSuggestBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        PlSpeciesSearchAutoSuggestBox.Visibility = Visibility.Visible;
        PlSpeciesSearchAutoSuggestBox.Focus(FocusState.Programmatic);
    }

    private void TabView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (PlSpeciesTabView.SelectedIndex)
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
                    if (ViewModel.Tbl72PlSpeciessesList is { Count: 0 })
                    {
                        PlSpeciesClearMethod();
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
    private void PlSpeciesClearMethod()
    {
        PlSpeciesId.Text = string.Empty;
        GenusCombo.SelectedIndex = -1;
        PlSpeciesName.Text = string.Empty;
        PlSpeciesSubspecies.Text = string.Empty;
        PlSpeciesDivers.Text = string.Empty;
        SpeciesgroupCombo.SelectedIndex = -1;
        ValidCheckPlSpecies.IsChecked = false;
        ValidYearTextPlSpecies.Text = string.Empty;
        AuthorTextPlSpecies.Text = string.Empty;
        AuthorYearTextPlSpecies.Text = string.Empty;
        PlantLengthTextPlSpecies.Text = string.Empty;
        BasinHeightTextPlSpecies.Text = string.Empty;
        ImporterTextPlSpecies.Text = string.Empty;
        ImportingYearTextPlSpecies.Text = string.Empty;
        TradeNamePlSpecies.Text = string.Empty;
        MemoSpeciesPlSpecies.Text = string.Empty;
        Difficult1CheckPlSpecies.IsChecked = false;
        Difficult2CheckPlSpecies.IsChecked = false;
        Difficult3CheckPlSpecies.IsChecked = false;
        Difficult4CheckPlSpecies.IsChecked = false;
        Ph1TextPlSpecies.Text = string.Empty;
        Ph2TextPlSpecies.Text = string.Empty;
        Temp1TextPlSpecies.Text = string.Empty;
        Temp2TextPlSpecies.Text = string.Empty;
        Hardness1TextPlSpecies.Text = string.Empty;
        Hardness2TextPlSpecies.Text = string.Empty;
        CarboHardness1TextPlSpecies.Text = string.Empty;
        CarboHardness2TextPlSpecies.Text = string.Empty;
        MemoTechPlSpecies.Text = string.Empty;
        MemoCulturePlSpecies.Text = string.Empty;
        MemoReproductionPlSpecies.Text = string.Empty;
        MemoBuiltPlSpecies.Text = string.Empty;
        MemoColorPlSpecies.Text = string.Empty;
        MemoGlobalPlSpecies.Text = string.Empty;
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

