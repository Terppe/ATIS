using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;


//  RefSourcesView.xaml.cs Skriptdatum:   09.02.2021  10:32     

namespace ATIS.Ui.Views.Database.D90RefSource
{

    /// <summary>
    /// Interactionslogic for RefSourcesView.xaml
    /// </summary>
    public partial class RefSourcesView : UserControl
    {


        public RefSourcesView()
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
                        TbSearchRefSource.Focus();
                    }));
            }
        }

        private void TbSearchRefSource_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
            //    BtnGet.Focus();
                e.Handled = true;
            }
        }


    }
}