using System;


using System.Windows.Controls;


//  FamiliesView.xaml.cs Skriptdatum:  07.01.2021  10:32     

namespace ATIS.Ui.Views.Database.D45Family
{

    /// <summary>
    /// Interactionslogic for FamiliesView.xaml
    /// </summary>
    public partial class FamiliesView : UserControl
    {


        public FamiliesView()
        {
            DataContext = new FamiliesViewModel();

            InitializeComponent();
        }


    }
}