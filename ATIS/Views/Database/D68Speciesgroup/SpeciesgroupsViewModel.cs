using System;
using System.Collections.ObjectModel;
using System.ComponentModel;


using System.Windows.Data;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using log4net;

//    SpeciesgroupsViewModel Skriptdatum:  07.01.2021  10:32    

namespace ATIS.Ui.Views.Database.D68Speciesgroup
{

    public class SpeciesgroupsViewModel : ViewModelBase
    {
        // Version with Generic Unit Of Work and AtisDbContext for general use   

        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(typeof(SpeciesgroupsViewModel));
        private readonly CrudFunctions _extCrud = new CrudFunctions();
        private readonly DeleteFunctions _extDelete = new DeleteFunctions();
        private readonly SaveFunctions _extSave = new SaveFunctions();
        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private int _position;

        #endregion [Private Data Members]               

        #region [Constructor]

        public SpeciesgroupsViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                // Code runs "for real" 
                Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          


        //    Part 1    



        #region [Commands Speciesgroup]

        private RelayCommand _getSpeciesgroupsByNameOrIdCommand;
        public ICommand GetSpeciesgroupsByNameOrIdCommand => _getSpeciesgroupsByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetSpeciesgroupsByNameOrId(SearchSpeciesgroupName); });

        private RelayCommand _addSpeciesgroupCommand;
        public ICommand AddSpeciesgroupCommand => _addSpeciesgroupCommand ??= new RelayCommand(delegate { ExecuteAddSpeciesgroup(null); });

        private RelayCommand _copySpeciesgroupCommand;
        public ICommand CopySpeciesgroupCommand => _copySpeciesgroupCommand ??= new RelayCommand(delegate { ExecuteCopySpeciesgroup(null); });

        private RelayCommand _deleteSpeciesgroupCommand;
        public ICommand DeleteSpeciesgroupCommand => _deleteSpeciesgroupCommand ??= new RelayCommand(delegate { ExecuteDeleteSpeciesgroup(SearchSpeciesgroupName); });

        private RelayCommand _saveSpeciesgroupCommand;
        public ICommand SaveSpeciesgroupCommand => _saveSpeciesgroupCommand ??= new RelayCommand(delegate { ExecuteSaveSpeciesgroup(SearchSpeciesgroupName); });

        #endregion [Commands Speciesgroup]       


        #region [Methods Speciesgroup]

        private void ExecuteGetSpeciesgroupsByNameOrId(string searchName)
        {
            if (Tbl68SpeciesgroupsList == null)
                Tbl68SpeciesgroupsList ??= new ObservableCollection<Tbl68Speciesgroup>();
            else
                Tbl68SpeciesgroupsList.Clear();

            Tbl68SpeciesgroupsList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl68Speciesgroup>(searchName, "Speciesgroup");

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl68SpeciesgroupsList.Count)) return;

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 0;

            SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            SpeciesgroupsView.Refresh();
        }

        private void ExecuteAddSpeciesgroup(object o)
        {
            if (Tbl68SpeciesgroupsList == null)
                Tbl68SpeciesgroupsList ??= new ObservableCollection<Tbl68Speciesgroup>();
            else
                Tbl68SpeciesgroupsList.Clear();

            if (Tbl66GenussesAllList == null)
                Tbl66GenussesAllList ??= new ObservableCollection<Tbl66Genus>();
            else
                Tbl66GenussesAllList.Clear();

            Tbl68SpeciesgroupsList.Insert(0, new Tbl68Speciesgroup { SpeciesgroupName = CultRes.StringsRes.DatasetNew });

            Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("Genus");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            SpeciesgroupsView.MoveCurrentToFirst();
        }

        private void ExecuteCopySpeciesgroup(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl68Speciesgroup)) return;

            Tbl68SpeciesgroupsList = _extCrud.CopySpeciesgroup(CurrentTbl68Speciesgroup);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            SpeciesgroupsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteSpeciesgroup(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl68Speciesgroup)) return;

            _extDelete.DeleteSpeciesgroup(CurrentTbl68Speciesgroup);

            Tbl68SpeciesgroupsList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl68Speciesgroup>(searchName, "Speciesgroup");
            SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            SpeciesgroupsView.MoveCurrentToLast();
        }

        private void ExecuteSaveSpeciesgroup(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl68Speciesgroup)) return;

            _position = SpeciesgroupsView.CurrentPosition;

            var ret = _extSave.SaveSpeciesgroup(CurrentTbl68Speciesgroup);

            if (ret != true)
            {
                SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
                SpeciesgroupsView.Refresh();
                return;
            }

            if (CurrentTbl68Speciesgroup.SpeciesgroupId == 0) //new
            {
                Tbl68SpeciesgroupsList = _extCrud.GetLastSpeciesgroupsDatasetOrderById();
                SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
                SpeciesgroupsView.MoveCurrentToFirst();
            }
            else
            {
                Tbl68SpeciesgroupsList = _extCrud.GetSpeciesgroupsCollectionFromSearchNameOrIdOrderBy<Tbl68Speciesgroup>(searchName);
                SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
                SpeciesgroupsView.MoveCurrentToPosition(_position);
            }
        }
        #endregion [Methods Speciesgroup]                



        //    Part 2    



        //    Part 3    





        //    Part 4    


        #region [Public Commands Connect ==> Tbl69FiSpecies]                 

        private RelayCommand _addFiSpeciesCommand;
        public ICommand AddFiSpeciesCommand => _addFiSpeciesCommand ??= new RelayCommand(delegate { ExecuteAddFiSpecies(null); });

        private RelayCommand _copyFiSpeciesCommand;
        public ICommand CopyFiSpeciesCommand => _copyFiSpeciesCommand ??= new RelayCommand(delegate { ExecuteCopyFiSpecies(null); });

        private RelayCommand _deleteFiSpeciesCommand;
        public ICommand DeleteFiSpeciesCommand => _deleteFiSpeciesCommand ??= new RelayCommand(delegate { ExecuteDeleteFiSpecies(null); });

        private RelayCommand _saveFiSpeciesCommand;
        public ICommand SaveFiSpeciesCommand => _saveFiSpeciesCommand ??= new RelayCommand(delegate { ExecuteSaveFiSpecies(null); });

        #endregion [Public Commands Connect ==> Tbl69FiSpecies]    

        #region [Public Methods Connect ==> Tbl69FiSpecies]                   

        private void ExecuteAddFiSpecies(object o)
        {
            if (Tbl68SpeciesgroupsAllList == null)
                Tbl68SpeciesgroupsAllList ??= new ObservableCollection<Tbl68Speciesgroup>();
            else
                Tbl68SpeciesgroupsAllList.Clear();

            Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("Speciesgroup");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 0;

            Tbl69FiSpeciessesList ??= new ObservableCollection<Tbl69FiSpecies>();

            Tbl69FiSpeciessesList.Insert(0, new Tbl69FiSpecies { FiSpeciesName = CultRes.StringsRes.DatasetNew });
            Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("Genus");

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.MoveCurrentToFirst();
        }

        private void ExecuteCopyFiSpecies(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl69FiSpecies)) return;

            Tbl69FiSpeciessesList = _extCrud.CopyFiSpecies(CurrentTbl69FiSpecies);

            // evtl verbundene tabellen-Datensätze auch kopieren Names, Images, Synonyms und Geographics

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteFiSpecies(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl69FiSpecies)) return;

            //check if in Tbl69FiSpeciesses connected datasets no delete possible, Names, Images, Synonyms and Geographics delete and than return
            Tbl78NamesList = _extCrud.SearchForConnectedDatasetsWithFiSpeciesIdInTableName(CurrentTbl69FiSpecies.FiSpeciesId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl78NamesList.Count, "Name")) return;
            Tbl81ImagesList = _extCrud.SearchForConnectedDatasetsWithFiSpeciesIdInTableImage(CurrentTbl69FiSpecies.FiSpeciesId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl81ImagesList.Count, "Image")) return;
            Tbl84SynonymsList = _extCrud.SearchForConnectedDatasetsWithFiSpeciesIdInTableSynonym(CurrentTbl69FiSpecies.FiSpeciesId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl84SynonymsList.Count, "Synonym")) return;
            Tbl87GeographicsList = _extCrud.SearchForConnectedDatasetsWithFiSpeciesIdInTableGeographic(CurrentTbl69FiSpecies.FiSpeciesId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl87GeographicsList.Count, "Geographic")) return;

            _extDelete.DeleteFiSpecies(CurrentTbl69FiSpecies);

            Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromSpeciesgroupIdOrderBy<Tbl69FiSpecies>(CurrentTbl69FiSpecies.SpeciesgroupId);

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.MoveCurrentToFirst();
        }

        private void ExecuteSaveFiSpecies(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl69FiSpecies)) return;

            CurrentTbl69FiSpecies.SpeciesgroupId = CurrentTbl68Speciesgroup.SpeciesgroupId;
            Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromSpeciesgroupIdOrderBy<Tbl69FiSpecies>(CurrentTbl69FiSpecies.SpeciesgroupId);

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.MoveCurrentToFirst();
        }

        #endregion [Public Methods  Connect ==> Tbl69FiSpecies]                                                                                                                                            



        //    Part 5    


        #region [Public Commands Connect ==> Tbl72PlSpecies]                 

        private RelayCommand _addPlSpeciesCommand;

        public ICommand AddPlSpeciesCommand => _addPlSpeciesCommand ??= new RelayCommand(delegate { ExecuteAddPlSpecies(null); });

        private RelayCommand _copyPlSpeciesCommand;

        public ICommand CopyPlSpeciesCommand => _copyPlSpeciesCommand ??= new RelayCommand(delegate { ExecuteCopyPlSpecies(null); });

        private RelayCommand _deletePlSpeciesCommand;

        public ICommand DeletePlSpeciesCommand => _deletePlSpeciesCommand ??= new RelayCommand(delegate { ExecuteDeletePlSpecies(SearchSpeciesgroupName); });

        private RelayCommand _savePlSpeciesCommand;

        public ICommand SavePlSpeciesCommand => _savePlSpeciesCommand ??= new RelayCommand(delegate { ExecuteSavePlSpecies(SearchSpeciesgroupName); });
        #endregion [Public Commands Connect ==> Tbl72PlSpecies]                

        #region [Public Methods Connect ==> Tbl72PlSpecies]                        

        private void ExecuteAddPlSpecies(object o)
        {
            Tbl72PlSpeciessesList ??= new ObservableCollection<Tbl72PlSpecies>();
            Tbl72PlSpeciessesList.Insert(0, new Tbl72PlSpecies { PlSpeciesName = CultRes.StringsRes.DatasetNew });

            Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("Genus");
            Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("Speciesgroup");

            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            PlSpeciessesView.MoveCurrentToFirst();
        }

        private void ExecuteCopyPlSpecies(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl72PlSpecies)) return;

            Tbl72PlSpeciessesList = _extCrud.CopyPlSpecies(CurrentTbl72PlSpecies);

            // evtl verbundene tabellen-Datensätze auch kopieren Names, Images, Synonyms und Geographics

            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            PlSpeciessesView.MoveCurrentToFirst();
        }

        private void ExecuteDeletePlSpecies(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl72PlSpecies)) return;
            //check if in Tbl72PlSpeciesses connected datasets no delete possible, Names, Images, Synonyms and Geographics delete and than return
            Tbl78NamesList = _extCrud.SearchForConnectedDatasetsWithPlSpeciesIdInTableName(CurrentTbl72PlSpecies.PlSpeciesId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl78NamesList.Count, "Name")) return;
            Tbl81ImagesList = _extCrud.SearchForConnectedDatasetsWithPlSpeciesIdInTableImage(CurrentTbl72PlSpecies.PlSpeciesId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl81ImagesList.Count, "Image")) return;
            Tbl84SynonymsList = _extCrud.SearchForConnectedDatasetsWithPlSpeciesIdInTableSynonym(CurrentTbl72PlSpecies.PlSpeciesId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl84SynonymsList.Count, "Synonym")) return;
            Tbl87GeographicsList = _extCrud.SearchForConnectedDatasetsWithPlSpeciesIdInTableGeographic(CurrentTbl72PlSpecies.PlSpeciesId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl87GeographicsList.Count, "Geographic")) return;

            _extDelete.DeletePlSpecies(CurrentTbl72PlSpecies);

            Tbl72PlSpeciessesList = _extCrud.GetPlSpeciessesCollectionFromSpeciesgroupIdOrderBy<Tbl72PlSpecies>(CurrentTbl72PlSpecies.SpeciesgroupId);

            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            PlSpeciessesView.MoveCurrentToFirst();
        }

        private void ExecuteSavePlSpecies(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl72PlSpecies)) return;

            CurrentTbl72PlSpecies.SpeciesgroupId = CurrentTbl68Speciesgroup.SpeciesgroupId;

            _extSave.SavePlSpecies(CurrentTbl72PlSpecies);

            Tbl72PlSpeciessesList = _extCrud.GetPlSpeciessesCollectionFromSpeciesgroupIdOrderBy<Tbl72PlSpecies>(CurrentTbl72PlSpecies.SpeciesgroupId);

            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            PlSpeciessesView.MoveCurrentToFirst();
        }
        #endregion [Public Methods  Connect ==> Tbl72PlSpecies]                                                                                        


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

            Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromSpeciesgroupIdOrderBy<Tbl69FiSpecies>(CurrentTbl68Speciesgroup.SpeciesgroupId);
            Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("genus");
            Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("Speciesgroup");

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();

        }

        #endregion "Public Method Connected Tables by DoubleClick"     



        //    Part 10    


        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        private int _selectedMainSubRefTabIndex;
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
                    if (CurrentTbl68Speciesgroup != null)
                    {
                        Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromSpeciesgroupIdOrderBy<Tbl69FiSpecies>(CurrentTbl68Speciesgroup.SpeciesgroupId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("Genus");
                        Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("Speciesgroup");

                        FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                        FiSpeciessesView.Refresh();
                    }
                    SelectedDetailTabIndex = 1;
                }

                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl68Speciesgroup != null)
                    {
                        Tbl72PlSpeciessesList = _extCrud.GetPlSpeciessesCollectionFromGenusIdOrderBy<Tbl72PlSpecies>(CurrentTbl68Speciesgroup.SpeciesgroupId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("Genus");
                        Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("Speciesgroup");

                        PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
                        PlSpeciessesView.Refresh();
                    }
                    SelectedDetailTabIndex = 2;
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
                    if (CurrentTbl68Speciesgroup != null)
                    {

                    }
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 1)
                {
                    if (CurrentTbl68Speciesgroup != null)
                    {
                        Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromSpeciesgroupIdOrderBy<Tbl69FiSpecies>(CurrentTbl68Speciesgroup.SpeciesgroupId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("Genus");
                        Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("Speciesgroup");

                        FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                        FiSpeciessesView.Refresh();
                    }
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 2)
                {
                    if (CurrentTbl68Speciesgroup != null)
                    {
                        Tbl72PlSpeciessesList = _extCrud.GetPlSpeciessesCollectionFromGenusIdOrderBy<Tbl72PlSpecies>(CurrentTbl68Speciesgroup.SpeciesgroupId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("Genus");
                        Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("Speciesgroup");

                        PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
                        PlSpeciessesView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
                }

            }
        }
        #endregion "Public Commands to open Detail TabItems"          


        //    Part 11    



        #region "Public Properties Tbl78Name"

        public ICollectionView NamesView;
        private Tbl78Name CurrentTbl78Name => NamesView?.CurrentItem as Tbl78Name;

        private ObservableCollection<Tbl78Name> _tbl78NamesList;

        public ObservableCollection<Tbl78Name> Tbl78NamesList
        {
            get => _tbl78NamesList;
            set { _tbl78NamesList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl81Image"

        public ICollectionView ImagesView;
        private Tbl81Image CurrentTbl81Image => ImagesView?.CurrentItem as Tbl81Image;

        private ObservableCollection<Tbl81Image> _tbl81ImagesList;

        public ObservableCollection<Tbl81Image> Tbl81ImagesList
        {
            get => _tbl81ImagesList;
            set { _tbl81ImagesList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl84Synonym"

        private string _searchSynonymName = string.Empty;

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

        #region "Public Properties Tbl87Geographic"

        private string _searchGeographicName = string.Empty;

        public string SearchGeographicName
        {
            get => _searchGeographicName;
            set { _searchGeographicName = value; RaisePropertyChanged(""); }
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

        #region "Public Properties Tbl68Speciesgroup"

        private string _searchSpeciesgroupName = string.Empty;

        public string SearchSpeciesgroupName
        {
            get => _searchSpeciesgroupName;
            set { _searchSpeciesgroupName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView SpeciesgroupsView;
        private Tbl68Speciesgroup CurrentTbl68Speciesgroup => SpeciesgroupsView?.CurrentItem as Tbl68Speciesgroup;

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsList
        {
            get => _tbl68SpeciesgroupsList;
            set { _tbl68SpeciesgroupsList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
        {
            get => _tbl68SpeciesgroupsAllList;
            set { _tbl68SpeciesgroupsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   


        #region "Public Properties Tbl66Genus"

        private ObservableCollection<Tbl66Genus> _tbl66GenussesList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesList
        {
            get => _tbl66GenussesList;
            set { _tbl66GenussesList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList;
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(""); }
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
        #endregion "Public Properties"     


    }
}
