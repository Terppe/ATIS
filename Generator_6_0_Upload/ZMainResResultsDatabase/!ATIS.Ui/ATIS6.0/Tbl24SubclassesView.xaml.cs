using System;  

    
using System.Windows.Controls;   
 

      //  Tbl24SubclassesView.xaml.cs Skriptdatum:  13.12.2019  18:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for SubclassesView.xaml
    /// </summary>
    public partial class SubclassesView : UserControl
   {      

   
        public SubclassesView()
        {  
            DataContext = new SubclassesViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

