using System;


using System.Windows.Controls;


//  InfraordosView.xaml.cs Skriptdatum:  07.01.2021  10:32     

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