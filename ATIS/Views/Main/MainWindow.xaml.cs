using ATIS.Ui.Views.Log;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Configuration;
using System.Globalization;
using System.Net.Sockets;
using System.Reflection;
using System.Windows;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using ATIS.Ui.Helper.MessageBox;
using Microsoft.Data.SqlClient;

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
            var auth = new AuthenticationViewModel(new AuthenticationService());

            DataContext = new MainWindowViewModel(DialogCoordinator.Instance);

            InitializeComponent();

            LoadStuff();

            var appName = ReadSetting("ApplicationName");
            var srv = ReadSetting("ServerName");
            var port = ReadSetting("Port");

            Title = appName + " " + Assembly.GetExecutingAssembly().GetName().Version + " || " + srv;

            TbUser.Text = auth.AuthenticatedUser;

            //choose background colors from Windows 10
            //move to Einstellungen
            //     Background = SystemParameters.WindowGlassBrush;
            if (!CheckForServer(srv, Convert.ToInt32(port)))
            {
                TbDataBase.Text = "SQL-Server not running";
            }
            else
            {
                if (!CheckDatabaseExist(srv))
                {
                    TbDataBase.Text = "Database not Found";
                }
            }


            // Log to a sub-directory 'Log' of the current working directory. 
            // Prefix log file with 'MyLog_'.
            // Write XML file, not plain text.
            // This is an optional call and has only to be done once, 
            // pereferably before the first log entry is written. 
            SimpleLog.SetLogFile(logDir: ".\\Log", prefix: "MyLog_", writeText: true);

            // Write info message to log
            SimpleLog.Info("ATIS logging started.");
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

        //private async void MetroWindow_Closing1(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    if (e.Cancel)
        //    {
        //        return;
        //    }

        //    if (_viewModel.QuitConfirmationEnabled == false && _shutdown == false)
        //    {
        //        e.Cancel = true;

        //        // We have to delay the execution through BeginInvoke to prevent potential re-entrancy
        //        //save width, height, language, assets, theme
        //        Dispatcher.BeginInvoke(new Action(async () => await ConfirmShutdown()));
        //    }
        //    else
        //    {
        //        _viewModel.Dispose();
        //    }
        //}

        //private async Task ConfirmShutdown()
        //{
        //    var mySettings = new MetroDialogSettings
        //    {
        //        AffirmativeButtonText = "Quit",
        //        NegativeButtonText = "Cancel",
        //        AnimateShow = true,
        //        AnimateHide = false
        //    };

        //    var result = await this.ShowMessageAsync("Quit application?", "Sure you want to quit application?",
        //        MessageDialogStyle.AffirmativeAndNegative, mySettings);

        //    _shutdown = result == MessageDialogResult.Affirmative;

        //    if (_shutdown)
        //    {
        //        Application.Current.Shutdown();
        //    }
        //}

        //--------------------------------------------------------------------------
        private void LoadStuff()
        {
            //Top = Convert.ToDouble(ConfigurationManager.AppSettings["Top"]);
            //Left = Convert.ToDouble(ConfigurationManager.AppSettings["Left"]);
            //Height = Convert.ToDouble(ConfigurationManager.AppSettings["Height"]);
            //Width = Convert.ToDouble(ConfigurationManager.AppSettings["Width"]);
            //          WindowState = ConfigurationManager.AppSettings["WindowState"];

            var accentColor = ConfigurationManager.AppSettings["Accent1"];
            //     new PaletteHelper().ReplaceAccentColor(accentColor);
            var primaryColor = ConfigurationManager.AppSettings["Primary1"];
            //     new PaletteHelper().ReplacePrimaryColor(primaryColor);
            var theme = ConfigurationManager.AppSettings["Theme1"];
            //    new PaletteHelper().SetLightDark(theme != "Light");
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
                //     Settings.Default.Accent1 = new PaletteHelper().QueryPalette().AccentSwatch.Name;
                //    Settings.Default.Primary1 = new PaletteHelper().QueryPalette().PrimarySwatch.Name;

                AddUpdateAppSettings("Top", Top.ToString(CultureInfo.CurrentCulture));
                AddUpdateAppSettings("Left", Left.ToString(CultureInfo.CurrentCulture));
                AddUpdateAppSettings("Height", Height.ToString(CultureInfo.CurrentCulture));
                AddUpdateAppSettings("Width", Width.ToString(CultureInfo.CurrentCulture));
                //      AddUpdateAppSettings("WindowState", WindowState);

                //AddUpdateAppSettings("Accent1", accentColor);
                //AddUpdateAppSettings("Primary1", primaryColor);
                //AddUpdateAppSettings("Theme1", theme);

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

        //------------------------------- Settings --------------------------

        static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                var result = appSettings[key] ?? "Not Found";
                return result;
            }
            catch (ConfigurationErrorsException e)
            {
                AllMessageBoxes.ErrorMessageBox(e.Message, "Error reading app settings");
                SimpleLog.Error("Error reading app settings");
            }
            return null;
        }

        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException e)
            {
                AllMessageBoxes.ErrorMessageBox(e.Message, "Error writing app settings");
                SimpleLog.Error("Error writing app settings");

            }
        }

    }
}
