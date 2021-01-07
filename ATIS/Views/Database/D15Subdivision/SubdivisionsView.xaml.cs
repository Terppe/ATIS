using System;


using System.Windows.Controls;


//  SubdivisionsView.xaml.cs Skriptdatum:  07.01.2021  12:32     

namespace ATIS.Ui.Views.Database.D15Subdivision
{

    /// <summary>
    /// Interactionslogic for SubdivisionsView.xaml
    /// </summary>
    public partial class SubdivisionsView : UserControl
    {


        public SubdivisionsView()
        {
            DataContext = new SubdivisionsViewModel();

            InitializeComponent();
        }


    }
}