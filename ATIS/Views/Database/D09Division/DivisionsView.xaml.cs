using System.Windows.Controls;

namespace ATIS.Ui.Views.Database.D09Division
{
    /// <summary>
    /// Interaktionslogik für DivisionsView.xaml
    /// </summary>
    public partial class DivisionsView : UserControl
    {
        public DivisionsView()
        {
            DataContext = new DivisionsViewModel();

            InitializeComponent();
        }

     }
}
