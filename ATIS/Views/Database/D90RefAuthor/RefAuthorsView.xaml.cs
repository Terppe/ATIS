using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;


//  RefAuthorsView.xaml.cs Skriptdatum:  07.02.2021  10:32     

namespace ATIS.Ui.Views.Database.D90RefAuthor
{

    /// <summary>
    /// Interactionslogic for RefAuthorsView.xaml
    /// </summary>
    public partial class RefAuthorsView : UserControl
    {


        public RefAuthorsView()
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
                        TbSearchRefAuthor.Focus();
                    }));
            }
        }
        private void TbSearchRefAuthor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
             //   BtnGet.Focus();
                e.Handled = true;
            }
        }


    }
}