using System;


using System.Windows.Controls;


//  SubordosView.xaml.cs Skriptdatum:  07.01.2021  10:32     

namespace ATIS.Ui.Views.Database.D36Subordo
{

    /// <summary>
    /// Interactionslogic for SubordosView.xaml
    /// </summary>
    public partial class SubordosView : UserControl
    {


        public SubordosView()
        {
            DataContext = new SubordosViewModel();

            InitializeComponent();
        }


    }
}