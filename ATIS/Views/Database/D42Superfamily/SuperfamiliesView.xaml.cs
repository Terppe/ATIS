using System;


using System.Windows.Controls;


//  SuperfamiliesView.xaml.cs Skriptdatum:  07.01.2021  10:32     

namespace ATIS.Ui.Views.Database.D42Superfamily
{

    /// <summary>
    /// Interactionslogic for SuperfamiliesView.xaml
    /// </summary>
    public partial class SuperfamiliesView : UserControl
    {


        public SuperfamiliesView()
        {
            DataContext = new SuperfamiliesViewModel();

            InitializeComponent();
        }


    }
}