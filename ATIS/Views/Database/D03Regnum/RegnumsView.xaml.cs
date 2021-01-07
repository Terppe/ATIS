using System;

using System.Windows.Controls;

//  Tbl03RegnumsView.xaml.cs Skriptdatum:  04.01.2021  12:32       

namespace ATIS.Ui.Views.Database.D03Regnum
{
    /// <summary>
    /// Interactionslogic for RegnumsView.xaml
    /// </summary>
    public partial class RegnumsView : UserControl
    {
        public RegnumsView()
        {
            DataContext = new RegnumsViewModel();

            InitializeComponent();
        }
    }
}
