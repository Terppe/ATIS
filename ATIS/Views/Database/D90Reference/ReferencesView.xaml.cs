using System;


using System.Windows.Controls;


//  ReferencesView.xaml.cs Skriptdatum:  03.02.2021  10:32     

namespace ATIS.Ui.Views.Database.D90Reference
{

    /// <summary>
    /// Interactionslogic for ReferencesView.xaml
    /// </summary>
    public partial class ReferencesView : UserControl
    {


        public ReferencesView()
        {
            DataContext = new ReferencesViewModel();

            InitializeComponent();
        }


    }
}