using System;  

    
using System.Windows.Controls;   
 

      //  Tbl90RefAuthorsView.xaml.cs Skriptdatum:  30.03.2019  10:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for RefAuthorsView.xaml
    /// </summary>
    public partial class RefAuthorsView : UserControl
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
                    TbSearchRefAuthor.Focus();
                }));
            }
        } 
        private void TbSearchRefAuthor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                BtnGet.Focus();
                e.Handled = true;
            }
        }   
 

    }
}   

