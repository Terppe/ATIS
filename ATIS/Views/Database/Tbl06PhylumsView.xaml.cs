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
    /// Interaktionslogik für Tbl03PhylumsView.xaml
    /// </summary>
    public partial class Tbl06PhylumsView : UserControl
    {
        public Tbl06PhylumsView()
        {
            DataContext = new Tbl06PhylumsViewModel();

            InitializeComponent();
        }
    }
}
