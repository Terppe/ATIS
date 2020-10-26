using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;   
 

      //  Tbl66GenussesView.xaml.cs Skriptdatum:  23.12.2017  10:32     

namespace WPFUI.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl66GenussesView.xaml
    /// </summary>
    public partial class Tbl66GenussesView : UserControl
   {      

   
        public Tbl66GenussesView()
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
                    TbSearchGenus.Focus();
                }));
            }
        }   
 

    
        private void ClearGenusButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchGenus.Text = "";
            TbSearchGenus.Focus();
        }  
       
        private void ClearInfratribusButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchInfratribus.Text = "";
            TbSearchInfratribus.Focus();
        }  
         
        private void ClearFiSpeciesButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchFiSpecies.Text = "";
            TbSearchFiSpecies.Focus();
        }  
         
        private void ClearPlSpeciesButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchPlSpecies.Text = "";
            TbSearchPlSpecies.Focus();
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
        

        private void Tbl69FiSpeciessesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl69FiSpeciessesListScroll.VerticalOffset;
            Tbl69FiSpeciessesListScroll.ScrollToVerticalOffset(y - x);
         }   
        
        private void Tbl72PlSpeciessesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl72PlSpeciessesListScroll.VerticalOffset;
            Tbl72PlSpeciessesListScroll.ScrollToVerticalOffset(y - x);
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

