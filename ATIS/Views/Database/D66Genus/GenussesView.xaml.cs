using System;


using System.Windows.Controls;


//  GenussesView.xaml.cs Skriptdatum:  07.01.2021  10:32     

namespace ATIS.Ui.Views.Database.D66Genus
{

    /// <summary>
    /// Interactionslogic for GenussesView.xaml
    /// </summary>
    public partial class GenussesView : UserControl
    {


        public GenussesView()
        {
            DataContext = new GenussesViewModel();

            InitializeComponent();
        }


    }
}