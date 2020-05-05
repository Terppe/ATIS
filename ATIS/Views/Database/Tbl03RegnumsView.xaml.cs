using System.Windows.Controls;

namespace ATIS.Ui.Views.Database
{
    /// <summary>
    /// Interaktionslogik für Tbl03RegnumsView.xaml
    /// </summary>
    public partial class Tbl03RegnumsView : UserControl
    {
        public Tbl03RegnumsView()
        {
            DataContext = new Tbl03RegnumsViewModel();

            InitializeComponent();
        }
    }
}
