using ATIS.Ui.Views.Main;
using MahApps.Metro.Controls;

namespace ATIS.Ui.Views.Log
{
    /// <summary>
    /// Interaktionslogik für PasswordChangeWindow.xaml
    /// </summary>
    public partial class PasswordChangeWindow : MetroWindow, MainWindowViewModel.IView
    {
        public PasswordChangeWindow()
        {
            InitializeComponent();
        }

        public PasswordChangeWindow(AuthenticationViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }

        public IViewModel ViewModel { get; set; }
    }
}
