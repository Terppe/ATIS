using System;  

    
using System.Windows.Controls;   
 

      //  Tbl45FamiliesView.xaml.cs Skriptdatum:  10.12.2020  10:32     

namespace ATIS.Ui.Views.Database.D45Family
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

