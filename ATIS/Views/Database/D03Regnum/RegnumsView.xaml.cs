using System.Windows.Controls;

namespace ATIS.Ui.Views.Database.D03Regnum
{
    /// <summary>
    /// Interaktionslogik für RegnumsView.xaml
    /// </summary>
    public partial class RegnumsView : UserControl
    {
        public RegnumsView()
        {
            DataContext = new RegnumsViewModel();

            InitializeComponent();
        }
    }
}
