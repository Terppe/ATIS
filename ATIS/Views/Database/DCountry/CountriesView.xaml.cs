﻿using System.Windows.Controls;


//  CountriesView.xaml.cs Skriptdatum:   29.11.2018 12:32       

namespace ATIS.Ui.Views.Database.DCountry
{

    /// <summary>
    /// Interactionslogic for CountriesView.xaml
    /// </summary>
    public partial class CountriesView : UserControl
    {


        public CountriesView()
        {
            DataContext = new CountriesViewModel();

            InitializeComponent();
        }


    }
}