
using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using ATIS.WinUi.Services;
using CommunityToolkit.Mvvm.ComponentModel;

//    Tbl54SupertribussesViewModel Skriptdatum:  01.04.2023  10:32    

namespace ATIS.WinUi.ViewModels.Database;

public class Tbl54SupertribussesViewModel : ObservableObject
{

    #region [Private Data Members]
    private readonly IDataService _dataService;
    public ObservableCollection<Tbl54Supertribus?> SupertribusItems { get; } = new();
    public ObservableCollection<Tbl57Tribus> TribusItems { get; } = new();

    public ObservableCollection<Tbl51Infrafamily> InfrafamilyItems { get; } = new();

    public ObservableCollection<Tbl90Reference> ReferenceExpertItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceSourceItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceAuthorItems { get; } = new();
    public ObservableCollection<Tbl93Comment> CommentItems { get; } = new();
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]      

    #region [Constructor]
    public Tbl54SupertribussesViewModel(IDataService dataService)
    {
        _dataService = dataService;
        SelectedMainDetailTabIndex = 1; //Tab Datagrid
        GetAllCollections();
    }

    private void GetAllCollections()
    {
        Tbl54SupertribussesAllList ??= new ObservableCollection<Tbl54Supertribus>();
        Tbl54SupertribussesAllList = _dataService.GetTbl54SupertribussesCollectionOrderBySupertribusName();
        Tbl51InfrafamiliesAllList ??= new ObservableCollection<Tbl51Infrafamily>();
        Tbl51InfrafamiliesAllList = _dataService.GetTbl51InfrafamiliesCollectionOrderByInfrafamilyName();
        Tbl48SubfamiliesAllList ??= new ObservableCollection<Tbl48Subfamily>();
        Tbl48SubfamiliesAllList = _dataService.GetTbl48SubfamiliesCollectionOrderBySubfamilyName();

        Tbl90ExpertsAllList ??= new ObservableCollection<Tbl90RefExpert>();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();
        Tbl90SourcesAllList ??= new ObservableCollection<Tbl90RefSource>();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();
        Tbl90AuthorsAllList ??= new ObservableCollection<Tbl90RefAuthor>();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();
    }

    #endregion [Constructor]  


    //    Part 1    



    #region [Commands Supertribus]

    public ICommand GetSupertribussesByNameOrIdCommand => new RelayCommand(execute: delegate
    {
        var task = GetSupertribussesByNameOrId_Executed(SearchSupertribusName);
    });

    public ICommand AddSupertribusCommand => new RelayCommand<string>(AddSupertribus_Executed);
    public ICommand CopySupertribusCommand => new RelayCommand<string>(CopySupertribus_Executed);

    public ICommand DeleteSupertribusCommand => new RelayCommand(execute: delegate { DeleteSupertribus_Executed(SearchSupertribusName); });

    public ICommand SaveSupertribusCommand => new RelayCommand(execute: delegate { var task = SaveSupertribus_Executed(SearchSupertribusName); });
    public ICommand RefreshSupertribusServerCommand => new RelayCommand(execute: delegate { RefreshSupertribusServer_Executed(SearchSupertribusName); });

    #endregion [Commands Supertribus]       

    #region [Methods Tbl54Supertribus]

    private async Task GetSupertribussesByNameOrId_Executed(string? parm)
    {
        IsLoading = true;
        SupertribusStartModify();
        Tbl51InfrafamiliesList?.Clear();
        Tbl54SupertribussesList?.Clear();
        Tbl57TribussesList?.Clear();
        Tbl90ReferenceExpertsList?.Clear();
        Tbl90ReferenceSourcesList?.Clear();
        Tbl90ReferenceAuthorsList?.Clear();
        Tbl93CommentsList?.Clear();

        InfrafamilyItems.Clear();
        SupertribusItems.Clear();
        TribusItems.Clear();
        ReferenceAuthorItems.Clear();
        ReferenceSourceItems.Clear();
        ReferenceExpertItems.Clear();
        CommentItems.Clear();

        Tbl54SupertribussesList ??= new ObservableCollection<Tbl54Supertribus>();
        Tbl54SupertribussesList = await _dataService.GetTbl54SupertribussesCollectionOrderBySupertribusNameFromSearchNameOrId(parm!);

        if (Tbl54SupertribussesList is { Count: 0 })
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        SupertribusDataSetCount = Tbl54SupertribussesList.Count;
        RefreshSupertribusItems();

        SelectedMainDetailTabIndex = 1;
        IsLoading = false;
    }

    private async void AddSupertribus_Executed(string? parm)
    {
        SupertribusStartEdit();
        SupertribusStartNew();

        //Id search for first Dataset of Tbl51InfrafamiliesList
        var single = await _dataService.GetInfrafamilySingleFirstDataset();
        var id = single.InfrafamilyId;

        Tbl54SupertribussesList ??= new ObservableCollection<Tbl54Supertribus>();
        Tbl54SupertribussesList.Insert(index: 0, item: new Tbl54Supertribus { SupertribusName = "New", InfrafamilyId = id });

        RefreshSupertribusItems();
    }

    private async void CopySupertribus_Executed(string? parm)
    {
        SupertribusStartEdit();
        SupertribusStartNew();

        Tbl54SupertribussesList = await _dataService.CopySupertribus(SupertribusSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshSupertribusItems();
    }

    private async void DeleteSupertribus_Executed(string? parm)
    {

        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(SupertribusSelected!.SupertribusName!))
        {
            //necessary to delete before
            await _dataService.DeleteConnectedTribusses(SupertribusSelected);
            await _dataService.DeleteConnectedSupertribusReferences(SupertribusSelected);
            await _dataService.DeleteConnectedSupertribusComments(SupertribusSelected);

            var ret = _dataService.DeleteSupertribus(SupertribusSelected);
            if (!await ret)
            {
                return;
            }

            Tbl54SupertribussesList = await _dataService.GetTbl54SupertribussesCollectionOrderBySupertribusNameFromSearchNameOrId(parm!);

            SupertribusDataSetCount = Tbl54SupertribussesList.Count;
            RefreshSupertribusItems();
        }
    }

    private async Task SaveSupertribus_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(SupertribusSelected?.SupertribusName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl54SupertribussesList != null)
        {

            var iNdx = Tbl54SupertribussesList.IndexOf(Tbl54SupertribussesList.First(t =>
                 t.SupertribusName == SupertribusSelected.SupertribusName));

            var ret = _dataService.SaveSupertribus(SupertribusSelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl54SupertribussesList = await _dataService.GetLastDatasetInTbl54Supertribusses();
                RefreshSupertribusItems();
            }
            else
            {
                if (SupertribusSelected.SupertribusId == 0) //new
                {
                    Tbl54SupertribussesList = await _dataService.GetLastDatasetInTbl54Supertribusses();
                    RefreshSupertribusItems();
                }
                else
                {
                    Tbl54SupertribussesList = await _dataService.GetTbl54SupertribussesCollectionOrderBySupertribusNameFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl54SupertribussesList!.Count)
                    {
                        SupertribusItems.Clear();
                        foreach (var item in Tbl54SupertribussesList)
                        {
                            SupertribusItems.Add(item);
                        }

                        SupertribusSelected = Tbl54SupertribussesList[iNdx];
                    }
                }
            }
        }
        SupertribusDataSetCount = Tbl54SupertribussesList!.Count;
        SupertribusCancelEditsAsync();
    }

    private async void RefreshSupertribusServer_Executed(string? parm)
    {
        Tbl54SupertribussesList = await _dataService.GetTbl54SupertribussesCollectionOrderBySupertribusNameFromSearchNameOrId(parm!);

        SupertribusDataSetCount = Tbl54SupertribussesList.Count;
        RefreshSupertribusItems();
    }

    public void SupertribusStartEdit() => IsInEdit = true;
    public void SupertribusStartModify() => IsModified = true;
    public void SupertribusStartNew() => IsNewSupertribus = true;
    public event EventHandler AddNewSupertribusCanceled = null!;
    public void SupertribusCancelEditsAsync()
    {
        if (IsNewSupertribus)
        {
            IsInEdit = false;
            AddNewSupertribusCanceled?.Invoke(this, EventArgs.Empty);
            IsNewSupertribus = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }

    #endregion [Methods Tbl54Supertribus]    




    //    Part 2    


    #region "Public Commands Connect <== Tbl51Infrafamily"                 

    public ICommand SaveInfrafamilyCommand => new RelayCommand<string>(SaveInfrafamily_Executed);
    public ICommand RefreshInfrafamilyServerCommand => new RelayCommand(execute: delegate { RefreshInfrafamilyServer_Executed(SearchSupertribusName); });

    private async void SaveInfrafamily_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(InfrafamilySelected?.InfrafamilyName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl51InfrafamiliesList != null)
        {
            var iNdx = Tbl51InfrafamiliesList.IndexOf(Tbl51InfrafamiliesList.First(t =>
               t.InfrafamilyName == InfrafamilySelected.InfrafamilyName));

            var ret = _dataService.SaveInfrafamily(InfrafamilySelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl51InfrafamiliesList = await _dataService.GetLastDatasetInTbl51Infrafamilies();
                RefreshInfrafamilyItems();
            }
            else
            {
                if (InfrafamilySelected.InfrafamilyId == 0) //new
                {
                    Tbl51InfrafamiliesList = await _dataService.GetLastDatasetInTbl51Infrafamilies();
                    RefreshInfrafamilyItems();
                }
                else
                {
                    Tbl51InfrafamiliesList = await _dataService.GetTbl51InfrafamiliesCollectionOrderByInfrafamilyNameFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl51InfrafamiliesList!.Count)
                    {
                        InfrafamilyItems.Clear();
                        foreach (var item in Tbl51InfrafamiliesList)
                        {
                            InfrafamilyItems.Add(item);
                        }

                        InfrafamilySelected = Tbl51InfrafamiliesList[iNdx];
                    }
                }
            }
        }

        InfrafamilyDataSetCount = Tbl51InfrafamiliesList!.Count;
        InfrafamilyCancelEditsAsync();
    }
    private async void RefreshInfrafamilyServer_Executed(string? parm)
    {
        if (parm != null)
        {
            Tbl51InfrafamiliesList =
                await _dataService.GetTbl51InfrafamiliesCollectionOrderByInfrafamilyNameFromSearchNameOrId(parm);
        }

        InfrafamilyDataSetCount = Tbl51InfrafamiliesList.Count;
        RefreshInfrafamilyItems();
    }

    public void InfrafamilyStartEdit() => IsInEdit = true;
    public void InfrafamilyStartModify() => IsModified = true;
    public void InfrafamilyCancelEditsAsync()
    {
        IsInEdit = false;
        IsModified = false;
    }

    #endregion "Public Commands"                  



    //    Part 3    





    //    Part 4    


    #region [Public Commands Connect ==> Tbl57Tribus]       

    public ICommand AddTribusCommand => new RelayCommand<string>(AddTribus_Executed);
    public ICommand CopyTribusCommand => new RelayCommand<string>(CopyTribus_Executed);
    public ICommand DeleteTribusCommand => new RelayCommand<string>(DeleteTribus_Executed);
    public ICommand SaveTribusCommand => new RelayCommand<string>(SaveTribus_Executed);
    public ICommand RefreshTribusServerCommand => new RelayCommand(execute: delegate { RefreshTribusServer_Executed(TribusSelected!.SupertribusId); });

    #endregion [Public Commands Connect ==> Tbl57Tribus]    

    #region [Public Methods Connect ==> Tbl57Tribus]                   

    private void AddTribus_Executed(string? parm)
    {
        TribusStartEdit();
        TribusStartNew();
        Tbl57TribussesList.Insert(0, new Tbl57Tribus { TribusName = "New", SupertribusId = SupertribusSelected.SupertribusId });

        TribusItems.Clear();
        foreach (var item in Tbl57TribussesList)
        {
            TribusItems.Add(item);
        }
        TribusSelected = TribusItems.First();
    }

    private async void CopyTribus_Executed(string? parm)
    {
        TribusStartEdit();
        TribusStartNew();
        TribusSelected.SupertribusId = SupertribusSelected.SupertribusId;  //combo vorbelegen

        Tbl57TribussesList = await _dataService.CopyTribus(TribusSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshTribusItems();
    }

    private async void DeleteTribus_Executed(string? parm)
    {
        if (TribusSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.DeleteTribus(TribusSelected);
        if (!await ret)
        {
            return;
        }

        Tbl57TribussesList = _dataService.GetTbl57TribussesCollectionOrderByTribusNameFromSupertribusId(TribusSelected.SupertribusId);

        TribusDataSetCount = Tbl57TribussesList.Count;
        RefreshTribusItems();
    }

    private async void SaveTribus_Executed(string? parm)
    {
        if (TribusSelected != null && string.IsNullOrEmpty(TribusSelected.TribusName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl57TribussesList != null)
        {
            var indx = Tbl57TribussesList.IndexOf(Tbl57TribussesList.First(t =>
              TribusSelected != null && t.TribusName == TribusSelected.TribusName));

            if (TribusSelected != null)
            {
                if (SupertribusSelected != null)
                {
                    TribusSelected.SupertribusId = SupertribusSelected.SupertribusId;

                    var ret = _dataService.SaveTribus(TribusSelected);
                    if (!await ret)
                    {
                        return;
                    }

                    if (TribusSelected.TribusId == 0) //new
                    {
                        Tbl57TribussesList = await _dataService.GetLastDatasetInTbl57Tribusses();
                        RefreshTribusItems();
                    }
                    else
                    {
                        Tbl57TribussesList = _dataService.GetTbl57TribussesCollectionOrderByTribusNameFromSupertribusId(SupertribusSelected.SupertribusId);
                        //   Index Position ?
                        if (indx < Tbl57TribussesList.Count)
                        {
                            TribusItems.Clear();
                            foreach (var item in Tbl57TribussesList)
                            {
                                TribusItems.Add(item);
                            }

                            TribusSelected = Tbl57TribussesList[indx];  //Index
                        }
                    }
                }
            }
        }
        TribusDataSetCount = Tbl57TribussesList!.Count;
        TribusCancelEditsAsync();
    }

    private void RefreshTribusServer_Executed(int id)
    {
        Tbl57TribussesList ??= new ObservableCollection<Tbl57Tribus>();
        Tbl57TribussesList = _dataService.GetTbl57TribussesCollectionOrderByTribusNameFromSupertribusId(id);

        TribusDataSetCount = Tbl57TribussesList.Count;

        RefreshTribusItems();
    }
    public void TribusStartEdit() => IsInEdit = true;
    public void TribusStartModify() => IsModified = true;
    public void TribusStartNew() => IsNewTribus = true;
    public event EventHandler AddNewTribusCanceled = null!;
    public void TribusCancelEditsAsync()
    {
        if (IsNewTribus)
        {
            IsInEdit = false;
            AddNewTribusCanceled?.Invoke(this, EventArgs.Empty);
            IsNewTribus = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }
    #endregion [Public Methods  Connect ==> Tbl57Tribus]                                                                                                                                            



    //    Part 5    




    //    Part 6    




    //    Part 7    



    //    Part 8    


    #region [Commands Supertribus ==> Tbl90Reference Author]
    public ICommand AddReferenceAuthorCommand => new RelayCommand<string>(AddReferenceAuthor_Executed);
    public ICommand CopyReferenceAuthorCommand => new RelayCommand<string>(CopyReferenceAuthor_Executed);
    public ICommand DeleteReferenceAuthorCommand => new RelayCommand<string>(DeleteReferenceAuthor_Executed);
    public ICommand SaveReferenceAuthorCommand => new RelayCommand<string>(SaveReferenceAuthor_Executed);
    public ICommand RefreshReferenceAuthorServerCommand => new RelayCommand<string>(RefreshReferenceAuthorServer_Executed);
    #endregion [Commands Supertribus ==> Tbl90Reference Author]                

    #region [Methods Supertribus ==> Tbl90Reference Author]

    private void AddReferenceAuthor_Executed(string? parm)
    {
        ReferenceAuthorStartEdit();
        ReferenceAuthorStartNew();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

        Tbl90ReferenceAuthorsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", SupertribusId = SupertribusSelected!.SupertribusId });

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
        ReferenceAuthorSelected.SupertribusId = SupertribusSelected.SupertribusId; //combo vorbelegen

        Tbl90ReferenceAuthorsList = await _dataService.CopyReferenceSupertribus(ReferenceAuthorSelected, "Author");

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

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSupertribusIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.SupertribusId);
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

            if (SupertribusSelected != null)
            {
                ReferenceAuthorSelected.SupertribusId = SupertribusSelected.SupertribusId;
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
                Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSupertribusIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.SupertribusId);
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

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSupertribusIdAndRefSourceIdIsNullAndRefExpertIdIsNull(SupertribusSelected.SupertribusId);

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
    #endregion [Methods Supertribus ==> Tbl90Reference Author]                   

    #region [Commands Supertribus ==> Tbl90Reference Source]  
    public ICommand AddReferenceSourceCommand => new RelayCommand<string>(AddReferenceSource_Executed);
    public ICommand CopyReferenceSourceCommand => new RelayCommand<string>(CopyReferenceSource_Executed);
    public ICommand DeleteReferenceSourceCommand => new RelayCommand<string>(DeleteReferenceSource_Executed);
    public ICommand SaveReferenceSourceCommand => new RelayCommand<string>(SaveReferenceSource_Executed);
    public ICommand RefreshReferenceSourceServerCommand => new RelayCommand<string>(RefreshReferenceSourceServer_Executed);
    #endregion [Commands Supertribus ==> Tbl90Reference Source]         

    #region [Methods Supertribus ==> Tbl90Reference Source]      

    private void AddReferenceSource_Executed(string? parm)
    {
        ReferenceSourceStartEdit();
        ReferenceSourceStartNew();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

        Tbl90ReferenceSourcesList.Insert(index: 0, item: new Tbl90Reference { Info = "New", SupertribusId = SupertribusSelected!.SupertribusId });
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
        if (SupertribusSelected != null)
        {
            ReferenceSourceSelected.SupertribusId = SupertribusSelected.SupertribusId; //combo vorbelegen
        }
        Tbl90ReferenceSourcesList = await _dataService.CopyReferenceSupertribus(ReferenceSourceSelected, "Source");
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

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromSupertribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.SupertribusId);
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

            if (SupertribusSelected != null)
            {
                ReferenceSourceSelected.SupertribusId = SupertribusSelected.SupertribusId;
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
                Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromSupertribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.SupertribusId);
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

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromSupertribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(SupertribusSelected.SupertribusId);

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
    #endregion [Methods Supertribus ==> Tbl90Reference Source]           

    #region [Commands Supertribus ==> Tbl90Reference Expert]       
    public ICommand AddReferenceExpertCommand => new RelayCommand<string>(AddReferenceExpert_Executed);
    public ICommand CopyReferenceExpertCommand => new RelayCommand<string>(CopyReferenceExpert_Executed);
    public ICommand DeleteReferenceExpertCommand => new RelayCommand<string>(DeleteReferenceExpert_Executed);
    public ICommand SaveReferenceExpertCommand => new RelayCommand<string>(SaveReferenceExpert_Executed);
    public ICommand RefreshReferenceExpertServerCommand => new RelayCommand<string>(RefreshReferenceExpertServer_Executed);
    #endregion [Commands Supertribus ==> Tbl90Reference Expert]                    

    #region [Methods Supertribus ==> Tbl90Reference Expert]                 

    private void AddReferenceExpert_Executed(string? parm)
    {
        ReferenceExpertStartEdit();
        ReferenceExpertStartNew();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

        Tbl90ReferenceExpertsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", SupertribusId = SupertribusSelected!.SupertribusId });
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
        if (SupertribusSelected != null)
        {
            ReferenceExpertSelected.SupertribusId = SupertribusSelected.SupertribusId; //combo vorbelegen
        }
        Tbl90ReferenceExpertsList = await _dataService.CopyReferenceSupertribus(ReferenceExpertSelected, "Expert");
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

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSupertribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.SupertribusId);
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
            if (SupertribusSelected != null)
            {
                ReferenceExpertSelected.SupertribusId = SupertribusSelected.SupertribusId;
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
                Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSupertribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.SupertribusId);
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

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSupertribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(SupertribusSelected.SupertribusId);

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
    #endregion [Methods Supertribus ==> Tbl90Reference Expert]                                 

    #region [Commands Supertribus ==> Tbl93Comments]         
    public ICommand AddCommentCommand => new RelayCommand<string>(AddComment_Executed);
    public ICommand CopyCommentCommand => new RelayCommand<string>(CopyComment_Executed);
    public ICommand DeleteCommentCommand => new RelayCommand<string>(DeleteComment_Executed);
    public ICommand SaveCommentCommand => new RelayCommand<string>(SaveComment_Executed);
    public ICommand RefreshCommentServerCommand => new RelayCommand<string>(RefreshCommentServer_Executed);
    #endregion [Commands Supertribus ==> Tbl93Comments]           


    #region [Methods Supertribus ==> Tbl93Comments]        

    private void AddComment_Executed(string? parm)
    {
        CommentStartEdit();
        CommentStartNew();
        Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = "New", SupertribusId = SupertribusSelected!.SupertribusId });

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
        if (SupertribusSelected != null)
        {
            CommentSelected.SupertribusId = SupertribusSelected.SupertribusId;  //combo vorbelegen
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

        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromSupertribusId(CommentSelected.SupertribusId);
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
                if (SupertribusSelected != null)
                {
                    CommentSelected.SupertribusId = SupertribusSelected.SupertribusId;

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
                        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromSupertribusId(SupertribusSelected.SupertribusId);
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
        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromSupertribusId(SupertribusSelected.SupertribusId);

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
    #endregion [Methods Supertribus ==> Tbl93Comments]                             


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
                if (SupertribusSelected != null)
                {
                    IsLoading = true;
                    InfrafamilyStartModify();
                    Tbl48SubfamiliesAllList = _dataService.GetTbl48SubfamiliesCollectionOrderBySubfamilyName();

                    Tbl51InfrafamiliesList = _dataService.GetTbl51InfrafamiliesCollectionOrderByInfrafamilyNameFromInfrafamilyId(SupertribusSelected.InfrafamilyId);

                    InfrafamilyDataSetCount = Tbl51InfrafamiliesList.Count;
                    RefreshInfrafamilyItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 1)
            {
            }

            if (_selectedMainDetailTabIndex == 2)
            {
                if (SupertribusSelected != null)
                {
                    IsLoading = true;
                    TribusStartModify();
                    Tbl57TribussesList = _dataService.GetTbl57TribussesCollectionOrderByTribusNameFromSupertribusId(SupertribusSelected.SupertribusId);

                    TribusDataSetCount = Tbl57TribussesList.Count;
                    RefreshTribusItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 3)
            {
                if (SupertribusSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSupertribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(SupertribusSelected.SupertribusId);

                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 4)
            {
                if (SupertribusSelected != null)
                {
                    IsLoading = true;
                    CommentStartModify();
                    Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromSupertribusId(SupertribusSelected.SupertribusId);

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
                if (SupertribusSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSupertribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(SupertribusSelected.SupertribusId);
                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 1)
            {
                if (SupertribusSelected != null)
                {
                    IsLoading = true;
                    ReferenceSourceStartModify();
                    Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

                    Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromSupertribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(SupertribusSelected.SupertribusId);

                    ReferenceSourceDataSetCount = Tbl90ReferenceSourcesList.Count;
                    RefreshReferenceSourceItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 2)
            {
                if (SupertribusSelected != null)
                {
                    IsLoading = true;
                    ReferenceAuthorStartModify();
                    Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

                    Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSupertribusIdAndRefSourceIdIsNullAndRefExpertIdIsNull(SupertribusSelected.SupertribusId);

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

    private Tbl51Infrafamily _infrafamilySelected = null!;
    public Tbl51Infrafamily InfrafamilySelected
    {
        get => _infrafamilySelected;
        set => SetProperty(ref _infrafamilySelected, value);
    }

    private Tbl54Supertribus _supertribusSelected = null!;
    public Tbl54Supertribus SupertribusSelected
    {
        get => _supertribusSelected;
        set => SetProperty(ref _supertribusSelected, value);
    }

    private Tbl57Tribus _tribusSelected = null!;
    public Tbl57Tribus TribusSelected
    {
        get => _tribusSelected;
        set => SetProperty(ref _tribusSelected, value);
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
    public string SearchSupertribusName { get; set; } = null!;

    private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesList = null!;
    public ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesList
    {
        get => _tbl51InfrafamiliesList;
        set
        {
            _tbl51InfrafamiliesList = value; OnPropertyChanged();
        }
    }
    private ObservableCollection<Tbl51Infrafamily>? _tbl51InfrafamiliesAllList;
    public ObservableCollection<Tbl51Infrafamily>? Tbl51InfrafamiliesAllList
    {
        get => _tbl51InfrafamiliesAllList;
        set
        {
            _tbl51InfrafamiliesAllList = value; OnPropertyChanged();
        }
    }
    private ObservableCollection<Tbl48Subfamily>? _tbl48SubfamiliesAllList;
    public ObservableCollection<Tbl48Subfamily>? Tbl48SubfamiliesAllList
    {
        get => _tbl48SubfamiliesAllList;
        set
        {
            _tbl48SubfamiliesAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl54Supertribus> _tbl54SupertribussesList = null!;
    public ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesList
    {
        get => _tbl54SupertribussesList;
        set
        {
            _tbl54SupertribussesList = value; OnPropertyChanged();
        }
    }
    private ObservableCollection<Tbl54Supertribus> _tbl54SupertribussesAllList = null!;
    public ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesAllList
    {
        get => _tbl54SupertribussesAllList;
        set
        {
            _tbl54SupertribussesAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl57Tribus> _tbl57TribussesList = null!;
    public ObservableCollection<Tbl57Tribus> Tbl57TribussesList
    {
        get => _tbl57TribussesList;
        set
        {
            _tbl57TribussesList = value; OnPropertyChanged();
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
    private int _infrafamilyDataSetCount;
    public int InfrafamilyDataSetCount
    {
        get => _infrafamilyDataSetCount;
        set
        {
            _infrafamilyDataSetCount = value; OnPropertyChanged();
        }
    }

    private int _supertribusDataSetCount;
    public int SupertribusDataSetCount
    {
        get => _supertribusDataSetCount;
        set
        {
            _supertribusDataSetCount = value; OnPropertyChanged();
        }
    }

    private int _tribusDataSetCount;
    public int TribusDataSetCount
    {
        get => _tribusDataSetCount;
        set
        {
            _tribusDataSetCount = value; OnPropertyChanged();
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

    private bool _isNewSupertribus;
    public bool IsNewSupertribus
    {
        get => _isNewSupertribus;
        set => SetProperty(ref _isNewSupertribus, value);
    }
    private bool _isNewTribus;
    public bool IsNewTribus
    {
        get => _isNewTribus;
        set => SetProperty(ref _isNewTribus, value);
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
    private void RefreshInfrafamilyItems()
    {
        InfrafamilyItems.Clear();
        if (Tbl51InfrafamiliesList != null)
        {
            foreach (var item in Tbl51InfrafamiliesList)
            {
                InfrafamilyItems.Add(item);
            }
            if (Tbl51InfrafamiliesList.Count == 0)
            {
                return;
            }

            if (InfrafamilySelected == null && Tbl51InfrafamiliesList.Count != 0)
            {
                InfrafamilySelected = InfrafamilyItems.FirstOrDefault()!;
            }
        }
    }

    private void RefreshSupertribusItems()
    {
        SupertribusItems.Clear();
        if (Tbl54SupertribussesList != null)
        {
            foreach (var item in Tbl54SupertribussesList)
            {
                SupertribusItems.Add(item);
            }
            if (Tbl54SupertribussesList.Count == 0)
            {
                return;
            }

            if (SupertribusSelected == null && Tbl54SupertribussesList.Count != 0)
            {
                SupertribusSelected = SupertribusItems.FirstOrDefault()!;
            }
        }
    }

    private void RefreshTribusItems()
    {
        TribusItems.Clear();
        if (Tbl57TribussesList != null)
        {
            foreach (var item in Tbl57TribussesList)
            {
                TribusItems.Add(item);
            }
            if (Tbl57TribussesList.Count == 0)
            {
                return;
            }

            if (TribusSelected == null && Tbl57TribussesList.Count != 0)
            {
                TribusSelected = TribusItems.FirstOrDefault()!;
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
