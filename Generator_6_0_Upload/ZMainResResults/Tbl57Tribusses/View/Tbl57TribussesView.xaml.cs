using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;   
 

      //  Tbl57TribussesView.xaml.cs Skriptdatum:  13.12.2019  10:32     

namespace WPFUI.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl57TribussesView.xaml
    /// </summary>
    public partial class Tbl57TribussesView : UserControl
   {      

   
        public Tbl57TribussesView()
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
                    TbSearchTribus.Focus();
                }));
            }
        }   
 

    
        private void ClearTribusButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchTribus.Text = "";
            TbSearchTribus.Focus();
        }  
       
        private void ClearSupertribusButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchSupertribus.Text = "";
            TbSearchSupertribus.Focus();
        }  
         
        private void ClearSubtribusButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchSubtribus.Text = "";
            TbSearchSubtribus.Focus();
        }  
         
        private void ClearInfratribusButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchInfratribus.Text = "";
            TbSearchInfratribus.Focus();
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
        
        private void Tbl54SupertribussesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl54SupertribussesListScroll.VerticalOffset;
            Tbl54SupertribussesListScroll.ScrollToVerticalOffset(y - x);
         }   
        

        private void Tbl57TribussesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl57TribussesListScroll.VerticalOffset;
            Tbl57TribussesListScroll.ScrollToVerticalOffset(y - x);
         }   
        

        private void Tbl60SubtribussesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl60SubtribussesListScroll.VerticalOffset;
            Tbl60SubtribussesListScroll.ScrollToVerticalOffset(y - x);
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

