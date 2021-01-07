using System;


using System.Windows.Controls;


//  SubclassesView.xaml.cs Skriptdatum:  07.01.2021  18:32     

namespace ATIS.Ui.Views.Database.D24Subclass
{

    /// <summary>
    /// Interactionslogic for SubclassesView.xaml
    /// </summary>
    public partial class SubclassesView : UserControl
    {


        public SubclassesView()
        {
            DataContext = new SubclassesViewModel();

            InitializeComponent();
        }


    }
}