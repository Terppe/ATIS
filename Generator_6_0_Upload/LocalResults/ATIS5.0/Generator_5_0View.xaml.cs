using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;   
 

      //  Tbl03RegnumsView.xaml.cs Skriptdatum:  07.11.2018  12:32       

namespace Te.Atis.Ui.Desktop.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl03RegnumsView.xaml
    /// </summary>
    public partial class Tbl03RegnumsView : UserControl
   {      

   
        public Tbl03RegnumsView()
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
                    TbSearchRegnum.Focus();
                }));
            }
        }   
 

    }
}   

