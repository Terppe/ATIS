using ATIS.Ui.Views.Log;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Net.Sockets;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using ControlzEx.Theming;
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
            // check if Application is already running  if it is running - Kill
            if (Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly()?.Location)).Length > 1) Process.GetCurrentProcess().Kill();

            DataContext = new MainWindowViewModel(DialogCoordinator.Instance);
            InitializeComponent();

            LoadStuff();

            if (!CheckForServer(ConfigurationManager.AppSettings.Get("ServerName"),
                    Convert.ToInt32(ConfigurationManager.AppSettings.Get("Port"))))
            {
                TbDataBase.Text = "SQL-Server not running";
            }
            else
            {
                if (!CheckDatabaseExist(ConfigurationManager.AppSettings.Get("ServerName")))
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

            Title = ConfigurationManager.AppSettings.Get("ApplicationName");
            App.Text = ConfigurationManager.AppSettings.Get("ApplicationName");
            Version.Text = "Copyright © Rudolf Terppé | Version " +
                           Assembly.GetExecutingAssembly().GetName().Version;

            //choose background colors from Windows 10
            //move to Einstellungen
            //    Background = SystemParameters.WindowGlassBrush;

            var background = ConfigurationManager.AppSettings.Get("BackgroundBrush");
            var conver = new BrushConverter();
            Background = (Brush)conver.ConvertFromString(background) as SolidColorBrush;
        }

        //------------------------Check Server and Database-------------------------------

        private bool CheckForServer(string address, int port)
        {
            var timeout = 500;
            if (ConfigurationManager.AppSettings.Get("RemoteTestTimeout") != null)
                timeout = int.Parse(ConfigurationManager.AppSettings.Get("RemoteTestTimeout") ?? string.Empty);
            var result = false;
            try
            {
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    var asyncResult = socket.BeginConnect(address, port, null, null);
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
                AffirmativeButtonText = CultRes.StringsRes.Quit,
                NegativeButtonText = CultRes.StringsRes.Cancel,
                AnimateShow = true,
                AnimateHide = false
            };
            var result = await this.ShowMessageAsync(CultRes.StringsRes.AppClose,
                CultRes.StringsRes.AppCloseQuestion,
                MessageDialogStyle.AffirmativeAndNegative, mySettings);

            var appTheme = ThemeManager.Current.DetectTheme(Application.Current);

            _shutdown = result == MessageDialogResult.Affirmative;
            if (_shutdown)
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (appTheme != null)
                {
                    config.AppSettings.Settings["Theme1"].Value = appTheme.BaseColorScheme;
                    config.AppSettings.Settings["Accent1"].Value = appTheme.ColorScheme;
                }

                config.AppSettings.Settings["Top"].Value = Top.ToString(CultureInfo.InvariantCulture);
                config.AppSettings.Settings["Left"].Value = Left.ToString(CultureInfo.InvariantCulture);
                config.AppSettings.Settings["Width"].Value = Width.ToString(CultureInfo.InvariantCulture);
                config.AppSettings.Settings["Height"].Value = Height.ToString(CultureInfo.InvariantCulture);

                config.AppSettings.Settings["Primary1"].Value = "Teal";
                config.AppSettings.Settings["Updated"].Value = false.ToString();
                config.AppSettings.Settings["CheckedUpdate"].Value = 0.1.ToString(CultureInfo.InvariantCulture);

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("AppSettings");

                Application.Current.Shutdown();
            }
        }
    }
}
