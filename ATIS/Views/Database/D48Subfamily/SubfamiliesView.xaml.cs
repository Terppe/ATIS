using System;


using System.Windows.Controls;


//  Tbl48SubfamiliesView.xaml.cs Skriptdatum:  10.12.2020  10:32     

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