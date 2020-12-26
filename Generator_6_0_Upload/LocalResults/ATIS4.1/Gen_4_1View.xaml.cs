using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;   
 

      //  Tbl15SubdivisionsView.xaml.cs Skriptdatum:  14.06.2018  12:32     

namespace Te.Atis.Ui.Desktop.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl15SubdivisionsView.xaml
    /// </summary>
    public partial class Tbl15SubdivisionsView : UserControl
   {      

   
        public Tbl15SubdivisionsView()
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
                    TbSearchSubdivision.Focus();
                }));
            }
        }   
 

    }
}   

