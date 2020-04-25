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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ATIS.Views.Database
{
    /// <summary>
    /// Interaktionslogik für Tbl03RegnumsView.xaml
    /// </summary>
    public partial class Tbl03RegnumsView : UserControl
    {
        public Tbl03RegnumsView()
        {
            DataContext = new Tbl03RegnumsViewModel();

            InitializeComponent();
        }
    }
}
