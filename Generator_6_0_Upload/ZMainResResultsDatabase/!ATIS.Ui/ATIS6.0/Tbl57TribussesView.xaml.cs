using System;  

    
using System.Windows.Controls;   
 

      //  Tbl57TribussesView.xaml.cs Skriptdatum:  13.12.2019  10:32     

namespace ATIS.Ui.Views.Database.D57Tribus
{  

    /// <summary>
    /// Interactionslogic for TribussesView.xaml
    /// </summary>
    public partial class TribussesView : UserControl
   {      

   
        public TribussesView()
        {  
            DataContext = new TribussesViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

