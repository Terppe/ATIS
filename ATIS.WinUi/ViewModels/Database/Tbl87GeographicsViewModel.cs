
using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using CommunityToolkit.Mvvm.ComponentModel;

//    Tbl87GeographicsViewModel Skriptdatum:  23.04.2023  10:32    

namespace ATIS.WinUi.ViewModels.Database;

public class Tbl87GeographicsViewModel : ObservableObject
{

    #region [Private Data Members]
    private readonly IDataService _dataService = null!;
    public ObservableCollection<Tbl87Geographic> GeographicItems { get; } = new();

    public ObservableCollection<Tbl69FiSpecies> FiSpeciesItems { get; } = new();
    public ObservableCollection<Tbl72PlSpecies> PlSpeciesItems { get; } = new();

    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]           

    #region [Constructor]
    public Tbl87GeographicsViewModel(IDataService dataService)
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

        TblCountriesAllList ??= new ObservableCollection<TblCountry>();
        TblCountriesAllList = _dataService.GetTblCountriesCollectionOrderByName();
        Tbl69FiSpeciessesAllList ??= new ObservableCollection<Tbl69FiSpecies>();
        Tbl69FiSpeciessesAllList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDivers();
        Tbl72PlSpeciessesAllList ??= new ObservableCollection<Tbl72PlSpecies>();
        Tbl72PlSpeciessesAllList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDivers();

    }

    #endregion [Constructor]  


    //    Part 1    



    #region [Commands Geographic]

    public ICommand GetGeographicsByInfoOrIdCommand => new RelayCommand(delegate { var task = GetGeographicsByInfoOrId_Executed(SearchGeographicInfoOrId); });

    public ICommand AddGeographicCommand => new RelayCommand<string>(AddGeographic_Executed);
    public ICommand CopyGeographicCommand => new RelayCommand<string>(CopyGeographic_Executed);

    public ICommand DeleteGeographicCommand => new RelayCommand(execute: delegate { var task = DeleteGeographic(o: null); });

    public ICommand SaveGeographicCommand => new RelayCommand(execute: delegate { var task = SaveGeographic(o: null); });
    public ICommand RefreshGeographicServerCommand => new RelayCommand(execute: delegate { var task = RefreshGeographicServer(GeographicSelected.GeographicId); });

    #endregion [Commands Geographic]    

    #region [Public Methods Connect ==> Tbl87Geographic]

    private async Task GetGeographicsByInfoOrId_Executed(string searchInfoOrId)
    {
        GeographicStartModify();
        Tbl69FiSpeciessesAllList ??= new ObservableCollection<Tbl69FiSpecies>();
        Tbl69FiSpeciessesAllList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDivers();
        Tbl72PlSpeciessesAllList ??= new ObservableCollection<Tbl72PlSpecies>();
        Tbl72PlSpeciessesAllList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDivers();

        Tbl87GeographicsList ??= new ObservableCollection<Tbl87Geographic>();
        Tbl87GeographicsList = await _dataService.GetTbl87GeographicsCollectionOrderByInfoFromSearchInfoOrGeographicId(searchInfoOrId);
        if (Tbl87GeographicsList.Count == 0)
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        GeographicDataSetCount = Tbl87GeographicsList.Count;
        RefreshGeographicItems();

        SelectedMainDetailTabIndex = 2;
    }

    private async void AddGeographic_Executed(string? parm)
    {
        GeographicStartEdit();
        GeographicStartNew();
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

        Tbl87GeographicsList ??= new ObservableCollection<Tbl87Geographic>();
        Tbl87GeographicsList.Insert(0, new Tbl87Geographic { Info = "New", FiSpeciesId = id, PlSpeciesId = id1 });
        RefreshGeographicItems();
    }

    private async void CopyGeographic_Executed(string? parm)
    {
        GeographicStartEdit();
        GeographicStartNew();

        Tbl87GeographicsList = await _dataService.CopyGeographic(GeographicSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshGeographicItems();
    }

    private async Task DeleteGeographic(object? o)
    {
        if (GeographicSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }

        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(GeographicSelected!.Info!))
        {
            //necessary to delete before
            var ret = _dataService.DeleteGeographic(GeographicSelected);
            if (!await ret)
            {
                return;
            }

            Tbl87GeographicsList = _dataService.GetTbl87GeographicsCollectionOrderByInfoFromFiSpeciesId(GeographicSelected.FiSpeciesId);
            GeographicDataSetCount = Tbl87GeographicsList.Count;
            RefreshGeographicItems();
        }
    }

    private async Task SaveGeographic(object? o)
    {
        if (string.IsNullOrEmpty(GeographicSelected.Info))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (GeographicSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }

        GeographicSelected ??= Tbl87GeographicsList[0];
        var indx = Tbl87GeographicsList.IndexOf(Tbl87GeographicsList.First(t =>
        t.Info == GeographicSelected.Info));
        var ret = _dataService.SaveGeographic(GeographicSelected);
        if (!await ret)
        {
            return;
        }

        if (GeographicSelected.GeographicId == 0) //new
        {
            Tbl87GeographicsList = await _dataService.GetLastDatasetInTbl87Geographics();
            RefreshGeographicItems();
        }
        else
        {
            Tbl87GeographicsList =
                _dataService.GetTbl87GeographicsCollectionOrderByInfoFromFiSpeciesId(GeographicSelected
                    .FiSpeciesId);
            //   Index Position ?
            if (indx < Tbl87GeographicsList.Count)
            {
                GeographicItems.Clear();
                foreach (var item in Tbl87GeographicsList)
                {
                    GeographicItems.Add(item);
                }

                GeographicSelected = Tbl87GeographicsList[indx]; //Index
            }
        }
        GeographicDataSetCount = Tbl87GeographicsList.Count;
        GeographicCancelEditsAsync();
    }


    private Task RefreshGeographicServer(int fiSpeciesId)
    {
        Tbl87GeographicsList ??= new ObservableCollection<Tbl87Geographic>();
        Tbl87GeographicsList = _dataService.GetTbl87GeographicsCollectionOrderByInfoFromFiSpeciesId(fiSpeciesId);

        GeographicDataSetCount = Tbl87GeographicsList.Count;

        RefreshGeographicItems();
        return Task.CompletedTask;
    }

    public void GeographicStartEdit() => IsInEdit = true;
    public void GeographicStartModify() => IsModified = true;
    public void GeographicStartNew() => IsNewGeographic = true;
    public event EventHandler AddNewGeographicCanceled = null!;

    public void GeographicCancelEditsAsync()
    {
        if (IsNewGeographic)
        {
            AddNewGeographicCanceled?.Invoke(this, EventArgs.Empty);
            IsInEdit = false;
            IsNewGeographic = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }

    #endregion [Methods Geographic]     




    //    Part 2    


    #region "Public Commands Connect <== Tbl69FiSpecies"                 

    public ICommand SaveFiSpeciesCommand => new RelayCommand<string>(SaveFiSpecies_Executed);
    public ICommand RefreshFiSpeciesServerCommand => new RelayCommand(execute: delegate { RefreshFiSpeciesServer_Executed(SearchGeographicInfoOrId); });

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

        Tbl69FiSpeciessesList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromFiSpeciesId(GeographicSelected.FiSpeciesId);
        RefreshFiSpeciesItems();
        FiSpeciesCancelEditsAsync();
    }
    private void RefreshFiSpeciesServer_Executed(object o)
    {
        Tbl66GenussesAllList ??= new ObservableCollection<Tbl66Genus>();
        Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

        Tbl69FiSpeciessesList ??= new ObservableCollection<Tbl69FiSpecies>();
        Tbl69FiSpeciessesList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromFiSpeciesId(GeographicSelected.FiSpeciesId);

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
    public ICommand RefreshPlSpeciesServerCommand => new RelayCommand(execute: delegate { RefreshPlSpeciesServer_Executed(SearchGeographicInfoOrId); });

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

        Tbl72PlSpeciessesList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromPlSpeciesId(GeographicSelected.PlSpeciesId);
        RefreshPlSpeciesItems();
        PlSpeciesCancelEditsAsync();
    }
    private void RefreshPlSpeciesServer_Executed(object o)
    {
        Tbl66GenussesAllList ??= new ObservableCollection<Tbl66Genus>();
        Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

        Tbl72PlSpeciessesList ??= new ObservableCollection<Tbl72PlSpecies>();
        Tbl72PlSpeciessesList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromPlSpeciesId(GeographicSelected.PlSpeciesId);

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
                if (GeographicSelected != null)
                {
                    IsLoading = true;
                    FiSpeciesStartModify();
                    Tbl68SpeciesgroupsAllList = _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroup();
                    Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

                    Tbl69FiSpeciessesList ??= new ObservableCollection<Tbl69FiSpecies>();
                    Tbl69FiSpeciessesList =
                        _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromFiSpeciesId(GeographicSelected.FiSpeciesId);
                    FiSpeciesDataSetCount = Tbl69FiSpeciessesList.Count;
                    RefreshFiSpeciesItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 1)
            {
                if (GeographicSelected != null)
                {
                    IsLoading = true;
                    PlSpeciesStartModify();
                    Tbl68SpeciesgroupsAllList = _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroup();
                    Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

                    Tbl72PlSpeciessesList =
                        _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromPlSpeciesId(GeographicSelected.PlSpeciesId);
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

    private Tbl69FiSpecies _fispeciesSelected = null!;
    public Tbl69FiSpecies FiSpeciesSelected
    {
        get => _fispeciesSelected;
        set => SetProperty(ref _fispeciesSelected, value);
    }

    private Tbl87Geographic _geographicSelected = null!;
    public Tbl87Geographic GeographicSelected
    {
        get => _geographicSelected;
        set => SetProperty(ref _geographicSelected, value);
    }

    private Tbl72PlSpecies _plspeciesSelected = null!;

    public Tbl72PlSpecies PlSpeciesSelected
    {
        get => _plspeciesSelected;
        set => SetProperty(ref _plspeciesSelected, value);
    }

    private Tbl90Reference _referenceSelected = null!;
    public Tbl90Reference ReferenceSelected
    {
        get => _referenceSelected;
        set => SetProperty(ref _referenceSelected, value);
    }

    private Tbl90Reference _referenceExpertSelected = null!;
    public Tbl90Reference ReferenceExpertSelected
    {
        get => _referenceExpertSelected;
        set => SetProperty(ref _referenceExpertSelected, value);
    }

    private Tbl90Reference _referenceSourceSelected = null!;
    public Tbl90Reference ReferenceSourceSelected
    {
        get => _referenceSourceSelected;
        set => SetProperty(ref _referenceSourceSelected, value);
    }
    private Tbl90Reference _referenceAuthorSelected = null!;
    public Tbl90Reference ReferenceAuthorSelected
    {
        get => _referenceAuthorSelected;
        set => SetProperty(ref _referenceAuthorSelected, value);
    }

    private Tbl93Comment _commentSelected = null!;
    public Tbl93Comment CommentSelected
    {
        get => _commentSelected;
        set => SetProperty(ref _commentSelected, value);
    }
    #endregion

    #region Public Properties
    public string SearchGeographicInfoOrId { get; set; } = null!;

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
    private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesList = null!;

    public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesList
    {
        get => _tbl72PlSpeciessesList;
        set
        {
            _tbl72PlSpeciessesList = value; OnPropertyChanged(nameof(Tbl72PlSpeciessesList));
        }
    }

    private ObservableCollection<Tbl87Geographic> _tbl87GeographicsList = null!;
    public ObservableCollection<Tbl87Geographic> Tbl87GeographicsList
    {
        get => _tbl87GeographicsList;
        set
        {
            _tbl87GeographicsList = value; OnPropertyChanged();
        }
    }
    private ObservableCollection<TblCountry> _tblCountriesAllList = null!;
    public ObservableCollection<TblCountry> TblCountriesAllList
    {
        get => _tblCountriesAllList;
        set
        {
            _tblCountriesAllList = value; OnPropertyChanged();
        }
    }
    private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList = null!;

    public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
    {
        get => _tbl66GenussesAllList;
        set
        {
            _tbl66GenussesAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList = null!;

    public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
    {
        get => _tbl68SpeciesgroupsAllList;
        set
        {
            _tbl68SpeciesgroupsAllList = value; OnPropertyChanged();
        }
    }


    private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesAllList = null!;

    public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesAllList
    {
        get => _tbl72PlSpeciessesAllList;
        set
        {
            _tbl72PlSpeciessesAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList = null!;
    public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
    {
        get => _tbl90AuthorsAllList;
        set
        {
            _tbl90AuthorsAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList = null!;
    public ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
    {
        get => _tbl90SourcesAllList;
        set
        {
            _tbl90SourcesAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList = null!;
    public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
    {
        get => _tbl90ExpertsAllList;
        set
        {
            _tbl90ExpertsAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl90Reference> _tbl90ReferenceAuthorsList = null!;
    public ObservableCollection<Tbl90Reference> Tbl90ReferenceAuthorsList
    {
        get => _tbl90ReferenceAuthorsList;
        set
        {
            _tbl90ReferenceAuthorsList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl90Reference> _tbl90ReferenceSourcesList = null!;
    public ObservableCollection<Tbl90Reference> Tbl90ReferenceSourcesList
    {
        get => _tbl90ReferenceSourcesList;
        set
        {
            _tbl90ReferenceSourcesList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl90Reference> _tbl90ReferenceExpertsList = null!;
    public ObservableCollection<Tbl90Reference> Tbl90ReferenceExpertsList
    {
        get => _tbl90ReferenceExpertsList;
        set
        {
            _tbl90ReferenceExpertsList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl93Comment> _tbl93CommentsList = null!;
    public ObservableCollection<Tbl93Comment> Tbl93CommentsList
    {
        get => _tbl93CommentsList;
        set
        {
            _tbl93CommentsList = value; OnPropertyChanged();
        }
    }


    //---------------------------------------------------------------------
    private int _fispeciesDataSetCount;
    public int FiSpeciesDataSetCount
    {
        get => _fispeciesDataSetCount;
        set
        {
            _fispeciesDataSetCount = value; OnPropertyChanged();
        }
    }

    private int _geographicDataSetCount;
    public int GeographicDataSetCount
    {
        get => _geographicDataSetCount;
        set
        {
            _geographicDataSetCount = value; OnPropertyChanged();
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
    private int _referenceSourceDataSetCount;
    public int ReferenceSourceDataSetCount
    {
        get => _referenceSourceDataSetCount;
        set
        {
            _referenceSourceDataSetCount = value; OnPropertyChanged();
        }
    }
    private int _referenceAuthorDataSetCount;
    public int ReferenceAuthorDataSetCount
    {
        get => _referenceAuthorDataSetCount;
        set
        {
            _referenceAuthorDataSetCount = value; OnPropertyChanged();
        }
    }

    private int _commentDataSetCount;
    public int CommentDataSetCount
    {
        get => _commentDataSetCount;
        set
        {
            _commentDataSetCount = value; OnPropertyChanged();
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

    private bool _isNewGeographic;
    public bool IsNewGeographic
    {
        get => _isNewGeographic;
        set => SetProperty(ref _isNewGeographic, value);
    }
    private bool _isNewName;
    public bool IsNewName
    {
        get => _isNewName;
        set => SetProperty(ref _isNewName, value);
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

    private void RefreshGeographicItems()
    {
        GeographicItems.Clear();
        if (Tbl87GeographicsList != null)
        {
            foreach (var item in Tbl87GeographicsList)
            {
                GeographicItems.Add(item);
            }
            if (Tbl87GeographicsList.Count == 0)
            {
                return;
            }

            if (GeographicSelected == null && Tbl87GeographicsList.Count != 0)
            {
                GeographicSelected = GeographicItems.FirstOrDefault()!;
            }
        }
    }

    #endregion

    #endregion

    public Tbl87GeographicsViewModel()
    {

    }

}
