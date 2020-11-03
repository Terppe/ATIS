using System;  

    
using System.Windows.Controls;   
 

      //  Tbl15SubdivisionsView.xaml.cs Skriptdatum:  12.12.2019  12:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for SubdivisionsView.xaml
    /// </summary>
    public partial class SubdivisionsView : UserControl
   {      

   
        public SubdivisionsView()
        {  
            DataContext = new SubdivisionsViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

