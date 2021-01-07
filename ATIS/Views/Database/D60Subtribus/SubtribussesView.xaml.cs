using System;


using System.Windows.Controls;


//  SubtribussesView.xaml.cs Skriptdatum:  07.01.2021  10:32     

namespace ATIS.Ui.Views.Database.D60Subtribus
{

    /// <summary>
    /// Interactionslogic for SubtribussesView.xaml
    /// </summary>
    public partial class SubtribussesView : UserControl
    {


        public SubtribussesView()
        {
            DataContext = new SubtribussesViewModel();

            InitializeComponent();
        }


    }
}