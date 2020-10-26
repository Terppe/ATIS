using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;   
 

      //  Tbl45FamiliesView.xaml.cs Skriptdatum:  19.06.2018  10:32     

namespace WPFUI.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl45FamiliesView.xaml
    /// </summary>
    public partial class Tbl45FamiliesView : UserControl
   {      

   
        public Tbl45FamiliesView()
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
                    TbSearchFamily.Focus();
                }));
            }
        }   
 

    
        private void ClearFamilyButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchFamily.Text = "";
            TbSearchFamily.Focus();
        }  
       
        private void ClearSuperfamilyButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchSuperfamily.Text = "";
            TbSearchSuperfamily.Focus();
        }  
         
        private void ClearSubfamilyButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchSubfamily.Text = "";
            TbSearchSubfamily.Focus();
        }  
         
        private void ClearInfrafamilyButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchInfrafamily.Text = "";
            TbSearchInfrafamily.Focus();
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
        
        private void Tbl42SuperfamiliesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl42SuperfamiliesListScroll.VerticalOffset;
            Tbl42SuperfamiliesListScroll.ScrollToVerticalOffset(y - x);
         }   
        

        private void Tbl45FamiliesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl45FamiliesListScroll.VerticalOffset;
            Tbl45FamiliesListScroll.ScrollToVerticalOffset(y - x);
         }   
        

        private void Tbl48SubfamiliesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl48SubfamiliesListScroll.VerticalOffset;
            Tbl48SubfamiliesListScroll.ScrollToVerticalOffset(y - x);
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

