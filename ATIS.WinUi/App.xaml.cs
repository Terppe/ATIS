using System.Configuration;
using System.Globalization;
using ATIS.WinUi.Activation;
using ATIS.WinUi.Contracts.Services;
using ATIS.WinUi.Models;
using ATIS.WinUi.Services;
using ATIS.WinUi.ViewModels;
using ATIS.WinUi.ViewModels.Authentication;
using ATIS.WinUi.ViewModels.Database;
using ATIS.WinUi.ViewModels.Feature;
using ATIS.WinUi.ViewModels.Main;
using ATIS.WinUi.ViewModels.Search;
using ATIS.WinUi.Views;
using ATIS.WinUi.Views.Database;
using ATIS.WinUi.Views.Feature;
using ATIS.WinUi.Views.Main;
using ATIS.WinUi.Views.Search;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;

namespace ATIS.WinUi;

// To learn more about WinUI 3, see https://docs.microsoft.com/windows/apps/winui/winui3/.
public partial class App : Application
{
    // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    public IHost Host
    {
        get;
    }

    public static T GetService<T>()
        where T : class
    {
        if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
        }

        return service;
    }

    public static WindowEx MainWindow { get; } = new MainWindow();
    public static FrameworkElement? MainRoot { get; set; }

    public App()
    {
        InitializeComponent();

        Host = Microsoft.Extensions.Hosting.Host.
        CreateDefaultBuilder().
        UseContentRoot(AppContext.BaseDirectory).
        ConfigureServices((context, services) =>
        {
            // Default Activation Handler
            services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

            // Other Activation Handlers

            // Services
            services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
            services.AddTransient<INavigationViewService, NavigationViewService>();

            services.AddSingleton<IActivationService, ActivationService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<INavigationService, NavigationService>();

            // Core Services
            services.AddSingleton<IDataService, DataService>();
            services.AddSingleton<IFileService, FileService>();

            // Views and ViewModels
            services.AddTransient<DatabasePage>();
            services.AddTransient<DatabaseViewModel>();
            services.AddTransient<SearchQuickPage>();
            services.AddTransient<SearchViewModel>();
            services.AddTransient<FishesPage>();
            services.AddTransient<FishesViewModel>();
            services.AddTransient<PlantsPage>();
            services.AddTransient<PlantsViewModel>();
            services.AddTransient<DiseasesPage>();
            services.AddTransient<DiseasesViewModel>();
            services.AddTransient<FoodsPage>();
            services.AddTransient<FoodsViewModel>();
            services.AddTransient<MiscellaniesPage>();
            services.AddTransient<MiscellaniesViewModel>();

            services.AddTransient<Tbl03RegnumsPage>();
            services.AddTransient<Tbl03RegnumsViewModel>();
            services.AddTransient<Tbl06PhylumsPage>();
            services.AddTransient<Tbl06PhylumsViewModel>();
            services.AddTransient<Tbl09DivisionsPage>();
            services.AddTransient<Tbl09DivisionsViewModel>();
            services.AddTransient<Tbl12SubphylumsPage>();
            services.AddTransient<Tbl12SubphylumsViewModel>();
            services.AddTransient<Tbl15SubdivisionsPage>();
            services.AddTransient<Tbl15SubdivisionsViewModel>();
            services.AddTransient<Tbl18SuperclassesPage>();
            services.AddTransient<Tbl18SuperclassesViewModel>();
            services.AddTransient<Tbl21ClassesPage>();
            services.AddTransient<Tbl21ClassesViewModel>();
            services.AddTransient<Tbl24SubclassesPage>();
            services.AddTransient<Tbl24SubclassesViewModel>();
            services.AddTransient<Tbl27InfraclassesPage>();
            services.AddTransient<Tbl27InfraclassesViewModel>();
            services.AddTransient<Tbl30LegiosPage>();
            services.AddTransient<Tbl30LegiosViewModel>();
            services.AddTransient<Tbl33OrdosPage>();
            services.AddTransient<Tbl33OrdosViewModel>();
            services.AddTransient<Tbl36SubordosPage>();
            services.AddTransient<Tbl36SubordosViewModel>();
            services.AddTransient<Tbl39InfraordosPage>();
            services.AddTransient<Tbl39InfraordosViewModel>();
            services.AddTransient<Tbl42SuperfamiliesPage>();
            services.AddTransient<Tbl42SuperfamiliesViewModel>();
            services.AddTransient<Tbl45FamiliesPage>();
            services.AddTransient<Tbl45FamiliesViewModel>();
            services.AddTransient<Tbl48SubfamiliesPage>();
            services.AddTransient<Tbl48SubfamiliesViewModel>();
            services.AddTransient<Tbl51InfrafamiliesPage>();
            services.AddTransient<Tbl51InfrafamiliesViewModel>();
            services.AddTransient<Tbl54SupertribussesPage>();
            services.AddTransient<Tbl54SupertribussesViewModel>();
            services.AddTransient<Tbl57TribussesPage>();
            services.AddTransient<Tbl57TribussesViewModel>();
            services.AddTransient<Tbl60SubtribussesPage>();
            services.AddTransient<Tbl60SubtribussesViewModel>();
            services.AddTransient<Tbl63InfratribussesPage>();
            services.AddTransient<Tbl63InfratribussesViewModel>();
            services.AddTransient<Tbl66GenussesPage>();
            services.AddTransient<Tbl66GenussesViewModel>();
            services.AddTransient<Tbl68SpeciesgroupsPage>();
            services.AddTransient<Tbl68SpeciesgroupsViewModel>();
            services.AddTransient<Tbl69FiSpeciessesPage>();
            services.AddTransient<Tbl69FiSpeciessesViewModel>();
            services.AddTransient<Tbl72PlSpeciessesPage>();
            services.AddTransient<Tbl72PlSpeciessesViewModel>();
            services.AddTransient<Tbl78NamesPage>();
            services.AddTransient<Tbl78NamesViewModel>();
            services.AddTransient<Tbl81ImagesPage>();
            services.AddTransient<Tbl81ImagesViewModel>();
            services.AddTransient<Tbl84SynonymsPage>();
            services.AddTransient<Tbl84SynonymsViewModel>();
            services.AddTransient<Tbl87GeographicsPage>();
            services.AddTransient<Tbl87GeographicsViewModel>();

            services.AddTransient<Tbl90RefAuthorsPage>();
            services.AddTransient<Tbl90RefAuthorsViewModel>();
            services.AddTransient<Tbl90RefExpertsPage>();
            services.AddTransient<Tbl90RefExpertsViewModel>();
            services.AddTransient<Tbl90RefSourcesPage>();
            services.AddTransient<Tbl90RefSourcesViewModel>();
            services.AddTransient<Tbl90ReferencesPage>();
            services.AddTransient<Tbl90ReferencesViewModel>();

            services.AddTransient<Tbl93CommentsPage>();
            services.AddTransient<Tbl93CommentsViewModel>();

            services.AddTransient<TblCountriesPage>();
            services.AddTransient<TblCountriesViewModel>();

            services.AddTransient<TblUserProfilesPage>();
            services.AddTransient<TblUserProfilesViewModel>();

            services.AddTransient<AuthenticationViewModel>();
            //services.AddTransient<RegisterPage>();
            //services.AddTransient<PasswordChangePage>();
            //services.AddTransient<PasswordForgotPage>();

            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SettingsPage>();
            services.AddTransient<CheckUpdatePageViewModel>();
            services.AddTransient<CheckUpdatePage>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainPage>();
            services.AddTransient<ShellPage>();
            services.AddTransient<ShellViewModel>();
            services.AddTransient<GalleryPage>();
            services.AddTransient<GeosViewModel>();
            services.AddTransient<Test1Page>();
            services.AddTransient<Test1ViewModel>();
            services.AddTransient<Test2Page>();
            services.AddTransient<Test2ViewModel>();

            // Configuration
            services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));
        }).
        Build();

        UnhandledException += App_UnhandledException;
    }

    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        // TODO: Log and handle exceptions as appropriate.
        // https://docs.microsoft.com/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application.unhandledexception.
    }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    { 
        // Log to a sub-directory 'Log' of the current working directory. 
        // Prefix log file with 'MyLog_'.
        // Write XML file, not plain text.
        // This is an optional call and has only to be done once, 
        // pereferably before the first log entry is written. 
        SimpleLog.SetLogFile(logDir: ".\\Log", prefix: "MyLog_", writeText: true);

        SimpleLog.Info("        =============  Started Logging  =============        ");

        base.OnLaunched(args);
        await App.GetService<IActivationService>().ActivateAsync(args);

        MainRoot = MainWindow.Content as FrameworkElement;  //for ContentDialog


        //Create a custom principal with an anonymous identity at startup
        var customPrincipal = new CustomPrincipal();
        AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);

        SetLanguageDictionary();

        var cult = ConfigurationManager.AppSettings.Get("Culture");

        if (Equals(cult, CultureInfo.CurrentUICulture.ToString()) == false)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cult ?? string.Empty);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cult ?? string.Empty);
        }

        //FrameworkElement.LanguageProperty.OverrideMetadata(
        //    typeof(FrameworkElement),
        //    new FrameworkPropertyMetadata(
        //        XmlLanguage.GetLanguage(
        //            CultureInfo.CurrentCulture.IetfLanguageTag)));

        //ThemeManager.Current.ChangeThemeBaseColor(Current, ConfigurationManager.AppSettings.Get("Theme1")!);
        //ThemeManager.Current.ChangeThemeColorScheme(Current, ConfigurationManager.AppSettings.Get("Accent1")!);

    }

    public void SetLanguageDictionary()
    {
        var dict = new ResourceDictionary();
        //switch (ConfigurationManager.AppSettings.Get("Culture"))
        //{
        //    case "de-DE":
        //        dict.Source = new Uri("CultRes/StringsRes.de-DE.xaml", UriKind.Relative);
        //        break;
        //    case "fr-FR":
        //        dict.Source = new Uri("CultRes/StringsRes.fr-FR.xaml", UriKind.Relative);
        //        break;
        //    case "sp-SP":
        //        dict.Source = new Uri("CultRes/StringsRes.sp-SP.xaml", UriKind.Relative);
        //        break;
        //    default:
        //        dict.Source = new Uri("CultRes/StringsRes.xaml", UriKind.Relative);
        //        break;
        //}
        Resources.MergedDictionaries.Add(dict);
    }
    public static void ChangeLanguage(string culture)
    {
        var dict = new ResourceDictionary();
        //switch (culture)
        //{
        //    case "de-DE":
        //        dict.Source = new Uri("CultRes/StringsRes.de-DE.xaml", UriKind.Relative);
        //        break;
        //    case "fr-FR":
        //        dict.Source = new Uri("CultRes/StringsRes.fr-FR.xaml", UriKind.Relative);
        //        break;
        //    case "sp-SP":
        //        dict.Source = new Uri("CultRes/StringsRes.sp-SP.xaml", UriKind.Relative);
        //        break;
        //    default:
        //        dict.Source = new Uri("CultRes/StringsRes.xaml", UriKind.Relative);
        //        break;
        //}
        Current.Resources.MergedDictionaries.Add(dict);

        var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        config.AppSettings.Settings["Culture"].Value = CultureInfo.GetCultureInfoByIetfLanguageTag(culture).ToString();
        config.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection("AppSettings");
    }

}
