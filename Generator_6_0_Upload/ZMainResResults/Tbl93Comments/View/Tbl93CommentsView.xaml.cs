using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;   
 

      //  Tbl93CommentsView.xaml.cs Skriptdatum:  29.11.2018  10:32     

namespace WPFUI.Views.Database
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
 

    
        private void ClearCommentButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchComment.Text = "";
            TbSearchComment.Focus();
        }  
        

        private void Tbl93CommentsListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl93CommentsListScroll.VerticalOffset;
            Tbl93CommentsListScroll.ScrollToVerticalOffset(y - x);
         }   
 

    }
}   

