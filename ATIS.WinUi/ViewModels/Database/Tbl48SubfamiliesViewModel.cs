
using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using ATIS.WinUi.Services;
using CommunityToolkit.Mvvm.ComponentModel;

//    Tbl48SubfamiliesViewModel Skriptdatum:  31.03.2023  10:32    

namespace ATIS.WinUi.ViewModels.Database;

public class Tbl48SubfamiliesViewModel : ObservableObject
{

    #region [Private Data Members]
    private readonly IDataService _dataService;
    public ObservableCollection<Tbl48Subfamily?> SubfamilyItems { get; } = new();
    public ObservableCollection<Tbl51Infrafamily> InfrafamilyItems { get; } = new();

    public ObservableCollection<Tbl45Family> FamilyItems { get; } = new();

    public ObservableCollection<Tbl90Reference> ReferenceExpertItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceSourceItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceAuthorItems { get; } = new();
    public ObservableCollection<Tbl93Comment> CommentItems { get; } = new();
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]      

    #region [Constructor]
    public Tbl48SubfamiliesViewModel(IDataService dataService)
    {
        _dataService = dataService;
        SelectedMainDetailTabIndex = 1; //Tab Datagrid
        GetAllCollections();
    }

    private void GetAllCollections()
    {
        Tbl48SubfamiliesAllList ??= new ObservableCollection<Tbl48Subfamily>();
        Tbl48SubfamiliesAllList = _dataService.GetTbl48SubfamiliesCollectionOrderBySubfamilyName();
        Tbl45FamiliesAllList ??= new ObservableCollection<Tbl45Family>();
        Tbl45FamiliesAllList = _dataService.GetTbl45FamiliesCollectionOrderByFamilyName();
        Tbl42SuperfamiliesAllList ??= new ObservableCollection<Tbl42Superfamily>();
        Tbl42SuperfamiliesAllList = _dataService.GetTbl42SuperfamiliesCollectionOrderBySuperfamilyName();

        Tbl90ExpertsAllList ??= new ObservableCollection<Tbl90RefExpert>();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();
        Tbl90SourcesAllList ??= new ObservableCollection<Tbl90RefSource>();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();
        Tbl90AuthorsAllList ??= new ObservableCollection<Tbl90RefAuthor>();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();
    }

    #endregion [Constructor]  


    //    Part 1    



    #region [Commands Subfamily]

    public ICommand GetSubfamiliesByNameOrIdCommand => new RelayCommand(execute: delegate
    {
        var task = GetSubfamiliesByNameOrId_Executed(SearchSubfamilyName);
    });

    public ICommand AddSubfamilyCommand => new RelayCommand<string>(AddSubfamily_Executed);
    public ICommand CopySubfamilyCommand => new RelayCommand<string>(CopySubfamily_Executed);

    public ICommand DeleteSubfamilyCommand => new RelayCommand(execute: delegate { DeleteSubfamily_Executed(SearchSubfamilyName); });

    public ICommand SaveSubfamilyCommand => new RelayCommand(execute: delegate { var task = SaveSubfamily_Executed(SearchSubfamilyName); });
    public ICommand RefreshSubfamilyServerCommand => new RelayCommand(execute: delegate { RefreshSubfamilyServer_Executed(SearchSubfamilyName); });

    #endregion [Commands Subfamily]       

    #region [Methods Tbl48Subfamily]

    private async Task GetSubfamiliesByNameOrId_Executed(string? parm)
    {
        IsLoading = true;
        SubfamilyStartModify();
        Tbl45FamiliesList?.Clear();
        Tbl48SubfamiliesList?.Clear();
        Tbl51InfrafamiliesList?.Clear();
        Tbl90ReferenceExpertsList?.Clear();
        Tbl90ReferenceSourcesList?.Clear();
        Tbl90ReferenceAuthorsList?.Clear();
        Tbl93CommentsList?.Clear();

        FamilyItems.Clear();
        SubfamilyItems.Clear();
        InfrafamilyItems.Clear();
        ReferenceAuthorItems.Clear();
        ReferenceSourceItems.Clear();
        ReferenceExpertItems.Clear();
        CommentItems.Clear();

        Tbl48SubfamiliesList ??= new ObservableCollection<Tbl48Subfamily>();
        Tbl48SubfamiliesList = await _dataService.GetTbl48SubfamiliesCollectionOrderBySubfamilyNameFromSearchNameOrId(parm!);

        if (Tbl48SubfamiliesList is { Count: 0 })
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        SubfamilyDataSetCount = Tbl48SubfamiliesList.Count;
        RefreshSubfamilyItems();

        SelectedMainDetailTabIndex = 1;
        IsLoading = false;
    }

    private async void AddSubfamily_Executed(string? parm)
    {
        SubfamilyStartEdit();
        SubfamilyStartNew();

        //Id search for first Dataset of Tbl45FamiliesList
        var single = await _dataService.GetFamilySingleFirstDataset();
        var id = single.FamilyId;

        Tbl48SubfamiliesList ??= new ObservableCollection<Tbl48Subfamily>();
        Tbl48SubfamiliesList.Insert(index: 0, item: new Tbl48Subfamily { SubfamilyName = "New", FamilyId = id });

        RefreshSubfamilyItems();
    }

    private async void CopySubfamily_Executed(string? parm)
    {
        SubfamilyStartEdit();
        SubfamilyStartNew();

        Tbl48SubfamiliesList = await _dataService.CopySubfamily(SubfamilySelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshSubfamilyItems();
    }

    private async void DeleteSubfamily_Executed(string? parm)
    {

        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(SubfamilySelected!.SubfamilyName!))
        {
            //necessary to delete before
            await _dataService.DeleteConnectedInfrafamilies(SubfamilySelected);
            await _dataService.DeleteConnectedSubfamilyReferences(SubfamilySelected);
            await _dataService.DeleteConnectedSubfamilyComments(SubfamilySelected);

            var ret = _dataService.DeleteSubfamily(SubfamilySelected);
            if (!await ret)
            {
                return;
            }

            Tbl48SubfamiliesList = await _dataService.GetTbl48SubfamiliesCollectionOrderBySubfamilyNameFromSearchNameOrId(parm!);

            SubfamilyDataSetCount = Tbl48SubfamiliesList.Count;
            RefreshSubfamilyItems();
        }
    }

    private async Task SaveSubfamily_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(SubfamilySelected?.SubfamilyName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl48SubfamiliesList != null)
        {

            var iNdx = Tbl48SubfamiliesList.IndexOf(Tbl48SubfamiliesList.First(t =>
                 t.SubfamilyName == SubfamilySelected.SubfamilyName));

            var ret = _dataService.SaveSubfamily(SubfamilySelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl48SubfamiliesList = await _dataService.GetLastDatasetInTbl48Subfamilies();
                RefreshSubfamilyItems();
            }
            else
            {
                if (SubfamilySelected.SubfamilyId == 0) //new
                {
                    Tbl48SubfamiliesList = await _dataService.GetLastDatasetInTbl48Subfamilies();
                    RefreshSubfamilyItems();
                }
                else
                {
                    Tbl48SubfamiliesList = await _dataService.GetTbl48SubfamiliesCollectionOrderBySubfamilyNameFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl48SubfamiliesList!.Count)
                    {
                        SubfamilyItems.Clear();
                        foreach (var item in Tbl48SubfamiliesList)
                        {
                            SubfamilyItems.Add(item);
                        }

                        SubfamilySelected = Tbl48SubfamiliesList[iNdx];
                    }
                }
            }
        }
        SubfamilyDataSetCount = Tbl48SubfamiliesList!.Count;
        SubfamilyCancelEditsAsync();
    }

    private async void RefreshSubfamilyServer_Executed(string? parm)
    {
        Tbl48SubfamiliesList = await _dataService.GetTbl48SubfamiliesCollectionOrderBySubfamilyNameFromSearchNameOrId(parm!);

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

    #endregion [Methods Tbl48Subfamily]    




    //    Part 2    


    #region "Public Commands Connect <== Tbl45Family"                 

    public ICommand SaveFamilyCommand => new RelayCommand<string>(SaveFamily_Executed);
    public ICommand RefreshFamilyServerCommand => new RelayCommand(execute: delegate { RefreshFamilyServer_Executed(SearchSubfamilyName); });

    private async void SaveFamily_Executed(string? parm)
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
    public void FamilyCancelEditsAsync()
    {
        IsInEdit = false;
        IsModified = false;
    }

    #endregion "Public Commands"                  



    //    Part 3    





    //    Part 4    


    #region [Public Commands Connect ==> Tbl51Infrafamily]       

    public ICommand AddInfrafamilyCommand => new RelayCommand<string>(AddInfrafamily_Executed);
    public ICommand CopyInfrafamilyCommand => new RelayCommand<string>(CopyInfrafamily_Executed);
    public ICommand DeleteInfrafamilyCommand => new RelayCommand<string>(DeleteInfrafamily_Executed);
    public ICommand SaveInfrafamilyCommand => new RelayCommand<string>(SaveInfrafamily_Executed);
    public ICommand RefreshInfrafamilyServerCommand => new RelayCommand(execute: delegate { RefreshInfrafamilyServer_Executed(InfrafamilySelected!.SubfamilyId); });

    #endregion [Public Commands Connect ==> Tbl51Infrafamily]    

    #region [Public Methods Connect ==> Tbl51Infrafamily]                   

    private void AddInfrafamily_Executed(string? parm)
    {
        InfrafamilyStartEdit();
        InfrafamilyStartNew();
        Tbl51InfrafamiliesList.Insert(0, new Tbl51Infrafamily { InfrafamilyName = "New", SubfamilyId = SubfamilySelected.SubfamilyId });

        InfrafamilyItems.Clear();
        foreach (var item in Tbl51InfrafamiliesList)
        {
            InfrafamilyItems.Add(item);
        }
        InfrafamilySelected = InfrafamilyItems.First();
    }

    private async void CopyInfrafamily_Executed(string? parm)
    {
        InfrafamilyStartEdit();
        InfrafamilyStartNew();
        InfrafamilySelected.SubfamilyId = SubfamilySelected.SubfamilyId;  //combo vorbelegen

        Tbl51InfrafamiliesList = await _dataService.CopyInfrafamily(InfrafamilySelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshInfrafamilyItems();
    }

    private async void DeleteInfrafamily_Executed(string? parm)
    {
        if (InfrafamilySelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.DeleteInfrafamily(InfrafamilySelected);
        if (!await ret)
        {
            return;
        }

        Tbl51InfrafamiliesList = _dataService.GetTbl51InfrafamiliesCollectionOrderByInfrafamilyNameFromSubfamilyId(InfrafamilySelected.SubfamilyId);

        InfrafamilyDataSetCount = Tbl51InfrafamiliesList.Count;
        RefreshInfrafamilyItems();
    }

    private async void SaveInfrafamily_Executed(string? parm)
    {
        if (InfrafamilySelected != null && string.IsNullOrEmpty(InfrafamilySelected.InfrafamilyName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl51InfrafamiliesList != null)
        {
            var indx = Tbl51InfrafamiliesList.IndexOf(Tbl51InfrafamiliesList.First(t =>
              InfrafamilySelected != null && t.InfrafamilyName == InfrafamilySelected.InfrafamilyName));

            if (InfrafamilySelected != null)
            {
                if (SubfamilySelected != null)
                {
                    InfrafamilySelected.SubfamilyId = SubfamilySelected.SubfamilyId;

                    var ret = _dataService.SaveInfrafamily(InfrafamilySelected);
                    if (!await ret)
                    {
                        return;
                    }

                    if (InfrafamilySelected.InfrafamilyId == 0) //new
                    {
                        Tbl51InfrafamiliesList = await _dataService.GetLastDatasetInTbl51Infrafamilies();
                        RefreshInfrafamilyItems();
                    }
                    else
                    {
                        Tbl51InfrafamiliesList = _dataService.GetTbl51InfrafamiliesCollectionOrderByInfrafamilyNameFromSubfamilyId(SubfamilySelected.SubfamilyId);
                        //   Index Position ?
                        if (indx < Tbl51InfrafamiliesList.Count)
                        {
                            InfrafamilyItems.Clear();
                            foreach (var item in Tbl51InfrafamiliesList)
                            {
                                InfrafamilyItems.Add(item);
                            }

                            InfrafamilySelected = Tbl51InfrafamiliesList[indx];  //Index
                        }
                    }
                }
            }
        }
        InfrafamilyDataSetCount = Tbl51InfrafamiliesList!.Count;
        InfrafamilyCancelEditsAsync();
    }

    private void RefreshInfrafamilyServer_Executed(int id)
    {
        Tbl51InfrafamiliesList ??= new ObservableCollection<Tbl51Infrafamily>();
        Tbl51InfrafamiliesList = _dataService.GetTbl51InfrafamiliesCollectionOrderByInfrafamilyNameFromSubfamilyId(id);

        InfrafamilyDataSetCount = Tbl51InfrafamiliesList.Count;

        RefreshInfrafamilyItems();
    }
    public void InfrafamilyStartEdit() => IsInEdit = true;
    public void InfrafamilyStartModify() => IsModified = true;
    public void InfrafamilyStartNew() => IsNewInfrafamily = true;
    public event EventHandler AddNewInfrafamilyCanceled = null!;
    public void InfrafamilyCancelEditsAsync()
    {
        if (IsNewInfrafamily)
        {
            IsInEdit = false;
            AddNewInfrafamilyCanceled?.Invoke(this, EventArgs.Empty);
            IsNewInfrafamily = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }
    #endregion [Public Methods  Connect ==> Tbl51Infrafamily]                                                                                                                                            



    //    Part 5    




    //    Part 6    




    //    Part 7    



    //    Part 8    


    #region [Commands Subfamily ==> Tbl90Reference Author]
    public ICommand AddReferenceAuthorCommand => new RelayCommand<string>(AddReferenceAuthor_Executed);
    public ICommand CopyReferenceAuthorCommand => new RelayCommand<string>(CopyReferenceAuthor_Executed);
    public ICommand DeleteReferenceAuthorCommand => new RelayCommand<string>(DeleteReferenceAuthor_Executed);
    public ICommand SaveReferenceAuthorCommand => new RelayCommand<string>(SaveReferenceAuthor_Executed);
    public ICommand RefreshReferenceAuthorServerCommand => new RelayCommand<string>(RefreshReferenceAuthorServer_Executed);
    #endregion [Commands Subfamily ==> Tbl90Reference Author]                

    #region [Methods Subfamily ==> Tbl90Reference Author]

    private void AddReferenceAuthor_Executed(string? parm)
    {
        ReferenceAuthorStartEdit();
        ReferenceAuthorStartNew();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

        Tbl90ReferenceAuthorsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", SubfamilyId = SubfamilySelected!.SubfamilyId });

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
        ReferenceAuthorSelected.SubfamilyId = SubfamilySelected.SubfamilyId; //combo vorbelegen

        Tbl90ReferenceAuthorsList = await _dataService.CopyReferenceSubfamily(ReferenceAuthorSelected, "Author");

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

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubfamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.SubfamilyId);
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

            if (SubfamilySelected != null)
            {
                ReferenceAuthorSelected.SubfamilyId = SubfamilySelected.SubfamilyId;
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
                Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubfamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.SubfamilyId);
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

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubfamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNull(SubfamilySelected.SubfamilyId);

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
    #endregion [Methods Subfamily ==> Tbl90Reference Author]                   

    #region [Commands Subfamily ==> Tbl90Reference Source]  
    public ICommand AddReferenceSourceCommand => new RelayCommand<string>(AddReferenceSource_Executed);
    public ICommand CopyReferenceSourceCommand => new RelayCommand<string>(CopyReferenceSource_Executed);
    public ICommand DeleteReferenceSourceCommand => new RelayCommand<string>(DeleteReferenceSource_Executed);
    public ICommand SaveReferenceSourceCommand => new RelayCommand<string>(SaveReferenceSource_Executed);
    public ICommand RefreshReferenceSourceServerCommand => new RelayCommand<string>(RefreshReferenceSourceServer_Executed);
    #endregion [Commands Subfamily ==> Tbl90Reference Source]         

    #region [Methods Subfamily ==> Tbl90Reference Source]      

    private void AddReferenceSource_Executed(string? parm)
    {
        ReferenceSourceStartEdit();
        ReferenceSourceStartNew();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

        Tbl90ReferenceSourcesList.Insert(index: 0, item: new Tbl90Reference { Info = "New", SubfamilyId = SubfamilySelected!.SubfamilyId });
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
        if (SubfamilySelected != null)
        {
            ReferenceSourceSelected.SubfamilyId = SubfamilySelected.SubfamilyId; //combo vorbelegen
        }
        Tbl90ReferenceSourcesList = await _dataService.CopyReferenceSubfamily(ReferenceSourceSelected, "Source");
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

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubfamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.SubfamilyId);
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

            if (SubfamilySelected != null)
            {
                ReferenceSourceSelected.SubfamilyId = SubfamilySelected.SubfamilyId;
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
                Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubfamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.SubfamilyId);
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

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubfamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(SubfamilySelected.SubfamilyId);

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
    #endregion [Methods Subfamily ==> Tbl90Reference Source]           

    #region [Commands Subfamily ==> Tbl90Reference Expert]       
    public ICommand AddReferenceExpertCommand => new RelayCommand<string>(AddReferenceExpert_Executed);
    public ICommand CopyReferenceExpertCommand => new RelayCommand<string>(CopyReferenceExpert_Executed);
    public ICommand DeleteReferenceExpertCommand => new RelayCommand<string>(DeleteReferenceExpert_Executed);
    public ICommand SaveReferenceExpertCommand => new RelayCommand<string>(SaveReferenceExpert_Executed);
    public ICommand RefreshReferenceExpertServerCommand => new RelayCommand<string>(RefreshReferenceExpertServer_Executed);
    #endregion [Commands Subfamily ==> Tbl90Reference Expert]                    

    #region [Methods Subfamily ==> Tbl90Reference Expert]                 

    private void AddReferenceExpert_Executed(string? parm)
    {
        ReferenceExpertStartEdit();
        ReferenceExpertStartNew();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

        Tbl90ReferenceExpertsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", SubfamilyId = SubfamilySelected!.SubfamilyId });
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
        if (SubfamilySelected != null)
        {
            ReferenceExpertSelected.SubfamilyId = SubfamilySelected.SubfamilyId; //combo vorbelegen
        }
        Tbl90ReferenceExpertsList = await _dataService.CopyReferenceSubfamily(ReferenceExpertSelected, "Expert");
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

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubfamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.SubfamilyId);
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
            if (SubfamilySelected != null)
            {
                ReferenceExpertSelected.SubfamilyId = SubfamilySelected.SubfamilyId;
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
                Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubfamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.SubfamilyId);
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

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubfamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(SubfamilySelected.SubfamilyId);

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
    #endregion [Methods Subfamily ==> Tbl90Reference Expert]                                 

    #region [Commands Subfamily ==> Tbl93Comments]         
    public ICommand AddCommentCommand => new RelayCommand<string>(AddComment_Executed);
    public ICommand CopyCommentCommand => new RelayCommand<string>(CopyComment_Executed);
    public ICommand DeleteCommentCommand => new RelayCommand<string>(DeleteComment_Executed);
    public ICommand SaveCommentCommand => new RelayCommand<string>(SaveComment_Executed);
    public ICommand RefreshCommentServerCommand => new RelayCommand<string>(RefreshCommentServer_Executed);
    #endregion [Commands Subfamily ==> Tbl93Comments]           


    #region [Methods Subfamily ==> Tbl93Comments]        

    private void AddComment_Executed(string? parm)
    {
        CommentStartEdit();
        CommentStartNew();
        Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = "New", SubfamilyId = SubfamilySelected!.SubfamilyId });

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
        if (SubfamilySelected != null)
        {
            CommentSelected.SubfamilyId = SubfamilySelected.SubfamilyId;  //combo vorbelegen
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

        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromSubfamilyId(CommentSelected.SubfamilyId);
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
                if (SubfamilySelected != null)
                {
                    CommentSelected.SubfamilyId = SubfamilySelected.SubfamilyId;

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
                        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromSubfamilyId(SubfamilySelected.SubfamilyId);
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
        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromSubfamilyId(SubfamilySelected.SubfamilyId);

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
    #endregion [Methods Subfamily ==> Tbl93Comments]                             


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
                if (SubfamilySelected != null)
                {
                    IsLoading = true;
                    FamilyStartModify();
                    Tbl42SuperfamiliesAllList = _dataService.GetTbl42SuperfamiliesCollectionOrderBySuperfamilyName();

                    Tbl45FamiliesList = _dataService.GetTbl45FamiliesCollectionOrderByFamilyNameFromFamilyId(SubfamilySelected.FamilyId);

                    FamilyDataSetCount = Tbl45FamiliesList.Count;
                    RefreshFamilyItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 1)
            {
            }

            if (_selectedMainDetailTabIndex == 2)
            {
                if (SubfamilySelected != null)
                {
                    IsLoading = true;
                    InfrafamilyStartModify();
                    Tbl51InfrafamiliesList = _dataService.GetTbl51InfrafamiliesCollectionOrderByInfrafamilyNameFromSubfamilyId(SubfamilySelected.SubfamilyId);

                    InfrafamilyDataSetCount = Tbl51InfrafamiliesList.Count;
                    RefreshInfrafamilyItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 3)
            {
                if (SubfamilySelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubfamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(SubfamilySelected.SubfamilyId);

                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 4)
            {
                if (SubfamilySelected != null)
                {
                    IsLoading = true;
                    CommentStartModify();
                    Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromSubfamilyId(SubfamilySelected.SubfamilyId);

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
                if (SubfamilySelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubfamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(SubfamilySelected.SubfamilyId);
                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 1)
            {
                if (SubfamilySelected != null)
                {
                    IsLoading = true;
                    ReferenceSourceStartModify();
                    Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

                    Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubfamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(SubfamilySelected.SubfamilyId);

                    ReferenceSourceDataSetCount = Tbl90ReferenceSourcesList.Count;
                    RefreshReferenceSourceItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 2)
            {
                if (SubfamilySelected != null)
                {
                    IsLoading = true;
                    ReferenceAuthorStartModify();
                    Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

                    Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubfamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNull(SubfamilySelected.SubfamilyId);

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

    private Tbl51Infrafamily _infrafamilySelected = null!;
    public Tbl51Infrafamily InfrafamilySelected
    {
        get => _infrafamilySelected;
        set => SetProperty(ref _infrafamilySelected, value);
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
    public string SearchSubfamilyName { get; set; } = null!;

    private ObservableCollection<Tbl45Family> _tbl45FamiliesList = null!;
    public ObservableCollection<Tbl45Family> Tbl45FamiliesList
    {
        get => _tbl45FamiliesList;
        set
        {
            _tbl45FamiliesList = value; OnPropertyChanged();
        }
    }
    private ObservableCollection<Tbl45Family>? _tbl45FamiliesAllList;
    public ObservableCollection<Tbl45Family>? Tbl45FamiliesAllList
    {
        get => _tbl45FamiliesAllList;
        set
        {
            _tbl45FamiliesAllList = value; OnPropertyChanged();
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
    private ObservableCollection<Tbl48Subfamily> _tbl48SubfamiliesAllList = null!;
    public ObservableCollection<Tbl48Subfamily> Tbl48SubfamiliesAllList
    {
        get => _tbl48SubfamiliesAllList;
        set
        {
            _tbl48SubfamiliesAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesList = null!;
    public ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesList
    {
        get => _tbl51InfrafamiliesList;
        set
        {
            _tbl51InfrafamiliesList = value; OnPropertyChanged();
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

    private int _infrafamilyDataSetCount;
    public int InfrafamilyDataSetCount
    {
        get => _infrafamilyDataSetCount;
        set
        {
            _infrafamilyDataSetCount = value; OnPropertyChanged();
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

    private bool _isNewSubfamily;
    public bool IsNewSubfamily
    {
        get => _isNewSubfamily;
        set => SetProperty(ref _isNewSubfamily, value);
    }
    private bool _isNewInfrafamily;
    public bool IsNewInfrafamily
    {
        get => _isNewInfrafamily;
        set => SetProperty(ref _isNewInfrafamily, value);
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
