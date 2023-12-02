
using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using ATIS.WinUi.Services;
using CommunityToolkit.Mvvm.ComponentModel;

//    Tbl63InfratribussesViewModel Skriptdatum:  01.04.2023  10:32    

namespace ATIS.WinUi.ViewModels.Database;

public class Tbl63InfratribussesViewModel : ObservableObject
{

    #region [Private Data Members]
    private readonly IDataService _dataService;
    public ObservableCollection<Tbl63Infratribus?> InfratribusItems { get; } = new();
    public ObservableCollection<Tbl66Genus> GenusItems { get; } = new();

    public ObservableCollection<Tbl60Subtribus> SubtribusItems { get; } = new();

    public ObservableCollection<Tbl90Reference> ReferenceExpertItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceSourceItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceAuthorItems { get; } = new();
    public ObservableCollection<Tbl93Comment> CommentItems { get; } = new();
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]      

    #region [Constructor]
    public Tbl63InfratribussesViewModel(IDataService dataService)
    {
        _dataService = dataService;
        SelectedMainDetailTabIndex = 1; //Tab Datagrid
        GetAllCollections();
    }

    private void GetAllCollections()
    {
        Tbl63InfratribussesAllList ??= new ObservableCollection<Tbl63Infratribus>();
        Tbl63InfratribussesAllList = _dataService.GetTbl63InfratribussesCollectionOrderByInfratribusName();
        Tbl60SubtribussesAllList ??= new ObservableCollection<Tbl60Subtribus>();
        Tbl60SubtribussesAllList = _dataService.GetTbl60SubtribussesCollectionOrderBySubtribusName();
        Tbl57TribussesAllList ??= new ObservableCollection<Tbl57Tribus>();
        Tbl57TribussesAllList = _dataService.GetTbl57TribussesCollectionOrderByTribusName();

        Tbl90ExpertsAllList ??= new ObservableCollection<Tbl90RefExpert>();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();
        Tbl90SourcesAllList ??= new ObservableCollection<Tbl90RefSource>();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();
        Tbl90AuthorsAllList ??= new ObservableCollection<Tbl90RefAuthor>();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();
    }

    #endregion [Constructor]  


    //    Part 1    



    #region [Commands Infratribus]

    public ICommand GetInfratribussesByNameOrIdCommand => new RelayCommand(execute: delegate
    {
        var task = GetInfratribussesByNameOrId_Executed(SearchInfratribusName);
    });

    public ICommand AddInfratribusCommand => new RelayCommand<string>(AddInfratribus_Executed);
    public ICommand CopyInfratribusCommand => new RelayCommand<string>(CopyInfratribus_Executed);

    public ICommand DeleteInfratribusCommand => new RelayCommand(execute: delegate { DeleteInfratribus_Executed(SearchInfratribusName); });

    public ICommand SaveInfratribusCommand => new RelayCommand(execute: delegate { var task = SaveInfratribus_Executed(SearchInfratribusName); });
    public ICommand RefreshInfratribusServerCommand => new RelayCommand(execute: delegate { RefreshInfratribusServer_Executed(SearchInfratribusName); });

    #endregion [Commands Infratribus]       

    #region [Methods Tbl63Infratribus]

    private async Task GetInfratribussesByNameOrId_Executed(string? parm)
    {
        IsLoading = true;
        InfratribusStartModify();
        Tbl60SubtribussesList?.Clear();
        Tbl63InfratribussesList?.Clear();
        Tbl66GenussesList?.Clear();
        Tbl90ReferenceExpertsList?.Clear();
        Tbl90ReferenceSourcesList?.Clear();
        Tbl90ReferenceAuthorsList?.Clear();
        Tbl93CommentsList?.Clear();

        SubtribusItems.Clear();
        InfratribusItems.Clear();
        GenusItems.Clear();
        ReferenceAuthorItems.Clear();
        ReferenceSourceItems.Clear();
        ReferenceExpertItems.Clear();
        CommentItems.Clear();

        Tbl63InfratribussesList ??= new ObservableCollection<Tbl63Infratribus>();
        Tbl63InfratribussesList = await _dataService.GetTbl63InfratribussesCollectionOrderByInfratribusNameFromSearchNameOrId(parm!);

        if (Tbl63InfratribussesList is { Count: 0 })
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        InfratribusDataSetCount = Tbl63InfratribussesList.Count;
        RefreshInfratribusItems();

        SelectedMainDetailTabIndex = 1;
        IsLoading = false;
    }

    private async void AddInfratribus_Executed(string? parm)
    {
        InfratribusStartEdit();
        InfratribusStartNew();

        //Id search for first Dataset of Tbl60SubtribussesList
        var single = await _dataService.GetSubtribusSingleFirstDataset();
        var id = single.SubtribusId;

        Tbl63InfratribussesList ??= new ObservableCollection<Tbl63Infratribus>();
        Tbl63InfratribussesList.Insert(index: 0, item: new Tbl63Infratribus { InfratribusName = "New", SubtribusId = id });

        RefreshInfratribusItems();
    }

    private async void CopyInfratribus_Executed(string? parm)
    {
        InfratribusStartEdit();
        InfratribusStartNew();

        Tbl63InfratribussesList = await _dataService.CopyInfratribus(InfratribusSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshInfratribusItems();
    }

    private async void DeleteInfratribus_Executed(string? parm)
    {

        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(InfratribusSelected!.InfratribusName!))
        {
            //necessary to delete before
            await _dataService.DeleteConnectedGenusses(InfratribusSelected);
            await _dataService.DeleteConnectedInfratribusReferences(InfratribusSelected);
            await _dataService.DeleteConnectedInfratribusComments(InfratribusSelected);

            var ret = _dataService.DeleteInfratribus(InfratribusSelected);
            if (!await ret)
            {
                return;
            }

            Tbl63InfratribussesList = await _dataService.GetTbl63InfratribussesCollectionOrderByInfratribusNameFromSearchNameOrId(parm!);

            InfratribusDataSetCount = Tbl63InfratribussesList.Count;
            RefreshInfratribusItems();
        }
    }

    private async Task SaveInfratribus_Executed(string? parm)
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
    public void InfratribusStartNew() => IsNewInfratribus = true;
    public event EventHandler AddNewInfratribusCanceled = null!;
    public void InfratribusCancelEditsAsync()
    {
        if (IsNewInfratribus)
        {
            IsInEdit = false;
            AddNewInfratribusCanceled?.Invoke(this, EventArgs.Empty);
            IsNewInfratribus = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }

    #endregion [Methods Tbl63Infratribus]    




    //    Part 2    


    #region "Public Commands Connect <== Tbl60Subtribus"                 

    public ICommand SaveSubtribusCommand => new RelayCommand<string>(SaveSubtribus_Executed);
    public ICommand RefreshSubtribusServerCommand => new RelayCommand(execute: delegate { RefreshSubtribusServer_Executed(SearchInfratribusName); });

    private async void SaveSubtribus_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(SubtribusSelected?.SubtribusName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl60SubtribussesList != null)
        {
            var iNdx = Tbl60SubtribussesList.IndexOf(Tbl60SubtribussesList.First(t =>
               t.SubtribusName == SubtribusSelected.SubtribusName));

            var ret = _dataService.SaveSubtribus(SubtribusSelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl60SubtribussesList = await _dataService.GetLastDatasetInTbl60Subtribusses();
                RefreshSubtribusItems();
            }
            else
            {
                if (SubtribusSelected.SubtribusId == 0) //new
                {
                    Tbl60SubtribussesList = await _dataService.GetLastDatasetInTbl60Subtribusses();
                    RefreshSubtribusItems();
                }
                else
                {
                    Tbl60SubtribussesList = await _dataService.GetTbl60SubtribussesCollectionOrderBySubtribusNameFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl60SubtribussesList!.Count)
                    {
                        SubtribusItems.Clear();
                        foreach (var item in Tbl60SubtribussesList)
                        {
                            SubtribusItems.Add(item);
                        }

                        SubtribusSelected = Tbl60SubtribussesList[iNdx];
                    }
                }
            }
        }

        SubtribusDataSetCount = Tbl60SubtribussesList!.Count;
        SubtribusCancelEditsAsync();
    }
    private async void RefreshSubtribusServer_Executed(string? parm)
    {
        Tbl60SubtribussesList = await _dataService.GetTbl60SubtribussesCollectionOrderBySubtribusNameFromSearchNameOrId(parm!);

        SubtribusDataSetCount = Tbl60SubtribussesList.Count;
        RefreshSubtribusItems();
    }

    public void SubtribusStartEdit() => IsInEdit = true;
    public void SubtribusStartModify() => IsModified = true;
    public void SubtribusCancelEditsAsync()
    {
        IsInEdit = false;
        IsModified = false;
    }

    #endregion "Public Commands"                  



    //    Part 3    





    //    Part 4    


    #region [Public Commands Connect ==> Tbl66Genus]       

    public ICommand AddGenusCommand => new RelayCommand<string>(AddGenus_Executed);
    public ICommand CopyGenusCommand => new RelayCommand<string>(CopyGenus_Executed);
    public ICommand DeleteGenusCommand => new RelayCommand<string>(DeleteGenus_Executed);
    public ICommand SaveGenusCommand => new RelayCommand<string>(SaveGenus_Executed);
    public ICommand RefreshGenusServerCommand => new RelayCommand(execute: delegate { RefreshGenusServer_Executed(GenusSelected!.InfratribusId); });

    #endregion [Public Commands Connect ==> Tbl66Genus]    

    #region [Public Methods Connect ==> Tbl66Genus]                   

    private void AddGenus_Executed(string? parm)
    {
        GenusStartEdit();
        GenusStartNew();
        Tbl66GenussesList.Insert(0, new Tbl66Genus { GenusName = "New", InfratribusId = InfratribusSelected.InfratribusId });

        GenusItems.Clear();
        foreach (var item in Tbl66GenussesList)
        {
            GenusItems.Add(item);
        }
        GenusSelected = GenusItems.First();
    }

    private async void CopyGenus_Executed(string? parm)
    {
        GenusStartEdit();
        GenusStartNew();
        GenusSelected.InfratribusId = InfratribusSelected.InfratribusId;  //combo vorbelegen

        Tbl66GenussesList = await _dataService.CopyGenus(GenusSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshGenusItems();
    }

    private async void DeleteGenus_Executed(string? parm)
    {
        if (GenusSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.DeleteGenus(GenusSelected);
        if (!await ret)
        {
            return;
        }

        Tbl66GenussesList = _dataService.GetTbl66GenussesCollectionOrderByGenusNameFromInfratribusId(GenusSelected.InfratribusId);

        GenusDataSetCount = Tbl66GenussesList.Count;
        RefreshGenusItems();
    }

    private async void SaveGenus_Executed(string? parm)
    {
        if (GenusSelected != null && string.IsNullOrEmpty(GenusSelected.GenusName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl66GenussesList != null)
        {
            var indx = Tbl66GenussesList.IndexOf(Tbl66GenussesList.First(t =>
              GenusSelected != null && t.GenusName == GenusSelected.GenusName));

            if (GenusSelected != null)
            {
                if (InfratribusSelected != null)
                {
                    GenusSelected.InfratribusId = InfratribusSelected.InfratribusId;

                    var ret = _dataService.SaveGenus(GenusSelected);
                    if (!await ret)
                    {
                        return;
                    }

                    if (GenusSelected.GenusId == 0) //new
                    {
                        Tbl66GenussesList = await _dataService.GetLastDatasetInTbl66Genusses();
                        RefreshGenusItems();
                    }
                    else
                    {
                        Tbl66GenussesList = _dataService.GetTbl66GenussesCollectionOrderByGenusNameFromInfratribusId(InfratribusSelected.InfratribusId);
                        //   Index Position ?
                        if (indx < Tbl66GenussesList.Count)
                        {
                            GenusItems.Clear();
                            foreach (var item in Tbl66GenussesList)
                            {
                                GenusItems.Add(item);
                            }

                            GenusSelected = Tbl66GenussesList[indx];  //Index
                        }
                    }
                }
            }
        }
        GenusDataSetCount = Tbl66GenussesList!.Count;
        GenusCancelEditsAsync();
    }

    private void RefreshGenusServer_Executed(int id)
    {
        Tbl66GenussesList ??= new ObservableCollection<Tbl66Genus>();
        Tbl66GenussesList = _dataService.GetTbl66GenussesCollectionOrderByGenusNameFromInfratribusId(id);

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
    #endregion [Public Methods  Connect ==> Tbl66Genus]                                                                                                                                            



    //    Part 5    




    //    Part 6    




    //    Part 7    



    //    Part 8    


    #region [Commands Infratribus ==> Tbl90Reference Author]
    public ICommand AddReferenceAuthorCommand => new RelayCommand<string>(AddReferenceAuthor_Executed);
    public ICommand CopyReferenceAuthorCommand => new RelayCommand<string>(CopyReferenceAuthor_Executed);
    public ICommand DeleteReferenceAuthorCommand => new RelayCommand<string>(DeleteReferenceAuthor_Executed);
    public ICommand SaveReferenceAuthorCommand => new RelayCommand<string>(SaveReferenceAuthor_Executed);
    public ICommand RefreshReferenceAuthorServerCommand => new RelayCommand<string>(RefreshReferenceAuthorServer_Executed);
    #endregion [Commands Infratribus ==> Tbl90Reference Author]                

    #region [Methods Infratribus ==> Tbl90Reference Author]

    private void AddReferenceAuthor_Executed(string? parm)
    {
        ReferenceAuthorStartEdit();
        ReferenceAuthorStartNew();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

        Tbl90ReferenceAuthorsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", InfratribusId = InfratribusSelected!.InfratribusId });

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
        ReferenceAuthorSelected.InfratribusId = InfratribusSelected.InfratribusId; //combo vorbelegen

        Tbl90ReferenceAuthorsList = await _dataService.CopyReferenceInfratribus(ReferenceAuthorSelected, "Author");

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

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromInfratribusIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.InfratribusId);
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

            if (InfratribusSelected != null)
            {
                ReferenceAuthorSelected.InfratribusId = InfratribusSelected.InfratribusId;
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
                Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromInfratribusIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.InfratribusId);
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

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromInfratribusIdAndRefSourceIdIsNullAndRefExpertIdIsNull(InfratribusSelected.InfratribusId);

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
    #endregion [Methods Infratribus ==> Tbl90Reference Author]                   

    #region [Commands Infratribus ==> Tbl90Reference Source]  
    public ICommand AddReferenceSourceCommand => new RelayCommand<string>(AddReferenceSource_Executed);
    public ICommand CopyReferenceSourceCommand => new RelayCommand<string>(CopyReferenceSource_Executed);
    public ICommand DeleteReferenceSourceCommand => new RelayCommand<string>(DeleteReferenceSource_Executed);
    public ICommand SaveReferenceSourceCommand => new RelayCommand<string>(SaveReferenceSource_Executed);
    public ICommand RefreshReferenceSourceServerCommand => new RelayCommand<string>(RefreshReferenceSourceServer_Executed);
    #endregion [Commands Infratribus ==> Tbl90Reference Source]         

    #region [Methods Infratribus ==> Tbl90Reference Source]      

    private void AddReferenceSource_Executed(string? parm)
    {
        ReferenceSourceStartEdit();
        ReferenceSourceStartNew();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

        Tbl90ReferenceSourcesList.Insert(index: 0, item: new Tbl90Reference { Info = "New", InfratribusId = InfratribusSelected!.InfratribusId });
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
        if (InfratribusSelected != null)
        {
            ReferenceSourceSelected.InfratribusId = InfratribusSelected.InfratribusId; //combo vorbelegen
        }
        Tbl90ReferenceSourcesList = await _dataService.CopyReferenceInfratribus(ReferenceSourceSelected, "Source");
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

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromInfratribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.InfratribusId);
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

            if (InfratribusSelected != null)
            {
                ReferenceSourceSelected.InfratribusId = InfratribusSelected.InfratribusId;
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
                Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromInfratribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.InfratribusId);
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

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromInfratribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(InfratribusSelected.InfratribusId);

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
    #endregion [Methods Infratribus ==> Tbl90Reference Source]           

    #region [Commands Infratribus ==> Tbl90Reference Expert]       
    public ICommand AddReferenceExpertCommand => new RelayCommand<string>(AddReferenceExpert_Executed);
    public ICommand CopyReferenceExpertCommand => new RelayCommand<string>(CopyReferenceExpert_Executed);
    public ICommand DeleteReferenceExpertCommand => new RelayCommand<string>(DeleteReferenceExpert_Executed);
    public ICommand SaveReferenceExpertCommand => new RelayCommand<string>(SaveReferenceExpert_Executed);
    public ICommand RefreshReferenceExpertServerCommand => new RelayCommand<string>(RefreshReferenceExpertServer_Executed);
    #endregion [Commands Infratribus ==> Tbl90Reference Expert]                    

    #region [Methods Infratribus ==> Tbl90Reference Expert]                 

    private void AddReferenceExpert_Executed(string? parm)
    {
        ReferenceExpertStartEdit();
        ReferenceExpertStartNew();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

        Tbl90ReferenceExpertsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", InfratribusId = InfratribusSelected!.InfratribusId });
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
        if (InfratribusSelected != null)
        {
            ReferenceExpertSelected.InfratribusId = InfratribusSelected.InfratribusId; //combo vorbelegen
        }
        Tbl90ReferenceExpertsList = await _dataService.CopyReferenceInfratribus(ReferenceExpertSelected, "Expert");
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

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromInfratribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.InfratribusId);
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
            if (InfratribusSelected != null)
            {
                ReferenceExpertSelected.InfratribusId = InfratribusSelected.InfratribusId;
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
                Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromInfratribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.InfratribusId);
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

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromInfratribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(InfratribusSelected.InfratribusId);

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
    #endregion [Methods Infratribus ==> Tbl90Reference Expert]                                 

    #region [Commands Infratribus ==> Tbl93Comments]         
    public ICommand AddCommentCommand => new RelayCommand<string>(AddComment_Executed);
    public ICommand CopyCommentCommand => new RelayCommand<string>(CopyComment_Executed);
    public ICommand DeleteCommentCommand => new RelayCommand<string>(DeleteComment_Executed);
    public ICommand SaveCommentCommand => new RelayCommand<string>(SaveComment_Executed);
    public ICommand RefreshCommentServerCommand => new RelayCommand<string>(RefreshCommentServer_Executed);
    #endregion [Commands Infratribus ==> Tbl93Comments]           


    #region [Methods Infratribus ==> Tbl93Comments]        

    private void AddComment_Executed(string? parm)
    {
        CommentStartEdit();
        CommentStartNew();
        Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = "New", InfratribusId = InfratribusSelected!.InfratribusId });

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
        if (InfratribusSelected != null)
        {
            CommentSelected.InfratribusId = InfratribusSelected.InfratribusId;  //combo vorbelegen
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

        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromInfratribusId(CommentSelected.InfratribusId);
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
                if (InfratribusSelected != null)
                {
                    CommentSelected.InfratribusId = InfratribusSelected.InfratribusId;

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
                        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromInfratribusId(InfratribusSelected.InfratribusId);
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
        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromInfratribusId(InfratribusSelected.InfratribusId);

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
    #endregion [Methods Infratribus ==> Tbl93Comments]                             


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
                if (InfratribusSelected != null)
                {
                    IsLoading = true;
                    SubtribusStartModify();
                    Tbl57TribussesAllList = _dataService.GetTbl57TribussesCollectionOrderByTribusName();

                    Tbl60SubtribussesList = _dataService.GetTbl60SubtribussesCollectionOrderBySubtribusNameFromSubtribusId(InfratribusSelected.SubtribusId);

                    SubtribusDataSetCount = Tbl60SubtribussesList.Count;
                    RefreshSubtribusItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 1)
            {
            }

            if (_selectedMainDetailTabIndex == 2)
            {
                if (InfratribusSelected != null)
                {
                    IsLoading = true;
                    GenusStartModify();
                    Tbl66GenussesList = _dataService.GetTbl66GenussesCollectionOrderByGenusNameFromInfratribusId(InfratribusSelected.InfratribusId);

                    GenusDataSetCount = Tbl66GenussesList.Count;
                    RefreshGenusItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 3)
            {
                if (InfratribusSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromInfratribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(InfratribusSelected.InfratribusId);

                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 4)
            {
                if (InfratribusSelected != null)
                {
                    IsLoading = true;
                    CommentStartModify();
                    Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromInfratribusId(InfratribusSelected.InfratribusId);

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
                if (InfratribusSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromInfratribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(InfratribusSelected.InfratribusId);
                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 1)
            {
                if (InfratribusSelected != null)
                {
                    IsLoading = true;
                    ReferenceSourceStartModify();
                    Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

                    Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromInfratribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(InfratribusSelected.InfratribusId);

                    ReferenceSourceDataSetCount = Tbl90ReferenceSourcesList.Count;
                    RefreshReferenceSourceItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 2)
            {
                if (InfratribusSelected != null)
                {
                    IsLoading = true;
                    ReferenceAuthorStartModify();
                    Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

                    Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromInfratribusIdAndRefSourceIdIsNullAndRefExpertIdIsNull(InfratribusSelected.InfratribusId);

                    ReferenceAuthorDataSetCount = Tbl90ReferenceAuthorsList.Count;
                    RefreshReferenceAuthorItems();
                    IsLoading = false;
                }
            }
        }
    }
    #endregion "Public Commands to open Main and Detail TabItems"          



    //    Part 11 


    #region All Properties

    #region Selected Properties

    private Tbl60Subtribus _subtribusSelected = null!;
    public Tbl60Subtribus SubtribusSelected
    {
        get => _subtribusSelected;
        set => SetProperty(ref _subtribusSelected, value);
    }

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
    public string SearchInfratribusName { get; set; } = null!;

    private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesList = null!;
    public ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesList
    {
        get => _tbl60SubtribussesList;
        set
        {
            _tbl60SubtribussesList = value; OnPropertyChanged();
        }
    }
    private ObservableCollection<Tbl60Subtribus>? _tbl60SubtribussesAllList;
    public ObservableCollection<Tbl60Subtribus>? Tbl60SubtribussesAllList
    {
        get => _tbl60SubtribussesAllList;
        set
        {
            _tbl60SubtribussesAllList = value; OnPropertyChanged();
        }
    }
    private ObservableCollection<Tbl57Tribus>? _tbl57TribussesAllList;
    public ObservableCollection<Tbl57Tribus>? Tbl57TribussesAllList
    {
        get => _tbl57TribussesAllList;
        set
        {
            _tbl57TribussesAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesList = null!;
    public ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesList
    {
        get => _tbl63InfratribussesList;
        set
        {
            _tbl63InfratribussesList = value; OnPropertyChanged();
        }
    }
    private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesAllList = null!;
    public ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesAllList
    {
        get => _tbl63InfratribussesAllList;
        set
        {
            _tbl63InfratribussesAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl66Genus> _tbl66GenussesList = null!;
    public ObservableCollection<Tbl66Genus> Tbl66GenussesList
    {
        get => _tbl66GenussesList;
        set
        {
            _tbl66GenussesList = value; OnPropertyChanged();
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
    private int _subtribusDataSetCount;
    public int SubtribusDataSetCount
    {
        get => _subtribusDataSetCount;
        set
        {
            _subtribusDataSetCount = value; OnPropertyChanged();
        }
    }

    private int _infratribusDataSetCount;
    public int InfratribusDataSetCount
    {
        get => _infratribusDataSetCount;
        set
        {
            _infratribusDataSetCount = value; OnPropertyChanged();
        }
    }

    private int _genusDataSetCount;
    public int GenusDataSetCount
    {
        get => _genusDataSetCount;
        set
        {
            _genusDataSetCount = value; OnPropertyChanged();
        }
    }

    private int _referenceExpertDataSetCount;
    public int ReferenceExpertDataSetCount
    {
        get => _referenceExpertDataSetCount;
        set
        {
            _referenceExpertDataSetCount = value; OnPropertyChanged();
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

    private bool _isNewInfratribus;
    public bool IsNewInfratribus
    {
        get => _isNewInfratribus;
        set => SetProperty(ref _isNewInfratribus, value);
    }
    private bool _isNewGenus;
    public bool IsNewGenus
    {
        get => _isNewGenus;
        set => SetProperty(ref _isNewGenus, value);
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
    private void RefreshSubtribusItems()
    {
        SubtribusItems.Clear();
        if (Tbl60SubtribussesList != null)
        {
            foreach (var item in Tbl60SubtribussesList)
            {
                SubtribusItems.Add(item);
            }
            if (Tbl60SubtribussesList.Count == 0)
            {
                return;
            }

            if (SubtribusSelected == null && Tbl60SubtribussesList.Count != 0)
            {
                SubtribusSelected = SubtribusItems.FirstOrDefault()!;
            }
        }
    }

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
                GenusSelected = GenusItems.FirstOrDefault()!;
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
