using System.ComponentModel;
using System.Security.Permissions;
using System.Windows;
using ATIS.Ui.Views.Main;
using MahApps.Metro.Controls;

namespace ATIS.Ui.Views.Log
{
    /// <summary>
    /// Interaktionslogik für RegisterWindow.xaml
    /// </summary>
    [PrincipalPermission(SecurityAction.Demand)]

    public partial class RegisterWindow : MetroWindow, MainWindowViewModel.IView
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        public RegisterWindow(AuthenticationViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            IsNonCloseButtonClicked = true;
            Close(); // this will trigger the Closing () event method
        }

        public bool IsNonCloseButtonClicked { get; set; }

        private void MetroWindow_Closing(object sender, CancelEventArgs e)
        {
            MessageBox.Show("Closing called");

            if (IsNonCloseButtonClicked)
            {
                e.Cancel = false;

                // Non X Cancelbutton clicked - statements
                if (e.Cancel)
                {
                    IsNonCloseButtonClicked = false; // reset the flag
                }
            }
            else
            {

                // X button clicked - statements
            }
        }
        //void MetroWindow_Closing1(object sender, CancelEventArgs e)
        //{
        //    MessageBox.Show("Closing called");
        //    if (IsNonCloseButtonClicked)
        //    {
        //        string msg = "Close without saving?";
        //        MessageBoxResult result =
        //            MessageBox.Show(
        //                msg,
        //                "Data App",
        //                MessageBoxButton.YesNo,
        //                MessageBoxImage.Warning);
        //        if (result == MessageBoxResult.No)
        //        {
        //            // If user doesn't want to close, cancel closure
        //            e.Cancel = true;
        //            IsNonCloseButtonClicked = false; // reset the flag
        //        }
        //    }
        //}

        #region IView Members
        public IViewModel ViewModel
        {
            get => DataContext as IViewModel;
            set => DataContext = value;
        }
        #endregion

    }
}
