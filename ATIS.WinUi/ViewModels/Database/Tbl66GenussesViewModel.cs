
using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using ATIS.WinUi.Services;
using CommunityToolkit.Mvvm.ComponentModel;

//    Tbl66GenussesViewModel Skriptdatum:  01.04.2023  10:32    

namespace ATIS.WinUi.ViewModels.Database;

public class Tbl66GenussesViewModel : ObservableObject
{

    #region [Private Data Members]
    private readonly IDataService _dataService;
    public ObservableCollection<Tbl66Genus?> GenusItems { get; } = new();
    public ObservableCollection<Tbl69FiSpecies> FiSpeciesItems { get; } = new();

    public ObservableCollection<Tbl72PlSpecies> PlSpeciesItems { get; } = new();
    public ObservableCollection<Tbl63Infratribus> InfratribusItems { get; } = new();

    public ObservableCollection<Tbl90Reference> ReferenceExpertItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceSourceItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceAuthorItems { get; } = new();
    public ObservableCollection<Tbl93Comment> CommentItems { get; } = new();
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]      

    #region [Constructor]
    public Tbl66GenussesViewModel(IDataService dataService)
    {
        _dataService = dataService;
        SelectedMainDetailTabIndex = 1; //Tab Datagrid
        GetAllCollections();
    }

    private void GetAllCollections()
    {
        Tbl68SpeciesgroupsAllList ??= new ObservableCollection<Tbl68Speciesgroup>();
        Tbl68SpeciesgroupsAllList = _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroup();
        Tbl66GenussesAllList ??= new ObservableCollection<Tbl66Genus>();
        Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();
        Tbl63InfratribussesAllList ??= new ObservableCollection<Tbl63Infratribus>();
        Tbl63InfratribussesAllList = _dataService.GetTbl63InfratribussesCollectionOrderByInfratribusName();
        Tbl60SubtribussesAllList ??= new ObservableCollection<Tbl60Subtribus>();
        Tbl60SubtribussesAllList = _dataService.GetTbl60SubtribussesCollectionOrderBySubtribusName();

        Tbl90ExpertsAllList ??= new ObservableCollection<Tbl90RefExpert>();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();
        Tbl90SourcesAllList ??= new ObservableCollection<Tbl90RefSource>();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();
        Tbl90AuthorsAllList ??= new ObservableCollection<Tbl90RefAuthor>();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();
    }

    #endregion [Constructor]  


    //    Part 1    



    #region [Commands Genus]

    public ICommand GetGenussesByNameOrIdCommand => new RelayCommand(execute: delegate
    { var task = GetGenussesByNameOrId_Executed(SearchGenusName); });

    public ICommand AddGenusCommand => new RelayCommand<string>(AddGenus_Executed);
    public ICommand CopyGenusCommand => new RelayCommand<string>(CopyGenus_Executed);

    public ICommand DeleteGenusCommand => new RelayCommand(execute: delegate { DeleteGenus_Executed(SearchGenusName); });

    public ICommand SaveGenusCommand => new RelayCommand(execute: delegate { var task = SaveGenus_Executed(SearchGenusName); });
    public ICommand RefreshGenusServerCommand => new RelayCommand(execute: delegate { RefreshGenusServer_Executed(SearchGenusName); });

    #endregion [Commands Genus]       

    #region [Methods Tbl66Genus]

    private async Task GetGenussesByNameOrId_Executed(string? parm)
    {
        IsLoading = true;
        GenusStartModify();
        Tbl63InfratribussesList?.Clear();
        Tbl66GenussesList?.Clear();
        Tbl69FiSpeciessesList?.Clear();
        Tbl90ReferenceExpertsList?.Clear();
        Tbl90ReferenceSourcesList?.Clear();
        Tbl90ReferenceAuthorsList?.Clear();
        Tbl93CommentsList?.Clear();

        InfratribusItems.Clear();
        GenusItems.Clear();
        FiSpeciesItems.Clear();
        ReferenceAuthorItems.Clear();
        ReferenceSourceItems.Clear();
        ReferenceExpertItems.Clear();
        CommentItems.Clear();

        Tbl66GenussesList ??= new ObservableCollection<Tbl66Genus>();
        Tbl66GenussesList = await _dataService.GetTbl66GenussesCollectionOrderByGenusNameFromSearchNameOrId(parm!);

        if (Tbl66GenussesList is { Count: 0 })
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        GenusDataSetCount = Tbl66GenussesList.Count;
        RefreshGenusItems();

        SelectedMainDetailTabIndex = 1;
        IsLoading = false;
    }

    private async void AddGenus_Executed(string? parm)
    {
        GenusStartEdit();
        GenusStartNew();

        //Id search for first Dataset of Tbl63InfratribussesList
        var single = await _dataService.GetInfratribusSingleFirstDataset();
        var id = single.InfratribusId;

        Tbl66GenussesList ??= new ObservableCollection<Tbl66Genus>();
        Tbl66GenussesList.Insert(index: 0, item: new Tbl66Genus { GenusName = "New", InfratribusId = id });

        RefreshGenusItems();
    }
    private async void CopyGenus_Executed(string? parm)
    {
        GenusStartEdit();
        GenusStartNew();

        Tbl66GenussesList = await _dataService.CopyGenus(GenusSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshGenusItems();
    }
    private async void DeleteGenus_Executed(string? parm)
    {

        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(GenusSelected!.GenusName!))
        {
            //necessary to delete before
            await _dataService.DeleteConnectedFiSpeciesses(GenusSelected);
            await _dataService.DeleteConnectedPlSpeciesses(GenusSelected);
            await _dataService.DeleteConnectedGenusReferences(GenusSelected);
            await _dataService.DeleteConnectedGenusComments(GenusSelected);

            var ret = _dataService.DeleteGenus(GenusSelected);
            if (!await ret)
            {
                return;
            }

            Tbl66GenussesList = await _dataService.GetTbl66GenussesCollectionOrderByGenusNameFromSearchNameOrId(parm!);

            GenusDataSetCount = Tbl66GenussesList.Count;
            RefreshGenusItems();
        }
    }
    private async Task SaveGenus_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(GenusSelected?.GenusName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl66GenussesList != null)
        {

            var iNdx = Tbl66GenussesList.IndexOf(Tbl66GenussesList.First(t =>
                 t.GenusName == GenusSelected.GenusName));

            var ret = _dataService.SaveGenus(GenusSelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl66GenussesList = await _dataService.GetLastDatasetInTbl66Genusses();
                RefreshGenusItems();
            }
            else
            {
                if (GenusSelected.GenusId == 0) //new
                {
                    Tbl66GenussesList = await _dataService.GetLastDatasetInTbl66Genusses();
                    RefreshGenusItems();
                }
                else
                {
                    Tbl66GenussesList = await _dataService.GetTbl66GenussesCollectionOrderByGenusNameFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl66GenussesList!.Count)
                    {
                        GenusItems.Clear();
                        foreach (var item in Tbl66GenussesList)
                        {
                            GenusItems.Add(item);
                        }

                        GenusSelected = Tbl66GenussesList[iNdx];
                    }
                }
            }
        }
        GenusDataSetCount = Tbl66GenussesList!.Count;
        GenusCancelEditsAsync();
    }
    private async void RefreshGenusServer_Executed(string? parm)
    {
        Tbl66GenussesList = await _dataService.GetTbl66GenussesCollectionOrderByGenusNameFromSearchNameOrId(parm!);

        GenusDataSetCount = Tbl66GenussesList.Count;
        RefreshGenusItems();
    }
    public void GenusStartEdit() => IsInEdit = true;
    public void GenusStartModify() => IsModified = true;
    public void GenusStartNew() => IsNewGenus = true;
    public event EventHandler AddNewGenusCanceled = null!;
    public void GenusCancelEditsAsync()
    {
        if (IsNewGenus)
        {
            IsInEdit = false;
            AddNewGenusCanceled?.Invoke(this, EventArgs.Empty);
            IsNewGenus = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }

    #endregion [Methods Tbl66Genus]    




    //    Part 2    


    #region "Public Commands Connect <== Tbl63Infratribus"                 

    public ICommand SaveInfratribusCommand => new RelayCommand<string>(SaveInfratribus_Executed);
    public ICommand RefreshInfratribusServerCommand => new RelayCommand(execute: delegate { RefreshInfratribusServer_Executed(SearchGenusName); });

    private async void SaveInfratribus_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(InfratribusSelected?.InfratribusName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl63InfratribussesList != null)
        {
            var iNdx = Tbl63InfratribussesList.IndexOf(Tbl63InfratribussesList.First(t =>
               t.InfratribusName == InfratribusSelected.InfratribusName));

            var ret = _dataService.SaveInfratribus(InfratribusSelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl63InfratribussesList = await _dataService.GetLastDatasetInTbl63Infratribusses();
                RefreshInfratribusItems();
            }
            else
            {
                if (InfratribusSelected.InfratribusId == 0) //new
                {
                    Tbl63InfratribussesList = await _dataService.GetLastDatasetInTbl63Infratribusses();
                    RefreshInfratribusItems();
                }
                else
                {
                    Tbl63InfratribussesList = await _dataService.GetTbl63InfratribussesCollectionOrderByInfratribusNameFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl63InfratribussesList!.Count)
                    {
                        InfratribusItems.Clear();
                        foreach (var item in Tbl63InfratribussesList)
                        {
                            InfratribusItems.Add(item);
                        }

                        InfratribusSelected = Tbl63InfratribussesList[iNdx];
                    }
                }
            }
        }

        InfratribusDataSetCount = Tbl63InfratribussesList!.Count;
        InfratribusCancelEditsAsync();
    }
    private async void RefreshInfratribusServer_Executed(string? parm)
    {
        Tbl63InfratribussesList = await _dataService.GetTbl63InfratribussesCollectionOrderByInfratribusNameFromSearchNameOrId(parm!);

        InfratribusDataSetCount = Tbl63InfratribussesList.Count;
        RefreshInfratribusItems();
    }
    public void InfratribusStartEdit() => IsInEdit = true;
    public void InfratribusStartModify() => IsModified = true;
    public void InfratribusCancelEditsAsync()
    {
        IsInEdit = false;
        IsModified = false;
    }

    #endregion "Public Commands"                  



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
        Tbl69FiSpeciessesList.Insert(0, new Tbl69FiSpecies { FiSpeciesName = "New", GenusId = GenusSelected.GenusId });

        FiSpeciesItems.Clear();
        foreach (var item in Tbl69FiSpeciessesList)
        {
            FiSpeciesItems.Add(item);
        }
        FiSpeciesSelected = FiSpeciesItems.First();
    }

    private async void CopyFiSpecies_Executed(string? parm)
    {
        FiSpeciesStartEdit();
        FiSpeciesStartNew();
        FiSpeciesSelected.GenusId = GenusSelected.GenusId;  //combo vorbelegen

        Tbl69FiSpeciessesList = await _dataService.CopyFiSpecies(FiSpeciesSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshFiSpeciesItems();
    }

    private async void DeleteFiSpecies_Executed(string? s)
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
        if (FiSpeciesSelected != null && string.IsNullOrEmpty(FiSpeciesSelected.FiSpeciesName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }

        if (Tbl69FiSpeciessesList != null)
        {
            var indx = Tbl69FiSpeciessesList.IndexOf(Tbl69FiSpeciessesList.First(t =>
                FiSpeciesSelected != null && t.FiSpeciesName == FiSpeciesSelected.FiSpeciesName));

            if (FiSpeciesSelected != null)
            {
                if (GenusSelected != null)
                {
                    FiSpeciesSelected.GenusId = GenusSelected.GenusId;

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
                        Tbl69FiSpeciessesList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromGenusId(FiSpeciesSelected.GenusId);
                        //   Index Position ?
                        if (indx < Tbl69FiSpeciessesList.Count)
                        {
                            FiSpeciesItems.Clear();
                            foreach (var item in Tbl69FiSpeciessesList)
                            {
                                FiSpeciesItems.Add(item);
                            }

                            FiSpeciesSelected = Tbl69FiSpeciessesList[indx]; //Index
                        }
                    }
                }
            }
        }
    }

    private void RefreshFiSpeciesServer_Executed(string? parm)
    {
        Tbl69FiSpeciessesList ??= new ObservableCollection<Tbl69FiSpecies>();
        Tbl69FiSpeciessesList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromGenusId(GenusSelected.GenusId);

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
        Tbl72PlSpeciessesList ??= new ObservableCollection<Tbl72PlSpecies>();
        Tbl72PlSpeciessesList.Insert(index: 0, item: new Tbl72PlSpecies { PlSpeciesName = "New" });

        RefreshPlSpeciesItems();
    }
    private async void CopyPlSpecies_Executed(string? parm)
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
    private async void DeletePlSpecies_Executed(string? parm)
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

        Tbl72PlSpeciessesList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromGenusId(PlSpeciesSelected.GenusId);

        RefreshPlSpeciesItems();
    }
    private async void SavePlSpecies_Executed(string? parm)
    {
        if (PlSpeciesSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        PlSpeciesSelected ??= Tbl72PlSpeciessesList[0];
        var indx = Tbl72PlSpeciessesList.IndexOf(Tbl72PlSpeciessesList.First(t =>
            t.PlSpeciesName == PlSpeciesSelected.PlSpeciesName));

        PlSpeciesSelected.GenusId = GenusSelected.GenusId;

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
    private void RefreshPlSpeciesServer_Executed(string? parm)
    {
        Tbl72PlSpeciessesList ??= new ObservableCollection<Tbl72PlSpecies>();
        Tbl72PlSpeciessesList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromGenusId(GenusSelected.GenusId);

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


    #region [Commands Genus ==> Tbl90Reference Author]
    public ICommand AddReferenceAuthorCommand => new RelayCommand<string>(AddReferenceAuthor_Executed);
    public ICommand CopyReferenceAuthorCommand => new RelayCommand<string>(CopyReferenceAuthor_Executed);
    public ICommand DeleteReferenceAuthorCommand => new RelayCommand<string>(DeleteReferenceAuthor_Executed);
    public ICommand SaveReferenceAuthorCommand => new RelayCommand<string>(SaveReferenceAuthor_Executed);
    public ICommand RefreshReferenceAuthorServerCommand => new RelayCommand<string>(RefreshReferenceAuthorServer_Executed);
    #endregion [Commands Genus ==> Tbl90Reference Author]                

    #region [Methods Genus ==> Tbl90Reference Author]

    private void AddReferenceAuthor_Executed(string? parm)
    {
        ReferenceAuthorStartEdit();
        ReferenceAuthorStartNew();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

        Tbl90ReferenceAuthorsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", GenusId = GenusSelected!.GenusId });

        ReferenceAuthorDataSetCount = Tbl90ReferenceAuthorsList.Count;

        ReferenceAuthorItems.Clear();
        foreach (var item in Tbl90ReferenceAuthorsList)
        {
            ReferenceAuthorItems.Add(item);
        }
        ReferenceAuthorSelected = ReferenceAuthorItems.First();
    }

    private async void CopyReferenceAuthor_Executed(string? parm)
    {
        ReferenceAuthorStartEdit();
        ReferenceAuthorStartNew();
        ReferenceAuthorSelected.GenusId = GenusSelected.GenusId; //combo vorbelegen

        Tbl90ReferenceAuthorsList = await _dataService.CopyReferenceGenus(ReferenceAuthorSelected, "Author");

        ReferenceAuthorDataSetCount = Tbl90ReferenceAuthorsList.Count;
        RefreshReferenceAuthorItems();
    }

    private async void DeleteReferenceAuthor_Executed(string? parm)
    {
        if (ReferenceAuthorSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.DeleteReference(ReferenceAuthorSelected);
        if (!await ret)
        {
            return;
        }

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromGenusIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.GenusId);
        ReferenceAuthorDataSetCount = Tbl90ReferenceAuthorsList.Count;
        RefreshReferenceAuthorItems();
    }

    private async void SaveReferenceAuthor_Executed(string? parm)
    {
        if (ReferenceAuthorSelected != null && string.IsNullOrEmpty(ReferenceAuthorSelected.Info))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }

        if (Tbl90ReferenceAuthorsList != null)
        {
            ReferenceAuthorSelected ??= Tbl90ReferenceAuthorsList[0];
            var indx = Tbl90ReferenceAuthorsList.IndexOf(Tbl90ReferenceAuthorsList.First(t =>
                t.Info == ReferenceAuthorSelected.Info));

            if (GenusSelected != null)
            {
                ReferenceAuthorSelected.GenusId = GenusSelected.GenusId;
            }

            var ret = _dataService.SaveReference(ReferenceAuthorSelected, "Author");
            if (!await ret)
            {
                return;
            }

            if (ReferenceAuthorSelected.ReferenceId == 0) //new
            {
                Tbl90ReferenceAuthorsList = await _dataService.GetLastDatasetInTbl90References();
                RefreshReferenceAuthorItems();
            }
            else
            {
                Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromGenusIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.GenusId);
                //   Index Position ?
                if (indx < Tbl90ReferenceAuthorsList.Count)
                {
                    ReferenceAuthorItems.Clear();
                    foreach (var item in Tbl90ReferenceAuthorsList)
                    {
                        ReferenceAuthorItems.Add(item);
                    }
                    ReferenceAuthorSelected = Tbl90ReferenceAuthorsList[indx];  //Index
                }
            }
        }
        ReferenceAuthorCancelEditsAsync();
    }

    private void RefreshReferenceAuthorServer_Executed(string? parm)
    {
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromGenusIdAndRefSourceIdIsNullAndRefExpertIdIsNull(GenusSelected.GenusId);

        ReferenceAuthorDataSetCount = Tbl90ReferenceAuthorsList.Count;
        RefreshReferenceAuthorItems();
    }

    public void ReferenceAuthorStartEdit() => IsInEdit = true;
    public void ReferenceAuthorStartModify() => IsModified = true;
    public void ReferenceAuthorStartNew() => IsNewReferenceAuthor = true;
    public event EventHandler AddNewReferenceAuthorCanceled = null!;
    public void ReferenceAuthorCancelEditsAsync()
    {
        if (IsNewReferenceAuthor)
        {
            IsInEdit = false;
            AddNewReferenceAuthorCanceled?.Invoke(this, EventArgs.Empty);
            IsNewReferenceAuthor = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }
    #endregion [Methods Genus ==> Tbl90Reference Author]                   

    #region [Commands Genus ==> Tbl90Reference Source]  
    public ICommand AddReferenceSourceCommand => new RelayCommand<string>(AddReferenceSource_Executed);
    public ICommand CopyReferenceSourceCommand => new RelayCommand<string>(CopyReferenceSource_Executed);
    public ICommand DeleteReferenceSourceCommand => new RelayCommand<string>(DeleteReferenceSource_Executed);
    public ICommand SaveReferenceSourceCommand => new RelayCommand<string>(SaveReferenceSource_Executed);
    public ICommand RefreshReferenceSourceServerCommand => new RelayCommand<string>(RefreshReferenceSourceServer_Executed);
    #endregion [Commands Genus ==> Tbl90Reference Source]         

    #region [Methods Genus ==> Tbl90Reference Source]      

    private void AddReferenceSource_Executed(string? parm)
    {
        ReferenceSourceStartEdit();
        ReferenceSourceStartNew();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

        Tbl90ReferenceSourcesList.Insert(index: 0, item: new Tbl90Reference { Info = "New", GenusId = GenusSelected!.GenusId });
        ReferenceSourceDataSetCount = Tbl90ReferenceSourcesList.Count;
        ReferenceSourceItems.Clear();
        foreach (var item in Tbl90ReferenceSourcesList)
        {
            ReferenceSourceItems.Add(item);
        }
        ReferenceSourceSelected = ReferenceSourceItems.First();
    }

    private async void CopyReferenceSource_Executed(string? parm)
    {
        ReferenceSourceStartEdit();
        ReferenceSourceStartNew();
        if (GenusSelected != null)
        {
            ReferenceSourceSelected.GenusId = GenusSelected.GenusId; //combo vorbelegen
        }
        Tbl90ReferenceSourcesList = await _dataService.CopyReferenceGenus(ReferenceSourceSelected, "Source");
        ReferenceSourceDataSetCount = Tbl90ReferenceSourcesList.Count;
        RefreshReferenceSourceItems();
    }

    private async void DeleteReferenceSource_Executed(string? parm)
    {
        if (ReferenceSourceSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }

        var ret = _dataService.DeleteReference(ReferenceSourceSelected);
        if (!await ret)
        {
            return;
        }

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromGenusIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.GenusId);
        ReferenceSourceDataSetCount = Tbl90ReferenceSourcesList.Count;
        RefreshReferenceSourceItems();
    }

    private async void SaveReferenceSource_Executed(string? parm)
    {
        if (ReferenceSourceSelected != null && string.IsNullOrEmpty(ReferenceSourceSelected.Info))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }

        if (Tbl90ReferenceSourcesList != null)
        {
            ReferenceSourceSelected ??= Tbl90ReferenceSourcesList[0];
            var indx = Tbl90ReferenceSourcesList.IndexOf(Tbl90ReferenceSourcesList.First(t =>
                t.Info == ReferenceSourceSelected.Info));

            if (GenusSelected != null)
            {
                ReferenceSourceSelected.GenusId = GenusSelected.GenusId;
            }
            var ret = _dataService.SaveReference(ReferenceSourceSelected, "Source");
            if (!await ret)
            {
                return;
            }

            if (ReferenceSourceSelected.ReferenceId == 0) //new
            {
                Tbl90ReferenceSourcesList = await _dataService.GetLastDatasetInTbl90References();
                RefreshReferenceSourceItems();
            }
            else
            {
                Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromGenusIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.GenusId);
                //   Index Position ?
                if (indx < Tbl90ReferenceSourcesList.Count)
                {
                    ReferenceSourceItems.Clear();
                    foreach (var item in Tbl90ReferenceSourcesList)
                    {
                        ReferenceSourceItems.Add(item);
                    }
                    ReferenceSourceSelected = Tbl90ReferenceSourcesList[indx];  //Index
                }
            }
        }
        ReferenceSourceCancelEditsAsync();
    }

    private void RefreshReferenceSourceServer_Executed(string? parm)
    {
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromGenusIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(GenusSelected.GenusId);

        ReferenceSourceDataSetCount = Tbl90ReferenceSourcesList.Count;
        RefreshReferenceSourceItems();
    }

    public void ReferenceSourceStartEdit() => IsInEdit = true;
    public void ReferenceSourceStartModify() => IsModified = true;
    public void ReferenceSourceStartNew() => IsNewReferenceSource = true;
    public event EventHandler AddNewReferenceSourceCanceled = null!;
    public void ReferenceSourceCancelEditsAsync()
    {
        if (IsNewReferenceSource)
        {
            IsInEdit = false;
            AddNewReferenceSourceCanceled?.Invoke(this, EventArgs.Empty);
            IsNewReferenceSource = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }
    #endregion [Methods Genus ==> Tbl90Reference Source]           

    #region [Commands Genus ==> Tbl90Reference Expert]       
    public ICommand AddReferenceExpertCommand => new RelayCommand<string>(AddReferenceExpert_Executed);
    public ICommand CopyReferenceExpertCommand => new RelayCommand<string>(CopyReferenceExpert_Executed);
    public ICommand DeleteReferenceExpertCommand => new RelayCommand<string>(DeleteReferenceExpert_Executed);
    public ICommand SaveReferenceExpertCommand => new RelayCommand<string>(SaveReferenceExpert_Executed);
    public ICommand RefreshReferenceExpertServerCommand => new RelayCommand<string>(RefreshReferenceExpertServer_Executed);
    #endregion [Commands Genus ==> Tbl90Reference Expert]                    

    #region [Methods Genus ==> Tbl90Reference Expert]                 

    private void AddReferenceExpert_Executed(string? parm)
    {
        ReferenceExpertStartEdit();
        ReferenceExpertStartNew();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

        Tbl90ReferenceExpertsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", GenusId = GenusSelected!.GenusId });
        ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
        ReferenceExpertItems.Clear();
        foreach (var item in Tbl90ReferenceExpertsList)
        {
            ReferenceExpertItems.Add(item);
        }
        ReferenceExpertSelected = ReferenceExpertItems.First();
    }

    private async void CopyReferenceExpert_Executed(string? parm)
    {
        ReferenceExpertStartEdit();
        ReferenceExpertStartNew();
        if (GenusSelected != null)
        {
            ReferenceExpertSelected.GenusId = GenusSelected.GenusId; //combo vorbelegen
        }
        Tbl90ReferenceExpertsList = await _dataService.CopyReferenceGenus(ReferenceExpertSelected, "Expert");
        ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
        RefreshReferenceExpertItems();
    }

    private async void DeleteReferenceExpert_Executed(string? parm)
    {
        if (ReferenceExpertSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.DeleteReference(ReferenceExpertSelected);
        if (!await ret)
        {
            return;
        }

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromGenusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.GenusId);
        ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
        RefreshReferenceExpertItems();
    }

    private async void SaveReferenceExpert_Executed(string? parm)
    {
        if (ReferenceExpertSelected != null && string.IsNullOrEmpty(ReferenceExpertSelected.Info))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }

        if (Tbl90ReferenceExpertsList != null)
        {
            ReferenceExpertSelected ??= Tbl90ReferenceExpertsList[0];

            var indx = Tbl90ReferenceExpertsList.IndexOf(Tbl90ReferenceExpertsList.First(t =>
                t.Info == ReferenceExpertSelected.Info));
            if (GenusSelected != null)
            {
                ReferenceExpertSelected.GenusId = GenusSelected.GenusId;
            }
            var ret = _dataService.SaveReference(ReferenceExpertSelected, "Expert");
            if (!await ret)
            {
                return;
            }

            if (ReferenceExpertSelected.ReferenceId == 0) //new
            {
                Tbl90ReferenceExpertsList = await _dataService.GetLastDatasetInTbl90References();
                RefreshReferenceExpertItems();
            }
            else
            {
                Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromGenusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.GenusId);
                //   Index Position ?
                if (indx < Tbl90ReferenceExpertsList.Count)
                {
                    ReferenceExpertItems.Clear();
                    foreach (var item in Tbl90ReferenceExpertsList)
                    {
                        ReferenceExpertItems.Add(item);
                    }
                    ReferenceExpertSelected = Tbl90ReferenceExpertsList[indx];  //Index
                }
            }
        }
        ReferenceExpertCancelEditsAsync();
    }

    private void RefreshReferenceExpertServer_Executed(string? parm)
    {
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromGenusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(GenusSelected.GenusId);

        ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
        RefreshReferenceExpertItems();
    }

    public void ReferenceExpertStartEdit() => IsInEdit = true;
    public void ReferenceExpertStartModify() => IsModified = true;
    public void ReferenceExpertStartNew() => IsNewReferenceExpert = true;
    public event EventHandler AddNewReferenceExpertCanceled = null!;
    public void ReferenceExpertCancelEditsAsync()
    {
        if (IsNewReferenceExpert)
        {
            IsInEdit = false;
            AddNewReferenceExpertCanceled?.Invoke(this, EventArgs.Empty);
            IsNewReferenceExpert = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }
    #endregion [Methods Genus ==> Tbl90Reference Expert]                                 

    #region [Commands Genus ==> Tbl93Comments]         
    public ICommand AddCommentCommand => new RelayCommand<string>(AddComment_Executed);
    public ICommand CopyCommentCommand => new RelayCommand<string>(CopyComment_Executed);
    public ICommand DeleteCommentCommand => new RelayCommand<string>(DeleteComment_Executed);
    public ICommand SaveCommentCommand => new RelayCommand<string>(SaveComment_Executed);
    public ICommand RefreshCommentServerCommand => new RelayCommand<string>(RefreshCommentServer_Executed);
    #endregion [Commands Genus ==> Tbl93Comments]           

    #region [Methods Genus ==> Tbl93Comments]        

    private void AddComment_Executed(string? parm)
    {
        CommentStartEdit();
        CommentStartNew();
        Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = "New", GenusId = GenusSelected!.GenusId });

        CommentDataSetCount = Tbl93CommentsList.Count;
        CommentItems.Clear();
        foreach (var item in Tbl93CommentsList)
        {
            CommentItems.Add(item);
        }
        CommentSelected = CommentItems.First();
    }

    private async void CopyComment_Executed(string? parm)
    {
        CommentStartEdit();
        CommentStartNew();
        if (GenusSelected != null)
        {
            CommentSelected.GenusId = GenusSelected.GenusId;  //combo vorbelegen
        }
        Tbl93CommentsList = await _dataService.CopyComment(CommentSelected);

        CommentDataSetCount = Tbl93CommentsList.Count;
        RefreshCommentItems();
    }

    private async void DeleteComment_Executed(string? parm)
    {
        if (CommentSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.DeleteComment(CommentSelected);
        if (!await ret)
        {
            return;
        }

        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromGenusId(CommentSelected.GenusId);
        CommentDataSetCount = Tbl93CommentsList.Count;
        RefreshCommentItems();
    }

    private async void SaveComment_Executed(string? parm)
    {
        if (CommentSelected != null && string.IsNullOrEmpty(CommentSelected.Info))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl93CommentsList != null)
        {
            var indx = Tbl93CommentsList.IndexOf(Tbl93CommentsList.First(t =>
             CommentSelected != null && t.Info == CommentSelected.Info));
            if (CommentSelected != null)
            {
                if (GenusSelected != null)
                {
                    CommentSelected.GenusId = GenusSelected.GenusId;

                    var ret = _dataService.SaveComment(CommentSelected);
                    if (!await ret)
                    {
                        return;
                    }

                    if (CommentSelected.CommentId == 0) //new
                    {
                        Tbl93CommentsList = await _dataService.GetLastDatasetInTbl93Comments();
                        RefreshCommentItems();
                    }
                    else
                    {
                        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromGenusId(GenusSelected.GenusId);
                        //   Index Position ?
                        if (indx < Tbl93CommentsList.Count)
                        {
                            CommentItems.Clear();
                            foreach (var item in Tbl93CommentsList)
                            {
                                CommentItems.Add(item);
                            }

                            CommentSelected = Tbl93CommentsList[indx];  //Index
                        }
                    }
                }
            }
        }
        CommentCancelEditsAsync();
    }

    private void RefreshCommentServer_Executed(string? parm)
    {
        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromGenusId(GenusSelected.GenusId);

        CommentDataSetCount = Tbl93CommentsList.Count;
        RefreshCommentItems();
    }

    public void CommentStartEdit() => IsInEdit = true;
    public void CommentStartModify() => IsModified = true;
    public void CommentStartNew() => IsNewComment = true;
    public event EventHandler AddNewCommentCanceled = null!;
    public void CommentCancelEditsAsync()
    {
        if (IsNewComment)
        {
            IsInEdit = false;
            AddNewCommentCanceled?.Invoke(this, EventArgs.Empty);
            IsNewComment = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }
    #endregion [Methods Genus ==> Tbl93Comments]                             


    //    Part 9    



    //    Part 10    


    #region "Public Commands to open Main and Detail TabItems"

    private int _selectedMainDetailTabIndex;
    private int _selectedMainDetailRefTabIndex;


    public int SelectedMainDetailTabIndex
    {
        get => _selectedMainDetailTabIndex;
        set
        {
            if (value == _selectedMainDetailTabIndex)
            {
                return;
            }

            _selectedMainDetailTabIndex = value; OnPropertyChanged(nameof(SelectedMainDetailTabIndex));

            if (_selectedMainDetailTabIndex == 0)
            {
                if (GenusSelected != null)
                {
                    IsLoading = true;
                    InfratribusStartModify();
                    Tbl60SubtribussesAllList = _dataService.GetTbl60SubtribussesCollectionOrderBySubtribusName();

                    Tbl63InfratribussesList = _dataService.GetTbl63InfratribussesCollectionOrderByInfratribusNameFromInfratribusId(GenusSelected.InfratribusId);
                    InfratribusDataSetCount = Tbl63InfratribussesList.Count;
                    RefreshInfratribusItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 1)
            {
            }

            if (_selectedMainDetailTabIndex == 2)
            {
                if (GenusSelected != null)
                {
                    IsLoading = true;
                    FiSpeciesStartModify();
                    Tbl68SpeciesgroupsAllList = _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroup();
                    Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

                    Tbl69FiSpeciessesList =
                        _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromGenusId(GenusSelected.GenusId);
                    FiSpeciesDataSetCount = Tbl69FiSpeciessesList.Count;
                    RefreshFiSpeciesItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 3)
            {
                if (GenusSelected != null)
                {
                    IsLoading = true;
                    PlSpeciesStartModify();
                    Tbl68SpeciesgroupsAllList = _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroup();
                    Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

                    Tbl72PlSpeciessesList =
                        _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromGenusId(GenusSelected.GenusId);
                    PlSpeciesDataSetCount = Tbl72PlSpeciessesList.Count;
                    RefreshPlSpeciesItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 4)
            {
                if (GenusSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList =
                        _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromGenusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(GenusSelected.GenusId);
                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }
            if (_selectedMainDetailTabIndex == 5)
            {
                if (GenusSelected != null)
                {
                    IsLoading = true;
                    CommentStartModify();
                    Tbl93CommentsList =
                        _dataService.GetTbl93CommentsCollectionOrderByInfoFromGenusId(GenusSelected.GenusId);
                    CommentDataSetCount = Tbl93CommentsList.Count;
                    RefreshCommentItems();
                    IsLoading = false;
                }
            }

        }
    }

    public int SelectedMainDetailRefTabIndex
    {
        get => _selectedMainDetailRefTabIndex;
        set
        {
            if (value == _selectedMainDetailRefTabIndex)
            {
                return;
            }

            _selectedMainDetailRefTabIndex = value; OnPropertyChanged(nameof(SelectedMainDetailRefTabIndex));

            if (_selectedMainDetailRefTabIndex == 0)
            {
                if (GenusSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromGenusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(GenusSelected.GenusId);
                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 1)
            {
                if (GenusSelected != null)
                {
                    IsLoading = true;
                    ReferenceSourceStartModify();
                    Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

                    Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromGenusIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(GenusSelected.GenusId);

                    ReferenceSourceDataSetCount = Tbl90ReferenceSourcesList.Count;
                    RefreshReferenceSourceItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 2)
            {
                if (GenusSelected != null)
                {
                    IsLoading = true;
                    ReferenceAuthorStartModify();
                    Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

                    Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromGenusIdAndRefSourceIdIsNullAndRefExpertIdIsNull(GenusSelected.GenusId);

                    ReferenceAuthorDataSetCount = Tbl90ReferenceAuthorsList.Count;
                    RefreshReferenceAuthorItems();
                    IsLoading = false;
                }
            }
        }
    }

    #endregion "Public Commands to open Main und Ref TabItems"    



    //    Part 11 


    #region All Properties

    #region Selected Properties

    private Tbl63Infratribus _infratribusSelected = null!;
    public Tbl63Infratribus InfratribusSelected
    {
        get => _infratribusSelected;
        set => SetProperty(ref _infratribusSelected, value);
    }

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

    private int _fiSpeciesDataSetCount;
    public int FiSpeciesDataSetCount
    {
        get => _fiSpeciesDataSetCount;
        set
        {
            _fiSpeciesDataSetCount = value; OnPropertyChanged(nameof(FiSpeciesDataSetCount));
        }
    }

    private int _plSpeciesDataSetCount;
    public int PlSpeciesDataSetCount
    {
        get => _plSpeciesDataSetCount;
        set
        {
            _plSpeciesDataSetCount = value; OnPropertyChanged(nameof(PlSpeciesDataSetCount));
        }
    }


    private int _genusDataSetCount;
    public int GenusDataSetCount
    {
        get => _genusDataSetCount;
        set
        {
            _genusDataSetCount = value; OnPropertyChanged(nameof(GenusDataSetCount));
        }
    }

    private int _infratribusDataSetCount;
    public int InfratribusDataSetCount
    {
        get => _infratribusDataSetCount;
        set
        {
            _infratribusDataSetCount = value; OnPropertyChanged(nameof(InfratribusDataSetCount));
        }
    }


    private int _referenceExpertDataSetCount;
    public int ReferenceExpertDataSetCount
    {
        get => _referenceExpertDataSetCount;
        set
        {
            _referenceExpertDataSetCount = value; OnPropertyChanged(nameof(ReferenceExpertDataSetCount));
        }
    }
    private int _referenceSourceDataSetCount;
    public int ReferenceSourceDataSetCount
    {
        get => _referenceSourceDataSetCount;
        set
        {
            _referenceSourceDataSetCount = value; OnPropertyChanged(nameof(ReferenceSourceDataSetCount));
        }
    }
    private int _referenceAuthorDataSetCount;
    public int ReferenceAuthorDataSetCount
    {
        get => _referenceAuthorDataSetCount;
        set
        {
            _referenceAuthorDataSetCount = value; OnPropertyChanged(nameof(ReferenceAuthorDataSetCount));
        }
    }

    private int _commentDataSetCount;
    public int CommentDataSetCount
    {
        get => _commentDataSetCount;
        set
        {
            _commentDataSetCount = value; OnPropertyChanged(nameof(CommentDataSetCount));
        }
    }
    //------------------------------------

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
    private bool _isNewGenus;
    public bool IsNewGenus
    {
        get => _isNewGenus;
        set => SetProperty(ref _isNewGenus, value);
    }
    private bool _isNewFiSpecies;
    public bool IsNewFiSpecies
    {
        get => _isNewFiSpecies;
        set => SetProperty(ref _isNewFiSpecies, value);
    }

    private bool _isNewPlSpecies;
    public bool IsNewPlSpecies
    {
        get => _isNewPlSpecies;
        set => SetProperty(ref _isNewPlSpecies, value);
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
    //--------------------------------------------------- 


    private string _searchGenusName = "";
    public string SearchGenusName
    {
        get => _searchGenusName;
        set
        {
            _searchGenusName = value; OnPropertyChanged(nameof(SearchGenusName));
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

    private ObservableCollection<Tbl78Name> _tbl78NamesList = null!;

    public ObservableCollection<Tbl78Name> Tbl78NamesList
    {
        get => _tbl78NamesList;
        set
        {
            _tbl78NamesList = value; OnPropertyChanged(nameof(Tbl78NamesList));
        }
    }

    private ObservableCollection<Tbl81Image> _tbl81ImagesList = null!;

    public ObservableCollection<Tbl81Image> Tbl81ImagesList
    {
        get => _tbl81ImagesList;
        set
        {
            _tbl81ImagesList = value; OnPropertyChanged(nameof(Tbl81ImagesList));
        }
    }

    private string _searchSynonymName = string.Empty;

    public string SearchSynonymName
    {
        get => _searchSynonymName;
        set
        {
            _searchSynonymName = value; OnPropertyChanged(nameof(SearchSynonymName));
        }
    }


    private ObservableCollection<Tbl84Synonym> _tbl84SynonymsList = null!;

    public ObservableCollection<Tbl84Synonym> Tbl84SynonymsList
    {
        get => _tbl84SynonymsList;
        set
        {
            _tbl84SynonymsList = value; OnPropertyChanged(nameof(Tbl84SynonymsList));
        }
    }

    private ObservableCollection<Tbl84Synonym> _tbl84SynonymsAllList = null!;

    public ObservableCollection<Tbl84Synonym> Tbl84SynonymsAllList
    {
        get => _tbl84SynonymsAllList;
        set
        {
            _tbl84SynonymsAllList = value; OnPropertyChanged(nameof(Tbl84SynonymsAllList));
        }
    }


    private string _searchGeographicName = string.Empty;

    public string SearchGeographicName
    {
        get => _searchGeographicName;
        set
        {
            _searchGeographicName = value; OnPropertyChanged(nameof(SearchGeographicName));
        }
    }

    private ObservableCollection<Tbl87Geographic> _tbl87GeographicsList = null!;

    public ObservableCollection<Tbl87Geographic> Tbl87GeographicsList
    {
        get => _tbl87GeographicsList;
        set
        {
            _tbl87GeographicsList = value; OnPropertyChanged(nameof(Tbl87GeographicsList));
        }
    }

    private ObservableCollection<Tbl87Geographic> _tbl87GeographicsAllList = null!;

    public ObservableCollection<Tbl87Geographic> Tbl87GeographicsAllList
    {
        get => _tbl87GeographicsAllList;
        set
        {
            _tbl87GeographicsAllList = value; OnPropertyChanged(nameof(Tbl87GeographicsAllList));
        }
    }

    private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesList = null!;
    public ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesList
    {
        get => _tbl63InfratribussesList;
        set
        {
            _tbl63InfratribussesList = value; OnPropertyChanged(nameof(Tbl63InfratribussesList));
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

    private ObservableCollection<Tbl66Genus> _tbl66GenussesList = null!;
    public ObservableCollection<Tbl66Genus> Tbl66GenussesList
    {
        get => _tbl66GenussesList;
        set
        {
            _tbl66GenussesList = value; OnPropertyChanged(nameof(Tbl66GenussesList));
        }
    }


    private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesList = null!;
    public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
    {
        get => _tbl69FiSpeciessesList;
        set
        {
            _tbl69FiSpeciessesList = value; OnPropertyChanged(nameof(Tbl69FiSpeciessesList));
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

    private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList = null!;
    public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
    {
        get => _tbl68SpeciesgroupsAllList;
        set
        {
            _tbl68SpeciesgroupsAllList = value; OnPropertyChanged(nameof(Tbl68SpeciesgroupsAllList));
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

    private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList = null!;
    public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
    {
        get => _tbl90AuthorsAllList;
        set
        {
            _tbl90AuthorsAllList = value; OnPropertyChanged(nameof(Tbl90AuthorsAllList));
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

    private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList = null!;
    public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
    {
        get => _tbl90ExpertsAllList;
        set
        {
            _tbl90ExpertsAllList = value; OnPropertyChanged(nameof(Tbl90ExpertsAllList));
        }
    }

    private ObservableCollection<Tbl90Reference> _tbl90ReferenceAuthorsList = null!;
    public ObservableCollection<Tbl90Reference> Tbl90ReferenceAuthorsList
    {
        get => _tbl90ReferenceAuthorsList;
        set
        {
            _tbl90ReferenceAuthorsList = value; OnPropertyChanged(nameof(Tbl90ReferenceAuthorsList));
        }
    }

    private ObservableCollection<Tbl90Reference> _tbl90ReferenceSourcesList = null!;
    public ObservableCollection<Tbl90Reference> Tbl90ReferenceSourcesList
    {
        get => _tbl90ReferenceSourcesList;
        set
        {
            _tbl90ReferenceSourcesList = value; OnPropertyChanged(nameof(Tbl90ReferenceSourcesList));
        }
    }

    private ObservableCollection<Tbl90Reference> _tbl90ReferenceExpertsList = null!;
    public ObservableCollection<Tbl90Reference> Tbl90ReferenceExpertsList
    {
        get => _tbl90ReferenceExpertsList;
        set
        {
            _tbl90ReferenceExpertsList = value; OnPropertyChanged(nameof(Tbl90ReferenceExpertsList));
        }
    }

    private ObservableCollection<Tbl93Comment> _tbl93CommentsList = null!;
    public ObservableCollection<Tbl93Comment> Tbl93CommentsList
    {
        get => _tbl93CommentsList;
        set
        {
            _tbl93CommentsList = value; OnPropertyChanged(nameof(Tbl93CommentsList));
        }
    }

    #endregion "Public Properties"     

    #region Refresh Methods

    private void RefreshInfratribusItems()
    {
        InfratribusItems.Clear();
        if (Tbl63InfratribussesList != null)
        {
            foreach (var item in Tbl63InfratribussesList)
            {
                InfratribusItems.Add(item);
            }

            if (Tbl63InfratribussesList.Count == 0)
            {
                return;
            }

            if (InfratribusSelected == null && Tbl63InfratribussesList.Count != 0)
            {
                InfratribusSelected = InfratribusItems.FirstOrDefault()!;
            }
        }
    }
    private void RefreshGenusItems()
    {
        GenusItems.Clear();
        if (Tbl66GenussesList != null)
        {
            foreach (var item in Tbl66GenussesList)
            {
                GenusItems.Add(item);
            }

            if (Tbl66GenussesList.Count == 0)
            {
                return;
            }

            if (GenusSelected == null && Tbl66GenussesList.Count != 0)
            {
                GenusSelected = GenusItems.First()!;
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
                FiSpeciesSelected = FiSpeciesItems.First();
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
                PlSpeciesSelected = PlSpeciesItems.First();
            }
        }
    }
    private void RefreshReferenceExpertItems()
    {
        ReferenceExpertItems.Clear();
        if (Tbl90ReferenceExpertsList != null)
        {
            foreach (var item in Tbl90ReferenceExpertsList)
            {
                ReferenceExpertItems.Add(item);
            }
            if (Tbl90ReferenceExpertsList.Count == 0)
            {
                return;
            }

            if (ReferenceExpertSelected == null && Tbl90ReferenceExpertsList.Count != 0)
            {
                ReferenceExpertSelected = ReferenceExpertItems.FirstOrDefault()!;
            }
        }
    }
    private void RefreshReferenceSourceItems()
    {
        ReferenceSourceItems.Clear();
        if (Tbl90ReferenceSourcesList != null)
        {
            foreach (var item in Tbl90ReferenceSourcesList)
            {
                ReferenceSourceItems.Add(item);
            }
            if (Tbl90ReferenceSourcesList.Count == 0)
            {
                return;
            }

            if (ReferenceSourceSelected == null && Tbl90ReferenceSourcesList.Count != 0)
            {
                ReferenceSourceSelected = ReferenceSourceItems.FirstOrDefault()!;
            }
        }
    }
    private void RefreshReferenceAuthorItems()
    {
        ReferenceAuthorItems.Clear();
        if (Tbl90ReferenceAuthorsList != null)
        {
            foreach (var item in Tbl90ReferenceAuthorsList)
            {
                ReferenceAuthorItems.Add(item);
            }
            if (Tbl90ReferenceAuthorsList.Count == 0)
            {
                return;
            }

            if (ReferenceAuthorSelected == null && Tbl90ReferenceAuthorsList.Count != 0)
            {
                ReferenceAuthorSelected = ReferenceAuthorItems.FirstOrDefault()!;
            }
        }
    }
    private void RefreshCommentItems()
    {
        CommentItems.Clear();
        if (Tbl93CommentsList != null)
        {
            foreach (var item in Tbl93CommentsList)
            {
                CommentItems.Add(item);
            }
            if (Tbl93CommentsList.Count == 0)
            {
                return;
            }

            if (CommentSelected == null && Tbl93CommentsList.Count != 0)
            {
                CommentSelected = CommentItems.FirstOrDefault()!;
            }
        }
    }

    #endregion
    #endregion

}
