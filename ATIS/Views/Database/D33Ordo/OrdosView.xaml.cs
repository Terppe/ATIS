using System;


using System.Windows.Controls;


//  OrdosView.xaml.cs Skriptdatum:  07.01.2021  10:32     

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