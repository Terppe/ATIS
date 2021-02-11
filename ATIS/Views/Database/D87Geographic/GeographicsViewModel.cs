using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using log4net;
using Microsoft.EntityFrameworkCore;

using System.Globalization;
using System.Collections.Generic;

//    GeographicsViewModel Skriptdatum:  02.02.2021  10:32    

namespace ATIS.Ui.Views.Database.D87Geographic
{

    public class GeographicsViewModel : ViewModelBase
    {
        // Version with Generic Unit Of Work and AtisDbContext for general use   

        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(typeof(GeographicsViewModel));
        private readonly CrudFunctions _extCrud = new CrudFunctions();
        private readonly DeleteFunctions _extDelete = new DeleteFunctions();
        private readonly SaveFunctions _extSave = new SaveFunctions();
        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private int _position;

        #endregion [Private Data Members]               

        #region [Constructor]

        public GeographicsViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                // Code runs "for real" 
                GetValueContinent();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          


        //    Part 1    



        #region [Commands Geographic]

        private RelayCommand _getGeographicsByIdCommand;
        public ICommand GetGeographicsByIdCommand => _getGeographicsByIdCommand ??= new RelayCommand(delegate { ExecuteGetGeographicsById(SearchGeographicId); });

        private RelayCommand _addGeographicCommand;
        public ICommand AddGeographicCommand => _addGeographicCommand ??= new RelayCommand(delegate { ExecuteAddGeographic(null); });

        private RelayCommand _copyGeographicCommand;
        public ICommand CopyGeographicCommand => _copyGeographicCommand ??= new RelayCommand(delegate { ExecuteCopyGeographic(null); });

        private RelayCommand _deleteGeographicCommand;
        public ICommand DeleteGeographicCommand => _deleteGeographicCommand ??= new RelayCommand(delegate { ExecuteDeleteGeographic(SearchGeographicId); });

        private RelayCommand _saveGeographicCommand;
        public ICommand SaveGeographicCommand => _saveGeographicCommand ??= new RelayCommand(delegate { ExecuteSaveGeographic(SearchGeographicId); });

        #endregion [Commands Geographic]       


        #region [Methods Geographic]

        private void ExecuteGetGeographicsById(int searchId)
        {
            if (Tbl69FiSpeciessesAllList == null)
                Tbl69FiSpeciessesAllList ??= new ObservableCollection<Tbl69FiSpecies>();
            else
                Tbl69FiSpeciessesAllList.Clear();

            if (Tbl72PlSpeciessesAllList == null)
                Tbl72PlSpeciessesAllList ??= new ObservableCollection<Tbl72PlSpecies>();
            else
                Tbl72PlSpeciessesAllList.Clear();

            if (Tbl87GeographicsList == null)
                Tbl87GeographicsList ??= new ObservableCollection<Tbl87Geographic>();
            else
                Tbl87GeographicsList.Clear();

            TblCountriesAllList = _extCrud.GetCollectionAllOrderBy<TblCountry>("Country");
            Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("FiSpecies");
            Tbl72PlSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl72PlSpecies>("PlSpecies");

            Tbl87GeographicsList = _extCrud.GetGeographicsCollectionFromSearchIdOrderBy<Tbl87Geographic>(searchId);

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl87GeographicsList.Count)) return;

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 2;

            GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
            GeographicsView.Refresh();
        }

        private void ExecuteAddGeographic(object o)
        {
            if (Tbl87GeographicsList == null)
                Tbl87GeographicsList ??= new ObservableCollection<Tbl87Geographic>();
            else
                Tbl87GeographicsList.Clear();

            if (Tbl69FiSpeciessesAllList == null)
                Tbl69FiSpeciessesAllList ??= new ObservableCollection<Tbl69FiSpecies>();
            else
                Tbl69FiSpeciessesAllList.Clear();

            if (Tbl72PlSpeciessesAllList == null)
                Tbl72PlSpeciessesAllList ??= new ObservableCollection<Tbl72PlSpecies>();
            else
                Tbl72PlSpeciessesAllList.Clear();

            Tbl87GeographicsList.Insert(0, new Tbl87Geographic { Info = CultRes.StringsRes.DatasetNew });

            TblCountriesAllList = _extCrud.GetCollectionAllOrderBy<TblCountry>("Country");
            Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("FiSpecies");
            Tbl72PlSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl72PlSpecies>("PlSpecies");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 2;

            GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
            GeographicsView.MoveCurrentToFirst();
        }

        private void ExecuteCopyGeographic(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl87Geographic)) return;

            Tbl87GeographicsList = _extCrud.CopyGeographic(CurrentTbl87Geographic);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
            GeographicsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteGeographic(int searchId)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl87Geographic)) return;

            _extDelete.DeleteGeographic(CurrentTbl87Geographic);

            Tbl87GeographicsList = _extCrud.GetGeographicsCollectionFromSearchIdOrderBy<Tbl87Geographic>(searchId);
            GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
            GeographicsView.MoveCurrentToLast();
        }

        private void ExecuteSaveGeographic(int searchId)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl87Geographic)) return;

            _position = GeographicsView.CurrentPosition;

            var ret = _extSave.SaveGeographic(CurrentTbl87Geographic);

            if (ret != true)
            {
                GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
                GeographicsView.Refresh();
                return;
            }

            if (CurrentTbl87Geographic.GeographicId == 0) //new
            {
                Tbl87GeographicsList = _extCrud.GetLastGeographicsDatasetOrderById();
                GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
                GeographicsView.MoveCurrentToFirst();
            }
            else
            {
                Tbl87GeographicsList = _extCrud.GetGeographicsCollectionFromSearchIdOrderBy<Tbl87Geographic>(searchId);
                GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
                GeographicsView.MoveCurrentToPosition(_position);
            }
        }
        #endregion [Methods Geographic]                



        //    Part 2    


        #region "Public Commands Connect <== Tbl69FiSpecies"                 

        private RelayCommand _saveFiSpeciesCommand;

        public ICommand SaveFiSpeciesCommand => _saveFiSpeciesCommand ??= new RelayCommand(delegate { ExecuteSaveFiSpecies(null); });

        private void ExecuteSaveFiSpecies(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl69FiSpecies)) return;

            _extSave.SaveFiSpecies(CurrentTbl69FiSpecies);

            Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromFiSpeciesIdOrderBy<Tbl69FiSpecies>(CurrentTbl87Geographic.FiSpeciesId);
            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();
        }

        #endregion "Public Commands"                  


        //    Part 3    


        #region "Public Commands Connect <== Tbl72PlSpecies"                 

        private RelayCommand _savePlSpeciesCommand;

        public ICommand SavePlSpeciesCommand => _savePlSpeciesCommand ??= new RelayCommand(delegate { ExecuteSavePlSpecies(null); });


        private void ExecuteSavePlSpecies(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl72PlSpecies)) return;

            _extSave.SavePlSpecies(CurrentTbl72PlSpecies);

            Tbl72PlSpeciessesList = _extCrud.GetPlSpeciessesCollectionFromPlSpeciesIdOrderBy<Tbl72PlSpecies>(CurrentTbl87Geographic.PlSpeciesId);
            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            PlSpeciessesView.Refresh();
        }

        #endregion "Public Commands"                  




        //    Part 4    




        //    Part 5    



        //    Part 6    




        //    Part 7    



        //    Part 8    



        //    Part 9    



        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        #endregion "Public Commands Connected Tables by DoubleClick"

        #region "Public Method Connected Tables by DoubleClick"

        private void GetConnectedTablesById(object o)
        {

            Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("Genus");
            Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("Speciesgroup");

            Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromFiSpeciesIdOrderBy<Tbl69FiSpecies>(CurrentTbl87Geographic.FiSpeciesId);

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();

        }

        #endregion "Public Method Connected Tables by DoubleClick"     



        //    Part 10    


        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        private int _selectedDetailTabIndex;


        public int SelectedMainTabIndex
        {
            get => _selectedMainTabIndex;
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value; RaisePropertyChanged("");

                if (_selectedMainTabIndex == 0)
                {
                    if (CurrentTbl87Geographic != null)
                    {
                        Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromFiSpeciesIdOrderBy<Tbl69FiSpecies>(CurrentTbl87Geographic.FiSpeciesId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("Genus");
                        Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("Speciesgroup");

                        FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                        FiSpeciessesView.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }

                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl87Geographic != null)
                    {
                        Tbl72PlSpeciessesList = _extCrud.GetPlSpeciessesCollectionFromPlSpeciesIdOrderBy<Tbl72PlSpecies>(CurrentTbl87Geographic.PlSpeciesId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("Genus");
                        Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("Speciesgroup");

                        PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
                        PlSpeciessesView.Refresh();
                    }
                    SelectedDetailTabIndex = 1;
                }

            }
        }

        public int SelectedDetailTabIndex
        {
            get => _selectedDetailTabIndex;
            set
            {
                if (value == _selectedDetailTabIndex) return;
                _selectedDetailTabIndex = value; RaisePropertyChanged("");

                if (_selectedDetailTabIndex == 0)
                {
                    if (CurrentTbl87Geographic != null)
                    {
                        Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromFiSpeciesIdOrderBy<Tbl69FiSpecies>(CurrentTbl87Geographic.FiSpeciesId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("Genus");
                        Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("Speciesgroup");

                        FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                        FiSpeciessesView.Refresh();
                    }
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 1)
                {
                    if (CurrentTbl87Geographic != null)
                    {
                        Tbl72PlSpeciessesList = _extCrud.GetPlSpeciessesCollectionFromPlSpeciesIdOrderBy<Tbl72PlSpecies>(CurrentTbl87Geographic.PlSpeciesId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("Genus");
                        Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("Speciesgroup");

                        PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
                        PlSpeciessesView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 2)
                {
                    if (CurrentTbl87Geographic != null)
                    {
                        //  var plantaeRegnum = _extCrud.GetPlSpeciesSingleByPlSpeciesName<Tbl72PlSpecies>("Plantae#Regnum#");
                        // CurrentTbl87Geographic.PlSpeciesId = plantaeRegnum.PlSpeciesId;
                        //    CurrentTbl87Geographic.PlSpeciesId = 1;
                        //  var animaliaRegnum = _extCrud.GetFiSpeciesSingleByFiSpeciesName<Tbl69FiSpecies>("Animalia#Regnum#");
                        // CurrentTbl87Geographic.FiSpeciesId = animaliaRegnum.FiSpeciesId;
                        //  CurrentTbl87Geographic.FiSpeciesId = 2;

                        if (CurrentTbl87Geographic.FiSpeciesId == 2)
                        {
                            Tbl87GeographicsList = _extCrud.GetNamesCollectionFromPlSpeciesIdOrderBy<Tbl87Geographic>(CurrentTbl87Geographic.PlSpeciesId);
                        }
                        if (CurrentTbl87Geographic.PlSpeciesId == 1)
                        {
                            Tbl87GeographicsList = _extCrud.GetNamesCollectionFromFiSpeciesIdOrderBy<Tbl87Geographic>(CurrentTbl87Geographic.FiSpeciesId);
                        }

                        TblCountriesAllList = _extCrud.GetCollectionAllOrderBy<TblCountry>("Country");
                        Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("FiSpecies");
                        Tbl72PlSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl72PlSpecies>("PlSpecies");

                        GeographicsView = CollectionViewSource.GetDefaultView(Tbl87GeographicsList);
                        GeographicsView.Refresh();
                    }
                    SelectedMainTabIndex = 0;
                }

            }
        }
        #endregion "Public Commands to open Detail TabItems"          


        //    Part 11    


        #region "Public Properties Tbl87Geographic"

        private int _searchGeographicId = 0;
        public int SearchGeographicId
        {
            get => _searchGeographicId;
            set { _searchGeographicId = value; RaisePropertyChanged(""); }
        }

        public ICollectionView GeographicsView;
        private Tbl87Geographic CurrentTbl87Geographic => GeographicsView?.CurrentItem as Tbl87Geographic;

        private ObservableCollection<Tbl87Geographic> _tbl87GeographicsList;
        public ObservableCollection<Tbl87Geographic> Tbl87GeographicsList
        {
            get => _tbl87GeographicsList;
            set { _tbl87GeographicsList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl87Geographic> _tbl87GeographicsAllList;
        public ObservableCollection<Tbl87Geographic> Tbl87GeographicsAllList
        {
            get => _tbl87GeographicsAllList;
            set { _tbl87GeographicsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   

        #region "Public Properties Tbl69FiSpecies"

        public ICollectionView FiSpeciessesView;
        private Tbl69FiSpecies CurrentTbl69FiSpecies => FiSpeciessesView?.CurrentItem as Tbl69FiSpecies;

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesList;
        public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
        {
            get => _tbl69FiSpeciessesList;
            set { _tbl69FiSpeciessesList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList;
        public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
        {
            get => _tbl69FiSpeciessesAllList;
            set { _tbl69FiSpeciessesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   


        #region "Public Properties Tbl72PlSpecies"

        public ICollectionView PlSpeciessesView;
        private Tbl72PlSpecies CurrentTbl72PlSpecies => PlSpeciessesView?.CurrentItem as Tbl72PlSpecies;

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesList;
        public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesList
        {
            get => _tbl72PlSpeciessesList;
            set { _tbl72PlSpeciessesList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesAllList;
        public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesAllList
        {
            get => _tbl72PlSpeciessesAllList;
            set { _tbl72PlSpeciessesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   

        #region "Public Properties Tbl68Speciesgroup"

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
        {
            get => _tbl68SpeciesgroupsAllList;
            set { _tbl68SpeciesgroupsAllList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties Tbl68Speciesgroup"

        #region "Public Properties Tbl66Genus"

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList;
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties Tbl66Genus"

        #region "Private Continent, Country"

        private void GetValueContinent()
        {
            _continents = new List<Continent>()
            {
                new Continent {Name = "Africa"},
                new Continent {Name = "Antarctica"},
                new Continent {Name = "Asia"},
                new Continent {Name = "Australia"},
                new Continent {Name = "Central/South America"},
                new Continent {Name = "Europe"},
                new Continent {Name = "North America/Caribbean"}
            };

            _selectedContinent = new Continent();
        }

        private List<Continent> _continents;

        public List<Continent> Continents
        {
            get => _continents;
            set { _continents = value; RaisePropertyChanged(""); }
        }

        private Continent _selectedContinent;

        public Continent SelectedContinent
        {
            get => _selectedContinent;
            set { _selectedContinent = value; RaisePropertyChanged(""); }
        }

        public class Continent
        {
            public string Name
            {
                get;
                set;
            }
        }

        private ObservableCollection<TblCountry> _tblCountriesAllList;
        public ObservableCollection<TblCountry> TblCountriesAllList
        {
            get => _tblCountriesAllList;
            set { _tblCountriesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Private Methods"  


    }
}
