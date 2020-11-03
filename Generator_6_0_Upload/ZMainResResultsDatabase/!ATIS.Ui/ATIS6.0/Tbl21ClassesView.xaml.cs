using System;  

    
using System.Windows.Controls;   
 

      //  Tbl21ClassesView.xaml.cs Skriptdatum:  12.12.2019  18:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for ClassesView.xaml
    /// </summary>
    public partial class ClassesView : UserControl
   {      

   
        public ClassesView()
        {  
            DataContext = new ClassesViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

