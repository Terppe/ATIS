using System;  

    
using System.Windows.Controls;   
 

      //  Tbl93CommentsView.xaml.cs Skriptdatum:  29.11.2018  10:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for CommentsView.xaml
    /// </summary>
    public partial class CommentsView : UserControl
   {      

   
        public CommentsView()
        {  
            DataContext = new CommentsViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

