using System.Windows;
using System.Windows.Controls;

namespace ATIS.Ui.Views.Main
{
    /// <summary>
    /// Interaktionslogik für SettingView.xaml
    /// </summary>
    public partial class SettingView : UserControl
    {
        public SettingView()
        {
            InitializeComponent();
        }

        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rdButton)
            {
                switch (rdButton.Tag.ToString())
                {
                    case "Dark":
        //                AppSettings.CurrentTheme = ElementTheme.Dark;
                        break;
                    case "light":
       //                 AppSettings.CurrentTheme = ElementTheme.Light;
                        break;
                }

       //         AppSettings.ThemeUpdate();
            }
        }
    }
}
