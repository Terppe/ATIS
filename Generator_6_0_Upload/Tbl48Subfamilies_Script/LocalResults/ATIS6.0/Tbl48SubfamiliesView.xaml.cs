using System;  

    
using System.Windows.Controls;   
 

      //  Tbl48SubfamiliesView.xaml.cs Skriptdatum:  08.11.2018  10:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for SubfamiliesView.xaml
    /// </summary>
    public partial class SubfamiliesView : UserControl
   {      

   
        public SubfamiliesView()
        {  
            DataContext = new SubfamiliesViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

