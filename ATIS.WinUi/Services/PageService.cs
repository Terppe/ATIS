using ABI.Windows.Data.Pdf;
using ATIS.WinUi.Contracts.Services;
using ATIS.WinUi.ViewModels;
using ATIS.WinUi.ViewModels.Database;
using ATIS.WinUi.ViewModels.Feature;
using ATIS.WinUi.ViewModels.Main;
using ATIS.WinUi.ViewModels.Search;
using ATIS.WinUi.Views;
using ATIS.WinUi.Views.Database;
using ATIS.WinUi.Views.Feature;
using ATIS.WinUi.Views.Main;
using ATIS.WinUi.Views.Search;
using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml.Controls;

namespace ATIS.WinUi.Services;

public class PageService : IPageService
{
    private readonly Dictionary<string, Type> _pages = new();

    public PageService()
    {
        Configure<MainViewModel, MainPage>();
        Configure<DatabaseViewModel, DatabasePage>();
        Configure<SearchViewModel, SearchQuickPage>();
        Configure<FishesViewModel, FishesPage>();
        Configure<PlantsViewModel, PlantsPage>();
        Configure<DiseasesViewModel, DiseasesPage>();
        Configure<FoodsViewModel, FoodsPage>();
        Configure<MiscellaniesViewModel, MiscellaniesPage>();

        Configure<Tbl03RegnumsViewModel, Tbl03RegnumsPage>();
        Configure<Tbl06PhylumsViewModel, Tbl06PhylumsPage>();
        Configure<Tbl09DivisionsViewModel, Tbl09DivisionsPage>();
        Configure<Tbl12SubphylumsViewModel, Tbl12SubphylumsPage>();
        Configure<Tbl15SubdivisionsViewModel, Tbl15SubdivisionsPage>();
        Configure<Tbl18SuperclassesViewModel, Tbl18SuperclassesPage>();
        Configure<Tbl21ClassesViewModel, Tbl21ClassesPage>();
        Configure<Tbl24SubclassesViewModel, Tbl24SubclassesPage>();
        Configure<Tbl27InfraclassesViewModel, Tbl27InfraclassesPage>();
        Configure<Tbl30LegiosViewModel, Tbl30LegiosPage>();
        Configure<Tbl33OrdosViewModel, Tbl33OrdosPage>();
        Configure<Tbl36SubordosViewModel, Tbl36SubordosPage>();
        Configure<Tbl39InfraordosViewModel, Tbl39InfraordosPage>();
        Configure<Tbl42SuperfamiliesViewModel, Tbl42SuperfamiliesPage>();
        Configure<Tbl45FamiliesViewModel, Tbl45FamiliesPage>();
        Configure<Tbl48SubfamiliesViewModel, Tbl48SubfamiliesPage>();
        Configure<Tbl51InfrafamiliesViewModel, Tbl51InfrafamiliesPage>();
        Configure<Tbl54SupertribussesViewModel, Tbl54SupertribussesPage>();
        Configure<Tbl57TribussesViewModel, Tbl57TribussesPage>();
        Configure<Tbl60SubtribussesViewModel, Tbl60SubtribussesPage>();
        Configure<Tbl63InfratribussesViewModel, Tbl63InfratribussesPage>();
        Configure<Tbl66GenussesViewModel, Tbl66GenussesPage>();
        Configure<Tbl68SpeciesgroupsViewModel, Tbl68SpeciesgroupsPage>();
        Configure<Tbl69FiSpeciessesViewModel, Tbl69FiSpeciessesPage>();
        Configure<Tbl72PlSpeciessesViewModel, Tbl72PlSpeciessesPage>();
        Configure<Tbl78NamesViewModel, Tbl78NamesPage>();
        Configure<Tbl81ImagesViewModel, Tbl81ImagesPage>();
        Configure<Tbl84SynonymsViewModel, Tbl84SynonymsPage>();
        Configure<Tbl87GeographicsViewModel, Tbl87GeographicsPage>();
        Configure<GeosViewModel, GalleryPage>();
        Configure<Test1ViewModel, Test1Page>();
        Configure<Test2ViewModel, Test2Page>();

        Configure<Tbl90RefAuthorsViewModel, Tbl90RefAuthorsPage>();
        Configure<Tbl90RefExpertsViewModel, Tbl90RefExpertsPage>();
        Configure<Tbl90RefSourcesViewModel, Tbl90RefSourcesPage>();
        Configure<Tbl90ReferencesViewModel, Tbl90ReferencesPage>();

        Configure<Tbl93CommentsViewModel, Tbl93CommentsPage>();

        Configure<TblCountriesViewModel, TblCountriesPage>();

        Configure<TblUserProfilesViewModel, TblUserProfilesPage>();

        Configure<SettingsViewModel, SettingsPage>();
        Configure<CheckUpdatePageViewModel, CheckUpdatePage>();
    }

    public Type GetPageType(string key)
    {
        Type? pageType;
        lock (_pages)
        {
            if (!_pages.TryGetValue(key, out pageType))
            {
                throw new ArgumentException($"Page not found: {key}. Did you forget to call PageService.Configure?");
            }
        }

        return pageType;
    }

    private void Configure<VM, V>()
        where VM : ObservableObject
        where V : Page
    {
        lock (_pages)
        {
            var key = typeof(VM).FullName!;
            if (_pages.ContainsKey(key))
            {
                throw new ArgumentException($"The key {key} is already configured in PageService");
            }

            var type = typeof(V);
            if (_pages.Any(p => p.Value == type))
            {
                throw new ArgumentException($"This type is already configured with key {_pages.First(p => p.Value == type).Key}");
            }

            _pages.Add(key, type);
        }
    }
}
