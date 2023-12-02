
using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using ATIS.WinUi.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Tbl18Superclass = ATIS.WinUi.Models.Tbl18Superclass;

//    Tbl12SubphylumsViewModel Skriptdatum:  27.03.2023  12:32    

namespace ATIS.WinUi.ViewModels.Database;

public class Tbl12SubphylumsViewModel : ObservableObject
{

    #region [Private Data Members]
    private readonly IDataService _dataService;
    private static readonly AtisDbContext Context = new();
    public ObservableCollection<Tbl12Subphylum> SubphylumItems { get; } = new();
    public ObservableCollection<Tbl18Superclass> SuperclassItems { get; } = new();

    public ObservableCollection<Tbl06Phylum> PhylumItems { get; } = new();

    public ObservableCollection<Tbl90Reference> ReferenceExpertItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceSourceItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceAuthorItems { get; } = new();
    public ObservableCollection<Tbl93Comment> CommentItems { get; } = new();
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]      

    #region [Constructor]
    public Tbl12SubphylumsViewModel(IDataService dataService)
    {
        _dataService = dataService;
        SelectedMainDetailTabIndex = 1; //Tab Datagrid
        GetAllCollections();
    }

    private void GetAllCollections()
    {
        Tbl12SubphylumsAllList ??= new ObservableCollection<Tbl12Subphylum>();
        Tbl12SubphylumsAllList = _dataService.GetTbl12SubphylumsCollectionOrderBySubphylumName();
        Tbl15SubdivisionsAllList ??= new ObservableCollection<Tbl15Subdivision>();
        Tbl15SubdivisionsAllList = _dataService.GetTbl15SubdivisionsCollectionOrderBySubdivisionName();

        Tbl06PhylumsAllList ??= new ObservableCollection<Tbl06Phylum>();
        Tbl06PhylumsAllList = _dataService.GetTbl06PhylumsCollectionOrderByPhylumName();
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



    #region [Commands Subphylum]

    public ICommand GetSubphylumsByNameOrIdCommand => new RelayCommand(execute: delegate
    {
        var task = GetSubphylumsByNameOrId_Executed(SearchSubphylumName);
    });

    public ICommand AddSubphylumCommand => new RelayCommand<string>(AddSubphylum_Executed);
    public ICommand CopySubphylumCommand => new RelayCommand<string>(CopySubphylum_Executed);

    public ICommand DeleteSubphylumCommand => new RelayCommand(execute: delegate { DeleteSubphylum_Executed(SearchSubphylumName); });

    public ICommand SaveSubphylumCommand => new RelayCommand(execute: delegate { var task = SaveSubphylum_Executed(SearchSubphylumName); });
    public ICommand RefreshSubphylumServerCommand => new RelayCommand(execute: delegate { RefreshSubphylumServer_Executed(SearchSubphylumName); });

    #endregion [Commands Subphylum]       

    #region [Methods Tbl12Subphylum]

    private async Task GetSubphylumsByNameOrId_Executed(string? parm)
    {
        IsLoading = true;
        SubphylumStartModify();
        Tbl06PhylumsList?.Clear();
        Tbl12SubphylumsList?.Clear();
        Tbl18SuperclassesList?.Clear();
        Tbl90ReferenceExpertsList?.Clear();
        Tbl90ReferenceSourcesList?.Clear();
        Tbl90ReferenceAuthorsList?.Clear();
        Tbl93CommentsList?.Clear();

        PhylumItems.Clear();
        SubphylumItems.Clear();
        SuperclassItems.Clear();
        ReferenceAuthorItems.Clear();
        ReferenceSourceItems.Clear();
        ReferenceExpertItems.Clear();
        CommentItems.Clear();

        Tbl12SubphylumsList ??= new ObservableCollection<Tbl12Subphylum>();
        Tbl12SubphylumsList = await _dataService.GetTbl12SubphylumsCollectionOrderBySubphylumNameFromSearchNameOrId(parm!);

        if (Tbl12SubphylumsList is { Count: 0 })
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        SubphylumDataSetCount = Tbl12SubphylumsList.Count;
        RefreshSubphylumItems();

        SelectedMainDetailTabIndex = 1;
        IsLoading = false;
    }

    private async void AddSubphylum_Executed(string? parm)
    {
        SubphylumStartEdit();
        SubphylumStartNew();

        //Id search for first Dataset of Tbl06PhylumsList
        var single = await _dataService.GetPhylumSingleFirstDataset();
        var id = single.PhylumId;

        Tbl12SubphylumsList ??= new ObservableCollection<Tbl12Subphylum>();
        Tbl12SubphylumsList.Insert(index: 0, item: new Tbl12Subphylum { SubphylumName = "New", PhylumId = id });

        RefreshSubphylumItems();
    }

    private async void CopySubphylum_Executed(string? parm)
    {
        SubphylumStartEdit();
        SubphylumStartNew();

        Tbl12SubphylumsList = await _dataService.CopySubphylum(SubphylumSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshSubphylumItems();
    }

    private async void DeleteSubphylum_Executed(string? parm)
    {

        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(SubphylumSelected!.SubphylumName!))
        {
            //necessary to delete before
            await _dataService.DeleteConnectedSuperclassesWithSubphylum(SubphylumSelected);
            await _dataService.DeleteConnectedSubphylumReferences(SubphylumSelected);
            await _dataService.DeleteConnectedSubphylumComments(SubphylumSelected);

            var ret = _dataService.DeleteSubphylum(SubphylumSelected);
            if (!await ret)
            {
                return;
            }

            Tbl12SubphylumsList = await _dataService.GetTbl12SubphylumsCollectionOrderBySubphylumNameFromSearchNameOrId(parm);

            RefreshSubphylumItems();
        }
    }

    private async Task SaveSubphylum_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(SubphylumSelected?.SubphylumName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl12SubphylumsList != null)
        {

            var iNdx = Tbl12SubphylumsList.IndexOf(Tbl12SubphylumsList.First(t =>
                 t.SubphylumName == SubphylumSelected.SubphylumName));

            var ret = _dataService.SaveSubphylum(SubphylumSelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl12SubphylumsList = await _dataService.GetLastDatasetInTbl12Subphylums();
                RefreshSubphylumItems();
            }
            else
            {
                if (SubphylumSelected.SubphylumId == 0) //new
                {
                    Tbl12SubphylumsList = await _dataService.GetLastDatasetInTbl12Subphylums();
                    RefreshSubphylumItems();
                }
                else
                {
                    Tbl12SubphylumsList = await _dataService.GetTbl12SubphylumsCollectionOrderBySubphylumNameFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl12SubphylumsList!.Count)
                    {
                        SubphylumItems.Clear();
                        foreach (var item in Tbl12SubphylumsList)
                        {
                            SubphylumItems.Add(item);
                        }

                        SubphylumSelected = Tbl12SubphylumsList[iNdx];
                    }
                }
            }
        }
        SubphylumDataSetCount = Tbl12SubphylumsList!.Count;
        SubphylumCancelEditsAsync();
    }

    private async void RefreshSubphylumServer_Executed(string? parm)
    {
        Tbl12SubphylumsList = await _dataService.GetTbl12SubphylumsCollectionOrderBySubphylumNameFromSearchNameOrId(parm!);

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

    #endregion [Methods Tbl12Subphylum]    




    //    Part 2    


    #region "Public Commands Connect <== Tbl06Phylum"                 

    public ICommand SavePhylumCommand => new RelayCommand<string>(SavePhylum_Executed);
    public ICommand RefreshPhylumServerCommand => new RelayCommand(execute: delegate { RefreshPhylumServer_Executed(); });

    private async void SavePhylum_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(PhylumSelected.PhylumName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (PhylumSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.SavePhylum(PhylumSelected);

        if (!await ret)
        {
            return;
        }

        Tbl06PhylumsList = _dataService.GetTbl06PhylumsCollectionOrderByPhylumNameFromPhylumId(SubphylumSelected.PhylumId);
        RefreshPhylumItems();
        PhylumCancelEditsAsync();
    }

    private void RefreshPhylumServer_Executed()
    {
        Tbl03RegnumsAllList ??= new ObservableCollection<Tbl03Regnum>();
        Tbl03RegnumsAllList = _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnum();

        Tbl06PhylumsList = _dataService.GetTbl06PhylumsCollectionOrderByPhylumNameFromPhylumId(SubphylumSelected.PhylumId);

        PhylumDataSetCount = Tbl06PhylumsList.Count;

        RefreshPhylumItems();
    }
    public void PhylumStartEdit() => IsInEdit = true;
    public void PhylumStartModify() => IsModified = true;
    public void PhylumCancelEditsAsync()
    {
        IsInEdit = false;
        IsModified = false;
    }
    #endregion "Public Commands"                          



    //    Part 3    





    //    Part 4    


    #region [Public Commands Connect ==> Tbl18Superclass]       

    public ICommand AddSuperclassCommand => new RelayCommand<string>(AddSuperclass_Executed);
    public ICommand CopySuperclassCommand => new RelayCommand<string>(CopySuperclass_Executed);
    public ICommand DeleteSuperclassCommand => new RelayCommand<string>(DeleteSuperclass_Executed);
    public ICommand SaveSuperclassCommand => new RelayCommand<string>(SaveSuperclass_Executed);
    public ICommand RefreshSuperclassServerCommand => new RelayCommand<string>(RefreshSuperclassServer_Executed);

    #endregion [Public Commands Connect ==> Tbl18Superclass]    

    #region [Public Methods Connect ==> Tbl18Superclass]                   

    private void AddSuperclass_Executed(string? parm)
    {
        SuperclassStartEdit();
        SuperclassStartNew();
        Tbl18SuperclassesList.Insert(0, new Tbl18Superclass { SuperclassName = "New", SubphylumId = SubphylumSelected.SubphylumId });

        SuperclassItems.Clear();
        foreach (var item in Tbl18SuperclassesList)
        {
            SuperclassItems.Add(item);
        }
        SuperclassSelected = SuperclassItems.First();
    }

    private async void CopySuperclass_Executed(string? parm)
    {
        SuperclassStartEdit();
        SuperclassStartNew();
        SuperclassSelected.SubphylumId = SubphylumSelected.SubphylumId;  //combo vorbelegen

        Tbl18SuperclassesList = await _dataService.CopySuperclass(SuperclassSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshSuperclassItems();
    }

    private async void DeleteSuperclass_Executed(string? parm)
    {
        if (SuperclassSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.DeleteSuperclass(SuperclassSelected);
        if (!await ret)
        {
            return;
        }

        Tbl18SuperclassesList = _dataService.GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSubphylumId(SuperclassSelected.SubphylumId);

        SuperclassDataSetCount = Tbl18SuperclassesList.Count;
        RefreshSuperclassItems();
    }

    private async void SaveSuperclass_Executed(string? parm)
    {
        if (SuperclassSelected != null && string.IsNullOrEmpty(SuperclassSelected.SuperclassName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl18SuperclassesList != null)
        {
            var indx = Tbl18SuperclassesList.IndexOf(Tbl18SuperclassesList.First(t =>
              SuperclassSelected != null && t.SuperclassName == SuperclassSelected.SuperclassName));

            if (SuperclassSelected != null)
            {
                if (SubphylumSelected != null)
                {
                    SuperclassSelected.SubphylumId = SubphylumSelected.SubphylumId;

                    //Search for SuperclassSelected.SubdivisionID with Plantae#Regnum#  Special
                    if (Context.Tbl15Subdivisions != null)
                    {
                        var plantaeRegnum = Context.Tbl15Subdivisions.FirstOrDefault(e => e.SubdivisionName == "Plantae#Regnum#");
                        if (plantaeRegnum != null)
                        {
                            SuperclassSelected.SubdivisionId = plantaeRegnum.SubdivisionId;
                        }
                    }


                    var ret = _dataService.SaveSuperclass(SuperclassSelected);
                    if (!await ret)
                    {
                        return;
                    }

                    if (SuperclassSelected.SuperclassId == 0) //new
                    {
                        Tbl18SuperclassesList = await _dataService.GetLastDatasetInTbl18Superclasses();
                        RefreshSuperclassItems();
                    }
                    else
                    {
                        Tbl18SuperclassesList = _dataService.GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSubphylumId(SubphylumSelected.SubphylumId);
                        //   Index Position ?
                        if (indx < Tbl18SuperclassesList.Count)
                        {
                            SuperclassItems.Clear();
                            foreach (var item in Tbl18SuperclassesList)
                            {
                                SuperclassItems.Add(item);
                            }

                            SuperclassSelected = Tbl18SuperclassesList[indx];  //Index
                        }
                    }
                }
            }
        }
        SuperclassDataSetCount = Tbl18SuperclassesList!.Count;
        SuperclassCancelEditsAsync();
    }

    private void RefreshSuperclassServer_Executed(string? parm)
    {
        Tbl18SuperclassesList ??= new ObservableCollection<Tbl18Superclass>();
        Tbl18SuperclassesList = _dataService.GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSubphylumId(SubphylumSelected.SubphylumId);

        SuperclassDataSetCount = Tbl18SuperclassesList.Count;

        RefreshSuperclassItems();
    }
    public void SuperclassStartEdit() => IsInEdit = true;
    public void SuperclassStartModify() => IsModified = true;
    public void SuperclassStartNew() => IsNewSuperclass = true;
    public event EventHandler AddNewSuperclassCanceled = null!;
    public void SuperclassCancelEditsAsync()
    {
        if (IsNewSuperclass)
        {
            IsInEdit = false;
            AddNewSuperclassCanceled?.Invoke(this, EventArgs.Empty);
            IsNewSuperclass = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }
    #endregion [Public Methods  Connect ==> Tbl18Superclass]                                                                                                                                            



    //    Part 5    




    //    Part 6    




    //    Part 7    



    //    Part 8    


    #region [Commands Subphylum ==> Tbl90Reference Author]
    public ICommand AddReferenceAuthorCommand => new RelayCommand<string>(AddReferenceAuthor_Executed);
    public ICommand CopyReferenceAuthorCommand => new RelayCommand<string>(CopyReferenceAuthor_Executed);
    public ICommand DeleteReferenceAuthorCommand => new RelayCommand<string>(DeleteReferenceAuthor_Executed);
    public ICommand SaveReferenceAuthorCommand => new RelayCommand<string>(SaveReferenceAuthor_Executed);
    public ICommand RefreshReferenceAuthorServerCommand => new RelayCommand<string>(RefreshReferenceAuthorServer_Executed);
    #endregion [Commands Subphylum ==> Tbl90Reference Author]                

    #region [Methods Subphylum ==> Tbl90Reference Author]

    private void AddReferenceAuthor_Executed(string? parm)
    {
        ReferenceAuthorStartEdit();
        ReferenceAuthorStartNew();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

        Tbl90ReferenceAuthorsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", SubphylumId = SubphylumSelected!.SubphylumId });

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
        ReferenceAuthorSelected.SubphylumId = SubphylumSelected.SubphylumId; //combo vorbelegen

        Tbl90ReferenceAuthorsList = await _dataService.CopyReferenceSubphylum(ReferenceAuthorSelected, "Author");

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

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubphylumIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.SubphylumId);
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

            if (SubphylumSelected != null)
            {
                ReferenceAuthorSelected.SubphylumId = SubphylumSelected.SubphylumId;
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
                Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubphylumIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.SubphylumId);
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

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubphylumIdAndRefSourceIdIsNullAndRefExpertIdIsNull(SubphylumSelected.SubphylumId);

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
    #endregion [Methods Subphylum ==> Tbl90Reference Author]                   

    #region [Commands Subphylum ==> Tbl90Reference Source]  
    public ICommand AddReferenceSourceCommand => new RelayCommand<string>(AddReferenceSource_Executed);
    public ICommand CopyReferenceSourceCommand => new RelayCommand<string>(CopyReferenceSource_Executed);
    public ICommand DeleteReferenceSourceCommand => new RelayCommand<string>(DeleteReferenceSource_Executed);
    public ICommand SaveReferenceSourceCommand => new RelayCommand<string>(SaveReferenceSource_Executed);
    public ICommand RefreshReferenceSourceServerCommand => new RelayCommand<string>(RefreshReferenceSourceServer_Executed);
    #endregion [Commands Subphylum ==> Tbl90Reference Source]         

    #region [Methods Subphylum ==> Tbl90Reference Source]      

    private void AddReferenceSource_Executed(string? parm)
    {
        ReferenceSourceStartEdit();
        ReferenceSourceStartNew();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

        Tbl90ReferenceSourcesList.Insert(index: 0, item: new Tbl90Reference { Info = "New", SubphylumId = SubphylumSelected!.SubphylumId });
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
        if (SubphylumSelected != null)
        {
            ReferenceSourceSelected.SubphylumId = SubphylumSelected.SubphylumId; //combo vorbelegen
        }
        Tbl90ReferenceSourcesList = await _dataService.CopyReferenceSubphylum(ReferenceSourceSelected, "Source");
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

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubphylumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.SubphylumId);
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

            if (SubphylumSelected != null)
            {
                ReferenceSourceSelected.SubphylumId = SubphylumSelected.SubphylumId;
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
                Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubphylumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.SubphylumId);
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

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubphylumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(SubphylumSelected.SubphylumId);

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
    #endregion [Methods Subphylum ==> Tbl90Reference Source]           

    #region [Commands Subphylum ==> Tbl90Reference Expert]       
    public ICommand AddReferenceExpertCommand => new RelayCommand<string>(AddReferenceExpert_Executed);
    public ICommand CopyReferenceExpertCommand => new RelayCommand<string>(CopyReferenceExpert_Executed);
    public ICommand DeleteReferenceExpertCommand => new RelayCommand<string>(DeleteReferenceExpert_Executed);
    public ICommand SaveReferenceExpertCommand => new RelayCommand<string>(SaveReferenceExpert_Executed);
    public ICommand RefreshReferenceExpertServerCommand => new RelayCommand<string>(RefreshReferenceExpertServer_Executed);
    #endregion [Commands Subphylum ==> Tbl90Reference Expert]                    

    #region [Methods Subphylum ==> Tbl90Reference Expert]                 

    private void AddReferenceExpert_Executed(string? parm)
    {
        ReferenceExpertStartEdit();
        ReferenceExpertStartNew();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

        Tbl90ReferenceExpertsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", SubphylumId = SubphylumSelected!.SubphylumId });
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
        if (SubphylumSelected != null)
        {
            ReferenceExpertSelected.SubphylumId = SubphylumSelected.SubphylumId; //combo vorbelegen
        }
        Tbl90ReferenceExpertsList = await _dataService.CopyReferenceSubphylum(ReferenceExpertSelected, "Expert");
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

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubphylumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.SubphylumId);
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
            if (SubphylumSelected != null)
            {
                ReferenceExpertSelected.SubphylumId = SubphylumSelected.SubphylumId;
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
                Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubphylumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.SubphylumId);
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

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubphylumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(SubphylumSelected.SubphylumId);

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
    #endregion [Methods Subphylum ==> Tbl90Reference Expert]                                 

    #region [Commands Subphylum ==> Tbl93Comments]         
    public ICommand AddCommentCommand => new RelayCommand<string>(AddComment_Executed);
    public ICommand CopyCommentCommand => new RelayCommand<string>(CopyComment_Executed);
    public ICommand DeleteCommentCommand => new RelayCommand<string>(DeleteComment_Executed);
    public ICommand SaveCommentCommand => new RelayCommand<string>(SaveComment_Executed);
    public ICommand RefreshCommentServerCommand => new RelayCommand<string>(RefreshCommentServer_Executed);
    #endregion [Commands Subphylum ==> Tbl93Comments]           


    #region [Methods Subphylum ==> Tbl93Comments]        

    private void AddComment_Executed(string? parm)
    {
        CommentStartEdit();
        CommentStartNew();
        Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = "New", SubphylumId = SubphylumSelected!.SubphylumId });

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
        if (SubphylumSelected != null)
        {
            CommentSelected.SubphylumId = SubphylumSelected.SubphylumId;  //combo vorbelegen
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

        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromSubphylumId(CommentSelected.SubphylumId);
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
                if (SubphylumSelected != null)
                {
                    CommentSelected.SubphylumId = SubphylumSelected.SubphylumId;

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
                        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromSubphylumId(SubphylumSelected.SubphylumId);
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
        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromSubphylumId(SubphylumSelected.SubphylumId);

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
    #endregion [Methods Subphylum ==> Tbl93Comments]                             


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

            _selectedMainDetailTabIndex = value; OnPropertyChanged();

            if (_selectedMainDetailTabIndex == 0)
            {
                if (SubphylumSelected != null)
                {
                    IsLoading = true;
                    PhylumStartModify();
                    Tbl03RegnumsAllList = _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnum();

                    Tbl06PhylumsList = _dataService.GetTbl06PhylumsCollectionOrderByPhylumNameFromPhylumId(SubphylumSelected.PhylumId);

                    PhylumDataSetCount = Tbl06PhylumsList.Count;
                    RefreshPhylumItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 1)
            {
            }

            if (_selectedMainDetailTabIndex == 2)
            {
                if (SubphylumSelected != null)
                {
                    IsLoading = true;
                    SuperclassStartModify();
                    Tbl12SubphylumsAllList = _dataService.GetTbl12SubphylumsCollectionOrderBySubphylumName();
                    Tbl15SubdivisionsAllList = _dataService.GetTbl15SubdivisionsCollectionOrderBySubdivisionName();

                    Tbl18SuperclassesList = _dataService.GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSubphylumId(SubphylumSelected.SubphylumId);

                    SuperclassDataSetCount = Tbl18SuperclassesList.Count;
                    RefreshSuperclassItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 3)
            {
                if (SubphylumSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubphylumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(SubphylumSelected.SubphylumId);

                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 4)
            {
                if (SubphylumSelected != null)
                {
                    IsLoading = true;
                    CommentStartModify();
                    Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromSubphylumId(SubphylumSelected.SubphylumId);

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

            _selectedMainDetailRefTabIndex = value; OnPropertyChanged();

            if (_selectedMainDetailRefTabIndex == 0)
            {
                if (SubphylumSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubphylumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(SubphylumSelected.SubphylumId);
                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 1)
            {
                if (SubphylumSelected != null)
                {
                    IsLoading = true;
                    ReferenceSourceStartModify();
                    Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

                    Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubphylumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(SubphylumSelected.SubphylumId);

                    ReferenceSourceDataSetCount = Tbl90ReferenceSourcesList.Count;
                    RefreshReferenceSourceItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 2)
            {
                if (SubphylumSelected != null)
                {
                    IsLoading = true;
                    ReferenceAuthorStartModify();
                    Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

                    Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubphylumIdAndRefSourceIdIsNullAndRefExpertIdIsNull(SubphylumSelected.SubphylumId);

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

    private Tbl18Superclass _superclassSelected = null!;
    public Tbl18Superclass SuperclassSelected
    {
        get => _superclassSelected;
        set => SetProperty(ref _superclassSelected, value);
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
    public string SearchSubphylumName { get; set; } = null!;

    private ObservableCollection<Tbl03Regnum>? _tbl03RegnumsAllList;
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
    private ObservableCollection<Tbl06Phylum>? _tbl06PhylumsAllList;
    public ObservableCollection<Tbl06Phylum>? Tbl06PhylumsAllList
    {
        get => _tbl06PhylumsAllList;
        set
        {
            _tbl06PhylumsAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsList = null!;
    public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsList
    {
        get => _tbl12SubphylumsList;
        set
        {
            _tbl12SubphylumsList = value; OnPropertyChanged();
        }
    }
    private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsAllList = null!;
    public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsAllList
    {
        get => _tbl12SubphylumsAllList;
        set
        {
            _tbl12SubphylumsAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsAllList = null!;
    public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsAllList
    {
        get => _tbl15SubdivisionsAllList;
        set
        {
            _tbl15SubdivisionsAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesList = null!;
    public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesList
    {
        get => _tbl18SuperclassesList;
        set
        {
            _tbl18SuperclassesList = value; OnPropertyChanged();
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
    private int _phylumDataSetCount;
    public int PhylumDataSetCount
    {
        get => _phylumDataSetCount;
        set
        {
            _phylumDataSetCount = value; OnPropertyChanged();
        }
    }

    private int _subphylumDataSetCount;
    public int SubphylumDataSetCount
    {
        get => _subphylumDataSetCount;
        set
        {
            _subphylumDataSetCount = value; OnPropertyChanged();
        }
    }

    private int _superclassDataSetCount;
    public int SuperclassDataSetCount
    {
        get => _superclassDataSetCount;
        set
        {
            _superclassDataSetCount = value; OnPropertyChanged();
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

    private bool _isNewSubphylum;
    public bool IsNewSubphylum
    {
        get => _isNewSubphylum;
        set => SetProperty(ref _isNewSubphylum, value);
    }
    private bool _isNewSuperclass;
    public bool IsNewSuperclass
    {
        get => _isNewSuperclass;
        set => SetProperty(ref _isNewSuperclass, value);
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

    private void RefreshSuperclassItems()
    {
        SuperclassItems.Clear();
        if (Tbl18SuperclassesList != null)
        {
            foreach (var item in Tbl18SuperclassesList)
            {
                SuperclassItems.Add(item);
            }
            if (Tbl18SuperclassesList.Count == 0)
            {
                return;
            }

            if (SuperclassSelected == null && Tbl18SuperclassesList.Count != 0)
            {
                SuperclassSelected = SuperclassItems.FirstOrDefault()!;
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
  
