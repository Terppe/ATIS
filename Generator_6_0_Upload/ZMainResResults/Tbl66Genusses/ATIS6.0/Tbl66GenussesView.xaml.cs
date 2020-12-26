using System;  

    
using System.Windows.Controls;   
 

      //  Tbl66GenussesView.xaml.cs Skriptdatum:  13.12.2020  10:32     

namespace ATIS.Ui.Views.Database.D66Genus
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

