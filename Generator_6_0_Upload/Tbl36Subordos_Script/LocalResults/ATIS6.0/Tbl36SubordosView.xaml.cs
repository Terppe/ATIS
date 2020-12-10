using System;  

    
using System.Windows.Controls;   
 

      //  Tbl36SubordosView.xaml.cs Skriptdatum:  15.12.2019  10:32     

namespace ATIS.Ui.Views.Database.D36Subordo
{  

    /// <summary>
    /// Interactionslogic for SubordosView.xaml
    /// </summary>
    public partial class SubordosView : UserControl
   {      

   
        public SubordosView()
        {  
            DataContext = new SubordosViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

