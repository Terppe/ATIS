using System.Windows.Controls;

namespace ATIS.Ui.Views.Database.D15Subdivision
{
    /// <summary>
    /// Interaktionslogik für SubdivisionsView.xaml
    /// </summary>
    public partial class SubdivisionsView : UserControl
    {
        public SubdivisionsView()
        {
            DataContext = new SubdivisionsViewModel();

            InitializeComponent();
        }
    }
}
