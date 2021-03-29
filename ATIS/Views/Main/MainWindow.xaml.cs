using ATIS.Ui.Views.Log;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Net.Sockets;
using System.Reflection;
using System.Windows;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using ATIS.Ui.Properties;
using ControlzEx.Theming;
using MaterialDesignThemes.Wpf;
using Microsoft.Data.SqlClient;
using System.Windows.Media;
using Theme = ControlzEx.Theming.Theme;

namespace ATIS.Ui.Views.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private bool _shutdown;
        private static readonly AllMessageBoxes AllMessageBoxes = new AllMessageBoxes();

        public MainWindow()
        {
            // check if Application is already running  if it is running - Kill
            if (Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly()?.Location)).Length > 1) Process.GetCurrentProcess().Kill();

            DataContext = new MainWindowViewModel(DialogCoordinator.Instance);
            InitializeComponent();

            LoadStuff();

            if (!CheckForServer(Settings.Default.ServerName, Convert.ToInt32(Settings.Default.Port)))
            {
                TbDataBase.Text = "SQL-Server not running";
            }
            else
            {
                if (!CheckDatabaseExist(Settings.Default.ServerName))
                {
                    TbDataBase.Text = "Database not Found";
                }
            }

            // Write info message to log
            SimpleLog.Info("MainWindow logging started.");
        }

        //---------------------------Flyout --------------------------------

        private void ShowModal(object sender, RoutedEventArgs e)
        {
            ToggleFlyout(0);
        }

        private void ToggleFlyout(int index)
        {
            var flyout = Flyouts.Items[index] as Flyout;
            if (flyout == null)
            {
                return;
            }

            flyout.IsOpen = !flyout.IsOpen;
        }


        //--------------------------------HamburgerMenu------------------------------------

        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            HamburgerMenuControl.Content = e.InvokedItem;

            if (!e.IsItemOptions && HamburgerMenuControl.IsPaneOpen)
            {
                // close the menu if a item was selected
                HamburgerMenuControl.IsPaneOpen = false;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            IsNonCloseButtonClicked = true;

            // Write info message to log
            SimpleLog.Info(CultRes.StringsRes.AppClosed);

            Close(); // this will trigger the Closing () event method
        }

        public bool IsNonCloseButtonClicked { get; set; }

        private void ShowLogFile(object sender, RoutedEventArgs e)
        {
            SimpleLog.ShowLogFile();
        }

        private void LoadStuff()
        {
            var auth = new AuthenticationViewModel(new AuthenticationService());
            TbUser.Text = auth.AuthenticatedUser;

            Title = Settings.Default.ApplicationName;
            App.Text = Settings.Default.ApplicationName;
            Version.Text = "Copyright © Rudolf Terppé | Version " + Assembly.GetExecutingAssembly().GetName().Version + " | " + Settings.Default.ServerName;
            //choose background colors from Windows 10
            //move to Einstellungen
            //    Background = SystemParameters.WindowGlassBrush;

            WindowState = Settings.Default.WindowState;

            // Set the window theme to Dark.Red
            //       ThemeManager.Current.ChangeTheme(this, Settings.Default.Theme1);

            //   var theme = ThemeManager.Current.GetTheme("BaseDark");
            //      var accent = ThemeManager.Current.BaseColors;

            //new PaletteHelper().ReplaceAccentColor(accentColor);
            //var primaryColor = Settings.Default.Primary1;
            //new PaletteHelper().ReplacePrimaryColor(primaryColor);
            //var theme = Settings.Default.Theme1;
            //new PaletteHelper().SetLightDark(theme != "Light");
        }

        //------------------------Check Server and Database-------------------------------

        private bool CheckForServer(string address, int port)
        {
            var timeout = 500;
            if (ConfigurationManager.AppSettings["RemoteTestTimeout"] != null)
                timeout = int.Parse(ConfigurationManager.AppSettings["RemoteTestTimeout"]);
            var result = false;
            try
            {
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    IAsyncResult asyncResult = socket.BeginConnect(address, port, null, null);
                    result = asyncResult.AsyncWaitHandle.WaitOne(timeout, true);
                    socket.Close();
                }
                return result;
            }
            catch (SqlException e)
            {
                MessageBox.Show(CultRes.StringsRes.NoConnectServer, e.Message,
                    MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);

                SimpleLog.Error(CultRes.StringsRes.NoConnectServer + e.Message);
                return false;
            }
        }
        /// <summary>
        ///Check if database exist or SQLServer not running
        /// </summary>
        /// <returns></returns>
        public static bool CheckDatabaseExist(string db)
        {
            var ret = false;

            try
            {
                using (var context = new AtisDbContext())
                {
                    var dbExists = context.Database.CanConnect();

                    if (!dbExists)
                    {
                        // MessageBox.Show(CultRes.StringsRes.NoConnectServer + " " + db, $"CultRes.StringsRes.NoConnectServer: {db}");

                        MessageBox.Show($"Database does not exist", "SQL-Server ok " + db,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);
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
            }
            catch (SqlException e)
            {
                MessageBox.Show($"CultRes.StringsRes.NoConnectServer: {db}", e.Message,
                    MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);

                SimpleLog.Error($"CultRes.StringsRes.NoConnectServer: {db}");
                ret = false;
            }
            return ret;
        }

        //------------------------------------------------------------------
        private async void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !_shutdown;
            if (_shutdown) return;
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Quit",
                NegativeButtonText = "Cancel",
                AnimateShow = true,
                AnimateHide = false
            };
            var result = await this.ShowMessageAsync(CultRes.StringsRes.AppClose,
                CultRes.StringsRes.AppCloseQuestion,
                MessageDialogStyle.AffirmativeAndNegative, mySettings);

            _shutdown = result == MessageDialogResult.Affirmative;
            if (_shutdown)
            {
                //Settings.Default.Accent1 = new PaletteHelper().QueryPalette().AccentSwatch.Name;
                //Settings.Default.Primary1 = new PaletteHelper().QueryPalette().PrimarySwatch.Name;
                //Settings.Default.Theme1 = new PaletteHelper().SetLightDark(isDark); 

                Settings.Default.WindowState = WindowState;
                Theme theme = ThemeManager.Current.DetectTheme(this);
                //  Settings.Default.Theme1 = theme;
                Settings.Default.Theme1 = ThemeManager.Current.DetectTheme().Name;
                Settings.Default.Accent1 = ThemeManager.Current.DetectTheme().DisplayName;
                Settings.Default.Primary1 = ThemeManager.Current.DetectTheme().Name;
                //var appTheme = ThemeManager.GetAppTheme(SelectedTheme.Name);
                //var accent = ThemeManager.GetAccent(SelectedAccent.Name);

                //Settings.Default.Theme1 = SelectedTheme.Name;
                //Settings.Default.Accent1 = SelectedAccent.Name;
                //Theme1 direct in PaletteSelectorViewModel
                Settings.Default.Save();

                Application.Current.Shutdown();
            }
        }

        //private void MenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    // set the Red accent and dark theme only to the current window
        //    var theme = ThemeManager.GetAppTheme("BaseDark");
        //    var accent = ThemeManager.GetAccent("Red");
        //    ThemeManager.ChangeAppStyle(Application.Current, accent, theme);
        //}



    }
}
