using System;  

    
using System.Windows.Controls;   
 

      //  Tbl09DivisionsView.xaml.cs Skriptdatum:  04.11.2020  12:32     

namespace ATIS.Ui.Views.Database.D09Division
{  

    /// <summary>
    /// Interactionslogic for DivisionsView.xaml
    /// </summary>
    public partial class DivisionsView : UserControl
   {      

   
        public DivisionsView()
        {  
            DataContext = new DivisionsViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

