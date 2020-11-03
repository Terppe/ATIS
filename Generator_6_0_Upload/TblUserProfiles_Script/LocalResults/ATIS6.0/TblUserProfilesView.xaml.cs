using System;  

    
using System.Windows.Controls;   
 

      //  TblUserProfilesView.xaml.cs Skriptdatum:   26.02.2019  10:32     

namespace ATIS.Ui.Views.Database.ListDetails
{  

    /// <summary>
    /// Interactionslogic for UserProfilesView.xaml
    /// </summary>
    public partial class UserProfilesView : UserControl
   {      

   
        public UserProfilesView()
        {  
            DataContext = new UserProfilesViewModel();  
       
            InitializeComponent();   
        }      
 

    }
}   

