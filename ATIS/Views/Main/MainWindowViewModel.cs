using System.Collections.ObjectModel;
using System.Windows.Input;
using ATIS.Ui.Helper;
using ATIS.Ui.Views.Database;
using ATIS.Ui.Views.Database.D03Regnum;
using ATIS.Ui.Views.Database.D06Phylum;

namespace ATIS.Ui.Views.Main
{
    public class MainWindowViewModel : ViewModelBase
    {

        public MainWindowViewModel()
        {
 
        }
        private RelayCommand _getByNameOrIdCommand;
        public ICommand GetByNameCommand => _getByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetByName(SearchName); });


        private void ExecuteGetByName(string searchName)
        {
            //      TabIndexDetail = 1;

            //PhylumsCollection.Clear();
            //DivisionsCollection.Clear();
            //SubphylumsCollection.Clear();
            //SubdivisionsCollection.Clear();
            //ReferencesCollection.Clear();
            //ReferenceExpertsCollection.Clear();
            //ReferenceSourcesCollection.Clear();
            //ReferenceAuthorsCollection.Clear();
            //CommentsCollection.Clear();

            //    RegnumsCollection = _extGet.SearchNameAndIdReturnCollection<Tbl03Regnum>(searchName, "regnum");
            //   RaisePropertyChanged("RegnumsCollection");
        }

        public string SearchName { get; set; }

    }
}
