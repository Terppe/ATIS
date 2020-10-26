using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;   
 

      //  Tbl90RefAuthorsView.xaml.cs Skriptdatum:  14.11.2017  10:32     

namespace WPFUI.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl90RefAuthorsView.xaml
    /// </summary>
    public partial class Tbl90RefAuthorsView : UserControl
   {      

   
        public Tbl90RefAuthorsView()
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
                    TbSearchAuthor.Focus();
                }));
            }
        }   
 

    
        private void ClearAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchAuthor.Text = "";
            TbSearchAuthor.Focus();
        }  
        

        private void Tbl90RefAuthorsListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl90RefAuthorsListScroll.VerticalOffset;
            Tbl90RefAuthorsListScroll.ScrollToVerticalOffset(y - x);
         }   
 

    }
}   

