using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using ATIS.Ui.Views.Log;
using ATIS.Ui.Views.Search;
using ControlzEx.Theming;
using MahApps.Metro.Controls.Dialogs;
using MaterialDesignThemes.Wpf;

namespace ATIS.Ui.Views.Main
{
    public class MainWindowViewModel : ViewModelBase, IDataErrorInfo, IDisposable
    {
        /// <summary>
        /// Interaktionslogik für LoginWindow.xaml
        /// </summary>
        public interface IView
        {
            IViewModel ViewModel
            {
                get;
                set;
            }
            void Show();
        }

        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly IDisposable _disposable;
        private readonly AtisDbContext _context = new AtisDbContext();
        private readonly SearchBasicGet _extSearchGet = new SearchBasicGet();

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

        //-----------------------Show Login Magnus Montin---------------------------------
        private RelayCommand _showLoginCommand;
        public ICommand ShowLoginCommand => _showLoginCommand ??= new RelayCommand(delegate { ShowLogin(); });

        public void ShowLogin()
        {
            //Show the login view
            var viewModel = new AuthenticationViewModel(new AuthenticationService());
            var loginWindow = new LoginWindow(viewModel);
            loginWindow.Show();
        }

        //-----------------------Show Register Magnus Montin---------------------------------
        private RelayCommand _showRegisterCommand;
        public ICommand ShowRegisterCommand => _showRegisterCommand ??= new RelayCommand(delegate { ShowRegister(); });

        public void ShowRegister()
        {
            //Show the register view
            var viewModel = new AuthenticationViewModel(new AuthenticationService());
            var registerWindow = new RegisterWindow(viewModel);
            registerWindow.Show();
        }

        //----------------------------SearchQuick------------------------------------------
        private SearchWindow _view;
        //  public ICommand RunQuickSearchCommand => new AnotherCommandImplementation(ExecuteSearchQuickDialog);

        private RelayCommand _runQuickSearchCommand;
        public ICommand RunQuickSearchCommand => _runQuickSearchCommand ??= new RelayCommand(delegate { ExecuteSearchQuickDialog(FilterText); });

        private void ExecuteSearchQuickDialog(string filterText)
        {
            var viewModel = new SearchQuickViewModel(filterText);
            var searchWindow = new SearchWindow(viewModel);
            searchWindow.TbSearch.Text = filterText;
            searchWindow.Show();
        }

        //   public ObservableCollection<Tbl66Genus> GenussesCollection { get; set; }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            MessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.SearchBeClosed, MessageBoxButton.OK);
        }


        //------------------------------------------------------------------
        //private RelayCommand _searchQuickByNameCommand;
        //public ICommand RunQuickSearchCommand => _searchQuickByNameCommand ??= new RelayCommand(delegate { InitSearchQuick(FilterText); });

        //private void InitSearchQuick(string filterText)
        //{
        //    if (string.IsNullOrEmpty(filterText)) return;

        //    GenussesCollection ??= new ObservableCollection<Tbl66Genus>();
        //    GenussesCollection = _extSearchGet.SearchFilterTextReturnCollection<Tbl66Genus>(filterText, "genus");
        //    RaisePropertyChanged("GenussesCollection");
        //}

        //----------------------------------------------------------------------

        //private RelayCommand _getByNameOrIdCommand;
        //public ICommand GetByNameCommand => _getByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetByName(FilterText); });


        //private void ExecuteGetByName(string searchName)
        //{
        //    //      TabIndexDetail = 1;

        //    //PhylumsCollection.Clear();
        //    //DivisionsCollection.Clear();
        //    //SubphylumsCollection.Clear();
        //    //SubdivisionsCollection.Clear();
        //    //ReferencesCollection.Clear();
        //    //ReferenceExpertsCollection.Clear();
        //    //ReferenceSourcesCollection.Clear();
        //    //ReferenceAuthorsCollection.Clear();
        //    //CommentsCollection.Clear();

        //    //    RegnumsCollection = _extGet.SearchNameAndIdReturnCollection<Tbl03Regnum>(searchName, "regnum");
        //    //   RaisePropertyChanged("RegnumsCollection");
        //}

        public string FilterText { get; set; }

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
