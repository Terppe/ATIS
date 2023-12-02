
using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Data;
using Windows.ApplicationModel.Search;
using ATIS.WinUi.Models;
using Tbl18Superclass = ATIS.WinUi.Models.Tbl18Superclass;

//    Tbl90ReferencesViewModel Skriptdatum:  26.04.2023  10:32    

namespace ATIS.WinUi.ViewModels.Database;

public class Tbl90ReferencesViewModel : ObservableObject
{

    #region [Private Data Members]
    private readonly IDataService _dataService = null!;
    public ObservableCollection<Tbl90Reference> ReferenceItems { get; } = new();
    public int ZRef;
    public int ZCom;

    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]           

    #region [Constructor]
    public Tbl90ReferencesViewModel(IDataService dataService)
    {
        _dataService = dataService;
        GetAllCollections();
    }

    private void GetAllCollections()
    {
        Tbl03RegnumsAllList ??= new ObservableCollection<Tbl03Regnum>();
        Tbl03RegnumsAllList = _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnum()!;

        Tbl06PhylumsAllList ??= new ObservableCollection<Tbl06Phylum>();
        Tbl06PhylumsAllList = _dataService.GetTbl06PhylumsCollectionOrderByPhylumName();

        Tbl09DivisionsAllList ??= new ObservableCollection<Tbl09Division>();
        Tbl09DivisionsAllList = _dataService.GetTbl09DivisionsCollectionOrderByDivisionName();

        Tbl12SubphylumsAllList ??= new ObservableCollection<Tbl12Subphylum>();
        Tbl12SubphylumsAllList = _dataService.GetTbl12SubphylumsCollectionOrderBySubphylumName();

        Tbl15SubdivisionsAllList ??= new ObservableCollection<Tbl15Subdivision>();
        Tbl15SubdivisionsAllList = _dataService.GetTbl15SubdivisionsCollectionOrderBySubdivisionName();

        Tbl18SuperclassesAllList ??= new ObservableCollection<Tbl18Superclass>();
        Tbl18SuperclassesAllList = _dataService.GetTbl18SuperclassesCollectionOrderBySuperclassName();

        Tbl21ClassesAllList ??= new ObservableCollection<Tbl21Class>();
        Tbl21ClassesAllList = _dataService.GetTbl21ClassesCollectionOrderByClassName();

        Tbl24SubclassesAllList ??= new ObservableCollection<Tbl24Subclass>();
        Tbl24SubclassesAllList = _dataService.GetTbl24SubclassesCollectionOrderBySubclassName();

        Tbl27InfraclassesAllList ??= new ObservableCollection<Tbl27Infraclass>();
        Tbl27InfraclassesAllList = _dataService.GetTbl27InfraclassesCollectionOrderByInfraclassName();

        Tbl30LegiosAllList ??= new ObservableCollection<Tbl30Legio>();
        Tbl30LegiosAllList = _dataService.GetTbl30LegiosCollectionOrderByLegioName();

        Tbl33OrdosAllList ??= new ObservableCollection<Tbl33Ordo>();
        Tbl33OrdosAllList = _dataService.GetTbl33OrdosCollectionOrderByOrdoName();

        Tbl36SubordosAllList ??= new ObservableCollection<Tbl36Subordo>();
        Tbl36SubordosAllList = _dataService.GetTbl36SubordosCollectionOrderBySubordoName();

        Tbl39InfraordosAllList ??= new ObservableCollection<Tbl39Infraordo>();
        Tbl39InfraordosAllList = _dataService.GetTbl39InfraordosCollectionOrderByInfraordoName();

        Tbl42SuperfamiliesAllList ??= new ObservableCollection<Tbl42Superfamily>();
        Tbl42SuperfamiliesAllList = _dataService.GetTbl42SuperfamiliesCollectionOrderBySuperfamilyName();

        Tbl45FamiliesAllList ??= new ObservableCollection<Tbl45Family>();
        Tbl45FamiliesAllList = _dataService.GetTbl45FamiliesCollectionOrderByFamilyName();

        Tbl48SubfamiliesAllList ??= new ObservableCollection<Tbl48Subfamily>();
        Tbl48SubfamiliesAllList = _dataService.GetTbl48SubfamiliesCollectionOrderBySubfamilyName();

        Tbl51InfrafamiliesAllList ??= new ObservableCollection<Tbl51Infrafamily>();
        Tbl51InfrafamiliesAllList = _dataService.GetTbl51InfrafamiliesCollectionOrderByInfrafamilyName();

        Tbl54SupertribussesAllList ??= new ObservableCollection<Tbl54Supertribus>();
        Tbl54SupertribussesAllList = _dataService.GetTbl54SupertribussesCollectionOrderBySupertribusName();

        Tbl57TribussesAllList ??= new ObservableCollection<Tbl57Tribus>();
        Tbl57TribussesAllList = _dataService.GetTbl57TribussesCollectionOrderByTribusName();

        Tbl60SubtribussesAllList ??= new ObservableCollection<Tbl60Subtribus>();
        Tbl60SubtribussesAllList = _dataService.GetTbl60SubtribussesCollectionOrderBySubtribusName();

        Tbl63InfratribussesAllList ??= new ObservableCollection<Tbl63Infratribus>();
        Tbl63InfratribussesAllList = _dataService.GetTbl63InfratribussesCollectionOrderByInfratribusName();

        Tbl66GenussesAllList ??= new ObservableCollection<Tbl66Genus>();
        Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

        Tbl69FiSpeciessesAllList ??= new ObservableCollection<Tbl69FiSpecies>();
        Tbl69FiSpeciessesAllList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDivers();

        Tbl72PlSpeciessesAllList ??= new ObservableCollection<Tbl72PlSpecies>();
        Tbl72PlSpeciessesAllList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDivers();

        Tbl90ExpertsAllList ??= new ObservableCollection<Tbl90RefExpert>();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();
        Tbl90SourcesAllList ??= new ObservableCollection<Tbl90RefSource>();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();
        Tbl90AuthorsAllList ??= new ObservableCollection<Tbl90RefAuthor>();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();
    }

    #endregion [Constructor]  


    //    Part 1    



    #region [Commands Reference]

    public ICommand GetReferencesByNameOrIdCommand => new RelayCommand(execute: delegate
    {
        var task = GetReferencesByInfoOrId_Executed(SearchReferenceInfoOrId);
    });

    public ICommand AddReferenceCommand => new RelayCommand<string>(AddReference_Executed);
    public ICommand CopyReferenceCommand => new RelayCommand<string>(CopyReference_Executed);
    public ICommand DeleteReferenceCommand => new RelayCommand<string>(DeleteReference_Executed);

   // public ICommand DeleteReferenceCommand => new RelayCommand(execute: delegate { var task = DeleteReference_Executed(SearchReferenceInfoOrId); });

    public ICommand SaveReferenceCommand => new RelayCommand(execute: delegate { var task = SaveReference_Executed(SearchReferenceInfoOrId); });
    public ICommand RefreshReferenceServerCommand => new RelayCommand(execute: delegate { var task = RefreshReferenceServer_Executed(SearchReferenceInfoOrId); });

    #endregion [Commands Reference]       

    #region [Methods Reference]

    private async Task GetReferencesByInfoOrId_Executed(string searchInfo)
    {
        ReferenceStartModify();
        Tbl90ReferencesList?.Clear();
        ReferenceItems.Clear();

        Tbl90ReferencesList ??= new ObservableCollection<Tbl90Reference>();
        Tbl90ReferencesList =  _dataService.GetTbl90ReferencesCollectionOrderByInfoFromSearchInfoOrId(searchInfo);

        if (Tbl90ReferencesList.Count == 0)
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        ReferenceDataSetCount = Tbl90ReferencesList.Count;

        RefreshReferenceItems();

    }

    private void AddReference_Executed(string? parm)
    {
        ReferenceStartEdit();
        ReferenceStartNew();
        Tbl90ReferencesList ??= new ObservableCollection<Tbl90Reference>();
        Tbl90ReferencesList.Insert(index: 0, item: new Tbl90Reference { Info = "New" });

        RefreshReferenceItems();
    }

    private async void CopyReference_Executed(string? parm)
    {
        ReferenceStartEdit();
        ReferenceStartNew();

        Tbl90ReferencesList = await _dataService.CopyReference(ReferenceSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshReferenceItems();
    }

    private async void DeleteReference_Executed(string? parm)
    {

        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(ReferenceSelected!.Info!))
        {
            //necessary to delete before
            var ret = _dataService.DeleteReference(ReferenceSelected);
            if (!await ret)
            {
                return;
            }

            Tbl90ReferencesList =  _dataService.GetTbl90ReferencesCollectionOrderByInfoFromSearchInfoOrId(parm!);

            ReferenceDataSetCount = Tbl90ReferencesList.Count;
            RefreshReferenceItems();
        }
    }

    private async Task SaveReference_Executed(string searchInfo)
    {
        if (string.IsNullOrEmpty(ReferenceSelected.Info))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (ReferenceSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }

        // only one of 3 comboboxen RefExpertId, RefSourceId, RefAuthorId are to select 
        if (await GetReferenceCheck())
        {
            return;
        }

        // only one of comboboxes FiSpeciesId, PlSpeciesId, GenusId,.... are to select
        if (await GetRefComboboxCheck())
        {
            return;
        }

        ReferenceSelected ??= Tbl90ReferencesList[0];
        var iNdx = Tbl90ReferencesList.IndexOf(Tbl90ReferencesList.First(t =>
            t.Info == ReferenceSelected.Info));

        var ret = _dataService.SaveReference(ReferenceSelected, searchInfo);
        if (!await ret)
        {
            return;
        }

        if (string.IsNullOrEmpty(searchInfo))
        {
            Tbl90ReferencesList = await _dataService.GetLastDatasetInTbl90References();
            RefreshReferenceItems();
        }
        else
        {
            if (ReferenceSelected.ReferenceId == 0) //new
            {
                Tbl90ReferencesList = await _dataService.GetLastDatasetInTbl90References();
                RefreshReferenceItems();
            }
            else
            {
                Tbl90ReferencesList =  _dataService.GetTbl90ReferencesCollectionOrderByInfoFromSearchInfoOrId(searchInfo);
                //   Index Position ?
                if (iNdx < Tbl90ReferencesList.Count)
                {
                    ReferenceItems.Clear();
                    foreach (var item in Tbl90ReferencesList)
                    {
                        ReferenceItems.Add(item);
                    }

                    ReferenceSelected = Tbl90ReferencesList[iNdx];
                }
            }
        }
        ReferenceDataSetCount = Tbl90ReferencesList.Count;
        ReferenceCancelEditsAsync();
    }
    private async Task<bool> GetReferenceCheck()
    {
        if (ReferenceSelected.RefExpertId != null) { ZRef++; }

        if (ReferenceSelected.RefSourceId != null) { ZRef++; }

        if (ReferenceSelected.RefAuthorId != null) { ZRef++; }

        if (ZRef == 1)
        {
            return false;
        }

        await _allDialogs.ReferenceIncorrectWarnMessageDialogAsync();
        return true;
    }

    private async Task<bool> GetRefComboboxCheck()
    {
        if (ReferenceSelected.FiSpeciesId != null) { ZCom++; }
        if (ReferenceSelected.PlSpeciesId != null) { ZCom++; }
        if (ReferenceSelected.GenusId != null) { ZCom++; }
        if (ReferenceSelected.InfratribusId != null) { ZCom++; }
        if (ReferenceSelected.SubtribusId != null) { ZCom++; }
        if (ReferenceSelected.TribusId != null) { ZCom++; }
        if (ReferenceSelected.SupertribusId != null) { ZCom++; }
        if (ReferenceSelected.InfrafamilyId != null) { ZCom++; }
        if (ReferenceSelected.SubfamilyId != null) { ZCom++; }
        if (ReferenceSelected.FamilyId != null) { ZCom++; }
        if (ReferenceSelected.SuperfamilyId != null) { ZCom++; }
        if (ReferenceSelected.InfraordoId != null) { ZCom++; }
        if (ReferenceSelected.SubordoId != null) { ZCom++; }
        if (ReferenceSelected.OrdoId != null) { ZCom++; }
        if (ReferenceSelected.LegioId != null) { ZCom++; }
        if (ReferenceSelected.InfraclassId != null) { ZCom++; }
        if (ReferenceSelected.SubclassId != null) { ZCom++; }
        if (ReferenceSelected.ClassId != null) { ZCom++; }
        if (ReferenceSelected.SuperclassId != null) { ZCom++; }
        if (ReferenceSelected.SubdivisionId != null) { ZCom++; }
        if (ReferenceSelected.SubphylumId != null) { ZCom++; }
        if (ReferenceSelected.DivisionId != null) { ZCom++; }
        if (ReferenceSelected.PhylumId != null) { ZCom++; }
        if (ReferenceSelected.RegnumId != null) { ZCom++; }

        if (ZCom == 1)
        {
            return false;
        }

        await _allDialogs.TooMuchIdsSelectedWarnMessageDialogAsync();
        return true;
    }

    private Task RefreshReferenceServer_Executed(string? parm)
    {
        Tbl90ReferencesList ??= new ObservableCollection<Tbl90Reference>();
        Tbl90ReferencesList =  _dataService.GetTbl90ReferencesCollectionOrderByInfoFromSearchInfoOrId(parm!);

        ReferenceDataSetCount = Tbl90ReferencesList.Count;
        RefreshReferenceItems();
        return Task.CompletedTask;
    }

    public void ReferenceStartEdit() => IsInEdit = true;
    public void ReferenceStartModify() => IsModified = true;
    public void ReferenceStartNew() => IsNewReference = true;
    public event EventHandler AddNewReferenceCanceled = null!;
    public void ReferenceCancelEditsAsync()
    {
        if (IsNewReference)
        {
            AddNewReferenceCanceled?.Invoke(this, EventArgs.Empty);
            IsInEdit = false;
            IsNewReference = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }

    #endregion [Methods Reference]    




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



    #region All Properties

    #region Selected Properties

    private Tbl90Reference _referenceSelected = null!;
    public Tbl90Reference ReferenceSelected
    {
        get => _referenceSelected;
        set => SetProperty(ref _referenceSelected, value);
    }

    private Tbl90RefExpert _refExpertSelected = null!;
    public Tbl90RefExpert RefExpertSelected
    {
        get => _refExpertSelected;
        set => SetProperty(ref _refExpertSelected, value);
    }

    private Tbl90RefSource _refSourceSelected = null!;
    public Tbl90RefSource RefSourceSelected
    {
        get => _refSourceSelected;
        set => SetProperty(ref _refSourceSelected, value);
    }

    private Tbl90RefAuthor _refAuthorSelected = null!;
    public Tbl90RefAuthor RefAuthorSelected
    {
        get => _refAuthorSelected;
        set => SetProperty(ref _refAuthorSelected, value);
    }

    #endregion

    #region Refresh Properties

    private void RefreshReferenceItems()
    {
        ReferenceItems.Clear();
        foreach (var item in Tbl90ReferencesList)
        {
            ReferenceItems.Add(item);
        }
        if (Tbl90ReferencesList.Count == 0)
        {
            return;
        }

        if (ReferenceSelected == null && Tbl90ReferencesList.Count != 0)
        {
            ReferenceSelected = ReferenceItems.FirstOrDefault()!;
        }
    }
    //private void RefreshRefExpertItems()
    //{
    //    RefExpertItems.Clear();
    //    foreach (Tbl90RefExpert item in Tbl90RefExpertsList)
    //    {
    //        RefExpertItems.Add(item);
    //    }
    //    //     RefExpertSelected ??= ExpertItems.First();
    //    if (RefExpertSelected == null && Tbl90RefExpertsList.Count != 0)
    //    {
    //        RefExpertSelected = RefExpertItems.First();
    //    }
    //}

    //private void RefreshRefSourceItems()
    //{
    //    RefSourceItems.Clear();

    //    foreach (Tbl90RefSource item in Tbl90RefSourcesList)
    //    {
    //        RefSourceItems.Add(item);
    //    }
    //    //     RefSourceSelected ??= RefSourceItems.First();
    //    if (RefSourceSelected == null && Tbl90RefSourcesList.Count != 0)
    //    {
    //        RefSourceSelected = RefSourceItems.First();
    //    }
    //}

    //private void RefreshRefAuthorItems()
    //{
    //    RefAuthorItems.Clear();
    //    foreach (Tbl90RefAuthor item in Tbl90RefAuthorsList)
    //    {
    //        RefAuthorItems.Add(item);
    //    }
    //    //     RefAuthorSelected ??= AuthorItems.First();
    //    if (RefAuthorSelected == null && Tbl90RefAuthorsList.Count != 0)
    //    {
    //        RefAuthorSelected = RefAuthorItems.First();
    //    }
    //}


    #endregion Refresh Properties

    #region Public Properties  

    private int _referenceDataSetCount;
    public int ReferenceDataSetCount
    {
        get => _referenceDataSetCount;
        set
        {
            _referenceDataSetCount = value; OnPropertyChanged(nameof(ReferenceDataSetCount));
        }
    }

    private string _searchReferenceInfoOrId = "";

    public string SearchReferenceInfoOrId
    {
        get => _searchReferenceInfoOrId;
        set
        {
            _searchReferenceInfoOrId = value; OnPropertyChanged(nameof(SearchReferenceInfoOrId));
        }
    }

    private ObservableCollection<Tbl90Reference> _tbl90ReferencesList = null!;

    public ObservableCollection<Tbl90Reference> Tbl90ReferencesList
    {
        get => _tbl90ReferencesList;
        set
        {
            _tbl90ReferencesList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList = null!;
    public ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
    {
        get => _tbl03RegnumsAllList;
        set
        {
            _tbl03RegnumsAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList = null!;
    public ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
    {
        get => _tbl06PhylumsAllList;
        set
        {
            _tbl06PhylumsAllList = value; OnPropertyChanged(nameof(Tbl06PhylumsAllList));
        }
    }

    private ObservableCollection<Tbl09Division> _tbl09DivisionsAllList = null!;
    public ObservableCollection<Tbl09Division> Tbl09DivisionsAllList
    {
        get => _tbl09DivisionsAllList;
        set
        {
            _tbl09DivisionsAllList = value; OnPropertyChanged(nameof(Tbl09DivisionsAllList));
        }
    }

    private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsAllList = null!;
    public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsAllList
    {
        get => _tbl12SubphylumsAllList;
        set
        {
            _tbl12SubphylumsAllList = value; OnPropertyChanged(nameof(Tbl12SubphylumsAllList));
        }
    }

    private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsAllList = null!;
    public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsAllList
    {
        get => _tbl15SubdivisionsAllList;
        set
        {
            _tbl15SubdivisionsAllList = value; OnPropertyChanged(nameof(Tbl15SubdivisionsAllList));
        }
    }

    private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesAllList = null!;
    public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesAllList
    {
        get => _tbl18SuperclassesAllList;
        set
        {
            _tbl18SuperclassesAllList = value; OnPropertyChanged(nameof(Tbl18SuperclassesAllList));
        }
    }

    private ObservableCollection<Tbl21Class> _tbl21ClassesAllList = null!;
    public ObservableCollection<Tbl21Class> Tbl21ClassesAllList
    {
        get => _tbl21ClassesAllList;
        set
        {
            _tbl21ClassesAllList = value; OnPropertyChanged(nameof(Tbl21ClassesAllList));
        }
    }

    private ObservableCollection<Tbl24Subclass> _tbl24SubclassesAllList = null!;
    public ObservableCollection<Tbl24Subclass> Tbl24SubclassesAllList
    {
        get => _tbl24SubclassesAllList;
        set
        {
            _tbl24SubclassesAllList = value; OnPropertyChanged(nameof(Tbl24SubclassesAllList));
        }
    }

    private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesAllList = null!;
    public ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesAllList
    {
        get => _tbl27InfraclassesAllList;
        set
        {
            _tbl27InfraclassesAllList = value; OnPropertyChanged(nameof(Tbl27InfraclassesAllList));
        }
    }

    private ObservableCollection<Tbl30Legio> _tbl30LegiosAllList = null!;
    public ObservableCollection<Tbl30Legio> Tbl30LegiosAllList
    {
        get => _tbl30LegiosAllList;
        set
        {
            _tbl30LegiosAllList = value; OnPropertyChanged(nameof(Tbl30LegiosAllList));
        }
    }

    private ObservableCollection<Tbl33Ordo> _tbl33OrdosAllList = null!;
    public ObservableCollection<Tbl33Ordo> Tbl33OrdosAllList
    {
        get => _tbl33OrdosAllList;
        set
        {
            _tbl33OrdosAllList = value; OnPropertyChanged(nameof(Tbl33OrdosAllList));
        }
    }

    private ObservableCollection<Tbl36Subordo> _tbl36SubordosAllList = null!;
    public ObservableCollection<Tbl36Subordo> Tbl36SubordosAllList
    {
        get => _tbl36SubordosAllList;
        set
        {
            _tbl36SubordosAllList = value; OnPropertyChanged(nameof(Tbl36SubordosAllList));
        }
    }

    private ObservableCollection<Tbl39Infraordo> _tbl39InfraordosAllList = null!;
    public ObservableCollection<Tbl39Infraordo> Tbl39InfraordosAllList
    {
        get => _tbl39InfraordosAllList;
        set
        {
            _tbl39InfraordosAllList = value; OnPropertyChanged(nameof(Tbl39InfraordosAllList));
        }
    }

    private ObservableCollection<Tbl42Superfamily> _tbl42SuperfamiliesAllList = null!;
    public ObservableCollection<Tbl42Superfamily> Tbl42SuperfamiliesAllList
    {
        get => _tbl42SuperfamiliesAllList;
        set
        {
            _tbl42SuperfamiliesAllList = value; OnPropertyChanged(nameof(Tbl42SuperfamiliesAllList));
        }
    }

    private ObservableCollection<Tbl45Family> _tbl45FamiliesAllList = null!;
    public ObservableCollection<Tbl45Family> Tbl45FamiliesAllList
    {
        get => _tbl45FamiliesAllList;
        set
        {
            _tbl45FamiliesAllList = value; OnPropertyChanged(nameof(Tbl45FamiliesAllList));
        }
    }

    private ObservableCollection<Tbl48Subfamily> _tbl48SubfamiliesAllList = null!;
    public ObservableCollection<Tbl48Subfamily> Tbl48SubfamiliesAllList
    {
        get => _tbl48SubfamiliesAllList;
        set
        {
            _tbl48SubfamiliesAllList = value; OnPropertyChanged(nameof(Tbl48SubfamiliesAllList));
        }
    }

    private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesAllList = null!;
    public ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesAllList
    {
        get => _tbl51InfrafamiliesAllList;
        set
        {
            _tbl51InfrafamiliesAllList = value; OnPropertyChanged(nameof(Tbl51InfrafamiliesAllList));
        }
    }

    private ObservableCollection<Tbl54Supertribus> _tbl54SupertribussesAllList = null!;
    public ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesAllList
    {
        get => _tbl54SupertribussesAllList;
        set
        {
            _tbl54SupertribussesAllList = value; OnPropertyChanged(nameof(Tbl54SupertribussesAllList));
        }
    }

    private ObservableCollection<Tbl57Tribus> _tbl57TribussesAllList = null!;
    public ObservableCollection<Tbl57Tribus> Tbl57TribussesAllList
    {
        get => _tbl57TribussesAllList;
        set
        {
            _tbl57TribussesAllList = value; OnPropertyChanged(nameof(Tbl57TribussesAllList));
        }
    }

    private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesAllList = null!;
    public ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesAllList
    {
        get => _tbl60SubtribussesAllList;
        set
        {
            _tbl60SubtribussesAllList = value; OnPropertyChanged(nameof(Tbl60SubtribussesAllList));
        }
    }

    private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesAllList = null!;
    public ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesAllList
    {
        get => _tbl63InfratribussesAllList;
        set
        {
            _tbl63InfratribussesAllList = value; OnPropertyChanged(nameof(Tbl63InfratribussesAllList));
        }
    }

    private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList = null!;
    public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
    {
        get => _tbl66GenussesAllList;
        set
        {
            _tbl66GenussesAllList = value; OnPropertyChanged(nameof(Tbl66GenussesAllList));
        }
    }

    private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList = null!;
    public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
    {
        get => _tbl69FiSpeciessesAllList;
        set
        {
            _tbl69FiSpeciessesAllList = value; OnPropertyChanged(nameof(Tbl69FiSpeciessesAllList));
        }
    }

    private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesAllList = null!;
    public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesAllList
    {
        get => _tbl72PlSpeciessesAllList;
        set
        {
            _tbl72PlSpeciessesAllList = value; OnPropertyChanged(nameof(Tbl72PlSpeciessesAllList));
        }
    }

    private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList = null!;
    public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
    {
        get => _tbl90ExpertsAllList;
        set
        {
            _tbl90ExpertsAllList = value; OnPropertyChanged(nameof(Tbl90ExpertsAllList));
        }
    }

    private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList = null!;
    public ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
    {
        get => _tbl90SourcesAllList;
        set
        {
            _tbl90SourcesAllList = value; OnPropertyChanged(nameof(Tbl90SourcesAllList));
        }
    }
    private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList = null!;
    public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
    {
        get => _tbl90AuthorsAllList;
        set
        {
            _tbl90AuthorsAllList = value; OnPropertyChanged(nameof(Tbl90AuthorsAllList));
        }
    }
    private ObservableCollection<Tbl90RefExpert> _tbl90RefExpertsList = null!;
    public ObservableCollection<Tbl90RefExpert> Tbl90RefExpertsList
    {
        get => _tbl90RefExpertsList;
        set
        {
            _tbl90RefExpertsList = value; OnPropertyChanged(nameof(Tbl90RefExpertsList));
        }
    }


    private ObservableCollection<Tbl90RefSource> _tbl90RefSourcesList = null!;
    public ObservableCollection<Tbl90RefSource> Tbl90RefSourcesList
    {
        get => _tbl90RefSourcesList;
        set
        {
            _tbl90RefSourcesList = value; OnPropertyChanged(nameof(Tbl90RefSourcesList));
        }
    }

    private ObservableCollection<Tbl90RefAuthor> _tbl90RefAuthorsList = null!;
    public ObservableCollection<Tbl90RefAuthor> Tbl90RefAuthorsList
    {
        get => _tbl90RefAuthorsList;
        set
        {
            _tbl90RefAuthorsList = value; OnPropertyChanged(nameof(Tbl90RefAuthorsList));
        }
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

    private bool _isNewReference;
    public bool IsNewReference
    {
        get => _isNewReference;
        set => SetProperty(ref _isNewReference, value);
    }

    #endregion Public Properties

    #endregion All Properties

    public Tbl90ReferencesViewModel()
    {

    }



    #region Methode AllLists

//    private void ConnectedAllLists()
//{
//    if (Tbl03RegnumsAllList == null)
//        Tbl03RegnumsAllList ??= new ObservableCollection<Tbl03Regnum>();
//    else
//        Tbl03RegnumsAllList.Clear();
//    Tbl03RegnumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl03Regnum>("Regnum");

//    if (Tbl06PhylumsAllList == null)
//        Tbl06PhylumsAllList ??= new ObservableCollection<Tbl06Phylum>();
//    else
//        Tbl06PhylumsAllList.Clear();
//    Tbl06PhylumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl06Phylum>("Phylum");

//    if (Tbl09DivisionsAllList == null)
//        Tbl09DivisionsAllList ??= new ObservableCollection<Tbl09Division>();
//    else
//        Tbl09DivisionsAllList.Clear();
//    Tbl09DivisionsAllList = _extCrud.GetCollectionAllOrderBy<Tbl09Division>("Division");

//    if (Tbl12SubphylumsAllList == null)
//        Tbl12SubphylumsAllList ??= new ObservableCollection<Tbl12Subphylum>();
//    else
//        Tbl12SubphylumsAllList.Clear();
//    Tbl12SubphylumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl12Subphylum>("Subphylum");

//    if (Tbl15SubdivisionsAllList == null)
//        Tbl15SubdivisionsAllList ??= new ObservableCollection<Tbl15Subdivision>();
//    else
//        Tbl15SubdivisionsAllList.Clear();
//    Tbl15SubdivisionsAllList = _extCrud.GetCollectionAllOrderBy<Tbl15Subdivision>("Subdivision");

//    if (Tbl18SuperclassesAllList == null)
//        Tbl18SuperclassesAllList ??= new ObservableCollection<Tbl18Superclass>();
//    else
//        Tbl18SuperclassesAllList.Clear();
//    Tbl18SuperclassesAllList = _extCrud.GetCollectionAllOrderBy<Tbl18Superclass>("Superclass");

//    if (Tbl21ClassesAllList == null)
//        Tbl21ClassesAllList ??= new ObservableCollection<Tbl21Class>();
//    else
//        Tbl21ClassesAllList.Clear();
//    Tbl21ClassesAllList = _extCrud.GetCollectionAllOrderBy<Tbl21Class>("Class");

//    if (Tbl24SubclassesAllList == null)
//        Tbl24SubclassesAllList ??= new ObservableCollection<Tbl24Subclass>();
//    else
//        Tbl24SubclassesAllList.Clear();
//    Tbl24SubclassesAllList = _extCrud.GetCollectionAllOrderBy<Tbl24Subclass>("Subclass");

//    if (Tbl27InfraclassesAllList == null)
//        Tbl27InfraclassesAllList ??= new ObservableCollection<Tbl27Infraclass>();
//    else
//        Tbl27InfraclassesAllList.Clear();
//    Tbl27InfraclassesAllList = _extCrud.GetCollectionAllOrderBy<Tbl27Infraclass>("Infraclass");

//    if (Tbl30LegiosAllList == null)
//        Tbl30LegiosAllList ??= new ObservableCollection<Tbl30Legio>();
//    else
//        Tbl30LegiosAllList.Clear();
//    Tbl30LegiosAllList = _extCrud.GetCollectionAllOrderBy<Tbl30Legio>("Legio");

//    if (Tbl33OrdosAllList == null)
//        Tbl33OrdosAllList ??= new ObservableCollection<Tbl33Ordo>();
//    else
//        Tbl33OrdosAllList.Clear();
//    Tbl33OrdosAllList = _extCrud.GetCollectionAllOrderBy<Tbl33Ordo>("Ordo");

//    if (Tbl36SubordosAllList == null)
//        Tbl36SubordosAllList ??= new ObservableCollection<Tbl36Subordo>();
//    else
//        Tbl36SubordosAllList.Clear();
//    Tbl36SubordosAllList = _extCrud.GetCollectionAllOrderBy<Tbl36Subordo>("Subordo");

//    if (Tbl39InfraordosAllList == null)
//        Tbl39InfraordosAllList ??= new ObservableCollection<Tbl39Infraordo>();
//    else
//        Tbl39InfraordosAllList.Clear();
//    Tbl39InfraordosAllList = _extCrud.GetCollectionAllOrderBy<Tbl39Infraordo>("Infraordo");

//    if (Tbl42SuperfamiliesAllList == null)
//        Tbl42SuperfamiliesAllList ??= new ObservableCollection<Tbl42Superfamily>();
//    else
//        Tbl42SuperfamiliesAllList.Clear();
//    Tbl42SuperfamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl42Superfamily>("Superfamily");

//    if (Tbl45FamiliesAllList == null)
//        Tbl45FamiliesAllList ??= new ObservableCollection<Tbl45Family>();
//    else
//        Tbl45FamiliesAllList.Clear();
//    Tbl45FamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl45Family>("Family");

//    if (Tbl48SubfamiliesAllList == null)
//        Tbl48SubfamiliesAllList ??= new ObservableCollection<Tbl48Subfamily>();
//    else
//        Tbl48SubfamiliesAllList.Clear();
//    Tbl48SubfamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl48Subfamily>("Subfamily");

//    if (Tbl51InfrafamiliesAllList == null)
//        Tbl51InfrafamiliesAllList ??= new ObservableCollection<Tbl51Infrafamily>();
//    else
//        Tbl51InfrafamiliesAllList.Clear();
//    Tbl51InfrafamiliesAllList = _extCrud.GetCollectionAllOrderBy<Tbl51Infrafamily>("Infrafamily");

//    if (Tbl54SupertribussesAllList == null)
//        Tbl54SupertribussesAllList ??= new ObservableCollection<Tbl54Supertribus>();
//    else
//        Tbl54SupertribussesAllList.Clear();
//    Tbl54SupertribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl54Supertribus>("Supertribus");

//    if (Tbl57TribussesAllList == null)
//        Tbl57TribussesAllList ??= new ObservableCollection<Tbl57Tribus>();
//    else
//        Tbl57TribussesAllList.Clear();
//    Tbl57TribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl57Tribus>("Tribus");

//    if (Tbl60SubtribussesAllList == null)
//        Tbl60SubtribussesAllList ??= new ObservableCollection<Tbl60Subtribus>();
//    else
//        Tbl60SubtribussesAllList.Clear();
//    Tbl60SubtribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl60Subtribus>("Subtribus");

//    if (Tbl63InfratribussesAllList == null)
//        Tbl63InfratribussesAllList ??= new ObservableCollection<Tbl63Infratribus>();
//    else
//        Tbl63InfratribussesAllList.Clear();
//    Tbl63InfratribussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl63Infratribus>("Infratribus");

//    if (Tbl66GenussesAllList == null)
//        Tbl66GenussesAllList ??= new ObservableCollection<Tbl66Genus>();
//    else
//        Tbl66GenussesAllList.Clear();
//    Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("Genus");

//    if (Tbl69FiSpeciessesAllList == null)
//        Tbl69FiSpeciessesAllList ??= new ObservableCollection<Tbl69FiSpecies>();
//    else
//        Tbl69FiSpeciessesAllList.Clear();
//    Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("FiSpecies");

//    if (Tbl72PlSpeciessesAllList == null)
//        Tbl72PlSpeciessesAllList ??= new ObservableCollection<Tbl72PlSpecies>();
//    else
//        Tbl72PlSpeciessesAllList.Clear();
//    Tbl72PlSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl72PlSpecies>("PlSpecies");


//    if (Tbl90RefExpertsAllList == null)
//        Tbl90RefExpertsAllList ??= new ObservableCollection<Tbl90RefExpert>();
//    else
//        Tbl90RefExpertsAllList.Clear();
//    Tbl90RefExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");

//    if (Tbl90RefSourcesAllList == null)
//        Tbl90RefSourcesAllList ??= new ObservableCollection<Tbl90RefSource>();
//    else
//        Tbl90RefSourcesAllList.Clear();
//    Tbl90RefSourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

//    if (Tbl90RefAuthorsAllList == null)
//        Tbl90RefAuthorsAllList ??= new ObservableCollection<Tbl90RefAuthor>();
//    else
//        Tbl90RefAuthorsAllList.Clear();
//    Tbl90RefAuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("Author");
//    }

        #endregion     
 






   
}   
