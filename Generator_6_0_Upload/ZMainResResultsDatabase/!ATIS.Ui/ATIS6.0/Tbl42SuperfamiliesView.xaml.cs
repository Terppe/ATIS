using System;  

    
using System.Windows.Controls;   
 

      //  Tbl42SuperfamiliesView.xaml.cs Skriptdatum:  08.11.2018  10:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for SuperfamiliesView.xaml
    /// </summary>
    public partial class SuperfamiliesView : UserControl
   {      

   
        public SuperfamiliesView()
        {  
            DataContext = new SuperfamiliesViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

