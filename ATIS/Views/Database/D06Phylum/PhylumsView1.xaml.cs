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

namespace ATIS.Ui.Views.Database.D06Phylum
{
    /// <summary>
    /// Interaktionslogik für PhylumsView1.xaml
    /// </summary>
    public partial class PhylumsView1 : UserControl
    {
        public PhylumsView1()
        {
            DataContext = new PhylumsViewModel1();

            InitializeComponent();
        }
    }
}
