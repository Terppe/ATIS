using System;  

    
using System.Windows.Controls;   
 

      //  Tbl06PhylumsView.xaml.cs Skriptdatum:  27.10.2020  12:32     

namespace ATIS.Ui.Views.Database.D06Phylum
{  

    /// <summary>
    /// Interactionslogic for PhylumsView.xaml
    /// </summary>
    public partial class PhylumsView : UserControl
   {      

   
        public PhylumsView()
        {  
            DataContext = new PhylumsViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

