using System;  

    
using System.Windows.Controls;   
 

      //  Tbl63InfratribussesView.xaml.cs Skriptdatum:  08.11.2018  10:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for InfratribussesView.xaml
    /// </summary>
    public partial class InfratribussesView : UserControl
   {      

   
        public InfratribussesView()
        {  
            DataContext = new InfratribussesViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

