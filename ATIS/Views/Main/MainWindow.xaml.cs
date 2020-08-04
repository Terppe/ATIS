using Microsoft.Toolkit.Wpf.UI.XamlHost;
using System;
using System.Windows;
using Windows.UI.Xaml.Controls;
using ATIS.Ui.Views.Database.D03Regnum;
using ATIS.Ui.Views.Database.D06Phylum;
using MahApps.Metro.Controls;
using ElementTheme = Windows.UI.Xaml.ElementTheme;
using Media = Windows.UI.Xaml.Media;

namespace ATIS.Ui.Views.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        //private readonly Media.FontFamily _segoeFontFamily;

        public MainWindow()
        {
            DataContext = new MainWindowViewModel();

            InitializeComponent();
            //_segoeFontFamily = new Media.FontFamily("Segoe MDL2 Assets");
        }

        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            HamburgerMenuControl.Content = e.InvokedItem;
        }

        //private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        //{
        //    this.HamburgerMenuControl.Content = e.InvokedItem;

        //    if (!e.IsItemOptions && this.HamburgerMenuControl.IsPaneOpen)
        //    {
        //        // close the menu if a item was selected
        //        // this.HamburgerMenuControl.IsPaneOpen = false;
        //    }
        //}



        //private void Host_ChildChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        WindowsXamlHost host = (WindowsXamlHost)sender;

        //        if (host.Child is NavigationView navView)
        //        {
        //            navView.RequestedTheme = (ElementTheme)Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.ElementTheme.Light;
        //            navView.IsBackEnabled = false;
        //            navView.IsBackButtonVisible = NavigationViewBackButtonVisible.Collapsed;
        //            navView.AutoSuggestBox = new AutoSuggestBox();
        //            navView.AutoSuggestBox.QueryIcon = new SymbolIcon(Symbol.Find);
        //            navView.PaneDisplayMode = NavigationViewPaneDisplayMode.Left;
        //            //navView.CompactPaneLength = 280;
        //            //              navView.CompactModeThresholdWidth = 1;

        //            //              navView.ExpandedModeThresholdWidth = 100000;
        //            navView.PaneTitle = "ATIS";
        //            //    navigationView.PaneFooter =  
        //            navView.IsPaneOpen = true;
        //            //      navView.PaneCustomContent.Visibility = Windows.UI.Xaml.Visibility.Visible;

        //            navView.MenuItems.Add(new NavigationViewItemSeparator());

        //            var homeItem = new NavigationViewItem()
        //            {
        //                Content = "Home",
        //                Icon = new FontIcon()
        //                {
        //                    FontFamily = _segoeFontFamily,
        //                    Glyph = "\uE80F"
        //                }
        //            };

        //            var searchAdvancedItem = new NavigationViewItem()
        //            {
        //                Content = "Search Advanced",
        //                Icon = new FontIcon()
        //                {
        //                    FontFamily = _segoeFontFamily,
        //                    Glyph = "\uE80F"
        //                }
        //            };

        //            var imageItem = new NavigationViewItem()
        //            {
        //                Content = "Image/Video",
        //                Icon = new FontIcon()
        //                {
        //                    FontFamily = _segoeFontFamily,
        //                    Glyph = "\uE719"
        //                }
        //            };

        //            var databaseItem = new NavigationViewItem()
        //            {
        //                Content = "Database",
        //                Icon = new FontIcon()
        //                {
        //                    FontFamily = _segoeFontFamily,
        //                    Glyph = "\uE719"
        //                }
        //            };

        //            //var databaseSepItem = new NavigationViewItemSeparator();
        //            //var databaseHeaderItem = new NavigationViewItemHeader()
        //            //{
        //            //    Content = "Database"
        //            //};


        //            var infoItem = new NavigationViewItem()
        //            {
        //                Content = "Info Area",
        //                Icon = new FontIcon()
        //                {
        //                    FontFamily = _segoeFontFamily,
        //                    Glyph = "\uE8C7"
        //                }
        //            };

        //            var userItem = new NavigationViewItem()
        //            {
        //                Content = "User Area",
        //                Icon = new FontIcon()
        //                {
        //                    FontFamily = _segoeFontFamily,
        //                    Glyph = "\uE8C7"
        //                }
        //            };

        //            var adminItem = new NavigationViewItem()
        //            {
        //                Content = "Admin Area",
        //                Icon = new FontIcon()
        //                {
        //                    FontFamily = _segoeFontFamily,
        //                    Glyph = "\uE8C7"
        //                }
        //            };

        //            var feedbackItem = new NavigationViewItem()
        //            {
        //                Content = "Feedback",
        //                Icon = new FontIcon()
        //                {
        //                    FontFamily = _segoeFontFamily,
        //                    Glyph = "\uE8C7"
        //                }
        //            };

        //            var aboutItem = new NavigationViewItem()
        //            {
        //                Content = "About",
        //                Icon = new FontIcon()
        //                {
        //                    FontFamily = _segoeFontFamily,
        //                    Glyph = "\uE8C7"
        //                }
        //            };

        //            navView.MenuItems.Add(homeItem);
        //            navView.MenuItems.Add(searchAdvancedItem);
        //            navView.MenuItems.Add(imageItem);
        //            navView.MenuItems.Add(databaseItem);
        //            //navView.MenuItems.Add(databaseSepItem);
        //            //navView.MenuItems.Add(databaseHeaderItem);
        //            navView.MenuItems.Add(infoItem);
        //            navView.MenuItems.Add(userItem);
        //            navView.MenuItems.Add(adminItem);
        //            navView.MenuItems.Add(feedbackItem);
        //            navView.MenuItems.Add(aboutItem);

        //            navView.ItemInvoked += navigationView_ItemInvoked;
        //        }

        //    }
        //    catch (Exception exception)
        //    {
        //        Console.WriteLine(exception);
        //        throw;
        //    }
        //}

        //private void navigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        //{
        //    var item = args.InvokedItemContainer as NavigationViewItem;
        //    if (item == null)
        //        return;
        //    switch (item.Content)
        //    {
        //        case "Einstellungen":
        //            Main.Content = new SettingView();
        //            break;
        //        case "Home":
        //            Main.Content = new HomeView();
        //            break;
        //        case "Search Advanced":
        //            Main.Content = new HomeView();
        //            break;
        //        case "Image/Video":
        //            Main.Content = new HomeView();
        //            break;
        //        case "Database":
        //            Main.Content = new DatabaseView();
        //            break;
        //        case "Info Area":
        //            Main.Content = new HomeView();
        //            break;
        //        case "User Area":
        //            Main.Content = new HomeView();
        //            break;
        //        case "Admin Area":
        //            Main.Content = new HomeView();
        //            break;
        //        case "Feedback":
        //            Main.Content = new HomeView();
        //            break;
        //        case "About":
        //            Main.Content = new HomeView();
        //            break;
        //        default:
        //            Main.Content = new HomeView();
        //            break;
        //    }

        //}
    }
}
