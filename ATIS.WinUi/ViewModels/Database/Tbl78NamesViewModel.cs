using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using CommunityToolkit.Mvvm.ComponentModel;

//    Tbl78NamesViewModel Skriptdatum:  15.04.2023  10:32    

namespace ATIS.WinUi.ViewModels.Database;

public class Tbl78NamesViewModel : ObservableObject
{

    #region [Private Data Members]
    private readonly IDataService _dataService = null!;
    public ObservableCollection<Tbl78Name> NameItems { get; } = new();

    public ObservableCollection<Tbl69FiSpecies> FiSpeciesItems { get; } = new();
    public ObservableCollection<Tbl72PlSpecies> PlSpeciesItems { get; } = new();

    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]           

    #region [Constructor]
    public Tbl78NamesViewModel(IDataService dataService)
    {
        _dataService = dataService;
        SelectedMainDetailTabIndex = 2; //Tab Datagrid
        GetAllCollections();
    }

    private void GetAllCollections()
    {
        Tbl68SpeciesgroupsAllList ??= new ObservableCollection<Tbl68Speciesgroup>();
        Tbl68SpeciesgroupsAllList = _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroup();
        Tbl66GenussesAllList ??= new ObservableCollection<Tbl66Genus>();
        Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

        Tbl69FiSpeciessesAllList ??= new ObservableCollection<Tbl69FiSpecies>();
        Tbl69FiSpeciessesAllList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDivers();
        Tbl72PlSpeciessesAllList ??= new ObservableCollection<Tbl72PlSpecies>();
        Tbl72PlSpeciessesAllList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDivers();
    }

    #endregion [Constructor]  


    //    Part 1    



    #region [Commands Name]

    public ICommand GetNamesByNameOrIdCommand => new RelayCommand(execute: delegate
    { var task = GetNamesByNameOrId_Executed(SearchNameName); });

    public ICommand AddNameCommand => new RelayCommand<string>(AddName_Executed);
    public ICommand CopyNameCommand => new RelayCommand<string>(CopyName_Executed);

    public ICommand DeleteNameCommand => new RelayCommand(execute: delegate { DeleteName_Executed(SearchNameName); });

    public ICommand SaveNameCommand => new RelayCommand(execute: delegate { var task = SaveName_Executed(SearchNameName); });
    public ICommand RefreshNameServerCommand => new RelayCommand(execute: delegate { RefreshNameServer_Executed(SearchNameName); });

    #endregion [Commands Name]       

    #region [Methods Name]

    private async Task GetNamesByNameOrId_Executed(string searchName)
    {
        NameStartModify();
        Tbl69FiSpeciessesAllList ??= new ObservableCollection<Tbl69FiSpecies>();
        Tbl69FiSpeciessesAllList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDivers();
        Tbl72PlSpeciessesAllList ??= new ObservableCollection<Tbl72PlSpecies>();
        Tbl72PlSpeciessesAllList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDivers();

        Tbl78NamesList ??= new ObservableCollection<Tbl78Name>();
        Tbl78NamesList = await _dataService.GetTbl78NamesCollectionOrderByNameNameFromSearchNameOrId(searchName);

        if (Tbl78NamesList.Count == 0)
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        NameDataSetCount = Tbl78NamesList.Count;
        RefreshNameItems();

        SelectedMainDetailTabIndex = 2;
    }

    private async void AddName_Executed(string? parm)
    {
        NameStartEdit();
        NameStartNew();
        Tbl69FiSpeciessesAllList ??= new ObservableCollection<Tbl69FiSpecies>();
        Tbl69FiSpeciessesAllList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDivers();
        Tbl72PlSpeciessesAllList ??= new ObservableCollection<Tbl72PlSpecies>();
        Tbl72PlSpeciessesAllList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDivers();

        //Id search for first Dataset of Tbl69FiSpeciessesList + Tbl72PlSpeciessesList
        var id = 0;
        var single = await _dataService.GetFiSpeciesSingleFirstDataset();
        if (single != null)
        {
            id = single.FiSpeciesId;
        }

        var id1 = 0;
        var single1 = await _dataService.GetPlSpeciesSingleFirstDataset();
        if (single != null)
        {
            id1 = single1.PlSpeciesId;
        }

        Tbl78NamesList ??= new ObservableCollection<Tbl78Name>();
        Tbl78NamesList.Insert(0, new Tbl78Name { NameName = "New", FiSpeciesId = id, PlSpeciesId = id1 });
        RefreshNameItems();
    }

    private async void CopyName_Executed(string? parm)
    {
        NameStartEdit();
        NameStartNew();

        Tbl78NamesList = await _dataService.CopyName(NameSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshNameItems();
    }

    private async void DeleteName_Executed(string? parm)
    {

        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(NameSelected!.NameName!))
        {
            //necessary to delete before
            var ret = _dataService.DeleteName(NameSelected);
            if (!await ret)
            {
                return;
            }

            Tbl78NamesList = await _dataService.GetTbl78NamesCollectionOrderByNameNameFromSearchNameOrId(parm!);

            NameDataSetCount = Tbl78NamesList.Count;
            RefreshNameItems();
        }
    }

    private async Task SaveName_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(NameSelected?.NameName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }

        if (Tbl78NamesList != null)
        {

            var iNdx = Tbl78NamesList.IndexOf(Tbl78NamesList.First(t =>
                t.NameName == NameSelected.NameName));

            var ret = _dataService.SaveName(NameSelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl78NamesList = await _dataService.GetLastDatasetInTbl78Names();
                RefreshNameItems();
            }
            else
            {
                if (NameSelected.NameId == 0) //new
                {
                    Tbl78NamesList = await _dataService.GetLastDatasetInTbl78Names();
                    RefreshNameItems();
                }
                else
                {
                    Tbl78NamesList = await _dataService.GetTbl78NamesCollectionOrderByNameNameFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl78NamesList.Count)
                    {
                        NameItems.Clear();
                        foreach (var item in Tbl78NamesList)
                        {
                            NameItems.Add(item);
                        }

                        NameSelected = Tbl78NamesList[iNdx];
                    }
                }
            }

            NameDataSetCount = Tbl78NamesList.Count;
            NameCancelEditsAsync();
        }
    }

    private async void RefreshNameServer_Executed(string? parm)
    {
        Tbl78NamesList = await _dataService.GetTbl78NamesCollectionOrderByNameNameFromSearchNameOrId(parm!);

        NameDataSetCount = Tbl78NamesList.Count;
        RefreshNameItems();
    }

    public void NameStartEdit() => IsInEdit = true;
    public void NameStartModify() => IsModified = true;
    public void NameStartNew() => IsNewName = true;
    public event EventHandler AddNewNameCanceled = null!;
    public void NameCancelEditsAsync()
    {
        if (IsNewName)
        {
            IsInEdit = false;
            AddNewNameCanceled?.Invoke(this, EventArgs.Empty);
            IsNewName = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }

    #endregion [Methods Tbl78Name]    




    //    Part 2    


    #region "Public Commands Connect <== Tbl69FiSpecies"                 

    public ICommand SaveFiSpeciesCommand => new RelayCommand<string>(SaveFiSpecies_Executed);
    public ICommand RefreshFiSpeciesServerCommand => new RelayCommand(execute: delegate { RefreshFiSpeciesServer_Executed(SearchNameName); });

    private async void SaveFiSpecies_Executed(string? s)
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
        var ret = _dataService.SaveFiSpecies(FiSpeciesSelected);

        if (!await ret)
        {
            return;
        }

        Tbl69FiSpeciessesList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromFiSpeciesId(NameSelected.FiSpeciesId);
        RefreshFiSpeciesItems();
        FiSpeciesCancelEditsAsync();
    }
    private void RefreshFiSpeciesServer_Executed(object o)
    {
        Tbl66GenussesAllList ??= new ObservableCollection<Tbl66Genus>();
        Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

        Tbl69FiSpeciessesList ??= new ObservableCollection<Tbl69FiSpecies>();
        Tbl69FiSpeciessesList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromFiSpeciesId(NameSelected.FiSpeciesId);

        FiSpeciesDataSetCount = Tbl69FiSpeciessesList.Count;

        RefreshFiSpeciesItems();
    }
    public void FiSpeciesStartEdit() => IsInEdit = true;
    public void FiSpeciesStartModify() => IsModified = true;
    public void FiSpeciesCancelEditsAsync()
    {
        IsInEdit = false;
        IsModified = false;
    }

    #endregion "Public Commands"                       



    //    Part 3    


    #region "Public Commands Connect <== Tbl72PlSpecies"                 

    public ICommand SavePlSpeciesCommand => new RelayCommand<string>(SavePlSpecies_Executed);
    public ICommand RefreshPlSpeciesServerCommand => new RelayCommand(execute: delegate { RefreshPlSpeciesServer_Executed(SearchNameName); });

    private async void SavePlSpecies_Executed(string? s)
    {
        if (string.IsNullOrEmpty(PlSpeciesSelected.PlSpeciesName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (PlSpeciesSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.SavePlSpecies(PlSpeciesSelected);

        if (!await ret)
        {
            return;
        }

        Tbl72PlSpeciessesList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromPlSpeciesId(NameSelected.PlSpeciesId);
        RefreshPlSpeciesItems();
        PlSpeciesCancelEditsAsync();
    }
    private void RefreshPlSpeciesServer_Executed(object o)
    {
        Tbl66GenussesAllList ??= new ObservableCollection<Tbl66Genus>();
        Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

        Tbl72PlSpeciessesList ??= new ObservableCollection<Tbl72PlSpecies>();
        Tbl72PlSpeciessesList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromPlSpeciesId(NameSelected.PlSpeciesId);

        PlSpeciesDataSetCount = Tbl72PlSpeciessesList.Count;

        RefreshPlSpeciesItems();
    }

    public void PlSpeciesStartEdit() => IsInEdit = true;
    public void PlSpeciesStartModify() => IsModified = true;
    public void PlSpeciesCancelEditsAsync()
    {
        IsInEdit = false;
        IsModified = false;
    }

    #endregion "Public Commands"                       




    //    Part 4    




    //    Part 5    




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
                if (NameSelected != null)
                {
                    IsLoading = true;
                    FiSpeciesStartModify();
                    Tbl68SpeciesgroupsAllList = _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroup();
                    Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();
   
                    Tbl69FiSpeciessesList ??= new ObservableCollection<Tbl69FiSpecies>();
                    Tbl69FiSpeciessesList =
                        _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromFiSpeciesId(NameSelected.FiSpeciesId);
                    FiSpeciesDataSetCount = Tbl69FiSpeciessesList.Count;
                    RefreshFiSpeciesItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 1)
            {
                if (NameSelected != null)
                {
                    IsLoading = true;
                    PlSpeciesStartModify();
                    Tbl68SpeciesgroupsAllList = _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroup();
                    Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

                    Tbl72PlSpeciessesList =
                        _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromPlSpeciesId(NameSelected.PlSpeciesId);
                    PlSpeciesDataSetCount = Tbl72PlSpeciessesList.Count;
                    RefreshPlSpeciesItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 2)
            {
            }

        }
    }


    #endregion "Public Commands to open Main and Detail TabItems"   



    //    Part 11 



    #region All Properties

    #region Selected Properties
    private Tbl78Name _nameSelected = null!;
    public Tbl78Name NameSelected
    {
        get => _nameSelected;
        set => SetProperty(ref _nameSelected, value);
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
    #endregion

    #region Public Properties
    public string SearchNameName { get; set; } = null!;

    private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesList = null!;
    public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
    {
        get => _tbl69FiSpeciessesList;
        set
        {
            _tbl69FiSpeciessesList = value; OnPropertyChanged();
        }
    }
    private ObservableCollection<Tbl69FiSpecies>? _tbl69FiSpeciessesAllList;
    public ObservableCollection<Tbl69FiSpecies>? Tbl69FiSpeciessesAllList
    {
        get => _tbl69FiSpeciessesAllList;
        set
        {
            _tbl69FiSpeciessesAllList = value; OnPropertyChanged();
        }
    }
    private ObservableCollection<Tbl72PlSpecies>? _tbl72PlSpeciessesAllList;
    public ObservableCollection<Tbl72PlSpecies>? Tbl72PlSpeciessesAllList
    {
        get => _tbl72PlSpeciessesAllList;
        set
        {
            _tbl72PlSpeciessesAllList = value; OnPropertyChanged();
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

    private ObservableCollection<Tbl78Name> _tbl78NamesList = null!;
    public ObservableCollection<Tbl78Name> Tbl78NamesList
    {
        get => _tbl78NamesList;
        set
        {
            _tbl78NamesList = value; OnPropertyChanged();
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





    //---------------------------------------------------------------------
    private string _searchName = "";

    public string SearchName
    {
        get => _searchName;
        set
        {
            _searchName = value; OnPropertyChanged();
        }
    }

    private int _fispeciesDataSetCount;
    public int FiSpeciesDataSetCount
    {
        get => _fispeciesDataSetCount;
        set
        {
            _fispeciesDataSetCount = value; OnPropertyChanged();
        }
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

    private int _nameDataSetCount;
    public int NameDataSetCount
    {
        get => _nameDataSetCount;
        set
        {
            _nameDataSetCount = value; OnPropertyChanged();
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


    //---------------------------------------------------------------------
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

    private bool _isNewName;
    public bool IsNewName
    {
        get => _isNewName;
        set => SetProperty(ref _isNewName, value);
    }
    private bool _isNewImage;
    public bool IsNewImage
    {
        get => _isNewImage;
        set => SetProperty(ref _isNewImage, value);
    }

    private bool _isNewReferenceExpert;
    public bool IsNewReferenceExpert
    {
        get => _isNewReferenceExpert;
        set => SetProperty(ref _isNewReferenceExpert, value);
    }

    private bool _isNewReferenceSource;
    public bool IsNewReferenceSource
    {
        get => _isNewReferenceSource;
        set => SetProperty(ref _isNewReferenceSource, value);
    }

    private bool _isNewReferenceAuthor;
    public bool IsNewReferenceAuthor
    {
        get => _isNewReferenceAuthor;
        set => SetProperty(ref _isNewReferenceAuthor, value);
    }

    private bool _isNewComment;
    public bool IsNewComment
    {
        get => _isNewComment;
        set => SetProperty(ref _isNewComment, value);
    }
    #endregion "Public Properties"            

    #region Refresh Methods
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

    private void RefreshNameItems()
    {
        NameItems.Clear();
        if (Tbl78NamesList != null)
        {
            foreach (var item in Tbl78NamesList)
            {
                NameItems.Add(item);
            }
            if (Tbl78NamesList.Count == 0)
            {
                return;
            }

            if (NameSelected == null && Tbl78NamesList.Count != 0)
            {
                NameSelected = NameItems.FirstOrDefault()!;
            }
        }
    }

    #endregion

    #endregion

    public Tbl78NamesViewModel()
    {

    }

}
