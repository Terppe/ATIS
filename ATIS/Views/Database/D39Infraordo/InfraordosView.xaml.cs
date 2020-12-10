using System;


using System.Windows.Controls;


//  Tbl39InfraordosView.xaml.cs Skriptdatum:  10.12.2020  10:32     

namespace ATIS.Ui.Views.Database.D39Infraordo
{

    /// <summary>
    /// Interactionslogic for InfraordosView.xaml
    /// </summary>
    public partial class InfraordosView : UserControl
    {


        public InfraordosView()
        {
            DataContext = new InfraordosViewModel();

            InitializeComponent();
        }


    }
}