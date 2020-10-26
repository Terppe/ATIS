using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;   
 

      //  Tbl24SubclassesView.xaml.cs Skriptdatum:  13.12.2019  18:32     

namespace Te.Atis.Ui.Desktop.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl24SubclassesView.xaml
    /// </summary>
    public partial class Tbl24SubclassesView : UserControl
   {      

   
        public Tbl24SubclassesView()
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
                    TbSearchSubclass.Focus();
                }));
            }
        }
        private void TbSearchSubclass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                BtnGet.Focus();
                e.Handled = true;
            }
        }   
 

    }
}   

