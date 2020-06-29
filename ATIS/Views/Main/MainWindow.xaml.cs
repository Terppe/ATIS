using Microsoft.Toolkit.Wpf.UI.XamlHost;
using System;
using System.Windows;
using Windows.UI.Xaml.Controls;
using ATIS.Ui.Views.Database.D03Regnum;
using ATIS.Ui.Views.Database.D06Phylum;
using ElementTheme = Windows.UI.Xaml.ElementTheme;
using Media = Windows.UI.Xaml.Media;

namespace ATIS.Ui.Views.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Media.FontFamily _segoeFontFamily;

        public MainWindow()
        {
            DataContext = new MainWindowViewModel();

            InitializeComponent();
            _segoeFontFamily = new Media.FontFamily("Segoe MDL2 Assets");
        }




        private void Host_ChildChanged(object sender, EventArgs e)
        {
            try
            {
                WindowsXamlHost host = (WindowsXamlHost)sender;

                if (host.Child is NavigationView navView)
                {
                    navView.RequestedTheme = (ElementTheme)Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.ElementTheme.Light;
                    navView.IsBackButtonVisible = NavigationViewBackButtonVisible.Auto;
                    navView.AutoSuggestBox = new AutoSuggestBox();
                    navView.PaneDisplayMode = NavigationViewPaneDisplayMode.Left;
                    navView.PaneTitle = "ATIS";
                    //    navigationView.PaneFooter =  
                    //navView.IsPaneOpen = true;
                    //      navView.PaneCustomContent.Visibility = Windows.UI.Xaml.Visibility.Visible;

                    navView.MenuItems.Add(new NavigationViewItemSeparator());

                    var homeItem = new NavigationViewItem()
                    {
                        Content = "Home",
                        Icon = new FontIcon()
                        {
                            FontFamily = _segoeFontFamily,
                            Glyph = "\uE80F"
                        }
                    };

                    var databaseItem = new NavigationViewItem()
                    {
                        Content = "Database",
                        Icon = new FontIcon()
                        {
                            FontFamily = _segoeFontFamily,
                            Glyph = "\uE719"
                        }
                    };

                    var databaseSepItem = new NavigationViewItemSeparator();
                    var databaseHeaderItem = new NavigationViewItemHeader()
                    {
                        Content = "Database"
                    };


                    var regnumItem = new NavigationViewItem()
                    {
                        Content = "Regnums",
                        Icon = new FontIcon()
                        {
                            FontFamily = _segoeFontFamily,
                            Glyph = "\uE8C7"
                        }
                    };

                    var phylumItem = new NavigationViewItem()
                    {
                        Content = "Phylums",
                        Icon = new FontIcon()
                        {
                            FontFamily = _segoeFontFamily,
                            Glyph = "\uE8C7"
                        }
                    };

                    navView.MenuItems.Add(homeItem);
                    navView.MenuItems.Add(databaseItem);
                    navView.MenuItems.Add(databaseSepItem);
                    navView.MenuItems.Add(databaseHeaderItem);
                    navView.MenuItems.Add(regnumItem);
                    navView.MenuItems.Add(phylumItem);

                    navView.ItemInvoked += navigationView_ItemInvoked;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void navigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item = args.InvokedItemContainer as NavigationViewItem;
            if (item == null)
                return;
            switch (item.Content)
            {
                case "Einstellungen":
                    Main.Content = new SettingView();
                    break;
                case "Home":
                    Main.Content = new HomeView();
                    break;
                case "Database":
                    Main.Content = new DatabaseView();
                    break;
                case "Regnums":
                    Main.Content = new RegnumsView();
                    break;
                case "Phylums":
                    Main.Content = new PhylumsView();
                    break;
                default:
                    Main.Content = new HomeView();
                    break;
            }
        }
    }
}
