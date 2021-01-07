using System;


using System.Windows.Controls;


//  SupertribussesView.xaml.cs Skriptdatum:  07.01.2021  10:32     

namespace ATIS.Ui.Views.Database.D54Supertribus
{

    /// <summary>
    /// Interactionslogic for SupertribussesView.xaml
    /// </summary>
    public partial class SupertribussesView : UserControl
    {


        public SupertribussesView()
        {
            DataContext = new SupertribussesViewModel();

            InitializeComponent();
        }


    }
}