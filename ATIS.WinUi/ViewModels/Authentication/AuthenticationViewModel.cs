using System.Configuration;
using System.Text.RegularExpressions;
using System.Windows.Input;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using ATIS.WinUi.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using MimeKit;
using MailKit.Security;
using RelayCommand = CommunityToolkit.Mvvm.Input.RelayCommand;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace ATIS.WinUi.ViewModels.Authentication;
public interface IViewModel
{
}

public class AuthenticationViewModel : ObservableObject
{
    #region [Private Data Members]

    private readonly AtisDbContext _context = new();
    private readonly AllDialogs _allDialogs = new();

    private string _email = null!;
    private string _status = null!;
    private string _errormessage = null!;
    private string _authenticatedUser = null!;
    private string _passwordChar = null!;
    private string _passwordCharNew = null!;
    private string _passwordCharOld = null!;
    //   private readonly DelegateCommand _logoutCommand;

    #endregion [Private Data Members]

    #region [Constructor]

    public AuthenticationViewModel(AuthenticationService authenticationService)
    {
        //_authenticationService = authenticationService;
        //_logoutCommand = new DelegateCommand(Logout, CanLogout);
    }
    #endregion [Constructor]



    #region Commands
    public ICommand LoginCommand => new RelayCommand(execute: delegate { var task = Login(PasswordChar); });
    public ICommand RegisterCommand => new RelayCommand(execute: delegate { var task = Register(PasswordChar); });
    public ICommand GotFocusPasswordChangeCommand => new RelayCommand(execute: delegate { var task = GotFocusPasswordChange(PasswordCharOld); });
    public ICommand PasswordChangeCommand => new RelayCommand(execute: delegate { var task = PasswordChange(PasswordCharNew); });
    public ICommand PasswordForgotCommand => new RelayCommand(execute: delegate { var task = PasswordForgotWithEmail(); });
    //public ICommand LogoutCommand => new RelayCommand(execute: delegate { var task = Logout(null); });

    //public DelegateCommand LogoutCommand => _logoutCommand;

    #endregion


    #region Properties

    public string Email
    {
        get => _email;
        set
        {
            _email = value; OnPropertyChanged(nameof(Email));
        }
    }

    public string Status
    {
        get => _status;
        set
        {
            _status = value; OnPropertyChanged(nameof(Status));
        }
    }

    public string Errormessage
    {
        get => _errormessage;
        set
        {
            _errormessage = value; OnPropertyChanged(nameof(Errormessage));
        }
    }
    public string PasswordChar
    {
        get => _passwordChar;
        set
        {
            _passwordChar = value; OnPropertyChanged(nameof(PasswordChar));
        }
    }
    public string PasswordCharNew
    {
        get => _passwordCharNew;
        set
        {
            _passwordCharNew = value; OnPropertyChanged(nameof(PasswordCharNew));
        }
    }

    public string PasswordCharOld
    {
        get => _passwordCharOld;
        set
        {
            _passwordCharOld = value; OnPropertyChanged(nameof(PasswordCharOld));
        }
    }

    public string AuthenticatedUser
    {
        get
        {
            if (IsAuthenticated)
            {
                return string.Format("Signed in as {0}. {1}", Email,
                    Thread.CurrentPrincipal != null && Thread.CurrentPrincipal.IsInRole("Administrator") ? "You are an administrator!"
                        : "You are NOT a member of the administrators group.");
            }

            return "Not authenticated!";
        }
        set
        {
            _authenticatedUser = value; OnPropertyChanged();
        }

    }

    #endregion

    public static bool IsAuthenticated => Thread.CurrentPrincipal != null && Thread.CurrentPrincipal.Identity!.IsAuthenticated;

    private Task Login(string parameter)
    {
        if (parameter == null)
        {
            return Task.CompletedTask;
        }

        var clearTextPassword = parameter;
        Errormessage = "";
        Status = "";

        if (Email == null)
        {
            Errormessage = "Email is a required field.";
        }
        else if (!Regex.IsMatch(Email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
        {
            Errormessage = "Email must formated like so; xxx@xxx.xxx";
        }
        else
        {
            try
            {
                var authenticationService = new AuthenticationService();
                //Validate credentials through the authentication service
                var user = authenticationService.AuthenticateUser(Email, clearTextPassword);

                //Get the current principal object

                if (Thread.CurrentPrincipal is not CustomPrincipal customPrincipal)
                {
                    throw new ArgumentException(
                        "The application's default thread principal must be set to a CustomPrincipal object on startup.");
                }

                //Authenticate the user
                customPrincipal.Identity = new CustomIdentity(user.Email, user.Role);


                //Update UI
                OnPropertyChanged(nameof(AuthenticatedUser));
                OnPropertyChanged(nameof(IsAuthenticated));

                Email = string.Empty; //reset
                PasswordChar = string.Empty; //reset
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

        return Task.CompletedTask;
    }
    private async Task Register(string parameter)
    {
        if (parameter == null)
        {
            return;
        }

        {
            var clearTextPassword = parameter;
            Errormessage = "";
            Status = "";

            if (Email == null)
            {
                Errormessage = "Email is a required field.";
            }
            else if (!Regex.IsMatch(Email,
                @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                Errormessage = "Email must formated like so; xxx@xxx.xxx";
            }
            else
            {
                try
                {
                    if (_context.TblUserProfiles != null)
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

                            _context.TblUserProfiles.Add(userprofile);

                            _context.SaveChanges();

                            await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(Email);

                            await Login(parameter);   //Login

                            await _allDialogs.LogInSuccessInfoMessageDialogAsync();
                        }
                        else
                        {
                            await AllDialogs.DatasetExistInfoMessageDialogAsync();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Status = string.Format("ERROR: {0}", ex.Message);
                }
            }
        }
    }
    //private bool CanLogin(object parameter)
    //{
    //    return !IsAuthenticated;
    //}

    //private void Logout(object o)
    //{
    //    if (Thread.CurrentPrincipal as CustomPrincipal is { } customPrincipal)
    //    {
    //        customPrincipal.Identity = new AnonymousIdentity();
    //        OnPropertyChanged(nameof(AuthenticatedUser));
    //        OnPropertyChanged(nameof(IsAuthenticated));
    //        Status = string.Empty;
    //    }
    //    //  return Task.CompletedTask;
    //}

    //private bool CanLogout(object parameter)
    //{
    //    return IsAuthenticated;
    //}

    //private bool CanRegister(object parameter)
    //{
    //    return !IsAuthenticated;
    //}
    public static string GeneratePassword(int passLength)
    {
        var chars = "abcdefghijklmnopqrstuvwxyz@#$&ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var result = new string(
            Enumerable.Repeat(chars, passLength)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
        return result;
    }

    private async Task PasswordForgotWithEmail()
    {
        Errormessage = "";
        Status = "";

        var clearTextPasswordNew = GeneratePassword(8);

        if (Email == null)
        {
            Errormessage = "Email is a required field.";
        }
        else if (!Regex.IsMatch(Email,
                     @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
        {
            Errormessage = "Email must formated like so; xxx@xxx.xxx";
        }
        else
        {
            //---------------save
            try
            {
                if (_context.TblUserProfiles != null)
                {
                    var userprofile = _context.TblUserProfiles.FirstOrDefault(i => i.Email == Email);

                    if (userprofile != null)  //update
                    {
                        {
                            userprofile.Password = Crypt.CalculateHash(clearTextPasswordNew, Email);
                            userprofile.Writer = Environment.UserName;
                            userprofile.WriterDate = DateTime.Now;
                            userprofile.Updater = Environment.UserName;
                            userprofile.UpdaterDate = DateTime.Now;
                        }
                        if (!await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(Email))
                        {
                            return;
                        }

                        _context.TblUserProfiles.Update(userprofile);

                        _context.SaveChanges();

                        await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(Email);
                    }
                    else
                    {
                        await AllDialogs.DatasetNotExistInfoMessageDialogAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Status = string.Format("ERROR: {0}", ex.Message);
            }

            //---------------mail
            try
            {
                //for Provider 1&1
                var smtpServer = ConfigurationManager.AppSettings["SmtpHost"];
                var smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
                var imapServer = ConfigurationManager.AppSettings["ImapHost"];
                var imapPort = Convert.ToInt32(ConfigurationManager.AppSettings["ImapPort"]);
                var email = ConfigurationManager.AppSettings["Sender"];


                //var smtpServer = "smtp.1und1.de";
                //var smtpPort = 587;  //465
                //var imapServer = "imap.1und1.de";
                //var imapPort = 993;
                //string email = "rudolf@terppe.de";
                //string password = "$MeineFh@M0424%";

                //encrypt   fc+vbItFfOezE5aiz5uzZy43xAB/v+AWvPd/r8VLwE8=
                //          var encryptMailAccountPasswordTest = Crypt.Encrypt("$MeineFh@M0424%");


                var encryptMailAccountPassword = ConfigurationManager.AppSettings.Get("EncryptMailAccountPassword");
                if (encryptMailAccountPassword != null)
                {
                    var decryptMailAccountPassword = Crypt.Decrypt(encryptMailAccountPassword);
                    var password = decryptMailAccountPassword;

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Hotline T-Soft", email)); //Deine E-Mailadresse, von der gesendet werden soll, entspricht oftmals deinem Benutzernamen
                    message.To.Add(new MailboxAddress("User", Email));
                    message.Subject = "Forgot Password";
                    message.Body = new TextPart("plain")
                    {
                        Text = @"Your new generated password is " + " " + clearTextPasswordNew
                    };

                    var smtpClient = new SmtpClient();

                    try
                    {
                        if (smtpServer != null)
                        {
                            if (email != null)
                            {
                                SendEmail(smtpClient, message, smtpServer, smtpPort, email, password);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Errormessage = "Error: " + ex.Message;
                        Errormessage = "Stack: " + ex.StackTrace;
                    }
                    finally
                    {
                        smtpClient.Disconnect(true);
                        smtpClient.Dispose();
                    }
                }

                //var imapClient = new ImapClient();

                //try
                //{
                //	RetrieveInbox(imapClient, imapServer, imapPort, email, password);
                //}
                //catch (Exception ex)
                //{
                //Errormessage = "Error: " + ex.Message;
                //Errormessage = "Stack: " + ex.StackTrace;
                //	Console.WriteLine(ex.Message);
                //	Console.WriteLine(ex.StackTrace);
                //}
                //finally
                //{
                //	imapClient.Disconnect(true);
                //	imapClient.Dispose();
                //}					
            }
            catch (Exception ex)
            {
                Errormessage = "Error: " + ex.Message;
            }
        }
    }

    private static void SendEmail(SmtpClient client, MimeMessage message, string server, int port, string email, string password)
    {
        client.ServerCertificateValidationCallback = (s, c, ch, e) => true;
        client.Connect(server, port, SecureSocketOptions.StartTls);

        // Note: only needed if the SMTP server requires authentication
        client.Authenticate(email, password);
        client.Send(message);
    }

    //private static void RetrieveInbox(ImapClient client, string server, int port, string email, string password)
    //{
    //    client.ServerCertificateValidationCallback = (s, c, ch, e) => true;
    //    client.Connect(server, port, true);
    //    client.Authenticate(email, password);

    //    var inbox = client.Inbox;
    //    inbox.Open(FolderAccess.ReadOnly);

    //    //Console.WriteLine("Total messages: {0}", inbox.Count);
    //    //Console.WriteLine("Recent messages: {0}", inbox.Recent);

    //    for (var i = 0; i < inbox.Count; i++)
    //    {
    //        var message = inbox.GetMessage(i);
    //        //Console.WriteLine("Subject: {0}", message.Subject);
    //    }
    //}

    private async Task GotFocusPasswordChange(string parameter)
    {
        if (parameter == null)
        {
            return;
        }

        {
            var clearTextPasswordOld = parameter;

            Errormessage = "";
            Status = "";

            if (Email == null)
            {
                Errormessage = "Email is a required field.";
            }
            else if (!Regex.IsMatch(Email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                Errormessage = "Email must formated like so; xxx@xxx.xxx";
            }
            else
            {
                try
                {
                    var crypPassword = Crypt.CalculateHash(clearTextPasswordOld, Email);

                    if (_context.TblUserProfiles != null)
                    {
                        var userprofile = _context.TblUserProfiles.FirstOrDefault(i => i.Email == Email && i.Password == crypPassword);

                        if (userprofile == null)
                        {
                            await AllDialogs.DatasetNotExistInfoMessageDialogAsync();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Status = string.Format("ERROR: {0}", ex.Message);
                }
            }
        }
    }

    private async Task PasswordChange(string parameter)
    {
        if (parameter == null)
        {
            return;
        }

        {
            var clearTextPasswordNew = parameter;
            Errormessage = "";
            Status = "";

            if (Email == null)
            {
                Errormessage = "Email is a required field.";
            }
            else if (!Regex.IsMatch(Email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                Errormessage = "Email must formated like so; xxx@xxx.xxx";
            }
            else
            {
                try
                {
                    if (_context.TblUserProfiles != null)
                    {
                        var userprofile = _context.TblUserProfiles.FirstOrDefault(i => i.Email == Email);

                        if (userprofile != null)  //update
                        {
                            {
                                userprofile.Password = Crypt.CalculateHash(clearTextPasswordNew, Email);
                                userprofile.Writer = Environment.UserName;
                                userprofile.WriterDate = DateTime.Now;
                                userprofile.Updater = Environment.UserName;
                                userprofile.UpdaterDate = DateTime.Now;
                            }


                            if (!await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(Email))
                            {
                                return;
                            }

                            _context.TblUserProfiles.Update(userprofile);

                            _context.SaveChanges();


                            await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(Email);
                        }
                        else
                        {
                            await AllDialogs.DatasetNotExistInfoMessageDialogAsync();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Status = string.Format("ERROR: {0}", ex.Message);
                }
            }
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

    public AuthenticationViewModel()
    {

    }
}
