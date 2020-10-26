using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;   
 

      //  Tbl09DivisionsView.xaml.cs Skriptdatum:  07.11.2018  12:32     

namespace Te.Atis.Ui.Desktop.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl09DivisionsView.xaml
    /// </summary>
    public partial class Tbl09DivisionsView : UserControl
   {      

   
        public Tbl09DivisionsView()
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
                    TbSearchDivision.Focus();
                }));
            }
        }   
 

    }
}   

