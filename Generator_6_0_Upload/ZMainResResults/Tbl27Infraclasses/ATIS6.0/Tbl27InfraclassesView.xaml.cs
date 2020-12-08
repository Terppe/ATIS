using System;  

    
using System.Windows.Controls;   
 

      //  Tbl27InfraclassesView.xaml.cs Skriptdatum:  08.11.2018  18:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for InfraclassesView.xaml
    /// </summary>
    public partial class InfraclassesView : UserControl
   {      

   
        public InfraclassesView()
        {  
            DataContext = new InfraclassesViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

