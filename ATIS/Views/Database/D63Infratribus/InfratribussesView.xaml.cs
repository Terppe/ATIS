using System;


using System.Windows.Controls;


//  InfratribussesView.xaml.cs Skriptdatum:  07.01.2021  10:32     

namespace ATIS.Ui.Views.Database.D63Infratribus
{

    /// <summary>
    /// Interactionslogic for InfratribussesView.xaml
    /// </summary>
    public partial class InfratribussesView : UserControl
    {


        public InfratribussesView()
        {
            DataContext = new InfratribussesViewModel();

            InitializeComponent();
        }


    }
}