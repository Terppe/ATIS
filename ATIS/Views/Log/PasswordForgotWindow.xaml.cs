using ATIS.Ui.Views.Main;
using MahApps.Metro.Controls;

namespace ATIS.Ui.Views.Log
{
    /// <summary>
    /// Interaktionslogik für PasswordForgotWindow.xaml
    /// </summary>
    public partial class PasswordForgotWindow : MetroWindow, MainWindowViewModel.IView
    {
        public PasswordForgotWindow()
        {
            InitializeComponent();
        }

        public PasswordForgotWindow(AuthenticationViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }

        public IViewModel ViewModel { get; set; }
    }
}
