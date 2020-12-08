using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;   
 

      //  Tbl90RefSourcesView.xaml.cs Skriptdatum:   05.09.2017  10:32     

namespace WPFUI.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl90RefSourcesView.xaml
    /// </summary>
    public partial class Tbl90RefSourcesView : UserControl
   {      

   
        public Tbl90RefSourcesView()
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
                    TbSearchSource.Focus();
                }));
            }
        }   
 

    
        private void ClearSourceButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchSource.Text = "";
            TbSearchSource.Focus();
        }  
        

        private void Tbl90RefSourcesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl90RefSourcesListScroll.VerticalOffset;
            Tbl90RefSourcesListScroll.ScrollToVerticalOffset(y - x);
         }   
 

    }
}   

