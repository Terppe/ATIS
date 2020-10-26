using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;   
 

      //  Tbl84SynonymsView.xaml.cs Skriptdatum:  13.11.2018  10:32     

namespace Te.Atis.Ui.Desktop.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl84SynonymsView.xaml
    /// </summary>
    public partial class Tbl84SynonymsView : UserControl
   {      

   
        public Tbl84SynonymsView()
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
                    TbSearchSynonym.Focus();
                }));
            }
        }
        private void TbSearchSynonym_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                BtnGet.Focus();
                e.Handled = true;
            }
        }   
 

    }
}   

