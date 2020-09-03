using System;
using ATIS.Ui.Helper;

namespace ATIS.Ui.Views.Main
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel()
        {
            FlipViewImages = new Uri[]
            {
                new Uri("pack://application:,,,/ATIS.Ui;component/../Asset/Images/Aquarium.jpg",
                    UriKind.RelativeOrAbsolute),
                new Uri("pack://application:,,,/ATIS.Ui;component/../Asset/Images/Nilpferd.jpg",
                    UriKind.RelativeOrAbsolute),
                new Uri("pack://application:,,,/ATIS.Ui;component/../Asset/Images/Aquarium2.jpg",
                    UriKind.RelativeOrAbsolute),
                new Uri("pack://application:,,,/ATIS.Ui;component/../Asset/Images/Home.jpg",
                    UriKind.RelativeOrAbsolute),
                new Uri("pack://application:,,,/ATIS.Ui;component/../Asset/Images/Privat.jpg",
                    UriKind.RelativeOrAbsolute),
                new Uri("pack://application:,,,/ATIS.Ui;component/../Asset/Images/Settings.jpg",
                    UriKind.RelativeOrAbsolute)
            };
        }

        public Uri[] FlipViewImages { get; set; }


    }
}

