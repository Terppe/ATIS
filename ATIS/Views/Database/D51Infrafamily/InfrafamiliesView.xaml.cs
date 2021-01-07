using System;


using System.Windows.Controls;


//  InfrafamiliesView.xaml.cs Skriptdatum:  07.01.2021  10:32     

namespace ATIS.Ui.Views.Database.D51Infrafamily
{

    /// <summary>
    /// Interactionslogic for InfrafamiliesView.xaml
    /// </summary>
    public partial class InfrafamiliesView : UserControl
    {


        public InfrafamiliesView()
        {
            DataContext = new InfrafamiliesViewModel();

            InitializeComponent();
        }


    }
}