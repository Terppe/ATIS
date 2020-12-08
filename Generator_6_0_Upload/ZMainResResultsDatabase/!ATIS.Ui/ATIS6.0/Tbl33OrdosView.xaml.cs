using System;  

    
using System.Windows.Controls;   
 

      //  Tbl33OrdosView.xaml.cs Skriptdatum:  29.01.2019  10:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for OrdosView.xaml
    /// </summary>
    public partial class OrdosView : UserControl
   {      

   
        public OrdosView()
        {  
            DataContext = new OrdosViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

