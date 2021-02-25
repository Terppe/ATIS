using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using ATIS.Ui.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;

//    SynonymsViewModel Skriptdatum:  01.02.2021  10:32    

namespace ATIS.Ui.Views.Database.D84Synonym
{

    public class SynonymsViewModel : ViewModelBase
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

        public SynonymsViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                // Code runs "for real" 
                Tbl84SynonymsList = new ObservableCollection<Tbl84Synonym>();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          


        //    Part 1    



        #region [Commands Synonym]

        private RelayCommand _getSynonymsByNameOrIdCommand;
        public ICommand GetSynonymsByNameOrIdCommand => _getSynonymsByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetSynonymsByNameOrId(SearchSynonymName); });

        private RelayCommand _addSynonymCommand;
        public ICommand AddSynonymCommand => _addSynonymCommand ??= new RelayCommand(delegate { ExecuteAddSynonym(null); });

        private RelayCommand _copySynonymCommand;
        public ICommand CopySynonymCommand => _copySynonymCommand ??= new RelayCommand(delegate { ExecuteCopySynonym(null); });

        private RelayCommand _deleteSynonymCommand;
        public ICommand DeleteSynonymCommand => _deleteSynonymCommand ??= new RelayCommand(delegate { ExecuteDeleteSynonym(SearchSynonymName); });

        private RelayCommand _saveSynonymCommand;
        public ICommand SaveSynonymCommand => _saveSynonymCommand ??= new RelayCommand(delegate { ExecuteSaveSynonym(SearchSynonymName); });

        #endregion [Commands Synonym]       


        #region [Methods Synonym]

        private void ExecuteGetSynonymsByNameOrId(string searchName)
        {
            if (Tbl69FiSpeciessesAllList == null)
                Tbl69FiSpeciessesAllList ??= new ObservableCollection<Tbl69FiSpecies>();
            else
                Tbl69FiSpeciessesAllList.Clear();

            if (Tbl72PlSpeciessesAllList == null)
                Tbl72PlSpeciessesAllList ??= new ObservableCollection<Tbl72PlSpecies>();
            else
                Tbl72PlSpeciessesAllList.Clear();

            if (Tbl84SynonymsList == null)
                Tbl84SynonymsList ??= new ObservableCollection<Tbl84Synonym>();
            else
                Tbl84SynonymsList.Clear();

            Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("FiSpecies");
            Tbl72PlSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl72PlSpecies>("PlSpecies");

            Tbl84SynonymsList = _extCrud.GetSynonymsCollectionFromSearchNameOrIdOrderBy<Tbl84Synonym>(searchName);

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl84SynonymsList.Count)) return;

            SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
            SynonymsView.Refresh();
        }

        private void ExecuteAddSynonym(object o)
        {
            if (Tbl84SynonymsList == null)
                Tbl84SynonymsList ??= new ObservableCollection<Tbl84Synonym>();

            if (Tbl69FiSpeciessesAllList == null)
                Tbl69FiSpeciessesAllList ??= new ObservableCollection<Tbl69FiSpecies>();
            else
                Tbl69FiSpeciessesAllList.Clear();

            if (Tbl72PlSpeciessesAllList == null)
                Tbl72PlSpeciessesAllList ??= new ObservableCollection<Tbl72PlSpecies>();
            else
                Tbl72PlSpeciessesAllList.Clear();

            Tbl84SynonymsList.Insert(0, new Tbl84Synonym { SynonymName = CultRes.StringsRes.DatasetNew });

            Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("FiSpecies");
            Tbl72PlSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl72PlSpecies>("PlSpecies");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 2;

            SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
            SynonymsView.MoveCurrentToFirst();
        }

        private void ExecuteCopySynonym(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl84Synonym)) return;

            Tbl84SynonymsList = _extCrud.CopySynonym(CurrentTbl84Synonym);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
            SynonymsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteSynonym(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl84Synonym)) return;

            _extDelete.DeleteSynonym(CurrentTbl84Synonym);

            Tbl84SynonymsList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl84Synonym>(searchName, "Synonym");
            SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
            SynonymsView.MoveCurrentToLast();
        }

        private void ExecuteSaveSynonym(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl84Synonym)) return;

            _position = SynonymsView.CurrentPosition;

            var ret = _extSave.SaveSynonym(CurrentTbl84Synonym);

            if (ret != true)
            {
                SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
                SynonymsView.Refresh();
                return;
            }

            if (CurrentTbl84Synonym.SynonymId == 0) //new
            {
                Tbl84SynonymsList = _extCrud.GetLastSynonymsDatasetOrderById();
                SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
                SynonymsView.MoveCurrentToFirst();
            }
            else
            {
                Tbl84SynonymsList = _extCrud.GetSynonymsCollectionFromSearchNameOrIdOrderBy<Tbl84Synonym>(searchName);
                SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
                SynonymsView.MoveCurrentToPosition(_position);
            }
        }
        #endregion [Methods Synonym]                



        //    Part 2    


        #region "Public Commands Connect <== Tbl69FiSpecies"                 


        private RelayCommand _saveFiSpeciesCommand;

        public ICommand SaveFiSpeciesCommand => _saveFiSpeciesCommand ??= new RelayCommand(delegate { ExecuteSaveFiSpecies(null); });

        private void ExecuteSaveFiSpecies(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl69FiSpecies)) return;

            _extSave.SaveFiSpecies(CurrentTbl69FiSpecies);

            Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromFiSpeciesIdOrderBy<Tbl69FiSpecies>(CurrentTbl84Synonym.FiSpeciesId);
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

            Tbl72PlSpeciessesList = _extCrud.GetPlSpeciessesCollectionFromPlSpeciesIdOrderBy<Tbl72PlSpecies>(CurrentTbl84Synonym.PlSpeciesId);
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

            Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromFiSpeciesIdOrderBy<Tbl69FiSpecies>(CurrentTbl84Synonym.FiSpeciesId);

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
                    if (CurrentTbl84Synonym != null)
                    {
                        Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromFiSpeciesIdOrderBy<Tbl69FiSpecies>(CurrentTbl84Synonym.FiSpeciesId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("Genus");
                        Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("Speciesgroup");

                        FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                        FiSpeciessesView.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }

                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl84Synonym != null)
                    {
                        Tbl72PlSpeciessesList = _extCrud.GetPlSpeciessesCollectionFromPlSpeciesIdOrderBy<Tbl72PlSpecies>(CurrentTbl84Synonym.PlSpeciesId);

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
                    if (CurrentTbl84Synonym != null)
                    {
                        Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromFiSpeciesIdOrderBy<Tbl69FiSpecies>(CurrentTbl84Synonym.FiSpeciesId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("Genus");
                        Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("Speciesgroup");

                        FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                        FiSpeciessesView.Refresh();
                    }
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 1)
                {
                    if (CurrentTbl84Synonym != null)
                    {
                        Tbl72PlSpeciessesList = _extCrud.GetPlSpeciessesCollectionFromPlSpeciesIdOrderBy<Tbl72PlSpecies>(CurrentTbl84Synonym.PlSpeciesId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("Genus");
                        Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("Speciesgroup");

                        PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
                        PlSpeciessesView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 2)
                {
                    if (CurrentTbl84Synonym != null)
                    {
                        //  var plantaeRegnum = _extCrud.GetPlSpeciesSingleByPlSpeciesName<Tbl72PlSpecies>("Plantae#Regnum#");
                        // CurrentTbl78Name.PlSpeciesId = plantaeRegnum.PlSpeciesId;
                        //    CurrentTbl78Name.PlSpeciesId = 1;
                        //  var animaliaRegnum = _extCrud.GetFiSpeciesSingleByFiSpeciesName<Tbl69FiSpecies>("Animalia#Regnum#");
                        // CurrentTbl84Synonym.FiSpeciesId = animaliaRegnum.FiSpeciesId;
                        //  CurrentTbl84Synonym.FiSpeciesId = 2;

                        if (CurrentTbl84Synonym.FiSpeciesId == 2)
                        {
                            Tbl84SynonymsList = _extCrud.GetNamesCollectionFromPlSpeciesIdOrderBy<Tbl84Synonym>(CurrentTbl84Synonym.PlSpeciesId);
                        }
                        if (CurrentTbl84Synonym.PlSpeciesId == 1)
                        {
                            Tbl84SynonymsList = _extCrud.GetNamesCollectionFromFiSpeciesIdOrderBy<Tbl84Synonym>(CurrentTbl84Synonym.FiSpeciesId);
                        }

                        Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("FiSpecies");
                        Tbl72PlSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl72PlSpecies>("PlSpecies");

                        SynonymsView = CollectionViewSource.GetDefaultView(Tbl84SynonymsList);
                        SynonymsView.Refresh();
                    }
                    SelectedMainTabIndex = 0;
                }

            }
        }
        #endregion "Public Commands to open Detail TabItems"          


        //    Part 11    


        #region "Public Properties Tbl84Synonym"

        private string _searchSynonymName = "";
        public string SearchSynonymName
        {
            get => _searchSynonymName;
            set { _searchSynonymName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView SynonymsView;
        private Tbl84Synonym CurrentTbl84Synonym => SynonymsView?.CurrentItem as Tbl84Synonym;

        private ObservableCollection<Tbl84Synonym> _tbl84SynonymsList;
        public ObservableCollection<Tbl84Synonym> Tbl84SynonymsList
        {
            get => _tbl84SynonymsList;
            set { _tbl84SynonymsList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl84Synonym> _tbl84SynonymsAllList;
        public ObservableCollection<Tbl84Synonym> Tbl84SynonymsAllList
        {
            get => _tbl84SynonymsAllList;
            set { _tbl84SynonymsAllList = value; RaisePropertyChanged(""); }
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


    }
}
