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

namespace ATIS.Ui.Views.Database.D03Regnum
{
    /// <summary>
    /// Interaktionslogik für RegnumsView1.xaml
    /// </summary>
    public partial class RegnumsView1 : UserControl
    {
        public RegnumsView1()
        {
            DataContext = new RegnumsViewModel1();

            InitializeComponent();

        }
    }
}
