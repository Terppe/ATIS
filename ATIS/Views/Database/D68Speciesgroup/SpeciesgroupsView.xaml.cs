using System;


using System.Windows.Controls;


//  SpeciesgroupsView.xaml.cs Skriptdatum:  07.01.2021  10:32     

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