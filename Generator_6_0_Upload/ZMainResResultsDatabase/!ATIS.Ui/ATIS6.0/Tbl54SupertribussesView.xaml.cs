using System;  

    
using System.Windows.Controls;   
 

      //  Tbl54SupertribussesView.xaml.cs Skriptdatum:  08.11.2018  10:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for SupertribussesView.xaml
    /// </summary>
    public partial class SupertribussesView : UserControl
   {      

   
        public SupertribussesView()
        {  
            DataContext = new SupertribussesViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

