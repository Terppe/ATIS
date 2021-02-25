using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using ATIS.Ui.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;

//    CountriesViewModel Skriptdatum:   13.02.2021 12:32      

namespace ATIS.Ui.Views.Database.DCountry
{

    public class CountriesViewModel : ViewModelBase
    {
        // Version with Generic Unit Of Work and AtisDbContext for general use   

        #region [Private Data Members]
        private readonly CrudFunctions _extCrud = new CrudFunctions();
        private readonly DeleteFunctions _extDelete = new DeleteFunctions();
        private readonly SaveFunctions _extSave = new SaveFunctions();
        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private int _position;

        #endregion [Private Data Members]               

        #region [Constructor]

        public CountriesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                // Code runs "for real" 
                TblCountriesList = new ObservableCollection<TblCountry>();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          


        //    Part 1    



        #region [Commands Country]

        private RelayCommand _getCountriesByNameOrIdCommand;
        public ICommand GetCountriesByNameOrIdCommand => _getCountriesByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetCountriesByNameOrId(SearchCountryName); });

        private RelayCommand _addCountryCommand;
        public ICommand AddCountryCommand => _addCountryCommand ??= new RelayCommand(delegate { ExecuteAddCountry(null); });

        private RelayCommand _copyCountryCommand;
        public ICommand CopyCountryCommand => _copyCountryCommand ??= new RelayCommand(delegate { ExecuteCopyCountry(null); });

        private RelayCommand _deleteCountryCommand;
        public ICommand DeleteCountryCommand => _deleteCountryCommand ??= new RelayCommand(delegate { ExecuteDeleteCountry(SearchCountryName); });

        private RelayCommand _saveCountryCommand;
        public ICommand SaveCountryCommand => _saveCountryCommand ??= new RelayCommand(delegate { ExecuteSaveCountry(SearchCountryName); });

        #endregion [Commands Country]       


        #region [Methods Country]

        private void ExecuteGetCountriesByNameOrId(string searchName)
        {
            if (TblCountriesList == null)
                TblCountriesList ??= new ObservableCollection<TblCountry>();
            else
                TblCountriesList.Clear();

            TblCountriesList = _extCrud.GetCountriesCollectionFromSearchNameOrIdOrderBy<TblCountry>(searchName);

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(TblCountriesList.Count)) return;

            CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
            CountriesView.Refresh();
        }

        private void ExecuteAddCountry(object o)
        {
            if (TblCountriesList == null)
                TblCountriesList ??= new ObservableCollection<TblCountry>();

            TblCountriesList.Insert(0, new TblCountry { Name = CultRes.StringsRes.DatasetNew });

            CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
            CountriesView.MoveCurrentToFirst();
        }

        private void ExecuteCopyCountry(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTblCountry)) return;

            TblCountriesList = _extCrud.CopyCountry(CurrentTblCountry);

            CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
            CountriesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteCountry(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTblCountry)) return;

            _extDelete.DeleteCountry(CurrentTblCountry);

            TblCountriesList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<TblCountry>(searchName, "Country");
            CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
            CountriesView.MoveCurrentToLast();
        }

        private void ExecuteSaveCountry(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTblCountry)) return;

            _position = CountriesView.CurrentPosition;

            var ret = _extSave.SaveCountry(CurrentTblCountry);

            if (ret != true)
            {
                CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
                CountriesView.Refresh();
                return;
            }

            if (CurrentTblCountry.CountryId == 0) //new
            {
                TblCountriesList = _extCrud.GetLastCountriesDatasetOrderById();
                CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
                CountriesView.MoveCurrentToFirst();
            }
            else
            {
                TblCountriesList = _extCrud.GetCountriesCollectionFromSearchNameOrIdOrderBy<TblCountry>(searchName);
                CountriesView = CollectionViewSource.GetDefaultView(TblCountriesList);
                CountriesView.MoveCurrentToPosition(_position);
            }
        }
        #endregion "Public Commands"                   



        //    Part 2    




        //    Part 3    





        //    Part 4    




        //    Part 5    



        //    Part 6    




        //    Part 7    



        //    Part 8    



        //    Part 9    




        //    Part 10    



 //    Part 10    



                //    Part 11    


                #region "Public Properties TblCountry"

        private string _searchCountryName = "";
        public string SearchCountryName
        {
            get => _searchCountryName;
            set { _searchCountryName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView CountriesView;
        private TblCountry CurrentTblCountry => CountriesView?.CurrentItem as TblCountry;

        private ObservableCollection<TblCountry> _tblCountriesList;
        public ObservableCollection<TblCountry> TblCountriesList
        {
            get => _tblCountriesList;
            set { _tblCountriesList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<TblCountry> _tblCountriesAllList;
        public ObservableCollection<TblCountry> TblCountriesAllList
        {
            get => _tblCountriesAllList;
            set { _tblCountriesAllList = value; RaisePropertyChanged(""); }
        }


        #endregion "Public Properties"   


    }
}
