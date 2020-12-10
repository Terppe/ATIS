using System;


using System.Windows.Controls;


//  Tbl27InfraclassesView.xaml.cs Skriptdatum:  10.12.2020  18:32     

namespace ATIS.Ui.Views.Database.D27Infraclass
{

    /// <summary>
    /// Interactionslogic for InfraclassesView.xaml
    /// </summary>
    public partial class InfraclassesView : UserControl
    {


        public InfraclassesView()
        {
            DataContext = new InfraclassesViewModel();

            InitializeComponent();
        }


    }
}