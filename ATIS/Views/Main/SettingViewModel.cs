using System.Windows.Input;
using ATIS.Ui.Helper;

namespace ATIS.Ui.Views.Main
{
    public class SettingViewModel : ViewModelBase
    {

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

    }
}