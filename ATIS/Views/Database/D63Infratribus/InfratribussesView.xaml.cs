using System;


using System.Windows.Controls;


//  Tbl63InfratribussesView.xaml.cs Skriptdatum:  13.12.2020  10:32     

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