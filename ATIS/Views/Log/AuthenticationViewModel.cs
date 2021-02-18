using ATIS.Ui.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using ATIS.Ui.Views.Main;
using System;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace ATIS.Ui.Views.Log
{
    public interface IViewModel { }


    public class AuthenticationViewModel : ViewModelBase, IViewModel, INotifyPropertyChanged
    {
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();

        private readonly AuthenticationService _authenticationService;
        private readonly DelegateCommand _loginCommand;
        private readonly DelegateCommand _closeCommand;
        private readonly DelegateCommand _logoutCommand;
        private readonly DelegateCommand _registerCommand;
        private readonly DelegateCommand _showViewCommand;

        private string _email;
        private string _status;
        private string _errormessage;

        public AuthenticationViewModel(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _loginCommand = new DelegateCommand(Login, CanLogin);
            _closeCommand = new DelegateCommand(ExecuteClose, null);
            _logoutCommand = new DelegateCommand(Logout, CanLogout);
            _registerCommand = new DelegateCommand(Register, CanRegister);
            _showViewCommand = new DelegateCommand(ShowView, null);
        }

        #region Properties
        public string Email
        {
            get => _email;
            set { _email = value; RaisePropertyChanged("Email"); }
        }

        public string Status
        {
            get => _status;
            set { _status = value; RaisePropertyChanged("Status"); }
        }

        public string Errormessage
        {
            get => _errormessage;
            set { _errormessage = value; RaisePropertyChanged("Errormessage"); }
        }

        public string AuthenticatedUser
        {
            get
            {
                if (IsAuthenticated)
                    return string.Format(CultRes.StringsRes.AuthUserQuestion,
                        Thread.CurrentPrincipal.Identity.Name,
                        Thread.CurrentPrincipal.IsInRole("Administrator") ? CultRes.StringsRes.AuthUserQuestion1
                            : CultRes.StringsRes.AuthUserQuestion2);

                return CultRes.StringsRes.AuthUserNot;
            }
        }

        #endregion

        #region Commands
        public DelegateCommand LoginCommand => _loginCommand;
        public DelegateCommand LogoutCommand => _logoutCommand;
        public DelegateCommand CloseCommand => _closeCommand;
        public DelegateCommand RegisterCommand => _registerCommand;
        public DelegateCommand ShowViewCommand => _showViewCommand;

        #endregion

        public bool IsAuthenticated => Thread.CurrentPrincipal.Identity.IsAuthenticated;

        private void Login(object parameter)
        {
            var passwordBox = (PasswordBox)parameter;
            if (passwordBox != null)
            {
                string clearTextPassword = passwordBox.Password;
                Errormessage = "";
                Status = "";

                if (Email == null)
                {
                    Errormessage = CultRes.StringsRes.RequiredEMail;
                }
                else if (!Regex.IsMatch(Email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                {
                    Errormessage = CultRes.StringsRes.RequiredEMail2;
                }
                else
                {
                    try
                    {
                        //Validate credentials through the authentication service
                        User user = _authenticationService.AuthenticateUser(Email, clearTextPassword);

                        //Get the current principal object
                        CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;

                        if (customPrincipal == null)
                            throw new ArgumentException(
                                "The application's default thread principal must be set to a CustomPrincipal object on startup.");

                        //Authenticate the user
                        customPrincipal.Identity = new CustomIdentity(user.Email, user.Role);

                        //Update UI
                        RaisePropertyChanged("AuthenticatedUser");
                        RaisePropertyChanged("IsAuthenticated");
                        _loginCommand.RaiseCanExecuteChanged();
                        _logoutCommand.RaiseCanExecuteChanged();
                        Email = string.Empty; //reset
                        passwordBox.Password = string.Empty; //reset
                        Status = string.Empty;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Status = "Login failed! Please provide some valid credentials.";
                    }
                    catch (Exception ex)
                    {
                        Status = string.Format("ERROR: {0}", ex.Message);
                    }
                }
            }
        }

        private bool CanLogin(object parameter)
        {
            return !IsAuthenticated;
        }

        public void ExecuteClose(object parameter)
        {
            var registerWindow = parameter as RegisterWindow;
            registerWindow?.Close();
        }

        private void Logout(object parameter)
        {
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal != null)
            {
                customPrincipal.Identity = new AnonymousIdentity();
                RaisePropertyChanged("AuthenticatedUser");
                RaisePropertyChanged("IsAuthenticated");
                _loginCommand.RaiseCanExecuteChanged();
                _logoutCommand.RaiseCanExecuteChanged();
                Status = string.Empty;
            }
        }

        private bool CanLogout(object parameter)
        {
            return IsAuthenticated;
        }

        private void Register(object parameter)
        {
            var passwordBox = (PasswordBox)parameter;
            if (passwordBox != null)
            {
                string clearTextPassword = passwordBox.Password;
                Errormessage = "";
                Status = "";

                if (Email == null)
                {
                    Errormessage = CultRes.StringsRes.RequiredEMail;
                }
                else if (!Regex.IsMatch(Email,
                    @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                {
                    Errormessage = CultRes.StringsRes.RequiredEMail2;
                }
                else
                {
                    try
                    {
                        var userprofile = _context.TblUserProfiles.SingleOrDefault(i => i.Email == Email);

                        if (userprofile == null)
                        {
                            userprofile = new TblUserProfile //add new
                            {
                                Email = Email,
                                CountId = RandomHelper.Randomnumber(),
                                Password = Crypt.CalculateHash(clearTextPassword, Email),
                                Role = "User",
                                StartDate = DateTime.Now,
                                EndDate = DateTime.Now,
                                Writer = Environment.UserName,
                                WriterDate = DateTime.Now,
                                Updater = Environment.UserName,
                                UpdaterDate = DateTime.Now,
                            };

                            _uow.TblUserProfiles.Add(userprofile);

                            _uow.Complete();

                            MessageBox.Show(CultRes.StringsRes.SaveSuccess, Email,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show(CultRes.StringsRes.DatasetExist, Email,
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        Status = string.Format("ERROR: {0}", ex.Message);
                    }
                }
            }
        }

        private bool CanRegister(object parameter)
        {
            return !IsAuthenticated;
        }

        private void ShowView(object parameter)
        {
            try
            {
                Status = string.Empty;
                MainWindowViewModel.IView view = null;
                switch (parameter)
                {
                    case "Login":
                        view = new LoginWindow();
                        break;
                    case "Register":
                        view = new RegisterWindow();
                        break;
                    case "Admin":
                        //           view = new AdminWindow();
                        break;
                    case "Secret":
                        //           view = new SecretWindow();
                        break;
                }

                view.Show();
            }
            catch (SecurityException)
            {
                Status = "You are not authorized!";
            }
        }


        #region INotifyPropertyChanged Members
        //public event PropertyChangedEventHandler PropertyChanged;

        //private void NotifyPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}
        #endregion
    }
}
