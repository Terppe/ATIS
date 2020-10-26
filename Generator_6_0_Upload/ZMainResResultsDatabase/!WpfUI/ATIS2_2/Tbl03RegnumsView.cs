using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;   
 

      //  Tbl03RegnumsView.xaml.cs Skriptdatum:  20.08.2017  12:32       

namespace WPFUI.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl03RegnumsView.xaml
    /// </summary>
    public partial class Tbl03RegnumsView : UserControl
   {      

   
        public Tbl03RegnumsView()
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
                    TbSearchRegnum.Focus();
                }));
            }
        }   
 

    
        private void ClearRegnumButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchRegnum.Text = "";
            TbSearchRegnum.Focus();
        }  
       
        private void ClearPhylumButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchPhylum.Text = "";
            TbSearchPhylum.Focus();
        }  
         
        private void ClearDivisionButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchDivision.Text = "";
            TbSearchDivision.Focus();
        }  
          
        private void ClearRefExpertButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchRefExpert.Text = "";
            TbSearchRefExpert.Focus();
        }

        private void ClearRefSourceButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchRefSource.Text = "";
            TbSearchRefSource.Focus();
        }

        private void ClearRefAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchRefAuthor.Text = "";
            TbSearchRefAuthor.Focus();
        }

        private void ClearCommentButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchComment.Text = "";
            TbSearchComment.Focus();
        }           
        

        private void Tbl03RegnumsListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl03RegnumsListScroll.VerticalOffset;
            Tbl03RegnumsListScroll.ScrollToVerticalOffset(y - x);
         }   
        

        private void Tbl06PhylumsListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl06PhylumsListScroll.VerticalOffset;
            Tbl06PhylumsListScroll.ScrollToVerticalOffset(y - x);
         }   
        
        private void Tbl09DivisionsListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl09DivisionsListScroll.VerticalOffset;
            Tbl09DivisionsListScroll.ScrollToVerticalOffset(y - x);
         }   
        
        private void Tbl90RefExpertsListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl90RefExpertsListScroll.VerticalOffset;
            Tbl90RefExpertsListScroll.ScrollToVerticalOffset(y - x);
        }
        private void Tbl90RefSourcesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl90RefSourcesListScroll.VerticalOffset;
            Tbl90RefSourcesListScroll.ScrollToVerticalOffset(y - x);
        }
        private void Tbl90RefAuthorsListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl90RefAuthorsListScroll.VerticalOffset;
            Tbl90RefAuthorsListScroll.ScrollToVerticalOffset(y - x);
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

