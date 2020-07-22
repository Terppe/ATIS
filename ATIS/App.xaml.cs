using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

namespace ATIS.Ui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //log4net.Config.XmlConfigurator.Configure();
            //Log.Info("        =============  Started Logging  =============        ");

            //  var cul = Settings.Default.Culture;
            //    ChangeLanguage(cul.IetfLanguageTag);
            //   ChangeLanguage(Settings.Default["Culture"].ToString());
            SetLanguageDictionary();

            //Create a custom principal with an anonymous identity at startup
            //var customPrincipal = new CustomPrincipal();
            //AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);


            //if (Equals(SqlMapper.Settings.Default.Culture, CultureInfo.CurrentUICulture) == false)
            //{
            //    Thread.CurrentThread.CurrentCulture = SqlMapper.Settings.Default.Culture;
            //    Thread.CurrentThread.CurrentUICulture = SqlMapper.Settings.Default.Culture;
            //}

            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(
                    CultureInfo.CurrentCulture.IetfLanguageTag)));

            base.OnStartup(e);
        }

        public void SetLanguageDictionary()
        {
            var dict = new ResourceDictionary();
            switch (Thread.CurrentThread.CurrentCulture.ToString())
            //    switch (SqlMapper.Settings.Default["Culture"].ToString())
            {
                case "de-DE":
                    dict.Source = new Uri("/Atis.Culture;component/Cultures/StringsRes.de-DE.xaml", UriKind.Relative);
                    break;
                case "fr-FR":
                    dict.Source = new Uri("/Atis.Culture;component/Cultures/StringsRes.fr-FR.xaml", UriKind.Relative);
                    break;
                case "pt-PT":
                    dict.Source = new Uri("/Atis.Culture;component/Cultures/StringsRes.pt-PT.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("/Atis.Culture;component/Cultures/StringsRes.xaml", UriKind.Relative);
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
                    dict.Source = new Uri("/Atis.Culture;component/Cultures/StringsRes.de-DE.xaml", UriKind.Relative);
                    break;
                case "fr-FR":
                    dict.Source = new Uri("/Atis.Culture;component/Cultures/StringsRes.fr-FR.xaml", UriKind.Relative);
                    break;
                case "pt-PT":
                    dict.Source = new Uri("/Atis.Culture;component/Cultures/StringsRes.pt-PT.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("/Atis.Culture;component/Cultures/StringsRes.xaml", UriKind.Relative);
                    break;
            }
            Application.Current.Resources.MergedDictionaries.Add(dict);

            //     SqlMapper.Settings.Default["Culture"] = CultureInfo.GetCultureInfoByIetfLanguageTag(culture);
            //     SqlMapper.Settings.Default.Save();


        }

    }
}
