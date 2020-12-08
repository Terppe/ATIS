using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;   
 

      //  TblCountersView.xaml.cs Skriptdatum:  3.1.2012  12:32       

namespace WPFUI.Views.Database
{  

    /// <summary>
    /// Interactionslogic for TblCountersView.xaml
    /// </summary>
    public partial class TblCountersView : UserControl
   {      

   
        public TblCountersView()
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
                    TbSearchCounter.Focus();
                }));
            }
        }   
 

    
        private void ClearCounterButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchCounter.Text = "";
            TbSearchCounter.Focus();
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

    }
}   

