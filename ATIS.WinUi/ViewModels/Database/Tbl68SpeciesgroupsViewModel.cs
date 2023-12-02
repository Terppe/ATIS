
using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using CommunityToolkit.Mvvm.ComponentModel;

//    Tbl68SpeciesgroupsViewModel Skriptdatum:  14.04.2023  10:32    

namespace ATIS.WinUi.ViewModels.Database;

public class Tbl68SpeciesgroupsViewModel : ObservableObject
{

    #region [Private Data Members]

    private readonly IDataService _dataService;

    public ObservableCollection<Tbl68Speciesgroup?> SpeciesgroupItems { get; } = new();

    public ObservableCollection<Tbl69FiSpecies> FiSpeciesItems { get; } = new();

    public ObservableCollection<Tbl72PlSpecies> PlSpeciesItems { get; } = new();

    private readonly AllDialogs _allDialogs = new();

    #endregion [Private Data Members]

    #region [Constructor]

    public Tbl68SpeciesgroupsViewModel(IDataService dataService)
    {
        _dataService = dataService;
        SelectedMainDetailTabIndex = 0; //Tab Datagrid
        GetAllCollections();
    }

    private void GetAllCollections()
    {
        Tbl68SpeciesgroupsAllList ??= new ObservableCollection<Tbl68Speciesgroup>();
        Tbl68SpeciesgroupsAllList = _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroup();
        Tbl66GenussesAllList ??= new ObservableCollection<Tbl66Genus>();
        Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();
    }

    #endregion [Constructor]


    //    Part 1    



    #region [Commands Speciesgroup]

    public ICommand GetSpeciesgroupsByNameOrIdCommand => new RelayCommand(execute: delegate
        { var task = GetSpeciesgroupsByNameOrId_Executed(SearchSpeciesgroupName); });
    public ICommand AddSpeciesgroupCommand => new RelayCommand<string>(AddSpeciesgroup_Executed);
    public ICommand CopySpeciesgroupCommand => new RelayCommand<string>(CopySpeciesgroup_Executed);
    public ICommand DeleteSpeciesgroupCommand => new RelayCommand(execute: delegate { DeleteSpeciesgroup_Executed(SearchSpeciesgroupName); });
    public ICommand SaveSpeciesgroupCommand => new RelayCommand(execute: delegate { var task = SaveSpeciesgroup_Executed(SearchSpeciesgroupName); });
    public ICommand RefreshSpeciesgroupServerCommand => new RelayCommand(execute: delegate { RefreshSpeciesgroupServer_Executed(SearchSpeciesgroupName); });

    #endregion [Commands Speciesgroup]

    #region [Methods Speciesgroup]

    private async Task GetSpeciesgroupsByNameOrId_Executed(string searchName)
    {
        SpeciesgroupStartModify();
        Tbl68SpeciesgroupsList?.Clear();
        Tbl69FiSpeciessesList?.Clear();
        Tbl72PlSpeciessesList?.Clear();

        SpeciesgroupItems.Clear();
        FiSpeciesItems.Clear();
        PlSpeciesItems.Clear();

        Tbl68SpeciesgroupsList ??= new ObservableCollection<Tbl68Speciesgroup>();
        Tbl68SpeciesgroupsList = await _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroupFromSearchNameOrId(searchName);

        if (Tbl68SpeciesgroupsList.Count == 0)
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }

        SpeciesgroupDataSetCount = Tbl68SpeciesgroupsList.Count;
        RefreshSpeciesgroupItems();

        SelectedMainDetailTabIndex = 0;
    }

    private void AddSpeciesgroup_Executed(string? parm)
    {
        SpeciesgroupStartEdit();
        SpeciesgroupStartNew();
        Tbl68SpeciesgroupsList ??= new ObservableCollection<Tbl68Speciesgroup>();
        Tbl68SpeciesgroupsList.Insert(0, new Tbl68Speciesgroup { SpeciesgroupName = "New" });

        RefreshSpeciesgroupItems();
    }

    private async void CopySpeciesgroup_Executed(string? parm)
    {
        SpeciesgroupStartEdit();
        SpeciesgroupStartNew();

        Tbl68SpeciesgroupsList = await _dataService.CopySpeciesgroup(SpeciesgroupSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshSpeciesgroupItems();
    }

    private async void DeleteSpeciesgroup_Executed(string? parm)
    {

        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(SpeciesgroupSelected!.SpeciesgroupName!))
        {
            var ret = _dataService.DeleteSpeciesgroup(SpeciesgroupSelected);
            if (!await ret)
            {
                return;
            }

            Tbl68SpeciesgroupsList = await _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroupFromSearchNameOrId(parm!);

            SpeciesgroupDataSetCount = Tbl68SpeciesgroupsList.Count;
            RefreshSpeciesgroupItems();
        }
    }

    private async Task SaveSpeciesgroup_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(SpeciesgroupSelected?.SpeciesgroupName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }

        if (Tbl68SpeciesgroupsList != null)
        {

            var iNdx = Tbl68SpeciesgroupsList.IndexOf(Tbl68SpeciesgroupsList.First(t =>
                t.SpeciesgroupName == SpeciesgroupSelected.SpeciesgroupName));

            var ret = _dataService.SaveSpeciesgroup(SpeciesgroupSelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl68SpeciesgroupsList = await _dataService.GetLastDatasetInTbl68Speciesgroups();
                RefreshSpeciesgroupItems();
            }
            else
            {
                if (SpeciesgroupSelected.SpeciesgroupId == 0) //new
                {
                    Tbl68SpeciesgroupsList = await _dataService.GetLastDatasetInTbl68Speciesgroups();
                    RefreshSpeciesgroupItems();
                }
                else
                {
                    Tbl68SpeciesgroupsList = await _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroupFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl68SpeciesgroupsList.Count)
                    {
                        SpeciesgroupItems.Clear();
                        foreach (var item in Tbl68SpeciesgroupsList)
                        {
                            SpeciesgroupItems.Add(item);
                        }

                        SpeciesgroupSelected = Tbl68SpeciesgroupsList[iNdx];
                    }
                }
            }

            SpeciesgroupDataSetCount = Tbl68SpeciesgroupsList.Count;
            SpeciesgroupCancelEditsAsync();
        }
    }

    private async void RefreshSpeciesgroupServer_Executed(string? parm)
    {
        Tbl68SpeciesgroupsList ??= new ObservableCollection<Tbl68Speciesgroup>();
        Tbl68SpeciesgroupsList = await _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroupFromSearchNameOrId(parm!);

        SpeciesgroupDataSetCount = Tbl68SpeciesgroupsList.Count;
        RefreshSpeciesgroupItems();
    }

    public void SpeciesgroupStartEdit() => IsInEdit = true;
    public void SpeciesgroupStartModify() => IsModified = true;
    public void SpeciesgroupStartNew() => IsNewSpeciesgroup = true;
    public event EventHandler AddNewSpeciesgroupCanceled = null!;
    public void SpeciesgroupCancelEditsAsync()
    {
        if (IsNewSpeciesgroup)
        {
            AddNewSpeciesgroupCanceled?.Invoke(this, EventArgs.Empty);
            IsInEdit = false;
            IsNewSpeciesgroup = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }


    #endregion [Methods Speciesgroup]     




    //    Part 2    




    //    Part 3    





    //    Part 4    


    #region [Public Commands Connect ==> Tbl69FiSpecies]       

    public ICommand AddFiSpeciesCommand => new RelayCommand<string>(AddFiSpecies_Executed);
    public ICommand CopyFiSpeciesCommand => new RelayCommand<string>(CopyFiSpecies_Executed);
    public ICommand DeleteFiSpeciesCommand => new RelayCommand<string>(DeleteFiSpecies_Executed);
    public ICommand SaveFiSpeciesCommand => new RelayCommand<string>(SaveFiSpecies_Executed);
    public ICommand RefreshFiSpeciesServerCommand => new RelayCommand<string>(RefreshFiSpeciesServer_Executed);

    #endregion [Public Commands Connect ==> Tbl69FiSpecies]    

    #region [Public Methods Connect ==> Tbl69FiSpecies]                   

    private void AddFiSpecies_Executed(string? parm)
    {
        FiSpeciesStartEdit();
        FiSpeciesStartNew();
        Tbl66GenussesAllList ??= new ObservableCollection<Tbl66Genus>();
        Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();
        Tbl69FiSpeciessesList ??= new ObservableCollection<Tbl69FiSpecies>();
        Tbl69FiSpeciessesList.Insert(0, new Tbl69FiSpecies { FiSpeciesName = "New", GenusId = GenusSelected.GenusId });
        RefreshFiSpeciesItems();
    }

    private async void CopyFiSpecies_Executed(string? parm)
    {
        if (FiSpeciesSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        FiSpeciesStartEdit();
        FiSpeciesStartNew();
        Tbl69FiSpeciessesList ??= new ObservableCollection<Tbl69FiSpecies>();
        FiSpeciesSelected.GenusId = GenusSelected.GenusId;  //combo vorbelegen

        Tbl69FiSpeciessesList = await _dataService.CopyFiSpecies(FiSpeciesSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshFiSpeciesItems();
    }

    private async void DeleteFiSpecies_Executed(string? parm)
    {
        if (FiSpeciesSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.DeleteFiSpecies(FiSpeciesSelected);
        if (!await ret)
        {
            return;
        }

        Tbl69FiSpeciessesList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromGenusId(FiSpeciesSelected.GenusId);
        RefreshFiSpeciesItems();
    }

    private async void SaveFiSpecies_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(FiSpeciesSelected.FiSpeciesName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (FiSpeciesSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        FiSpeciesSelected ??= Tbl69FiSpeciessesList[0];
        var indx = Tbl69FiSpeciessesList.IndexOf(Tbl69FiSpeciessesList.First(t =>
            t.FiSpeciesName == FiSpeciesSelected.FiSpeciesName));

        FiSpeciesSelected.SpeciesgroupId = SpeciesgroupSelected.SpeciesgroupId;

        var ret = _dataService.SaveFiSpecies(FiSpeciesSelected);
        if (!await ret)
        {
            return;
        }

        if (FiSpeciesSelected.FiSpeciesId == 0) //new
        {
            Tbl69FiSpeciessesList = await _dataService.GetLastDatasetInTbl69FiSpeciesses();
            RefreshFiSpeciesItems();
        }
        else
        {
            Tbl69FiSpeciessesList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromSpeciesgroupId(SpeciesgroupSelected.SpeciesgroupId);
            //   Index Position ?
            if (indx < Tbl69FiSpeciessesList.Count)
            {
                FiSpeciesItems.Clear();
                foreach (var item in Tbl69FiSpeciessesList)
                {
                    FiSpeciesItems.Add(item);
                }

                FiSpeciesSelected = Tbl69FiSpeciessesList[indx];  //Index
            }
        }
    }

    private void RefreshFiSpeciesServer_Executed(string? s)
    {
        Tbl69FiSpeciessesList ??= new ObservableCollection<Tbl69FiSpecies>();
        Tbl69FiSpeciessesList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromSpeciesgroupId(SpeciesgroupSelected.SpeciesgroupId);

        FiSpeciesDataSetCount = Tbl69FiSpeciessesList.Count;

        RefreshFiSpeciesItems();
    }

    public void FiSpeciesStartEdit() => IsInEdit = true;
    public void FiSpeciesStartModify() => IsModified = true;
    public void FiSpeciesStartNew() => IsNewFiSpecies = true;
    public event EventHandler AddNewFiSpeciesCanceled = null!;
    public void FiSpeciesCancelEditsAsync()
    {
        if (IsNewFiSpecies)
        {
            IsInEdit = false;
            AddNewFiSpeciesCanceled?.Invoke(this, EventArgs.Empty);
            IsNewFiSpecies = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }

    #endregion [Public Methods  Connect ==> Tbl69FiSpecies]                                                                                                                            



    //    Part 5    


    #region [Public Commands Connect ==> Tbl72PlSpecies]       
    public ICommand AddPlSpeciesCommand => new RelayCommand<string>(AddPlSpecies_Executed);
    public ICommand CopyPlSpeciesCommand => new RelayCommand<string>(CopyPlSpecies_Executed);
    public ICommand DeletePlSpeciesCommand => new RelayCommand<string>(DeletePlSpecies_Executed);
    public ICommand SavePlSpeciesCommand => new RelayCommand<string>(SavePlSpecies_Executed);
    public ICommand RefreshPlSpeciesServerCommand => new RelayCommand<string>(RefreshPlSpeciesServer_Executed);
    #endregion [Public Commands Connect ==> Tbl72PlSpecies]    

    #region [Public Methods Connect ==> Tbl72PlSpecies]                   

    private void AddPlSpecies_Executed(string? parm)
    {
        PlSpeciesStartEdit();
        PlSpeciesStartNew();
        Tbl66GenussesAllList ??= new ObservableCollection<Tbl66Genus>();
        Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();
        Tbl72PlSpeciessesList ??= new ObservableCollection<Tbl72PlSpecies>();
        Tbl72PlSpeciessesList.Insert(0, new Tbl72PlSpecies { PlSpeciesName = "New", GenusId = GenusSelected.GenusId });
        RefreshPlSpeciesItems();
    }
    private async void CopyPlSpecies_Executed(string? s)
    {
        if (PlSpeciesSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        Tbl72PlSpeciessesList ??= new ObservableCollection<Tbl72PlSpecies>();
        Tbl72PlSpeciessesList = await _dataService.CopyPlSpecies(PlSpeciesSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment
        RefreshPlSpeciesItems();
    }
    private async void DeletePlSpecies_Executed(string? s)
    {
        if (PlSpeciesSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.DeletePlSpecies(PlSpeciesSelected);
        if (!await ret)
        {
            return;
        }

        Tbl72PlSpeciessesList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromSpeciesgroupId(SpeciesgroupSelected.SpeciesgroupId);

        RefreshPlSpeciesItems();
    }
    private async void SavePlSpecies_Executed(string? s)
    {
        if (PlSpeciesSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        PlSpeciesSelected ??= Tbl72PlSpeciessesList[0];
        var indx = Tbl72PlSpeciessesList.IndexOf(Tbl72PlSpeciessesList.First(t =>
            t.PlSpeciesName == PlSpeciesSelected.PlSpeciesName));

        PlSpeciesSelected.SpeciesgroupId = SpeciesgroupSelected.SpeciesgroupId;

        var ret = _dataService.SavePlSpecies(PlSpeciesSelected);
        if (!await ret)
        {
            return;
        }

        if (PlSpeciesSelected.PlSpeciesId == 0) //new
        {
            Tbl72PlSpeciessesList = await _dataService.GetLastDatasetInTbl72PlSpeciesses();
            RefreshPlSpeciesItems();
        }
        else
        {
            Tbl72PlSpeciessesList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromGenusId(PlSpeciesSelected.GenusId);
            //   Index Position ?
            if (indx < Tbl72PlSpeciessesList.Count)
            {
                PlSpeciesItems.Clear();
                foreach (var item in Tbl72PlSpeciessesList)
                {
                    PlSpeciesItems.Add(item);
                }

                PlSpeciesSelected = Tbl72PlSpeciessesList[indx];  //Index
            }
        }
    }
    private void RefreshPlSpeciesServer_Executed(string? s)
    {
        Tbl72PlSpeciessesList ??= new ObservableCollection<Tbl72PlSpecies>();
        Tbl72PlSpeciessesList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromSpeciesgroupId(SpeciesgroupSelected.SpeciesgroupId);

        PlSpeciesDataSetCount = Tbl72PlSpeciessesList.Count;

        RefreshPlSpeciesItems();
    }
    public void PlSpeciesStartEdit() => IsInEdit = true;
    public void PlSpeciesStartModify() => IsModified = true;
    public void PlSpeciesStartNew() => IsNewPlSpecies = true;
    public event EventHandler AddNewPlSpeciesCanceled = null!;
    public void PlSpeciesCancelEditsAsync()
    {
        if (IsNewPlSpecies)
        {
            IsInEdit = false;
            AddNewPlSpeciesCanceled?.Invoke(this, EventArgs.Empty);
            IsNewPlSpecies = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }
    #endregion [Public Methods  Connect ==> Tbl72PlSpecies]                                                                               



    //    Part 6    




    //    Part 7    



    //    Part 8    



    //    Part 9    



    //    Part 10    


    #region "Public Commands to open Main and Detail TabItems"

    private int _selectedMainDetailTabIndex;

    public int SelectedMainDetailTabIndex
    {
        get => _selectedMainDetailTabIndex;
        set
        {
            if (value == _selectedMainDetailTabIndex)
            {
                return;
            }

            _selectedMainDetailTabIndex = value; OnPropertyChanged();

            if (_selectedMainDetailTabIndex == 0)
            {
            }

            if (_selectedMainDetailTabIndex == 1)
            {
                if (SpeciesgroupSelected != null)
                {
                    FiSpeciesStartModify();
                    Tbl68SpeciesgroupsAllList = _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroup();
                    Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

                    Tbl69FiSpeciessesList =
                        _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromSpeciesgroupId(SpeciesgroupSelected.SpeciesgroupId);
                    FiSpeciesDataSetCount = Tbl69FiSpeciessesList.Count;
                    RefreshFiSpeciesItems();
                }
            }

            if (_selectedMainDetailTabIndex == 2)
            {
                if (SpeciesgroupSelected != null)
                {
                    IsLoading = true;
                    PlSpeciesStartModify();
                    Tbl68SpeciesgroupsAllList = _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroup();
                    Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

                    Tbl72PlSpeciessesList =
                        _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromGenusId(SpeciesgroupSelected.SpeciesgroupId);
                    PlSpeciesDataSetCount = Tbl72PlSpeciessesList.Count;
                    RefreshPlSpeciesItems();
                    IsLoading = false;
                }
            }
        }
    }
    #endregion "Public Commands to open Main und Ref TabItems"       




    //    Part 11 

    #region All Properties

    #region Selected Properties

    private Tbl66Genus _genusSelected = null!;
    public Tbl66Genus GenusSelected
    {
        get => _genusSelected;
        set => SetProperty(ref _genusSelected, value);
    }

    private Tbl68Speciesgroup _speciesgroupSelected = null!;
    public Tbl68Speciesgroup SpeciesgroupSelected
    {
        get => _speciesgroupSelected;
        set => SetProperty(ref _speciesgroupSelected, value);
    }

    private Tbl69FiSpecies _fispeciesSelected = null!;
    public Tbl69FiSpecies FiSpeciesSelected
    {
        get => _fispeciesSelected;
        set => SetProperty(ref _fispeciesSelected, value);
    }
    private Tbl72PlSpecies _plspeciesSelected = null!;

    public Tbl72PlSpecies PlSpeciesSelected
    {
        get => _plspeciesSelected;
        set => SetProperty(ref _plspeciesSelected, value);
    }

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

    private bool _isNewPlSpecies;
    public bool IsNewPlSpecies
    {
        get => _isNewPlSpecies;
        set => SetProperty(ref _isNewPlSpecies, value);
    }

    private bool _isNewFiSpecies;
    public bool IsNewFiSpecies
    {
        get => _isNewFiSpecies;
        set => SetProperty(ref _isNewFiSpecies, value);
    }

    private bool _isNewSpeciesgroup;
    public bool IsNewSpeciesgroup
    {
        get => _isNewSpeciesgroup;
        set => SetProperty(ref _isNewSpeciesgroup, value);
    }

    private int _speciesgroupDataSetCount;
    public int SpeciesgroupDataSetCount
    {
        get => _speciesgroupDataSetCount;
        set
        {
            _speciesgroupDataSetCount = value; OnPropertyChanged();
        }
    }
    private int _fiSpeciesDataSetCount;
    public int FiSpeciesDataSetCount
    {
        get => _fiSpeciesDataSetCount;
        set
        {
            _fiSpeciesDataSetCount = value; OnPropertyChanged();
        }
    }

    private int _plSpeciesDataSetCount;
    public int PlSpeciesDataSetCount
    {
        get => _plSpeciesDataSetCount;
        set
        {
            _plSpeciesDataSetCount = value; OnPropertyChanged();
        }
    }


    #endregion Selected Properties

    #region Refresh Methods

    private void RefreshSpeciesgroupItems()
    {
        SpeciesgroupItems.Clear();
        if (Tbl68SpeciesgroupsList != null)
        {
            foreach (var item in Tbl68SpeciesgroupsList)
            {
                SpeciesgroupItems.Add(item);
            }
            if (Tbl68SpeciesgroupsList.Count == 0)
            {
                return;
            }

            if (SpeciesgroupSelected == null && Tbl68SpeciesgroupsList.Count != 0)
            {
                SpeciesgroupSelected = SpeciesgroupItems.FirstOrDefault()!;
            }
        }
    }

    private void RefreshFiSpeciesItems()
    {
        FiSpeciesItems.Clear();
        if (Tbl69FiSpeciessesList != null)
        {
            foreach (var item in Tbl69FiSpeciessesList)
            {
                FiSpeciesItems.Add(item);
            }
            if (Tbl69FiSpeciessesList.Count == 0)
            {
                return;
            }

            if (FiSpeciesSelected == null && Tbl69FiSpeciessesList.Count != 0)
            {
                FiSpeciesSelected = FiSpeciesItems.FirstOrDefault()!;
            }
        }
    }
    private void RefreshPlSpeciesItems()
    {
        PlSpeciesItems.Clear();
        if (Tbl72PlSpeciessesList != null)
        {
            foreach (var item in Tbl72PlSpeciessesList)
            {
                PlSpeciesItems.Add(item);
            }
            if (Tbl72PlSpeciessesList.Count == 0)
            {
                return;
            }

            if (PlSpeciesSelected == null && Tbl72PlSpeciessesList.Count != 0)
            {
                PlSpeciesSelected = PlSpeciesItems.FirstOrDefault()!;
            }
        }
    }

    #endregion Refresh Methods

    #region Public Properties  

    private string _searchSpeciesgroupName = "";

    public string SearchSpeciesgroupName
    {
        get => _searchSpeciesgroupName;
        set
        {
            _searchSpeciesgroupName = value; OnPropertyChanged();
        }
    }
 
    private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesList = null!;

    public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
    {
        get => _tbl69FiSpeciessesList;
        set
        {
            _tbl69FiSpeciessesList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesList = null!;
    public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesList
    {
        get => _tbl72PlSpeciessesList;
        set
        {
            _tbl72PlSpeciessesList = value; OnPropertyChanged();
        }
    }


 

    private ObservableCollection<Tbl66Genus> _tbl66GenussesList = null!;

    public ObservableCollection<Tbl66Genus> Tbl66GenussesList
    {
        get => _tbl66GenussesList;
        set
        {
            _tbl66GenussesList = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList = null!;

    public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
    {
        get => _tbl66GenussesAllList;
        set
        {
            _tbl66GenussesAllList = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsList = null!;

    public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsList
    {
        get => _tbl68SpeciesgroupsList;
        set
        {
            _tbl68SpeciesgroupsList = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList = null!;

    public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
    {
        get => _tbl68SpeciesgroupsAllList;
        set
        {
            _tbl68SpeciesgroupsAllList = value;
            OnPropertyChanged();
        }
    }


    #endregion Public Properties

    #endregion All Properties

}
