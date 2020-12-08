using System;  

    
using System.Windows.Controls;   
 

      //  Tbl66GenussesView.xaml.cs Skriptdatum:  12.12.2019  10:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for GenussesView.xaml
    /// </summary>
    public partial class GenussesView : UserControl
   {      

   
        public GenussesView()
        {  
            DataContext = new GenussesViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

