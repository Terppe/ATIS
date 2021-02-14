using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;


using System.Windows.Data;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;

//    RefSourcesViewModel Skriptdatum:   09.02.2021  10:32    

namespace ATIS.Ui.Views.Database.D90RefSource
{

    public class RefSourcesViewModel : ViewModelBase
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

        public RefSourcesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                // Code runs "for real" 
                Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource>();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          


        //    Part 1    



        #region [Commands RefSource]

        private RelayCommand _getRefSourcesByNameOrIdCommand;
        public ICommand GetRefSourcesByNameOrIdCommand => _getRefSourcesByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetRefSourcesByNameOrId(SearchRefSourceName); });

        private RelayCommand _addRefSourceCommand;
        public ICommand AddRefSourceCommand => _addRefSourceCommand ??= new RelayCommand(delegate { ExecuteAddRefSource(null); });

        private RelayCommand _copyRefSourceCommand;
        public ICommand CopyRefSourceCommand => _copyRefSourceCommand ??= new RelayCommand(delegate { ExecuteCopyRefSource(null); });

        private RelayCommand _deleteRefSourceCommand;
        public ICommand DeleteRefSourceCommand => _deleteRefSourceCommand ??= new RelayCommand(delegate { ExecuteDeleteRefSource(SearchRefSourceName); });

        private RelayCommand _saveRefSourceCommand;
        public ICommand SaveRefSourceCommand => _saveRefSourceCommand ??= new RelayCommand(delegate { ExecuteSaveRefSource(SearchRefSourceName); });

        #endregion [Commands RefSource]       


        #region [Methods RefSource]

        private void ExecuteGetRefSourcesByNameOrId(string searchName)
        {
            if (Tbl90RefSourcesList == null)
                Tbl90RefSourcesList ??= new ObservableCollection<Tbl90RefSource>();
            else
                Tbl90RefSourcesList.Clear();

            Tbl90RefSourcesList = _extCrud.GetRefSourcesCollectionFromSearchNameOrIdOrderBy<Tbl90RefSource>(searchName);

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl90RefSourcesList.Count)) return;

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
        }

        private void ExecuteAddRefSource(object o)
        {
            if (Tbl90RefSourcesList == null)
                Tbl90RefSourcesList ??= new ObservableCollection<Tbl90RefSource>();
            else
                Tbl90RefSourcesList.Clear();

            Tbl90RefSourcesList.Insert(0, new Tbl90RefSource { RefSourceName = CultRes.StringsRes.DatasetNew });

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.MoveCurrentToFirst();
        }

        private void ExecuteCopyRefSource(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90RefSource)) return;

            Tbl90RefSourcesList = _extCrud.CopyRefSource(CurrentTbl90RefSource);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteRefSource(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90RefSource)) return;

            _extDelete.DeleteRefSource(CurrentTbl90RefSource);

            Tbl90RefSourcesList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl90RefSource>(searchName, "RefSource");
            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.MoveCurrentToLast();
        }

        private void ExecuteSaveRefSource(string searchName)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90RefSource)) return;

            _position = RefSourcesView.CurrentPosition;

            var ret = _extSave.SaveRefSource(CurrentTbl90RefSource);

            if (ret != true)
            {
                RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
                RefSourcesView.Refresh();
                return;
            }

            if (_position == 0) //new
            {
                Tbl90RefSourcesList = _extCrud.GetLastRefSourcesDatasetOrderById();
                RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
                RefSourcesView.MoveCurrentToFirst();
            }
            else
            {
                Tbl90RefSourcesList = _extCrud.GetRefSourcesCollectionFromSearchNameOrIdOrderBy<Tbl90RefSource>(searchName);
                RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
                RefSourcesView.MoveCurrentToPosition(_position);
            }
        }
        #endregion [Methods RefSource]                



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


        #region "Public Properties Tbl90RefSource"

        private string _searchRefSourceName = "";
        public string SearchRefSourceName
        {
            get => _searchRefSourceName;
            set { _searchRefSourceName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView RefSourcesView;
        private Tbl90RefSource CurrentTbl90RefSource => RefSourcesView?.CurrentItem as Tbl90RefSource;

        private ObservableCollection<Tbl90RefSource> _tbl90RefSourcesList;
        public ObservableCollection<Tbl90RefSource> Tbl90RefSourcesList
        {
            get => _tbl90RefSourcesList;
            set { _tbl90RefSourcesList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl90RefSource> _tbl90RefSourcesAllList;
        public ObservableCollection<Tbl90RefSource> Tbl90RefSourcesAllList
        {
            get => _tbl90RefSourcesAllList;
            set { _tbl90RefSourcesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   


    }
}
