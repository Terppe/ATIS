using System;


using System.Windows.Controls;


//  Tbl68SpeciesgroupsView.xaml.cs Skriptdatum:  15.12.2020  10:32     

namespace ATIS.Ui.Views.Database.D68Speciesgroup
{

    /// <summary>
    /// Interactionslogic for SpeciesgroupsView.xaml
    /// </summary>
    public partial class SpeciesgroupsView : UserControl
    {


        public SpeciesgroupsView()
        {
            DataContext = new SpeciesgroupsViewModel();

            InitializeComponent();
        }


    }
}