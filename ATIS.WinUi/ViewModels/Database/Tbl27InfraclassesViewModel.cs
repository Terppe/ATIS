
using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using ATIS.WinUi.Services;
using CommunityToolkit.Mvvm.ComponentModel;

//    Tbl27InfraclassesViewModel Skriptdatum:  30.03.2023  18:32    

namespace ATIS.WinUi.ViewModels.Database;

public class Tbl27InfraclassesViewModel : ObservableObject
{

    #region [Private Data Members]
    private readonly IDataService _dataService;
    public ObservableCollection<Tbl27Infraclass?> InfraclassItems { get; } = new();
    public ObservableCollection<Tbl30Legio> LegioItems { get; } = new();

    public ObservableCollection<Tbl24Subclass> SubclassItems { get; } = new();

    public ObservableCollection<Tbl90Reference> ReferenceExpertItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceSourceItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceAuthorItems { get; } = new();
    public ObservableCollection<Tbl93Comment> CommentItems { get; } = new();
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]      

    #region [Constructor]
    public Tbl27InfraclassesViewModel(IDataService dataService)
    {
        _dataService = dataService;
        SelectedMainDetailTabIndex = 1; //Tab Datagrid
        GetAllCollections();
    }

    private void GetAllCollections()
    {
        Tbl27InfraclassesAllList ??= new ObservableCollection<Tbl27Infraclass>();
        Tbl27InfraclassesAllList = _dataService.GetTbl27InfraclassesCollectionOrderByInfraclassName();
        Tbl24SubclassesAllList ??= new ObservableCollection<Tbl24Subclass>();
        Tbl24SubclassesAllList = _dataService.GetTbl24SubclassesCollectionOrderBySubclassName();
        Tbl21ClassesAllList ??= new ObservableCollection<Tbl21Class>();
        Tbl21ClassesAllList = _dataService.GetTbl21ClassesCollectionOrderByClassName();

        Tbl90ExpertsAllList ??= new ObservableCollection<Tbl90RefExpert>();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();
        Tbl90SourcesAllList ??= new ObservableCollection<Tbl90RefSource>();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();
        Tbl90AuthorsAllList ??= new ObservableCollection<Tbl90RefAuthor>();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();
    }

    #endregion [Constructor]  


    //    Part 1    



    #region [Commands Infraclass]

    public ICommand GetInfraclassesByNameOrIdCommand => new RelayCommand(execute: delegate
    {
        var task = GetInfraclassesByNameOrId_Executed(SearchInfraclassName);
    });

    public ICommand AddInfraclassCommand => new RelayCommand<string>(AddInfraclass_Executed);
    public ICommand CopyInfraclassCommand => new RelayCommand<string>(CopyInfraclass_Executed);

    public ICommand DeleteInfraclassCommand => new RelayCommand(execute: delegate { DeleteInfraclass_Executed(SearchInfraclassName); });

    public ICommand SaveInfraclassCommand => new RelayCommand(execute: delegate { var task = SaveInfraclass_Executed(SearchInfraclassName); });
    public ICommand RefreshInfraclassServerCommand => new RelayCommand(execute: delegate { RefreshInfraclassServer_Executed(SearchInfraclassName); });

    #endregion [Commands Infraclass]       

    #region [Methods Tbl27Infraclass]

    private async Task GetInfraclassesByNameOrId_Executed(string? parm)
    {
        IsLoading = true;
        InfraclassStartModify();
        Tbl24SubclassesList?.Clear();
        Tbl27InfraclassesList?.Clear();
        Tbl30LegiosList?.Clear();
        Tbl90ReferenceExpertsList?.Clear();
        Tbl90ReferenceSourcesList?.Clear();
        Tbl90ReferenceAuthorsList?.Clear();
        Tbl93CommentsList?.Clear();

        SubclassItems.Clear();
        InfraclassItems.Clear();
        LegioItems.Clear();
        ReferenceAuthorItems.Clear();
        ReferenceSourceItems.Clear();
        ReferenceExpertItems.Clear();
        CommentItems.Clear();

        Tbl27InfraclassesList ??= new ObservableCollection<Tbl27Infraclass>();
        Tbl27InfraclassesList = await _dataService.GetTbl27InfraclassesCollectionOrderByInfraclassNameFromSearchNameOrId(parm!);

        if (Tbl27InfraclassesList is { Count: 0 })
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        InfraclassDataSetCount = Tbl27InfraclassesList.Count;
        RefreshInfraclassItems();

        SelectedMainDetailTabIndex = 1;
        IsLoading = false;
    }

    private async void AddInfraclass_Executed(string? parm)
    {
        InfraclassStartEdit();
        InfraclassStartNew();

        //Id search for first Dataset of Tbl24SubclassesList
        var single = await _dataService.GetSubclassSingleFirstDataset();
        var id = single.SubclassId;

        Tbl27InfraclassesList ??= new ObservableCollection<Tbl27Infraclass>();
        Tbl27InfraclassesList.Insert(index: 0, item: new Tbl27Infraclass { InfraclassName = "New", SubclassId = id });

        RefreshInfraclassItems();
    }

    private async void CopyInfraclass_Executed(string? parm)
    {
        InfraclassStartEdit();
        InfraclassStartNew();

        Tbl27InfraclassesList = await _dataService.CopyInfraclass(InfraclassSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshInfraclassItems();
    }

    private async void DeleteInfraclass_Executed(string? parm)
    {

        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(InfraclassSelected!.InfraclassName!))
        {
            //necessary to delete before
            await _dataService.DeleteConnectedLegios(InfraclassSelected);
            await _dataService.DeleteConnectedInfraclassReferences(InfraclassSelected);
            await _dataService.DeleteConnectedInfraclassComments(InfraclassSelected);

            var ret = _dataService.DeleteInfraclass(InfraclassSelected);
            if (!await ret)
            {
                return;
            }

            Tbl27InfraclassesList = await _dataService.GetTbl27InfraclassesCollectionOrderByInfraclassNameFromSearchNameOrId(parm!);

            InfraclassDataSetCount = Tbl27InfraclassesList.Count;
            RefreshInfraclassItems();
        }
    }

    private async Task SaveInfraclass_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(InfraclassSelected?.InfraclassName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl27InfraclassesList != null)
        {

            var iNdx = Tbl27InfraclassesList.IndexOf(Tbl27InfraclassesList.First(t =>
                 t.InfraclassName == InfraclassSelected.InfraclassName));

            var ret = _dataService.SaveInfraclass(InfraclassSelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl27InfraclassesList = await _dataService.GetLastDatasetInTbl27Infraclasses();
                RefreshInfraclassItems();
            }
            else
            {
                if (InfraclassSelected.InfraclassId == 0) //new
                {
                    Tbl27InfraclassesList = await _dataService.GetLastDatasetInTbl27Infraclasses();
                    RefreshInfraclassItems();
                }
                else
                {
                    Tbl27InfraclassesList = await _dataService.GetTbl27InfraclassesCollectionOrderByInfraclassNameFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl27InfraclassesList!.Count)
                    {
                        InfraclassItems.Clear();
                        foreach (var item in Tbl27InfraclassesList)
                        {
                            InfraclassItems.Add(item);
                        }

                        InfraclassSelected = Tbl27InfraclassesList[iNdx];
                    }
                }
            }
        }
        InfraclassDataSetCount = Tbl27InfraclassesList!.Count;
        InfraclassCancelEditsAsync();
    }

    private async void RefreshInfraclassServer_Executed(string? parm)
    {
        Tbl27InfraclassesList = await _dataService.GetTbl27InfraclassesCollectionOrderByInfraclassNameFromSearchNameOrId(parm!);

        InfraclassDataSetCount = Tbl27InfraclassesList.Count;
        RefreshInfraclassItems();
    }

    public void InfraclassStartEdit() => IsInEdit = true;
    public void InfraclassStartModify() => IsModified = true;
    public void InfraclassStartNew() => IsNewInfraclass = true;
    public event EventHandler AddNewInfraclassCanceled = null!;
    public void InfraclassCancelEditsAsync()
    {
        if (IsNewInfraclass)
        {
            IsInEdit = false;
            AddNewInfraclassCanceled?.Invoke(this, EventArgs.Empty);
            IsNewInfraclass = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }

    #endregion [Methods Tbl27Infraclass]    




    //    Part 2    


    #region "Public Commands Connect <== Tbl24Subclass"                 

    public ICommand SaveSubclassCommand => new RelayCommand<string>(SaveSubclass_Executed);
    public ICommand RefreshSubclassServerCommand => new RelayCommand(execute: delegate { RefreshSubclassServer_Executed(SearchInfraclassName); });

    private async void SaveSubclass_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(SubclassSelected?.SubclassName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl24SubclassesList != null)
        {
            var iNdx = Tbl24SubclassesList.IndexOf(Tbl24SubclassesList.First(t =>
               t.SubclassName == SubclassSelected.SubclassName));

            var ret = _dataService.SaveSubclass(SubclassSelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl24SubclassesList = await _dataService.GetLastDatasetInTbl24Subclasses();
                RefreshSubclassItems();
            }
            else
            {
                if (SubclassSelected.SubclassId == 0) //new
                {
                    Tbl24SubclassesList = await _dataService.GetLastDatasetInTbl24Subclasses();
                    RefreshSubclassItems();
                }
                else
                {
                    Tbl24SubclassesList = await _dataService.GetTbl24SubclassesCollectionOrderBySubclassNameFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl24SubclassesList!.Count)
                    {
                        SubclassItems.Clear();
                        foreach (var item in Tbl24SubclassesList)
                        {
                            SubclassItems.Add(item);
                        }

                        SubclassSelected = Tbl24SubclassesList[iNdx];
                    }
                }
            }
        }

        SubclassDataSetCount = Tbl24SubclassesList!.Count;
        SubclassCancelEditsAsync();
    }
    private async void RefreshSubclassServer_Executed(string? parm)
    {
        Tbl24SubclassesList = await _dataService.GetTbl24SubclassesCollectionOrderBySubclassNameFromSearchNameOrId(parm!);

        SubclassDataSetCount = Tbl24SubclassesList.Count;
        RefreshSubclassItems();
    }

    public void SubclassStartEdit() => IsInEdit = true;
    public void SubclassStartModify() => IsModified = true;
    public void SubclassCancelEditsAsync()
    {
        IsInEdit = false;
        IsModified = false;
    }

    #endregion "Public Commands"                  



    //    Part 3    





    //    Part 4    


    #region [Public Commands Connect ==> Tbl30Legio]       

    public ICommand AddLegioCommand => new RelayCommand<string>(AddLegio_Executed);
    public ICommand CopyLegioCommand => new RelayCommand<string>(CopyLegio_Executed);
    public ICommand DeleteLegioCommand => new RelayCommand<string>(DeleteLegio_Executed);
    public ICommand SaveLegioCommand => new RelayCommand<string>(SaveLegio_Executed);
    public ICommand RefreshLegioServerCommand => new RelayCommand(execute: delegate { RefreshLegioServer_Executed(LegioSelected!.InfraclassId); });

    #endregion [Public Commands Connect ==> Tbl30Legio]    

    #region [Public Methods Connect ==> Tbl30Legio]                   

    private void AddLegio_Executed(string? parm)
    {
        LegioStartEdit();
        LegioStartNew();
        Tbl30LegiosList.Insert(0, new Tbl30Legio { LegioName = "New", InfraclassId = InfraclassSelected.InfraclassId });

        LegioItems.Clear();
        foreach (var item in Tbl30LegiosList)
        {
            LegioItems.Add(item);
        }
        LegioSelected = LegioItems.First();
    }

    private async void CopyLegio_Executed(string? parm)
    {
        LegioStartEdit();
        LegioStartNew();
        LegioSelected.InfraclassId = InfraclassSelected.InfraclassId;  //combo vorbelegen

        Tbl30LegiosList = await _dataService.CopyLegio(LegioSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshLegioItems();
    }

    private async void DeleteLegio_Executed(string? parm)
    {
        if (LegioSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.DeleteLegio(LegioSelected);
        if (!await ret)
        {
            return;
        }

        Tbl30LegiosList = _dataService.GetTbl30LegiosCollectionOrderByLegioNameFromInfraclassId(LegioSelected.InfraclassId);

        LegioDataSetCount = Tbl30LegiosList.Count;
        RefreshLegioItems();
    }

    private async void SaveLegio_Executed(string? parm)
    {
        if (LegioSelected != null && string.IsNullOrEmpty(LegioSelected.LegioName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl30LegiosList != null)
        {
            var indx = Tbl30LegiosList.IndexOf(Tbl30LegiosList.First(t =>
              LegioSelected != null && t.LegioName == LegioSelected.LegioName));

            if (LegioSelected != null)
            {
                if (InfraclassSelected != null)
                {
                    LegioSelected.InfraclassId = InfraclassSelected.InfraclassId;

                    var ret = _dataService.SaveLegio(LegioSelected);
                    if (!await ret)
                    {
                        return;
                    }

                    if (LegioSelected.LegioId == 0) //new
                    {
                        Tbl30LegiosList = await _dataService.GetLastDatasetInTbl30Legios();
                        RefreshLegioItems();
                    }
                    else
                    {
                        Tbl30LegiosList = _dataService.GetTbl30LegiosCollectionOrderByLegioNameFromInfraclassId(InfraclassSelected.InfraclassId);
                        //   Index Position ?
                        if (indx < Tbl30LegiosList.Count)
                        {
                            LegioItems.Clear();
                            foreach (var item in Tbl30LegiosList)
                            {
                                LegioItems.Add(item);
                            }

                            LegioSelected = Tbl30LegiosList[indx];  //Index
                        }
                    }
                }
            }
        }
        LegioDataSetCount = Tbl30LegiosList!.Count;
        LegioCancelEditsAsync();
    }

    private void RefreshLegioServer_Executed(int id)
    {
        Tbl30LegiosList ??= new ObservableCollection<Tbl30Legio>();
        Tbl30LegiosList = _dataService.GetTbl30LegiosCollectionOrderByLegioNameFromInfraclassId(id);

        LegioDataSetCount = Tbl30LegiosList.Count;

        RefreshLegioItems();
    }
    public void LegioStartEdit() => IsInEdit = true;
    public void LegioStartModify() => IsModified = true;
    public void LegioStartNew() => IsNewLegio = true;
    public event EventHandler AddNewLegioCanceled = null!;
    public void LegioCancelEditsAsync()
    {
        if (IsNewLegio)
        {
            IsInEdit = false;
            AddNewLegioCanceled?.Invoke(this, EventArgs.Empty);
            IsNewLegio = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }
    #endregion [Public Methods  Connect ==> Tbl30Legio]                                                                                                                                            



    //    Part 5    




    //    Part 6    




    //    Part 7    



    //    Part 8    


    #region [Commands Infraclass ==> Tbl90Reference Author]
    public ICommand AddReferenceAuthorCommand => new RelayCommand<string>(AddReferenceAuthor_Executed);
    public ICommand CopyReferenceAuthorCommand => new RelayCommand<string>(CopyReferenceAuthor_Executed);
    public ICommand DeleteReferenceAuthorCommand => new RelayCommand<string>(DeleteReferenceAuthor_Executed);
    public ICommand SaveReferenceAuthorCommand => new RelayCommand<string>(SaveReferenceAuthor_Executed);
    public ICommand RefreshReferenceAuthorServerCommand => new RelayCommand<string>(RefreshReferenceAuthorServer_Executed);
    #endregion [Commands Infraclass ==> Tbl90Reference Author]                

    #region [Methods Infraclass ==> Tbl90Reference Author]

    private void AddReferenceAuthor_Executed(string? parm)
    {
        ReferenceAuthorStartEdit();
        ReferenceAuthorStartNew();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

        Tbl90ReferenceAuthorsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", InfraclassId = InfraclassSelected!.InfraclassId });

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
        ReferenceAuthorSelected.InfraclassId = InfraclassSelected.InfraclassId; //combo vorbelegen

        Tbl90ReferenceAuthorsList = await _dataService.CopyReferenceInfraclass(ReferenceAuthorSelected, "Author");

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

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromInfraclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.InfraclassId);
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

            if (InfraclassSelected != null)
            {
                ReferenceAuthorSelected.InfraclassId = InfraclassSelected.InfraclassId;
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
                Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromInfraclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.InfraclassId);
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

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromInfraclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull(InfraclassSelected.InfraclassId);

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
    #endregion [Methods Infraclass ==> Tbl90Reference Author]                   

    #region [Commands Infraclass ==> Tbl90Reference Source]  
    public ICommand AddReferenceSourceCommand => new RelayCommand<string>(AddReferenceSource_Executed);
    public ICommand CopyReferenceSourceCommand => new RelayCommand<string>(CopyReferenceSource_Executed);
    public ICommand DeleteReferenceSourceCommand => new RelayCommand<string>(DeleteReferenceSource_Executed);
    public ICommand SaveReferenceSourceCommand => new RelayCommand<string>(SaveReferenceSource_Executed);
    public ICommand RefreshReferenceSourceServerCommand => new RelayCommand<string>(RefreshReferenceSourceServer_Executed);
    #endregion [Commands Infraclass ==> Tbl90Reference Source]         

    #region [Methods Infraclass ==> Tbl90Reference Source]      

    private void AddReferenceSource_Executed(string? parm)
    {
        ReferenceSourceStartEdit();
        ReferenceSourceStartNew();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

        Tbl90ReferenceSourcesList.Insert(index: 0, item: new Tbl90Reference { Info = "New", InfraclassId = InfraclassSelected!.InfraclassId });
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
        if (InfraclassSelected != null)
        {
            ReferenceSourceSelected.InfraclassId = InfraclassSelected.InfraclassId; //combo vorbelegen
        }
        Tbl90ReferenceSourcesList = await _dataService.CopyReferenceInfraclass(ReferenceSourceSelected, "Source");
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

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromInfraclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.InfraclassId);
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

            if (InfraclassSelected != null)
            {
                ReferenceSourceSelected.InfraclassId = InfraclassSelected.InfraclassId;
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
                Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromInfraclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.InfraclassId);
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

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromInfraclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(InfraclassSelected.InfraclassId);

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
    #endregion [Methods Infraclass ==> Tbl90Reference Source]           

    #region [Commands Infraclass ==> Tbl90Reference Expert]       
    public ICommand AddReferenceExpertCommand => new RelayCommand<string>(AddReferenceExpert_Executed);
    public ICommand CopyReferenceExpertCommand => new RelayCommand<string>(CopyReferenceExpert_Executed);
    public ICommand DeleteReferenceExpertCommand => new RelayCommand<string>(DeleteReferenceExpert_Executed);
    public ICommand SaveReferenceExpertCommand => new RelayCommand<string>(SaveReferenceExpert_Executed);
    public ICommand RefreshReferenceExpertServerCommand => new RelayCommand<string>(RefreshReferenceExpertServer_Executed);
    #endregion [Commands Infraclass ==> Tbl90Reference Expert]                    

    #region [Methods Infraclass ==> Tbl90Reference Expert]                 

    private void AddReferenceExpert_Executed(string? parm)
    {
        ReferenceExpertStartEdit();
        ReferenceExpertStartNew();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

        Tbl90ReferenceExpertsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", InfraclassId = InfraclassSelected!.InfraclassId });
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
        if (InfraclassSelected != null)
        {
            ReferenceExpertSelected.InfraclassId = InfraclassSelected.InfraclassId; //combo vorbelegen
        }
        Tbl90ReferenceExpertsList = await _dataService.CopyReferenceInfraclass(ReferenceExpertSelected, "Expert");
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

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromInfraclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.InfraclassId);
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
            if (InfraclassSelected != null)
            {
                ReferenceExpertSelected.InfraclassId = InfraclassSelected.InfraclassId;
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
                Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromInfraclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.InfraclassId);
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

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromInfraclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(InfraclassSelected.InfraclassId);

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
    #endregion [Methods Infraclass ==> Tbl90Reference Expert]                                 

    #region [Commands Infraclass ==> Tbl93Comments]         
    public ICommand AddCommentCommand => new RelayCommand<string>(AddComment_Executed);
    public ICommand CopyCommentCommand => new RelayCommand<string>(CopyComment_Executed);
    public ICommand DeleteCommentCommand => new RelayCommand<string>(DeleteComment_Executed);
    public ICommand SaveCommentCommand => new RelayCommand<string>(SaveComment_Executed);
    public ICommand RefreshCommentServerCommand => new RelayCommand<string>(RefreshCommentServer_Executed);
    #endregion [Commands Infraclass ==> Tbl93Comments]           


    #region [Methods Infraclass ==> Tbl93Comments]        

    private void AddComment_Executed(string? parm)
    {
        CommentStartEdit();
        CommentStartNew();
        Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = "New", InfraclassId = InfraclassSelected!.InfraclassId });

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
        if (InfraclassSelected != null)
        {
            CommentSelected.InfraclassId = InfraclassSelected.InfraclassId;  //combo vorbelegen
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

        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromInfraclassId(CommentSelected.InfraclassId);
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
                if (InfraclassSelected != null)
                {
                    CommentSelected.InfraclassId = InfraclassSelected.InfraclassId;

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
                        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromInfraclassId(InfraclassSelected.InfraclassId);
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
        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromInfraclassId(InfraclassSelected.InfraclassId);

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
    #endregion [Methods Infraclass ==> Tbl93Comments]                             


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
                if (InfraclassSelected != null)
                {
                    IsLoading = true;
                    SubclassStartModify();
                    Tbl21ClassesAllList = _dataService.GetTbl21ClassesCollectionOrderByClassName();

                    Tbl24SubclassesList = _dataService.GetTbl24SubclassesCollectionOrderBySubclassNameFromSubclassId(InfraclassSelected.SubclassId);

                    SubclassDataSetCount = Tbl24SubclassesList.Count;
                    RefreshSubclassItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 1)
            {
            }

            if (_selectedMainDetailTabIndex == 2)
            {
                if (InfraclassSelected != null)
                {
                    IsLoading = true;
                    LegioStartModify();
                    Tbl30LegiosList = _dataService.GetTbl30LegiosCollectionOrderByLegioNameFromInfraclassId(InfraclassSelected.InfraclassId);

                    LegioDataSetCount = Tbl30LegiosList.Count;
                    RefreshLegioItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 3)
            {
                if (InfraclassSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromInfraclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(InfraclassSelected.InfraclassId);

                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 4)
            {
                if (InfraclassSelected != null)
                {
                    IsLoading = true;
                    CommentStartModify();
                    Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromInfraclassId(InfraclassSelected.InfraclassId);

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
                if (InfraclassSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromInfraclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(InfraclassSelected.InfraclassId);
                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 1)
            {
                if (InfraclassSelected != null)
                {
                    IsLoading = true;
                    ReferenceSourceStartModify();
                    Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

                    Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromInfraclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(InfraclassSelected.InfraclassId);

                    ReferenceSourceDataSetCount = Tbl90ReferenceSourcesList.Count;
                    RefreshReferenceSourceItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 2)
            {
                if (InfraclassSelected != null)
                {
                    IsLoading = true;
                    ReferenceAuthorStartModify();
                    Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

                    Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromInfraclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull(InfraclassSelected.InfraclassId);

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

    private Tbl24Subclass _subclassSelected = null!;
    public Tbl24Subclass SubclassSelected
    {
        get => _subclassSelected;
        set => SetProperty(ref _subclassSelected, value);
    }

    private Tbl27Infraclass _infraclassSelected = null!;
    public Tbl27Infraclass
        InfraclassSelected
    {
        get => _infraclassSelected;
        set => SetProperty(ref _infraclassSelected, value);
    }

    private Tbl30Legio _legioSelected = null!;
    public Tbl30Legio LegioSelected
    {
        get => _legioSelected;
        set => SetProperty(ref _legioSelected, value);
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
    public string SearchInfraclassName { get; set; } = null!;

    private ObservableCollection<Tbl24Subclass> _tbl24SubclassesList = null!;
    public ObservableCollection<Tbl24Subclass> Tbl24SubclassesList
    {
        get => _tbl24SubclassesList;
        set
        {
            _tbl24SubclassesList = value; OnPropertyChanged();
        }
    }
    private ObservableCollection<Tbl24Subclass>? _tbl24SubclassesAllList;
    public ObservableCollection<Tbl24Subclass>? Tbl24SubclassesAllList
    {
        get => _tbl24SubclassesAllList;
        set
        {
            _tbl24SubclassesAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl21Class>? _tbl21ClassesAllList;
    public ObservableCollection<Tbl21Class>? Tbl21ClassesAllList
    {
        get => _tbl21ClassesAllList;
        set
        {
            _tbl21ClassesAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesList = null!;
    public ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesList
    {
        get => _tbl27InfraclassesList;
        set
        {
            _tbl27InfraclassesList = value; OnPropertyChanged();
        }
    }
    private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesAllList = null!;
    public ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesAllList
    {
        get => _tbl27InfraclassesAllList;
        set
        {
            _tbl27InfraclassesAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl30Legio> _tbl30LegiosList = null!;
    public ObservableCollection<Tbl30Legio> Tbl30LegiosList
    {
        get => _tbl30LegiosList;
        set
        {
            _tbl30LegiosList = value; OnPropertyChanged();
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
    private int _subclassDataSetCount;
    public int SubclassDataSetCount
    {
        get => _subclassDataSetCount;
        set
        {
            _subclassDataSetCount = value; OnPropertyChanged();
        }
    }

    private int _infraclassDataSetCount;
    public int InfraclassDataSetCount
    {
        get => _infraclassDataSetCount;
        set
        {
            _infraclassDataSetCount = value; OnPropertyChanged();
        }
    }

    private int _legioDataSetCount;
    public int LegioDataSetCount
    {
        get => _legioDataSetCount;
        set
        {
            _legioDataSetCount = value; OnPropertyChanged();
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

    private bool _isNewInfraclass;
    public bool IsNewInfraclass
    {
        get => _isNewInfraclass;
        set => SetProperty(ref _isNewInfraclass, value);
    }
    private bool _isNewLegio;
    public bool IsNewLegio
    {
        get => _isNewLegio;
        set => SetProperty(ref _isNewLegio, value);
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
    private void RefreshSubclassItems()
    {
        SubclassItems.Clear();
        if (Tbl24SubclassesList != null)
        {
            foreach (var item in Tbl24SubclassesList)
            {
                SubclassItems.Add(item);
            }
            if (Tbl24SubclassesList.Count == 0)
            {
                return;
            }

            if (SubclassSelected == null && Tbl24SubclassesList.Count != 0)
            {
                SubclassSelected = SubclassItems.FirstOrDefault()!;
            }
        }
    }

    private void RefreshInfraclassItems()
    {
        InfraclassItems.Clear();
        if (Tbl27InfraclassesList != null)
        {
            foreach (var item in Tbl27InfraclassesList)
            {
                InfraclassItems.Add(item);
            }
            if (Tbl27InfraclassesList.Count == 0)
            {
                return;
            }

            if (InfraclassSelected == null && Tbl27InfraclassesList.Count != 0)
            {
                InfraclassSelected = InfraclassItems.FirstOrDefault()!;
            }
        }
    }

    private void RefreshLegioItems()
    {
        LegioItems.Clear();
        if (Tbl30LegiosList != null)
        {
            foreach (var item in Tbl30LegiosList)
            {
                LegioItems.Add(item);
            }
            if (Tbl30LegiosList.Count == 0)
            {
                return;
            }

            if (LegioSelected == null && Tbl30LegiosList.Count != 0)
            {
                LegioSelected = LegioItems.FirstOrDefault()!;
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
