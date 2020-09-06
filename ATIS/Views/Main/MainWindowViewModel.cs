using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ATIS.Ui.Helper;
using ControlzEx.Theming;
using MahApps.Metro.Controls.Dialogs;

namespace ATIS.Ui.Views.Main
{
    public class MainWindowViewModel : ViewModelBase
    {

        public MainWindowViewModel()
        {

        }


        //--------------------Search -------------
        private RelayCommand _getByNameOrIdCommand;
        public ICommand GetByNameCommand => _getByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetByName(SearchName); });


        private void ExecuteGetByName(string searchName)
        {
            //      TabIndexDetail = 1;

            //PhylumsCollection.Clear();
            //DivisionsCollection.Clear();
            //SubphylumsCollection.Clear();
            //SubdivisionsCollection.Clear();
            //ReferencesCollection.Clear();
            //ReferenceExpertsCollection.Clear();
            //ReferenceSourcesCollection.Clear();
            //ReferenceAuthorsCollection.Clear();
            //CommentsCollection.Clear();

            //    RegnumsCollection = _extGet.SearchNameAndIdReturnCollection<Tbl03Regnum>(searchName, "regnum");
            //   RaisePropertyChanged("RegnumsCollection");
        }

        public string SearchName { get; set; }

    }
}
