
using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using CommunityToolkit.Mvvm.ComponentModel;

//    Tbl84SynonymsViewModel Skriptdatum:  21.04.2023  10:32    

namespace ATIS.WinUi.ViewModels.Database;

public class Tbl84SynonymsViewModel : ObservableObject
{

    #region [Private Data Members]
    private readonly IDataService _dataService = null!;
    public ObservableCollection<Tbl84Synonym> SynonymItems { get; } = new();

    public ObservableCollection<Tbl69FiSpecies> FiSpeciesItems { get; } = new();
    public ObservableCollection<Tbl72PlSpecies> PlSpeciesItems { get; } = new();

    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]           

    #region [Constructor]
    public Tbl84SynonymsViewModel(IDataService dataService)
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



    #region [Commands Synonym]

    public ICommand GetSynonymsByNameOrIdCommand => new RelayCommand(execute: delegate
    { var task = GetSynonymsByNameOrId_Executed(SearchSynonymName); });

    public ICommand AddSynonymCommand => new RelayCommand<string>(AddSynonym_Executed);
    public ICommand CopySynonymCommand => new RelayCommand<string>(CopySynonym_Executed);

    public ICommand DeleteSynonymCommand => new RelayCommand(execute: delegate { DeleteSynonym_Executed(SearchSynonymName); });

    public ICommand SaveSynonymCommand => new RelayCommand(execute: delegate { var task = SaveSynonym_Executed(SearchSynonymName); });
    public ICommand RefreshSynonymServerCommand => new RelayCommand(execute: delegate { RefreshSynonymServer_Executed(SearchSynonymName); });

    #endregion [Commands Synonym]       

    #region [Methods Synonym]

    private async Task GetSynonymsByNameOrId_Executed(string searchName)
    {
        SynonymStartModify();
        Tbl69FiSpeciessesAllList ??= new ObservableCollection<Tbl69FiSpecies>();
        Tbl69FiSpeciessesAllList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDivers();
        Tbl72PlSpeciessesAllList ??= new ObservableCollection<Tbl72PlSpecies>();
        Tbl72PlSpeciessesAllList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDivers();

        Tbl84SynonymsList ??= new ObservableCollection<Tbl84Synonym>();
        Tbl84SynonymsList = await _dataService.GetTbl84SynonymsCollectionOrderBySynonymNameFromSearchNameOrId(searchName);

        if (Tbl84SynonymsList.Count == 0)
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        SynonymDataSetCount = Tbl84SynonymsList.Count;
        RefreshSynonymItems();

        SelectedMainDetailTabIndex = 2;
    }

    private async void AddSynonym_Executed(string? parm)
    {
        SynonymStartEdit();
        SynonymStartNew();
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

        Tbl84SynonymsList ??= new ObservableCollection<Tbl84Synonym>();
        Tbl84SynonymsList.Insert(0, new Tbl84Synonym { SynonymName = "New", FiSpeciesId = id, PlSpeciesId = id1 });
        RefreshSynonymItems();
    }


    private async void CopySynonym_Executed(string? parm)
    {
        SynonymStartEdit();
        SynonymStartNew();

        Tbl84SynonymsList = await _dataService.CopySynonym(SynonymSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshSynonymItems();
    }

    private async void DeleteSynonym_Executed(string? parm)
    {

        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(SynonymSelected!.SynonymName!))
        {
            //necessary to delete before
            var ret = _dataService.DeleteSynonym(SynonymSelected);
            if (!await ret)
            {
                return;
            }

            Tbl84SynonymsList = await _dataService.GetTbl84SynonymsCollectionOrderBySynonymNameFromSearchNameOrId(parm!);

            SynonymDataSetCount = Tbl84SynonymsList.Count;
            RefreshSynonymItems();
        }
    }

    private async Task SaveSynonym_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(SynonymSelected?.SynonymName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl84SynonymsList != null)
        {

            var iNdx = Tbl84SynonymsList.IndexOf(Tbl84SynonymsList.First(t =>
                t.SynonymName == SynonymSelected.SynonymName));

            var ret = _dataService.SaveSynonym(SynonymSelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl84SynonymsList = await _dataService.GetLastDatasetInTbl84Synonyms();
                RefreshSynonymItems();
            }
            else
            {
                if (SynonymSelected.SynonymId == 0) //new
                {
                    Tbl84SynonymsList = await _dataService.GetLastDatasetInTbl84Synonyms();
                    RefreshSynonymItems();
                }
                else
                {
                    Tbl84SynonymsList = await _dataService.GetTbl84SynonymsCollectionOrderBySynonymNameFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl84SynonymsList.Count)
                    {
                        SynonymItems.Clear();
                        foreach (var item in Tbl84SynonymsList)
                        {
                            SynonymItems.Add(item);
                        }

                        SynonymSelected = Tbl84SynonymsList[iNdx];
                    }
                }
            }
            SynonymDataSetCount = Tbl84SynonymsList.Count;
            SynonymCancelEditsAsync();
        }
    }

    private async void RefreshSynonymServer_Executed(string? parm)
    {
        Tbl84SynonymsList = await _dataService.GetTbl84SynonymsCollectionOrderBySynonymNameFromSearchNameOrId(parm!);

        SynonymDataSetCount = Tbl84SynonymsList.Count;
        RefreshSynonymItems();
    }

    public void SynonymStartEdit() => IsInEdit = true;
    public void SynonymStartModify() => IsModified = true;
    public void SynonymStartNew() => IsNewSynonym = true;
    public event EventHandler AddNewSynonymCanceled = null!;
    public void SynonymCancelEditsAsync()
    {
        if (IsNewSynonym)
        {
            IsInEdit = false;
            AddNewSynonymCanceled?.Invoke(this, EventArgs.Empty);
            IsNewSynonym = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }

    #endregion [Methods Tbl84Synonym]    




    //    Part 2    


    #region "Public Commands Connect <== Tbl69FiSpecies"                 

    public ICommand SaveFiSpeciesCommand => new RelayCommand<string>(SaveFiSpecies_Executed);
    public ICommand RefreshFiSpeciesServerCommand => new RelayCommand(execute: delegate { RefreshFiSpeciesServer_Executed(SearchSynonymName); });

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

        Tbl69FiSpeciessesList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromFiSpeciesId(SynonymSelected.FiSpeciesId);
        RefreshFiSpeciesItems();
        FiSpeciesCancelEditsAsync();
    }
    private void RefreshFiSpeciesServer_Executed(object o)
    {
        Tbl66GenussesAllList ??= new ObservableCollection<Tbl66Genus>();
        Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

        Tbl69FiSpeciessesList ??= new ObservableCollection<Tbl69FiSpecies>();
        Tbl69FiSpeciessesList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromFiSpeciesId(SynonymSelected.FiSpeciesId);

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
    public ICommand RefreshPlSpeciesServerCommand => new RelayCommand(execute: delegate { RefreshPlSpeciesServer_Executed(SearchSynonymName); });

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

        Tbl72PlSpeciessesList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromPlSpeciesId(SynonymSelected.PlSpeciesId);
        RefreshPlSpeciesItems();
        PlSpeciesCancelEditsAsync();
    }
    private void RefreshPlSpeciesServer_Executed(object o)
    {
        Tbl66GenussesAllList ??= new ObservableCollection<Tbl66Genus>();
        Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

        Tbl72PlSpeciessesList ??= new ObservableCollection<Tbl72PlSpecies>();
        Tbl72PlSpeciessesList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromPlSpeciesId(SynonymSelected.PlSpeciesId);

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
                if (SynonymSelected != null)
                {
                    IsLoading = true;
                    FiSpeciesStartModify();
                    Tbl68SpeciesgroupsAllList = _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroup();
                    Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

                    Tbl69FiSpeciessesList ??= new ObservableCollection<Tbl69FiSpecies>();
                    Tbl69FiSpeciessesList =
                        _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromFiSpeciesId(SynonymSelected.FiSpeciesId);
                    FiSpeciesDataSetCount = Tbl69FiSpeciessesList.Count;
                    RefreshFiSpeciesItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 1)
            {
                if (SynonymSelected != null)
                {
                    IsLoading = true;
                    PlSpeciesStartModify();
                    Tbl68SpeciesgroupsAllList = _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroup();
                    Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

                    Tbl72PlSpeciessesList =
                        _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromPlSpeciesId(SynonymSelected.PlSpeciesId);
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
    private Tbl84Synonym _synonymSelected = null!;
    public Tbl84Synonym SynonymSelected
    {
        get => _synonymSelected;
        set => SetProperty(ref _synonymSelected, value);
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

    private ObservableCollection<Tbl84Synonym> _tbl84SynonymsList = null!;
    public ObservableCollection<Tbl84Synonym> Tbl84SynonymsList
    {
        get => _tbl84SynonymsList;
        set
        {
            _tbl84SynonymsList = value; OnPropertyChanged();
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
    private string _searchSynonymName = "";

    public string SearchSynonymName
    {
        get => _searchSynonymName;
        set
        {
            _searchSynonymName = value; OnPropertyChanged();
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

    private int _synonymDataSetCount;
    public int SynonymDataSetCount
    {
        get => _synonymDataSetCount;
        set
        {
            _synonymDataSetCount = value; OnPropertyChanged();
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

    private bool _isNewSynonym;
    public bool IsNewSynonym
    {
        get => _isNewSynonym;
        set => SetProperty(ref _isNewSynonym, value);
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
    private void RefreshSynonymItems()
    {
        SynonymItems.Clear();
        if (Tbl84SynonymsList != null)
        {
            foreach (var item in Tbl84SynonymsList)
            {
                SynonymItems.Add(item);
            }
            if (Tbl84SynonymsList.Count == 0)
            {
                return;
            }

            if (SynonymSelected == null && Tbl84SynonymsList.Count != 0)
            {
                SynonymSelected = SynonymItems.FirstOrDefault()!;
            }
        }
    }


    #endregion

    #endregion


    public Tbl84SynonymsViewModel()
    {

    }


}
