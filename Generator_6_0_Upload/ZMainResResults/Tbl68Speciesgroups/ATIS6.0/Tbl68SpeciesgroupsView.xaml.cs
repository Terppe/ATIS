using System;  

    
using System.Windows.Controls;   
 

      //  Tbl68SpeciesgroupsView.xaml.cs Skriptdatum:  09.11.2018  10:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for SpeciesgroupsView.xaml
    /// </summary>
    public partial class SpeciesgroupsView : UserControl
   {      

   
        public SpeciesgroupsView()
        {  
            DataContext = new SpeciesgroupsViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

