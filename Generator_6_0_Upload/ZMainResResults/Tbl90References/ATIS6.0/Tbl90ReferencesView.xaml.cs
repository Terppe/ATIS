using System;  

    
using System.Windows.Controls;   
 

      //  Tbl90ReferencesView.xaml.cs Skriptdatum:  29.11.2018  10:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for ReferencesView.xaml
    /// </summary>
    public partial class ReferencesView : UserControl
   {      

   
        public ReferencesView()
        {  
            DataContext = new ReferencesViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

