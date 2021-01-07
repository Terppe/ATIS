using System;


using System.Windows.Controls;


//  SubfamiliesView.xaml.cs Skriptdatum:  07.01.2021  10:32     

namespace ATIS.Ui.Views.Database.D48Subfamily
{

    /// <summary>
    /// Interactionslogic for SubfamiliesView.xaml
    /// </summary>
    public partial class SubfamiliesView : UserControl
    {


        public SubfamiliesView()
        {
            DataContext = new SubfamiliesViewModel();

            InitializeComponent();
        }


    }
}