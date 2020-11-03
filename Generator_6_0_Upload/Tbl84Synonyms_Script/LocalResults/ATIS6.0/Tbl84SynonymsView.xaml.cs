using System;  

    
using System.Windows.Controls;   
 

      //  Tbl84SynonymsView.xaml.cs Skriptdatum:  13.11.2018  10:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for SynonymsView.xaml
    /// </summary>
    public partial class SynonymsView : UserControl
   {      

   
        public SynonymsView()
        {  
            DataContext = new SynonymsViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

