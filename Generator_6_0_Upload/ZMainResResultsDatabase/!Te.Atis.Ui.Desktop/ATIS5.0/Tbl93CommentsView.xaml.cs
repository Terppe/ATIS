using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;   
 

      //  Tbl93CommentsView.xaml.cs Skriptdatum:  29.11.2018  10:32     

namespace Te.Atis.Ui.Desktop.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl93CommentsView.xaml
    /// </summary>
    public partial class Tbl93CommentsView : UserControl
   {      

   
        public Tbl93CommentsView()
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
                    TbSearchComment.Focus();
                }));
            }
        }   
 

    }
}   

