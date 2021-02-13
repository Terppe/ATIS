using System.Windows.Controls;


//  UserProfilesView.xaml.cs Skriptdatum:   26.02.2019  10:32     

namespace ATIS.Ui.Views.Database.DUserprofile
{

    /// <summary>
    /// Interactionslogic for UserProfilesView.xaml
    /// </summary>
    public partial class UserprofilesView : UserControl
    {


        public UserprofilesView()
        {
            DataContext = new UserprofilesViewModel();

            InitializeComponent();
        }


    }
}