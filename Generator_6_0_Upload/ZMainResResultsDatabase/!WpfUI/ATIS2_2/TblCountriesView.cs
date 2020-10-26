using System;  

    
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;   
 

      //  TblCountriesView.xaml.cs Skriptdatum:   09.09.2017 12:32       

namespace WPFUI.Views.Database
{  

    /// <summary>
    /// Interactionslogic for TblCountriesView.xaml
    /// </summary>
    public partial class TblCountriesView : UserControl
   {      

   
        public TblCountriesView()
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
                    TbSearchCountry.Focus();
                }));
            }
        }   
 

    
        private void ClearCountryButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchCountry.Text = "";
            TbSearchCountry.Focus();
        }  
        

        private void TblCountriesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = TblCountriesListScroll.VerticalOffset;
            TblCountriesListScroll.ScrollToVerticalOffset(y - x);
         }   
 

    }
}   

