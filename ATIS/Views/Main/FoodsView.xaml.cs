using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace ATIS.Ui.Views.Main
{
    /// <summary>
    /// Interaktionslogik für FoodsView.xaml
    /// </summary>
    public partial class FoodsView : UserControl
    {
        public FoodsView()
        {
            InitializeComponent();

            var background = ConfigurationManager.AppSettings.Get("BackgroundBrush");
            var conver = new BrushConverter();
            Background = (Brush)conver.ConvertFromString(background) as SolidColorBrush;

        }
    }
}
