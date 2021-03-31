using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using ATIS.Ui.Views.Log;
using ATIS.Ui.Views.Search;
using MahApps.Metro.Controls.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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
        //     private readonly IDisposable _disposable;
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
        //  private SearchWindow _view;
        //  public ICommand RunQuickSearchCommand => new AnotherCommandImplementation(ExecuteSearchQuickDialog);

        private RelayCommand _runQuickSearchCommand;
        public ICommand RunQuickSearchCommand => _runQuickSearchCommand ??= new RelayCommand(delegate { ExecuteSearchQuickDialog(FilterText); });

        private void ExecuteSearchQuickDialog(string filterText)
        {
            var viewModel = new SearchQuickViewModel(filterText);
            var searchWindow = new SearchWindow(viewModel);
            //    searchWindow.TbSearch.Text = filterText;
            searchWindow.Show();
        }

        //   public ObservableCollection<Tbl66Genus> GenussesCollection { get; set; }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            MessageBox.Show(CultRes.StringsRes.Warning, CultRes.StringsRes.SearchBeClosed, MessageBoxButton.OK);
        }


        //--------------------------------LoginWindow------------------------------------------------------------
        private RelayCommand _loginWindowCommand;
        public ICommand LoginWindowCommand => _loginWindowCommand ??= new RelayCommand(delegate { OpenLoginWindow(); });

        //      public ICommand LoginWindowCommand => new DelegateCommand(OpenLoginWindow);

        private void OpenLoginWindow()
        {
            var viewModel = new AuthenticationViewModel(new AuthenticationService());
            IView loginWindow = new LoginWindow(viewModel);
            loginWindow.Show();
        }
        //--------------------------------RegisterWindow------------------------------------------------------------
        private RelayCommand _registerWindowCommand;
        public ICommand RegisterWindowCommand => _registerWindowCommand ??= new RelayCommand(delegate { OpenRegisterWindow(); });

        //     public ICommand RegisterWindowCommand => new DelegateCommand(OpenRegisterWindow);

        private void OpenRegisterWindow()
        {
            var viewModel = new AuthenticationViewModel(new AuthenticationService());
            IView registerWindow = new RegisterWindow(viewModel);
            registerWindow.Show();
        }

        //--------------------------------PasswordForgotWindow------------------------------------------------------------
        private RelayCommand _passwordForgotWindowCommand;
        public ICommand PasswordForgotWindowCommand => _passwordForgotWindowCommand ??= new RelayCommand(delegate { OpenPasswordForgotWindow(); });

        //    public ICommand PasswordForgotWindowCommand => new DelegateCommand(OpenPasswordForgotWindow);

        private void OpenPasswordForgotWindow()
        {
            var viewModel = new AuthenticationViewModel(new AuthenticationService());
            IView passwordForgotWindow = new PasswordForgotWindow(viewModel);
            passwordForgotWindow.Show();
        }
        //--------------------------------PasswordChangeWindow------------------------------------------------------------
        private RelayCommand _passwordChangeWindowCommand;
        public ICommand PasswordChangeWindowCommand => _passwordChangeWindowCommand ??= new RelayCommand(delegate { OpenPasswordChangeWindow(); });

        //   public ICommand PasswordChangeWindowCommand => new DelegateCommand(OpenPasswordChangeWindow);

        private void OpenPasswordChangeWindow()
        {
            var viewModel = new AuthenticationViewModel(new AuthenticationService());
            IView passwordChangeWindow = new PasswordChangeWindow(viewModel);
            passwordChangeWindow.Show();
        }
        //------------------------------------------------------------------
        //----------------------------------------------------------------
        private RelayCommand _changeCulUsaCommand;
        public ICommand ChangeCulUsaCommand => _changeCulUsaCommand ??= new RelayCommand(delegate { ChangeCultureUsa(null); });

        private static void ChangeCultureUsa(object o)
        {
            App.ChangeLanguage("en-US");
        }
        //----------------------------------------------------------------
        private RelayCommand _changeCulFrenchCommand;
        public ICommand ChangeCulFrenchCommand => _changeCulFrenchCommand ??= new RelayCommand(delegate { ChangeCultureFrench(null); });

        private static void ChangeCultureFrench(object o)
        {
            App.ChangeLanguage("fr-FR");
        }
        //----------------------------------------------------------------
        private RelayCommand _changeCulGermanCommand;
        public ICommand ChangeCulGermanCommand => _changeCulGermanCommand ??= new RelayCommand(delegate { ChangeCultureGerman(null); });

        private static void ChangeCultureGerman(object o)
        {
            App.ChangeLanguage("de-DE");
        }
        //----------------------------------------------------------------
        private RelayCommand _changeCulPortuguiseCommand;
        public ICommand ChangeCulPortuguiseCommand => _changeCulPortuguiseCommand ??= new RelayCommand(delegate { ChangeCulturePortuguise(null); });


        private static void ChangeCulturePortuguise(object o)
        {
            App.ChangeLanguage("pt-PT");
        }

        //-----------------------------------------------------------
        private RelayCommand _changeCulSpainCommand;
        public ICommand ChangeCulSpainCommand => _changeCulSpainCommand ??= new RelayCommand(delegate { ChangeCultureSpain(null); });


        private static void ChangeCultureSpain(object o)
        {
            App.ChangeLanguage("sp-SP");
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
            //      if (_disposable != null) _disposable.Dispose();
        }


        public string Error { get; }

        public string this[string columnName] => throw new NotImplementedException();


    }
}
