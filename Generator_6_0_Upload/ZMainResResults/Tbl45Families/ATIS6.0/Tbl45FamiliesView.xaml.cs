using System;  

    
using System.Windows.Controls;   
 

      //  Tbl45FamiliesView.xaml.cs Skriptdatum:  19.06.2018  10:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for FamiliesView.xaml
    /// </summary>
    public partial class FamiliesView : UserControl
   {      

   
        public FamiliesView()
        {  
            DataContext = new FamiliesViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

