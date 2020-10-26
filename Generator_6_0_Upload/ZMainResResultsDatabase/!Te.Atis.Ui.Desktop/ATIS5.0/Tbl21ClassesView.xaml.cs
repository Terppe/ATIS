using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;   
 

      //  Tbl21ClassesView.xaml.cs Skriptdatum:  16.12.2018  18:32     

namespace Te.Atis.Ui.Desktop.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl21ClassesView.xaml
    /// </summary>
    public partial class Tbl21ClassesView : UserControl
   {      

   
        public Tbl21ClassesView()
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
                    TbSearchClass.Focus();
                }));
            }
        }   
 

    }
}   

