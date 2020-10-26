using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;   
 

      //  Tbl57TribussesView.xaml.cs Skriptdatum:  08.11.2018  10:32     

namespace Te.Atis.Ui.Desktop.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl57TribussesView.xaml
    /// </summary>
    public partial class Tbl57TribussesView : UserControl
   {      

   
        public Tbl57TribussesView()
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
                    TbSearchTribus.Focus();
                }));
            }
        }   
 

    }
}   

