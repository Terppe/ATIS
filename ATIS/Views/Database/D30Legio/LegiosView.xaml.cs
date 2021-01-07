using System;


using System.Windows.Controls;


//  LegiosView.xaml.cs Skriptdatum:  07.01.2021  10:32     

namespace ATIS.Ui.Views.Database.D30Legio
{

    /// <summary>
    /// Interactionslogic for LegiosView.xaml
    /// </summary>
    public partial class LegiosView : UserControl
    {


        public LegiosView()
        {
            DataContext = new LegiosViewModel();

            InitializeComponent();
        }


    }
}