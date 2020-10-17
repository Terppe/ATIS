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
using System.Windows.Threading;
using ATIS.Ui.Views.Database.D09Division;

namespace ATIS.Ui.Views.Database.D15Subdivision
{
    /// <summary>
    /// Interaktionslogik für SubdivisionView.xaml
    /// </summary>
    public partial class SubdivisionView : UserControl
    {
        public SubdivisionView()
        {
            DataContext = new DivisionsViewModel();

            InitializeComponent();
            IsVisibleChanged += UserControl_IsVisibleChanged;
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                Dispatcher.BeginInvoke(
                    DispatcherPriority.ContextIdle,
                    new Action(delegate
                    {
                        TbSearchDivision.Focus();
                    }));
            }
        }
        private void TbSearchDivision_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                BtnGet.Focus();
                e.Handled = true;
            }
        }
    }
}
