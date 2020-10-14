using ATIS.Ui.Views.Main;
using MahApps.Metro.Controls;
using System.ComponentModel;
using System.Security.Permissions;
using System.Windows;

namespace ATIS.Ui.Views.Log
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    [PrincipalPermission(SecurityAction.Demand)]
    public partial class LoginWindow : MetroWindow, MainWindowViewModel.IView
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        public LoginWindow(AuthenticationViewModel viewModel)
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

        #region IView Members
        public IViewModel ViewModel
        {
            get => DataContext as IViewModel;
            set => DataContext = value;
        }
        #endregion
    }
}
