using System;  

    
using System.Windows.Controls;   
 

      //  Tbl30LegiosView.xaml.cs Skriptdatum:  08.11.20201817  10:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for LegiosView.xaml
    /// </summary>
    public partial class LegiosView : UserControl
   {      

   
        public LegiosView()
        {  
            DataContext = new LegiosViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

