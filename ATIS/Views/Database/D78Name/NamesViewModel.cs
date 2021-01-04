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

using System.Collections.Generic;

//    NamesViewModel Skriptdatum:  22.01.2019  10:32    

namespace ATIS.Ui.Views.Database.D78Name
{

    public class NamesViewModel : ViewModelBase
    {
        // Version with Generic Unit Of Work and AtisDbContext for general use   

        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(typeof(NamesViewModel));
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly CrudFunctions _extCrud = new CrudFunctions();

        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private readonly GenericMessageBoxes<Tbl78Name> _genNameMessageBoxes = new GenericMessageBoxes<Tbl78Name>();
        private readonly GenericMessageBoxes<Tbl69FiSpecies> _genFiSpeciesMessageBoxes = new GenericMessageBoxes<Tbl69FiSpecies>();
        private readonly GenericMessageBoxes<Tbl72PlSpecies> _genPlSpeciesMessageBoxes = new GenericMessageBoxes<Tbl72PlSpecies>();
        private int _position;

        #endregion [Private Data Members]               

        #region [Constructor]

        public NamesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                GetValueLanguage();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          


        //    Part 1    



        #region [Commands Name]

        private RelayCommand _getNamesByNameOrIdCommand;
        public ICommand GetNamesByNameOrIdCommand => _getNamesByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetNamesByNameOrId(SearchNameName); });

        private RelayCommand _addNameCommand;
        public ICommand AddNameCommand => _addNameCommand ??= new RelayCommand(delegate { ExecuteAddName(null); });

        private RelayCommand _copyNameCommand;
        public ICommand CopyNameCommand => _copyNameCommand ??= new RelayCommand(delegate { ExecuteCopyName(null); });

        private RelayCommand _deleteNameCommand;
        public ICommand DeleteNameCommand => _deleteNameCommand ??= new RelayCommand(delegate { ExecuteDeleteName(SearchNameName); });

        private RelayCommand _saveNameCommand;
        public ICommand SaveNameCommand => _saveNameCommand ??= new RelayCommand(delegate { ExecuteSaveName(SearchNameName); });

        #endregion [Commands Name]       

        #region [Methods Name]

        private void ExecuteGetNamesByNameOrId(string searchName)
        {
            Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("fispecies"); 
            Tbl72PlSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl72PlSpecies>("plspecies");

            Tbl78NamesList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl78Name>(SearchNameName, "name");

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl78NamesList.Count)) return;

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 2;

            NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            NamesView.Refresh();
        }

        private void ExecuteAddName(object o)
        {
            Tbl78NamesList = new ObservableCollection<Tbl78Name>();
            Tbl78NamesList.Insert(0, new Tbl78Name { NameName = CultRes.StringsRes.DatasetNew });

            Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("fispecies");
            Tbl72PlSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl72PlSpecies>("plspecies");

            NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            NamesView.MoveCurrentToFirst();
        }

        private void ExecuteCopyName(object o)
        {
            if (_genNameMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl78Name)) return;

            Tbl78NamesList = _extCrud.CopyName(CurrentTbl78Name);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
            NamesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteName(string searchName)
        {
            if (_genNameMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl78Name)) return;

            try
            {
                var name = _uow.Tbl78Names.GetById(CurrentTbl78Name.NameId);
                if (name != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl78Name.NameName)) return;

                    _extCrud.DeleteName(name);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl78Name.NameName);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl78Name.NameName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGetNamesByNameOrId(searchName);

            NamesView.MoveCurrentToFirst();
        }

        private void ExecuteSaveName(string searchName)
        {
            if (_genNameMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl78Name)) return;

            //Combobox select FiSpeciesID  may be not 0
            if (CurrentTbl78Name.FiSpeciesId == 0)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                var name = _uow.Tbl78Names.GetById(CurrentTbl78Name.NameId);
                //   var phylum = _context.Tbl78Names.AsNoTracking().FirstOrDefault(a=>a.NameId == CurrentTbl78Name.NameId);
                //          _context.Entry(name).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl78Name.NameName))
                    return;

                if (CurrentTbl78Name.NameId == 0)
                    name = _extCrud.NameAdd(CurrentTbl78Name);
                else
                    name = _extCrud.NameUpdate(name, CurrentTbl78Name);

                _position = NamesView.CurrentPosition;

                try
                {
                    _extCrud.NameSave(name, CurrentTbl78Name);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, CurrentTbl78Name.NameId == 0
                    ? "DatasetNew"
                    : CurrentTbl78Name.NameName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            ExecuteGetNamesByNameOrId(searchName);
            NamesView.MoveCurrentToPosition(_position);
        }

        #endregion [Methods Name]



        //    Part 2    


        #region "Public Commands Connect <== Tbl69FiSpecies"                 


        private RelayCommand _saveFiSpeciesCommand;

        public ICommand SaveFiSpeciesCommand => _saveFiSpeciesCommand ??= new RelayCommand(delegate { ExecuteSaveFiSpecies(SearchNameName); });

        private void ExecuteSaveFiSpecies(string searchName)
        {
            if (_genFiSpeciesMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl69FiSpecies)) return;

            try
            {
                var fispecies = _uow.Tbl69FiSpeciesses.GetById(CurrentTbl69FiSpecies.FiSpeciesId);

                if (CurrentTbl69FiSpecies.FiSpeciesId == 0)
                    fispecies = _extCrud.FiSpeciesAdd(CurrentTbl69FiSpecies);
                else
                    fispecies = _extCrud.FiSpeciesUpdate(fispecies, CurrentTbl69FiSpecies);

                _position = NamesView.CurrentPosition;

                var cap = CurrentTbl69FiSpecies.FiSpeciesName;
                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(cap)) return;

                try
                {
                    _extCrud.FiSpeciesSave(fispecies, CurrentTbl69FiSpecies);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(),
                            CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl69FiSpecies.FiSpeciesId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl69FiSpecies.FiSpeciesName);
            }

            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            ExecuteGetNamesByNameOrId(searchName);
            NamesView.MoveCurrentToPosition(_position);
        }

        #endregion "Public Commands"                  



        //    Part 3    


        #region "Public Commands Connect <== Tbl72PlSpecies"                 

        private RelayCommand _savePlSpeciesCommand;

        public ICommand SavePlSpeciesCommand => _savePlSpeciesCommand ??= new RelayCommand(delegate { ExecuteSavePlSpecies(SearchNameName); });

        private void ExecuteSavePlSpecies(string searchName)
        {
            if (_genPlSpeciesMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl72PlSpecies)) return;

            try
            {
                var plspecies = _uow.Tbl72PlSpeciesses.GetById(CurrentTbl72PlSpecies.PlSpeciesId);

                if (CurrentTbl72PlSpecies.PlSpeciesId == 0)
                    plspecies = _extCrud.PlSpeciesAdd(CurrentTbl72PlSpecies);
                else
                    plspecies = _extCrud.PlSpeciesUpdate(plspecies, CurrentTbl72PlSpecies);

                _position = NamesView.CurrentPosition;

                var cap = CurrentTbl72PlSpecies.PlSpeciesName;
                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(cap)) return;

                try
                {
                    _extCrud.PlSpeciesSave(plspecies, CurrentTbl72PlSpecies);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(),
                            CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl72PlSpecies.PlSpeciesId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl72PlSpecies.PlSpeciesName);
            }

            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            ExecuteGetNamesByNameOrId(searchName);
            NamesView.MoveCurrentToPosition(_position);
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
            Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("genus");
            Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("speciesgroup");

            Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromFiSpeciesIdOrderBy<Tbl69FiSpecies>(CurrentTbl78Name.FiSpeciesId);

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
                    if (CurrentTbl78Name != null)
                    {
                        Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromFiSpeciesIdOrderBy<Tbl69FiSpecies>(CurrentTbl78Name.FiSpeciesId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("genus");
                        Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("speciesgroup");

                        FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                        FiSpeciessesView.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }

                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl78Name != null)
                    {
                        Tbl72PlSpeciessesList = _extCrud.GetPlSpeciessesCollectionFromPlSpeciesIdOrderBy<Tbl72PlSpecies>(CurrentTbl78Name.PlSpeciesId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("genus");
                        Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("speciesgroup");

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
                    if (CurrentTbl78Name != null)
                    {
                        Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromFiSpeciesIdOrderBy<Tbl69FiSpecies>(CurrentTbl78Name.FiSpeciesId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("genus");
                        Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("speciesgroup");

                        FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                        FiSpeciessesView.Refresh();
                    }
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 1)
                {
                    if (CurrentTbl78Name != null)
                    {
                        Tbl72PlSpeciessesList = _extCrud.GetPlSpeciessesCollectionFromPlSpeciesIdOrderBy<Tbl72PlSpecies>(CurrentTbl78Name.PlSpeciesId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("genus");
                        Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("speciesgroup");

                        PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
                        PlSpeciessesView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 2)
                {
                    if (CurrentTbl78Name != null)
                    {
                        Tbl78NamesList = _extCrud.GetNamesCollectionFromPlSpeciesIdOrderBy<Tbl78Name>(CurrentTbl78Name.PlSpeciesId);

                        Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("fispecies");
                        Tbl72PlSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl72PlSpecies>("plspecies");

                        NamesView = CollectionViewSource.GetDefaultView(Tbl78NamesList);
                        NamesView.Refresh();
                    }
                    SelectedMainTabIndex = 0;
                }
            }
        }

        #endregion "Public Commands to open Detail TabItems"          


        //    Part 11    


        #region "Public Properties Tbl78Name"

        private string _searchNameName = "";
        public string SearchNameName
        {
            get => _searchNameName;
            set { _searchNameName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView NamesView;
        private Tbl78Name CurrentTbl78Name => NamesView?.CurrentItem as Tbl78Name;

        private ObservableCollection<Tbl78Name> _tbl78NamesList;
        public ObservableCollection<Tbl78Name> Tbl78NamesList
        {
            get => _tbl78NamesList;
            set { _tbl78NamesList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl78Name> _tbl78NamesAllList;
        public ObservableCollection<Tbl78Name> Tbl78NamesAllList
        {
            get => _tbl78NamesAllList;
            set { _tbl78NamesAllList = value; RaisePropertyChanged(""); }
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

        #region Language

        private void GetValueLanguage()
        {
            _languages = new List<Language>()
            {
                new Language {Name = "GER"},
                new Language {Name = "ENG"},
                new Language {Name = "FRE"},
                new Language {Name = "POR"}
            };

            _selectedLanguage = new Language();
        }

        private List<Language> _languages;

        public List<Language> Languages
        {
            get => _languages;
            set { _languages = value; RaisePropertyChanged(""); }
        }

        private Language _selectedLanguage;

        public Language SelectedLanguage
        {
            get => _selectedLanguage;
            set { _selectedLanguage = value; RaisePropertyChanged(""); }
        }

        public class Language
        {
            public string Name { get; set; }
        }

        #endregion Language  


    }
}
