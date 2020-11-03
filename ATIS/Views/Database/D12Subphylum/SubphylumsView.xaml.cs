using System;


using System.Windows.Controls;


//  Tbl12SubphylumsView.xaml.cs Skriptdatum:  30.10.2020  12:32     

namespace ATIS.Ui.Views.Database.D12Subphylum
{

    /// <summary>
    /// Interactionslogic for SubphylumsView.xaml
    /// </summary>
    public partial class SubphylumsView : UserControl
    {


        public SubphylumsView()
        {
            DataContext = new SubphylumsViewModel();

            InitializeComponent();
        }


    }
}