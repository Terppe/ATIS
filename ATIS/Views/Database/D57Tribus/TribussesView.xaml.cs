using System;


using System.Windows.Controls;


//  TribussesView.xaml.cs Skriptdatum:  07.01.2021  10:32     

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