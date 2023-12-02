using System.Collections.ObjectModel;
using System.Windows.Input;
using ATIS.WinUi.Contracts.Services;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ATIS.WinUi.ViewModels.Database;

    public class Tbl90RefSourcesViewModel : ObservableObject
    {
        #region [Private Data Members]
        private readonly IDataService _dataService;
        public ObservableCollection<Tbl90RefSource> RefSourceItems { get; } = new();

        private readonly AllDialogs _allDialogs = new();
        #endregion [Private Data Members]           

        #region [Constructor]
        public Tbl90RefSourcesViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        #endregion [Constructor]  


        //    Part 1    



        #region [Commands RefSource]

        public ICommand GetRefSourcesByNameOrIdCommand => new RelayCommand(execute: delegate
        {
            var task = GetRefSourcesByNameOrId(searchName: SearchRefSourceName);
        });

        public ICommand AddRefSourceCommand => new RelayCommand(execute: delegate { var task = AddRefSource(o: null); });
        public ICommand CopyRefSourceCommand => new RelayCommand(execute: delegate { var task = CopyRefSource(o: null); });

        public ICommand DeleteRefSourceCommand => new RelayCommand(execute: delegate { var task = DeleteRefSource(SearchRefSourceName); });

        public ICommand SaveRefSourceCommand => new RelayCommand(execute: delegate { var task = SaveRefSource(SearchRefSourceName); });
        public ICommand RefreshRefSourceServerCommand => new RelayCommand(execute: delegate { var task = RefreshRefSourceServer(SearchRefSourceName); });

        #endregion [Commands RefSource]       

        #region [Methods RefSource]

        private async Task GetRefSourcesByNameOrId(string searchName)
        {
            RefSourceStartModify();
            Tbl90RefSourcesList?.Clear();

            RefSourceItems.Clear();

            Tbl90RefSourcesList ??= new ObservableCollection<Tbl90RefSource>();
            Tbl90RefSourcesList = await _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceNameFromSearchNameOrId(searchName);

            if (Tbl90RefSourcesList.Count == 0)
            {
                await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
                return;
            }
            RefSourceDataSetCount = Tbl90RefSourcesList.Count;
            RefreshRefSourceItems();
        }

        private Task AddRefSource(object? o)
        {
            RefSourceStartEdit();
            RefSourceStartNew();

            Tbl90RefSourcesList ??= new ObservableCollection<Tbl90RefSource>();
            Tbl90RefSourcesList.Insert(0, new Tbl90RefSource { RefSourceName = "New" });

            RefreshRefSourceItems();
            return Task.CompletedTask;
        }

        private async Task CopyRefSource(object? o)
        {
            if (RefSourceSelected == null)
            {
                await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
                return;
            }
            RefSourceStartEdit();
            RefSourceStartNew();
            Tbl90RefSourcesList ??= new ObservableCollection<Tbl90RefSource>();
            Tbl90RefSourcesList = await _dataService.CopyRefSource(RefSourceSelected);
            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            RefreshRefSourceItems();
        }

        private async Task DeleteRefSource(string searchName)
        {
            if (RefSourceSelected == null)
            {
                await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
                return;
            }

            if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(RefSourceSelected.RefSourceName!))
            {
                //necessary to delete before
                var ret = _dataService.DeleteRefSource(RefSourceSelected);
                if (!await ret)
                {
                    return;
                }

                Tbl90RefSourcesList = await _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceNameFromSearchNameOrId(searchName);

                RefSourceDataSetCount = Tbl90RefSourcesList.Count;
                RefreshRefSourceItems();
            }
        }

        private async Task SaveRefSource(string searchName)
        {
            if (string.IsNullOrEmpty(RefSourceSelected.RefSourceName))
            {
                await _allDialogs.NameRequiredWarnMessageDialogAsync();
                return;
            }
            if (RefSourceSelected == null)
            {
                await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
                return;
            }
            RefSourceSelected ??= Tbl90RefSourcesList[0];
            var iNdx = Tbl90RefSourcesList.IndexOf(Tbl90RefSourcesList.First(t =>
                 t.RefSourceName == RefSourceSelected.RefSourceName));

            var ret = _dataService.SaveRefSource(RefSourceSelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(searchName))
            {
                Tbl90RefSourcesList = await _dataService.GetLastDatasetInTbl90RefSources();
                RefreshRefSourceItems();
            }
            else
            {
                if (RefSourceSelected.RefSourceId == 0) //new
                {
                    Tbl90RefSourcesList = await _dataService.GetLastDatasetInTbl90RefSources();
                    RefreshRefSourceItems();
                }
                else
                {
                    Tbl90RefSourcesList = await _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceNameFromSearchNameOrId(searchName);
                    //   Index Position ?
                    if (iNdx < Tbl90RefSourcesList.Count)
                    {
                        RefSourceItems.Clear();
                        foreach (var item in Tbl90RefSourcesList)
                        {
                            RefSourceItems.Add(item);
                        }

                        RefSourceSelected = Tbl90RefSourcesList[iNdx];
                    }
                }
            }
            RefSourceDataSetCount = Tbl90RefSourcesList.Count;
            RefSourceCancelEditsAsync();
        }

        private async Task RefreshRefSourceServer(string searchName)
        {
            Tbl90RefSourcesList ??= new ObservableCollection<Tbl90RefSource>();
            Tbl90RefSourcesList = await _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceNameFromSearchNameOrId(searchName);

            RefSourceDataSetCount = Tbl90RefSourcesList.Count;
            RefreshRefSourceItems();
        }

        public void RefSourceStartEdit() => IsInEdit = true;
        public void RefSourceStartModify() => IsModified = true;
        public void RefSourceStartNew() => IsNewRefSource = true;
        public event EventHandler AddNewRefSourceCanceled = null!;
        public void RefSourceCancelEditsAsync()
        {
            if (IsNewRefSource)
            {
                AddNewRefSourceCanceled?.Invoke(this, EventArgs.Empty);
                IsInEdit = false;
                IsNewRefSource = false;
            }
            else
            {
                IsInEdit = false;
                IsModified = false;
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




        //    Part 11 


        #region Selected Properties


        private Tbl90RefSource _refSourceSelected = null!;

        public Tbl90RefSource RefSourceSelected
        {
            get => _refSourceSelected;
            set => SetProperty(ref _refSourceSelected, value);
        }


        #endregion

        #region Refresh Properties


        private void RefreshRefSourceItems()
        {
            RefSourceItems.Clear();
            foreach (var item in Tbl90RefSourcesList)
            {
                RefSourceItems.Add(item);
            }
            if (Tbl90RefSourcesList.Count == 0)
            {
                return;
            }

            if (RefSourceSelected == null && Tbl90RefSourcesList.Count != 0)
            {
                RefSourceSelected = RefSourceItems.First();
            }
        }
        #endregion Refresh Properties

        #region Public Properties  

        private int _refSourceDataSetCount;
        public int RefSourceDataSetCount
        {
            get => _refSourceDataSetCount;
            set
            {
                _refSourceDataSetCount = value; OnPropertyChanged();
            }
        }

        private string _searchRefSourceName = "";

        public string SearchRefSourceName
        {
            get => _searchRefSourceName;
            set
            {
                _searchRefSourceName = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<Tbl90RefSource> _tbl90RefSourcesList = null!;

        public ObservableCollection<Tbl90RefSource> Tbl90RefSourcesList
        {
            get => _tbl90RefSourcesList;
            set
            {
                _tbl90RefSourcesList = value; OnPropertyChanged();
            }
        }

        //---------------------------------------------------
        public bool IsModified
        {
            get; set;
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private bool _isInEdit = false;
        public bool IsInEdit
        {
            get => _isInEdit;
            set => SetProperty(ref _isInEdit, value);
        }

        private bool _isNewRefSource;
        public bool IsNewRefSource
        {
            get => _isNewRefSource;
            set => SetProperty(ref _isNewRefSource, value);
        }



        #endregion Public Properties

    }

