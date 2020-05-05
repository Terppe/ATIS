using System.Collections.ObjectModel;
using ATIS.Ui.Helper;
using ATIS.Ui.Views.Database;

namespace ATIS.Ui.Views.Main
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ViewModelBase> _settings;

        ///  ############# TreeView #############
        public ObservableCollection<ViewModelBase> Settings => _settings;


        public MainWindowViewModel()
        {
            _settings = new ObservableCollection<ViewModelBase>();

            Settings.Add(new HomeViewModel());
            Settings.Add(new DatabaseViewModel());
            Settings.Add(new Tbl06PhylumsViewModel());


        }

        public override string Name { get; }
    }
}
