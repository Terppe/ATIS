using System;  

    
using System.Windows.Controls;   
 

      //  Tbl51InfrafamiliesView.xaml.cs Skriptdatum:  08.11.2018  10:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for InfrafamiliesView.xaml
    /// </summary>
    public partial class InfrafamiliesView : UserControl
   {      

   
        public InfrafamiliesView()
        {  
            DataContext = new InfrafamiliesViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

