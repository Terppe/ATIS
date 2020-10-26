using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;   
 

      //  Tbl06PhylumsView.xaml.cs Skriptdatum:  05.12.2018  12:32     

namespace Te.Atis.Ui.Desktop.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl06PhylumsView.xaml
    /// </summary>
    public partial class Tbl06PhylumsView : UserControl
   {      

   
        public Tbl06PhylumsView()
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
                    TbSearchPhylum.Focus();
                }));
            }
        }   
 

    }
}   

