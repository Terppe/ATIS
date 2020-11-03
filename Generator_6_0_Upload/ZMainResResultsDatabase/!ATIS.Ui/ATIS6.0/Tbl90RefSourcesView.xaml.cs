using System;  

    
using System.Windows.Controls;   
 

      //  Tbl90RefSourcesView.xaml.cs Skriptdatum:   29.11.2018  10:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for RefSourcesView.xaml
    /// </summary>
    public partial class RefSourcesView : UserControl
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
                    TbSearchRefSource.Focus();
                }));
            }
        } 

        private void TbSearchRefSource_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                BtnGet.Focus();
                e.Handled = true;
            }
        }   
 

    }
}   

