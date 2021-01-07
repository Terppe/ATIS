using System;


using System.Windows.Controls;


//  SuperclassesView.xaml.cs Skriptdatum:  07.01.2021  12:32     

namespace ATIS.Ui.Views.Database.D18Superclass
{

    /// <summary>
    /// Interactionslogic for SuperclassesView.xaml
    /// </summary>
    public partial class SuperclassesView : UserControl
    {


        public SuperclassesView()
        {
            DataContext = new SuperclassesViewModel();

            InitializeComponent();
        }


    }
}