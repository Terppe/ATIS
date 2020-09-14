using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using MahApps.Metro.Controls.Dialogs;

namespace ATIS.Ui.Views.Main
{
    public class MainWindowViewModel : ViewModelBase, IDataErrorInfo, IDisposable
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly IDisposable _disposable;
        private readonly AtisDbContext _context = new AtisDbContext();

        public MainWindowViewModel(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;

        }

        //----------------Show Login Dialog via Vm ------------

        private RelayCommand _showLoginDialogCommand;
        public ICommand ShowLoginDialogCommand => _showLoginDialogCommand ??= new RelayCommand(delegate { ShowLoginDialog(); });

        public async void ShowLoginDialog()
        {
            // note that setting allows much additional functionality
            var settings = new LoginDialogSettings { NegativeButtonText = "Cancel", NegativeButtonVisibility = Visibility.Visible };

            await _dialogCoordinator.ShowLoginAsync(this, "Please Login", "Login from ViewModel. For Test Username = admin, Password = admin ", settings)
                .ContinueWith(t => HandleLoginClose(t.Result));
        }

        private async void HandleLoginClose(LoginDialogData tResult)
        {
            if (tResult != null)
            {
                var hashedPassword = Crypt.CalculateHash(tResult.Password, tResult.Username);
                var userData = _context.TblUserProfiles.SingleOrDefault(i => i.Email.Equals(tResult.Username) && i.Password.Equals(hashedPassword));
                if (tResult.Username == "admin" && tResult.Password == "admin")  //for testing with admin
                {
                    await _dialogCoordinator.ShowMessageAsync(this, "Administrator", tResult.Password);
                }
                else
                {
                    if (userData != null)
                    {
                        await _dialogCoordinator.ShowMessageAsync(this, userData.Role, "Sie sind eingeloggt");
                    }
                    else
                    {
                        await _dialogCoordinator.ShowMessageAsync(this, "Fehler bei der Eingabe", "E-Mail or Password");
                    }
                }
            }
            else
            {
                //  if (disposable != null) disposable.Dispose();  // Fehler
                Dispose();
            }
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
