using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
