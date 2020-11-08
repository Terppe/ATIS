using System;  

    
using System.Windows.Controls;   
 

      //  Tbl21ClassesView.xaml.cs Skriptdatum:  05.11.2020  18:32     

namespace ATIS.Ui.Views.Database.D21Class
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

