using System;  

    
using System.Windows.Controls;   
 

      //  Tbl60SubtribussesView.xaml.cs Skriptdatum:  08.11.2018  10:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for SubtribussesView.xaml
    /// </summary>
    public partial class SubtribussesView : UserControl
   {      

   
        public SubtribussesView()
        {  
            DataContext = new SubtribussesViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

