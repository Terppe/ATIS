using System.Configuration;
using System.Diagnostics;
using System.Net.Sockets;
using System.Reflection;
using Windows.System;
using ATIS.WinUi.Contracts.Services;
using ATIS.WinUi.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using ATIS.WinUi.Maps;
using ATIS.WinUi.Models;
using ATIS.WinUi.Services;
using ATIS.WinUi.ViewModels;
using ATIS.WinUi.ViewModels.Authentication;
using ATIS.WinUi.ViewModels.Main;
using Mapsui.Extensions;
using Mapsui.Tiling;
using Mapsui;
using Microsoft.Data.SqlClient;
using Microsoft.UI.Xaml.Input;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ATIS.WinUi.Views.Main;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Test1Page : Page
{
    public Test1ViewModel ViewModel
    {
        get;
    }
    private static readonly AllDialogs AllDialogs = new();

    public Test1Page()
    {
        ViewModel = App.GetService<Test1ViewModel>();
        InitializeComponent();

    }
    public Test1Page(Test1ViewModel viewModel)
    {
        //// check if Application is already running.  if it is running - Kill
        //if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly()?.Location)).Length > 1)
        //{
        //    Process.GetCurrentProcess().Kill();
        //}

        ViewModel = viewModel;
        InitializeComponent();


        //ViewModel.NavigationService.Frame = NavigationFrame;
        //ViewModel.NavigationViewService.Initialize(NavigationViewControl);

        //// TODO: Set the title bar icon by updating /Assets/WindowIcon.ico.
        //// A custom title bar is required for full window theme and Mica support.
        //// https://docs.microsoft.com/windows/apps/develop/title-bar?tabs=winui3#full-customization
        //App.MainWindow.ExtendsContentIntoTitleBar = true;
        //App.MainWindow.Activated += MainWindow_Activated;

        //((App)Application.Current).EnsureSettings();
     //   ApplyTheme();

        //var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this); // MWindow in App.cs
        //var windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
        //var appWindow = AppWindow.GetFromWindowId(windowId);

        //var size = new Windows.Graphics.SizeInt32
        //{
        //    Width = 1400, //save app.config usw.
        //    //   size.Height = 830;
        //    Height = 910
        //};
        //appWindow.Resize(size);


        //if (!CheckForServer(ConfigurationManager.AppSettings.Get("ServerName"),
        //        Convert.ToInt32(ConfigurationManager.AppSettings.Get("Port"))))
        //{
        //    TbDataBase.Text = "SQL-Server not running";
        //}
        //else
        //{
        //    if (!CheckDatabaseExist(ConfigurationManager.AppSettings.Get("ServerName")))
        //    {
        //        TbDataBase.Text = "Database not Found";
        //    }
        //}

    }
    //------------------------Check Server and Database-------------------------------
    private static bool CheckForServer(string? address, int port)
    {
        var timeout = 500;
        if (ConfigurationManager.AppSettings.Get("RemoteTestTimeout") != null)
        {
            timeout = int.Parse(ConfigurationManager.AppSettings.Get("RemoteTestTimeout") ?? string.Empty);
        }

        try
        {
            var result = false;
            using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            if (address != null)
            {
                var asyncResult = socket.BeginConnect(address, port, null, null);
                result = asyncResult.AsyncWaitHandle.WaitOne(timeout, true);
            }

            socket.Close();

            return result;
        }
        catch (SqlException e)
        {
            _ = AllDialogs.ErrorMessageDialogAsync(e.ToString());

            _ = AllDialogs.NoServerConnectWarnMessageDialogAsync();
            //MessageBox.Show(CultRes.StringsRes.NoConnectServer, e.Message,
            //    MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);

            SimpleLog.Error("Server not connected" + e.Message);
            return false;
        }
    }

    /// <summary>
    ///Check if database exist or SQLServer not running
    /// </summary>
    /// <returns></returns>
    public static bool CheckDatabaseExist(string? db)
    {
        var ret = false;

        try
        {
            using var context = new AtisDbContext();
            var dbExists = context.Database.CanConnect();

            if (!dbExists)
            {
                // MessageBox.Show(CultRes.StringsRes.NoConnectServer + " " + db, $"CultRes.StringsRes.NoConnectServer: {db}");
                if (db != null)
                {
                    _ = AllDialogs.WarningNoDatabaseWithSqlServerSelectedMessageDialogAsync(db);
                }

                //MessageBox.Show($"Database does not exist", "SQL-Server ok " + db,
                //    MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);
                if (true)
                {
                    ret = false;
                }

                SimpleLog.Error("Database does not exist - SQL - Server ok");
            }
            else
            {
                ret = true;
            }
        }
        catch (SqlException e)
        {
            _ = AllDialogs.ErrorMessageDialogAsync(e.ToString());

            _ = AllDialogs.NoServerConnectWarnMessageDialogAsync();
            //MessageBox.Show($"CultRes.StringsRes.NoConnectServer: {db}", e.Message,
            //    MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);

            SimpleLog.Error($"Server not connected: {db}");
            ret = false;
        }

        return ret;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        //KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu));
        //KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.GoBack));
    }

    private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
    {
        //  var resource = args.WindowActivationState == WindowActivationState.Deactivated ? "WindowCaptionForegroundDisabled" : "WindowCaptionForeground";

    //    TitleTextBlockMain.Text = ConfigurationManager.AppSettings.Get("ApplicationName");
        //  AppTitleBarText.Foreground = (SolidColorBrush)Application.Current.Resources[resource];
    }

    //private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
    //{
    //    var keyboardAccelerator = new KeyboardAccelerator() { Key = key };

    //    if (modifiers.HasValue)
    //    {
    //        keyboardAccelerator.Modifiers = modifiers.Value;
    //    }

    //    keyboardAccelerator.Invoked += OnKeyboardAcceleratorInvoked;

    //    return keyboardAccelerator;
    //}

    private static void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        var navigationService = App.GetService<INavigationService>();

        var result = navigationService.GoBack();

        args.Handled = result;
    }


    private void ToggleButton_Click(object sender, RoutedEventArgs e)
    {
        //var settings = ((App)Application.Current).Settings;
        //if (settings != null)
        //{
        //    settings.IsLightTheme = !settings.IsLightTheme;
        //}

        //((App)Application.Current).SaveSettings();
        //Root.ActualThemeChanged += Root_ActualThemeChanged;
        //ApplyTheme();
    }


    //private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
    //{
    //    if (sender is MenuFlyoutItem selectedItem)
    //    {
    //        var sortOption = selectedItem.Tag.ToString();
    //        switch (sortOption)
    //        {
    //            case "login":
    //                await LogIn();
    //                break;
    //            case "register":
    //                await ContentDialogRegister.ShowAsync();
    //                break;
    //            case "changepw":
    //                await ContentDialogPasswordChange.ShowAsync();
    //                break;
    //            case "forgotpw":
    //                await ContentDialogPasswordForgot.ShowAsync();
    //                break;
    //            case "logout":
    //                await LogOut();
    //                break;
    //            case "close":
    //                await Close();
    //                break;
    //        }
    //    }
    //}

    //private async Task LogIn()
    //{
    //    await ContentDialogLogIn.ShowAsync();

    //    if (Thread.CurrentPrincipal is not CustomPrincipal customPrincipal)
    //    {
    //        return;
    //    }

    //    var name = customPrincipal.Identity.Email;

    //    TbUser.Foreground = new SolidColorBrush(Colors.Green);
    //    TbUser.Text = name + " logged in";
    //}
    //private Task LogOut()
    //{
    //    if (Thread.CurrentPrincipal is CustomPrincipal customPrincipal)
    //    {
    //        customPrincipal.Identity = new AnonymousIdentity();
    //    }

    //    TbUser.Text = "nobody logged in!";
    //    TbUser.Foreground = new SolidColorBrush(Colors.Red);
    //    return Task.CompletedTask;
    //}
    private static async Task Close()
    {
        if (await AllDialogs.CloseAppQuestionConfirmationDialogAsync("ATIS"))
        {
            // Close this Application
            App.MainWindow.Close();
            //   Process.GetCurrentProcess().Kill();
            Application.Current.Exit();
        }
    }

}
