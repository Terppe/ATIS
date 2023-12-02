using System.Collections.ObjectModel;
using System.Windows.Input;
using ATIS.WinUi.Contracts.Services;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

//    Tbl06PhylumsViewModel Skriptdatum:  21.03.2023  12:32    

namespace ATIS.WinUi.ViewModels.Database;

public class Tbl06PhylumsViewModel : ObservableObject
{
    #region [Private Data Members]
    private readonly IDataService _dataService;
    public ObservableCollection<Tbl06Phylum> PhylumItems { get; } = new();
    public ObservableCollection<Tbl12Subphylum> SubphylumItems { get; } = new();
    public ObservableCollection<Tbl03Regnum> RegnumItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceExpertItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceSourceItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceAuthorItems { get; } = new();
    public ObservableCollection<Tbl93Comment> CommentItems { get; } = new();
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]

    #region [Constructor]
    public Tbl06PhylumsViewModel(IDataService dataService)
    {
        _dataService = dataService;
        SelectedMainDetailTabIndex = 1; //Tab Datagrid
        GetAllCollections();
    }

 
    private void GetAllCollections()
    {
        Tbl03RegnumsAllList ??= new ObservableCollection<Tbl03Regnum>();
        Tbl03RegnumsAllList = _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnum();
        Tbl06PhylumsAllList ??= new ObservableCollection<Tbl06Phylum>();
        Tbl06PhylumsAllList = _dataService.GetTbl06PhylumsCollectionOrderByPhylumName();

        Tbl90ExpertsAllList ??= new ObservableCollection<Tbl90RefExpert>();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();
        Tbl90AuthorsAllList ??= new ObservableCollection<Tbl90RefAuthor>();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();
        Tbl90SourcesAllList ??= new ObservableCollection<Tbl90RefSource>();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();
    }

    #endregion [Constructor]

    //    Part 1    

    #region [Commands Phylum]
    public ICommand GetPhylumsByNameOrIdCommand => new RelayCommand(execute: delegate 
        { var task = GetPhylumsByNameOrId_Executed(SearchPhylumName); });
    public ICommand AddPhylumCommand => new RelayCommand<string>(AddPhylum_Executed);
    public ICommand CopyPhylumCommand => new RelayCommand<string>(CopyPhylum_Executed);
    public ICommand DeletePhylumCommand => new RelayCommand(execute: delegate { DeletePhylum_Executed(SearchPhylumName); });
    public ICommand SavePhylumCommand => new RelayCommand(execute: delegate { var task = SavePhylum_Executed(SearchPhylumName); });
    public ICommand RefreshPhylumServerCommand => new RelayCommand(execute: delegate { RefreshPhylumServer_Executed(SearchPhylumName); });
    #endregion [Commands Phylum]

    #region [Methods Tbl06Phylum]                   
    private async Task GetPhylumsByNameOrId_Executed(string? parm)
    {
        IsLoading = true;
        PhylumStartModify();
        Tbl03RegnumsList?.Clear();
        Tbl06PhylumsList?.Clear();
        Tbl12SubphylumsList?.Clear();
        Tbl90ReferenceExpertsList?.Clear();
        Tbl90ReferenceSourcesList?.Clear();
        Tbl90ReferenceAuthorsList?.Clear();
        Tbl93CommentsList?.Clear();

        RegnumItems.Clear();
        PhylumItems.Clear();
        SubphylumItems.Clear();
        ReferenceAuthorItems.Clear();
        ReferenceSourceItems.Clear();
        ReferenceExpertItems.Clear();
        CommentItems.Clear();
        Tbl06PhylumsList ??= new ObservableCollection<Tbl06Phylum>();
        Tbl06PhylumsList = await _dataService.GetTbl06PhylumsCollectionOrderByPhylumNameFromSearchNameOrId(parm!);
        if (Tbl06PhylumsList is { Count: 0 })
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        PhylumDataSetCount = Tbl06PhylumsList.Count;
        RefreshPhylumItems();
        SelectedMainDetailTabIndex = 1;
        IsLoading = false;
    }
    private async void AddPhylum_Executed(string? parm)
    {
        PhylumStartEdit();
        PhylumStartNew();

        Tbl03RegnumsAllList = _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnum();

        //Id search for first Dataset of Tbl03RegnumsList
        var single = await _dataService.GetRegnumSingleFirstDataset();
        var id = single.RegnumId;

        Tbl06PhylumsList ??= new ObservableCollection<Tbl06Phylum>();
        Tbl06PhylumsList.Insert(index: 0, item: new Tbl06Phylum { PhylumName = "New", RegnumId = id });

        RefreshPhylumItems();
    }
    private async void CopyPhylum_Executed(string? parm)
    {
        PhylumStartEdit();
        PhylumStartNew();
        Tbl06PhylumsList ??= new ObservableCollection<Tbl06Phylum>();
        Tbl06PhylumsList = await _dataService.CopyPhylum(PhylumSelected);
        
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshPhylumItems();
    }
    private async void DeletePhylum_Executed(string? parm)
    {
        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(PhylumSelected!.PhylumName!))
        {
            //necessary to delete before
            await _dataService.DeleteConnectedSubphylums(PhylumSelected);
            await _dataService.DeleteConnectedPhylumReferences(PhylumSelected);
            await _dataService.DeleteConnectedPhylumComments(PhylumSelected);

            var ret = _dataService.DeletePhylum(PhylumSelected);
            if (!await ret)
            {
                return;
            }

            Tbl06PhylumsList ??= new ObservableCollection<Tbl06Phylum>();
            Tbl06PhylumsList = await _dataService.GetTbl06PhylumsCollectionOrderByPhylumNameFromSearchNameOrId(parm!);

            PhylumDataSetCount = Tbl06PhylumsList.Count;
            RefreshPhylumItems();
        }
    }
    private async Task SavePhylum_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(PhylumSelected?.PhylumName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl06PhylumsList != null)
        {
            var iNdx = Tbl06PhylumsList.IndexOf(Tbl06PhylumsList.First(t =>
                t.PhylumName == PhylumSelected.PhylumName));

            var ret = _dataService.SavePhylum(PhylumSelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl06PhylumsList = await _dataService.GetLastDatasetInTbl06Phylums();
                RefreshPhylumItems();
            }
            else
            {
                if (PhylumSelected.PhylumId == 0) //new
                {
                    Tbl06PhylumsList = await _dataService.GetLastDatasetInTbl06Phylums();
                    RefreshPhylumItems();
                }
                else
                {
                    Tbl06PhylumsList = await _dataService.GetTbl06PhylumsCollectionOrderByPhylumNameFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl06PhylumsList!.Count)
                    {
                        PhylumItems.Clear();
                        foreach (var item in Tbl06PhylumsList)
                        {
                            PhylumItems.Add(item);
                        }

                        PhylumSelected = Tbl06PhylumsList[iNdx];
                    }
                }
            }
        }

        PhylumDataSetCount = Tbl06PhylumsList!.Count;
        RefreshPhylumItems();
        PhylumCancelEditsAsync();
    }
    private async void RefreshPhylumServer_Executed(string? parm)
    {
        Tbl06PhylumsList = await _dataService.GetTbl06PhylumsCollectionOrderByPhylumNameFromSearchNameOrId(parm!);

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
    #endregion [Methods Tbl06Phylum]                   

    //    Part 2    


    #region "Public Commands Connect <== Tbl03Regnum"                 

    public ICommand SaveRegnumCommand => new RelayCommand<string>(SaveRegnum_Executed);
    public ICommand RefreshRegnumServerCommand => new RelayCommand(execute: delegate { RefreshRegnumServer_Executed(SearchPhylumName); });

    private async void SaveRegnum_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(RegnumSelected?.RegnumName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl03RegnumsList != null)
        {
            var iNdx = Tbl03RegnumsList.IndexOf(Tbl03RegnumsList.First(t =>
               t.RegnumName == RegnumSelected.RegnumName));

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
                    Tbl03RegnumsList = await _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnumFromSearchNameOrId(parm);
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
        RegnumCancelEditsAsync();
    }
    private async void RefreshRegnumServer_Executed(string? parm)
    {
        Tbl03RegnumsList = await _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnumFromSearchNameOrId(parm);

        RegnumDataSetCount = Tbl03RegnumsList.Count;
        RefreshRegnumItems();
    }

    public void RegnumStartEdit() => IsInEdit = true;
    public void RegnumStartModify() => IsModified = true;
    public void RegnumCancelEditsAsync()
    {
        IsInEdit = false;
        IsModified = false;
    }

    #endregion "Public Commands"                  



    //    Part 3    

    //    Part 4    

    #region [Public Commands Connect ==> Tbl12Subphylum]
    public ICommand AddSubphylumCommand => new RelayCommand<string>(AddSubphylum_Executed);
    public ICommand CopySubphylumCommand => new RelayCommand<string>(CopySubphylum_Executed);
    public ICommand DeleteSubphylumCommand => new RelayCommand<string>(DeleteSubphylum_Executed);
    public ICommand SaveSubphylumCommand => new RelayCommand<string>(SaveSubphylum_Executed);
    public ICommand RefreshSubphylumServerCommand => new RelayCommand(execute: delegate { RefreshSubphylumServer_Executed(SubphylumSelected!.PhylumId); });
    #endregion [Public Commands Connect ==> Tbl12Subphylum]    

    #region [Public Methods Connect ==> Tbl12Subphylum]                   
    private void AddSubphylum_Executed(string? parm)
    {
        SubphylumStartEdit();
        SubphylumStartNew();
        Tbl12SubphylumsList.Insert(0, new Tbl12Subphylum { SubphylumName = "New", PhylumId = PhylumSelected.PhylumId });

        SubphylumItems.Clear();
        foreach (var item in Tbl12SubphylumsList)
        {
            SubphylumItems.Add(item);
        }
        SubphylumSelected = SubphylumItems.First();
    }
    private async void CopySubphylum_Executed(string? parm)
    {
        SubphylumStartEdit();
        SubphylumStartNew();
        SubphylumSelected.PhylumId = PhylumSelected.PhylumId; //combo vorbelegen

        Tbl12SubphylumsList = await _dataService.CopySubphylum(SubphylumSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshSubphylumItems();
    }
    private async void DeleteSubphylum_Executed(string? parm)
    {
        if (SubphylumSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.DeleteSubphylum(SubphylumSelected);
        if (!await ret)
        {
            return;
        }

        Tbl12SubphylumsList = _dataService.GetTbl12SubphylumsCollectionOrderBySubphylumNameFromPhylumId(SubphylumSelected.PhylumId);
  
        SubphylumDataSetCount = Tbl12SubphylumsList.Count;
        RefreshSubphylumItems();
    }
    private async void SaveSubphylum_Executed(string? parm)
    {
        if (SubphylumSelected != null && string.IsNullOrEmpty(SubphylumSelected.SubphylumName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }

        if (Tbl12SubphylumsList != null)
        {
            var indx = Tbl12SubphylumsList.IndexOf(Tbl12SubphylumsList.First(t =>
                SubphylumSelected != null && t.SubphylumName == SubphylumSelected.SubphylumName));

            if (SubphylumSelected != null)
            {
                if (PhylumSelected != null)
                {
                    SubphylumSelected.PhylumId = PhylumSelected.PhylumId;

                    var ret = _dataService.SaveSubphylum(SubphylumSelected);
                    if (!await ret)
                    {
                        return;
                    }

                    if (SubphylumSelected.SubphylumId == 0) //new
                    {
                        Tbl12SubphylumsList = await _dataService.GetLastDatasetInTbl12Subphylums();
                        RefreshSubphylumItems();
                    }
                    else
                    {
                        Tbl12SubphylumsList = _dataService.GetTbl12SubphylumsCollectionOrderBySubphylumNameFromPhylumId(PhylumSelected.PhylumId);
                        //   Index Position ?
                        if (indx < Tbl12SubphylumsList.Count)
                        {
                            SubphylumItems.Clear();
                            foreach (var item in Tbl12SubphylumsList)
                            {
                                SubphylumItems.Add(item);
                            }

                            SubphylumSelected = Tbl12SubphylumsList[indx]; //Index
                        }
                    }
                }
            }
        }
        SubphylumDataSetCount = Tbl12SubphylumsList!.Count;
        SubphylumCancelEditsAsync();
    }
    private void RefreshSubphylumServer_Executed(int id)
    {
        Tbl12SubphylumsList ??= new ObservableCollection<Tbl12Subphylum>();
        Tbl12SubphylumsList = _dataService.GetTbl12SubphylumsCollectionOrderBySubphylumNameFromPhylumId(id);

        SubphylumDataSetCount = Tbl12SubphylumsList.Count;

        RefreshSubphylumItems();
    }
    public void SubphylumStartEdit() => IsInEdit = true;
    public void SubphylumStartModify() => IsModified = true;
    public void SubphylumStartNew() => IsNewSubphylum = true;
    public event EventHandler AddNewSubphylumCanceled = null!;
    public void SubphylumCancelEditsAsync()
    {
        if (IsNewSubphylum)
        {
            IsInEdit = false;
            AddNewSubphylumCanceled?.Invoke(this, EventArgs.Empty);
            IsNewSubphylum = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }
    #endregion [Public Methods Connect ==> Tbl12Subphylum]                   

    //    Part 5    

    //    Part 6    

    //    Part 7    

    //    Part 8    

    #region [Commands Phylum ==> Tbl90Reference Author]
    public ICommand AddReferenceAuthorCommand => new RelayCommand<string>(AddReferenceAuthor_Executed);
    public ICommand CopyReferenceAuthorCommand => new RelayCommand<string>(CopyReferenceAuthor_Executed);
    public ICommand DeleteReferenceAuthorCommand => new RelayCommand<string>(DeleteReferenceAuthor_Executed);
    public ICommand SaveReferenceAuthorCommand => new RelayCommand<string>(SaveReferenceAuthor_Executed);
    public ICommand RefreshReferenceAuthorServerCommand => new RelayCommand<string>(RefreshReferenceAuthorServer_Executed);
    #endregion [Commands Phylum ==> Tbl90Reference Author]

    #region [Methods Phylum ==> Tbl90Reference Author]
    private void AddReferenceAuthor_Executed(string? parm)
    {
        ReferenceAuthorStartEdit();
        ReferenceAuthorStartNew();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

        Tbl90ReferenceAuthorsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", PhylumId = PhylumSelected.PhylumId });
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
        ReferenceAuthorSelected.PhylumId = PhylumSelected.PhylumId; //combo vorbelegen

        Tbl90ReferenceAuthorsList = await _dataService.CopyReferencePhylum(ReferenceAuthorSelected, "Author");

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

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromPhylumIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.PhylumId);
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

            if (PhylumSelected != null)
            {
                ReferenceAuthorSelected.PhylumId = PhylumSelected.PhylumId;
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
                Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromPhylumIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.PhylumId);
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
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromPhylumIdAndRefSourceIdIsNullAndRefExpertIdIsNull(PhylumSelected.PhylumId);

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
    #endregion [Methods Phylum ==> Tbl90Reference Author]

    #region [Commands Phylum ==> Tbl90Reference Source]      
    public ICommand AddReferenceSourceCommand => new RelayCommand<string>(AddReferenceSource_Executed);
    public ICommand CopyReferenceSourceCommand => new RelayCommand<string>(CopyReferenceSource_Executed);
    public ICommand DeleteReferenceSourceCommand => new RelayCommand<string>(DeleteReferenceSource_Executed);
    public ICommand SaveReferenceSourceCommand => new RelayCommand<string>(SaveReferenceSource_Executed);
    public ICommand RefreshReferenceSourceServerCommand => new RelayCommand<string>(RefreshReferenceSourceServer_Executed);
    #endregion [Commands Phylum ==> Tbl90Reference Source]      

    #region [Methods Phylum ==> Tbl90Reference Source]      
    private void AddReferenceSource_Executed(string? parm)
    {
        ReferenceSourceStartEdit();
        ReferenceSourceStartNew();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

        Tbl90ReferenceSourcesList.Insert(index: 0, item: new Tbl90Reference { Info = "New", PhylumId = PhylumSelected.PhylumId });
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
        if (PhylumSelected != null)
        {
            ReferenceSourceSelected.PhylumId = PhylumSelected.PhylumId; //combo vorbelegen
        }

        Tbl90ReferenceSourcesList = await _dataService.CopyReferencePhylum(ReferenceSourceSelected, "Source");

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

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromPhylumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.PhylumId);
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

            if (PhylumSelected != null)
            {
                ReferenceSourceSelected.PhylumId = PhylumSelected.PhylumId;
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
                Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromPhylumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.PhylumId);
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
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();
        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromPhylumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(PhylumSelected.PhylumId);

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
    #endregion [Methods Phylum ==> Tbl90Reference Source]      

    #region [Commands Phylum ==> Tbl90Reference Expert]                 
    public ICommand AddReferenceExpertCommand => new RelayCommand<string>(AddReferenceExpert_Executed);
    public ICommand CopyReferenceExpertCommand => new RelayCommand<string>(CopyReferenceExpert_Executed);
    public ICommand DeleteReferenceExpertCommand => new RelayCommand<string>(DeleteReferenceExpert_Executed);
    public ICommand SaveReferenceExpertCommand => new RelayCommand<string>(SaveReferenceExpert_Executed);
    public ICommand RefreshReferenceExpertServerCommand => new RelayCommand<string>(RefreshReferenceExpertServer_Executed);
    #endregion [Commands Phylum ==> Tbl90Reference Expert]                 

    #region [Methods Phylum ==> Tbl90Reference Expert]   
    private void AddReferenceExpert_Executed(string? parm)
    {
        ReferenceExpertStartEdit();
        ReferenceExpertStartNew();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

        Tbl90ReferenceExpertsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", PhylumId = PhylumSelected.PhylumId });
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
        if (PhylumSelected != null)
        {
            ReferenceExpertSelected.PhylumId = PhylumSelected.PhylumId; //combo vorbelegen
        }

        Tbl90ReferenceExpertsList = await _dataService.CopyReferencePhylum(ReferenceExpertSelected, "Expert");

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

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromPhylumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.PhylumId);
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

            if (PhylumSelected != null)
            {
                ReferenceExpertSelected.PhylumId = PhylumSelected.PhylumId;
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
                Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromPhylumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.PhylumId);
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
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromPhylumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(PhylumSelected.PhylumId);

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
    #endregion [Methods Phylum ==> Tbl90Reference Expert]                 

    #region [Commands Phylum ==> Tbl93Comments]  
    public ICommand AddCommentCommand => new RelayCommand<string>(AddComment_Executed);
    public ICommand CopyCommentCommand => new RelayCommand<string>(CopyComment_Executed);
    public ICommand DeleteCommentCommand => new RelayCommand<string>(DeleteComment_Executed);
    public ICommand SaveCommentCommand => new RelayCommand<string>(SaveComment_Executed);
    public ICommand RefreshCommentServerCommand => new RelayCommand<string>(RefreshCommentServer_Executed);
    #endregion [Commands Phylum ==> Tbl93Comments]         

    #region [Methods Phylum ==> Tbl93Comments]  
    private void AddComment_Executed(string? parm)
    {
        CommentStartEdit();
        CommentStartNew();
        Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = "New", PhylumId = PhylumSelected.PhylumId });

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
        if (PhylumSelected != null)
        {
            CommentSelected.PhylumId = PhylumSelected.PhylumId; //combo vorbelegen
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

        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromPhylumId(CommentSelected.PhylumId);
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
                if (PhylumSelected != null)
                {
                    CommentSelected.PhylumId = PhylumSelected.PhylumId;

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
                        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromPhylumId(PhylumSelected.PhylumId);
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
        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromPhylumId(PhylumSelected.PhylumId);

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
    #endregion [Methods Phylum ==> Tbl93Comments]        

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
                if (PhylumSelected != null)
                {
                    IsLoading = true;
                    RegnumStartModify();
                    Tbl03RegnumsList = _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnumFromRegnumId(PhylumSelected.RegnumId);

                    RegnumDataSetCount = Tbl03RegnumsList.Count;
                    RefreshRegnumItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 1)
            {
                Tbl03RegnumsAllList = _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnum();
            }

            if (_selectedMainDetailTabIndex == 2)
            {
                if (PhylumSelected != null)
                {
                    IsLoading = true;
                    SubphylumStartModify();
                    Tbl06PhylumsAllList = _dataService.GetTbl06PhylumsCollectionOrderByPhylumName();

                    Tbl12SubphylumsList = _dataService.GetTbl12SubphylumsCollectionOrderBySubphylumNameFromPhylumId(PhylumSelected.PhylumId);
                    SubphylumDataSetCount = Tbl12SubphylumsList.Count;
                    RefreshSubphylumItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 3)
            {
                if (PhylumSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromPhylumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(PhylumSelected.PhylumId);
                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 4)
            {
                if (PhylumSelected != null)
                {
                    IsLoading = true;
                    CommentStartModify();
                    Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromPhylumId(PhylumSelected.PhylumId);

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
                if (PhylumSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromPhylumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(PhylumSelected.PhylumId);
                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 1)
            {
                if (PhylumSelected != null)
                {
                    IsLoading = true;
                    ReferenceSourceStartModify();
                    Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

                    Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromPhylumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(PhylumSelected.PhylumId);

                    ReferenceSourceDataSetCount = Tbl90ReferenceSourcesList.Count;
                    RefreshReferenceSourceItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 2)
            {
                if (PhylumSelected != null)
                {
                    IsLoading = true;
                    ReferenceAuthorStartModify();
                    Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

                    Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromPhylumIdAndRefSourceIdIsNullAndRefExpertIdIsNull(PhylumSelected.PhylumId);

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

    private Tbl12Subphylum _subphylumSelected = null!;
    public Tbl12Subphylum SubphylumSelected
    {
        get => _subphylumSelected;
        set => SetProperty(ref _subphylumSelected, value);
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

    public string SearchPhylumName { get; set; } = null!;

    private ObservableCollection<Tbl03Regnum> _tbl03RegnumsList = null!;
    public ObservableCollection<Tbl03Regnum> Tbl03RegnumsList
    {
        get => _tbl03RegnumsList;
        set { _tbl03RegnumsList = value; OnPropertyChanged(); }
    }
    private ObservableCollection<Tbl03Regnum>? _tbl03RegnumsAllList;
    public ObservableCollection<Tbl03Regnum>? Tbl03RegnumsAllList
    {
        get => _tbl03RegnumsAllList;
        set { _tbl03RegnumsAllList = value; OnPropertyChanged(); }
    }

    private ObservableCollection<Tbl06Phylum> _tbl06PhylumsList = null!;
    public ObservableCollection<Tbl06Phylum> Tbl06PhylumsList
    {
        get => _tbl06PhylumsList;
        set { _tbl06PhylumsList = value; OnPropertyChanged(); }
    }
    private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList = null!;
    public ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
    {
        get => _tbl06PhylumsAllList;
        set { _tbl06PhylumsAllList = value; OnPropertyChanged(); }
    }

    private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsList = null!;
    public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsList
    {
        get => _tbl12SubphylumsList;
        set { _tbl12SubphylumsList = value; OnPropertyChanged(); }
    }

    private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList = null!;
    public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
    {
        get => _tbl90AuthorsAllList;
        set { _tbl90AuthorsAllList = value; OnPropertyChanged(); }
    }

    private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList = null!;
    public ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
    {
        get => _tbl90SourcesAllList;
        set { _tbl90SourcesAllList = value; OnPropertyChanged(); }
    }

    private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList = null!;
    public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
    {
        get => _tbl90ExpertsAllList;
        set { _tbl90ExpertsAllList = value; OnPropertyChanged(); }
    }

    private ObservableCollection<Tbl90Reference> _tbl90ReferenceAuthorsList = null!;
    public ObservableCollection<Tbl90Reference> Tbl90ReferenceAuthorsList
    {
        get => _tbl90ReferenceAuthorsList;
        set { _tbl90ReferenceAuthorsList = value; OnPropertyChanged(); }
    }

    private ObservableCollection<Tbl90Reference> _tbl90ReferenceSourcesList = null!;
    public ObservableCollection<Tbl90Reference> Tbl90ReferenceSourcesList
    {
        get => _tbl90ReferenceSourcesList;
        set { _tbl90ReferenceSourcesList = value; OnPropertyChanged(); }
    }

    private ObservableCollection<Tbl90Reference> _tbl90ReferenceExpertsList = null!;
    public ObservableCollection<Tbl90Reference> Tbl90ReferenceExpertsList
    {
        get => _tbl90ReferenceExpertsList;
        set { _tbl90ReferenceExpertsList = value; OnPropertyChanged(); }
    }

    private ObservableCollection<Tbl93Comment> _tbl93CommentsList = null!;
    public ObservableCollection<Tbl93Comment> Tbl93CommentsList
    {
        get => _tbl93CommentsList;
        set { _tbl93CommentsList = value; OnPropertyChanged(); }
    }

    //---------------------------------------------------------------------
    private int _regnumDataSetCount;
    public int RegnumDataSetCount
    {
        get => _regnumDataSetCount;
        set { _regnumDataSetCount = value; OnPropertyChanged(); }
    }

    private int _phylumDataSetCount;
    public int PhylumDataSetCount
    {
        get => _phylumDataSetCount;
        set { _phylumDataSetCount = value; OnPropertyChanged(); }
    }

    private int _subphylumDataSetCount;
    public int SubphylumDataSetCount
    {
        get => _subphylumDataSetCount;
        set { _subphylumDataSetCount = value; OnPropertyChanged(); }
    }

    private int _referenceExpertDataSetCount;
    public int ReferenceExpertDataSetCount
    {
        get => _referenceExpertDataSetCount;
        set { _referenceExpertDataSetCount = value; OnPropertyChanged(); }
    }
    private int _referenceSourceDataSetCount;
    public int ReferenceSourceDataSetCount
    {
        get => _referenceSourceDataSetCount;
        set { _referenceSourceDataSetCount = value; OnPropertyChanged(); }
    }
    private int _referenceAuthorDataSetCount;
    public int ReferenceAuthorDataSetCount
    {
        get => _referenceAuthorDataSetCount;
        set { _referenceAuthorDataSetCount = value; OnPropertyChanged(); }
    }

    private int _commentDataSetCount;
    public int CommentDataSetCount
    {
        get => _commentDataSetCount;
        set { _commentDataSetCount = value; OnPropertyChanged(); }
    }
    //---------------------------------------------------
    public bool IsModified { get; set; }

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

    private bool _isNewPhylum;
    public bool IsNewPhylum
    {
        get => _isNewPhylum;
        set => SetProperty(ref _isNewPhylum, value);
    }
    private bool _isNewSubphylum;
    public bool IsNewSubphylum
    {
        get => _isNewSubphylum;
        set => SetProperty(ref _isNewSubphylum, value);
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

    private void RefreshSubphylumItems()
    {
        SubphylumItems.Clear();
        if (Tbl12SubphylumsList != null)
        {
            foreach (var item in Tbl12SubphylumsList)
            {
                SubphylumItems.Add(item);
            }

            if (Tbl12SubphylumsList.Count == 0)
            {
                return;
            }

            if (SubphylumSelected == null && Tbl12SubphylumsList.Count != 0)
            {
                SubphylumSelected = SubphylumItems.FirstOrDefault()!;
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
