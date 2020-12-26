using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;   
 

      //  TblUserProfilesView.xaml.cs Skriptdatum:   15.11.2017  10:32     

namespace WPFUI.Views.Database
{  

    /// <summary>
    /// Interactionslogic for TblUserProfilesView.xaml
    /// </summary>
    public partial class TblUserProfilesView : UserControl
   {      

   
        public TblUserProfilesView()
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
                    TbSearchUserProfile.Focus();
                }));
            }
        }   
 

    
        private void ClearUserProfileButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchUserProfile.Text = "";
            TbSearchUserProfile.Focus();
        }  
        

        private void TblUserProfilesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = TblUserProfilesListScroll.VerticalOffset;
            TblUserProfilesListScroll.ScrollToVerticalOffset(y - x);
         }   
 

    }
}   

