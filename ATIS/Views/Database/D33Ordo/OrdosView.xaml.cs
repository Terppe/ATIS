using System;


using System.Windows.Controls;


//  Tbl33OrdosView.xaml.cs Skriptdatum:  10.12.2020  10:32     

namespace ATIS.Ui.Views.Database.D33Ordo
{

    /// <summary>
    /// Interactionslogic for OrdosView.xaml
    /// </summary>
    public partial class OrdosView : UserControl
    {


        public OrdosView()
        {
            DataContext = new OrdosViewModel();

            InitializeComponent();
        }


    }
}