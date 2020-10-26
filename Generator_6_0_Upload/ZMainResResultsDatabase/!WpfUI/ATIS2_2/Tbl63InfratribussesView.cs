using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;   
 

      //  Tbl63InfratribussesView.xaml.cs Skriptdatum:  26.08.2017  10:32     

namespace WPFUI.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl63InfratribussesView.xaml
    /// </summary>
    public partial class Tbl63InfratribussesView : UserControl
   {      

   
        public Tbl63InfratribussesView()
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
                    TbSearchInfratribus.Focus();
                }));
            }
        }   
 

    
        private void ClearInfratribusButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchInfratribus.Text = "";
            TbSearchInfratribus.Focus();
        }  
       
        private void ClearSubtribusButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchSubtribus.Text = "";
            TbSearchSubtribus.Focus();
        }  
         
        private void ClearGenusButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchGenus.Text = "";
            TbSearchGenus.Focus();
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
        
        private void Tbl60SubtribussesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl60SubtribussesListScroll.VerticalOffset;
            Tbl60SubtribussesListScroll.ScrollToVerticalOffset(y - x);
         }   
        

        private void Tbl63InfratribussesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl63InfratribussesListScroll.VerticalOffset;
            Tbl63InfratribussesListScroll.ScrollToVerticalOffset(y - x);
         }   
        

        private void Tbl66GenussesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl66GenussesListScroll.VerticalOffset;
            Tbl66GenussesListScroll.ScrollToVerticalOffset(y - x);
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

