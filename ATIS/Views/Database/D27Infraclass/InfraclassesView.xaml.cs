﻿using System;


using System.Windows.Controls;


//  InfraclassesView.xaml.cs Skriptdatum:  07.01.2021  18:32     

namespace ATIS.Ui.Views.Database.D27Infraclass
{

    /// <summary>
    /// Interactionslogic for InfraclassesView.xaml
    /// </summary>
    public partial class InfraclassesView : UserControl
    {


        public InfraclassesView()
        {
            DataContext = new InfraclassesViewModel();

            InitializeComponent();
        }


    }
}