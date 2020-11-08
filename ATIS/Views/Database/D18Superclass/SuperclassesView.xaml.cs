using System.Windows.Controls;

namespace ATIS.Ui.Views.Database.D18Superclass
{
    /// <summary>
    /// Interaktionslogik für SuperclassesView.xaml
    /// </summary>
    public partial class SuperclassesView : UserControl
    {
        public SuperclassesView()
        {
            DataContext = new SuperclassesViewModel();

            InitializeComponent();
        }
    }
}
