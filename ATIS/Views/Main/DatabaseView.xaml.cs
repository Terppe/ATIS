using System.Windows.Controls;

namespace ATIS.Ui.Views.Main
{
    /// <summary>
    /// Interaktionslogik für DatabaseView.xaml
    /// </summary>
    public partial class DatabaseView : UserControl
    {
        public DatabaseView()
        {
            DataContext = new DatabaseViewModel();

            InitializeComponent();
        }
    }
}
