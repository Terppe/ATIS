using ATIS.Ui.Views.Log;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Threading.Tasks;
using System.Windows;
using ControlzEx.Theming;

namespace ATIS.Ui.Views.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private bool _shutdown;
        private readonly MainWindowViewModel _viewModel;

        public MainWindow()
        {
            var auth = new AuthenticationViewModel(new AuthenticationService());

            //   DataContext = new MainWindowViewModel(DialogCoordinator.Instance);
            _viewModel = new MainWindowViewModel(DialogCoordinator.Instance);
            DataContext = _viewModel;

            InitializeComponent();
            TbUser.Text = auth.AuthenticatedUser;
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
                this.HamburgerMenuControl.IsPaneOpen = false;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            IsNonCloseButtonClicked = true;
            Close(); // this will trigger the Closing () event method
        }

        public bool IsNonCloseButtonClicked { get; set; }


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
                Application.Current.Shutdown();
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
