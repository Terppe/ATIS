
using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using ATIS.WinUi.Services;
using CommunityToolkit.Mvvm.ComponentModel;

//    Tbl45FamiliesViewModel Skriptdatum:  31.03.2023  10:32    

namespace ATIS.WinUi.ViewModels.Database;

public class Tbl45FamiliesViewModel : ObservableObject
{

    #region [Private Data Members]
    private readonly IDataService _dataService;
    public ObservableCollection<Tbl45Family?> FamilyItems { get; } = new();
    public ObservableCollection<Tbl48Subfamily> SubfamilyItems { get; } = new();

    public ObservableCollection<Tbl42Superfamily> SuperfamilyItems { get; } = new();

    public ObservableCollection<Tbl90Reference> ReferenceExpertItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceSourceItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceAuthorItems { get; } = new();
    public ObservableCollection<Tbl93Comment> CommentItems { get; } = new();
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]      

    #region [Constructor]
    public Tbl45FamiliesViewModel(IDataService dataService)
    {
        _dataService = dataService;
        SelectedMainDetailTabIndex = 1; //Tab Datagrid
        GetAllCollections();
    }

    private void GetAllCollections()
    {
        Tbl45FamiliesAllList ??= new ObservableCollection<Tbl45Family>();
        Tbl45FamiliesAllList = _dataService.GetTbl45FamiliesCollectionOrderByFamilyName();
        Tbl42SuperfamiliesAllList ??= new ObservableCollection<Tbl42Superfamily>();
        Tbl42SuperfamiliesAllList = _dataService.GetTbl42SuperfamiliesCollectionOrderBySuperfamilyName();
        Tbl39InfraordosAllList ??= new ObservableCollection<Tbl39Infraordo>();
        Tbl39InfraordosAllList = _dataService.GetTbl39InfraordosCollectionOrderByInfraordoName();

        Tbl90ExpertsAllList ??= new ObservableCollection<Tbl90RefExpert>();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();
        Tbl90SourcesAllList ??= new ObservableCollection<Tbl90RefSource>();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();
        Tbl90AuthorsAllList ??= new ObservableCollection<Tbl90RefAuthor>();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();
    }

    #endregion [Constructor]  


    //    Part 1    



    #region [Commands Family]

    public ICommand GetFamiliesByNameOrIdCommand => new RelayCommand(execute: delegate
    {
        var task = GetFamiliesByNameOrId_Executed(SearchFamilyName);
    });

    public ICommand AddFamilyCommand => new RelayCommand<string>(AddFamily_Executed);
    public ICommand CopyFamilyCommand => new RelayCommand<string>(CopyFamily_Executed);

    public ICommand DeleteFamilyCommand => new RelayCommand(execute: delegate { DeleteFamily_Executed(SearchFamilyName); });

    public ICommand SaveFamilyCommand => new RelayCommand(execute: delegate { var task = SaveFamily_Executed(SearchFamilyName); });
    public ICommand RefreshFamilyServerCommand => new RelayCommand(execute: delegate { RefreshFamilyServer_Executed(SearchFamilyName); });

    #endregion [Commands Family]       

    #region [Methods Tbl45Family]

    private async Task GetFamiliesByNameOrId_Executed(string? parm)
    {
        IsLoading = true;
        FamilyStartModify();
        Tbl42SuperfamiliesList?.Clear();
        Tbl45FamiliesList?.Clear();
        Tbl48SubfamiliesList?.Clear();
        Tbl90ReferenceExpertsList?.Clear();
        Tbl90ReferenceSourcesList?.Clear();
        Tbl90ReferenceAuthorsList?.Clear();
        Tbl93CommentsList?.Clear();

        SuperfamilyItems.Clear();
        FamilyItems.Clear();
        SubfamilyItems.Clear();
        ReferenceAuthorItems.Clear();
        ReferenceSourceItems.Clear();
        ReferenceExpertItems.Clear();
        CommentItems.Clear();

        Tbl45FamiliesList ??= new ObservableCollection<Tbl45Family>();
        Tbl45FamiliesList = await _dataService.GetTbl45FamiliesCollectionOrderByFamilyNameFromSearchNameOrId(parm!);

        if (Tbl45FamiliesList is { Count: 0 })
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        FamilyDataSetCount = Tbl45FamiliesList.Count;
        RefreshFamilyItems();

        SelectedMainDetailTabIndex = 1;
        IsLoading = false;
    }

    private async void AddFamily_Executed(string? parm)
    {
        FamilyStartEdit();
        FamilyStartNew();

        //Id search for first Dataset of Tbl42SuperfamiliesList
        var single = await _dataService.GetSuperfamilySingleFirstDataset();
        var id = single.SuperfamilyId;

        Tbl45FamiliesList ??= new ObservableCollection<Tbl45Family>();
        Tbl45FamiliesList.Insert(index: 0, item: new Tbl45Family { FamilyName = "New", SuperfamilyId = id });

        RefreshFamilyItems();
    }

    private async void CopyFamily_Executed(string? parm)
    {
        FamilyStartEdit();
        FamilyStartNew();

        Tbl45FamiliesList = await _dataService.CopyFamily(FamilySelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshFamilyItems();
    }

    private async void DeleteFamily_Executed(string? parm)
    {

        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(FamilySelected!.FamilyName!))
        {
            //necessary to delete before
            await _dataService.DeleteConnectedSubfamilies(FamilySelected);
            await _dataService.DeleteConnectedFamilyReferences(FamilySelected);
            await _dataService.DeleteConnectedFamilyComments(FamilySelected);

            var ret = _dataService.DeleteFamily(FamilySelected);
            if (!await ret)
            {
                return;
            }

            Tbl45FamiliesList = await _dataService.GetTbl45FamiliesCollectionOrderByFamilyNameFromSearchNameOrId(parm!);

            FamilyDataSetCount = Tbl45FamiliesList.Count;
            RefreshFamilyItems();
        }
    }

    private async Task SaveFamily_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(FamilySelected?.FamilyName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl45FamiliesList != null)
        {

            var iNdx = Tbl45FamiliesList.IndexOf(Tbl45FamiliesList.First(t =>
                 t.FamilyName == FamilySelected.FamilyName));

            var ret = _dataService.SaveFamily(FamilySelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl45FamiliesList = await _dataService.GetLastDatasetInTbl45Families();
                RefreshFamilyItems();
            }
            else
            {
                if (FamilySelected.FamilyId == 0) //new
                {
                    Tbl45FamiliesList = await _dataService.GetLastDatasetInTbl45Families();
                    RefreshFamilyItems();
                }
                else
                {
                    Tbl45FamiliesList = await _dataService.GetTbl45FamiliesCollectionOrderByFamilyNameFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl45FamiliesList!.Count)
                    {
                        FamilyItems.Clear();
                        foreach (var item in Tbl45FamiliesList)
                        {
                            FamilyItems.Add(item);
                        }

                        FamilySelected = Tbl45FamiliesList[iNdx];
                    }
                }
            }
        }
        FamilyDataSetCount = Tbl45FamiliesList!.Count;
        FamilyCancelEditsAsync();
    }

    private async void RefreshFamilyServer_Executed(string? parm)
    {
        Tbl45FamiliesList = await _dataService.GetTbl45FamiliesCollectionOrderByFamilyNameFromSearchNameOrId(parm!);

        FamilyDataSetCount = Tbl45FamiliesList.Count;
        RefreshFamilyItems();
    }

    public void FamilyStartEdit() => IsInEdit = true;
    public void FamilyStartModify() => IsModified = true;
    public void FamilyStartNew() => IsNewFamily = true;
    public event EventHandler AddNewFamilyCanceled = null!;
    public void FamilyCancelEditsAsync()
    {
        if (IsNewFamily)
        {
            IsInEdit = false;
            AddNewFamilyCanceled?.Invoke(this, EventArgs.Empty);
            IsNewFamily = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }

    #endregion [Methods Tbl45Family]    




    //    Part 2    


    #region "Public Commands Connect <== Tbl42Superfamily"                 

    public ICommand SaveSuperfamilyCommand => new RelayCommand<string>(SaveSuperfamily_Executed);
    public ICommand RefreshSuperfamilyServerCommand => new RelayCommand(execute: delegate { RefreshSuperfamilyServer_Executed(SearchFamilyName); });

    private async void SaveSuperfamily_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(SuperfamilySelected?.SuperfamilyName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl42SuperfamiliesList != null)
        {
            var iNdx = Tbl42SuperfamiliesList.IndexOf(Tbl42SuperfamiliesList.First(t =>
               t.SuperfamilyName == SuperfamilySelected.SuperfamilyName));

            var ret = _dataService.SaveSuperfamily(SuperfamilySelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl42SuperfamiliesList = await _dataService.GetLastDatasetInTbl42Superfamilies();
                RefreshSuperfamilyItems();
            }
            else
            {
                if (SuperfamilySelected.SuperfamilyId == 0) //new
                {
                    Tbl42SuperfamiliesList = await _dataService.GetLastDatasetInTbl42Superfamilies();
                    RefreshSuperfamilyItems();
                }
                else
                {
                    Tbl42SuperfamiliesList = await _dataService.GetTbl42SuperfamiliesCollectionOrderBySuperfamilyNameFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl42SuperfamiliesList!.Count)
                    {
                        SuperfamilyItems.Clear();
                        foreach (var item in Tbl42SuperfamiliesList)
                        {
                            SuperfamilyItems.Add(item);
                        }

                        SuperfamilySelected = Tbl42SuperfamiliesList[iNdx];
                    }
                }
            }
        }

        SuperfamilyDataSetCount = Tbl42SuperfamiliesList!.Count;
        SuperfamilyCancelEditsAsync();
    }
    private async void RefreshSuperfamilyServer_Executed(string? parm)
    {
        Tbl42SuperfamiliesList = await _dataService.GetTbl42SuperfamiliesCollectionOrderBySuperfamilyNameFromSearchNameOrId(parm!);

        SuperfamilyDataSetCount = Tbl42SuperfamiliesList.Count;
        RefreshSuperfamilyItems();
    }

    public void SuperfamilyStartEdit() => IsInEdit = true;
    public void SuperfamilyStartModify() => IsModified = true;
    public void SuperfamilyCancelEditsAsync()
    {
        IsInEdit = false;
        IsModified = false;
    }

    #endregion "Public Commands"                  



    //    Part 3    





    //    Part 4    


    #region [Public Commands Connect ==> Tbl48Subfamily]       

    public ICommand AddSubfamilyCommand => new RelayCommand<string>(AddSubfamily_Executed);
    public ICommand CopySubfamilyCommand => new RelayCommand<string>(CopySubfamily_Executed);
    public ICommand DeleteSubfamilyCommand => new RelayCommand<string>(DeleteSubfamily_Executed);
    public ICommand SaveSubfamilyCommand => new RelayCommand<string>(SaveSubfamily_Executed);
    public ICommand RefreshSubfamilyServerCommand => new RelayCommand(execute: delegate { RefreshSubfamilyServer_Executed(SubfamilySelected!.FamilyId); });

    #endregion [Public Commands Connect ==> Tbl48Subfamily]    

    #region [Public Methods Connect ==> Tbl48Subfamily]                   

    private void AddSubfamily_Executed(string? parm)
    {
        SubfamilyStartEdit();
        SubfamilyStartNew();
        Tbl48SubfamiliesList.Insert(0, new Tbl48Subfamily { SubfamilyName = "New", FamilyId = FamilySelected.FamilyId });

        SubfamilyItems.Clear();
        foreach (var item in Tbl48SubfamiliesList)
        {
            SubfamilyItems.Add(item);
        }
        SubfamilySelected = SubfamilyItems.First();
    }

    private async void CopySubfamily_Executed(string? parm)
    {
        SubfamilyStartEdit();
        SubfamilyStartNew();
        SubfamilySelected.FamilyId = FamilySelected.FamilyId;  //combo vorbelegen

        Tbl48SubfamiliesList = await _dataService.CopySubfamily(SubfamilySelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshSubfamilyItems();
    }

    private async void DeleteSubfamily_Executed(string? parm)
    {
        if (SubfamilySelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.DeleteSubfamily(SubfamilySelected);
        if (!await ret)
        {
            return;
        }

        Tbl48SubfamiliesList = _dataService.GetTbl48SubfamiliesCollectionOrderBySubfamilyNameFromFamilyId(SubfamilySelected.FamilyId);

        SubfamilyDataSetCount = Tbl48SubfamiliesList.Count;
        RefreshSubfamilyItems();
    }

    private async void SaveSubfamily_Executed(string? parm)
    {
        if (SubfamilySelected != null && string.IsNullOrEmpty(SubfamilySelected.SubfamilyName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl48SubfamiliesList != null)
        {
            var indx = Tbl48SubfamiliesList.IndexOf(Tbl48SubfamiliesList.First(t =>
              SubfamilySelected != null && t.SubfamilyName == SubfamilySelected.SubfamilyName));

            if (SubfamilySelected != null)
            {
                if (FamilySelected != null)
                {
                    SubfamilySelected.FamilyId = FamilySelected.FamilyId;

                    var ret = _dataService.SaveSubfamily(SubfamilySelected);
                    if (!await ret)
                    {
                        return;
                    }

                    if (SubfamilySelected.SubfamilyId == 0) //new
                    {
                        Tbl48SubfamiliesList = await _dataService.GetLastDatasetInTbl48Subfamilies();
                        RefreshSubfamilyItems();
                    }
                    else
                    {
                        Tbl48SubfamiliesList = _dataService.GetTbl48SubfamiliesCollectionOrderBySubfamilyNameFromFamilyId(FamilySelected.FamilyId);
                        //   Index Position ?
                        if (indx < Tbl48SubfamiliesList.Count)
                        {
                            SubfamilyItems.Clear();
                            foreach (var item in Tbl48SubfamiliesList)
                            {
                                SubfamilyItems.Add(item);
                            }

                            SubfamilySelected = Tbl48SubfamiliesList[indx];  //Index
                        }
                    }
                }
            }
        }
        SubfamilyDataSetCount = Tbl48SubfamiliesList!.Count;
        SubfamilyCancelEditsAsync();
    }

    private void RefreshSubfamilyServer_Executed(int id)
    {
        Tbl48SubfamiliesList ??= new ObservableCollection<Tbl48Subfamily>();
        Tbl48SubfamiliesList = _dataService.GetTbl48SubfamiliesCollectionOrderBySubfamilyNameFromFamilyId(id);

        SubfamilyDataSetCount = Tbl48SubfamiliesList.Count;

        RefreshSubfamilyItems();
    }
    public void SubfamilyStartEdit() => IsInEdit = true;
    public void SubfamilyStartModify() => IsModified = true;
    public void SubfamilyStartNew() => IsNewSubfamily = true;
    public event EventHandler AddNewSubfamilyCanceled = null!;
    public void SubfamilyCancelEditsAsync()
    {
        if (IsNewSubfamily)
        {
            IsInEdit = false;
            AddNewSubfamilyCanceled?.Invoke(this, EventArgs.Empty);
            IsNewSubfamily = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }
    #endregion [Public Methods  Connect ==> Tbl48Subfamily]                                                                                                                                            



    //    Part 5    




    //    Part 6    




    //    Part 7    



    //    Part 8    


    #region [Commands Family ==> Tbl90Reference Author]
    public ICommand AddReferenceAuthorCommand => new RelayCommand<string>(AddReferenceAuthor_Executed);
    public ICommand CopyReferenceAuthorCommand => new RelayCommand<string>(CopyReferenceAuthor_Executed);
    public ICommand DeleteReferenceAuthorCommand => new RelayCommand<string>(DeleteReferenceAuthor_Executed);
    public ICommand SaveReferenceAuthorCommand => new RelayCommand<string>(SaveReferenceAuthor_Executed);
    public ICommand RefreshReferenceAuthorServerCommand => new RelayCommand<string>(RefreshReferenceAuthorServer_Executed);
    #endregion [Commands Family ==> Tbl90Reference Author]                

    #region [Methods Family ==> Tbl90Reference Author]

    private void AddReferenceAuthor_Executed(string? parm)
    {
        ReferenceAuthorStartEdit();
        ReferenceAuthorStartNew();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

        Tbl90ReferenceAuthorsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", FamilyId = FamilySelected!.FamilyId });

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
        ReferenceAuthorSelected.FamilyId = FamilySelected.FamilyId; //combo vorbelegen

        Tbl90ReferenceAuthorsList = await _dataService.CopyReferenceFamily(ReferenceAuthorSelected, "Author");

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

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromFamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.FamilyId);
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

            if (FamilySelected != null)
            {
                ReferenceAuthorSelected.FamilyId = FamilySelected.FamilyId;
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
                Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromFamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.FamilyId);
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

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromFamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNull(FamilySelected.FamilyId);

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
    #endregion [Methods Family ==> Tbl90Reference Author]                   

    #region [Commands Family ==> Tbl90Reference Source]  
    public ICommand AddReferenceSourceCommand => new RelayCommand<string>(AddReferenceSource_Executed);
    public ICommand CopyReferenceSourceCommand => new RelayCommand<string>(CopyReferenceSource_Executed);
    public ICommand DeleteReferenceSourceCommand => new RelayCommand<string>(DeleteReferenceSource_Executed);
    public ICommand SaveReferenceSourceCommand => new RelayCommand<string>(SaveReferenceSource_Executed);
    public ICommand RefreshReferenceSourceServerCommand => new RelayCommand<string>(RefreshReferenceSourceServer_Executed);
    #endregion [Commands Family ==> Tbl90Reference Source]         

    #region [Methods Family ==> Tbl90Reference Source]      

    private void AddReferenceSource_Executed(string? parm)
    {
        ReferenceSourceStartEdit();
        ReferenceSourceStartNew();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

        Tbl90ReferenceSourcesList.Insert(index: 0, item: new Tbl90Reference { Info = "New", FamilyId = FamilySelected!.FamilyId });
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
        if (FamilySelected != null)
        {
            ReferenceSourceSelected.FamilyId = FamilySelected.FamilyId; //combo vorbelegen
        }
        Tbl90ReferenceSourcesList = await _dataService.CopyReferenceFamily(ReferenceSourceSelected, "Source");
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

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromFamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.FamilyId);
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

            if (FamilySelected != null)
            {
                ReferenceSourceSelected.FamilyId = FamilySelected.FamilyId;
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
                Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromFamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.FamilyId);
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

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromFamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(FamilySelected.FamilyId);

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
    #endregion [Methods Family ==> Tbl90Reference Source]           

    #region [Commands Family ==> Tbl90Reference Expert]       
    public ICommand AddReferenceExpertCommand => new RelayCommand<string>(AddReferenceExpert_Executed);
    public ICommand CopyReferenceExpertCommand => new RelayCommand<string>(CopyReferenceExpert_Executed);
    public ICommand DeleteReferenceExpertCommand => new RelayCommand<string>(DeleteReferenceExpert_Executed);
    public ICommand SaveReferenceExpertCommand => new RelayCommand<string>(SaveReferenceExpert_Executed);
    public ICommand RefreshReferenceExpertServerCommand => new RelayCommand<string>(RefreshReferenceExpertServer_Executed);
    #endregion [Commands Family ==> Tbl90Reference Expert]                    

    #region [Methods Family ==> Tbl90Reference Expert]                 

    private void AddReferenceExpert_Executed(string? parm)
    {
        ReferenceExpertStartEdit();
        ReferenceExpertStartNew();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

        Tbl90ReferenceExpertsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", FamilyId = FamilySelected!.FamilyId });
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
        if (FamilySelected != null)
        {
            ReferenceExpertSelected.FamilyId = FamilySelected.FamilyId; //combo vorbelegen
        }
        Tbl90ReferenceExpertsList = await _dataService.CopyReferenceFamily(ReferenceExpertSelected, "Expert");
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

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromFamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.FamilyId);
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
            if (FamilySelected != null)
            {
                ReferenceExpertSelected.FamilyId = FamilySelected.FamilyId;
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
                Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromFamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.FamilyId);
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

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromFamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(FamilySelected.FamilyId);

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
    #endregion [Methods Family ==> Tbl90Reference Expert]                                 

    #region [Commands Family ==> Tbl93Comments]         
    public ICommand AddCommentCommand => new RelayCommand<string>(AddComment_Executed);
    public ICommand CopyCommentCommand => new RelayCommand<string>(CopyComment_Executed);
    public ICommand DeleteCommentCommand => new RelayCommand<string>(DeleteComment_Executed);
    public ICommand SaveCommentCommand => new RelayCommand<string>(SaveComment_Executed);
    public ICommand RefreshCommentServerCommand => new RelayCommand<string>(RefreshCommentServer_Executed);
    #endregion [Commands Family ==> Tbl93Comments]           


    #region [Methods Family ==> Tbl93Comments]        

    private void AddComment_Executed(string? parm)
    {
        CommentStartEdit();
        CommentStartNew();
        Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = "New", FamilyId = FamilySelected!.FamilyId });

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
        if (FamilySelected != null)
        {
            CommentSelected.FamilyId = FamilySelected.FamilyId;  //combo vorbelegen
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

        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromFamilyId(CommentSelected.FamilyId);
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
                if (FamilySelected != null)
                {
                    CommentSelected.FamilyId = FamilySelected.FamilyId;

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
                        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromFamilyId(FamilySelected.FamilyId);
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
        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromFamilyId(FamilySelected.FamilyId);

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
    #endregion [Methods Family ==> Tbl93Comments]                             


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
                if (FamilySelected != null)
                {
                    IsLoading = true;
                    SuperfamilyStartModify();
                    Tbl39InfraordosAllList = _dataService.GetTbl39InfraordosCollectionOrderByInfraordoName();

                    Tbl42SuperfamiliesList = _dataService.GetTbl42SuperfamiliesCollectionOrderBySuperfamilyNameFromSuperfamilyId(FamilySelected.SuperfamilyId);

                    SuperfamilyDataSetCount = Tbl42SuperfamiliesList.Count;
                    RefreshSuperfamilyItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 1)
            {
            }

            if (_selectedMainDetailTabIndex == 2)
            {
                if (FamilySelected != null)
                {
                    IsLoading = true;
                    SubfamilyStartModify();
                    Tbl48SubfamiliesList = _dataService.GetTbl48SubfamiliesCollectionOrderBySubfamilyNameFromFamilyId(FamilySelected.FamilyId);

                    SubfamilyDataSetCount = Tbl48SubfamiliesList.Count;
                    RefreshSubfamilyItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 3)
            {
                if (FamilySelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromFamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(FamilySelected.FamilyId);

                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 4)
            {
                if (FamilySelected != null)
                {
                    IsLoading = true;
                    CommentStartModify();
                    Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromFamilyId(FamilySelected.FamilyId);

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
                if (FamilySelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromFamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(FamilySelected.FamilyId);
                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 1)
            {
                if (FamilySelected != null)
                {
                    IsLoading = true;
                    ReferenceSourceStartModify();
                    Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

                    Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromFamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(FamilySelected.FamilyId);

                    ReferenceSourceDataSetCount = Tbl90ReferenceSourcesList.Count;
                    RefreshReferenceSourceItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 2)
            {
                if (FamilySelected != null)
                {
                    IsLoading = true;
                    ReferenceAuthorStartModify();
                    Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

                    Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromFamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNull(FamilySelected.FamilyId);

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

    private Tbl42Superfamily _superfamilySelected = null!;
    public Tbl42Superfamily SuperfamilySelected
    {
        get => _superfamilySelected;
        set => SetProperty(ref _superfamilySelected, value);
    }

    private Tbl45Family _familySelected = null!;
    public Tbl45Family FamilySelected
    {
        get => _familySelected;
        set => SetProperty(ref _familySelected, value);
    }

    private Tbl48Subfamily _subfamilySelected = null!;
    public Tbl48Subfamily SubfamilySelected
    {
        get => _subfamilySelected;
        set => SetProperty(ref _subfamilySelected, value);
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
    public string SearchFamilyName { get; set; } = null!;

    private ObservableCollection<Tbl42Superfamily> _tbl42SuperfamiliesList = null!;
    public ObservableCollection<Tbl42Superfamily> Tbl42SuperfamiliesList
    {
        get => _tbl42SuperfamiliesList;
        set
        {
            _tbl42SuperfamiliesList = value; OnPropertyChanged();
        }
    }
    private ObservableCollection<Tbl42Superfamily>? _tbl42SuperfamiliesAllList;
    public ObservableCollection<Tbl42Superfamily>? Tbl42SuperfamiliesAllList
    {
        get => _tbl42SuperfamiliesAllList;
        set
        {
            _tbl42SuperfamiliesAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl45Family> _tbl45FamiliesList = null!;
    public ObservableCollection<Tbl45Family> Tbl45FamiliesList
    {
        get => _tbl45FamiliesList;
        set
        {
            _tbl45FamiliesList = value; OnPropertyChanged();
        }
    }
    private ObservableCollection<Tbl45Family> _tbl45FamiliesAllList = null!;
    public ObservableCollection<Tbl45Family> Tbl45FamiliesAllList
    {
        get => _tbl45FamiliesAllList;
        set
        {
            _tbl45FamiliesAllList = value; OnPropertyChanged();
        }
    }
    private ObservableCollection<Tbl39Infraordo>? _tbl39InfraordosAllList;
    public ObservableCollection<Tbl39Infraordo>? Tbl39InfraordosAllList
    {
        get => _tbl39InfraordosAllList;
        set
        {
            _tbl39InfraordosAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl48Subfamily> _tbl48SubfamiliesList = null!;
    public ObservableCollection<Tbl48Subfamily> Tbl48SubfamiliesList
    {
        get => _tbl48SubfamiliesList;
        set
        {
            _tbl48SubfamiliesList = value; OnPropertyChanged();
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
    private int _superfamilyDataSetCount;
    public int SuperfamilyDataSetCount
    {
        get => _superfamilyDataSetCount;
        set
        {
            _superfamilyDataSetCount = value; OnPropertyChanged();
        }
    }

    private int _familyDataSetCount;
    public int FamilyDataSetCount
    {
        get => _familyDataSetCount;
        set
        {
            _familyDataSetCount = value; OnPropertyChanged();
        }
    }

    private int _subfamilyDataSetCount;
    public int SubfamilyDataSetCount
    {
        get => _subfamilyDataSetCount;
        set
        {
            _subfamilyDataSetCount = value; OnPropertyChanged();
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

    private bool _isNewFamily;
    public bool IsNewFamily
    {
        get => _isNewFamily;
        set => SetProperty(ref _isNewFamily, value);
    }
    private bool _isNewSubfamily;
    public bool IsNewSubfamily
    {
        get => _isNewSubfamily;
        set => SetProperty(ref _isNewSubfamily, value);
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
    private void RefreshSuperfamilyItems()
    {
        SuperfamilyItems.Clear();
        if (Tbl42SuperfamiliesList != null)
        {
            foreach (var item in Tbl42SuperfamiliesList)
            {
                SuperfamilyItems.Add(item);
            }
            if (Tbl42SuperfamiliesList.Count == 0)
            {
                return;
            }

            if (SuperfamilySelected == null && Tbl42SuperfamiliesList.Count != 0)
            {
                SuperfamilySelected = SuperfamilyItems.FirstOrDefault()!;
            }
        }
    }

    private void RefreshFamilyItems()
    {
        FamilyItems.Clear();
        if (Tbl45FamiliesList != null)
        {
            foreach (var item in Tbl45FamiliesList)
            {
                FamilyItems.Add(item);
            }
            if (Tbl45FamiliesList.Count == 0)
            {
                return;
            }

            if (FamilySelected == null && Tbl45FamiliesList.Count != 0)
            {
                FamilySelected = FamilyItems.FirstOrDefault()!;
            }
        }
    }

    private void RefreshSubfamilyItems()
    {
        SubfamilyItems.Clear();
        if (Tbl48SubfamiliesList != null)
        {
            foreach (var item in Tbl48SubfamiliesList)
            {
                SubfamilyItems.Add(item);
            }
            if (Tbl48SubfamiliesList.Count == 0)
            {
                return;
            }

            if (SubfamilySelected == null && Tbl48SubfamiliesList.Count != 0)
            {
                SubfamilySelected = SubfamilyItems.FirstOrDefault()!;
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
