using System;  

    
using System.Windows.Controls;   
 

      //  Tbl18SuperclassesView.xaml.cs Skriptdatum:  04.11.2020  12:32     

namespace ATIS.Ui.Views.Database.D18Superclass
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

