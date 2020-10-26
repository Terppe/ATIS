using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;   
 

      //  Tbl90RefAuthorsView.xaml.cs Skriptdatum:  29.11.2017  10:32     

namespace Te.Atis.Ui.Desktop.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl90RefAuthorsView.xaml
    /// </summary>
    public partial class Tbl90RefAuthorsView : UserControl
   {      

                
        public Tbl90RefAuthorsView()
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
                    TbSearchAuthor.Focus();
                }));
            }
        } 
        private void TbSearchRefAuthor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                BtnGet.Focus();
                e.Handled = true;
            }
        }   
 

    }
}   

