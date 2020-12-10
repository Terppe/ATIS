using System;  

    
using System.Windows.Controls;   
 

      //  Tbl42SuperfamiliesView.xaml.cs Skriptdatum:  10.12.2020  10:32     

namespace ATIS.Ui.Views.Database.D42Superfamily
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

