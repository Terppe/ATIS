using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;   
 

      //  Tbl78NamesView.xaml.cs Skriptdatum:  12.11.2018  10:32     

namespace Te.Atis.Ui.Desktop.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl78NamesView.xaml
    /// </summary>
    public partial class Tbl78NamesView : UserControl
   {      

   
        public Tbl78NamesView()
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
                    TbSearchName.Focus();
                }));
            }
        }   
 

    }
}   

