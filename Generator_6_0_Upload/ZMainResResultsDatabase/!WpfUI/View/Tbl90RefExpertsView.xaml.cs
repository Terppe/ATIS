using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;   
 

      //  Tbl90RefExpertsView.xaml.cs Skriptdatum:  14.11.2017  10:32     

namespace WPFUI.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl90RefExpertsView.xaml
    /// </summary>
    public partial class Tbl90RefExpertsView : UserControl
   {      

   
        public Tbl90RefExpertsView()
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
                    TbSearchExpert.Focus();
                }));
            }
        }   
 

    
        private void ClearExpertButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchExpert.Text = "";
            TbSearchExpert.Focus();
        }  
        

        private void Tbl90RefExpertsListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl90RefExpertsListScroll.VerticalOffset;
            Tbl90RefExpertsListScroll.ScrollToVerticalOffset(y - x);
         }   
 

    }
}   

