using ATIS.Ui.Views.Log;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using ATIS.Ui.Helper;
using ATIS.Ui.Properties;
using ControlzEx.Theming;
using MahApps.Metro.Theming;

namespace ATIS.Ui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Log to a sub-directory 'Log' of the current working directory. 
            // Prefix log file with 'MyLog_'.
            // Write XML file, not plain text.
            // This is an optional call and has only to be done once, 
            // pereferably before the first log entry is written. 
            SimpleLog.SetLogFile(logDir: ".\\Log", prefix: "MyLog_", writeText: true);

            SimpleLog.Info("        =============  Started Logging  =============        ");

            SetLanguageDictionary();

            //Create a custom principal with an anonymous identity at startup
            var customPrincipal = new CustomPrincipal();
            AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);

            if (Equals(Settings.Default.Culture, CultureInfo.CurrentUICulture) == false)
            {
                Thread.CurrentThread.CurrentCulture = Settings.Default.Culture;
                Thread.CurrentThread.CurrentUICulture = Settings.Default.Culture;
            }

            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(
                        CultureInfo.CurrentCulture.IetfLanguageTag)));

            var currentTheme = Settings.Default.Theme1;
            var currentAccent = Settings.Default.Accent1;
            //Theme theme = ThemeManager.ChangeTheme(Current, currentAccent, currentTheme );
            //Theme theme1 = ThemeManager.ChangeTheme(this, currentAccent, Settings.Default.Theme1);
            //ThemeManager.ChangeTheme(FindResource("CurrentAccent") as FrameworkElement, currentAccent, currentTheme);
        }

        public void SetLanguageDictionary()
        {
            var dict = new ResourceDictionary();
            switch (Settings.Default["Culture"].ToString())
            {
                case "de-DE":
                    dict.Source = new Uri("CultRes/StringsRes.de-DE.xaml", UriKind.Relative);
                    break;
                case "fr-FR":
                    dict.Source = new Uri("CultRes/StringsRes.fr-FR.xaml", UriKind.Relative);
                    break;
                case "sp-SP":
                    dict.Source = new Uri("CultRes/StringsRes.sp-SP.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("CultRes/StringsRes.xaml", UriKind.Relative);
                    break;
            }
            Resources.MergedDictionaries.Add(dict);
        }
        public static void ChangeLanguage(string culture)
        {
            var dict = new ResourceDictionary();
            switch (culture)
            {
                case "de-DE":
                    dict.Source = new Uri("CultRes/StringsRes.de-DE.xaml", UriKind.Relative);
                    break;
                case "fr-FR":
                    dict.Source = new Uri("CultRes/StringsRes.fr-FR.xaml", UriKind.Relative);
                    break;
                case "sp-SP":
                    dict.Source = new Uri("CultRes/StringsRes.sp-SP.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("CultRes/StringsRes.xaml", UriKind.Relative);
                    break;
            }
            Current.Resources.MergedDictionaries.Add(dict);
            Settings.Default.Culture = CultureInfo.GetCultureInfoByIetfLanguageTag(culture);
            Settings.Default.Save();
        }

    }
}
