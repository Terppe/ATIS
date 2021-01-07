using System.Windows.Controls;


//  ClassesView.xaml.cs Skriptdatum:  07.01.2021  18:32     

namespace ATIS.Ui.Views.Database.D21Class
{

    /// <summary>
    /// Interactionslogic for ClassesView.xaml
    /// </summary>
    public partial class ClassesView : UserControl
    {


        public ClassesView()
        {
            DataContext = new ClassesViewModel();

            InitializeComponent();
        }


    }
}