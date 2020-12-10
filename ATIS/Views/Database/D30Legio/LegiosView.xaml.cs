using System;


using System.Windows.Controls;


//  Tbl30LegiosView.xaml.cs Skriptdatum:  10.12.2020  10:32     

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