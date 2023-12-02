using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using ATIS.WinUi.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Tbl18Superclass = ATIS.WinUi.Models.Tbl18Superclass;

//    Tbl15SubdivisionsViewModel Skriptdatum:  28.03.2023  12:32    

namespace ATIS.WinUi.ViewModels.Database;

public class Tbl15SubdivisionsViewModel : ObservableObject
{

    #region [Private Data Members]
    private readonly IDataService _dataService;
    private static readonly AtisDbContext Context = new();
    public ObservableCollection<Tbl15Subdivision> SubdivisionItems { get; } = new();
    public ObservableCollection<Tbl18Superclass> SuperclassItems { get; } = new();

    public ObservableCollection<Tbl09Division> DivisionItems { get; } = new();

    public ObservableCollection<Tbl90Reference> ReferenceExpertItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceSourceItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceAuthorItems { get; } = new();
    public ObservableCollection<Tbl93Comment> CommentItems { get; } = new();
    private readonly AllDialogs _allDialogs = new();
    #endregion [Private Data Members]      

    #region [Constructor]
    public Tbl15SubdivisionsViewModel(IDataService dataService)
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

        Tbl09DivisionsAllList ??= new ObservableCollection<Tbl09Division>();
        Tbl09DivisionsAllList = _dataService.GetTbl09DivisionsCollectionOrderByDivisionName();
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



    #region [Commands Subdivision]

    public ICommand GetSubdivisionsByNameOrIdCommand => new RelayCommand(execute: delegate
         { var task = GetSubdivisionsByNameOrId_Executed(SearchSubdivisionName); });

    public ICommand AddSubdivisionCommand => new RelayCommand<string>(AddSubdivision_Executed);
    public ICommand CopySubdivisionCommand => new RelayCommand<string>(CopySubdivision_Executed);

    public ICommand DeleteSubdivisionCommand => new RelayCommand(execute: delegate { DeleteSubdivision_Executed(SearchSubdivisionName); });

    public ICommand SaveSubdivisionCommand => new RelayCommand(execute: delegate { var task = SaveSubdivision_Executed(SearchSubdivisionName); });
    public ICommand RefreshSubdivisionServerCommand => new RelayCommand(execute: delegate { RefreshSubdivisionServer_Executed(SearchSubdivisionName); });

    #endregion [Commands Subdivision]       

    #region [Methods Tbl15Subdivision]

    private async Task GetSubdivisionsByNameOrId_Executed(string? parm)
    {
        IsLoading = true;
        SubdivisionStartModify();
        Tbl09DivisionsList?.Clear();
        Tbl15SubdivisionsList?.Clear();
        Tbl18SuperclassesList?.Clear();
        Tbl90ReferenceExpertsList?.Clear();
        Tbl90ReferenceSourcesList?.Clear();
        Tbl90ReferenceAuthorsList?.Clear();
        Tbl93CommentsList?.Clear();

        DivisionItems.Clear();
        SubdivisionItems.Clear();
        SuperclassItems.Clear();
        ReferenceAuthorItems.Clear();
        ReferenceSourceItems.Clear();
        ReferenceExpertItems.Clear();
        CommentItems.Clear();

        Tbl15SubdivisionsList ??= new ObservableCollection<Tbl15Subdivision>();
        Tbl15SubdivisionsList = await _dataService.GetTbl15SubdivisionsCollectionOrderBySubdivisionNameFromSearchNameOrId(parm!);

        if (Tbl15SubdivisionsList is { Count: 0 })
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        SubdivisionDataSetCount = Tbl15SubdivisionsList.Count;
        RefreshSubdivisionItems();

        SelectedMainDetailTabIndex = 1;
        IsLoading = false;
    }

    private async void AddSubdivision_Executed(string? parm)
    {
        SubdivisionStartEdit();
        SubdivisionStartNew();

        //Id search for first Dataset of Tbl09DivisionsList
        var single = await _dataService.GetDivisionSingleFirstDataset();
        var id = single.DivisionId;

        Tbl15SubdivisionsList ??= new ObservableCollection<Tbl15Subdivision>();
        Tbl15SubdivisionsList.Insert(index: 0, item: new Tbl15Subdivision { SubdivisionName = "New", DivisionId = id });

        RefreshSubdivisionItems();
    }

    private async void CopySubdivision_Executed(string? parm)
    {
        SubdivisionStartEdit();
        SubdivisionStartNew();

        Tbl15SubdivisionsList = await _dataService.CopySubdivision(SubdivisionSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshSubdivisionItems();
    }

    private async void DeleteSubdivision_Executed(string? parm)
    {

        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(SubdivisionSelected!.SubdivisionName!))
        {
            //necessary to delete before
            await _dataService.DeleteConnectedSuperclassesWithSubdivision(SubdivisionSelected);
            await _dataService.DeleteConnectedSubdivisionReferences(SubdivisionSelected);
            await _dataService.DeleteConnectedSubdivisionComments(SubdivisionSelected);

            var ret = _dataService.DeleteSubdivision(SubdivisionSelected);
            if (!await ret)
            {
                return;
            }

            Tbl15SubdivisionsList = await _dataService.GetTbl15SubdivisionsCollectionOrderBySubdivisionNameFromSearchNameOrId(parm!);

            RefreshSubdivisionItems();
        }
    }

    private async Task SaveSubdivision_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(SubdivisionSelected?.SubdivisionName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl15SubdivisionsList != null)
        {

            var iNdx = Tbl15SubdivisionsList.IndexOf(Tbl15SubdivisionsList.First(t =>
                 t.SubdivisionName == SubdivisionSelected.SubdivisionName));

            var ret = _dataService.SaveSubdivision(SubdivisionSelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl15SubdivisionsList = await _dataService.GetLastDatasetInTbl15Subdivisions();
                RefreshSubdivisionItems();
            }
            else
            {
                if (SubdivisionSelected.SubdivisionId == 0) //new
                {
                    Tbl15SubdivisionsList = await _dataService.GetLastDatasetInTbl15Subdivisions();
                    RefreshSubdivisionItems();
                }
                else
                {
                    Tbl15SubdivisionsList = await _dataService.GetTbl15SubdivisionsCollectionOrderBySubdivisionNameFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl15SubdivisionsList!.Count)
                    {
                        SubdivisionItems.Clear();
                        foreach (var item in Tbl15SubdivisionsList)
                        {
                            SubdivisionItems.Add(item);
                        }

                        SubdivisionSelected = Tbl15SubdivisionsList[iNdx];
                    }
                }
            }
        }
        SubdivisionDataSetCount = Tbl15SubdivisionsList!.Count;
        SubdivisionCancelEditsAsync();
    }

    private async void RefreshSubdivisionServer_Executed(string? parm)
    {
        Tbl15SubdivisionsList = await _dataService.GetTbl15SubdivisionsCollectionOrderBySubdivisionNameFromSearchNameOrId(parm!);

        SubdivisionDataSetCount = Tbl15SubdivisionsList.Count;
        RefreshSubdivisionItems();
    }

    public void SubdivisionStartEdit() => IsInEdit = true;
    public void SubdivisionStartModify() => IsModified = true;
    public void SubdivisionStartNew() => IsNewSubdivision = true;
    public event EventHandler AddNewSubdivisionCanceled = null!;
    public void SubdivisionCancelEditsAsync()
    {
        if (IsNewSubdivision)
        {
            IsInEdit = false;
            AddNewSubdivisionCanceled?.Invoke(this, EventArgs.Empty);
            IsNewSubdivision = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }

    #endregion [Methods Tbl15Subdivision]    




    //    Part 2    


    #region "Public Commands Connect <== Tbl09Division"                 

    public ICommand SaveDivisionCommand => new RelayCommand<string>(SaveDivision_Executed);
    public ICommand RefreshDivisionServerCommand => new RelayCommand(execute: delegate { RefreshDivisionServer_Executed(); });

    private async void SaveDivision_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(DivisionSelected.DivisionName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (DivisionSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.SaveDivision(DivisionSelected);

        if (!await ret)
        {
            return;
        }

        Tbl09DivisionsList = _dataService.GetTbl09DivisionsCollectionOrderByDivisionNameFromDivisionId(SubdivisionSelected.DivisionId);
        RefreshDivisionItems();
        DivisionCancelEditsAsync();
    }

    private void RefreshDivisionServer_Executed()
    {
        Tbl03RegnumsAllList ??= new ObservableCollection<Tbl03Regnum>();
        Tbl03RegnumsAllList = _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnum();

        Tbl09DivisionsList = _dataService.GetTbl09DivisionsCollectionOrderByDivisionNameFromDivisionId(SubdivisionSelected.DivisionId);

        DivisionDataSetCount = Tbl09DivisionsList.Count;

        RefreshDivisionItems();
    }
    public void DivisionStartEdit() => IsInEdit = true;
    public void DivisionStartModify() => IsModified = true;
    public void DivisionCancelEditsAsync()
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
    public ICommand RefreshSuperclassServerCommand => new RelayCommand(execute: delegate { RefreshSuperclassServer_Executed(SuperclassSelected!.SubdivisionId); });

    #endregion [Public Commands Connect ==> Tbl18Superclass]    

    #region [Public Methods Connect ==> Tbl18Superclass]                   

    private void AddSuperclass_Executed(string? parm)
    {
        SuperclassStartEdit();
        SuperclassStartNew();
        Tbl18SuperclassesList.Insert(0, new Tbl18Superclass { SuperclassName = "New", SubdivisionId = SubdivisionSelected.SubdivisionId });

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
        SuperclassSelected.SubdivisionId = SubdivisionSelected.SubdivisionId;  //combo vorbelegen

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

        Tbl18SuperclassesList = _dataService.GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSubdivisionId(SuperclassSelected.SubdivisionId);

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
                if (SubdivisionSelected != null)
                {
                    SuperclassSelected.SubdivisionId = SubdivisionSelected.SubdivisionId;

                    //Search for SuperclassSelected.SubphylumID with Animalia#Regnum#  Special
                    if (Context.Tbl12Subphylums != null)
                    {
                        var plantaeRegnum = Context.Tbl12Subphylums.FirstOrDefault(e => e.SubphylumName == "Plantae#Regnum#");
                        if (plantaeRegnum != null)
                        {
                            SuperclassSelected.SubphylumId = plantaeRegnum.SubphylumId;
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
                        Tbl18SuperclassesList = _dataService.GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSubdivisionId(SubdivisionSelected.SubdivisionId);
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

    private void RefreshSuperclassServer_Executed(int id)
    {
        Tbl18SuperclassesList ??= new ObservableCollection<Tbl18Superclass>();
        Tbl18SuperclassesList = _dataService.GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSubdivisionId(id);

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


    #region [Commands Subdivision ==> Tbl90Reference Author]
    public ICommand AddReferenceAuthorCommand => new RelayCommand<string>(AddReferenceAuthor_Executed);
    public ICommand CopyReferenceAuthorCommand => new RelayCommand<string>(CopyReferenceAuthor_Executed);
    public ICommand DeleteReferenceAuthorCommand => new RelayCommand<string>(DeleteReferenceAuthor_Executed);
    public ICommand SaveReferenceAuthorCommand => new RelayCommand<string>(SaveReferenceAuthor_Executed);
    public ICommand RefreshReferenceAuthorServerCommand => new RelayCommand<string>(RefreshReferenceAuthorServer_Executed);
    #endregion [Commands Subdivision ==> Tbl90Reference Author]                

    #region [Methods Subdivision ==> Tbl90Reference Author]

    private void AddReferenceAuthor_Executed(string? parm)
    {
        ReferenceAuthorStartEdit();
        ReferenceAuthorStartNew();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

        Tbl90ReferenceAuthorsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", SubdivisionId = SubdivisionSelected!.SubdivisionId });

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
        ReferenceAuthorSelected.SubdivisionId = SubdivisionSelected.SubdivisionId; //combo vorbelegen

        Tbl90ReferenceAuthorsList = await _dataService.CopyReferenceSubdivision(ReferenceAuthorSelected, "Author");

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

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubdivisionIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.SubdivisionId);
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

            if (SubdivisionSelected != null)
            {
                ReferenceAuthorSelected.SubdivisionId = SubdivisionSelected.SubdivisionId;
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
                Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubdivisionIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.SubdivisionId);
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

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubdivisionIdAndRefSourceIdIsNullAndRefExpertIdIsNull(SubdivisionSelected.SubdivisionId);

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
    #endregion [Methods Subdivision ==> Tbl90Reference Author]                   

    #region [Commands Subdivision ==> Tbl90Reference Source]  
    public ICommand AddReferenceSourceCommand => new RelayCommand<string>(AddReferenceSource_Executed);
    public ICommand CopyReferenceSourceCommand => new RelayCommand<string>(CopyReferenceSource_Executed);
    public ICommand DeleteReferenceSourceCommand => new RelayCommand<string>(DeleteReferenceSource_Executed);
    public ICommand SaveReferenceSourceCommand => new RelayCommand<string>(SaveReferenceSource_Executed);
    public ICommand RefreshReferenceSourceServerCommand => new RelayCommand<string>(RefreshReferenceSourceServer_Executed);
    #endregion [Commands Subdivision ==> Tbl90Reference Source]         

    #region [Methods Subdivision ==> Tbl90Reference Source]      

    private void AddReferenceSource_Executed(string? parm)
    {
        ReferenceSourceStartEdit();
        ReferenceSourceStartNew();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

        Tbl90ReferenceSourcesList.Insert(index: 0, item: new Tbl90Reference { Info = "New", SubdivisionId = SubdivisionSelected!.SubdivisionId });
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
        if (SubdivisionSelected != null)
        {
            ReferenceSourceSelected.SubdivisionId = SubdivisionSelected.SubdivisionId; //combo vorbelegen
        }
        Tbl90ReferenceSourcesList = await _dataService.CopyReferenceSubdivision(ReferenceSourceSelected, "Source");
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

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubdivisionIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.SubdivisionId);
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

            if (SubdivisionSelected != null)
            {
                ReferenceSourceSelected.SubdivisionId = SubdivisionSelected.SubdivisionId;
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
                Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubdivisionIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.SubdivisionId);
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

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubdivisionIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(SubdivisionSelected.SubdivisionId);

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
    #endregion [Methods Subdivision ==> Tbl90Reference Source]           

    #region [Commands Subdivision ==> Tbl90Reference Expert]       
    public ICommand AddReferenceExpertCommand => new RelayCommand<string>(AddReferenceExpert_Executed);
    public ICommand CopyReferenceExpertCommand => new RelayCommand<string>(CopyReferenceExpert_Executed);
    public ICommand DeleteReferenceExpertCommand => new RelayCommand<string>(DeleteReferenceExpert_Executed);
    public ICommand SaveReferenceExpertCommand => new RelayCommand<string>(SaveReferenceExpert_Executed);
    public ICommand RefreshReferenceExpertServerCommand => new RelayCommand<string>(RefreshReferenceExpertServer_Executed);
    #endregion [Commands Subdivision ==> Tbl90Reference Expert]                    

    #region [Methods Subdivision ==> Tbl90Reference Expert]                 

    private void AddReferenceExpert_Executed(string? parm)
    {
        ReferenceExpertStartEdit();
        ReferenceExpertStartNew();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

        Tbl90ReferenceExpertsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", SubdivisionId = SubdivisionSelected!.SubdivisionId });
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
        if (SubdivisionSelected != null)
        {
            ReferenceExpertSelected.SubdivisionId = SubdivisionSelected.SubdivisionId; //combo vorbelegen
        }
        Tbl90ReferenceExpertsList = await _dataService.CopyReferenceSubdivision(ReferenceExpertSelected, "Expert");
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

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubdivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.SubdivisionId);
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
            if (SubdivisionSelected != null)
            {
                ReferenceExpertSelected.SubdivisionId = SubdivisionSelected.SubdivisionId;
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
                Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubdivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.SubdivisionId);
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

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubdivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(SubdivisionSelected.SubdivisionId);

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
    #endregion [Methods Subdivision ==> Tbl90Reference Expert]                                 

    #region [Commands Subdivision ==> Tbl93Comments]         
    public ICommand AddCommentCommand => new RelayCommand<string>(AddComment_Executed);
    public ICommand CopyCommentCommand => new RelayCommand<string>(CopyComment_Executed);
    public ICommand DeleteCommentCommand => new RelayCommand<string>(DeleteComment_Executed);
    public ICommand SaveCommentCommand => new RelayCommand<string>(SaveComment_Executed);
    public ICommand RefreshCommentServerCommand => new RelayCommand<string>(RefreshCommentServer_Executed);
    #endregion [Commands Subdivision ==> Tbl93Comments]           


    #region [Methods Subdivision ==> Tbl93Comments]        

    private void AddComment_Executed(string? parm)
    {
        CommentStartEdit();
        CommentStartNew();
        Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = "New", SubdivisionId = SubdivisionSelected!.SubdivisionId });

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
        if (SubdivisionSelected != null)
        {
            CommentSelected.SubdivisionId = SubdivisionSelected.SubdivisionId;  //combo vorbelegen
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

        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromSubdivisionId(CommentSelected.SubdivisionId);
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
                if (SubdivisionSelected != null)
                {
                    CommentSelected.SubdivisionId = SubdivisionSelected.SubdivisionId;

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
                        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromSubdivisionId(SubdivisionSelected.SubdivisionId);
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
        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromSubdivisionId(SubdivisionSelected.SubdivisionId);

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
    #endregion [Methods Subdivision ==> Tbl93Comments]                             


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
                if (SubdivisionSelected != null)
                {
                    IsLoading = true;
                    DivisionStartModify();
                    Tbl03RegnumsAllList = _dataService.GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnum();

                    Tbl09DivisionsList = _dataService.GetTbl09DivisionsCollectionOrderByDivisionNameFromDivisionId(SubdivisionSelected.DivisionId);

                    DivisionDataSetCount = Tbl09DivisionsList.Count;
                    RefreshDivisionItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 1)
            {
            }

            if (_selectedMainDetailTabIndex == 2)
            {
                if (SubdivisionSelected != null)
                {
                    IsLoading = true;
                    SuperclassStartModify();
                    Tbl12SubphylumsAllList = _dataService.GetTbl12SubphylumsCollectionOrderBySubphylumName();
                    Tbl15SubdivisionsAllList = _dataService.GetTbl15SubdivisionsCollectionOrderBySubdivisionName();

                    Tbl18SuperclassesList = _dataService.GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSubdivisionId(SubdivisionSelected.SubdivisionId);

                    SuperclassDataSetCount = Tbl18SuperclassesList.Count;
                    RefreshSuperclassItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 3)
            {
                if (SubdivisionSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubdivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(SubdivisionSelected.SubdivisionId);

                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 4)
            {
                if (SubdivisionSelected != null)
                {
                    IsLoading = true;
                    CommentStartModify();
                    Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromSubdivisionId(SubdivisionSelected.SubdivisionId);

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
                if (SubdivisionSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubdivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(SubdivisionSelected.SubdivisionId);
                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 1)
            {
                if (SubdivisionSelected != null)
                {
                    IsLoading = true;
                    ReferenceSourceStartModify();
                    Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

                    Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubdivisionIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(SubdivisionSelected.SubdivisionId);

                    ReferenceSourceDataSetCount = Tbl90ReferenceSourcesList.Count;
                    RefreshReferenceSourceItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 2)
            {
                if (SubdivisionSelected != null)
                {
                    IsLoading = true;
                    ReferenceAuthorStartModify();
                    Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

                    Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubdivisionIdAndRefSourceIdIsNullAndRefExpertIdIsNull(SubdivisionSelected.SubdivisionId);

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

    private Tbl09Division _divisionSelected = null!;
    public Tbl09Division DivisionSelected
    {
        get => _divisionSelected;
        set => SetProperty(ref _divisionSelected, value);
    }

    private Tbl15Subdivision _subdivisionSelected = null!;
    public Tbl15Subdivision SubdivisionSelected
    {
        get => _subdivisionSelected;
        set => SetProperty(ref _subdivisionSelected, value);
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
    public string SearchSubdivisionName { get; set; } = null!;

    private ObservableCollection<Tbl03Regnum>? _tbl03RegnumsAllList;
    public ObservableCollection<Tbl03Regnum>? Tbl03RegnumsAllList
    {
        get => _tbl03RegnumsAllList;
        set
        {
            _tbl03RegnumsAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl09Division> _tbl09DivisionsList = null!;
    public ObservableCollection<Tbl09Division> Tbl09DivisionsList
    {
        get => _tbl09DivisionsList;
        set
        {
            _tbl09DivisionsList = value; OnPropertyChanged();
        }
    }
    private ObservableCollection<Tbl09Division>? _tbl09DivisionsAllList;
    public ObservableCollection<Tbl09Division>? Tbl09DivisionsAllList
    {
        get => _tbl09DivisionsAllList;
        set
        {
            _tbl09DivisionsAllList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsList = null!;
    public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsList
    {
        get => _tbl15SubdivisionsList;
        set
        {
            _tbl15SubdivisionsList = value; OnPropertyChanged();
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

    private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsAllList = null!;
    public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsAllList
    {
        get => _tbl12SubphylumsAllList;
        set
        {
            _tbl12SubphylumsAllList = value; OnPropertyChanged();
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
    private int _divisionDataSetCount;
    public int DivisionDataSetCount
    {
        get => _divisionDataSetCount;
        set
        {
            _divisionDataSetCount = value; OnPropertyChanged();
        }
    }

    private int _subdivisionDataSetCount;
    public int SubdivisionDataSetCount
    {
        get => _subdivisionDataSetCount;
        set
        {
            _subdivisionDataSetCount = value; OnPropertyChanged();
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

    private bool _isNewSubdivision;
    public bool IsNewSubdivision
    {
        get => _isNewSubdivision;
        set => SetProperty(ref _isNewSubdivision, value);
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

    private void RefreshSubdivisionItems()
    {
        SubdivisionItems.Clear();
        if (Tbl15SubdivisionsList != null)
        {
            foreach (var item in Tbl15SubdivisionsList)
            {
                SubdivisionItems.Add(item);
            }
            if (Tbl15SubdivisionsList.Count == 0)
            {
                return;
            }

            if (SubdivisionSelected == null && Tbl15SubdivisionsList.Count != 0)
            {
                SubdivisionSelected = SubdivisionItems.FirstOrDefault()!;
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

