using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace ATIS.Ui.Views.Database.D09Division
{
    /// <summary>
    /// Interaktionslogik für DivisionsView.xaml
    /// </summary>
    public partial class DivisionsView : UserControl
    {
        public DivisionsView()
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
