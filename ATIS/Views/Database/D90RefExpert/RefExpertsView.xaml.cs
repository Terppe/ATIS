using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;


//  RefExpertsView.xaml.cs Skriptdatum:  09.02.2021  10:32     

namespace ATIS.Ui.Views.Database.D90RefExpert
{

    /// <summary>
    /// Interactionslogic for RefExpertsView.xaml
    /// </summary>
    public partial class RefExpertsView : UserControl
    {


        public RefExpertsView()
        {
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
                        TbSearchRefExpert.Focus();
                    }));
            }
        }
        private void TbSearchRefExpert_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
              //  BtnGet.Focus();
                e.Handled = true;
            }
        }


    }
}