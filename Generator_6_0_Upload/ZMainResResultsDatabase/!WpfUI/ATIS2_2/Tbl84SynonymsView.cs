using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;   
 

      //  Tbl84SynonymsView.xaml.cs Skriptdatum:  04.09.2017  10:32     

namespace WPFUI.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl84SynonymsView.xaml
    /// </summary>
    public partial class Tbl84SynonymsView : UserControl
   {      

   
        public Tbl84SynonymsView()
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
                    TbSearchSynonym.Focus();
                }));
            }
        }   
 

    
        private void ClearSynonymButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchSynonym.Text = "";
            TbSearchSynonym.Focus();
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
        

        private void Tbl84SynonymsListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl84SynonymsListScroll.VerticalOffset;
            Tbl84SynonymsListScroll.ScrollToVerticalOffset(y - x);
         }   
 

    }
}   

