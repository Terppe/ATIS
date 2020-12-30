using System;


using System.Windows.Controls;


//  Tbl84SynonymsView.xaml.cs Skriptdatum:  29.12.2020  10:32     

namespace ATIS.Ui.Views.Database.D84Synonym
{

    /// <summary>
    /// Interactionslogic for SynonymsView.xaml
    /// </summary>
    public partial class SynonymsView : UserControl
    {


        public SynonymsView()
        {
            DataContext = new SynonymsViewModel();

            InitializeComponent();
        }


    }
}