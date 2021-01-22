using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Media;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ATIS.Ui.Helper.MsgBoxes
{
    // The MessageBoxEx class we getting kinda crowded, so I took advantage of the fact that it's a 
    // partial class, and segregated the static from the non-static. The code looks less chatotic 
    // and is easier to maintain as a result.

    /// <summary>
    /// Non-static interaction logic for MessageBoxEx.xaml
    /// </summary>
    public   partial class MessageBoxEx : Window, INotifyPropertyChanged
	{
		#region INotifyPropertyChanged

		private bool _isModified = false;
		public bool IsModified
		{
			get => _isModified;
			set { if (value == _isModified) return; _isModified = true; NotifyPropertyChanged(); }
		}
		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Notifies that the property changed, and sets IsModified to true.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
				if (propertyName != "IsModified")
				{
					IsModified = true;
				}
			}
		}

		#endregion INotifyPropertyChanged

		private const string DefaultCaption = "Application Message";

		#region fields

		private double _screenHeight;
		private string _message;
		private string _messageTitle;
		private MessageBoxButton _buttons;
		private MessageBoxResult _messageResult;
		private ImageSource _messageIcon;
		private MessageBoxImage _msgBoxImage;
		private double _buttonWidth = 0d;
		private bool _expanded = false;

		#endregion fields

		#region properties

		/// <summary>
		/// Get/set the screen's work area height
		/// </summary>
		public double ScreenHeight
		{
			get => _screenHeight;
			set { if (value == _screenHeight) return; _screenHeight = value; NotifyPropertyChanged(); }
		}
		/// <summary>
		/// Get/set the message text
		/// </summary>
		public string Message
		{
			get => _message;
			set { if (value != _message) { _message = value; NotifyPropertyChanged(); } }
		}
		/// <summary>
		/// Get/set the form caption 
		/// </summary>
		public string MessageTitle
		{
			get => _messageTitle;
			set { if (value != _messageTitle) { _messageTitle = value; NotifyPropertyChanged(); } }
		}
		/// <summary>
		/// Get/set the message box result (which button was pressed to dismiss the form)
		/// </summary>
		public MessageBoxResult MessageResult
		{
			get => _messageResult;
			set => _messageResult = value;
		}
		/// <summary>
		/// Get/set the buttons ued in the form (and update visibility for them)
		/// </summary>
		public MessageBoxButton Buttons
		{
			get => _buttons;
			set
			{
				if (value == _buttons) return;
				_buttons = value;
				NotifyPropertyChanged();
				NotifyPropertyChanged("ShowOk");
				NotifyPropertyChanged("ShowCancel");
				NotifyPropertyChanged("ShowYes");
				NotifyPropertyChanged("ShowNo");
			}
		}
		/// <summary>
		/// Get the visibility of the ok button
		/// </summary>
		public Visibility ShowOk => Buttons == MessageBoxButton.OK || Buttons == MessageBoxButton.OKCancel ? Visibility.Visible : Visibility.Collapsed;

		/// <summary>
		/// Get the visibility of the cancel button
		/// </summary>
		public Visibility ShowCancel => Buttons == MessageBoxButton.OKCancel || Buttons == MessageBoxButton.YesNoCancel ? Visibility.Visible : Visibility.Collapsed;

		/// <summary>
		/// Get the visibility of the yes button
		/// </summary>
		public Visibility ShowYes => Buttons == MessageBoxButton.YesNo || Buttons == MessageBoxButton.YesNoCancel ? Visibility.Visible : Visibility.Collapsed;

		/// <summary>
		/// Get the visibility of the no button
		/// </summary>
		public Visibility ShowNo => Buttons == MessageBoxButton.YesNo || Buttons == MessageBoxButton.YesNoCancel ? Visibility.Visible : Visibility.Collapsed;

		/// <summary>
		/// Get this visibility of the message box icon
		/// </summary>
		public Visibility ShowIcon => MessageIcon != null ? Visibility.Visible : Visibility.Collapsed;

		/// <summary>
		/// Get/set the icon specified by the user
		/// </summary>
		public ImageSource MessageIcon
		{
			get => _messageIcon;
			set { if (value != _messageIcon) { _messageIcon = value; NotifyPropertyChanged(); } }
		}
		/// <summary>
		/// Get/set the width of the largest button (so all buttons are the same width as the widest button)
		/// </summary>
		public double ButtonWidth
		{
			get => _buttonWidth;
			set { if (value != _buttonWidth) { _buttonWidth = value; NotifyPropertyChanged(); } }
		}
		/// <summary>
		/// Get/set the flag inicating whether the expander is expanded
		/// </summary>
		public bool Expanded
		{
			get => _expanded;
			set { if (value != _expanded) { _expanded = value; NotifyPropertyChanged(); } }
		}

		#endregion properties

		#region constructors

		/// <summary>
		/// Default constructor for VS designer)
		/// </summary>
		private MessageBoxEx()
		{
			InitializeComponent();
			DataContext = this;
			LargestButtonWidth();
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="title"></param>
		/// <param name="buttons">(Optinal) Message box button(s) to be displayed (default = OK)</param>
		/// <param name="image">(Optional) Message box image to display (default = None)</param>
		public MessageBoxEx(string msg, string title, MessageBoxButton buttons = MessageBoxButton.OK, MessageBoxImage image = MessageBoxImage.None)
		{
			InitializeComponent();
			DataContext = this;
			LargestButtonWidth();
			Init(msg, title, buttons, image);
		}

		#endregion constructors

		#region non-static methods

		/// <summary>
		/// Performs message box initialization
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="title">Window title</param>
		/// <param name="buttons">What buttons are to be displayed</param>
		/// <param name="image">What message box icon image is to be displayed</param>
        private void Init(string msg, string title, MessageBoxButton buttons, MessageBoxImage image)
		{
			ShowDetailsBtn = (string.IsNullOrEmpty(DetailsText)) ? Visibility.Collapsed : Visibility.Visible;
			ShowCheckBox = (CheckBoxData == null) ? Visibility.Collapsed : Visibility.Visible;

			// determine the screen area height, and the height of the textblock
			ScreenHeight = SystemParameters.WorkArea.Height - 150;

			// configure the form based on specified criteria
			Message = msg;
			MessageTitle = (string.IsNullOrEmpty(title.Trim())) ? DefaultCaption : title;
			Buttons = buttons;
			//if (ParentWindow != null)
			//{
			//	this.FontFamily = ParentWindow.FontFamily;
			//	this.FontSize = ParentWindow.FontSize;
			//}
			// set the button template (if specified)
			if (!string.IsNullOrEmpty(ButtonTemplateName))
			{
				BtnOk.SetResourceReference(TemplateProperty, ButtonTemplateName);
				BtnYes.SetResourceReference(TemplateProperty, ButtonTemplateName);
				BtnNo.SetResourceReference(TemplateProperty, ButtonTemplateName);
				BtnCancel.SetResourceReference(TemplateProperty, ButtonTemplateName);
			}

			// set the form's colors (you can also set these colors in your program's startup code 
			// (either in app.xaml.cs or MainWindow.cs) before you use the MessageBox for the 
			// first time
			MessageBackground = (MessageBackground == null) ? new SolidColorBrush(Colors.White) : MessageBackground;
			MessageForeground = (MessageForeground == null) ? new SolidColorBrush(Colors.Black) : MessageForeground;
			ButtonBackground = (ButtonBackground == null) ? new SolidColorBrush(ColorFromString("#cdcdcd")) : ButtonBackground;

			MessageIcon = null;

			// 2020.01.03/jms - added support for messagebox icons
			// some of the message box images have duplicate ordinal values but present the same 
			// image, so we have to normalize these values before we can determine which image 
			// to display.
			image = (image == MessageBoxImage.Stop || image == MessageBoxImage.Hand) ? MessageBoxImage.Error : image;
			image = (image == MessageBoxImage.Asterisk) ? MessageBoxImage.Information : image;
			image = (image == MessageBoxImage.Exclamation) ? MessageBoxImage.Warning : image;

			// svae it so we can use it later
			_msgBoxImage = image;
			ImgMsgBoxIcon.Tag = (image == MessageBoxImage.Error) ? 1 : 0;
			if (image == MessageBoxImage.Error)
			{
				Style style = (Style)(FindResource("ImageOpacityChanger"));
				if (style != null)
				{
					ImgMsgBoxIcon.Style = style;
					ToolTip tooltip = new ToolTip() { Content = "Click this icon for additional info/actions." };
					// for some reason, Image elements can't do tooltips, so I assign the tootip 
					// to the parent grid. This seems to work fine.
					ImgGrid.ToolTip = tooltip;
				}
			}

			// now that the image is normalized, we can see what the user actually wants. We can 
			// also play the appropriate sound based on which icon is specified. The sound names 
			// don't exactly align with the icon (which I find quite strange). Welcome to Windows.
			switch (image)
			{
				case MessageBoxImage.Error:
					MessageIcon = GetIcon(SystemIcons.Error);
					if (!_isSilent) { SystemSounds.Hand.Play(); }
					break;

				case MessageBoxImage.Information:
					MessageIcon = GetIcon(SystemIcons.Information);
					if (!_isSilent) { SystemSounds.Asterisk.Play(); }
					break;

				case MessageBoxImage.Question:
					MessageIcon = GetIcon(SystemIcons.Question);
					if (!_isSilent) { SystemSounds.Question.Play(); }
					break;
				case MessageBoxImage.Warning:
					MessageIcon = GetIcon(SystemIcons.Warning);
					if (!_isSilent) { SystemSounds.Exclamation.Play(); }
					break;
				default:
					MessageIcon = null;
					break;
			}
		}

		/// <summary>
		/// Converts the specified icon into a WPF-comptible ImageSource object.
		/// </summary>
		/// <param name="icon"></param>
		/// <returns>An ImageSource object that represents the specified icon.</returns>
		public ImageSource GetIcon(Icon icon)
		{
			var image = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
			return image;
		}

		// The form is rendered and position BEFORE the SizeToContent property takes effect, 
		// so we have to take stepts to re-center it after the size changes. This code takes care 
		// of the re-positioning, and is called from the SizeChanged event handler.
		/// <summary>
		/// Center the form on the screen.
		/// </summary>
        private void CenterInScreen()
		{
			double width = ActualWidth;
			double height = ActualHeight;
			Left = (SystemParameters.WorkArea.Width - width) / 2 + SystemParameters.WorkArea.Left;
			Top = (SystemParameters.WorkArea.Height - height) / 2 + SystemParameters.WorkArea.Top;
		}

		/// <summary>
		/// Calculate the width of the largest button.
		/// </summary>
        private void LargestButtonWidth()
		{
			// we base the width on the width of the content. This allows us to avoid the problems 
			// with button width/actualwidth properties, especially when a given button is 
			// Collapsed.
			Typeface typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);

			StackPanel panel = (StackPanel)StackButtons.Child;
			double width = 0;
			string largestName = string.Empty;
			foreach (Button button in panel.Children)
			{
				// Because we have a details button with a polygon, we have to wrangle the "content" 
				// a little differently than the rest of the buttons. using the FormattedText object 
				// will strip whitespace before measuring the text, so we convert spaces to double 
				// hyphens to compensate (I like to pad button Content with a leading and trailing 
				// space) so that the button is wide enough to present a more padded appearance.
				FormattedText formattedText = new FormattedText((button.Name == "btnDetails") ? "--Details--" : ((string)(button.Content)).Replace(" ", "--"),
																CultureInfo.CurrentUICulture,
																FlowDirection.LeftToRight,
																typeface, FontSize = FontSize, System.Windows.Media.Brushes.Black,
																VisualTreeHelper.GetDpi(this).PixelsPerDip);
				if (width < formattedText.Width)
				{
					largestName = button.Name;
				}
				width = Math.Max(width, formattedText.Width);
			}
			ButtonWidth = Math.Ceiling(width/*width + polyArrow.Width+polyArrow.Margin.Right+Margin.Left*/);
		}

		#endregion non-static methods

		////////////////////////////////////////////////////////////////////////////////////////////
		// Form events
		////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Handle the click event for the OK button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnOK_Click(object sender, RoutedEventArgs e)
		{
			MessageResult = MessageBoxResult.OK;
			DialogResult = true;
		}

		/// <summary>
		/// Handle the click event for the Yes button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnYes_Click(object sender, RoutedEventArgs e)
		{
			MessageResult = MessageBoxResult.Yes;
			DialogResult = true;
		}

		/// <summary>
		/// Handle the click event for the No button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnNo_Click(object sender, RoutedEventArgs e)
		{
			MessageResult = MessageBoxResult.No;
			DialogResult = true;
		}

		/// <summary>
		/// Handle the click event for the Cancel button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnCancel_Click(object sender, RoutedEventArgs e)
		{
			MessageResult = MessageBoxResult.Cancel;
			DialogResult = true;
		}

		/// <summary>
		/// Handle the size changed event so we can re-center the form on the screen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NotifiableWindow_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			// we have to do this because the SizeToContent property is evaluated AFTER the window 
			// is positioned.
			CenterInScreen();
		}

		/// <summary>
		/// Handle the window loaded event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// if this in an error message box, this tooltip will be displayed. The intent is to set 
			// this value one time, and use it throughout the application session. However, you can 
			// certainly set it before displaying the messagebox to something that is contextually 
			// appropriate, but you'll have to clear it or reset it each time you use the MessageBox.
			ImgMsgBoxIcon.ToolTip = (_msgBoxImage == MessageBoxImage.Error) ? MsgBoxIconToolTip : null;
		}

		/// <summary>
		///  Handles the window closing event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_Closing(object sender, CancelEventArgs e)
		{
			// we always clear the details text and checkbox data. 
			DetailsText = null;
			CheckBoxData = null;

			// if the user didn't click a button to close the form, we set the MessageResult to the 
			// most negative button value that was available.
			if (MessageResult == MessageBoxResult.None)
			{
				switch (Buttons)
				{
					case MessageBoxButton.OK: MessageResult = MessageBoxResult.OK; break;
					case MessageBoxButton.YesNoCancel:
					case MessageBoxButton.OKCancel: MessageResult = MessageBoxResult.Cancel; break;
					case MessageBoxButton.YesNo: MessageResult = MessageBoxResult.No; break;
				}
			}
		}

		/// <summary>
		/// Since an icon isn't a button, we have to look for the left-mouse-up event to know it's 
		/// been clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ImgMsgBoxIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			// we only want to allow the click if this is an error message, and the delegate 
			// object has been specified.
			if (DelegateObj != null && _msgBoxImage == MessageBoxImage.Error && Buttons == MessageBoxButton.OK)
			{
				MessageBoxResult result = DelegateObj.PerformAction(Message);
				//despite the result of the method, we close this message
				if (ExitAfterErrorAction)
				{
					DialogResult = true;
				}
			}
		}
	}

	/// <summary>
	/// Non-static interaction logic for MessageBoxEx.xaml
	/// </summary>
	public sealed partial class MessageBoxEx : Window, INotifyPropertyChanged
	{
		#region static fields

        private static bool _isSilent = false;

        #endregion static fields

		#region static properties

		/// <summary>
		/// Get/set the icon tooltip text
		/// </summary>
		private static string MsgBoxIconToolTip { get; set; }
		/// <summary>
		/// Get/set the external icon delegate object
		/// </summary>
        private static MsgBoxExDelegate DelegateObj { get; set; }
		/// <summary>
		/// Get/set the flag that indicates whether the parent messagebox is closed after the 
		/// external action is finished.
		/// </summary>
        private static bool ExitAfterErrorAction { get; set; }

		/// <summary>
		/// Get/set the parent content control
		/// </summary>
		public static ContentControl ParentWindow { get; set; }

        /// <summary>
        /// Get/set the button template name (for styling buttons)
        /// </summary>
        public static string ButtonTemplateName { get; set; }

        /// <summary>
        /// Get/set the brush for the message text background
        /// </summary>
        public static SolidColorBrush MessageBackground { get; set; }

        /// <summary>
        /// Get/set the brush for the message text foreground
        /// </summary>
        public static SolidColorBrush MessageForeground { get; set; }

        /// <summary>
        /// Get/set the brush for the button panel background
        /// </summary>
        public static SolidColorBrush ButtonBackground { get; set; }

        /// <summary>
        /// Get/set max form width
        /// </summary>
        public static double MaxFormWidth { get; set; } = 800;

        /// <summary>
        /// Get the visibility of the no button
        /// </summary>
        public static Visibility ShowDetailsBtn { get; set; } = Visibility.Collapsed;

        /// <summary>
        /// Get/set details text
        /// </summary>
        public static string DetailsText { get; set; }

        /// <summary>
        /// Get/set the visibility of the checkbox
        /// </summary>
        public static Visibility ShowCheckBox { get; set; } = Visibility.Collapsed;

        /// <summary>
        /// Get/set the checkbox data object
        /// </summary>
        public static MsgBoxExCheckBoxData CheckBoxData { get; set; } = null;

        #endregion static properties

		#region static show methods

		/// <summary>
		/// Does the work of actually opening the messagebox
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="title"></param>
		/// <param name="buttons"></param>
		/// <param name="image"></param>
		/// <returns></returns>
		private static MessageBoxResult OpenMessageBox(string msg, string title, MessageBoxButton buttons, MessageBoxImage image)
		{
			MessageBoxEx form = new MessageBoxEx(msg, title, buttons, image) { Owner = Application.Current.MainWindow };
			form.ShowDialog();
			return form.MessageResult;

		}

		/////////////////////////////////////// without icons

		/// <summary>
		/// Show a messagebox, using default caption and just the OK button
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult Show(string msg)
		{
			return OpenMessageBox(msg, DefaultCaption, MessageBoxButton.OK, MessageBoxImage.None);
		}

		/// <summary>
		/// Show a messagebox with the specified caption, and just the OK button
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="title">The messagebox caption</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult Show(string msg, string title)
		{
			title = (string.IsNullOrEmpty(title)) ? DefaultCaption : title;
			return OpenMessageBox(msg, title, MessageBoxButton.OK, MessageBoxImage.None);
		}

		/// <summary>
		/// Show a messagebox with the default caption and the specified buttons
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="buttons">The buttons to display</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult Show(string msg, MessageBoxButton buttons)
		{
			return OpenMessageBox(msg, DefaultCaption, buttons, MessageBoxImage.None);
		}

		/// <summary>
		/// Show a mesagebox with the specified caption and button(s)
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="title">The title for the message box</param>
		/// <param name="parentWindow">The parent window that supplies the font family/size</param>
		/// <param name="buttons">The buttons to display</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult Show(string msg, string title, MessageBoxButton buttons)
		{
			title = (string.IsNullOrEmpty(title)) ? DefaultCaption : title;
			return OpenMessageBox(msg, title, buttons, MessageBoxImage.None);
		}

		/////////////////////////////////////// with icons

		/// <summary>
		/// Show a messagebox, using default caption, the OK button, and the specified icon
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="image">The message box icon to diplay</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult Show(string msg, MessageBoxImage image)
		{
			return OpenMessageBox(msg, DefaultCaption, MessageBoxButton.OK, image);
		}

		/// <summary>
		/// Show a messagebox with the specified caption, the OK button, and the specified icon
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="title">The messagebox caption</param>
		/// <param name="image">The message box icon to diplay</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult Show(string msg, string title, MessageBoxImage image)
		{
			title = (string.IsNullOrEmpty(title)) ? DefaultCaption : title;
			return OpenMessageBox(msg, title, MessageBoxButton.OK, image);
		}

		/// <summary>
		/// Show a messagebox with the default caption, the specified button(s), and the specified icon
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="buttons">The buttons to display</param>
		/// <param name="image">The message box icon to diplay</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult Show(string msg, MessageBoxButton buttons, MessageBoxImage image)
		{
			return OpenMessageBox(msg, DefaultCaption, buttons, image);
		}

		/// <summary>
		/// Show a mesagebox with the specified caption, the specified button(s), and the specified icon
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="title">The title for the message box</param>
		/// <param name="parentWindow">The parent window that supplies the font family/size</param>
		/// <param name="buttons">The buttons to display</param>
		/// <param name="image">The message box icon to diplay</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult Show(string msg, string title, MessageBoxButton buttons, MessageBoxImage image)
		{
			return OpenMessageBox(msg, title, buttons, image);
		}

		/////////////////////////////////////// details without icons

		/// <summary>
		/// Show a messagebox, using default caption and just the OK button
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="details">Amplifying message details (turns on "Details" expander)</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithDetails(string msg, string details)
		{
			DetailsText = details;
			return OpenMessageBox(msg, DefaultCaption, MessageBoxButton.OK, MessageBoxImage.None);
		}

		/// <summary>
		/// Show a messagebox with the specified caption, and just the OK button
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="details">Amplifying message details (turns on "Details" expander)</param>
		/// <param name="title">The messagebox caption</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithDetails(string msg, string details, string title)
		{
			DetailsText = details;
			title = (string.IsNullOrEmpty(title)) ? DefaultCaption : title;
			return OpenMessageBox(msg, title, MessageBoxButton.OK, MessageBoxImage.None);
		}

		/// <summary>
		/// Show a messagebox with the default caption and the specified buttons
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="details">Amplifying message details (turns on "Details" expander)</param>
		/// <param name="buttons">The buttons to display</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithDetails(string msg, string details, MessageBoxButton buttons)
		{
			DetailsText = details;
			return OpenMessageBox(msg, DefaultCaption, buttons, MessageBoxImage.None);
		}

		/// <summary>
		/// Show a mesagebox with the specified caption and button(s)
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="details">Amplifying message details (turns on "Details" expander)</param>
		/// <param name="title">The title for the message box</param>
		/// <param name="parentWindow">The parent window that supplies the font family/size</param>
		/// <param name="buttons">The buttons to display</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithDetails(string msg, string details, string title, MessageBoxButton buttons)
		{
			DetailsText = details;
			title = (string.IsNullOrEmpty(title)) ? DefaultCaption : title;
			return OpenMessageBox(msg, title, buttons, MessageBoxImage.None);
		}

		/////////////////////////////////////// details with icons

		/// <summary>
		/// Show a messagebox, using default caption, the OK button, and the specified icon
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="details">Amplifying message details (turns on "Details" expander)</param>
		/// <param name="image">The message box icon to diplay</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithDetails(string msg, string details, MessageBoxImage image)
		{
			DetailsText = details;
			return OpenMessageBox(msg, DefaultCaption, MessageBoxButton.OK, image);
		}

		/// <summary>
		/// Show a messagebox with the specified caption, the OK button, and the specified icon
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="details">Amplifying message details (turns on "Details" expander)</param>
		/// <param name="title">The messagebox caption</param>
		/// <param name="image">The message box icon to diplay</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithDetails(string msg, string details, string title, MessageBoxImage image)
		{
			DetailsText = details;
			title = (string.IsNullOrEmpty(title)) ? DefaultCaption : title;
			return OpenMessageBox(msg, title, MessageBoxButton.OK, image);
		}

		/// <summary>
		/// Show a messagebox with the default caption, the specified button(s), and the specified icon
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="details">Amplifying message details (turns on "Details" expander)</param>
		/// <param name="buttons">The buttons to display</param>
		/// <param name="image">The message box icon to diplay</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithDetails(string msg, string details, MessageBoxButton buttons, MessageBoxImage image)
		{
			DetailsText = details;
			return OpenMessageBox(msg, DefaultCaption, buttons, image);
		}

		/// <summary>
		/// Show a mesagebox with the specified caption, the specified button(s), and the specified icon
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="details">Amplifying message details (turns on "Details" expander)</param>
		/// <param name="title">The title for the message box</param>
		/// <param name="parentWindow">The parent window that supplies the font family/size</param>
		/// <param name="buttons">The buttons to display</param>
		/// <param name="image">The message box icon to diplay</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithDetails(string msg, string details, string title, MessageBoxButton buttons, MessageBoxImage image)
		{
			DetailsText = details;
			return OpenMessageBox(msg, title, buttons, image);
		}

		/////////////////////////////////////// checkbox without icons

		/// <summary>
		/// Show a messagebox, using default caption and just the OK button
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="data">Bindable data object</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithCheckBox(string msg, MsgBoxExCheckBoxData data)
		{
			MessageBoxResult result = MessageBoxResult.None;
			if (data != null)
			{
				CheckBoxData = data;
				result = OpenMessageBox(msg, DefaultCaption, MessageBoxButton.OK, MessageBoxImage.None);
			}
			return result;
		}

		/// <summary>
		/// Show a messagebox with the specified caption, and just the OK button
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="data">Bindable data object</param>
		/// <param name="title">The messagebox caption</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithCheckBox(string msg, MsgBoxExCheckBoxData data, string title)
		{
			MessageBoxResult result = MessageBoxResult.None;
			if (data != null)
			{
				CheckBoxData = data;
				title = (string.IsNullOrEmpty(title)) ? DefaultCaption : title;
				result = OpenMessageBox(msg, title, MessageBoxButton.OK, MessageBoxImage.None);
			}
			return result;
		}

		/// <summary>
		/// Show a messagebox with the default caption and the specified buttons
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="data">Bindable data object</param>
		/// <param name="buttons">The buttons to display</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithCheckBox(string msg, MsgBoxExCheckBoxData data, MessageBoxButton buttons)
		{
			MessageBoxResult result = MessageBoxResult.None;
			if (data != null)
			{
				CheckBoxData = data;
				result = OpenMessageBox(msg, DefaultCaption, buttons, MessageBoxImage.None);
			}
			return result;
		}

		/// <summary>
		/// Show a mesagebox with the specified caption and button(s)
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="data">Bindable data object</param>
		/// <param name="title">The title for the message box</param>
		/// <param name="parentWindow">The parent window that supplies the font family/size</param>
		/// <param name="buttons">The buttons to display</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithCheckBox(string msg, MsgBoxExCheckBoxData data, string title, MessageBoxButton buttons)
		{
			MessageBoxResult result = MessageBoxResult.None;
			if (data != null)
			{
				CheckBoxData = data;
				title = (string.IsNullOrEmpty(title)) ? DefaultCaption : title;
				result = OpenMessageBox(msg, title, buttons, MessageBoxImage.None);
			}
			return result;
		}

		/////////////////////////////////////// checkbox with icons

		/// <summary>
		/// Show a messagebox, using default caption and just the OK button
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="data">Bindable data object</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithCheckBox(string msg, MsgBoxExCheckBoxData data, MessageBoxImage image)
		{
			MessageBoxResult result = MessageBoxResult.None;
			if (data != null)
			{
				CheckBoxData = data;
				result = OpenMessageBox(msg, DefaultCaption, MessageBoxButton.OK, image);
			}
			return result;
		}

		/// <summary>
		/// Show a messagebox with the specified caption, and just the OK button
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="data">Bindable data object</param>
		/// <param name="title">The messagebox caption</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithCheckBox(string msg, MsgBoxExCheckBoxData data, string title, MessageBoxImage image)
		{
			MessageBoxResult result = MessageBoxResult.None;
			if (data != null)
			{
				CheckBoxData = data;
				title = (string.IsNullOrEmpty(title)) ? DefaultCaption : title;
				result = OpenMessageBox(msg, title, MessageBoxButton.OK, image);
			}
			return result;
		}

		/// <summary>
		/// Show a messagebox with the default caption and the specified buttons
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="data">Bindable data object</param>
		/// <param name="buttons">The buttons to display</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithCheckBox(string msg, MsgBoxExCheckBoxData data, MessageBoxButton buttons, MessageBoxImage image)
		{
			MessageBoxResult result = MessageBoxResult.None;
			if (data != null)
			{
				CheckBoxData = data;
				result = OpenMessageBox(msg, DefaultCaption, buttons, image);
			}
			return result;
		}

		/// <summary>
		/// Show a mesagebox with the specified caption and button(s)
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="data">Bindable data object</param>
		/// <param name="title">The title for the message box</param>
		/// <param name="parentWindow">The parent window that supplies the font family/size</param>
		/// <param name="buttons">The buttons to display</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithCheckBox(string msg, MsgBoxExCheckBoxData data, string title, MessageBoxButton buttons, MessageBoxImage image)
		{
			MessageBoxResult result = MessageBoxResult.None;
			if (data != null)
			{
				CheckBoxData = data;
				title = (string.IsNullOrEmpty(title)) ? DefaultCaption : title;
				result = OpenMessageBox(msg, title, buttons, image);
			}
			return result;
		}

		/////////////////////////////////////// checkbox and details without icons

		/// <summary>
		/// Show a messagebox, using default caption and just the OK button
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="data">Bindable data object</param>
		/// <param name="details">Amplifying message details (turns on "Details" expander)</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithCheckBoxAndDetails(string msg, MsgBoxExCheckBoxData data, string details)
		{
			MessageBoxResult result = MessageBoxResult.None;
			if (data != null)
			{
				DetailsText = details;
				CheckBoxData = data;
				result = OpenMessageBox(msg, DefaultCaption, MessageBoxButton.OK, MessageBoxImage.None);
			}
			return result;
		}

		/// <summary>
		/// Show a messagebox with the specified caption, and just the OK button
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="data">Bindable data object</param>
		/// <param name="details">Amplifying message details (turns on "Details" expander)</param>
		/// <param name="title">The messagebox caption</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithCheckBoxAndDetails(string msg, MsgBoxExCheckBoxData data, string details, string title)
		{
			MessageBoxResult result = MessageBoxResult.None;
			if (data != null)
			{
				DetailsText = details;
				CheckBoxData = data;
				title = (string.IsNullOrEmpty(title)) ? DefaultCaption : title;
				result = OpenMessageBox(msg, title, MessageBoxButton.OK, MessageBoxImage.None);
			}
			return result;
		}

		/// <summary>
		/// Show a messagebox with the default caption and the specified buttons
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="data">Bindable data object</param>
		/// <param name="details">Amplifying message details (turns on "Details" expander)</param>
		/// <param name="buttons">The buttons to display</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithCheckBoxAndDetails(string msg, MsgBoxExCheckBoxData data, string details, MessageBoxButton buttons)
		{
			MessageBoxResult result = MessageBoxResult.None;
			if (data != null)
			{
				DetailsText = details;
				CheckBoxData = data;
				result = OpenMessageBox(msg, DefaultCaption, buttons, MessageBoxImage.None);
			}
			return result;
		}

		/// <summary>
		/// Show a mesagebox with the specified caption and button(s)
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="data">Bindable data object</param>
		/// <param name="details">Amplifying message details (turns on "Details" expander)</param>
		/// <param name="title">The title for the message box</param>
		/// <param name="parentWindow">The parent window that supplies the font family/size</param>
		/// <param name="buttons">The buttons to display</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithCheckBoxAndDetails(string msg, MsgBoxExCheckBoxData data, string details, string title, MessageBoxButton buttons)
		{
			MessageBoxResult result = MessageBoxResult.None;
			if (data != null)
			{
				DetailsText = details;
				CheckBoxData = data;
				title = (string.IsNullOrEmpty(title)) ? DefaultCaption : title;
				result = OpenMessageBox(msg, title, buttons, MessageBoxImage.None);
			}
			return result;
		}

		/////////////////////////////////////// checkbox and details  with icons

		/// <summary>
		/// Show a messagebox, using default caption and just the OK button
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="data">Bindable data object</param>
		/// <param name="details">Amplifying message details (turns on "Details" expander)</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithCheckBoxAndDetails(string msg, MsgBoxExCheckBoxData data, string details, MessageBoxImage image)
		{
			MessageBoxResult result = MessageBoxResult.None;
			if (data != null)
			{
				DetailsText = details;
				CheckBoxData = data;
				result = OpenMessageBox(msg, DefaultCaption, MessageBoxButton.OK, image);
			}
			return result;
		}

		/// <summary>
		/// Show a messagebox with the specified caption, and just the OK button
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="data">Bindable data object</param>
		/// <param name="details">Amplifying message details (turns on "Details" expander)</param>
		/// <param name="title">The messagebox caption</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithCheckBoxAndDetails(string msg, MsgBoxExCheckBoxData data, string details, string title, MessageBoxImage image)
		{
			MessageBoxResult result = MessageBoxResult.None;
			if (data != null)
			{
				DetailsText = details;
				CheckBoxData = data;
				title = (string.IsNullOrEmpty(title)) ? DefaultCaption : title;
				result = OpenMessageBox(msg, title, MessageBoxButton.OK, image);
			}
			return result;
		}

		/// <summary>
		/// Show a messagebox with the default caption and the specified buttons
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="data">Bindable data object</param>
		/// <param name="details">Amplifying message details (turns on "Details" expander)</param>
		/// <param name="buttons">The buttons to display</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithCheckBoxAndDetails(string msg, MsgBoxExCheckBoxData data, string details, MessageBoxButton buttons, MessageBoxImage image)
		{
			MessageBoxResult result = MessageBoxResult.None;
			if (data != null)
			{
				DetailsText = details;
				CheckBoxData = data;
				result = OpenMessageBox(msg, DefaultCaption, buttons, image);
			}
			return result;
		}

		/// <summary>
		/// Show a mesagebox with the specified caption and button(s)
		/// </summary>
		/// <param name="msg">The message to display</param>
		/// <param name="data">Bindable data object</param>
		/// <param name="details">Amplifying message details (turns on "Details" expander)</param>
		/// <param name="title">The title for the message box</param>
		/// <param name="parentWindow">The parent window that supplies the font family/size</param>
		/// <param name="buttons">The buttons to display</param>
		/// <returns>The button that was clicked to dismiss the messagebox</returns>
		public static MessageBoxResult ShowWithCheckBoxAndDetails(string msg, MsgBoxExCheckBoxData data, string details, string title, MessageBoxButton buttons, MessageBoxImage image)
		{
			MessageBoxResult result = MessageBoxResult.None;
			if (data != null)
			{
				DetailsText = details;
				CheckBoxData = data;
				title = (string.IsNullOrEmpty(title)) ? DefaultCaption : title;
				result = OpenMessageBox(msg, title, buttons, image);
			}
			return result;
		}

		#endregion static show methods

		#region static configuration methods

		// colors

		/// <summary>
		/// Set the background color for the message area
		/// </summary>
		/// <param name="color"></param>
		public static void SetMessageBackground(System.Windows.Media.Color color)
		{
			try
			{
				MessageBackground = new SolidColorBrush(color);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, ex.ToString());
			}
		}

		/// <summary>
		/// Set the foreground color for the message area
		/// </summary>
		/// <param name="color"></param>
		public static void SetMessageForeground(System.Windows.Media.Color color)
		{
			try
			{
				MessageForeground = new SolidColorBrush(color);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, ex.ToString());
			}
		}

		/// <summary>
		/// Set the background color for the button panel area
		/// </summary>
		/// <param name="color"></param>
		public static void SetButtonBackground(System.Windows.Media.Color color)
		{
			try
			{
				ButtonBackground = new SolidColorBrush(color);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, ex.ToString());
			}
		}

		/// <summary>
		///  Create a WPF-compatible Color fro an octet string (such as "#123456")
		/// </summary>
		/// <param name="colorOctet"></param>
		/// <returns></returns>
		public static System.Windows.Media.Color ColorFromString(string colorOctet)
		{
			System.Windows.Media.Color wpfColor = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(colorOctet);
			return wpfColor;
		}

		// mechanicals

		/// <summary>
		/// Set the parent window to allow the messge box to inherit the font family and size
		/// </summary>
		/// <param name="parent"></param>
		public static void SetParentWindow(ContentControl parent)
		{
			ParentWindow = parent;
		}

		/// <summary>
		/// Set the custom button template *NAME*
		/// </summary>
		/// <param name="name"></param>
		public static void SetButtonTemplateName(string name)
		{
			ButtonTemplateName = name;
		}

		/// <summary>
		/// Sets the max form width to largest of 300 or the specified value
		/// </summary>
		/// <param name="value"></param>
		public static void SetMaxFormWidth(double value)
		{
			MaxFormWidth = Math.Max(value, 300);
		}

		// message box icon 

		public static void SetAsSilent(bool quiet)
		{
			_isSilent = quiet;
		}

		/// <summary>
		/// Sets the error delegate object which provides code that runs when the message box icon 
		/// is clicked.
		/// </summary>
		/// <param name="obj"></param>
		public static void SetErrorDelegate(MsgBoxExDelegate obj)
		{
			DelegateObj = obj;
		}

		/// <summary>
		/// Causes the original messagebox to close after the delegate action has pbeen processed.
		/// </summary>
		/// <param name="exitAfter"></param>
		public static void SetExitAfterErrorAction(bool exitAfter)
		{
			ExitAfterErrorAction = exitAfter;
		}

		/// <summary>
		/// Sets the tooltip for the message box icon. If the tooltip text is null, it won't be 
		/// displayed.
		/// </summary>
		/// <param name="tooltip"></param>
		public static void SetMsgBoxIconToolTip(string tooltip)
		{
			MsgBoxIconToolTip = tooltip;
		}

		public static void ShowDetailsButton(bool show)
		{
			ShowDetailsBtn = (show) ? Visibility.Visible : Visibility.Collapsed;
		}

		#endregion static configuration methods
	}

	// This class doesn't have to be inherted because its use is highly specific.
	/// <summary>
	/// Reresents the object that allows the checkbox state to be discoevered externally of the 
	/// messagebox.
	/// </summary>
	public class MsgBoxExCheckBoxData : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged

		private bool _isModified = false;

        public bool IsModified
        {
            get => _isModified;
            set { if (value != _isModified) { _isModified = true; NotifyPropertyChanged(); } }
        }
		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Notifies that the property changed, and sets IsModified to true.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
				if (propertyName != "IsModified")
				{
					IsModified = true;
				}
			}
		}

		#endregion INotifyPropertyChanged

		private string _checkBoxText;
		private bool _checkBoxIsChecked;

        /// <summary>
        /// Get/set the text content of the checkbox
        /// </summary>
        public string CheckBoxText
        {
            get => _checkBoxText;
            set { if (value != _checkBoxText) { _checkBoxText = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Get/set the flag that indicates whether the checkbox is checked
        /// </summary>
        public bool CheckBoxIsChecked
        {
            get => _checkBoxIsChecked;
            set { if (value != _checkBoxIsChecked) { _checkBoxIsChecked = value; NotifyPropertyChanged(); } }
        }
	}

	// This class MUST be inherited, and the PerformAction method MUST be overriden.
	/// <summary>
	/// Represents the object that allows a message box icon to execute code. This class must be 
	/// inherited.
	/// </summary>
	public abstract class MsgBoxExDelegate
	{
		/// <summary>
		/// Get/set the message text from the calling message box
		/// </summary>
		public string Message { get; set; }
		/// <summary>
		/// Get/set the details text (if it was specified in the messagebox)
		/// </summary>
		public string Details { get; set; }
		/// <summary>
		/// Get/set the message datetime at which this object was created
		/// </summary>
		public DateTime MessageDate { get; set; }

		/// <summary>
		/// Performs the desired action, and returns the result. MUST BE OVERIDDEN IN INHERITING CLASS. 
		/// </summary>
		/// <returns></returns>
		public virtual MessageBoxResult PerformAction(string message, string details = null)
		{
			throw new NotImplementedException();
		}
	}

}
