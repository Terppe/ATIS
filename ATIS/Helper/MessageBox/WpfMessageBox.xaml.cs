using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ATIS.Ui.Helper.MessageBox
{
    /// <summary>
    /// Interaktionslogik für WpfMessageBox.xaml
    /// </summary>
    public partial class WpfMessageBox : Window
    {
        private WpfMessageBox()
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
        }
        static WpfMessageBox _messageBox;
        static MessageBoxResult _result = MessageBoxResult.No;
        public static MessageBoxResult Show
        (string caption, string msg, MessageBoxType type)
        {
            switch (type)
            {
                case MessageBoxType.ConfirmationWithYesNo:
                    return Show(caption, msg, MessageBoxButton.YesNo,
                    System.Windows.MessageBoxImage.Question);
                case MessageBoxType.ConfirmationWithYesNoCancel:
                    return Show(caption, msg, MessageBoxButton.YesNoCancel,
                    System.Windows.MessageBoxImage.Question);
                case MessageBoxType.Information:
                    return Show(caption, msg, MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Information);
                case MessageBoxType.Error:
                    return Show(caption, msg, MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Error);
                case MessageBoxType.Warning:
                    return Show(caption, msg, MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Warning);
                default:
                    return MessageBoxResult.No;
            }
        }
        public static MessageBoxResult Show(string msg, MessageBoxType type)
        {
            return Show(string.Empty, msg, type);
        }
        public static MessageBoxResult Show(string msg)
        {
            return Show(string.Empty, msg,
            MessageBoxButton.OK, System.Windows.MessageBoxImage.None);
        }
        public static MessageBoxResult Show
            (string caption, string text, MessageBoxButton button)
        {
            return Show(caption, text,  MessageBoxButton.OK, System.Windows.MessageBoxImage.None);
        }
        public static MessageBoxResult Show
            (string caption, string text, MessageBoxButton button, MessageBoxImage error)
        {
            return Show(caption, text, button,  System.Windows.MessageBoxImage.None);
        }
        public static MessageBoxResult Show
        (string caption, string text,
        MessageBoxButton button, System.Windows.MessageBoxImage image)
        {
            _messageBox = new WpfMessageBox
            { TxtMsg = { Text = text }, MessageTitle = { Text = caption } };
            SetVisibilityOfButtons(button);
            SetImageOfMessageBox(image);
            _messageBox.ShowDialog();
            return _result;
        }
        private static void SetVisibilityOfButtons(MessageBoxButton button)
        {
            switch (button)
            {
                case MessageBoxButton.OK:
                    _messageBox.BtnCancel.Visibility = Visibility.Collapsed;
                    _messageBox.BtnNo.Visibility = Visibility.Collapsed;
                    _messageBox.BtnYes.Visibility = Visibility.Collapsed;
                    _messageBox.BtnOk.Focus();
                    break;
                case MessageBoxButton.OKCancel:
                    _messageBox.BtnNo.Visibility = Visibility.Collapsed;
                    _messageBox.BtnYes.Visibility = Visibility.Collapsed;
                    _messageBox.BtnCancel.Focus();
                    break;
                case MessageBoxButton.YesNo:
                    _messageBox.BtnOk.Visibility = Visibility.Collapsed;
                    _messageBox.BtnCancel.Visibility = Visibility.Collapsed;
                    _messageBox.BtnNo.Focus();
                    break;
                case MessageBoxButton.YesNoCancel:
                    _messageBox.BtnOk.Visibility = Visibility.Collapsed;
                    _messageBox.BtnCancel.Focus();
                    break;
                default:
                    break;
            }
        }
        private static void SetImageOfMessageBox(System.Windows.MessageBoxImage image)
        {
            switch (image)
            {
                case System.Windows.MessageBoxImage.Warning:
                    _messageBox.SetImage("Warning.png");
                    break;
                case System.Windows.MessageBoxImage.Question:
                    _messageBox.SetImage("Question.png");
                    break;
                case System.Windows.MessageBoxImage.Information:
                    _messageBox.SetImage("Information.png");
                    break;
                case System.Windows.MessageBoxImage.Error:
                    _messageBox.SetImage("Error.png");
                    break;
                default:
                    _messageBox.Img.Visibility = Visibility.Collapsed;
                    break;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Equals(sender, BtnOk))
                _result = MessageBoxResult.OK;
            else if (Equals(sender, BtnYes))
                _result = MessageBoxResult.Yes;
            else if (Equals(sender, BtnNo))
                _result = MessageBoxResult.No;
            else if (Equals(sender, BtnCancel))
                _result = MessageBoxResult.Cancel;
            else
                _result = MessageBoxResult.None;
            _messageBox.Close();
            _messageBox = null;
        }
        private void SetImage(string imageName)
        {
            var uri = $"./MsgBoxImages/{imageName}";
            var uriSource = new Uri(uri, UriKind.RelativeOrAbsolute);
            Img.Source = new BitmapImage(uriSource);
        }

    }
}


