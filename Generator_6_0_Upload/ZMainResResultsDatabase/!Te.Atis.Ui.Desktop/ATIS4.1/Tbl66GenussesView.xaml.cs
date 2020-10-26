using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;   
 

      //  Tbl66GenussesView.xaml.cs Skriptdatum:  19.06.2018  10:32     

namespace Te.Atis.Ui.Desktop.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl66GenussesView.xaml
    /// </summary>
    public partial class Tbl66GenussesView : UserControl
   {      

   
        public Tbl66GenussesView()
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
                    TbSearchGenus.Focus();
                }));
            }
        }   
 

    }
}   

