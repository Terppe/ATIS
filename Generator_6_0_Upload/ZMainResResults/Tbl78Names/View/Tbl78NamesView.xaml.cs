using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;   
 

      //  Tbl78NamesView.xaml.cs Skriptdatum:  22.01.2019  10:32     

namespace WPFUI.Views.Database
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
 

    
        private void ClearNameButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchName.Text = "";
            TbSearchName.Focus();
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
        

        private void Tbl78NamesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl78NamesListScroll.VerticalOffset;
            Tbl78NamesListScroll.ScrollToVerticalOffset(y - x);
         }   
 

    }
}   

