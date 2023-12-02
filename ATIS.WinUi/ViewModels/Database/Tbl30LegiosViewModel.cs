
using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using ATIS.WinUi.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using ATIS.WinUi.ViewModels.Database;
using CommunityToolkit.WinUI.UI.Controls;

//    Tbl30LegiosViewModel Skriptdatum:  31.03.2023  10:32    

namespace ATIS.WinUi.ViewModels.Database;

public class Tbl30LegiosViewModel : ObservableObject
{

    #region [Private Data Members]
    private readonly IDataService _dataService;
    public ObservableCollection<Tbl30Legio?> LegioItems { get; } = new();
    public ObservableCollection<Tbl33Ordo> OrdoItems { get; } = new();

    public ObservableCollection<Tbl27Infraclass> InfraclassItems { get; } = new();

    public ObservableCollection<Tbl90Reference> ReferenceExpertItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceSourceItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceAuthorItems { get; } = new();
    public ObservableCollection<Tbl93Comment> CommentItems { get; } = new();
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]      

    #region [Constructor]
    public Tbl30LegiosViewModel(IDataService dataService)
    {
        _dataService = dataService;
        SelectedMainDetailTabIndex = 1; //Tab Datagrid
        GetAllCollections();
    }

    private void GetAllCollections()
    {
        Tbl30LegiosAllList ??= new ObservableCollection<Tbl30Legio>();
        Tbl30LegiosAllList = _dataService.GetTbl30LegiosCollectionOrderByLegioName();
        Tbl27InfraclassesAllList ??= new ObservableCollection<Tbl27Infraclass>();
        Tbl27InfraclassesAllList = _dataService.GetTbl27InfraclassesCollectionOrderByInfraclassName();
        Tbl24SubclassesAllList ??= new ObservableCollection<Tbl24Subclass>();
        Tbl24SubclassesAllList = _dataService.GetTbl24SubclassesCollectionOrderBySubclassName();

        Tbl90ExpertsAllList ??= new ObservableCollection<Tbl90RefExpert>();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();
        Tbl90SourcesAllList ??= new ObservableCollection<Tbl90RefSource>();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();
        Tbl90AuthorsAllList ??= new ObservableCollection<Tbl90RefAuthor>();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();
    }

    #endregion [Constructor]  


    //    Part 1    



    #region [Commands Legio]

    public ICommand GetLegiosByNameOrIdCommand => new RelayCommand(execute: delegate
    {
        var task = GetLegiosByNameOrId_Executed(SearchLegioName);
    });

    public ICommand AddLegioCommand => new RelayCommand<string>(AddLegio_Executed);
    public ICommand CopyLegioCommand => new RelayCommand<string>(CopyLegio_Executed);

    public ICommand DeleteLegioCommand => new RelayCommand(execute: delegate { DeleteLegio_Executed(SearchLegioName); });

    public ICommand SaveLegioCommand => new RelayCommand(execute: delegate { var task = SaveLegio_Executed(SearchLegioName); });
    public ICommand RefreshLegioServerCommand => new RelayCommand(execute: delegate { RefreshLegioServer_Executed(SearchLegioName); });

    #endregion [Commands Legio]       

    #region [Methods Tbl30Legio]

    private async Task GetLegiosByNameOrId_Executed(string? parm)
    {
        IsLoading = true;
        LegioStartModify();
        Tbl27InfraclassesList?.Clear();
        Tbl30LegiosList?.Clear();
        Tbl33OrdosList?.Clear();
        Tbl90ReferenceExpertsList?.Clear();
        Tbl90ReferenceSourcesList?.Clear();
        Tbl90ReferenceAuthorsList?.Clear();
        Tbl93CommentsList?.Clear();

        InfraclassItems.Clear();
        LegioItems.Clear();
        OrdoItems.Clear();
        ReferenceAuthorItems.Clear();
        ReferenceSourceItems.Clear();
        ReferenceExpertItems.Clear();
        CommentItems.Clear();

        Tbl30LegiosList ??= new ObservableCollection<Tbl30Legio>();
        Tbl30LegiosList = await _dataService.GetTbl30LegiosCollectionOrderByLegioNameFromSearchNameOrId(parm!);

        if (Tbl30LegiosList is { Count: 0 })
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        LegioDataSetCount = Tbl30LegiosList.Count;
        RefreshLegioItems();

        SelectedMainDetailTabIndex = 1;
        IsLoading = false;
    }

    private async void AddLegio_Executed(string? parm)
    {
        LegioStartEdit();
        LegioStartNew();

        //Id search for first Dataset of Tbl27InfraclassesList
        var single = await _dataService.GetInfraclassSingleFirstDataset();
        var id = single.InfraclassId;

        Tbl30LegiosList ??= new ObservableCollection<Tbl30Legio>();
        Tbl30LegiosList.Insert(index: 0, item: new Tbl30Legio { LegioName = "New", InfraclassId = id });

        RefreshLegioItems();
    }

    private async void CopyLegio_Executed(string? parm)
    {
        LegioStartEdit();
        LegioStartNew();

        Tbl30LegiosList = await _dataService.CopyLegio(LegioSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshLegioItems();
    }

    private async void DeleteLegio_Executed(string? parm)
    {

        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(LegioSelected!.LegioName!))
        {
            //necessary to delete before
            await _dataService.DeleteConnectedOrdos(LegioSelected);
            await _dataService.DeleteConnectedLegioReferences(LegioSelected);
            await _dataService.DeleteConnectedLegioComments(LegioSelected);

            var ret = _dataService.DeleteLegio(LegioSelected);
            if (!await ret)
            {
                return;
            }

            Tbl30LegiosList = await _dataService.GetTbl30LegiosCollectionOrderByLegioNameFromSearchNameOrId(parm!);

            LegioDataSetCount = Tbl30LegiosList.Count;
            RefreshLegioItems();
        }
    }

    private async Task SaveLegio_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(LegioSelected?.LegioName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl30LegiosList != null)
        {

            var iNdx = Tbl30LegiosList.IndexOf(Tbl30LegiosList.First(t =>
                 t.LegioName == LegioSelected.LegioName));

            var ret = _dataService.SaveLegio(LegioSelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl30LegiosList = await _dataService.GetLastDatasetInTbl30Legios();
                RefreshLegioItems();
            }
            else
            {
                if (LegioSelected.LegioId == 0) //new
                {
                    Tbl30LegiosList = await _dataService.GetLastDatasetInTbl30Legios();
                    RefreshLegioItems();
                }
                else
                {
                    Tbl30LegiosList = await _dataService.GetTbl30LegiosCollectionOrderByLegioNameFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl30LegiosList!.Count)
                    {
                        LegioItems.Clear();
                        foreach (var item in Tbl30LegiosList)
                        {
                            LegioItems.Add(item);
                        }

                        LegioSelected = Tbl30LegiosList[iNdx];
                    }
                }
            }
        }
        LegioDataSetCount = Tbl30LegiosList!.Count;
        LegioCancelEditsAsync();
    }

    private async void RefreshLegioServer_Executed(string? parm)
    {
        Tbl30LegiosList = await _dataService.GetTbl30LegiosCollectionOrderByLegioNameFromSearchNameOrId(parm!);

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

    #endregion [Methods Tbl30Legio]    




    //    Part 2    


    #region "Public Commands Connect <== Tbl27Infraclass"                 

    public ICommand SaveInfraclassCommand => new RelayCommand<string>(SaveInfraclass_Executed);
    public ICommand RefreshInfraclassServerCommand => new RelayCommand(execute: delegate { RefreshInfraclassServer_Executed(SearchLegioName); });

    private async void SaveInfraclass_Executed(string? parm)
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
    public void InfraclassCancelEditsAsync()
    {
        IsInEdit = false;
        IsModified = false;
    }

    #endregion "Public Commands"                  



    //    Part 3    





    //    Part 4    


    #region [Public Commands Connect ==> Tbl33Ordo]       

    public ICommand AddOrdoCommand => new RelayCommand<string>(AddOrdo_Executed);
    public ICommand CopyOrdoCommand => new RelayCommand<string>(CopyOrdo_Executed);
    public ICommand DeleteOrdoCommand => new RelayCommand<string>(DeleteOrdo_Executed);
    public ICommand SaveOrdoCommand => new RelayCommand<string>(SaveOrdo_Executed);
    public ICommand RefreshOrdoServerCommand => new RelayCommand(execute: delegate { RefreshOrdoServer_Executed(OrdoSelected!.LegioId); });

    #endregion [Public Commands Connect ==> Tbl33Ordo]    

    #region [Public Methods Connect ==> Tbl33Ordo]                   

    private void AddOrdo_Executed(string? parm)
    {
        OrdoStartEdit();
        OrdoStartNew();
        Tbl33OrdosList.Insert(0, new Tbl33Ordo { OrdoName = "New", LegioId = LegioSelected.LegioId });

        OrdoItems.Clear();
        foreach (var item in Tbl33OrdosList)
        {
            OrdoItems.Add(item);
        }
        OrdoSelected = OrdoItems.First();
    }

    private async void CopyOrdo_Executed(string? parm)
    {
        OrdoStartEdit();
        OrdoStartNew();
        OrdoSelected.LegioId = LegioSelected.LegioId;  //combo vorbelegen

        Tbl33OrdosList = await _dataService.CopyOrdo(OrdoSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshOrdoItems();
    }

    private async void DeleteOrdo_Executed(string? parm)
    {
        if (OrdoSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.DeleteOrdo(OrdoSelected);
        if (!await ret)
        {
            return;
        }

        Tbl33OrdosList = _dataService.GetTbl33OrdosCollectionOrderByOrdoNameFromLegioId(OrdoSelected.LegioId);

        OrdoDataSetCount = Tbl33OrdosList.Count;
        RefreshOrdoItems();
    }

    private async void SaveOrdo_Executed(string? parm)
    {
        if (OrdoSelected != null && string.IsNullOrEmpty(OrdoSelected.OrdoName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl33OrdosList != null)
        {
            var indx = Tbl33OrdosList.IndexOf(Tbl33OrdosList.First(t =>
              OrdoSelected != null && t.OrdoName == OrdoSelected.OrdoName));

            if (OrdoSelected != null)
            {
                if (LegioSelected != null)
                {
                    OrdoSelected.LegioId = LegioSelected.LegioId;

                    var ret = _dataService.SaveOrdo(OrdoSelected);
                    if (!await ret)
                    {
                        return;
                    }

                    if (OrdoSelected.OrdoId == 0) //new
                    {
                        Tbl33OrdosList = await _dataService.GetLastDatasetInTbl33Ordos();
                        RefreshOrdoItems();
                    }
                    else
                    {
                        Tbl33OrdosList = _dataService.GetTbl33OrdosCollectionOrderByOrdoNameFromLegioId(LegioSelected.LegioId);
                        //   Index Position ?
                        if (indx < Tbl33OrdosList.Count)
                        {
                            OrdoItems.Clear();
                            foreach (var item in Tbl33OrdosList)
                            {
                                OrdoItems.Add(item);
                            }

                            OrdoSelected = Tbl33OrdosList[indx];  //Index
                        }
                    }
                }
            }
        }
        OrdoDataSetCount = Tbl33OrdosList!.Count;
        OrdoCancelEditsAsync();
    }

    private void RefreshOrdoServer_Executed(int id)
    {
        Tbl33OrdosList ??= new ObservableCollection<Tbl33Ordo>();
        Tbl33OrdosList = _dataService.GetTbl33OrdosCollectionOrderByOrdoNameFromLegioId(id);

        OrdoDataSetCount = Tbl33OrdosList.Count;

        RefreshOrdoItems();
    }
    public void OrdoStartEdit() => IsInEdit = true;
    public void OrdoStartModify() => IsModified = true;
    public void OrdoStartNew() => IsNewOrdo = true;
    public event EventHandler AddNewOrdoCanceled = null!;
    public void OrdoCancelEditsAsync()
    {
        if (IsNewOrdo)
        {
            IsInEdit = false;
            AddNewOrdoCanceled?.Invoke(this, EventArgs.Empty);
            IsNewOrdo = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }
    #endregion [Public Methods  Connect ==> Tbl33Ordo]                                                                                                                                            



    //    Part 5    




    //    Part 6    




    //    Part 7    



    //    Part 8    


    #region [Commands Legio ==> Tbl90Reference Author]
    public ICommand AddReferenceAuthorCommand => new RelayCommand<string>(AddReferenceAuthor_Executed);
    public ICommand CopyReferenceAuthorCommand => new RelayCommand<string>(CopyReferenceAuthor_Executed);
    public ICommand DeleteReferenceAuthorCommand => new RelayCommand<string>(DeleteReferenceAuthor_Executed);
    public ICommand SaveReferenceAuthorCommand => new RelayCommand<string>(SaveReferenceAuthor_Executed);
    public ICommand RefreshReferenceAuthorServerCommand => new RelayCommand<string>(RefreshReferenceAuthorServer_Executed);
    #endregion [Commands Legio ==> Tbl90Reference Author]                

    #region [Methods Legio ==> Tbl90Reference Author]

    private void AddReferenceAuthor_Executed(string? parm)
    {
        ReferenceAuthorStartEdit();
        ReferenceAuthorStartNew();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

        Tbl90ReferenceAuthorsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", LegioId = LegioSelected!.LegioId });

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
        ReferenceAuthorSelected.LegioId = LegioSelected.LegioId; //combo vorbelegen

        Tbl90ReferenceAuthorsList = await _dataService.CopyReferenceLegio(ReferenceAuthorSelected, "Author");

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

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromLegioIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.LegioId);
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

            if (LegioSelected != null)
            {
                ReferenceAuthorSelected.LegioId = LegioSelected.LegioId;
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
                Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromLegioIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.LegioId);
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

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromLegioIdAndRefSourceIdIsNullAndRefExpertIdIsNull(LegioSelected.LegioId);

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
    #endregion [Methods Legio ==> Tbl90Reference Author]                   

    #region [Commands Legio ==> Tbl90Reference Source]  
    public ICommand AddReferenceSourceCommand => new RelayCommand<string>(AddReferenceSource_Executed);
    public ICommand CopyReferenceSourceCommand => new RelayCommand<string>(CopyReferenceSource_Executed);
    public ICommand DeleteReferenceSourceCommand => new RelayCommand<string>(DeleteReferenceSource_Executed);
    public ICommand SaveReferenceSourceCommand => new RelayCommand<string>(SaveReferenceSource_Executed);
    public ICommand RefreshReferenceSourceServerCommand => new RelayCommand<string>(RefreshReferenceSourceServer_Executed);
    #endregion [Commands Legio ==> Tbl90Reference Source]         

    #region [Methods Legio ==> Tbl90Reference Source]      

    private void AddReferenceSource_Executed(string? parm)
    {
        ReferenceSourceStartEdit();
        ReferenceSourceStartNew();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

        Tbl90ReferenceSourcesList.Insert(index: 0, item: new Tbl90Reference { Info = "New", LegioId = LegioSelected!.LegioId });
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
        if (LegioSelected != null)
        {
            ReferenceSourceSelected.LegioId = LegioSelected.LegioId; //combo vorbelegen
        }
        Tbl90ReferenceSourcesList = await _dataService.CopyReferenceLegio(ReferenceSourceSelected, "Source");
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

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromLegioIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.LegioId);
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

            if (LegioSelected != null)
            {
                ReferenceSourceSelected.LegioId = LegioSelected.LegioId;
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
                Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromLegioIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.LegioId);
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

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromLegioIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(LegioSelected.LegioId);

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
    #endregion [Methods Legio ==> Tbl90Reference Source]           

    #region [Commands Legio ==> Tbl90Reference Expert]       
    public ICommand AddReferenceExpertCommand => new RelayCommand<string>(AddReferenceExpert_Executed);
    public ICommand CopyReferenceExpertCommand => new RelayCommand<string>(CopyReferenceExpert_Executed);
    public ICommand DeleteReferenceExpertCommand => new RelayCommand<string>(DeleteReferenceExpert_Executed);
    public ICommand SaveReferenceExpertCommand => new RelayCommand<string>(SaveReferenceExpert_Executed);
    public ICommand RefreshReferenceExpertServerCommand => new RelayCommand<string>(RefreshReferenceExpertServer_Executed);
    #endregion [Commands Legio ==> Tbl90Reference Expert]                    

    #region [Methods Legio ==> Tbl90Reference Expert]                 

    private void AddReferenceExpert_Executed(string? parm)
    {
        ReferenceExpertStartEdit();
        ReferenceExpertStartNew();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

        Tbl90ReferenceExpertsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", LegioId = LegioSelected!.LegioId });
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
        if (LegioSelected != null)
        {
            ReferenceExpertSelected.LegioId = LegioSelected.LegioId; //combo vorbelegen
        }
        Tbl90ReferenceExpertsList = await _dataService.CopyReferenceLegio(ReferenceExpertSelected, "Expert");
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

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromLegioIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.LegioId);
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
            if (LegioSelected != null)
            {
                ReferenceExpertSelected.LegioId = LegioSelected.LegioId;
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
                Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromLegioIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.LegioId);
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

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromLegioIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(LegioSelected.LegioId);

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
    #endregion [Methods Legio ==> Tbl90Reference Expert]                                 

    #region [Commands Legio ==> Tbl93Comments]         
    public ICommand AddCommentCommand => new RelayCommand<string>(AddComment_Executed);
    public ICommand CopyCommentCommand => new RelayCommand<string>(CopyComment_Executed);
    public ICommand DeleteCommentCommand => new RelayCommand<string>(DeleteComment_Executed);
    public ICommand SaveCommentCommand => new RelayCommand<string>(SaveComment_Executed);
    public ICommand RefreshCommentServerCommand => new RelayCommand<string>(RefreshCommentServer_Executed);
    #endregion [Commands Legio ==> Tbl93Comments]           


    #region [Methods Legio ==> Tbl93Comments]        

    private void AddComment_Executed(string? parm)
    {
        CommentStartEdit();
        CommentStartNew();
        Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = "New", LegioId = LegioSelected!.LegioId });

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
        if (LegioSelected != null)
        {
            CommentSelected.LegioId = LegioSelected.LegioId;  //combo vorbelegen
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

        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromLegioId(CommentSelected.LegioId);
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
                if (LegioSelected != null)
                {
                    CommentSelected.LegioId = LegioSelected.LegioId;

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
                        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromLegioId(LegioSelected.LegioId);
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
        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromLegioId(LegioSelected.LegioId);

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
    #endregion [Methods Legio ==> Tbl93Comments]                             


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
                if (LegioSelected != null)
                {
                    IsLoading = true;
                    InfraclassStartModify();
                    Tbl24SubclassesAllList = _dataService.GetTbl24SubclassesCollectionOrderBySubclassName();

                    Tbl27InfraclassesList = _dataService.GetTbl27InfraclassesCollectionOrderByInfraclassNameFromInfraclassId(LegioSelected.InfraclassId);

                    InfraclassDataSetCount = Tbl27InfraclassesList.Count;
                    RefreshInfraclassItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 1)
            {
            }

            if (_selectedMainDetailTabIndex == 2)
            {
                if (LegioSelected != null)
                {
                    IsLoading = true;
                    OrdoStartModify();
                    Tbl33OrdosList = _dataService.GetTbl33OrdosCollectionOrderByOrdoNameFromLegioId(LegioSelected.LegioId);

                    OrdoDataSetCount = Tbl33OrdosList.Count;
                    RefreshOrdoItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 3)
            {
                if (LegioSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromLegioIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(LegioSelected.LegioId);

                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 4)
            {
                if (LegioSelected != null)
                {
                    IsLoading = true;
                    CommentStartModify();
                    Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromLegioId(LegioSelected.LegioId);

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
                if (LegioSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromLegioIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(LegioSelected.LegioId);
                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 1)
            {
                if (LegioSelected != null)
                {
                    IsLoading = true;
                    ReferenceSourceStartModify();
                    Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

                    Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromLegioIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(LegioSelected.LegioId);

                    ReferenceSourceDataSetCount = Tbl90ReferenceSourcesList.Count;
                    RefreshReferenceSourceItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 2)
            {
                if (LegioSelected != null)
                {
                    IsLoading = true;
                    ReferenceAuthorStartModify();
                    Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

                    Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromLegioIdAndRefSourceIdIsNullAndRefExpertIdIsNull(LegioSelected.LegioId);

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

    private Tbl27Infraclass _infraclassSelected = null!;
    public Tbl27Infraclass InfraclassSelected
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

    private Tbl33Ordo _ordoSelected = null!;
    public Tbl33Ordo OrdoSelected
    {
        get => _ordoSelected;
        set => SetProperty(ref _ordoSelected, value);
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
    public string SearchLegioName { get; set; } = null!;

    private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesList = null!;
    public ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesList
    {
        get => _tbl27InfraclassesList;
        set
        {
            _tbl27InfraclassesList = value; OnPropertyChanged();
        }
    }
    private ObservableCollection<Tbl27Infraclass>? _tbl27InfraclassesAllList;
    public ObservableCollection<Tbl27Infraclass>? Tbl27InfraclassesAllList
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
    private ObservableCollection<Tbl30Legio> _tbl30LegiosAllList = null!;
    public ObservableCollection<Tbl30Legio> Tbl30LegiosAllList
    {
        get => _tbl30LegiosAllList;
        set
        {
            _tbl30LegiosAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl33Ordo> _tbl33OrdosList = null!;
    public ObservableCollection<Tbl33Ordo> Tbl33OrdosList
    {
        get => _tbl33OrdosList;
        set
        {
            _tbl33OrdosList = value; OnPropertyChanged();
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

    private int _ordoDataSetCount;
    public int OrdoDataSetCount
    {
        get => _ordoDataSetCount;
        set
        {
            _ordoDataSetCount = value; OnPropertyChanged();
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

    private bool _isNewLegio;
    public bool IsNewLegio
    {
        get => _isNewLegio;
        set => SetProperty(ref _isNewLegio, value);
    }
    private bool _isNewOrdo;
    public bool IsNewOrdo
    {
        get => _isNewOrdo;
        set => SetProperty(ref _isNewOrdo, value);
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

    private void RefreshOrdoItems()
    {
        OrdoItems.Clear();
        if (Tbl33OrdosList != null)
        {
            foreach (var item in Tbl33OrdosList)
            {
                OrdoItems.Add(item);
            }
            if (Tbl33OrdosList.Count == 0)
            {
                return;
            }

            if (OrdoSelected == null && Tbl33OrdosList.Count != 0)
            {
                OrdoSelected = OrdoItems.FirstOrDefault()!;
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
