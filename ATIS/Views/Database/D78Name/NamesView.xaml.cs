using System.Windows.Controls;


//  Tbl78NamesView.xaml.cs Skriptdatum:  22.01.2019  10:32     

namespace ATIS.Ui.Views.Database.D78Name
{

    /// <summary>
    /// Interactionslogic for NamesView.xaml
    /// </summary>
    public partial class NamesView : UserControl
    {


        public NamesView()
        {
            DataContext = new NamesViewModel();

            InitializeComponent();
        }


    }
}