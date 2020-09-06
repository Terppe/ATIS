using System;
using System.ComponentModel;
using System.Windows.Input;
using ATIS.Ui.Helper;
using MahApps.Metro.Controls.Dialogs;

namespace ATIS.Ui.Views.Main
{
    public class MainWindowViewModel : ViewModelBase, IDataErrorInfo, IDisposable
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly IDisposable _disposable;

        public MainWindowViewModel(IDialogCoordinator dialogCoordinator)
        {

        }


        //--------------------Search -------------
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

        //----------------Close -----------------
        private bool _quitConfirmationEnabled;

        public bool QuitConfirmationEnabled
        {
            get => _quitConfirmationEnabled;
            //     set => this.Set(ref this._quitConfirmationEnabled, value);
            set => _quitConfirmationEnabled = value;
        }
        public void Dispose()
        {
            //  HotkeyManager.Current.Remove("demo");
            if (_disposable != null) _disposable.Dispose();
        }


        public string Error { get; }

        public string this[string columnName] => throw new NotImplementedException();


    }
}
