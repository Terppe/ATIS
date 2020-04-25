using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ATIS.Helper;
using ATIS.Views.Database;

namespace ATIS.Views.Main
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
