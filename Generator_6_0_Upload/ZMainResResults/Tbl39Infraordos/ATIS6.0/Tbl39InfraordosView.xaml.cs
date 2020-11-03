using System;  

    
using System.Windows.Controls;   
 

      //  Tbl39InfraordosView.xaml.cs Skriptdatum:  08.11.2018  10:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for InfraordosView.xaml
    /// </summary>
    public partial class InfraordosView : UserControl
   {      

   
        public InfraordosView()
        {  
            DataContext = new InfraordosViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

