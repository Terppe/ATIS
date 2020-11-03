using System;  

    
using System.Windows.Controls;   
 

      //  Tbl18SuperclassesView.xaml.cs Skriptdatum:  12.12.2018  12:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for SuperclassesView.xaml
    /// </summary>
    public partial class SuperclassesView : UserControl
   {      

   
        public SuperclassesView()
        {  
            DataContext = new SuperclassesViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

