using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;   
 

      //  Tbl90RefSourcesView.xaml.cs Skriptdatum:   24.07.2018  10:32     

namespace Te.Atis.Ui.Desktop.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl90RefSourcesView.xaml
    /// </summary>
    public partial class Tbl90RefSourcesView : UserControl
   {      

                
        public Tbl90RefSourcesView()
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
                    TbSearchSource.Focus();
                }));
            }
        }   
 

    }
}   

