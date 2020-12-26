using System;  

    
using System.Windows.Controls;   
 

      //  Tbl15SubdivisionsView.xaml.cs Skriptdatum:  04.11.2020  12:32     

namespace ATIS.Ui.Views.Database.D15Subdivision
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

