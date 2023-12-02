using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using CommunityToolkit.Mvvm.ComponentModel;

//    Tbl03RegnumsViewModel Skriptdatum:  26.03.2023  12:32      

namespace ATIS.WinUi.ViewModels.Database;

public class Tbl03RegnumsViewModel : ObservableObject
{
    #region [Private Data Members]
    private readonly IDataService _dataService;
    public ObservableCollection<Tbl03Regnum?> RegnumItems { get; } = new();
    public ObservableCollection<Tbl06Phylum> PhylumItems { get; } = new();
    public ObservableCollection<Tbl09Division> DivisionItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceExpertItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceSourceItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceAuthorItems { get; } = new();
    public ObservableCollection<Tbl93Comment> CommentItems { get; } = new();
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]

    #region [Constructor]
    public Tbl03RegnumsViewModel(IDataService dataService) 
    {
        _dataService = dataService;
        SelectedMainDetailTabIndex = 0; //Tab Datagrid
        GetAllCollections();
    }

    private  void GetAllCollections()
    {
        Tbl03RegnumsAllList ??= new ObservableCollection<Tbl03Regnum>();
        Tbl03RegnumsAllList = _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnum();

        Tbl90ExpertsAllList ??= new ObservableCollection<Tbl90RefExpert>();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();
        Tbl90SourcesAllList ??= new ObservableCollection<Tbl90RefSource>();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();
        Tbl90AuthorsAllList ??= new ObservableCollection<Tbl90RefAuthor>();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();
    }

    #endregion [Constructor]

    //    Part 1    

    #region [Commands Regnum]
    public ICommand GetRegnumsByNameOrIdCommand => new RelayCommand(execute: delegate 
        { var task = GetRegnumsByNameOrId_Executed(SearchRegnumName); });
    public ICommand AddRegnumCommand => new RelayCommand<string>(AddRegnum_Executed);
    public ICommand CopyRegnumCommand => new RelayCommand<string>(CopyRegnum_Executed);
    public ICommand DeleteRegnumCommand => new RelayCommand(execute: delegate { DeleteRegnum_Executed(SearchRegnumName); });
    public ICommand SaveRegnumCommand => new RelayCommand(execute: delegate { var task = SaveRegnum_Executed(SearchRegnumName); });
    public ICommand RefreshRegnumServerCommand => new RelayCommand(execute: delegate { RefreshRegnumServer_Executed(SearchRegnumName); });
    #endregion [Commands Regnum]

    #region [Methods Regnum]
    private async Task GetRegnumsByNameOrId_Executed(string? parm)
    {
        IsLoading = true;
        RegnumStartModify();
        Tbl03RegnumsList?.Clear();
        Tbl06PhylumsList?.Clear();
        Tbl09DivisionsList?.Clear();
        Tbl90ReferenceExpertsList?.Clear();
        Tbl90ReferenceSourcesList?.Clear();
        Tbl90ReferenceAuthorsList?.Clear();
        Tbl93CommentsList?.Clear();

        RegnumItems.Clear();
        PhylumItems.Clear();
        DivisionItems.Clear();
        ReferenceAuthorItems.Clear();
        ReferenceSourceItems.Clear();
        ReferenceExpertItems.Clear();
        CommentItems.Clear();
        Tbl03RegnumsList ??= new ObservableCollection<Tbl03Regnum>();
        Tbl03RegnumsList = await _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnumFromSearchNameOrId(parm);
        if (Tbl03RegnumsList is { Count: 0 })
        {
           await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        RegnumDataSetCount = Tbl03RegnumsList!.Count;
        RefreshRegnumItems();
        SelectedMainDetailTabIndex = 0;
        IsLoading = false;
    }
    private void AddRegnum_Executed(string? parm)
    {
        RegnumStartEdit();
        RegnumStartNew();
        Tbl03RegnumsList ??= new ObservableCollection<Tbl03Regnum>();
        Tbl03RegnumsList.Insert(index: 0, item: new Tbl03Regnum { RegnumName = "New" });

        RegnumItems.Clear();
        foreach (var item in Tbl03RegnumsList)
        {
            RegnumItems.Add(item);
        }
        RegnumSelected = RegnumItems.First()!;
    }
    private async void CopyRegnum_Executed(string? parm)
    {
        RegnumStartEdit();
        RegnumStartNew();
        Tbl03RegnumsList ??= new ObservableCollection<Tbl03Regnum>();
        if (RegnumSelected != null)
        {
            Tbl03RegnumsList = await _dataService.CopyRegnum(RegnumSelected);
        }
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshRegnumItems();
    }
    private async void DeleteRegnum_Executed(string? parm)
    {
        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(RegnumSelected!.RegnumName!))
        {
            //necessary to delete before
            await _dataService.DeleteConnectedPhylums(RegnumSelected);
            await _dataService.DeleteConnectedDivisions(RegnumSelected);
            await _dataService.DeleteConnectedRegnumReferences(RegnumSelected);
            await _dataService.DeleteConnectedRegnumComments(RegnumSelected);

            var ret = _dataService.DeleteRegnum(RegnumSelected);
            if (!await ret)
            {
                return;
            }

            Tbl03RegnumsList =  await _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnumFromSearchNameOrId(parm);

            RefreshRegnumItems();
        }
    }
    private async Task SaveRegnum_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(RegnumSelected?.RegnumName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl03RegnumsList != null)
        {
            var iNdx = Tbl03RegnumsList.IndexOf(Tbl03RegnumsList.First(t =>
                t.RegnumName == RegnumSelected.RegnumName && t.Subregnum == RegnumSelected.Subregnum));

            var ret = _dataService.SaveRegnum(RegnumSelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl03RegnumsList = await _dataService.GetLastDatasetInTbl03Regnums();
                RefreshRegnumItems();
            }
            else
            {
                if (RegnumSelected.RegnumId == 0) //new
                {
                    Tbl03RegnumsList = await _dataService.GetLastDatasetInTbl03Regnums();
                    RefreshRegnumItems();
                }
                else
                {
                    Tbl03RegnumsList =  await _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnumFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl03RegnumsList!.Count)
                    {
                        RegnumItems.Clear();
                        foreach (var item in Tbl03RegnumsList)
                        {
                            RegnumItems.Add(item);
                        }

                        RegnumSelected = Tbl03RegnumsList[iNdx];
                    }
                }
            }
        }
        RegnumDataSetCount = Tbl03RegnumsList!.Count;
        RefreshRegnumItems();
        RegnumCancelEditsAsync();
    }
    private async void RefreshRegnumServer_Executed(string? parm)
    {
        Tbl03RegnumsList ??= new ObservableCollection<Tbl03Regnum>();
        Tbl03RegnumsList = await _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnumFromSearchNameOrId(parm);

        RegnumDataSetCount = Tbl03RegnumsList.Count;
        RefreshRegnumItems();
    }
    public void RegnumStartEdit() => IsInEdit = true;
    public void RegnumStartModify() => IsModified = true;
    public void RegnumStartNew() => IsNewRegnum = true;
    public event EventHandler? NewRegnumCanceled;
    public void RegnumCancelEditsAsync()
    {
        if (IsNewRegnum)
        {
            NewRegnumCanceled?.Invoke(this, EventArgs.Empty);
            IsInEdit = false;
            IsNewRegnum = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }

    #endregion [Methods Regnum]

    //    Part 2    

    //    Part 3    

    //    Part 4    

    #region [Public Commands Connect ==> Tbl06Phylum]
    public ICommand AddPhylumCommand => new RelayCommand<string>(AddPhylum_Executed);
    public ICommand CopyPhylumCommand => new RelayCommand<string>(CopyPhylum_Executed);
    public ICommand DeletePhylumCommand => new RelayCommand<string>(DeletePhylum_Executed);
    public ICommand SavePhylumCommand => new RelayCommand<string>(SavePhylum_Executed);
    public ICommand RefreshPhylumServerCommand => new RelayCommand(execute: delegate { RefreshPhylumServer_Executed(PhylumSelected!.RegnumId); });
    #endregion [Public Commands Connect ==> Tbl06Phylum]    

    #region [Public Methods Connect ==> Tbl06Phylum]                   
    private async void AddPhylum_Executed(string? parm)
    {
        PhylumStartEdit();
        PhylumStartNew();

        //Id search for first Dataset of Tbl03RegnumsList
        var single = await _dataService.GetRegnumSingleFirstDataset();
        int id;
        if (true)
        {
            id = single.RegnumId;
        }

        Tbl06PhylumsList ??= new ObservableCollection<Tbl06Phylum>();
        Tbl06PhylumsList.Insert(index: 0, item: new Tbl06Phylum { PhylumName = "New", RegnumId = id });

        PhylumItems.Clear();
        foreach (var item in Tbl06PhylumsList)
        {
            PhylumItems.Add(item);
        }
        PhylumSelected = PhylumItems.First();
    }
    private async void CopyPhylum_Executed(string? parm)
    {
        PhylumStartEdit();
        PhylumStartNew();
        Tbl06PhylumsList ??= new ObservableCollection<Tbl06Phylum>();
        if (PhylumSelected != null)
        {
            if (RegnumSelected != null)
            {
                PhylumSelected.RegnumId = RegnumSelected.RegnumId; //combo vorbelegen
            }

            Tbl06PhylumsList = await _dataService.CopyPhylum(PhylumSelected);
        }

        RefreshPhylumItems();
    }
    private async void DeletePhylum_Executed(string? parm)
    {
        if (PhylumSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.DeletePhylum(PhylumSelected);
        if (!await ret)
        {
            return;
        }

        Tbl06PhylumsList = _dataService.GetTbl06PhylumsCollectionOrderByPhylumNameFromRegnumId(PhylumSelected.RegnumId);
        PhylumDataSetCount = Tbl06PhylumsList.Count;
        RefreshPhylumItems();
    }
    private async void SavePhylum_Executed(string? parm)
    {
        if (PhylumSelected != null && string.IsNullOrEmpty(PhylumSelected.PhylumName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }

        if (Tbl06PhylumsList != null)
        {
            var indx = Tbl06PhylumsList.IndexOf(Tbl06PhylumsList.First(t =>
                PhylumSelected != null && t.PhylumName == PhylumSelected.PhylumName));

            if (PhylumSelected != null)
            {
                if (RegnumSelected != null)
                {
                    PhylumSelected.RegnumId = RegnumSelected.RegnumId;

                    var ret = _dataService.SavePhylum(PhylumSelected);
                    if (!await ret)
                    {
                        return;
                    }

                    if (PhylumSelected.PhylumId == 0) //new
                    {
                        Tbl06PhylumsList = await _dataService.GetLastDatasetInTbl06Phylums();
                        RefreshPhylumItems();
                    }
                    else
                    {
                        Tbl06PhylumsList = _dataService.GetTbl06PhylumsCollectionOrderByPhylumNameFromRegnumId(RegnumSelected.RegnumId);
                        //   Index Position ?
                        if (indx < Tbl06PhylumsList.Count)
                        {
                            PhylumItems.Clear();
                            foreach (var item in Tbl06PhylumsList)
                            {
                                PhylumItems.Add(item);
                            }

                            PhylumSelected = Tbl06PhylumsList[indx]; //Index
                        }
                    }
                }
            }
        }
        PhylumDataSetCount = Tbl06PhylumsList!.Count;
        PhylumCancelEditsAsync();
    }
    private void RefreshPhylumServer_Executed(int id)
    {
        Tbl06PhylumsList ??= new ObservableCollection<Tbl06Phylum>();
        Tbl06PhylumsList = _dataService.GetTbl06PhylumsCollectionOrderByPhylumNameFromRegnumId(id);

        PhylumDataSetCount = Tbl06PhylumsList.Count;

        RefreshPhylumItems();
    }
    public void PhylumStartEdit() => IsInEdit = true;
    public void PhylumStartModify() => IsModified = true;
    public void PhylumStartNew() => IsNewPhylum = true;
    public event EventHandler AddNewPhylumCanceled = null!;
    public void PhylumCancelEditsAsync()
    {
        if (IsNewPhylum)
        {
            IsInEdit = false;
            AddNewPhylumCanceled?.Invoke(this, EventArgs.Empty);
            IsNewPhylum = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }
    #endregion [Public Methods Connect ==> Tbl06Phylum]                   

    //    Part 5    

    #region [Public Commands Connect ==> Tbl09Division]
    public ICommand AddDivisionCommand => new RelayCommand<string>(AddDivision_Executed);
    public ICommand CopyDivisionCommand => new RelayCommand<string>(CopyDivision_Executed);
    public ICommand DeleteDivisionCommand => new RelayCommand<string>(DeleteDivision_Executed);
    public ICommand SaveDivisionCommand => new RelayCommand<string>(SaveDivision_Executed);
    public ICommand RefreshDivisionServerCommand => new RelayCommand(execute: delegate { RefreshDivisionServer_Executed(DivisionSelected!.RegnumId); });
    #endregion [Public Commands Connect ==> Tbl09Division]    

    #region [Public Methods Connect ==> Tbl09Division]                   
    private async void AddDivision_Executed(string? obj)
    {
        DivisionStartEdit();
        DivisionStartNew();

        //Id search for first Dataset of Tbl03RegnumsList
        var single = await _dataService.GetRegnumSingleFirstDataset();
        int id;
        if (true)
        {
            id = single.RegnumId;
        }

        Tbl09DivisionsList ??= new ObservableCollection<Tbl09Division>();
        Tbl09DivisionsList.Insert(index: 0, item: new Tbl09Division { DivisionName = "New", RegnumId = id });

        DivisionItems.Clear();
        foreach (var item in Tbl09DivisionsList)
        {
            DivisionItems.Add(item);
        }
        DivisionSelected = DivisionItems.First();
    }
    private async void CopyDivision_Executed(string? obj)
    {
        DivisionStartEdit();
        DivisionStartNew();
        Tbl09DivisionsList ??= new ObservableCollection<Tbl09Division>();
        if (DivisionSelected != null)
        {
            if (RegnumSelected != null)
            {
                DivisionSelected.RegnumId = RegnumSelected.RegnumId; //combo vorbelegen
            }

            Tbl09DivisionsList = await _dataService.CopyDivision(DivisionSelected);
        }

        RefreshDivisionItems();
    }
    private async void DeleteDivision_Executed(string? obj)
    {
        if (DivisionSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.DeleteDivision(DivisionSelected);
        if (!await ret)
        {
            return;
        }

        Tbl09DivisionsList = _dataService.GetTbl09DivisionsCollectionOrderByDivisionNameFromRegnumId(DivisionSelected.RegnumId);
        RefreshDivisionItems();
    }
    private async void SaveDivision_Executed(string? obj)
    {
        if (DivisionSelected != null && string.IsNullOrEmpty(DivisionSelected.DivisionName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }

        if (Tbl09DivisionsList != null)
        {
            var indx = Tbl09DivisionsList.IndexOf(Tbl09DivisionsList.First(t =>
                DivisionSelected != null && t.DivisionName == DivisionSelected.DivisionName));

            if (DivisionSelected != null)
            {
                if (RegnumSelected != null)
                {
                    DivisionSelected.RegnumId = RegnumSelected.RegnumId;

                    var ret = _dataService.SaveDivision(DivisionSelected);
                    if (!await ret)
                    {
                        return;
                    }

                    if (DivisionSelected.DivisionId == 0) //new
                    {
                        Tbl09DivisionsList = await _dataService.GetLastDatasetInTbl09Divisions();
                        RefreshDivisionItems();
                    }
                    else
                    {
                        Tbl09DivisionsList = _dataService.GetTbl09DivisionsCollectionOrderByDivisionNameFromRegnumId(RegnumSelected.RegnumId);
                        //   Index Position ?
                        if (indx < Tbl09DivisionsList.Count)
                        {
                            DivisionItems.Clear();
                            foreach (var item in Tbl09DivisionsList)
                            {
                                DivisionItems.Add(item);
                            }

                            DivisionSelected = Tbl09DivisionsList[indx]; //Index
                        }
                    }
                }
            }
        }
        DivisionCancelEditsAsync();
    }
    private void RefreshDivisionServer_Executed(int Id)
    {
        Tbl09DivisionsList ??= new ObservableCollection<Tbl09Division>();
        Tbl09DivisionsList = _dataService.GetTbl09DivisionsCollectionOrderByDivisionNameFromRegnumId(Id);

        DivisionDataSetCount = Tbl09DivisionsList.Count;

        RefreshDivisionItems();
    }
    public void DivisionStartEdit() => IsInEdit = true;
    public void DivisionStartModify() => IsModified = true;
    public void DivisionStartNew() => IsNewDivision = true;
    public event EventHandler AddNewDivisionCanceled = null!;
    public void DivisionCancelEditsAsync()
    {
        if (IsNewDivision)
        {
            IsInEdit = false;
            AddNewDivisionCanceled?.Invoke(this, EventArgs.Empty);
            IsNewDivision = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }
    #endregion [Public Methods Connect ==> Tbl09Division]                   

    //    Part 6    

    //    Part 7    

    //    Part 8    

    #region [Commands Regnum ==> Tbl90Reference Author]
    public ICommand AddReferenceAuthorCommand => new RelayCommand<string>(AddReferenceAuthor_Executed);
    public ICommand CopyReferenceAuthorCommand => new RelayCommand<string>(CopyReferenceAuthor_Executed);
    public ICommand DeleteReferenceAuthorCommand => new RelayCommand<string>(DeleteReferenceAuthor_Executed);
    public ICommand SaveReferenceAuthorCommand => new RelayCommand<string>(SaveReferenceAuthor_Executed);
    public ICommand RefreshReferenceAuthorServerCommand => new RelayCommand<string>(RefreshReferenceAuthorServer_Executed);
    #endregion [Commands Regnum ==> Tbl90Reference Author]

    #region [Methods Regnum ==> Tbl90Reference Author]
    private void AddReferenceAuthor_Executed(string? parm)
    {
        ReferenceAuthorStartEdit();
        ReferenceAuthorStartNew();
        Tbl90AuthorsAllList ??= new ObservableCollection<Tbl90RefAuthor>();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

        Tbl90ReferenceAuthorsList ??= new ObservableCollection<Tbl90Reference>();
        Tbl90ReferenceAuthorsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", RegnumId = RegnumSelected!.RegnumId });
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
        Tbl90ReferenceAuthorsList ??= new ObservableCollection<Tbl90Reference>();
        if (ReferenceAuthorSelected != null)
        {
            if (RegnumSelected != null)
            {
                ReferenceAuthorSelected.RegnumId = RegnumSelected.RegnumId; //combo vorbelegen
            }

            Tbl90ReferenceAuthorsList = await _dataService.CopyReferenceRegnum(ReferenceAuthorSelected, "Author");
        }

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

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromRegnumIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.RegnumId);
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

            if (RegnumSelected != null)
            {
                ReferenceAuthorSelected.RegnumId = RegnumSelected.RegnumId;
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
                Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromRegnumIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.RegnumId);
                //   Index Position ?
                if (indx < Tbl90ReferenceAuthorsList.Count)
                {
                    ReferenceAuthorItems.Clear();
                    foreach (var item in Tbl90ReferenceAuthorsList)
                    {
                        ReferenceAuthorItems.Add(item);
                    }

                    ReferenceAuthorSelected = Tbl90ReferenceAuthorsList[indx]; //Index
                }
            }
        }
        ReferenceAuthorCancelEditsAsync();
    }
    private void RefreshReferenceAuthorServer_Executed(string? parm)
    {
        Tbl90AuthorsAllList ??= new ObservableCollection<Tbl90RefAuthor>();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

        Tbl90ReferenceAuthorsList ??= new ObservableCollection<Tbl90Reference>();
        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromRegnumIdAndRefSourceIdIsNullAndRefExpertIdIsNull(RegnumSelected!.RegnumId);

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
    #endregion [Methods Regnum ==> Tbl90Reference Author]

    #region [Commands Regnum ==> Tbl90Reference Source]      
    public ICommand AddReferenceSourceCommand => new RelayCommand<string>(AddReferenceSource_Executed);
    public ICommand CopyReferenceSourceCommand => new RelayCommand<string>(CopyReferenceSource_Executed);
    public ICommand DeleteReferenceSourceCommand => new RelayCommand<string>(DeleteReferenceSource_Executed);
    public ICommand SaveReferenceSourceCommand => new RelayCommand<string>(SaveReferenceSource_Executed);
    public ICommand RefreshReferenceSourceServerCommand => new RelayCommand<string>(RefreshReferenceSourceServer_Executed);
    #endregion [Commands Regnum ==> Tbl90Reference Source]      

    #region [Methods Regnum ==> Tbl90Reference Source]      
    private void AddReferenceSource_Executed(string? parm)
    {
        ReferenceSourceStartEdit();
        ReferenceSourceStartNew();
        Tbl90SourcesAllList ??= new ObservableCollection<Tbl90RefSource>();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

        Tbl90ReferenceSourcesList ??= new ObservableCollection<Tbl90Reference>();
        Tbl90ReferenceSourcesList.Insert(index: 0, item: new Tbl90Reference { Info = "New", RegnumId = RegnumSelected!.RegnumId });
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
        Tbl90ReferenceSourcesList ??= new ObservableCollection<Tbl90Reference>();
        if (ReferenceSourceSelected != null)
        {
            if (RegnumSelected != null)
            {
                ReferenceSourceSelected.RegnumId = RegnumSelected.RegnumId; //combo vorbelegen
            }

            Tbl90ReferenceSourcesList = await _dataService.CopyReferenceRegnum(ReferenceSourceSelected, "Source");
        }

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

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromRegnumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.RegnumId);
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

            if (RegnumSelected != null)
            {
                ReferenceSourceSelected.RegnumId = RegnumSelected.RegnumId;
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
                Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromRegnumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.RegnumId);
                //   Index Position ?
                if (indx < Tbl90ReferenceSourcesList.Count)
                {
                    ReferenceSourceItems.Clear();
                    foreach (var item in Tbl90ReferenceSourcesList)
                    {
                        ReferenceSourceItems.Add(item);
                    }

                    ReferenceSourceSelected = Tbl90ReferenceSourcesList[indx]; //Index
                }
            }
        }
        ReferenceSourceCancelEditsAsync();
    }
    private void RefreshReferenceSourceServer_Executed(string? parm)
    {
        Tbl90SourcesAllList ??= new ObservableCollection<Tbl90RefSource>();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

        Tbl90ReferenceSourcesList ??= new ObservableCollection<Tbl90Reference>();
        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromRegnumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(RegnumSelected!.RegnumId);

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
    #endregion [Methods Regnum ==> Tbl90Reference Source]      

    #region [Commands Regnum ==> Tbl90Reference Expert]                 
    public ICommand AddReferenceExpertCommand => new RelayCommand<string>(AddReferenceExpert_Executed);
    public ICommand CopyReferenceExpertCommand => new RelayCommand<string>(CopyReferenceExpert_Executed);
    public ICommand DeleteReferenceExpertCommand => new RelayCommand<string>(DeleteReferenceExpert_Executed);
    public ICommand SaveReferenceExpertCommand => new RelayCommand<string>(SaveReferenceExpert_Executed);
    public ICommand RefreshReferenceExpertServerCommand => new RelayCommand<string>(RefreshReferenceExpertServer_Executed);
    #endregion [Commands Regnum ==> Tbl90Reference Expert]                 

    #region [Methods Regnum ==> Tbl90Reference Expert]   
    private void AddReferenceExpert_Executed(string? parm)
    {
        ReferenceExpertStartEdit();
        ReferenceExpertStartNew();
        Tbl90ExpertsAllList ??= new ObservableCollection<Tbl90RefExpert>();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

        Tbl90ReferenceExpertsList ??= new ObservableCollection<Tbl90Reference>();
        Tbl90ReferenceExpertsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", RegnumId = RegnumSelected!.RegnumId });
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
        Tbl90ReferenceExpertsList ??= new ObservableCollection<Tbl90Reference>();
        if (ReferenceExpertSelected != null)
        {
            if (RegnumSelected != null)
            {
                ReferenceExpertSelected.RegnumId = RegnumSelected.RegnumId; //combo vorbelegen
            }

            Tbl90ReferenceExpertsList = await _dataService.CopyReferenceRegnum(ReferenceExpertSelected, "Expert");
        }

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

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.RegnumId);
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

            if (RegnumSelected != null)
            {
                ReferenceExpertSelected.RegnumId = RegnumSelected.RegnumId;
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
                Tbl90ReferenceExpertsList =
                    _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.RegnumId);
                //   Index Position ?
                if (indx < Tbl90ReferenceExpertsList.Count)
                {
                    ReferenceExpertItems.Clear();
                    foreach (var item in Tbl90ReferenceExpertsList)
                    {
                        ReferenceExpertItems.Add(item);
                    }

                    ReferenceExpertSelected = Tbl90ReferenceExpertsList[indx]; //Index
                }
            }
        }
        ReferenceExpertCancelEditsAsync();
    }
    private void RefreshReferenceExpertServer_Executed(string? parm)
    {
        Tbl90ExpertsAllList ??= new ObservableCollection<Tbl90RefExpert>();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

        Tbl90ReferenceExpertsList ??= new ObservableCollection<Tbl90Reference>();
        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(RegnumSelected!.RegnumId);

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
    #endregion [Methods Regnum ==> Tbl90Reference Expert]                 

    #region [Commands Regnum ==> Tbl93Comments]  
    public ICommand AddCommentCommand => new RelayCommand<string>(AddComment_Executed);
    public ICommand CopyCommentCommand => new RelayCommand<string>(CopyComment_Executed);
    public ICommand DeleteCommentCommand => new RelayCommand<string>(DeleteComment_Executed);
    public ICommand SaveCommentCommand => new RelayCommand<string>(SaveComment_Executed);
    public ICommand RefreshCommentServerCommand => new RelayCommand<string>(RefreshCommentServer_Executed);
    #endregion [Commands Regnum ==> Tbl93Comments]         

    #region [Methods Regnum ==> Tbl93Comments]  
    private void AddComment_Executed(string? parm)
    {
        CommentStartEdit();
        CommentStartNew();
        Tbl93CommentsList ??= new ObservableCollection<Tbl93Comment>();
        Tbl93CommentsList.Insert(index: 0, item: new Tbl93Comment { Info = "New", RegnumId = RegnumSelected!.RegnumId });
  
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
        Tbl93CommentsList ??= new ObservableCollection<Tbl93Comment>();
        if (CommentSelected != null)
        {
            if (RegnumSelected != null)
            {
                CommentSelected.RegnumId = RegnumSelected.RegnumId; //combo vorbelegen
            }

            Tbl93CommentsList = await _dataService.CopyComment(CommentSelected);
        }
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

        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromRegnumId(CommentSelected.RegnumId);
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
                if (RegnumSelected != null)
                {
                    CommentSelected.RegnumId = RegnumSelected.RegnumId;

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
                        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromRegnumId(RegnumSelected.RegnumId);
                        //   Index Position ?
                        if (indx < Tbl93CommentsList.Count)
                        {
                            CommentItems.Clear();
                            foreach (var item in Tbl93CommentsList)
                            {
                                CommentItems.Add(item);
                            }

                            CommentSelected = Tbl93CommentsList[indx]; //Index
                        }
                    }
                }
            }
        }
        CommentCancelEditsAsync();
    }
    private void RefreshCommentServer_Executed(string? parm)
    {
        Tbl93CommentsList ??= new ObservableCollection<Tbl93Comment>();
        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromRegnumId(RegnumSelected!.RegnumId);

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
    #endregion [Methods Regnum ==> Tbl93Comments]        

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
            }

            if (_selectedMainDetailTabIndex == 1)
            {
                if (RegnumSelected != null)
                {
                    IsLoading = true;
                    PhylumStartModify();
                    Tbl03RegnumsAllList ??= new ObservableCollection<Tbl03Regnum>();
                    Tbl03RegnumsAllList = _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnum();

                    Tbl06PhylumsList ??= new ObservableCollection<Tbl06Phylum>();
                    Tbl06PhylumsList = _dataService.GetTbl06PhylumsCollectionOrderByPhylumNameFromRegnumId(RegnumSelected.RegnumId);
                    PhylumDataSetCount = Tbl06PhylumsList.Count;
                    RefreshPhylumItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 2)
            {
                if (RegnumSelected != null)
                {
                    IsLoading = true;
                    DivisionStartModify();
                    Tbl03RegnumsAllList ??= new ObservableCollection<Tbl03Regnum>();
                    Tbl03RegnumsAllList = _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnum();

                    Tbl09DivisionsList ??= new ObservableCollection<Tbl09Division>();
                    Tbl09DivisionsList = _dataService.GetTbl09DivisionsCollectionOrderByDivisionNameFromRegnumId(RegnumSelected.RegnumId);
                    DivisionDataSetCount = Tbl09DivisionsList.Count;
                    RefreshDivisionItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 3)
            {
                if (RegnumSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList ??= new ObservableCollection<Tbl90RefExpert>();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList ??= new ObservableCollection<Tbl90Reference>();
                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(RegnumSelected.RegnumId);
                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 4)
            {
                if (RegnumSelected != null)
                {
                    IsLoading = true;
                    CommentStartModify();
                    Tbl93CommentsList ??= new ObservableCollection<Tbl93Comment>();
                    Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromRegnumId(RegnumSelected.RegnumId);

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
                if (RegnumSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList ??= new ObservableCollection<Tbl90RefExpert>();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList ??= new ObservableCollection<Tbl90Reference>();
                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(RegnumSelected.RegnumId);
                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 1)
            {
                if (RegnumSelected != null)
                {
                    IsLoading = true;
                    ReferenceSourceStartModify();
                    Tbl90SourcesAllList ??= new ObservableCollection<Tbl90RefSource>();
                    Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

                    Tbl90ReferenceSourcesList ??= new ObservableCollection<Tbl90Reference>();
                    Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromRegnumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(RegnumSelected.RegnumId);

                    ReferenceSourceDataSetCount = Tbl90ReferenceSourcesList.Count;
                    RefreshReferenceSourceItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 2)
            {
                if (RegnumSelected != null)
                {
                    IsLoading = true;
                    ReferenceAuthorStartModify();
                    Tbl90AuthorsAllList ??= new ObservableCollection<Tbl90RefAuthor>();
                    Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

                    Tbl90ReferenceAuthorsList ??= new ObservableCollection<Tbl90Reference>();
                    Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromRegnumIdAndRefSourceIdIsNullAndRefExpertIdIsNull(RegnumSelected.RegnumId);

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

    private Tbl03Regnum _regnumSelected = null!;
    public Tbl03Regnum RegnumSelected
    {
        get => _regnumSelected;
        set => SetProperty(ref _regnumSelected, value);
    }

    private Tbl06Phylum _phylumSelected = null!;
    public Tbl06Phylum PhylumSelected
    {
        get => _phylumSelected;
        set => SetProperty(ref _phylumSelected, value);
    }

    private Tbl09Division _divisionSelected = null!;
    public Tbl09Division DivisionSelected
    {
        get => _divisionSelected;
        set => SetProperty(ref _divisionSelected, value);
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
    public string SearchRegnumName { get; set; } = null!;


    private ObservableCollection<Tbl03Regnum> _tbl03RegnumsList = null!;
    public ObservableCollection<Tbl03Regnum> Tbl03RegnumsList
    {
        get => _tbl03RegnumsList;
        set
        {
            _tbl03RegnumsList = value; OnPropertyChanged();
        }
    }
    private ObservableCollection<Tbl03Regnum>? _tbl03RegnumsAllList = null!;
    public ObservableCollection<Tbl03Regnum>? Tbl03RegnumsAllList
    {
        get => _tbl03RegnumsAllList;
        set
        {
            _tbl03RegnumsAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl06Phylum> _tbl06PhylumsList = null!;
    public ObservableCollection<Tbl06Phylum> Tbl06PhylumsList
    {
        get => _tbl06PhylumsList;
        set
        {
            _tbl06PhylumsList = value; OnPropertyChanged();
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

    private ObservableCollection<Tbl09Division>? _tbl09DivisionsList;
    public ObservableCollection<Tbl09Division>? Tbl09DivisionsList
    {
        get => _tbl09DivisionsList;
        set
        {
            _tbl09DivisionsList = value; OnPropertyChanged(nameof(Tbl09DivisionsList));
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

    private int _regnumDataSetCount;
    public int RegnumDataSetCount
    {
        get => _regnumDataSetCount;
        set
        {
            _regnumDataSetCount = value; OnPropertyChanged();
        }
    }

    private int _phylumDataSetCount;
    public int PhylumDataSetCount
    {
        get => _phylumDataSetCount;
        set
        {
            _phylumDataSetCount = value; OnPropertyChanged();
        }
    }

    private int _divisionDataSetCount;
    public int DivisionDataSetCount
    {
        get => _divisionDataSetCount;
        set
        {
            _divisionDataSetCount = value; OnPropertyChanged();
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

    private bool _isNewRegnum;
    public bool IsNewRegnum
    {
        get => _isNewRegnum;
        set => SetProperty(ref _isNewRegnum, value);
    }
    private bool _isNewPhylum;
    public bool IsNewPhylum
    {
        get => _isNewPhylum;
        set => SetProperty(ref _isNewPhylum, value);
    }

    private bool _isNewDivision;
    public bool IsNewDivision
    {
        get => _isNewDivision;
        set => SetProperty(ref _isNewDivision, value);
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

    private void RefreshRegnumItems()
    {
        RegnumItems.Clear();
        if (Tbl03RegnumsList != null)
        {
            foreach (var item in Tbl03RegnumsList)
            {
                RegnumItems.Add(item);
            }

            if (Tbl03RegnumsList.Count == 0)
            {
                return;
            }

            if (RegnumSelected == null && Tbl03RegnumsList.Count != 0)
            {
                RegnumSelected = RegnumItems.FirstOrDefault()!;
            }
        }
    }

    private void RefreshPhylumItems()
    {
        PhylumItems.Clear();
        if (Tbl06PhylumsList != null)
        {
            foreach (var item in Tbl06PhylumsList)
            {
                PhylumItems.Add(item);
            }

            if (Tbl06PhylumsList.Count == 0)
            {
                return;
            }

            if (PhylumSelected == null && Tbl06PhylumsList.Count != 0)
            {
                PhylumSelected = PhylumItems.FirstOrDefault()!;
            }
        }
    }
    private void RefreshDivisionItems()
    {
        DivisionItems.Clear();
        if (Tbl09DivisionsList != null)
        {
            foreach (var item in Tbl09DivisionsList)
            {
                DivisionItems.Add(item);
            }

            if (Tbl09DivisionsList.Count == 0)
            {
                return;
            }

            if (DivisionSelected == null && Tbl09DivisionsList.Count != 0)
            {
                DivisionSelected = DivisionItems.FirstOrDefault()!;
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